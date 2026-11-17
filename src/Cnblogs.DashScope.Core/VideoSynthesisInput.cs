namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of a video synthesis task (HappyHorse models).
/// </summary>
public class VideoSynthesisInput
{
    /// <summary>
    /// The prompt describing the video to generate or the edit to perform.
    /// </summary>
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// The media inputs: a first frame image (text-to-video's sibling image-to-video),
    /// reference images, or a source video with reference images for editing.
    /// Omit for pure text-to-video.
    /// </summary>
    public List<VideoSynthesisMedia>? Media { get; set; }
}
