namespace Cnblogs.DashScope.Core;

/// <summary>
/// The video of a video_url content part.
/// </summary>
/// <param name="Url">The url of the video, or a base64 data uri of a local file.</param>
public record OpenAiCompatibleChatVideoUrl(
    string? Url = null);
