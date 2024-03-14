namespace Cnblogs.DashScope.Core;

/// <summary>
/// The parameters of background generation task.
/// </summary>
public class BackgroundGenerationParameters : IBackgroundGenerationParameters
{
    /// <inheritdoc />
    public int? N { get; set; }

    /// <inheritdoc />
    public int? NoiseLevel { get; set; }

    /// <inheritdoc />
    public float? RefPromptWeight { get; set; }
}
