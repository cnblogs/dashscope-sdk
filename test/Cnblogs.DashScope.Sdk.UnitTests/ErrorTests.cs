using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
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
            var ex = await Assert.ThrowsAsync<DashScopeException>(act);
            Assert.Equivalent(testCase.ResponseModel, ex.Error);
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
            var ex = await Assert.ThrowsAsync<DashScopeException>(act);
            Assert.Equivalent(testCase.ResponseModel, ex.Error);
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
            var ex = await Assert.ThrowsAsync<DashScopeException>(act);
            Assert.Equivalent(testCase.ResponseModel, ex.Error);
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
            var testCase = Snapshots.TextGeneration.TextFormat.SinglePrompt;

            // Act
            var act = async ()
                => await client.GetTextCompletionAsync(testCase.RequestModel);

            // Assert
            var ex = await Assert.ThrowsAsync<DashScopeException>(act);
            Assert.Null(ex.Error);
        }

        [Fact]
        public async Task Error_OpenAiCompatibleError_ExceptionAsync()
        {
            // Arrange
            var testCase = Snapshots.Error.UploadErrorNoSse;
            var (client, _) = await Sut.GetTestClientAsync(false, testCase);

            // Act
            var act = async () => await client.OpenAiCompatibleUploadFileAsync(
                Snapshots.OpenAiCompatibleFile.TestFile.OpenRead(),
                Snapshots.OpenAiCompatibleFile.TestFile.Name,
                "other");

            // Assert
            var ex = await Assert.ThrowsAsync<DashScopeException>(act);
            Assert.Equivalent(testCase.ResponseModel, ex.Error);
        }
    }
}
