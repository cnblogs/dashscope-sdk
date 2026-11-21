namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for image-to-image synthesis tasks.
/// Which members are supported depends on the model family — the edit members
/// (strength, scales, upscale factor, is-sketch) apply to wanx2.1-imageedit functions only,
/// the sketch members apply to wanx-sketch-to-image-lite.
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

    /// <summary>
    /// The style of the generated image, e.g. &lt;auto&gt; (default), &lt;3d cartoon&gt;, &lt;anime&gt;,
    /// &lt;oil painting&gt;, &lt;watercolor&gt;, &lt;sketch&gt;, &lt;chinese painting&gt; or &lt;flat illustration&gt;.
    /// Supported by wanx-sketch-to-image-lite and wanx-x-painting.
    /// </summary>
    string? Style { get; set; }

    /// <summary>
    /// The RGB colors that mark the repaint region in the mask image, e.g. [[0, 0, 0], [134, 134, 134]].
    /// When empty the mask is binarized (white marks the region). Used by mask-based region repaint models.
    /// </summary>
    List<List<int>>? MaskColor { get; set; }

    /// <summary>
    /// How strongly the sketch constrains the output, range 0-10, defaults to 10
    /// (higher means closer to the input sketch). wanx-sketch-to-image-lite only.
    /// </summary>
    int? SketchWeight { get; set; }

    /// <summary>
    /// Whether to extract sketch edges from an RGB input image. When <c>true</c>, <see cref="SketchColor"/>
    /// is ignored. wanx-sketch-to-image-lite only.
    /// </summary>
    bool? SketchExtraction { get; set; }

    /// <summary>
    /// The RGB colors treated as pen strokes in the sketch image, e.g. [[134, 134, 134], [0, 0, 0]],
    /// all other colors are treated as background. Effective when <see cref="SketchExtraction"/> is
    /// not enabled. wanx-sketch-to-image-lite only.
    /// </summary>
    List<List<int>>? SketchColor { get; set; }
}
