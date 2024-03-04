using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

public static class Sut
{
    public static async Task<(IDashScopeClient Client, MockHttpMessageHandler Handler)> GetTestClientAsync<TResponse>(
        bool sse,
        RequestSnapshot<TResponse> testCase)
    {
        var pair = GetTestClient();
        var expected = await testCase.ToResponseMessageAsync(sse);
        pair.Handler.Configure().MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
            .Returns(expected);
        return pair;
    }

    public static (DashScopeClientCore Client, MockHttpMessageHandler Handler) GetTestClient()
    {
        var handler = Substitute.ForPartsOf<MockHttpMessageHandler>();
        var client = new DashScopeClientCore(new HttpClient(handler) { BaseAddress = new Uri("https://example.com") });
        return (client, handler);
    }
}
