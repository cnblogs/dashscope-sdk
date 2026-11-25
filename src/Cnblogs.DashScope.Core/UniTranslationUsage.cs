namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of an omni-modal translation (model qwen-mt-uni), billed by the input tokens.
/// Every member is unset when the server reports no usage: while an asynchronous task is pending
/// or running, and when a finished task failed (the server answers with an empty object then).
/// </summary>
/// <param name="InputTokens">The count of the input tokens.</param>
/// <param name="OutputTokens">The count of the output tokens.</param>
/// <param name="TotalTokens">The count of the input and the output tokens.</param>
/// <param name="InputTokensDetails">The breakdown of <see cref="InputTokens"/> by content type.</param>
/// <param name="OutputTokensDetails">The breakdown of <see cref="OutputTokens"/> by content type.</param>
public record UniTranslationUsage(
    int? InputTokens = null,
    int? OutputTokens = null,
    int? TotalTokens = null,
    UniTranslationInputTokenDetails? InputTokensDetails = null,
    UniTranslationOutputTokenDetails? OutputTokensDetails = null);
