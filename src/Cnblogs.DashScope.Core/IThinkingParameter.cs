namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for thinking.
/// </summary>
public interface IThinkingParameter
{
    /// <summary>
    /// Thinking option. Valid for supported models.(e.g. qwen3)
    /// </summary>
    bool? EnableThinking { get; set; }

    /// <summary>
    /// Maximum length of thinking content. Valid for supported models.(e.g. qwen3)
    /// </summary>
    int? ThinkingBudget { get; set; }

    /// <summary>
    /// Make past reasoning content in the chat history visible to the LLM.
    /// </summary>
    bool? PreserveThinking { get; set; }

    /// <summary>
    /// Ignore reasoning content in the chat history when doing multi-turn conversation, the counterpart of <see cref="PreserveThinking"/>. Valid for GLM series models (glm-5.3/5.2/5.1/5).
    /// </summary>
    bool? ClearThinking { get; set; }

    /// <summary>
    /// Controls the reasoning effort for deepseek-v4 series models, "low", "medium", "high" equals to "high", "xhigh" equals to "max".
    /// </summary>
    string? ReasoningEffort { get; set; }
}
