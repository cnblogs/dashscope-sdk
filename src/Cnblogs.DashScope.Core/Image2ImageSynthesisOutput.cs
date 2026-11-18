namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of an image-to-image synthesis task.
/// Translation models expose the translated image in <see cref="ImageUrl"/>,
/// edit models expose the results in <see cref="Results"/>.
/// </summary>
public record Image2ImageSynthesisOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The url of the translated image. Returned by translation models.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The generated images. Returned by edit models.
    /// </summary>
    public List<ImageSynthesisResult>? Results { get; set; }
}
