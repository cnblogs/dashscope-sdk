namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The gradient of background generation text.
/// </summary>
/// <param name="Type">Type of gradient. e.g. <c>linear</c></param>
/// <param name="GradientUnits">Unit of gradient. e.g. <c>pixels</c></param>
/// <param name="ColorStops">Color stops of gradient.</param>
public record BackgroundGenerationTextResultGradient(
    string Type,
    string GradientUnits,
    List<BackgroundGenerationTextResultGradientColorStop> ColorStops)
{
    /// <summary>
    /// Coords of each stop, use "x1", "y1", "x2", "y2"... to access the coords.
    /// </summary>
    public Dictionary<string, int>? Coords { get; set; }
}
