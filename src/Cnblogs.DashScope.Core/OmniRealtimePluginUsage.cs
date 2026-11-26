namespace Cnblogs.DashScope.Core;

/// <summary>
/// The plugin usage of an omni realtime response, present when the web search is enabled.
/// </summary>
/// <param name="Search">The web search usage.</param>
public record OmniRealtimePluginUsage(
    OmniRealtimeSearchUsage? Search = null);
