namespace Cnblogs.DashScope.Core;

/// <summary>
/// The speech detection configuration of a live translation session. qwen3.8-livetranslate-flash-realtime
/// only, nested in <see cref="LiveTranslateAudioInput.TurnDetection"/>.
/// </summary>
public class LiveTranslateSpeakerDetection
{
    /// <summary>
    /// The type of the detection: speaker_detection (default, real-time speaker diarization) or
    /// server_vad (voice activity detection).
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The threshold separating silence from valid speech when using speaker_detection; sounds
    /// below it are filtered out and never sent to the model. Fixed to 0.5. Server echo.
    /// </summary>
    public float? Threshold { get; set; }

    /// <summary>
    /// The minimal silence (milliseconds) after speech. Server echo.
    /// </summary>
    public int? SilenceDurationMs { get; set; }
}
