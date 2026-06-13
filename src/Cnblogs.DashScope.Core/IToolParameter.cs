namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for tool calls behavior.
/// </summary>
public interface IToolParameter
{
    /// <summary>
    /// Streaming tool calls that with object or array type parameters.
    /// </summary>
    bool? ToolStream { get; set; }
}
