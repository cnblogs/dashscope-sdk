namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Output for TTS task.
    /// </summary>
    /// <param name="Sentence">The output sentences.</param>
    public record SpeechSynthesizerOutput(SpeechSynthesizerOutputSentences? Sentence);
}
