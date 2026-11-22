namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for voice customization requests.
/// </summary>
public class VoiceCustomizationParameters : IVoiceCustomizationParameters
{
    /// <inheritdoc />
    public int? SampleRate { get; set; }

    /// <inheritdoc />
    public string? ResponseFormat { get; set; }
}
