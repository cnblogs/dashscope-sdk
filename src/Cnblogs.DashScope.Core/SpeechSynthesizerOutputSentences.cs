namespace Cnblogs.DashScope.Core;

/// <summary>
/// Sentences for TTS output.
/// </summary>
/// <param name="Words">Word level timestamps, only available in sentence-end events when word timestamp is enabled (CosyVoice and qwen-audio-tts models) or in Sambert result-generated events.</param>
/// <param name="BeginTime">Start timestamp of the sentence audio, in milliseconds. Sambert models only.</param>
/// <param name="EndTime">End timestamp of the sentence audio, in milliseconds. Sambert models only.</param>
public record SpeechSynthesizerOutputSentences(
    SpeechSynthesizerOutputWord[]? Words,
    int? BeginTime = null,
    int? EndTime = null);
