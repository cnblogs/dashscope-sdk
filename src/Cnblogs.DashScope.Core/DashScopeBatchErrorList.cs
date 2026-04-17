namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents the error list for a <see cref="DashScopeBatch"/> job.
/// </summary>
/// <param name="Object">The object type, fixed to <c>list</c>.</param>
/// <param name="Data">The list of error details.</param>
public record DashScopeBatchErrorList(string Object, List<DashScopeBatchErrorData>? Data);
