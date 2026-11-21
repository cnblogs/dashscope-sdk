using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A websocket session for the TTS realtime API (qwen3-tts-*-realtime models). The session talks
/// to the /api-ws/v1/realtime endpoint with event-based (session.update / input_text_buffer.append /
/// input_text_buffer.commit / session.finish) messages, the audio is returned as base64 encoded
/// chunks inside response.audio.delta events. Create it with
/// <see cref="IDashScopeClient.CreateTtsRealtimeSocketSessionAsync"/>, which connects the underlying
/// websocket and waits for the session.created event. The server closes the connection after
/// session.finished, so a session cannot be reused for a second run.
/// </summary>
public sealed class TtsRealtimeSocketSession : IDisposable
{
    private static readonly UnboundedChannelOptions ChannelOptions = new()
    {
        SingleWriter = true,
        SingleReader = true,
        AllowSynchronousContinuations = true
    };

    private readonly IClientWebSocket _socket;
    private readonly Channel<TtsRealtimeServerEvent> _eventOutput = Channel.CreateUnbounded<TtsRealtimeServerEvent>(ChannelOptions);
    private readonly Channel<byte> _audioOutput = Channel.CreateUnbounded<byte>(ChannelOptions);
    private TaskCompletionSource<TtsRealtimeSession> _sessionCreatedSignal = NewSignal<TtsRealtimeSession>();
    private TaskCompletionSource<TtsRealtimeSession> _sessionUpdatedSignal = NewSignal<TtsRealtimeSession>();
    private TaskCompletionSource<string> _committedSignal = NewSignal<string>();
    private TaskCompletionSource<bool> _clearedSignal = NewSignal<bool>();
    private TaskCompletionSource<bool> _finishedSignal = NewSignal<bool>();
    private TaskCompletionSource<TtsRealtimeError> _errorSignal = NewSignal<TtsRealtimeError>();

    internal TtsRealtimeSocketSession(IClientWebSocket socket)
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
    internal async Task<TtsRealtimeSession> StartAsync(CancellationToken cancellationToken = default)
    {
        _ = ReceiveMessagesAsync(cancellationToken);
        return await WaitAsync(_sessionCreatedSignal, cancellationToken);
    }

    /// <summary>
    /// Send a session.update event and wait for the session.updated confirmation.
    /// </summary>
    /// <param name="session">The session configuration to apply, <see cref="TtsRealtimeSession.Voice"/> is required.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The applied session configuration returned by the server.</returns>
    public async Task<TtsRealtimeSession> UpdateSessionAsync(
        TtsRealtimeSession session,
        CancellationToken cancellationToken = default)
    {
        _sessionUpdatedSignal = NewSignal<TtsRealtimeSession>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "session.update", session },
            cancellationToken);
        return await WaitAsync(_sessionUpdatedSignal, cancellationToken);
    }

    /// <summary>
    /// Append text to the text buffer, the server synthesizes it based on the session mode.
    /// In server_commit mode complete sentences are synthesized automatically; in commit mode
    /// call <see cref="CommitTextAsync"/> to trigger the synthesis.
    /// </summary>
    /// <param name="text">The text to append.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public Task AppendTextAsync(string text, CancellationToken cancellationToken = default)
        => SendEventAsync(
            new { event_id = NewEventId(), type = "input_text_buffer.append", text },
            cancellationToken);

    /// <summary>
    /// Commit the text buffer and wait for the input_text_buffer.committed confirmation.
    /// In commit mode this triggers the synthesis; in server_commit mode it synthesizes all
    /// buffered text immediately while keeping the mode.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The id of the created message item.</returns>
    public async Task<string> CommitTextAsync(CancellationToken cancellationToken = default)
    {
        _committedSignal = NewSignal<string>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "input_text_buffer.commit" },
            cancellationToken);
        return await WaitAsync(_committedSignal, cancellationToken);
    }

    /// <summary>
    /// Clear the text buffer and wait for the input_text_buffer.cleared confirmation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public async Task ClearTextAsync(CancellationToken cancellationToken = default)
    {
        _clearedSignal = NewSignal<bool>();
        await SendEventAsync(
            new { event_id = NewEventId(), type = "input_text_buffer.clear" },
            cancellationToken);
        await WaitAsync(_clearedSignal, cancellationToken);
    }

    /// <summary>
    /// Send a session.finish event and wait for the session.finished event. The server returns
    /// the remaining audio, then closes the connection.
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
    /// Get the decoded audio stream, assembled from the base64 chunks of response.audio.delta events.
    /// </summary>
    public IAsyncEnumerable<byte> GetAudioAsync() => _audioOutput.Reader.ReadAllAsync();

    /// <summary>
    /// Get the server event stream. Every event is also delivered here, including the ones
    /// that the send methods of this session wait for internally.
    /// </summary>
    public IAsyncEnumerable<TtsRealtimeServerEvent> GetEventsAsync() => _eventOutput.Reader.ReadAllAsync();

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
            _errorSignal = NewSignal<TtsRealtimeError>();
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

                var serverEvent = JsonSerializer.Deserialize<TtsRealtimeServerEvent>(
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

    private void HandleEvent(TtsRealtimeServerEvent serverEvent)
    {
        switch (serverEvent.Type)
        {
            case "session.created":
                _sessionCreatedSignal.TrySetResult(serverEvent.Session!);
                break;
            case "session.updated":
                _sessionUpdatedSignal.TrySetResult(serverEvent.Session!);
                break;
            case "input_text_buffer.committed":
                _committedSignal.TrySetResult(serverEvent.ItemId ?? string.Empty);
                break;
            case "input_text_buffer.cleared":
                _clearedSignal.TrySetResult(true);
                break;
            case "session.finished":
                _finishedSignal.TrySetResult(true);
                CompleteOutputs();
                break;
            case "error":
                _errorSignal.TrySetResult(serverEvent.Error ?? new TtsRealtimeError());
                break;
            case "response.audio.delta":
                var audio = Convert.FromBase64String(serverEvent.Delta ?? string.Empty);
                foreach (var b in audio)
                {
                    _audioOutput.Writer.TryWrite(b);
                }

                break;
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
