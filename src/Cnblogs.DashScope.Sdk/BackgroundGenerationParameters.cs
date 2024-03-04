namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The parameters of background generation task.
/// </summary>
public class BackgroundGenerationParameters
{
    /// <summary>
    /// The number of images to be generated.
    /// </summary>
    public int? N { get; set; }

    /// <summary>
    /// Range at [0, 999], controls the distance from generated image to reference image.
    /// </summary>
    public int? NoiseLevel { get; set; }

    /// <summary>
    /// Range at [0,1]. When RefImageUrl and RefPrompt are both set, controls the percentage of ref prompt weight.
    /// </summary>
    public float? RefPromptWeight { get; set; }
}
