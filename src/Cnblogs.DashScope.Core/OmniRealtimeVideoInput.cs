namespace Cnblogs.DashScope.Core;

/// <summary>
/// The video input configuration of an omni realtime session.
/// </summary>
/// <param name="RepresentationCompact">How the video frames (sent with
/// <see cref="OmniRealtimeSocketSession.AppendImageAsync(ReadOnlyMemory{byte}, CancellationToken)"/>)
/// are represented: none (default) keeps the full detail, normal aggregates them to a quarter of
/// the tokens. Set it before the first audio is sent, it cannot be changed afterwards.</param>
public record OmniRealtimeVideoInput(
    string? RepresentationCompact = null);
