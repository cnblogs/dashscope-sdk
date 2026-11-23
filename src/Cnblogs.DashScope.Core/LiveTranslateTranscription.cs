namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input audio transcription configuration of a live translation session.
/// qwen3.5-livetranslate-flash-realtime only; the ASR of qwen3.8-livetranslate-flash-realtime
/// is always on and free.
/// </summary>
/// <param name="Model">The ASR model, defaults to qwen3-asr-flash-realtime. Set it to null
/// explicitly to disable the ASR.</param>
/// <param name="Language">The language of the input audio. Unset to detect automatically.</param>
public record LiveTranslateTranscription(
    string? Model = null,
    string? Language = null);
