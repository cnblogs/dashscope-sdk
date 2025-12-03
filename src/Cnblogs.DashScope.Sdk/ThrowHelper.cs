using System.Diagnostics.CodeAnalysis;

namespace Cnblogs.DashScope.Sdk
{
    internal static class ThrowHelper
    {
        [DoesNotReturn]
        public static string UnknownModelName(string argumentName, object value)
        {
            throw new ArgumentOutOfRangeException(
                argumentName,
                value,
                "Unknown model type, please use the overload that accepts a string ‘model’ parameter.");
        }
    }
}
