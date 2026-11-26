namespace Cnblogs.DashScope.Core;

/// <summary>
/// A content part of an omni realtime conversation item. For sending a message item:
/// input_text (Text). The server echoes the user audio items with the input_audio type only, the
/// reply items carry the subtitle of the audio in <paramref name="Transcript"/> (audio) or the
/// text itself (text).
/// </summary>
/// <param name="Type">The content type: input_text (client), input_audio, audio or text.</param>
/// <param name="Text">The text of the content part.</param>
/// <param name="Transcript">The transcript of the audio content part.</param>
public record OmniRealtimeItemContent(
    string? Type = null,
    string? Text = null,
    string? Transcript = null);
