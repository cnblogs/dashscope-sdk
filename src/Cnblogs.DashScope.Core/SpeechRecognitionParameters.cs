namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for real-time speech recognition task.
/// </summary>
public class SpeechRecognitionParameters
{
    /// <summary>
    /// Format of the input audio. Can be pcm, wav, mp3, opus, speex, aac or amr.
    /// Note that opus and speex must be Ogg-encapsulated, wav must be PCM encoded,
    /// and only AMR-NB is supported for amr.
    /// </summary>
    public string Format { get; set; } = "pcm";

    /// <summary>
    /// Sample rate of the input audio in Hz. paraformer-realtime-v2 accepts any sample rate,
    /// while paraformer-realtime-8k-v2 only accepts 8000.
    /// </summary>
    public int SampleRate { get; set; } = 16000;

    /// <summary>
    /// ID of the vocabulary (hot word) list to use.
    /// </summary>
    public string? VocabularyId { get; set; }

    /// <summary>
    /// Whether to remove disfluency (filler words) from the result, defaults to false.
    /// Only supported by Paraformer models.
    /// </summary>
    public bool? DisfluencyRemovalEnabled { get; set; }

    /// <summary>
    /// Languages of the input audio. Leave unset to let the model detect automatically.
    /// Supported values are zh, en, ja, yue, ko, de, fr and ru.
    /// </summary>
    public List<string>? LanguageHints { get; set; }

    /// <summary>
    /// Whether to enable semantic punctuation based sentence splitting, defaults to false.
    /// When true, VAD based sentence splitting is disabled. Only supported by paraformer-realtime-v2.
    /// </summary>
    public bool? SemanticPunctuationEnabled { get; set; }

    /// <summary>
    /// The silence threshold in milliseconds used by VAD sentence splitting, defaults to 1300.
    /// Range is [200, 6000]. Only supported by paraformer-realtime-v2 and only effective when
    /// <see cref="SemanticPunctuationEnabled"/> is false.
    /// </summary>
    public int? MaxSentenceSilence { get; set; }

    /// <summary>
    /// Whether to enable multi-threshold mode, which prevents VAD sentence splitting from cutting
    /// a sentence too long, defaults to false. Only supported by paraformer-realtime-v2 and only
    /// effective when <see cref="SemanticPunctuationEnabled"/> is false.
    /// </summary>
    public bool? MultiThresholdModeEnabled { get; set; }

    /// <summary>
    /// Whether to add punctuations to the recognition result, defaults to true.
    /// Only supported by paraformer-realtime-v2.
    /// </summary>
    public bool? PunctuationPredictionEnabled { get; set; }

    /// <summary>
    /// Whether to enable heartbeat. When true, sending silence audio keeps the connection alive,
    /// defaults to false. Only supported by paraformer-realtime-v2.
    /// </summary>
    public bool? Heartbeat { get; set; }

    /// <summary>
    /// Whether to enable inverse text normalization, which converts Chinese numbers in the result
    /// to Arabic numerals, defaults to true. Only supported by paraformer-realtime-v2.
    /// </summary>
    public bool? InverseTextNormalizationEnabled { get; set; }
}
