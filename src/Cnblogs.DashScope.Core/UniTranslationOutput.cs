using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of an omni-modal translation (model qwen-mt-uni). The synchronous call
/// <see cref="IDashScopeClient.GetUniTranslationAsync"/> always reports a succeeded translation,
/// a request that fails throws a <see cref="DashScopeException"/> instead. The task members are
/// only set on the asynchronous tasks created with
/// <see cref="IDashScopeClient.CreateUniTranslationTaskAsync"/> and queried with
/// <see cref="IDashScopeClient.GetUniTranslationTaskAsync"/>. Such a task ends with the status
/// Succeeded even when the translation failed, read <see cref="Success"/> and
/// <see cref="Message"/> to tell them apart.
/// </summary>
/// <param name="Success">Whether the translation succeeded. Unset while the task is pending
/// or running.</param>
/// <param name="Code">The result code: 200 when the translation succeeded, 400 when the input
/// is rejected (e.g. the file cannot be downloaded), 500 for a server error.</param>
/// <param name="Message">"Success" when the translation succeeded, the reason of the failure
/// otherwise.</param>
/// <param name="RequestId">The id of the translation, which is the id of the asynchronous
/// task.</param>
/// <param name="Data">The translation result. Only present when the translation succeeded.</param>
/// <param name="TaskId">The unique id of the task. Asynchronous tasks only.</param>
/// <param name="TaskStatus">The status of the task. Asynchronous tasks only.</param>
/// <param name="SubmitTime">Submit time of the task.</param>
/// <param name="ScheduledTime">Scheduled(Started) time of the task.</param>
/// <param name="EndTime">End time of the task, unset while the task is running.</param>
public record UniTranslationOutput(
    [property: JsonPropertyName("Success")] bool? Success = null,
    [property: JsonPropertyName("Code")] int? Code = null,
    [property: JsonPropertyName("Message")] string? Message = null,
    [property: JsonPropertyName("RequestId")] string? RequestId = null,
    [property: JsonPropertyName("Data")] UniTranslationData? Data = null,
    string? TaskId = null,
    DashScopeTaskStatus? TaskStatus = null,
    [property: JsonConverter(typeof(DashScopeDateTimeConvertor))] DateTime? SubmitTime = null,
    [property: JsonConverter(typeof(DashScopeDateTimeConvertor))] DateTime? ScheduledTime = null,
    [property: JsonConverter(typeof(DashScopeDateTimeConvertor))] DateTime? EndTime = null);
