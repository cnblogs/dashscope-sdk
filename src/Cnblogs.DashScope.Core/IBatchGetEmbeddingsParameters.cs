namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameter of batch get embeddings request.
/// </summary>
/// <remarks>
/// The batch embedding API only supports <see cref="TextType"/>, other parameters of
/// <see cref="ITextEmbeddingParameters"/> like <c>dimension</c> or <c>output_type</c> are
/// silently ignored by the server.
/// </remarks>
public interface IBatchGetEmbeddingsParameters
{
    /// <summary>
    /// The text type("query" or "document"). Defaults to "document".
    /// </summary>
    string? TextType { get; set; }
}
