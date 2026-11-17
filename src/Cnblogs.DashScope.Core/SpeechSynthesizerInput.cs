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

    /// <summary>
    /// Directive for finish-task command, currently only "cancel" is supported,
    /// which cancels the current round of synthesis.
    /// </summary>
    public string? Directive { get; set; }
}
