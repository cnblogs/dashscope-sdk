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
    /// Creates a <see cref="TextChatMessageContent"/> with text content.
    /// </summary>
    /// <param name="text">The text content.</param>
    public TextChatMessageContent(string text)
    {
        Text = text;
        DocUrls = null;
        FileParsingStrategy = null;
    }

    /// <summary>
    /// Creates a <see cref="TextChatMessageContent"/> with text content and doc urls.
    /// </summary>
    /// <param name="text">The text content.</param>
    /// <param name="docUrls">The doc urls.</param>
    /// <param name="fileParsingStrategy">Can be one of ['auto', 'text_only', 'text_and_images'].</param>
    public TextChatMessageContent(string text, IEnumerable<string>? docUrls, string? fileParsingStrategy)
    {
        Text = text;
        DocUrls = docUrls;
        FileParsingStrategy = fileParsingStrategy;
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
