namespace Cnblogs.DashScope.Core;

/// <summary>
/// A content part of a live translation conversation item.
/// </summary>
/// <param name="Type">The content type: text or audio.</param>
/// <param name="Text">The text of the content part.</param>
/// <param name="Transcript">The transcript of the audio content part. Server echo.</param>
public record LiveTranslateItemContent(
    string? Type = null,
    string? Text = null,
    string? Transcript = null);
