using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.QWenMultimodal;

/// <summary>
/// Extension methods for qwen-vl and qwen-audio: https://help.aliyun.com/zh/dashscope/developer-reference/tongyi-qianwen-vl-plus-api?spm=a2c4g.11186623.0.0.6cd412b09sQtru#8f79b5d0f8ker
/// </summary>
public static class QWenMultimodalGenerationApi
{
    /// <summary>
    /// Request completion with qWen-vl models.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The requesting model's name.</param>
    /// <param name="messages">The messages to complete.</param>
    /// <param name="parameters">The optional configuration for this request.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenMultimodalCompletionAsync(QWenMultimodalModel.QWenVlPlus, messages, parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetMultimodalGenerationAsync(
    ///             new ModelRequest&lt;MultimodalInput, IMultimodalParameters&gt;
    ///             {
    ///                 Model = "qwen-vl-plus",
    ///                 Input = new MultimodalInput { Messages = messages },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetMultimodalGenerationAsync instead")]
    public static Task<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetQWenMultimodalCompletionAsync(
        this IDashScopeClient client,
        QWenMultimodalModel model,
        IEnumerable<MultimodalMessage> messages,
        MultimodalParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        return client.GetQWenMultimodalCompletionAsync(model.GetModelName(), messages, parameters, cancellationToken);
    }

    /// <summary>
    /// Request completion with qWen-vl models.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The requesting model's name.</param>
    /// <param name="messages">The messages to complete.</param>
    /// <param name="parameters">The optional configuration for this request.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenMultimodalCompletionAsync("qwen-vl-plus", messages, parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetMultimodalGenerationAsync(
    ///             new ModelRequest&lt;MultimodalInput, IMultimodalParameters&gt;
    ///             {
    ///                 Model = "qwen-vl-plus",
    ///                 Input = new MultimodalInput { Messages = messages },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetMultimodalGenerationAsync instead")]
    public static Task<ModelResponse<MultimodalOutput, MultimodalTokenUsage>> GetQWenMultimodalCompletionAsync(
        this IDashScopeClient client,
        string model,
        IEnumerable<MultimodalMessage> messages,
        MultimodalParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        return client.GetMultimodalGenerationAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>
            {
                Model = model,
                Input = new MultimodalInput { Messages = messages },
                Parameters = parameters
            },
            cancellationToken);
    }

    /// <summary>
    /// Request completion with qWen-vl models.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The requesting model's name.</param>
    /// <param name="messages">The messages to complete.</param>
    /// <param name="parameters">The optional configuration for this request.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenMultimodalCompletionStreamAsync("qwen-vl-plus", messages, parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetMultimodalGenerationStreamAsync(
    ///             new ModelRequest&lt;MultimodalInput, IMultimodalParameters&gt;
    ///             {
    ///                 Model = "qwen-vl-plus",
    ///                 Input = new MultimodalInput { Messages = messages },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetMultimodalGenerationStreamAsync instead")]
    public static IAsyncEnumerable<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
        GetQWenMultimodalCompletionStreamAsync(
            this IDashScopeClient client,
            QWenMultimodalModel model,
            IEnumerable<MultimodalMessage> messages,
            MultimodalParameters? parameters = null,
            CancellationToken cancellationToken = default)
    {
        return client.GetQWenMultimodalCompletionStreamAsync(
            model.GetModelName(),
            messages,
            parameters,
            cancellationToken);
    }

    /// <summary>
    /// Request completion with qWen-vl models.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The requesting model's name.</param>
    /// <param name="messages">The messages to complete.</param>
    /// <param name="parameters">The optional configuration for this request.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns></returns>
    /// <remarks>
    ///     Migrate from
    ///     <code>
    ///         client.GetQWenMultimodalCompletionStreamAsync("qwen-vl-plus", messages, parameters);
    ///     </code>
    ///     to
    ///     <code>
    ///         client.GetMultimodalGenerationStreamAsync(
    ///             new ModelRequest&lt;MultimodalInput, IMultimodalParameters&gt;
    ///             {
    ///                 Model = "qwen-vl-plus",
    ///                 Input = new MultimodalInput { Messages = messages },
    ///                 Parameters = parameters
    ///             });
    ///     </code>
    /// </remarks>
    [Obsolete("Use GetMultimodalGenerationStreamAsync instead")]
    public static IAsyncEnumerable<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
        GetQWenMultimodalCompletionStreamAsync(
            this IDashScopeClient client,
            string model,
            IEnumerable<MultimodalMessage> messages,
            MultimodalParameters? parameters = null,
            CancellationToken cancellationToken = default)
    {
        return client.GetMultimodalGenerationStreamAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>
            {
                Model = model,
                Input = new MultimodalInput { Messages = messages },
                Parameters = parameters
            },
            cancellationToken);
    }
}
