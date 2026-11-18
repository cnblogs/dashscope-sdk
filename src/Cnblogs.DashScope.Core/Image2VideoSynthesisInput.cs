namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of an image2video synthesis task, shared by the models on this endpoint.
/// wan2.2-kf2v / wanx2.1-kf2v-plus (image-to-video by first and last frames) use
/// <see cref="FirstFrameUrl"/> (plus optional <see cref="LastFrameUrl"/> or <see cref="Template"/>),
/// wan2.2-animate-move / wan2.2-animate-mix (image-to-motion and subject replacement)
/// use <see cref="ImageUrl"/> and <see cref="VideoUrl"/>,
/// wan2.2-s2v (digital human) uses <see cref="ImageUrl"/> and <see cref="AudioUrl"/>,
/// while videoretalk (lip sync) uses <see cref="VideoUrl"/> and <see cref="AudioUrl"/>
/// (plus optional <see cref="RefImageUrl"/>).
/// </summary>
public class Image2VideoSynthesisInput
{
    /// <summary>
    /// The prompt describing the transition between the first and the last frame.
    /// Ignored when <see cref="Template"/> is set. kf2v models only.
    /// </summary>
    public string? Prompt { get; set; }

    /// <summary>
    /// The negative prompt describing what should not appear in the video. kf2v models only.
    /// </summary>
    public string? NegativePrompt { get; set; }

    /// <summary>
    /// The url of the first frame image. Required by kf2v models,
    /// the aspect ratio of the output video follows this image.
    /// </summary>
    public string? FirstFrameUrl { get; set; }

    /// <summary>
    /// The url of the last frame image, optional. kf2v models only.
    /// </summary>
    public string? LastFrameUrl { get; set; }

    /// <summary>
    /// The name of the video effect template. When set, only the first frame image is needed.
    /// See the effect list of the model in the Bailian documentation. kf2v models only.
    /// </summary>
    public string? Template { get; set; }

    /// <summary>
    /// The url of the person image to animate or to replace the subject of the video with.
    /// Required by animate models and wan2.2-s2v (digital human).
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The url of the audio the digital human speaks or performs with,
    /// or the human voice the generated video lip-syncs to.
    /// Required by wan2.2-s2v (digital human) and videoretalk.
    /// </summary>
    public string? AudioUrl { get; set; }

    /// <summary>
    /// The url of the reference video providing the motion. Required by animate models,
    /// or the person video whose lip movement gets replaced. Required by videoretalk.
    /// </summary>
    public string? VideoUrl { get; set; }

    /// <summary>
    /// The url of the face reference image telling which face to lip-sync when the
    /// video contains multiple faces. Optional, videoretalk only. When omitted, the
    /// largest face in the first frame with a face is picked.
    /// </summary>
    public string? RefImageUrl { get; set; }

    /// <summary>
    /// Whether to add a "千问AI生成" watermark on the bottom right corner, defaults to <c>false</c>.
    /// Note that animate models take this on the input while kf2v models take it in the parameters.
    /// </summary>
    public bool? Watermark { get; set; }
}
