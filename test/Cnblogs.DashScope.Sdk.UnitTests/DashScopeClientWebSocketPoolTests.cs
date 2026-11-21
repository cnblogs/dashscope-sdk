using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeClientWebSocketPoolTests
{
    [Fact]
    public async Task RentSocket_PoolIsEmpty_CreateAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        var pool = new DashScopeClientWebSocketPool(option, CreateSocket);

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
        var readySocket = CreateSocket(string.Empty, null, false);
        await readySocket.ConnectAsync(new Uri(option.SelectBaseWssAddress()));
        var pool = new DashScopeClientWebSocketPool(new[] { readySocket });

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
        var fakeSocket = new FakeClientWebSocket();
        var closedSocket = new DashScopeClientWebSocket(fakeSocket);
        await closedSocket.CloseAsync();
        var pool = new DashScopeClientWebSocketPool(new[] { closedSocket }, CreateSocket);

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
        var pool = new DashScopeClientWebSocketPool(option, CreateSocket);
        await Task.WhenAll(Enumerable.Range(0, option.SocketPoolSize).Select(async _ => await pool.RentSocketAsync()));

        // Act
        var act = async () => await pool.RentSocketAsync();

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async Task RentSocket_DataInspectionNotConfigured_CreateSocketWithoutInspectionAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        bool? inspectionEnabled = null;
        var pool = new DashScopeClientWebSocketPool(
            option,
            (apiKey, workspaceId, enableDataInspection) =>
            {
                inspectionEnabled = enableDataInspection;
                return CreateSocket(apiKey, workspaceId, enableDataInspection);
            });

        // Act
        await pool.RentSocketAsync();

        // Assert
        Assert.True(inspectionEnabled == false);
    }

    [Fact]
    public async Task RentSocket_DataInspectionEnabledInOptions_CreateInspectedSocketAsync()
    {
        // Arrange
        var option = new DashScopeOptions { DataInspection = DataInspectionOptions.CipInputAndOutput };
        bool? inspectionEnabled = null;
        var pool = new DashScopeClientWebSocketPool(
            option,
            (apiKey, workspaceId, enableDataInspection) =>
            {
                inspectionEnabled = enableDataInspection;
                return CreateSocket(apiKey, workspaceId, enableDataInspection);
            });

        // Act
        await pool.RentSocketAsync();

        // Assert
        Assert.True(inspectionEnabled);
    }

    [Fact]
    public async Task CreateRealtimeSocket_ConnectRealtimeUri_SuccessAsync()
    {
        // Arrange
        var fakeSocket = new FakeClientWebSocket();
        var option = new DashScopeOptions { WorkspaceId = "llm-test" };
        var pool = new DashScopeClientWebSocketPool(
            option,
            CreateSocket,
            (_, _, _) => fakeSocket);

        // Act
        var socket = await pool.CreateRealtimeSocketAsync("qwen3-tts-flash-realtime");

        // Assert
        Assert.Equal(
            new Uri("wss://llm-test.cn-beijing.maas.aliyuncs.com/api-ws/v1/realtime?model=qwen3-tts-flash-realtime"),
            fakeSocket.ConnectedUri);
        Assert.StrictEqual(fakeSocket, socket);
    }

    [Fact]
    public async Task RentSocket_RequestMatchesPoolMode_ReusePooledSocketAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        var pool = new DashScopeClientWebSocketPool(option, CreateSocket);
        var first = await pool.RentSocketAsync();

        // Act
        first.Dispose();

        // Assert
        Assert.Equal(1, pool.AvailableSocketCount);
    }

    [Fact]
    public async Task RentSocket_RequestDiffersFromPoolMode_CreateTemporarySocketAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        FakeClientWebSocket? created = null;
        var pool = new DashScopeClientWebSocketPool(
            option,
            (_, _, enableDataInspection) =>
            {
                Assert.True(enableDataInspection);
                created = new FakeClientWebSocket();
                return new DashScopeClientWebSocket(created, enableDataInspection);
            });

        // Act
        var socket = await pool.RentSocketAsync(dataInspection: DataInspectionOptions.CipInputAndOutput);
        socket.Dispose();

        // Assert
        Assert.NotNull(created);
        Assert.Equal(0, pool.AvailableSocketCount);
        Assert.True(created!.DisposeCalled);
    }

    [Fact]
    public async Task RentSocket_PoolInspectedRequestDisabled_CreateTemporarySocketAsync()
    {
        // Arrange
        var option = new DashScopeOptions { DataInspection = DataInspectionOptions.CipInputAndOutput };
        FakeClientWebSocket? created = null;
        var pool = new DashScopeClientWebSocketPool(
            option,
            (_, _, enableDataInspection) =>
            {
                Assert.False(enableDataInspection);
                created = new FakeClientWebSocket();
                return new DashScopeClientWebSocket(created, enableDataInspection);
            });

        // Act
        var socket = await pool.RentSocketAsync(dataInspection: DataInspectionOptions.Disabled);
        socket.Dispose();

        // Assert
        Assert.NotNull(created);
        Assert.Equal(0, pool.AvailableSocketCount);
        Assert.True(created!.DisposeCalled);
    }

    [Fact]
    public async Task RentSocket_TemporarySocket_IgnoresPoolSizeAsync()
    {
        // Arrange
        var option = new DashScopeOptions { SocketPoolSize = 1 };
        var pool = new DashScopeClientWebSocketPool(option, CreateSocket);
        await pool.RentSocketAsync();

        // Act
        var temporary = await pool.RentSocketAsync(dataInspection: DataInspectionOptions.CipInputAndOutput);

        // Assert
        Assert.Equal(DashScopeWebSocketState.Ready, temporary.Socket.State);
    }

    [Fact]
    public async Task Dispose_ManuallyDispose_DisposeAllPooledSocketsAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        var fake1 = new FakeClientWebSocket();
        var fake2 = new FakeClientWebSocket();
        var s1 = new DashScopeClientWebSocket(fake1);
        var s2 = new DashScopeClientWebSocket(fake2);
        var sockets = new[] { s1, s2 };
        foreach (var socket in sockets)
        {
            await socket.ConnectAsync(new Uri(option.SelectBaseWssAddress()));
        }

        var pool = new DashScopeClientWebSocketPool(sockets);

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
        var pool = new DashScopeClientWebSocketPool(
            option,
            (_, _, _) => new DashScopeClientWebSocket(fakeSocket));

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
        var pool = new DashScopeClientWebSocketPool(
            option,
            (_, _, _) => new DashScopeClientWebSocket(fakeSocket));

        // Act
        var socket = await pool.RentSocketAsync();
        pool.ReturnSocket(socket.Socket);

        // Assert
        Assert.Equal(1, pool.AvailableSocketCount);
        Assert.Equal(0, pool.ActiveSocketCount);
        Assert.False(fakeSocket.DisposeCalled);
    }

    private static DashScopeClientWebSocket CreateSocket(string apiKey, string? workspaceId, bool enableDataInspection)
    {
        return new DashScopeClientWebSocket(new FakeClientWebSocket(), enableDataInspection);
    }
}
