using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The session configuration of an omni realtime session (qwen3.8-omni-flash-realtime,
/// qwen3.5-omni-plus-realtime, qwen3.5-omni-flash-realtime), sent in a session.update event and
/// echoed by the session.created / session.updated events. Only include the members to change -
/// omitted ones keep their current value; Id / Object / Model are server echoes.
/// </summary>
public class OmniRealtimeSession
{
    /// <summary>
    /// The id of the session. Server echo.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Always realtime.session. Server echo.
    /// </summary>
    public string? Object { get; set; }

    /// <summary>
    /// The model of the session. Server echo.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// The output modalities: ["text"] to answer in text only, or ["text", "audio"] (default) to
    /// answer in text and audio.
    /// </summary>
    public List<string>? Modalities { get; set; }

    /// <summary>
    /// The voice of the synthesized audio, a system voice (defaults to Tina) or the id of a voice
    /// cloned with qwen-omni-voice-cloning. <see cref="OmniRealtimeAudioOutput.Voice"/> wins when
    /// both are set.
    /// </summary>
    public string? Voice { get; set; }

    /// <summary>
    /// The input and output audio configuration: the formats with their sample rates, the voice
    /// (<see cref="OmniRealtimeAudioOutput.Voice"/>) and the multichannel input of
    /// qwen3.8-omni-flash-realtime.
    /// </summary>
    public OmniRealtimeAudio? Audio { get; set; }

    /// <summary>
    /// The input audio transcription configuration. Defaults to
    /// <see cref="OmniRealtimeTranscription.Omit"/>: the member is left out and the server keeps the
    /// transcription it starts a session with (enabled). A configuration enables it with the given
    /// model; null is sent as an explicit <c>input_audio_transcription</c> null and disables the
    /// transcription. Null in the sessions of the server events as well, which lack the member
    /// when the transcription is off.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public OmniRealtimeTranscription? InputAudioTranscription { get; set; } = OmniRealtimeTranscription.Omit;

    /// <summary>
    /// The format of the input audio, pcm or wav. A legacy member kept for the existing clients,
    /// prefer <see cref="OmniRealtimeAudio"/> which takes the sample rate as well.
    /// </summary>
    public string? InputAudioFormat { get; set; }

    /// <summary>
    /// The format of the output audio, pcm or wav. A legacy member kept for the existing clients,
    /// prefer <see cref="OmniRealtimeAudio"/>.
    /// </summary>
    public string? OutputAudioFormat { get; set; }

    /// <summary>
    /// Whether to answer in a colloquial (the default) or in a formal, written style (false).
    /// qwen3-omni-flash-realtime-2025-09-15 only.
    /// </summary>
    public bool? SmoothOutput { get; set; }

    /// <summary>
    /// The system instructions of the session: the goal and the role of the model.
    /// </summary>
    public string? Instructions { get; set; }

    /// <summary>
    /// The turn detection configuration. Defaults to <see cref="OmniRealtimeTurnDetection.Omit"/>:
    /// the member is left out and the server keeps the turn detection it starts a session with
    /// (server_vad). A configuration enables the automatic turn detection with the given settings;
    /// null is sent as an explicit <c>turn_detection</c> null and switches to the manual mode, where
    /// the client commits the audio (input_audio_buffer.commit) and triggers the response
    /// (response.create) itself. Null in the sessions of the server events as well, which lack the
    /// member when the turn detection is off.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public OmniRealtimeTurnDetection? TurnDetection { get; set; } = OmniRealtimeTurnDetection.Omit;

    /// <summary>
    /// Whether to enable the web search, defaults to false. Cannot be enabled together with
    /// <see cref="Tools"/>.
    /// </summary>
    public bool? EnableSearch { get; set; }

    /// <summary>
    /// The web search options, only effective when <see cref="EnableSearch"/> is true.
    /// </summary>
    public OmniRealtimeSearchOptions? SearchOptions { get; set; }

    /// <summary>
    /// The video input configuration. qwen3.8-omni-flash-realtime.
    /// </summary>
    public OmniRealtimeVideo? Video { get; set; }

    /// <summary>
    /// The tools the model may call: function tools executed by the client and MCP servers whose
    /// tools are executed by the server. Cannot be enabled together with
    /// <see cref="EnableSearch"/>. In the sessions of the server events the functions are echoed,
    /// the MCP servers without their url and credentials.
    /// </summary>
    public List<OmniRealtimeTool>? Tools { get; set; }

    /// <summary>
    /// The sampling temperature, range [0, 2). Higher values answer more diversely.
    /// </summary>
    public float? Temperature { get; set; }

    /// <summary>
    /// The nucleus sampling threshold, range (0, 1.0]. Advised to set either this or
    /// <see cref="Temperature"/>.
    /// </summary>
    public float? TopP { get; set; }

    /// <summary>
    /// The size of the sampling candidates, 0 or more. Disabled (top_p only) when greater than 100.
    /// </summary>
    public int? TopK { get; set; }

    /// <summary>
    /// The maximal token count of a response, e.g. to bound the cost or the response time. The
    /// response is truncated beyond it. [1, 65536] for qwen3.8-omni-flash-realtime, the maximal
    /// output length of the model for the others.
    /// </summary>
    public int? MaxTokens { get; set; }

    /// <summary>
    /// The penalty on repeated sequences, 1.0 for none. qwen3.8-omni-flash-realtime also takes 0.
    /// </summary>
    public float? RepetitionPenalty { get; set; }

    /// <summary>
    /// The penalty on tokens already present, range [-2.0, 2.0]. Positive values answer more
    /// diversely, negative ones more consistently.
    /// </summary>
    public float? PresencePenalty { get; set; }

    /// <summary>
    /// The seed for reproducible responses, 0 to 2^31 - 1, -1 for none.
    /// </summary>
    public int? Seed { get; set; }
}
