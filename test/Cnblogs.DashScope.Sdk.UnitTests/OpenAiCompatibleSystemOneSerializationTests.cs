using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class OpenAiCompatibleSystemOneSerializationTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task OpenAiCompatibleGetSystemOneDecision_NoSse_SuccessAsync(
        RequestSnapshot<SystemOneDecisionInput, SystemOneDecision> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var decision = await client.OpenAiCompatibleGetSystemOneDecisionAsync(testCase.RequestModel);

        // Assert
        // the request equivalence also covers the wire shapes: the state goes as it is given
        // (an object or an array), so does the criteria of a question (a map or an array)
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => m.Method == testCase.GetRequestMethod(sse)
                                           && m.RequestUri!.PathAndQuery == testCase.GetRequestPathAndQuery(sse)
                                           && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, decision);
    }

    public static TheoryData<RequestSnapshot<SystemOneDecisionInput, SystemOneDecision>> NoSseData
        => new()
        {
            Snapshots.OpenAiCompatibleSystemOne.TicketTriageNoSse,
            Snapshots.OpenAiCompatibleSystemOne.NoulNoSse
        };

    [Fact]
    public async Task OpenAiCompatibleGetSystemOneDecision_InvalidQuestionNoSse_ExceptionAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleSystemOne.InvalidQuestionNoSse;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var act = async () => await client.OpenAiCompatibleGetSystemOneDecisionAsync(testCase.RequestModel);

        // Assert
        // the errors take the DashScope shape although the path is under /compatible-mode,
        // the error detail has to survive
        var ex = await Assert.ThrowsAsync<DashScopeException>(act);
        Assert.Equal(400, ex.Status);
        Assert.Equivalent(testCase.ResponseModel, ex.Error);
    }

    [Fact]
    public async Task OpenAiCompatibleGetSystemOneDecision_SharedDomainNoSse_ExceptionAsync()
    {
        // Arrange
        const bool sse = false;
        // the endpoint is only served on the workspace domain
        var testCase = Snapshots.OpenAiCompatibleSystemOne.SharedDomainNoSse;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var act = async () => await client.OpenAiCompatibleGetSystemOneDecisionAsync(testCase.RequestModel);

        // Assert
        // the shared domain answers 404 with an empty body, there is no error detail
        var ex = await Assert.ThrowsAsync<DashScopeException>(act);
        Assert.Equal(404, ex.Status);
        Assert.Null(ex.Error);
    }
}
