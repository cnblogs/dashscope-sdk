namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents output of multi-model generation.
/// </summary>
/// <param name="Choices">The generated message.</param>
public record MultimodalOutput(List<MultimodalChoice> Choices);
