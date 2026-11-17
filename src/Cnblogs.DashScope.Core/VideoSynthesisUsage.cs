using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The usage of a video synthesis task.
/// </summary>
/// <param name="Duration">The billed duration in seconds.</param>
/// <param name="InputVideoDuration">The duration of the input video in seconds.</param>
/// <param name="OutputVideoDuration">The duration of the output video in seconds.</param>
/// <param name="VideoCount">The count of generated videos, always 1.</param>
/// <param name="SR">The vertical resolution of the output video, e.g. 720 or 1080.</param>
/// <param name="Ratio">The aspect ratio of the output video.</param>
public record VideoSynthesisUsage(
    int? Duration = null,
    int? InputVideoDuration = null,
    int? OutputVideoDuration = null,
    int? VideoCount = null,
    [property: JsonPropertyName("SR")] int? SR = null,
    string? Ratio = null);
