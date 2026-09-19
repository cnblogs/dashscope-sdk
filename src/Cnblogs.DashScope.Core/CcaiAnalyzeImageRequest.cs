using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Request body for AnalyzeImage (e.g. watermark detection).
/// </summary>
public class CcaiAnalyzeImageRequest
{
    /// <summary>
    /// Whether to stream the response via SSE.
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream { get; set; }
    /// <summary>
    /// Image URL list.
    /// </summary>
    [JsonPropertyName("imageUrls")]
    public List<string>? ImageUrls { get; set; }

    /// <summary>
    /// Analysis task types. See <see cref="CcaiImageResultTypes"/>.
    /// </summary>
    [JsonPropertyName("resultTypes")]
    public List<string>? ResultTypes { get; set; }
}

/// <summary>
/// Request body for GeneralAnalyzeImage.
/// </summary>
public class CcaiGeneralAnalyzeImageRequest
{
    /// <summary>
    /// Image URL list.
    /// </summary>
    [JsonPropertyName("imageUrls")]
    public required List<string> ImageUrls { get; set; }

    /// <summary>
    /// Whether to stream the response via SSE.
    /// </summary>
    [JsonPropertyName("stream")]
    public bool Stream { get; set; }

    /// <summary>
    /// Custom prompt. Ignored when <see cref="TemplateIds"/> is present.
    /// </summary>
    [JsonPropertyName("customPrompt")]
    public string? CustomPrompt { get; set; }

    /// <summary>
    /// Template ids. Takes precedence over <see cref="CustomPrompt"/> when both are set.
    /// </summary>
    [JsonPropertyName("templateIds")]
    public List<long>? TemplateIds { get; set; }
}

/// <summary>
/// Response body for AnalyzeImage / GeneralAnalyzeImage.
/// </summary>
public class CcaiAnalyzeImageResponse
{
    /// <summary>
    /// Request id.
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether the request succeeded.
    /// </summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>
    /// Analysis result text.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Finish reason. For streaming, null while generating; typically "stop" when done.
    /// </summary>
    [JsonPropertyName("finishReason")]
    public string? FinishReason { get; set; }

    /// <summary>
    /// Input token count.
    /// </summary>
    [JsonPropertyName("inputTokens")]
    public long? InputTokens { get; set; }

    /// <summary>
    /// Output token count.
    /// </summary>
    [JsonPropertyName("outputTokens")]
    public long? OutputTokens { get; set; }

    /// <summary>
    /// Total token count.
    /// </summary>
    [JsonPropertyName("totalTokens")]
    public long? TotalTokens { get; set; }
}
