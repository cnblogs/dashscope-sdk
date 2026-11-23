namespace Cnblogs.DashScope.Core;

/// <summary>
/// The web search options of an audio chat session. Only effective when
/// <see cref="AudioChatSession.EnableSearch"/> is set to true.
/// </summary>
/// <param name="EnableSource">Whether to return the sources of the search results.</param>
public record AudioChatSearchOptions(
    bool? EnableSource = null);
