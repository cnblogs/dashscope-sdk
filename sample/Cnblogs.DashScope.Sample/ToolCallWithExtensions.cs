using System.ComponentModel;
using System.Text.Json;
using Cnblogs.DashScope.Core;
using Microsoft.Extensions.AI;

namespace Cnblogs.DashScope.Sample;

public static class ToolCallWithExtensions
{
    public static async Task ToolCallWithExtensionAsync(this IDashScopeClient dashScopeClient)
    {
        [Description("Gets the weather")]
        string GetWeather(string location) => Random.Shared.NextDouble() > 0.5 ? "It's sunny" : "It's raining";

        var chatOptions = new ChatOptions { Tools = [AIFunctionFactory.Create(GetWeather)] };

        var client = dashScopeClient.AsChatClient("qwen-max").AsBuilder().UseFunctionInvocation().Build();
        await foreach (var message in client.GetStreamingResponseAsync("What is weather of LA today?", chatOptions))
        {
            Console.WriteLine(JsonSerializer.Serialize(message));
        }

        Console.WriteLine();
    }
}
