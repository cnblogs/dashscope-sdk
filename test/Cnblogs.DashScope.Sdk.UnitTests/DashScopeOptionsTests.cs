using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeOptionsTests
{
    [Theory]
    [InlineData(null, null, null, "https://dashscope.aliyuncs.com/api/v1/")]
    [InlineData("https://proxy.cnblogs.com/api/v1/", null, null, "https://proxy.cnblogs.com/api/v1/")]
    [InlineData(" ", "llm-test", "us-east-1", "https://llm-test.us-east-1.maas.aliyuncs.com/api/v1/")]
    [InlineData(null, "llm-test", "ap-southeast-1", "https://llm-test.ap-southeast-1.maas.aliyuncs.com/api/v1/")]
    [InlineData(null, null, "ap-southeast-1", "https://dashscope-intl.aliyuncs.com/api/v1/")]
    [InlineData(null, "llm-test", null, "https://llm-test.cn-beijing.maas.aliyuncs.com/api/v1/")]
    public void SelectBaseHttpAddress_ConfigurationCombination_ReturnExpectedAddress(
        string? baseAddress,
        string? workspaceId,
        string? regionId,
        string expected)
    {
        // Arrange
        var options = new DashScopeOptions
        {
            BaseAddress = baseAddress, WorkspaceId = workspaceId, RegionId = regionId
        };

        // Act
        var address = options.SelectBaseHttpAddress();

        // Assert
        Assert.Equal(expected, address);
    }

    [Theory]
    [InlineData(null, null, null, "wss://dashscope.aliyuncs.com/api-ws/v1/inference/")]
    [InlineData("wss://proxy.cnblogs.com/api-ws/v1/inference/", null, null, "wss://proxy.cnblogs.com/api-ws/v1/inference/")]
    [InlineData(" ", "llm-test", "us-east-1", "wss://llm-test.us-east-1.maas.aliyuncs.com/api-ws/v1/inference/")]
    [InlineData(null, "llm-test", "ap-southeast-1", "wss://llm-test.ap-southeast-1.maas.aliyuncs.com/api-ws/v1/inference/")]
    [InlineData(null, null, "ap-southeast-1", "wss://dashscope-intl.aliyuncs.com/api-ws/v1/inference/")]
    [InlineData(null, "llm-test", null, "wss://llm-test.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference/")]
    public void SelectBaseWssAddress_ConfigurationCombination_ReturnExpectedAddress(
        string? websocketBaseAddress,
        string? workspaceId,
        string? regionId,
        string expected)
    {
        // Arrange
        var options = new DashScopeOptions
        {
            WebsocketBaseAddress = websocketBaseAddress,
            WorkspaceId = workspaceId,
            RegionId = regionId
        };

        // Act
        var address = options.SelectBaseWssAddress();

        // Assert
        Assert.Equal(expected, address);
    }
}
