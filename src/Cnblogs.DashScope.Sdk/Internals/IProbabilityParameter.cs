namespace Cnblogs.DashScope.Sdk.Internals;

internal interface IProbabilityParameter
{
    /// <summary>
    /// The probability threshold during generation, defaults to 0.8 when null.
    /// </summary>
    public float? TopP { get; }

    /// <summary>
    /// The size of the candidate set for sampling during generation.
    /// A larger value increases the randomness of the generated results.
    /// </summary>
    /// <remarks>top_k would been disabled if applied null or any value larger than 100.</remarks>
    public int? TopK { get; }
}
