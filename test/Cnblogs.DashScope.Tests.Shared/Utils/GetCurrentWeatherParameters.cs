using Json.Schema.Generation;

namespace Cnblogs.DashScope.Tests.Shared.Utils
{
    public class GetCurrentWeatherParameters
    {
        [Required]
        [Description("要获取天气的省市名称，例如浙江省杭州市")]
        public string Location { get; init; } = string.Empty;
    }
}
