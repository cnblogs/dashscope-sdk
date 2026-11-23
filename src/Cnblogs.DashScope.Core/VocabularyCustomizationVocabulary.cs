namespace Cnblogs.DashScope.Core;

/// <summary>
/// A vocabulary in the list returned by the list_vocabulary action.
/// </summary>
/// <param name="VocabularyId">The id of the vocabulary, usable by speech recognition requests.</param>
/// <param name="GmtCreate">The creation time.</param>
/// <param name="GmtModified">The last modification time.</param>
/// <param name="Status">The vocabulary status: OK (usable) or UNDEPLOYED (not usable).</param>
public record VocabularyCustomizationVocabulary(
    string? VocabularyId = null,
    string? GmtCreate = null,
    string? GmtModified = null,
    string? Status = null);
