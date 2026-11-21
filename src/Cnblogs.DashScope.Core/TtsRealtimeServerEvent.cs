namespace Cnblogs.DashScope.Core;

/// <summary>
/// A server event of the TTS realtime API (qwen3-tts-*-realtime models). Which members are
/// present depends on <see cref="Type"/>. Consume the events via
/// <see cref="TtsRealtimeSocketSession.GetEventsAsync"/>.
/// </summary>
/// <param name="EventId">The server side id of the event.</param>
/// <param name="Type">The event type: session.created, session.updated, input_text_buffer.committed, input_text_buffer.cleared, response.created, response.output_item.added, response.content_part.added, response.audio.delta, response.audio.done, response.content_part.done, response.output_item.done, response.done, session.finished or error.</param>
/// <param name="Session">The session configuration. session.created / session.updated events.</param>
/// <param name="Error">The error detail. error events.</param>
/// <param name="ItemId">The id of the message item the event belongs to.</param>
/// <param name="OutputIndex">The index of the output item, always 0 for now.</param>
/// <param name="ContentIndex">The index of the content part within the output item, always 0 for now.</param>
/// <param name="Item">The output item. response.output_item.added / done events.</param>
/// <param name="Part">The content part. response.content_part.added / done events.</param>
/// <param name="Response">The response. response.created / done events, carries the usage in response.done.</param>
/// <param name="Delta">The base64 encoded audio chunk. response.audio.delta events, use <see cref="TtsRealtimeSocketSession.GetAudioAsync"/> for the decoded audio stream.</param>
public record TtsRealtimeServerEvent(
    string? EventId = null,
    string? Type = null,
    TtsRealtimeSession? Session = null,
    TtsRealtimeError? Error = null,
    string? ItemId = null,
    int? OutputIndex = null,
    int? ContentIndex = null,
    TtsRealtimeItem? Item = null,
    TtsRealtimeContentPart? Part = null,
    TtsRealtimeResponse? Response = null,
    string? Delta = null);
