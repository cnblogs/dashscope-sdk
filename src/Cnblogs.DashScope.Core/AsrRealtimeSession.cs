using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The session configuration of the ASR realtime API (qwen3-asr-flash-realtime models). Sent in
/// a session.update event and returned in the session.created / session.updated events. Which
/// members are present depends on the direction: <see cref="Id"/>, <see cref="Object"/> and
/// <see cref="Model"/> are server side only, the others configure the recognition.
/// </summary>
public class AsrRealtimeSession
{
    /// <summary>
    /// The id of the session, e.g. sess_xxx. Server side only.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The object type, always realtime.session. Server side only.
    /// </summary>
    public string? Object { get; set; }

    /// <summary>
    /// The model in use. Server side only.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// The modalities of the session, text so far. Server side only.
    /// </summary>
    public List<string>? Modalities { get; set; }

    /// <summary>
    /// The format of the input audio: pcm (default) or opus.
    /// </summary>
    public string? InputAudioFormat { get; set; }

    /// <summary>
    /// The sample rate of the input audio in Hz: 16000 (default) or 8000. An 8000 Hz source is
    /// upsampled to 16000 Hz server side, which adds a small latency - only use it for 8000 Hz
    /// audio (e.g. phone lines).
    /// </summary>
    public int? SampleRate { get; set; }

    /// <summary>
    /// The recognition configuration, e.g. the language of the audio.
    /// </summary>
    public AsrRealtimeTranscription? InputAudioTranscription { get; set; }

    /// <summary>
    /// The VAD configuration: a non-null value enables the VAD mode, in which the server detects
    /// speech boundaries and submits the audio for recognition automatically. Keep it null for
    /// the manual mode, in which the client calls
    /// <see cref="AsrRealtimeSocketSession.CommitAudioAsync"/> to submit the buffered audio,
    /// and set <see cref="DisableTurnDetection"/> to switch the server to it.
    /// </summary>
    public AsrRealtimeTurnDetection? TurnDetection { get; set; }

    /// <summary>
    /// Send an explicit <c>turn_detection</c> null in the session.update event, which switches
    /// the session to the manual mode (the server starts a session with the VAD mode enabled).
    /// Set it to true and leave <see cref="TurnDetection"/> null.
    /// </summary>
    [JsonIgnore]
    public bool DisableTurnDetection { get; set; }
}
