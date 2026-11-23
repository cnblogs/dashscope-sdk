namespace Cnblogs.DashScope.Core;

/// <summary>
/// A content part of an audio chat conversation item. For sending: input_text (Text),
/// input_audio (Audio as base64), output_text (Text); the server echoes input_audio parts with
/// the Transcript instead of the raw audio, and response parts carry the subtitle of the audio
/// in <paramref name="Transcript"/>.
/// </summary>
/// <param name="Type">The content type: input_text, input_audio, output_text (client) or text,
/// audio (server).</param>
/// <param name="Text">The text of the content part.</param>
/// <param name="Audio">The base64 encoded audio of the content part. Sending input_audio only.</param>
/// <param name="Transcript">The transcript of the audio content part. Server echo.</param>
public record AudioChatItemContent(
    string? Type = null,
    string? Text = null,
    string? Audio = null,
    string? Transcript = null);
