namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a ws request to DashScope api.
/// </summary>
/// <typeparam name="TPayload">The type of the payload.</typeparam>
public class WsRequest<TPayload>
{
    /// <summary>
    /// Header info of a ws request.
    /// </summary>
    public required WsHeader Header { get; init; }

    /// <summary>
    /// Contents of a ws request.
    /// </summary>
    public required TPayload Payload { get; init; }
}
