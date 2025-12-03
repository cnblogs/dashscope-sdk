using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
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
            var task = await client.UploadFilesAsync(
                "file-extract",
                new[]
                {
                    new DashScopeUploadFileInput(Snapshots.File.TestFile.OpenRead(), Snapshots.File.TestFile.Name),
                    new DashScopeUploadFileInput(Snapshots.File.TestImage.OpenRead(), Snapshots.File.TestImage.Name)
                });

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(r
                    => r.Method == testCase.GetRequestMethod(sse)
                       && ("/api/v1" + r.RequestUri!.PathAndQuery) == testCase.GetRequestPathAndQuery(sse)),
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
            var task = await client.GetFileAsync(testCase.ResponseModel.Data.FileId);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(r
                    => r.Method == testCase.GetRequestMethod(sse)
                       && ("/api/v1" + r.RequestUri!.PathAndQuery) == testCase.GetRequestPathAndQuery(sse)),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, task);
        }

        [Fact]
        public async Task File_List_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.File.ListFilesNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var list = await client.ListFilesAsync(1, 2);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(r
                    => r.Method == testCase.GetRequestMethod(sse)
                       && ("/api/v1" + r.RequestUri!.PathAndQuery) == testCase.GetRequestPathAndQuery(sse)),
                Arg.Any<CancellationToken>());
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
            var task = await client.DeleteFileAsync("file-fe-5d5eb068893f4b5e8551ada4");

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(r
                    => r.Method == testCase.GetRequestMethod(sse)
                       && ("/api/v1" + r.RequestUri!.PathAndQuery) == testCase.GetRequestPathAndQuery(sse)),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, task);
        }
    }
}
