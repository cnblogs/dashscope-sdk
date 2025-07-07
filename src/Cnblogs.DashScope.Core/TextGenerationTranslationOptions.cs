namespace Cnblogs.DashScope.Core;

/// <summary>
/// Configurations when using translation models.
/// </summary>
public class TextGenerationTranslationOptions
{
    /// <summary>
    /// The language name of the input text. Use 'auto' to enable auto-detection.
    /// </summary>
    public string SourceLang { get; set; } = "auto";

    /// <summary>
    /// The language name of the output text.
    /// </summary>
    public string TargetLang { get; set; } = string.Empty;

    /// <summary>
    /// Term list for translation.
    /// </summary>
    public IEnumerable<TranslationReference>? Terms { get; set; }

    /// <summary>
    /// Sample texts for translation
    /// </summary>
    public IEnumerable<TranslationReference>? TmList { get; set; }

    /// <summary>
    /// Domain info about the source text. Only supports English.
    /// </summary>
    public string? Domains { get; set; }
}
