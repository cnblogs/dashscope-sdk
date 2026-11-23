namespace Cnblogs.DashScope.Core;

/// <summary>
/// One content of the text rerank api: the query, a candidate document, or the document echoed
/// in a result. Exactly the set members are sent, a text is sent as a plain string when every
/// content of the request is a text (see <see cref="TextRerankInput"/>).
/// </summary>
/// <param name="Text">The text.</param>
/// <param name="Image">The image, a publicly accessible url or a base64 data uri (<c>data:image/{format};base64,{data}</c>). qwen3-vl-rerank only.</param>
/// <param name="Video">The video, a publicly accessible url. qwen3-vl-rerank only, not supported in the query.</param>
public record TextRerankContent(string? Text = null, string? Image = null, string? Video = null)
{
    /// <summary>
    /// Converts a text to a <see cref="TextRerankContent"/> implicitly.
    /// </summary>
    /// <param name="text">The text.</param>
    public static implicit operator TextRerankContent(string text) => new(Text: text);
}
