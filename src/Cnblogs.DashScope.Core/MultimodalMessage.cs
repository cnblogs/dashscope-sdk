using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a multimodal message.
/// </summary>
/// <param name="Role">The role associated with this message.</param>
/// <param name="Content">The contents of this message.</param>
/// <param name="ReasoningContent">Thoughts from the model.</param>
/// <param name="Annotations">Language annotations from the model.</param>
/// <param name="ToolCalls">Function call requests from the model.</param>
/// <param name="ToolCallId">Tool call id for tool message.</param>
public record MultimodalMessage(
    string Role,
    IReadOnlyList<MultimodalMessageContent> Content,
    string? ReasoningContent = null,
    IReadOnlyList<MultimodalAnnotation>? Annotations = null,
    List<ToolCall>? ToolCalls = null,
    string? ToolCallId = null)
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
    /// <param name="toolCalls">Tool calls from the model.</param>
    /// <returns></returns>
    public static MultimodalMessage Assistant(
        IReadOnlyList<MultimodalMessageContent> contents,
        string? reasoningContent = null,
        List<ToolCall>? toolCalls = null)
    {
        return new MultimodalMessage(DashScopeRoleNames.Assistant, contents, reasoningContent, ToolCalls: toolCalls);
    }

    /// <summary>
    /// Creates a tool message.
    /// </summary>
    /// <param name="contents">Message contents</param>
    /// <param name="toolCallId">ID of the call.</param>
    /// <returns></returns>
    public static MultimodalMessage Tool(IReadOnlyList<MultimodalMessageContent> contents, string? toolCallId = null)
    {
        return new MultimodalMessage(DashScopeRoleNames.Tool, contents, ToolCallId: toolCallId);
    }

    internal bool IsOss() => Content.Any(c => c.IsOss());
}
