namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for image synthesis task.
/// </summary>
public interface IImageSynthesisParameters : ISeedParameter
{
    /// <summary>
    /// Generated image style, defaults to '&lt;auto&gt;'. Use <see cref="ImageStyles"/> to get all available options.
    /// </summary>
    string? Style { get; }

    /// <summary>
    /// Generated image size, defaults to 1024*1024. Another options are: 1280*720 and 720*1280.
    /// </summary>
    string? Size { get; }

    /// <summary>
    /// Number of images requested. Max number is 4, defaults to 1.
    /// </summary>
    int? N { get; }

    /// <summary>
    /// Let LLM rewrite your positive prompt, Defaults to true.
    /// </summary>
    bool? PromptExtend { get; }

    /// <summary>
    /// Adds AI-Generated watermark on bottom right corner.
    /// </summary>
    bool? Watermark { get; }
}
