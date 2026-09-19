using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Request body for AnalyzeConversation (LingQue CCAI Conversation Analysis AIO).
/// </summary>
public class AnalyzeConversationRequest
{
    /// <summary>
    /// Dialogue content.
    /// </summary>
    [JsonPropertyName("dialogue")]
    public CcaiDialogue? Dialogue { get; set; }

    /// <summary>
    /// Analysis task types. See <see cref="CcaiResultTypes"/>.
    /// </summary>
    [JsonPropertyName("resultTypes")]
    public required List<string> ResultTypes { get; set; }

    /// <summary>
    /// Whether to stream the response via SSE.
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream { get; set; }

    /// <summary>
    /// Fields to extract when <see cref="CcaiResultTypes.Fields"/> is requested.
    /// </summary>
    [JsonPropertyName("fields")]
    public List<CcaiField>? Fields { get; set; }

    /// <summary>
    /// Service inspection configuration when <see cref="CcaiResultTypes.ServiceInspection"/> is requested.
    /// </summary>
    [JsonPropertyName("serviceInspection")]
    public CcaiServiceInspection? ServiceInspection { get; set; }

    /// <summary>
    /// Category tags when <see cref="CcaiResultTypes.CategoryTag"/> is requested.
    /// </summary>
    [JsonPropertyName("categoryTags")]
    public List<CcaiCategoryTag>? CategoryTags { get; set; }

    /// <summary>
    /// User profile fields when <see cref="CcaiResultTypes.UserProfile"/> is requested.
    /// </summary>
    [JsonPropertyName("userProfiles")]
    public List<CcaiUserProfile>? UserProfiles { get; set; }

    /// <summary>
    /// Few-shot examples.
    /// </summary>
    [JsonPropertyName("examples")]
    public List<CcaiExample>? Examples { get; set; }

    /// <summary>
    /// Model code, e.g. tyxmTurbo / tyxmPlus.
    /// </summary>
    [JsonPropertyName("modelCode")]
    public string? ModelCode { get; set; }

    /// <summary>
    /// Optional scene name.
    /// </summary>
    [JsonPropertyName("sceneName")]
    public string? SceneName { get; set; }

    /// <summary>
    /// Custom prompt. Must contain <c>${dialogue}</c> when used.
    /// </summary>
    [JsonPropertyName("customPrompt")]
    public string? CustomPrompt { get; set; }

    /// <summary>
    /// Response format: jsonObject or text.
    /// </summary>
    [JsonPropertyName("responseFormatType")]
    public string? ResponseFormatType { get; set; }

    /// <summary>
    /// Time constraints for analysis.
    /// </summary>
    [JsonPropertyName("timeConstraintList")]
    public List<string>? TimeConstraintList { get; set; }

    /// <summary>
    /// Creates a non-streaming summary request (best practice: summary generation).
    /// </summary>
    /// <param name="dialogue">Dialogue to analyze.</param>
    /// <param name="extraResultTypes">Additional result types such as title/keywords.</param>
    /// <returns>Configured request.</returns>
    public static AnalyzeConversationRequest ForSummary(
        CcaiDialogue dialogue,
        params string[] extraResultTypes)
    {
        var types = new List<string> { CcaiResultTypes.Summary };
        types.AddRange(extraResultTypes);
        return new AnalyzeConversationRequest
        {
            Dialogue = dialogue,
            ResultTypes = types,
            Stream = false
        };
    }

    /// <summary>
    /// Creates a non-streaming field extraction request (best practice: work-order field extraction).
    /// </summary>
    /// <param name="dialogue">Dialogue to analyze.</param>
    /// <param name="fields">Fields to extract.</param>
    /// <returns>Configured request.</returns>
    public static AnalyzeConversationRequest ForFieldExtraction(
        CcaiDialogue dialogue,
        IEnumerable<CcaiField> fields)
    {
        return new AnalyzeConversationRequest
        {
            Dialogue = dialogue,
            Fields = fields.ToList(),
            ResultTypes = new List<string> { CcaiResultTypes.Fields },
            Stream = false
        };
    }

    /// <summary>
    /// Creates a non-streaming service inspection request (best practice: quality inspection).
    /// </summary>
    /// <param name="dialogue">Dialogue to analyze.</param>
    /// <param name="serviceInspection">Inspection configuration.</param>
    /// <returns>Configured request.</returns>
    public static AnalyzeConversationRequest ForServiceInspection(
        CcaiDialogue dialogue,
        CcaiServiceInspection serviceInspection)
    {
        return new AnalyzeConversationRequest
        {
            Dialogue = dialogue,
            ServiceInspection = serviceInspection,
            ResultTypes = new List<string> { CcaiResultTypes.ServiceInspection },
            Stream = false
        };
    }
}
