namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a vocabulary customization request. Which members are present depends
/// on <see cref="VocabularyCustomizationInput.Action"/>: create_vocabulary returns
/// <see cref="VocabularyId"/>, list_vocabulary returns <see cref="VocabularyList"/>,
/// query_vocabulary returns the detail members, update_vocabulary and delete_vocabulary
/// return an empty output.
/// </summary>
/// <param name="VocabularyId">The id of the created vocabulary.</param>
/// <param name="VocabularyList">The vocabularies returned by list_vocabulary.</param>
/// <param name="GmtCreate">The creation time of the vocabulary. query_vocabulary.</param>
/// <param name="GmtModified">The last modification time of the vocabulary. query_vocabulary.</param>
/// <param name="Status">The vocabulary status: OK (usable) or UNDEPLOYED (not usable).
/// query_vocabulary and list_vocabulary.</param>
/// <param name="TargetModel">The model consuming the vocabulary. query_vocabulary.</param>
/// <param name="Vocabulary">The hotwords of the vocabulary. query_vocabulary.</param>
public record VocabularyCustomizationOutput(
    string? VocabularyId = null,
    List<VocabularyCustomizationVocabulary>? VocabularyList = null,
    string? GmtCreate = null,
    string? GmtModified = null,
    string? Status = null,
    string? TargetModel = null,
    List<VocabularyWord>? Vocabulary = null);
