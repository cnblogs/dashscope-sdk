using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input audio transcription configuration of an omni realtime session. The transcription is
/// enabled by default (the recognized text arrives in the conversation.item.input_audio_transcription.*
/// events); a null <see cref="OmniRealtimeSession.InputAudioTranscription"/> disables it.
/// </summary>
/// <param name="Model">The transcription model, fixed to qwen3-asr-flash-realtime.</param>
public record OmniRealtimeTranscription(string? Model = null)
{
    /// <summary>
    /// Stands for leaving the input_audio_transcription member out of the session.update event,
    /// the default of <see cref="OmniRealtimeSession.InputAudioTranscription"/>: the server keeps
    /// the transcription it applies by itself (enabled). Recognized by reference, do not modify it.
    /// </summary>
    public static readonly OmniRealtimeTranscription Omit = new();
}
