using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A websocket session for the audio chat API (qwen-audio-3.x-realtime models). The session
/// talks to the /api-ws/v1/realtime endpoint with event-based messages: configure it with
/// <see cref="UpdateSessionAsync"/>, append the user audio with <see cref="AppendAudioAsync"/>
/// (the server detects the speech boundaries and triggers the inference in the server_vad /
/// smart_turn mode; submit with <see cref="CommitAudioAsync"/> and trigger with
/// <see cref="CreateResponseAsync"/> in the push-to-talk mode), inject or manage conversation
/// items with the item methods, and consume the streamed results via <see cref="GetEventsAsync"/>
/// or <see cref="GetAudioAsync"/>. Create it with
/// <see cref="IDashScopeClient.CreateAudioChatSocketSessionAsync"/>, which connects the
/// underlying websocket and waits for the session.created event. The session ends when the
/// connection is disposed.
/// </summary>
public sealed class AudioChatSocketSession : IDisposable
{
    private const int ChunkSize = 3200;

    private static readonly UnboundedChannelOptions ChannelOptions = new()
    {
        SingleWriter = true,
        SingleReader = true,
        AllowSynchronousContinuations = true
    };

    private readonly IClientWebSocket _socket;
    private readonly Channel<AudioChatServerEvent> _eventOutput = Channel.CreateUnbounded<AudioChatServerEvent>(ChannelOptions);
    private readonly Channel<byte> _audioOutput = Channel.CreateUnbounded<byte>(ChannelOptions);
    private TaskCompletionSource<AudioChatSession> _sessionCreatedSignal = NewSignal<AudioChatSession>();
    private TaskCompletionSource<AudioChatSession> _sessionUpdatedSignal = NewSignal<AudioChatSession>();
    private TaskCompletionSource<AudioChatServerEvent> _itemCreatedSignal = NewSignal<AudioChatServerEvent>();
    private TaskCompletionSource<AudioChatItem> _itemRetrievedSignal = NewSignal<AudioChatItem>();
    private TaskCompletionSource<string> _itemDeletedSignal = NewSignal<string>();
    private TaskCompletionSource<string> _committedSignal = NewSignal<string>();
    private TaskCompletionSource<bool> _clearedSignal = NewSignal<bool>();
    private TaskCompletionSource<AudioChatResponse> _responseDoneSignal = NewSignal<AudioChatResponse>();
    private TaskCompletionSource<AudioChatError> _errorSignal = NewSignal<AudioChatError>();

    internal AudioChatSocketSession(IClientWebSocket socket)
    {
        _socket = socket;
    }

    private static TaskCompletionSource<T> NewSignal<T>() => new(TaskCreationOptions.RunContinuationsAsynchronously);

    /// <summary>
    /// Start receiving events on the connected websocket and wait for the session.created event.
    /// The connection itself is established by the pool before the session is created.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The initial session configuration returned by the server.</returns>
    internal async Task<AudioChatSession> StartAsync(CancellationToken cancellationToken = default)
    {
        _ = ReceiveMessagesAsync(cancellationToken);
        return await WaitAsync(_sessionCreatedSignal, cancellationToken);
    }

    /// <summary>
    /// Send a session.update event and wait for the session.updated confirmation. The server
    /// uses the default configuration when this is never called. See
    /// <see cref="AudioChatSession.DisableTurnDetection"/> for switching to the push-to-talk
    /// mode.
    /// </summary>
    /// <param name="session">The session configuration to apply.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The applied session configuration returned by the server.</returns>
    public async Task<AudioChatSession> UpdateSessionAsync(
        AudioChatSession session,
        CancellationToken cancellationToken = default)
    {
        _sessionUpdatedSignal = NewSignal<AudioChatSession>();
        // WhenWritingNull skips null properties; the dictionary and the JsonNode keep the
        // explicit turn_detection null of the push-to-talk mode on the wire.
        var sessionJson = JsonSerializer.SerializeToNode(session, DashScopeDefaults.SerializationOptions)!;
        if (session.DisableTurnDetection)
        {
            sessionJson["turn_detection"] = null;
        }

        await SendEventAsync(
            new Dictionary<string, JsonNode?>
            {
                ["event_id"] = NewEventId(),
                ["type"] = "session.update",
                ["session"] = sessionJson
            },
            cancellationToken);
        return await WaitAsync(_sessionUpdatedSignal, cancellationToken);
    }

    /// <summary>
    /// Append audio to the input buffer, sent as base64 encoded chunks inside
    /// input_audio_buffer.append events. The server sends no acknowledgement for them.
    /// </summary>
    /// <param name="audio">The audio chunk (16kHz 16bit mono pcm).</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public Task AppendAudioAsync(ReadOnlyMemory<byte> audio, CancellationToken cancellationToken = default)
        => SendEventAsync(
            new { event_id = NewEventId(), type = "input_audio_buffer.append", audio = Convert.ToBase64String(audio.Span) },
            cancellationToken);

    /// <summary>
    /// Append audio to the input buffer, reading the stream in 3200 byte chunks. See
    /// <see cref="AppendAudioAsync(ReadOnlyMemory{byte}, CancellationToken)"/> for the details.
    /// </summary>
    /// <param name="audio">The audio stream.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public async Task AppendAudioAsync(Stream audio, CancellationToken cancellationToken = default)
    {
        var buffer = new byte[ChunkSize];
        int read;
        while ((read = await audio.ReadAsync(buffer, cancellationToken)) > 0)
        {
            await AppendAudioAsync(buffer.AsMemory(0, read), cancellationToken);
        }
    }

    /// <summary>
    /// Commit the input buffer and wait for the input_audio_buffer.committed confirmation,
    /// which turns the buffered audio into a user message. Push-to-talk mode only - it is
    /// ignored in the server_vad / smart_turn modes. Trigger the inference with
    /// <see cref="CreateResponseAsync"/> afterwards.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The id of the created user message item.</returns>
    public async Task<string> CommitAudioAsync(CancellationToken cancellationToken = default)
    {
        _committedSignal = NewSignal<string>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "input_audio_buffer.commit" },
            cancellationToken);
        return await WaitAsync(_committedSignal, cancellationToken);
    }

    /// <summary>
    /// Clear the unsubmitted audio of the input buffer and wait for the input_audio_buffer.cleared
    /// confirmation. Push-to-talk mode only.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public async Task ClearAudioBufferAsync(CancellationToken cancellationToken = default)
    {
        _clearedSignal = NewSignal<bool>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "input_audio_buffer.clear" },
            cancellationToken);
        await WaitAsync(_clearedSignal, cancellationToken);
    }

    /// <summary>
    /// Create a conversation item and wait for the conversation.item.created confirmation. Use
    /// it to inject history context (message items) or to write back the output of a function
    /// call (function_call_output items).
    /// </summary>
    /// <param name="item">The item to create.</param>
    /// <param name="previousItemId">Insert the item after this one; append to the end when null.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The created item confirmation event.</returns>
    public async Task<AudioChatServerEvent> CreateItemAsync(
        AudioChatItem item,
        string? previousItemId = null,
        CancellationToken cancellationToken = default)
    {
        _itemCreatedSignal = NewSignal<AudioChatServerEvent>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "conversation.item.create", previous_item_id = previousItemId, item },
            cancellationToken);
        return await WaitAsync(_itemCreatedSignal, cancellationToken);
    }

    /// <summary>
    /// Retrieve a conversation item and wait for the conversation.item.retrieved confirmation.
    /// Audio contents come back with the transcript only, without the raw audio.
    /// </summary>
    /// <param name="itemId">The id of the item to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The retrieved item.</returns>
    public async Task<AudioChatItem> RetrieveItemAsync(string itemId, CancellationToken cancellationToken = default)
    {
        _itemRetrievedSignal = NewSignal<AudioChatItem>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "conversation.item.retrieve", item_id = itemId },
            cancellationToken);
        return await WaitAsync(_itemRetrievedSignal, cancellationToken);
    }

    /// <summary>
    /// Delete a conversation item and wait for the conversation.item.deleted confirmation.
    /// </summary>
    /// <param name="itemId">The id of the item to delete.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The id of the deleted item.</returns>
    public async Task<string> DeleteItemAsync(string itemId, CancellationToken cancellationToken = default)
    {
        _itemDeletedSignal = NewSignal<string>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "conversation.item.delete", item_id = itemId },
            cancellationToken);
        return await WaitAsync(_itemDeletedSignal, cancellationToken);
    }

    /// <summary>
    /// Trigger a model inference and wait for the response.done event, which arrives after all
    /// the streamed deltas. Required in the push-to-talk mode; optional in the server_vad /
    /// smart_turn modes (the server triggers the inference automatically, and a manual
    /// inference is only allowed when none is in progress).
    /// </summary>
    /// <param name="modalities">Overrides the output modalities of this response.</param>
    /// <param name="voice">Overrides the voice of this response.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The complete response.</returns>
    public async Task<AudioChatResponse> CreateResponseAsync(
        List<string>? modalities = null,
        string? voice = null,
        CancellationToken cancellationToken = default)
    {
        _responseDoneSignal = NewSignal<AudioChatResponse>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "response.create", response = new { modalities, voice } },
            cancellationToken);
        return await WaitAsync(_responseDoneSignal, cancellationToken);
    }

    /// <summary>
    /// Cancel the inference in progress and wait for the response.done event with the cancelled
    /// status. The partial text already generated is kept in the item list.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The cancelled response.</returns>
    public async Task<AudioChatResponse> CancelResponseAsync(CancellationToken cancellationToken = default)
    {
        _responseDoneSignal = NewSignal<AudioChatResponse>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "response.cancel" },
            cancellationToken);
        return await WaitAsync(_responseDoneSignal, cancellationToken);
    }

    /// <summary>
    /// Get the server event stream. Every event is also delivered here, including the ones
    /// that the send methods of this session wait for internally.
    /// </summary>
    public IAsyncEnumerable<AudioChatServerEvent> GetEventsAsync() => _eventOutput.Reader.ReadAllAsync();

    /// <summary>
    /// Get the synthesized audio of the responses as a continuous byte stream, decoded from
    /// the base64 payloads of the response.audio.delta events (24kHz 16bit mono pcm).
    /// </summary>
    public IAsyncEnumerable<byte> GetAudioAsync() => _audioOutput.Reader.ReadAllAsync();

    private async Task SendEventAsync(object clientEvent, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(clientEvent, DashScopeDefaults.SerializationOptions);
        await _socket.SendAsync(
            new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)),
            WebSocketMessageType.Text,
            true,
            cancellationToken);
    }

    private static string NewEventId() => Guid.NewGuid().ToString();

    private async Task<TResult> WaitAsync<TResult>(TaskCompletionSource<TResult> signal, CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(
            () => signal.TrySetCanceled(cancellationToken));
        var completed = await Task.WhenAny(signal.Task, _errorSignal.Task);
        if (completed == _errorSignal.Task)
        {
            var error = await _errorSignal.Task;
            _errorSignal = NewSignal<AudioChatError>();
            throw new DashScopeException(
                null,
                400,
                new DashScopeError { Code = error.Code ?? string.Empty, Message = error.Message ?? string.Empty },
                error.Message ?? "The realtime session received an error event.");
        }

        return await signal.Task;
    }

    private async Task ReceiveMessagesAsync(CancellationToken cancellationToken)
    {
        var buffer = new byte[1024 * 4];
        var segment = new ArraySegment<byte>(buffer);
        var message = new MemoryStream();

        try
        {
            while (_socket.CloseStatus == null)
            {
                WebSocketReceiveResult result;
                message.SetLength(0);
                do
                {
                    result = await _socket.ReceiveAsync(segment, cancellationToken);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
                        CompleteOutputs();
                        return;
                    }

                    await message.WriteAsync(buffer, 0, result.Count, cancellationToken);
                }
                while (result.EndOfMessage == false);

                var serverEvent = JsonSerializer.Deserialize<AudioChatServerEvent>(
                    Encoding.UTF8.GetString(message.ToArray()),
                    DashScopeDefaults.SerializationOptions);
                if (serverEvent is null)
                {
                    continue;
                }

                await _eventOutput.Writer.WriteAsync(serverEvent, cancellationToken);
                HandleEvent(serverEvent);
            }
        }
        catch
        {
            // close the session when exception happens.
            CompleteOutputs();
        }

        CompleteOutputs();
    }

    private void HandleEvent(AudioChatServerEvent serverEvent)
    {
        switch (serverEvent.Type)
        {
            case "session.created":
                _sessionCreatedSignal.TrySetResult(serverEvent.Session!);
                break;
            case "session.updated":
                _sessionUpdatedSignal.TrySetResult(serverEvent.Session!);
                break;
            case "conversation.item.created":
                _itemCreatedSignal.TrySetResult(serverEvent);
                break;
            case "conversation.item.retrieved":
                _itemRetrievedSignal.TrySetResult(serverEvent.Item!);
                break;
            case "conversation.item.deleted":
                _itemDeletedSignal.TrySetResult(serverEvent.ItemId ?? string.Empty);
                break;
            case "input_audio_buffer.committed":
                _committedSignal.TrySetResult(serverEvent.ItemId ?? string.Empty);
                break;
            case "input_audio_buffer.cleared":
                _clearedSignal.TrySetResult(true);
                break;
            case "response.done":
                _responseDoneSignal.TrySetResult(serverEvent.Response!);
                break;
            case "response.audio.delta":
                WriteAudio(serverEvent.Delta);
                break;
            case "error":
                _errorSignal.TrySetResult(serverEvent.Error ?? new AudioChatError());
                break;
        }
    }

    private void WriteAudio(string? delta)
    {
        if (string.IsNullOrEmpty(delta))
        {
            return;
        }

        foreach (var b in Convert.FromBase64String(delta))
        {
            _audioOutput.Writer.TryWrite(b);
        }
    }

    private void CompleteOutputs()
    {
        _eventOutput.Writer.TryComplete();
        _audioOutput.Writer.TryComplete();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _socket.Dispose();
            CompleteOutputs();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }
}
