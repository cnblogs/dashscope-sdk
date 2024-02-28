namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// DashScope APIs.
/// </summary>
public interface IDashScopeClient
{
    /// <summary>
    /// Return textual completions as configured for a given prompt.
    /// </summary>
    /// <param name="input">The raw input payload for completion.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The completion result.</returns>
    Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetTextCompletionAsync(
        ModelRequest<TextGenerationInput, TextGenerationParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin a chat completions request and get an async enumerable of text output.
    /// </summary>
    /// <param name="input">The raw input payload for completion.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetTextCompletionStreamAsync(
        ModelRequest<TextGenerationInput, TextGenerationParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Return textual completions as configured for a given input.
    /// </summary>
    /// <param name="input">The raw input payload for completion.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    Task<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetMultimodalGenerationAsync(
        ModelRequest<MultimodalInput, MultimodalParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin a multimodal completions request and get an async enumerable of text output.
    /// </summary>
    /// <param name="input">The raw input payload for completion.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    IAsyncEnumerable<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetMultimodalGenerationStreamAsync(
        ModelRequest<MultimodalInput, MultimodalParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Return the computed embeddings for a given prompt.
    /// </summary>
    /// <param name="input">The input texts.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> GetEmbeddingsAsync(
        ModelRequest<TextEmbeddingInput, TextEmbeddingParameters> input,
        CancellationToken cancellationToken = default);
}
