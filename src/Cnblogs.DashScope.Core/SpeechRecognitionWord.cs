namespace Cnblogs.DashScope.Core;

/// <summary>
/// A recognized word with timestamps.
/// </summary>
/// <param name="Text">Text of the word.</param>
/// <param name="BeginTime">Start timestamp of the word audio, in milliseconds.</param>
/// <param name="EndTime">End timestamp of the word audio, in milliseconds.</param>
/// <param name="Punctuation">The punctuation appended after the word, can be empty.</param>
/// <param name="Fixed">Whether the timestamps of the word are settled. When false, they may be
/// adjusted in later events. Recorded speech recognition of the Fun-ASR-Flash / Qwen-Audio-3.x-ASR-Flash
/// models.</param>
/// <param name="SpeakerId">The speaker index of the word, null when speaker diarization is not
/// enabled. Recorded speech recognition of qwen-audio-3.1-asr-flash when speaker diarization
/// is enabled.</param>
public record SpeechRecognitionWord(
    string Text,
    int BeginTime,
    int EndTime,
    string? Punctuation,
    bool? Fixed = null,
    int? SpeakerId = null);
