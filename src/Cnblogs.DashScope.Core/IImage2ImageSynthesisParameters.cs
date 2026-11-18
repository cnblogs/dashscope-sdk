namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for image-to-image synthesis tasks.
/// Which members are supported depends on the model family — the edit members
/// (strength, scales, upscale factor, sketch) apply to wanx2.1-imageedit functions only.
/// </summary>
public interface IImage2ImageSynthesisParameters : ISeedParameter
{
    /// <summary>
    /// Generated image size. Supported by instruction-based edit models.
    /// </summary>
    string? Size { get; set; }

    /// <summary>
    /// Number of images requested. Max number is 4, defaults to 1.
    /// </summary>
    int? N { get; set; }

    /// <summary>
    /// Let LLM rewrite your positive prompt. Supported by instruction-based edit models.
    /// </summary>
    bool? PromptExtend { get; set; }

    /// <summary>
    /// Adds AI-Generated watermark on bottom right corner.
    /// </summary>
    bool? Watermark { get; set; }

    /// <summary>
    /// The strength of the edit, range 0-1, defaults to 0.5.
    /// Available for stylization_all, stylization_local, description_edit and doodle.
    /// </summary>
    float? Strength { get; set; }

    /// <summary>
    /// The scale of the top edge when expanding the image, range 1-2. Available for expand.
    /// </summary>
    float? TopScale { get; set; }

    /// <summary>
    /// The scale of the bottom edge when expanding the image, range 1-2. Available for expand.
    /// </summary>
    float? BottomScale { get; set; }

    /// <summary>
    /// The scale of the left edge when expanding the image, range 1-2. Available for expand.
    /// </summary>
    float? LeftScale { get; set; }

    /// <summary>
    /// The scale of the right edge when expanding the image, range 1-2. Available for expand.
    /// </summary>
    float? RightScale { get; set; }

    /// <summary>
    /// The upscale factor for super resolution, range 1-4. Available for super_resolution.
    /// </summary>
    int? UpscaleFactor { get; set; }

    /// <summary>
    /// Whether the base image is a sketch, used by doodle to keep the sketch style.
    /// </summary>
    bool? IsSketch { get; set; }
}
