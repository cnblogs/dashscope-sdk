using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The usage of a video synthesis task.
/// Which members are present depends on the model family.
/// </summary>
/// <param name="Duration">The billed duration in seconds (input plus output video; for video-style-transform it is the duration of the output video).</param>
/// <param name="InputVideoDuration">The duration of the input video in seconds, 0 when there is no input video.</param>
/// <param name="OutputVideoDuration">The duration of the output video in seconds.</param>
/// <param name="VideoCount">The count of generated videos, always 1.</param>
/// <param name="SR">The resolution tier of the output video, e.g. 720, 1080 or 4K (pixverse-upscale; wan models send a number, PixVerse models send a string, video-style-transform reports the billed short edge, i.e. the requested min_len).</param>
/// <param name="Ratio">The aspect ratio of the output video.</param>
/// <param name="Fps">The frame rate of the output video, wan3.0 defaults to 30 (videoretalk follows the input video).</param>
/// <param name="Size">The resolution of the output video in "width*height" form (legacy wan2.1-2.6 models, videoretalk follows the input video).</param>
/// <param name="VideoDuration">The duration of the generated video in seconds (legacy wan2.1-2.6, wanx2.1-vace-plus, wan2.2-kf2v, animate and videoretalk models).</param>
/// <param name="VideoRatio">The service mode or aspect ratio of the generation (legacy wan2.1-2.6, wanx2.1-vace-plus, wan2.2-kf2v, animate and videoretalk models, the latter always reports "standard").</param>
/// <param name="Audio">Whether the generated video contains audio (wan2.6-*-flash and PixVerse models), billed differently from silent videos.</param>
/// <param name="ShotType">The shot type of the generated video. PixVerse models only.</param>
/// <param name="ReferenceType">The types of the reference media, e.g. "image,video". PixVerse reference-to-video and Vidu reference-to-video models.</param>
/// <param name="ImageCount">The count of billed reference images beyond the free quota (5). MiniMax-H3 only.</param>
/// <param name="InputSeconds">The total duration of the input reference videos in seconds. MiniMax-H3 only.</param>
/// <param name="OutputSeconds">The duration of the output video in seconds. MiniMax-H3 only.</param>
/// <param name="TotalSeconds">The total billed duration in seconds (input plus output). MiniMax-H3 only.</param>
public record VideoSynthesisUsage(
    double? Duration = null,
    double? InputVideoDuration = null,
    double? OutputVideoDuration = null,
    int? VideoCount = null,
    [property: JsonPropertyName("SR")]
    [property: JsonConverter(typeof(DashScopeStringConvertor))]
    string? SR = null,
    string? Ratio = null,
    int? Fps = null,
    string? Size = null,
    double? VideoDuration = null,
    string? VideoRatio = null,
    bool? Audio = null,
    string? ShotType = null,
    string? ReferenceType = null,
    int? ImageCount = null,
    int? InputSeconds = null,
    int? OutputSeconds = null,
    int? TotalSeconds = null);
