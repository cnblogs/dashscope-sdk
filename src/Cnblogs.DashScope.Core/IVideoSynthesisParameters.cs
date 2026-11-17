namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for video synthesis tasks.
/// </summary>
public interface IVideoSynthesisParameters
{
    /// <summary>
    /// The resolution of the output video: 480P, 720P or 1080P (default).
    /// </summary>
    string? Resolution { get; set; }

    /// <summary>
    /// The aspect ratio of the output video, e.g. 16:9 (default), 9:16 or 1:1.
    /// Not available for video editing.
    /// </summary>
    string? Ratio { get; set; }

    /// <summary>
    /// The duration of the output video in seconds, range 3-15, defaults to 5.
    /// Not available for video editing.
    /// </summary>
    int? Duration { get; set; }

    /// <summary>
    /// Whether to add an AI-generated watermark on the bottom right corner, defaults to <c>true</c>.
    /// </summary>
    bool? Watermark { get; set; }
}
