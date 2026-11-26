namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of an omni realtime response, returned by the response.done events of a
/// completed response. Absent when the response was cancelled.
/// </summary>
public class OmniRealtimeUsage
{
    /// <summary>
    /// The total token count.
    /// </summary>
    public int? TotalTokens { get; set; }

    /// <summary>
    /// The input token count. It covers the whole context, so the previous turns count again in
    /// every response.
    /// </summary>
    public int? InputTokens { get; set; }

    /// <summary>
    /// The output token count.
    /// </summary>
    public int? OutputTokens { get; set; }

    /// <summary>
    /// The input token details.
    /// </summary>
    public OmniRealtimeTokenDetails? InputTokensDetails { get; set; }

    /// <summary>
    /// The output token details.
    /// </summary>
    public OmniRealtimeTokenDetails? OutputTokensDetails { get; set; }

    /// <summary>
    /// The plugin usage, present when the web search is enabled.
    /// </summary>
    public OmniRealtimePluginUsage? Plugins { get; set; }
}
