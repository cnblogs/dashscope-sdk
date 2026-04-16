using Cnblogs.DashScope.Core;
using Microsoft.Extensions.AI;

namespace Cnblogs.DashScope.Sample.MsExtensionsAI;

public class MsExtensionsAiToolCallExample : MsExtensionsAiSample
{
    /// <inheritdoc />
    public override string Description => "Tool calls with streaming";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        var chatClient = client.AsChatClient("qwen3.5-35b-a3b").AsBuilder().UseFunctionInvocation().Build();
        var options = new ChatOptions()
        {
            Tools = [AIFunctionFactory.Create(GetWeather), AIFunctionFactory.Create(GetNow)],
            ToolMode = new AutoChatToolMode(),
            AllowMultipleToolCalls = true,
        };
        var stream = chatClient.GetStreamingResponseAsync("现在的时间以及杭州的天气怎么样？", options);
        await foreach (var chatResponseUpdate in stream)
        {
            Console.Write(chatResponseUpdate);
        }
    }

    private string GetWeather(WeatherReportParameters payload)
    {
        Console.WriteLine($"Tool {nameof(GetWeather)} called, payload: {payload}");
        return $"{payload.Location} 大部多云，气温 "
               + payload.Unit switch
               {
                   TemperatureUnit.Celsius => "18 摄氏度",
                   TemperatureUnit.Fahrenheit => "64 华氏度",
                   _ => throw new InvalidOperationException()
               };
    }

    private string GetNow()
    {
        Console.WriteLine($"Tool {nameof(GetNow)} called");
        return DateTime.Now.ToString("s");
    }
}

/*
Tool GetNow called
Tool GetWeather called, payload: WeatherReportParameters { Location = 杭州, Unit = Celsius }
当前时间是2026年2月3日20:39。

杭州大部分多云，气温18摄氏度。
 */
