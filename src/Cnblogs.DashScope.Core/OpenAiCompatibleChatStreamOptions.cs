namespace Cnblogs.DashScope.Core;

/// <summary>
/// The streaming options of an OpenAI compatible chat completion request.
/// </summary>
/// <param name="IncludeUsage">Whether the last chunk carries the token usage. Streaming only.</param>
public record OpenAiCompatibleChatStreamOptions(
    bool? IncludeUsage = null);
