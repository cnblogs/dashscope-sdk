using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals
{
    /// <summary>
    /// The converter for <see cref="ToolChoice"/>
    /// </summary>
    internal class ToolChoiceJsonConverter : JsonConverter<ToolChoice>
    {
        /// <inheritdoc />
        public override ToolChoice? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();
                return str switch
                {
                    "auto" => ToolChoice.AutoChoice,
                    "none" => ToolChoice.NoneChoice,
                    _ => throw new JsonException("Unknown tool choice type")
                };
            }

            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Unknown tool choice.");
            }

            var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            var functionValid = element.TryGetProperty("function", out var function);
            var typeValid = element.TryGetProperty("type", out var type);
            if (functionValid == false || typeValid == false || type.ValueKind != JsonValueKind.String)
            {
                throw new JsonException("Unknown tool choice type");
            }

            var toolFunction = function.Deserialize<ToolChoiceFunction>(options);
            var toolChoiceType = type.GetString();
            if (toolFunction == null || toolChoiceType != "function")
            {
                throw new JsonException("Unknown tool choice type");
            }

            return ToolChoice.FunctionChoice(toolFunction.Name);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, ToolChoice value, JsonSerializerOptions options)
        {
            if (value.Type != "function")
            {
                writer.WriteStringValue(value.Type);
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteString("type", value.Type);
                writer.WritePropertyName("function");
                JsonSerializer.Serialize(writer, value.Function, options);
                writer.WriteEndObject();
            }
        }
    }
}
