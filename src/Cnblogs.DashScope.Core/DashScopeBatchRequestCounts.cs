namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents the request processing statistics for a <see cref="DashScopeBatch"/> job.
/// </summary>
/// <param name="Total">The total number of requests in the batch.</param>
/// <param name="Completed">The number of completed requests.</param>
/// <param name="Failed">The number of failed requests.</param>
public record DashScopeBatchRequestCounts(int Total, int Completed, int Failed);
