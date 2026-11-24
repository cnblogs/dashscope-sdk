using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Core implementations for <see cref="IDashScopeClient"/>, should only been created by DI container.
/// </summary>
public class DashScopeClientCore : IDashScopeClient
{
    private readonly HttpClient _httpClient;
    private readonly DashScopeClientWebSocketPool _socketPool;

    /// <summary>
    /// For DI container to inject pre-configured httpclient.
    /// </summary>
    /// <param name="httpClient">Pre-configured httpclient.</param>
    /// <param name="pool">Websocket pool.</param>
    public DashScopeClientCore(HttpClient httpClient, DashScopeClientWebSocketPool pool)
    {
        _httpClient = httpClient;
        _socketPool = pool;
    }

    /// <inheritdoc />
    public Uri? BaseAddress => _httpClient.BaseAddress;

    /// <summary>
    /// Maximum upload speed in byte/s, -1 means no limit.
    /// </summary>
    public int MaximumUploadSpeed { get; set; } = -1;

    /// <summary>
    /// Default content inspection configuration for all requests, can be overridden
    /// or disabled by request level configuration. Defaults to no inspection.
    /// </summary>
    public DataInspectionOptions? DataInspection { get; set; }

    /// <inheritdoc />
    public Task<ApplicationResponse> GetApplicationResponseAsync(
        string applicationId,
        ApplicationRequest input,
        CancellationToken cancellationToken = default)
    {
        return GetApplicationResponseAsync<Dictionary<string, object?>>(applicationId, input, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ApplicationResponse> GetApplicationResponseAsync<TBizContent>(
        string applicationId,
        ApplicationRequest<TBizContent> input,
        CancellationToken cancellationToken = default)
        where TBizContent : class
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.Application(applicationId), input);
        return (await SendAsync<ApplicationResponse>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<ApplicationResponse> GetApplicationResponseStreamAsync(
        string applicationId,
        ApplicationRequest input,
        CancellationToken cancellationToken = default)
    {
        return GetApplicationResponseStreamAsync<Dictionary<string, object?>>(applicationId, input, cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<ApplicationResponse> GetApplicationResponseStreamAsync<TBizContent>(
        string applicationId,
        ApplicationRequest<TBizContent> input,
        CancellationToken cancellationToken = default)
        where TBizContent : class
    {
        var request = BuildSseRequest(HttpMethod.Post, ApiLinks.Application(applicationId), input);
        return StreamAsync<ApplicationResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetTextCompletionAsync(
        ModelRequest<TextGenerationInput, ITextGenerationParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.TextGeneration, input);
        return (await SendAsync<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>(
            request,
            cancellationToken))!;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetTextCompletionStreamAsync(
        ModelRequest<TextGenerationInput, ITextGenerationParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildSseRequest(HttpMethod.Post, ApiLinks.TextGeneration, input);
        return StreamAsync<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>(request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetMultimodalGenerationAsync(
        ModelRequest<MultimodalInput, IMultimodalParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.MultimodalGeneration, input);
        return (await SendAsync<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetMultimodalGenerationStreamAsync(
        ModelRequest<MultimodalInput, IMultimodalParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildSseRequest(HttpMethod.Post, ApiLinks.MultimodalGeneration, input);
        return StreamAsync<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>(request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>> CreateSpeechTranscriptionTaskAsync(
        ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.AsrTranscription, input, isTask: true);
        return (await SendAsync<ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>(
            request,
            cancellationToken))!;
    }

    /// <inheritdoc />
    public Task<DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>> GetSpeechTranscriptionTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default)
    {
        return GetTaskAsync<SpeechTranscriptionOutput, SpeechTranscriptionUsage>(taskId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>> GetSpeechSynthesisAsync(
        ModelRequest<SpeechSynthesisInput> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.SpeechSynthesizer, input);
        return (await SendAsync<ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>>(
            request,
            cancellationToken))!;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>> GetSpeechSynthesisStreamAsync(
        ModelRequest<SpeechSynthesisInput> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildSseRequest(HttpMethod.Post, ApiLinks.SpeechSynthesizer, input);
        return StreamAsync<ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>>(request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>> CustomizeVoiceAsync(
        ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.VoiceCustomization, input);
        return (await SendAsync<ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>>(
            request,
            cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>>
        CustomizeVocabularyAsync(
            ModelRequest<VocabularyCustomizationInput> input,
            CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.VocabularyCustomization, input);
        return (await SendAsync<ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>>(
            request,
            cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> GetEmbeddingsAsync(
        ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.TextEmbedding, input);
        return (await SendAsync<ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>>(
            request,
            cancellationToken))!;
    }

    /// <inheritdoc />
    public async
        Task<ModelResponse<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>> BatchGetEmbeddingsAsync(
            ModelRequest<BatchGetEmbeddingsInput, IBatchGetEmbeddingsParameters> input,
            CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.TextEmbedding, input, isTask: true);
        return (await SendAsync<ModelResponse<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>>(
            request,
            cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<TextRerankOutput, TextRerankUsage>> RerankDocumentsAsync(
        ModelRequest<TextRerankInput, TextRerankParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.TextRerank, input);
        return (await SendAsync<ModelResponse<TextRerankOutput, TextRerankUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>> CreateImageSynthesisTaskAsync(
        ModelRequest<ImageSynthesisInput, IImageSynthesisParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.Text2ImageSynthesis, input, isTask: true);
        return (await SendAsync<ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>> CreateImage2ImageSynthesisTaskAsync(
        ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.Image2ImageSynthesis, input, isTask: true);
        return (await SendAsync<ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>> GetImage2ImageSynthesisAsync(
        ModelRequest<Image2ImageSynthesisInput> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.Image2ImageSynthesis, input);
        return (await SendAsync<ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public Task<DashScopeTask<Image2ImageSynthesisOutput, ImageSynthesisUsage>> GetImage2ImageSynthesisTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default)
    {
        return GetTaskAsync<Image2ImageSynthesisOutput, ImageSynthesisUsage>(taskId, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>> GetImageSynthesisTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default)
    {
        return GetTaskAsync<ImageSynthesisOutput, ImageSynthesisUsage>(taskId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>> CreateVideoSynthesisTaskAsync(
        ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.VideoSynthesis, input, isTask: true);
        return (await SendAsync<ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public Task<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>> GetVideoSynthesisTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default)
    {
        return GetTaskAsync<VideoSynthesisOutput, VideoSynthesisUsage>(taskId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>> CreateImage2VideoSynthesisTaskAsync(
        ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.Image2VideoSynthesis, input, isTask: true);
        return (await SendAsync<ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public Task<DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>> GetImage2VideoSynthesisTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default)
    {
        return GetTaskAsync<Image2VideoSynthesisOutput, VideoSynthesisUsage>(taskId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateTripo3DTaskAsync(
        ModelRequest<Tripo3DInput, ITripo3DParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.ThreeDGeneration, input, isTask: true);
        return (await SendAsync<ModelResponse<Tripo3DOutput, Tripo3DUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public Task<DashScopeTask<Tripo3DOutput, Tripo3DUsage>> GetTripo3DTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default)
    {
        return GetTaskAsync<Tripo3DOutput, Tripo3DUsage>(taskId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<FaceDetectOutput, ImageSynthesisUsage>> GetFaceDetectAsync(
        ModelRequest<FaceDetectInput> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.FaceDetect, input);
        return (await SendAsync<ModelResponse<FaceDetectOutput, ImageSynthesisUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<FaceDetectionOutput, ImageSynthesisUsage>> DetectFacesAsync(
        ModelRequest<FaceDetectionInput> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.FaceDetection, input);
        return (await SendAsync<ModelResponse<FaceDetectionOutput, ImageSynthesisUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeFineTuneJob> CreateFineTuneJobAsync(
        DashScopeCreateFineTuneJobRequest input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.FineTunes, input);
        return (await SendAsync<DashScopeFineTuneJob>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeFineTuneJob> GetFineTuneJobAsync(
        string jobId,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Get, $"{ApiLinks.FineTunes}/{jobId}");
        return (await SendAsync<DashScopeFineTuneJob>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public Task<DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>> GetImageGenerationTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default)
    {
        return GetTaskAsync<ImageGenerationOutput, MultimodalTokenUsage>(taskId, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DashScopeTask<BackgroundGenerationOutput, BackgroundGenerationUsage>> GetBackgroundGenerationTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default)
    {
        return GetTaskAsync<BackgroundGenerationOutput, BackgroundGenerationUsage>(taskId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<DashScopeTask<TOutput, TUsage>> GetTaskAsync<TOutput, TUsage>(
        string taskId,
        CancellationToken cancellationToken = default)
        where TUsage : class
    {
        var request = BuildRequest(HttpMethod.Get, $"{ApiLinks.Tasks}{taskId}");
        return (await SendAsync<DashScopeTask<TOutput, TUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeTaskList> ListTasksAsync(
        string? taskId = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? modelName = null,
        DashScopeTaskStatus? status = null,
        int? pageNo = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = new StringBuilder();
        if (string.IsNullOrEmpty(taskId) == false)
        {
            queryString.Append($"task_id={taskId}");
        }

        if (startTime.HasValue)
        {
            queryString.Append($"&start_time={startTime:yyyyMMddHHmmss}");
        }

        if (endTime.HasValue)
        {
            // the doc format YYYYMMDDhhmmss means a 24-hour clock, e.g. 20230420193058 for 19:30:58
            queryString.Append($"&end_time={endTime:yyyyMMddHHmmss}");
        }

        if (string.IsNullOrEmpty(modelName) == false)
        {
            queryString.Append($"&model_name={modelName}");
        }

        if (status.HasValue)
        {
            // the wire format is upper case (PENDING / RUNNING / ...) and the filter is case-sensitive
            queryString.Append($"&status={status.Value.ToString().ToUpperInvariant()}");
        }

        if (pageNo.HasValue)
        {
            queryString.Append($"&page_no={pageNo}");
        }

        if (pageSize.HasValue)
        {
            queryString.Append($"&page_size={pageSize}");
        }

        var request = BuildRequest(HttpMethod.Get, $"{ApiLinks.Tasks}?{queryString}");
        return (await SendAsync<DashScopeTaskList>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeTaskOperationResponse> CancelTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, $"{ApiLinks.Tasks}{taskId}/cancel");
        return (await SendAsync<DashScopeTaskOperationResponse>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<TokenizationOutput, TokenizationUsage>> TokenizeAsync(
        ModelRequest<TextGenerationInput, ITextGenerationParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.Tokenizer, input);
        return (await SendAsync<ModelResponse<TokenizationOutput, TokenizationUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<ImageGenerationOutput, MultimodalTokenUsage>> CreateImageGenerationTaskAsync(
        ModelRequest<ImageGenerationInput, IMultimodalParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.ImageGeneration, input, isTask: true);
        return (await SendAsync<ModelResponse<ImageGenerationOutput, MultimodalTokenUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>>
        CreateBackgroundGenerationTaskAsync(
            ModelRequest<BackgroundGenerationInput, IBackgroundGenerationParameters> input,
            CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.BackgroundGeneration, input, isTask: true);
        return (await SendAsync<ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>>(
            request,
            cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeBatch> OpenAiCompatibleCreateBatchAsync(
        DashScopeCreateBatchRequest input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.BatchesCompatible, input);
        return (await SendCompatibleAsync<DashScopeBatch>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeBatch> OpenAiCompatibleGetBatchAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Get, ApiLinks.BatchesCompatible + $"/{id}");
        return (await SendCompatibleAsync<DashScopeBatch>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeBatchList> OpenAiCompatibleListBatchesAsync(
        string? after = null,
        int? limit = null,
        string? dsName = null,
        string? inputFileIds = null,
        string? status = null,
        string? createAfter = null,
        string? createBefore = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = new QueryStringBuilder()
            .Add(after)
            .Add(limit)
            .Add(status)
            .Add(dsName, "ds_name")
            .Add(createAfter, "create_after")
            .Add(createBefore, "create_before")
            .Add(inputFileIds, "input_file_ids");
        var request = BuildRequest(HttpMethod.Get, ApiLinks.BatchesCompatible + queryString.Build());
        return (await SendCompatibleAsync<DashScopeBatchList>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<OpenAiCompatibleChatCompletion> OpenAiCompatibleGetChatCompletionAsync(
        OpenAiCompatibleChatRequest input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildCompatibleChatRequest(input, stream: false);
        return (await SendCompatibleAsync<OpenAiCompatibleChatCompletion>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<OpenAiCompatibleChatChunk> OpenAiCompatibleGetChatCompletionStreamAsync(
        OpenAiCompatibleChatRequest input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildCompatibleChatRequest(input, stream: true);
        return StreamCompatibleAsync<OpenAiCompatibleChatChunk>(request, cancellationToken);
    }

    /// <summary>
    /// Build the request against the OpenAI compatible chat completions endpoint. The stream
    /// flag lives in the body for this endpoint, so it is written onto the serialized payload
    /// instead of the request type.
    /// </summary>
    private static HttpRequestMessage BuildCompatibleChatRequest(OpenAiCompatibleChatRequest input, bool stream)
    {
        var payload = JsonSerializer.SerializeToNode(input, DashScopeDefaults.SerializationOptions)!;
        payload["stream"] = stream;
        var message = new HttpRequestMessage(HttpMethod.Post, ApiLinks.ChatCompletionsCompatible)
        {
            Content = JsonContent.Create(payload, options: DashScopeDefaults.SerializationOptions)
        };
        if (stream)
        {
            // the OpenAI compatible endpoint switches on the body stream flag, the standard
            // Accept header is all it takes (no X-DashScope-SSE)
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
        }

        return message;
    }

    /// <inheritdoc />
    public async Task<DashScopeBatch> OpenAiCompatibleCancelBatchAsync(
        string batchId,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.BatchesCompatible + $"/{batchId}/cancel");
        return (await SendCompatibleAsync<DashScopeBatch>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeFile> OpenAiCompatibleUploadFileAsync(
        Stream file,
        string filename,
        string purpose = "file-extract",
        CancellationToken cancellationToken = default)
    {
        var form = new MultipartFormDataContent();
        form.Add(new StreamContent(file), "file", filename);
        form.Add(new StringContent(purpose), nameof(purpose));
        var content = new ThrottledContent(form, MaximumUploadSpeed);
        var request = new HttpRequestMessage(HttpMethod.Post, ApiLinks.FilesCompatible) { Content = content };
        return (await SendCompatibleAsync<DashScopeFile>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeFile> OpenAiCompatibleGetFileAsync(
        DashScopeFileId id,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Get, ApiLinks.FilesCompatible + $"/{id}");
        return (await SendCompatibleAsync<DashScopeFile>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public Task<Stream> OpenAiCompatibleGetFileContentAsync(
        DashScopeFileId id,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Get, ApiLinks.FilesCompatible + $"/{id}/content");
        return SendCompatibleStreamAsync(request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<DashScopeOpenAiCompatibleFileList> OpenAiCompatibleListFilesAsync(
        string? after = null,
        int? limit = null,
        string? createAfter = null,
        string? createBefore = null,
        string? purpose = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = new QueryStringBuilder()
            .Add(after)
            .Add(limit)
            .Add(createAfter, "create_after")
            .Add(createBefore, "create_before")
            .Add(purpose);
        var request = BuildRequest(HttpMethod.Get, ApiLinks.FilesCompatible + queryString.Build());
        return (await SendCompatibleAsync<DashScopeOpenAiCompatibleFileList>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeDeleteFileResult> OpenAiCompatibleDeleteFileAsync(
        DashScopeFileId id,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Delete, ApiLinks.FilesCompatible + $"/{id}");
        return (await SendCompatibleAsync<DashScopeDeleteFileResult>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<SpeechSynthesizerSocketSession> CreateSpeechSynthesizerSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketPool.RentSocketAsync(dataInspection, cancellationToken);
        return new SpeechSynthesizerSocketSession(socket, modelId);
    }

    /// <inheritdoc />
    public async Task<TtsRealtimeSocketSession> CreateTtsRealtimeSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketPool.CreateRealtimeSocketAsync(modelId, dataInspection, cancellationToken);
        var session = new TtsRealtimeSocketSession(socket);
        await session.StartAsync(cancellationToken);
        return session;
    }

    /// <inheritdoc />
    public async Task<SpeechRecognitionSocketSession> CreateSpeechRecognitionSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketPool.RentSocketAsync(dataInspection, cancellationToken);
        return new SpeechRecognitionSocketSession(socket, modelId);
    }

    /// <inheritdoc />
    public async Task<AsrRealtimeSocketSession> CreateAsrRealtimeSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketPool.CreateRealtimeSocketAsync(modelId, dataInspection, cancellationToken);
        var session = new AsrRealtimeSocketSession(socket);
        await session.StartAsync(cancellationToken);
        return session;
    }

    /// <inheritdoc />
    public async Task<AudioChatSocketSession> CreateAudioChatSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketPool.CreateRealtimeSocketAsync(modelId, dataInspection, cancellationToken);
        var session = new AudioChatSocketSession(socket);
        await session.StartAsync(cancellationToken);
        return session;
    }

    /// <inheritdoc />
    public async Task<LiveTranslateSocketSession> CreateLiveTranslateSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketPool.CreateRealtimeSocketAsync(modelId, dataInspection, cancellationToken);
        var session = new LiveTranslateSocketSession(socket);
        await session.StartAsync(cancellationToken);
        return session;
    }

    /// <inheritdoc />
    public Task<DashScopeTemporaryUploadPolicy?> GetTemporaryUploadPolicyAsync(
        string modelId,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Get, ApiLinks.Uploads + $"?action=getPolicy&model={modelId}");
        return SendAsync<DashScopeTemporaryUploadPolicy>(request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<string> UploadTemporaryFileAsync(
        string modelId,
        Stream fileStream,
        string filename,
        CancellationToken cancellationToken = default)
    {
        var policy = await GetTemporaryUploadPolicyAsync(modelId, cancellationToken);
        if (policy is null)
        {
            throw new DashScopeException(
                "/api/v1/upload",
                200,
                null,
                "GET /api/v1/upload returns empty response, check your connection");
        }

        return await UploadTemporaryFileAsync(fileStream, filename, policy);
    }

    /// <inheritdoc />
    public async Task<string> UploadTemporaryFileAsync(
        Stream fileStream,
        string filename,
        DashScopeTemporaryUploadPolicy policy)
    {
        var key = $"{policy.Data.UploadDir}/{filename}";
        var form = DashScopeMultipartContent.Create();
        form.Add(GetFormDataStringContent(policy.Data.OssAccessKeyId, "OSSAccessKeyId"));
        form.Add(GetFormDataStringContent(policy.Data.Policy, "policy"));
        form.Add(GetFormDataStringContent(policy.Data.Signature, "Signature"));
        form.Add(GetFormDataStringContent(key, "key"));
        form.Add(GetFormDataStringContent(policy.Data.XOssObjectAcl, "x-oss-object-acl"));
        form.Add(GetFormDataStringContent(policy.Data.XOssForbidOverwrite, "x-oss-forbid-overwrite"));
        var file = new StreamContent(fileStream);
        file.Headers.ContentType = null;
        file.Headers.TryAddWithoutValidation(
            "Content-Disposition",
            $"form-data; name=\"file\"; filename=\"{filename}\"");
        file.Headers.TryAddWithoutValidation("Content-Type", "application/octet-stream");
        form.Add(file);
        var content = new ThrottledContent(form, MaximumUploadSpeed);
        var response = await _httpClient.PostAsync(policy.Data.UploadHost, content);
        if (response.IsSuccessStatusCode)
        {
            return $"oss://{key}";
        }

        throw new DashScopeException(
            policy.Data.UploadHost,
            (int)response.StatusCode,
            null,
            await response.Content.ReadAsStringAsync());
    }

    /// <inheritdoc />
    public async Task<DashScopeFileResponse<DashScopeUploadFileData>> UploadFilesAsync(
        string purpose,
        IEnumerable<DashScopeUploadFileInput> files,
        bool leaveStreamOpen = false)
    {
        var form = DashScopeMultipartContent.Create();
        form.Add(GetFormDataStringContent(purpose, nameof(purpose)));

        List<Stream>? toClose = null;
        foreach (var fileData in files)
        {
            var file = new StreamContent(fileData.FileStream);
            file.Headers.ContentType = null;
            file.Headers.TryAddWithoutValidation(
                "Content-Disposition",
                $"form-data; name=\"file\"; filename=\"{fileData.FileName}\"");
            form.Add(file);
            if (string.IsNullOrWhiteSpace(fileData.Description) == false)
            {
                form.Add(GetFormDataStringContent(fileData.Description, "description"));
            }

            if (!leaveStreamOpen)
            {
                toClose ??= new List<Stream>();
                toClose.Add(fileData.FileStream);
            }
        }

        var content = new ThrottledContent(form, MaximumUploadSpeed);
        var response = await _httpClient.PostAsync(ApiLinks.Files(), content);
        if (toClose?.Count > 0)
        {
            toClose.ForEach(x => x.Dispose());
        }

        return (await response.Content.ReadFromJsonAsync<DashScopeFileResponse<DashScopeUploadFileData>>(
            DashScopeDefaults.SerializationOptions))!;
    }

    /// <inheritdoc />
    public async Task<DashScopeFileResponse<DashScopeListFilesData>> ListFilesAsync(
        int pageNo,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = new QueryStringBuilder().Add(pageNo, "page_no").Add(pageSize, "page_size");
        var request = BuildRequest(HttpMethod.Get, ApiLinks.Files() + query.Build());
        var response = await SendAsync<DashScopeFileResponse<DashScopeListFilesData>>(request, cancellationToken);
        return response!;
    }

    /// <inheritdoc />
    public Task<DashScopeFileResponse<DashScopeFileDetail>> GetFileAsync(
        DashScopeFileId fileId,
        CancellationToken cancellationToken = default)
    {
        return GetFileAsync(fileId.Value, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<DashScopeFileResponse<DashScopeFileDetail>> GetFileAsync(
        string fileId,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Get, ApiLinks.Files(fileId));
        var response = await SendAsync<DashScopeFileResponse<DashScopeFileDetail>>(request, cancellationToken);
        return response!;
    }

    /// <inheritdoc />
    public Task<DashScopeFileResponse> DeleteFileAsync(
        DashScopeFileId fileId,
        CancellationToken cancellationToken = default)
    {
        return DeleteFileAsync(fileId.Value, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<DashScopeFileResponse> DeleteFileAsync(
        string fileId,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Delete, ApiLinks.Files(fileId));
        var response = await SendAsync<DashScopeFileResponse>(request, cancellationToken);
        return response!;
    }

    private static StringContent GetFormDataStringContent(string value, string key)
    {
        var content = new StringContent(value);
        content.Headers.ContentType = null;
        content.Headers.TryAddWithoutValidation("Content-Disposition", $"form-data; name=\"{key}\"");
        return content;
    }

    private static HttpRequestMessage BuildSseRequest<TPayload>(HttpMethod method, string url, TPayload payload)
        where TPayload : class
    {
        return BuildRequest(method, url, payload, true);
    }

    private static HttpRequestMessage BuildRequest(HttpMethod method, string url)
    {
        return BuildRequest(method, url, (string?)null);
    }

    private static HttpRequestMessage BuildRequest<TPayload>(
        HttpMethod method,
        string url,
        TPayload? payload = null,
        bool sse = false,
        bool isTask = false)
        where TPayload : class
    {
        var message = new HttpRequestMessage(method, url)
        {
            Content = payload != null
                ? JsonContent.Create(payload, options: DashScopeDefaults.SerializationOptions)
                : null
        };

        if (sse)
        {
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
            // some model requires this header to enable incremental output.
            message.Headers.Add("X-DashScope-SSE", "enable");
        }

        if (isTask)
        {
            message.Headers.Add("X-DashScope-Async", "enable");
        }

        if (payload is IDashScopeWorkspaceConfig config && string.IsNullOrWhiteSpace(config.WorkspaceId) == false)
        {
            message.Headers.Add("X-DashScope-WorkSpace", config.WorkspaceId);
        }

        if (payload is IDashScopeOssUploadConfig ossConfig && ossConfig.EnableOssResolve())
        {
            message.Headers.Add("X-DashScope-OssResourceResolve", "enable");
        }

        if (payload is IDataInspectionConfig inspectionConfig && inspectionConfig.DataInspection is not null)
        {
            // pass the request level decision to the send pipeline, where it takes precedence over client level configuration.
            message.Options.Set(DataInspectionOptions.RequestOptionKey, inspectionConfig.DataInspection);
        }

        return message;
    }

    private void ApplyDataInspection(HttpRequestMessage message)
    {
        const string headerName = "X-DashScope-DataInspection";
        if (message.Options.TryGetValue(DataInspectionOptions.RequestOptionKey, out var requestOptions))
        {
            // request level configuration exists, a null header value means inspection is explicitly disabled.
            if (requestOptions.ToHeaderValue() is { } requestValue)
            {
                message.Headers.Add(headerName, requestValue);
            }
        }
        else if (DataInspection?.ToHeaderValue() is { } clientValue)
        {
            message.Headers.Add(headerName, clientValue);
        }
    }

    private async Task<Stream> SendCompatibleStreamAsync(
        HttpRequestMessage message,
        CancellationToken cancellationToken)
    {
        var response = await GetSuccessResponseAsync<OpenAiErrorResponse>(
            message,
            r => new DashScopeError
            {
                Code = r.Error.Type,
                Message = r.Error.Message,
                RequestId = string.Empty
            },
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        return await response.Content.ReadAsStreamAsync(cancellationToken);
    }

    private async Task<TResponse?> SendCompatibleAsync<TResponse>(
        HttpRequestMessage message,
        CancellationToken cancellationToken)
        where TResponse : class
    {
        var response = await GetSuccessResponseAsync<OpenAiErrorResponse>(
            message,
            r => new DashScopeError
            {
                Code = r.Error.Type,
                Message = r.Error.Message,
                RequestId = string.Empty
            },
            HttpCompletionOption.ResponseContentRead,
            cancellationToken);
        return await response.Content.ReadFromJsonAsync<TResponse>(
            DashScopeDefaults.SerializationOptions,
            cancellationToken);
    }

    private async Task<TResponse?> SendAsync<TResponse>(HttpRequestMessage message, CancellationToken cancellationToken)
        where TResponse : class
    {
        var response = await GetSuccessResponseAsync(
            message,
            HttpCompletionOption.ResponseContentRead,
            cancellationToken);
        return await response.Content.ReadFromJsonAsync<TResponse>(
            DashScopeDefaults.SerializationOptions,
            cancellationToken);
    }

    /// <summary>
    /// Read a Server-Sent-Events stream of the OpenAI compatible endpoints: every data line
    /// carries a json payload, the stream terminates at the [DONE] marker and errors take the
    /// OpenAI error shape (mapped by <see cref="SendCompatibleAsync{TResponse}"/>).
    /// </summary>
    private async IAsyncEnumerable<TResponse> StreamCompatibleAsync<TResponse>(
        HttpRequestMessage message,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var response = await GetSuccessResponseAsync<OpenAiErrorResponse>(
            message,
            r => new DashScopeError
            {
                Code = r.Error.Type,
                Message = r.Error.Message,
                RequestId = string.Empty
            },
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);
        using StreamReader reader = new(await response.Content.ReadAsStreamAsync(cancellationToken), Encoding.UTF8);
        while (await reader.ReadLineAsync() is { } line)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }

            if (line.StartsWith("data:") == false)
            {
                continue;
            }

            var data = line["data:".Length..].Trim();
            if (data == "[DONE]")
            {
                yield break;
            }

            yield return JsonSerializer.Deserialize<TResponse>(data, DashScopeDefaults.SerializationOptions)!;
        }
    }

    private async IAsyncEnumerable<TResponse> StreamAsync<TResponse>(
        HttpRequestMessage message,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var response = await GetSuccessResponseAsync(
            message,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);
        using StreamReader reader = new(await response.Content.ReadAsStreamAsync(cancellationToken), Encoding.UTF8);
        while (await reader.ReadLineAsync() is { } line)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }

            if (line.StartsWith("data:") == false)
            {
                continue;
            }

            var data = line["data:".Length..];
            if (data.StartsWith("{\"code\":"))
            {
                var error =
                    JsonSerializer.Deserialize<DashScopeError>(data, DashScopeDefaults.SerializationOptions)!;
                throw new DashScopeException(
                    message.RequestUri?.ToString(),
                    (int)response.StatusCode,
                    error,
                    error.Message);
            }

            yield return JsonSerializer.Deserialize<TResponse>(data, DashScopeDefaults.SerializationOptions)!;
        }
    }

    private async Task<HttpResponseMessage> GetSuccessResponseAsync(
        HttpRequestMessage message,
        HttpCompletionOption completeOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        return await GetSuccessResponseAsync<DashScopeError>(message, f => f, completeOption, cancellationToken);
    }

    private async Task<HttpResponseMessage> GetSuccessResponseAsync<TError>(
        HttpRequestMessage message,
        Func<TError, DashScopeError> errorMapper,
        HttpCompletionOption completeOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response;
        ApplyDataInspection(message);
        try
        {
            response = await _httpClient.SendAsync(message, completeOption, cancellationToken);
        }
        catch (Exception e)
        {
            throw new DashScopeException(message.RequestUri?.ToString(), 0, null, e.Message);
        }

        if (response.IsSuccessStatusCode)
        {
            return response;
        }

        DashScopeError? error = null;
        try
        {
            var r = await response.Content.ReadFromJsonAsync<TError>(
                DashScopeDefaults.SerializationOptions,
                cancellationToken);
            error = r == null ? null : errorMapper.Invoke(r);
        }
        catch (Exception)
        {
            // ignore
        }

        await ThrowDashScopeExceptionAsync(error, message, response, cancellationToken);
        // will never reach here
        return response;
    }

    [DoesNotReturn]
    private static async Task ThrowDashScopeExceptionAsync(
        DashScopeError? error,
        HttpRequestMessage message,
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        var errorMessage = error?.Message ?? await response.Content.ReadAsStringAsync(cancellationToken);
        throw new DashScopeException(
            message.RequestUri?.ToString(),
            (int)response.StatusCode,
            error,
            errorMessage);
    }
}
