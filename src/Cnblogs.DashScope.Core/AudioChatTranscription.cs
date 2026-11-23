namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input audio transcription configuration of an audio chat session. Setting the language
/// improves the recognition effect; the model detects the language automatically when unset.
/// qwen-audio-3.1-realtime-plus only.
/// </summary>
/// <param name="Language">The language of the input audio, e.g. zh, en, ja. Unset to detect
/// automatically.</param>
/// <param name="Model">The transcription model. Server echo only, always fun-asr so far.</param>
public record AudioChatTranscription(
    string? Language = null,
    string? Model = null);
