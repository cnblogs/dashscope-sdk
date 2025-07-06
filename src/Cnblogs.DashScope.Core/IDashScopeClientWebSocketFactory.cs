namespace Cnblogs.DashScope.Core;

/// <summary>
/// A factory abstraction for a component that can create DashScopeClientWebSocket instance.
/// </summary>
public interface IDashScopeClientWebSocketFactory
{
    /// <summary>
    /// Creates a new <see cref="DashScopeClientWebSocket"/>.
    /// </summary>
    /// <param name="apiKey">The api key.</param>
    /// <param name="workspaceId">Optional workspace id.</param>
    /// <returns></returns>
    DashScopeClientWebSocket GetClientWebSocket(string apiKey, string? workspaceId = null);
}
