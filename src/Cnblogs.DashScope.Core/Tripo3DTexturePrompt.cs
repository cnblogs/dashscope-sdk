namespace Cnblogs.DashScope.Core;

/// <summary>
/// The guidance input of Tripo-Texture, steering the texture generation with text or
/// reference images. Text, image and images are mutually exclusive; style image can only
/// be used together with text. Required when the model to texture does not come from a
/// Tripo 3D generation task.
/// </summary>
public class Tripo3DTexturePrompt
{
    /// <summary>
    /// The prompt describing the desired texture style, e.g. "worn leather with scratches".
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// The public url of the style reference image, can only be used together with
    /// <see cref="Text"/>. Resolution of at least 256x256 is recommended.
    /// </summary>
    public string? StyleImage { get; set; }

    /// <summary>
    /// The public url of a single reference image.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// The multi-view reference images, keyed by the view name: front, left, back or right.
    /// The front view is required and at least two views must be provided, omit the views
    /// without an image.
    /// </summary>
    public Dictionary<string, string>? Images { get; set; }
}
