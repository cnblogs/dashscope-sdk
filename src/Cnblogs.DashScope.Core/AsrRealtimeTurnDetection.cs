namespace Cnblogs.DashScope.Core;

/// <summary>
/// The VAD (Voice Activity Detection) configuration of an ASR realtime session. When
/// <see cref="AsrRealtimeSession.TurnDetection"/> is null the session runs in manual mode, in
/// which the client submits the buffered audio for recognition with
/// <see cref="AsrRealtimeSocketSession.CommitAudioAsync"/> instead.
/// </summary>
public class AsrRealtimeTurnDetection
{
    /// <summary>
    /// Stands for the server default VAD configuration in <see cref="AsrRealtimeSession.TurnDetection"/>:
    /// the turn_detection member is left out of the session.update event, so the server keeps its
    /// default settings (the VAD mode). Recognized by reference, do not modify it.
    /// </summary>
    public static readonly AsrRealtimeTurnDetection ServerDefault = new();

    /// <summary>
    /// The type of turn detection, fixed to server_vad.
    /// </summary>
    public string Type { get; set; } = "server_vad";

    /// <summary>
    /// The sensitivity of the VAD, range [-1, 1], defaults to 0.2, 0.0 recommended. Lower values
    /// make the VAD more sensitive (background noise may be classified as speech), higher values
    /// less sensitive (some speech may be filtered in noisy environments).
    /// </summary>
    public float? Threshold { get; set; }

    /// <summary>
    /// How long the silence lasts before a turn is considered finished, in milliseconds, range
    /// [200, 6000], defaults to 800, 400 recommended. Lower values make the model respond faster
    /// but may split sentences at natural pauses, higher values handle pauses inside long
    /// sentences better at the cost of latency.
    /// </summary>
    public int? SilenceDurationMs { get; set; }

    /// <summary>
    /// Whether to create a response when a turn ends, always true. Server side only.
    /// </summary>
    public bool? CreateResponse { get; set; }

    /// <summary>
    /// Whether an ongoing response is interrupted when a new turn starts, always true.
    /// Server side only.
    /// </summary>
    public bool? InterruptResponse { get; set; }
}
