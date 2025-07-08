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
var completion = await client.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
// Or use model name string
// var completion = await client.GetQWenCompletionAsync("qwen-max", prompt);
Console.WriteLine(completion.Output.Text);
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
        var completion = await client.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
        return completion.Output.Text;
    }
}
```
## Supported APIs
- [Chat](#Chat) - QWen3, DeepSeek, etc. Supports reasoning, tool calling, web search, translation
- [Multimodal](#multimodal) - QWen-VL, QVQ, etc. Supports reasoning, visual understanding, OCR, audio understanding
- [Text-to-Speech (TTS)](#Text-to-Speech) - CosyVoice, Sambert
- [Image Generation](#image-generation) - Wanx2.1 (text-to-image, portrait style transfer)
- [Application Call](#application-call)
- [Text Vectorization](#text-vectorization)


### Chat

Use `GetTextCompletionAsync`/`GetTextCompletionStreamAsync` for direct text generation.
For QWen and DeepSeek, use shortcuts: `GetQWenChatCompletionAsync`/`GetDeepSeekChatCompletionAsync`

[Official Documentation](https://help.aliyun.com/zh/model-studio/user-guide/text-generation/)

```csharp
var history = new List<ChatMessage>
{
    ChatMessage.User("Please remember this number, 42"),
    ChatMessage.Assistant("I have remembered this number."),
    ChatMessage.User("What was the number I metioned before?")
}
var parameters = new TextGenerationParameters()
{
    ResultFormat = ResultFormats.Message
};
var completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenMax, history, parameters);
Console.WriteLine(completion.Output.Choices[0].Message.Content); // The number is 42
```

#### Reasoning

Access model thoughts via `ReasoningContent` property
```csharp
var history = new List<TextChatMessage>
{
    TextChatMessage.User("Calculate 1+1")
};
var completion = await client.GetDeepSeekChatCompletionAsync(DeepSeekLlm.DeepSeekR1, history);
Console.WriteLine(completion.Output.Choices[0]!.Message.ReasoningContent);
```
For QWen3 models, enable reasoning with `TextGenerationParameters.EnableThinking`
```csharp
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
```

#### Tool Calling
Define a function for model to use:
```csharp
string GetCurrentWeather(GetCurrentWeatherParameters parameters)
{
    return "Sunny";
}
public record GetCurrentWeatherParameters(
    [property: Required]
    [property: Description("City and state, e.g. San Francisco, CA")]
    string Location,
    [property: JsonConverter(typeof(EnumStringConverter<TemperatureUnit>))]
    TemperatureUnit Unit = TemperatureUnit.Celsius);
public enum TemperatureUnit { Celsius, Fahrenheit }
```
Invoke with tool definitions. We using  `JsonSchema.Net`  for example, you could use any other library to generate JSON schema)
```csharp
var tools = new List<ToolDefinition>
{
    new(
        ToolTypes.Function,
        new FunctionDefinition(
            nameof(GetCurrentWeather),
            "Get current weather",
            new JsonSchemaBuilder().FromType<GetCurrentWeatherParameters>().Build()))
};
var history = new List<ChatMessage> { ChatMessage.User("What's the weather in CA?") };
var parameters = new TextGenerationParameters { ResultFormat = ResultFormats.Message, Tools = tools };

// request model
var completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenMax, history, parameters);
Console.WriteLine(completion.Output.Choice[0].Message.ToolCalls[0].Function.Name); // GetCurrentWeather
history.Add(completion.Output.Choice[0].Message);

// calls tool
var result = GetCurrentWeather(new() { Location = "CA" });
history.Add(new("tool", result, nameof(GetCurrentWeather)));

// Get final answer
completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenMax, history, parameters);
Console.WriteLine(completion.Output.Choices[0].Message.Content); // "Current weather in California: Sunny"
```
#### File Upload (Long Context Models)
For Qwen-Long models:
```csharp
var file = new FileInfo("test.txt");
var uploadedFile = await dashScopeClient.UploadFileAsync(file.OpenRead(), file.Name);
var history = new List<ChatMessage> { ChatMessage.File(uploadedFile.Id) };
var completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenLong, history);
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
