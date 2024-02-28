using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Core implementations for <see cref="IDashScopeClient"/>, should only been created by DI container.
/// </summary>
public class DashScopeClientCore : IDashScopeClient
{
    private static readonly JsonSerializerOptions SerializationOptions =
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        };

    private readonly HttpClient _httpClient;

    /// <summary>
    /// For DI container to inject pre-configured httpclient.
    /// </summary>
    /// <param name="httpClient">Pre-configured httpclient.</param>
    public DashScopeClientCore(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetTextCompletionAsync(
        ModelRequest<TextGenerationInput, TextGenerationParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.TextGeneration, input);
        return (await SendAsync<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>(
            request,
            cancellationToken))!;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetTextCompletionStreamAsync(
        ModelRequest<TextGenerationInput, TextGenerationParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildSseRequest(HttpMethod.Post, ApiLinks.TextGeneration, input);
        return StreamAsync<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>(request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetMultimodalGenerationAsync(
        ModelRequest<MultimodalInput, MultimodalParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.MultimodalGeneration, input);
        return (await SendAsync<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>(request, cancellationToken))!;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetMultimodalGenerationStreamAsync(
        ModelRequest<MultimodalInput, MultimodalParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildSseRequest(HttpMethod.Post, ApiLinks.MultimodalGeneration, input);
        return StreamAsync<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>(request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> GetEmbeddingsAsync(
        ModelRequest<TextEmbeddingInput, TextEmbeddingParameters> input,
        CancellationToken cancellationToken = default)
    {
        var request = BuildRequest(HttpMethod.Post, ApiLinks.TextEmbedding, input);
        return (await SendAsync<ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>>(
            request,
            cancellationToken))!;
    }

    private static HttpRequestMessage BuildSseRequest<TPayload>(HttpMethod method, string url, TPayload payload)
    {
        return BuildRequest(method, url, payload, true);
    }

    private static HttpRequestMessage BuildRequest<TPayload>(
        HttpMethod method,
        string url,
        TPayload payload,
        bool sse = false)
    {
        var message = new HttpRequestMessage(method, url)
        {
            Content = JsonContent.Create(payload, options: SerializationOptions)
        };

        if (sse)
        {
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
        }

        return message;
    }

    private async Task<TResponse?> SendAsync<TResponse>(HttpRequestMessage message, CancellationToken cancellationToken)
        where TResponse : class
    {
        var response = await GetSuccessResponseAsync(
            message,
            HttpCompletionOption.ResponseContentRead,
            cancellationToken);
        return await response.Content.ReadFromJsonAsync<TResponse>(SerializationOptions, cancellationToken);
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
        while (!reader.EndOfStream)
        {
            if (cancellationToken.IsCancellationRequested)
                throw new TaskCanceledException();

            var line = await reader.ReadLineAsync(cancellationToken);
            if (line != null && line.StartsWith("data:"))
            {
                var data = line["data:".Length..];
                if (data.StartsWith("{\"code\":"))
                {
                    var error = JsonSerializer.Deserialize<DashScopeError>(data, SerializationOptions)!;
                    throw new DashScopeException(
                        message.RequestUri?.ToString(),
                        (int)response.StatusCode,
                        error,
                        error.Message);
                }

                yield return JsonSerializer.Deserialize<TResponse>(data, SerializationOptions)!;
            }
        }
    }

    private async Task<HttpResponseMessage> GetSuccessResponseAsync(
        HttpRequestMessage message,
        HttpCompletionOption completeOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response;
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
            error = await response.Content.ReadFromJsonAsync<DashScopeError>(SerializationOptions, cancellationToken);
        }
        catch (Exception)
        {
            // ignore
        }

        var errorMessage = error?.Message ?? await response.Content.ReadAsStringAsync(cancellationToken);
        throw new DashScopeException(message.RequestUri?.ToString(), (int)response.StatusCode, error, errorMessage);
    }
}
