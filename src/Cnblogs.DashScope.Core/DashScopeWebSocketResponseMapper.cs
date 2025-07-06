using System.Text.Json;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Mapper class for <see cref="DashScopeWebSocketResponse{TOutput}"/>
/// </summary>
internal static class DashScopeWebSocketResponseMapper
{
    public static DashScopeWebSocketResponse<TOutput> DeserializeOutput<TOutput>(this DashScopeWebSocketResponse<JsonElement> source)
        where TOutput : class
    {
        var output = source.Payload.Output;
        if (output.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
        {
            return new DashScopeWebSocketResponse<TOutput>(
                source.Header,
                new DashScopeWebSocketResponsePayload<TOutput>(null, source.Payload.Usage));
        }

        var mapped = output.Deserialize<TOutput>(DashScopeDefaults.SerializationOptions);
        return new DashScopeWebSocketResponse<TOutput>(
            source.Header,
            new DashScopeWebSocketResponsePayload<TOutput>(mapped, source.Payload.Usage));
    }
}
