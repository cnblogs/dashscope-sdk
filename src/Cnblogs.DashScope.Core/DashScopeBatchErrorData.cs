namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a single error entry in <see cref="DashScopeBatchErrorList"/>.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Message">The error message.</param>
/// <param name="Param">The parameter name that caused the validation error.</param>
/// <param name="Line">The line number in the input file where the error occurred.</param>
public record DashScopeBatchErrorData(string Code, string Message, string? Param, int? Line);
