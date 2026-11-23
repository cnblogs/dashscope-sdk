namespace Cnblogs.DashScope.Core;

/// <summary>
/// A choice of a streamed OpenAI compatible chat completion chunk.
/// </summary>
public class OpenAiCompatibleChatChoice
{
    /// <summary>
    /// The index of the choice, always 0 so far.
    /// </summary>
    public int? Index { get; set; }

    /// <summary>
    /// The incremental content of the choice.
    /// </summary>
    public OpenAiCompatibleChatDelta? Delta { get; set; }

    /// <summary>
    /// The reason the generation stopped: stop, length, or null while it is still running.
    /// </summary>
    public string? FinishReason { get; set; }
}
