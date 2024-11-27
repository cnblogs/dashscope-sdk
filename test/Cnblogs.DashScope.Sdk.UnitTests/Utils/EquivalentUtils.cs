using FluentAssertions;

namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

public static class EquivalentUtils
{
    internal static bool IsEquivalent<T>(this T left, T right)
    {
        try
        {
            left.Should().BeEquivalentTo(right);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
