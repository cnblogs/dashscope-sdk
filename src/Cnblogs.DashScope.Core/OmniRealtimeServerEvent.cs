using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// A server event of an omni realtime session (qwen3.8-omni-flash-realtime,
/// qwen3.5-omni-plus-realtime, qwen3.5-omni-flash-realtime). Which members are present depends on
/// <see cref="Type"/>. Consume the events via
/// <see cref="OmniRealtimeSocketSession.GetEventsAsync"/>.
/// </summary>
/// <param name="EventId">The server side id of the event.</param>
/// <param name="Type">The event type: error, session.created, session.updated,
/// input_audio_buffer.speech_started, input_audio_buffer.speech_stopped,
/// input_audio_buffer.committed, input_audio_buffer.cleared, conversation.item.created,
/// conversation.item.deleted, conversation.item.input_audio_transcription.delta,
/// conversation.item.input_audio_transcription.completed,
/// conversation.item.input_audio_transcription.failed, response.created,
/// response.output_item.added, response.content_part.added, response.text.delta,
/// response.text.done, response.audio_transcript.delta, response.audio_transcript.done,
/// response.audio.delta, response.audio.done, response.function_call_arguments.delta,
/// response.function_call_arguments.done, response.content_part.done,
/// response.output_item.done, response.done, mcp_list_tools.in_progress,
/// mcp_list_tools.completed, mcp_list_tools.failed, response.mcp_call_arguments.delta,
/// response.mcp_call_arguments.done, response.mcp_call.in_progress,
/// response.mcp_call.completed or response.mcp_call.failed. The mcp_* events and the tool
/// events of the remote MCP servers belong to qwen3.8-omni-flash-realtime.</param>
/// <param name="Session">The session configuration. session.created / session.updated events.</param>
/// <param name="Error">The error detail. error and
/// conversation.item.input_audio_transcription.failed events.</param>
/// <param name="ResponseId">The id of the response the event belongs to. response.* events and
/// conversation.item.created events of an MCP approval request.</param>
/// <param name="ItemId">The id of the item the event belongs to: the user message item of the
/// buffer and transcription events, the output item of the response.content_part.* and text
/// events, the mcp_list_tools item of the mcp_list_tools.* events and the mcp_call item of the
/// response.mcp_call.* events. It equals the id of the approval request in a
/// conversation.item.created event of an MCP approval request.</param>
/// <param name="PreviousItemId">The id of the item before the created one: the user message item
/// of the response to it in conversation.item.created events, the mcp_call item of its approval
/// request, the function_call item of its output.</param>
/// <param name="OutputIndex">The index of the output item within the response.</param>
/// <param name="ContentIndex">The index of the content part within the item, 0 so far.</param>
/// <param name="Delta">The incremental content: response.text.delta,
/// response.audio_transcript.delta, response.function_call_arguments.delta,
/// response.mcp_call_arguments.delta, conversation.item.input_audio_transcription.delta and
/// response.audio.delta events (base64 audio, use
/// <see cref="OmniRealtimeSocketSession.GetAudioAsync"/> for the decoded audio stream).</param>
/// <param name="Text">The confirmed text so far (<paramref name="Stash"/> holds the part that may
/// still be revised) in conversation.item.input_audio_transcription.delta events, the complete
/// text in response.text.done events.</param>
/// <param name="Stash">The tentative text that may still be revised.
/// conversation.item.input_audio_transcription.delta events.</param>
/// <param name="Transcript">The final text: the recognized user speech in
/// conversation.item.input_audio_transcription.completed events, the reply in
/// response.audio_transcript.done events.</param>
/// <param name="Item">The item of the event: the created, deleted or output item in
/// conversation.item.created / response.output_item.added / response.output_item.done events.</param>
/// <param name="Part">The content part added or completed. response.content_part.added /
/// response.content_part.done events.</param>
/// <param name="Response">The response of the event. response.created / response.done events.</param>
/// <param name="CallId">The id of the function call. response.function_call_arguments.delta /
/// response.function_call_arguments.done events.</param>
/// <param name="Name">The name of the function. response.function_call_arguments.done events.</param>
/// <param name="Arguments">The complete arguments of the call as a JSON string.
/// response.function_call_arguments.done and response.mcp_call_arguments.done events.</param>
/// <param name="Language">The language of the recognized speech.
/// conversation.item.input_audio_transcription.delta / completed events.</param>
/// <param name="Emotion">The emotion of the recognized speech, e.g. neutral.
/// conversation.item.input_audio_transcription.delta / completed events.</param>
/// <param name="AudioStartMs">The offset of the detected speech start in the audio buffer, in
/// milliseconds. input_audio_buffer.speech_started events.</param>
/// <param name="AudioEndMs">The offset of the detected speech end in the audio buffer, in
/// milliseconds. input_audio_buffer.speech_stopped events.</param>
/// <param name="Obfuscation">An undocumented per-event token, observed on
/// conversation.item.input_audio_transcription.delta and function / MCP arguments delta events.</param>
public record OmniRealtimeServerEvent(
    string? EventId = null,
    string? Type = null,
    [property: JsonConverter(typeof(OmniRealtimeSessionJsonConverter))] OmniRealtimeSession? Session = null,
    OmniRealtimeError? Error = null,
    string? ResponseId = null,
    string? ItemId = null,
    string? PreviousItemId = null,
    int? OutputIndex = null,
    int? ContentIndex = null,
    string? Delta = null,
    string? Text = null,
    string? Stash = null,
    string? Transcript = null,
    OmniRealtimeItem? Item = null,
    OmniRealtimeItemContent? Part = null,
    OmniRealtimeResponse? Response = null,
    string? CallId = null,
    string? Name = null,
    string? Arguments = null,
    string? Language = null,
    string? Emotion = null,
    int? AudioStartMs = null,
    int? AudioEndMs = null,
    string? Obfuscation = null);
