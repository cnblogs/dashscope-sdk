namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for a speech transcription task, shared by the models on this endpoint
/// (qwen3-asr-flash-filetrans, paraformer-v2 and other file transcription models). Which
/// members are supported depends on the model.
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
    /// Whether to remove disfluency (filler words) from the result, defaults to false.
    /// Paraformer file transcription models only.
    /// </summary>
    public bool? DisfluencyRemovalEnabled { get; set; }

    /// <summary>
    /// Languages of the audio, e.g. zh, en, yue. Leave unset to let the model detect
    /// automatically. Paraformer file transcription models only.
    /// </summary>
    public List<string>? LanguageHints { get; set; }

    /// <summary>
    /// Whether to align the timestamps of the first and the last word with the start and the
    /// end of the audio. Paraformer file transcription models only.
    /// </summary>
    public bool? TimestampAlignmentEnabled { get; set; }
}
