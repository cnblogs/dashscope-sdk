namespace Cnblogs.DashScope.Core;

/// <summary>
/// The message of a complete OpenAI compatible chat completion choice.
/// </summary>
public class OpenAiCompatibleChatCompletionMessage
{
    /// <summary>
    /// The role of the assistant.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// The text content, empty when the message only carries tool calls.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// The reasoning of the thinking models, absent when the thinking is off and empty
    /// for the models that never think (e.g. qwen3.5-omni-flash).
    /// </summary>
    public string? ReasoningContent { get; set; }

    /// <summary>
    /// The audio of the message, present in the responses of models with audio output.
    /// </summary>
    public OpenAiCompatibleChatMessageAudio? Audio { get; set; }

    /// <summary>
    /// The tools the model asks to call, present when the finish reason is tool_calls.
    /// </summary>
    public List<ToolCall>? ToolCalls { get; set; }
}
