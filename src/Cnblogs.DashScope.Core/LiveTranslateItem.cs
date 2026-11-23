namespace Cnblogs.DashScope.Core;

/// <summary>
/// A conversation item of a live translation session. For a VAD segment the server creates
/// both the source recognition item and the translated message item; the id of the recognition
/// item equals the <c>previous_item_id</c> of the translated one.
/// </summary>
public class LiveTranslateItem
{
    /// <summary>
    /// The id of the item.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Always realtime.item. Server echo.
    /// </summary>
    public string? Object { get; set; }

    /// <summary>
    /// The type of the item, message so far.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The status of the item, e.g. completed.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The role of the message, e.g. assistant.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// The content of the message.
    /// </summary>
    public List<LiveTranslateItemContent>? Content { get; set; }
}
