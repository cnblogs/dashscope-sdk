namespace Cnblogs.DashScope.Core;

/// <summary>
/// The audio output configuration of a live translation session. Server echo,
/// qwen3.8-livetranslate-flash-realtime only, nested in <see cref="LiveTranslateAudio.Output"/>.
/// </summary>
public class LiveTranslateAudioOutput
{
    /// <summary>
    /// The format of the output audio. Server echo.
    /// </summary>
    public LiveTranslateAudioFormat? Format { get; set; }

    /// <summary>
    /// The voice of the output audio, defaults to Tina. Server echo.
    /// </summary>
    public string? Voice { get; set; }
}
