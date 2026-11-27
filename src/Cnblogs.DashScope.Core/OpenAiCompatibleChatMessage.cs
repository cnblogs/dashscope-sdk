namespace Cnblogs.DashScope.Core;

/// <summary>
/// A chat message of an OpenAI compatible chat completion request. The content is always
/// a list of parts - plain text goes in a <c>text</c> part
/// (<see cref="OpenAiCompatibleChatContentPart.Text"/>), which every model accepts.
/// </summary>
public class OpenAiCompatibleChatMessage
{
    /// <summary>
    /// The role of the message author: system, user, assistant or tool.
    /// </summary>
    public string Role { get; set; } = "user";

    /// <summary>
    /// The content parts of the message. A tool message carries the output of the tool in
    /// a <c>text</c> part.
    /// </summary>
    public List<OpenAiCompatibleChatContentPart> Content { get; set; } = new();

    /// <summary>
    /// The reasoning of an assistant message of an earlier turn, copy it from the
    /// <see cref="OpenAiCompatibleChatCompletionMessage.ReasoningContent"/> of that turn:
    /// qwen3.8-omni-flash then continues from its earlier thinking.
    /// </summary>
    public string? ReasoningContent { get; set; }

    /// <summary>
    /// The tool calls of an assistant message of an earlier turn, copy them from the
    /// <see cref="OpenAiCompatibleChatCompletionMessage.ToolCalls"/> of that turn.
    /// </summary>
    public List<ToolCall>? ToolCalls { get; set; }

    /// <summary>
    /// The id of the tool call a tool message answers, the <see cref="ToolCall.Id"/> of
    /// the assistant message.
    /// </summary>
    public string? ToolCallId { get; set; }
}
