namespace Cnblogs.DashScope.Core;

/// <summary>
/// One view image of a multi-image Tripo 3D generation task.
/// </summary>
/// <param name="Type">The format of the image, jpeg or png.</param>
/// <param name="FileToken">The public url of the image. Leave both members null for an empty slot (a view without image).</param>
public record Tripo3DImage(string? Type = null, string? FileToken = null);
