using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of an image generation task.
/// wan2.6+ text-to-image models expose the images in <see cref="Choices"/>,
/// wanx style repaint models expose them in <see cref="Results"/>.
/// </summary>
public record ImageGenerationOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The generated images. Returned by wanx style repaint models.
    /// </summary>
    public List<ImageGenerationResult>? Results { get; set; }

    /// <summary>
    /// The generated choices. Returned by wan2.6+ text-to-image models.
    /// </summary>
    public List<MultimodalChoice>? Choices { get; set; }

    /// <summary>
    /// The style index of generated image. Returned by wanx style repaint models.
    /// </summary>
    public int StyleIndex { get; set; }

    /// <summary>
    /// The generation start time.
    /// </summary>
    [JsonConverter(typeof(DashScopeDateTimeConvertor))]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// The error code, will be 0 when succeeded.
    /// </summary>
    public int? ErrorCode { get; set; }

    /// <summary>
    /// The error message, will be 'Success' when succeeded.
    /// </summary>
    public string? ErrorMessage { get; set; }
}
