namespace Cnblogs.DashScope.Core;

/// <summary>
/// The audio input configuration of a live translation session. qwen3.8-livetranslate-flash-realtime
/// only, nested in <see cref="LiveTranslateAudio.Input"/>.
/// </summary>
public class LiveTranslateAudioInput
{
    /// <summary>
    /// The format of the input audio. Server echo.
    /// </summary>
    public LiveTranslateAudioFormat? Format { get; set; }

    /// <summary>
    /// The speech detection configuration: speaker_detection (default) or server_vad.
    /// </summary>
    public LiveTranslateSpeakerDetection? TurnDetection { get; set; }
}
