using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;
using NSubstitute.Extensions;
using Xunit.Abstractions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeClientWebSocketWrapperTests
{
    [Fact]
    public async Task Dispose_CallDispose_ReturnSocketToPoolAsync()
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
        socket.Dispose();

        // Assert
        Assert.Equal(1, pool.AvailableSocketCount);
        Assert.Equal(0, pool.ActiveSocketCount);
        Assert.False(fakeSocket.DisposeCalled);
    }
}
