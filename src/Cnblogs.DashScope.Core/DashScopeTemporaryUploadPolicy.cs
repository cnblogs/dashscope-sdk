namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents one response of get upload policy api call.
/// </summary>
/// <param name="RequestId">Unique id for current request.</param>
/// <param name="Data">The grant data.</param>
public record DashScopeTemporaryUploadPolicy(string RequestId, DashScopeTemporaryUploadPolicyData Data);
