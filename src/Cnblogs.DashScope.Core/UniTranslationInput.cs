using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of an omni-modal translation (model qwen-mt-uni): either the texts in
/// <see cref="SourceTexts"/> or the file in <see cref="FileUrl"/>, exactly one of them.
/// </summary>
public class UniTranslationInput
{
    /// <summary>
    /// The publicly accessible HTTP(S) url of the file to translate, the modality (document,
    /// image or audio) is told from the url extension. Documents: pdf, docx, pptx, xlsx, txt,
    /// html, md; images: png, jpg; audio: mp3, wav (the server also accepts the undocumented
    /// xls, webp, bmp, m4a, aac, flac, ogg, opus and mp4). At most 100 MB, documents at most
    /// 200 pages, audio between 3 seconds and 60 minutes. The official document requires the
    /// url to contain no Chinese characters. Pass <see cref="UniTranslationInputExt.FormatHint"/>
    /// when the url has no extension. The result is a translated file, see
    /// <see cref="UniTranslationData.TranslatedFileUrl"/>.
    /// </summary>
    [JsonPropertyName("fileUrl")]
    public string? FileUrl { get; set; }

    /// <summary>
    /// The texts to translate, at most 50 non-empty strings of at most 10,000 characters each.
    /// Always sent as an array, so <see cref="UniTranslationData.TranslatedTexts"/> comes back
    /// as an array in the same order.
    /// </summary>
    public List<string>? SourceTexts { get; set; }

    /// <summary>
    /// The source language code, e.g. zh, en, ja. Unset (or "auto") to detect it automatically.
    /// </summary>
    public string? SourceLang { get; set; }

    /// <summary>
    /// The target language, as its code (e.g. en, zh-tw) or its English name (e.g. English),
    /// case insensitive.
    /// </summary>
    public string TargetLang { get; set; } = string.Empty;

    /// <summary>
    /// The extra options of the translation.
    /// </summary>
    public UniTranslationInputExt? Ext { get; set; }
}
