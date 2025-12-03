using System.Net;
using System.Net.WebSockets;
using System.Reflection;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    public class DashScopeClientWebSocketFactoryTests
    {
        private static readonly FieldInfo ClientWebSocketWrapperGetter =
            typeof(DashScopeClientWebSocket).GetField("_socket", BindingFlags.NonPublic | BindingFlags.Instance)!;

        private static readonly FieldInfo ClientWebSocketGetter =
            typeof(ClientWebSocketWrapper).GetField("_socket", BindingFlags.NonPublic | BindingFlags.Instance)!;

        private static readonly PropertyInfo RequestHeaderGetter = typeof(ClientWebSocketOptions).GetProperty(
            "RequestHeaders",
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        [Fact]
        public void CreateSocket_WithWorkspaceId_SetKeyAndSpaceIdProperly()
        {
            // Arrange
            const string apiKey = "apikey";
            const string workspaceId = "some-space";
            var factory = new DashScopeClientWebSocketFactory();

            // Act
            var socket = factory.GetClientWebSocket(apiKey, workspaceId);
            var socketWrapper = ClientWebSocketWrapperGetter.GetValue(socket) as ClientWebSocketWrapper;
            var clientWebSocket = ClientWebSocketGetter.GetValue(socketWrapper) as ClientWebSocket;
            var headers = RequestHeaderGetter.GetValue(clientWebSocket?.Options) as WebHeaderCollection;

            // Assert
            Assert.NotNull(socketWrapper);
            Assert.NotNull(headers);
            Assert.Equal("bearer " + apiKey, headers.Get("Authorization"));
            Assert.Equal(workspaceId, headers.Get("X-DashScope-WorkspaceId"));
        }
    }
}
