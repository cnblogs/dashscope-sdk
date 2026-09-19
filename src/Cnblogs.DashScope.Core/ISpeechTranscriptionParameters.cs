namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for asynchronous speech transcription.
/// </summary>
public interface ISpeechTranscriptionParameters
{
    /// <summary>
    /// Channel indexes to recognize, starting from 0. Defaults to <c>[0]</c>.
    /// </summary>
    IReadOnlyList<int>? ChannelId { get; set; }

    /// <summary>
    /// Pre-compiled vocabulary id from speech vocabulary CRUD APIs.
    /// </summary>
    string? VocabularyId { get; set; }

    /// <summary>
    /// Instant vocabulary as word-to-weight map. Supported by Qwen-Audio filetrans models.
    /// Weight range is [1, 5] or 50 for super hot words.
    /// </summary>
    Dictionary<string, int>? Vocabulary { get; set; }

    /// <summary>
    /// Sensitive word filter configuration as a JSON string.
    /// </summary>
    string? SpecialWordFilter { get; set; }

    /// <summary>
    /// Language hints for recognition (e.g. zh, en, ja).
    /// </summary>
    IReadOnlyList<string>? LanguageHints { get; set; }

    /// <summary>
    /// Enable speaker diarization for mono audio.
    /// </summary>
    bool? DiarizationEnabled { get; set; }

    /// <summary>
    /// Expected speaker count when diarization is enabled. Range: 2-100.
    /// </summary>
    int? SpeakerCount { get; set; }

    /// <summary>
    /// Paraformer v1 phrase id (<c>resource_id</c>). Prefer <see cref="VocabularyId"/> for v2 models.
    /// </summary>
    string? ResourceId { get; set; }

    /// <summary>
    /// Fixed value <c>asr_phrase</c> when using <see cref="ResourceId"/>.
    /// </summary>
    string? ResourceType { get; set; }

    /// <summary>
    /// Remove disfluencies (Paraformer). Defaults to false.
    /// </summary>
    bool? DisfluencyRemovalEnabled { get; set; }

    /// <summary>
    /// Enable timestamp alignment (Paraformer). Defaults to false.
    /// </summary>
    bool? TimestampAlignmentEnabled { get; set; }
}
