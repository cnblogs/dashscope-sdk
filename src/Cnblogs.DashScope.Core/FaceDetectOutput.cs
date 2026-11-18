namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a face detection request (model wan2.2-s2v-detect).
/// </summary>
public record FaceDetectOutput
{
    /// <summary>
    /// Whether the image passed the detection, i.e. it is suitable for digital human generation.
    /// </summary>
    public bool? CheckPass { get; set; }

    /// <summary>
    /// Whether a human portrait was detected in the image.
    /// </summary>
    public bool? Humanoid { get; set; }
}
