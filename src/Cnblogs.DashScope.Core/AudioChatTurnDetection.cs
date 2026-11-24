namespace Cnblogs.DashScope.Core;

/// <summary>
/// The turn detection configuration of an audio chat session. A non-null value enables the
/// automatic turn detection: <c>server_vad</c> (acoustic only) or <c>smart_turn</c> (acoustic
/// plus semantic, meaningless sounds such as "um" do not trigger a turn); a null
/// <see cref="AudioChatSession.TurnDetection"/> switches to the push-to-talk mode.
/// </summary>
public class AudioChatTurnDetection
{
    /// <summary>
    /// Stands for leaving the turn_detection member out of the session.update event, the default
    /// of <see cref="AudioChatSession.TurnDetection"/>: the server keeps the turn detection it
    /// applies by itself (server_vad). Recognized by reference, do not modify it.
    /// </summary>
    public static readonly AudioChatTurnDetection Omit = new();

    /// <summary>
    /// The type of the turn detection: server_vad (default) or smart_turn.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The sensitivity of the VAD, range [-1.0, 1.0], defaults to 0.5. Lower values are more
    /// sensitive. server_vad only.
    /// </summary>
    public float? Threshold { get; set; }

    /// <summary>
    /// The minimal silence (milliseconds) after speech before the turn ends, range [200, 6000],
    /// defaults to 800. server_vad only.
    /// </summary>
    public int? SilenceDurationMs { get; set; }

    /// <summary>
    /// The urls of the pre-recorded audio of the target speaker (16kHz pcm or wav, at most 5),
    /// used for speaker enhancement: the model locks onto the target speaker and ignores other
    /// voices. smart_turn only, and only the first session.update takes effect.
    /// </summary>
    public List<string>? VoiceprintAudioUrls { get; set; }
}
