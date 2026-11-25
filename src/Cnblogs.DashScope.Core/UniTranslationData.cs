using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The result of an omni-modal translation (model qwen-mt-uni).
/// </summary>
/// <param name="FileType">The type of the result: strings for a texts input, document, image or
/// audio for a file input.</param>
/// <param name="TranslatedTexts">The translated texts, in the order of the source texts.
/// Texts input only.</param>
/// <param name="TranslatedFileUrl">The download url of the translated file, signed and valid
/// for 24 hours. File input only.</param>
public record UniTranslationData(
    [property: JsonPropertyName("FileType")] string FileType,
    [property: JsonPropertyName("TranslatedTexts")] List<string>? TranslatedTexts = null,
    [property: JsonPropertyName("TranslatedFileUrl")] string? TranslatedFileUrl = null);
