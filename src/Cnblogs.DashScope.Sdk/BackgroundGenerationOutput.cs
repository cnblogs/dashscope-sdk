namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The output of background generation task.
/// </summary>
public record BackgroundGenerationOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The generated image urls.
    /// </summary>
    public List<BackgroundGenerationResult>? Results { get; set; }

    /// <summary>
    /// The generated result of texts.
    /// </summary>
    public BackgroundGenerationTextResult? TextResults { get; set; }
}
