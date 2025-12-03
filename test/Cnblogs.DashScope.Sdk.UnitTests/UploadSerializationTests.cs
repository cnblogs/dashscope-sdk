using System.Net;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    public class UploadSerializationTests
    {
        [Fact]
        public async Task Upload_GetPolicy_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var testCase = Snapshots.Upload.GetPolicyNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

            // Act
            var task = await client.GetTemporaryUploadPolicyAsync("qwen-vl-plus");

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(r => r.RequestUri!.PathAndQuery.Contains("model=qwen-vl-plus")),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(testCase.ResponseModel, task);
        }

        [Fact]
        public async Task Upload_SubmitFileForm_SuccessAsync()
        {
            // Arrange
            var file = Snapshots.OpenAiCompatibleFile.TestImage;
            var policy = Snapshots.Upload.GetPolicyNoSse.ResponseModel;
            var testCase = Snapshots.Upload.UploadTemporaryFileNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(new HttpResponseMessage(HttpStatusCode.NoContent));

            // Act
            var ossUri = await client.UploadTemporaryFileAsync(file.OpenRead(), file.Name, policy);
            var expectedRequestForm = testCase.GetRequestForm(false);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(r
                    => r.RequestUri == new Uri(policy.Data.UploadHost)
                       && Checkers.CheckFormContent(r, expectedRequestForm)),
                Arg.Any<CancellationToken>());
            Assert.Equal($"oss://{policy.Data.UploadDir}/{file.Name}", ossUri);
        }

        [Fact]
        public async Task Upload_GetOssLinkDirectly_SuccessAsync()
        {
            // Arrange
            const bool sse = false;
            var file = Snapshots.OpenAiCompatibleFile.TestImage;
            var policyCase = Snapshots.Upload.GetPolicyNoSse;
            var testCase = Snapshots.Upload.UploadTemporaryFileNoSse;
            var (client, handler) = await Sut.GetTestClientAsync(sse, policyCase);

            // Act
            var uri = await client.UploadTemporaryFileAsync("qwen-vl-plus", file.OpenRead(), file.Name);
            var expectedRequestForm = testCase.GetRequestForm(false);

            // Assert
            handler.Received().MockSend(
                Arg.Is<HttpRequestMessage>(r
                    => r.RequestUri == new Uri(policyCase.ResponseModel.Data.UploadHost)
                       && Checkers.CheckFormContent(r, expectedRequestForm)),
                Arg.Any<CancellationToken>());
            Assert.Equal($"oss://{policyCase.ResponseModel.Data.UploadDir}/{file.Name}", uri);
        }

        [Fact]
        public async Task Upload_GetPolicyFailed_ThrowsAsync()
        {
            // Arrange
            var file = Snapshots.OpenAiCompatibleFile.TestImage;
            var (client, _) = await Sut.GetTestClientAsync(
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("null") });

            // Act
            var act = async () => await client.UploadTemporaryFileAsync("qwen-vl-plus", file.OpenRead(), file.Name);

            // Assert
            await Assert.ThrowsAsync<DashScopeException>(act);
        }
    }
}
