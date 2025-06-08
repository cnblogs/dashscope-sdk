using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.AspNetCore;

/// <summary>
/// The <see cref="DashScopeClientCore"/> with DI and options pattern support.
/// </summary>
public class DashScopeClientAspNetCore
    : DashScopeClientCore
{
    /// <summary>
    /// The <see cref="DashScopeClientCore"/> with DI and options pattern support.
    /// </summary>
    /// <param name="factory">The factory to create <see cref="HttpClient"/>.</param>
    /// <param name="pool">The socket pool for WebSocket API calls.</param>
    public DashScopeClientAspNetCore(IHttpClientFactory factory, DashScopeClientWebSocketPool pool)
        : base(factory.CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName), pool)
    {
    }
}
