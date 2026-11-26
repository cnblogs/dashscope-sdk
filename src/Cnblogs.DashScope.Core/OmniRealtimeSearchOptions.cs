namespace Cnblogs.DashScope.Core;

/// <summary>
/// The web search options of an omni realtime session, only effective when
/// <see cref="OmniRealtimeSession.EnableSearch"/> is true.
/// </summary>
/// <param name="EnableSource">Whether to return the sources of the search results.</param>
public record OmniRealtimeSearchOptions(
    bool? EnableSource = null);
