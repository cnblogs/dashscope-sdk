using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads and writes the model responses of one endpoint, see
/// <see cref="ModelResponseJsonConverterFactory"/>.
/// </summary>
internal sealed class ModelResponseJsonConverter<TOutput, TUsage> : JsonConverter<ModelResponse<TOutput, TUsage>>
    where TOutput : class
    where TUsage : class
{
    /// <inheritdoc />
    public override ModelResponse<TOutput, TUsage>? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var json = document.RootElement;
        if (IsBareOutput(json))
        {
            return new ModelResponse<TOutput, TUsage>
            {
                Output = json.Deserialize<TOutput>(options)
                         ?? throw new JsonException("The json of the output is null")
            };
        }

        // the envelope is the shape of the json: the model response itself would read itself here
        var envelope = json.Deserialize<Envelope>(options);
        return envelope == null
            ? null
            : new ModelResponse<TOutput, TUsage>
            {
                Output = envelope.Output!,
                Usage = envelope.Usage,
                RequestId = envelope.RequestId ?? string.Empty
            };
    }

    /// <summary>
    /// Whether the json is an output without the envelope of a response: it carries neither an
    /// output nor a request id nor a usage. A response without an output but with the other
    /// members keeps reading as it always did.
    /// </summary>
    private static bool IsBareOutput(JsonElement json)
    {
        return json.ValueKind == JsonValueKind.Object
               && json.TryGetProperty("output", out _) == false
               && json.TryGetProperty("request_id", out _) == false
               && json.TryGetProperty("usage", out _) == false;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, ModelResponse<TOutput, TUsage> value, JsonSerializerOptions options)
    {
        var envelope = new Envelope
        {
            Output = value.Output,
            Usage = value.Usage,
            RequestId = value.RequestId
        };
        JsonSerializer.Serialize(writer, envelope, options);
    }

    /// <summary>
    /// The shape of the json of a response. Its members map the members of the model response
    /// one to one, a new member goes to both.
    /// </summary>
    private sealed class Envelope
    {
        public TOutput? Output { get; set; }

        public TUsage? Usage { get; set; }

        public string? RequestId { get; set; }
    }
}
