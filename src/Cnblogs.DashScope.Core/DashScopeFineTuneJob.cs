using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a fine-tune job created via <see cref="IDashScopeClient.CreateFineTuneJobAsync"/>,
/// queryable via <see cref="IDashScopeClient.GetFineTuneJobAsync"/>.
/// </summary>
/// <param name="RequestId">The unique id of this request.</param>
/// <param name="Output">The fine-tune job details.</param>
public record DashScopeFineTuneJob(string RequestId, DashScopeFineTuneJobOutput Output);

/// <summary>
/// The details of a fine-tune job.
/// </summary>
public record DashScopeFineTuneJobOutput
{
    /// <summary>
    /// The unique id of the job, used to query it.
    /// </summary>
    public string JobId { get; set; } = string.Empty;

    /// <summary>
    /// The name of the job, usually the same as <see cref="JobId"/>.
    /// </summary>
    public string? JobName { get; set; }

    /// <summary>
    /// The status of the job.
    /// </summary>
    public DashScopeTaskStatus Status { get; set; }

    /// <summary>
    /// The fine-tuned model name, use it as the model of subsequent inference calls.
    /// Present when the job succeeded.
    /// </summary>
    public string? FinetunedOutput { get; set; }

    /// <summary>
    /// The model being fine-tuned.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// The base (pre-trained) model of the job.
    /// </summary>
    public string? BaseModel { get; set; }

    /// <summary>
    /// The training files of the job.
    /// </summary>
    public List<string>? TrainingFileIds { get; set; }

    /// <summary>
    /// The validation files of the job, empty when there is none.
    /// </summary>
    public List<string>? ValidationFileIds { get; set; }

    /// <summary>
    /// The training datasets of the job.
    /// </summary>
    public List<string>? TrainingDatasets { get; set; }

    /// <summary>
    /// The validation datasets of the job.
    /// </summary>
    public List<string>? ValidationDatasets { get; set; }

    /// <summary>
    /// The evaluation datasets of the job.
    /// </summary>
    public List<string>? EvaluationDatasets { get; set; }

    /// <summary>
    /// The message about the training cost, present on job creation.
    /// </summary>
    public string? CostMessage { get; set; }

    /// <summary>
    /// The charge type of the job, e.g. <c>token</c>.
    /// </summary>
    public string? ChargeType { get; set; }

    /// <summary>
    /// The scheduling priority of the job, e.g. <c>L3</c>.
    /// </summary>
    public string? Priority { get; set; }

    /// <summary>
    /// The hyper parameters of the job, usually empty.
    /// </summary>
    public Dictionary<string, object?>? HyperParameters { get; set; }

    /// <summary>
    /// The training type, currently fixed to <c>sft</c>.
    /// </summary>
    public string? TrainingType { get; set; }

    /// <summary>
    /// The creation time of the job.
    /// </summary>
    [JsonConverter(typeof(DashScopeDateTimeConvertor))]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// The end time of the job, present when the job finished (succeeded or failed).
    /// </summary>
    [JsonConverter(typeof(DashScopeDateTimeConvertor))]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// The workspace id of the API key used by the call.
    /// </summary>
    public string? WorkspaceId { get; set; }

    /// <summary>
    /// The user identity (UID) of the API key used by the call.
    /// </summary>
    public string? UserIdentity { get; set; }

    /// <summary>
    /// The UID of the user who last modified the job.
    /// </summary>
    public string? Modifier { get; set; }

    /// <summary>
    /// The UID of the creator of the job.
    /// </summary>
    public string? Creator { get; set; }

    /// <summary>
    /// The logical group of the job, e.g. <c>facechain</c>.
    /// </summary>
    public string? Group { get; set; }

    /// <summary>
    /// The count of training runs, for billing.
    /// </summary>
    public int? Usage { get; set; }

    /// <summary>
    /// The model name of the job, usually the same as <see cref="JobId"/>.
    /// </summary>
    public string? ModelName { get; set; }

    /// <summary>
    /// The max count of fine-tuned models the job can produce.
    /// </summary>
    public int? MaxOutputCnt { get; set; }

    /// <summary>
    /// The count of fine-tuned models already produced, present when the job finished.
    /// </summary>
    public int? OutputCnt { get; set; }
}
