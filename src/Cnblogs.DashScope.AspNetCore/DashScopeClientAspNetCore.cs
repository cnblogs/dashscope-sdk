using Cnblogs.DashScope.Core;
using Microsoft.Extensions.Options;

namespace Cnblogs.DashScope.AspNetCore;

/// <summary>
/// The <see cref="DashScopeClientCore"/> with DI and options pattern support.
/// </summary>
public class DashScopeClientAspNetCore(IHttpClientFactory factory, DashScopeClientWebSocketPool pool)
    : DashScopeClientCore(factory.CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName), pool);
