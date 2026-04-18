namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents metadata for a DashScope batch job.
/// </summary>
public class DashScopeBatchMetadata
{
    /// <summary>
    /// The name of the batch job. Must be within 100 characters.
    /// </summary>
    public string? DsName { get; set; }

    /// <summary>
    /// The description of the batch job. Must be within 200 characters.
    /// </summary>
    public string? DsDescription { get; set; }

    /// <summary>
    /// The callback URL that will be invoked when the batch job is completed.
    /// </summary>
    public string? DsBatchFinishCallback { get; set; }
}
