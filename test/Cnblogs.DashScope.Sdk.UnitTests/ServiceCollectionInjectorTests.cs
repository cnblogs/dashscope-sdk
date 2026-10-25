using System.Net.Http.Headers;
using Cnblogs.DashScope.AspNetCore;
using Cnblogs.DashScope.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ServiceCollectionInjectorTests
{
    private const string ApiKey = "api key";
    private const string ProxyApi = "https://www.gateway.com/dashScope/v1/";

    [Fact]
    public void Parameter_Normal_Inject()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddDashScopeClient(ApiKey);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName);
        var client = provider.GetRequiredService<IDashScopeClient>();

        // Assert
        Assert.NotNull(client);
        Assert.IsType<DashScopeClientAspNetCore>(client);
        Assert.NotNull(httpClient);
        Assert.Equivalent(
            new AuthenticationHeaderValue("Bearer", ApiKey),
            httpClient.DefaultRequestHeaders.Authorization);
    }

    [Fact]
    public void Parameter_HasProxy_Inject()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddDashScopeClient(ApiKey, baseAddress: ProxyApi);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName);
        var client = provider.GetRequiredService<IDashScopeClient>();

        // Assert
        Assert.NotNull(client);
        Assert.IsType<DashScopeClientAspNetCore>(client);
        Assert.NotNull(httpClient);
        Assert.Equivalent(
            new AuthenticationHeaderValue("Bearer", ApiKey),
            httpClient.DefaultRequestHeaders.Authorization);
        Assert.Equivalent(new Uri(ProxyApi), httpClient.BaseAddress);
    }

    [Fact]
    public void Configuration_Normal_Inject()
    {
        // Arrange
        var services = new ServiceCollection();
        var config = new Dictionary<string, string?>
        {
            { "irrelevant", "irr" },
            { "dashScope:apiKey", ApiKey },
            { "dashScope:baseAddress", ProxyApi }
        };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        // Act
        services.AddDashScopeClient(configuration);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName);
        var client = provider.GetRequiredService<IDashScopeClient>();

        // Assert
        Assert.NotNull(client);
        Assert.IsType<DashScopeClientAspNetCore>(client);
        Assert.NotNull(httpClient);
        Assert.Equivalent(
            new AuthenticationHeaderValue("Bearer", ApiKey),
            httpClient.DefaultRequestHeaders.Authorization);
        Assert.Equivalent(new Uri(ProxyApi), httpClient.BaseAddress);
    }

    [Fact]
    public void Configuration_CustomSectionName_Inject()
    {
        // Arrange
        var services = new ServiceCollection();
        var config = new Dictionary<string, string?>
        {
            { "irrelevant", "irr" },
            { "dashScopeCustom:apiKey", ApiKey },
            { "dashScopeCustom:baseAddress", ProxyApi }
        };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        // Act
        services.AddDashScopeClient(configuration, "dashScopeCustom");
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName);
        var client = provider.GetRequiredService<IDashScopeClient>();

        // Assert
        Assert.NotNull(client);
        Assert.IsType<DashScopeClientAspNetCore>(client);
        Assert.NotNull(httpClient);
        Assert.Equivalent(
            new AuthenticationHeaderValue("Bearer", ApiKey),
            httpClient.DefaultRequestHeaders.Authorization);
        Assert.Equivalent(new Uri(ProxyApi), httpClient.BaseAddress);
    }

    [Fact]
    public void Configuration_AddMultipleTime_Replace()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddDashScopeClient(ApiKey, baseAddress: ProxyApi);
        services.AddDashScopeClient(ApiKey, baseAddress: ProxyApi);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName);
        var client = provider.GetRequiredService<IDashScopeClient>();

        // Assert
        Assert.NotNull(client);
        Assert.IsType<DashScopeClientAspNetCore>(client);
        Assert.NotNull(httpClient);
        Assert.Equivalent(
            new AuthenticationHeaderValue("Bearer", ApiKey),
            httpClient.DefaultRequestHeaders.Authorization);
        Assert.Equivalent(new Uri(ProxyApi), httpClient.BaseAddress);
    }

    [Fact]
    public void Parameter_WorkspaceAndRegion_MaasAddress()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddDashScopeClient(ApiKey, workspaceId: "llm-test", region: DashScopeRegion.Singapore);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName);

        // Assert
        Assert.Equivalent(new Uri("https://llm-test.ap-southeast-1.maas.aliyuncs.com/api/v1/"), httpClient.BaseAddress);
        Assert.Equal("llm-test", httpClient.DefaultRequestHeaders.GetValues("X-DashScope-WorkSpace").First());
    }

    [Fact]
    public void Parameter_RegionOnly_IntlAddress()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddDashScopeClient(ApiKey, region: DashScopeRegion.Singapore);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName);

        // Assert
        Assert.Equivalent(new Uri("https://dashscope-intl.aliyuncs.com/api/v1/"), httpClient.BaseAddress);
        Assert.False(httpClient.DefaultRequestHeaders.Contains("X-DashScope-WorkSpace"));
    }

    [Fact]
    public void Configuration_WorkspaceAndRegionId_MaasAddress()
    {
        // Arrange
        var services = new ServiceCollection();
        var config = new Dictionary<string, string?>
        {
            { "dashScope:apiKey", ApiKey },
            { "dashScope:workspaceId", "llm-test" },
            { "dashScope:regionId", "us-east-1" }
        };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        // Act
        services.AddDashScopeClient(configuration);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName);

        // Assert
        Assert.Equivalent(new Uri("https://llm-test.us-east-1.maas.aliyuncs.com/api/v1/"), httpClient.BaseAddress);
    }

    [Fact]
    public void Configuration_NoBaseAddress_DefaultAddress()
    {
        // Arrange
        var services = new ServiceCollection();
        var config = new Dictionary<string, string?> { { "dashScope:apiKey", ApiKey } };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        // Act
        services.AddDashScopeClient(configuration);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient(DashScopeAspNetCoreDefaults.DefaultHttpClientName);

        // Assert
        Assert.Equivalent(new Uri("https://dashscope.aliyuncs.com/api/v1/"), httpClient.BaseAddress);
    }

    [Fact]
    public void Configuration_NoApiKey_Throw()
    {
        // Arrange
        var services = new ServiceCollection();
        var config = new Dictionary<string, string?> { { "irrelevant", "irr" }, { "dashScope:baseAddress", ProxyApi } };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        // Act
        var act = () => services.AddDashScopeClient(configuration);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
