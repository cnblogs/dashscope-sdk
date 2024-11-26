using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a multimodal message.
/// </summary>
/// <param name="Role">The role associated with this message.</param>
/// <param name="Content">The contents of this message.</param>
public record MultimodalMessage(string Role, IReadOnlyList<MultimodalMessageContent> Content)
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
    /// <returns></returns>
    public static MultimodalMessage Assistant(IReadOnlyList<MultimodalMessageContent> contents)
    {
        return new MultimodalMessage(DashScopeRoleNames.Assistant, contents);
    }
}
