namespace Cnblogs.DashScope.Core;

/// <summary>
/// One content of the multimodal embedding api, a vector is computed from it or it takes part
/// in a fused vector (see <see cref="MultimodalEmbeddingInput"/>). Exactly the members that are
/// set are sent, a text always goes as a <c>{"text": ...}</c> object: the plain string element
/// the official document mentions is rejected by the tongyi-embedding-vision models, and a list
/// mixing strings and objects is rejected by every model.
/// </summary>
/// <param name="Text">The text.</param>
/// <param name="Image">The image, a publicly accessible url or a base64 data uri (<c>data:image/{format};base64,{data}</c>).</param>
/// <param name="Video">The video, a publicly accessible url, base64 is not supported.</param>
/// <param name="MultiImages">The images taken as one content, one vector is computed from all of them, in the same forms as <paramref name="Image"/>. tongyi-embedding-vision models only, the other models reject it.</param>
public record MultimodalEmbeddingContent(
    string? Text = null,
    string? Image = null,
    string? Video = null,
    List<string>? MultiImages = null)
{
    /// <summary>
    /// Converts a text to a <see cref="MultimodalEmbeddingContent"/> implicitly.
    /// </summary>
    /// <param name="text">The text.</param>
    public static implicit operator MultimodalEmbeddingContent(string text) => new(Text: text);
}
