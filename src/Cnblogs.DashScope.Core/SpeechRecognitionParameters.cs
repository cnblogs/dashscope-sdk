namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for Fun-ASR-Flash / Qwen-Audio ASR Flash recognition.
/// </summary>
public class SpeechRecognitionParameters : ISpeechRecognitionParameters
{
    /// <inheritdoc />
    public string? Format { get; set; }

    /// <inheritdoc />
    public string? SampleRate { get; set; }

    /// <inheritdoc />
    public string? VocabularyId { get; set; }

    /// <inheritdoc />
    public Dictionary<string, int>? Vocabulary { get; set; }

    /// <inheritdoc />
    public IReadOnlyList<string>? LanguageHints { get; set; }
}
