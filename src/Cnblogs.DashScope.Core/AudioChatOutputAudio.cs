namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output audio configuration of an audio chat session. qwen-audio-3.1-realtime-plus only.
/// </summary>
/// <param name="Language">The language of the synthesized audio. Best effort - it does not change
/// the language of the response text, instruct the model via
/// <see cref="AudioChatSession.Instructions"/> for that.</param>
public record AudioChatOutputAudio(
    string? Language = null);
