using System.Text;
using System.Text.Json;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;
using Json.Schema;
using Json.Schema.Generation;

namespace Cnblogs.DashScope.Sample.Text;

public class ChatToolCallingSample : ISample
{
    /// <inheritdoc />
    public string Description => "Chat with tool calling";

    /// <inheritdoc />
    public async Task RunAsync(IDashScopeClient client)
    {
        var tools = new List<ToolDefinition>
        {
            new(
                ToolTypes.Function,
                new FunctionDefinition(
                    nameof(GetWeather),
                    "获得当前天气",
                    new JsonSchemaBuilder().FromType<WeatherReportParameters>().Build()))
        };
        var messages = new List<TextChatMessage>();
        messages.Add(TextChatMessage.System("You are a helpful assistant"));
        List<ToolCall>? pendingToolCalls = null;
        while (true)
        {
            if (pendingToolCalls?.Count > 0)
            {
                // call tools
                foreach (var call in pendingToolCalls)
                {
                    var payload = JsonSerializer.Deserialize<WeatherReportParameters>(call.Function.Arguments!)!;
                    var response = GetWeather(payload);
                    Console.WriteLine("Tool > " + response);
                    messages.Add(TextChatMessage.Tool(response, call.Id));
                }

                pendingToolCalls = null;
            }
            else
            {
                // get user input
                Console.Write("User > ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a user input.");
                    return;
                }

                messages.Add(TextChatMessage.User(input));
            }

            var completion = client.GetTextCompletionStreamAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Model = "qwen-turbo",
                    Input = new TextGenerationInput() { Messages = messages },
                    Parameters = new TextGenerationParameters()
                    {
                        ResultFormat = "message",
                        EnableThinking = false,
                        IncrementalOutput = true,
                        Tools = tools,
                        ToolChoice = ToolChoice.AutoChoice,
                        ParallelToolCalls = true
                    }
                });
            var reply = new StringBuilder();
            TextGenerationTokenUsage? usage = null;
            var argumentDictionary = new Dictionary<int, StringBuilder>();
            var firstReplyChunk = true;
            await foreach (var chunk in completion)
            {
                usage = chunk.Usage;
                var choice = chunk.Output.Choices![0];
                if (choice.Message.ToolCalls != null && choice.Message.ToolCalls.Count != 0)
                {
                    pendingToolCalls ??= new List<ToolCall>();
                    foreach (var call in choice.Message.ToolCalls)
                    {
                        var hasPartial = argumentDictionary.TryGetValue(call.Index, out var partialArgument);
                        if (!hasPartial || partialArgument == null)
                        {
                            partialArgument = new StringBuilder();
                            argumentDictionary[call.Index] = partialArgument;
                            pendingToolCalls.Add(call);
                        }

                        partialArgument.Append(call.Function.Arguments);
                    }

                    continue;
                }

                if (firstReplyChunk)
                {
                    Console.Write("Assistant > ");
                    firstReplyChunk = false;
                }

                Console.Write(choice.Message.Content);
                reply.Append(choice.Message.Content);
            }

            if (argumentDictionary.Count != 0)
            {
                if (firstReplyChunk)
                {
                    Console.Write("Assistant > ");
                }

                pendingToolCalls?.ForEach(p =>
                {
                    p.Function.Arguments = argumentDictionary[p.Index].ToString();
                    Console.Write($"调用：{p.Function.Name}({p.Function.Arguments}); ");
                });
            }

            Console.WriteLine();
            messages.Add(TextChatMessage.Assistant(reply.ToString(), toolCalls: pendingToolCalls));
            if (usage != null)
            {
                Console.WriteLine(
                    $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
            }
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
User > 杭州和上海的天气怎么样？
Assistant > 调用：GetWeather({"Location": "浙江省杭州市", "Unit": "Celsius"}); 调用：GetWeather({"Location": "上海市", "Unit": "Celsius"});
Usage: in(196)/out(54)/total(250)
Tool > 浙江省杭州市 大部多云，气温 18 摄氏度
Tool > 上海市 大部多云，气温 18 摄氏度
Assistant > 浙江省杭州市和上海市的天气大部多云，气温均为18摄氏度。
Usage: in(302)/out(19)/total(321)
 */
