namespace Cnblogs.DashScope.Core;

/// <summary>
/// The breakdown of the input tokens of a multimodal embedding request, returned by the
/// tongyi-embedding-vision models.
/// </summary>
/// <param name="ImageTokens">The token count of the images and videos.</param>
/// <param name="TextTokens">The token count of the texts.</param>
public record MultimodalEmbeddingInputTokenDetails(int ImageTokens, int TextTokens);
