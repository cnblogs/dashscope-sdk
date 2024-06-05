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
    /// <remarks>
    ///     The underlying httpclient is cached by apiKey and timeout.
    ///     Client created with same apiKey and timeout value will share same underlying <see cref="HttpClient"/> instance.
    /// </remarks>
    public DashScopeClient(string apiKey, TimeSpan? timeout = null)
        : base(GetConfiguredClient(apiKey, timeout))
    {
    }

    private static HttpClient GetConfiguredClient(string apiKey, TimeSpan? timeout)
    {
        var client = ClientPools.GetValueOrDefault(GetCacheKey());
        if (client is null)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(DashScopeDefaults.DashScopeApiBaseAddress),
                Timeout = timeout ?? TimeSpan.FromMinutes(2)
            };
            ClientPools.Add(GetCacheKey(), client);
        }

        return client;

        string GetCacheKey() => $"{apiKey}-{timeout?.TotalMilliseconds}";
    }
}
