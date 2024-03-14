namespace Cnblogs.DashScope.Core;

/// <summary>
/// The results of background generation task.
/// </summary>
/// <param name="Urls">The urls of images that put texts on.</param>
/// <param name="Params">The generated texts for image.</param>
public record BackgroundGenerationTextResult(
    List<BackgroundGenerationTextResultUrl> Urls,
    List<BackgroundGenerationTextResultParams>? Params);
