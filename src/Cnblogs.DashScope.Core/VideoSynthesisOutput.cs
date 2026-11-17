namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a video synthesis task.
/// </summary>
public record VideoSynthesisOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The prompt before rewriting, present when the model rewrites the prompt.
    /// </summary>
    public string? OrigPrompt { get; set; }

    /// <summary>
    /// The url of the generated video.
    /// </summary>
    public string? VideoUrl { get; set; }
}
