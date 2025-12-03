using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    public class ApplicationSerializationTests
    {
        [Fact]
        public async Task SingleCompletion_TextNoSse_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Application.SinglePromptNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var response = await client.GetApplicationResponseAsync("anyId", testCase.RequestModel);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, response);
        }

        [Fact]
        public async Task SingleCompletion_ThoughtNoSse_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Application.SinglePromptWithThoughtsNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var response = await client.GetApplicationResponseAsync("anyId", testCase.RequestModel);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, response);
        }

        [Fact]
        public async Task SingleCompletion_TextSse_SuccessAsync()
        {
            // Arrange
            const bool sse = true;
            var testCase = Snapshots.Application.SinglePromptSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var outputs = await client.GetApplicationResponseStreamAsync("anyId", testCase.RequestModel).ToListAsync();
            var text = string.Join(string.Empty, outputs.Select(o => o.Output.Text));

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
                Arg.Any<CancellationToken>());
            Assert.All(outputs.SkipLast(1), x => Assert.Equal("null", x.Output.FinishReason));
            Assert.Equal(testCase.ResponseModel.Output.Text, text);
            var last = outputs.Last();
            last = last with
            {
                Output = last.Output with
                {
                    Text = testCase.ResponseModel.Output.Text, Thoughts = testCase.ResponseModel.Output.Thoughts
                }
            };
            Assert.Equivalent(testCase.ResponseModel, last);
        }

        [Fact]
        public async Task ConversationCompletion_SessionIdNoSse_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Application.ConversationSessionIdNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var response = await client.GetApplicationResponseAsync("anyId", testCase.RequestModel);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, response);
        }

        [Fact]
        public async Task ConversationCompletion_MessageNoSse_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Application.ConversationMessageNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var response = await client.GetApplicationResponseAsync("anyId", testCase.RequestModel);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, response);
        }

        [Fact]
        public async Task SingleCompletion_MemoryNoSse_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Application.SinglePromptWithMemoryNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var response = await client.GetApplicationResponseAsync("anyId", testCase.RequestModel);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, response);
        }

        [Fact]
        public async Task SingleCompletion_WorkflowNoSse_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Application.WorkflowNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var response = await client.GetApplicationResponseAsync("anyId", testCase.RequestModel);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, response);
        }
    }
}
