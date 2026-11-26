using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A WebSocket session for the omni realtime API (qwen3.8-omni-flash-realtime,
/// qwen3.5-omni-plus-realtime, qwen3.5-omni-flash-realtime). The session talks to the
/// /api-ws/v1/realtime endpoint with event-based messages: configure it with
/// <see cref="UpdateSessionAsync"/>, append the user audio with AppendAudioAsync and video frames
/// with <see cref="AppendImageAsync"/> (the server detects the speech boundaries and triggers the
/// response in the VAD mode; commit with <see cref="CommitAudioAsync"/> and trigger with
/// <see cref="CreateResponseAsync"/> in the manual mode), write back function call results with
/// <see cref="CreateItemAsync"/>, answer MCP approval requests with
/// <see cref="RespondToMcpApprovalAsync"/>, and consume the streamed results via
/// <see cref="GetEventsAsync"/> or <see cref="GetAudioAsync"/>. Create it with
/// <see cref="IDashScopeClient.CreateOmniRealtimeSocketSessionAsync"/>, which connects the
/// underlying websocket and waits for the session.created event. The session ends when it is
/// disposed - there is no session.finish event.
/// </summary>
public sealed class OmniRealtimeSocketSession : IDisposable
{
    private const int ChunkSize = 3200;

    private static readonly UnboundedChannelOptions ChannelOptions = new()
    {
        SingleWriter = true,
        SingleReader = true,
        AllowSynchronousContinuations = true
    };

    private readonly IClientWebSocket _socket;
    private readonly Channel<OmniRealtimeServerEvent> _eventOutput = Channel.CreateUnbounded<OmniRealtimeServerEvent>(ChannelOptions);
    private readonly Channel<byte> _audioOutput = Channel.CreateUnbounded<byte>(ChannelOptions);
    // cancelled once the connection is over, the methods waiting for a server event give up on it
    private readonly CancellationTokenSource _ended = new();
    private int _endedSignalled;
    private TaskCompletionSource<OmniRealtimeSession> _sessionCreatedSignal = NewSignal<OmniRealtimeSession>();
    private TaskCompletionSource<OmniRealtimeSession> _sessionUpdatedSignal = NewSignal<OmniRealtimeSession>();
    private TaskCompletionSource<string> _committedSignal = NewSignal<string>();
    private TaskCompletionSource<bool> _clearedSignal = NewSignal<bool>();
    private TaskCompletionSource<OmniRealtimeResponse> _responseDoneSignal = NewSignal<OmniRealtimeResponse>();
    // whether a method waits for the response.done event: a cancel joins that wait instead of
    // replacing it, both get the cancelled response
    private bool _responseDoneWaitPending;
    private OmniRealtimeItemCreatedWait? _itemCreatedWait;
    private TaskCompletionSource<OmniRealtimeError> _errorSignal = NewSignal<OmniRealtimeError>();

    internal OmniRealtimeSocketSession(IClientWebSocket socket)
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
    internal async Task<OmniRealtimeSession> StartAsync(CancellationToken cancellationToken = default)
    {
        _ = ReceiveMessagesAsync(cancellationToken);
        return await WaitAsync(_sessionCreatedSignal, "session.created", cancellationToken);
    }

    /// <summary>
    /// Send a session.update event and wait for the session.updated confirmation. The server
    /// uses the default configuration when this is never called. See
    /// <see cref="OmniRealtimeSession.TurnDetection"/> for the manual mode.
    /// </summary>
    /// <param name="session">The session configuration to apply.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The applied session configuration returned by the server.</returns>
    /// <exception cref="DashScopeException">Throws when the server rejects the configuration.
    /// That ends the session: the server closes the connection right after the error event, so
    /// create a new session for the next attempt.</exception>
    public async Task<OmniRealtimeSession> UpdateSessionAsync(
        OmniRealtimeSession session,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(session);
        _sessionUpdatedSignal = NewSignal<OmniRealtimeSession>();
        // TurnDetection and InputAudioTranscription are always written, a null as an explicit
        // turn_detection / input_audio_transcription null; only the Omit instances (the defaults)
        // leave the members out
        var sessionJson = JsonSerializer.SerializeToNode(session, DashScopeDefaults.SerializationOptions)!;
        if (ReferenceEquals(session.TurnDetection, OmniRealtimeTurnDetection.Omit))
        {
            sessionJson.AsObject().Remove("turn_detection");
        }

        if (ReferenceEquals(session.InputAudioTranscription, OmniRealtimeTranscription.Omit))
        {
            sessionJson.AsObject().Remove("input_audio_transcription");
        }

        await SendEventAsync(
            new Dictionary<string, JsonNode?>
            {
                ["event_id"] = NewEventId(),
                ["type"] = "session.update",
                ["session"] = sessionJson
            },
            cancellationToken);
        return await WaitAsync(_sessionUpdatedSignal, "session.updated", cancellationToken);
    }

    /// <summary>
    /// Append audio to the input buffer, sent as base64 encoded chunks inside
    /// input_audio_buffer.append events. The server sends no acknowledgement for them. In the VAD
    /// mode the server only detects the end of the speech after some silence, so keep appending
    /// audio (a silent microphone does) after the user stopped speaking instead of stopping the
    /// stream.
    /// </summary>
    /// <param name="audio">The audio chunk (16kHz 16bit mono pcm, or what the session
    /// configures).</param>
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
        ArgumentNullException.ThrowIfNull(audio);
        var buffer = new byte[ChunkSize];
        int read;
        while ((read = await audio.ReadAsync(buffer, cancellationToken)) > 0)
        {
            await AppendAudioAsync(buffer.AsMemory(0, read), cancellationToken);
        }
    }

    /// <summary>
    /// Append a video frame to the image buffer, sent as a base64 encoded jpg / jpeg inside an
    /// input_image_buffer.append event. At most 256 KB after the encoding, about one frame per
    /// second; at least one audio append must precede the image appends, and the frames of a turn
    /// must be sent before it ends (before the commit in the manual mode, before the speech stops
    /// in the VAD mode) to belong to it. The server sends no acknowledgement for them.
    /// </summary>
    /// <param name="image">The jpg / jpeg frame.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public Task AppendImageAsync(ReadOnlyMemory<byte> image, CancellationToken cancellationToken = default)
        => SendEventAsync(
            new { event_id = NewEventId(), type = "input_image_buffer.append", image = Convert.ToBase64String(image.Span) },
            cancellationToken);

    /// <summary>
    /// Commit the input buffer and wait for the input_audio_buffer.committed confirmation, which
    /// turns the buffered audio (and images) into a user message. Manual mode only - in the VAD
    /// mode the server commits the audio itself. An empty buffer throws a
    /// <see cref="DashScopeException"/> (the server answers with an error event), the session
    /// stays usable.
    /// Trigger the response with <see cref="CreateResponseAsync"/> afterwards.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The id of the created user message item.</returns>
    public async Task<string> CommitAudioAsync(CancellationToken cancellationToken = default)
    {
        _committedSignal = NewSignal<string>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "input_audio_buffer.commit" },
            cancellationToken);
        return await WaitAsync(_committedSignal, "input_audio_buffer.committed", cancellationToken);
    }

    /// <summary>
    /// Clear the unsubmitted audio (and images) of the input buffer and wait for the
    /// input_audio_buffer.cleared confirmation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public async Task ClearAudioBufferAsync(CancellationToken cancellationToken = default)
    {
        _clearedSignal = NewSignal<bool>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "input_audio_buffer.clear" },
            cancellationToken);
        await WaitAsync(_clearedSignal, "input_audio_buffer.cleared", cancellationToken);
    }

    /// <summary>
    /// Create a conversation item and wait for the conversation.item.created confirmation of an
    /// item of the same type and role: a user message item (input_text content) to inject context
    /// or to talk to the model in text, or a function_call_output item to write back the result of
    /// a function call - then trigger the next response with <see cref="CreateResponseAsync"/>.
    /// Answer an MCP approval request with <see cref="RespondToMcpApprovalAsync"/> instead, the
    /// server does not confirm that one.
    /// </summary>
    /// <param name="item">The item to create.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The created item confirmation event.</returns>
    public async Task<OmniRealtimeServerEvent> CreateItemAsync(
        OmniRealtimeItem item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (item.Type == "mcp_approval_response")
        {
            throw new ArgumentException(
                "Answer an MCP approval request with RespondToMcpApprovalAsync instead, the server does not confirm this one.",
                nameof(item));
        }

        var wait = new OmniRealtimeItemCreatedWait(NewSignal<OmniRealtimeServerEvent>(), item.Type, item.Role);
        _itemCreatedWait = wait;
        await SendEventAsync(
            new { event_id = NewEventId(), type = "conversation.item.create", item },
            cancellationToken);
        return await WaitAsync(wait.Signal, "conversation.item.created", cancellationToken);
    }

    /// <summary>
    /// Answer an MCP approval request (a conversation.item.created event whose item type is
    /// mcp_approval_request) and let the server execute the call - or fail it without executing
    /// it when <paramref name="approve"/> is false. The server sends no confirmation, the call
    /// goes on in the response.mcp_call.* events. An approval left unanswered times out after a
    /// minute and fails the call.
    /// </summary>
    /// <param name="approvalRequestId">The id of the approval request, the ItemId of the
    /// conversation.item.created event carrying it.</param>
    /// <param name="approve">Whether to approve the call.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public Task RespondToMcpApprovalAsync(
        string approvalRequestId,
        bool approve = true,
        CancellationToken cancellationToken = default)
        => SendEventAsync(
            new
            {
                event_id = NewEventId(),
                type = "conversation.item.create",
                item = new OmniRealtimeItem
                {
                    Type = "mcp_approval_response",
                    ApprovalRequestId = approvalRequestId,
                    Approve = approve
                }
            },
            cancellationToken);

    /// <summary>
    /// Trigger a model response and wait for the response.done event, which arrives after all the
    /// streamed deltas. Required in the manual mode; in the VAD mode the server triggers the
    /// response itself, a manual one is for the turns that follow a tool result. An error event
    /// the server answers with throws a <see cref="DashScopeException"/>, the session stays
    /// usable in that case. A response that
    /// asks for a function call ends with the function_call items in the output: execute them,
    /// write the results back with <see cref="CreateItemAsync"/> and trigger the next response
    /// with this method. For a response that executed MCP tools, trigger the next response to let
    /// the model answer based on the tool results.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The complete response.</returns>
    public async Task<OmniRealtimeResponse> CreateResponseAsync(CancellationToken cancellationToken = default)
    {
        _responseDoneSignal = NewSignal<OmniRealtimeResponse>();
        _responseDoneWaitPending = true;
        await SendEventAsync(
            new { event_id = NewEventId(), type = "response.create" },
            cancellationToken);
        return await WaitAsync(_responseDoneSignal, "response.done", cancellationToken);
    }

    /// <summary>
    /// Cancel the response in progress and wait for the response.done event with the cancelled
    /// status. The text already generated is kept in the item list, and a
    /// <see cref="CreateResponseAsync"/> still waiting for the response completes with the
    /// cancelled response as well. When no response is in progress the server answers nothing at
    /// all, so the call waits until it is cancelled or the session ends.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The cancelled response.</returns>
    public async Task<OmniRealtimeResponse> CancelResponseAsync(CancellationToken cancellationToken = default)
    {
        if (_responseDoneWaitPending == false)
        {
            _responseDoneSignal = NewSignal<OmniRealtimeResponse>();
            _responseDoneWaitPending = true;
        }

        await SendEventAsync(
            new { event_id = NewEventId(), type = "response.cancel" },
            cancellationToken);
        return await WaitAsync(_responseDoneSignal, "response.done", cancellationToken);
    }

    /// <summary>
    /// Get the server event stream. Every event is also delivered here, including the ones
    /// that the send methods of this session wait for internally.
    /// </summary>
    public IAsyncEnumerable<OmniRealtimeServerEvent> GetEventsAsync() => _eventOutput.Reader.ReadAllAsync();

    /// <summary>
    /// Get the synthesized audio of the responses as a continuous byte stream, decoded from
    /// the base64 payloads of the response.audio.delta events (24kHz 16bit mono pcm, or what the
    /// session configures).
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

    private async Task<TResult> WaitAsync<TResult>(
        TaskCompletionSource<TResult> signal,
        string eventName,
        CancellationToken cancellationToken)
    {
        await using var endedRegistration = _ended.Token.Register(
            () => signal.TrySetException(
                new DashScopeException(null, 0, null, $"The realtime session ended{DescribeClose()} before the {eventName} event arrived.")));
        await using var registration = cancellationToken.Register(
            () => signal.TrySetCanceled(cancellationToken));
        // the error event wins even when the connection ends right behind it, its detail is
        // more helpful than "the session ended"; every wait in flight sees the same error
        var errorTask = _errorSignal.Task;
        await Task.WhenAny(signal.Task, errorTask);
        if (errorTask.IsCompleted)
        {
            var error = await errorTask;
            _errorSignal = NewSignal<OmniRealtimeError>();
            // not every error carries a code, e.g. committing an empty buffer only names its type
            throw new DashScopeException(
                null,
                400,
                new DashScopeError { Code = error.Code ?? error.Type ?? string.Empty, Message = error.Message ?? string.Empty },
                error.Message ?? "The realtime session received an error event.");
        }

        return await signal.Task;
    }

    /// <summary>
    /// How the server closed the connection, e.g. " (InvalidPayloadData: Invalid modalities: ...)";
    /// empty while it is still open.
    /// </summary>
    private string DescribeClose()
    {
        if (_socket.CloseStatus == null)
        {
            return string.Empty;
        }

        var description = _socket.CloseStatusDescription;
        return string.IsNullOrEmpty(description)
            ? $" ({_socket.CloseStatus})"
            : $" ({_socket.CloseStatus}: {description})";
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
                        return;
                    }

                    await message.WriteAsync(buffer, 0, result.Count, cancellationToken);
                }
                while (result.EndOfMessage == false);

                var serverEvent = JsonSerializer.Deserialize<OmniRealtimeServerEvent>(
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
            // the connection is gone or the session is being disposed, the outputs end below
        }
        finally
        {
            CompleteOutputs();
        }
    }

    private void HandleEvent(OmniRealtimeServerEvent serverEvent)
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
            case "conversation.item.created":
                // the server creates message items on its own as well, only an item of the kind
                // that was sent confirms the conversation.item.create
                var wait = _itemCreatedWait;
                var item = serverEvent.Item;
                if (wait != null && item != null && item.Type == wait.Type && item.Role == wait.Role)
                {
                    wait.Signal.TrySetResult(serverEvent);
                }

                break;
            case "response.done":
                _responseDoneWaitPending = false;
                _responseDoneSignal.TrySetResult(serverEvent.Response!);
                break;
            case "response.audio.delta":
                WriteAudio(serverEvent.Delta);
                break;
            case "error":
                _errorSignal.TrySetResult(serverEvent.Error ?? new OmniRealtimeError());
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

        // fail the waits in flight instead of leaving them pending forever; exactly once, because
        // the receive loop may still get here after Dispose cancelled the token and disposed the source
        if (Interlocked.Exchange(ref _endedSignalled, 1) == 0)
        {
            _ended.Cancel();
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _socket.Dispose();
            CompleteOutputs();
            _ended.Dispose();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }
}
