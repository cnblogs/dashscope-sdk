namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a socket-based real-time speech recognition session. Related model: paraformer-realtime.
/// </summary>
public sealed class SpeechRecognitionSocketSession
    : IDisposable
{
    private const int ChunkSize = 3200; // 100 ms of 16 kHz 16-bit mono audio

    private readonly DashScopeClientWebSocketWrapper _socket;
    private readonly string _modelId;

    /// <summary>
    /// Represents a socket-based real-time speech recognition session.
    /// </summary>
    /// <param name="socket">Underlying websocket.</param>
    /// <param name="modelId">Model name to use.</param>
    public SpeechRecognitionSocketSession(DashScopeClientWebSocketWrapper socket, string modelId)
    {
        _socket = socket;
        _modelId = modelId;
    }

    /// <summary>
    /// Send a run-task command, use random GUID as taskId.
    /// </summary>
    /// <param name="parameters">Recognition parameters.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The generated taskId.</returns>
    public Task<string> RunTaskAsync(
        SpeechRecognitionParameters parameters,
        CancellationToken cancellationToken = default)
    {
        return RunTaskAsync(Guid.NewGuid().ToString(), parameters, cancellationToken);
    }

    /// <summary>
    /// Send a run-task command.
    /// </summary>
    /// <param name="taskId">Unique taskId.</param>
    /// <param name="parameters">Recognition parameters.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns><paramref name="taskId"/>.</returns>
    public async Task<string> RunTaskAsync(
        string taskId,
        SpeechRecognitionParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var command = new DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>
        {
            Header = new DashScopeWebSocketRequestHeader
            {
                Action = "run-task", TaskId = taskId,
            },
            Payload = new DashScopeWebSocketRequestPayload<SpeechRecognitionInput, SpeechRecognitionParameters>
            {
                TaskGroup = "audio",
                Task = "asr",
                Function = "recognition",
                Model = _modelId,
                Parameters = parameters
            }
        };

        _socket.ResetTask();
        await _socket.SendMessageAsync(command, cancellationToken);
        await _socket.TaskStarted;
        return taskId;
    }

    /// <summary>
    /// Send a chunk of audio to the server, in binary websocket frames.
    /// </summary>
    /// <param name="audio">The audio data to send. The audio must be mono, with format and sample rate
    /// matching the run-task parameters.</param>
    /// <param name="cancellationToken">Cancellation token to use.</param>
    public Task SendAudioAsync(ReadOnlyMemory<byte> audio, CancellationToken cancellationToken = default)
        => _socket.SendBinaryAsync(audio, cancellationToken);

    /// <summary>
    /// Send an audio stream to the server, in binary websocket frames of 100 ms (16 kHz 16-bit mono).
    /// </summary>
    /// <param name="audio">The audio stream to send. The audio must be mono, with format and sample rate
    /// matching the run-task parameters.</param>
    /// <param name="cancellationToken">Cancellation token to use.</param>
    public async Task SendAudioAsync(Stream audio, CancellationToken cancellationToken = default)
    {
        var buffer = new byte[ChunkSize];
        int read;
        while ((read = await audio.ReadAsync(buffer, cancellationToken)) > 0)
        {
            await SendAudioAsync(buffer.AsMemory(0, read), cancellationToken);
        }
    }

    /// <summary>
    /// Send finish-task command, which tells the server that all audio has been sent.
    /// </summary>
    /// <param name="taskId">Unique id of the task.</param>
    /// <param name="cancellationToken">Cancellation token to use.</param>
    public async Task FinishTaskAsync(string taskId, CancellationToken cancellationToken = default)
    {
        var command = new DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>
        {
            Header = new DashScopeWebSocketRequestHeader
            {
                TaskId = taskId,
                Action = "finish-task"
            },
            Payload = new DashScopeWebSocketRequestPayload<SpeechRecognitionInput, SpeechRecognitionParameters>()
        };
        await _socket.SendMessageAsync(command, cancellationToken);
    }

    /// <summary>
    /// Get the message stream, which contains result-generated, task-finished and task-failed events.
    /// </summary>
    public async IAsyncEnumerable<DashScopeWebSocketResponse<SpeechRecognitionOutput>> GetMessagesAsync()
    {
        await foreach (var response in _socket.JsonOutput)
        {
            yield return response.DeserializeOutput<SpeechRecognitionOutput>();
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _socket.Dispose();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }
}
