using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The extra options of the image-to-image synthesis task.
/// </summary>
public class Image2ImageSynthesisInputExt
{
    /// <summary>
    /// The hint of the scene and translation style of the source text.
    /// </summary>
    [JsonPropertyName("domainHint")]
    public string? DomainHint { get; set; }

    /// <summary>
    /// The terms to filter out before translation. Only fully matched terms are filtered.
    /// </summary>
    public List<string>? Sensitives { get; set; }

    /// <summary>
    /// The predefined translations for specific terms.
    /// </summary>
    public List<Image2ImageSynthesisInputExtTerm>? Terminologies { get; set; }

    /// <summary>
    /// The extra configs for the translation task.
    /// </summary>
    public Image2ImageSynthesisConfig? Config { get; set; }
}
