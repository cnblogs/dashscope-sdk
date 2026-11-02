using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Content of the <see cref="TextChatMessage"/>.
/// </summary>
[JsonConverter(typeof(TextChatMessageContentConvertor))]
public class TextChatMessageContent
{
    /// <summary>
    /// The text part of the content.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Optional doc urls.
    /// </summary>
    public IEnumerable<string>? DocUrls { get; }

    /// <summary>
    /// Control the content that model can read. Can be one of ['auto', 'text_only', 'text_and_images'].
    /// </summary>
    public string? FileParsingStrategy { get; }

    /// <summary>
    /// Mark a cache breakpoint on this content block, the model caches the prefix
    /// from the start of the messages array to this content block. Only supported
    /// by models that support explicit context cache.
    /// </summary>
    public CacheControlOptions? CacheControl { get; }

    /// <summary>
    /// Creates a <see cref="TextChatMessageContent"/> with text content.
    /// </summary>
    /// <param name="text">The text content.</param>
    public TextChatMessageContent(string text)
    {
        Text = text;
        DocUrls = null;
        FileParsingStrategy = null;
        CacheControl = null;
    }

    /// <summary>
    /// Creates a <see cref="TextChatMessageContent"/> with text content and an explicit cache breakpoint.
    /// </summary>
    /// <param name="text">The text content.</param>
    /// <param name="cacheControl">Mark an explicit cache breakpoint on the text content.</param>
    public TextChatMessageContent(string text, CacheControlOptions cacheControl)
    {
        Text = text;
        DocUrls = null;
        FileParsingStrategy = null;
        CacheControl = cacheControl;
    }

    /// <summary>
    /// Creates a <see cref="TextChatMessageContent"/> with text content and doc urls.
    /// </summary>
    /// <param name="text">The text content.</param>
    /// <param name="docUrls">The doc urls.</param>
    /// <param name="fileParsingStrategy">Can be one of ['auto', 'text_only', 'text_and_images'].</param>
    /// <param name="cacheControl">Mark an explicit cache breakpoint on the text content.</param>
    public TextChatMessageContent(
        string text,
        IEnumerable<string>? docUrls,
        string? fileParsingStrategy,
        CacheControlOptions? cacheControl = null)
    {
        Text = text;
        DocUrls = docUrls;
        FileParsingStrategy = fileParsingStrategy;
        CacheControl = cacheControl;
    }

    /// <summary>
    /// Convert string to TextChatMessageContent implicitly.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    /// <returns></returns>
    public static implicit operator TextChatMessageContent(string value) => new(value);

    /// <summary>
    /// Convert to string implicitly.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns></returns>
    public static implicit operator string(TextChatMessageContent value) => value.Text;

    /// <inheritdoc />
    public override string ToString()
    {
        return Text;
    }
}
