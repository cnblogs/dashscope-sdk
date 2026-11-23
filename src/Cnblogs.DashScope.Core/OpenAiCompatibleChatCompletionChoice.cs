namespace Cnblogs.DashScope.Core;

/// <summary>
/// A choice of a complete OpenAI compatible chat completion.
/// </summary>
public class OpenAiCompatibleChatCompletionChoice
{
    /// <summary>
    /// The index of the choice, always 0 so far.
    /// </summary>
    public int? Index { get; set; }

    /// <summary>
    /// The message of the choice.
    /// </summary>
    public OpenAiCompatibleChatCompletionMessage? Message { get; set; }

    /// <summary>
    /// The reason the generation stopped: stop or length.
    /// </summary>
    public string? FinishReason { get; set; }
}
