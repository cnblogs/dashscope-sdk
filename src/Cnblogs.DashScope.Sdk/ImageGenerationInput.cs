namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The input of image generation.
/// </summary>
public class ImageGenerationInput
{
    /// <summary>
    /// The image url to generation new image from.
    /// </summary>
    public required string ImageUrl { get; set; }

    /// <summary>
    /// The style the new image should use, checkout docs for sample image of different indexes: https://help.aliyun.com/zh/dashscope/developer-reference/tongyi-wanxiang-style-repaint
    /// </summary>
    public required int StyleIndex { get; set; }
}
