using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    public class WorkspaceIdTests
    {
        [Fact]
        public async Task ApplicationCall_WithWorkspaceId_ApplyAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Application.WorkflowInDifferentWorkSpaceNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            await client.GetApplicationResponseAsync("anyId", testCase.RequestModel);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(
                    m => m.Headers.GetValues("X-DashScope-WorkSpace").First() == testCase.RequestModel.WorkspaceId),
                Arg.Any<CancellationToken>());
        }
    }
}
