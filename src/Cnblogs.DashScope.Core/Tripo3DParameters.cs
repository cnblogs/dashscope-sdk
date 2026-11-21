namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters of Tripo 3D tasks.
/// </summary>
public class Tripo3DParameters : ITripo3DParameters
{
    /// <inheritdoc />
    public string? TextureQuality { get; set; }

    /// <inheritdoc />
    public string? GeometryQuality { get; set; }

    /// <inheritdoc />
    public bool? Pbr { get; set; }

    /// <inheritdoc />
    public bool? Texture { get; set; }

    /// <inheritdoc />
    public bool? Quad { get; set; }

    /// <inheritdoc />
    public bool? SmartLowPoly { get; set; }

    /// <inheritdoc />
    public bool? GenerateParts { get; set; }

    /// <inheritdoc />
    public string? Compress { get; set; }

    /// <inheritdoc />
    public string? NegativePrompt { get; set; }

    /// <inheritdoc />
    public int? FaceLimit { get; set; }

    /// <inheritdoc />
    public int? ModelSeed { get; set; }

    /// <inheritdoc />
    public int? ImageSeed { get; set; }

    /// <inheritdoc />
    public int? TextureSeed { get; set; }

    /// <inheritdoc />
    public string? TextureAlignment { get; set; }

    /// <inheritdoc />
    public string? Orientation { get; set; }

    /// <inheritdoc />
    public string? ExportOrientation { get; set; }

    /// <inheritdoc />
    public bool? AutoSize { get; set; }

    /// <inheritdoc />
    public bool? ExportUv { get; set; }

    /// <inheritdoc />
    public bool? EnableImageAutofix { get; set; }

    /// <inheritdoc />
    public string? ModelVersion { get; set; }

    /// <inheritdoc />
    public Tripo3DTexturePrompt? TexturePrompt { get; set; }

    /// <inheritdoc />
    public bool? Bake { get; set; }

    /// <inheritdoc />
    public List<string>? PartNames { get; set; }

    /// <inheritdoc />
    public string? SegmentationGranularity { get; set; }

    /// <inheritdoc />
    public bool? SplitByConnectivity { get; set; }

    /// <inheritdoc />
    public string? RefImage { get; set; }

    /// <inheritdoc />
    public string? Granularity { get; set; }

    /// <inheritdoc />
    public string? Hint { get; set; }

    /// <inheritdoc />
    public List<double>? Transform { get; set; }

    /// <inheritdoc />
    public string? CompletionMode { get; set; }

    /// <inheritdoc />
    public bool? ForceSymmetry { get; set; }

    /// <inheritdoc />
    public bool? FlattenBottom { get; set; }

    /// <inheritdoc />
    public double? FlattenBottomThreshold { get; set; }

    /// <inheritdoc />
    public int? TextureSize { get; set; }

    /// <inheritdoc />
    public string? TextureFormat { get; set; }

    /// <inheritdoc />
    public bool? PivotToCenterBottom { get; set; }

    /// <inheritdoc />
    public double? ScaleFactor { get; set; }

    /// <inheritdoc />
    public bool? PackUv { get; set; }

    /// <inheritdoc />
    public bool? WithAnimation { get; set; }

    /// <inheritdoc />
    public bool? AnimateInPlace { get; set; }

    /// <inheritdoc />
    public bool? ExportVertexColors { get; set; }

    /// <inheritdoc />
    public string? FbxPreset { get; set; }

    /// <inheritdoc />
    public string? OutFormat { get; set; }

    /// <inheritdoc />
    public string? Spec { get; set; }

    /// <inheritdoc />
    public string? RigType { get; set; }

    /// <inheritdoc />
    public List<string>? Animations { get; set; }

    /// <inheritdoc />
    public string? Animation { get; set; }

    /// <inheritdoc />
    public bool? ExportWithGeometry { get; set; }
}
