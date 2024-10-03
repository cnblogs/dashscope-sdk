namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for speech synthesizer task.
/// </summary>
public class SpeechSynthesizerParameters : ISpeechSynthesizerParameters
{
    /// <inheritdoc />
    public string Voice { get; set; } = SpeechSynthesizerVoices.LongXiaoChun;

    /// <inheritdoc />
    public string? Format { get; set; } = "Default";

    /// <inheritdoc />
    public int? SampleRate { get; set; } = 0;

    /// <inheritdoc />
    public int? Volume { get; set; }

    /// <inheritdoc />
    public double? Rate { get; set; }

    /// <inheritdoc />
    public double? Pitch { get; set; }

    /// <inheritdoc />
    public string TextType { get; set; } = "PlainText";

    /// <inheritdoc />
    public bool WordTimestampEnabled { get; set; }

    /// <inheritdoc />
    public bool PhonemeTimestampEnabled { get; set; }
}
