﻿using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a chat message between the user and the model.
/// </summary>
/// <param name="Role">The role of this message.</param>
/// <param name="Content">The content of this message.</param>
/// <param name="Name">Used when role is tool, represents the function name of this message generated by.</param>
/// <param name="ToolCalls">Calls to the function.</param>
[method: JsonConstructor]
public record ChatMessage(
    string Role,
    string Content,
    string? Name = null,
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
}
