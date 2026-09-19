namespace Cnblogs.DashScope.Core;

/// <summary>
/// Output of create vocabulary.
/// </summary>
/// <param name="VocabularyId">Created vocabulary list id.</param>
public record SpeechVocabularyCreateOutput(string VocabularyId);

/// <summary>
/// Output of list vocabulary.
/// </summary>
/// <param name="VocabularyList">Matched vocabulary summaries.</param>
public record SpeechVocabularyListOutput(List<SpeechVocabularyListItem>? VocabularyList = null);

/// <summary>
/// Summary item in vocabulary list response.
/// </summary>
/// <param name="VocabularyId">Vocabulary list id.</param>
/// <param name="GmtCreate">Creation time.</param>
/// <param name="GmtModified">Last modified time.</param>
/// <param name="Status">Status such as OK or UNDEPLOYED.</param>
public record SpeechVocabularyListItem(
    string? VocabularyId = null,
    string? GmtCreate = null,
    string? GmtModified = null,
    string? Status = null);

/// <summary>
/// Output of query vocabulary.
/// </summary>
/// <param name="GmtCreate">Creation time.</param>
/// <param name="GmtModified">Last modified time.</param>
/// <param name="Status">Status such as OK or UNDEPLOYED.</param>
/// <param name="TargetModel">Target ASR model.</param>
/// <param name="Vocabulary">Hot word entries.</param>
public record SpeechVocabularyQueryOutput(
    string? GmtCreate = null,
    string? GmtModified = null,
    string? Status = null,
    string? TargetModel = null,
    List<SpeechVocabularyItem>? Vocabulary = null);

/// <summary>
/// Empty output for update / delete vocabulary.
/// </summary>
public record SpeechVocabularyMutationOutput;

/// <summary>
/// Usage of vocabulary customization APIs.
/// </summary>
/// <param name="Count">Affected vocabulary count, typically 1.</param>
public record SpeechVocabularyUsage(int Count);
