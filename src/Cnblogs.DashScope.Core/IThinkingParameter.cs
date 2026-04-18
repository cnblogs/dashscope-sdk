namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for thinking.
/// </summary>
public interface IThinkingParameter
{
    /// <summary>
    /// Thinking option. Valid for supported models.(e.g. qwen3)
    /// </summary>
    bool? EnableThinking { get; }

    /// <summary>
    /// Maximum length of thinking content. Valid for supported models.(e.g. qwen3)
    /// </summary>
    int? ThinkingBudget { get; set; }

    /// <summary>
    /// Make past reasoning content in the chat history visible to the LLM.
    /// </summary>
    bool? PreserveThinking { get; set; }
}
