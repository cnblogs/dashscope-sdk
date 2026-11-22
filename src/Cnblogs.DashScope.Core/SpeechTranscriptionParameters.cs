using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for a speech transcription task, shared by the models on this endpoint
/// (qwen3-asr-flash-filetrans, Fun-ASR / Qwen-Audio-3.x-ASR-Flash-Filetrans, paraformer-v2
/// and other file transcription models). Which members are supported depends on the model.
/// </summary>
public class SpeechTranscriptionParameters
{
    /// <summary>
    /// The language of the audio, e.g. zh, yue, en, ja. Only one language can be specified;
    /// leave it unset when the language is unknown or mixed. qwen3-asr-flash-filetrans only.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Whether to enable inverse text normalization (ITN), which converts numbers in the result
    /// to Arabic numerals, defaults to false. Only applies to Chinese and English audio.
    /// qwen3-asr-flash-filetrans only.
    /// </summary>
    public bool? EnableItn { get; set; }

    /// <summary>
    /// Whether to return word level timestamps instead of sentence level ones, defaults to
    /// false, which also splits sentences by VAD only while true splits them by VAD and
    /// punctuation. qwen3-asr-flash-filetrans only.
    /// </summary>
    public bool? EnableWords { get; set; }

    /// <summary>
    /// The channel indexes of the audio file to transcribe, starting from 0, e.g.
    /// <c>[0]</c> for the first track, <c>[0, 1]</c> for the first two tracks. Defaults to
    /// <c>[0]</c>. Each track is billed independently.
    /// </summary>
    public List<int>? ChannelId { get; set; }

    /// <summary>
    /// Whether to keep dialect expressions in the result, defaults to false, which transcribes
    /// dialects into Mandarin text. qwen-audio-3.1-asr-flash-filetrans only.
    /// </summary>
    public bool? KeepDialect { get; set; }

    /// <summary>
    /// ID of the pre-compiled vocabulary (hot word) list to use, which is created via the
    /// vocabulary API in advance and reused across requests. Fun-ASR / Qwen-Audio-3.x-ASR-Flash-Filetrans
    /// models only.
    /// </summary>
    public string? VocabularyId { get; set; }

    /// <summary>
    /// The instant hot words, which need no vocabulary list: the key is the hot word, the value
    /// is its weight. The weight is in range [1, 5] (the higher, the more the model tends to
    /// output the word) or 50 for super hot words, which have a much higher recall rate (at
    /// most 50 of them). Merged with the list of <see cref="VocabularyId"/> when both are set,
    /// at most 2000 words are used. qwen-audio-3.x-asr-flash-filetrans models only.
    /// </summary>
    public Dictionary<string, int>? Vocabulary { get; set; }

    /// <summary>
    /// The sensitive words to replace or remove in the transcription result. The server expects
    /// the filter JSON-encoded as a string, the SDK takes care of it. Fun-ASR / Qwen-Audio-3.x-ASR-Flash-Filetrans
    /// and Paraformer file transcription models.
    /// </summary>
    [JsonConverter(typeof(SpeechRecognitionSpecialWordFilterJsonConverter))]
    public SpeechRecognitionSpecialWordFilter? SpecialWordFilter { get; set; }

    /// <summary>
    /// Whether to enable speaker diarization, defaults to false. Only applies to single channel
    /// audio, and the audio is recommended to be at most 2 hours long. When enabled, the result
    /// tells speakers apart by speaker_id. Fun-ASR / Qwen-Audio-3.x-ASR-Flash-Filetrans and
    /// Paraformer file transcription models.
    /// </summary>
    public bool? DiarizationEnabled { get; set; }

    /// <summary>
    /// The reference count of speakers, range [2, 100], only effective when
    /// <see cref="DiarizationEnabled"/> is true. The model auto detects the count when unset;
    /// setting it only guides the algorithm, which does not guarantee the exact count.
    /// Fun-ASR / Qwen-Audio-3.x-ASR-Flash-Filetrans and Paraformer file transcription models.
    /// </summary>
    public int? SpeakerCount { get; set; }

    /// <summary>
    /// Whether to remove disfluency (filler words) from the result, defaults to false.
    /// Paraformer file transcription models only.
    /// </summary>
    public bool? DisfluencyRemovalEnabled { get; set; }

    /// <summary>
    /// Languages of the audio, e.g. zh, en, yue. Leave unset to let the model detect
    /// automatically. At most 4 values take effect for Qwen-Audio-3.x-ASR-Flash-Filetrans
    /// models and only the first one takes effect for Fun-ASR models; for Paraformer models
    /// only paraformer-v2 supports it.
    /// </summary>
    public List<string>? LanguageHints { get; set; }

    /// <summary>
    /// Whether to align the timestamps of the first and the last word with the start and the
    /// end of the audio. Paraformer file transcription models only.
    /// </summary>
    public bool? TimestampAlignmentEnabled { get; set; }
}
