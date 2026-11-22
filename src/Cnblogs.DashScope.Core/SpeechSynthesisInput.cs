namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of the speech synthesizer API (only served on the workspace domain,
/// configure <see cref="DashScopeOptions.WorkspaceId"/> to route requests there):
/// speech synthesis of qwen-audio-tts models takes <see cref="Text"/> and
/// <see cref="Voice"/>; audio generation of qwen-audio-*-tts-next models takes
/// <see cref="TextPrompt"/> with optional reference audios instead. Which members are
/// supported depends on the model family.
/// </summary>
public class SpeechSynthesisInput
{
    /// <summary>
    /// The text to synthesize. Supports SSML (requires <see cref="EnableSsml"/>) and
    /// LaTeX formulas. qwen-audio-tts models; omitted when null (audio generation models
    /// use <see cref="TextPrompt"/> instead).
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// The voice: a system voice, a cloned voice or a designed voice. qwen-audio-tts
    /// models; omitted when null (audio generation models take reference audios instead).
    /// </summary>
    public string? Voice { get; set; }

    /// <summary>
    /// The audio format: mp3 (default), pcm, wav or opus. Opus output is not supported
    /// by qwen-audio-*-tts-next models.
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// The sample rate in Hz. qwen-audio-tts models: 8000, 12000, 16000, 22050 (default),
    /// 24000, 44100 or 48000 (opus only 8000, 12000, 16000, 24000 and 48000);
    /// qwen-audio-*-tts-next models: 8000, 16000, 24000, 44100 or 48000 (default).
    /// </summary>
    public int? SampleRate { get; set; }

    /// <summary>
    /// The volume, range [0, 100], defaults to 50. Available to both model families.
    /// </summary>
    public int? Volume { get; set; }

    /// <summary>
    /// The speech rate, range [0.5, 2.0], defaults to 1.0. Available to both model families.
    /// </summary>
    public float? Rate { get; set; }

    /// <summary>
    /// The bitrate in kbps. Opus output of qwen-audio-tts models: range [6, 510],
    /// defaults to 32; mp3 CBR output of qwen-audio-*-tts-next models: defaults to 128,
    /// constrained by the sample rate and the mp3 encoding profiles.
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

    /// <summary>
    /// The prompt describing the audio to generate or the text to synthesize, at most
    /// 3000 characters. Reference audios are referenced in order as @voice1, @voice2
    /// and @voice3. qwen-audio-*-tts-next models.
    /// </summary>
    public string? TextPrompt { get; set; }

    /// <summary>
    /// The reference audios (at most 3, each up to 30 seconds and 10 MB; wav, mp3 or
    /// ogg opus, no bare pcm), referenced by <see cref="TextPrompt"/> as @voice1...
    /// Omit for pure text generation. qwen-audio-*-tts-next models.
    /// </summary>
    public List<SpeechSynthesisReference>? References { get; set; }

    /// <summary>
    /// The channel count of the generated audio: 1 (mono) or 2 (stereo), defaults to 2.
    /// qwen-audio-*-tts-next models.
    /// </summary>
    public int? Channels { get; set; }

    /// <summary>
    /// Whether to encode mp3 output with a constant bitrate (CBR) instead of a variable
    /// bitrate (VBR), defaults to false. mp3 output of qwen-audio-*-tts-next models.
    /// </summary>
    public bool? EnableCbr { get; set; }

    /// <summary>
    /// The mp3 VBR quality, range [0, 9], 0 is the best. mp3 VBR output of
    /// qwen-audio-*-tts-next models.
    /// </summary>
    public int? Quality { get; set; }
}
