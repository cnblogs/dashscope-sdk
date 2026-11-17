namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for function-based image edit tasks.
/// </summary>
public class ImageEditFunctionParameters : IImageEditFunctionParameters
{
    /// <inheritdoc />
    public ulong? Seed { get; set; }

    /// <inheritdoc />
    public int? N { get; set; }

    /// <inheritdoc />
    public bool? Watermark { get; set; }

    /// <inheritdoc />
    public float? Strength { get; set; }

    /// <inheritdoc />
    public float? TopScale { get; set; }

    /// <inheritdoc />
    public float? BottomScale { get; set; }

    /// <inheritdoc />
    public float? LeftScale { get; set; }

    /// <inheritdoc />
    public float? RightScale { get; set; }

    /// <inheritdoc />
    public int? UpscaleFactor { get; set; }

    /// <inheritdoc />
    public bool? IsSketch { get; set; }
}
