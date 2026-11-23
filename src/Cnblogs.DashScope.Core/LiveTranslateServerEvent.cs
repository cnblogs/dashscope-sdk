namespace Cnblogs.DashScope.Core;

/// <summary>
/// A server event of the live translation API (qwen3.5 / qwen3.8-livetranslate-flash-realtime).
/// Which members are present depends on <see cref="Type"/>. Consume the events via
/// <see cref="LiveTranslateSocketSession.GetEventsAsync"/>.
/// </summary>
/// <param name="EventId">The server side id of the event.</param>
/// <param name="Type">The event type: error, session.created, session.updated, session.finished,
/// response.created, response.done, response.text.delta, response.text.text, response.text.done,
/// response.audio.delta, response.audio.done, response.audio_transcript.delta,
/// response.audio_transcript.text, response.audio_transcript.done, response.output_item.added,
/// response.output_item.done, response.content_part.added, response.content_part.done,
/// input_audio_buffer.speech_started, input_audio_buffer.speech_stopped,
/// input_audio_buffer.committed, input_audio_buffer.cleared, conversation.item.created,
/// conversation.item.input_audio_transcription.delta,
/// conversation.item.input_audio_transcription.text,
/// conversation.item.input_audio_transcription.completed or
/// conversation.item.input_audio_transcription.failed.</param>
/// <param name="Session">The session configuration. session.created / session.updated events.</param>
/// <param name="Error">The error detail. error and
/// conversation.item.input_audio_transcription.failed events.</param>
/// <param name="Response">The response of the event. response.created / response.done events.</param>
/// <param name="ResponseId">The id of the response the event belongs to.</param>
/// <param name="ItemId">The id of the item the event belongs to.</param>
/// <param name="PreviousItemId">The id of the item before the created one.
/// conversation.item.created events - the id of the source recognition item, correlating the
/// source text and the translation.</param>
/// <param name="OutputIndex">The index of the output item within the response.</param>
/// <param name="ContentIndex">The index of the content part within the item.</param>
/// <param name="Delta">The incremental content. response.text.delta,
/// response.audio_transcript.delta, response.audio.delta (base64 pcm) and
/// conversation.item.input_audio_transcription.delta events.</param>
/// <param name="Text">The confirmed text so far (<paramref name="Stash"/> holds the part that
/// may still be revised). response.text.text, response.audio_transcript.text,
/// conversation.item.input_audio_transcription.text and response.text.done events.</param>
/// <param name="Stash">The tentative text that may still be revised. response.text.text,
/// response.audio_transcript.text and conversation.item.input_audio_transcription.text events.</param>
/// <param name="Transcript">The final translation or transcription.
/// response.audio_transcript.done and
/// conversation.item.input_audio_transcription.completed events.</param>
/// <param name="Item">The item of the event. conversation.item.created,
/// response.output_item.added and response.output_item.done events.</param>
/// <param name="Part">The content part added or completed. response.content_part.added /
/// response.content_part.done events.</param>
/// <param name="Language">The detected language of the audio.
/// conversation.item.input_audio_transcription.text / completed events.</param>
/// <param name="Emotion">The detected emotion of the audio.
/// conversation.item.input_audio_transcription.text / completed events.</param>
/// <param name="AudioStartMs">The start timestamp of the detected speech, in milliseconds.
/// input_audio_buffer.speech_started events.</param>
/// <param name="AudioEndMs">The end timestamp of the detected speech, in milliseconds.
/// input_audio_buffer.speech_stopped events.</param>
public record LiveTranslateServerEvent(
    string? EventId = null,
    string? Type = null,
    LiveTranslateSession? Session = null,
    LiveTranslateError? Error = null,
    LiveTranslateResponse? Response = null,
    string? ResponseId = null,
    string? ItemId = null,
    string? PreviousItemId = null,
    int? OutputIndex = null,
    int? ContentIndex = null,
    string? Delta = null,
    string? Text = null,
    string? Stash = null,
    string? Transcript = null,
    LiveTranslateItem? Item = null,
    LiveTranslateItemContent? Part = null,
    string? Language = null,
    string? Emotion = null,
    int? AudioStartMs = null,
    int? AudioEndMs = null);
