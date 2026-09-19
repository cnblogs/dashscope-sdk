using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A weighted hot word entry for LingQue CCAI vocabularies.
/// Weight range is typically [-6, 5].
/// </summary>
public class CcaiWordWeight
{
    /// <summary>
    /// Hot word text.
    /// </summary>
    [JsonPropertyName("word")]
    public required string Word { get; set; }

    /// <summary>
    /// Recognition weight. Positive values increase match probability.
    /// </summary>
    [JsonPropertyName("weight")]
    public required int Weight { get; set; }
}

/// <summary>
/// Vocabulary metadata returned by list/get APIs.
/// </summary>
public class CcaiVocab
{
    /// <summary>
    /// Vocabulary id.
    /// </summary>
    [JsonPropertyName("vocabularyId")]
    public string? VocabularyId { get; set; }

    /// <summary>
    /// Vocabulary name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Vocabulary description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// ASR model code, e.g. nls.
    /// </summary>
    [JsonPropertyName("audioModelCode")]
    public string? AudioModelCode { get; set; }

    /// <summary>
    /// Weighted hot words.
    /// </summary>
    [JsonPropertyName("wordWeightList")]
    public List<CcaiWordWeight>? WordWeightList { get; set; }
}

/// <summary>
/// Request to create a CCAI vocabulary.
/// </summary>
public class CcaiCreateVocabRequest
{
    /// <summary>
    /// Workspace id.
    /// </summary>
    [JsonPropertyName("workspaceId")]
    public required string WorkspaceId { get; set; }

    /// <summary>
    /// Vocabulary name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Weighted hot words.
    /// </summary>
    [JsonPropertyName("wordWeightList")]
    public required List<CcaiWordWeight> WordWeightList { get; set; }

    /// <summary>
    /// Optional description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Optional ASR model code.
    /// </summary>
    [JsonPropertyName("audioModelCode")]
    public string? AudioModelCode { get; set; }
}

/// <summary>
/// Response for CreateVocab.
/// </summary>
public class CcaiCreateVocabResponse
{
    /// <summary>
    /// Request id.
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether the call succeeded.
    /// </summary>
    [JsonPropertyName("success")]
    public object? Success { get; set; }

    /// <summary>
    /// Created vocabulary payload.
    /// </summary>
    [JsonPropertyName("data")]
    public CcaiCreateVocabData? Data { get; set; }
}

/// <summary>
/// CreateVocab data payload.
/// </summary>
public class CcaiCreateVocabData
{
    /// <summary>
    /// Created vocabulary id. Pass this to CreateTask transcription.vocabularyId.
    /// </summary>
    [JsonPropertyName("vocabularyId")]
    public string? VocabularyId { get; set; }
}

/// <summary>
/// Request to update a CCAI vocabulary.
/// </summary>
public class CcaiUpdateVocabRequest
{
    /// <summary>
    /// Workspace id.
    /// </summary>
    [JsonPropertyName("workspaceId")]
    public required string WorkspaceId { get; set; }

    /// <summary>
    /// Vocabulary id to update.
    /// </summary>
    [JsonPropertyName("vocabularyId")]
    public required string VocabularyId { get; set; }

    /// <summary>
    /// Optional new name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Optional new description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Optional replacement word list.
    /// </summary>
    [JsonPropertyName("wordWeightList")]
    public List<CcaiWordWeight>? WordWeightList { get; set; }
}

/// <summary>
/// Request to delete a CCAI vocabulary.
/// </summary>
public class CcaiDeleteVocabRequest
{
    /// <summary>
    /// Workspace id.
    /// </summary>
    [JsonPropertyName("workspaceId")]
    public required string WorkspaceId { get; set; }

    /// <summary>
    /// Vocabulary id to delete.
    /// </summary>
    [JsonPropertyName("vocabularyId")]
    public required string VocabularyId { get; set; }
}

/// <summary>
/// Request to get a CCAI vocabulary by id.
/// </summary>
public class CcaiGetVocabRequest
{
    /// <summary>
    /// Workspace id.
    /// </summary>
    [JsonPropertyName("workspaceId")]
    public required string WorkspaceId { get; set; }

    /// <summary>
    /// Vocabulary id.
    /// </summary>
    [JsonPropertyName("vocabularyId")]
    public required string VocabularyId { get; set; }
}

/// <summary>
/// Request to list CCAI vocabularies in a workspace.
/// </summary>
public class CcaiListVocabRequest
{
    /// <summary>
    /// Workspace id.
    /// </summary>
    [JsonPropertyName("workspaceId")]
    public required string WorkspaceId { get; set; }
}

/// <summary>
/// Response for UpdateVocab / DeleteVocab.
/// </summary>
public class CcaiVocabMutationResponse
{
    /// <summary>
    /// Request id.
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether the call succeeded.
    /// </summary>
    [JsonPropertyName("success")]
    public object? Success { get; set; }

    /// <summary>
    /// Mutation result flag.
    /// </summary>
    [JsonPropertyName("data")]
    public object? Data { get; set; }
}

/// <summary>
/// Response for GetVocab.
/// </summary>
public class CcaiGetVocabResponse
{
    /// <summary>
    /// Request id.
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether the call succeeded.
    /// </summary>
    [JsonPropertyName("success")]
    public object? Success { get; set; }

    /// <summary>
    /// Vocabulary detail.
    /// </summary>
    [JsonPropertyName("data")]
    public CcaiVocab? Data { get; set; }
}

/// <summary>
/// Response for ListVocab.
/// </summary>
public class CcaiListVocabResponse
{
    /// <summary>
    /// Request id.
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether the call succeeded.
    /// </summary>
    [JsonPropertyName("success")]
    public object? Success { get; set; }

    /// <summary>
    /// Vocabularies in the workspace.
    /// </summary>
    [JsonPropertyName("data")]
    public List<CcaiVocab>? Data { get; set; }
}
