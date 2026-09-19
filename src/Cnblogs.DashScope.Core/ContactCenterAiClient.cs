using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Default client for LingQue CCAI Conversation Analysis AIO.
/// </summary>
public sealed class ContactCenterAiClient : IContactCenterAiClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ContactCenterAiOptions _options;
    private readonly bool _disposeHttpClient;

    /// <summary>
    /// Creates a client with the given options. A dedicated <see cref="HttpClient"/> is created.
    /// </summary>
    /// <param name="options">Access key and endpoint options.</param>
    public ContactCenterAiClient(ContactCenterAiOptions options)
        : this(CreateHttpClient(options), options, disposeHttpClient: true)
    {
    }

    /// <summary>
    /// Creates a client with a pre-configured <see cref="HttpClient"/> (for DI).
    /// </summary>
    /// <param name="httpClient">HTTP client. BaseAddress should be https://{endpoint}/.</param>
    /// <param name="options">Access key and endpoint options.</param>
    public ContactCenterAiClient(HttpClient httpClient, ContactCenterAiOptions options)
        : this(httpClient, options, disposeHttpClient: false)
    {
    }

    private ContactCenterAiClient(HttpClient httpClient, ContactCenterAiOptions options, bool disposeHttpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(options);
        if (httpClient.BaseAddress is null)
        {
            throw new ArgumentException("HttpClient.BaseAddress must be set to https://{endpoint}/.", nameof(httpClient));
        }

        if (string.IsNullOrWhiteSpace(options.AccessKeyId))
        {
            throw new ArgumentException("AccessKeyId is required.", nameof(options));
        }

        if (string.IsNullOrWhiteSpace(options.AccessKeySecret))
        {
            throw new ArgumentException("AccessKeySecret is required.", nameof(options));
        }

        if (!string.Equals(httpClient.BaseAddress.Host, options.Endpoint, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException(
                $"HttpClient.BaseAddress host '{httpClient.BaseAddress.Host}' must match options.Endpoint '{options.Endpoint}'.",
                nameof(httpClient));
        }

        _httpClient = httpClient;
        _options = options;
        _disposeHttpClient = disposeHttpClient;
    }

    /// <inheritdoc />
    public Task<AnalyzeConversationResponse> AnalyzeConversationAsync(
        string workspaceId,
        string appId,
        AnalyzeConversationRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        request.Stream = false;

        return SendAsync<AnalyzeConversationRequest, AnalyzeConversationResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.AnalyzeConversation(workspaceId, appId),
            "AnalyzeConversation",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<AnalyzeConversationResponse> AnalyzeConversationStreamAsync(
        string workspaceId,
        string appId,
        AnalyzeConversationRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        request.Stream = true;

        return StreamAsync<AnalyzeConversationRequest, AnalyzeConversationResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.AnalyzeConversation(workspaceId, appId),
            "AnalyzeConversation",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiRunCompletionResponse> RunCompletionAsync(
        string workspaceId,
        string appId,
        CcaiRunCompletionRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        request.Stream = false;

        return SendAsync<CcaiRunCompletionRequest, CcaiRunCompletionResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.RunCompletion(workspaceId, appId),
            "RunCompletion",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<CcaiRunCompletionResponse> RunCompletionStreamAsync(
        string workspaceId,
        string appId,
        CcaiRunCompletionRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        request.Stream = true;

        return StreamAsync<CcaiRunCompletionRequest, CcaiRunCompletionResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.RunCompletion(workspaceId, appId),
            "RunCompletion",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiRunCompletionResponse> RunCompletionMessageAsync(
        string workspaceId,
        string appId,
        CcaiRunCompletionMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Messages);
        request.Stream = false;

        return SendAsync<CcaiRunCompletionMessageRequest, CcaiRunCompletionResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.RunCompletionMessage(workspaceId, appId),
            "RunCompletionMessage",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<CcaiRunCompletionResponse> RunCompletionMessageStreamAsync(
        string workspaceId,
        string appId,
        CcaiRunCompletionMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Messages);
        request.Stream = true;

        return StreamAsync<CcaiRunCompletionMessageRequest, CcaiRunCompletionResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.RunCompletionMessage(workspaceId, appId),
            "RunCompletionMessage",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiAnalyzeImageResponse> AnalyzeImageAsync(
        string workspaceId,
        string appId,
        CcaiAnalyzeImageRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        request.Stream = false;

        return SendAsync<CcaiAnalyzeImageRequest, CcaiAnalyzeImageResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.AnalyzeImage(workspaceId, appId),
            "AnalyzeImage",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<CcaiAnalyzeImageResponse> AnalyzeImageStreamAsync(
        string workspaceId,
        string appId,
        CcaiAnalyzeImageRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        request.Stream = true;

        return StreamAsync<CcaiAnalyzeImageRequest, CcaiAnalyzeImageResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.AnalyzeImage(workspaceId, appId),
            "AnalyzeImage",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiAnalyzeImageResponse> GeneralAnalyzeImageAsync(
        string workspaceId,
        string appId,
        CcaiGeneralAnalyzeImageRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.ImageUrls);
        request.Stream = false;

        return SendAsync<CcaiGeneralAnalyzeImageRequest, CcaiAnalyzeImageResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.GeneralAnalyzeImage(workspaceId, appId),
            "GeneralAnalyzeImage",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<CcaiAnalyzeImageResponse> GeneralAnalyzeImageStreamAsync(
        string workspaceId,
        string appId,
        CcaiGeneralAnalyzeImageRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.ImageUrls);
        request.Stream = true;

        return StreamAsync<CcaiGeneralAnalyzeImageRequest, CcaiAnalyzeImageResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.GeneralAnalyzeImage(workspaceId, appId),
            "GeneralAnalyzeImage",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiCreateTaskResponse> CreateTaskAsync(
        string workspaceId,
        string appId,
        CcaiCreateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(workspaceId, nameof(workspaceId));
        EnsureNotEmpty(appId, nameof(appId));
        ArgumentNullException.ThrowIfNull(request);

        return SendAsync<CcaiCreateTaskRequest, CcaiCreateTaskResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.CreateTask(workspaceId, appId),
            "CreateTask",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiGetTaskResultResponse> GetTaskResultAsync(
        string taskId,
        IEnumerable<string>? requiredFieldList = null,
        CancellationToken cancellationToken = default)
    {
        EnsureNotEmpty(taskId, nameof(taskId));

        var query = new QueryStringBuilder().Add(taskId, "taskId");
        if (requiredFieldList is not null)
        {
            foreach (var field in requiredFieldList)
            {
                query.Add(field, "requiredFieldList");
            }
        }

        var path = ContactCenterAiApiLinks.GetTaskResult + query.Build();
        return SendAsync<object, CcaiGetTaskResultResponse>(
            HttpMethod.Get,
            path,
            "GetTaskResult",
            null,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiCreateVocabResponse> CreateVocabAsync(
        CcaiCreateVocabRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        EnsureNotEmpty(request.WorkspaceId, nameof(request.WorkspaceId));
        EnsureNotEmpty(request.Name, nameof(request.Name));
        ArgumentNullException.ThrowIfNull(request.WordWeightList);

        return SendAsync<CcaiCreateVocabRequest, CcaiCreateVocabResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.CreateVocab,
            "CreateVocab",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiVocabMutationResponse> UpdateVocabAsync(
        CcaiUpdateVocabRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        EnsureNotEmpty(request.WorkspaceId, nameof(request.WorkspaceId));
        EnsureNotEmpty(request.VocabularyId, nameof(request.VocabularyId));

        return SendAsync<CcaiUpdateVocabRequest, CcaiVocabMutationResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.UpdateVocab,
            "UpdateVocab",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiListVocabResponse> ListVocabAsync(
        CcaiListVocabRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        EnsureNotEmpty(request.WorkspaceId, nameof(request.WorkspaceId));

        return SendAsync<CcaiListVocabRequest, CcaiListVocabResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.ListVocab,
            "ListVocab",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiVocabMutationResponse> DeleteVocabAsync(
        CcaiDeleteVocabRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        EnsureNotEmpty(request.WorkspaceId, nameof(request.WorkspaceId));
        EnsureNotEmpty(request.VocabularyId, nameof(request.VocabularyId));

        return SendAsync<CcaiDeleteVocabRequest, CcaiVocabMutationResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.DeleteVocab,
            "DeleteVocab",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<CcaiGetVocabResponse> GetVocabAsync(
        CcaiGetVocabRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        EnsureNotEmpty(request.WorkspaceId, nameof(request.WorkspaceId));
        EnsureNotEmpty(request.VocabularyId, nameof(request.VocabularyId));

        return SendAsync<CcaiGetVocabRequest, CcaiGetVocabResponse>(
            HttpMethod.Post,
            ContactCenterAiApiLinks.GetVocab,
            "GetVocab",
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            _httpClient.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    private async Task<TResponse> SendAsync<TRequest, TResponse>(
        HttpMethod method,
        string pathAndQuery,
        string action,
        TRequest? payload,
        CancellationToken cancellationToken)
        where TRequest : class
        where TResponse : class
    {
        using var message = await BuildSignedRequestAsync(method, pathAndQuery, action, payload, sse: false, cancellationToken);
        using var response = await SendForSuccessAsync(message, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<TResponse>(
            ContactCenterAiDefaults.SerializationOptions,
            cancellationToken);
        return result ?? throw new ContactCenterAiException(
            message.RequestUri?.ToString(),
            (int)response.StatusCode,
            null,
            null,
            null,
            "Empty response body.");
    }

    private async IAsyncEnumerable<TResponse> StreamAsync<TRequest, TResponse>(
        HttpMethod method,
        string pathAndQuery,
        string action,
        TRequest payload,
        [EnumeratorCancellation] CancellationToken cancellationToken)
        where TRequest : class
        where TResponse : class
    {
        using var message = await BuildSignedRequestAsync(method, pathAndQuery, action, payload, sse: true, cancellationToken);
        using var response = await SendForSuccessAsync(
            message,
            cancellationToken,
            HttpCompletionOption.ResponseHeadersRead);
        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        while (await reader.ReadLineAsync() is { } line)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }

            if (line.StartsWith("data:", StringComparison.Ordinal) == false)
            {
                continue;
            }

            var data = line["data:".Length..].Trim();
            if (string.IsNullOrEmpty(data))
            {
                continue;
            }

            yield return JsonSerializer.Deserialize<TResponse>(data, ContactCenterAiDefaults.SerializationOptions)!;
        }
    }

    private async Task<HttpRequestMessage> BuildSignedRequestAsync<TRequest>(
        HttpMethod method,
        string pathAndQuery,
        string action,
        TRequest? payload,
        bool sse,
        CancellationToken cancellationToken)
        where TRequest : class
    {
        byte[]? bodyBytes = null;
        HttpContent? content = null;
        if (payload is not null)
        {
            bodyBytes = JsonSerializer.SerializeToUtf8Bytes(payload, ContactCenterAiDefaults.SerializationOptions);
            content = new ByteArrayContent(bodyBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
        }

        var request = new HttpRequestMessage(method, pathAndQuery) { Content = content };
        if (sse)
        {
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
        }

        if (string.IsNullOrWhiteSpace(_options.SecurityToken) == false)
        {
            request.Headers.TryAddWithoutValidation("x-acs-security-token", _options.SecurityToken);
        }

        // Ensure absolute URI for signing host/path.
        if (request.RequestUri is { IsAbsoluteUri: false })
        {
            request.RequestUri = new Uri(_httpClient.BaseAddress!, request.RequestUri);
        }

        Acs3Signer.Sign(
            request,
            _options.AccessKeyId,
            _options.AccessKeySecret,
            action,
            ContactCenterAiDefaults.ApiVersion,
            _options.Endpoint,
            bodyBytes);

        await Task.CompletedTask;
        cancellationToken.ThrowIfCancellationRequested();
        return request;
    }

    private async Task<HttpResponseMessage> SendForSuccessAsync(
        HttpRequestMessage message,
        CancellationToken cancellationToken,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
    {
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.SendAsync(message, completionOption, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ContactCenterAiException(message.RequestUri?.ToString(), 0, null, null, null, ex.Message);
        }

        if (response.IsSuccessStatusCode)
        {
            return response;
        }

        string? errorCode = null;
        string? errorInfo = null;
        string? requestId = null;
        string? raw = null;
        try
        {
            raw = await response.Content.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(raw);
            if (doc.RootElement.TryGetProperty("errorCode", out var code))
            {
                errorCode = code.GetString();
            }

            if (doc.RootElement.TryGetProperty("Code", out var code2))
            {
                errorCode ??= code2.GetString();
            }

            if (doc.RootElement.TryGetProperty("errorInfo", out var info))
            {
                errorInfo = info.GetString();
            }

            if (doc.RootElement.TryGetProperty("Message", out var msg))
            {
                errorInfo ??= msg.GetString();
            }

            if (doc.RootElement.TryGetProperty("requestId", out var rid))
            {
                requestId = rid.GetString();
            }

            if (doc.RootElement.TryGetProperty("RequestId", out var rid2))
            {
                requestId ??= rid2.GetString();
            }
        }
        catch
        {
            // ignore parse failures
        }

        throw new ContactCenterAiException(
            message.RequestUri?.ToString(),
            (int)response.StatusCode,
            errorCode,
            errorInfo,
            requestId,
            errorInfo ?? raw ?? response.ReasonPhrase ?? "ContactCenterAI request failed.");
    }

    private static HttpClient CreateHttpClient(ContactCenterAiOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.AccessKeyId))
        {
            throw new ArgumentException("AccessKeyId is required.", nameof(options));
        }

        if (string.IsNullOrWhiteSpace(options.AccessKeySecret))
        {
            throw new ArgumentException("AccessKeySecret is required.", nameof(options));
        }

        return new HttpClient
        {
            BaseAddress = new Uri($"https://{options.Endpoint}/"),
            Timeout = options.Timeout
        };
    }

    private static void EnsureNotEmpty(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", paramName);
        }
    }
}
