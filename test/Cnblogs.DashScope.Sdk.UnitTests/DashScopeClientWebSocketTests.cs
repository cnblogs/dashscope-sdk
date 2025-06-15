using System.Net;
using System.Net.WebSockets;
using System.Reflection;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Core.Internals;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeClientWebSocketTests
{
    private static readonly FieldInfo InnerSocketInfo =
        typeof(DashScopeClientWebSocket).GetField("_socket", BindingFlags.NonPublic | BindingFlags.Instance)
        ?? throw new InvalidOperationException(
            $"Can not found {nameof(DashScopeClientWebSocket)}._client, please update this test after refactoring");

    private static readonly PropertyInfo InnerRequestHeaderInfo =
        typeof(ClientWebSocketOptions).GetProperty("RequestHeaders", BindingFlags.NonPublic | BindingFlags.Instance)
        ?? throw new InvalidOperationException(
            $"Can not found {nameof(ClientWebSocketOptions)}.RequestHeaders property, please update this test after framework change");

    [Fact]
    public void Constructor_UseApiKeyAndWorkspaceId_EnsureConfigured()
    {
        // Arrange
        const string apiKey = "apiKey";
        const string workspaceId = "workspaceId";

        // Act
        var client = new DashScopeClientWebSocket(apiKey, workspaceId);
        var headers = ExtractHeaders(client);

        // Assert
        Assert.Equal($"bearer {apiKey}", headers.GetValues("Authorization")?.First());
        Assert.Equal("enable", headers.GetValues("X-DashScope-DataInspection")?.First());
        Assert.Equal(workspaceId, headers.GetValues("X-DashScope-WorkspaceId")?.First());
    }

    [Fact]
    public void Constructor_UseApiKeyWithoutWorkspaceId_EnsureConfigured()
    {
        // Arrange
        const string apiKey = "apiKey";

        // Act
        var client = new DashScopeClientWebSocket(apiKey);
        var headers = ExtractHeaders(client);

        // Assert
        Assert.Equal($"bearer {apiKey}", headers.GetValues("Authorization")?.First());
        Assert.Equal("enable", headers.GetValues("X-DashScope-DataInspection")?.First());
        Assert.Null(headers.GetValues("X-DashScope-WorkspaceId"));
    }

    [Fact]
    public void Constructor_UsePreconfiguredSocket_EnsureConfigured()
    {
        // Arrange
        using var socket = new ClientWebSocketWrapper(new ClientWebSocket());

        // Act
        var client = new DashScopeClientWebSocket(socket);

        // Assert
        Assert.StrictEqual(socket, InnerSocketInfo.GetValue(client));
    }

    [Fact]
    public async Task ConnectAsync_InitialConnect_ChangeStateAsync()
    {
        // Arrange
        var socket = Substitute.For<IClientWebSocket>();
        var client = new DashScopeClientWebSocket(socket);
        var apiUri = new Uri("ws://test.com");

        // Act
        await client.ConnectAsync<SpeechSynthesizerOutput>(apiUri);

        // Assert
        Assert.Equal(DashScopeWebSocketState.Ready, client.State);
        await socket.Received(1).ConnectAsync(Arg.Is(apiUri), Arg.Any<CancellationToken>());
        await socket.Received().ReceiveAsync(Arg.Any<ArraySegment<byte>>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ResetOutput_WithInitialOutput_CompleteThenCreateNewOutputAsync()
    {
        // Arrange
        var socket = Substitute.For<IClientWebSocket>();
        var client = new DashScopeClientWebSocket(socket);
        client.ResetOutput();
        var oldOutput = client.BinaryOutput;
        var oldSignal = client.TaskStarted;

        // Act
        client.ResetOutput();

        // Assert
        Assert.False(await oldSignal);
        Assert.True(oldOutput.Completion.IsCompletedSuccessfully);
        Assert.NotSame(oldOutput, client.BinaryOutput);
        Assert.NotSame(oldSignal, client.TaskStarted);
    }

    [Fact]
    public async Task SendMessageAsync_SocketClosed_ThrowAsync()
    {
        // Arrange
        var socket = Substitute.For<IClientWebSocket>();
        var client = new DashScopeClientWebSocket(socket);
        var snapshot = Snapshots.SpeechSynthesizer.RunTask;
        await client.CloseAsync();

        // Act
        var act = () => client.SendMessageAsync(snapshot.Message);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async Task SendMessageAsync_Connected_SendAsync()
    {
        // Arrange
        var socket = Substitute.For<IClientWebSocket>();
        var client = new DashScopeClientWebSocket(socket);
        var snapshot = Snapshots.SpeechSynthesizer.RunTask;

        // Act
        await client.ConnectAsync<SpeechSynthesizerOutput>(new Uri(DashScopeDefaults.WebsocketApiBaseAddress));
        await client.SendMessageAsync(snapshot.Message);

        // Assert
        await socket.Received().SendAsync(
            Arg.Is<ArraySegment<byte>>(s => Checkers.IsJsonEquivalent(s, snapshot.GetMessageJson())),
            WebSocketMessageType.Text,
            true,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ReceiveMessageAsync_ServerClosed_CloseAsync()
    {
        // Arrange
        var (_, dashScopeClientWebSocket, server) = await Sut.GetSocketTestClientAsync<SpeechSynthesizerOutput>();

        // Act
        await server.WriteServerCloseAsync();

        // Assert
        Assert.Equal(DashScopeWebSocketState.Closed, dashScopeClientWebSocket.State);
        Assert.Equal(WebSocketCloseStatus.NormalClosure, server.CloseStatus);
    }

    [Fact]
    public async Task ReceiveMessageAsync_TaskStarted_UpdateStateToRunningAsync()
    {
        // Arrange
        var (_, clientWebSocket, server) = await Sut.GetSocketTestClientAsync<SpeechSynthesizerOutput>();
        var snapshot = Snapshots.SpeechSynthesizer.TaskStarted;
        var taskStarted = clientWebSocket.TaskStarted;

        // Act
        await server.WriteServerMessageAsync(snapshot.GetMessageJson());
        var timeout = Task.Delay(2000); // socket handles message in other thread, wait for it.
        var any = await Task.WhenAny(timeout, taskStarted);

        // Assert
        Assert.Equal(any, taskStarted);
        Assert.Equal(DashScopeWebSocketState.RunningTask, clientWebSocket.State);
    }

    private static WebHeaderCollection ExtractHeaders(DashScopeClientWebSocket socket)
    {
        var obj = InnerSocketInfo.GetValue(socket);
        if (obj is not IClientWebSocket clientWebSocket)
        {
            throw new InvalidOperationException($"Get null when trying to fetch {InnerSocketInfo.Name}");
        }

        obj = InnerRequestHeaderInfo.GetValue(clientWebSocket.Options);
        if (obj is not WebHeaderCollection headers)
        {
            throw new InvalidOperationException(
                $"Wrong type or null when trying to fetch {InnerRequestHeaderInfo.Name}");
        }

        return headers;
    }
}
