using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.TextEmbedding;

/// <summary>
/// Extensions for text embedding, doc: https://help.aliyun.com/zh/dashscope/developer-reference/text-embedding-api-details
/// </summary>
public static class TextEmbeddingApi
{
    /// <summary>
    /// Get embeddings for given texts.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="texts">The texts to compute embedding from.</param>
    /// <param name="parameters">Optional parameter for embedding.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    public static Task<ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> GetTextEmbeddingsAsync(
        this IDashScopeClient client,
        TextEmbeddingModel model,
        IEnumerable<string> texts,
        TextEmbeddingParameters? parameters,
        CancellationToken cancellationToken = default)
    {
        return client.GetTextEmbeddingsAsync(model.GetModelName(), texts, parameters, cancellationToken);
    }

    /// <summary>
    /// Get embeddings for given texts.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="texts">The texts to compute embedding from.</param>
    /// <param name="parameters">Optional parameter for embedding.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    public static Task<ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> GetTextEmbeddingsAsync(
        this IDashScopeClient client,
        string model,
        IEnumerable<string> texts,
        TextEmbeddingParameters? parameters,
        CancellationToken cancellationToken = default)
    {
        return client.GetEmbeddingsAsync(
            new ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>
            {
                Input = new TextEmbeddingInput { Texts = texts },
                Model = model,
                Parameters = parameters
            },
            cancellationToken);
    }
}
