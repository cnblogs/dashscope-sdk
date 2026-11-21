namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters of Tripo 3D tasks. Which members are supported depends on the model.
/// </summary>
public interface ITripo3DParameters
{
    /// <summary>
    /// The quality of the texture: standard (default), detailed or extreme (8K).
    /// Used by Tripo-H3.1 / Tripo-P1.0 and Tripo-Texture.
    /// </summary>
    string? TextureQuality { get; set; }

    /// <summary>
    /// The geometry precision: standard (default, up to 1.5M faces) or ultra (up to 2M faces).
    /// Tripo-H3.1 only, ignored by Tripo-P1.0.
    /// </summary>
    string? GeometryQuality { get; set; }

    /// <summary>
    /// Whether to generate a PBR material model, defaults to <c>true</c>. When enabled the
    /// texture is also enabled and pbr_model_url is returned. Used by Tripo-H3.1 / Tripo-P1.0
    /// and Tripo-Texture.
    /// </summary>
    bool? Pbr { get; set; }

    /// <summary>
    /// Whether to generate a texture, defaults to <c>true</c>. Set both texture and
    /// <see cref="Pbr"/> to <c>false</c> to get a base model without texture (base_model_url).
    /// Tripo-H3.1 / Tripo-P1.0 only.
    /// </summary>
    bool? Texture { get; set; }

    /// <summary>
    /// Whether to output quad (instead of triangle) meshes, defaults to <c>false</c>. When
    /// enabled the output format is forced to FBX and the default face limit is 10000.
    /// Used by Tripo-H3.1 (ignored by Tripo-P1.0), Tripo-Retopology and Tripo-ConvertFormat.
    /// </summary>
    bool? Quad { get; set; }

    /// <summary>
    /// Whether to generate a smart low-poly model with clean topology, defaults to <c>false</c>.
    /// When enabled the face limit range is 500~20000 (triangle) / 500~10000 (quad), and it
    /// takes precedence over <see cref="GenerateParts"/>. Tripo-H3.1 only.
    /// </summary>
    bool? SmartLowPoly { get; set; }

    /// <summary>
    /// Whether to generate editable segmented parts, defaults to <c>false</c>. When enabled,
    /// texture and pbr default to <c>false</c> and setting either to <c>true</c> (or passing
    /// <see cref="TextureQuality"/>) returns 400; quad is ignored. Tripo-H3.1 only.
    /// </summary>
    bool? GenerateParts { get; set; }

    /// <summary>
    /// The compression mode: geometry (meshopt compression). Omit to disable compression.
    /// Used by Tripo-H3.1 and Tripo-Texture.
    /// </summary>
    string? Compress { get; set; }

    /// <summary>
    /// The negative prompt describing what should not appear in the 3D model,
    /// max 255 characters. Tripo-H3.1 / Tripo-P1.0 only.
    /// </summary>
    string? NegativePrompt { get; set; }

    /// <summary>
    /// The face limit of the output model. Tripo-H3.1: up to 1500000 triangle faces
    /// (2000000 when geometry quality is ultra) or 150000 quad faces; Tripo-P1.0: 50~20000.
    /// Also used by Tripo-Retopology (see the version ranges in its documentation) and
    /// Tripo-ConvertFormat (recommended at least 100, defaults to unlimited / 10000 with quad).
    /// Not supported by Tripo-MeshCompletion.
    /// </summary>
    int? FaceLimit { get; set; }

    /// <summary>
    /// The random seed of the geometry (or the whole generation for Tripo-ImageToSplat),
    /// range -2147483648~2147483647. The same seed and input produce the same result.
    /// Used by Tripo-H3.1 / Tripo-P1.0 and Tripo-ImageToSplat.
    /// </summary>
    int? ModelSeed { get; set; }

    /// <summary>
    /// The random seed of the image generation stage, range -2147483648~2147483647.
    /// Mainly used by text-to-3d. Tripo-H3.1 / Tripo-P1.0 only.
    /// </summary>
    int? ImageSeed { get; set; }

    /// <summary>
    /// The random seed of the texture generation, range -2147483648~2147483647.
    /// Used by Tripo-H3.1 / Tripo-P1.0 and Tripo-Texture.
    /// </summary>
    int? TextureSeed { get; set; }

    /// <summary>
    /// The texture alignment priority: original_image (default, match the input image colors)
    /// or geometry (match the generated geometry). Used by Tripo-H3.1 / Tripo-P1.0 and
    /// Tripo-Texture.
    /// </summary>
    string? TextureAlignment { get; set; }

    /// <summary>
    /// The orientation of the model: default (auto) or align_image (align to the input image
    /// view), effective only when the texture is enabled. Tripo-H3.1 / Tripo-P1.0 only.
    /// </summary>
    string? Orientation { get; set; }

    /// <summary>
    /// The front axis of the exported model: +x (default), -x, +y or -y. Not recommended when
    /// the model will be post-processed later - adjust the orientation in Tripo-ConvertFormat
    /// (which also supports this member) as the last step instead. Used by Tripo-H3.1 /
    /// Tripo-P1.0 and Tripo-ConvertFormat.
    /// </summary>
    string? ExportOrientation { get; set; }

    /// <summary>
    /// Whether to scale the model to real-world size in meters, defaults to <c>false</c>.
    /// Tripo-H3.1 / Tripo-P1.0 only.
    /// </summary>
    bool? AutoSize { get; set; }

    /// <summary>
    /// Whether to unwrap UVs during generation, defaults to <c>true</c>. Set to <c>false</c> to
    /// speed up generation; the UVs are then handled in the texture stage.
    /// Tripo-H3.1 / Tripo-P1.0 only.
    /// </summary>
    bool? ExportUv { get; set; }

    /// <summary>
    /// Whether to automatically fix the input image, defaults to <c>false</c>. Effective only
    /// for image-to-3d. Tripo-H3.1 / Tripo-P1.0 only.
    /// </summary>
    bool? EnableImageAutofix { get; set; }

    /// <summary>
    /// The version of the algorithm: Tripo-Texture uses v3.0-20250812 (default) or
    /// v2.5-20250123 (match the version that generated the model); Tripo-Segmentation uses
    /// v1.0-20250506 (default, geometry-based) or v2.0-20260430 (beta, semantic + geometry);
    /// Tripo-MeshCompletion only supports v1.0-20250506; Tripo-Retopology uses v2.0 (default,
    /// smart low-poly) or v1.0 (basic decimation); Tripo-AutoRig uses v1.0-20240301 (default,
    /// biped) or v2.5-20260210 (non-biped animals).
    /// </summary>
    string? ModelVersion { get; set; }

    /// <summary>
    /// The guidance input of Tripo-Texture. Required when the model to texture does not come
    /// from a Tripo 3D generation task. Tripo-Texture only.
    /// </summary>
    Tripo3DTexturePrompt? TexturePrompt { get; set; }

    /// <summary>
    /// Whether to bake advanced materials into base textures to improve compatibility,
    /// defaults to <c>true</c>. Used by Tripo-Texture, Tripo-Retopology (not supported by
    /// version v1.0) and Tripo-ConvertFormat.
    /// </summary>
    bool? Bake { get; set; }

    /// <summary>
    /// The names of the parts to process (e.g. tripo_part_0), taken from the node names of the
    /// preceding Tripo-Segmentation result model (read them from the GLB, they are not returned
    /// by the task). Omit to process all parts; unknown names do not fail but are still billed.
    /// Used by Tripo-Texture, Tripo-MeshCompletion, Tripo-Retopology (not supported by version
    /// v1.0) and Tripo-ConvertFormat.
    /// </summary>
    List<string>? PartNames { get; set; }

    /// <summary>
    /// The segmentation granularity: simple, balanced (default) or detailed.
    /// Tripo-Segmentation version v2.0-20260430 only.
    /// </summary>
    string? SegmentationGranularity { get; set; }

    /// <summary>
    /// Whether to split by connectivity, defaults to <c>true</c>.
    /// Tripo-Segmentation version v2.0-20260430 only.
    /// </summary>
    bool? SplitByConnectivity { get; set; }

    /// <summary>
    /// The public url of the color-coded segmentation mask image used to assist semantic
    /// analysis, up to 20MB, PNG recommended. When set, granularity and split by connectivity
    /// are ignored. Tripo-Segmentation version v2.0-20260430 only.
    /// </summary>
    string? RefImage { get; set; }

    /// <summary>
    /// The granularity of Tripo-SmartSegmentation: coarse (2~3 large categories), medium
    /// (default, 4~8 objects or parts) or fine (9~52 small parts).
    /// </summary>
    string? Granularity { get; set; }

    /// <summary>
    /// The prompt telling Tripo-SmartSegmentation which parts to separate, e.g.
    /// "separate the cup body and the lid".
    /// </summary>
    string? Hint { get; set; }

    /// <summary>
    /// The 4x4 column-major transform matrix (16 doubles) applied to the model, required by
    /// Tripo-SmartSegmentation when the input type is model; ignored for image input.
    /// </summary>
    List<double>? Transform { get; set; }

    /// <summary>
    /// The completion mode of Tripo-MeshCompletion: ai_completion (default, AI diffusion
    /// completion generating real geometry) or quick_cap (fast capping of open boundaries
    /// without AI), billed differently.
    /// </summary>
    string? CompletionMode { get; set; }

    /// <summary>
    /// Whether to force the model to be symmetric, defaults to <c>false</c>. Effective only
    /// when quad is enabled. Tripo-ConvertFormat only, billed as an advanced conversion.
    /// </summary>
    bool? ForceSymmetry { get; set; }

    /// <summary>
    /// Whether to flatten the bottom of the model, defaults to <c>false</c>.
    /// Tripo-ConvertFormat only, billed as an advanced conversion.
    /// </summary>
    bool? FlattenBottom { get; set; }

    /// <summary>
    /// The depth threshold of the bottom flattening, defaults to 0.01. Effective only when
    /// flatten bottom is enabled. Tripo-ConvertFormat only; a value other than 0.01 is billed
    /// as an advanced conversion.
    /// </summary>
    double? FlattenBottomThreshold { get; set; }

    /// <summary>
    /// The size of the diffuse texture in pixels, defaults to 4096 (512~8192 recommended, power
    /// of two not required). Tripo-ConvertFormat only; a value other than 4096 is billed as an
    /// advanced conversion.
    /// </summary>
    int? TextureSize { get; set; }

    /// <summary>
    /// The texture format: JPEG (default), PNG, WEBP, BMP, DPX, HDR, OPEN_EXR, TARGA or TIFF.
    /// Tripo-ConvertFormat only; a value other than JPEG is billed as an advanced conversion.
    /// </summary>
    string? TextureFormat { get; set; }

    /// <summary>
    /// Whether to move the pivot point to the bottom center of the model, defaults to
    /// <c>false</c>. Tripo-ConvertFormat only, billed as an advanced conversion.
    /// </summary>
    bool? PivotToCenterBottom { get; set; }

    /// <summary>
    /// The scale factor of the exported model, must be greater than 0, defaults to 1.0.
    /// Tripo-ConvertFormat only; a value other than 1.0 is billed as an advanced conversion.
    /// </summary>
    double? ScaleFactor { get; set; }

    /// <summary>
    /// Whether to pack all UVs into one layout, defaults to <c>false</c>.
    /// Tripo-ConvertFormat only.
    /// </summary>
    bool? PackUv { get; set; }

    /// <summary>
    /// Whether the export contains skeleton and animation data, defaults to <c>true</c>.
    /// Tripo-ConvertFormat only.
    /// </summary>
    bool? WithAnimation { get; set; }

    /// <summary>
    /// Whether to export in-place animation (no root bone translation), defaults to
    /// <c>false</c>. Used by Tripo-ConvertFormat and Tripo-AnimationRetarget.
    /// </summary>
    bool? AnimateInPlace { get; set; }

    /// <summary>
    /// Whether to export vertex colors, defaults to <c>false</c>. Effective only for OBJ and
    /// GLTF formats. Tripo-ConvertFormat only.
    /// </summary>
    bool? ExportVertexColors { get; set; }

    /// <summary>
    /// The compatibility preset of the FBX format: blender (default), 3dsmax, mixamo or
    /// bake_scale (bake the scale transform into the geometry). Tripo-ConvertFormat only.
    /// </summary>
    string? FbxPreset { get; set; }

    /// <summary>
    /// The output format: glb (default) or fbx. Used by Tripo-AutoRig and
    /// Tripo-AnimationRetarget.
    /// </summary>
    string? OutFormat { get; set; }

    /// <summary>
    /// The naming convention of the skeleton: tripo (default) or mixamo. Models rigged with
    /// the mixamo convention cannot be used by Tripo-AnimationRetarget. Tripo-AutoRig only,
    /// effective only when the rig type is biped.
    /// </summary>
    string? Spec { get; set; }

    /// <summary>
    /// The skeleton type: biped (default), quadruped, hexapod, octopod, avian, serpentine or
    /// aquatic. It is recommended to get this value from a Tripo-RigCheck task first.
    /// Tripo-AutoRig only.
    /// </summary>
    string? RigType { get; set; }

    /// <summary>
    /// The list of preset animation names (up to 5, billed per animation), each in the
    /// preset:&lt;name&gt; form, e.g. preset:walk; non-biped rigs use preset:&lt;rig&gt;:&lt;name&gt;,
    /// e.g. preset:quadruped:walk. See the animation list in the Bailian documentation.
    /// Mutually exclusive with <see cref="Animation"/>. Tripo-AnimationRetarget only.
    /// </summary>
    List<string>? Animations { get; set; }

    /// <summary>
    /// The legacy single preset animation name, same format as the entries of
    /// <see cref="Animations"/>, counted and billed as one animation. Prefer
    /// <see cref="Animations"/>. Tripo-AnimationRetarget only.
    /// </summary>
    string? Animation { get; set; }

    /// <summary>
    /// Whether the export contains the geometry, defaults to <c>true</c>.
    /// Tripo-AnimationRetarget only.
    /// </summary>
    bool? ExportWithGeometry { get; set; }
}
