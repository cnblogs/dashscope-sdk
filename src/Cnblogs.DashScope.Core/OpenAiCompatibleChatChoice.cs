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
    /// The incremental content of the choice. Absent in the chunk that opens a web search
    /// stream of qwen3.8-omni-flash, which only announces the assistant.
    /// </summary>
    public OpenAiCompatibleChatDelta? Delta { get; set; }

    /// <summary>
    /// The reason the generation stopped: stop, tool_calls, length, or null while it is
    /// still running.
    /// </summary>
    public string? FinishReason { get; set; }
}
