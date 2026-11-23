namespace Cnblogs.DashScope.Core;

/// <summary>
/// The same language output options of a live translation session, only effective when the
/// source and the target language are the same (and the target is zh or en).
/// qwen3.5-livetranslate-flash-realtime only.
/// </summary>
/// <param name="SkipText">Whether to skip the text output when the languages are the same.</param>
/// <param name="SkipAudio">Whether to skip the audio output when the languages are the same.</param>
public record LiveTranslateSameLanguageSkipOptions(
    bool? SkipText = null,
    bool? SkipAudio = null);
