namespace Cnblogs.DashScope.Core;

/// <summary>
/// The hotword corpus of a live translation session, improving the translation of specific
/// phrases.
/// </summary>
public class LiveTranslateCorpus
{
    /// <summary>
    /// The phrase map: keys are the source language phrases, values are their target language
    /// translations, e.g. { ["人工智能"] = "Artificial Intelligence" }.
    /// </summary>
    public Dictionary<string, string>? Phrases { get; set; }
}
