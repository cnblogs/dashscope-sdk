namespace Cnblogs.DashScope.Core;

/// <summary>
/// Options for DashScope client.
/// </summary>
public class DashScopeOptions
{
    /// <summary>
    /// The section name of the option.
    /// </summary>
    public const string DashScope = nameof(DashScope);

    /// <summary>
    /// The api key used to access DashScope api
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Base address for DashScope HTTP API.
    /// </summary>
    public string? BaseAddress { get; set; }

    /// <summary>
    /// Base address for DashScope websocket API.
    /// </summary>
    public string? WebsocketBaseAddress { get; set; }

    /// <summary>
    /// Default workspace ID.
    /// </summary>
    public string? WorkspaceId { get; set; }

    /// <summary>
    /// The region of your workspace.
    /// </summary>
    public string? RegionId { get; set; }

    /// <summary>
    /// Default socket pool size.
    /// </summary>
    public int SocketPoolSize { get; set; } = 32;

    /// <summary>
    /// Maximum upload speed in byte per second.
    /// </summary>
    public int MaximumUploadSpeed { get; set; } = -1;

    /// <summary>
    /// Select current base address for HTTP requests based on the configuration.
    /// </summary>
    /// <returns></returns>
    public string SelectBaseHttpAddress() => string.IsNullOrWhiteSpace(BaseAddress)
        ? DashScopeDefaults.HttpBaseAddress(WorkspaceId, RegionId)
        : BaseAddress;

    /// <summary>
    /// Select current base address for WebSocket requests based on the configuration.
    /// </summary>
    /// <returns></returns>
    public string SelectBaseWssAddress() => string.IsNullOrWhiteSpace(WebsocketBaseAddress)
        ? DashScopeDefaults.WebsocketBaseAddress(WorkspaceId, RegionId)
        : WebsocketBaseAddress;
}
