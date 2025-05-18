namespace Cnblogs.DashScope.Core;

/// <summary>
/// Input for TTS task.
/// </summary>
public class SpeechSynthesizerInput
{
    /// <summary>
    /// Input text, can be null for run-task command.
    /// </summary>
    public string? Text { get; set; }
}
