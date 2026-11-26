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
    /// <param name="modelId">The model, <see cref="ChatOptions.ModelId"/> overrides it when set.</param>
    /// <param name="useVl">Whether to call the multimodal generation endpoint. Leave it <c>null</c> to
    /// decide by the model of each request: the multimodal models (e.g. qwen3.5 / qwen3.6 series,
    /// qwen3.7-plus, qwen3.8-max, qwen3-vl-plus, gui-plus) go there, the other models go to the
    /// text generation endpoint.</param>
    /// <returns>An <see cref="IChatClient"/> that can be used to converse via the <see cref="IDashScopeClient"/>.</returns>
    public static IChatClient AsChatClient(this IDashScopeClient dashScopeClient, string modelId, bool? useVl = null)
        => new DashScopeChatClient(dashScopeClient, modelId, useVl);

    /// <summary>Gets an <see cref="IChatClient"/> for use with this <see cref="IDashScopeClient"/>.</summary>
    /// <param name="dashScopeClient">The client.</param>
    /// <param name="modelId">The model, <see cref="ChatOptions.ModelId"/> overrides it when set.</param>
    /// <param name="options">The endpoint to call and whether to look the models up in the model
    /// list of the service, see <see cref="DashScopeChatClientOptions"/>.</param>
    /// <returns>An <see cref="IChatClient"/> that can be used to converse via the <see cref="IDashScopeClient"/>.</returns>
    public static IChatClient AsChatClient(
        this IDashScopeClient dashScopeClient,
        string modelId,
        DashScopeChatClientOptions options)
        => new DashScopeChatClient(dashScopeClient, modelId, options);

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
