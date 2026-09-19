namespace Cnblogs.DashScope.Core;

/// <summary>
/// Content item of a speech recognition message.
/// </summary>
/// <param name="Type">Content type: <c>input_audio</c>, <c>input_text</c>, or <c>text</c>.</param>
/// <param name="Text">Text when type is <c>input_text</c> or <c>text</c>.</param>
/// <param name="InputAudio">Audio payload when type is <c>input_audio</c>.</param>
public record SpeechRecognitionMessageContent(
    string Type,
    string? Text = null,
    SpeechRecognitionInputAudio? InputAudio = null)
{
    private const string OssSchema = "oss://";

    /// <summary>
    /// Create an audio content from URL or Data URI.
    /// </summary>
    /// <param name="data">Public URL or Base64 Data URI.</param>
    /// <returns>The content item.</returns>
    public static SpeechRecognitionMessageContent InputAudioContent(string data)
        => new("input_audio", InputAudio: new SpeechRecognitionInputAudio(data));

    /// <summary>
    /// Create an audio content from binary bytes as Data URI.
    /// </summary>
    /// <param name="bytes">Audio binary.</param>
    /// <param name="mediaType">MIME type of the audio.</param>
    /// <returns>The content item.</returns>
    public static SpeechRecognitionMessageContent InputAudioContent(ReadOnlySpan<byte> bytes, string mediaType)
        => InputAudioContent($"data:{mediaType};base64,{Convert.ToBase64String(bytes)}");

    /// <summary>
    /// Create an <c>input_text</c> context content.
    /// </summary>
    /// <param name="text">The text content.</param>
    /// <returns>The content item.</returns>
    public static SpeechRecognitionMessageContent InputText(string text)
        => new("input_text", Text: text);

    /// <summary>
    /// Create a <c>text</c> (assistant) context content.
    /// </summary>
    /// <param name="text">The text content.</param>
    /// <returns>The content item.</returns>
    public static SpeechRecognitionMessageContent TextContent(string text)
        => new("text", Text: text);

    internal bool IsOss()
        => InputAudio?.Data?.StartsWith(OssSchema, StringComparison.Ordinal) == true;
}

/// <summary>
/// Audio input for speech recognition flash API.
/// </summary>
/// <param name="Data">Public URL or Base64 Data URI.</param>
public record SpeechRecognitionInputAudio(string Data);
