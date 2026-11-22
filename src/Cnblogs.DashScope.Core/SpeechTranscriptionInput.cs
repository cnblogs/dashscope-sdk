namespace Cnblogs.DashScope.Core;

/// <summary>
/// Input for a speech transcription task (qwen3-asr-flash-filetrans, paraformer-v2 and other
/// file transcription models).
/// </summary>
public class SpeechTranscriptionInput
{
    /// <summary>
    /// The url of the audio file to transcribe. The url must be publicly accessible.
    /// qwen3-asr-flash-filetrans only.
    /// </summary>
    public string? FileUrl { get; set; }

    /// <summary>
    /// The urls of the audio files to transcribe. The urls must be publicly accessible.
    /// Paraformer file transcription models only.
    /// </summary>
    public List<string>? FileUrls { get; set; }
}
