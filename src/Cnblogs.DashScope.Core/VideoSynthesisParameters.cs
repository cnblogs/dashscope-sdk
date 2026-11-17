namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for video synthesis tasks.
/// </summary>
public class VideoSynthesisParameters : IVideoSynthesisParameters
{
    /// <inheritdoc />
    public string? Resolution { get; set; }

    /// <inheritdoc />
    public string? Ratio { get; set; }

    /// <inheritdoc />
    public int? Duration { get; set; }

    /// <inheritdoc />
    public bool? Watermark { get; set; }
}
