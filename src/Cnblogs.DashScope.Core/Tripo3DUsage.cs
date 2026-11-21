using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The usage of a Tripo 3D task. Which members are present depends on the model;
/// each credit member is returned only when it contributes a non-zero surcharge.
/// </summary>
/// <param name="ThreeDTaskType">The type of the task: text-to-3d, image-to-3d or multi-image-to-3d (Tripo-H3.1 / Tripo-P1.0), image-to-splat (Tripo-ImageToSplat), texture (Tripo-Texture), mesh-segmentation (Tripo-Segmentation), smart-segmentation (Tripo-SmartSegmentation), mesh-completion (Tripo-MeshCompletion), retopology (Tripo-Retopology), convert-3d-format (Tripo-ConvertFormat), rig-check (Tripo-RigCheck), auto-rig (Tripo-AutoRig) or animation-retarget (Tripo-AnimationRetarget).</param>
/// <param name="CreditsConsumed">The total credits consumed by the task.</param>
/// <param name="TextureQuality">The credits of the texture, charged by the texture quality. Tripo-H3.1 / Tripo-P1.0 and Tripo-Texture.</param>
/// <param name="GeometryQuality">The credits of the ultra geometry quality. Tripo-H3.1 only.</param>
/// <param name="Quad">The credits of the quad mesh output. Tripo-H3.1 only.</param>
/// <param name="SmartLowPoly">The credits of the smart low-poly model. Tripo-H3.1 only.</param>
/// <param name="GenerateParts">The credits of the part generation. Tripo-H3.1 only.</param>
/// <param name="SegType">The credits of the segmentation, charged by the input type. Tripo-SmartSegmentation only.</param>
/// <param name="CompletionMode">The credits of the completion, charged by the completion mode. Tripo-MeshCompletion only.</param>
/// <param name="RetopologyVersion">The credits of the retopology, charged by the algorithm version. Tripo-Retopology only.</param>
/// <param name="ConvertLevel">The credits of the conversion, charged by the complexity (basic or advanced). Tripo-ConvertFormat only.</param>
public record Tripo3DUsage(
    [property: JsonPropertyName("3d_task_type")]
    string? ThreeDTaskType = null,
    int? CreditsConsumed = null,
    int? TextureQuality = null,
    int? GeometryQuality = null,
    int? Quad = null,
    int? SmartLowPoly = null,
    int? GenerateParts = null,
    int? SegType = null,
    int? CompletionMode = null,
    int? RetopologyVersion = null,
    int? ConvertLevel = null);
