﻿using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sample;
using Cnblogs.DashScope.Sdk;
using Cnblogs.DashScope.Sdk.QWen;
using Cnblogs.DashScope.Sdk.TextEmbedding;
using Cnblogs.DashScope.Sdk.Wanx;
using Json.Schema;
using Json.Schema.Generation;
using Microsoft.Extensions.AI;

Console.WriteLine("Reading key from environment variable DASHSCOPE_KEY");
var apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_KEY", EnvironmentVariableTarget.Process)
             ?? Environment.GetEnvironmentVariable("DASHSCOPE_KEY", EnvironmentVariableTarget.User);
if (string.IsNullOrEmpty(apiKey))
{
    Console.Write("ApiKey > ");
    apiKey = Console.ReadLine();
}

var dashScopeClient = new DashScopeClient(apiKey!);

Console.WriteLine("Choose the sample you want to run:");
foreach (var sampleType in Enum.GetValues<SampleType>())
{
    Console.WriteLine($"{(int)sampleType}.{sampleType.GetDescription()}");
}

Console.WriteLine();
Console.Write("Choose an option: ");
var type = (SampleType)int.Parse(Console.ReadLine()!);

string userInput;
switch (type)
{
    case SampleType.TextCompletion:
        Console.Write("Prompt > ");
        userInput = Console.ReadLine()!;
        await TextCompletionAsync(userInput);
        break;
    case SampleType.TextCompletionSse:
        Console.Write("Prompt > ");
        userInput = Console.ReadLine()!;
        await TextCompletionStreamAsync(userInput);
        break;
    case SampleType.ChatCompletion:
        await ChatStreamAsync();
        break;
    case SampleType.ChatCompletionWithTool:
        await ChatWithToolsAsync();
        break;
    case SampleType.MultimodalCompletion:
        await ChatWithImageAsync();
        break;
    case SampleType.ChatCompletionWithFiles:
        await ChatWithFilesAsync();
        break;
    case SampleType.Text2Image:
        await Text2ImageAsync();
        break;
    case SampleType.MicrosoftExtensionsAi:
        await ChatWithMicrosoftExtensions();
        break;
    case SampleType.MicrosoftExtensionsAiToolCall:
        await dashScopeClient.ToolCallWithExtensionAsync();
        break;
    case SampleType.ApplicationCall:
        Console.Write("Application Id > ");
        var applicationId = Console.ReadLine()!;
        Console.Write("Prompt > ");
        userInput = Console.ReadLine()!;
        await ApplicationCallAsync(applicationId, userInput);
        break;
    case SampleType.TextToSpeech:
        {
            using var tts = await dashScopeClient.CreateSpeechSynthesizerSocketSessionAsync("cosyvoice-v2");
            var taskId = await tts.RunTaskAsync(
                new SpeechSynthesizerParameters { Voice = "longxiaochun_v2", Format = "mp3" });
            await tts.ContinueTaskAsync(taskId, "博客园");
            await tts.ContinueTaskAsync(taskId, "代码改变世界");
            await tts.FinishTaskAsync(taskId);
            var file = new FileInfo("tts.mp3");
            await using var stream = file.OpenWrite();
            await foreach (var b in tts.GetAudioAsync())
            {
                stream.WriteByte(b);
            }

            stream.Close();

            var tokenUsage = 0;
            await foreach (var message in tts.GetMessagesAsync())
            {
                if (message.Payload.Usage?.Characters > tokenUsage)
                {
                    tokenUsage = message.Payload.Usage.Characters;
                }
            }

            Console.WriteLine($"audio saved to {file.FullName}, token usage: {tokenUsage}");
            break;
        }

    case SampleType.TextEmbedding:
        Console.Write("text> ");
        var text = Console.ReadLine();
        if (string.IsNullOrEmpty(text))
        {
            text = "Coding changes world";
            Console.WriteLine($"using default text: {text}");
        }

        var response = await dashScopeClient.GetTextEmbeddingsAsync(
            TextEmbeddingModel.TextEmbeddingV3,
            [text],
            new TextEmbeddingParameters() { Dimension = 512, });
        var array = response.Output.Embeddings.First().Embedding;
        Console.WriteLine("Embedding");
        Console.WriteLine(string.Join('\n', array));
        Console.WriteLine($"Token usage: {response.Usage?.TotalTokens}");
        break;
}

return;

// text completion
async Task TextCompletionAsync(string prompt)
{
    var response = await dashScopeClient.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
    Console.WriteLine(response.Output.Text);
}

// text completion stream
async Task TextCompletionStreamAsync(string prompt)
{
    var stream = dashScopeClient.GetQWenCompletionStreamAsync(
        QWenLlm.QWenMax,
        prompt,
        new TextGenerationParameters { IncrementalOutput = true });
    await foreach (var modelResponse in stream)
    {
        Console.Write(modelResponse.Output.Text);
    }
}

async Task ChatStreamAsync()
{
    var history = new List<TextChatMessage>();
    while (true)
    {
        Console.Write("user > ");
        var input = Console.ReadLine()!;
        history.Add(TextChatMessage.User(input));
        var stream = dashScopeClient
            .GetQWenChatStreamAsync(
                QWenLlm.QWenPlusLatest,
                history,
                new TextGenerationParameters
                {
                    IncrementalOutput = true,
                    ResultFormat = ResultFormats.Message,
                    EnableThinking = true
                });
        var role = string.Empty;
        var message = new StringBuilder();
        await foreach (var modelResponse in stream)
        {
            var chunk = modelResponse.Output.Choices![0];
            if (string.IsNullOrEmpty(role) && string.IsNullOrEmpty(chunk.Message.Role) == false)
            {
                role = chunk.Message.Role;
                Console.Write(chunk.Message.Role + " > ");
            }

            message.Append(chunk.Message.Content);
            var write = string.IsNullOrEmpty(chunk.Message.ReasoningContent)
                ? chunk.Message.Content
                : chunk.Message.ReasoningContent;
            Console.Write(write);
        }

        Console.WriteLine();
        history.Add(new TextChatMessage(role, message.ToString()));
    }

    // ReSharper disable once FunctionNeverReturns
}

async Task ChatWithImageAsync()
{
    var image = await File.ReadAllBytesAsync("Lenna.jpg");
    var response = dashScopeClient.GetMultimodalGenerationStreamAsync(
        new ModelRequest<MultimodalInput, IMultimodalParameters>()
        {
            Model = "qvq-plus",
            Input = new MultimodalInput()
            {
                Messages =
                [
                    MultimodalMessage.User(
                    [
                        MultimodalMessageContent.ImageContent(image, "image/jpeg"),
                        MultimodalMessageContent.TextContent("她是谁？")
                    ])
                ]
            },
            Parameters = new MultimodalParameters { IncrementalOutput = true, VlHighResolutionImages = false }
        });
    var reasoning = false;
    await foreach (var modelResponse in response)
    {
        var choice = modelResponse.Output.Choices.FirstOrDefault();
        if (choice != null)
        {
            if (choice.FinishReason != "null")
            {
                break;
            }

            if (string.IsNullOrEmpty(choice.Message.ReasoningContent) == false)
            {
                if (reasoning == false)
                {
                    reasoning = true;
                    Console.WriteLine("<think>");
                }

                Console.Write(choice.Message.ReasoningContent);
                continue;
            }

            if (reasoning)
            {
                reasoning = false;
                Console.WriteLine("</think>");
            }

            Console.Write(choice.Message.Content[0].Text);
        }
    }
}

async Task ChatWithFilesAsync()
{
    var history = new List<TextChatMessage>();
    Console.WriteLine("uploading file \"test.txt\" ");
    var file = new FileInfo("test.txt");
    var uploadedFile = await dashScopeClient.UploadFileAsync(file.OpenRead(), file.Name);
    Console.WriteLine("file uploaded, id: " + uploadedFile.Id);
    Console.WriteLine();

    var fileMessage = TextChatMessage.File(uploadedFile.Id);
    history.Add(fileMessage);
    Console.WriteLine("system > " + fileMessage.Content);
    var userPrompt = TextChatMessage.User("该文件的内容是什么");
    history.Add(userPrompt);
    Console.WriteLine("user > " + userPrompt.Content);
    var stream = dashScopeClient.GetQWenChatStreamAsync(
        QWenLlm.QWenLong,
        history,
        new TextGenerationParameters { IncrementalOutput = true, ResultFormat = ResultFormats.Message });
    var role = string.Empty;
    var message = new StringBuilder();
    await foreach (var modelResponse in stream)
    {
        var chunk = modelResponse.Output.Choices![0];
        if (string.IsNullOrEmpty(role) && string.IsNullOrEmpty(chunk.Message.Role) == false)
        {
            role = chunk.Message.Role;
            Console.Write(chunk.Message.Role + " > ");
        }

        message.Append(chunk.Message.Content);
        Console.Write(chunk.Message.Content);
    }

    Console.WriteLine();
    history.Add(new TextChatMessage(role, message.ToString()));

    Console.WriteLine();
    Console.WriteLine("Deleting file by id: " + uploadedFile.Id);
    var result = await dashScopeClient.DeleteFileAsync(uploadedFile.Id);
    Console.WriteLine("Deletion result: " + result.Deleted);
}

async Task ChatWithToolsAsync()
{
    var history = new List<TextChatMessage>();
    var tools = new List<ToolDefinition>
    {
        new(
            ToolTypes.Function,
            new FunctionDefinition(
                nameof(GetWeather),
                "获得当前天气",
                new JsonSchemaBuilder().FromType<WeatherReportParameters>().Build()))
    };
    var chatParameters = new TextGenerationParameters { ResultFormat = ResultFormats.Message, Tools = tools };
    var question = TextChatMessage.User("请问现在杭州的天气如何？");
    history.Add(question);
    Console.WriteLine($"{question.Role} > {question.Content}");

    var response = await dashScopeClient.GetQWenChatCompletionAsync(QWenLlm.QWenMax, history, chatParameters);
    var toolCallMessage = response.Output.Choices![0].Message;
    history.Add(toolCallMessage);
    Console.WriteLine(
        $"{toolCallMessage.Role} > {toolCallMessage.ToolCalls![0].Function.Name}{toolCallMessage.ToolCalls[0].Function.Arguments}");

    var toolResponse = GetWeather(
        JsonSerializer.Deserialize<WeatherReportParameters>(toolCallMessage.ToolCalls[0].Function.Arguments!)!);
    var toolMessage = TextChatMessage.Tool(toolResponse, nameof(GetWeather));
    history.Add(toolMessage);
    Console.WriteLine($"{toolMessage.Role} > {toolMessage.Content}");

    var answer = await dashScopeClient.GetQWenChatCompletionAsync(QWenLlm.QWenMax, history, chatParameters);
    Console.WriteLine($"{answer.Output.Choices![0].Message.Role} > {answer.Output.Choices[0].Message.Content}");

    string GetWeather(WeatherReportParameters parameters)
    {
        return "大部多云，气温 "
               + parameters.Unit switch
               {
                   TemperatureUnit.Celsius => "18 摄氏度",
                   TemperatureUnit.Fahrenheit => "64 华氏度",
                   _ => throw new InvalidOperationException()
               };
    }
}

async Task ChatWithMicrosoftExtensions()
{
    Console.WriteLine("Requesting model...");
    var chatClient = dashScopeClient.AsChatClient("qwen-max");
    List<ChatMessage> conversation =
        new() { new(ChatRole.System, "You are a helpful AI assistant"), new(ChatRole.User, "What is AI?") };
    var response = await chatClient.GetResponseAsync(conversation);
    var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true };
    Console.WriteLine(JsonSerializer.Serialize(response, serializerOptions));
}

async Task Text2ImageAsync()
{
    Console.Write("Prompt> ");
    var prompt = Console.ReadLine();
    if (string.IsNullOrEmpty(prompt))
    {
        Console.WriteLine("Using sample prompt");
        prompt = "A fluffy cat";
    }

    var task = await dashScopeClient.CreateWanxImageSynthesisTaskAsync(
        WanxModel.WanxV21Turbo,
        prompt,
        null,
        new ImageSynthesisParameters { Style = ImageStyles.OilPainting });
    Console.WriteLine($"Task({task.TaskId}) submitted, checking status...");
    var watch = Stopwatch.StartNew();
    while (watch.Elapsed.TotalSeconds < 120)
    {
        var result = await dashScopeClient.GetWanxImageSynthesisTaskAsync(task.TaskId);
        Console.WriteLine($"{watch.ElapsedMilliseconds}ms - Status: {result.Output.TaskStatus}");
        if (result.Output.TaskStatus == DashScopeTaskStatus.Succeeded)
        {
            Console.WriteLine($"Image generation finished, URL: {result.Output.Results![0].Url}");
            return;
        }

        if (result.Output.TaskStatus == DashScopeTaskStatus.Failed)
        {
            Console.WriteLine($"Image generation failed, error message: {result.Output.Message}");
            return;
        }

        await Task.Delay(500);
    }

    Console.WriteLine($"Task timout, taskId: {task.TaskId}");
}

async Task ApplicationCallAsync(string applicationId, string prompt)
{
    var request = new ApplicationRequest { Input = new ApplicationInput { Prompt = prompt } };
    var response = await dashScopeClient.GetApplicationResponseAsync(applicationId, request);
    Console.WriteLine(response.Output.Text);
}
