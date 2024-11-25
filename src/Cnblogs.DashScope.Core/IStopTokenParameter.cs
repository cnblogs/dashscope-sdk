namespace Cnblogs.DashScope.Core;

/// <summary>
/// Marks parameter supports setting stop tokens.
/// </summary>
public interface IStopTokenParameter
{
    /// <summary>
    /// Stop generation when next token or string is in given range.
    /// </summary>
    public TextGenerationStop? Stop { get; }
}
