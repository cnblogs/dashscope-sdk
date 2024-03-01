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

    /// <summary>
    /// Create a batch task of computing embeddings.
    /// </summary>
    /// <param name="input">The texts url.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>>
        BatchGetEmbeddingsAsync(
            ModelRequest<BatchGetEmbeddingsInput, BatchGetEmbeddingsParameters> input,
            CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the task status of given id.
    /// </summary>
    /// <param name="taskId">The task id.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <typeparam name="TOutput">The output type.</typeparam>
    /// <typeparam name="TUsage">The usage type.</typeparam>
    /// <returns></returns>
    Task<DashScopeTask<TOutput, TUsage>> GetTaskAsync<TOutput, TUsage>(
        string taskId,
        CancellationToken cancellationToken = default)
        where TUsage : class;

    /// <summary>
    /// List DashScope tasks.
    /// </summary>
    /// <param name="taskId">Filter task by task id.</param>
    /// <param name="startTime">Start time. If <paramref name="endTime"/> remains null, then <paramref name="endTime"/> will be set to 24hrs after this value.</param>
    /// <param name="endTime">End time. If <paramref name="startTime"/> remains null, then <paramref name="startTime"/> will be set to 24hrs before this value.</param>
    /// <param name="modelName">Filter task by model name.</param>
    /// <param name="status">Filter task by status.</param>
    /// <param name="pageNo">The page index.</param>
    /// <param name="pageSize">Item count per page.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>If start time and end time are both <c>null</c>, then tasks within recent 24hrs will be returned.</remarks>
    Task<DashScopeTaskList> ListTasksAsync(
        string? taskId = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? modelName = null,
        DashScopeTaskStatus? status = null,
        int? pageNo = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancel DashScope task.
    /// </summary>
    /// <param name="taskId">The id of task to be cancelled.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<DashScopeTaskOperationResponse> CancelTaskAsync(string taskId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get tokenization result of input texts.
    /// </summary>
    /// <param name="input">The input texts.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<TokenizationOutput, TokenizationUsage>> TokenizeAsync(
        ModelRequest<TextGenerationInput, TextGenerationParameters> input,
        CancellationToken cancellationToken = default);
}
