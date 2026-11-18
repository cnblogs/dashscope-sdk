namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of a face detection request (model wan2.2-s2v-detect),
/// used to check whether an image is suitable for wan2.2-s2v digital human generation.
/// </summary>
public class FaceDetectInput
{
    /// <summary>
    /// The url of the image to detect.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
}
