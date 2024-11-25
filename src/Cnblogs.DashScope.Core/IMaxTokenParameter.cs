namespace Cnblogs.DashScope.Core;

/// <summary>
/// Marks parameter supports setting maximum number of output tokens.
/// </summary>
public interface IMaxTokenParameter
{
    /// <summary>
    /// The maximum number of tokens the model can generate.
    /// </summary>
    /// <remarks>
    /// Default and maximum number of tokens is 1500(qwen-turbo) or 2000(qwen-max, qwen-max-1201, qwen-max-longcontext, qwen-plus).
    /// </remarks>
    public int? MaxTokens { get; }
}
