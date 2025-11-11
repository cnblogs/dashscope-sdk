using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.DeepSeek;

/// <summary>
/// Extensions for calling DeepSeek models, see: https://help.aliyun.com/zh/model-studio/developer-reference/deepseek
/// </summary>
[Obsolete("Use generic GetTextStreamCompletionAsync instead")]
public static class DeepSeekTextGenerationApi
{
    private static TextGenerationParameters StreamingParameters { get; } = new() { IncrementalOutput = true };

    /// <summary>
    /// Get text completion from deepseek model.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model name.</param>
    /// <param name="messages">The context messages.</param>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetDeepSeekChatCompletionAsync(DeepSeekLlm.DeepSeekV3, messages);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "deepseek-v3",
    ///                 Input = new TextGenerationInput { Messages = messages },
    ///                 Parameters = StreamingParameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionAsync() instead, check remarks section.")]
    public static async Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
        GetDeepSeekChatCompletionAsync(
            this IDashScopeClient client,
            DeepSeekLlm model,
            IEnumerable<TextChatMessage> messages)
    {
        return await client.GetDeepSeekChatCompletionAsync(model.GetModelName(), messages);
    }

    /// <summary>
    /// Get text completion from deepseek model.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model name.</param>
    /// <param name="messages">The context messages.</param>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetDeepSeekChatCompletionAsync(model, messages);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = model,
    ///                 Input = new TextGenerationInput { Messages = messages },
    ///                 Parameters = StreamingParameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionAsync() instead, check remarks section.")]
    public static async Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
        GetDeepSeekChatCompletionAsync(
            this IDashScopeClient client,
            string model,
            IEnumerable<TextChatMessage> messages)
    {
        return await client.GetTextCompletionAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = model,
                Input = new TextGenerationInput { Messages = messages },
                Parameters = null
            });
    }

    /// <summary>
    /// Get streamed completion from deepseek model.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="model"></param>
    /// <param name="messages"></param>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetDeepSeekChatCompletionStreamAsync(DeepSeekLlm.DeepSeekV3, messages);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionStreamAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = "deepseek-v3",
    ///                 Input = new TextGenerationInput { Messages = messages },
    ///                 Parameters = StreamingParameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionStreamAsync() instead, check remarks section.")]
    public static IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
        GetDeepSeekChatCompletionStreamAsync(
            this IDashScopeClient client,
            DeepSeekLlm model,
            IEnumerable<TextChatMessage> messages)
    {
        return client.GetDeepSeekChatCompletionStreamAsync(model.GetModelName(), messages);
    }

    /// <summary>
    /// Get streamed completion from deepseek model.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="model"></param>
    /// <param name="messages"></param>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetDeepSeekChatCompletionStreamAsync(model, messages);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetTextCompletionStreamAsync(
    ///             new ModelRequest&lt;TextGenerationInput, ITextGenerationParameters&gt;
    ///             {
    ///                 Model = model,
    ///                 Input = new TextGenerationInput { Messages = messages },
    ///                 Parameters = StreamingParameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetTextCompletionStreamAsync() instead, check remarks section.")]
    public static IAsyncEnumerable<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
        GetDeepSeekChatCompletionStreamAsync(
            this IDashScopeClient client,
            string model,
            IEnumerable<TextChatMessage> messages)
    {
        return client.GetTextCompletionStreamAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = model,
                Input = new TextGenerationInput { Messages = messages },
                Parameters = StreamingParameters
            });
    }
}
