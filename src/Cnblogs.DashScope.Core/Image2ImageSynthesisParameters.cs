namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for image-to-image synthesis tasks.
/// </summary>
public class Image2ImageSynthesisParameters : IImage2ImageSynthesisParameters
{
    /// <inheritdoc />
    public string? Size { get; set; }

    /// <inheritdoc />
    public int? N { get; set; }

    /// <inheritdoc />
    public ulong? Seed { get; set; }

    /// <inheritdoc />
    public bool? PromptExtend { get; set; }

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

    /// <inheritdoc />
    public string? Style { get; set; }

    /// <inheritdoc />
    public List<List<int>>? MaskColor { get; set; }

    /// <inheritdoc />
    public int? SketchWeight { get; set; }

    /// <inheritdoc />
    public bool? SketchExtraction { get; set; }

    /// <inheritdoc />
    public List<List<int>>? SketchColor { get; set; }
}
