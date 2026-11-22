namespace Cnblogs.DashScope.Core;

/// <summary>
/// A text content of a <see cref="SpeechRecognitionContext"/> message.
/// </summary>
/// <param name="Type">The content type: input_text for user messages, text for assistant messages.</param>
/// <param name="Text">The text content.</param>
public record SpeechRecognitionContextContent(string Type, string Text);
