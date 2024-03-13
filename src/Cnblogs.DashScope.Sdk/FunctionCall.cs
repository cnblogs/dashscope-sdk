namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents a call to function.
/// </summary>
/// <param name="Name">Name of the function to call.</param>
/// <param name="Arguments">Arguments of this call, usually a json string.</param>
public record FunctionCall(string Name, string? Arguments);
