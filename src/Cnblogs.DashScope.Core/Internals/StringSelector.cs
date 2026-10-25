namespace Cnblogs.DashScope.Core.Internals;

internal static class StringSelector
{
    public static string DefaultIfNullOrWhitespace(this string? value, Func<string> factory)
        => string.IsNullOrWhiteSpace(value) ? factory() : value;
}
