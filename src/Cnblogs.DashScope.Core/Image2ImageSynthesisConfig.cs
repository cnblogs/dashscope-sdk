using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The extra configs of the image-to-image synthesis task.
/// </summary>
public class Image2ImageSynthesisConfig
{
    /// <summary>
    /// Whether to skip the translation for the main subject in the image, such as people, products or logos.
    /// </summary>
    [JsonPropertyName("imageSegment")]
    public bool? ImageSegment { get; set; }
}
