namespace Cnblogs.DashScope.Core;

/// <summary>
/// A content part of the TTS realtime API, part of a response item.
/// </summary>
/// <param name="Type">The type of the part, e.g. audio.</param>
/// <param name="Text">The text of the part, present in response.content_part.added / done events.</param>
/// <param name="Transcript">The transcript of the audio, present in the output of response.done events.</param>
public record TtsRealtimeContentPart(string? Type = null, string? Text = null, string? Transcript = null);
