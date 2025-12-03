using Xunit;

namespace Cnblogs.DashScope.Tests.Shared.Utils
{
    public static class EquivalentUtils
    {
        public static bool IsEquivalent<T>(this T left, T right)
        {
            try
            {
                Assert.Equivalent(right, left);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
