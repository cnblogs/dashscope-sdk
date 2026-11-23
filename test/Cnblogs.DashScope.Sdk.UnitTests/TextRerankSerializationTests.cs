using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class TextRerankSerializationTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task RerankDocuments_NoSse_SuccessAsync(
        RequestSnapshot<ModelRequest<TextRerankInput, TextRerankParameters>,
            ModelResponse<TextRerankOutput, TextRerankUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.RerankDocumentsAsync(testCase.RequestModel);

        // Assert
        // the request equivalence also covers the wire shapes: the text only documents go as
        // strings, the multimodal documents all go as objects and the image query goes as an object
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<TextRerankInput, TextRerankParameters>,
        ModelResponse<TextRerankOutput, TextRerankUsage>>> NoSseData
        => new()
        {
            Snapshots.TextRerank.NoSse,
            Snapshots.TextRerank.Qwen37NoSse,
            Snapshots.TextRerank.VlNoSse,
            Snapshots.TextRerank.VlImageQueryNoSse
        };
}
