namespace Cnblogs.DashScope.Core;

/// <summary>
/// The audio output of a qwen3-tts speech synthesis request.
/// </summary>
public class MultimodalAudio
{
    /// <summary>
    /// The base64 encoded audio data. Empty in the last frame of streaming output, where
    /// the audio is available via <see cref="Url"/> instead.
    /// </summary>
    public string? Data { get; set; }

    /// <summary>
    /// The url of the complete audio file, valid for 24 hours.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// The id of the audio.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The UNIX timestamp when <see cref="Url"/> expires.
    /// </summary>
    public long? ExpiresAt { get; set; }
}
