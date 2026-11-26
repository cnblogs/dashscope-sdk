namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input audio configuration of an omni realtime session.
/// </summary>
/// <param name="Format">The format of the input audio.</param>
public record OmniRealtimeAudioInput(
    OmniRealtimeAudioFormat? Format = null);
