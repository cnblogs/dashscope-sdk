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
        "ApiKey": "your-api-key"
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
  - [Deep Thinking](#deep-thinking)
  - [Web Search](#web-search)
  - [Tool Calling](#tool-calling)
  - [Prefix Completion](#prefix-completion)
  - [Long Context (Qwen-Long)](#long-context-qwen-long)
- [Multimodal](#multimodal) - QWen-VL, QVQ, etc. Supports reasoning/visual understanding/OCR/audio understanding
- [Text-to-Speech](#text-to-speech) - CosyVoice, Sambert, etc. For TTS applications
- [Image Generation](#image-generation) - wanx2.1, etc. For text-to-image and portrait style transfer
- [Application Invocation](#application-invocation)
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

#### Thinking Models

The model's thinking process is stored in a separate `ReasoningContent` property. When saving to conversation history, ignore it and only keep the model's reply `Content`.

Some models accept `EnableThinking` to control deep thinking, which can be set in `Parameters`.

For more settings on thinking models, see [Deep Thinking](#deep-thinking) below.
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

### Multimodal
Use `GetMultimodalGenerationAsync`/`GetMultimodalGenerationStreamAsync`
[Official Documentation](https://help.aliyun.com/zh/model-studio/multimodal)

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
        Parameters = new MultimodalParameters { IncrementalOutput = true, VlHighResolutionImages = false }
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
### Text-to-Speech

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
### Image Generation
#### Text-to-Image
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

### Application Call
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

### Text Vectorization

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
