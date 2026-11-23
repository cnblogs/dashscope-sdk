namespace Cnblogs.DashScope.Core;

/// <summary>
/// The audio configuration of a live translation session, the client can only adjust the input
/// speech detection. qwen3.8-livetranslate-flash-realtime only.
/// </summary>
public class LiveTranslateAudio
{
    /// <summary>
    /// The audio input configuration.
    /// </summary>
    public LiveTranslateAudioInput? Input { get; set; }

    /// <summary>
    /// The audio output configuration. Server echo.
    /// </summary>
    public LiveTranslateAudioOutput? Output { get; set; }
}
