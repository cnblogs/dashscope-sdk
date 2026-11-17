namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of a function-based image edit task (model wanx2.1-imageedit).
/// </summary>
public class ImageEditFunctionInput
{
    /// <summary>
    /// The prompt describing the edit to perform.
    /// </summary>
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// The edit function, e.g. description_edit, description_edit_with_mask, stylization_all, remove_watermark,
    /// expand, super_resolution, colorization, doodle, control_cartoon_feature or stylization_local.
    /// </summary>
    public string Function { get; set; } = string.Empty;

    /// <summary>
    /// The url of the image to edit.
    /// </summary>
    public string BaseImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// The url of the mask image, required by local repaint functions (e.g. description_edit_with_mask).
    /// </summary>
    public string? MaskImageUrl { get; set; }
}
