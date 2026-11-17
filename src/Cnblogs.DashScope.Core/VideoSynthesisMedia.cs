namespace Cnblogs.DashScope.Core;

/// <summary>
/// One media item of a video synthesis task.
/// </summary>
/// <param name="Type">The media type: "first_frame" (image-to-video), "reference_image" (reference-to-video and video editing) or "video" (video editing).</param>
/// <param name="Url">The url of the media.</param>
public record VideoSynthesisMedia(string Type, string Url);
