using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class VocabularyCustomizationSerializationTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task VocabularyCustomization_NoSse_SuccessAsync(
        RequestSnapshot<ModelRequest<VocabularyCustomizationInput>,
            ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CustomizeVocabularyAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<VocabularyCustomizationInput>,
            ModelResponse<VocabularyCustomizationOutput, VocabularyCustomizationUsage>>> NoSseData
        => new()
        {
            Snapshots.VocabularyCustomization.CreateNoSse,
            Snapshots.VocabularyCustomization.ListNoSse,
            Snapshots.VocabularyCustomization.UpdateNoSse,
            Snapshots.VocabularyCustomization.QueryNoSse,
            Snapshots.VocabularyCustomization.DeleteNoSse
        };
}
