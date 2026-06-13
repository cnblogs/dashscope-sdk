namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of the image-to-image synthesis task.
/// </summary>
public record Image2ImageSynthesisOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The url of the translated image.
    /// </summary>
    public string? ImageUrl { get; set; }
}
