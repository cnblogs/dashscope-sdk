namespace Cnblogs.DashScope.Core;

/// <summary>
/// A complete OpenAI compatible chat completion.
/// </summary>
public class OpenAiCompatibleChatCompletion
{
    /// <summary>
    /// The id of the completion.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Always chat.completion.
    /// </summary>
    public string? Object { get; set; }

    /// <summary>
    /// The creation timestamp of the request.
    /// </summary>
    public int? Created { get; set; }

    /// <summary>
    /// The model of the request.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// The choices of the completion.
    /// </summary>
    public List<OpenAiCompatibleChatCompletionChoice> Choices { get; set; } = new();

    /// <summary>
    /// The token usage.
    /// </summary>
    public OpenAiCompatibleChatUsage? Usage { get; set; }
}
