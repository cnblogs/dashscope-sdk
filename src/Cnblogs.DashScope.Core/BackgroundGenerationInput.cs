namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of background generation task.
/// </summary>
public class BackgroundGenerationInput
{
    /// <summary>
    /// The image url to generation background on.
    /// </summary>
    public string BaseImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// The reference image url for.
    /// </summary>
    public string? RefImageUrl { get; set; }

    /// <summary>
    /// The prompt the background would generation from.
    /// </summary>
    public string? RefPrompt { get; set; }

    /// <summary>
    /// The negative prompt.
    /// </summary>
    public string? NegRefPrompt { get; set; }

    /// <summary>
    /// The title to be put in generated image, max length is 8.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Valid when <see cref="Title"/> is set, max length is 10.
    /// </summary>
    public string? SubTitle { get; set; }
}
