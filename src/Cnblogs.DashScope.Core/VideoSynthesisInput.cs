namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of a video synthesis task, shared by all models on this endpoint:
/// HappyHorse, wan2.7, wan3.0, legacy wan2.1-2.6 and wanx2.1-vace.
/// Which members are supported depends on the model family.
/// </summary>
public class VideoSynthesisInput
{
    /// <summary>
    /// The prompt describing the video to generate or the edit to perform.
    /// wan3.0 also uses it to reference media items ("图1"/"视频1", i.e. image 1 / video 1).
    /// </summary>
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// The negative prompt describing what should not appear in the video. Supported by wan models.
    /// </summary>
    public string? NegativePrompt { get; set; }

    /// <summary>
    /// The url of the audio used to generate the video, supported by wan2.7 text-to-video
    /// and wan2.5/wan2.6 text-to-video / image-to-video.
    /// </summary>
    public string? AudioUrl { get; set; }

    /// <summary>
    /// The media inputs, the supported types depend on the model:
    /// first_frame (image-to-video, reference-to-video and video editing), last_frame (wan2.7/wan3.0
    /// image-to-video), reference_image, reference_video, reference_audio (wan3.0), driving_audio and
    /// first_clip (wan2.7 image-to-video), video (video editing), file or link (wan3.0).
    /// Omit for pure text-to-video.
    /// </summary>
    public List<VideoSynthesisMedia>? Media { get; set; }

    /// <summary>
    /// The url of the first frame image, required by legacy wan2.1-2.6 image-to-video.
    /// </summary>
    public string? ImgUrl { get; set; }

    /// <summary>
    /// The name of the video effect template. When set, the prompt is ignored and (except for
    /// wanx2.1-vace) only the first frame image is needed. Legacy wan2.1-2.6 models.
    /// See the effect list of the model in the Bailian documentation.
    /// </summary>
    public string? Template { get; set; }

    /// <summary>
    /// The urls of the reference images (up to 5) and videos (up to 3, at most 5 in total),
    /// required by wan2.6 reference-to-video. Each item contains a single subject.
    /// </summary>
    public List<string>? ReferenceUrls { get; set; }

    /// <summary>
    /// The function to perform, required by wanx2.1-vace-plus: image_reference, video_repainting,
    /// video_edit, video_extension or video_outpainting.
    /// </summary>
    public string? Function { get; set; }

    /// <summary>
    /// The urls of the reference images (1-3), required by wanx2.1-vace-plus image_reference.
    /// </summary>
    public List<string>? RefImagesUrl { get; set; }

    /// <summary>
    /// The url of the video to repaint, edit or outpaint. wanx2.1-vace-plus only.
    /// </summary>
    public string? VideoUrl { get; set; }

    /// <summary>
    /// The url of the first frame image, used by wanx2.1-vace video_extension.
    /// </summary>
    public string? FirstFrameUrl { get; set; }

    /// <summary>
    /// The url of the last frame image, used by wanx2.1-vace video_extension.
    /// </summary>
    public string? LastFrameUrl { get; set; }

    /// <summary>
    /// The url of the first video clip, used by wanx2.1-vace video_extension.
    /// </summary>
    public string? FirstClipUrl { get; set; }

    /// <summary>
    /// The url of the last video clip, used by wanx2.1-vace video_extension.
    /// </summary>
    public string? LastClipUrl { get; set; }

    /// <summary>
    /// The url of the mask image marking the edit area of the video,
    /// used by wanx2.1-vace local editing.
    /// </summary>
    public string? MaskImageUrl { get; set; }

    /// <summary>
    /// The id of the frame in which the mask target appears, effective when
    /// <see cref="MaskImageUrl"/> is set. wanx2.1-vace only.
    /// </summary>
    public int? MaskFrameId { get; set; }

    /// <summary>
    /// The url of the mask video marking the edit area of the video,
    /// an alternative to <see cref="MaskImageUrl"/>. wanx2.1-vace only.
    /// </summary>
    public string? MaskVideoUrl { get; set; }
}
