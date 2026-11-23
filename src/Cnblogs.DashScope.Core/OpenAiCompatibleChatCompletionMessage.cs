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
    /// The text content.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// The audio of the message, present in the responses of models with audio output.
    /// </summary>
    public OpenAiCompatibleChatMessageAudio? Audio { get; set; }
}
