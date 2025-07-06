using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeClientWebSocketPoolTests
{
    [Fact]
    public async Task RentSocket_PoolIsEmpty_CreateAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        var factory = Substitute.For<IDashScopeClientWebSocketFactory>();
        factory.Configure().GetClientWebSocket(Arg.Any<string>(), Arg.Any<string>())
            .Returns(new DashScopeClientWebSocket(new FakeClientWebSocket()));
        var pool = new DashScopeClientWebSocketPool(factory, option);

        // Act
        var socket = await pool.RentSocketAsync();

        // Assert
        Assert.Equal(DashScopeWebSocketState.Ready, socket.Socket.State);
    }

    [Fact]
    public async Task RentSocket_HasAvailableSocket_ReturnAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        var factory = Substitute.For<IDashScopeClientWebSocketFactory>();
        factory.Configure().GetClientWebSocket(Arg.Any<string>(), Arg.Any<string>())
            .Returns(new DashScopeClientWebSocket(new FakeClientWebSocket()));
        var readySocket = factory.GetClientWebSocket(string.Empty);
        await readySocket.ConnectAsync(new Uri(option.WebsocketBaseAddress));
        var pool = new DashScopeClientWebSocketPool(new[] { readySocket }, factory);

        // Act
        var socket = await pool.RentSocketAsync();

        // Assert
        Assert.Equal(DashScopeWebSocketState.Ready, socket.Socket.State);
        Assert.StrictEqual(readySocket, socket.Socket);
    }

    [Fact]
    public async Task RentSocket_SocketExpired_DisposeAndMoveNext()
    {
        // Arrange
        var factory = Substitute.For<IDashScopeClientWebSocketFactory>();
        factory.Configure().GetClientWebSocket(Arg.Any<string>(), Arg.Any<string>())
            .Returns(_ => new DashScopeClientWebSocket(new FakeClientWebSocket()));

        var fakeSocket = new FakeClientWebSocket();
        var closedSocket = new DashScopeClientWebSocket(fakeSocket);
        await closedSocket.CloseAsync();
        var pool = new DashScopeClientWebSocketPool(new[] { closedSocket }, factory);

        // Act
        var socket = await pool.RentSocketAsync();

        // Assert
        Assert.Equal(DashScopeWebSocketState.Ready, socket.Socket.State);
        Assert.NotStrictEqual(closedSocket, socket.Socket);
        Assert.True(fakeSocket.DisposeCalled);
    }

    [Fact]
    public async Task RentSocket_PoolStarvation_ThrowAsync()
    {
        // Arrange
        var option = new DashScopeOptions { SocketPoolSize = 3 };
        var factory = Substitute.For<IDashScopeClientWebSocketFactory>();
        factory.Configure().GetClientWebSocket(Arg.Any<string>(), Arg.Any<string>())
            .Returns(_ => new DashScopeClientWebSocket(new FakeClientWebSocket()));
        var pool = new DashScopeClientWebSocketPool(factory, option);
        await Task.WhenAll(Enumerable.Range(0, option.SocketPoolSize).Select(async _ => await pool.RentSocketAsync()));

        // Act
        var act = async () => await pool.RentSocketAsync();

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async Task Dispose_ManuallyDispose_DisposeAllPooledSocketsAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        var factory = Substitute.For<IDashScopeClientWebSocketFactory>();
        factory.Configure().GetClientWebSocket(Arg.Any<string>(), Arg.Any<string>())
            .Returns(_ => new DashScopeClientWebSocket(new FakeClientWebSocket()));
        var fake1 = new FakeClientWebSocket();
        var fake2 = new FakeClientWebSocket();
        var s1 = new DashScopeClientWebSocket(fake1);
        var s2 = new DashScopeClientWebSocket(fake2);
        var sockets = new[] { s1, s2 };
        foreach (var socket in sockets)
        {
            await socket.ConnectAsync(new Uri(option.WebsocketBaseAddress));
        }

        var pool = new DashScopeClientWebSocketPool(sockets, factory);

        // Act
        var active = await pool.RentSocketAsync();
        pool.Dispose();

        // Assert
        Assert.NotNull(active);
        Assert.True(fake1.DisposeCalled);
        Assert.True(fake2.DisposeCalled);
    }

    [Fact]
    public async Task ReturnSocket_SocketNotReady_DisposeAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        var fakeSocket = new FakeClientWebSocket();
        var factory = Substitute.For<IDashScopeClientWebSocketFactory>();
        factory.Configure().GetClientWebSocket(Arg.Any<string>(), Arg.Any<string>())
            .Returns(new DashScopeClientWebSocket(fakeSocket));
        var pool = new DashScopeClientWebSocketPool(factory, option);

        // Act
        var socket = await pool.RentSocketAsync();
        await fakeSocket.WriteServerCloseAsync();
        pool.ReturnSocket(socket.Socket);

        // Assert
        Assert.Equal(0, pool.ActiveSocketCount);
        Assert.Equal(0, pool.AvailableSocketCount);
        Assert.Equal(DashScopeWebSocketState.Closed, socket.Socket.State);
        Assert.True(fakeSocket.DisposeCalled);
    }

    [Fact]
    public async Task ReturnSocket_SocketReady_SaveAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        var fakeSocket = new FakeClientWebSocket();
        var factory = Substitute.For<IDashScopeClientWebSocketFactory>();
        factory.Configure().GetClientWebSocket(Arg.Any<string>(), Arg.Any<string>())
            .Returns(new DashScopeClientWebSocket(fakeSocket));
        var pool = new DashScopeClientWebSocketPool(factory, option);

        // Act
        var socket = await pool.RentSocketAsync();
        pool.ReturnSocket(socket.Socket);

        // Assert
        Assert.Equal(1, pool.AvailableSocketCount);
        Assert.Equal(0, pool.ActiveSocketCount);
        Assert.False(fakeSocket.DisposeCalled);
    }
}
