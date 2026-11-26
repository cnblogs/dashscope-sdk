namespace Cnblogs.DashScope.Core;

/// <summary>
/// The status details of an omni realtime response, present when the response was cancelled.
/// </summary>
/// <param name="Type">The type of the status, cancelled.</param>
/// <param name="Reason">The reason of the cancellation, client_cancelled for a response.cancel
/// event of the client.</param>
public record OmniRealtimeStatusDetails(
    string? Type = null,
    string? Reason = null);
