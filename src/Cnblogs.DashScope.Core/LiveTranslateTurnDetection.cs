namespace Cnblogs.DashScope.Core;

/// <summary>
/// The turn detection configuration of a live translation session. qwen3.5-livetranslate-flash-realtime
/// only. A non-null value enables the VAD mode where the server detects the speech boundaries,
/// submits the audio buffer and triggers the translation automatically; a null
/// <see cref="LiveTranslateSession.TurnDetection"/> switches to the manual mode.
/// </summary>
public class LiveTranslateTurnDetection
{
    /// <summary>
    /// Stands for the server default VAD configuration in
    /// <see cref="LiveTranslateSession.TurnDetection"/>: the turn_detection member is left out of
    /// the session.update event, so the server keeps its default settings (the VAD mode).
    /// Recognized by reference, do not modify it.
    /// </summary>
    public static readonly LiveTranslateTurnDetection ServerDefault = new();

    /// <summary>
    /// The type of the VAD, fixed to server_vad.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The sensitivity of the VAD, range [-1.0, 1.0], defaults to 0.2. Lower values are more
    /// sensitive.
    /// </summary>
    public float? Threshold { get; set; }

    /// <summary>
    /// The minimal silence (milliseconds) after speech before the audio buffer is submitted,
    /// range [200, 6000], defaults to 1000.
    /// </summary>
    public int? SilenceDurationMs { get; set; }

    /// <summary>
    /// The audio (milliseconds) kept before the detected speech starts. Server echo.
    /// </summary>
    public int? PrefixPaddingMs { get; set; }

    /// <summary>
    /// Whether to trigger the translation automatically once the speech ends. Server echo.
    /// </summary>
    public bool? CreateResponse { get; set; }

    /// <summary>
    /// Whether to interrupt the translation in progress when new speech starts. Server echo.
    /// </summary>
    public bool? InterruptResponse { get; set; }
}
