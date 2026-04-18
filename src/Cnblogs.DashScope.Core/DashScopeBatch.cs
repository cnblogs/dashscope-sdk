namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a DashScope batch job.
/// </summary>
/// <param name="Id">The unique identifier of the batch job.</param>
/// <param name="Object">The object type, fixed to <c>batch</c>.</param>
/// <param name="Endpoint">The API endpoint that the batch requests are sent to.</param>
/// <param name="Errors">The error list for the batch job.</param>
/// <param name="InputFileId">The ID of the input file.</param>
/// <param name="CompletionWindow">The time window within which the batch job should complete.</param>
/// <param name="Status">The current status of the batch job.</param>
/// <param name="OutputFileId">The ID of the file containing successful results.</param>
/// <param name="ErrorFileId">The ID of the file containing failed results.</param>
/// <param name="CreatedAt">The Unix timestamp when the batch job was created.</param>
/// <param name="InProgressAt">The Unix timestamp when the batch job started processing.</param>
/// <param name="ExpiresAt">The Unix timestamp when the batch job will expire.</param>
/// <param name="FinalizingAt">The Unix timestamp when the batch job began finalizing.</param>
/// <param name="CompletedAt">The Unix timestamp when the batch job was marked as completed.</param>
/// <param name="FailedAt">The Unix timestamp when the batch job was marked as failed.</param>
/// <param name="ExpiredAt">The Unix timestamp when the batch job was marked as expired.</param>
/// <param name="CancellingAt">The Unix timestamp when cancellation was requested.</param>
/// <param name="CancelledAt">The Unix timestamp when the batch job was canceled.</param>
/// <param name="RequestCounts">The request processing statistics for the batch job.</param>
/// <param name="Metadata">The metadata associated with the batch job.</param>
public record DashScopeBatch(
    string Id,
    string Object,
    string Endpoint,
    DashScopeBatchErrorList? Errors,
    string InputFileId,
    string CompletionWindow,
    string Status,
    string? OutputFileId,
    string? ErrorFileId,
    int CreatedAt,
    int? InProgressAt,
    int? ExpiresAt,
    int? FinalizingAt,
    int? CompletedAt,
    int? FailedAt,
    int? ExpiredAt,
    int? CancellingAt,
    int? CancelledAt,
    DashScopeBatchRequestCounts RequestCounts,
    DashScopeBatchMetadata Metadata);
