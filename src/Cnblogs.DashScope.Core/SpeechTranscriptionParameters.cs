namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for asynchronous speech transcription.
/// </summary>
public class SpeechTranscriptionParameters : ISpeechTranscriptionParameters
{
    /// <inheritdoc />
    public IReadOnlyList<int>? ChannelId { get; set; }

    /// <inheritdoc />
    public string? VocabularyId { get; set; }

    /// <inheritdoc />
    public Dictionary<string, int>? Vocabulary { get; set; }

    /// <inheritdoc />
    public string? SpecialWordFilter { get; set; }

    /// <inheritdoc />
    public IReadOnlyList<string>? LanguageHints { get; set; }

    /// <inheritdoc />
    public bool? DiarizationEnabled { get; set; }

    /// <inheritdoc />
    public int? SpeakerCount { get; set; }

    /// <inheritdoc />
    public string? ResourceId { get; set; }

    /// <inheritdoc />
    public string? ResourceType { get; set; }

    /// <inheritdoc />
    public bool? DisfluencyRemovalEnabled { get; set; }

    /// <inheritdoc />
    public bool? TimestampAlignmentEnabled { get; set; }
}
