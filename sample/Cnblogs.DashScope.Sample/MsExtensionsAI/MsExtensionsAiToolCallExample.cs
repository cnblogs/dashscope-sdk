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
        var chatClient = client.AsChatClient("qwen-turbo");
        chatClient = chatClient.AsBuilder().UseFunctionInvocation().Build();
        var options = new ChatOptions()
        {
            Tools = [AIFunctionFactory.Create(GetWeather)],
            ToolMode = new AutoChatToolMode(),
            AllowMultipleToolCalls = true
        };
        var stream = chatClient.GetStreamingResponseAsync("杭州和上海的天气怎么样？", options);
        await foreach (var chatResponseUpdate in stream)
        {
            Console.Write(chatResponseUpdate);
        }
    }

    private string GetWeather(WeatherReportParameters payload)
        => $"{payload.Location} 大部多云，气温 "
           + payload.Unit switch
           {
               TemperatureUnit.Celsius => "18 摄氏度",
               TemperatureUnit.Fahrenheit => "64 华氏度",
               _ => throw new InvalidOperationException()
           };
}

/*
 * 杭州和上海的天气都是多云，气温均为18摄氏度。
 */
