namespace Cnblogs.DashScope.Core;

/// <summary>
/// The audio configuration of an omni realtime session, preferred over the
/// <see cref="OmniRealtimeSession.InputAudioFormat"/> / <see cref="OmniRealtimeSession.OutputAudioFormat"/>
/// members it configures with the sample rates at once.
/// </summary>
/// <param name="Input">The input audio configuration.</param>
/// <param name="Output">The output audio configuration.</param>
public record OmniRealtimeAudio(
    OmniRealtimeAudioInput? Input = null,
    OmniRealtimeAudioOutput? Output = null);
