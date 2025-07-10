using Cnblogs.DashScope.Tests.Shared.Utils;

using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class FileSerializationTests
{
    [Fact]
    public async Task File_Upload_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.File.UploadFileNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.UploadFileAsync(Snapshots.File.TestFile.OpenRead(), Snapshots.File.TestFile.Name);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(r => r.RequestUri!.AbsolutePath == "/compatible-mode/v1/files"),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task File_Get_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.File.GetFileNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.GetFileAsync(testCase.ResponseModel.Id);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(
                r => r.RequestUri!.AbsolutePath == "/compatible-mode/v1/files/" + testCase.ResponseModel.Id.Value),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, task);
    }

    [Fact]
    public async Task File_List_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.File.ListFileNoSse;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var list = await client.ListFilesAsync();

        // Assert
        Assert.Equivalent(testCase.ResponseModel, list);
    }

    [Fact]
    public async Task File_Delete_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.File.DeleteFileNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var task = await client.DeleteFileAsync(testCase.ResponseModel.Id);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(
                r => r.RequestUri!.AbsolutePath == "/compatible-mode/v1/files/" + testCase.ResponseModel.Id.Value),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, task);
    }
}
