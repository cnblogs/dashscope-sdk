namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Default implementation for <see cref="IDashScopeClientWebSocketFactory"/>.
    /// </summary>
    public class DashScopeClientWebSocketFactory : IDashScopeClientWebSocketFactory
    {
        /// <inheritdoc />
        public DashScopeClientWebSocket GetClientWebSocket(string apiKey, string? workspaceId = null)
        {
            return new DashScopeClientWebSocket(apiKey, workspaceId);
        }
    }
}
