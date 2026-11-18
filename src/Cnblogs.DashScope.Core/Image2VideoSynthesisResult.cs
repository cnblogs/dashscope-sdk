namespace Cnblogs.DashScope.Core;

/// <summary>
/// The result of an animate video synthesis task
/// (wan2.2-animate-move / wan2.2-animate-mix models).
/// </summary>
/// <param name="VideoUrl">The url of the generated video.</param>
public record Image2VideoSynthesisResult(string? VideoUrl = null);
