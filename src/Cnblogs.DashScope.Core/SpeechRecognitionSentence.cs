namespace Cnblogs.DashScope.Core;

/// <summary>
/// A recognized sentence. Intermediate results have <paramref name="SentenceEnd"/> set to false,
/// and the final result of the sentence has it set to true. Intermediate results may be revised,
/// update the displayed text of the sentence by <paramref name="SentenceId"/> instead of appending
/// them, and replace it with the final result.
/// </summary>
/// <param name="BeginTime">Start timestamp of the sentence, in milliseconds.</param>
/// <param name="EndTime">End timestamp of the sentence, in milliseconds, null for intermediate results.</param>
/// <param name="Text">Recognized text of the sentence.</param>
/// <param name="Heartbeat">Whether this result is a heartbeat event, which can be skipped.</param>
/// <param name="SentenceEnd">Whether the sentence has ended. true for final results, false for intermediate ones.</param>
/// <param name="EmoTag">Emotion of the sentence, can be positive, negative or neutral.
/// Only supported by paraformer-realtime-8k-v2 and only present when <paramref name="SentenceEnd"/> is true.</param>
/// <param name="EmoConfidence">Confidence of the emotion, range [0.0, 1.0].
/// Only supported by paraformer-realtime-8k-v2 and only present when <paramref name="SentenceEnd"/> is true.</param>
/// <param name="Words">Word level timestamps of the sentence.</param>
/// <param name="SentenceBegin">Whether the sentence just began, in which case the text may be empty.
/// Documented by the qwen-audio-*-asr-flash models, paraformer-realtime models send it as well.</param>
/// <param name="SentenceId">The serial number of the sentence, starting from 1 (0 for heartbeat events).
/// Documented by the qwen-audio-*-asr-flash models, paraformer-realtime models send it as well.</param>
public record SpeechRecognitionSentence(
    int? BeginTime,
    int? EndTime,
    string? Text,
    bool? Heartbeat,
    bool? SentenceEnd,
    string? EmoTag,
    float? EmoConfidence,
    SpeechRecognitionWord[]? Words,
    bool? SentenceBegin = null,
    int? SentenceId = null);
