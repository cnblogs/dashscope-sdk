namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a socket-based TTS session.
/// </summary>
public sealed class SpeechSynthesizerSocketSession
    : IDisposable
{
    private readonly DashScopeClientWebSocketWrapper _socket;
    private readonly string _modelId;

    /// <summary>
    /// Represents a socket-based TTS session.
    /// </summary>
    /// <param name="socket">Underlying websocket.</param>
    /// <param name="modelId">Model name to use.</param>
    public SpeechSynthesizerSocketSession(DashScopeClientWebSocketWrapper socket, string modelId)
    {
        _socket = socket;
        _modelId = modelId;
    }

    /// <summary>
    /// Send a run-task command, use random GUID as taskId.
    /// </summary>
    /// <param name="parameters">Input parameters.</param>
    /// <param name="text">Input text.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The generated taskId.</returns>
    public Task<string> RunTaskAsync(
        SpeechSynthesizerParameters parameters,
        string? text = null,
        CancellationToken cancellationToken = default)
    {
        return RunTaskAsync(Guid.NewGuid().ToString(), parameters, text, cancellationToken);
    }

    /// <summary>
    /// Send a run-task command.
    /// </summary>
    /// <param name="taskId">Unique taskId.</param>
    /// <param name="parameters">Input parameters.</param>
    /// <param name="text">Input text.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns><paramref name="taskId"/>.</returns>
    public async Task<string> RunTaskAsync(
        string taskId,
        SpeechSynthesizerParameters parameters,
        string? text = null,
        CancellationToken cancellationToken = default)
    {
        var command = new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>
        {
            Header = new DashScopeWebSocketRequestHeader
            {
                Action = "run-task", TaskId = taskId,
            },
            Payload = new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>
            {
                Input = new SpeechSynthesizerInput { Text = text, },
                TaskGroup = "audio",
                Task = "tts",
                Function = "SpeechSynthesizer",
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
    /// Append input text to task.
    /// </summary>
    /// <param name="taskId">TaskId to append.</param>
    /// <param name="input">Text to append.</param>
    /// <param name="cancellationToken">Cancellation token to use.</param>
    public async Task ContinueTaskAsync(string taskId, string input, CancellationToken cancellationToken = default)
    {
        var command = new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>
        {
            Header = new DashScopeWebSocketRequestHeader
            {
                Action = "continue-task",
                TaskId = taskId,
                Streaming = null
            },
            Payload = new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>
            {
                Input = new SpeechSynthesizerInput { Text = input }
            }
        };
        await _socket.SendMessageAsync(command, cancellationToken);
    }

    /// <summary>
    /// Send finish-task command.
    /// </summary>
    /// <param name="taskId">Unique id of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public async Task FinishTaskAsync(string taskId, CancellationToken cancellationToken = default)
    {
        var command = new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>
        {
            Header = new DashScopeWebSocketRequestHeader
            {
                TaskId = taskId,
                Action = "finish-task",
                Streaming = null
            },
            Payload = new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>
            {
                Input = new SpeechSynthesizerInput()
            }
        };
        await _socket.SendMessageAsync(command, cancellationToken);
    }

    /// <summary>
    /// Get the audio stream.
    /// </summary>
    /// <returns></returns>
    public IAsyncEnumerable<byte> GetAudioAsync()
    {
        return _socket.BinaryOutput;
    }

    /// <summary>
    /// Get the message stream.
    /// </summary>
    /// <returns></returns>
    public async IAsyncEnumerable<DashScopeWebSocketResponse<SpeechSynthesizerOutput>> GetMessagesAsync()
    {
        await foreach (var response in _socket.JsonOutput)
        {
            yield return response.DeserializeOutput<SpeechSynthesizerOutput>();
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
