namespace Cnblogs.DashScope.Core;

/// <summary>
/// The usage of a speech transcription task.
/// </summary>
public class SpeechTranscriptionUsage
{
    /// <summary>
    /// The duration of the audio, in seconds. qwen3-asr-flash-filetrans.
    /// </summary>
    public int? Seconds { get; set; }

    /// <summary>
    /// The duration of the audio, in seconds. Fun-ASR / Qwen-Audio-3.x-ASR-Flash-Filetrans
    /// and Paraformer file transcription models.
    /// </summary>
    public int? Duration { get; set; }

    /// <summary>
    /// The token count of the input. qwen-audio-3.1-asr-flash-filetrans.
    /// </summary>
    public int? InputTokens { get; set; }

    /// <summary>
    /// The token count of the output. qwen-audio-3.1-asr-flash-filetrans.
    /// </summary>
    public int? OutputTokens { get; set; }

    /// <summary>
    /// The total token count, the sum of <see cref="InputTokens"/> and <see cref="OutputTokens"/>.
    /// qwen-audio-3.1-asr-flash-filetrans.
    /// </summary>
    public int? TotalTokens { get; set; }
}
