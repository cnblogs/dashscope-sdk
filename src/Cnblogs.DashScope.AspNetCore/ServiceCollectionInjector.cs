using System.Net.Http.Headers;
using System.Net.Http.Json;
using Cnblogs.DashScope.Core;
using Microsoft.Extensions.Configuration;

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
        string sectionName = "dashScope")
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
        var apiKey = section["apiKey"]
                     ?? throw new InvalidOperationException("There is no apiKey provided in given section");
        var baseAddress = section["baseAddress"];
        return string.IsNullOrEmpty(baseAddress)
            ? services.AddDashScopeClient(apiKey)
            : services.AddDashScopeClient(apiKey, baseAddress);
    }

    /// <summary>
    /// Add DashScope client, allows usage of <see cref="IDashScopeClient"/> with DI.
    /// </summary>
    /// <param name="services">The service collection to add service to.</param>
    /// <param name="apiKey">The DashScope api key.</param>
    /// <param name="baseAddress">The DashScope api base address, you may change this value if you are using proxy.</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddDashScopeClient(
        this IServiceCollection services,
        string apiKey,
        string baseAddress = "https://dashscope.aliyuncs.com/api/v1/")
    {
        return services.AddHttpClient<IDashScopeClient, DashScopeClientCore>(
            h =>
            {
                h.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                h.BaseAddress = new Uri(baseAddress);
            });
    }
}
