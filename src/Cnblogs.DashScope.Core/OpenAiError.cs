namespace Cnblogs.DashScope.Core;

/// <summary>
/// Error from OpenAi compatible API.
/// </summary>
/// <param name="Message">Error message.</param>
/// <param name="Type">Error type.</param>
/// <param name="Param">Problem param name.</param>
/// <param name="Code">Error code.</param>
public record OpenAiError(string Message, string Type, string? Param, string? Code);
