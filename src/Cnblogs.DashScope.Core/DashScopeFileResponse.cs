namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represent a file api response.
/// </summary>
/// <param name="RequestId">Unique ID of the request.</param>
public record DashScopeFileResponse(string RequestId);

/// <summary>
/// Represent a file api response.
/// </summary>
/// <param name="RequestId">Unique ID of the request.</param>
/// <param name="Data">Data of the response.</param>
/// <typeparam name="TData">Type of the data.</typeparam>
public record DashScopeFileResponse<TData>(string RequestId, TData Data) : DashScopeFileResponse(RequestId);
