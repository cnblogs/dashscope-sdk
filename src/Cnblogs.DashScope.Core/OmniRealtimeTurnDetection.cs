namespace Cnblogs.DashScope.Core;

/// <summary>
/// The turn detection configuration of an omni realtime session. A non-null value enables the
/// automatic turn detection: the server detects the speech boundaries, commits the audio and
/// triggers the response by itself. Which types a model supports differs - the qwen3.5-omni-*-realtime
/// models and qwen3.8-omni-flash-realtime take server_vad and semantic_vad, the latter also
/// semantic_vad_ec. A null <see cref="OmniRealtimeSession.TurnDetection"/> switches to the manual
/// mode, where the client commits the audio and triggers the response.
/// </summary>
public class OmniRealtimeTurnDetection
{
    /// <summary>
    /// Stands for leaving the turn_detection member out of the session.update event, the default
    /// of <see cref="OmniRealtimeSession.TurnDetection"/>: the server keeps the turn detection it
    /// applies by itself (server_vad). Recognized by reference, do not modify it.
    /// </summary>
    public static readonly OmniRealtimeTurnDetection Omit = new();

    /// <summary>
    /// The type of the turn detection: server_vad (default, acoustic only), semantic_vad (filters
    /// meaningless sounds such as backchannel responses and background noise as well) or
    /// semantic_vad_ec (a semantic detection that is easier to trigger and to interrupt the model
    /// with, for customer service and business scenarios; qwen3.8-omni-flash-realtime).
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The sensitivity of the VAD, range [-1.0, 1.0], defaults to 0.5. Lower values are more
    /// sensitive.
    /// </summary>
    public float? Threshold { get; set; }

    /// <summary>
    /// The minimal silence (milliseconds) after speech before the turn ends, range [200, 6000],
    /// defaults to 800. Lower values respond faster but may split at pauses inside a sentence.
    /// </summary>
    public int? SilenceDurationMs { get; set; }

    /// <summary>
    /// How long the user may stay silent (milliseconds) before the model takes the initiative to
    /// keep the conversation going, range [5000, 30000]. qwen3.5-omni-*-realtime models in the
    /// server_vad mode only.
    /// </summary>
    public int? IdleTimeoutMs { get; set; }

    /// <summary>
    /// The audio (milliseconds) kept before the detected speech starts. Server echo.
    /// </summary>
    public int? PrefixPaddingMs { get; set; }

    /// <summary>
    /// Whether to create a response when a turn ends. Server echo.
    /// </summary>
    public bool? CreateResponse { get; set; }

    /// <summary>
    /// Whether to interrupt the response in progress when new speech starts. Server echo.
    /// </summary>
    public bool? InterruptResponse { get; set; }
}
