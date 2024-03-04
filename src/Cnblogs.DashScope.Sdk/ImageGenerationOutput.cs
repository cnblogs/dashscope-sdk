using System.Text.Json.Serialization;
using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The output of image generation task.
/// </summary>
public record ImageGenerationOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The generated image url.
    /// </summary>
    public List<ImageGenerationResult>? Results { get; set; }

    /// <summary>
    /// The style index of generated image.
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
