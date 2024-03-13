using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents a chat message between the user and the model.
/// </summary>
/// <param name="Role">The role of this message.</param>
/// <param name="Content">The content of this message.</param>
/// <param name="ToolCalls">Calls to the function.</param>
public record ChatMessage(string Role, string Content, List<ToolCall>? ToolCalls = null) : IMessage<string>;
