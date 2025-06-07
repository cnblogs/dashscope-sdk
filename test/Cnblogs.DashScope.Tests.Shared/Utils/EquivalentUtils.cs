using FluentAssertions;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static class EquivalentUtils
{
    public static bool IsEquivalent<T>(this T left, T right)
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
