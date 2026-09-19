using Cnblogs.DashScope.AspNetCore;
using Cnblogs.DashScope.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// DI helpers for LingQue CCAI Conversation Analysis AIO.
/// </summary>
public static class ContactCenterAiServiceCollectionExtensions
{
    /// <summary>
    /// Adds <see cref="IContactCenterAiClient"/> using configuration section (default: contactCenterAi).
    /// </summary>
    public static IHttpClientBuilder AddContactCenterAiClient(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = "contactCenterAi")
    {
        var section = configuration.GetRequiredSection(sectionName);
        return services.AddContactCenterAiClient(section);
    }

    /// <summary>
    /// Adds <see cref="IContactCenterAiClient"/> using a configuration section.
    /// </summary>
    public static IHttpClientBuilder AddContactCenterAiClient(
        this IServiceCollection services,
        IConfigurationSection section)
    {
        var accessKeyId = section["accessKeyId"];
        if (string.IsNullOrWhiteSpace(accessKeyId))
        {
            throw new InvalidOperationException("There is no accessKeyId provided in given section");
        }

        var accessKeySecret = section["accessKeySecret"];
        if (string.IsNullOrWhiteSpace(accessKeySecret))
        {
            throw new InvalidOperationException("There is no accessKeySecret provided in given section");
        }

        var endpoint = section["endpoint"] ?? "contactcenterai.cn-shanghai.aliyuncs.com";

        services.Configure<ContactCenterAiOptions>(section);
        return services.AddContactCenterAiHttpClient(endpoint);
    }

    /// <summary>
    /// Adds <see cref="IContactCenterAiClient"/> with explicit credentials.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="accessKeyId">AccessKey ID.</param>
    /// <param name="accessKeySecret">AccessKey Secret.</param>
    /// <param name="endpoint">Optional API endpoint host.</param>
    /// <param name="regionId">Optional region id.</param>
    /// <param name="securityToken">Optional STS token.</param>
    /// <returns>HTTP client builder.</returns>
    public static IHttpClientBuilder AddContactCenterAiClient(
        this IServiceCollection services,
        string accessKeyId,
        string accessKeySecret,
        string? endpoint = null,
        string? regionId = null,
        string? securityToken = null)
    {
        var resolvedEndpoint = endpoint ?? "contactcenterai.cn-shanghai.aliyuncs.com";
        services.Configure<ContactCenterAiOptions>(o =>
        {
            o.AccessKeyId = accessKeyId;
            o.AccessKeySecret = accessKeySecret;
            o.Endpoint = resolvedEndpoint;
            if (regionId != null)
            {
                o.RegionId = regionId;
            }

            o.SecurityToken = securityToken;
        });

        return services.AddContactCenterAiHttpClient(resolvedEndpoint);
    }

    private static IHttpClientBuilder AddContactCenterAiHttpClient(
        this IServiceCollection services,
        string endpoint)
    {
        services.AddScoped<IContactCenterAiClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<ContactCenterAiOptions>>().Value;
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient(DashScopeAspNetCoreDefaults.ContactCenterAiHttpClientName);
            return new ContactCenterAiClient(httpClient, options);
        });

        return services.AddHttpClient(DashScopeAspNetCoreDefaults.ContactCenterAiHttpClientName)
            .ConfigureHttpClient((sp, h) =>
            {
                var options = sp.GetRequiredService<IOptions<ContactCenterAiOptions>>().Value;
                h.BaseAddress = new Uri($"https://{endpoint}/");
                h.Timeout = options.Timeout;
            });
}
