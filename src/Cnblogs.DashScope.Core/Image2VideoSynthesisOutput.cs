namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of an image2video synthesis task.
/// wan2.2-kf2v / wanx2.1-kf2v-plus tasks expose the video in <see cref="VideoUrl"/>,
/// while wan2.2-animate-move / wan2.2-animate-mix tasks expose it in <see cref="Results"/>.
/// </summary>
public record Image2VideoSynthesisOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The prompt before rewriting, present when the model rewrites the prompt. Returned by kf2v models.
    /// </summary>
    public string? OrigPrompt { get; set; }

    /// <summary>
    /// The url of the generated video. Returned by kf2v models.
    /// </summary>
    public string? VideoUrl { get; set; }

    /// <summary>
    /// The generation result. Returned by animate models.
    /// </summary>
    public Image2VideoSynthesisResult? Results { get; set; }
}
