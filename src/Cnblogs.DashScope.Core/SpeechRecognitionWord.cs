namespace Cnblogs.DashScope.Core;

/// <summary>
/// A recognized word with timestamps.
/// </summary>
/// <param name="Text">Text of the word.</param>
/// <param name="BeginTime">Start timestamp of the word audio, in milliseconds.</param>
/// <param name="EndTime">End timestamp of the word audio, in milliseconds.</param>
/// <param name="Punctuation">The punctuation appended after the word, can be empty.</param>
public record SpeechRecognitionWord(string Text, int BeginTime, int EndTime, string? Punctuation);
