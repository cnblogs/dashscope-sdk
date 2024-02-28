using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public static class Sut
{
    public static async Task<(DashScopeClientCore Client, MockHttpMessageHandler Handler)> GetTestClientAsync<TRequest, TResponse>(
        bool sse,
        RequestSnapshot<TRequest, TResponse> testCase)
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
