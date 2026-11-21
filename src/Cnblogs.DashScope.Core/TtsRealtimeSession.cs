namespace Cnblogs.DashScope.Core;

/// <summary>
/// The session configuration of the TTS realtime API (qwen3-tts-*-realtime models).
/// Sent in a session.update event and returned in the session.created / session.updated events.
/// Which members are present depends on the direction: <see cref="Id"/>, <see cref="Object"/> and
/// <see cref="Model"/> are server side only, the others configure the synthesis.
/// </summary>
public class TtsRealtimeSession
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
    /// The interaction mode: server_commit (default, the server decides when to synthesize,
    /// recommended) or commit (the client triggers synthesis with input_text_buffer.commit).
    /// </summary>
    public string? Mode { get; set; }

    /// <summary>
    /// The model in use. Server side only.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// The voice to use, see the voice list in the Bailian documentation. Required in a session.update.
    /// </summary>
    public string? Voice { get; set; }

    /// <summary>
    /// The language of the text: Auto (default, handles mixed languages) or a concrete one -
    /// Chinese, English, German, Italian, Portuguese, Spanish, Japanese, Korean, French or Russian.
    /// Specifying a concrete language improves the synthesis quality for single-language text.
    /// </summary>
    public string? LanguageType { get; set; }

    /// <summary>
    /// The output audio format: pcm (default), wav, mp3 or opus.
    /// </summary>
    public string? ResponseFormat { get; set; }

    /// <summary>
    /// The output audio sample rate in Hz: 8000, 16000, 24000 (default) or 48000.
    /// </summary>
    public int? SampleRate { get; set; }

    /// <summary>
    /// The speech rate, range 0.5~2.0, defaults to 1.0.
    /// </summary>
    public float? SpeechRate { get; set; }

    /// <summary>
    /// The volume, range 0~100, defaults to 50.
    /// </summary>
    public int? Volume { get; set; }

    /// <summary>
    /// The pitch, range 0.5~2.0, defaults to 1.0.
    /// </summary>
    public float? PitchRate { get; set; }

    /// <summary>
    /// The bitrate in kbps, range 6~510, defaults to 128. Only available when the format is opus.
    /// </summary>
    public int? BitRate { get; set; }
}
