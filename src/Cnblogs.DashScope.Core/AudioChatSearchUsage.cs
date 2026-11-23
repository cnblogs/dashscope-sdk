namespace Cnblogs.DashScope.Core;

/// <summary>
/// The web search usage of an audio chat response.
/// </summary>
/// <param name="Count">The count of the searches.</param>
/// <param name="Strategy">The strategy of the searches.</param>
public record AudioChatSearchUsage(
    int? Count = null,
    string? Strategy = null);
