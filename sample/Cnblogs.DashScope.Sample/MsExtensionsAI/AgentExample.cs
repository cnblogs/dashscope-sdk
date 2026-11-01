using Cnblogs.DashScope.Core;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace Cnblogs.DashScope.Sample.MsExtensionsAI;

public class AgentExample : MsExtensionsAiSample
{
    /// <inheritdoc />
    public override string Description => "Agent with Microsoft Agent Framework (instructions + tool calls)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // ChatClientAgent keeps the conversation history itself and turns the agent-level
        // instructions into ChatOptions.Instructions on every request, which this SDK maps
        // to a leading system message. Tool calls are executed by the agent loop automatically.
        var chatClient = client.AsChatClient("qwen-plus");
        var agent = new ChatClientAgent(
            chatClient,
            instructions: "你是智能助理，可以查询天气和当前时间，回答要简洁。",
            name: "assistant",
            tools: [AIFunctionFactory.Create(GetWeather), AIFunctionFactory.Create(GetNow)]);

        // The session carries the conversation history across runs.
        var session = await agent.CreateSessionAsync();
        while (true)
        {
            Console.Write("User > ");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter a user input.");
                return;
            }

            var response = await agent.RunAsync(input, session);
            Console.WriteLine($"Assistant > {response.Text}");
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
User > 现在几点了？杭州天气怎么样？
Tool GetNow called
Tool GetWeather called, payload: WeatherReportParameters { Location = 杭州, Unit = Celsius }
Assistant > 现在是2026年11月1日18:36。杭州天气：大部多云，气温18℃。
User > 那上海呢？
Tool GetWeather called, payload: WeatherReportParameters { Location = 上海, Unit = Celsius }
Assistant > 上海天气：大部多云，气温18℃。
 */
