namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of a voice customization request (voice cloning and voice designing via the
/// voice-enrollment model). The action picks the operation, which members are required
/// depends on it; this API is only served on the workspace domain, configure
/// <see cref="DashScopeOptions.WorkspaceId"/> to route requests there.
/// </summary>
public class VoiceCustomizationInput
{
    /// <summary>
    /// The operation to perform: create_voice, list_voice, query_voice, update_voice
    /// or delete_voice.
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// The model driving the cloned or designed voice, must match the model of subsequent
    /// synthesis requests. create_voice only.
    /// </summary>
    public string? TargetModel { get; set; }

    /// <summary>
    /// The reference audio for cloning: a public url or a base64 data uri
    /// (e.g. <c>data:audio/wav;base64,...</c>). create_voice (cloning) and update_voice.
    /// </summary>
    public string? Audio { get; set; }

    /// <summary>
    /// The prefix of the voice name, digits and letters only, at most 10 characters.
    /// The generated voice id follows <c>{target_model}-{prefix}-{id}</c> for cloning
    /// and <c>{target_model}-vd-{prefix}-{id}</c> for designing. create_voice, and the
    /// filter of list_voice.
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// The language of the sample audio or of the voice to generate (a single element),
    /// improves the cloning and designing effect. create_voice only, defaults to ["zh"].
    /// </summary>
    public List<string>? LanguageHints { get; set; }

    /// <summary>
    /// The maximal duration (seconds, range [3.0, 30.0]) of the reference audio used for
    /// cloning after preprocessing, defaults to 10. create_voice (cloning) only.
    /// </summary>
    public float? MaxPromptAudioLength { get; set; }

    /// <summary>
    /// Whether to enable audio preprocessing (noise reduction, enhancement and volume
    /// leveling); recommended for noisy samples, disable to best preserve the timbre in
    /// quiet recordings. Defaults to false. create_voice (cloning) only.
    /// </summary>
    public bool? EnablePreprocess { get; set; }

    /// <summary>
    /// Whether to normalize the volume of the sample audio, defaults to false. The volume
    /// of audio synthesized with the voice differs between voices created with this
    /// setting on and off. create_voice (cloning) only.
    /// </summary>
    public bool? EnableVolumeNormalization { get; set; }

    /// <summary>
    /// The text describing the voice to design (Chinese or English, at most 500
    /// characters). create_voice (designing) only.
    /// </summary>
    public string? VoicePrompt { get; set; }

    /// <summary>
    /// The text of the preview audio (15~200 characters, Chinese or English).
    /// create_voice (designing) only.
    /// </summary>
    public string? PreviewText { get; set; }

    /// <summary>
    /// The id of the voice to query, update or delete. query_voice, update_voice and
    /// delete_voice.
    /// </summary>
    public string? VoiceId { get; set; }

    /// <summary>
    /// The page index of list_voice.
    /// </summary>
    public int? PageIndex { get; set; }

    /// <summary>
    /// The page size of list_voice.
    /// </summary>
    public int? PageSize { get; set; }
}
