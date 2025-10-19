using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents video content of multimodal input.
/// </summary>
[JsonConverter(typeof(MultimodalMessageVideoContentJsonConverter))]
public class MultimodalMessageVideoContent
{
    /// <summary>
    /// The type of the video input.
    /// </summary>
    public MultimodalMessageVideoContentType Type { get; set; }

    /// <summary>
    /// The urls of the video file(s).
    /// </summary>
    public List<string> Urls { get; set; } = new();

    /// <summary>
    /// Create a video content from a video file url.
    /// </summary>
    /// <param name="url">The url of the video file.</param>
    /// <returns></returns>
    public static MultimodalMessageVideoContent Video(string url)
    {
        return new MultimodalMessageVideoContent
        {
            Type = MultimodalMessageVideoContentType.Video, Urls = new List<string> { url }
        };
    }

    /// <summary>
    /// Create a video input from still frames.
    /// </summary>
    /// <param name="urls">The urls of the frames.</param>
    /// <returns></returns>
    public static MultimodalMessageVideoContent FrameSequence(IEnumerable<string> urls)
    {
        return new MultimodalMessageVideoContent
        {
            Type = MultimodalMessageVideoContentType.FrameSequence, Urls = new List<string>(urls)
        };
    }
}
