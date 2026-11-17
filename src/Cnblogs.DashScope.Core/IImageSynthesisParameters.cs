namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for image synthesis task.
/// </summary>
public interface IImageSynthesisParameters : ISeedParameter
{
    /// <summary>
    /// Generated image style, defaults to '&lt;auto&gt;'. Use <see cref="ImageStyles"/> to get all available options.
    /// </summary>
    string? Style { get; set; }

    /// <summary>
    /// Generated image size. Defaults to 1024*1024 for wanx models (other options: 1280*720, 720*1280)
    /// and 1280*1280 for wan2.5-t2i-preview, which allows free size as long as total pixels stay
    /// in [1280*1280, 1440*1440] and aspect ratio stays in [1:4, 4:1].
    /// </summary>
    string? Size { get; set; }

    /// <summary>
    /// Number of images requested. Max number is 4, defaults to 4.
    /// </summary>
    int? N { get; set; }

    /// <summary>
    /// Let LLM rewrite your positive prompt, Defaults to true.
    /// </summary>
    bool? PromptExtend { get; set; }

    /// <summary>
    /// Adds AI-Generated watermark on bottom right corner.
    /// </summary>
    bool? Watermark { get; set; }
}
