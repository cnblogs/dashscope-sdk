namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of an image generation task, shared by the models on this endpoint:
/// wan2.6+ text-to-image models take multimodal <see cref="Messages"/>,
/// while the legacy wanx style repaint model takes <see cref="ImageUrl"/> and <see cref="StyleIndex"/>.
/// </summary>
public class ImageGenerationInput
{
    /// <summary>
    /// The messages describing the images to generate. wan2.6+ text-to-image models.
    /// </summary>
    public List<MultimodalMessage>? Messages { get; set; }

    /// <summary>
    /// The image url to generate a new image from. wanx style repaint models only.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The style the new image should use, checkout docs for sample image of different indexes: https://help.aliyun.com/zh/dashscope/developer-reference/tongyi-wanxiang-style-repaint
    /// wanx style repaint models only.
    /// </summary>
    public int? StyleIndex { get; set; }
}
