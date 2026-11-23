namespace Cnblogs.DashScope.Core;

/// <summary>
/// The status details of an audio chat response, present when the response is cancelled
/// or failed.
/// </summary>
/// <param name="Type">The type of the status, e.g. cancelled.</param>
/// <param name="Reason">The reason of the cancellation: turn_detected (interrupted by new
/// speech) or client_cancelled (response.cancel).</param>
public record AudioChatStatusDetails(
    string? Type = null,
    string? Reason = null);
