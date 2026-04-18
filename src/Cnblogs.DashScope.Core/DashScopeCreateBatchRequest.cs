namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents the request payload for creating a batch job via <see cref="IDashScopeClient.OpenAiCompatibleCreateBatchAsync"/>.
/// </summary>
public class DashScopeCreateBatchRequest
{
    /// <summary>
    /// The ID of the input file uploaded via <see cref="IDashScopeClient.OpenAiCompatibleUploadFileAsync"/>.
    /// </summary>
    public string InputFileId { get; set; } = string.Empty;

    /// <summary>
    /// The API endpoint for the batch requests. Must match the endpoint configured in the input file.
    /// </summary>
    /// <example>
    /// <para>For embedding: <c>/v1/embeddings</c></para>
    /// <para>For test model: <c>/v1/chat/ds-test</c></para>
    /// <para>Others: <c>/v1/chat/completions</c></para>
    /// </example>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// The timeout for the batch job. Supports <c>h</c> (hours) and <c>d</c> (days). Range: 24h to 336h.
    /// </summary>
    /// <example><c>24h</c>, <c>14d</c></example>
    public string CompletionWindow { get; set; } = string.Empty;

    /// <summary>
    /// Optional metadata for the batch job.
    /// </summary>
    public DashScopeBatchMetadata? Metadata { get; set; }
}
