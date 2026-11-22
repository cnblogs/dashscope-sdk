using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for real-time speech recognition task, shared by the models on this endpoint:
/// paraformer-realtime and the qwen-audio-*-asr-flash models (qwen-audio-3.1-asr-flash-message,
/// qwen-audio-3.0-asr-flash-streaming and qwen-audio-3.1-asr-flash-streaming). Which members are
/// supported depends on the model; the server does not reject unsupported members, it ignores them.
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
    /// Sample rate of the input audio in Hz. paraformer-realtime-v2 and the
    /// qwen-audio-asr-flash-streaming models accept any sample rate, while paraformer-realtime-8k-v2
    /// only accepts 8000. qwen-audio-3.1-asr-flash-message documents 16000 as the only supported
    /// value (an 8000 Hz wav was accepted and recognized as well when probed).
    /// </summary>
    public int SampleRate { get; set; } = 16000;

    /// <summary>
    /// ID of the vocabulary (hot word) list to use.
    /// </summary>
    public string? VocabularyId { get; set; }

    /// <summary>
    /// The instant hot words, which need no vocabulary list: the key is the hot word, the value is
    /// its weight. The weight is in range [1, 5] (the higher, the more the model tends to output the
    /// word) or 50 for super hot words, which have a much higher recall rate (at most 50 of them).
    /// Merged with the list of <see cref="VocabularyId"/> when both are set, at most 2000 words are
    /// used. qwen-audio-*-asr-flash models.
    /// </summary>
    public Dictionary<string, int>? Vocabulary { get; set; }

    /// <summary>
    /// Whether to remove disfluency (filler words) from the result, defaults to false.
    /// Supported by Paraformer models. qwen-audio-3.1-asr-flash-message also removes repetitions
    /// and self-corrections and tidies up the expression of the final result.
    /// </summary>
    public bool? DisfluencyRemovalEnabled { get; set; }

    /// <summary>
    /// Whether to return intermediate results, defaults to false, then only the final result of each
    /// sentence is returned. Only supported by qwen-audio-3.1-asr-flash-message, the other models
    /// always return intermediate results.
    /// </summary>
    public bool? IntermediateResultEnabled { get; set; }

    /// <summary>
    /// Whether to keep dialect expressions in the final result, defaults to false, which transcribes
    /// dialects into Mandarin text. qwen-audio-3.1-asr-flash-message and qwen-audio-3.1-asr-flash-streaming.
    /// </summary>
    public bool? KeepDialect { get; set; }

    /// <summary>
    /// The VAD model, defaults to far_field_meeting_16k (far field), near_meeting_16k for near field.
    /// qwen-audio-3.1-asr-flash-message and qwen-audio-3.1-asr-flash-streaming.
    /// </summary>
    public string? VadModel { get; set; }

    /// <summary>
    /// The threshold telling speech from noise, range [-1.0, 1.0], which adjusts the sensitivity of
    /// the VAD: values near -1 classify more noise as speech (more noise gets transcribed), values
    /// near +1 classify more speech as noise (some speech gets filtered). An advanced setting that
    /// may notably affect the recognition, adjust it in small steps (0.1) after testing.
    /// qwen-audio-*-asr-flash models.
    /// </summary>
    public float? SpeechNoiseThreshold { get; set; }

    /// <summary>
    /// The sensitive words to replace or remove in the recognition result. Only supported by the
    /// qwen-audio-asr-flash-streaming models. The server expects the filter JSON-encoded as a
    /// string, the SDK takes care of it.
    /// </summary>
    [JsonConverter(typeof(SpeechRecognitionSpecialWordFilterJsonConverter))]
    public SpeechRecognitionSpecialWordFilter? SpecialWordFilter { get; set; }

    /// <summary>
    /// Languages of the input audio. Leave unset to let the model detect automatically.
    /// Supported values are zh, en, ja, yue, ko, de, fr and ru for Paraformer models; zh, en, ja,
    /// ko, vi, th, id, ms, tl, hi, ar, fr, de, es, pt, ru, it, nl, sv, da, fi, no, el, pl, cs, hu,
    /// ro, bg, hr and sk for qwen-audio-*-asr-flash models, where at most 4 values take effect and
    /// the hints only apply to final results.
    /// </summary>
    public List<string>? LanguageHints { get; set; }

    /// <summary>
    /// Whether to enable semantic punctuation based sentence splitting, defaults to false.
    /// When true, VAD based sentence splitting is disabled. Only supported by paraformer-realtime-v2.
    /// </summary>
    public bool? SemanticPunctuationEnabled { get; set; }

    /// <summary>
    /// The silence threshold in milliseconds used by VAD sentence splitting, defaults to 1300 for
    /// paraformer-realtime-v2 and 800 for qwen-audio-*-asr-flash models. Range is [200, 6000].
    /// Supported by paraformer-realtime-v2 (only effective when
    /// <see cref="SemanticPunctuationEnabled"/> is false) and qwen-audio-*-asr-flash models.
    /// </summary>
    public int? MaxSentenceSilence { get; set; }

    /// <summary>
    /// Whether to enable multi-threshold mode, which prevents VAD sentence splitting from cutting
    /// a sentence too long, defaults to false. Supported by paraformer-realtime-v2 (only effective
    /// when <see cref="SemanticPunctuationEnabled"/> is false) and qwen-audio-3.1-asr-flash-streaming.
    /// </summary>
    public bool? MultiThresholdModeEnabled { get; set; }

    /// <summary>
    /// Whether to add punctuations to the recognition result, defaults to true.
    /// Only supported by paraformer-realtime-v2.
    /// </summary>
    public bool? PunctuationPredictionEnabled { get; set; }

    /// <summary>
    /// Whether to enable heartbeat. When true, sending silence audio keeps the connection alive,
    /// defaults to false. Supported by paraformer-realtime-v2 and qwen-audio-*-asr-flash models.
    /// </summary>
    public bool? Heartbeat { get; set; }

    /// <summary>
    /// Whether to enable inverse text normalization, which converts Chinese numbers in the result
    /// to Arabic numerals, defaults to true. Only supported by paraformer-realtime-v2.
    /// </summary>
    public bool? InverseTextNormalizationEnabled { get; set; }
}
