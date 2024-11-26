﻿using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a chat message between the user and the model.
/// </summary>
/// <param name="Role">The role of this message.</param>
/// <param name="Content">The content of this message.</param>
/// <param name="Name">Used when role is tool, represents the function name of this message generated by.</param>
/// <param name="Partial">Notify model that next message should use this message as prefix.</param>
/// <param name="ToolCalls">Calls to the function.</param>
[method: JsonConstructor]
public record ChatMessage(
    string Role,
    string Content,
    string? Name = null,
    bool? Partial = null,
    List<ToolCall>? ToolCalls = null) : IMessage<string>
{
    /// <summary>
    /// Create chat message from an uploaded DashScope file.
    /// </summary>
    /// <param name="fileId">The id of the file.</param>
    public ChatMessage(DashScopeFileId fileId)
        : this("system", fileId.ToUrl())
    {
    }

    /// <summary>
    /// Create chat message from multiple DashScope file.
    /// </summary>
    /// <param name="fileIds">Ids of the files.</param>
    public ChatMessage(IEnumerable<DashScopeFileId> fileIds)
        : this("system", string.Join(',', fileIds.Select(f => f.ToUrl())))
    {
    }

    /// <summary>
    /// Creates a file message.
    /// </summary>
    /// <param name="fileId">The id of the file.</param>
    /// <returns></returns>
    public static ChatMessage File(DashScopeFileId fileId)
    {
        return new ChatMessage(fileId);
    }

    /// <summary>
    /// Creates a file message.
    /// </summary>
    /// <param name="fileIds">The file id list.</param>
    /// <returns></returns>
    public static ChatMessage File(IEnumerable<DashScopeFileId> fileIds)
    {
        return new ChatMessage(fileIds);
    }

    /// <summary>
    /// Create a user message.
    /// </summary>
    /// <param name="content">Content of the message.</param>
    /// <param name="name">Author name.</param>
    /// <returns></returns>
    public static ChatMessage User(string content, string? name = null)
    {
        return new ChatMessage(DashScopeRoleNames.User, content, name);
    }

    /// <summary>
    /// Create a system message.
    /// </summary>
    /// <param name="content">The content of the message.</param>
    /// <returns></returns>
    public static ChatMessage System(string content)
    {
        return new ChatMessage(DashScopeRoleNames.System, content);
    }

    /// <summary>
    /// Create an assistant message
    /// </summary>
    /// <param name="content">The content of the message.</param>
    /// <param name="partial">When set to true, content of this message would be the prefix of next model output.</param>
    /// <param name="name">Author name.</param>
    /// <param name="toolCalls">Tool calls by model.</param>
    /// <returns></returns>
    public static ChatMessage Assistant(string content, bool? partial = null, string? name = null, List<ToolCall>? toolCalls = null)
    {
        return new ChatMessage(DashScopeRoleNames.Assistant, content, name, partial, toolCalls);
    }

    /// <summary>
    /// Create a tool message.
    /// </summary>
    /// <param name="content">The output from tool.</param>
    /// <param name="name">The name of the tool.</param>
    /// <returns></returns>
    public static ChatMessage Tool(string content, string? name = null)
    {
        return new ChatMessage(DashScopeRoleNames.Tool, content, name);
    }
}
