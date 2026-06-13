namespace Cnblogs.DashScope.Core;

/// <summary>
/// Marks parameter supports setting maximum number of output tokens.
/// </summary>
public interface IMaxTokenParameter
{
    /// <summary>
    /// The maximum number of tokens the model can generate.
    /// </summary>
    [Obsolete("Use MaxCompletionTokens instead")]
    int? MaxTokens { get; set; }

    /// <summary>
    /// The maximum number of tokens the model can generate, include reasoning output.
    /// </summary>
    int? MaxCompletionTokens { get; set; }
}
