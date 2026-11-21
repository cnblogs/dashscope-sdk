namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of an image-to-image synthesis task, shared by all models on this endpoint:
/// image translation (qwen-mt-image), function-based editing (wanx2.1-imageedit) and
/// instruction-based editing (e.g. wan2.5-i2i-preview).
/// Which members are supported depends on the model family.
/// </summary>
public class Image2ImageSynthesisInput
{
    /// <summary>
    /// The prompt describing the edit to perform. Edit models only.
    /// </summary>
    public string? Prompt { get; set; }

    /// <summary>
    /// The negative prompt describing content that should not appear in the result.
    /// Instruction-based edit models only.
    /// </summary>
    public string? NegativePrompt { get; set; }

    /// <summary>
    /// The edit function, e.g. description_edit, description_edit_with_mask, stylization_all, remove_watermark,
    /// expand, super_resolution, colorization, doodle, control_cartoon_feature or stylization_local.
    /// Function-based edit models only.
    /// </summary>
    public string? Function { get; set; }

    /// <summary>
    /// Urls of the reference images, at most 3. The order defines the image order used by the prompt.
    /// Instruction-based edit models only.
    /// </summary>
    public List<string>? Images { get; set; }

    /// <summary>
    /// The url of the image to edit. Function-based edit models only.
    /// </summary>
    public string? BaseImageUrl { get; set; }

    /// <summary>
    /// The url of the mask image, required by local repaint functions (e.g. description_edit_with_mask).
    /// Function-based edit models only.
    /// </summary>
    public string? MaskImageUrl { get; set; }

    /// <summary>
    /// The url of the sketch image. The sketch aspect ratio should match the output, a white
    /// background is recommended. Sketch-to-image models (wanx-sketch-to-image-lite) only.
    /// </summary>
    public string? SketchImageUrl { get; set; }

    /// <summary>
    /// The url of the source image to translate. Translation models only.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The language of the text in the source image, defaults to <c>"auto"</c>. Translation models only.
    /// </summary>
    public string? SourceLang { get; set; }

    /// <summary>
    /// The target language to translate the text into. Translation models only.
    /// </summary>
    public string? TargetLang { get; set; }

    /// <summary>
    /// The extra options for the translation. Translation models only.
    /// </summary>
    public Image2ImageSynthesisInputExt? Ext { get; set; }
}
