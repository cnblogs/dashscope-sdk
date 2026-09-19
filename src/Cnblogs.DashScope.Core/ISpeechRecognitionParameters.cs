namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for Fun-ASR-Flash / Qwen-Audio ASR Flash recognition.
/// </summary>
public interface ISpeechRecognitionParameters
{
    /// <summary>
    /// Audio format (e.g. wav, mp3, opus). Required by the API.
    /// </summary>
    string? Format { get; set; }

    /// <summary>
    /// Sample rate in Hz as string (e.g. <c>16000</c>).
    /// </summary>
    string? SampleRate { get; set; }

    /// <summary>
    /// Pre-compiled vocabulary id.
    /// </summary>
    string? VocabularyId { get; set; }

    /// <summary>
    /// Instant vocabulary as word-to-weight map. Supported by qwen-audio-3.0-asr-flash.
    /// </summary>
    Dictionary<string, int>? Vocabulary { get; set; }

    /// <summary>
    /// Language hints for recognition.
    /// </summary>
    IReadOnlyList<string>? LanguageHints { get; set; }
}
