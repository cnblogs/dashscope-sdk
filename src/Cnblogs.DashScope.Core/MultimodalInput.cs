using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents inputs of a multi-model generation request, shared by all models on this
/// endpoint: multimodal chat (qwen-vl / qwen-audio / qwen-video), OCR, image generation
/// (qwen-image / z-image / wan t2i), speech synthesis (qwen3-tts-flash, MiniMax speech),
/// speech recognition (qwen3-asr-flash, Fun-ASR-Flash / Qwen-Audio-3.x-ASR-Flash) and
/// other models. Which members are supported depends on the model family.
/// </summary>
public class MultimodalInput : IDashScopeOssUploadConfig
{
    /// <summary>
    /// The messages of context, model will generate from last user message.
    /// Null for speech synthesis models, which take <see cref="Text"/> and voice
    /// settings instead; omitted from the request body in that case.
    /// </summary>
    public IEnumerable<MultimodalMessage>? Messages { get; set; }

    /// <summary>
    /// The text to synthesize, required by speech synthesis models (qwen3-tts-flash,
    /// MiniMax speech). qwen3-tts-flash accepts at most 600 characters, MiniMax speech
    /// less than 10000 characters (streaming output is recommended beyond 3000).
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// The voice used for synthesis, required by qwen3-tts-flash models.
    /// </summary>
    public string? Voice { get; set; }

    /// <summary>
    /// The language of the synthesized audio, supported by qwen3-tts-flash models,
    /// defaults to <c>Auto</c>. Specifying a single language (Chinese, English, German,
    /// Italian, Portuguese, Spanish, Japanese, Korean, French, Russian) usually yields
    /// better results than <c>Auto</c> on single-language text.
    /// </summary>
    public string? LanguageType { get; set; }

    /// <summary>
    /// The voice settings, required by MiniMax speech models.
    /// </summary>
    public MinimaxVoiceSetting? VoiceSetting { get; set; }

    /// <summary>
    /// The audio settings, supported by MiniMax speech models.
    /// </summary>
    public MinimaxAudioSetting? AudioSetting { get; set; }

    /// <summary>
    /// The pronunciation replacement rules, supported by MiniMax speech models.
    /// </summary>
    public MinimaxPronunciationDict? PronunciationDict { get; set; }

    /// <summary>
    /// The mixed voices and their weights (at most 4), supported by MiniMax speech models;
    /// when set, <see cref="MinimaxVoiceSetting.VoiceId"/> should be left empty.
    /// </summary>
    public List<MinimaxTimbreWeight>? TimbreWeights { get; set; }

    /// <summary>
    /// Whether to enhance recognition of the specified language or dialect
    /// (e.g. Chinese, English, auto), supported by MiniMax speech models.
    /// </summary>
    public string? LanguageBoost { get; set; }

    /// <summary>
    /// The voice effects, supported by MiniMax speech models.
    /// </summary>
    public MinimaxVoiceModify? VoiceModify { get; set; }

    /// <summary>
    /// Whether to enable subtitles, supported by MiniMax speech models in non-streaming
    /// mode only.
    /// </summary>
    public bool? SubtitleEnable { get; set; }

    /// <summary>
    /// The form of the returned audio: url (24h valid download url) or hex (binary in hex
    /// string), supported by MiniMax speech models in non-streaming mode only, defaults
    /// to hex.
    /// </summary>
    public string? OutputFormat { get; set; }

    /// <summary>
    /// Whether to append the invisible AIGC watermark to the synthesized audio,
    /// supported by MiniMax speech models in non-streaming mode only.
    /// </summary>
    public bool? AigcWatermark { get; set; }

    /// <summary>
    /// The streaming output options, supported by MiniMax speech models in streaming
    /// mode only (when the SSE header is enabled).
    /// </summary>
    public MinimaxStreamOptions? StreamOptions { get; set; }

    /// <summary>
    /// The operation to perform, "voice_clone" clones a voice from the sample audio.
    /// MiniMax speech models only (shared domain, not the workspace domain).
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// The url of the audio to clone: mp3, m4a or wav, 10 seconds to 5 minutes, at most
    /// 20 MB. MiniMax voice cloning only.
    /// </summary>
    public string? AudioUrl { get; set; }

    /// <summary>
    /// The id of the cloned voice: 8~256 characters, starting with a letter, letters /
    /// digits / - / _ only, must not end with - or _, globally unique. MiniMax voice
    /// cloning only.
    /// </summary>
    public string? VoiceId { get; set; }

    /// <summary>
    /// The prompt sample improving the cloning similarity and stability.
    /// MiniMax voice cloning only.
    /// </summary>
    public MinimaxClonePrompt? ClonePrompt { get; set; }

    /// <summary>
    /// Whether to denoise the audio to clone, defaults to false.
    /// MiniMax voice cloning only.
    /// </summary>
    public bool? NeedNoiseReduction { get; set; }

    /// <summary>
    /// Whether to normalize the volume of the audio to clone, defaults to false.
    /// MiniMax voice cloning only.
    /// </summary>
    public bool? NeedVolumeNormalization { get; set; }

    /// <inheritdoc />
    public bool EnableOssResolve() => Messages?.Any(m => m.IsOss()) == true;
}
