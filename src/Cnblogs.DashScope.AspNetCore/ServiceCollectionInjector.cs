using System.Net.Http.Headers;
using Cnblogs.DashScope.AspNetCore;
using Cnblogs.DashScope.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Injector methods for DI container.
/// </summary>
public static class ServiceCollectionInjector
{
    /// <summary>
    /// Add DashScope client, allows usage of <see cref="IDashScopeClient"/> with DI.
    /// </summary>
    /// <param name="services">The service collection to add service to.</param>
    /// <param name="configuration">The root <see cref="IConfiguration"/>.</param>
    /// <param name="sectionName">The section name for DashScope.</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddDashScopeClient(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = DashScopeOptions.DashScope)
    {
        var section = configuration.GetRequiredSection(sectionName);
        return services.AddDashScopeClient(section);
    }

    /// <summary>
    /// Add DashScope client, allows usage of <see cref="IDashScopeClient"/> with DI.
    /// </summary>
    /// <param name="services">The service collection to add service to.</param>
    /// <param name="section">The DashScope configuration section.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">There is no api key provided in section.</exception>
    public static IHttpClientBuilder AddDashScopeClient(this IServiceCollection services, IConfigurationSection section)
    {
        if (string.IsNullOrWhiteSpace(section["apiKey"]))
        {
            throw new InvalidOperationException("There is no apiKey provided in given section");
        }

        services.Configure<DashScopeOptions>(section);
        return services.AddDashScopeHttpClient();
    }

    /// <summary>
    /// Add DashScope client, allows usage of <see cref="IDashScopeClient"/> with DI.
    /// </summary>
    /// <param name="services">The service collection to add service to.</param>
    /// <param name="apiKey">The DashScope api key.</param>
    /// <param name="baseAddress">The DashScope api base address, you may change this value if you are using proxy.</param>
    /// <param name="baseWebsocketAddress">The DashScope websocket base address, you may want to change this value if use are using proxy.</param>
    /// <param name="workspaceId">Default workspace id to use.</param>
    /// <param name="region">The region of your workspace.</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddDashScopeClient(
        this IServiceCollection services,
        string apiKey,
        string? baseAddress = null,
        string? baseWebsocketAddress = null,
        string? workspaceId = null,
        DashScopeRegion? region = null)
    {
        services.Configure<DashScopeOptions>(o =>
        {
            o.ApiKey = apiKey;
            o.BaseAddress = baseAddress;
            o.WebsocketBaseAddress = baseWebsocketAddress;
            o.WorkspaceId = workspaceId;
            o.RegionId = (region ?? DashScopeRegion.Beijing).ToRegionId();
        });

        return services.AddDashScopeHttpClient();
    }

    private static IHttpClientBuilder AddDashScopeHttpClient(this IServiceCollection services)
    {
        services.AddSingleton<IDashScopeClientWebSocketFactory, DashScopeClientWebSocketFactory>();
        services.AddSingleton<DashScopeClientWebSocketPool>(sp
            => new DashScopeClientWebSocketPool(
                sp.GetRequiredService<IDashScopeClientWebSocketFactory>(),
                sp.GetRequiredService<IOptions<DashScopeOptions>>().Value));
        services.AddScoped<IDashScopeClient, DashScopeClientAspNetCore>();
        return services.AddHttpClient(
            DashScopeAspNetCoreDefaults.DefaultHttpClientName,
            (sp, h) =>
            {
                var options = sp.GetRequiredService<IOptions<DashScopeOptions>>().Value;
                h.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.ApiKey);
                if (string.IsNullOrWhiteSpace(options.WorkspaceId) == false)
                {
                    h.DefaultRequestHeaders.Add("X-DashScope-WorkSpace", options.WorkspaceId);
                }

                h.BaseAddress = new Uri(options.SelectBaseHttpAddress());
            });
    }
}
