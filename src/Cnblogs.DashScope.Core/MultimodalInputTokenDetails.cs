namespace Cnblogs.DashScope.Core;

/// <summary>
/// Token details for multimodal inputs.
/// </summary>
/// <param name="ImageTokens">Token count of image.</param>
/// <param name="VideoTokens">Token count of video.</param>
/// <param name="TextTokens">Token count of text.</param>
public record MultimodalInputTokenDetails(int? ImageTokens = null, int? VideoTokens = null, int? TextTokens = null);
