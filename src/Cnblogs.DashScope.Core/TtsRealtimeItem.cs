namespace Cnblogs.DashScope.Core;

/// <summary>
/// An output item of the TTS realtime API.
/// </summary>
/// <param name="Id">The id of the item, e.g. item_xxx.</param>
/// <param name="Object">The object type, always realtime.item.</param>
/// <param name="Type">The type of the item, always message.</param>
/// <param name="Status">The status of the item: in_progress or completed.</param>
/// <param name="Role">The role of the item, always assistant.</param>
/// <param name="Content">The content parts of the item, audio parts in response.done events.</param>
public record TtsRealtimeItem(
    string? Id = null,
    string? Object = null,
    string? Type = null,
    string? Status = null,
    string? Role = null,
    List<TtsRealtimeContentPart>? Content = null);
