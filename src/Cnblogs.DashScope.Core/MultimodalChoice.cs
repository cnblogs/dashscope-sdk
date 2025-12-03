namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents one generation choice.
    /// </summary>
    /// <param name="FinishReason">Generation finish reason, "null" means generation not finished yet.</param>
    /// <param name="Message">The generated message.</param>
    public record MultimodalChoice(string FinishReason, MultimodalMessage Message);
}
