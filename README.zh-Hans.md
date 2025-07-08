[English](https://github.com/cnblogs/dashscope-sdk/blob/main/README.md) | 简体中文

# Cnblogs.DashScopeSDK

[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AI?style=flat&logo=nuget&label=Cnblogs.DashScope.AI)](https://www.nuget.org/packages/Cnblogs.DashScope.AI)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.Sdk?style=flat&logo=nuget&label=Cnblogs.DashScope.Sdk&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.Sdk)](https://www.nuget.org/packages/Cnblogs.DashScope.Sdk)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AspNetCore?style=flat&logo=nuget&label=Cnblogs.DashScope.AspNetCore&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.AspNetCore)](https://www.nuget.org/packages/Cnblogs.DashScope.AspNetCore)

由博客园维护并使用的非官方灵积（百炼）服务 SDK。

使用前注意：当前项目正在积极开发中，小版本也可能包含破坏性更改，升级前请查看对应版本 Release Note 进行迁移。

## 快速开始

### 使用 `Microsoft.Extensions.AI` 接口

安装 NuGet 包 `Cnblogs.DashScope.AI`

```csharp
var client = new DashScopeClient("your-api-key").AsChatClient("qwen-max");
var completion = await client.CompleteAsync("hello");
Console.WriteLine(completion)
```

### 控制台应用

安装 NuGet 包 `Cnblogs.DashScope.Sdk`。

```csharp
var client = new DashScopeClient("your-api-key");
var completion = await client.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
// 也可以直接输入模型名称进行调用
// var completion = await client.GetQWenCompletionAsync("qwen-max", prompt);
Console.WriteLine(completion.Output.Text);
```

### ASP.NET Core 应用

安装 NuGet 包 `Cnblogs.DashScope.AspNetCore`。

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

应用类中
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

## 支持的 API

- [对话](#对话) - QWen3, DeepSeek 等，支持推理/工具调用/网络搜索/翻译等场景
- [多模态](#多模态) - QWen-VL，QVQ 等，支持推理/视觉理解/OCR/音频理解等场景
- [语音合成](#语音合成) - CosyVoice，Sambert 等，支持 TTS 等应用场景
- [图像生成](#图像生成) - wanx2.1 等，支持文生图，人像风格重绘等应用场景
- [应用调用](#应用调用)
- [文本向量](#文本向量)

### 对话

使用 `dashScopeClient.GetTextCompletionAsync` 和 `dashScopeClient.GetTextCompletionStreamAsync` 来直接访问文本生成接口。

针对通义千问和 DeekSeek，我们提供了快捷方法进行调用： `GetQWenChatCompletionAsync` /`GetDeepSeekChatCompletionAsync`

相关文档：https://help.aliyun.com/zh/model-studio/user-guide/text-generation/

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

#### 推理

使用推理模型时，模型的思考过程可以通过 `ReasoningContent` 属性获取。

```csharp
var history = new List<TextChatMessage>
{
    TextChatMessage.User("Calculate 1+1")
};
var completion = await client.GetDeepSeekChatCompletionAsync(DeepSeekLlm.DeepSeekR1, history);
Console.WriteLine(completion.Output.Choices[0]!.Message.ReasoningContent);
```

对于支持的模型（例如 qwen3），可以使用 `TextGenerationParameters.EnableThinking` 决定是否使用模型的推理能力。

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

#### 工具调用

创建一个可供模型使用的方法。

```csharp
string GetCurrentWeather(GetCurrentWeatherParameters parameters)
{
    // implementation is irrenlvent
    return "Sunny"
}

public record GetCurrentWeatherParameters(
    [property: Required]
    [property: Description("The city and state, e.g. San Francisco, CA")]
    string Location,
    [property: JsonConverter(typeof(EnumStringConverter<TemperatureUnit>))]
    TemperatureUnit Unit = TemperatureUnit.Celsius);

public enum TemperatureUnit
{
    Celsius,
    Fahrenheit
}
```

对话时带上方法的名称、描述和参数列表，参数列表以 JSON Schema 的形式提供（这里使用 `JsonSchema.Net` 库，您也可以使用其它具有类似功能的库）。

```csharp
var tools = new List<ToolDefinition>()
{
    new(
        ToolTypes.Function,
        new FunctionDefinition(
            nameof(GetCurrentWeather),
            "获取当前天气",
            new JsonSchemaBuilder().FromType<GetCurrentWeatherParameters>().Build()))
};

var history = new List<ChatMessage>
{
    ChatMessage.User("What is the weather today in C.A?")
};

var parameters = new TextGenerationParamters()
{
    ResultFormat = ResultFormats.Message,
    Tools = tools
};

// 向模型提问并提供可用的方法
var completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenMax, history, parameters);

// 模型试图调用方法
Console.WriteLine(completion.Output.Choice[0].Message.ToolCalls[0].Function.Name); // GetCurrentWeather
history.Add(completion.Output.Choice[0].Message);

// 调用方法并将结果保存到聊天记录中
var result = GetCurrentWeather(JsonSerializer.Deserialize<GetCurrentWeatherParameters>(completion.Output.Choice[0].Message.ToolCalls[0].Function.Arguments));
history.Add(new("tool", result, nameof(GetCurrentWeather)));

// 模型根据调用结果返回答案
completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenMax, history, parameters);
Console.WriteLine(completion.Output.Choice[0].Message.Content) // 现在浙江省杭州市的天气是大部多云，气温为 18 摄氏度。
```

当模型认为应当调用工具时，返回消息中 `ToolCalls` 会提供调用的详情，本地在调用完成后可以把结果以 `tool` 角色返回。

#### 上传文件（qwen-long）

使用长上下文模型时，需要先提前将文件上传到 DashScope 来获得 Id。

```csharp
var file = new FileInfo("test.txt");
var uploadedFile = await dashScopeClient.UploadFileAsync(file.OpenRead(), file.Name);
```

使用文件 Id 初始化一个消息，内部会转换成 system 角色的一个文件引用。

```csharp
var history = new List<ChatMessage>
{
    ChatMessage.File(uploadedFile.Id),   // 多文件情况下可以直接传入文件 Id 数组, 例如：[file1.Id, file2.Id]
    ChatMessage.User("总结一下文件的内容。")
}
var parameters = new TextGenerationParameters()
{
    ResultFormat = ResultFormats.Message
};
var completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenLong, history, parameters);
Console.WriteLine(completion.Output.Choices[0].Message.Content);
```

如果需要，完成对话后可以使用 API 删除之前上传的文件。

```csharp
var deletionResult = await dashScopeClient.DeleteFileAsync(uploadedFile.Id);
```

### 多模态

使用 `dashScopeClient.GetMultimodalGenerationAsync` 和 `dashScopeClient.GetMultimodalGenerationStreamAsync` 来访问多模态文本生成接口。

相关文档：[多模态_大模型服务平台百炼(Model Studio)-阿里云帮助中心](https://help.aliyun.com/zh/model-studio/multimodal)

#### 视觉理解/推理

使用 `MultimodalMessage.User()` 可以快速创建对应角色的消息。

媒体内容可以通过公网 URL 或者 `byte[]` 传入。

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

### 语音合成

通过 `dashScopeClient.CreateSpeechSynthesizerSocketSessionAsync()` 来创建一个语音合成会话。

**注意：使用 using 语句来自动释放会话，或者手动 Dispose 会话，尽量不要重用会话。**

相关文档：[语音合成-CosyVoice_大模型服务平台百炼(Model Studio)-阿里云帮助中心](https://help.aliyun.com/zh/model-studio/cosyvoice-large-model-for-speech-synthesis)

```csharp
using var tts = await dashScopeClient.CreateSpeechSynthesizerSocketSessionAsync("cosyvoice-v2");
var taskId = await tts.RunTaskAsync(
    new SpeechSynthesizerParameters { Voice = "longxiaochun_v2", Format = "mp3" });
await tts.ContinueTaskAsync(taskId, "博客园");
await tts.ContinueTaskAsync(taskId, "代码改变世界");
await tts.FinishTaskAsync(taskId);
var file = new FileInfo("tts.mp3");
using var stream = file.OpenWrite();
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
```

### 图像生成

#### 文生图

我们针对通义万相提供了快捷 API `dashScopeClient.CreateWanxImageSynthesisTaskAsync()` 和 `GetWanxImageSynthesisTaskAsync()`。

图片生成需要数秒到数十秒不等，对于 HTTP 请求来说太长，需要通过任务方式生成。

先使用 `CreateWanxImageSynthesisTaskAsync()` 创建任务，再轮询 `GetWanxImageSynthesisTaskAsync()` 检查任务完成状态。

相关文档：[通义万相2.1文生图V2版API参考_大模型服务平台百炼(Model Studio)-阿里云帮助中心](https://help.aliyun.com/zh/model-studio/text-to-image-v2-api-reference)

```csharp
var prompt = Console.ReadLine();
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
```

#### 人像风格重绘和图像背景生成

与文生图类似，先创建任务，再轮询状态。

人像风格重绘 - `CreateWanxImageGenerationTaskAsync` 和 `GetWanxImageGenerationTaskAsync`

图像背景生成 - `CreateWanxBackgroundGenerationTaskAsync` 和 `GetWanxBackgroundGenerationTaskAsync`

### 应用调用

`GetApplicationResponseAsync` 用于进行应用调用。

`GetApplicationResponseStreamAsync` 用于流式调用。

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

`ApplicationRequest` 默认使用 `Dictionary<string, object?>` 作为 `BizParams` 的类型。

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

如需强类型支持，可以使用泛型类 `ApplicationRequest<TBizParams>`。
注意 SDK 在 JSON 序列化时使用 `snake_case`。如果你的应用采用其他的命名规则，请使用 `[JsonPropertyName("camelCase")]` 来手动指定序列化时的属性名称。

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

### 文本向量

使用 `GetTextEmbeddingsAsync` 来调用文本向量接口。

相关文档：[通用文本向量同步接口API详情_大模型服务平台百炼(Model Studio)-阿里云帮助中心](https://help.aliyun.com/zh/model-studio/text-embedding-synchronous-api)

```csharp
var text = Console.ReadLine();
var response = await dashScopeClient.GetTextEmbeddingsAsync(
    TextEmbeddingModel.TextEmbeddingV4,
    [text],
    new TextEmbeddingParameters() { Dimension = 512, });
var array = response.Output.Embeddings.First().Embedding;
Console.WriteLine("Embedding");
Console.WriteLine(string.Join('\n', array));
Console.WriteLine($"Token usage: {response.Usage?.TotalTokens}");
```

查看 [快照文件](./test/Cnblogs.DashScope.Tests.Shared/Utils/Snapshots.cs) 获得 API 调用参数示例.

查看 [测试](./test) 获得更多 API 使用示例。
