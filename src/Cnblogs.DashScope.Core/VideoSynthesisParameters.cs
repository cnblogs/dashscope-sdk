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
    public string? Size { get; set; }

    /// <inheritdoc />
    public bool? Audio { get; set; }

    /// <inheritdoc />
    public string? AudioSetting { get; set; }

    /// <inheritdoc />
    public bool? PromptExtend { get; set; }

    /// <inheritdoc />
    public string? ShotType { get; set; }

    /// <inheritdoc />
    public bool? Watermark { get; set; }

    /// <inheritdoc />
    public string? ControlCondition { get; set; }

    /// <inheritdoc />
    public double? Strength { get; set; }

    /// <inheritdoc />
    public List<string>? ObjOrBg { get; set; }

    /// <inheritdoc />
    public string? MaskType { get; set; }

    /// <inheritdoc />
    public double? ExpandRatio { get; set; }

    /// <inheritdoc />
    public string? ExpandMode { get; set; }

    /// <inheritdoc />
    public double? TopScale { get; set; }

    /// <inheritdoc />
    public double? BottomScale { get; set; }

    /// <inheritdoc />
    public double? LeftScale { get; set; }

    /// <inheritdoc />
    public double? RightScale { get; set; }

    /// <inheritdoc />
    public ulong? Seed { get; set; }
}
