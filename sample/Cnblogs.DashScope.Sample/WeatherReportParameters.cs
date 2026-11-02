using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Sample;

public record WeatherReportParameters(
    [property: Description("要获取天气的省市名称，例如浙江省杭州市")]
    string Location,
    [property: JsonConverter(typeof(JsonStringEnumConverter<TemperatureUnit>))]
    [property: Description("温度单位")]
    TemperatureUnit Unit = TemperatureUnit.Celsius);

public enum TemperatureUnit
{
    Celsius,
    Fahrenheit
}
