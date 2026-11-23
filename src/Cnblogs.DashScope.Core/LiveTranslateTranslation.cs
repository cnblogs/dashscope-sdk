namespace Cnblogs.DashScope.Core;

/// <summary>
/// The translation configuration of a live translation session.
/// </summary>
public class LiveTranslateTranslation
{
    /// <summary>
    /// The target language of the translation, defaults to en.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// The same language output options. qwen3.5-livetranslate-flash-realtime only.
    /// </summary>
    public LiveTranslateSameLanguageSkipOptions? SameLanguageSkipOptions { get; set; }

    /// <summary>
    /// The hotword corpus improving the translation of specific phrases.
    /// </summary>
    public LiveTranslateCorpus? Corpus { get; set; }
}
