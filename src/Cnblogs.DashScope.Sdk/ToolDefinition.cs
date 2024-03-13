namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Definition of a tool that model can call during generation.
/// </summary>
/// <param name="Type">The type of this tool. Use <see cref="ToolTypes"/> to get all available options.</param>
/// <param name="Function">Not null when <paramref name="Type"/> is tool.</param>
public record ToolDefinition(string Type, FunctionDefinition? Function);
