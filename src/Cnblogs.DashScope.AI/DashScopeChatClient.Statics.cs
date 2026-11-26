using Cnblogs.DashScope.AI.Internals;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.AI;

public sealed partial class DashScopeChatClient
{
    private static readonly TextGenerationParameters
        DefaultTextGenerationParameter = new() { ResultFormat = "message" };

    /// <summary>
    /// Model prefixes that support <c>response_format</c> of type <c>json_schema</c>, see
    /// https://docs.bailian.console.aliyun.com/zh/model-studio/qwen-structured-output for the full list.
    /// </summary>
    private static readonly string[] JsonSchemaModelPrefixes =
    [
        "qwen3.7-flash", "qwen3.7-plus", "qwen3.7-max", "qwen3.8-flash", "qwen3.8-max"
    ];

    /// <summary>
    /// Model prefixes that are served by the multimodal generation endpoint, the other models are
    /// served by the text generation endpoint, and calling the wrong one fails with 400 "url error".
    /// Add a model here only after verifying it against the real service, the endpoint does not
    /// follow the model name, e.g. qwen3.7-max is a text model while qwen3.7-max-2026-06-08 and
    /// qwen3.8-max are multimodal ones. This is the fallback used when a model was not looked up in
    /// the model list of the service: with <see cref="DashScopeChatClientOptions.AutoSyncModelList"/>
    /// on, the endpoint follows what the service reports about the model (the "VU" /
    /// "Multimodal-Omni" capabilities of a model hosted by Bailian) instead, except for the models
    /// the service reports wrongly (see <see cref="DashScopeModelCache"/>). The models supplied
    /// directly by third parties (e.g. xiaomi/mimo-*, ZHIPU/GLM-*) are served by the text
    /// generation endpoint even when they accept images.
    /// </summary>
    private static readonly string[] MultimodalModelPrefixes =
    [
        "qwen3.5", "qwen3.6", "qwen3.7-plus", "qwen3.7-flash", "qwen3.7-max-2026-06-08", "qwen3.8", "qwen4",
        "qwen-vl", "qwen3-vl", "qwen3-omni", "gui-plus",
        "deepseek-v4.1", "kimi-k2.5", "kimi-k2.6", "kimi-k2.7", "kimi-k3"
    ];

    /// <summary>
    /// Model prefixes that read <c>max_completion_tokens</c>, which limits the reasoning output and
    /// the answer together, see
    /// https://docs.bailian.console.aliyun.com/zh/model-studio/qwen-api-via-dashscope for the list
    /// of the official documentation. That list is not accurate for every model: the ones supplied
    /// directly by third parties (e.g. ZHIPU/GLM-5.3, MiniMax/MiniMax-M2.5) ignore the parameter
    /// even when the documentation names them. The models outside this whitelist silently ignore
    /// the parameter and only read <c>max_tokens</c>, nothing in the model list API
    /// (GET /api/v1/models) tells the two kinds apart, so verify a new model with a tiny limit
    /// (the response ends with finish_reason "length") before adding it. The version aliases
    /// (qwen-plus, qwen-flash) are left out on purpose, they may point to a model of either kind.
    /// </summary>
    private static readonly string[] MaxCompletionTokenModelPrefixes =
    [
        "qwen3.5", "qwen3.6", "qwen3.7", "qwen3.8", "qwen4", "qwen-mt", "tongyi-xiaomi-analysis",
        "deepseek-v4-pro", "deepseek-v4.1", "deepseek-v4-flash-0731", "glm-5", "MiniMax-M2.5",
        "kimi-k2.5", "kimi-k2.6", "kimi-k2.7", "kimi-k3"
    ];
}
