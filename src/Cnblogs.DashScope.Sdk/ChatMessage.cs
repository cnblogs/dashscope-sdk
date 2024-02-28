using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents a chat message between the user and the model.
/// </summary>
public record ChatMessage(string Role, string Content) : IMessage<string>;
