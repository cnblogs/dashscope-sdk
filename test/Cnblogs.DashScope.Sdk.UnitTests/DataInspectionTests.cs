using System.Net;
using System.Text;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DataInspectionTests
{
    [Fact]
    public async Task TextCompletion_NoDataInspectionConfigured_NoHeaderAsync()
    {
        // Arrange
        var (client, handler) = await GetTextCompletionClientAsync();

        // Act
        await client.GetTextCompletionAsync(GetTextCompletionRequest());

        // Assert
        handler.Received(1).MockSend(
            Arg.Is<HttpRequestMessage>(m => m.Headers.Contains("X-DashScope-DataInspection") == false),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task TextCompletion_ClientLevelDataInspection_ApplyHeaderAsync()
    {
        // Arrange
        var (client, handler) = await GetTextCompletionClientAsync();
        client.DataInspection = DataInspectionOptions.CipInputAndOutput;

        // Act
        await client.GetTextCompletionAsync(GetTextCompletionRequest());

        // Assert
        handler.Received(1).MockSend(
            Arg.Is<HttpRequestMessage>(
                m => m.Headers.GetValues("X-DashScope-DataInspection").First()
                    == "{\"input\":\"cip\",\"output\":\"cip\"}"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task TextCompletion_RequestLevelDataInspection_OverrideClientAsync()
    {
        // Arrange
        var (client, handler) = await GetTextCompletionClientAsync();
        client.DataInspection = DataInspectionOptions.CipInput;

        // Act
        await client.GetTextCompletionAsync(GetTextCompletionRequest(DataInspectionOptions.CipOutput));

        // Assert
        handler.Received(1).MockSend(
            Arg.Is<HttpRequestMessage>(
                m => m.Headers.GetValues("X-DashScope-DataInspection").Single() == "{\"output\":\"cip\"}"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task TextCompletion_RequestLevelDisabled_SuppressClientConfigurationAsync()
    {
        // Arrange
        var (client, handler) = await GetTextCompletionClientAsync();
        client.DataInspection = DataInspectionOptions.CipInputAndOutput;

        // Act
        await client.GetTextCompletionAsync(GetTextCompletionRequest(DataInspectionOptions.Disabled));

        // Assert
        handler.Received(1).MockSend(
            Arg.Is<HttpRequestMessage>(m => m.Headers.Contains("X-DashScope-DataInspection") == false),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task TextCompletion_RequestLevelDataInspectionOnly_ApplyHeaderAsync()
    {
        // Arrange
        var (client, handler) = await GetTextCompletionClientAsync();

        // Act
        await client.GetTextCompletionAsync(GetTextCompletionRequest(DataInspectionOptions.CipInput));

        // Assert
        handler.Received(1).MockSend(
            Arg.Is<HttpRequestMessage>(
                m => m.Headers.GetValues("X-DashScope-DataInspection").Single() == "{\"input\":\"cip\"}"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public void ToHeaderValue_Disabled_ReturnNull()
    {
        Assert.Null(DataInspectionOptions.Disabled.ToHeaderValue());
        Assert.Null(new DataInspectionOptions().ToHeaderValue());
    }

    [Fact]
    public void ToHeaderValue_InspectBothDirections_ReturnBothEngines()
    {
        Assert.Equal("{\"input\":\"cip\",\"output\":\"cip\"}", DataInspectionOptions.CipInputAndOutput.ToHeaderValue());
    }

    private static Task<(DashScopeClientCore Client, MockHttpMessageHandler Handler)> GetTextCompletionClientAsync()
    {
        var (client, handler) = Sut.GetTestClient();
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(
                "{\"output\":{\"choices\":[{\"finish_reason\":\"stop\",\"message\":{\"role\":\"assistant\",\"content\":\"ok\"}}]},\"request_id\":\"test\"}",
                Encoding.UTF8,
                "application/json")
        };
        handler.Configure().MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
            .Returns(response);
        return Task.FromResult((client, handler));
    }

    private static ModelRequest<TextGenerationInput, ITextGenerationParameters> GetTextCompletionRequest(
        DataInspectionOptions? dataInspection = null)
    {
        return new ModelRequest<TextGenerationInput, ITextGenerationParameters>
        {
            Model = "qwen-plus",
            Input = new TextGenerationInput
            {
                Messages = new List<TextChatMessage> { TextChatMessage.User("hello") }
            },
            Parameters = new TextGenerationParameters { ResultFormat = "message" },
            DataInspection = dataInspection
        };
    }
}
