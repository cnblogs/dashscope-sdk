using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeDefaultsTests
{
    [Theory]
    [InlineData(null, null, "https://dashscope.aliyuncs.com/api/v1/")]
    [InlineData(null, "cn-beijing", "https://dashscope.aliyuncs.com/api/v1/")]
    [InlineData(null, "ap-southeast-1", "https://dashscope-intl.aliyuncs.com/api/v1/")]
    [InlineData(null, "cn-hongkong", "https://cn-hongkong.dashscope.aliyuncs.com/api/v1/")]
    [InlineData(null, "us-east-1", "https://dashscope.aliyuncs.com/api/v1/")]
    [InlineData("llm-test", null, "https://llm-test.cn-beijing.maas.aliyuncs.com/api/v1/")]
    [InlineData("llm-test", "ap-southeast-1", "https://llm-test.ap-southeast-1.maas.aliyuncs.com/api/v1/")]
    [InlineData("llm-test", "us-east-1", "https://llm-test.us-east-1.maas.aliyuncs.com/api/v1/")]
    public void HttpBaseAddress_WorkspaceAndRegionCombination_ReturnExpectedAddress(
        string? workspaceId,
        string? regionId,
        string expected)
    {
        // Act
        var address = DashScopeDefaults.HttpBaseAddress(workspaceId, regionId);

        // Assert
        Assert.Equal(expected, address);
    }

    [Theory]
    [InlineData(null, null, "wss://dashscope.aliyuncs.com/api-ws/v1/inference/")]
    [InlineData(null, "ap-southeast-1", "wss://dashscope-intl.aliyuncs.com/api-ws/v1/inference/")]
    [InlineData(null, "cn-hongkong", "wss://cn-hongkong.dashscope.aliyuncs.com/api-ws/v1/inference/")]
    [InlineData("llm-test", null, "wss://llm-test.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference/")]
    [InlineData("llm-test", "ap-southeast-1", "wss://llm-test.ap-southeast-1.maas.aliyuncs.com/api-ws/v1/inference/")]
    public void WebsocketBaseAddress_WorkspaceAndRegionCombination_ReturnExpectedAddress(
        string? workspaceId,
        string? regionId,
        string expected)
    {
        // Act
        var address = DashScopeDefaults.WebsocketBaseAddress(workspaceId, regionId);

        // Assert
        Assert.Equal(expected, address);
    }
}
