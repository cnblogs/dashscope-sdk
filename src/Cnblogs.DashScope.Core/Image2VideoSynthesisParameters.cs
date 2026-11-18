namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for image2video synthesis tasks.
/// </summary>
public class Image2VideoSynthesisParameters : IImage2VideoSynthesisParameters
{
    /// <inheritdoc />
    public string? Mode { get; set; }

    /// <inheritdoc />
    public bool? CheckImage { get; set; }

    /// <inheritdoc />
    public string? Resolution { get; set; }

    /// <inheritdoc />
    public int? Duration { get; set; }

    /// <inheritdoc />
    public bool? PromptExtend { get; set; }

    /// <inheritdoc />
    public bool? Watermark { get; set; }

    /// <inheritdoc />
    public bool? VideoExtension { get; set; }

    /// <inheritdoc />
    public int? QueryFaceThreshold { get; set; }

    /// <inheritdoc />
    public ulong? Seed { get; set; }
}
