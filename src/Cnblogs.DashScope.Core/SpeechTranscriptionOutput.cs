namespace Cnblogs.DashScope.Core;

/// <summary>
/// Output of an asynchronous speech transcription task.
/// </summary>
public record SpeechTranscriptionOutput : DashScopeTaskOutput
{
    /// <summary>
    /// Per-file subtask results.
    /// </summary>
    public List<SpeechTranscriptionSubtaskResult>? Results { get; set; }
}
