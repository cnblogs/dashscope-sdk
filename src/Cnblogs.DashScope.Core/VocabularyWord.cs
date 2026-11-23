namespace Cnblogs.DashScope.Core;

/// <summary>
/// A hotword of a vocabulary customization request, or of the vocabulary returned by
/// the query_vocabulary action.
/// </summary>
/// <param name="Text">The hotword text, an actual word rather than arbitrary characters:
/// at most 15 characters when containing non-ASCII ones, otherwise at most 7 space
/// separated segments. The language must be supported by the target model.</param>
/// <param name="Weight">The weight of the hotword, range [1, 5], commonly 4. Larger
/// values favor the hotword but may hurt the recognition of other words.</param>
/// <param name="Lang">The language of the audio to recognize (e.g. zh, en, ja, more for
/// Paraformer). When unset the model detects the language automatically.</param>
public record VocabularyWord(
    string? Text = null,
    int? Weight = null,
    string? Lang = null);
