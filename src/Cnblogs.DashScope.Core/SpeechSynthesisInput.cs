namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of a speech synthesis request of qwen-audio-tts models. This API is only
/// served on the workspace domain, configure <see cref="DashScopeOptions.WorkspaceId"/>
/// (same region as the API key) to route requests there.
/// </summary>
public class SpeechSynthesisInput
{
    /// <summary>
    /// The text to synthesize. Supports SSML (requires <see cref="EnableSsml"/>) and
    /// LaTeX formulas.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The voice: a system voice, a cloned voice or a designed voice.
    /// </summary>
    public string Voice { get; set; } = string.Empty;

    /// <summary>
    /// The audio format: mp3 (default), pcm, wav or opus.
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// The sample rate in Hz: 8000, 12000, 16000, 22050 (default), 24000, 44100 or 48000.
    /// Opus only supports 8000, 12000, 16000, 24000 and 48000.
    /// </summary>
    public int? SampleRate { get; set; }

    /// <summary>
    /// The volume, range [0, 100], defaults to 50.
    /// </summary>
    public int? Volume { get; set; }

    /// <summary>
    /// The speech rate, range [0.5, 2.0], defaults to 1.0.
    /// </summary>
    public float? Rate { get; set; }

    /// <summary>
    /// The bitrate in kbps, range [6, 510], defaults to 32. Opus only.
    /// </summary>
    public int? BitRate { get; set; }

    /// <summary>
    /// The pitch, range [0.5, 2.0], defaults to 1.0.
    /// </summary>
    public float? Pitch { get; set; }

    /// <summary>
    /// Whether the text is SSML, defaults to false.
    /// </summary>
    public bool? EnableSsml { get; set; }

    /// <summary>
    /// Whether to enable word level timestamps, defaults to false. Streaming mode only;
    /// word info is returned in sentence-end events.
    /// </summary>
    public bool? WordTimestampEnabled { get; set; }

    /// <summary>
    /// The random seed, range [0, 65535], defaults to 0. The same seed reproduces the
    /// same synthesis when other parameters are equal.
    /// </summary>
    public int? Seed { get; set; }

    /// <summary>
    /// The target language of the synthesis (e.g. zh, en, ja), improves the reading of
    /// digits, abbreviations and symbols. Only the first element is used.
    /// </summary>
    public List<string>? LanguageHints { get; set; }

    /// <summary>
    /// The instruction controlling dialect, emotion or role effects.
    /// </summary>
    public string? Instruction { get; set; }

    /// <summary>
    /// Whether to embed the invisible AIGC tag into the audio (wav / mp3 / opus),
    /// defaults to false.
    /// </summary>
    public bool? EnableAigcTag { get; set; }

    /// <summary>
    /// The ContentPropagator field of the AIGC tag, defaults to the Alibaba Cloud UID.
    /// Only takes effect when <see cref="EnableAigcTag"/> is true.
    /// </summary>
    public string? AigcPropagator { get; set; }

    /// <summary>
    /// The PropagateID field of the AIGC tag, defaults to the request id.
    /// Only takes effect when <see cref="EnableAigcTag"/> is true.
    /// </summary>
    public string? AigcPropagateId { get; set; }

    /// <summary>
    /// The hot fix configuration: pronunciation corrections and text replacements
    /// applied before synthesis.
    /// </summary>
    public SpeechSynthesizerHotFix? HotFix { get; set; }
}
