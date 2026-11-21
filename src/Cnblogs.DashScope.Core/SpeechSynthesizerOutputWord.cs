namespace Cnblogs.DashScope.Core;

/// <summary>
/// A word with timestamps in TTS output.
/// </summary>
/// <param name="Text">Text of the word.</param>
/// <param name="BeginIndex">Start index of the word in the sentence, starting from 0. CosyVoice and qwen-audio-tts models only, null for Sambert.</param>
/// <param name="EndIndex">End index of the word in the sentence, starting from 1. CosyVoice and qwen-audio-tts models only, null for Sambert.</param>
/// <param name="BeginTime">Start timestamp of the word audio, in milliseconds.</param>
/// <param name="EndTime">End timestamp of the word audio, in milliseconds.</param>
/// <param name="Phonemes">Phoneme level timestamps, present when phoneme timestamp is enabled. Sambert models only.</param>
public record SpeechSynthesizerOutputWord(
    string Text,
    int? BeginIndex = null,
    int? EndIndex = null,
    int BeginTime = 0,
    int EndTime = 0,
    List<SpeechSynthesizerPhoneme>? Phonemes = null);
