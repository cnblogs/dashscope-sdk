using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class OpenAiCompatibleFileSerializationTests
{
    [Fact]
    public async Task OpenAiFile_Upload_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleFile.UploadFileCompatibleNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.OpenAiCompatibleUploadFileAsync(
            Snapshots.OpenAiCompatibleFile.TestFile.OpenRead(),
            Snapshots.OpenAiCompatibleFile.TestFile.Name);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r
                => r.Method == testCase.GetRequestMethod(sse)
                   && r.RequestUri!.PathAndQuery == testCase.GetRequestPathAndQuery(sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task OpenAiFile_Get_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleFile.GetFileCompatibleNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.OpenAiCompatibleGetFileAsync(testCase.ResponseModel.Id);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r
                => r.Method == testCase.GetRequestMethod(sse)
                   && r.RequestUri!.PathAndQuery == testCase.GetRequestPathAndQuery(sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task OpenAiFile_List_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleFile.ListFileCompatibleNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var list = await client.OpenAiCompatibleListFilesAsync(
            "file-fe-e457d4773c3f4c9fbfadffaf",
            3,
            "20250101",
            "20251101",
            "file-extract");

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r
                => r.Method == testCase.GetRequestMethod(sse)
                   && r.RequestUri!.PathAndQuery == testCase.GetRequestPathAndQuery(sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, list);
    }

    [Fact]
    public async Task OpenAiFile_Delete_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.OpenAiCompatibleFile.DeleteFileCompatibleNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.OpenAiCompatibleDeleteFileAsync(testCase.ResponseModel.Id);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r
                => r.Method == testCase.GetRequestMethod(sse)
                   && r.RequestUri!.PathAndQuery == testCase.GetRequestPathAndQuery(sse)),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, task);
    }
}
