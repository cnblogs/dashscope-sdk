using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals
{
    internal class DashScopeDateTimeConvertor : JsonConverter<DateTime>
    {
        private static readonly string[] DateTimeFormats = { "yyyy-MM-dd HH:mm:ss.fff", "yyyy-MM-dd HH:mm:ss.FFF" };

        /// <inheritdoc />
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var date = reader.GetString();
            foreach (var format in DateTimeFormats)
            {
                if (DateTime.TryParseExact(
                        date,
                        format,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var value))
                {
                    return value;
                }
            }

            throw new JsonException($"Failed to parse task date, value: {date}");
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateTimeFormats[0]));
        }
    }
}
