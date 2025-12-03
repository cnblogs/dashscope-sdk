using System.Text.Json.Serialization;
using Json.More;
using Json.Schema.Generation;

namespace Cnblogs.DashScope.Sample
{
    public record WeatherReportParameters(
        [property: Required]
        [property: Description("要获取天气的省市名称，例如浙江省杭州市")]
        string Location,
        [property: JsonConverter(typeof(EnumStringConverter<TemperatureUnit>))]
        [property: Description("温度单位")]
        TemperatureUnit Unit = TemperatureUnit.Celsius);

    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit
    }
}
