using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Options for DashScope client.
/// </summary>
public class DashScopeOptions
{
    /// <summary>
    /// The api key used to access DashScope api
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Base address for DashScope HTTP API.
    /// </summary>
    public string BaseAddress { get; set; } = DashScopeDefaults.DashScopeHttpApiBaseAddress;

    /// <summary>
    /// Base address for DashScope websocket API.
    /// </summary>
    public string BaseWebsocketAddress { get; set; } = DashScopeDefaults.DashScopeWebsocketApiBaseAddress;

    /// <summary>
    /// Default workspace Id.
    /// </summary>
    public string? WorkspaceId { get; set; }

    /// <summary>
    /// Default socket pool size.
    /// </summary>
    public int SocketPoolSize { get; set; } = 10;
}
