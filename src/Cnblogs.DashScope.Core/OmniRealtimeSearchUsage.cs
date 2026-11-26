namespace Cnblogs.DashScope.Core;

/// <summary>
/// The web search usage of an omni realtime response.
/// </summary>
/// <param name="Count">The count of the searches.</param>
/// <param name="Strategy">The strategy of the searches, e.g. agent.</param>
public record OmniRealtimeSearchUsage(
    int? Count = null,
    string? Strategy = null);
