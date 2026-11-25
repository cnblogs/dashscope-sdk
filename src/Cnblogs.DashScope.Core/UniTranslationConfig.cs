using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The extra configs of an omni-modal translation (model qwen-mt-uni).
/// </summary>
public class UniTranslationConfig
{
    /// <summary>
    /// Whether to skip the translation of the text on the main subject of the image, such as
    /// people, products or logos, defaults to false. Image input only.
    /// </summary>
    [JsonPropertyName("imageSegment")]
    public bool? ImageSegment { get; set; }
}
