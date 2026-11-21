namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of a face detection request (model facechain-facedetect), used to check
/// whether the images contain a usable face before FaceChain fine-tuning.
/// </summary>
public class FaceDetectionInput
{
    /// <summary>
    /// The urls of the images to detect, each at least 256*256 and at most 4096*4096 pixels,
    /// at most 5 MB, in JPEG/JPG/PNG/WEBP format. Urls containing non-ASCII characters
    /// must be percent-encoded first.
    /// </summary>
    public List<string> Images { get; set; } = new();
}
