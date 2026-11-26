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
    /// Create an asynchronous speech transcription task for long audio files with
    /// qwen3-asr-flash-filetrans (or Paraformer file transcription models) and poll the result
    /// with <see cref="GetSpeechTranscriptionTaskAsync"/>. The task output carries a
    /// transcription url valid for 24 hours, download the JSON result file in time.
    /// </summary>
    /// <param name="input">The input of the transcription.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The created task with its id and status.</returns>
    Task<ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>> CreateSpeechTranscriptionTaskAsync(
        ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the status / result of a speech transcription task created by
    /// <see cref="CreateSpeechTranscriptionTaskAsync"/>.
    /// </summary>
    /// <param name="taskId">The id of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The task with its status and result.</returns>
    Task<DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>> GetSpeechTranscriptionTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Synthesize speech from text with qwen-audio-tts models (e.g. qwen-audio-3.0-tts-flash)
    /// and get the complete audio as a download url. This API is only served on the workspace
    /// domain, configure <see cref="DashScopeOptions.WorkspaceId"/> to route requests there.
    /// </summary>
    /// <param name="input">The input of the synthesis.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The synthesis result with the audio url.</returns>
    Task<ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>> GetSpeechSynthesisAsync(
        ModelRequest<SpeechSynthesisInput> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin a speech synthesis request of qwen-audio-tts models and get an async enumerable
    /// of sentence events: sentence-begin / sentence-synthesis (base64 audio chunks, append
    /// them in order) / sentence-end frames, ending with a frame carrying the complete audio
    /// url. This API is only served on the workspace domain, configure
    /// <see cref="DashScopeOptions.WorkspaceId"/> to route requests there.
    /// </summary>
    /// <param name="input">The input of the synthesis.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The sentence events and the final audio url.</returns>
    IAsyncEnumerable<ModelResponse<SpeechSynthesisOutput, SpeechSynthesisUsage>> GetSpeechSynthesisStreamAsync(
        ModelRequest<SpeechSynthesisInput> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Clone a voice from a sample audio or design a voice from a text description, and
    /// manage the voices (list / query / update / delete), model <c>voice-enrollment</c>.
    /// The <see cref="VoiceCustomizationInput.Action"/> picks the operation. This API is
    /// only served on the workspace domain, configure <see cref="DashScopeOptions.WorkspaceId"/>
    /// to route requests there. MiniMax voices are cloned via
    /// <see cref="GetMultimodalGenerationAsync"/> with
    /// <see cref="MultimodalInput.Action"/> set to "voice_clone" instead.
    /// </summary>
    /// <param name="input">The input of the customization.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The customization result, which members are present depends on the action.</returns>
    Task<ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>> CustomizeVoiceAsync(
        ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Manage the custom hotword vocabularies of speech recognition (create / list /
    /// query / update / delete), model <c>speech-biasing</c>. The
    /// <see cref="VocabularyCustomizationInput.Action"/> picks the operation. This API is
    /// only served on the workspace domain, configure <see cref="DashScopeOptions.WorkspaceId"/>
    /// to route requests there.
    /// </summary>
    /// <param name="input">The input of the customization.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The customization result, which members are present depends on the action.</returns>
    Task<ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>> CustomizeVocabularyAsync(
        ModelRequest<VocabularyCustomizationInput> input,
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
    /// Compute the embeddings of texts, images and videos in one vector space, e.g. to search
    /// images by a text or to compare the contents across the modalities. Models:
    /// tongyi-embedding-vision-plus, tongyi-embedding-vision-flash and their 2026-03-06 snapshots,
    /// multimodal-embedding-v1, qwen3-vl-embedding and qwen2.5-vl-embedding.
    /// </summary>
    /// <param name="input">The contents to compute the embeddings from.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>One embedding per content, or the vector fused from all the contents, see <see cref="MultimodalEmbeddingInput"/>.</returns>
    Task<ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>> GetMultimodalEmbeddingsAsync(
        ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Rank the candidate documents by their relevance to the query, e.g. to refine the documents
    /// recalled by a vector search. Models: qwen3.7-text-rerank, qwen3-rerank, qwen3-vl-rerank
    /// (also ranks images and videos), gte-rerank-v2.
    /// </summary>
    /// <param name="input">The query and the documents to rank.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The documents ranked by the relevance score descending.</returns>
    Task<ModelResponse<TextRerankOutput, TextRerankUsage>> RerankDocumentsAsync(
        ModelRequest<TextRerankInput, TextRerankParameters> input,
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
    /// Translate texts, documents (pdf, docx, pptx, xlsx, txt, html, md), images (png, jpg) or
    /// audio (mp3, wav) with the omni-modal translation model qwen-mt-uni, e.g. from Chinese to
    /// English. The call blocks until the result is ready, which takes minutes for large
    /// documents: submit them with <see cref="CreateUniTranslationTaskAsync"/> instead.
    /// </summary>
    /// <param name="input">The input of the translation.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The translation result with the translated texts or the url of the translated file.</returns>
    Task<ModelResponse<UniTranslationOutput, UniTranslationUsage>> GetUniTranslationAsync(
        ModelRequest<UniTranslationInput> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create an asynchronous translation task with the omni-modal translation model qwen-mt-uni.
    /// The request is the same as <see cref="GetUniTranslationAsync"/>. Query the result with
    /// <see cref="GetUniTranslationTaskAsync"/>, the url of a translated file is valid for 24 hours.
    /// </summary>
    /// <param name="input">The input of the translation.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The created task with its id and status.</returns>
    Task<ModelResponse<UniTranslationOutput, UniTranslationUsage>> CreateUniTranslationTaskAsync(
        ModelRequest<UniTranslationInput> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the status / result of a translation task created by
    /// <see cref="CreateUniTranslationTaskAsync"/>. Unlike other tasks, a translation that fails
    /// still ends with the status Succeeded: check <see cref="UniTranslationOutput.Success"/> and
    /// read <see cref="UniTranslationOutput.Message"/> when it is false.
    /// </summary>
    /// <param name="taskId">The id of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The task with its status and result.</returns>
    Task<DashScopeTask<UniTranslationOutput, UniTranslationUsage>> GetUniTranslationTaskAsync(
        string taskId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a video synthesis task, shared by all models on the video synthesis endpoint:
    /// HappyHorse, wan2.7 and wan3.0 (text-to-video, image-to-video, reference-to-video and
    /// video editing), legacy wan2.1-2.6, wanx2.1-vace-plus, video-style-transform
    /// (video style transform), PixVerse models (text-to-video, image-to-video by first
    /// frame or keyframes, reference-to-video, lipsync, motion control and upscale),
    /// Kling models (text-to-video, image-to-video by first frame or keyframes,
    /// reference-to-video and video editing), Vidu models (text-to-video, image-to-video,
    /// keyframe-to-video and reference-to-video) and MiniMax-H3.
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
    /// Create a Tripo 3D task, shared by all Tripo models on the 3d-generation endpoint:
    /// Tripo-H3.1 / Tripo-P1.0 (text-to-3d, image-to-3d and multi-image-to-3d),
    /// Tripo-ImageToSplat (Gaussian Splat from an image), post-processing models
    /// (Tripo-Texture, Tripo-Segmentation, Tripo-SmartSegmentation, Tripo-MeshCompletion,
    /// Tripo-Retopology, Tripo-ConvertFormat) and animation models (Tripo-RigCheck,
    /// Tripo-AutoRig, Tripo-AnimationRetarget). Which input and parameter members are
    /// supported depends on the model.
    /// Query the result with <see cref="GetTripo3DTaskAsync"/>.
    /// </summary>
    /// <param name="input">The input of the Tripo 3D task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    Task<ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateTripo3DTaskAsync(
        ModelRequest<Tripo3DInput, ITripo3DParameters> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query the status of a Tripo 3D task created via <see cref="CreateTripo3DTaskAsync"/>.
    /// </summary>
    /// <param name="taskId">The id of the task.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The task status.</returns>
    Task<DashScopeTask<Tripo3DOutput, Tripo3DUsage>> GetTripo3DTaskAsync(
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
    /// Detect synchronously whether images contain a usable face (model facechain-facedetect),
    /// typically used to validate images before FaceChain fine-tuning.
    /// </summary>
    /// <param name="input">The input of the detection.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The detection result of each image, in the same order as the input.</returns>
    Task<ModelResponse<FaceDetectionOutput, ImageSynthesisUsage>> DetectFacesAsync(
        ModelRequest<FaceDetectionInput> input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a fine-tune job (e.g. model facechain-finetune, which trains a personalized
    /// portrait model from 1~10 face images). Poll <see cref="GetFineTuneJobAsync"/> until
    /// the job succeeds and use <see cref="DashScopeFineTuneJobOutput.FinetunedOutput"/>
    /// as the model of subsequent inference calls.
    /// </summary>
    /// <param name="input">The request describing the job.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The created job.</returns>
    Task<DashScopeFineTuneJob> CreateFineTuneJobAsync(
        DashScopeCreateFineTuneJobRequest input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Query a fine-tune job created via <see cref="CreateFineTuneJobAsync"/>.
    /// </summary>
    /// <param name="jobId">The id of the job.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The job details.</returns>
    Task<DashScopeFineTuneJob> GetFineTuneJobAsync(
        string jobId,
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
    /// Create a chat completion via the OpenAI compatible endpoint
    /// (<c>/compatible-mode/v1/chat/completions</c>). Only for models that cannot be called
    /// via the DashScope API (e.g. qwen3-livetranslate-flash, the audio / video file
    /// translation models); when a model supports both styles, prefer the DashScope methods
    /// or the Microsoft.Extensions.AI integration instead.
    /// </summary>
    /// <param name="input">The completion request, stream is always sent as false.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The completion.</returns>
    Task<OpenAiCompatibleChatCompletion> OpenAiCompatibleGetChatCompletionAsync(
        OpenAiCompatibleChatRequest input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a streamed chat completion via the OpenAI compatible endpoint
    /// (<c>/compatible-mode/v1/chat/completions</c>). Only for models that cannot be called
    /// via the DashScope API - some of them (qwen3-livetranslate-flash) only support the
    /// streaming mode; when a model supports both styles, prefer the DashScope methods or
    /// the Microsoft.Extensions.AI integration instead.
    /// </summary>
    /// <param name="input">The completion request, stream is always sent as true.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The completion chunks, terminated by the server [DONE] marker.</returns>
    IAsyncEnumerable<OpenAiCompatibleChatChunk> OpenAiCompatibleGetChatCompletionStreamAsync(
        OpenAiCompatibleChatRequest input,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a decision from the decision model (decision-model, decision-model-preview) via the
    /// System One endpoint (<c>/compatible-mode/v1/systemone</c>). A single forward pass answers
    /// every question of the request with a choice, a yes / no probability or a score, no text is
    /// generated. This API is only served on the workspace domain, configure
    /// <see cref="DashScopeOptions.WorkspaceId"/> to route requests there.
    /// </summary>
    /// <param name="input">The decision request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The decision, the answer of every question keyed by its question id.</returns>
    /// <exception cref="DashScopeException">Throws when the request fails, e.g. a question is
    /// invalid (Status is 400), or when the client is not bound to a workspace (Status is
    /// 404).</exception>
    Task<SystemOneDecision> OpenAiCompatibleGetSystemOneDecisionAsync(
        SystemOneDecisionInput input,
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
    /// Start a speech synthesizer session. Related models: cosyvoice, qwen-audio-3.1-tts,
    /// qwen-audio-3.0-tts and sambert (which uses the one-shot
    /// <see cref="SpeechSynthesizerSocketSession.SynthesizeAsync(SpeechSynthesizerParameters, string, CancellationToken)"/> calling mode)
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
    /// Start a TTS realtime session (qwen3-tts-*-realtime models), the connection is established
    /// and the session.created event is awaited before the session is returned. Configure the
    /// session with <see cref="TtsRealtimeSocketSession.UpdateSessionAsync"/>, append text with
    /// <see cref="TtsRealtimeSocketSession.AppendTextAsync"/> and end it with
    /// <see cref="TtsRealtimeSocketSession.FinishAsync"/>. Unlike the speech synthesizer session,
    /// a realtime session is closed by the server after finish and cannot be reused.
    /// </summary>
    /// <param name="modelId">The model to use, e.g. qwen3-tts-flash-realtime.</param>
    /// <param name="dataInspection">Data inspection configuration for the connection handshake,
    /// null to follow the client level configuration. See <see cref="DataInspectionOptions"/> for available values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<TtsRealtimeSocketSession> CreateTtsRealtimeSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Start a real-time speech recognition session. Related models: paraformer-realtime,
    /// fun-asr-realtime, qwen-audio-3.1-asr-flash-message and qwen-audio-asr-flash-streaming
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
    /// Start an ASR realtime session (qwen3-asr-flash-realtime models), the connection is
    /// established and the session.created event is awaited before the session is returned.
    /// Configure the session (audio format, language, VAD / manual mode) with
    /// <see cref="AsrRealtimeSocketSession.UpdateSessionAsync"/>, append audio with
    /// <see cref="AsrRealtimeSocketSession.AppendAudioAsync(Stream, CancellationToken)"/>, submit
    /// it in the manual mode with <see cref="AsrRealtimeSocketSession.CommitAudioAsync"/> and end
    /// the session with <see cref="AsrRealtimeSocketSession.FinishAsync"/>. Unlike the speech
    /// recognition session, a realtime session is closed by the server after finish and cannot
    /// be reused.
    /// </summary>
    /// <param name="modelId">The model to use, e.g. qwen3-asr-flash-realtime.</param>
    /// <param name="dataInspection">Data inspection configuration for the connection handshake,
    /// null to follow the client level configuration. See <see cref="DataInspectionOptions"/> for available values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<AsrRealtimeSocketSession> CreateAsrRealtimeSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a realtime audio chat session (qwen-audio-3.x-realtime models) for voice
    /// conversations: append the user audio, the server detects the speech (or the client
    /// submits it in the push-to-talk mode), runs the inference and streams back the response
    /// text and audio. The session ends when it is disposed.
    /// </summary>
    /// <param name="modelId">The model to use, e.g. qwen-audio-3.1-realtime-plus.</param>
    /// <param name="dataInspection">Data inspection configuration for the connection handshake,
    /// null to follow the client level configuration. See <see cref="DataInspectionOptions"/> for available values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<AudioChatSocketSession> CreateAudioChatSocketSessionAsync(
        string modelId,
        DataInspectionOptions? dataInspection = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a realtime live translation session (qwen3.5 / qwen3.8-livetranslate-flash-realtime):
    /// append the source audio (and video frames), the server recognizes and translates the
    /// speech and streams back the translation text and audio. Finish the session with
    /// <see cref="LiveTranslateSocketSession.FinishAsync"/> and dispose it afterwards.
    /// </summary>
    /// <param name="modelId">The model to use, e.g. qwen3.5-livetranslate-flash-realtime.</param>
    /// <param name="dataInspection">Data inspection configuration for the connection handshake,
    /// null to follow the client level configuration. See <see cref="DataInspectionOptions"/> for available values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<LiveTranslateSocketSession> CreateLiveTranslateSocketSessionAsync(
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

    /// <summary>
    /// List the models of Bailian via <c>GET /api/v1/models</c>, one page per call. Every model
    /// carries its capabilities (text generation, reasoning, visual understanding, ...), the input
    /// and output modalities, the features, the prices and the token limits, see
    /// https://docs.bailian.console.aliyun.com/zh/model-studio/list-models.
    /// </summary>
    /// <param name="query">The filters and the page to return, every member is optional. When it is
    /// null the first page of 20 models is returned.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns>The page: the total number of matching models, the page index and the models.</returns>
    /// <exception cref="DashScopeException">Throws when the request fails, e.g. a paging parameter
    /// is rejected. The service answers with status 200 and <c>success</c> false in that case, the
    /// detail is in <see cref="DashScopeException.Error"/>.</exception>
    /// <example>
    /// <code>
    /// var page = await client.ListModelsAsync(new ModelListQuery { Model = "qwen3.8-max" });
    /// var model = page.Output.Models[0];
    /// </code>
    /// </example>
    Task<DashScopeModelList> ListModelsAsync(
        ModelListQuery? query = null,
        CancellationToken cancellationToken = default);
}
