namespace Cnblogs.DashScope.Core;

/// <summary>
/// Usage info of websocket task.
/// </summary>
/// <param name="Characters">Current character usage count. Available in speech synthesis tasks.</param>
/// <param name="Duration">Billed audio duration in seconds. Available in speech recognition tasks.</param>
/// <param name="InputTokens">Input token count. Available in speech synthesis tasks of qwen-audio-3.1-tts-plus / qwen-audio-3.1-tts-flash.</param>
/// <param name="OutputTokens">Output token count. Available in speech synthesis tasks of qwen-audio-3.1-tts-plus / qwen-audio-3.1-tts-flash.</param>
/// <param name="TotalTokens">Total token count, the sum of <paramref name="InputTokens"/> and <paramref name="OutputTokens"/>. Available in speech synthesis tasks of qwen-audio-3.1-tts-plus / qwen-audio-3.1-tts-flash.</param>
public record DashScopeWebSocketResponseUsage(
    int Characters = 0,
    int? Duration = null,
    int? InputTokens = null,
    int? OutputTokens = null,
    int? TotalTokens = null);
