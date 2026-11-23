namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input of a vocabulary customization request (custom hotword management via the
/// speech-biasing model). The action picks the operation, which members are required
/// depends on it; this API is only served on the workspace domain, configure
/// <see cref="DashScopeOptions.WorkspaceId"/> to route requests there.
/// </summary>
public class VocabularyCustomizationInput
{
    /// <summary>
    /// The operation to perform: create_vocabulary, list_vocabulary, query_vocabulary,
    /// update_vocabulary or delete_vocabulary.
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// The model consuming the vocabulary, must match the model of subsequent speech
    /// recognition requests. create_vocabulary only.
    /// </summary>
    public string? TargetModel { get; set; }

    /// <summary>
    /// The prefix of the vocabulary id, digits and lowercase letters only, at most 10
    /// characters. The generated id follows <c>vocab-{prefix}-{id}</c>. create_vocabulary,
    /// and the filter of list_vocabulary.
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// The hotwords of the vocabulary. create_vocabulary and update_vocabulary (the new
    /// list completely replaces the previous one).
    /// </summary>
    public List<VocabularyWord>? Vocabulary { get; set; }

    /// <summary>
    /// The id of the vocabulary to query, update or delete. query_vocabulary,
    /// update_vocabulary and delete_vocabulary.
    /// </summary>
    public string? VocabularyId { get; set; }

    /// <summary>
    /// The page index of list_vocabulary, starting from 0, defaults to 0.
    /// </summary>
    public int? PageIndex { get; set; }

    /// <summary>
    /// The page size of list_vocabulary, defaults to 10.
    /// </summary>
    public int? PageSize { get; set; }
}
