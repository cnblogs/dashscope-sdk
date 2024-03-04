namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The color stop of gradient in background generation result.
/// </summary>
/// <param name="Color">The color of current stop.</param>
/// <param name="Offset">The position of current stop.</param>
public record BackgroundGenerationTextResultGradientColorStop(string Color, float Offset);
