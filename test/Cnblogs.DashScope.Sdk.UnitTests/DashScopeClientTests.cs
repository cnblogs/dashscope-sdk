using System.Net.Http.Headers;
using System.Reflection;
using Cnblogs.DashScope.Core;
using Xunit.Abstractions;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    public class DashScopeClientTests
    {
        private readonly ITestOutputHelper _output;

        public DashScopeClientTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void DashScopeClient_Constructor_New()
        {
            // Arrange
            const string apiKey = "apiKey";

            // Act
            var client = new DashScopeClient(apiKey);
            _output.WriteLine("hash: " + client.GetHashCode()); // do something to avoid optimization
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
            Assert.NotSame(value2, value);
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
            Assert.Same(value2, value);
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
            Assert.Equivalent(new AuthenticationHeaderValue("Bearer", apiKey), value?.DefaultRequestHeaders.Authorization);
        }

        [Fact]
        public void DashScopeClient_Constructor_WithWorkspaceId()
        {
            // Arrange
            const string apiKey = "key";
            const string workspaceId = "workspaceId";
            var client = new DashScopeClient(apiKey, workspaceId: workspaceId);

            // Act
            var value = HttpClientAccessor.GetValue(client) as HttpClient;

            // Assert
            Assert.Equal(workspaceId, value?.DefaultRequestHeaders.GetValues("X-DashScope-WorkSpace").First());
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
            Assert.Equivalent(new Uri(privateEndpoint), value?.BaseAddress);
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
}
