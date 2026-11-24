using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads the session of an audio chat server event. The Omit default of
/// <see cref="AudioChatSession.TurnDetection"/> is for the sessions the clients send: a
/// turn_detection missing from a server event means the turn detection is off, which is a null.
/// </summary>
internal class AudioChatSessionJsonConverter : JsonConverter<AudioChatSession>
{
    /// <inheritdoc />
    public override AudioChatSession? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var session = JsonSerializer.Deserialize<AudioChatSession>(ref reader, options);
        if (session is null || ReferenceEquals(session.TurnDetection, AudioChatTurnDetection.Omit) == false)
        {
            return session;
        }

        session.TurnDetection = null;
        return session;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, AudioChatSession value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
