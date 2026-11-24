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
    /// The VAD configuration, defaults to <see cref="AsrRealtimeTurnDetection.Omit"/>: the member
    /// is left out and the server keeps the VAD settings it starts a session with. A configuration
    /// enables the VAD mode with the given settings, in which the server detects speech boundaries
    /// and submits the audio for recognition automatically. null is sent as an explicit
    /// <c>turn_detection</c> null and switches to the manual mode, in which the client calls
    /// <see cref="AsrRealtimeSocketSession.CommitAudioAsync"/> to submit the buffered audio. Null
    /// in the sessions of the server events as well, which lack the member when the VAD is off.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public AsrRealtimeTurnDetection? TurnDetection { get; set; } = AsrRealtimeTurnDetection.Omit;
}
