namespace Cnblogs.DashScope.Core;

/// <summary>
/// A phoneme with timestamps in TTS output, present in the words of Sambert sentence events
/// when phoneme timestamp is enabled.
/// </summary>
/// <param name="Text">Text of the phoneme, e.g. ch_c.</param>
/// <param name="Tone">The tone of the phoneme.</param>
/// <param name="BeginTime">Start timestamp of the phoneme audio, in milliseconds.</param>
/// <param name="EndTime">End timestamp of the phoneme audio, in milliseconds.</param>
public record SpeechSynthesizerPhoneme(string Text, int Tone, int BeginTime, int EndTime);
