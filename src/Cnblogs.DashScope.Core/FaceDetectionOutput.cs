namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a face detection request (model facechain-facedetect).
/// </summary>
public record FaceDetectionOutput
{
    /// <summary>
    /// The detection result of each input image, in the same order as the input.
    /// </summary>
    public List<bool>? IsFace { get; set; }

    /// <summary>
    /// The reason an image did not pass the detection, empty when it passed.
    /// </summary>
    public List<string>? FailedReason { get; set; }
}
