namespace Cnblogs.DashScope.Core;

/// <summary>
/// The usage of a speech transcription task.
/// </summary>
public class SpeechTranscriptionUsage
{
    /// <summary>
    /// The duration of the audio, in seconds.
    /// </summary>
    public int? Seconds { get; set; }

    /// <summary>
    /// The duration of the processed audio, in seconds. Paraformer file transcription models.
    /// </summary>
    public int? Duration { get; set; }
}
