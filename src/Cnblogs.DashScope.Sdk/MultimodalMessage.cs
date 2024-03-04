using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents a multimodal message.
/// </summary>
/// <param name="Role">The role associated with this message.</param>
/// <param name="Content">The contents of this message.</param>
public record MultimodalMessage(string Role, IReadOnlyList<MultimodalMessageContent> Content)
    : IMessage<IReadOnlyList<MultimodalMessageContent>>;
