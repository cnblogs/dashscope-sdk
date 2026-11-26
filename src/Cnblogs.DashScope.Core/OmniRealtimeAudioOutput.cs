namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output audio configuration of an omni realtime session.
/// </summary>
/// <param name="Format">The format of the output audio.</param>
/// <param name="Voice">The voice of the synthesized audio. qwen3.8-omni-flash-realtime, which
/// prefers it over <see cref="OmniRealtimeSession.Voice"/> when both are set.</param>
public record OmniRealtimeAudioOutput(
    OmniRealtimeAudioFormat? Format = null,
    string? Voice = null);
