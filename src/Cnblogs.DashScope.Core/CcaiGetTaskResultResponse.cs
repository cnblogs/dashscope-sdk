using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Response for GetTaskResult.
/// </summary>
public class CcaiGetTaskResultResponse
{
    /// <summary>
    /// Task result payload.
    /// </summary>
    [JsonPropertyName("data")]
    public CcaiTaskResultData? Data { get; set; }

    /// <summary>
    /// Request id.
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether the request succeeded.
    /// </summary>
    [JsonPropertyName("success")]
    public string? Success { get; set; }
}

/// <summary>
/// Offline task result data.
/// </summary>
public class CcaiTaskResultData
{
    /// <summary>
    /// Task id.
    /// </summary>
    [JsonPropertyName("taskId")]
    public string? TaskId { get; set; }

    /// <summary>
    /// Result text.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Error message when the task failed.
    /// </summary>
    [JsonPropertyName("taskErrorMessage")]
    public string? TaskErrorMessage { get; set; }

    /// <summary>
    /// Task status: QUEUE, FINISH, ERROR.
    /// </summary>
    [JsonPropertyName("taskStatus")]
    public string? TaskStatus { get; set; }

    /// <summary>
    /// ASR results for audio tasks.
    /// </summary>
    [JsonPropertyName("asrResult")]
    public List<CcaiAsrResult>? AsrResult { get; set; }

    /// <summary>
    /// Extra JSON payload.
    /// </summary>
    [JsonPropertyName("extra")]
    public string? Extra { get; set; }

    /// <summary>
    /// RAG status.
    /// </summary>
    [JsonPropertyName("ragStatus")]
    public string? RagStatus { get; set; }

    /// <summary>
    /// RAG result.
    /// </summary>
    [JsonPropertyName("ragResult")]
    public string? RagResult { get; set; }

    /// <summary>
    /// RAG error message.
    /// </summary>
    [JsonPropertyName("ragErrorMessage")]
    public string? RagErrorMessage { get; set; }
}

/// <summary>
/// ASR sentence result.
/// </summary>
public class CcaiAsrResult
{
    /// <summary>
    /// Begin offset in milliseconds.
    /// </summary>
    [JsonPropertyName("begin")]
    public int? Begin { get; set; }

    /// <summary>
    /// End offset in milliseconds.
    /// </summary>
    [JsonPropertyName("end")]
    public int? End { get; set; }

    /// <summary>
    /// Emotion energy value.
    /// </summary>
    [JsonPropertyName("emotionValue")]
    public int? EmotionValue { get; set; }

    /// <summary>
    /// Track / role id.
    /// </summary>
    [JsonPropertyName("role")]
    public string? Role { get; set; }

    /// <summary>
    /// Role display name.
    /// </summary>
    [JsonPropertyName("roleName")]
    public string? RoleName { get; set; }

    /// <summary>
    /// Speech rate.
    /// </summary>
    [JsonPropertyName("speechRate")]
    public int? SpeechRate { get; set; }

    /// <summary>
    /// Recognized text.
    /// </summary>
    [JsonPropertyName("words")]
    public string? Words { get; set; }
}
