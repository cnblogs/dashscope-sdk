using System.Net.Http.Headers;
using System.Reflection;
using Cnblogs.DashScope.Core;
using FluentAssertions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeClientTests
{
    [Fact]
    public void DashScopeClient_Constructor_New()
    {
        // Arrange
        const string apiKey = "apiKey";

        // Act
        var act = () => new DashScopeClient(apiKey);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(ParamsShouldNotCache))]
    public void DashScopeClient_Constructor_NotCacheableParams(
        string apiKey,
        string apiKey2,
        TimeSpan? timeout,
        TimeSpan? timeout2)
    {
        // Arrange
        var client = new DashScopeClient(apiKey, timeout);
        var client2 = new DashScopeClient(apiKey2, timeout2);

        // Act
        var value = HttpClientAccessor.GetValue(client);
        var value2 = HttpClientAccessor.GetValue(client2);

        // Assert
        value.Should().NotBe(value2);
    }

    [Theory]
    [MemberData(nameof(ParamsShouldCache))]
    public void DashScopeClient_Constructor_CacheableParams(
        string apiKey,
        string apiKey2,
        TimeSpan? timeout,
        TimeSpan? timeout2)
    {
        // Arrange
        var client = new DashScopeClient(apiKey, timeout);
        var client2 = new DashScopeClient(apiKey2, timeout2);

        // Act
        var value = HttpClientAccessor.GetValue(client);
        var value2 = HttpClientAccessor.GetValue(client2);

        // Assert
        value.Should().Be(value2);
    }

    [Fact]
    public void DashScopeClient_Constructor_WithApiKeyHeader()
    {
        // Arrange
        const string apiKey = "key";
        var client = new DashScopeClient(apiKey);

        // Act
        var value = HttpClientAccessor.GetValue(client) as HttpClient;

        // Assert
        value?.DefaultRequestHeaders.Authorization?.Should()
            .BeEquivalentTo(new AuthenticationHeaderValue("Bearer", apiKey));
    }

    [Fact]
    public void DashScopeClient_Constructor_WithWorkspaceId()
    {
        // Arrange
        const string apiKey = "key";
        const string workspaceId = "workspaceId";
        var client = new DashScopeClient(apiKey, null, null, workspaceId);

        // Act
        var value = HttpClientAccessor.GetValue(client) as HttpClient;

        // Assert
        value?.DefaultRequestHeaders.GetValues("X-DashScope-WorkSpace").Should().BeEquivalentTo(workspaceId);
    }

    [Fact]
    public void DashScopeClient_Constructor_WithPrivateEndpoint()
    {
        // Arrange
        const string apiKey = "key";
        const string privateEndpoint = "https://dashscope.cnblogs.com/api/v1";
        var client = new DashScopeClient(apiKey, null, privateEndpoint);

        // Act
        var value = HttpClientAccessor.GetValue(client) as HttpClient;

        // Assert
        value?.BaseAddress.Should().BeEquivalentTo(new Uri(privateEndpoint));
    }

    public static TheoryData<string, string, TimeSpan?, TimeSpan?> ParamsShouldNotCache
        => new()
        {
            { "apiKey", "apiKey2", null, null }, // same null timespan with different apikey
            {
                // same timespan with different apiKey
                "apikey", "apiKey2", TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2)
            },
            {
                // same apikey with different timeout
                "apiKey", "apiKey", TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(1)
            }
        };

    public static TheoryData<string, string, TimeSpan?, TimeSpan?> ParamsShouldCache
        => new()
        {
            { "apiKey", "apiKey", null, null }, // same null timespan with same apikey
            {
                // same timespan with same apiKey
                "apiKey", "apiKey", TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2)
            }
        };

    private static readonly FieldInfo HttpClientAccessor = typeof(DashScopeClientCore).GetField(
        "_httpClient",
        BindingFlags.Instance | BindingFlags.NonPublic)!;
}
