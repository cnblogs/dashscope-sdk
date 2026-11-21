namespace Cnblogs.DashScope.Core;

/// <summary>
/// The audio settings of a MiniMax speech synthesis request.
/// </summary>
public class MinimaxAudioSetting
{
    /// <summary>
    /// The sample rate in Hz: 8000, 16000, 22050, 24000, 32000 or 44100, defaults to 32000.
    /// </summary>
    public int? SampleRate { get; set; }

    /// <summary>
    /// The bitrate in bps: 32000, 64000, 128000 or 256000, defaults to 128000.
    /// Only takes effect when <see cref="Format"/> is mp3.
    /// </summary>
    public int? Bitrate { get; set; }

    /// <summary>
    /// The audio format: mp3, pcm, flac or wav, defaults to mp3.
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// The channel count: 1 (mono) or 2 (stereo), defaults to 1.
    /// </summary>
    public int? Channel { get; set; }

    /// <summary>
    /// Whether to encode with a constant bitrate, only takes effect in streaming mode
    /// with the mp3 format, defaults to false.
    /// </summary>
    public bool? ForceCbr { get; set; }
}
