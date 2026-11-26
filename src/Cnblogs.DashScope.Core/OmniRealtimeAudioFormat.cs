namespace Cnblogs.DashScope.Core;

/// <summary>
/// The format of the input or output audio of an omni realtime session,
/// <see cref="OmniRealtimeAudioInput.Format"/> and <see cref="OmniRealtimeAudioOutput.Format"/>.
/// Configure it before the first audio is sent, it cannot be changed afterwards.
/// </summary>
/// <param name="Type">The container: pcm (default, raw 16 bit mono PCM) or wav. The multichannel
/// input of qwen3.8-omni-flash-realtime takes pcm only.</param>
/// <param name="SampleRate">The sample rate in Hz: 8000, 16000 (default for the input), 24000
/// (default for the output) or 48000. The multichannel input of qwen3.8-omni-flash-realtime
/// takes 16000 only.</param>
/// <param name="SampleFormat">The sample format, s16le only. Multichannel input of
/// qwen3.8-omni-flash-realtime, defaults to s16le.</param>
/// <param name="Channels">The count of the input channels: 1 (default), 2 or 4. Multichannel input
/// of qwen3.8-omni-flash-realtime.</param>
/// <param name="Packing">How the samples of the channels are arranged, interleaved only.
/// Multichannel input of qwen3.8-omni-flash-realtime, defaults to interleaved.</param>
/// <param name="ChannelLayout">The layout of the channels: mono for 1 channel (default),
/// raw_mic_array for 2 and foa_ambix for 4 channels. Multichannel input of
/// qwen3.8-omni-flash-realtime, defaults by the channel count.</param>
public record OmniRealtimeAudioFormat(
    string? Type = null,
    int? SampleRate = null,
    string? SampleFormat = null,
    int? Channels = null,
    string? Packing = null,
    string? ChannelLayout = null);
