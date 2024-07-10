using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ErrorTests
{
    [Fact]
    public async Task Error_AuthError_ExceptionAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Error.AuthError;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var act = async () => await client.GetTextCompletionAsync(testCase.RequestModel);

        // Assert
        (await act.Should().ThrowAsync<DashScopeException>()).And.Error.Should().BeEquivalentTo(testCase.ResponseModel);
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Error_ParameterErrorNoSse_ExceptionAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.Error.ParameterError;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var act = async () => await client.GetTextCompletionAsync(testCase.RequestModel);

        // Assert
        (await act.Should().ThrowAsync<DashScopeException>()).And.Error.Should().BeEquivalentTo(testCase.ResponseModel);
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Error_ParameterErrorSse_ExceptionAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.Error.ParameterErrorSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var stream = client.GetTextCompletionStreamAsync(testCase.RequestModel);
        var act = async () => await stream.LastAsync();

        // Assert
        (await act.Should().ThrowAsync<DashScopeException>()).And.Error.Should().BeEquivalentTo(testCase.ResponseModel);
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Error_NetworkError_ExceptionAsync()
    {
        // Arrange
        var (client, handler) = Sut.GetTestClient();
        handler.Configure().MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
            .Throws(new InvalidOperationException("Network error!"));

        // Act
        var act = async ()
            => await client.GetTextCompletionAsync(Snapshots.TextGeneration.TextFormat.SinglePrompt.RequestModel);

        // Assert
        (await act.Should().ThrowAsync<DashScopeException>()).And.Error.Should().BeNull();
    }

    [Fact]
    public async Task Error_OpenAiCompatibleError_ExceptionAsync()
    {
        // Arrange
        var testCase = Snapshots.Error.UploadErrorNoSse;
        var (client, _) = await Sut.GetTestClientAsync(false, testCase);

        // Act
        var act = async () => await client.UploadFileAsync(
            Snapshots.File.TestFile.OpenRead(),
            Snapshots.File.TestFile.Name,
            "other");

        // Assert
        (await act.Should().ThrowAsync<DashScopeException>()).And.Error.Should().BeEquivalentTo(testCase.ResponseModel);
    }
}
