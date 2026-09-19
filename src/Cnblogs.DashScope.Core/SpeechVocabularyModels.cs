namespace Cnblogs.DashScope.Core;

/// <summary>
/// Model name for speech vocabulary customization APIs.
/// </summary>
public static class SpeechVocabularyModels
{
    /// <summary>
    /// Fixed model name for vocabulary CRUD.
    /// </summary>
    public const string SpeechBiasing = "speech-biasing";
}

/// <summary>
/// Actions for speech vocabulary customization APIs.
/// </summary>
public static class SpeechVocabularyActions
{
    /// <summary>
    /// Create a vocabulary list.
    /// </summary>
    public const string Create = "create_vocabulary";

    /// <summary>
    /// List vocabulary lists.
    /// </summary>
    public const string List = "list_vocabulary";

    /// <summary>
    /// Query one vocabulary list.
    /// </summary>
    public const string Query = "query_vocabulary";

    /// <summary>
    /// Update a vocabulary list.
    /// </summary>
    public const string Update = "update_vocabulary";

    /// <summary>
    /// Delete a vocabulary list.
    /// </summary>
    public const string Delete = "delete_vocabulary";
}

/// <summary>
/// Known vocabulary deployment statuses.
/// </summary>
public static class SpeechVocabularyStatuses
{
    /// <summary>
    /// Vocabulary is ready to use.
    /// </summary>
    public const string Ok = "OK";

    /// <summary>
    /// Vocabulary is not deployed / not callable.
    /// </summary>
    public const string Undeployed = "UNDEPLOYED";
}
