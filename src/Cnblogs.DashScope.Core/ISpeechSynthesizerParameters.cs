namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for speech synthesizer task.
/// </summary>
public interface ISpeechSynthesizerParameters
{
    /// <summary>
    /// The voice to use. Use <see cref="SpeechSynthesizerVoices"/> to see all options.
    /// </summary>
    public string Voice { get; }

    /// <summary>
    /// The format of generated audio. Use <see cref="SpeechSynthesizerAudioFormats"/> to see all options.
    /// </summary>
    public string? Format { get; }

    /// <summary>
    /// The sample rate of generated audio. Can be one of 8000, 16000, 22050, 24000, 44100 and 48000.
    /// </summary>
    public int? SampleRate { get; }

    /// <summary>
    /// The volume of generated audio, range from 0 to 100, defaults to 50.
    /// </summary>
    public int? Volume { get; }

    /// <summary>
    /// The rate of speech, range from 0.5 to 2, defaults to 1.0.
    /// </summary>
    public double? Rate { get; }

    /// <summary>
    /// The rate of pitch, range from 0.5 to 2, defaults to 1.0.
    /// </summary>
    public double? Pitch { get; }

    /// <summary>
    /// The type of text, currently can only be PlainText.
    /// </summary>
    public string TextType { get; }

    /// <summary>
    /// Is timestamp enabled.
    /// </summary>
    public bool WordTimestampEnabled { get; }

    /// <summary>
    /// Is phoneme timestamp enabled.
    /// </summary>
    public bool PhonemeTimestampEnabled { get; }
}
