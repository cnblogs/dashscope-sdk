namespace Cnblogs.DashScope.Core;

/// <summary>
/// A sentence of a qwen-audio-tts speech synthesis request.
/// </summary>
/// <param name="Index">The index of the sentence, starting from 0.</param>
/// <param name="Words">The words with timestamps, returned in sentence-end events when
/// <see cref="SpeechSynthesisInput.WordTimestampEnabled"/> is set.</param>
public record SpeechSynthesisSentence(
    int Index,
    SpeechSynthesizerOutputWord[]? Words = null);
