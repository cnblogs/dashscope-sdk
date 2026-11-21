namespace Cnblogs.DashScope.Core;

/// <summary>
/// The extra information of a MiniMax speech synthesis response.
/// </summary>
public class MinimaxSpeechExtraInfo
{
    /// <summary>
    /// The audio duration in milliseconds.
    /// </summary>
    public int? AudioLength { get; set; }

    /// <summary>
    /// The audio sample rate.
    /// </summary>
    public int? AudioSampleRate { get; set; }

    /// <summary>
    /// The audio size in bytes.
    /// </summary>
    public int? AudioSize { get; set; }

    /// <summary>
    /// The audio bitrate.
    /// </summary>
    public int? Bitrate { get; set; }

    /// <summary>
    /// The audio format: mp3, pcm or flac.
    /// </summary>
    public string? AudioFormat { get; set; }

    /// <summary>
    /// The audio channel count: 1 (mono) or 2 (stereo).
    /// </summary>
    public int? AudioChannel { get; set; }

    /// <summary>
    /// The ratio of invisible characters; audio is generated when the ratio is within 10%.
    /// </summary>
    public float? InvisibleCharacterRatio { get; set; }

    /// <summary>
    /// The billed character count.
    /// </summary>
    public int? UsageCharacters { get; set; }

    /// <summary>
    /// The count of pronounced characters (Chinese characters, digits and letters,
    /// excluding punctuation).
    /// </summary>
    public int? WordCount { get; set; }
}
