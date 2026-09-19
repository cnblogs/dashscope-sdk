using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Offline task creation request for LingQue CCAI Conversation Analysis AIO.
/// </summary>
public class CcaiCreateTaskRequest
{
    /// <summary>
    /// Task type: text or audio.
    /// </summary>
    [JsonPropertyName("taskType")]
    public required string TaskType { get; set; }

    /// <summary>
    /// Model code.
    /// </summary>
    [JsonPropertyName("modelCode")]
    public required string ModelCode { get; set; }

    /// <summary>
    /// Dialogue for text tasks.
    /// </summary>
    [JsonPropertyName("dialogue")]
    public CcaiDialogue? Dialogue { get; set; }

    /// <summary>
    /// Result types. See <see cref="CcaiResultTypes"/>.
    /// </summary>
    [JsonPropertyName("resultTypes")]
    public List<string>? ResultTypes { get; set; }

    /// <summary>
    /// Fields for extraction.
    /// </summary>
    [JsonPropertyName("fields")]
    public List<CcaiField>? Fields { get; set; }

    /// <summary>
    /// Service inspection configuration.
    /// </summary>
    [JsonPropertyName("serviceInspection")]
    public CcaiServiceInspection? ServiceInspection { get; set; }

    /// <summary>
    /// Template ids. Takes precedence over resultTypes when both are present.
    /// </summary>
    [JsonPropertyName("templateIds")]
    public List<string>? TemplateIds { get; set; }

    /// <summary>
    /// Audio transcription options for audio tasks.
    /// </summary>
    [JsonPropertyName("transcription")]
    public CcaiTranscription? Transcription { get; set; }

    /// <summary>
    /// Category tags.
    /// </summary>
    [JsonPropertyName("categoryTags")]
    public List<CcaiCategoryTag>? CategoryTags { get; set; }

    /// <summary>
    /// Custom prompt.
    /// </summary>
    [JsonPropertyName("customPrompt")]
    public string? CustomPrompt { get; set; }

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

    /// <summary>
    /// Callback URL when the task finishes.
    /// </summary>
    [JsonPropertyName("callBackUrl")]
    public string? CallBackUrl { get; set; }
}

/// <summary>
/// Audio transcription options for offline tasks.
/// </summary>
public class CcaiTranscription
{
    /// <summary>
    /// File name.
    /// </summary>
    [JsonPropertyName("fileName")]
    public required string FileName { get; set; }

    /// <summary>
    /// Voice file URL.
    /// </summary>
    [JsonPropertyName("voiceFileUrl")]
    public required string VoiceFileUrl { get; set; }

    /// <summary>
    /// Auto split speaker for mono audio. 0 = auto, 1 = disabled.
    /// </summary>
    [JsonPropertyName("autoSplit")]
    public int? AutoSplit { get; set; }

    /// <summary>
    /// Client channel for dual-track audio.
    /// </summary>
    [JsonPropertyName("clientChannel")]
    public int? ClientChannel { get; set; }

    /// <summary>
    /// Service channel for dual-track audio.
    /// </summary>
    [JsonPropertyName("serviceChannel")]
    public int? ServiceChannel { get; set; }

    /// <summary>
    /// Keywords used to identify the agent role on mono audio.
    /// </summary>
    [JsonPropertyName("serviceChannelKeywords")]
    public List<string>? ServiceChannelKeywords { get; set; }

    /// <summary>
    /// ASR model code.
    /// </summary>
    [JsonPropertyName("asrModelCode")]
    public string? AsrModelCode { get; set; }

    /// <summary>
    /// Priority level.
    /// </summary>
    [JsonPropertyName("level")]
    public string? Level { get; set; }

    /// <summary>
    /// Hot-word vocabulary id.
    /// </summary>
    [JsonPropertyName("vocabularyId")]
    public string? VocabularyId { get; set; }

    /// <summary>
    /// Whether to auto identify roles.
    /// </summary>
    [JsonPropertyName("roleIdentification")]
    public bool? RoleIdentification { get; set; }

    /// <summary>
    /// Language hint.
    /// </summary>
    [JsonPropertyName("languageHints")]
    public string? LanguageHints { get; set; }
}

/// <summary>
/// Response for CreateTask.
/// </summary>
public class CcaiCreateTaskResponse
{
    /// <summary>
    /// Task payload.
    /// </summary>
    [JsonPropertyName("data")]
    public CcaiCreateTaskData? Data { get; set; }

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
/// CreateTask data payload.
/// </summary>
public class CcaiCreateTaskData
{
    /// <summary>
    /// Created task id.
    /// </summary>
    [JsonPropertyName("taskId")]
    public string? TaskId { get; set; }
}
