namespace Cnblogs.DashScope.Core;

/// <summary>
/// Marks parameter supports setting maximum number of output tokens.
/// </summary>
public interface IMaxTokenParameter
{
    /// <summary>
    /// The maximum number of tokens of the model answer. Every model reads it, so it is the only
    /// limit that takes effect on the models that do not read <see cref="MaxCompletionTokens"/>.
    /// Whether the reasoning output counts towards it depends on the model: most thinking models
    /// (e.g. qwen3.x-plus / qwen3.x-flash) count the answer only, the reasoning output is limited
    /// by <c>thinking_budget</c> there; qwen4-max, deepseek-v4.x and glm-5.3 count the reasoning
    /// output and the answer together.
    /// </summary>
    int? MaxTokens { get; set; }

    /// <summary>
    /// The maximum number of tokens the model can generate, include reasoning output. Only part of
    /// the models read it: the qwen3.5 and later series (plus / flash from qwen3.5, max from
    /// qwen3.7), qwen-mt, kimi-k2.5 and the later kimi models, glm-5.x, MiniMax-M2.5,
    /// deepseek-v4-pro and the later deepseek models, and only when the model is hosted by Bailian:
    /// the models supplied directly by third parties (e.g. ZHIPU/GLM-5.3, MiniMax/MiniMax-M2.5)
    /// ignore it. The other models (e.g. qwen-flash, qwen-max, qwen3-vl-plus, gui-plus) silently
    /// ignore it as well, see
    /// https://docs.bailian.console.aliyun.com/zh/model-studio/qwen-api-via-dashscope and use
    /// <see cref="MaxTokens"/> with them.
    /// </summary>
    int? MaxCompletionTokens { get; set; }
}
