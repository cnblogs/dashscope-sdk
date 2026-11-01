namespace Cnblogs.DashScope.Core;

/// <summary>
/// The prompt token details of text embedding request.
/// </summary>
/// <param name="ImageTokens">Token count of images, always 0 for text embedding.</param>
/// <param name="TextTokens">Token count of texts.</param>
public record TextEmbeddingPromptTokenDetails(int? ImageTokens = null, int? TextTokens = null);
