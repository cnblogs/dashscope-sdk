namespace Cnblogs.DashScope.Core;

/// <summary>
/// A streamed chunk of an OpenAI compatible chat completion.
/// </summary>
public class OpenAiCompatibleChatChunk
{
    /// <summary>
    /// The id of the completion, shared by all chunks of the stream.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Always chat.completion.chunk.
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
    /// The choices of the chunk; empty in the last chunk, which only carries the usage.
    /// </summary>
    public List<OpenAiCompatibleChatChoice> Choices { get; set; } = new();

    /// <summary>
    /// The token usage, present in the last chunk when
    /// <see cref="OpenAiCompatibleChatStreamOptions.IncludeUsage"/> is set.
    /// </summary>
    public OpenAiCompatibleChatUsage? Usage { get; set; }
}
