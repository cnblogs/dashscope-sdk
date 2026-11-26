namespace Cnblogs.DashScope.Core;

/// <summary>
/// The context window and the token limits of a model. A member is null when the model does not report it.
/// </summary>
/// <param name="ContextWindow">The total length of the context window.</param>
/// <param name="MaxInputTokens">The maximum number of input tokens.</param>
/// <param name="MaxOutputTokens">The maximum number of output tokens.</param>
/// <param name="MaxReasoningTokens">The maximum number of reasoning tokens.</param>
/// <param name="ReasoningMaxInputTokens">The maximum number of input tokens in thinking mode.</param>
/// <param name="ReasoningMaxOutputTokens">The maximum number of output tokens in thinking mode.</param>
public record DashScopeModelInfo(
    int? ContextWindow,
    int? MaxInputTokens,
    int? MaxOutputTokens,
    int? MaxReasoningTokens,
    int? ReasoningMaxInputTokens,
    int? ReasoningMaxOutputTokens);
