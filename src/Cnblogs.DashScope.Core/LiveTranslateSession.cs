using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The session configuration of a live translation request (qwen3.5 / qwen3.8-livetranslate-flash-realtime),
/// sent via session.update and echoed by session.created / session.updated. Only include the
/// members to change - omitted ones keep their current value; Id / Object / Model and the
/// nested server echoes (InputModalities, LiveTranslateAudioOutput, prefix_padding_ms and
/// friends) are returned by the server.
/// </summary>
public class LiveTranslateSession
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
    /// The output modalities: ["text"] or ["text", "audio"] (default).
    /// qwen3.8-livetranslate-flash-realtime.
    /// </summary>
    public List<string>? OutputModalities { get; set; }

    /// <summary>
    /// The audio configuration (input speech detection). qwen3.8-livetranslate-flash-realtime.
    /// </summary>
    public LiveTranslateAudio? Audio { get; set; }

    /// <summary>
    /// The input modalities, e.g. ["audio"]. Server echo.
    /// qwen3.8-livetranslate-flash-realtime.
    /// </summary>
    public List<string>? InputModalities { get; set; }

    /// <summary>
    /// The output modalities: ["text"] or ["text", "audio"] (default).
    /// qwen3.5-livetranslate-flash-realtime.
    /// </summary>
    public List<string>? Modalities { get; set; }

    /// <summary>
    /// The voice of the synthesized audio: a system voice (defaults to Tina) or "default" /
    /// the id of a pre-cloned voice when the voice clone is enabled.
    /// </summary>
    public string? Voice { get; set; }

    /// <summary>
    /// Whether to clone the voice from the input audio for the translated output, defaults to
    /// false. When enabled, Voice must be "default" (cloning once / always) or a pre-cloned
    /// voice id.
    /// </summary>
    public bool? EnableVoiceClone { get; set; }

    /// <summary>
    /// The voice clone options, only effective when <see cref="EnableVoiceClone"/> is true.
    /// </summary>
    public LiveTranslateVoiceCloneOptions? VoiceCloneOptions { get; set; }

    /// <summary>
    /// The sample rate of the input audio in Hz: 8000 or 16000 (default).
    /// qwen3.5-livetranslate-flash-realtime.
    /// </summary>
    public int? SampleRate { get; set; }

    /// <summary>
    /// The format of the input audio: pcm (default) or opus.
    /// qwen3.5-livetranslate-flash-realtime.
    /// </summary>
    public string? InputAudioFormat { get; set; }

    /// <summary>
    /// The format of the output audio, pcm only. qwen3.5-livetranslate-flash-realtime.
    /// </summary>
    public string? OutputAudioFormat { get; set; }

    /// <summary>
    /// The input audio transcription (ASR) configuration: the source text comes back via the
    /// conversation.item.input_audio_transcription.* events. Set the model to null explicitly
    /// to disable the ASR. qwen3.5-livetranslate-flash-realtime.
    /// </summary>
    public LiveTranslateTranscription? InputAudioTranscription { get; set; }

    /// <summary>
    /// The turn detection configuration, qwen3.5-livetranslate-flash-realtime. A non-null value
    /// enables the VAD mode with the given settings;
    /// <see cref="LiveTranslateTurnDetection.ServerDefault"/> leaves the member out, so the
    /// server keeps its default VAD settings; null is sent as an explicit <c>turn_detection</c>
    /// null and switches to the manual mode, where the client submits the audio buffer with
    /// input_audio_buffer.commit (which triggers the translation automatically).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public LiveTranslateTurnDetection? TurnDetection { get; set; }

    /// <summary>
    /// The translation configuration.
    /// </summary>
    public LiveTranslateTranslation? Translation { get; set; }
}
