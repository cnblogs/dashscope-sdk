namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Optional parameters for image synthesis task.
/// </summary>
public class ImageSynthesisParameters
{
    /// <summary>
    /// Generated image style, defaults to '&lt;auto&gt;'. Use <see cref="ImageStyles"/> to get all available options.
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// Generated image size, defaults to 1024*1024. Another options are: 1280*720 and 720*1280.
    /// </summary>
    public string? Size { get; set; }

    /// <summary>
    /// Number of images requested. Max number is 4, defaults to 1.
    /// </summary>
    public int? N { get; set; }

    /// <summary>
    /// Seed for randomizer. Once set, generated image will use seed, seed+1, seed+2, seed+3 depends on <see cref="N"/>.
    /// </summary>
    public int? Seed { get; set; }
}
