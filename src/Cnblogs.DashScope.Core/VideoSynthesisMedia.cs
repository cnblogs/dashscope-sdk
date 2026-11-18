namespace Cnblogs.DashScope.Core;

/// <summary>
/// One media item of a video synthesis task.
/// </summary>
/// <param name="Type">The media type: first_frame, last_frame, reference_image, reference_video, reference_audio (wan3.0), driving_audio or first_clip (wan2.7 image-to-video), video (video editing), file or link (wan3.0).</param>
/// <param name="Url">The url of the media, a Base64 data URI or an asset id.</param>
/// <param name="ReferenceVoice">The url of the audio used as the voice of the subject in this media item, overriding the audio of a reference video. Supported by wan2.7 reference-to-video.</param>
public record VideoSynthesisMedia(string Type, string Url, string? ReferenceVoice = null);
