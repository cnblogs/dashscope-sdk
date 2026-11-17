namespace Cnblogs.DashScope.Core;

/// <summary>
/// One color of the palette for wan2.7 image models.
/// </summary>
/// <param name="Hex">The color in hex format, e.g. "#FF0000".</param>
/// <param name="Ratio">The percentage of the color, e.g. "25.00%". All ratios must sum up to 100.00%.</param>
public record MultimodalColorPalette(string Hex, string Ratio);
