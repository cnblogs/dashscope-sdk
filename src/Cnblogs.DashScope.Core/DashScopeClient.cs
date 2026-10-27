using System.Net.Http.Headers;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The DashScopeClient for direct usage
/// </summary>
public class DashScopeClient : DashScopeClientCore
{
    private static readonly Dictionary<string, HttpClient> ClientPools = new();
    private static readonly Dictionary<string, DashScopeClientWebSocketPool> SocketPools = new();

    /// <summary>
    /// Creates a DashScopeClient for further api call.
    /// </summary>
    /// <param name="apiKey">The DashScope api key.</param>
    /// <param name="timeout">The timeout for internal http client, defaults to 2 minute.</param>
    /// <param name="baseAddress">The base address for DashScope api call.</param>
    /// <param name="baseWebsocketAddress">The base address for DashScope websocket api call.</param>
    /// <param name="workspaceId">The workspace id.</param>
    /// <param name="region">The region of workspace.</param>
    /// <param name="socketPoolSize">Maximum size of socket pool.</param>
    /// <remarks>
    ///     The underlying httpclient is cached by constructor parameter list.
    ///     Client created with same parameter value will share same underlying <see cref="HttpClient"/> instance.
    /// </remarks>
    public DashScopeClient(
        string apiKey,
        TimeSpan? timeout = null,
        string? baseAddress = null,
        string? baseWebsocketAddress = null,
        string? workspaceId = null,
        DashScopeRegion? region = null,
        int socketPoolSize = 32)
        : base(
            GetConfiguredClient(
                apiKey,
                timeout,
                baseAddress,
                workspaceId,
                region?.ToRegionId()),
            GetConfiguredSocketPool(
                apiKey,
                baseWebsocketAddress,
                socketPoolSize,
                workspaceId,
                region?.ToRegionId()))
    {
    }

    private static DashScopeClientWebSocketPool GetConfiguredSocketPool(
        string apiKey,
        string? baseAddress,
        int socketPoolSize,
        string? workspaceId,
        string? regionId)
    {
        var key = GetCacheKey();

        var pool = SocketPools.GetValueOrDefault(key);
        if (pool is null)
        {
            pool = new DashScopeClientWebSocketPool(
                new DashScopeClientWebSocketFactory(),
                new DashScopeOptions
                {
                    ApiKey = apiKey,
                    WebsocketBaseAddress = baseAddress,
                    SocketPoolSize = socketPoolSize,
                    WorkspaceId = workspaceId,
                    RegionId = regionId
                });
            SocketPools.Add(key, pool);
        }

        return pool;

        string GetCacheKey() => $"{apiKey}-{socketPoolSize}-{baseAddress}-{workspaceId}-{regionId}";
    }

    private static HttpClient GetConfiguredClient(
        string apiKey,
        TimeSpan? timeout = null,
        string? baseAddress = null,
        string? workspaceId = null,
        string? regionId = null)
    {
        var client = ClientPools.GetValueOrDefault(GetCacheKey());
        if (client is null)
        {
            var address = baseAddress.DefaultIfNullOrWhitespace(()
                => DashScopeDefaults.HttpBaseAddress(workspaceId, regionId));
            client = new HttpClient { BaseAddress = new Uri(address), Timeout = timeout ?? TimeSpan.FromMinutes(2) };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            if (string.IsNullOrWhiteSpace(workspaceId) == false)
            {
                client.DefaultRequestHeaders.Add("X-DashScope-WorkSpace", workspaceId);
            }

            ClientPools.Add(GetCacheKey(), client);
        }

        return client;

        string GetCacheKey() => $"{apiKey}-{timeout?.TotalMilliseconds}-{baseAddress}-{workspaceId}-{regionId}";
    }
}
