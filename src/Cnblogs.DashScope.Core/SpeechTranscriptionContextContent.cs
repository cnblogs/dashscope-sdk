namespace Cnblogs.DashScope.Core;

/// <summary>
/// Content item of a speech transcription context message.
/// </summary>
/// <param name="Type">Content type: <c>input_text</c> or <c>text</c>.</param>
/// <param name="Text">Text value for the content type.</param>
public record SpeechTranscriptionContextContent(string Type, string Text)
{
    /// <summary>
    /// Create an <c>input_text</c> content (user context / hot phrases).
    /// </summary>
    /// <param name="text">The text content.</param>
    /// <returns>The content item.</returns>
    public static SpeechTranscriptionContextContent InputText(string text)
        => new("input_text", text);

    /// <summary>
    /// Create a <c>text</c> content (assistant reply context).
    /// </summary>
    /// <param name="text">The text content.</param>
    /// <returns>The content item.</returns>
    public static SpeechTranscriptionContextContent TextContent(string text)
        => new("text", text);
}
