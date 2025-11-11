namespace Cnblogs.DashScope.Core;

/// <summary>
/// Token details for multimodal inputs.
/// </summary>
/// <param name="ImageTokens">Token count of image.</param>
/// <param name="TextTokens">Token count of text.</param>
public record MultimodalInputTokenDetails(int? ImageTokens, int? TextTokens);
