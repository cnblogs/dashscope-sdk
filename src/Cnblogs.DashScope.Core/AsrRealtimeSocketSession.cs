using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A websocket session for the ASR realtime API (qwen3-asr-flash-realtime models). The session
/// talks to the /api-ws/v1/realtime endpoint with event-based (session.update /
/// input_audio_buffer.append / input_audio_buffer.commit / session.finish) messages, the audio
/// is sent as base64 encoded chunks inside input_audio_buffer.append events. Create it with
/// <see cref="IDashScopeClient.CreateAsrRealtimeSocketSessionAsync"/>, which connects the
/// underlying websocket and waits for the session.created event. The server closes the
/// connection after session.finished, so a session cannot be reused for a second run.
/// </summary>
public sealed class AsrRealtimeSocketSession : IDisposable
{
    private const int ChunkSize = 3200;

    private static readonly UnboundedChannelOptions ChannelOptions = new()
    {
        SingleWriter = true,
        SingleReader = true,
        AllowSynchronousContinuations = true
    };

    private readonly IClientWebSocket _socket;
    private readonly Channel<AsrRealtimeServerEvent> _eventOutput = Channel.CreateUnbounded<AsrRealtimeServerEvent>(ChannelOptions);
    private TaskCompletionSource<AsrRealtimeSession> _sessionCreatedSignal = NewSignal<AsrRealtimeSession>();
    private TaskCompletionSource<AsrRealtimeSession> _sessionUpdatedSignal = NewSignal<AsrRealtimeSession>();
    private TaskCompletionSource<string> _committedSignal = NewSignal<string>();
    private TaskCompletionSource<bool> _finishedSignal = NewSignal<bool>();
    private TaskCompletionSource<AsrRealtimeError> _errorSignal = NewSignal<AsrRealtimeError>();

    internal AsrRealtimeSocketSession(IClientWebSocket socket)
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
    internal async Task<AsrRealtimeSession> StartAsync(CancellationToken cancellationToken = default)
    {
        _ = ReceiveMessagesAsync(cancellationToken);
        return await WaitAsync(_sessionCreatedSignal, cancellationToken);
    }

    /// <summary>
    /// Send a session.update event and wait for the session.updated confirmation. The server
    /// uses the default configuration when this is never called.
    /// </summary>
    /// <param name="session">The session configuration to apply.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The applied session configuration returned by the server.</returns>
    public async Task<AsrRealtimeSession> UpdateSessionAsync(
        AsrRealtimeSession session,
        CancellationToken cancellationToken = default)
    {
        _sessionUpdatedSignal = NewSignal<AsrRealtimeSession>();
        // TurnDetection is always written, a null as an explicit turn_detection null; only the
        // Omit instance (the default) leaves the member out
        var sessionJson = JsonSerializer.SerializeToNode(session, DashScopeDefaults.SerializationOptions)!;
        if (ReferenceEquals(session.TurnDetection, AsrRealtimeTurnDetection.Omit))
        {
            sessionJson.AsObject().Remove("turn_detection");
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
    /// Append audio to the input buffer. In the VAD mode the server detects speech boundaries
    /// and submits the audio for recognition automatically; in the manual mode call
    /// <see cref="CommitAudioAsync"/> to submit the buffered audio. The server sends no
    /// acknowledgement for this event.
    /// </summary>
    /// <param name="audio">The audio chunk, at most 15 MiB per call. Smaller chunks are
    /// recommended for lower latency.</param>
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
    /// which submits the buffered audio for recognition. Manual mode only - in the VAD mode the
    /// server rejects the event.
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
    /// Send a session.finish event and wait for the session.finished event. The server returns
    /// the transcription of the remaining audio, then closes the connection. In the VAD mode give
    /// the server a moment after the last audio first: the appends are not acknowledged, and a
    /// finish that arrives before the server has consumed the buffered audio ends the session
    /// without the transcription (about a second proved enough after a burst of audio).
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
    public IAsyncEnumerable<AsrRealtimeServerEvent> GetEventsAsync() => _eventOutput.Reader.ReadAllAsync();

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
            _errorSignal = NewSignal<AsrRealtimeError>();
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

                var serverEvent = JsonSerializer.Deserialize<AsrRealtimeServerEvent>(
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

    private void HandleEvent(AsrRealtimeServerEvent serverEvent)
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
            case "session.finished":
                _finishedSignal.TrySetResult(true);
                CompleteOutputs();
                break;
            case "error":
                _errorSignal.TrySetResult(serverEvent.Error ?? new AsrRealtimeError());
                break;
        }
    }

    private void CompleteOutputs()
    {
        _eventOutput.Writer.TryComplete();
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
