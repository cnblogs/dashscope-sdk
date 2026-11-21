namespace Cnblogs.DashScope.Core;

/// <summary>
/// One result item of a Tripo 3D task. Which members are present depends on the model.
/// All urls are signed temporary links valid for 2 hours.
/// </summary>
/// <param name="ModelUrl">The url of the 3D model: the generated model (Tripo-H3.1 / Tripo-P1.0, GLB or FBX with quad), the Gaussian Splat file (Tripo-ImageToSplat, .splat), the textured / completed / retopologized / rigged / retargeted model (Tripo-Texture / Tripo-MeshCompletion / Tripo-Retopology / Tripo-AutoRig / Tripo-AnimationRetarget), the converted model (Tripo-ConvertFormat) or the model itself (Tripo-Segmentation).</param>
/// <param name="PbrModelUrl">The url of the PBR material model (GLB, or FBX with quad), returned when pbr is enabled. Tripo-H3.1 / Tripo-P1.0 and Tripo-Texture.</param>
/// <param name="BaseModelUrl">The url of the base model without texture (GLB, or FBX with quad), returned when texture and pbr are both disabled. Tripo-H3.1 / Tripo-P1.0 only.</param>
/// <param name="RenderedImageUrl">The url of the rendered preview image of the 3D model. Tripo-H3.1 / Tripo-P1.0, Tripo-Texture, Tripo-Segmentation and Tripo-AutoRig.</param>
/// <param name="GeneratedImageUrl">The url of the intermediate image generated from the prompt. Tripo-H3.1 / Tripo-P1.0 text-to-3d only.</param>
/// <param name="SegModelUrl">The url of the segmented model. Tripo-Segmentation and Tripo-SmartSegmentation.</param>
/// <param name="MaskUrl">The url of the 2D segmentation mask image. Tripo-SmartSegmentation only.</param>
/// <param name="Prompt">The recognized part names, separated by commas. Tripo-SmartSegmentation only.</param>
/// <param name="ModelTaskId">The id of the internal 3D model subtask, usable as the original_model_task_id of Tripo-Texture / Tripo-Retopology / Tripo-AutoRig etc. Tripo-SmartSegmentation only.</param>
/// <param name="SegTaskId">The id of the internal mesh segmentation subtask, usable as the original_model_task_id of Tripo-MeshCompletion and together with part_names in Tripo-Texture / Tripo-Retopology. Tripo-SmartSegmentation only.</param>
/// <param name="Riggable">Whether the model can be rigged. Tripo-RigCheck only.</param>
/// <param name="RigType">The recommended skeleton type (biped, quadruped, hexapod, octopod, avian, serpentine or aquatic), use it as the rig_type of Tripo-AutoRig. Returned regardless of <paramref name="Riggable"/> but is a reference only when riggable is <c>false</c>. Tripo-RigCheck only.</param>
/// <param name="NormalizeTranslation">The translation applied when normalizing the model, returned by Tripo-RigCheck and the post-processing models, not listed in the Bailian documentation yet.</param>
/// <param name="NormalizeScale">The scale applied when normalizing the model, returned by Tripo-RigCheck and the post-processing models, not listed in the Bailian documentation yet.</param>
public record Tripo3DResult(
    string? ModelUrl = null,
    string? PbrModelUrl = null,
    string? BaseModelUrl = null,
    string? RenderedImageUrl = null,
    string? GeneratedImageUrl = null,
    string? SegModelUrl = null,
    string? MaskUrl = null,
    string? Prompt = null,
    string? ModelTaskId = null,
    string? SegTaskId = null,
    bool? Riggable = null,
    string? RigType = null,
    List<double>? NormalizeTranslation = null,
    double? NormalizeScale = null);
