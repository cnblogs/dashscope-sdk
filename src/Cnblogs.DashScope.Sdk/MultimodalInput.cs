namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents inputs of a multi-model generation request.
/// </summary>
public class MultimodalInput
{
    /// <summary>
    /// The messages of context, model will generate from last user message.
    /// </summary>
    public required IEnumerable<MultimodalMessage> Messages { get; set; }
}
