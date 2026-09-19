namespace Cnblogs.DashScope.Core;

/// <summary>
/// Client for LingQue CCAI Conversation Analysis AIO (ContactCenterAI / 2024-06-03).
/// </summary>
public interface IContactCenterAiClient
{
    /// <summary>
    /// Analyzes a conversation by task types (summary, fields, service inspection, etc.).
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<AnalyzeConversationResponse> AnalyzeConversationAsync(
        string workspaceId,
        string appId,
        AnalyzeConversationRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Streams AnalyzeConversation results via SSE. Set <see cref="AnalyzeConversationRequest.Stream"/> to true.
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>SSE chunks.</returns>
    IAsyncEnumerable<AnalyzeConversationResponse> AnalyzeConversationStreamAsync(
        string workspaceId,
        string appId,
        AnalyzeConversationRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Invokes a CCAI application by template ids.
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Completion response.</returns>
    Task<CcaiRunCompletionResponse> RunCompletionAsync(
        string workspaceId,
        string appId,
        CcaiRunCompletionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Streams RunCompletion results via SSE.
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>SSE chunks.</returns>
    IAsyncEnumerable<CcaiRunCompletionResponse> RunCompletionStreamAsync(
        string workspaceId,
        string appId,
        CcaiRunCompletionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Invokes a CCAI application with native prompt messages (RunCompletionMessage).
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Message-protocol request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Completion response.</returns>
    Task<CcaiRunCompletionResponse> RunCompletionMessageAsync(
        string workspaceId,
        string appId,
        CcaiRunCompletionMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Streams RunCompletionMessage results via SSE.
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Message-protocol request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>SSE chunks.</returns>
    IAsyncEnumerable<CcaiRunCompletionResponse> RunCompletionMessageStreamAsync(
        string workspaceId,
        string appId,
        CcaiRunCompletionMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Analyzes images (e.g. watermark detection).
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<CcaiAnalyzeImageResponse> AnalyzeImageAsync(
        string workspaceId,
        string appId,
        CcaiAnalyzeImageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Streams AnalyzeImage results via SSE.
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<CcaiAnalyzeImageResponse> AnalyzeImageStreamAsync(
        string workspaceId,
        string appId,
        CcaiAnalyzeImageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Runs general image analysis with custom prompt or template ids.
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<CcaiAnalyzeImageResponse> GeneralAnalyzeImageAsync(
        string workspaceId,
        string appId,
        CcaiGeneralAnalyzeImageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Streams GeneralAnalyzeImage results via SSE.
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<CcaiAnalyzeImageResponse> GeneralAnalyzeImageStreamAsync(
        string workspaceId,
        string appId,
        CcaiGeneralAnalyzeImageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an offline analysis task.
    /// </summary>
    /// <param name="workspaceId">Workspace id.</param>
    /// <param name="appId">Application id.</param>
    /// <param name="request">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created task info.</returns>
    Task<CcaiCreateTaskResponse> CreateTaskAsync(
        string workspaceId,
        string appId,
        CcaiCreateTaskRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets offline task result by task id.
    /// </summary>
    /// <param name="taskId">Task id.</param>
    /// <param name="requiredFieldList">Optional fields to include, e.g. asr_result.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<CcaiGetTaskResultResponse> GetTaskResultAsync(
        string taskId,
        IEnumerable<string>? requiredFieldList = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a hot-word vocabulary for LingQue CCAI speech transcription.
    /// </summary>
    /// <param name="request">Create request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created vocabulary id.</returns>
    Task<CcaiCreateVocabResponse> CreateVocabAsync(
        CcaiCreateVocabRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing hot-word vocabulary.
    /// </summary>
    /// <param name="request">Update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<CcaiVocabMutationResponse> UpdateVocabAsync(
        CcaiUpdateVocabRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists hot-word vocabularies in a workspace.
    /// </summary>
    /// <param name="request">List request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<CcaiListVocabResponse> ListVocabAsync(
        CcaiListVocabRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a hot-word vocabulary.
    /// </summary>
    /// <param name="request">Delete request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<CcaiVocabMutationResponse> DeleteVocabAsync(
        CcaiDeleteVocabRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a hot-word vocabulary by id.
    /// </summary>
    /// <param name="request">Get request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<CcaiGetVocabResponse> GetVocabAsync(
        CcaiGetVocabRequest request,
        CancellationToken cancellationToken = default);
}
