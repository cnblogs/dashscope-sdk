using System.Net.Http.Headers;
using FluentAssertions;
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
        var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(IDashScopeClient));

        // Assert
        provider.GetRequiredService<IDashScopeClient>().Should().NotBeNull().And
            .BeOfType<DashScopeClientCore>();
        httpClient.Should().NotBeNull();
        httpClient.DefaultRequestHeaders.Authorization.Should()
            .BeEquivalentTo(new AuthenticationHeaderValue("Bearer", ApiKey));
    }

    [Fact]
    public void Parameter_HasProxy_Inject()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddDashScopeClient(ApiKey, ProxyApi);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(IDashScopeClient));

        // Assert
        provider.GetRequiredService<IDashScopeClient>().Should().NotBeNull().And
            .BeOfType<DashScopeClientCore>();
        httpClient.Should().NotBeNull();
        httpClient.DefaultRequestHeaders.Authorization.Should()
            .BeEquivalentTo(new AuthenticationHeaderValue("Bearer", ApiKey));
        httpClient.BaseAddress.Should().BeEquivalentTo(new Uri(ProxyApi));
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
        var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(IDashScopeClient));

        // Assert
        provider.GetRequiredService<IDashScopeClient>().Should().NotBeNull().And
            .BeOfType<DashScopeClientCore>();
        httpClient.Should().NotBeNull();
        httpClient.DefaultRequestHeaders.Authorization.Should()
            .BeEquivalentTo(new AuthenticationHeaderValue("Bearer", ApiKey));
        httpClient.BaseAddress.Should().BeEquivalentTo(new Uri(ProxyApi));
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
        var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(IDashScopeClient));

        // Assert
        provider.GetRequiredService<IDashScopeClient>().Should().NotBeNull().And
            .BeOfType<DashScopeClientCore>();
        httpClient.Should().NotBeNull();
        httpClient.DefaultRequestHeaders.Authorization.Should()
            .BeEquivalentTo(new AuthenticationHeaderValue("Bearer", ApiKey));
        httpClient.BaseAddress.Should().BeEquivalentTo(new Uri(ProxyApi));
    }

    [Fact]
    public void Configuration_AddMultipleTime_Replace()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddDashScopeClient(ApiKey, ProxyApi);
        services.AddDashScopeClient(ApiKey, ProxyApi);
        var provider = services.BuildServiceProvider();
        var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(IDashScopeClient));

        // Assert
        provider.GetRequiredService<IDashScopeClient>().Should().NotBeNull().And
            .BeOfType<DashScopeClientCore>();
        httpClient.Should().NotBeNull();
        httpClient.DefaultRequestHeaders.Authorization.Should()
            .BeEquivalentTo(new AuthenticationHeaderValue("Bearer", ApiKey));
        httpClient.BaseAddress.Should().BeEquivalentTo(new Uri(ProxyApi));
    }

    [Fact]
    public void Configuration_NoApiKey_Throw()
    {
        // Arrange
        var services = new ServiceCollection();
        var config = new Dictionary<string, string?>
        {
            { "irrelevant", "irr" },
            { "dashScope:baseAddress", ProxyApi }
        };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();

        // Act
        var act = () => services.AddDashScopeClient(configuration);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}
