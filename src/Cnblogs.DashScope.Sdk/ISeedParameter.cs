namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Marks parameter supports seed option.
/// </summary>
public interface ISeedParameter
{
    /// <summary>
    /// The seed for randomizer, defaults to 1234 when null.
    /// </summary>
    public ulong? Seed { get; }
}
