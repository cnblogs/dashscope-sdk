namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for image2video synthesis tasks.
/// Which members are supported depends on the model family.
/// </summary>
public interface IImage2VideoSynthesisParameters : ISeedParameter
{
    /// <summary>
    /// The service mode of the animate models: wan-std (standard) or wan-pro (professional).
    /// Required by wan2.2-animate-move / wan2.2-animate-mix, not supported by kf2v models.
    /// </summary>
    string? Mode { get; set; }

    /// <summary>
    /// Whether to check the input image, defaults to <c>true</c>. Animate models only.
    /// </summary>
    bool? CheckImage { get; set; }

    /// <summary>
    /// The resolution tier of the output video: 480P, 720P or 1080P.
    /// kf2v models only, wan2.2-kf2v-flash defaults to 720P, wanx2.1-kf2v-plus only supports 720P.
    /// </summary>
    string? Resolution { get; set; }

    /// <summary>
    /// The duration of the output video in seconds. kf2v models only, currently fixed to 5.
    /// </summary>
    int? Duration { get; set; }

    /// <summary>
    /// Whether to enable prompt rewriting, defaults to <c>true</c>. kf2v models only.
    /// </summary>
    bool? PromptExtend { get; set; }

    /// <summary>
    /// Whether to add an AI-generated watermark on the bottom right corner, defaults to <c>false</c>.
    /// kf2v models only, animate models take the watermark on the input instead.
    /// </summary>
    bool? Watermark { get; set; }
}
