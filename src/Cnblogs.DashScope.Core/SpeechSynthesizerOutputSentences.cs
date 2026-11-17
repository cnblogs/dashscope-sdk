namespace Cnblogs.DashScope.Core;

/// <summary>
/// Sentences for TTS output.
/// </summary>
/// <param name="Words">Word level timestamps, only available in sentence-end events when word timestamp is enabled.</param>
public record SpeechSynthesizerOutputSentences(SpeechSynthesizerOutputWord[]? Words);
