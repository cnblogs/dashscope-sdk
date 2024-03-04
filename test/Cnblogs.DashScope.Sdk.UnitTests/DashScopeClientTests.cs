using FluentAssertions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeClientTests
{
    [Fact]
    public void DashScopeClient_Constructor_New()
    {
        // Arrange
        const string apiKey = "apiKey";

        // Act
        var act = () => new DashScopeClient(apiKey);

        // Assert
        act.Should().NotThrow();
    }
}
