namespace Cnblogs.DashScope.Core;

/// <summary>
/// Usage info of websocket task.
/// </summary>
/// <param name="Characters">Current character usage count. Available in speech synthesis tasks.</param>
/// <param name="Duration">Billed audio duration in seconds, rounded. Available in speech recognition tasks, reported in the final result of each sentence (the task-finished event does not always carry it).</param>
/// <param name="InputTokens">Input token count. Available in speech synthesis tasks of qwen-audio-3.1-tts-plus / qwen-audio-3.1-tts-flash, and in speech recognition tasks of qwen-audio-3.1-asr-flash-message / qwen-audio-3.1-asr-flash-streaming (qwen-audio-3.0-asr-flash-streaming reports the duration only).</param>
/// <param name="OutputTokens">Output token count. Available in speech synthesis tasks of qwen-audio-3.1-tts-plus / qwen-audio-3.1-tts-flash, and in speech recognition tasks of qwen-audio-3.1-asr-flash-message / qwen-audio-3.1-asr-flash-streaming (qwen-audio-3.0-asr-flash-streaming reports the duration only).</param>
/// <param name="TotalTokens">Total token count, the sum of <paramref name="InputTokens"/> and <paramref name="OutputTokens"/>. Available in speech synthesis tasks of qwen-audio-3.1-tts-plus / qwen-audio-3.1-tts-flash, and in speech recognition tasks of qwen-audio-3.1-asr-flash-message / qwen-audio-3.1-asr-flash-streaming (qwen-audio-3.0-asr-flash-streaming reports the duration only).</param>
public record DashScopeWebSocketResponseUsage(
    int Characters = 0,
    int? Duration = null,
    int? InputTokens = null,
    int? OutputTokens = null,
    int? TotalTokens = null);
