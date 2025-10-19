using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a multimodal message.
/// </summary>
/// <param name="Role">The role associated with this message.</param>
/// <param name="Content">The contents of this message.</param>
/// <param name="ReasoningContent">Thoughts from the model.</param>
/// <param name="Annotations">Language annotations from the model.</param>
public record MultimodalMessage(
    string Role,
    IReadOnlyList<MultimodalMessageContent> Content,
    string? ReasoningContent = null,
    IReadOnlyList<MultimodalAnnotation>? Annotations = null)
    : IMessage<IReadOnlyList<MultimodalMessageContent>>
{
    /// <summary>
    /// Create a user message.
    /// </summary>
    /// <param name="contents">Message contents.</param>
    /// <returns></returns>
    public static MultimodalMessage User(IReadOnlyList<MultimodalMessageContent> contents)
    {
        return new MultimodalMessage(DashScopeRoleNames.User, contents);
    }

    /// <summary>
    /// Create a system message.
    /// </summary>
    /// <param name="contents">Message contents.</param>
    /// <returns></returns>
    public static MultimodalMessage System(IReadOnlyList<MultimodalMessageContent> contents)
    {
        return new MultimodalMessage(DashScopeRoleNames.System, contents);
    }

    /// <summary>
    /// Creates an assistant message.
    /// </summary>
    /// <param name="contents">Message contents.</param>
    /// <param name="reasoningContent">Thoughts from the model.</param>
    /// <returns></returns>
    public static MultimodalMessage Assistant(
        IReadOnlyList<MultimodalMessageContent> contents,
        string? reasoningContent = null)
    {
        return new MultimodalMessage(DashScopeRoleNames.Assistant, contents, reasoningContent);
    }

    internal bool IsOss() => Content.Any(c => c.IsOss());
}
