using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Core.Internals;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static class Sut
{
    public static async Task<(IDashScopeClient Client, MockHttpMessageHandler Handler)> GetTestClientAsync(
        HttpResponseMessage response)
    {
        var pair = GetTestClient();
        pair.Handler.Configure().MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
            .Returns(response);
        return pair;
    }

    public static async Task<(IDashScopeClient Client, MockHttpMessageHandler Handler)> GetTestClientAsync<TResponse>(
        bool sse,
        RequestSnapshot<TResponse> testCase)
    {
        var expected = await testCase.ToResponseMessageAsync(sse);
        return await GetTestClientAsync(expected);
    }

    public static (DashScopeClientCore Client, MockHttpMessageHandler Handler) GetTestClient()
    {
        var handler = Substitute.ForPartsOf<MockHttpMessageHandler>();
        var client = new DashScopeClientCore(
            new HttpClient(handler) { BaseAddress = new Uri("https://example.com") },
            new DashScopeClientWebSocketPool(new DashScopeClientWebSocketFactory(), new DashScopeOptions()));
        return (client, handler);
    }

    // IClientWebSocket is internal, use InternalVisibleToAttribute make it visible to Cnblogs.DashScope.Sdk.UnitTests
    internal static async
        Task<(DashScopeClientCore Client, DashScopeClientWebSocket ClientWebSocket, FakeClientWebSocket Server)>
        GetSocketTestClientAsync()
    {
        var socket = new FakeClientWebSocket();
        var dsWebSocket = new DashScopeClientWebSocket(socket);
        await dsWebSocket.ConnectAsync(
            new Uri(DashScopeDefaults.WebsocketApiBaseAddress),
            CancellationToken.None);
        dsWebSocket.ResetOutput();
        var pool = new DashScopeClientWebSocketPool(
            new List<DashScopeClientWebSocket> { dsWebSocket },
            new DashScopeClientWebSocketFactory());
        var client = new DashScopeClientCore(new HttpClient(), pool);
        return (client, dsWebSocket, socket);
    }
}
