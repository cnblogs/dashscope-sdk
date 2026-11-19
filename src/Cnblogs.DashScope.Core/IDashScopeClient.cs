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
    /// Create an image-to-image synthesis task, shared by all models on this endpoint:
    /// image translation (qwen-mt-image, async), function-based editing (wanx2.1-imageedit)
    /// and instruction-based editing (e.g. wan2.5-i2i-preview).
    /// Query the result with <see cref="GetImage2ImageSynthesisTaskAsync"/>.
    /// </summary>
    /// <param name="input">The input of the image-to-image synthesis task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>> CreateImage2ImageSynthesisTaskAsync(
        ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Translate an image synchronously, only supported by qwen-mt-image-2.0.
    /// The call blocks until the translated image is ready.
    /// </summary>
    /// <param name="input">The input of the translation.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The translation result containing the translated image url.</returns>
    Task<ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>> GetImage2ImageSynthesisAsync(
        ModelRequest<Image2ImageSynthesisInput> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a video synthesis task, shared by all models on the video synthesis endpoint:
    /// HappyHorse, wan2.7 and wan3.0 (text-to-video, image-to-video, reference-to-video and
    /// video editing), legacy wan2.1-2.6, wanx2.1-vace-plus, video-style-transform
    /// (video style transform) and PixVerse models (text-to-video, image-to-video by first
    /// frame or keyframes, reference-to-video, lipsync, motion control and upscale).
    /// Query the result with <see cref="GetVideoSynthesisTaskAsync"/>.
    /// </summary>
    /// <param name="input">The input of the video synthesis task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>> CreateVideoSynthesisTaskAsync(
        ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query the status of a video synthesis task (also covers keyframe video synthesis tasks).
    /// </summary>
    /// <param name="taskId">The id of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The task status.</returns>
    Task<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>> GetVideoSynthesisTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create an image2video synthesis task (wan2.2-kf2v / wanx2.1-kf2v-plus image-to-video by
    /// first and last frames, wan2.2-animate-move image-to-motion, wan2.2-animate-mix subject
    /// replacement, wan2.2-s2v digital human and videoretalk lip sync).
    /// Query the result with <see cref="GetImage2VideoSynthesisTaskAsync"/>.
    /// </summary>
    /// <param name="input">The input of the image2video synthesis task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>> CreateImage2VideoSynthesisTaskAsync(
        ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query the status of an image2video synthesis task.
    /// </summary>
    /// <param name="taskId">The id of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The task status.</returns>
    Task<DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>> GetImage2VideoSynthesisTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Detect synchronously whether an image is suitable for wan2.2-s2v digital human generation
    /// (model wan2.2-s2v-detect). The call blocks until the detection is done and is billed
    /// per image even when the detection does not pass.
    /// </summary>
    /// <param name="input">The input of the detection.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The detection result.</returns>
    Task<ModelResponse<FaceDetectOutput, ImageSynthesisUsage>> GetFaceDetectAsync(
        ModelRequest<FaceDetectInput> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query the status of an image-to-image synthesis task.
    /// </summary>
    /// <param name="taskId">The id of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The task status.</returns>
    Task<DashScopeTask<Image2ImageSynthesisOutput, ImageSynthesisUsage>> GetImage2ImageSynthesisTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query for task status of image synthesis task.
    /// </summary>
    /// <param name="taskId">The task id.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The task status.</returns>
    Task<DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>> GetImageSynthesisTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get image generation task detail.
    /// </summary>
    /// <param name="taskId">The task id to query.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>>
        GetImageGenerationTaskAsync(
            string taskId,
            CancellationToken cancellationToken = default);

    /// <summary>
    /// Query background generation task status.
    /// </summary>
    /// <param name="taskId">The task id to query.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<DashScopeTask<BackgroundGenerationOutput, BackgroundGenerationUsage>>
        GetBackgroundGenerationTaskAsync(
            string taskId,
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
    /// Create an image generation task, shared by the models on this endpoint:
    /// wan2.6+ text-to-image models (multimodal messages) and the legacy wanx style
    /// repaint model (image url plus style index).
    /// Query the result with <see cref="GetImageGenerationTaskAsync"/>.
    /// </summary>
    /// <param name="input">The input of task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<ImageGenerationOutput, MultimodalTokenUsage>> CreateImageGenerationTaskAsync(
        ModelRequest<ImageGenerationInput, IMultimodalParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a background image generation task.
    /// </summary>
    /// <param name="input">The input of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>>
        CreateBackgroundGenerationTaskAsync(
            ModelRequest<BackgroundGenerationInput, IBackgroundGenerationParameters> input,
            CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a batch job.
    /// </summary>
    /// <param name="input">The batch job creation request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The created <see cref="DashScopeBatch"/> job.</returns>
    Task<DashScopeBatch> OpenAiCompatibleCreateBatchAsync(
        DashScopeCreateBatchRequest input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a batch job with ID.
    /// </summary>
    /// <param name="id">The ID of the batch.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns>The batch.</returns>
    Task<DashScopeBatch> OpenAiCompatibleGetBatchAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// List batches with query filters.
    /// </summary>
    /// <param name="after">Batch ID used for paging.</param>
    /// <param name="limit">Page size, range from 1 to 100. Defaults to 20.</param>
    /// <param name="dsName">Search by <see cref="DashScopeBatchMetadata.DsName"/> with keyword.</param>
    /// <param name="inputFileIds">Filter with specific input file ids, use ',' as separator if there were multiple.</param>
    /// <param name="status">Filter with status, use ',' as separator if there were multiple.</param>
    /// <param name="createAfter">Filter batches created after given time, format: <c>yyyyMMddHHmmss</c>.</param>
    /// <param name="createBefore">Filter batches created before given time, format: <c>yyyyMMddHHmmss</c>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns>The result batch list.</returns>
    Task<DashScopeBatchList> OpenAiCompatibleListBatchesAsync(
        string? after = null,
        int? limit = null,
        string? dsName = null,
        string? inputFileIds = null,
        string? status = null,
        string? createAfter = null,
        string? createBefore = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancel batch by id.
    /// </summary>
    /// <param name="batchId">The ID of the batch.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    Task<DashScopeBatch> OpenAiCompatibleCancelBatchAsync(
        string batchId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// OpenAI compatible upload api, for model to reference.
    /// </summary>
    /// <param name="file">File data.</param>
    /// <param name="filename">Name of the file.</param>
    /// <param name="purpose">Purpose of the file, use "file-extract" to allow model access the file. Use "batch" for uploading batch operations .jsonl file.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<DashScopeFile> OpenAiCompatibleUploadFileAsync(
        Stream file,
        string filename,
        string purpose = "file-extract",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get DashScope file by id.
    /// </summary>
    /// <param name="id">ID of the file.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    /// <exception cref="DashScopeException">Throws when file not exists, Status will be 404 in this case.</exception>
    Task<DashScopeFile> OpenAiCompatibleGetFileAsync(DashScopeFileId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Download DashScope file content by id.
    /// </summary>
    /// <param name="id">ID of the file.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    Task<Stream> OpenAiCompatibleGetFileContentAsync(DashScopeFileId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// List DashScope files.
    /// </summary>
    /// <param name="after">Fetch items after given file id.</param>
    /// <param name="limit">Limit item count per page.</param>
    /// <param name="createAfter">Filter files that created after given time. e.g. 20250306123000</param>
    /// <param name="createBefore">Filter files that created before given time. e.g. 20250306123000</param>
    /// <param name="purpose">Filter files with given purpose. e.g. file-extract or batch.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<DashScopeOpenAiCompatibleFileList> OpenAiCompatibleListFilesAsync(
        string? after = null,
        int? limit = null,
        string? createAfter = null,
        string? createBefore = null,
        string? purpose = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete DashScope file.
    /// </summary>
    /// <param name="id">The id of the file to delete.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    /// <exception cref="DashScopeException">Throws when file not exists, Status would be 404.</exception>
    Task<DashScopeDeleteFileResult> OpenAiCompatibleDeleteFileAsync(
        DashScopeFileId id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Start a speech synthesizer session. Related model: cosyvoice
    /// </summary>
    /// <param name="modelId">The model to use.</param>
    /// <param name="dataInspection">Data inspection configuration for the connection handshake,
    /// null to follow the client level configuration. See <see cref="DataInspectionOptions"/> for available values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<SpeechSynthesizerSocketSession> CreateSpeechSynthesizerSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Start a real-time speech recognition session. Related model: paraformer-realtime
    /// </summary>
    /// <param name="modelId">The model to use.</param>
    /// <param name="dataInspection">Data inspection configuration for the connection handshake,
    /// null to follow the client level configuration. See <see cref="DataInspectionOptions"/> for available values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<SpeechRecognitionSocketSession> CreateSpeechRecognitionSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a temporary upload grant for <paramref name="modelId"/> to access.
    /// </summary>
    /// <param name="modelId">The name of the model.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    Task<DashScopeTemporaryUploadPolicy?> GetTemporaryUploadPolicyAsync(
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
    Task<string> UploadTemporaryFileAsync(
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
    Task<string> UploadTemporaryFileAsync(
        Stream fileStream,
        string filename,
        DashScopeTemporaryUploadPolicy policy);

    /// <summary>
    /// Upload multiple files.
    /// </summary>
    /// <param name="purpose">Purpose of the file, can be 'file-extract', 'fine-tune', 'batch'.</param>
    /// <param name="files">The files to upload.</param>
    /// <param name="leaveStreamOpen">Do not dispose streams after request sent.</param>
    /// <returns></returns>
    Task<DashScopeFileResponse<DashScopeUploadFileData>> UploadFilesAsync(
        string purpose,
        IEnumerable<DashScopeUploadFileInput> files,
        bool leaveStreamOpen = false);

    /// <summary>
    /// List uploaded files.
    /// </summary>
    /// <param name="pageNo">Page index.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    Task<DashScopeFileResponse<DashScopeListFilesData>> ListFilesAsync(
        int pageNo,
        int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get uploaded file detail.
    /// </summary>
    /// <param name="fileId">The id of the file.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    Task<DashScopeFileResponse<DashScopeFileDetail>> GetFileAsync(
        DashScopeFileId fileId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get uploaded file detail.
    /// </summary>
    /// <param name="fileId">The ID of the file.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    Task<DashScopeFileResponse<DashScopeFileDetail>> GetFileAsync(
        string fileId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete uploaded file.
    /// </summary>
    /// <param name="fileId">The ID of the file.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    Task<DashScopeFileResponse> DeleteFileAsync(
        DashScopeFileId fileId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete uploaded file.
    /// </summary>
    /// <param name="fileId">The ID of the file.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    Task<DashScopeFileResponse> DeleteFileAsync(string fileId, CancellationToken cancellationToken = default);
}
