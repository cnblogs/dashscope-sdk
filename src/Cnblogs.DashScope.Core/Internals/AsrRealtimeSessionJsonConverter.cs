using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads the session of an ASR realtime server event. The Omit default of
/// <see cref="AsrRealtimeSession.TurnDetection"/> is for the sessions the clients send: a
/// turn_detection missing from a server event means the VAD is off, which is a null.
/// </summary>
internal class AsrRealtimeSessionJsonConverter : JsonConverter<AsrRealtimeSession>
{
    /// <inheritdoc />
    public override AsrRealtimeSession? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var session = JsonSerializer.Deserialize<AsrRealtimeSession>(ref reader, options);
        if (session is null || ReferenceEquals(session.TurnDetection, AsrRealtimeTurnDetection.Omit) == false)
        {
            return session;
        }

        session.TurnDetection = null;
        return session;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, AsrRealtimeSession value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
