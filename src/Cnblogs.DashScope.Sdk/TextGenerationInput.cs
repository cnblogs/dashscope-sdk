namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Input parameters for text generation.
/// </summary>
public class TextGenerationInput
{
    /// <summary>
    /// The prompt to generate completion from.
    /// </summary>
    public string? Prompt { get; set; }

    /// <summary>
    /// The collection of context messages associated with this chat completions request.
    /// </summary>
    public IEnumerable<ChatMessage>? Messages { get; set; }
}
