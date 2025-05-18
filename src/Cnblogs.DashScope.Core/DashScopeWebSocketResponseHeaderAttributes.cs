namespace Cnblogs.DashScope.Core;

/// <summary>
/// Attributes field in websocket response header.
/// </summary>
/// <param name="RequestUuid">UUID for current request.</param>
public record DashScopeWebSocketResponseHeaderAttributes(string? RequestUuid);
