using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The session configuration of an audio chat request (qwen-audio-3.x-realtime models), sent
/// via session.update and echoed by session.created / session.updated. Only include the members
/// to change - omitted ones keep their current value; Id / Object / Model are server echoes.
/// </summary>
public class AudioChatSession
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
    /// The output modalities: ["text"] or ["audio", "text"] (default).
    /// </summary>
    public List<string>? Modalities { get; set; }

    /// <summary>
    /// The voice of the synthesized audio: a system voice or the voice id of a cloned voice.
    /// Defaults to longanqian_v3.1 (3.1 Plus) or longanqian (3.0 Plus / Flash). Only the first
    /// session.update takes effect.
    /// </summary>
    public string? Voice { get; set; }

    /// <summary>
    /// Whether to enhance the emotion of the synthesized audio, defaults to true.
    /// </summary>
    public bool? EnableSpeechEmotion { get; set; }

    /// <summary>
    /// The system instructions of the session: the role, style and behavior preferences of
    /// the assistant.
    /// </summary>
    public string? Instructions { get; set; }

    /// <summary>
    /// The format of the input audio, pcm (16kHz 16bit mono) only. Modifiable only before the
    /// first audio is sent.
    /// </summary>
    public string? InputAudioFormat { get; set; }

    /// <summary>
    /// The format of the output audio, pcm (24kHz 16bit mono) only.
    /// </summary>
    public string? OutputAudioFormat { get; set; }

    /// <summary>
    /// The input audio transcription configuration. qwen-audio-3.1-realtime-plus only.
    /// </summary>
    public AudioChatTranscription? InputAudioTranscription { get; set; }

    /// <summary>
    /// The output audio configuration. qwen-audio-3.1-realtime-plus only.
    /// </summary>
    public AudioChatOutputAudio? OutputAudio { get; set; }

    /// <summary>
    /// The maximal count of history QA turns kept in the context, range [1, 50], defaults
    /// to 20.
    /// </summary>
    public int? MaxHistoryTurns { get; set; }

    /// <summary>
    /// Whether to enable web search, defaults to false. Cannot be enabled together with
    /// <see cref="Tools"/>.
    /// </summary>
    public bool? EnableSearch { get; set; }

    /// <summary>
    /// The web search options, only effective when <see cref="EnableSearch"/> is true.
    /// </summary>
    public AudioChatSearchOptions? SearchOptions { get; set; }

    /// <summary>
    /// The function calling tools the model may call. Cannot be enabled together with
    /// <see cref="EnableSearch"/>.
    /// </summary>
    public List<ToolDefinition>? Tools { get; set; }

    /// <summary>
    /// The turn detection configuration, modifiable only before the first audio is sent. A
    /// non-null value enables server_vad or smart_turn with the given settings;
    /// <see cref="AudioChatTurnDetection.ServerDefault"/> leaves the member out, so the server
    /// keeps its default turn detection; null is sent as an explicit <c>turn_detection</c> null
    /// and switches to the push-to-talk mode, where the client submits the audio
    /// (input_audio_buffer.commit) and triggers the inference (response.create) manually.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public AudioChatTurnDetection? TurnDetection { get; set; }
}
