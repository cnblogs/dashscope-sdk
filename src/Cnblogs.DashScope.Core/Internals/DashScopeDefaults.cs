using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Default values for DashScope client.
/// </summary>
public static class DashScopeDefaults
{
    /// <summary>
    /// Base address for HTTP API.
    /// </summary>
    public const string DashScopeHttpApiBaseAddress = "https://dashscope.aliyuncs.com/api/v1/";

    /// <summary>
    /// Base address for websocket API.
    /// </summary>
    public const string DashScopeWebsocketApiBaseAddress = "wss://dashscope.aliyuncs.com/api-ws/v1/inference/";

    /// <summary>
    /// Default json serializer options.
    /// </summary>
    public static readonly JsonSerializerOptions SerializationOptions =
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        };
}
