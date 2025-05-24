namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a socket-based TTS session.
/// </summary>
/// <param name="socket">Underlying websocket.</param>
/// <param name="modelId">Model name to use.</param>
public sealed class SpeechSynthesizerSocketSession(DashScopeClientWebSocketWrapper socket, string modelId)
    : IDisposable
{
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
        var command = new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>()
        {
            Header = new DashScopeWebSocketRequestHeader()
            {
                Action = "run-task", TaskId = taskId,
            },
            Payload = new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>()
            {
                Input = new SpeechSynthesizerInput() { Text = text, },
                TaskGroup = "audio",
                Task = "tts",
                Function = "SpeechSynthesizer",
                Model = modelId,
                Parameters = parameters
            }
        };
        socket.ResetTask();
        await socket.SendMessageAsync(command, cancellationToken);
        await socket.TaskStarted;
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
        var command = new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>()
        {
            Header = new DashScopeWebSocketRequestHeader()
            {
                Action = "continue-task", TaskId = taskId,
            },
            Payload = new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>()
            {
                Input = new SpeechSynthesizerInput() { Text = input }
            }
        };
        await socket.SendMessageAsync(command, cancellationToken);
    }

    /// <summary>
    /// Send finish-task command.
    /// </summary>
    /// <param name="taskId">Unique id of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    public async Task FinishTaskAsync(string taskId, CancellationToken cancellationToken = default)
    {
        var command = new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>()
        {
            Header = new DashScopeWebSocketRequestHeader() { TaskId = taskId, Action = "finish-task" },
            Payload = new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>()
            {
                Input = new SpeechSynthesizerInput()
            }
        };
        await socket.SendMessageAsync(command, cancellationToken);
    }

    /// <summary>
    /// Get the audio stream.
    /// </summary>
    /// <returns></returns>
    public IAsyncEnumerable<byte> GetAudioAsync()
    {
        return socket.BinaryOutput;
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            socket.Dispose();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
    }
}
