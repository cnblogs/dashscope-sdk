using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeClientWebSocketWrapperTests
{
    [Fact]
    public async Task Dispose_CallDispose_ReturnSocketToPoolAsync()
    {
        // Arrange
        var option = new DashScopeOptions();
        var fakeSocket = new FakeClientWebSocket();
        var pool = new DashScopeClientWebSocketPool(
            option,
            (_, _, _) => new DashScopeClientWebSocket(fakeSocket));

        // Act
        var socket = await pool.RentSocketAsync();
        socket.Dispose();

        // Assert
        Assert.Equal(1, pool.AvailableSocketCount);
        Assert.Equal(0, pool.ActiveSocketCount);
        Assert.False(fakeSocket.DisposeCalled);
    }
}
