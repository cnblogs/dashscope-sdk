namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for TTS task.
/// </summary>
public class SpeechSynthesizerParameters
{
    /// <summary>
    /// Fixed to "PlainText"
    /// </summary>
    public string TextType { get; set; } = "PlainText";

    /// <summary>
    /// The voice to use.
    /// </summary>
    public string Voice { get; set; } = string.Empty;

    /// <summary>
    /// Output file format, can be pcm, wav or mp3.
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// Output audio sample rate.
    /// </summary>
    public int? SampleRate { get; set; }

    /// <summary>
    /// Output audio volume, range between 0-100, defaults to 50.
    /// </summary>
    public int? Volume { get; set; }

    /// <summary>
    /// Speech speed, range between 0.5~2.0, defaults to 1.0.
    /// </summary>
    public float? Rate { get; set; }

    /// <summary>
    /// Pitch of the voice, range between 0.5~2, defaults to 1.0.
    /// </summary>
    public float? Pitch { get; set; }

    /// <summary>
    /// Bitrate of the output audio in kbps, only available when format is mp3 or opus.
    /// Range between 6-510, defaults to 32.
    /// </summary>
    public int? BitRate { get; set; }

    /// <summary>
    /// Random seed used by the model, the same seed reproduces the same result when other
    /// parameters are unchanged. Range between 0-65535, defaults to 0.
    /// </summary>
    public int? Seed { get; set; }

    /// <summary>
    /// Languages of the input text, used to fix unexpected reading of numbers, symbols or
    /// poor quality on minor languages. Only the first element is used for now.
    /// </summary>
    public List<string>? LanguageHints { get; set; }

    /// <summary>
    /// Instruction for controlling dialect, emotion or role of the synthesized voice.
    /// </summary>
    public string? Instruction { get; set; }

    /// <summary>
    /// Enable SSML, you can only send text once if enabled.
    /// </summary>
    public bool? EnableSsml { get; set; }

    /// <summary>
    /// Enable word level timestamps in the words field of sentence-end events, defaults to false.
    /// Only available in streaming mode, and only supported by selected models and voices.
    /// </summary>
    public bool? WordTimestampEnabled { get; set; }
}
