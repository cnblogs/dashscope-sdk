namespace Cnblogs.DashScope.Core;

/// <summary>
/// Output of speech recognition flash API.
/// </summary>
/// <param name="Text">Accumulated full recognition text.</param>
/// <param name="Sentence">Current sentence details.</param>
public record SpeechRecognitionOutput(string? Text = null, SpeechRecognitionSentence? Sentence = null);

/// <summary>
/// Sentence-level recognition result for flash API.
/// </summary>
/// <param name="SentenceId">Sentence id starting from 1.</param>
/// <param name="SentenceEnd">Whether this sentence is finalized.</param>
/// <param name="BeginTime">Begin timestamp in milliseconds.</param>
/// <param name="EndTime">End timestamp in milliseconds (when sentence ends).</param>
/// <param name="Text">Sentence text.</param>
/// <param name="ChannelId">Channel index starting from 0.</param>
/// <param name="Words">Word-level timestamps.</param>
public record SpeechRecognitionSentence(
    int? SentenceId = null,
    bool? SentenceEnd = null,
    int? BeginTime = null,
    int? EndTime = null,
    string? Text = null,
    int? ChannelId = null,
    List<SpeechRecognitionWord>? Words = null);

/// <summary>
/// Word-level recognition result for flash API.
/// </summary>
/// <param name="Text">Word text.</param>
/// <param name="BeginTime">Begin timestamp in milliseconds.</param>
/// <param name="EndTime">End timestamp in milliseconds.</param>
/// <param name="Punctuation">Punctuation after the word.</param>
/// <param name="Fixed">Whether the word timestamp is stable.</param>
public record SpeechRecognitionWord(
    string? Text = null,
    int? BeginTime = null,
    int? EndTime = null,
    string? Punctuation = null,
    bool? Fixed = null);

/// <summary>
/// Usage of speech recognition flash API.
/// </summary>
/// <param name="Duration">Processed audio duration in seconds.</param>
public record SpeechRecognitionUsage(int Duration);
