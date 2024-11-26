using System.Diagnostics.CodeAnalysis;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents one content of a <see cref="MultimodalMessage"/>.
/// </summary>
/// <param name="Image">Image url.</param>
/// <param name="Text">Text content.</param>
/// <param name="Audio">Audio url.</param>
/// <param name="Video">Video urls.</param>
/// <param name="MinPixels">For qwen-vl-ocr only. Minimal pixels for ocr task.</param>
/// <param name="MaxPixels">For qwen-vl-ocr only. Maximum pixels for ocr task.</param>
public record MultimodalMessageContent(
    [StringSyntax(StringSyntaxAttribute.Uri)]
    string? Image = null,
    string? Text = null,
    [StringSyntax(StringSyntaxAttribute.Uri)]
    string? Audio = null,
    IEnumerable<string>? Video = null,
    int? MinPixels = null,
    int? MaxPixels = null)
{
    /// <summary>
    /// Represents an image content.
    /// </summary>
    /// <param name="url">Image url.</param>
    /// <param name="minPixels">For qwen-vl-ocr only. Minimal pixels for ocr task.</param>
    /// <param name="maxPixels">For qwen-vl-ocr only. Maximum pixels for ocr task.</param>
    /// <returns></returns>
    public static MultimodalMessageContent ImageContent(string url, int? minPixels = null, int? maxPixels = null)
    {
        return new MultimodalMessageContent(url, MinPixels: minPixels, MaxPixels: maxPixels);
    }

    /// <summary>
    /// Represents an image content.
    /// </summary>
    /// <param name="bytes">Image binary to sent using base64 data uri.</param>
    /// <param name="mediaType">Image media type.</param>
    /// <param name="minPixels">For qwen-vl-ocr only. Minimal pixels for ocr task.</param>
    /// <param name="maxPixels">For qwen-vl-ocr only. Maximum pixels for ocr task.</param>
    /// <returns></returns>
    public static MultimodalMessageContent ImageContent(
        ReadOnlySpan<byte> bytes,
        string mediaType,
        int? minPixels = null,
        int? maxPixels = null)
    {
        return ImageContent(
            $"data:{mediaType};base64,{Convert.ToBase64String(bytes)}",
            minPixels,
            maxPixels);
    }

    /// <summary>
    /// Represents a text content.
    /// </summary>
    /// <param name="text">Text content.</param>
    /// <returns></returns>
    public static MultimodalMessageContent TextContent(string text)
    {
        return new MultimodalMessageContent(Text: text);
    }

    /// <summary>
    /// Represents an audio content.
    /// </summary>
    /// <param name="audioUrl">The url of the audio.</param>
    /// <returns></returns>
    public static MultimodalMessageContent AudioContent(string audioUrl)
    {
        return new MultimodalMessageContent(Audio: audioUrl);
    }

    /// <summary>
    /// Represents video contents.
    /// </summary>
    /// <param name="videoUrls">The urls of the videos.</param>
    /// <returns></returns>
    public static MultimodalMessageContent VideoContent(IEnumerable<string> videoUrls)
    {
        return new MultimodalMessageContent(Video: videoUrls);
    }
}
