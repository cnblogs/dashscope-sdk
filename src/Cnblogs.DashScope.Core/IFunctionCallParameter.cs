namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for function calling.
/// </summary>
public interface IFunctionCallParameter
{
    /// <summary>
    /// Available tools for model to call.
    /// </summary>
    IEnumerable<ToolDefinition>? Tools { get; set; }

    /// <summary>
    /// Behavior when choosing tools.
    /// </summary>
    ToolChoice? ToolChoice { get; set; }

    /// <summary>
    /// Whether to enable parallel tool calling
    /// </summary>
    bool? ParallelToolCalls { get; set; }
}
