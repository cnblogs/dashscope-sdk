using Cnblogs.DashScope.Tests.Shared.Utils;

using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    public class TokenizationSerializationTests
    {
        [Fact]
        public async Task Tokenization_NoSse_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Tokenization.TokenizeNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var response = await client.TokenizeAsync(testCase.RequestModel);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, response);
        }
    }
}
