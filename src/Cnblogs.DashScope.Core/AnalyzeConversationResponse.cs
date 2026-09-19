using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Response body for AnalyzeConversation.
/// </summary>
public class AnalyzeConversationResponse
{
    /// <summary>
    /// Error code when the call fails.
    /// </summary>
    [JsonPropertyName("errorCode")]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Error message when the call fails.
    /// </summary>
    [JsonPropertyName("errorInfo")]
    public string? ErrorInfo { get; set; }

    /// <summary>
    /// Finish reason. For streaming, null while generating; typically "stop" when done.
    /// </summary>
    [JsonPropertyName("finishReason")]
    public string? FinishReason { get; set; }

    /// <summary>
    /// Unique request id.
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether the request succeeded.
    /// Live AnalyzeConversation responses often omit this field; treat non-empty <see cref="Text"/>
    /// with <see cref="FinishReason"/> <c>stop</c> as success when this is null.
    /// </summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>
    /// Analysis result text.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Input token count.
    /// </summary>
    [JsonPropertyName("inputTokens")]
    public string? InputTokens { get; set; }

    /// <summary>
    /// Output token count.
    /// </summary>
    [JsonPropertyName("outputTokens")]
    public string? OutputTokens { get; set; }

    /// <summary>
    /// Total token count.
    /// </summary>
    [JsonPropertyName("totalTokens")]
    public string? TotalTokens { get; set; }
}
