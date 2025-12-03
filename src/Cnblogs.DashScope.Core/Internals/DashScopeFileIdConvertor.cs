using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals
{
    internal class DashScopeFileIdConvertor : JsonConverter<DashScopeFileId>
    {
        /// <inheritdoc />
        public override DashScopeFileId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var id = reader.GetString();
            if (id == null)
            {
                throw new JsonException("expected a file id, but found null");
            }

            return new DashScopeFileId(id);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DashScopeFileId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}
