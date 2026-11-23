namespace Cnblogs.DashScope.Core;

/// <summary>
/// A server event of the ASR realtime API (qwen3-asr-flash-realtime models). Which members are
/// present depends on <see cref="Type"/>. Consume the events via
/// <see cref="AsrRealtimeSocketSession.GetEventsAsync"/>.
/// </summary>
/// <param name="EventId">The server side id of the event.</param>
/// <param name="Type">The event type: session.created, session.updated,
/// input_audio_buffer.speech_started, input_audio_buffer.speech_stopped,
/// input_audio_buffer.committed, conversation.item.created,
/// conversation.item.input_audio_transcription.text,
/// conversation.item.input_audio_transcription.completed,
/// conversation.item.input_audio_transcription.failed, session.finished or error.</param>
/// <param name="Session">The session configuration. session.created / session.updated events.</param>
/// <param name="Error">The error detail. error events and
/// conversation.item.input_audio_transcription.failed events.</param>
/// <param name="ItemId">The id of the conversation item the event belongs to.</param>
/// <param name="PreviousItemId">The id of the previous item. input_audio_buffer.committed and
/// conversation.item.created events.</param>
/// <param name="AudioStartMs">The start timestamp of the detected speech, in milliseconds.
/// input_audio_buffer.speech_started events.</param>
/// <param name="AudioEndMs">The end timestamp of the detected speech, in milliseconds.
/// input_audio_buffer.speech_stopped events.</param>
/// <param name="Item">The created conversation item. conversation.item.created events.</param>
/// <param name="ContentIndex">The index of the content part within the item, always 0 so far.</param>
/// <param name="Language">The detected language of the audio. Transcription events.</param>
/// <param name="Emotion">The detected emotion of the audio. Transcription events.</param>
/// <param name="Text">The incremental transcription text. Empty in the events captured so far,
/// where the accumulated text arrives in <paramref name="Stash"/> instead.
/// conversation.item.input_audio_transcription.text events.</param>
/// <param name="Stash">The accumulated transcription of the item so far, which may still be
/// revised in later events (intermediate results). conversation.item.input_audio_transcription.text
/// events.</param>
/// <param name="Transcript">The final transcription of the item.
/// conversation.item.input_audio_transcription.completed events.</param>
/// <param name="Usage">The token usage of the transcription.
/// conversation.item.input_audio_transcription.completed events.</param>
public record AsrRealtimeServerEvent(
    string? EventId = null,
    string? Type = null,
    AsrRealtimeSession? Session = null,
    AsrRealtimeError? Error = null,
    string? ItemId = null,
    string? PreviousItemId = null,
    int? AudioStartMs = null,
    int? AudioEndMs = null,
    AsrRealtimeItem? Item = null,
    int? ContentIndex = null,
    string? Language = null,
    string? Emotion = null,
    string? Text = null,
    string? Stash = null,
    string? Transcript = null,
    AsrRealtimeUsage? Usage = null);
