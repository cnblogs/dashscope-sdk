using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for video synthesis tasks.
/// Which members are supported depends on the model family.
/// </summary>
public interface IVideoSynthesisParameters : ISeedParameter
{
    /// <summary>
    /// The resolution tier of the output video: 480P, 720P or 1080P, PixVerse models also support
    /// 360P and 540P (pixverse-motioncontrol stops at 720P).
    /// Supported values and the default value depend on the model, most models default to 1080P.
    /// Legacy wan2.1-2.6 and PixVerse text-to-video / reference-to-video models (except
    /// pixverse-v6-r2v-omni) use <see cref="Size"/> instead.
    /// </summary>
    string? Resolution { get; set; }

    /// <summary>
    /// The aspect ratio of the output video: 16:9, 9:16, 1:1, 4:3 or 3:4. wan3.0 additionally supports
    /// adaptive (its default) which follows the input media. Not supported by wan2.7 image-to-video
    /// (follows the input media) and legacy wan2.1-2.6 models.
    /// </summary>
    string? Ratio { get; set; }

    /// <summary>
    /// The aspect ratio of the output video: auto (default, follows the first input image or video),
    /// 16:9, 4:3, 1:1, 3:4, 9:16, 3:2, 2:3 or 21:9. Used together with <see cref="Resolution"/>
    /// instead of <see cref="Size"/>. pixverse-v6-r2v-omni only.
    /// </summary>
    string? AspectRatio { get; set; }

    /// <summary>
    /// The duration of the output video in seconds. The supported range depends on the model,
    /// most models default to 5. wan3.0 accepts -1 (smart duration), wan2.7-videoedit accepts 0
    /// (its default, keep the duration of the input video). pixverse-v6-r2v-omni requires 0 when
    /// a reference video is provided (the output follows the longest input video).
    /// </summary>
    int? Duration { get; set; }

    /// <summary>
    /// The resolution of the output video in "width*height" form, e.g. 1280*720.
    /// Used by legacy wan2.1-2.6 text-to-video / reference-to-video models, wanx2.1-vace-plus
    /// and PixVerse text-to-video / reference-to-video models (except pixverse-v6-r2v-omni).
    /// </summary>
    string? Size { get; set; }

    /// <summary>
    /// Whether the output video contains audio. When enabled PixVerse models generate matching
    /// background music or sound effects (billed differently, defaults to <c>false</c>).
    /// wan3.0 and wan2.6-*-flash models default to <c>true</c>. Takes precedence over the input audio url.
    /// </summary>
    bool? Audio { get; set; }

    /// <summary>
    /// How to handle the sound of the edited video: auto (default, decided by the prompt) or
    /// origin (keep the original audio). Supported by wan2.7-videoedit and HappyHorse video editing.
    /// </summary>
    string? AudioSetting { get; set; }

    /// <summary>
    /// Whether to enable prompt rewriting, defaults to <c>true</c>.
    /// </summary>
    bool? PromptExtend { get; set; }

    /// <summary>
    /// The shot type of the output video: single (default) or multi. Supported by wan2.6 models
    /// (only effective when prompt rewriting is enabled) and pixverse-v6-t2v / pixverse-v6-it2v.
    /// </summary>
    string? ShotType { get; set; }

    /// <summary>
    /// Whether to add an AI-generated watermark on the bottom right corner.
    /// HappyHorse models default to <c>true</c>, wan and PixVerse models default to <c>false</c>.
    /// </summary>
    bool? Watermark { get; set; }

    /// <summary>
    /// The video feature extraction method used to control the generation: posebodyface, posebody,
    /// depth or scribble. wanx2.1-vace-plus only, defaults to posebodyface.
    /// </summary>
    string? ControlCondition { get; set; }

    /// <summary>
    /// The strength of the control imposed by <see cref="ControlCondition"/>, range 0-1.
    /// wanx2.1-vace-plus only. Note that the service currently rejects fractional values
    /// (e.g. 0.5) with an InvalidParameter error, use integer values instead.
    /// </summary>
    double? Strength { get; set; }

    /// <summary>
    /// Whether each reference image controls the subject (obj) or the background (bg),
    /// e.g. ["obj", "bg"], aligned with the reference images. wanx2.1-vace image_reference only.
    /// </summary>
    List<string>? ObjOrBg { get; set; }

    /// <summary>
    /// How the edit area behaves: tracking (default) or fixed.
    /// Effective when a mask image is provided. wanx2.1-vace local editing only.
    /// </summary>
    string? MaskType { get; set; }

    /// <summary>
    /// The expansion ratio of the mask, defaults to 0.05. wanx2.1-vace local editing only.
    /// </summary>
    double? ExpandRatio { get; set; }

    /// <summary>
    /// The shape of the expanded mask: hull, bbox or original. wanx2.1-vace local editing only.
    /// </summary>
    string? ExpandMode { get; set; }

    /// <summary>
    /// The upscale factor of the top edge for outpainting, range 1-2. wanx2.1-vace outpainting only.
    /// </summary>
    double? TopScale { get; set; }

    /// <summary>
    /// The upscale factor of the bottom edge for outpainting, range 1-2. wanx2.1-vace outpainting only.
    /// </summary>
    double? BottomScale { get; set; }

    /// <summary>
    /// The upscale factor of the left edge for outpainting, range 1-2. wanx2.1-vace outpainting only.
    /// </summary>
    double? LeftScale { get; set; }

    /// <summary>
    /// The upscale factor of the right edge for outpainting, range 1-2. wanx2.1-vace outpainting only.
    /// </summary>
    double? RightScale { get; set; }

    /// <summary>
    /// The preset art style: 0 Japanese comic (default), 1 American comic, 2 fresh comic,
    /// 3 3D cartoon, 4 Chinese cartoon (best with costume-drama input), 5 paper art,
    /// 6 simple illustration or 7 Chinese ink wash. video-style-transform only.
    /// </summary>
    int? Style { get; set; }

    /// <summary>
    /// The frame rate of the output video, range 15-25, defaults to 15. video-style-transform only.
    /// </summary>
    int? VideoFps { get; set; }

    /// <summary>
    /// Whether to optimize the facial expressions for better lip-sync and expression fidelity,
    /// defaults to <c>true</c>. Turning it off may perform better when faces are small.
    /// video-style-transform only.
    /// </summary>
    bool? AnimateEmotion { get; set; }

    /// <summary>
    /// The short edge of the output video in pixels: 720 (default) or 540.
    /// Affects the billing, 720P costs more. video-style-transform only.
    /// </summary>
    int? MinLen { get; set; }

    /// <summary>
    /// Whether to apply free super-resolution to the output video, defaults to <c>false</c>.
    /// When <see cref="MinLen"/> is 540 this upscales the output to 1080P while still billing
    /// at the 540P rate, at the cost of longer processing. video-style-transform only.
    /// </summary>
    [JsonPropertyName("use_SR")]
    // ReSharper disable once InconsistentNaming
    bool? UseSR { get; set; }
}
