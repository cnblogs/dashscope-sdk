using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Core.Internals;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

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
        var client = new DashScopeClientCore(
            new HttpClient(handler) { BaseAddress = new Uri("https://example.com") },
            new DashScopeClientWebSocketPool(new DashScopeOptions()));
        return (client, handler);
    }

    // IClientWebSocket is internal, use InternalVisibleToAttribute make it visible to Cnblogs.DashScope.Sdk.UnitTests
    internal static async
        Task<(DashScopeClientCore Client, DashScopeClientWebSocket ClientWebSocket, FakeClientWebSocket Server)>
        GetSocketTestClientAsync<TOutput>()
        where TOutput : class
    {
        var socket = new FakeClientWebSocket();
        var dsWebSocket = new DashScopeClientWebSocket(socket);
        await dsWebSocket.ConnectAsync<TOutput>(
            new Uri(DashScopeDefaults.WebsocketApiBaseAddress),
            CancellationToken.None);
        dsWebSocket.ResetOutput();
        var pool = new DashScopeClientWebSocketPool(new List<DashScopeClientWebSocket> { dsWebSocket });
        var client = new DashScopeClientCore(new HttpClient(), pool);
        return (client, dsWebSocket, socket);
    }
}
