namespace Cnblogs.DashScope.Core;

/// <summary>
/// The inputs of image synthesis task
/// </summary>
public class ImageSynthesisInput
{
    /// <summary>
    /// The prompt to generate image from. This will be chopped at max length of 500 characters.
    /// </summary>
    public required string Prompt { get; set; }

    /// <summary>
    /// The negative prompt to generate image from. This will be chopped at max length of 500 characters.
    /// </summary>
    public string? NegativePrompt { get; set; }
}
