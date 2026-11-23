using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A websocket session for the live translation API (qwen3.5 / qwen3.8-livetranslate-flash-realtime).
/// The session talks to the /api-ws/v1/realtime endpoint with event-based messages: configure
/// it with <see cref="UpdateSessionAsync"/>, append the source audio with
/// <see cref="AppendAudioAsync"/> (and the video frames with <see cref="AppendImageAsync"/>),
/// the server detects the speech, translates it and streams the results - consume them via
/// <see cref="GetEventsAsync"/> or <see cref="GetAudioAsync"/>. Create it with
/// <see cref="IDashScopeClient.CreateLiveTranslateSocketSessionAsync"/>, which connects the
/// underlying websocket and waits for the session.created event. Call <see cref="FinishAsync"/>
/// to let the server finish the remaining translation, then dispose the session.
/// </summary>
public sealed class LiveTranslateSocketSession : IDisposable
{
    private const int ChunkSize = 3200;

    private static readonly UnboundedChannelOptions ChannelOptions = new()
    {
        SingleWriter = true,
        SingleReader = true,
        AllowSynchronousContinuations = true
    };

    private readonly IClientWebSocket _socket;
    private readonly Channel<LiveTranslateServerEvent> _eventOutput = Channel.CreateUnbounded<LiveTranslateServerEvent>(ChannelOptions);
    private readonly Channel<byte> _audioOutput = Channel.CreateUnbounded<byte>(ChannelOptions);
    private TaskCompletionSource<LiveTranslateSession> _sessionCreatedSignal = NewSignal<LiveTranslateSession>();
    private TaskCompletionSource<LiveTranslateSession> _sessionUpdatedSignal = NewSignal<LiveTranslateSession>();
    private TaskCompletionSource<string> _committedSignal = NewSignal<string>();
    private TaskCompletionSource<bool> _clearedSignal = NewSignal<bool>();
    private TaskCompletionSource<bool> _finishedSignal = NewSignal<bool>();
    private TaskCompletionSource<LiveTranslateError> _errorSignal = NewSignal<LiveTranslateError>();

    internal LiveTranslateSocketSession(IClientWebSocket socket)
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
    internal async Task<LiveTranslateSession> StartAsync(CancellationToken cancellationToken = default)
    {
        _ = ReceiveMessagesAsync(cancellationToken);
        return await WaitAsync(_sessionCreatedSignal, cancellationToken);
    }

    /// <summary>
    /// Send a session.update event and wait for the session.updated confirmation. The server
    /// uses the default configuration when this is never called. See
    /// <see cref="LiveTranslateSession.DisableTurnDetection"/> for switching to the manual mode.
    /// </summary>
    /// <param name="session">The session configuration to apply.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The applied session configuration returned by the server.</returns>
    public async Task<LiveTranslateSession> UpdateSessionAsync(
        LiveTranslateSession session,
        CancellationToken cancellationToken = default)
    {
        _sessionUpdatedSignal = NewSignal<LiveTranslateSession>();
        // WhenWritingNull skips null properties; the dictionary and the JsonNode keep the
        // explicit turn_detection null of the manual mode on the wire.
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
    /// <param name="audio">The audio chunk (pcm or opus, see
    /// <see cref="LiveTranslateSession.InputAudioFormat"/>).</param>
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
    /// Append a video frame to the image buffer, sent as a base64 encoded jpg / jpeg inside an
    /// input_image_buffer.append event. At most 2 frames per second, each at most 500 KiB before
    /// the encoding, and at least one audio append must precede the image appends. The server
    /// sends no acknowledgement for them.
    /// </summary>
    /// <param name="image">The jpg / jpeg frame.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public Task AppendImageAsync(ReadOnlyMemory<byte> image, CancellationToken cancellationToken = default)
        => SendEventAsync(
            new { event_id = NewEventId(), type = "input_image_buffer.append", image = Convert.ToBase64String(image.Span) },
            cancellationToken);

    /// <summary>
    /// Commit the input buffer and wait for the input_audio_buffer.committed confirmation, which
    /// also triggers the translation of the submitted audio automatically. Manual mode only -
    /// in the VAD mode the server commits the audio itself.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The id of the created conversation item.</returns>
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
    /// confirmation.
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
    /// Send a session.finish event and wait for the session.finished event. The server finishes
    /// the translation of the remaining audio first, then signals the end of the session.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public async Task FinishAsync(CancellationToken cancellationToken = default)
    {
        _finishedSignal = NewSignal<bool>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "session.finish" },
            cancellationToken);
        await WaitAsync(_finishedSignal, cancellationToken);
    }

    /// <summary>
    /// Get the server event stream. Every event is also delivered here, including the ones
    /// that the send methods of this session wait for internally.
    /// </summary>
    public IAsyncEnumerable<LiveTranslateServerEvent> GetEventsAsync() => _eventOutput.Reader.ReadAllAsync();

    /// <summary>
    /// Get the synthesized audio of the responses as a continuous byte stream, decoded from
    /// the base64 payloads of the response.audio.delta events.
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
            _errorSignal = NewSignal<LiveTranslateError>();
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

                var serverEvent = JsonSerializer.Deserialize<LiveTranslateServerEvent>(
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

    private void HandleEvent(LiveTranslateServerEvent serverEvent)
    {
        switch (serverEvent.Type)
        {
            case "session.created":
                _sessionCreatedSignal.TrySetResult(serverEvent.Session!);
                break;
            case "session.updated":
                _sessionUpdatedSignal.TrySetResult(serverEvent.Session!);
                break;
            case "input_audio_buffer.committed":
                _committedSignal.TrySetResult(serverEvent.ItemId ?? string.Empty);
                break;
            case "input_audio_buffer.cleared":
                _clearedSignal.TrySetResult(true);
                break;
            case "session.finished":
                _finishedSignal.TrySetResult(true);
                CompleteOutputs();
                break;
            case "response.audio.delta":
                WriteAudio(serverEvent.Delta);
                break;
            case "error":
                _errorSignal.TrySetResult(serverEvent.Error ?? new LiveTranslateError());
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
