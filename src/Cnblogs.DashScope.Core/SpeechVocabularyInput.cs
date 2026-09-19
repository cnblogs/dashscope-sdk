namespace Cnblogs.DashScope.Core;

/// <summary>
/// Input for speech vocabulary customization APIs. Use factory methods to set the correct action.
/// </summary>
public class SpeechVocabularyInput
{
    /// <summary>
    /// Operation action. See <see cref="SpeechVocabularyActions"/>.
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Target ASR model that will consume this vocabulary. Must match recognition model.
    /// </summary>
    public string? TargetModel { get; set; }

    /// <summary>
    /// Custom prefix for vocabulary id (lowercase letters and digits, max 10 chars).
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// Hot word entries.
    /// </summary>
    public IReadOnlyList<SpeechVocabularyItem>? Vocabulary { get; set; }

    /// <summary>
    /// Vocabulary list id.
    /// </summary>
    public string? VocabularyId { get; set; }

    /// <summary>
    /// Page index for list action, starting from 0.
    /// </summary>
    public int? PageIndex { get; set; }

    /// <summary>
    /// Page size for list action.
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// Create a create_vocabulary input.
    /// </summary>
    /// <param name="targetModel">ASR model that will consume this vocabulary.</param>
    /// <param name="prefix">Custom prefix.</param>
    /// <param name="vocabulary">Hot word entries.</param>
    /// <returns>The input payload.</returns>
    public static SpeechVocabularyInput Create(
        string targetModel,
        string prefix,
        IEnumerable<SpeechVocabularyItem> vocabulary)
        => new()
        {
            Action = SpeechVocabularyActions.Create,
            TargetModel = targetModel,
            Prefix = prefix,
            Vocabulary = vocabulary.ToList()
        };

    /// <summary>
    /// Create a list_vocabulary input.
    /// </summary>
    /// <param name="prefix">Optional prefix filter.</param>
    /// <param name="pageIndex">Page index starting from 0.</param>
    /// <param name="pageSize">Page size.</param>
    /// <returns>The input payload.</returns>
    public static SpeechVocabularyInput List(string? prefix = null, int? pageIndex = null, int? pageSize = null)
        => new()
        {
            Action = SpeechVocabularyActions.List,
            Prefix = prefix,
            PageIndex = pageIndex,
            PageSize = pageSize
        };

    /// <summary>
    /// Create a query_vocabulary input.
    /// </summary>
    /// <param name="vocabularyId">Vocabulary list id.</param>
    /// <returns>The input payload.</returns>
    public static SpeechVocabularyInput Query(string vocabularyId)
        => new()
        {
            Action = SpeechVocabularyActions.Query,
            VocabularyId = vocabularyId
        };

    /// <summary>
    /// Create an update_vocabulary input.
    /// </summary>
    /// <param name="vocabularyId">Vocabulary list id.</param>
    /// <param name="vocabulary">New hot word entries.</param>
    /// <returns>The input payload.</returns>
    public static SpeechVocabularyInput Update(string vocabularyId, IEnumerable<SpeechVocabularyItem> vocabulary)
        => new()
        {
            Action = SpeechVocabularyActions.Update,
            VocabularyId = vocabularyId,
            Vocabulary = vocabulary.ToList()
        };

    /// <summary>
    /// Create a delete_vocabulary input.
    /// </summary>
    /// <param name="vocabularyId">Vocabulary list id.</param>
    /// <returns>The input payload.</returns>
    public static SpeechVocabularyInput Delete(string vocabularyId)
        => new()
        {
            Action = SpeechVocabularyActions.Delete,
            VocabularyId = vocabularyId
        };
}
