using Cnblogs.DashScope.AI;
using Cnblogs.DashScope.Core;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.AI;

/// <summary>
/// Provides extension methods for working with <see cref="IDashScopeClient"/>s.</summary>
public static class DashScopeClientExtensions
{
    /// <summary>Gets an <see cref="IChatClient"/> for use with this <see cref="IDashScopeClient"/>.</summary>
    /// <param name="dashScopeClient">The client.</param>
    /// <param name="modelId">The model.</param>
    /// <returns>An <see cref="IChatClient"/> that can be used to converse via the <see cref="IDashScopeClient"/>.</returns>
    public static IChatClient AsChatClient(this IDashScopeClient dashScopeClient, string modelId)
        => new DashScopeChatClient(dashScopeClient, modelId);

    /// <summary>Gets an <see cref="IEmbeddingGenerator{String, Single}"/> for use with this <see cref="IDashScopeClient"/>.</summary>
    /// <param name="dashScopeClient">The client.</param>
    /// <param name="modelId">The model to use.</param>
    /// <param name="dimensions">The number of dimensions to generate in each embedding.</param>
    /// <returns>An <see cref="IEmbeddingGenerator{String, Embedding}"/> that can be used to generate embeddings via the <see cref="IDashScopeClient"/>.</returns>
    public static IEmbeddingGenerator<string, Embedding<float>> AsEmbeddingGenerator(
        this IDashScopeClient dashScopeClient,
        string modelId,
        int? dimensions = null)
        => new DashScopeTextEmbeddingGenerator(dashScopeClient, modelId, dimensions);
}
