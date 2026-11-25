using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The extra options of an omni-modal translation (model qwen-mt-uni).
/// </summary>
public class UniTranslationInputExt
{
    /// <summary>
    /// The hint of the scene and the translation style in English, at most 200 words, e.g.
    /// <c>These sentences are from seller-buyer conversations on a B2C ecommerce platform.</c>
    /// </summary>
    [JsonPropertyName("domainHint")]
    public string? DomainHint { get; set; }

    /// <summary>
    /// The format of the file when <see cref="UniTranslationInput.FileUrl"/> has no recognizable
    /// extension: <c>image</c> (or an image extension such as <c>jpg</c>), a document extension
    /// such as <c>pdf</c> or <c>docx</c>, or <c>audio</c>. When the url has an extension, the hint
    /// has to match it.
    /// </summary>
    public string? FormatHint { get; set; }

    /// <summary>
    /// The texts to keep untranslated, case sensitive, at most 50 non-empty strings. A source
    /// text that equals one of them is returned as it is, without being sent to the model.
    /// </summary>
    public List<string>? Sensitives { get; set; }

    /// <summary>
    /// The glossary, at most 100 terms. It is injected into the prompt, so the model is guided
    /// to use the terms rather than forced to.
    /// </summary>
    public List<UniTranslationGlossaryTerm>? Glossary { get; set; }

    /// <summary>
    /// The extra configs of the translation.
    /// </summary>
    public UniTranslationConfig? Config { get; set; }
}
