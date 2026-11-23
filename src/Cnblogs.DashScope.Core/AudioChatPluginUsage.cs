namespace Cnblogs.DashScope.Core;

/// <summary>
/// The plugin usage of an audio chat response, present when the web search is enabled.
/// </summary>
/// <param name="Search">The web search usage.</param>
public record AudioChatPluginUsage(
    AudioChatSearchUsage? Search = null);
