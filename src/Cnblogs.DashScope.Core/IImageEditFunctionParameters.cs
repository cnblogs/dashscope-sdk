namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for function-based image edit tasks, applicable members vary by edit function.
/// </summary>
public interface IImageEditFunctionParameters : ISeedParameter
{
    /// <summary>
    /// Number of images to generate, range 1-4, defaults to 1.
    /// </summary>
    int? N { get; set; }

    /// <summary>
    /// Whether to add an AI-generated watermark on the bottom right corner, defaults to <c>true</c>.
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
