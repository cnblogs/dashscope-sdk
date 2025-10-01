namespace Cnblogs.DashScope.Core;

/// <summary>
/// DashScope APIs.
/// </summary>
public interface IDashScopeClient
{
    /// <summary>
    /// Get the underlying api gateway address.
    /// </summary>
    Uri? BaseAddress { get; }

    /// <summary>
    /// Make a call to custom application.
    /// </summary>
    /// <param name="applicationId">Name of the application.</param>
    /// <param name="input">The request body.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ApplicationResponse> GetApplicationResponseAsync(
        string applicationId,
        ApplicationRequest input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Make a call to custom application.
    /// </summary>
    /// <param name="applicationId">Name of the application.</param>
    /// <param name="input">The request body.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <typeparam name="TBizParams">Type of the biz_content.</typeparam>
    /// <returns></returns>
    Task<ApplicationResponse> GetApplicationResponseAsync<TBizParams>(
        string applicationId,
        ApplicationRequest<TBizParams> input,
        CancellationToken cancellationToken = default)
        where TBizParams : class;

    /// <summary>
    /// Make a call to custom application.
    /// </summary>
    /// <param name="applicationId">Name of the application.</param>
    /// <param name="input">The request body.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    IAsyncEnumerable<ApplicationResponse> GetApplicationResponseStreamAsync(
        string applicationId,
        ApplicationRequest input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Make a call to custom application.
    /// </summary>
    /// <param name="applicationId">Name of the application.</param>
    /// <param name="input">The request body.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <typeparam name="TBizContent">Type of the biz_content.</typeparam>
    /// <returns></returns>
    IAsyncEnumerable<ApplicationResponse> GetApplicationResponseStreamAsync<TBizContent>(
        string applicationId,
        ApplicationRequest<TBizContent> input,
        CancellationToken cancellationToken = default)
        where TBizContent : class;

    /// <summary>
    /// Return textual completions as configured for a given prompt.
    /// </summary>
    /// <param name="input">The raw input payload for completion.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The completion result.</returns>
    Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetTextCompletionAsync(
        ModelRequest<TextGenerationInput, ITextGenerationParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin a chat completions request and get an async enumerable of text output.
    /// </summary>
    /// <param name="input">The raw input payload for completion.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetTextCompletionStreamAsync(
        ModelRequest<TextGenerationInput, ITextGenerationParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Return textual completions as configured for a given input.
    /// </summary>
    /// <param name="input">The raw input payload for completion.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    Task<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetMultimodalGenerationAsync(
        ModelRequest<MultimodalInput, IMultimodalParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin a multimodal completions request and get an async enumerable of text output.
    /// </summary>
    /// <param name="input">The raw input payload for completion.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    IAsyncEnumerable<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetMultimodalGenerationStreamAsync(
        ModelRequest<MultimodalInput, IMultimodalParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Return the computed embeddings for a given prompt.
    /// </summary>
    /// <param name="input">The input texts.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> GetEmbeddingsAsync(
        ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a batch task of computing embeddings.
    /// </summary>
    /// <param name="input">The texts url.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>>
        BatchGetEmbeddingsAsync(
            ModelRequest<BatchGetEmbeddingsInput, IBatchGetEmbeddingsParameters> input,
            CancellationToken cancellationToken = default);

    /// <summary>
    /// Create an image synthesis task.
    /// </summary>
    /// <param name="input">The input of image synthesis task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>> CreateImageSynthesisTaskAsync(
        ModelRequest<ImageSynthesisInput, IImageSynthesisParameters> input,
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
        ModelRequest<TextGenerationInput, ITextGenerationParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create an image generation task.
    /// </summary>
    /// <param name="input">The input of task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<ImageGenerationOutput, ImageGenerationUsage>> CreateImageGenerationTaskAsync(
        ModelRequest<ImageGenerationInput> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a background image generation task.
    /// </summary>
    /// <param name="input">The input of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public Task<ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>>
        CreateBackgroundGenerationTaskAsync(
            ModelRequest<BackgroundGenerationInput, IBackgroundGenerationParameters> input,
            CancellationToken cancellationToken = default);

    /// <summary>
    /// Upload file for model to reference.
    /// </summary>
    /// <param name="file">File data.</param>
    /// <param name="filename">Name of the file.</param>
    /// <param name="purpose">Purpose of the file, use "file-extract" to allow model access the file.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public Task<DashScopeFile> UploadFileAsync(
        Stream file,
        string filename,
        string purpose = "file-extract",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get DashScope file by id.
    /// </summary>
    /// <param name="id">Id of the file.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    /// <exception cref="DashScopeException">Throws when file not exists, Status will be 404 in this case.</exception>
    public Task<DashScopeFile> GetFileAsync(DashScopeFileId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// List DashScope files.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public Task<DashScopeFileList> ListFilesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete DashScope file.
    /// </summary>
    /// <param name="id">The id of the file to delete.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    /// <exception cref="DashScopeException">Throws when file not exists, Status would be 404.</exception>
    public Task<DashScopeDeleteFileResult> DeleteFileAsync(
        DashScopeFileId id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Start a speech synthesizer session. Related model: cosyvoice
    /// </summary>
    /// <param name="modelId">The model to use.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    public Task<SpeechSynthesizerSocketSession> CreateSpeechSynthesizerSocketSessionAsync(
        string modelId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a temporary upload grant for <see cref="modelId"/> to access.
    /// </summary>
    /// <param name="modelId">The name of the model.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<DashScopeTemporaryUploadPolicy?> GetTemporaryUploadPolicyAsync(
        string modelId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Upload file that granted.
    /// </summary>
    /// <param name="modelId">The model's id that can access the file.</param>
    /// <param name="fileStream">The file data.</param>
    /// <param name="filename">The name of the file.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Oss url of the file.</returns>
    /// <exception cref="DashScopeException">Throws if response code is not 200.</exception>
    public Task<string> UploadTemporaryFileAsync(
        string modelId,
        Stream fileStream,
        string filename,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Upload file that granted.
    /// </summary>
    /// <param name="fileStream">The file data.</param>
    /// <param name="filename"></param>
    /// <param name="policy">The grant info.</param>
    /// <returns></returns>
    /// <exception cref="DashScopeException">Throws if response code is not 200.</exception>
    public Task<string> UploadTemporaryFileAsync(
        Stream fileStream,
        string filename,
        DashScopeTemporaryUploadPolicy policy);
}
