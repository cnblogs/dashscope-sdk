English | [简体中文](https://github.com/cnblogs/dashscope-sdk/blob/main/README.zh-Hans.md)

# Cnblogs.DashScopeSDK

[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AI?style=flat&logo=nuget&label=Cnblogs.DashScope.AI)](https://www.nuget.org/packages/Cnblogs.DashScope.AI)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.Sdk?style=flat&logo=nuget&label=Cnblogs.DashScope.Sdk&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.Sdk)](https://www.nuget.org/packages/Cnblogs.DashScope.Sdk)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AspNetCore?style=flat&logo=nuget&label=Cnblogs.DashScope.AspNetCore&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.AspNetCore)](https://www.nuget.org/packages/Cnblogs.DashScope.AspNetCore)

A non-official DashScope (Bailian) service SDK maintained by Cnblogs.

**Note:** This project is actively under development. Breaking changes may occur even in minor versions. Please review the Release Notes before upgrading.

## Quick Start

### Using `Microsoft.Extensions.AI` Interface

Install NuGet package `Cnblogs.DashScope.AI`
```csharp
var client = new DashScopeClient("your-api-key").AsChatClient("qwen-max");
var completion = await client.CompleteAsync("hello");
Console.WriteLine(completion)
```

### Console Application

Install NuGet package `Cnblogs.DashScope.Sdk`
```csharp
var client = new DashScopeClient("your-api-key");
// you could use different api base, example:
// var client = new DashScopeClient("your-api-key", "https://dashscope-intl.aliyuncs.com/api/v1/");
var completion = await client.GetTextCompletionAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-turbo",
        Input = new TextGenerationInput()
        {
            Messages = new List<TextChatMessage>()
            {
                TextChatMessage.System("You are a helpful assistant"),
                TextChatMessage.User("你是谁？")
            }
        },
        Parameters = new TextGenerationParameters() { ResultFormat = "message" }
    });
Console.WriteLine(completion.Output.Choices![0].Message.Content)
```

### ASP.NET Core Application

Install NuGet package `Cnblogs.DashScope.AspNetCore`

`Program.cs`
```csharp
builder.AddDashScopeClient(builder.Configuration);
```

`appsettings.json`

```json
{
    "DashScope": {
        "ApiKey": "your-api-key",
        "BaseAddress": "https://dashscope-intl.aliyuncs.com/api/v1"
    }
}
```

Application class:

```csharp
public class YourService(IDashScopeClient client)
{
    public async Task<string> CompletePromptAsync(string prompt)
    {
    	var completion = await client.GetTextCompletionAsync(
        new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
        {
            Model = "qwen-turbo",
            Input = new TextGenerationInput()
            {
                Messages = new List<TextChatMessage>()
                {
                    TextChatMessage.System("You are a helpful assistant"),
                    TextChatMessage.User("你是谁？")
                }
            },
            Parameters = new TextGenerationParameters() { ResultFormat = "message" }
        });
		return completion.Output.Choices![0].Message.Content
    }
}
```
## Supported APIs
- [Text Generation](#text-generation) - QWen3, DeepSeek, etc. Supports reasoning/tool calling/web search/translation scenarios
  - [Conversation](#conversation)
  - [Thinking Models](#thinking-models)
  - [Web Search](#web-search)
  - [Tool Calling](#tool-calling)
  - [Prefix Completion](#prefix-completion)
  - [Long Context (Qwen-Long)](#long-context-qwen-long)
- [Multimodal](#multimodal) - QWen-VL, QVQ, etc. Supports reasoning/visual understanding/OCR/audio understanding
- [Text-to-Speech](#text-to-speech) - CosyVoice, Sambert, etc. For TTS applications
- [Image Generation](#image-generation) - wanx2.1, etc. For text-to-image and portrait style transfer
- [Application Call](#application-call)
- [Text Embeddings](#text-embeddings)


## Text Generation

Use `dashScopeClient.GetTextCompletionAsync` and `dashScopeClient.GetTextCompletionStreamAsync()` to access text generation APIs.

Common models: `qwen-max` `qwen-plus` `qwen-flush` etc.

Basic example:

```csharp
var client = new DashScopeClient("your-api-key");
var completion = await client.GetTextCompletionAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-turbo",
        Input = new TextGenerationInput()
        {
            Messages = new List<TextChatMessage>()
            {
                TextChatMessage.System("You are a helpful assistant"),
                TextChatMessage.User("Who are you?")
            }
        },
        Parameters = new TextGenerationParameters() { ResultFormat = "message" }
    });
Console.WriteLine(completion.Output.Choices![0].Message.Content)
```

### Conversation

#### Quick Start

The key is maintaining a `TextChatMessage` array as conversation history.

```csharp
var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
while (true)
{
    Console.Write("User > ");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Using default input: Who are you?");
        input = "Who are you?";
    }

    messages.Add(TextChatMessage.User(input));
    var completion = await client.GetTextCompletionAsync(
        new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
        {
            Model = "qwen-turbo",
            Input = new TextGenerationInput() { Messages = messages },
            Parameters = new TextGenerationParameters() { ResultFormat = "message" }
        });
    Console.WriteLine("Assistant > " + completion.Output.Choices![0].Message.Content);
    var usage = completion.Usage;
    if (usage != null)
    {
        Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
    }

    messages.Add(TextChatMessage.Assistant(completion.Output.Choices[0].Message.Content));
}
```

### Thinking Models

The model's thinking process is stored in a separate `ReasoningContent` property. When saving to conversation history, ignore it and only keep the model's reply `Content`.

Some models accept `EnableThinking` to control deep thinking, which can be set in `Parameters`.

```csharp
var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
while (true)
{
    Console.Write("User > ");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Please enter a user input.");
        return;
    }

    messages.Add(TextChatMessage.User(input));
    var completion = await client.GetTextCompletionAsync(
        new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
        {
            Model = "qwen-turbo",
            Input = new TextGenerationInput() { Messages = messages },
            Parameters = new TextGenerationParameters() { ResultFormat = "message", EnableThinking = true }
        });
    Console.WriteLine("Reasoning > " + completion.Output.Choices![0].Message.ReasoningContent);
    Console.WriteLine("Assistant > " + completion.Output.Choices![0].Message.Content);
    var usage = completion.Usage;
    if (usage != null)
    {
        Console.WriteLine(
            $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
    }

    messages.Add(TextChatMessage.Assistant(completion.Output.Choices[0].Message.Content));
}
```

#### Streaming Output

```cs
var request = new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
{
    Model = "qwen-turbo",
    Input = new TextGenerationInput() { Messages = messages },
    Parameters = new TextGenerationParameters()
    {
        ResultFormat = "message",
        EnableThinking = true,
        IncrementalOutput = true
    }
}
```

Use `client.GetTextCompletionStreamAsync` for streaming output. It's recommended to enable `IncrementalOutput` in `Parameters` for incremental output.

Incremental output:
> Example: ["I love","eating","apples"]

Non-incremental output:
> Example: ["I love","I love eating","I love eating apples"]

Streaming output returns an `IAsyncEnumerable`. Use `await foreach` to iterate, record incremental content, then save to conversation history.

var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
while (true)
{
    Console.Write("User > ");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Please enter a user input.");
        return;
    }

    messages.Add(TextChatMessage.User(input));
    var completion = client.GetTextCompletionStreamAsync(
        new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
        {
            Model = "qwen-turbo",
            Input = new TextGenerationInput() { Messages = messages },
            Parameters = new TextGenerationParameters()
            {
                ResultFormat = "message",
                EnableThinking = true,
                IncrementalOutput = true
            }
        });
    var reply = new StringBuilder();
    var reasoning = false;
    TextGenerationTokenUsage? usage = null;
    await foreach (var chunk in completion)
    {
        var choice = chunk.Output.Choices![0];
        if (string.IsNullOrEmpty(choice.Message.ReasoningContent) == false)
        {
            // reasoning
            if (reasoning == false)
            {
                Console.Write("Reasoning > ");
                reasoning = true;
            }
    
            Console.Write(choice.Message.ReasoningContent);
            continue;
        }
    
        if (reasoning)
        {
            reasoning = false;
            Console.WriteLine();
            Console.Write("Assistant > ");
        }
    
        Console.Write(choice.Message.Content);
        reply.Append(choice.Message.Content);
        usage = chunk.Usage;
    }
    
    Console.WriteLine();
    messages.Add(TextChatMessage.Assistant(reply.ToString()));
    if (usage != null)
    {
        Console.WriteLine(
            $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
    }
}

#### Limiting Thinking Length

Use `ThinkingBudget` in `Parameters` to limit the model's thinking length.

```cs
const int budget = 10;
Console.WriteLine($"Set thinking budget to {budget} tokens");
var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
while (true)
{
    Console.Write("User > ");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Please enter a user input.");
        return;
    }

    messages.Add(TextChatMessage.User(input));
    var completion = client.GetTextCompletionStreamAsync(
        new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
        {
            Model = "qwen-turbo",
            Input = new TextGenerationInput() { Messages = messages },
            Parameters = new TextGenerationParameters()
            {
                ResultFormat = "message",
                EnableThinking = true,
                ThinkingBudget = budget,
                IncrementalOutput = true
            }
        });
    var reply = new StringBuilder();
    var reasoning = false;
    TextGenerationTokenUsage? usage = null;
    await foreach (var chunk in completion)
    {
        var choice = chunk.Output.Choices![0];
        if (string.IsNullOrEmpty(choice.Message.ReasoningContent) == false)
        {
            // reasoning
            if (reasoning == false)
            {
                Console.Write("Reasoning > ");
                reasoning = true;
            }

            Console.Write(choice.Message.ReasoningContent);
            continue;
        }

        if (reasoning)
        {
            reasoning = false;
            Console.WriteLine();
            Console.Write("Assistant > ");
        }

        Console.Write(choice.Message.Content);
        reply.Append(choice.Message.Content);
        usage = chunk.Usage;
    }

    Console.WriteLine();
    messages.Add(TextChatMessage.Assistant(reply.ToString()));
    if (usage != null)
    {
        Console.WriteLine(
            $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
    }
}
```

### Web Search

Controlled mainly through `EnableSearch` and `SearchOptions` in `Parameters`.

Example request:

```cs
var request = new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
{
    Model = "qwen-turbo",
    Input = new TextGenerationInput() { Messages = messages },
    Parameters = new TextGenerationParameters()
    {
        ResultFormat = "message",
        EnableThinking = true,
        EnableSearch = true,
        SearchOptions = new TextGenerationSearchOptions()
        {
            SearchStrategy = "max", // max/turbo - controls number of search results
            EnableCitation = true,  // Add source citations to model reply
            CitationFormat = "[ref_<number>]", // Citation format
            EnableSource = true, // Return search source list in SearchInfo
            ForcedSearch = true, // Force model to search
            EnableSearchExtension = true, // Enable vertical domain search, results in SearchInfo.Extra
            PrependSearchResult = true // First packet contains only search results
        }
    }
};
```

### Tool Calling

Provide available tools to the model via `Tools` in `Parameters`. The model returns messages with `ToolCall` properties to invoke tools.

After receiving the message, the server needs to call the corresponding tools and insert results as `Tool` role messages into the conversation history before making another request. The model will summarize the tool call results.

By default, the model calls tools once per turn. To enable multiple tool calls, enable `ParallelToolCalls` in `Parameters`.

```
var tools = new List<ToolDefinition>
{
    new(
        ToolTypes.Function,
        new FunctionDefinition(
            nameof(GetWeather),
            "Get current weather",
            new JsonSchemaBuilder().FromType<WeatherReportParameters>().Build()))
};

var request = new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
{
    Model = "qwen-turbo",
    Input = new TextGenerationInput() { Messages = messages },
    Parameters = new TextGenerationParameters()
    {
        ResultFormat = "message",
        EnableThinking = true,
        IncrementalOutput = true,
        Tools = tools,
        ToolChoice = ToolChoice.AutoChoice,
        ParallelToolCalls = true // Model can call multiple tools at once
    }
}
```

### Structured Output (JSON Output)

Set `ResponseFormat` (remarks: **not** `ResultFormat`) in `Parameters` to JSON to force JSON output.

```csharp
var request = new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
{
    Model = "qwen-plus",
    Input = new TextGenerationInput() { Messages = messages },
    Parameters = new TextGenerationParameters()
    {
        ResultFormat = "message",
        ResponseFormat = DashScopeResponseFormat.Json,
        IncrementalOutput = true
    }
}
```

### Prefix Completion

Place the prefix to complete as an `assistant` message at the end of the `messages` array with `Partial` set to `true`. The model will complete the remaining content based on this prefix.

Deep thinking cannot be enabled in this mode.

```cs
var messages = new List<TextChatMessage>
{
    TextChatMessage.User("Complete following C# method."),
    TextChatMessage.Assistant("public int Fibonacci(int n)", partial: true)
};

var completion = client.GetTextCompletionStreamAsync(
new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
{
    Model = "qwen-turbo",
    Input = new TextGenerationInput() { Messages = messages },
    Parameters = new TextGenerationParameters()
    {
        ResultFormat = "message",
        IncrementalOutput = true
    }
});
```

### Long Context (Qwen-Long)
For Qwen-Long models:
```csharp
var file = new FileInfo("test.txt");
var uploadedFile = await dashScopeClient.UploadFileAsync(file.OpenRead(), file.Name);
var history = new List<ChatMessage> { ChatMessage.File(uploadedFile.Id) };
var completion = await client.client.GetTextCompletionAsync(
        new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
        {
            Model = "qwen-long",
            Input = new TextGenerationInput() { Messages = history },
            Parameters = new TextGenerationParameters()
            {
                ResultFormat = "message",
                EnableThinking = false,
            }
        });
Console.WriteLine(completion.Output.Choices[0].Message.Content);
// Cleanup
await dashScopeClient.DeleteFileAsync(uploadedFile.Id);
```

## Multimodal
Use `GetMultimodalGenerationAsync`/`GetMultimodalGenerationStreamAsync`
[Official Documentation](https://help.aliyun.com/zh/model-studio/multimodal)

### Upload file for multimodal usage

You can upload file to get an oss link before multimodal usage.

```csharp
await using var video = File.OpenRead("sample.mp4");
var ossLink = await client.UploadTemporaryFileAsync("qwen3-vl-plus", video, "sample.mp4");
Console.WriteLine($"File uploaded: {ossLink}");

var messages = new List<MultimodalMessage>();
messages.Add(
    MultimodalMessage.User(
    [
        MultimodalMessageContent.VideoContent(ossLink, fps: 2),
        // MultimodalMessageContent.VideoFrames(links),
        // MultimodalMessageContent.ImageContent(link)
        MultimodalMessageContent.TextContent("这段视频的内容是什么？")
    ]));
```

### Image recognition/thinking

```csharp
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
        Parameters =
            new MultimodalParameters
            {
                IncrementalOutput = true,
                // EnableThinking = true,
                VlHighResolutionImages = false
            }
    });

// output
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
```

### OCR

Base example of OCR

```csharp
// upload file
await using var tilted = File.OpenRead("tilted.png");
var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", tilted, "tilted.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages = new List<MultimodalMessage>();
messages.Add(
    MultimodalMessage.User(
    [
        // set enableRotate to true if your source image is tilted.
        MultimodalMessageContent.ImageContent(ossLink, enableRotate: true),
    ]));
var completion = client.GetMultimodalGenerationStreamAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-vl-ocr-latest",
        Input = new MultimodalInput() { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            IncrementalOutput = true,
        }
    });
var reply = new StringBuilder();
var first = false;
MultimodalTokenUsage? usage = null;
await foreach (var chunk in completion)
{
    var choice = chunk.Output.Choices[0];
    if (first)
    {
        first = false;
        Console.Write("Assistant > ");
    }

    if (choice.Message.Content.Count == 0)
    {
        continue;
    }

    Console.Write(choice.Message.Content[0].Text);
    reply.Append(choice.Message.Content[0].Text);
    usage = chunk.Usage;
}

Console.WriteLine();
messages.Add(MultimodalMessage.Assistant([MultimodalMessageContent.TextContent(reply.ToString())]));
if (usage != null)
{
    Console.WriteLine(
        $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/total({usage.TotalTokens})");
}
```

#### Built-in Tasks

##### Advanced Recognition

When using this task, do not enable streaming. Otherwise, `completion.Output.Choices[0].Message.Content[0].OcrResult.WordsInfo` will be `null`.

In addition to the standard text content, this task also returns the coordinates of the text.

To call this built-in task, set `Parameters.OcrOptions.Task` to `advanced_recognition`. No additional prompt is required.

![](sample/Cnblogs.DashScope.Sample/tilted.png)

```csharp
var messages = new List<MultimodalMessage>();
messages.Add(
    MultimodalMessage.User(
    [
        MultimodalMessageContent.ImageContent(ossLink),
    ]));
var completion = client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-vl-ocr-latest",
        Input = new MultimodalInput() { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            OcrOptions = new MultimodalOcrOptions()
            {
                Task = "advanced_recognition"
            }
        }
    });
```

Output usage:

```csharp
Console.WriteLine("Text:");
Console.WriteLine(completion.Output.Choices[0].Message.Content[0].Text);
Console.WriteLine("WordsInfo:");
foreach (var info in completion.Output.Choices[0].Message.Content[0].OcrResult!.WordsInfo!)
{
    var location = $"[{string.Join(',', info.Location)}]";
    var rect = $"[{string.Join(',', info.RotateRect)}]";
    Console.WriteLine(info.Text);
    Console.WriteLine($"Location: {location}");
    Console.WriteLine($"RotateRect: {rect}");
    Console.WriteLine();
}
```

Output: 

````csharp
Text: 
```json
[
        {"rotate_rect": [236, 254, 115, 299, 90], "text": "OpenAI 兼容"},
        {"rotate_rect": [646, 254, 115, 269, 90], "text": "DashScope"},
        {"rotate_rect": [236, 684, 115, 163, 90], "text": "Python"},
        {"rotate_rect": [492, 684, 115, 105, 90], "text": "Java"},
        {"rotate_rect": [712, 684, 115, 85, 90], "text": "curl"}
]
```
WordsInfo: 
OpenAI 兼容
Location: [46,55,205,55,205,87,46,87]
RotateRect: [125,71,159,32,0]

DashScope
Location: [272,55,415,55,415,87,272,87]
RotateRect: [344,71,32,143,90]

Python
Location: [82,175,169,175,169,207,82,207]
RotateRect: [126,191,32,87,90]

Java
Location: [234,175,289,175,289,207,234,207]
RotateRect: [262,191,55,32,0]

curl
Location: [356,175,401,175,401,207,356,207]
RotateRect: [378,191,32,45,90]
````

##### Key Information Extraction

When using built-in tasks, it is not recommended to enable streaming; otherwise, `completion.Output.Choices[0].Message.Content[0].OcrResult.KvResult` will be `null`.

To invoke this built-in task, set `Parameters.OcrOptions.Task` to `key_information_extraction`. No additional text information needs to be provided.

You can customize the output JSON format via `Parameters.OcrOptions.TaskConfig.ResultSchema` (with a maximum of 3 levels of nesting). If left blank, all fields will be output by default.

For example, suppose we want to extract objects of the following type from an image (JSON property names should, as much as possible, be based on the text present in the image):

![](sample/Cnblogs.DashScope.Sample/receipt.jpg)

```csharp
internal class ReceiptModel()
{
    [JsonPropertyName("乘车日期")]
    public string? Date { get; init; }

    [JsonPropertyName("发票")]
    public ReceiptSerials? Serials { get; init; }
}

internal class ReceiptSerials
{
    [JsonPropertyName("发票代码")]
    public string? Code { get; init; }

    [JsonPropertyName("发票号码")]
    public string? Serial { get; init; }
}
```

Property could be `null` if model failed to extract value for it.
Example request:

```csharp
await using var file = File.OpenRead("receipt.jpg");
var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", file, "receipt.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages =
    new List<MultimodalMessage> { MultimodalMessage.User([MultimodalMessageContent.ImageContent(ossLink)]) };
var completion = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-vl-ocr-latest",
        Input = new MultimodalInput() { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            OcrOptions = new MultimodalOcrOptions()
            {
                Task = "key_information_extraction",
                TaskConfig = new MultimodalOcrTaskConfig()
                {
                    ResultSchema = new Dictionary<string, object>()
                    {
                        {
                            "发票",
                            new Dictionary<string, string>()
                            {
                                { "发票代码", "提取图中的发票代码，通常为一组数字或字母组合" },
                                { "发票号码", "提取发票上的号码，通常由纯数字组成。" }
                            }
                        },
                        { "乘车日期", "对应图中乘车日期时间，格式为年-月-日，比如2025-03-05" }
                    }
                }
            }
        }
    });
```

Consume:

`KvResult` is `JsonElement`，you can deserialize it to any type you desire, or you could just use `Dictionary<string, JsonElement?>` .

````csharp
Console.WriteLine("Text:");
Console.WriteLine(completion.Output.Choices[0].Message.Content[0].Text);
Console.WriteLine("KvResults:");
var model = completion.Output.Choices[0].Message.Content[0].OcrResult!.KvResult?.Deserialize<ReceiptModel>();
Console.WriteLine($"Date: {model?.Date}");
Console.WriteLine($"Code: {model?.Serials?.Code}");
Console.WriteLine($"Serial: {model?.Serials?.Serial}");

/*
Text:
```json
{
    "乘车日期": "2013-06-29",
    "发票": {
        "发票代码": "221021325353",
        "发票号码": "10283819"
    }
}
```
KvResults:
Date: 2013-06-29
Code: 221021325353
Serial: 10283819
Usage: in(524)/out(65)/image(310)/total(589)
*/
````

##### Table Parsing

To invoke this built-in task, set `Parameters.OcrOptions.Task` to `table_parsing`. No additional text information needs to be provided.

This task will extract tables from images and return them in HTML format.

Example:

![](sample/Cnblogs.DashScope.Sample/table.jpg)

```csharp
await using var file = File.OpenRead("table.jpg");
var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", file, "table.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages =
    new List<MultimodalMessage> { MultimodalMessage.User([MultimodalMessageContent.ImageContent(ossLink)]) };
var completion = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-vl-ocr-latest",
        Input = new MultimodalInput { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            OcrOptions = new MultimodalOcrOptions()
            {
                Task = "table_parsing",
            }
        }
    });

Console.WriteLine(completion.Output.Choices[0].Message.Content[0].Text);
```

Return value(Model would wrap the html code in a markdown code fence):

````markdown
```html
<table>
  <tr>
    <td>Record of test data</td>
  </tr>
  <tr>
    <td>Project name：2B</td>
    <td>Control No.CEPRI-D-JS1-JS-057-2022-003</td>
  </tr>
  <tr>
    <td>Case name</td>
    <td>Test No.3 Conductor rupture GL+GR(max angle)</td>
    <td>Last load grade：</td>
    <td>0%</td>
    <td>Current load grade：</td>
  </tr>
  <tr>
    <td>Measure</td>
    <td>Load point</td>
    <td>Load method</td>
    <td>Actual Load(%)</td>
    <td>Actual Load(kN)</td>
  </tr>
  <tr>
    <td>channel</td>
    <td>V1</td>
    <td>活载荷</td>
    <td>147.95</td>
    <td>0.815</td>
  </tr>
  <tr>
    <td>V03</td>
    <td>V2</td>
    <td>活载荷</td>
    <td>111.75</td>
    <td>0.615</td>
  </tr>
  <tr>
    <td>V04</td>
    <td>V3</td>
    <td>活载荷</td>
    <td>9.74</td>
    <td>1.007</td>
  </tr>
  <tr>
    <td>V05</td>
    <td>V4</td>
    <td>活载荷</td>
    <td>7.88</td>
    <td>0.814</td>
  </tr>
  <tr>
    <td>V06</td>
    <td>V5</td>
    <td>活载荷</td>
    <td>8.11</td>
    <td>0.780</td>
  </tr>
  <tr>
    <td>V07</td>
    <td>V6</td>
    <td>活载荷</td>
    <td>8.54</td>
    <td>0.815</td>
  </tr>
  <tr>
    <td>V08</td>
    <td>V7</td>
    <td>活载荷</td>
    <td>6.77</td>
    <td>0.700</td>
  </tr>
  <tr>
    <td>V09</td>
    <td>V8</td>
    <td>活载荷</td>
    <td>8.59</td>
    <td>0.888</td>
  </tr>
  <tr>
    <td>L01</td>
    <td>L1</td>
    <td>活载荷</td>
    <td>13.33</td>
    <td>3.089</td>
  </tr>
  <tr>
    <td>L02</td>
    <td>L2</td>
    <td>活载荷</td>
    <td>9.69</td>
    <td>2.247</td>
  </tr>
  <tr>
    <td>L03</td>
    <td>L3</td>
    <td></td>
    <td>2.96</td>
    <td>1.480</td>
  </tr>
  <tr>
    <td>L04</td>
    <td>L4</td>
    <td></td>
    <td>3.40</td>
    <td>1.700</td>
  </tr>
  <tr>
    <td>L05</td>
    <td>L5</td>
    <td></td>
    <td>2.45</td>
    <td>1.224</td>
  </tr>
  <tr>
    <td>L06</td>
    <td>L6</td>
    <td></td>
    <td>2.01</td>
    <td>1.006</td>
  </tr>
  <tr>
    <td>L07</td>
    <td>L7</td>
    <td></td>
    <td>2.38</td>
    <td>1.192</td>
  </tr>
  <tr>
    <td>L08</td>
    <td>L8</td>
    <td></td>
    <td>2.10</td>
    <td>1.050</td>
  </tr>
  <tr>
    <td>T01</td>
    <td>T1</td>
    <td>活载荷</td>
    <td>25.29</td>
    <td>3.073</td>
  </tr>
  <tr>
    <td>T02</td>
    <td>T2</td>
    <td>活载荷</td>
    <td>27.39</td>
    <td>3.327</td>
  </tr>
  <tr>
    <td>T03</td>
    <td>T3</td>
    <td>活载荷</td>
    <td>8.03</td>
    <td>2.543</td>
  </tr>
  <tr>
    <td>T04</td>
    <td>T4</td>
    <td>活载荷</td>
    <td>11.19</td>
    <td>3.542</td>
  </tr>
  <tr>
    <td>T05</td>
    <td>T5</td>
    <td>活载荷</td>
    <td>11.34</td>
    <td>3.592</td>
  </tr>
  <tr>
    <td>T06</td>
    <td>T6</td>
    <td>活载荷</td>
    <td>16.47</td>
    <td>5.217</td>
  </tr>
  <tr>
    <td>T07</td>
    <td>T7</td>
    <td>活载荷</td>
    <td>11.05</td>
    <td>3.498</td>
  </tr>
  <tr>
    <td>T08</td>
    <td>T8</td>
    <td>活载荷</td>
    <td>8.66</td>
    <td>2.743</td>
  </tr>
  <tr>
    <td>T09</td>
    <td>WT1</td>
    <td>活载荷</td>
    <td>36.56</td>
    <td>2.365</td>
  </tr>
  <tr>
    <td>T10</td>
    <td>WT2</td>
    <td>活载荷</td>
    <td>24.55</td>
    <td>2.853</td>
  </tr>
  <tr>
    <td>T11</td>
    <td>WT3</td>
    <td>活载荷</td>
    <td>38.06</td>
    <td>4.784</td>
  </tr>
  <tr>
    <td>T12</td>
    <td>WT4</td>
    <td>活载荷</td>
    <td>37.70</td>
    <td>5.030</td>
  </tr>
  <tr>
    <td>T13</td>
    <td>WT5</td>
    <td>活载荷</td>
    <td>30.48</td>
    <td>4.524</td>
  </tr>
</table>
```
````

##### Document Parsing

To invoke this built-in task, set `Parameters.OcrOptions.Task` to `document_parsing`. No additional text information needs to be provided.

This task read images(usually scanned PDF) and return them in LaTeX format.

Example:

![](sample/Cnblogs.DashScope.Sample/scanned.jpg)

```csharp
await using var file = File.OpenRead("scanned.jpg");
var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", file, "scanned.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages =
    new List<MultimodalMessage> { MultimodalMessage.User([MultimodalMessageContent.ImageContent(ossLink)]) };
var completion = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-vl-ocr-latest",
        Input = new MultimodalInput { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            OcrOptions = new MultimodalOcrOptions()
            {
                Task = "document_parsing",
            }
        }
    });
```

Returns:

````markdown
```latex
\section*{Qwen2-VL: Enhancing Vision-Language Model's Perception of the World at Any Resolution}

Peng Wang* \quad Shuai Bai* \quad Sinan Tan* \quad Shijie Wang* \quad Zhihao Fan* \quad Jinze Bai*? \\
Keqin Chen \quad Xuejing Liu \quad Jialin Wang \quad Wenbin Ge \quad Yang Fan \quad Kai Dang \quad Mengfei Du \\
Xuancheng Ren \quad Rui Men \quad Dayiheng Liu \quad Chang Zhou \quad Jingren Zhou \quad Junyang Lin*? \\
Qwen Team \quad Alibaba Group

\begin{abstract}
We present the Qwen2-VL Series, an advanced upgrade of the previous Qwen-VL models that redefines the conventional predetermined-resolution approach in visual processing. Qwen2-VL introduces the Naive Dynamic Resolution mechanism, which enables the model to dynamically process images of varying resolutions into different numbers of visual tokens. This approach allows the model to generate more efficient and accurate visual representations, closely aligning with human perceptual processes. The model also integrates Multimodal Rotary Position Embedding (M-RoPE), facilitating the effective fusion of positional information across text, images, and videos. We employ a unified paradigm for processing both images and videos, enhancing the model's visual perception capabilities. To explore the potential of large multimodal models, Qwen2-VL investigates the scaling laws for large vision-language models (LVLMS). By scaling both the model size-with versions at 2B, 8B, and 72B parameters-and the amount of training data, the Qwen2-VL Series achieves highly competitive performance. Notably, the Qwen2-VL-72B model achieves results comparable to leading models such as GPT-4o and Claude3.5-Sonnet across various multimodal benchmarks, outperforming other generalist models. Code is available at \url{https://github.com/QwenLM/Qwen2-VL}.
\end{abstract}

\section{Introduction}

In the realm of artificial intelligence, Large Vision-Language Models (LVLMS) represent a significant leap forward, building upon the strong textual processing capabilities of traditional large language models. These advanced models now encompass the ability to interpret and analyze a broader spectrum of data, including images, audio, and video. This expansion of capabilities has transformed LVLMS into indispensable tools for tackling a variety of real-world challenges. Recognized for their unique capacity to condense extensive and intricate knowledge into functional representations, LVLMS are paving the way for more comprehensive cognitive systems. By integrating diverse data forms, LVLMS aim to more closely mimic the nuanced ways in which humans perceive and interact with their environment. This allows these models to provide a more accurate representation of how we engage with and perceive our environment.

Recent advancements in large vision-language models (LVLMS) (Li et al., 2023c; Liu et al., 2023b; Dai et al., 2023; Zhu et al., 2023; Huang et al., 2023a; Bai et al., 2023b; Liu et al., 2023a; Wang et al., 2023b; OpenAI, 2023; Team et al., 2023) have led to significant improvements in a short span. These models (OpenAI, 2023; Touvron et al., 2023a,b; Chiang et al., 2023; Bai et al., 2023a) generally follow a common approach of \textit{visual encoder} $\rightarrow$ \textit{cross-modal connector} $\rightarrow$ \textit{LLM}. This setup, combined with next-token prediction as the primary training method and the availability of high-quality datasets (Liu et al., 2023a; Zhang et al., 2023; Chen et al., 2023b);

*Equal core contribution, ?Corresponding author

```
````

##### Formula Recognition

To invoke this built-in task, set `Parameters.OcrOptions.Task` to `formula_recognition`. No additional text information needs to be provided.

This task read images(like handwriting formulas) and return them in LaTeX format.

Example:

![](sample/Cnblogs.DashScope.Sample/math.jpg)

```csharp
// upload file
await using var file = File.OpenRead("math.jpg");
var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", file, "math.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages =
    new List<MultimodalMessage> { MultimodalMessage.User([MultimodalMessageContent.ImageContent(ossLink)]) };
var completion = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-vl-ocr-latest",
        Input = new MultimodalInput { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            OcrOptions = new MultimodalOcrOptions()
            {
                Task = "formula_recognition",
            }
        }
    });

Console.WriteLine("LaTeX:");
Console.WriteLine(completion.Output.Choices[0].Message.Content[0].Text);

if (completion.Usage != null)
{
    var usage = completion.Usage;
    Console.WriteLine(
        $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/total({usage.TotalTokens})");
}
```

Returns:

````markdown
```latex
\begin{align*}
\tilde{G}(x) &= \frac{\alpha}{\kappa}x, \quad \tilde{T}_i = T, \quad \tilde{H}_i = \tilde{\kappa}T, \quad \tilde{\lambda}_i = \frac{1}{\kappa}\sum_{j=1}^{m}\omega_j - z_i, \\
L(\{p_n\}; m^n) + L(\{x^n\}, m^n) + L(\{m^n\}; q_n) &= L(m^n; q_n) \\
I^{m_n} - (L+1) &= z + \int_0^1 I^{m_n} - (L)z \leq x_m | L^{m_n} - (L) |^3 \\
&\leq \kappa\partial_1\psi(x) + \frac{\kappa^3}{6}\partial_2^3\psi(x) - V(x) \psi(x) = \int d^3y K(x,y) \psi(y), \\
\int_{B_{\kappa}(0)} I^{m}(w)^2 d\gamma &= \lim_{n\to\infty} \int_{B_{\kappa}(0)} r\psi(w_n)^2 d\gamma = \lim_{n\to\infty} \int_{B_{\kappa}(y_n)} d\gamma \geq \beta > 0,
\end{align*}
```
````

$$
\begin{align*}
\tilde{G}(x) &= \frac{\alpha}{\kappa}x, \quad \tilde{T}_i = T, \quad \tilde{H}_i = \tilde{\kappa}T, \quad \tilde{\lambda}_i = \frac{1}{\kappa}\sum_{j=1}^{m}\omega_j - z_i, \\
L(\{p_n\}; m^n) + L(\{x^n\}, m^n) + L(\{m^n\}; q_n) &= L(m^n; q_n) \\
I^{m_n} - (L+1) &= z + \int_0^1 I^{m_n} - (L)z \leq x_m | L^{m_n} - (L) |^3 \\
&\leq \kappa\partial_1\psi(x) + \frac{\kappa^3}{6}\partial_2^3\psi(x) - V(x) \psi(x) = \int d^3y K(x,y) \psi(y), \\
\int_{B_{\kappa}(0)} I^{m}(w)^2 d\gamma &= \lim_{n\to\infty} \int_{B_{\kappa}(0)} r\psi(w_n)^2 d\gamma = \lim_{n\to\infty} \int_{B_{\kappa}(y_n)} d\gamma \geq \beta > 0,
\end{align*}
$$



##### Text Recognition

To invoke this built-in task, set `Parameters.OcrOptions.Task` to `text_recognition`. No additional text information needs to be provided.

This task read the images and returns content in plain text(Chinese and English only).

Example:

![](sample/Cnblogs.DashScope.Sample/webpage.jpg)

```csharp
// upload file
await using var file = File.OpenRead("webpage.jpg");
var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", file, "webpage.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages =
    new List<MultimodalMessage> { MultimodalMessage.User([MultimodalMessageContent.ImageContent(ossLink)]) };
var completion = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-vl-ocr-latest",
        Input = new MultimodalInput { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            OcrOptions = new MultimodalOcrOptions()
            {
                Task = "text_recognition",
            }
        }
    });
```

Returns

```plaintext
OpenAI 兼容 DashScope
Python Java curl
```

##### Multilanguage

To invoke this built-in task, set `Parameters.OcrOptions.Task` to `multi_lan`. No additional text information needs to be provided.

This task read the images and returns content in plain text(Support more languages).

![](sample/Cnblogs.DashScope.Sample/multilanguage.jpg)

```csharp
await using var file = File.OpenRead("multilanguage.jpg");
var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", file, "multilanguage.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages =
    new List<MultimodalMessage> { MultimodalMessage.User([MultimodalMessageContent.ImageContent(ossLink)]) };
var completion = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-vl-ocr-latest",
        Input = new MultimodalInput { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            OcrOptions = new MultimodalOcrOptions()
            {
                Task = "multi_lan",
            }
        }
    });
```

Returns

```csharp
INTERNATIONAL
MOTHER LANGUAGE
DAY
你好!
Прив?т!
Bonjour!
Merhaba!
Ciao!
Hello!
Ola!
????
Salam!
```

## Text-to-Speech

Create a speech synthesis session using `dashScopeClient.CreateSpeechSynthesizerSocketSessionAsync()`.

Note: Use the using statement to automatically dispose the session, or manually call Dispose() to release resources. Avoid reusing sessions.

Create a synthesis session:
```csharp
using var tts = await dashScopeClient.CreateSpeechSynthesizerSocketSessionAsync("cosyvoice-v2");
var taskId = await tts.RunTaskAsync(new SpeechSynthesizerParameters { Voice = "longxiaochun_v2", Format = "mp3" });
await tts.ContinueTaskAsync(taskId, "Cnblogs");
await tts.ContinueTaskAsync(taskId, "Code changes the world");
await tts.FinishTaskAsync(taskId);
var file = new FileInfo("tts.mp3");
using var stream = file.OpenWrite();
await foreach (var b in tts.GetAudioAsync())
{
    stream.WriteByte(b);
}
Console.WriteLine($"Audio saved to {file.FullName}");
```
## Image Generation
### Text-to-Image
Use shortcuts for Wanx models:
```csharp
var task = await dashScopeClient.CreateWanxImageSynthesisTaskAsync(
    WanxModel.WanxV21Turbo,
    "A futuristic cityscape at sunset",
    new ImageSynthesisParameters { Style = ImageStyles.OilPainting });
// Pull status
while (true)
{
    var result = await dashScopeClient.GetWanxImageSynthesisTaskAsync(task.TaskId);
    if (result.Output.TaskStatus == DashScopeTaskStatus.Succeeded)
    {
        Console.WriteLine($"Image URL: {result.Output.Results[0].Url}");
        break;
    }
    await Task.Delay(500);
}
```
#### Portrait Style Transfer
Use `CreateWanxImageGenerationTaskAsync` and `GetWanxImageGenerationTaskAsync`

#### Background Generation

Use `CreateWanxBackgroundGenerationTaskAsync` and `GetWanxBackgroundGenerationTaskAsync`

## Application Call
```csharp
var request =
    new ApplicationRequest()
    {
        Input = new ApplicationInput() { Prompt = "Summarize this file." },
        Parameters = new ApplicationParameters()
        {
            TopK = 100,
            TopP = 0.8f,
            Seed = 1234,
            Temperature = 0.85f,
            RagOptions = new ApplicationRagOptions()
            {
                PipelineIds = ["thie5bysoj"],
                FileIds = ["file_d129d632800c45aa9e7421b30561f447_10207234"]
            }
        }
    };
var response = await client.GetApplicationResponseAsync("your-application-id", request);
Console.WriteLine(response.Output.Text);
```
`ApplicationRequest` uses `Dictionary<string, object?>` as the default type for `BizParams`.

```csharp
var request =
    new ApplicationRequest()
    {
        Input = new ApplicationInput()
        {
            Prompt = "Summarize this file.",
            BizParams = new Dictionary<string, object?>()
            {
                { "customKey1", "custom-value" }
            }
        }
    };
var response = await client.GetApplicationResponseAsync("your-application-id", request);
Console.WriteLine(response.Output.Text);
```
For strong typing support, you can use the generic class `ApplicationRequest<TBizParams>`.
Note that the SDK uses `snake_case` for JSON serialization. If your application uses different naming conventions, manually specify the serialized property names using `[JsonPropertyName("camelCase")]`.

```csharp
public record TestApplicationBizParam(
    [property: JsonPropertyName("sourceCode")]
    string SourceCode);
var request =
    new ApplicationRequest<TestApplicationBizParam>()
    {
        Input = new ApplicationInput<TestApplicationBizParam>()
        {
            Prompt = "Summarize this file.",
            BizParams = new TestApplicationBizParam("test")
        }
    };
var response = await client.GetApplicationResponseAsync("your-application-id", request);
Console.WriteLine(response.Output.Text);
```

## Text Embeddings

```csharp
var text = "Sample text for embedding";
var response = await dashScopeClient.GetTextEmbeddingsAsync(
    TextEmbeddingModel.TextEmbeddingV4,
    [text],
    new TextEmbeddingParameters { Dimension = 512 });
var embedding = response.Output.Embeddings.First().Embedding;
Console.WriteLine($"Embedding vector length: {embedding.Length}");
```

See [Snapshot Files](./test/Cnblogs.DashScope.Tests.Shared/Utils/Snapshots.cs) for API parameter examples.

Review [Tests](./test) for comprehensive usage examples.
