namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for image synthesis task.
/// </summary>
public interface IImageSynthesisParameters
{
    /// <summary>
    /// Generated image style, defaults to '&lt;auto&gt;'. Use <see cref="ImageStyles"/> to get all available options.
    /// </summary>
    public string? Style { get; }

    /// <summary>
    /// Generated image size, defaults to 1024*1024. Another options are: 1280*720 and 720*1280.
    /// </summary>
    public string? Size { get; }

    /// <summary>
    /// Number of images requested. Max number is 4, defaults to 1.
    /// </summary>
    public int? N { get; }

    /// <summary>
    /// Seed for randomizer, max at 4294967290. Once set, generated image will use seed, seed+1, seed+2, seed+3 depends on <see cref="N"/>.
    /// </summary>
    public uint? Seed { get; }
}
