namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of the image-to-image synthesis task.
/// </summary>
public class Image2ImageSynthesisInput
{
    /// <summary>
    /// The url of the source image to translate.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// The language of the text in the source image. Use <c>"auto"</c> to detect it automatically.
    /// </summary>
    public string SourceLang { get; set; } = "auto";

    /// <summary>
    /// The target language to translate the text into.
    /// </summary>
    public string TargetLang { get; set; } = "Chinese";

    /// <summary>
    /// The extra options for the translation task.
    /// </summary>
    public Image2ImageSynthesisInputExt? Ext { get; set; }
}
