namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of an instruction-based image edit task (e.g. model wan2.5-i2i-preview),
/// which edits images according to the prompt while keeping subject consistency.
/// </summary>
public class ImageEditInput
{
    /// <summary>
    /// The prompt describing the edit to perform.
    /// </summary>
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// The negative prompt describing content that should not appear in the result.
    /// </summary>
    public string? NegativePrompt { get; set; }

    /// <summary>
    /// Urls of the reference images, at most 3. The order defines the image order used by the prompt.
    /// </summary>
    public List<string> Images { get; set; } = new();
}
