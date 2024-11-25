using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Specify the behavior for model when tools are applied.
/// </summary>
[JsonConverter(typeof(ToolChoiceJsonConverter))]
public record ToolChoice
{
    /// <summary>
    /// Make sure tool choices can not be initiated directly.
    /// </summary>
    private ToolChoice(string type, ToolChoiceFunction? function = null)
    {
        Type = type;
        Function = function;
    }

    /// <summary>
    /// The type of tool choice.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// The function that model must call.
    /// </summary>
    public ToolChoiceFunction? Function { get; }

    /// <summary>
    /// Model can not call any tools.
    /// </summary>
    public static readonly ToolChoice NoneChoice = new("none");

    /// <summary>
    /// Model can freely pick between generating a message or calling one or more tools.
    /// </summary>
    public static readonly ToolChoice AutoChoice = new("auto");

    /// <summary>
    /// Model must call function with specified name.
    /// </summary>
    /// <param name="functionName">Name of function.</param>
    /// <returns></returns>
    public static ToolChoice FunctionChoice(string functionName)
        => new("function", new ToolChoiceFunction(functionName));
}
