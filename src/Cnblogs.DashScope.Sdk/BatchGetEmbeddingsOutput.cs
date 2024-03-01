using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The output of batch get embeddings api.
/// </summary>
public record BatchGetEmbeddingsOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The url of embedding file.
    /// </summary>
    public string? Url { get; set; }
}
