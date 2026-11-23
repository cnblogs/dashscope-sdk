namespace Cnblogs.DashScope.Core;

/// <summary>
/// A chat message of an OpenAI compatible chat completion request. The content is always
/// a list of parts - plain text goes in a <c>text</c> part
/// (<see cref="OpenAiCompatibleChatContentPart.Text"/>), which every model accepts.
/// </summary>
public class OpenAiCompatibleChatMessage
{
    /// <summary>
    /// The role of the message author: system, user or assistant.
    /// </summary>
    public string Role { get; set; } = "user";

    /// <summary>
    /// The content parts of the message.
    /// </summary>
    public List<OpenAiCompatibleChatContentPart> Content { get; set; } = new();
}
