using System.Net.Http.Headers;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The DashScopeClient for direct usage
/// </summary>
public class DashScopeClient : DashScopeClientCore
{
    private static readonly Dictionary<string, HttpClient> ClientPools = new();

    /// <summary>
    /// Creates a DashScopeClient for further api call.
    /// </summary>
    /// <param name="apiKey">The DashScope api key.</param>
    /// <param name="timeout">The timeout for internal http client, defaults to 2 minute.</param>
    /// <param name="baseAddress">The base address for dashscope api call.</param>
    /// <param name="workspaceId">The workspace id.</param>
    /// <remarks>
    ///     The underlying httpclient is cached by constructor parameter list.
    ///     Client created with same parameter value will share same underlying <see cref="HttpClient"/> instance.
    /// </remarks>
    public DashScopeClient(
        string apiKey,
        TimeSpan? timeout = null,
        string? baseAddress = null,
        string? workspaceId = null)
        : base(GetConfiguredClient(apiKey, timeout, baseAddress, workspaceId))
    {
    }

    private static HttpClient GetConfiguredClient(
        string apiKey,
        TimeSpan? timeout = null,
        string? baseAddress = null,
        string? workspaceId = null)
    {
        var client = ClientPools.GetValueOrDefault(GetCacheKey());
        if (client is null)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress ?? DashScopeDefaults.DashScopeApiBaseAddress),
                Timeout = timeout ?? TimeSpan.FromMinutes(2)
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Add("X-DashScope-WorkSpace", workspaceId);
            ClientPools.Add(GetCacheKey(), client);
        }

        return client;

        string GetCacheKey() => $"{apiKey}-{timeout?.TotalMilliseconds}-{baseAddress}-{workspaceId}";
    }
}
