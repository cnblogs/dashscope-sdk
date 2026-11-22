namespace Cnblogs.DashScope.Core;

/// <summary>
/// The preview audio of a designed voice.
/// </summary>
public class VoicePreviewAudio
{
    /// <summary>
    /// The base64 encoded preview audio.
    /// </summary>
    public string? Data { get; set; }

    /// <summary>
    /// The sample rate (Hz) of the preview audio.
    /// </summary>
    public int? SampleRate { get; set; }

    /// <summary>
    /// The format of the preview audio: pcm, wav or mp3.
    /// </summary>
    public string? ResponseFormat { get; set; }
}
