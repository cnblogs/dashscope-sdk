namespace Cnblogs.DashScope.Core;

/// <summary>
/// The video input configuration of an omni realtime session. qwen3.8-omni-flash-realtime.
/// </summary>
/// <param name="Input">The video input parameters.</param>
public record OmniRealtimeVideo(
    OmniRealtimeVideoInput? Input = null);
