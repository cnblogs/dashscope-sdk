namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents one choice of model made.
/// </summary>
public class TextGenerationChoice
{
    /// <summary>
    /// The reason for generation stop. null - still generating, "stop" - stop token or string hit, "length" - generation length is long enough.
    /// </summary>
    public string? FinishReason { get; set; }

    /// <summary>
    /// The generated message.
    /// </summary>
    public required ChatMessage Message { get; set; }
}
