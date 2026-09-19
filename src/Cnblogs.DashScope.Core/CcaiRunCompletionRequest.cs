using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Template-based completion request (RunCompletion). Uses PascalCase JSON names per official examples.
/// </summary>
public class CcaiRunCompletionRequest
{
    /// <summary>
    /// Dialogue content.
    /// </summary>
    [JsonPropertyName("Dialogue")]
    public required CcaiRunCompletionDialogue Dialogue { get; set; }

    /// <summary>
    /// Template ids under the CCAI application.
    /// </summary>
    [JsonPropertyName("TemplateIds")]
    public required List<long> TemplateIds { get; set; }

    /// <summary>
    /// Whether to enable SSE streaming.
    /// </summary>
    [JsonPropertyName("Stream")]
    public bool Stream { get; set; }

    /// <summary>
    /// Model code.
    /// </summary>
    [JsonPropertyName("ModelCode")]
    public string? ModelCode { get; set; }

    /// <summary>
    /// Fields for extraction templates.
    /// </summary>
    [JsonPropertyName("Fields")]
    public List<CcaiRunCompletionField>? Fields { get; set; }

    /// <summary>
    /// Service inspection configuration.
    /// </summary>
    [JsonPropertyName("ServiceInspection")]
    public CcaiRunCompletionServiceInspection? ServiceInspection { get; set; }

    /// <summary>
    /// Template variables.
    /// </summary>
    [JsonPropertyName("variables")]
    public List<CcaiVariable>? Variables { get; set; }

    /// <summary>
    /// Response format type.
    /// </summary>
    [JsonPropertyName("responseFormatType")]
    public string? ResponseFormatType { get; set; }
}

/// <summary>
/// Dialogue for RunCompletion.
/// </summary>
public class CcaiRunCompletionDialogue
{
    /// <summary>
    /// Session id.
    /// </summary>
    [JsonPropertyName("SessionId")]
    public string? SessionId { get; set; }

    /// <summary>
    /// Dialogue turns.
    /// </summary>
    [JsonPropertyName("Sentences")]
    public required List<CcaiRunCompletionSentence> Sentences { get; set; }
}

/// <summary>
/// Sentence for RunCompletion.
/// </summary>
public class CcaiRunCompletionSentence
{
    /// <summary>
    /// Chat id.
    /// </summary>
    [JsonPropertyName("ChatId")]
    public string? ChatId { get; set; }

    /// <summary>
    /// Role.
    /// </summary>
    [JsonPropertyName("Role")]
    public required string Role { get; set; }

    /// <summary>
    /// Text.
    /// </summary>
    [JsonPropertyName("Text")]
    public required string Text { get; set; }
}

/// <summary>
/// Field for RunCompletion.
/// </summary>
public class CcaiRunCompletionField
{
    /// <summary>
    /// Field code.
    /// </summary>
    [JsonPropertyName("Code")]
    public string? Code { get; set; }

    /// <summary>
    /// Field name.
    /// </summary>
    [JsonPropertyName("Name")]
    public required string Name { get; set; }

    /// <summary>
    /// Field description.
    /// </summary>
    [JsonPropertyName("Desc")]
    public string? Desc { get; set; }

    /// <summary>
    /// Enum values.
    /// </summary>
    [JsonPropertyName("EnumValues")]
    public List<CcaiRunCompletionFieldEnumValue>? EnumValues { get; set; }
}

/// <summary>
/// Enum value for RunCompletion field.
/// </summary>
public class CcaiRunCompletionFieldEnumValue
{
    /// <summary>
    /// Enum value.
    /// </summary>
    [JsonPropertyName("EnumValue")]
    public required string EnumValue { get; set; }

    /// <summary>
    /// Description.
    /// </summary>
    [JsonPropertyName("Desc")]
    public string? Desc { get; set; }
}

/// <summary>
/// Service inspection for RunCompletion.
/// </summary>
public class CcaiRunCompletionServiceInspection
{
    /// <summary>
    /// Inspection contents.
    /// </summary>
    [JsonPropertyName("InspectionContents")]
    public List<CcaiRunCompletionInspectionContent>? InspectionContents { get; set; }

    /// <summary>
    /// Inspection introduction.
    /// </summary>
    [JsonPropertyName("InspectionIntroduction")]
    public string? InspectionIntroduction { get; set; }

    /// <summary>
    /// Scene introduction.
    /// </summary>
    [JsonPropertyName("SceneIntroduction")]
    public string? SceneIntroduction { get; set; }
}

/// <summary>
/// Inspection content for RunCompletion.
/// </summary>
public class CcaiRunCompletionInspectionContent
{
    /// <summary>
    /// Title.
    /// </summary>
    [JsonPropertyName("Title")]
    public required string Title { get; set; }

    /// <summary>
    /// Content.
    /// </summary>
    [JsonPropertyName("Content")]
    public string? Content { get; set; }
}

/// <summary>
/// Template variable.
/// </summary>
public class CcaiVariable
{
    /// <summary>
    /// Variable code.
    /// </summary>
    [JsonPropertyName("variableCode")]
    public string? VariableCode { get; set; }

    /// <summary>
    /// Variable value.
    /// </summary>
    [JsonPropertyName("variableValue")]
    public string? VariableValue { get; set; }
}

/// <summary>
/// Response for RunCompletion.
/// </summary>
public class CcaiRunCompletionResponse
{
    /// <summary>
    /// Finish reason.
    /// </summary>
    [JsonPropertyName("FinishReason")]
    public string? FinishReason { get; set; }

    /// <summary>
    /// Request id.
    /// </summary>
    [JsonPropertyName("RequestId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Result text.
    /// </summary>
    [JsonPropertyName("Text")]
    public string? Text { get; set; }

    /// <summary>
    /// Input tokens.
    /// </summary>
    [JsonPropertyName("inputTokens")]
    public string? InputTokens { get; set; }

    /// <summary>
    /// Output tokens.
    /// </summary>
    [JsonPropertyName("outputTokens")]
    public string? OutputTokens { get; set; }

    /// <summary>
    /// Total tokens.
    /// </summary>
    [JsonPropertyName("totalTokens")]
    public string? TotalTokens { get; set; }

    /// <summary>
    /// RAG status.
    /// </summary>
    [JsonPropertyName("ragStatus")]
    public string? RagStatus { get; set; }
}
