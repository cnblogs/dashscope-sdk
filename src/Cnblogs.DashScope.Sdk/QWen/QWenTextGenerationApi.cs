using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.QWen;

/// <summary>
/// Extension methods for QWen text generation api, docs: https://help.aliyun.com/zh/dashscope/developer-reference/api-details
/// </summary>
public static class QWenTextGenerationApi
{
    /// <summary>
    /// Begin a completions request and get an async enumerable of response.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/></param>
    /// <param name="model">The model to use.</param>
    /// <param name="messages">The collection of context messages associated with this chat completions request.</param>
    /// <param name="parameters">The optional parameters for this completion request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <exception cref="DashScopeException">Request for generation is failed.</exception>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenChatCompletionAsync("qwen-plus", messages, parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionStreamAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "qwen-plus",
    ///                 Input = new TextGenerationInput { Messages = messages },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionStreamAsync instead")]
    public static IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetQWenChatStreamAsync(
        this IDashScopeClient dashScopeClient,
        QWenLlm model,
        IEnumerable<TextChatMessage> messages,
        TextGenerationParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        // does not allow empty history array
        return dashScopeClient.GetTextCompletionStreamAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = model.GetModelName(),
                Input = new TextGenerationInput { Messages = messages },
                Parameters = parameters
            },
            cancellationToken);
    }

    /// <summary>
    /// Begin a completions request and get an async enumerable of response.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/></param>
    /// <param name="model">The model to use.</param>
    /// <param name="messages">The collection of context messages associated with this chat completions request.</param>
    /// <param name="parameters">The optional parameters for this completion request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <exception cref="DashScopeException">Request for generation is failed.</exception>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenChatCompletionAsync("qwen-plus", messages, parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionStreamAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "qwen-plus",
    ///                 Input = new TextGenerationInput { Messages = messages },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionStreamAsync instead")]
    public static IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetQWenChatStreamAsync(
        this IDashScopeClient dashScopeClient,
        string model,
        IEnumerable<TextChatMessage> messages,
        TextGenerationParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        return dashScopeClient.GetTextCompletionStreamAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = model,
                Input = new TextGenerationInput { Messages = messages },
                Parameters = parameters
            },
            cancellationToken);
    }

    /// <summary>
    /// Request chat completion with QWen models.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="messages">The collection of context messages associated with this chat completions request.</param>
    /// <param name="parameters">The optional parameters for this completion request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <exception cref="DashScopeException">Request for generation is failed.</exception>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenChatCompletionAsync(QWenLlm.QwQ32B, messages, parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "qwq-32b",
    ///                 Input = new TextGenerationInput { Messages = messages },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionAsync instead")]
    public static Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetQWenChatCompletionAsync(
        this IDashScopeClient dashScopeClient,
        QWenLlm model,
        IEnumerable<TextChatMessage> messages,
        TextGenerationParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        return dashScopeClient.GetQWenChatCompletionAsync(
            model.GetModelName(),
            messages,
            parameters,
            cancellationToken);
    }

    /// <summary>
    /// Request chat completion with QWen models.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="messages">The collection of context messages associated with this chat completions request.</param>
    /// <param name="parameters">The optional parameters for this completion request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <exception cref="DashScopeException">Request for generation is failed.</exception>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenChatCompletionAsync("qwen-plus", messages, parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "qwen-plus",
    ///                 Input = new TextGenerationInput { Messages = messages },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionAsync instead")]
    public static Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetQWenChatCompletionAsync(
        this IDashScopeClient dashScopeClient,
        string model,
        IEnumerable<TextChatMessage> messages,
        TextGenerationParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        return dashScopeClient.GetTextCompletionAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = model,
                Input = new TextGenerationInput { Messages = messages },
                Parameters = parameters
            },
            cancellationToken);
    }

    /// <summary>
    /// Begin a completions request and get an async enumerable of response.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="prompt">The prompt to generate completion from.</param>
    /// <param name="parameters">The optional parameters for this completion request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <exception cref="DashScopeException">Request for generation is failed.</exception>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenCompletionStreamAsync(QWenLlm.QwQ32B, "prompt", parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionStreamAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "qwq-32b",
    ///                 Input = new TextGenerationInput { Messages = [TextChatMessage.User("prompt")] },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionStreamAsync instead")]
    public static IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
        GetQWenCompletionStreamAsync(
            this IDashScopeClient dashScopeClient,
            QWenLlm model,
            string prompt,
            TextGenerationParameters? parameters,
            CancellationToken cancellationToken = default)
    {
        return dashScopeClient.GetQWenCompletionStreamAsync(
            model.GetModelName(),
            prompt,
            parameters,
            cancellationToken);
    }

    /// <summary>
    /// Begin a completions request and get an async enumerable of response.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="prompt">The prompt to generate completion from.</param>
    /// <param name="parameters">The optional parameters for this completion request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <exception cref="DashScopeException">Request for generation is failed.</exception>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenCompletionStreamAsync("qwq-32b", "prompt", parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionStreamAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "qwq-32b",
    ///                 Input = new TextGenerationInput { Messages = [TextChatMessage.User("prompt")] },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionStreamAsync instead")]
    public static IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
        GetQWenCompletionStreamAsync(
            this IDashScopeClient dashScopeClient,
            string model,
            string prompt,
            TextGenerationParameters? parameters,
            CancellationToken cancellationToken = default)
    {
        return dashScopeClient.GetTextCompletionStreamAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = model,
                Input = new TextGenerationInput { Prompt = prompt },
                Parameters = parameters
            },
            cancellationToken);
    }

    /// <summary>
    /// Request completion with QWen models.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The QWen model to use.</param>
    /// <param name="prompt">The prompt to generate completion from.</param>
    /// <param name="parameters">The optional parameters for this completion request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <exception cref="DashScopeException">Request for generation is failed.</exception>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenCompletionAsync(QWenLlm.QwQ32B, "prompt", parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "qwq-32b",
    ///                 Input = new TextGenerationInput { Messages = [TextChatMessage.User("prompt")] },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionAsync instead")]
    public static Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetQWenCompletionAsync(
        this IDashScopeClient dashScopeClient,
        QWenLlm model,
        string prompt,
        TextGenerationParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        return dashScopeClient.GetQWenCompletionAsync(model.GetModelName(), prompt, parameters, cancellationToken);
    }

    /// <summary>
    /// Request completion with QWen models.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The QWen model to use.</param>
    /// <param name="prompt">The prompt to generate completion from.</param>
    /// <param name="parameters">The optional parameters for this completion request.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <exception cref="DashScopeException">Request for generation is failed.</exception>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenCompletionAsync("qwq-32b", "prompt", parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "qwq-32b",
    ///                 Input = new TextGenerationInput { Messages = [TextChatMessage.User("prompt")] },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionAsync instead")]
    public static Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetQWenCompletionAsync(
        this IDashScopeClient dashScopeClient,
        string model,
        string prompt,
        TextGenerationParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        return dashScopeClient.GetTextCompletionAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = model,
                Input = new TextGenerationInput { Prompt = prompt },
                Parameters = parameters
            },
            cancellationToken);
    }
}
