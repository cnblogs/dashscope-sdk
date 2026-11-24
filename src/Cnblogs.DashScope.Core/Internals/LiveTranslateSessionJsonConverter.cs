using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads the session of a live translation server event. The Omit default of
/// <see cref="LiveTranslateSession.TurnDetection"/> is for the sessions the clients send: a
/// turn_detection missing from a server event means the VAD is off, which is a null.
/// </summary>
internal class LiveTranslateSessionJsonConverter : JsonConverter<LiveTranslateSession>
{
    /// <inheritdoc />
    public override LiveTranslateSession? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var session = JsonSerializer.Deserialize<LiveTranslateSession>(ref reader, options);
        if (session is null || ReferenceEquals(session.TurnDetection, LiveTranslateTurnDetection.Omit) == false)
        {
            return session;
        }

        session.TurnDetection = null;
        return session;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, LiveTranslateSession value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
