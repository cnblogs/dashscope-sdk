namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// The pending wait for the confirmation of a conversation.item.create event of an omni realtime
/// session: the signal to complete and the type and role the created item must have.
/// </summary>
internal sealed record OmniRealtimeItemCreatedWait(
    TaskCompletionSource<OmniRealtimeServerEvent> Signal,
    string? Type,
    string? Role);
