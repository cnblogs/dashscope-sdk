using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads the model responses. The json of a response carries the output under "output", except
/// for the default frame the gateway opens some streams with (the web search of qwen3.8-omni-flash
/// on the workspace domain): it is the bare output, without the envelope. Such a frame is read as
/// a response holding that output.
/// </summary>
internal sealed class ModelResponseJsonConverterFactory : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType
               && typeToConvert.GetGenericTypeDefinition() == typeof(ModelResponse<,>);
    }

    /// <inheritdoc />
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return (JsonConverter?)Activator.CreateInstance(
            typeof(ModelResponseJsonConverter<,>).MakeGenericType(typeToConvert.GetGenericArguments()));
    }
}
