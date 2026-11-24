using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A server event of the audio chat API (qwen-audio-3.x-realtime models). Which members are
/// present depends on <see cref="Type"/>. Consume the events via
/// <see cref="AudioChatSocketSession.GetEventsAsync"/>.
/// </summary>
/// <param name="EventId">The server side id of the event.</param>
/// <param name="Type">The event type: error, session.created, session.updated,
/// input_audio_buffer.speech_started, input_audio_buffer.speech_stopped,
/// input_audio_buffer.committed, input_audio_buffer.cleared, conversation.item.created,
/// conversation.item.deleted, conversation.item.retrieved,
/// conversation.item.input_audio_transcription.delta,
/// conversation.item.input_audio_transcription.completed,
/// conversation.item.input_audio_transcription.failed,
/// conversation.item.ambient_audio_transcription.delta,
/// conversation.item.ambient_audio_transcription.completed, response.created,
/// response.output_item.added, response.content_part.added, response.text.delta,
/// response.text.done, response.audio_transcript.delta, response.audio_transcript.done,
/// response.audio.delta, response.audio.done, response.content_part.done,
/// response.output_item.done, response.function_call_arguments.delta,
/// response.function_call_arguments.done, response.done, voiceprint_audio_list.in_progress,
/// voiceprint_audio_list.completed or voiceprint_audio_list.failed.</param>
/// <param name="Session">The session configuration. session.created / session.updated events.</param>
/// <param name="Error">The error detail. error and
/// conversation.item.input_audio_transcription.failed events.</param>
/// <param name="ItemId">The id of the item the event belongs to.</param>
/// <param name="PreviousItemId">The id of the item before the created one.
/// input_audio_buffer.committed and conversation.item.created events.</param>
/// <param name="AudioStartMs">The start timestamp of the detected speech, in milliseconds.
/// input_audio_buffer.speech_started events.</param>
/// <param name="AudioEndMs">The end timestamp of the detected speech, in milliseconds.
/// input_audio_buffer.speech_stopped events.</param>
/// <param name="Reason">The reason the turn was judged invalid. smart_turn
/// input_audio_buffer.speech_stopped events (turn_invalid) and
/// voiceprint_audio_list.failed events.</param>
/// <param name="Item">The item of the event. conversation.item.created /
/// conversation.item.retrieved / response.output_item.added / response.output_item.done
/// events.</param>
/// <param name="ContentIndex">The index of the content part within the item.</param>
/// <param name="OutputIndex">The index of the output item within the response.</param>
/// <param name="ResponseId">The id of the response the event belongs to.</param>
/// <param name="Text">The confirmed text so far (<paramref name="Stash"/> holds the part that
/// may still be revised). conversation.item.input_audio_transcription.delta,
/// conversation.item.ambient_audio_transcription.delta and response.text.done events.</param>
/// <param name="Stash">The tentative text that may still be revised.
/// conversation.item.input_audio_transcription.delta and
/// conversation.item.ambient_audio_transcription.delta events.</param>
/// <param name="Delta">The incremental content. response.text.delta,
/// response.audio_transcript.delta, response.audio.delta (base64 pcm),
/// response.function_call_arguments.delta events.</param>
/// <param name="Transcript">The final transcription. conversation.item.input_audio_transcription.completed,
/// conversation.item.ambient_audio_transcription.completed and response.audio_transcript.done
/// events.</param>
/// <param name="Part">The content part added or completed. response.content_part.added /
/// response.content_part.done events.</param>
/// <param name="Response">The response of the event. response.created / response.done events.</param>
/// <param name="CallId">The id of the function call. response.function_call_arguments.delta /
/// response.function_call_arguments.done events.</param>
/// <param name="Name">The name of the function. response.function_call_arguments.done events.</param>
/// <param name="Arguments">The complete arguments of the function call as a JSON string.
/// response.function_call_arguments.done events.</param>
/// <param name="Obfuscation">An undocumented per-event token, observed on
/// conversation.item.input_audio_transcription.delta events.</param>
public record AudioChatServerEvent(
    string? EventId = null,
    string? Type = null,
    [property: JsonConverter(typeof(AudioChatSessionJsonConverter))] AudioChatSession? Session = null,
    AudioChatError? Error = null,
    string? ItemId = null,
    string? PreviousItemId = null,
    int? AudioStartMs = null,
    int? AudioEndMs = null,
    string? Reason = null,
    AudioChatItem? Item = null,
    int? ContentIndex = null,
    int? OutputIndex = null,
    string? ResponseId = null,
    string? Text = null,
    string? Stash = null,
    string? Delta = null,
    string? Transcript = null,
    AudioChatItemContent? Part = null,
    AudioChatResponse? Response = null,
    string? CallId = null,
    string? Name = null,
    string? Arguments = null,
    string? Obfuscation = null);
