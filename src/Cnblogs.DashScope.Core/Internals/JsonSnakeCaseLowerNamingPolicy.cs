namespace Cnblogs.DashScope.Core.Internals;

internal class JsonSnakeCaseLowerNamingPolicy : JsonSeparatorNamingPolicy
{
    public static readonly JsonSnakeCaseLowerNamingPolicy SnakeCaseLower = new();

    private JsonSnakeCaseLowerNamingPolicy()
        : base(true, '_')
    {
    }
}
