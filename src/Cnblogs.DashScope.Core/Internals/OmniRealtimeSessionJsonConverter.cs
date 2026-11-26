using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads the session of an omni realtime server event. The Omit defaults of
/// <see cref="OmniRealtimeSession.TurnDetection"/> and
/// <see cref="OmniRealtimeSession.InputAudioTranscription"/> are for the sessions the clients
/// send: a member missing from a server event means the turn detection / the transcription is
/// off, which is a null.
/// </summary>
internal class OmniRealtimeSessionJsonConverter : JsonConverter<OmniRealtimeSession>
{
    /// <inheritdoc />
    public override OmniRealtimeSession? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var session = JsonSerializer.Deserialize<OmniRealtimeSession>(ref reader, options);
        if (session is null)
        {
            return null;
        }

        if (ReferenceEquals(session.TurnDetection, OmniRealtimeTurnDetection.Omit))
        {
            session.TurnDetection = null;
        }

        if (ReferenceEquals(session.InputAudioTranscription, OmniRealtimeTranscription.Omit))
        {
            session.InputAudioTranscription = null;
        }

        return session;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, OmniRealtimeSession value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
