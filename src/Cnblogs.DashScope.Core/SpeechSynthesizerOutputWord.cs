namespace Cnblogs.DashScope.Core;

/// <summary>
/// A word with timestamps in TTS output.
/// </summary>
/// <param name="Text">Text of the word.</param>
/// <param name="BeginIndex">Start index of the word in the sentence, starting from 0.</param>
/// <param name="EndIndex">End index of the word in the sentence, starting from 1.</param>
/// <param name="BeginTime">Start timestamp of the word audio, in milliseconds.</param>
/// <param name="EndTime">End timestamp of the word audio, in milliseconds.</param>
public record SpeechSynthesizerOutputWord(string Text, int BeginIndex, int EndIndex, int BeginTime, int EndTime);
