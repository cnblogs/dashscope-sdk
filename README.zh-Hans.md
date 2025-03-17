[English](https://github.com/cnblogs/dashscope-sdk/blob/main/README.md) | 简体中文

[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AI?style=flat&logo=nuget&label=Cnblogs.DashScope.AI)](https://www.nuget.org/packages/Cnblogs.DashScope.AI)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.Sdk?style=flat&logo=nuget&label=Cnblogs.DashScope.Sdk&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.Sdk)](https://www.nuget.org/packages/Cnblogs.DashScope.Sdk)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AspNetCore?style=flat&logo=nuget&label=Cnblogs.DashScope.AspNetCore&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.AspNetCore)](https://www.nuget.org/packages/Cnblogs.DashScope.AspNetCore)

# Cnblogs.DashScopeSDK

由博客园维护并使用的非官方灵积（百炼）服务 SDK。

使用前注意：当前项目正在积极开发中，小版本也可能包含破坏性更改，升级前请查看对应版本 Release Note 进行迁移。

# 快速开始

## 使用 `Microsoft.Extensions.AI` 接口

安装 NuGet 包 `Cnblogs.DashScope.AI`

```csharp
var client = new DashScopeClient("your-api-key").AsChatClient("qwen-max");
var completion = await client.CompleteAsync("hello");
Console.WriteLine(completion)
```

## 控制台应用

安装 NuGet 包 `Cnblogs.DashScope.Sdk`。

```csharp
var client = new DashScopeClient("your-api-key");
var completion = await client.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
// 也可以直接输入模型名称进行调用
// var completion = await client.GetQWenCompletionAsync("qwen-max", prompt);
Console.WriteLine(completion.Output.Text);
```

## ASP.NET Core 应用

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

# 支持的 API

- 通用文本向量 - `dashScopeClient.GetTextEmbeddingsAsync()`
- 通义千问（`qwen-turbo`， `qwen-max` 等） - `dashScopeClient.GetQwenCompletionAsync()` and `dashScopeClient.GetQWenCompletionStreamAsync()`
- 百川开源大模型 - Use `dashScopeClient.GetBaiChuanTextCompletionAsync()`
- LLaMa2 大语言模型 - `dashScopeClient.GetLlama2TextCompletionAsync()`
- 通义千问 VL 和通义千问 Audio(`qwen-vl-max`， `qwen-audio`) - `dashScopeClient.GetQWenMultimodalCompletionAsync()` and `dashScopeClient.GetQWenMultimodalCompletionStreamAsync()`
- 通义万相系列
    - 文生图 - `CreateWanxImageSynthesisTaskAsync()` and `GetWanxImageSynthesisTaskAsync()`
    - 人像风格重绘 - `CreateWanxImageGenerationTaskAsync()` and `GetWanxImageGenerationTaskAsync()`
    - 图像背景生成 - `CreateWanxBackgroundGenerationTaskAsync()` and `GetWanxBackgroundGenerationTaskAsync()`
- 适用于 QWen-Long 的文件 API `dashScopeClient.UploadFileAsync()` and `dashScopeClient.DeleteFileAsync`
- 应用调用 `dashScopeClient.GetApplicationResponseAsync` 和 `dashScopeClient.GetApplicationResponseStreamAsync()`
- 其他使用相同 Endpoint 的模型

# 示例

查看 [测试](./test) 获得更多 API 使用示例。

## 单轮对话

```csharp
var prompt = "你好"
var completion = await client.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
Console.WriteLine(completion.Output.Text);
```

## 多轮对话

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

## 工具调用

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

对话时带上方法的名称、描述和参数列表，参数列表以 JSON Schema 的形式提供。

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

## 上传文件（QWen-Long）

需要先提前将文件上传到 DashScope 来获得 Id。

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

## 应用调用

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
