namespace Cnblogs.DashScope.Core;

/// <summary>
/// A context message for speech transcription.
/// </summary>
/// <param name="Role">Message role: <c>user</c> or <c>assistant</c>.</param>
/// <param name="Content">Message contents.</param>
public record SpeechTranscriptionContextMessage(string Role, IReadOnlyList<SpeechTranscriptionContextContent> Content);
