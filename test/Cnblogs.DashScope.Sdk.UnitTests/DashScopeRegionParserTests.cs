using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeRegionParserTests
{
    [Theory]
    [InlineData(DashScopeRegion.Beijing, "cn-beijing")]
    [InlineData(DashScopeRegion.Singapore, "ap-southeast-1")]
    [InlineData(DashScopeRegion.HongKong, "cn-hongkong")]
    [InlineData(DashScopeRegion.UsEast, "us-east-1")]
    [InlineData(DashScopeRegion.Germany, "eu-central-1")]
    [InlineData(DashScopeRegion.Japan, "ap-northeast-1")]
    public void ToRegionId_DefinedRegion_ReturnRegionId(DashScopeRegion region, string expected)
    {
        // Act
        var regionId = region.ToRegionId();

        // Assert
        Assert.Equal(expected, regionId);
    }

    [Fact]
    public void ToRegionId_UndefinedRegion_Throw()
    {
        // Arrange
        const DashScopeRegion undefinedRegion = (DashScopeRegion)99;

        // Act
        var act = () => undefinedRegion.ToRegionId();

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }
}
