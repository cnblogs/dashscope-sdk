English | [简体中文](https://github.com/cnblogs/dashscope-sdk/blob/main/README.zh-Hans.md)

# Cnblogs.DashScopeSDK

[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AI?style=flat&logo=nuget&label=Cnblogs.DashScope.AI)](https://www.nuget.org/packages/Cnblogs.DashScope.AI)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.Sdk?style=flat&logo=nuget&label=Cnblogs.DashScope.Sdk&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.Sdk)](https://www.nuget.org/packages/Cnblogs.DashScope.Sdk)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AspNetCore?style=flat&logo=nuget&label=Cnblogs.DashScope.AspNetCore&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.AspNetCore)](https://www.nuget.org/packages/Cnblogs.DashScope.AspNetCore)

A non-official DashScope (Bailian) service SDK maintained by Cnblogs.

**Note:** This project is actively under development. Breaking changes may occur even in minor versions. Please review the Release Notes before upgrading.

## Quick Start

### Console Application

Install NuGet package `Cnblogs.DashScope.Sdk`
```csharp
// The SDK defaults to the Beijing region, pass region if your API key belongs to another region
var client = new DashScopeClient("your-api-key", region: DashScopeRegion.Singapore);
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
        "RegionId": "ap-southeast-1"
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
### Using `Microsoft.Extensions.AI` Interface

Install NuGet package `Cnblogs.DashScope.AI`

```csharp
var client = new DashScopeClient("your-api-key", region: DashScopeRegion.Singapore).AsChatClient("qwen-max");
var completion = await client.GetResponseAsync("hello");
Console.WriteLine(completion.Text);
```

`IChatClient` picks the endpoint and the max token parameter by the model of each request (`ChatOptions.ModelId`, or the model passed to `AsChatClient()`):

- Endpoint: the models of the multimodal whitelist (qwen3.5 / qwen3.6 series, qwen3.7-plus / qwen3.7-flash, qwen3.8 and later series, qwen3-vl, qwen3-omni, gui-plus, kimi-k2.5 and later kimi models, deepseek-v4.1 and later deepseek models) are called via the multimodal API (`MultimodalParameters`), all the other models (e.g. qwen-plus, qwen-max, qwen3.7-max, deepseek-v4-pro, glm-5.3) via the text API (`TextGenerationParameters`), including the models supplied directly by third parties (e.g. `xiaomi/mimo-v2.6-pro-ultraspeed`) even when they accept images. Calling the wrong one fails with 400 "url error", and the endpoint does not follow the model version: qwen3.7-max is a text model, qwen3.8-max is a multimodal one.
- `ChatOptions.MaxOutputTokens` is sent as `max_completion_tokens` for the models that read it (qwen3.5 and later series, qwen-mt, tongyi-xiaomi-analysis, deepseek-v4-pro and later deepseek models, glm-5.x, kimi-k2.5 and later kimi models, MiniMax-M2.5), which limits the reasoning output and the answer together. It is sent as `max_tokens` for all the other models (e.g. qwen-plus, qwen-max, qwen3-vl-plus, gui-plus), because they silently ignore `max_completion_tokens`; `max_tokens` limits the answer only on thinking models.

Set `AdditionalProperties["useVl"]` / `AdditionalProperties["useMaxCompletionTokens"]` to `true` / `false` to force the mapping, e.g. when a new model appears before the whitelist is updated.

The endpoint can also be fixed for every request of a chat client: `AsChatClient(modelId, useVl)` takes a non-nullable `bool`, `true` for the multimodal API and `false` for the text API. Leave the argument out, or pass `null` as the options of `AsChatClient(modelId, options)`, to decide it by the model.

The endpoint whitelist is shipped with the SDK, so it can lag behind the models the service releases. To pick the endpoint by what the service reports instead, turn on `AutoSyncModelList` through `AsChatClient(modelId, options)`: a model is looked up in the [model list](#model-list) (`GET /api/v1/models?model=<modelId>`) the first time it is used, and it goes to the multimodal API when the service lists it with the `VU` or `Multimodal-Omni` capability and Bailian hosts it (except for `qwen3.8-2.4t-a95b`: the service lists it as a text model although the multimodal API serves it, the SDK corrects that). The looked up models are cached for the lifetime of the process and shared by every chat client of it, so each model costs one extra request per process. When the service does not list the model, or the lookup fails, the chat requests go on with the whitelist and the model is not looked up again for a minute. Nothing is requested when the option is off, or when `useVl` already decides the endpoint.

```csharp
var chatClient = dashScopeClient.AsChatClient(
    "qwen3.7-plus",
    new DashScopeChatClientOptions(AutoSyncModelList: true));
```

#### ToolCall

```csharp
var chatClient = client.AsChatClient("qwen-turbo").AsBuilder().UseFunctionInvocation().Build();
var options = new ChatOptions()
{
    Tools = [AIFunctionFactory.Create(GetWeather)],
    ToolMode = new AutoChatToolMode(),
    AllowMultipleToolCalls = true,
};
var stream = chatClient.GetStreamingResponseAsync("What's weather today?", options);
await foreach (var chatResponseUpdate in stream)
{
    Console.Write(chatResponseUpdate);
}
```

#### Fallback to raw messages

If you need to use input data or parameters not supported by `Microsoft.Extensions.AI`, you can directly invoke the underlying SDK by passing a raw `TextChatMessage` or `MultimodalMessage` via `RawRepresentation`.

Similarly, to pass unsupported parameters, you can also do so directly by setting the `raw` property within `AdditionalProperties`.

Example(Using `qwen-doc-turbo`)

```csharp
var messages = new List<TextChatMessage>()
{
    TextChatMessage.DocUrl(
        "从这两份产品手册中，提取所有产品信息，并整理成一个标准的JSON数组。每个对象需要包含：model(产品的型号)、name(产品的名称)、price(价格（去除货币符号和逗号）)",
        [
            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251107/jockge/%E7%A4%BA%E4%BE%8B%E4%BA%A7%E5%93%81%E6%89%8B%E5%86%8CA.docx",
            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251107/ztwxzr/%E7%A4%BA%E4%BE%8B%E4%BA%A7%E5%93%81%E6%89%8B%E5%86%8CB.docx"
        ])
};
var parameters = new TextGenerationParameters()
{
    ResultFormat = "message", IncrementalOutput = true,
};

var response = client
    .AsChatClient("qwen-doc-turbo")
    .GetStreamingResponseAsync(
        messages.Select(x => new ChatMessage() { RawRepresentation = x }),
        new ChatOptions()
        {
            AdditionalProperties = new AdditionalPropertiesDictionary() { { "raw", parameters } }
        });
await foreach (var chunk in response)
{
    Console.Write(chunk.Text);
}
```

Similarly, you can also retrieve the raw message from the `RawRepresentation` in the message returned by the model.

Example (Getting the token count for an image when calling `qwen3-vl-plus`):

```csharp
var response = client
    .AsChatClient("qwen3-vl-plus")
    .GetStreamingResponseAsync(
        new List<ChatMessage>()
        {
            new(
                ChatRole.User,
                new List<AIContent>()
                {
                    new UriContent(
                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241022/emyrja/dog_and_girl.jpeg",
                        MediaTypeNames.Image.Jpeg),
                    new UriContent(
                        "https://dashscope.oss-cn-beijing.aliyuncs.com/images/tiger.png",
                        MediaTypeNames.Image.Jpeg),
                    new TextContent("这些图展现了什么内容？")
                })
        },
        new ChatOptions());
var lastChunk = (ChatResponseUpdate?)null;
await foreach (var chunk in response)
{
    Console.Write(chunk.Text);
    lastChunk = chunk;
}

Console.WriteLine();

// Access underlying raw response
var raw = lastChunk?.RawRepresentation as ModelResponse<MultimodalOutput, MultimodalTokenUsage>;
Console.WriteLine($"Image token usage: {raw?.Usage?.ImageTokens}");
```

## Workspace and Region

Bailian serves different regions with different endpoints. The SDK defaults to the Beijing region (`cn-beijing`). If your API key belongs to another region, specify it via the `region` parameter (or `RegionId` in configuration):

| `DashScopeRegion` | Region ID | Endpoint without workspace |
| ----------------- | --------- | -------------------------- |
| `Beijing` (default) | `cn-beijing` | `https://dashscope.aliyuncs.com` |
| `Singapore` | `ap-southeast-1` | `https://dashscope-intl.aliyuncs.com` |
| `HongKong` | `cn-hongkong` | `https://cn-hongkong.dashscope.aliyuncs.com` |
| `UsEast` | `us-east-1` | workspace required |
| `Germany` | `eu-central-1` | workspace required |
| `Japan` | `ap-northeast-1` | workspace required |

Console application:

```csharp
var client = new DashScopeClient("your-api-key", region: DashScopeRegion.Singapore);
```

ASP.NET Core:

```csharp
builder.Services.AddDashScopeClient("your-api-key", region: DashScopeRegion.Singapore);
```

### Workspace

If you are using a Bailian workspace (业务空间), pass the workspace id together with its region. The SDK then switches to the workspace-specific endpoint `{workspaceId}.{regionId}.maas.aliyuncs.com` and appends the `X-DashScope-WorkSpace` header automatically. Note that the `UsEast`, `Germany` and `Japan` regions do not provide a shared endpoint, a workspace id is required for them:

```csharp
var client = new DashScopeClient(
    "your-api-key",
    workspaceId: "your-workspace-id",
    region: DashScopeRegion.Singapore);
// requests are sent to https://{workspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/
```

`appsettings.json`

```json
{
    "DashScope": {
        "ApiKey": "your-api-key",
        "WorkspaceId": "your-workspace-id",
        "RegionId": "ap-southeast-1"
    }
}
```

If you access Bailian through a proxy, set `BaseAddress`/`WebsocketBaseAddress` (or the `baseAddress` parameter) instead. An explicit base address always takes precedence over region and workspace.

### Content Inspection (AI Safety Guardrail)

Bailian supports the `X-DashScope-DataInspection` request header, which further inspects input and output content for violations using the AI safety guardrail. The inspection is disabled by default (the header is not sent). The guardrail service needs to be enabled on your account first, otherwise the API returns `403 Header.AccessDenied`. Not available for Qwen-Audio models.

Enable it for all requests on the client:

```csharp
// Console application
var client = new DashScopeClient("your-api-key")
{
    DataInspection = DataInspectionOptions.CipInputAndOutput
};

// ASP.NET Core
builder.Services.AddDashScopeClient("your-api-key");
builder.Services.Configure<DashScopeOptions>(o => o.DataInspection = DataInspectionOptions.CipInputAndOutput);
```

Or enable/override it on a single request:

```csharp
var completion = await client.GetTextCompletionAsync(new ModelRequest<TextGenerationInput, ITextGenerationParameters>
{
    Model = "qwen-plus",
    Input = new TextGenerationInput { Messages = new List<TextChatMessage> { TextChatMessage.User("你好") } },
    Parameters = new TextGenerationParameters { ResultFormat = "message" },
    DataInspection = DataInspectionOptions.CipInput
});
```

Available values are `Disabled` (default), `CipInput`, `CipOutput` and `CipInputAndOutput`. Request level configuration takes precedence over client level configuration, and setting `DataInspectionOptions.Disabled` on a request disables client level inspection for that request.

Client level configuration also applies to the WebSocket API (e.g. CosyVoice), which only supports enabling inspection for both directions at once, so any enabled direction turns on the `X-DashScope-DataInspection` header for WebSocket connections. The header is not sent by default. For a single WebSocket session, pass the `dataInspection` parameter when creating the session; when it differs from the client level configuration, a temporary connection is created for that session only and disposed afterwards.

## Supported APIs

- [Text Generation](#text-generation) - QWen3, DeepSeek, etc. Supports reasoning/tool calling/web search/translation scenarios
  - [Conversation](#conversation)
  - [Thinking Models](#thinking-models)
  - [Web Search](#web-search)
  - [Tool Calling](#tool-calling)
  - [Prefix Completion](#prefix-completion)
  - [Long Context (Qwen-Long)](#long-context-qwen-long)
  - [Code Interpreter](#code-interpreter)
  - [PPT Generation (Qwen-doc-turbo)](#ppt-generation-qwen-doc-turbo)
- [Multimodal](#multimodal) - QWen-VL, QVQ, etc. Supports reasoning/visual understanding/OCR/audio understanding
  - [Upload file for multimodal usage](#upload-file-for-multimodal-usage)
  - [Image Recognition/Thinking](#image-recognition/thinking)
  - [OCR](#ocr)
    - [Advanced Recognition](#advanced-recognition)
    - [Key Information Extraction](#key-information-extraction)
    - [Table Parsing](#table-parsing)
    - [Document Parsing](#document-parsing)
    - [Formula Recognition](#formula-recognition)
    - [Text Recognition](#text-recognition)
    - [Multilanguage](#multilanguage)
  - [GUI](#gui)
  - [Audio Understanding](#audio-understanding)
- [Text-to-Speech](#text-to-speech) - CosyVoice, Sambert, etc. For TTS applications
- [Real-Time Speech Recognition](#real-time-speech-recognition) - Paraformer, Qwen-Audio ASR. For streaming ASR applications
- [Recorded Speech Recognition](#recorded-speech-recognition) - qwen3-asr-flash, Fun-ASR-Flash, Qwen-Audio ASR. For recorded audio files
- [Image Generation](#image-generation) - qwen-image-2.0-pro, wanx2.1, etc. For text-to-image, image editing, portrait style transfer and image translation
- [Video Generation](#video-generation) - wan2.7/wan3.0, PixVerse, Kling, Vidu, etc. For text-to-video, image-to-video and video editing
- [3D Generation (Tripo)](#3d-generation-tripo) - Tripo-H3.1/P1.0 3D generation, Gaussian Splat, mesh post-processing and animation
- [Application Call](#application-call)
- [Batch](#batch)
- [Text Embeddings](#text-embeddings)
- [Multimodal Embedding](#multimodal-embedding) - tongyi-embedding-vision-plus/flash, qwen3-vl-embedding, qwen2.5-vl-embedding, multimodal-embedding-v1. For embedding texts, images and videos into one vector space
- [Text Rerank](#text-rerank) - qwen3-rerank, qwen3.7-text-rerank, gte-rerank-v2, qwen3-vl-rerank. For refining the documents recalled by a vector search
- [Omni-Modal Translation (Qwen-MT-Uni)](#omni-modal-translation-qwen-mt-uni) - qwen-mt-uni. For translating texts, documents, images and audio
- [Decision Model (System One)](#decision-model-system-one) - decision-model. For classification, yes/no judgement and scoring with a single forward pass, without generating text
- [Model List](#model-list) - Query the models of Bailian: capabilities, modalities, features, prices and token limits


## Text Generation

Use `dashScopeClient.GetTextCompletionAsync` and `dashScopeClient.GetTextCompletionStreamAsync()` to access text generation APIs.

Common models: `qwen-max` `qwen-plus` `qwen-flush` etc.

Basic example (see [Workspace and Region](#workspace-and-region) for client configuration):

```csharp
var client = new DashScopeClient("your-api-key", region: DashScopeRegion.Singapore);
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
        ResponseFormat = DashScopeResponseFormat.JsonObject,
        IncrementalOutput = true
    }
}
```

To make the model strictly follow a given JSON Schema, use `DashScopeResponseFormat.JsonSchema()` with a `DashScopeJsonSchema` (`Strict` is recommended to be `true`).

```csharp
var request = new ModelRequest<MultimodalInput, IMultimodalParameters>()
{
    Model = "qwen3.8-flash",
    Input = new MultimodalInput() { Messages = messages },
    Parameters = new MultimodalParameters()
    {
        ResponseFormat = DashScopeResponseFormat.JsonSchema(new DashScopeJsonSchema(
            "calculation",
            new JsonObject
            {
                ["type"] = "object",
                ["properties"] = new JsonObject
                {
                    ["result"] = new JsonObject { ["type"] = "number" }
                },
                ["required"] = new JsonArray("result"),
                ["additionalProperties"] = false
            },
            Strict: true))
    }
}
```

Notes on using `json_schema`:

- Only a few models support `json_schema` (e.g. qwen3.7/qwen3.8 series). Check the [structured output doc](https://docs.bailian.console.aliyun.com/zh/model-studio/qwen-structured-output) before using it, otherwise fall back to `json_object`.
- Among them, qwen3.7-plus / qwen3.7-flash and the qwen3.8 series are called via the multimodal API (`MultimodalParameters`), while qwen3.7-max is called via the text API (`TextGenerationParameters`): the endpoint does not follow the model version.
- Unlike `json_object`, `json_schema` does not require the prompt to contain the word "json".
- In the `IChatClient` (Microsoft.Extensions.AI) integration, when `ChatResponseFormat.ForJsonSchema(...)` is used: whitelisted models (qwen3.7/qwen3.8 series) are automatically mapped to `json_schema`; other models fall back to `json_object` (the schema is ignored). Set `AdditionalProperties["useJsonSchema"]` to `true`/`false` to force the mapping (similar to `useVl`), e.g. when a new model supports `json_schema` before the whitelist is updated. `ChatResponseFormat.Json` is always mapped to `json_object`.

### Agents (Microsoft Agent Framework)

The `IChatClient` returned by `AsChatClient()` works with [Microsoft Agent Framework](https://learn.microsoft.com/en-us/dotnet/ai/microsoft-agent-framework-overview) (`Microsoft.Agents.AI` NuGet package). The agent keeps the conversation history itself and passes the agent-level persona via `ChatOptions.Instructions`, which this SDK maps to a leading system message; tool calls are executed by the agent loop automatically.

```csharp
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

var chatClient = dashScopeClient.AsChatClient("qwen-plus");
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
        return;
    }

    var response = await agent.RunAsync(input, session);
    Console.WriteLine($"Assistant > {response.Text}");
}
```

See `sample/Cnblogs.DashScope.Sample/MsExtensionsAI/AgentExample.cs` for a runnable sample. If you prefer no extra dependency, `ChatOptions.Instructions` + tool messages (`FunctionCallContent`/`FunctionResultContent`) are enough to build a minimal agent loop by hand, see [Tool Calling](#tool-calling).

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
var uploadedFile = await dashScopeClient.OpenAiCompatibleUploadFileAsync(file.OpenRead(), file.Name);
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
await dashScopeClient.OpenAiCompatibleDeleteFileAsync(uploadedFile.Id);
```

### Code Interpreter

**This capability is mutually exclusive with Function Call and cannot be used simultaneously.**

Use the `EnableCodeInterpreter` parameter in `Parameters` to allow the model to write code and call an internal code interpreter for calculations.

Example Request:

```csharp
var completion = client.GetTextCompletionStreamAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen3-max-preview",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
            EnableThinking = true,
            EnableCodeInterpreter = true,
            IncrementalOutput = true
        }
    });
```

The code that model generated will be included in `chunk.Output.ToolInfo.CodeInterpreter`. The invocation process can be considered part of the reasoning process.

Full example,

```csharp
var messages = new List<TextChatMessage>();
const string input = "123的21次方是多少？";
Console.Write($"User > {input}");
messages.Add(TextChatMessage.User(input));
var completion = client.GetTextCompletionStreamAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
    {
        Model = "qwen3-max-preview",
        Input = new TextGenerationInput { Messages = messages },
        Parameters = new TextGenerationParameters
        {
            ResultFormat = "message",
            EnableThinking = true,
            EnableCodeInterpreter = true,
            IncrementalOutput = true
        }
    });
var reply = new StringBuilder();
var codeGenerated = false;
var reasoning = false;
TextGenerationTokenUsage? usage = null;
await foreach (var chunk in completion)
{
    var choice = chunk.Output.Choices![0];
    var tool = chunk.Output.ToolInfo?.FirstOrDefault();
    if (codeGenerated == false && tool?.CodeInterpreter != null)
    {
        Console.WriteLine($"Code > {tool.CodeInterpreter.Code}");
        codeGenerated = true;
    }

    if (string.IsNullOrEmpty(choice.Message.ReasoningContent) == false)
    {
        // reasoning
        if (reasoning == false)
        {
            Console.WriteLine();
            Console.Write("Reasoning > ");
            reasoning = true;
        }

        Console.Write(choice.Message.ReasoningContent);
        continue;
    }

    if (reasoning && string.IsNullOrEmpty(choice.Message.Content.Text) == false)
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
        $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/plugins({usage.Plugins?.CodeInterpreter?.Count})/total({usage.TotalTokens})");
}

/*
User > 123的21次方是多少？
Reasoning > 用户问的是123的21次方是多少。这是一个大数计算问题，我需要使用代码计算器来计算这个值。

我需要调用code_interpreter函数，传入计算123**21的Python代码。
123**21
用户询问123的21次方是多少，我使用代码计算器计算出了结果。结果是一个非常大的数字：77269364466549865653073473388030061522211723

我应该直接给出这个结果，因为这是一个精确的数学计算问题，不需要额外的解释或
Assistant > 123的21次方是：77269364466549865653073473388030061522211723
Usage: in(704)/out(234)/reasoning(142)/plugins(1)/total(938)
*/
```



### PPT Generation (Qwen-doc-turbo)

Use `qwen-doc-turbo` with the PPT skill to generate a presentation from a document URL or prompt. Pass the document link via `TextChatMessage.DocUrl` and enable the PPT skill through `Parameters.Skill`.

- `DashScopeModelSkill.PptCreative` — generate a presentation in creative mode (no template required).
- `DashScopeModelSkill.PptGeneral(templateId)` — generate in general mode using the given template.

The model emits the slide storyboard in `ReasoningContent`, and the final reply `Content` contains a download URL for the generated `.pptx` file.

```csharp
var messages = new List<TextChatMessage>
{
    TextChatMessage.System("You are a helpful assistant"),
    TextChatMessage.DocUrl("生成一个ppt", ["https://example.com/product-manual.docx"]),
};
var completion = client.GetTextCompletionStreamAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-doc-turbo",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
            IncrementalOutput = true,
            Skill = [DashScopeModelSkill.PptCreative]
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
        if (reasoning == false)
        {
            reasoning = true;
            Console.Write("Reasoning > ");
        }

        Console.Write(choice.Message.ReasoningContent);
        continue;
    }

    if (reasoning && string.IsNullOrEmpty(choice.Message.Content.Text) == false)
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
if (usage != null)
{
    Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
}

// reply.ToString() contains the .pptx download URL
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

#### Limit Upload Speed

You can limit the upload speed for all file upload APIs (`UploadTemporaryFileAsync`, `OpenAiCompatibleUploadFileAsync`, `UploadFilesAsync`) by setting `MaximumUploadSpeed`. The default value is `-1` (no limit).

Console application:

```csharp
var client = new DashScopeClient("your-api-key", region: DashScopeRegion.Singapore);
client.MaximumUploadSpeed = 1024 * 1024; // 1 MB/s
```

ASP.NET Core application (`appsettings.json`):

```json
{
    "DashScope": {
        "ApiKey": "your-api-key",
        "MaximumUploadSpeed": 1048576
    }
}
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

### GUI

Use `gui-plus` to generate standardized operational information based on screenshots and user intent.

Currently, the main capabilities are implemented through the `System Prompt`, where you can configure the model’s available capabilities and the output JSON format.

Sample system prompt:

```markdown
## 1. Core Role
You are an expert AI Vision Operation Agent. Your task is to analyze computer screenshots, understand user instructions, and then break down the task into single, precise GUI atomic operations.
## 2. [CRITICAL] JSON Schema & Absolute Rules
Your output **must** be a JSON object that strictly adheres to the following rules. **Any deviation will result in failure**.
- **[R1] Strict JSON**: Your response **must** be *and only be* a JSON object. Do not add any text, comments, or explanations before or after the JSON code block.
- **[R2] Strict `thought` Structure**: The `thought` object must contain a single sentence briefly describing your thought process. For example: "The user wants to open the browser. I see the Chrome icon on the desktop, so the next step is to click it."
- **[R3] Precise `action` Value**: The value of the `action` field **must** be an uppercase string defined in `## 3. Toolset` (e.g., `"CLICK"`, `"TYPE"`). No leading/trailing spaces or case variations are allowed.
- **[R4] Strict `parameters` Structure**: The structure of the `parameters` object **must** be **perfectly identical** to the template defined for the selected Action in `## 3. Toolset`. Key names and value types must match exactly.
## 3. Toolset (Available Actions)
### CLICK
- **Description**: Click on the screen.
- **Parameters Template**:
  {
    "x": <integer>,
    "y": <integer>,
    "description": "<string, optional: A short string describing what you are clicking on, e.g., 'Chrome browser icon' or 'Login button'.>"
  }

### TYPE
- **Description**: Type text.
- **Parameters Template**:
{
  "text": "<string>",
  "needs_enter": <boolean>
}

### SCROLL
- **Description**: Scroll the window.
- **Parameters Template**:
{
  "direction": "<'up' or 'down'>",
  "amount": "<'small', 'medium', or 'large'>"
}

### KEY_PRESS
- **Description**: Press a function key.
- **Parameters Template**:
{
  "key": "<string: e.g., 'enter', 'esc', 'alt+f4'>"
}

### FINISH
- **Description**: Task completed successfully.
- **Parameters Template**:
{
  "message": "<string: A summary of the task completion>"
}

### FAILE
- **Description**: Task cannot be completed.
- **Parameters Template**:
{
  "reason": "<string: A clear explanation of the failure reason>"
}

## 4. Thinking and Decision Framework
Before generating each action, strictly follow the following thought-verification process:
**Goal Analysis**: What is the user's ultimate goal?
**Screen Observation (Grounded Observation)**: Carefully analyze the screenshot. Your decisions must be based on visual evidence present in the screenshot. If you cannot see an element, you cannot interact with it.
**Action Decision**: Based on the goal and visible elements, select the most appropriate tool.
**Construct Output**:
a. Record your thought process in the `thought` field.
b. Select an `action`.
c. Precisely copy the `parameters` template for that action and fill in the values.
**Final Verification (Self-Correction)**: Before outputting, perform a final check:
- Is my response pure JSON?
- Is the `action` value correct (uppercase, no spaces)?
- Is the `parameters` structure 100% identical to the template? For example, for `CLICK`, are there separate `x` and `y` keys, and are their values integers?
```

Request:

```csharp
var messages = new List<MultimodalMessage>
{
    MultimodalMessage.System([MultimodalMessageContent.TextContent(SystemPrompt)]),
    MultimodalMessage.User(
    [
        MultimodalMessageContent.ImageContent(
            "https://img.alicdn.com/imgextra/i2/O1CN016iJ8ob1C3xP1s2M6z_!!6000000000026-2-tps-3008-1758.png"),
        MultimodalMessageContent.TextContent("Open browser")
    ])
};
var completion = client.GetMultimodalGenerationStreamAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "gui-plus",
        Input = new MultimodalInput() { Messages = messages },
        Parameters = new MultimodalParameters() { IncrementalOutput = true, }
    });
```

Response:

```csharp
{
  "thought": "用户想打开浏览器，我看到了桌面上的Google Chrome图标，因此下一步是点击它。",
  "action": "CLICK",
  "parameters": {
    "x": 1089,
    "y": 123
  }
}
```

Then you can execute the command that model returns, and reply the screenshot with next intension.

### Audio Understanding

Example(use `Qwen3-Omni-Captioner`)

```csharp
// upload file
await using var audio = File.OpenRead("noise.wav");
var ossLink = await client.UploadTemporaryFileAsync("qwen3-omni-30b-a3b-captioner", audio, "noise.wav");
Console.WriteLine($"File uploaded: {ossLink}");
var messages = new List<MultimodalMessage>
{
    MultimodalMessage.User(
    [
        // 也可以直接传入公网地址
        MultimodalMessageContent.AudioContent(ossLink),
    ])
};
var completion = client.GetMultimodalGenerationStreamAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen3-omni-30b-a3b-captioner",
        Input = new MultimodalInput() { Messages = messages },
        Parameters = new MultimodalParameters() { IncrementalOutput = true, }
    });
var reply = new StringBuilder();
var first = true;
MultimodalTokenUsage? usage = null;
await foreach (var chunk in completion)
{
    var choice = chunk.Output.Choices[0];
    if (first)
    {
        first = false;
        Console.WriteLine();
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
        $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/audio({usage.InputTokensDetails?.AudioTokens})/total({usage.TotalTokens})");
}
```

Sample output

```csharp
Assistant > The audio clip opens with a rapid, percussive metallic clatter, reminiscent of a typewriter or similar mechanical device, which continues in a steady rhythm throughout the recording. This clatter is slightly left-of-center in the stereo field and is accompanied by a faint, low-frequency hum, likely from a household appliance or HVAC system. The acoustic environment is a small, enclosed room with hard surfaces, indicated by the short, bright reverberation of both the clatter and the speaker’s voice. The audio quality is moderate, with a noticeable electronic hiss and some loss of high-frequency detail, but no digital distortion or clipping.

At the one-second mark, a male voice enters, positioned slightly right-of-center and closer to the microphone. He speaks in standard Mandarin, with a tone of weary exasperation: “哎 呀，这样我还怎么安静工作啊？” (“Aiyā, zěnyàng wǒ hái zěnme ānjìng gōngzuò a?”), which translates to “Oh, how can I possibly work quietly like this?” His speech is clear, with a slightly rising pitch on “安静” (“quietly”) and a falling pitch on “啊” (“a”), conveying a sense of complaint and fatigue. The accent is standard, with no regional inflection, and the voice is that of a young to middle-aged adult male.

Throughout the clip, the mechanical clatter remains constant and prominent, occasionally competing with the voice for clarity. There are no other sounds, such as footsteps, additional voices, or environmental noises, and the background is otherwise quiet. The interplay between the persistent mechanical noise and the speaker’s complaint creates a vivid sense of disruption and frustration, suggesting an environment where work is being impeded by an external, uncontrolled sound source.

Culturally, the use of Mandarin, standard pronunciation, and modern recording quality indicate a contemporary, urban Chinese setting. The language and tone are universally relatable, reflecting a common experience of being disturbed during work. The lack of regional markers or distinctive background noises suggests a generic, possibly domestic or office-like space, but with no clear indicators of a specific location or social context.

In summary, the audio portrays a modern Mandarin-speaking man, exasperated by a constant, distracting mechanical noise (likely a typewriter or similar device), attempting to work in a small, reverberant room. The recording’s technical and acoustic features reinforce the sense of disruption and frustration, while the language and setting suggest a contemporary, urban Chinese context.
Usage: in(160)/out(514)/audio(152)/total(674)
```

## Text-to-Speech

The non-realtime speech synthesis models - qwen3-tts (e.g. `qwen3-tts-flash`) and MiniMax speech (e.g. `MiniMax/speech-2.8-hd`) - use the multimodal generation HTTP API instead of a websocket session. The text and voice settings go into `MultimodalInput`; qwen3-tts answers with a downloadable wav url (base64 chunks in `Audio.Data` when streaming), MiniMax speech with hex encoded audio in `Output.Data` (or a url when `OutputFormat` is set to `url`):

```csharp
// qwen3-tts, non-streaming: the response carries the download url of the complete wav file
var response = await dashScopeClient.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>
    {
        Model = "qwen3-tts-flash",
        Input = new MultimodalInput
        {
            Text = "你好，我是千问。",
            Voice = "Cherry",
            LanguageType = "Chinese"
        }
    });
Console.WriteLine($"Audio url (valid for 24 hours): {response.Output.Audio!.Url}");

// qwen3-tts, streaming: intermediate frames carry base64 wav chunks for immediate playback
await foreach (var frame in dashScopeClient.GetMultimodalGenerationStreamAsync(
                   new ModelRequest<MultimodalInput, IMultimodalParameters>
                   {
                       Model = "qwen3-tts-flash",
                       Input = new MultimodalInput { Text = "你好，我是千问。", Voice = "Cherry" }
                   }))
{
    if (frame.Output.Audio?.Data is { Length: > 0 } data)
    {
        PlayAudio(Convert.FromBase64String(data)); // e.g. wav, 24 kHz 16 bit mono
    }
}

// MiniMax speech, non-streaming: the response carries the hex encoded mp3 audio
var minimaxResponse = await dashScopeClient.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>
    {
        Model = "MiniMax/speech-2.8-hd",
        Input = new MultimodalInput
        {
            Text = "你好，我是千问。",
            VoiceSetting = new MinimaxVoiceSetting { VoiceId = "male-qn-qingse", Emotion = "happy" },
            AudioSetting = new MinimaxAudioSetting { Format = "mp3", SampleRate = 32000 }
        }
    });
File.WriteAllBytes("minimax-tts.mp3", Convert.FromHexString(minimaxResponse.Output.Data!.Audio!));
```

In MiniMax streaming mode the intermediate frames carry hex chunks in `Output.Data` (`Status` 1) and the last frame aggregates the whole synthesis (`Status` 2); set `StreamOptions.ExcludeAggregatedAudio` to omit the aggregated audio from the last frame. Mixed voices (`TimbreWeights`), pronunciation replacement (`PronunciationDict`), voice effects (`VoiceModify`) and more voice settings are available on `MultimodalInput`.

The qwen-audio-tts models (e.g. `qwen-audio-3.0-tts-flash`) also have an HTTP mode via `GetSpeechSynthesisAsync` / `GetSpeechSynthesisStreamAsync`, with all parameters (voice, format, sample rate, speech rate, pitch, seed, `LanguageHints`, `Instruction`, AIGC tag, `HotFix` etc.) on `SpeechSynthesisInput`. Unlike the APIs above, this one is only served on the workspace domain, so configure `WorkspaceId` (same region as the API key) on the client:

```csharp
builder.Services.AddDashScopeClient("your-api-key", workspaceId: "your-workspace-id");

// non-streaming: the response carries the download url of the complete audio file
var response = await dashScopeClient.GetSpeechSynthesisAsync(
    new ModelRequest<SpeechSynthesisInput>
    {
        Model = "qwen-audio-3.0-tts-flash",
        Input = new SpeechSynthesisInput
        {
            Text = "我家的后面有一个很大的花园。",
            Voice = "longanhuan_v3.6",
            Format = "wav",
            SampleRate = 24000
        }
    });
Console.WriteLine($"Audio url (valid for 24 hours): {response.Output.Audio!.Url}");

// streaming: sentence events with base64 audio chunks, the final frame carries the url
await foreach (var frame in dashScopeClient.GetSpeechSynthesisStreamAsync(
                   new ModelRequest<SpeechSynthesisInput>
                   {
                       Model = "qwen-audio-3.0-tts-flash",
                       Input = new SpeechSynthesisInput
                       {
                           Text = "我家的后面有一个很大的花园。",
                           Voice = "longanhuan_v3.6",
                           Format = "wav",
                           WordTimestampEnabled = true
                       }
                   }))
{
    if (frame.Output.Audio?.Data is { Length: > 0 } data)
    {
        PlayAudio(Convert.FromBase64String(data));
    }

    if (frame.Output.Type == "sentence-end")
    {
        // word timestamps in frame.Output.Sentence.Words when enabled
    }
}
```

The same endpoint also serves audio generation with the qwen-audio-*-tts-next models (e.g. `qwen-audio-3.1-tts-next`): describe the audio in `TextPrompt` (reference audios are referenced in order as `@voice1` / `@voice2` / `@voice3`) and optionally pass up to 3 `References` (a public url or a base64 data uri each, 30 seconds / 10 MB at most; wav, mp3 or ogg opus), generated podcasts run up to 240 seconds and other scenarios up to 120 seconds:

```csharp
// generate a two-speaker dialogue with two reference audios
var response = await dashScopeClient.GetSpeechSynthesisAsync(
    new ModelRequest<SpeechSynthesisInput>
    {
        Model = "qwen-audio-3.1-tts-next",
        Input = new SpeechSynthesisInput
        {
            TextPrompt = "@voice1 says: \"What a sunny day!\" @voice2 answers: \"Let's go to the park.\"",
            References = new List<SpeechSynthesisReference>
            {
                new() { AudioUrl = "https://your-reference-1.wav" },
                new() { AudioUrl = "https://your-reference-2.wav" }
            },
            Format = "wav",
            SampleRate = 48000,
            Channels = 2
        }
    });
Console.WriteLine($"Audio url (24h): {response.Output.Audio!.Url}, duration: {response.Output.Audio.Duration}s");
```

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
The session is shared by the cosyvoice models and the qwen-audio-3.1-tts / qwen-audio-3.0-tts models (e.g. `qwen-audio-3.1-tts-flash`), which report token based usage (`InputTokens` / `OutputTokens` / `TotalTokens` alongside `Characters`) in sentence-end and task-finished events, and additionally support the opus output format.

The Sambert models (`sambert-*-v1`, Beijing region only, the voice is part of the model name) use the one-shot calling mode instead: the whole text is passed to `SynthesizeAsync` in a single run-task (streaming: out) and continue-task / finish-task are not supported. Sambert sentence events carry sentence level `BeginTime`/`EndTime`, word timestamps without index fields, and phoneme level timestamps (enable `WordTimestampEnabled` + `PhonemeTimestampEnabled`):
```csharp
using var tts = await dashScopeClient.CreateSpeechSynthesizerSocketSessionAsync("sambert-zhichu-v1");
await tts.SynthesizeAsync(
    new SpeechSynthesizerParameters { Format = "wav", WordTimestampEnabled = true, PhonemeTimestampEnabled = true },
    "床前明月光，疑是地上霜");
// audio in tts.GetAudioAsync(), sentences with phoneme timestamps in tts.GetMessagesAsync()
```

The qwen3-tts-*-realtime models (e.g. `qwen3-tts-flash-realtime`) use a different realtime API (`CreateTtsRealtimeSocketSessionAsync`): the connection is established and `session.created` awaited before the session is returned, configure it with `UpdateSessionAsync` (voice, output format, and the `server_commit` / `commit` interaction mode), append text with `AppendTextAsync`, force the synthesis with `CommitTextAsync` and end it with `FinishAsync`. The audio is delivered as base64 chunks inside `response.audio.delta` events and exposed decoded via `GetAudioAsync()`, the raw events (including `response.done` with the billing usage) via `GetEventsAsync()`. Unlike the speech synthesizer session, the server closes the connection after `session.finished`, so a realtime session cannot be reused for a second run:
```csharp
using var tts = await dashScopeClient.CreateTtsRealtimeSocketSessionAsync("qwen3-tts-flash-realtime");
await tts.UpdateSessionAsync(new TtsRealtimeSession { Voice = "Cherry", ResponseFormat = "mp3", SampleRate = 24000 });
await tts.AppendTextAsync("你好，我是千问。");
await tts.CommitTextAsync();
await tts.FinishAsync();
var file = new FileInfo("tts-realtime.mp3");
using var stream = file.OpenWrite();
await foreach (var b in tts.GetAudioAsync())
{
    stream.WriteByte(b);
}
```

### Voice Cloning and Designing

Custom voices are managed with `CustomizeVoiceAsync` (model `voice-enrollment`, same workspace domain as the qwen-audio-tts HTTP API): clone a voice from a sample audio or design one from a text description (`Action = "create_voice"`), then list / query / update / delete voices. Which `VoiceCustomizationInput` members are required depends on the action; the preview audio of a designed voice comes back as base64 in `Output.PreviewAudio`:

```csharp
// clone a voice from a sample audio (public url or base64 data uri)
var clone = await dashScopeClient.CustomizeVoiceAsync(
    new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
    {
        Model = "voice-enrollment",
        Input = new VoiceCustomizationInput
        {
            Action = "create_voice",
            TargetModel = "qwen-audio-3.0-tts-flash",
            Prefix = "myvoice",
            Audio = "https://your-audio-url.wav",
            LanguageHints = new List<string> { "zh" }
        }
    });
var clonedVoiceId = clone.Output.VoiceId; // use it as the voice of synthesis requests

// design a voice from a text description, the preview audio comes back as base64 wav
var design = await dashScopeClient.CustomizeVoiceAsync(
    new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
    {
        Model = "voice-enrollment",
        Input = new VoiceCustomizationInput
        {
            Action = "create_voice",
            TargetModel = "cosyvoice-v3.5-plus",
            VoicePrompt = "沉稳的中年男性，音色低沉浑厚",
            PreviewText = "各位听众朋友们大家好，欢迎收听本期节目",
            Prefix = "announcer",
            LanguageHints = new List<string> { "zh" }
        },
        Parameters = new VoiceCustomizationParameters { SampleRate = 24000, ResponseFormat = "wav" }
    });
File.WriteAllBytes(
    "preview.wav",
    Convert.FromBase64String(design.Output.PreviewAudio!.Data!));

// list voices (filter by prefix), query / update / delete by voice id
var list = await dashScopeClient.CustomizeVoiceAsync(
    new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
    {
        Model = "voice-enrollment",
        Input = new VoiceCustomizationInput { Action = "list_voice", Prefix = "myvoice" }
    });
```

Created voices go through a review: `Status` is DEPLOYING while reviewing, OK when usable and UNDEPLOYED when rejected. MiniMax voices are cloned on the shared domain instead: call `GetMultimodalGenerationAsync` with `MultimodalInput.Action = "voice_clone"`, the sample url (`AudioUrl`), a globally unique `VoiceId`, and the `Text` of the audition audio (billed at the synthesis price); the audition comes back in `Output.DemoAudio`. MiniMax voices cannot be managed (listed / updated / deleted) via these APIs.

## Real-Time Speech Recognition
Create a real-time speech recognition session using `dashScopeClient.CreateSpeechRecognitionSocketSessionAsync()`.

Audio is sent to the server as binary websocket frames, and recognition results are continuously returned via `GetMessagesAsync()`. Results with `SentenceEnd` set to `false` are intermediate results of the sentence, and the one with `true` is the final result.

Note: Use the using statement to automatically dispose the session, or manually call Dispose() to release resources. Avoid reusing sessions.

```csharp
using var asr = await dashScopeClient.CreateSpeechRecognitionSocketSessionAsync("paraformer-realtime-v2");
var taskId = await asr.RunTaskAsync(
    new SpeechRecognitionParameters
    {
        Format = "wav",
        SampleRate = 16000,
        LanguageHints = ["zh"],
        MaxSentenceSilence = 800
    });
await using var audio = File.OpenRead("speech.wav");
await asr.SendAudioAsync(audio); // or send chunks yourself via SendAudioAsync(ReadOnlyMemory<byte>)
await asr.FinishTaskAsync(taskId);

var duration = 0;
await foreach (var message in asr.GetMessagesAsync())
{
    var sentence = message.Payload.Output?.Sentence;
    if (sentence?.SentenceEnd == true)
    {
        Console.WriteLine($"Recognized: {sentence.Text}");
        duration = Math.Max(duration, message.Payload.Usage?.Duration ?? 0);
    }
}

Console.WriteLine($"Billed duration: {duration}s");
```

The session is shared by the Fun-ASR-Realtime and Qwen-Audio ASR models as well, which support the conversation context, instant hot words and more (see `SpeechRecognitionParameters` for the members each model supports):

- `fun-asr-realtime`: intermediate results are always returned, and the words of the final result carry settled timestamps (`Fixed`). Supports the conversation context, `VocabularyId` and `LanguageHints` (one value takes effect).
- `qwen-audio-3.1-asr-flash-message`: returns the final result of each sentence only, set `IntermediateResultEnabled` to get intermediate results as well. `DisfluencyRemovalEnabled` also removes repetitions and self-corrections from the final text. Finishing the task before any audio was sent fails with `NO_VALID_AUDIO_ERROR`.
- `qwen-audio-3.1-asr-flash-streaming` / `qwen-audio-3.0-asr-flash-streaming`: intermediate results are always returned. `SpecialWordFilter` replaces or removes sensitive words. 3.1 reports token usage, 3.0 reports the audio duration it is billed by.

The conversation context (previous turns of the user's speech and the LLM's replies) improves the recognition of proper nouns: pass it in the input of `RunTaskAsync` and update it while the task is running with `ContinueTaskAsync` (message model only):

```csharp
using var asr = await dashScopeClient.CreateSpeechRecognitionSocketSessionAsync("qwen-audio-3.1-asr-flash-message");
var taskId = await asr.RunTaskAsync(
    new SpeechRecognitionParameters
    {
        Format = "wav",
        SampleRate = 16000,
        IntermediateResultEnabled = true, // off by default for this model
        Vocabulary = new Dictionary<string, int> { ["博客园"] = 5 } // instant hot words, weight 1~5 or 50
    },
    new SpeechRecognitionInput
    {
        Context =
        [
            new SpeechRecognitionContext("user", [new SpeechRecognitionContextContent("input_text", "博客园是什么时候成立的")]),
            new SpeechRecognitionContext("assistant", [new SpeechRecognitionContextContent("text", "博客园成立于2004年。")])
        ]
    });
await using var audio = File.OpenRead("speech.wav");
await asr.SendAudioAsync(audio);
// the context can be updated while the task is running
await asr.ContinueTaskAsync(taskId, new SpeechRecognitionInput
{
    Context = [new SpeechRecognitionContext("user", [new SpeechRecognitionContextContent("input_text", "代码改变世界")])]
});
await asr.FinishTaskAsync(taskId);

var duration = 0;
await foreach (var message in asr.GetMessagesAsync())
{
    var sentence = message.Payload.Output?.Sentence;
    if (sentence?.SentenceEnd == true)
    {
        Console.WriteLine($"Recognized: {sentence.Text}");
        duration = Math.Max(duration, message.Payload.Usage?.Duration ?? 0);
    }
    else if (sentence?.Text?.Length > 0)
    {
        // intermediate results may be revised, update the text of the sentence identified by SentenceId instead of appending them
        Console.WriteLine($"Interim result of sentence {sentence.SentenceId}: {sentence.Text}");
    }
}

Console.WriteLine($"Billed duration: {duration}s");
```

Sensitive word filtering of the streaming models. The server expects the filter JSON-encoded as a string, the SDK takes care of it:

```csharp
using var asr = await dashScopeClient.CreateSpeechRecognitionSocketSessionAsync("qwen-audio-3.1-asr-flash-streaming");
var taskId = await asr.RunTaskAsync(
    new SpeechRecognitionParameters
    {
        Format = "pcm",
        SampleRate = 16000,
        SpecialWordFilter = new SpeechRecognitionSpecialWordFilter
        {
            SystemReservedFilter = true, // filtering is off by default
            FilterWithSigned = new SpeechRecognitionSpecialWordList { WordList = ["代码"] }, // replaced with **
            FilterWithEmpty = new SpeechRecognitionSpecialWordList { WordList = ["世界"] } // removed
        }
    });
// send audio and read the results the same way as above
```

Read the usage from the final result of each sentence, the `task-finished` event does not always carry it.

### Qwen3-ASR-Flash-Realtime
`qwen3-asr-flash-realtime` models use a separate realtime session (`CreateAsrRealtimeSocketSessionAsync`), which talks to the `/api-ws/v1/realtime` endpoint with event-based messages: configure the session (audio format, language, VAD) with `UpdateSessionAsync`, stream the audio with `AppendAudioAsync` (base64 encoded inside the messages, no binary frames) and end it with `FinishAsync`. The server closes the connection after `session.finished`, so a session cannot be reused.

The session starts in the VAD mode, where the server detects speech boundaries and submits the audio automatically, delivering incremental results in `conversation.item.input_audio_transcription.text` events (the accumulated text arrives in `Stash`, `Text` stays empty) and the final one in `...completed` (with the token usage). Leave `TurnDetection` at its default to keep the server's VAD settings, or set a configuration to tune them. Setting it to null (sent as an explicit `turn_detection` null) switches to the manual mode, where you call `CommitAudioAsync` to submit the buffered audio:

```csharp
using var asr = await dashScopeClient.CreateAsrRealtimeSocketSessionAsync("qwen3-asr-flash-realtime");
await asr.UpdateSessionAsync(
    new AsrRealtimeSession
    {
        InputAudioFormat = "pcm", // or opus
        SampleRate = 16000, // or 8000
        InputAudioTranscription = new AsrRealtimeTranscription { Language = "zh" }, // unset to auto detect
        // the VAD mode with custom settings; leave it unset to keep the server settings,
        // null switches to the manual mode (commit below)
        TurnDetection = new AsrRealtimeTurnDetection { Threshold = 0.0f, SilenceDurationMs = 400 }
    });
await using var audio = File.OpenRead("speech.pcm");
await asr.AppendAudioAsync(audio); // sent as base64 chunks of 3200 bytes
// manual mode only: submit the buffered audio for recognition
// await asr.CommitAudioAsync();
// VAD mode only: give the server a moment after the audio, see below
await Task.Delay(TimeSpan.FromSeconds(1));
await asr.FinishAsync();

await foreach (var e in asr.GetEventsAsync())
{
    switch (e.Type)
    {
        case "conversation.item.input_audio_transcription.text":
            Console.WriteLine($"Interim: {e.Stash}");
            break;
        case "conversation.item.input_audio_transcription.completed":
            Console.WriteLine($"Recognized: {e.Transcript} (language: {e.Language}, emotion: {e.Emotion})");
            break;
    }
}
```

In the VAD mode, do not call `FinishAsync` right after the last audio: `input_audio_buffer.append` has no acknowledgement, and a finish that arrives before the server has consumed the buffered audio ends the session without any transcription. Pushing a 4 second clip in one message lost it every time, in 3200 byte chunks (the `Stream` overload above) it takes about 300 ms, so wait a second to be safe. A live microphone stream delivers the audio continuously and does not run into this.

## Real-Time Voice Conversation (Qwen-Audio-Realtime)
The qwen-audio-3.x-realtime models (plus / flash) are full-duplex voice assistants: stream the user audio in, the server runs VAD / ASR / LLM / TTS in a pipeline and streams back the response text and audio. Create a session with `CreateAudioChatSocketSessionAsync` (same `/api-ws/v1/realtime` endpoint, event-based messages):

```csharp
using var chat = await dashScopeClient.CreateAudioChatSocketSessionAsync("qwen-audio-3.1-realtime-plus");
await chat.UpdateSessionAsync(
    new AudioChatSession
    {
        Voice = "longanqian", // longanqian_v3.1 on 3.1 plus
        Instructions = "You are a helpful assistant.", // system prompt of the session
        TurnDetection = new AudioChatTurnDetection
        {
            Type = "server_vad", // or "smart_turn": meaningless sounds ("um") do not trigger a turn
            Threshold = 0.5f,
            SilenceDurationMs = 500
        }
        // set TurnDetection = null instead for the push-to-talk mode: commit the audio with
        // CommitAudioAsync() and trigger the inference with CreateResponseAsync()
    });

// consume the events while the audio streams in (see below), then send the audio
await using var audio = File.OpenRead("speech.pcm"); // raw pcm, 16kHz 16bit mono
await chat.AppendAudioAsync(audio);
```

In the server_vad / smart_turn mode the server detects the speech boundaries and triggers the inference automatically; the interim ASR results arrive as `conversation.item.input_audio_transcription.delta` events (`Text` is the confirmed part, `Stash` the tentative remainder), the streamed response text and audio as `response.audio_transcript.delta` / `response.audio.delta` (base64 pcm, 24kHz — also available decoded via `GetAudioAsync()`), and a turn ends with `response.done`:

```csharp
var consuming = Task.Run(async () =>
{
    // ReSharper disable once AccessToDisposedClosure
    await foreach (var e in chat.GetEventsAsync())
    {
        switch (e.Type)
        {
            case "conversation.item.input_audio_transcription.delta":
                Console.WriteLine($"Heard: {e.Text}{e.Stash}");
                break;
            case "response.audio_transcript.delta":
                Console.WriteLine($"Said: {e.Delta}");
                break;
        }
    }
});
await using var file = File.Create("response.pcm");
await foreach (var b in chat.GetAudioAsync())
{
    file.WriteByte(b);
}
```

Beyond the audio loop the session also supports text input and history injection (`CreateItemAsync` with message items), function calling (`Tools` in the session config, execute the call and write the result back with a `function_call_output` item, then trigger the next turn with `CreateResponseAsync`), interrupting the model (`CancelResponseAsync`), and managing the conversation (`RetrieveItemAsync` / `DeleteItemAsync`). The 3.1 plus model additionally takes `InputAudioTranscription.Language` / `OutputAudio.Language` (both directions of translation-like control) and `MaxHistoryTurns`; smart_turn accepts `VoiceprintAudioUrls` for speaker enhancement. The session ends when it is disposed — there is no session.finish.

## Real-Time Live Translation (Qwen-Livetranslate-Realtime)
The qwen3.5 / qwen3.8-livetranslate-flash-realtime models translate live speech (and video frames) into a target language. Create a session with `CreateLiveTranslateSocketSessionAsync`, configure the translation and stream the source audio:

```csharp
using var translator = await dashScopeClient.CreateLiveTranslateSocketSessionAsync("qwen3.5-livetranslate-flash-realtime");
await translator.UpdateSessionAsync(
    new LiveTranslateSession
    {
        InputAudioFormat = "pcm", // or opus
        SampleRate = 16000,
        InputAudioTranscription = new LiveTranslateTranscription(
            Model: "qwen3-asr-flash-realtime", Language: "zh"), // Model = null disables the source echo
        TurnDetection = new LiveTranslateTurnDetection { Threshold = 0.2f, SilenceDurationMs = 1000 },
        Translation = new LiveTranslateTranslation
        {
            Language = "en", // target language, defaults to en
            Corpus = new LiveTranslateCorpus
            {
                // hotwords: the source phrase always translates to the given target phrase
                Phrases = new Dictionary<string, string> { ["博客园"] = "Cnblogs" }
            }
        }
    });

// consume the events while the audio streams in (see below), then send the audio and finish
await using var audio = File.OpenRead("speech.pcm");
await translator.AppendAudioAsync(audio);
// AppendImageAsync(frame) sends video frames as base64 jpg (at most 2 per second, qwen3.8 only)
await translator.FinishAsync(); // the server finishes the remaining translation, then ends the session
```

The source text arrives as `conversation.item.input_audio_transcription.*` events and the translated text as `response.audio_transcript.text` / `.delta` events (`Text` + `Stash` accumulate the interim translation, the final one arrives in `response.audio_transcript.done` and `response.done`); the synthesized translation audio is available via `GetAudioAsync()`:

```csharp
await foreach (var e in translator.GetEventsAsync())
{
    switch (e.Type)
    {
        case "conversation.item.input_audio_transcription.completed":
            Console.WriteLine($"Source: {e.Transcript} ({e.Language}, {e.Emotion})");
            break;
        case "response.audio_transcript.done":
            Console.WriteLine($"Translated: {e.Transcript}");
            break;
    }
}
```

The qwen3.8 model takes a different shape: `OutputModalities` plus `Audio.Input.TurnDetection` (speaker_detection separates speakers, the source and the translation items are correlated via `PreviousItemId`), and voice cloning (`EnableVoiceClone` + `VoiceCloneOptions.Frequency` never / once / always, with `Voice` set to "default" or a pre-cloned voice id) works on both models. In the manual mode (`TurnDetection = null`) the buffered audio is submitted with `CommitAudioAsync()`, which triggers the translation automatically.

### Non-realtime translation (qwen3-livetranslate-flash)
The qwen3-livetranslate-flash models translate an audio / video file in one shot. They **cannot be called via the DashScope API** - they are only served on the OpenAI compatible endpoint (`/compatible-mode/v1/chat/completions`), which the SDK exposes as the generic `OpenAiCompatibleGetChatCompletionAsync` / `OpenAiCompatibleGetChatCompletionStreamAsync` methods (the livetranslate models only support the streaming mode). Prefer the DashScope methods (or the Microsoft.Extensions.AI integration) whenever the model supports both styles; use the OpenAI compatible family only when it does not:

```csharp
var chunks = dashScopeClient.OpenAiCompatibleGetChatCompletionStreamAsync(
    new OpenAiCompatibleChatRequest
    {
        Model = "qwen3-livetranslate-flash",
        Messages =
        [
            new OpenAiCompatibleChatMessage
            {
                Role = "user",
                Content =
                [
                    // an audio file; a video goes in a video_url part (Url)
                    new OpenAiCompatibleChatContentPart(
                        Type: "input_audio",
                        InputAudio: new OpenAiCompatibleChatInputAudio(Data: audioUrl, Format: "wav"))
                ]
            }
        ],
        Modalities = ["text", "audio"], // audio output is optional, ["text"] also works
        Audio = new OpenAiCompatibleChatAudioOptions(Voice: "Cherry", Format: "wav"),
        StreamOptions = new OpenAiCompatibleChatStreamOptions(IncludeUsage: true), // usage in the last chunk
        TranslationOptions = new OpenAiCompatibleTranslationOptions(SourceLang: "zh", TargetLang: "en")
    });

var text = new StringBuilder();
await foreach (var chunk in chunks)
{
    text.Append(chunk.Choices[0].Delta?.Content); // the translated text, delta by delta
    var audio = chunk.Choices[0].Delta?.Audio?.Data; // the base64 wav deltas, when audio output is on
    var usage = chunk.Usage; // present in the last chunk with IncludeUsage
}
```

## Recorded Speech Recognition

### qwen3-asr-flash
Recognize an audio file (up to 5 minutes) with `GetMultimodalGenerationAsync`, passing the audio url (or a base64 data uri) as the audio content and the options in `AsrOptions`. A system message provides context (background text, entity word lists) that improves the recognition of proper nouns. The recognized text is in the message content, the language and emotion of the audio are in the annotations.

```csharp
var response = await dashScopeClient.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen3-asr-flash",
        Input = new MultimodalInput()
        {
            Messages =
            [
                MultimodalMessage.System([MultimodalMessageContent.TextContent("This is a recording of a classical Chinese poem")]),
                MultimodalMessage.User([MultimodalMessageContent.AudioContent(audioUrl)])
            ]
        },
        Parameters = new MultimodalParameters()
        {
            AsrOptions = new AsrOptions { Language = "zh", EnableItn = true }
        }
    });

var choice = response.Output.Choices[0];
Console.WriteLine($"Recognized: {choice.Message.Content[0].Text}");
Console.WriteLine($"Language: {choice.Message.Annotations![0].Language}, emotion: {choice.Message.Annotations[0].Emotion}");
Console.WriteLine($"Audio duration: {response.Usage?.Seconds}s, tokens: {response.Usage?.TotalTokens}");
```

### Fun-ASR-Flash / Qwen-Audio-3.x-ASR-Flash
Fun-ASR-Flash and Qwen-Audio-3.x-ASR-Flash models transcribe recorded audio files with sentence / word level timestamps on the same endpoint, but with typed contents and a sentence based response: call `GetMultimodalGenerationAsync` (or `GetMultimodalGenerationStreamAsync`, which returns intermediate results when the audio is at least 1 minute long; words with `Fixed` set to `false` may still be revised). The audio goes into an `InputAudioContent` (a publicly accessible url or a base64 data uri), previous turns of the conversation go into `InputTextContent` / `AssistantTextContent` as the context, and the result is in `Output.Text` / `Output.Sentence` instead of `Output.Choices`.

```csharp
var response = await dashScopeClient.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-audio-3.1-asr-flash",
        Input = new MultimodalInput()
        {
            Messages =
            [
                MultimodalMessage.User([MultimodalMessageContent.InputTextContent("Please recite Cong Jun Xing")]),
                MultimodalMessage.Assistant([MultimodalMessageContent.AssistantTextContent("Which one would you like to hear?")]),
                MultimodalMessage.User([MultimodalMessageContent.InputAudioContent(audioUrl)])
            ]
        },
        Parameters = new MultimodalParameters()
        {
            Format = "mp3",
            SampleRate = "16000",
            Vocabulary = new Dictionary<string, int> { ["青海"] = 5, ["楼兰"] = 5 }, // instant hot words, weight 1~5 or 50
            SpeakerDiarizationEnabled = true // qwen-audio-3.1-asr-flash only, returns Output.Sentences with SpeakerId
        }
    });

Console.WriteLine($"Recognized: {response.Output.Text}");
foreach (var sentence in response.Output.Sentences ?? [response.Output.Sentence!])
{
    Console.WriteLine($"Speaker {sentence.SpeakerId}: {sentence.Text} [{sentence.BeginTime}ms ~ {sentence.EndTime}ms]");
}
```

### Long audio files (file transcription)
Transcribe long audio files asynchronously with the file transcription models (qwen3-asr-flash-filetrans, qwen-audio-3.x-asr-flash-filetrans, fun-asr, paraformer-v2): submit a task with `CreateSpeechTranscriptionTaskAsync`, poll it with `GetSpeechTranscriptionTaskAsync`, and download the JSON result file from the `TranscriptionUrl` (valid for 24 hours) once the task succeeds. Which input members and parameters each model family supports varies, see `SpeechTranscriptionParameters` for the details — qwen3-asr-flash-filetrans takes a single `FileUrl` and returns `Output.Result`, while the other models take urls in `FileUrls` and return per-file results in `Output.Results` (Paraformer accepts multiple urls in one task).

```csharp
var task = await dashScopeClient.CreateSpeechTranscriptionTaskAsync(
    new ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>()
    {
        Model = "paraformer-v2",
        Input = new SpeechTranscriptionInput() { FileUrls = [audioUrl] },
        Parameters = new SpeechTranscriptionParameters()
        {
            DisfluencyRemovalEnabled = true, // remove filler words
            DiarizationEnabled = true, // speaker diarization, result carries speaker_id
            SpeakerCount = 2 // optional reference count of speakers
        }
    });

DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>? result;
do
{
    await Task.Delay(TimeSpan.FromSeconds(2));
    result = await dashScopeClient.GetSpeechTranscriptionTaskAsync(task.Output.TaskId);
}
while (result.Output.TaskStatus is DashScopeTaskStatus.Pending or DashScopeTaskStatus.Running);

// check SubTaskStatus when multiple files were submitted: the task status is SUCCEEDED
// as long as any subtask succeeded
foreach (var file in result.Output.Results!)
{
    Console.WriteLine($"{file.FileUrl}: {file.SubTaskStatus} -> {file.TranscriptionUrl}");
}
```

## Custom Hotword Vocabularies
Boost the recognition accuracy of domain terms with custom hotword vocabularies, managed with `CustomizeVocabularyAsync` (model `speech-biasing`, served on the workspace domain only — configure `WorkspaceId` as for the voice cloning API). The action picks the operation: create a vocabulary of hotwords (`Action = "create_vocabulary"`), then list / query / update / delete vocabularies. The returned id feeds the `VocabularyId` parameter of recognition requests and the vocabulary must be created with the same `TargetModel` as the recognition requests use:

```csharp
var created = await dashScopeClient.CustomizeVocabularyAsync(
    new ModelRequest<VocabularyCustomizationInput>
    {
        Model = "speech-biasing",
        Input = new VocabularyCustomizationInput
        {
            Action = "create_vocabulary",
            TargetModel = "fun-asr", // must match the model of the recognition requests
            Prefix = "myvocab", // digits and lowercase letters, at most 10 characters
            Vocabulary = [new VocabularyWord(Text: "博客园", Weight: 4, Lang: "zh")]
        }
    });
var vocabularyId = created.Output.VocabularyId; // vocab-{prefix}-{id}

// list (filter by prefix), query / update / delete by vocabulary id
var list = await dashScopeClient.CustomizeVocabularyAsync(
    new ModelRequest<VocabularyCustomizationInput>
    {
        Model = "speech-biasing",
        Input = new VocabularyCustomizationInput { Action = "list_vocabulary", Prefix = "myvocab" }
    });
```

Hotwords are `(Text, Weight, Lang)` triples — weight range [1, 5] (commonly 4), and `Lang` is optional (detected automatically when unset). update_vocabulary replaces the whole hotword list; update_vocabulary and delete_vocabulary return an empty `Output`, the vocabulary detail (`Status`, `TargetModel`, hotwords) is returned by query_vocabulary.

## Image Generation
### Text-to-Image
Use `GetMultimodalGenerationAsync` with model `qwen-image-2.0-pro` to generate images synchronously.
```csharp
var response = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-image-2.0-pro",
        Input = new MultimodalInput()
        {
            Messages = new List<MultimodalMessage>()
            {
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>()
                    {
                        MultimodalMessageContent.TextContent("A futuristic cityscape at sunset")
                    })
            }
        },
        Parameters = new MultimodalParameters()
        {
            NegativePrompt = "low resolution, low quality, deformed limbs, distorted fingers, oversaturated, wax figure, lack of facial detail, overly smooth, AI artifacts, chaotic composition, blurry or distorted text.",
            N = 1,
            PromptExtend = true,
            Watermark = false,
            Size = "2048*2048"
        }
    });
var url = response.Output.Choices[0].Message.Content[0].Image;
Console.WriteLine($"Generated image url: {url}");
```

Wan models work the same way: `wan2.6-t2i` supports synchronous calls via the same API and allows free image size as long as total pixels stay in [1280*1280, 1440*1440] and aspect ratio stays in [1:4, 4:1]. Models at `wan2.5-t2i-preview` and below only support the async task API (see Text-to-Image (Async Task)).
```csharp
var response = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "wan2.6-t2i",
        Input = new MultimodalInput()
        {
            Messages = new List<MultimodalMessage>()
            {
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>()
                    {
                        MultimodalMessageContent.TextContent("一间有着精致窗户的花店，漂亮的木质门，摆放着花朵")
                    })
            }
        },
        Parameters = new MultimodalParameters()
        {
            N = 1,
            Size = "1280*1280",
            PromptExtend = true,
            Watermark = false,
            Seed = 233
        }
    });
var url = response.Output.Choices[0].Message.Content[0].Image;
Console.WriteLine($"Generated image url: {url}");
```

The `wan2.7-image` / `wan2.7-image-pro` models add thinking mode (`ThinkingMode`), color palette control (`ColorPalette`, ratios sum up to 100.00%), sequential image group generation (`EnableSequential`, `n` becomes the max count 1-12) and interactive editing with bounding boxes (`BboxList`, one list of `[x1, y1, x2, y2]` boxes per input image). `wan2.7-image-pro` supports 4K output for text-to-image (2K for editing and groups).
```csharp
var response = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "wan2.7-image",
        Input = new MultimodalInput()
        {
            Messages = new List<MultimodalMessage>()
            {
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>()
                    {
                        MultimodalMessageContent.TextContent("一间有着精致窗户的花店，漂亮的木质门，摆放着花朵")
                    })
            }
        },
        Parameters = new MultimodalParameters()
        {
            Size = "1K",
            N = 1,
            Watermark = false,
            ThinkingMode = true,
            ColorPalette =
            [
                new MultimodalColorPalette("#C2D1E6", "50.00%"),
                new MultimodalColorPalette("#CDD8E9", "30.00%"),
                new MultimodalColorPalette("#B5C8DB", "20.00%")
            ]
        }
    });
var url = response.Output.Choices[0].Message.Content[0].Image;
Console.WriteLine($"Generated image url: {url}");
```

The lightweight `z-image-turbo` model generates a single image fast over the same API, with Chinese/English text rendering. `size` defaults to `1024*1536` with total pixels in [512*512, 2048*2048]; note that `prompt_extend` defaults to `false` and is billed higher when enabled — when set to `true` the response also carries the rewritten prompt and the reasoning process (`ReasoningContent`).
```csharp
var response = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "z-image-turbo",
        Input = new MultimodalInput()
        {
            Messages = new List<MultimodalMessage>()
            {
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>()
                    {
                        MultimodalMessageContent.TextContent("一只坐着的橘黄色的猫，表情愉悦，活泼可爱，逼真准确。")
                    })
            }
        },
        Parameters = new MultimodalParameters() { PromptExtend = false, Seed = 233, Size = "1120*1440" }
    });
var url = response.Output.Choices[0].Message.Content[0].Image;
Console.WriteLine($"Generated image url: {url}");
```

### Image Editing
Qwen-image edit models (`qwen-image-2.0-pro`, `qwen-image-2.0`, and the unified generation-and-editing models `qwen-image-3.0-pro`, `qwen-image-3.0`, `qwen-image-2.1-pro`, `qwen-image-2.1-turbo`) edit images by instruction through the same multimodal API: 1~3 input images (up to 10 for the qwen-image-2.1 series) followed by one text instruction, generating 1~6 images. When multiple images are provided, the output aspect ratio follows the last one.
```csharp
var response = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-image-2.0-pro",
        Input = new MultimodalInput()
        {
            Messages = new List<MultimodalMessage>()
            {
                MultimodalMessage.User(
                [
                    MultimodalMessageContent.ImageContent("<base-image-url>"),
                    MultimodalMessageContent.ImageContent("<style-image-url>"),
                    MultimodalMessageContent.TextContent("Use the city photo in image 1 as the base. Do not change the buildings, streets, vehicles or people. Draw three cartoon figures from image 2 around the building in a flat poster illustration style.")
                ])
            }
        },
        Parameters = new MultimodalParameters()
        {
            NegativePrompt = "low resolution, low quality, chaotic composition",
            N = 1,
            PromptExtend = true,
            PromptExtendMode = "direct",
            Seed = 233,
            Size = "1328*1328",
            Watermark = false
        }
    });
var url = response.Output.Choices[0].Message.Content[0].Image;
Console.WriteLine($"Edited image url: {url}");
```
`PromptExtendMode` controls how `PromptExtend` rewrites the prompt: `direct` (default, DPE) or `agent` (APE, text-to-image only, rejected for image editing). The unified models also accept the same message-based input as an async task via `CreateImageGenerationTaskAsync` / `GetImageGenerationTaskAsync`.

To edit a local image, upload it first and pass the returned OSS link:
```csharp
await using var image = File.OpenRead("input.jpg");
var ossLink = await client.UploadTemporaryFileAsync("qwen-image-2.0-pro", image, "input.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages = new List<MultimodalMessage>
{
    MultimodalMessage.User(
    [
        MultimodalMessageContent.ImageContent(ossLink),
        MultimodalMessageContent.TextContent("Please enhance the clarity of this photo")
    ])
};
var response = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-image-2.0-pro",
        Input = new MultimodalInput() { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            NegativePrompt = "low resolution, low quality, deformed limbs, distorted fingers, oversaturated, wax figure, lack of facial detail, overly smooth, AI artifacts, chaotic composition, blurry or distorted text.",
            N = 1,
            PromptExtend = true,
            Watermark = false,
            Size = "2048*2048"
        }
    });
var url = response.Output.Choices[0].Message.Content[0].Image;
Console.WriteLine($"Generated image url: {url}");
```

`wan2.6-image` supports image editing with 1~4 reference images, and interleaved text-image output when `EnableInterleave` is set to `true` (streaming only).
```csharp
var response = await client.GetMultimodalGenerationAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "wan2.6-image",
        Input = new MultimodalInput()
        {
            Messages = new List<MultimodalMessage>()
            {
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>()
                    {
                        MultimodalMessageContent.TextContent("参考图1的风格和图2的背景，生成番茄炒蛋"),
                        MultimodalMessageContent.ImageContent("<reference-image-1-url>"),
                        MultimodalMessageContent.ImageContent("<reference-image-2-url>")
                    })
            }
        },
        Parameters = new MultimodalParameters() { N = 1, EnableInterleave = false, Size = "1K" }
    });
var url = response.Output.Choices[0].Message.Content[0].Image;
Console.WriteLine($"Generated image url: {url}");
```

The `wan2.5-i2i-preview` model edits images by instruction with up to 3 reference images, keeping subject consistency across images. It runs as an async task and reports the rewritten prompt in the result:
```csharp
var response = await client.CreateImage2ImageSynthesisTaskAsync(
    new ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>()
    {
        Model = "wan2.5-i2i-preview",
        Input = new Image2ImageSynthesisInput()
        {
            Prompt = "将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
            Images = ["<reference-image-1-url>", "<reference-image-2-url>"]
        },
        Parameters = new Image2ImageSynthesisParameters() { N = 1 }
    });
var task = await client.GetImage2ImageSynthesisTaskAsync(response.Output.TaskId);
Console.WriteLine($"Edited image url: {task.Output.Results?[0].Url}, actual prompt: {task.Output.Results?[0].ActualPrompt}");
```

The classic `wanx2.1-imageedit` model edits images through predefined functions via an async task, using the same input type with a `Function`:
```csharp
var response = await client.CreateImage2ImageSynthesisTaskAsync(
    new ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>()
    {
        Model = "wanx2.1-imageedit",
        Input = new Image2ImageSynthesisInput()
        {
            Function = "description_edit",
            Prompt = "把她的头发修改为红色。",
            BaseImageUrl = "<your-image-url>"
        },
        Parameters = new Image2ImageSynthesisParameters() { N = 1 }
    });
var task = await client.GetImage2ImageSynthesisTaskAsync(response.Output.TaskId);
Console.WriteLine($"Edited image url: {task.Output.Results?[0].Url}");
```
Available functions include `description_edit`, `description_edit_with_mask` (requires `MaskImageUrl`), `stylization_all`, `stylization_local`, `remove_watermark`, `expand`, `super_resolution`, `colorization`, `doodle` and `control_cartoon_feature`; function-specific parameters like `Strength`, `UpscaleFactor` and the four `*Scale` properties live on `Image2ImageSynthesisParameters`.

The `wanx-sketch-to-image-lite` model turns a doodle/sketch into a complete image via the same task API, taking the sketch url on `SketchImageUrl` (aspect ratio should match the output, white background recommended). `SketchWeight` (0-10, defaults to 10) controls how close the output stays to the sketch; `SketchExtraction = true` extracts sketch edges from an RGB input; `SketchColor` lists the RGB pen-stroke colors when the sketch lines are colored (ignored with `SketchExtraction`):
```csharp
var response = await client.CreateImage2ImageSynthesisTaskAsync(
    new ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>()
    {
        Model = "wanx-sketch-to-image-lite",
        Input = new Image2ImageSynthesisInput()
        {
            Prompt = "一棵参天大树",
            SketchImageUrl = "<your-sketch-url>"
        },
        Parameters = new Image2ImageSynthesisParameters()
        {
            Size = "768*768",
            N = 1,
            SketchWeight = 3,
            Style = "<watercolor>"
        }
    });
var task = await client.GetImage2ImageSynthesisTaskAsync(response.Output.TaskId);
Console.WriteLine($"Generated image url: {task.Output.Results?[0].Url}");
```

### Text-to-Image (Async Task)
Use `CreateImageSynthesisTaskAsync` to create a task, then poll `GetImageSynthesisTaskAsync` to check completion status.
```csharp
var response = await client.CreateImageSynthesisTaskAsync(
    new ModelRequest<ImageSynthesisInput, IImageSynthesisParameters>()
    {
        Model = "qwen-image-plus",
        Input = new ImageSynthesisInput() { Prompt = "A futuristic cityscape at sunset" },
        Parameters = new ImageSynthesisParameters()
        {
            Size = "1664*928",
            N = 1,
            PromptExtend = true,
            Watermark = false
        }
    });
var taskId = response.Output.TaskId;
Console.WriteLine($"Task Created: {taskId}");
var waited = 0;
var timeout = 300;
do
{
    var task = await client.GetImageSynthesisTaskAsync(taskId);
    if (task.Output.TaskStatus == DashScopeTaskStatus.Succeeded)
    {
        Console.WriteLine($"Image URL: {task.Output.Results?.FirstOrDefault()?.Url}");
        break;
    }
    if (task.Output.TaskStatus == DashScopeTaskStatus.Failed)
    {
        Console.WriteLine($"Task failed: {task.Output.Message}");
        break;
    }
    Console.WriteLine($"Task Status: {task.Output.TaskStatus}");
    await Task.Delay(TimeSpan.FromSeconds(10));
    waited += 10;
} while (waited < timeout);
```

`wan2.6-t2i` uses a new async protocol: create the task with `CreateImageGenerationTaskAsync`, filling the multimodal `Messages` on `ImageGenerationInput` (the legacy wanx style repaint models use `ImageUrl` + `StyleIndex` on the same type instead), then poll with `GetImageGenerationTaskAsync`:
```csharp
var response = await client.CreateImageGenerationTaskAsync(
    new ModelRequest<ImageGenerationInput, IMultimodalParameters>()
    {
        Model = "wan2.6-t2i",
        Input = new ImageGenerationInput()
        {
            Messages = new List<MultimodalMessage>()
            {
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>()
                    {
                        MultimodalMessageContent.TextContent("一间有着精致窗户的花店，漂亮的木质门，摆放着花朵")
                    })
            }
        },
        Parameters = new MultimodalParameters() { N = 1, Size = "1280*1280", Seed = 233 }
    });
var task = await client.GetImageGenerationTaskAsync(response.Output.TaskId);
var url = task.Output.Choices?[0].Message.Content[0].Image;
Console.WriteLine($"Generated image url: {url}");
```

Kling (`kling/kling-v3-image-generation`, `kling/kling-v3-omni-image-generation`) and Vidu (`vidu/vidu-image_reference2image`, `vidu/vidu-image-pro_reference2image`, `vidu/vidu-image-lite_reference2image`, `vidu/viduq3-fast_reference2image`, `vidu/viduq2-pro_reference2image`, `vidu/viduq2-fast_reference2image`) image models run on the same task API with optional reference images in the message content. Kling takes `AspectRatio` (16:9 / 9:16 / 1:1) with `Resolution` (1k / 2k, 4k on omni) instead of `Size`; `kling-v3-omni` additionally generates a storyboard series with `ResultType = "series"` + `SeriesAmount` (2-9) and can keep subjects from the Kling subject list via `ElementList` on the input. Vidu takes `Size` / `Seed` and always returns one image (`N = 1`).
```csharp
var response = await client.CreateImageGenerationTaskAsync(
    new ModelRequest<ImageGenerationInput, IMultimodalParameters>()
    {
        Model = "kling/kling-v3-omni-image-generation",
        Input = new ImageGenerationInput()
        {
            Messages = new List<MultimodalMessage>()
            {
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>()
                    {
                        MultimodalMessageContent.TextContent("参考图1的风格和图2的背景，生成番茄炒蛋"),
                        MultimodalMessageContent.ImageContent("<reference-image-1-url>"),
                        MultimodalMessageContent.ImageContent("<reference-image-2-url>")
                    })
            }
        },
        Parameters = new MultimodalParameters()
        {
            N = 4,
            ResultType = "series",
            SeriesAmount = 4,
            AspectRatio = "1:1",
            Resolution = "1k"
        }
    });
var task = await client.GetImageGenerationTaskAsync(response.Output.TaskId);
// series mode returns all images in the content of the single choice
var urls = task.Output.Choices?[0].Message.Content.Select(c => c.Image).ToList();
Console.WriteLine($"Usage: count({task.Usage?.ImageCount}) size({task.Usage?.Size}) tier({task.Usage?.SR})");
```

### Image Translation

Translate the text within an image while preserving the original layout. With `qwen-mt-image-2.0` you can call `GetImage2ImageSynthesisAsync` to translate synchronously - the call blocks until the translated image is ready. The 2.0 model has no language restrictions (any of the 55 supported languages works for both source and target).

```csharp
var response = await client.GetImage2ImageSynthesisAsync(
    new ModelRequest<Image2ImageSynthesisInput>()
    {
        Model = "qwen-mt-image-2.0",
        Input = new Image2ImageSynthesisInput()
        {
            SourceLang = "zh",
            TargetLang = "en",
            ImageUrl = "<your-image-url>"
        }
    });
Console.WriteLine($"Translated image url: {response.Output.ImageUrl}");
```

For `qwen-mt-image` (which requires the source or target language to be Chinese or English), use the async task flow: create the task with `CreateImage2ImageSynthesisTaskAsync` and an `Image2ImageSynthesisInput`, then poll `GetImage2ImageSynthesisTaskAsync` for the result.

```csharp
var response = await client.CreateImage2ImageSynthesisTaskAsync(
    new ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>()
    {
        Model = "qwen-mt-image",
        Input = new Image2ImageSynthesisInput()
        {
            SourceLang = "zh",
            TargetLang = "en",
            ImageUrl = "https://example.com/image-with-text.jpg",
            Ext = new Image2ImageSynthesisInputExt()
            {
                // Hint for the scene and translation style of the source text
                DomainHint = "These sentences are from seller-buyer conversations on a B2C ecommerce platform.",
                // Terms that should be filtered out before translation
                Sensitives = ["基础"],
                // Predefined translations for specific terms
                Terminologies = [new Image2ImageSynthesisInputExtTerm("一体化", "Unified")],
                Config = new Image2ImageSynthesisConfig()
                {
                    // Skip translation for the main subject in the image, like people/product/logo
                    ImageSegment = false
                }
            }
        }
    });
var taskId = response.Output.TaskId;
Console.WriteLine($"Task Created: {taskId}");
var waited = 0;
var timeout = 300;
do
{
    var task = await client.GetImage2ImageSynthesisTaskAsync(taskId);
    if (task.Output.TaskStatus == DashScopeTaskStatus.Succeeded)
    {
        Console.WriteLine($"Translated image URL: {task.Output.ImageUrl}");
        break;
    }
    if (task.Output.TaskStatus == DashScopeTaskStatus.Failed)
    {
        Console.WriteLine($"Task failed: {task.Output.Message}");
        break;
    }
    Console.WriteLine($"Task Status: {task.Output.TaskStatus}");
    await Task.Delay(TimeSpan.FromSeconds(10));
    waited += 10;
} while (waited < timeout);
```

#### Portrait Style Transfer
Use `CreateImageGenerationTaskAsync` and `GetImageGenerationTaskAsync`

#### Background Generation
Use `CreateBackgroundGenerationTaskAsync` and `GetBackgroundGenerationTaskAsync`

### Portrait (FaceChain)
FaceChain trains a dedicated portrait model from 1~10 face images of a person (model `facechain-finetune`, billed per training run), then uses the trained model to generate portrait photos in various styles. Validate the images first with `DetectFacesAsync` (model `facechain-facedetect`, synchronous, currently free):
```csharp
var response = await client.DetectFacesAsync(
    new ModelRequest<FaceDetectionInput>()
    {
        Model = "facechain-facedetect",
        Input = new FaceDetectionInput() { Images = ["<face-image-1-url>", "<face-image-2-url>"] }
    });
foreach (var (isFace, reason) in response.Output.IsFace!.Zip(response.Output.FailedReason!))
{
    Console.WriteLine($"{isFace}: {reason}");
}
```

Create the fine-tune job with the image urls (or file ids from `UploadFilesAsync`) and poll until it finishes; the trained model name is in `FinetunedOutput`:
```csharp
var job = await client.CreateFineTuneJobAsync(
    new DashScopeCreateFineTuneJobRequest()
    {
        Model = "facechain-finetune",
        TrainingFileIds = ["<face-image-1-url>", "<face-image-2-url>"]
    });
DashScopeFineTuneJob finished;
do
{
    await Task.Delay(TimeSpan.FromSeconds(30));
    finished = await client.GetFineTuneJobAsync(job.Output.JobId);
} while (finished.Output.Status is DashScopeTaskStatus.Pending or DashScopeTaskStatus.Running);
Console.WriteLine($"Trained model: {finished.Output.FinetunedOutput}");
```
The Bailian documentation does not cover a portrait generation API for the trained model yet, use `FinetunedOutput` with the generation API documented for your workspace once available.

## Video Generation
Video generation and editing are async tasks: create the task with `CreateVideoSynthesisTaskAsync` and poll `GetVideoSynthesisTaskAsync`. The endpoint is shared by the HappyHorse, wan2.7, wan3.0, legacy wan2.1-2.6, wanx2.1-vace, PixVerse, Kling, Vidu and MiniMax models — one input type covers them all, the supported members depend on the model. Text-to-video (HappyHorse / wan2.7 / wan3.0):
```csharp
var response = await client.CreateVideoSynthesisTaskAsync(
    new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
    {
        Model = "wan2.7-t2v",
        Input = new VideoSynthesisInput() { Prompt = "一只猫在草地上奔跑", NegativePrompt = "低分辨率" },
        Parameters = new VideoSynthesisParameters() { Resolution = "720P", Ratio = "16:9", Duration = 5, Seed = 42 }
    });
var task = await client.GetVideoSynthesisTaskAsync(response.Output.TaskId);
Console.WriteLine($"Video url: {task.Output.VideoUrl}, billed duration: {task.Usage?.Duration}");
```
Media-driven generation passes `Media` items: first frame or first and last frames (image-to-video), reference images and videos (reference-to-video, optionally with a per-item `reference_voice` audio), a driving audio or a first clip (wan2.7 image-to-video), and a source `video` plus reference images (video editing). wan3.0 additionally accepts reference audio, files and web links, and can disable the audio track with `Audio = false`:
```csharp
var response = await client.CreateVideoSynthesisTaskAsync(
    new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
    {
        Model = "wan2.7-i2v-2026-04-25",
        Input = new VideoSynthesisInput()
        {
            Prompt = "让画面动起来",
            Media =
            [
                new VideoSynthesisMedia("first_frame", "<first-frame-image-url>"),
                new VideoSynthesisMedia("last_frame", "<last-frame-image-url>")
            ]
        },
        Parameters = new VideoSynthesisParameters() { Resolution = "720P", Duration = 5 }
    });
```
Legacy wan2.1-2.6 models use the same input type, filling `ImgUrl` (first-frame image-to-video) or `ReferenceUrls` (wan2.6 reference-to-video) plus the `Size` ("1280*720") and `ShotType` parameters:
```csharp
var response = await client.CreateVideoSynthesisTaskAsync(
    new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
    {
        Model = "wan2.6-i2v-flash",
        Input = new VideoSynthesisInput() { Prompt = "让画面动起来", ImgUrl = "<first-frame-image-url>" },
        Parameters = new VideoSynthesisParameters() { Resolution = "720P", Duration = 5, Audio = false }
    });
```
wanx2.1-vace-plus also uses the same types, with a `Function` (image reference, video repainting, video editing, video extension and outpainting) selecting its input fields (reference images, video, frames/clips, masks) and parameters (control condition, strength, masks, expand and scale options).

The video-style-transform model (video style transform) fills only `VideoUrl`, its parameters carry `Style` (0 Japanese comic to 7 Chinese ink wash), `VideoFps` (15-25), `AnimateEmotion` (facial expression optimization), `MinLen` (720 or 540, affects billing) and `UseSR` (free super-resolution; with 540 it upscales to 1080P while billing at 540P), and the result video is in `Output.OutputVideoUrl` with the billed short edge in `Usage.SR`.

The PixVerse models (Beijing region only, activate the PixVerse service in the Bailian console first) cover text-to-video (`pixverse-*-t2v`), image-to-video by first frame (`pixverse-*-it2v`) or keyframes (`pixverse-*-kf2v`), reference-to-video (`pixverse-*-r2v` and `pixverse-v6-r2v-omni`), lipsync (`pixverse-lipsync`), motion control (`pixverse-motioncontrol`) and upscaling (`pixverse-upscale`). The t2v/r2v models (except omni) set `Size` + `Duration`, the others set `Resolution` (+ `AspectRatio` for omni) instead; `Audio` generates matching sound effects (billed, defaults to false), `ShotType` (multi-shot) is supported by the v6 t2v/it2v models, and omni requires `Duration = 0` when a reference video is provided (the output follows the longest input video). Reference media are `image_url` / `video_url` media items, optionally named with `RefName` and referenced in the prompt as `@ref_name` (the server rewrites the references to `@1`, `@2`, ... in `Output.OrigPrompt`). The lipsync model takes a `video_url` plus either an `audio_url` media item or TTS via `lip_sync_tts_speaker_id` (voice `Id`, `auto` or a voice id) and `lip_sync_tts_content` (text `Content`) media items, without a prompt; motioncontrol takes an `image_url` character and a `video_url` motion clip; upscale takes a single `video_url` and always outputs 4K. PixVerse models report `Usage.SR` as a string ("720"/"4K", empty when not applicable). Reference-to-video with images, video and `@ref_name` references:
```csharp
var response = await client.CreateVideoSynthesisTaskAsync(
    new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
    {
        Model = "pixverse/pixverse-v6-r2v-omni",
        Input = new VideoSynthesisInput()
        {
            Prompt = "@男人 在@咖啡厅 中弹吉他 参考@参考视频",
            Media =
            [
                new VideoSynthesisMedia("video_url", "https://example.com/motion.mp4", RefName: "参考视频"),
                new VideoSynthesisMedia("image_url", "https://example.com/man.jpg", RefName: "男人"),
                new VideoSynthesisMedia("image_url", "https://example.com/cafe.png", RefName: "咖啡厅")
            ]
        },
        Parameters = new VideoSynthesisParameters() { Resolution = "720P", AspectRatio = "16:9", Duration = 0 }
    });
```

The Kling models (`kling/kling-v3-turbo`, `kling/kling-v3` and `kling/kling-v3-omni`-video-generation) cover text-to-video, image-to-video (first frame or first+last frames), reference-to-video and video editing (omni only). Kling uses `AspectRatio` (16:9 / 9:16 / 1:1, required for text-to-video) plus a `Mode` parameter (std 720P, pro 1080P or 4k) instead of resolution tiers; media items use the types first_frame / last_frame / refer (reference image) / feature (reference video) / base (video to edit, the output keeps its duration) with `KeepOriginalSound` ("yes"/"no") on video items, and the omni prompt references media items in array order as `&lt;&lt;&lt;image_1&gt;&gt;&gt;` / `&lt;&lt;&lt;video_1&gt;&gt;&gt;` / `&lt;&lt;&lt;element_1&gt;&gt;&gt;` (subjects come from the Kling subject list and are passed via `ElementList`). Multi-shot is enabled with `MultiShot` plus an input-level `ShotType` (intelligence for AI storyboarding, or customize with per-shot `MultiPrompt` segments); watermark=true additionally returns `Output.WatermarkVideoUrl`. Reference-to-video with a feature video and a refer image:
```csharp
var response = await client.CreateVideoSynthesisTaskAsync(
    new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
    {
        Model = "kling/kling-v3-omni-video-generation",
        Input = new VideoSynthesisInput()
        {
            Prompt = "参考<<<video_1>>>中人物的动作，让<<<image_1>>>中的角色在街头随音乐起舞",
            Media =
            [
                new VideoSynthesisMedia("feature", "https://example.com/motion.mp4", KeepOriginalSound: "no"),
                new VideoSynthesisMedia("refer", "https://example.com/man.jpg")
            ]
        },
        Parameters = new VideoSynthesisParameters() { Mode = "std", AspectRatio = "16:9", Duration = 5 }
    });
```

The Vidu models (`vidu/viduq2*` / `viduq3*` with the `_text2video` / `_img2video` / `_start-end2video` / `_reference2video` suffix selecting the calling mode) use plain `image` / `video` media items (ordered first/last frame for keyframe-to-video) and the existing resolution / duration / audio (q3 models) / watermark / seed parameters; `viduq2-pro_reference2video` also accepts reference videos and `Duration = 0` (auto-planned duration, up to 10 seconds).

The MiniMax model (`MiniMax/MiniMax-H3`) supports text-to-video, image-to-video (first / last / both frames) and multimodal reference-to-video (`image_url` / `feature` video / `driving_audio` media items, not combinable with frame images). It uses `Resolution` (768P or 2K), `Ratio` (adaptive by default, text-to-video requires a concrete ratio) and reports the extra usage fields `ImageCount`, `InputSeconds`, `OutputSeconds` and `TotalSeconds`.

Wan2.2 keyframe image-to-video (first and last frames) and the animate models (wan2.2-animate-move image-to-motion, wan2.2-animate-mix subject replacement) live on a separate endpoint and share `Image2VideoSynthesisInput` / `IImage2VideoSynthesisParameters`, call them with `CreateImage2VideoSynthesisTaskAsync`:
```csharp
var response = await client.CreateImage2VideoSynthesisTaskAsync(
    new ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters>()
    {
        Model = "wan2.2-kf2v-flash",
        Input = new Image2VideoSynthesisInput()
        {
            Prompt = "镜头从平视逐渐上升",
            FirstFrameUrl = "<first-frame-image-url>",
            LastFrameUrl = "<last-frame-image-url>"
        },
        Parameters = new Image2VideoSynthesisParameters() { Resolution = "480P" }
    });
```
The animate models fill `ImageUrl`/`VideoUrl` and a required `Mode` ("wan-std"/"wan-pro") instead, and take the watermark on the input rather than the parameters. The wan2.2-s2v digital human model fills `ImageUrl` plus an `AudioUrl` and generates a talking/performing video. The videoretalk lip sync model fills `VideoUrl` plus an `AudioUrl`, optionally `RefImageUrl` to pick the face when the video contains multiple faces, and takes `VideoExtension` (whether to extend the video to the audio length with a reverse-forward replay) and `QueryFaceThreshold` (face matching strictness, 120-200, requires `RefImageUrl`) on the parameters. Poll the task with `GetImage2VideoSynthesisTaskAsync`; kf2v and videoretalk tasks expose the video in `Output.VideoUrl`, animate and s2v tasks in `Output.Results.VideoUrl`.

Check whether an image is suitable for digital human generation with the synchronous `GetFaceDetectAsync` (model wan2.2-s2v-detect):
```csharp
var response = await client.GetFaceDetectAsync(
    new ModelRequest<FaceDetectInput>() { Model = "wan2.2-s2v-detect", Input = new FaceDetectInput() { ImageUrl = "<person-image-url>" } });
Console.WriteLine($"Check passed: {response.Output.CheckPass}, humanoid detected: {response.Output.Humanoid}");
```

## 3D Generation (Tripo)
The Tripo models are async tasks on the 3d-generation endpoint, create the task with `CreateTripo3DTaskAsync` and poll `GetTripo3DTaskAsync`. They are Beijing region only — activate the Tripo service in the Bailian console first. One input type covers the whole family, the supported members depend on the model:

- Generation: `Tripo/Tripo-H3.1` (high precision, up to 2M faces) and `Tripo/Tripo-P1.0` (fast, clean topology) support text-to-3d (`Prompt`), image-to-3d (`Image`) and multi-image-to-3d (`Images`, exactly four entries ordered front / left / back / right, an entry with null members marks an empty view).
- `Tripo/Tripo-ImageToSplat` generates a Gaussian Splat file (.splat) from a single image.
- Post-processing: `Tripo/Tripo-Texture` (texturing, guided by `TexturePrompt`), `Tripo/Tripo-Segmentation` (mesh segmentation), `Tripo/Tripo-SmartSegmentation` (end-to-end segmentation from an image or model), `Tripo/Tripo-MeshCompletion` (complete segmented parts), `Tripo/Tripo-Retopology` (decimation / low-poly retopology) and `Tripo/Tripo-ConvertFormat` (export to GLTF / USDZ / FBX / OBJ / STL / 3MF via `Format`). Post-processing models take the source model via `OriginalModelTaskId` (the task id of a preceding Tripo task from the same account) or `ModelUrl`.
- Animation: `Tripo/Tripo-RigCheck` (free check whether a model is riggable, returns the recommended `RigType`), `Tripo/Tripo-AutoRig` (skeleton binding) and `Tripo/Tripo-AnimationRetarget` (retarget preset animations onto a rigged model, up to 5 per task).

Text-to-3d:
```csharp
var response = await client.CreateTripo3DTaskAsync(
    new ModelRequest<Tripo3DInput, ITripo3DParameters>()
    {
        Model = "Tripo/Tripo-P1.0",
        Input = new Tripo3DInput { Prompt = "一只可爱的猫" },
        Parameters = new Tripo3DParameters { TextureQuality = "standard" }
    });
var task = await client.GetTripo3DTaskAsync(response.Output.TaskId);
// when task.Output.TaskStatus is Succeeded:
// model url:    task.Output.Results[0].ModelUrl (or PbrModelUrl / BaseModelUrl, valid for 2 hours)
// preview:      task.Output.Results[0].RenderedImageUrl
// credits used: task.Usage.CreditsConsumed
```
Multi-image-to-3d (front and back views):
```csharp
var response = await client.CreateTripo3DTaskAsync(
    new ModelRequest<Tripo3DInput, ITripo3DParameters>()
    {
        Model = "Tripo/Tripo-P1.0",
        Input = new Tripo3DInput()
        {
            Images =
            [
                new Tripo3DImage("png", "<front-image-url>"),
                new Tripo3DImage(), // empty view, serialized as {}
                new Tripo3DImage("png", "<back-image-url>"),
                new Tripo3DImage()
            ]
        },
        Parameters = new Tripo3DParameters { TextureQuality = "standard" }
    });
```
Chain tasks by passing the preceding task id, e.g. texture the generated model (or any public model url):
```csharp
var response = await client.CreateTripo3DTaskAsync(
    new ModelRequest<Tripo3DInput, ITripo3DParameters>()
    {
        Model = "Tripo/Tripo-Texture",
        Input = new Tripo3DInput { OriginalModelTaskId = generationTaskId },
        Parameters = new Tripo3DParameters { TextureQuality = "detailed" }
    });
```
A typical animation pipeline: `Tripo-RigCheck` (free, returns `Riggable` and the recommended `RigType`) → `Tripo-AutoRig` (`RigType`, non-biped animals need `ModelVersion = "v2.5-20260210"`) → `Tripo-AnimationRetarget` (`Animations`, e.g. `["preset:walk"]`; non-biped rigs use the `preset:<rigType>:<name>` form). The result urls are signed temporary links valid for 2 hours. See the `Tripo3D` group in Cnblogs.DashScope.Sample for a runnable pipeline.

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

## Batch

Use `OpenAiCompatibleUploadFileAsync`, `OpenAiCompatibleCreateBatchAsync`, `OpenAiCompatibleGetBatchAsync`, `OpenAiCompatibleListBatchesAsync`, and `OpenAiCompatibleCancelBatchAsync` to manage batch jobs.

Batch API allows you to submit multiple requests in a single JSONL file for asynchronous processing, which is useful for large-scale tasks.

### Upload File and Create Batch Job

```csharp
var client = new DashScopeClient("your-api-key", region: DashScopeRegion.Singapore);

// Prepare JSONL content
var jsonl =
    """
    {"custom_id":"1","method":"POST","url":"/v1/chat/ds-test","body":{"model":"batch-test-model","messages":[{"role":"system","content":"You are a helpful assistant."},{"role":"user","content":"你好！有什么可以帮助你的吗？"}]}}
    {"custom_id":"2","method":"POST","url":"/v1/chat/ds-test","body":{"model":"batch-test-model","messages":[{"role":"system","content":"You are a helpful assistant."},{"role":"user","content":"What is 2+2?"}]}}
    """;
using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonl));

// Upload file with purpose "batch"
var inputFile = await client.OpenAiCompatibleUploadFileAsync(stream, "batch_input.jsonl", "batch");

// Create batch job
var batch = await client.OpenAiCompatibleCreateBatchAsync(new DashScopeCreateBatchRequest()
{
    InputFileId = inputFile.Id.Value,
    CompletionWindow = "24h", // Supports "h" (hours) and "d" (days), range: 24h to 336h
    Endpoint = "/v1/chat/ds-test",
    Metadata = new DashScopeBatchMetadata()
    {
        DsName = "My Batch Job",           // Optional, max 100 characters
        DsDescription = "Description",      // Optional, max 200 characters
        DsBatchFinishCallback = "https://example.com/callback" // Optional callback URL
    }
});
```

### Poll for Completion and Get Results

```csharp
// Poll batch status
batch = await client.OpenAiCompatibleGetBatchAsync(batch.Id);

// When completed, download the output file
if (batch.Status == "completed" && batch.OutputFileId != null)
{
    await using var result = await client.OpenAiCompatibleGetFileContentAsync(batch.OutputFileId);
    using var streamReader = new StreamReader(result);
    var content = await streamReader.ReadToEndAsync();
    // or save to a jsonl file
    // using var fileStream = new FileStream($"{batch.OutputFileId}.jsonl", FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
    // await result.CopyToAsync(fileStream);
}
```

### List and Cancel Batches

```csharp
// List batches with optional filters
var batches = await client.OpenAiCompatibleListBatchesAsync(
    limit: 20,
    dsName: "keyword"); // Search by name

// Cancel a batch
var cancelled = await client.OpenAiCompatibleCancelBatchAsync(batchId);
```

### Cleanup

```csharp
// Delete uploaded files when no longer needed
var deletion = await client.OpenAiCompatibleDeleteFileAsync(fileId);
Console.WriteLine(deletion.Deleted ? $"Deleted: {deletion.Id}" : $"Deletion failed: {deletion.Id}");
```

## Text Embeddings

Call the text embedding API via `GetTextEmbeddingsAsync`. Supported models are `text-embedding-v1` ~ `v4` plus the qwen3.7 series:

- `qwen3.7-text-embedding`: 2560/2048/1536/1024(default)/768/512/256 dimensions, up to 128,000 tokens per text and 20 texts per request.
- `qwen3.7-text-embedding-flash`: 1024(default)/768/512/256 dimensions.

The qwen3.7 series also returns an extra `prompt_tokens` or `input_tokens` in `usage` (same value as `total_tokens`), and flash additionally returns a `prompt_tokens_details` breakdown.

```csharp
var text = "Sample text for embedding";
var response = await dashScopeClient.GetTextEmbeddingsAsync(
    "text-embedding-v4",
    [text],
    new TextEmbeddingParameters { Dimension = 512 });
var embedding = response.Output.Embeddings.First().Embedding;
Console.WriteLine($"Embedding vector length: {embedding.Length}");
```

## Multimodal Embedding

Compute the embeddings of texts, images and videos in one vector space via `GetMultimodalEmbeddingsAsync`, e.g. to search images or videos by a text, or the other way round. Supported models (the `Dimension` values they accept, the first one is the default):

- `tongyi-embedding-vision-plus`: 1152/1024/512/256/128/64 dimensions, also available as the `-2026-03-06` snapshot.
- `tongyi-embedding-vision-flash`: 768/512/256/128/64 dimensions, also available as the `-2026-03-06` snapshot.
- `multimodal-embedding-v1`: 1024 dimensions only.
- `qwen3-vl-embedding`: 2560/2048/1536/1024/768/512/256 dimensions.
- `qwen2.5-vl-embedding`: 1024/2048/768/512 dimensions.

Every element of `Contents` is a `MultimodalEmbeddingContent`: a `Text` (a `string` converts to it implicitly), an `Image` (a public url or a base64 data uri) or a `Video` (a public url). One embedding comes out of each, at its `Index` and with the `Type` of its modality:

```csharp
List<MultimodalEmbeddingContent> candidates =
[
    new(Image: imageUrl1),
    new(Image: imageUrl2),
    new(Video: videoUrl)
];
var response = await dashScopeClient.GetMultimodalEmbeddingsAsync(
    new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
    {
        Model = "tongyi-embedding-vision-plus",
        // index the candidates once, then match the queries against them by the cosine similarity
        Input = new MultimodalEmbeddingInput { Contents = candidates }
    });
foreach (var embedding in response.Output.Embeddings)
{
    Console.WriteLine($"{embedding.Index}: {embedding.Type}, {embedding.Embedding.Length} dimensions");
}
```

The models differ in how they fuse several contents into one embedding:

- `tongyi-embedding-vision-*-2026-03-06` fuse the members of one content into one embedding of the type `fused`, e.g. `new MultimodalEmbeddingContent(Text: "...", Image: imageUrl)`.
- `qwen3-vl-embedding` fuses all the contents into one embedding of the type `fusion` with `EnableFusion = true`.
- `qwen2.5-vl-embedding` always fuses all the contents into one embedding.
- The other models expect one member per content.

```csharp
var response = await dashScopeClient.GetMultimodalEmbeddingsAsync(
    new ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>
    {
        Model = "qwen3-vl-embedding",
        Input = new MultimodalEmbeddingInput
        {
            Contents = ["白色运动鞋，轻量透气，适合跑步和日常穿着", new MultimodalEmbeddingContent(Image: imageUrl)]
        },
        Parameters = new MultimodalEmbeddingParameters { EnableFusion = true } // one fused embedding out of all the contents
    });
Console.WriteLine(response.Output.Embeddings.Single().Type); // fusion
```

`MultiImages` (the images of a sequence, taken as one content) is accepted by the tongyi-embedding-vision models only. `Instruct` takes effect on `qwen3-vl-embedding` and `qwen2.5-vl-embedding` only, `ResLevel` and `MaxVideoFrames` on the 2026-03-06 snapshots only. `Fps` is documented, but it changed neither the vectors nor the usage of any model when tested. The `Usage` differs by model too. The tongyi-embedding-vision models count everything in `InputTokens` (with an `InputTokensDetails` breakdown) and return `OutputTokens`; the other models count the texts only in `InputTokens` and the images and videos in `ImageTokens`, `multimodal-embedding-v1` also returns `ImageCount` and `Duration`, and neither it nor `qwen2.5-vl-embedding` returns `TotalTokens`. The API is served on the shared domain too, no `WorkspaceId` is needed although the official docs show the workspace domain url.

## Text Rerank

Rank the documents recalled by a vector search (or any other candidates) by their relevance to the query via `RerankDocumentsAsync`, e.g. to pick the passages worth passing to the model. Supported models: `qwen3.7-text-rerank`, `qwen3-rerank` and `gte-rerank-v2` for texts, and `qwen3-vl-rerank` which also ranks images and videos. The results are sorted by the score descending, each one carries the `Index` of its document in the input list and a `RelevanceScore` in [0, 1] (relative to the documents of the same request, do not compare it across requests):

```csharp
var response = await dashScopeClient.RerankDocumentsAsync(
    new ModelRequest<TextRerankInput, TextRerankParameters>
    {
        Model = "qwen3-rerank",
        Input = new TextRerankInput
        {
            Query = "What is a text rerank model?",
            Documents = ["passage a", "passage b", "passage c"]
        },
        Parameters = new TextRerankParameters
        {
            TopN = 2, // the top ranked documents to return, all when unset
            ReturnDocuments = true, // echo each document in its result, false by default
            Instruct = "Given a web search query, retrieve relevant passages that answer the query."
        }
    });
foreach (var result in response.Output.Results)
{
    Console.WriteLine($"{result.Index}: {result.RelevanceScore} {result.Document?.Text}");
}
```

`Query` and every element of `Documents` are a `TextRerankContent`, a `string` converts to it implicitly (convert an existing chunk list element by element, e.g. `chunks.Select(c => (TextRerankContent)c)`). `qwen3-vl-rerank` also accepts images (a public url or a base64 data uri) and videos (a public url), as the query or as documents; the text documents are then sent as `{"text": ...}` objects along with them:

```csharp
var response = await dashScopeClient.RerankDocumentsAsync(
    new ModelRequest<TextRerankInput, TextRerankParameters>
    {
        Model = "qwen3-vl-rerank",
        Input = new TextRerankInput
        {
            Query = new TextRerankContent(Image: imageUrl), // or a text query
            Documents =
            [
                "passage",
                new TextRerankContent(Image: imageUrl),
                new TextRerankContent(Video: videoUrl)
            ]
        },
        Parameters = new TextRerankParameters { ReturnDocuments = true, Fps = 0.5f } // Fps: the ratio of the video frames to read
    });
foreach (var result in response.Output.Results)
{
    Console.WriteLine($"{result.RelevanceScore}: {result.Document?.Text ?? result.Document?.Image ?? result.Document?.Video}");
}
```

`gte-rerank-v2` accepts texts only and ignores `Instruct`. `qwen3.7-text-rerank` also returns `PromptTokens` in `Usage` and `qwen3-vl-rerank` returns `ImageTokens` and `InputTokens`. The API is served on the shared domain too, no `WorkspaceId` is needed although the official docs show the workspace domain url.

## Omni-Modal Translation (Qwen-MT-Uni)

Translate texts, documents (pdf, docx, pptx, xlsx, txt, html, md), images (png, jpg) or audio (mp3, wav) with the omni-modal translation model `qwen-mt-uni` via `GetUniTranslationAsync`, the call blocks until the result is ready. Pass the texts in `SourceTexts` or a publicly accessible file url in `FileUrl`, exactly one of them. A texts input comes back in `TranslatedTexts`, in the order of the source texts:

```csharp
var response = await dashScopeClient.GetUniTranslationAsync(
    new ModelRequest<UniTranslationInput>
    {
        Model = "qwen-mt-uni",
        Input = new UniTranslationInput
        {
            SourceTexts = ["机器学习正在改变世界。", "全场9折", "这款商品支持七天无理由退换。"],
            SourceLang = "zh", // unset to detect the source language
            TargetLang = "en",
            Ext = new UniTranslationInputExt
            {
                // Hint for the scene and translation style
                DomainHint = "These sentences are from seller-buyer conversations on a B2C ecommerce platform.",
                // A source text equal to one of them is kept as it is
                Sensitives = ["全场9折"],
                // Terms the model is guided to translate as given
                Glossary = [new UniTranslationGlossaryTerm("机器学习", "ML")]
            }
        }
    });
foreach (var text in response.Output.Data.TranslatedTexts)
{
    Console.WriteLine(text);
}
```

A file input comes back as a translated file of the same format (images as jpg, audio as mp3) in `TranslatedFileUrl`, a signed url valid for 24 hours:

```csharp
var response = await dashScopeClient.GetUniTranslationAsync(
    new ModelRequest<UniTranslationInput>
    {
        Model = "qwen-mt-uni",
        Input = new UniTranslationInput
        {
            FileUrl = "https://example.com/image-with-text.jpg",
            TargetLang = "zh",
            Ext = new UniTranslationInputExt
            {
                // The format when the url has no recognizable extension: image, pdf, docx, audio, ...
                FormatHint = "image",
                Config = new UniTranslationConfig
                {
                    // Skip translation for the main subject in the image, like people/product/logo
                    ImageSegment = true
                }
            }
        }
    });
Console.WriteLine(response.Output.Data.FileType); // strings, document, image or audio
Console.WriteLine($"Translated file url: {response.Output.Data.TranslatedFileUrl}");
```

Large documents and long audio take minutes, longer than the default `HttpClient` timeout: submit them as an asynchronous task with `CreateUniTranslationTaskAsync` (the same request) and poll `GetUniTranslationTaskAsync`:

```csharp
var task = await dashScopeClient.CreateUniTranslationTaskAsync(
    new ModelRequest<UniTranslationInput>
    {
        Model = "qwen-mt-uni",
        Input = new UniTranslationInput
        {
            FileUrl = "https://example.com/document.pdf",
            TargetLang = "en"
        }
    });

DashScopeTask<UniTranslationOutput, UniTranslationUsage>? result;
do
{
    await Task.Delay(TimeSpan.FromSeconds(10));
    result = await dashScopeClient.GetUniTranslationTaskAsync(task.Output.TaskId!);
}
while (result.Output.TaskStatus is DashScopeTaskStatus.Pending or DashScopeTaskStatus.Running);

// a translation that fails (e.g. the file cannot be downloaded) still ends with the status Succeeded
if (result.Output.Success == false)
{
    Console.WriteLine($"Translation failed: {result.Output.Message}");
}
else
{
    Console.WriteLine($"Translated file url: {result.Output.Data?.TranslatedFileUrl}");
}
```

`SourceTexts` takes at most 50 non-empty strings of at most 10,000 characters each and is always sent as an array, so `TranslatedTexts` is an array too. `TargetLang` takes a language code (e.g. `en`, `zh-tw`) or its English name (e.g. `English`), case insensitive; `Sensitives` takes at most 50 entries and `Glossary` at most 100 terms. A file is at most 100 MB, a document at most 200 pages and audio between 3 seconds and 60 minutes. The server also accepts the undocumented extensions `.xls`, `.webp`, `.bmp`, `.m4a`, `.aac`, `.flac`, `.ogg`, `.opus` and `.mp4`. The usage breaks the tokens down by content type in `InputTokensDetails` / `OutputTokensDetails` (`CharacterTokens`, `ImageTokens`, `DocumentTokens`, `AudioTokens`), it is empty when a task fails. Audio input could not be verified when tested: the server answered `InternalError` (`ASR failed after 5 retries`) for mp3 and wav inputs. The API is served on the shared domain too, no `WorkspaceId` is needed although the official docs show the workspace domain url.

## Decision Model (System One)

Get a structured decision from the decision model (`decision-model`, the preview `decision-model-preview` is accepted too) via `OpenAiCompatibleGetSystemOneDecisionAsync`: a single forward pass answers every question of the request with a choice, a yes / no probability or a score, together with the probability distribution and, for a choice / score answer, the confidence. No text is generated, so the latency and the cost do not depend on the output length. It fits the high frequency decisions like ticket triage, content moderation, agent routing or result verification.

The API is only served on the workspace domain, the client has to be created with a `WorkspaceId` (see [Workspace and Region](#workspace-and-region)), otherwise the server answers 404.

```csharp
// the decision model is only served on the workspace domain
var workspaceClient = new DashScopeClient("your-api-key", workspaceId: "your-workspace-id");
var decision = await workspaceClient.OpenAiCompatibleGetSystemOneDecisionAsync(
    new SystemOneDecisionInput
    {
        Model = "decision-model",
        State = new { ticket_id = "T-1001", content = "订单支付后超过 24 小时仍未到账，用户无法继续使用核心服务，要求立即处理。" }, // a text, a conversation (an array) or a structured object
        Questions = new Dictionary<string, SystemOneQuestion>
        {
            // pick one of the options
            ["department"] = SystemOneQuestion.Choice(
                "应该由哪个团队处理？",
                new Dictionary<string, string>
                {
                    ["billing"] = "支付、退款和账单问题",
                    ["technical"] = "产品故障和集成问题"
                }),
            // yes / no, the answer is the probability of yes
            ["escalate"] = SystemOneQuestion.Noul("是否需要立即通知值班人员？"),
            // an ordered scale, from low to high
            ["severity"] = SystemOneQuestion.Score(
                "这个问题有多严重？",
                ["轻微问题，不影响功能", "部分功能受影响，但存在替代方案", "核心功能不可用，没有替代方案", "造成严重业务或安全影响"])
        }
    });

var answers = decision.Answers; // keyed by the question id of the request
var department = answers["department"];
Console.WriteLine($"{department.Choice} {department.Confidence:F2}"); // e.g. billing 0.9956
Console.WriteLine(department.Probabilities!["technical"]);            // e.g. 0.0022
Console.WriteLine(answers["escalate"].Noul);                          // P(yes), e.g. 0.9971
Console.WriteLine($"{answers["severity"].Score:F2}");                 // the probability weighted level index, e.g. 2.02
```

The fields of an answer depend on the type of the question: a `choice` answer carries `Choice`, `Confidence` and `Probabilities`, a `noul` answer carries `Noul` only, a `score` answer carries `Score`, `Confidence`, `Legend` (the level index to level description map) and `Probabilities`. A request takes at most 16 questions, a `choice` question at most 255 options and a `score` question 2-255 levels. Although the endpoint lives under `/compatible-mode`, a failed request throws a `DashScopeException` with the usual DashScope error detail.

## Model List

List the models of Bailian with `ListModelsAsync` (`GET /api/v1/models`). Every model carries its capabilities, the input / output modalities, the features, the prices and the token limits. One page is returned per call:

```csharp
var page = await client.ListModelsAsync(new ModelListQuery
{
    Capabilities = ["TG", "VU"], // text generation or visual understanding
    PageSize = 100
});
foreach (var modelInfo in page.Output.Models)
{
    Console.WriteLine($"{modelInfo.Model}: {string.Join(", ", modelInfo.Capabilities)}");
}

Console.WriteLine($"{page.Output.Total} models in total"); // increase PageNo to read the next pages

// look one model up
var qwen = (await client.ListModelsAsync(new ModelListQuery { Model = "qwen3.7-max" })).Output.Models[0];
Console.WriteLine($"{qwen.Name} takes up to {qwen.ModelInfo.MaxInputTokens} input tokens, published at {qwen.PublishedTime}");
```

- Every filter of `ModelListQuery` is optional and is not sent when it is left unset: `Name` (fuzzy), `Model` (exact), `Providers`, `InferenceProviders`, `Capabilities`, `Features`, `ContextWindow`, `ServiceSite`, `Supports`, `DeploymentMethods`, `DeploymentPtuServiceTiers`, `Language` (`zh-CN` or `en-US`) and the paging. A model matches a list filter when it carries any of its values.
- `PageSize` is at most 200 and defaults to 20. A page beyond the last one is an empty page, not an error.
- `Model` matches the id exactly and the letter case matters (`Qwen3.7-Max` lists nothing). The service ignores an empty value and answers the whole list, leave it unset instead.
- A model supplied directly by a third party carries the vendor in its id (`ZHIPU/GLM-5.3`, `xiaomi/mimo-v2.6-pro-ultraspeed`) and in `InferenceProvider`, the models hosted by Bailian have `aliyun-bailian` there.
- A rejected parameter (e.g. `PageNo = 0`) throws a `DashScopeException` although the service answers with status 200, the detail is in `DashScopeException.Error`.

See [Snapshot Files](./test/Cnblogs.DashScope.Tests.Shared/Utils/Snapshots.cs) for API parameter examples.

Review [Tests](./test) for comprehensive usage examples.
