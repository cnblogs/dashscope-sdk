using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A message for speech recognition flash API.
/// </summary>
/// <param name="Role">Message role: <c>user</c> or <c>assistant</c>.</param>
/// <param name="Content">Message contents.</param>
public record SpeechRecognitionMessage(string Role, IReadOnlyList<SpeechRecognitionMessageContent> Content)
{
    /// <summary>
    /// Create a user message.
    /// </summary>
    /// <param name="contents">Message contents.</param>
    /// <returns>The user message.</returns>
    public static SpeechRecognitionMessage User(IReadOnlyList<SpeechRecognitionMessageContent> contents)
        => new(DashScopeRoleNames.User, contents);

    /// <summary>
    /// Create an assistant message.
    /// </summary>
    /// <param name="contents">Message contents.</param>
    /// <returns>The assistant message.</returns>
    public static SpeechRecognitionMessage Assistant(IReadOnlyList<SpeechRecognitionMessageContent> contents)
        => new(DashScopeRoleNames.Assistant, contents);

    internal bool IsOss() => Content.Any(c => c.IsOss());
}
