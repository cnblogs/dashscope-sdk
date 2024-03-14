using System.Net.Http.Headers;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The DashScopeClient for direct usage
/// </summary>
public class DashScopeClient : DashScopeClientCore
{
    private static HttpClient? singletonClient;
    private static HttpClient SingletonClient
    {
        get
        {
            singletonClient ??= new HttpClient(
                new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(2) })
            {
                BaseAddress = new Uri(DashScopeDefaults.DashScopeApiBaseAddress)
            };
            return singletonClient;
        }
    }

    /// <summary>
    /// Creates a DashScopeClient for further api call.
    /// </summary>
    /// <param name="apiKey">The DashScope api key.</param>
    public DashScopeClient(string apiKey)
        : base(SingletonClient)
    {
        SingletonClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }
}
