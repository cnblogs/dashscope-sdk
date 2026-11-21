namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of a Tripo 3D task, shared by all Tripo models on the 3d-generation endpoint:
/// Tripo-H3.1 / Tripo-P1.0 (text-to-3d, image-to-3d and multi-image-to-3d), Tripo-ImageToSplat,
/// Tripo-Texture, Tripo-Segmentation, Tripo-SmartSegmentation, Tripo-MeshCompletion,
/// Tripo-Retopology, Tripo-ConvertFormat, Tripo-RigCheck, Tripo-AutoRig and
/// Tripo-AnimationRetarget. Which members are supported depends on the model.
/// </summary>
public class Tripo3DInput
{
    /// <summary>
    /// The prompt describing the 3D model to generate, max 1024 characters.
    /// Required by text-to-3d (Tripo-H3.1 / Tripo-P1.0), mutually exclusive with
    /// <see cref="Image"/> and <see cref="Images"/>.
    /// </summary>
    public string? Prompt { get; set; }

    /// <summary>
    /// The public url of the single input image (JPEG or PNG, up to 20MB).
    /// Required by image-to-3d (Tripo-H3.1 / Tripo-P1.0) and Tripo-ImageToSplat
    /// (also supports WebP); or the image to segment when
    /// <see cref="SegType"/> is image (Tripo-SmartSegmentation, PNG / JPEG / WebP).
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// The four view images (front, left, back, right) of a multi-image-to-3d task,
    /// required by Tripo-H3.1 / Tripo-P1.0 multi-image mode. The list length is fixed to 4;
    /// pass an entry with all members null for a view without image (an empty object on the wire),
    /// the front view (the first entry) must have an image and 2~4 images are effective.
    /// </summary>
    public List<Tripo3DImage>? Images { get; set; }

    /// <summary>
    /// The id of a preceding task whose output model is used as the input of this task.
    /// The preceding task must have succeeded and belong to the same account.
    /// Used by Tripo-Texture, Tripo-Segmentation, Tripo-Retopology, Tripo-RigCheck and
    /// Tripo-AutoRig (alternative to <see cref="ModelUrl"/>); required by Tripo-MeshCompletion
    /// (a Tripo-Segmentation task or the seg task id returned by Tripo-SmartSegmentation),
    /// Tripo-ConvertFormat and Tripo-AnimationRetarget (a Tripo-AutoRig task).
    /// </summary>
    public string? OriginalModelTaskId { get; set; }

    /// <summary>
    /// The public url of the 3D model file (GLB, GLTF, FBX, OBJ or STL, up to 150MB;
    /// Tripo-RigCheck and Tripo-AutoRig do not support GLTF; Tripo-SmartSegmentation only
    /// supports GLB). Alternative to <see cref="OriginalModelTaskId"/>.
    /// </summary>
    public string? ModelUrl { get; set; }

    /// <summary>
    /// The input type of Tripo-SmartSegmentation: image (requires <see cref="Image"/>) or
    /// model (requires <see cref="ModelUrl"/> and Parameters.Transform).
    /// </summary>
    public string? SegType { get; set; }

    /// <summary>
    /// The target export format of Tripo-ConvertFormat: GLTF, USDZ, FBX, OBJ, STL or 3MF
    /// (lowercase glb is accepted as an alias of GLTF). OBJ does not support animations and
    /// STL contains geometry only.
    /// </summary>
    public string? Format { get; set; }
}
