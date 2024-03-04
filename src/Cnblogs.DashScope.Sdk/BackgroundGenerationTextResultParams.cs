namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The generated styles of text in one image of background generation task.
/// </summary>
/// <param name="SampleIdx">The id of image.</param>
/// <param name="Layers">The layers of styled text.</param>
public record BackgroundGenerationTextResultParams(int SampleIdx, List<BackgroundGenerationTextResultLayer> Layers);
