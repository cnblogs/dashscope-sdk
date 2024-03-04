namespace Cnblogs.DashScope.Sdk.Internals;

internal interface ISeedParameter
{
    /// <summary>
    /// The seed for randomizer, defaults to 1234 when null.
    /// </summary>
    public ulong? Seed { get; }
}
