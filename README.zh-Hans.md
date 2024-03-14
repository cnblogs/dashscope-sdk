[English](https://github.com/cnblogs/dashscope-sdk/blob/main/README.md) | 简体中文

[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.Sdk?style=flat&logo=nuget&label=Cnblogs.DashScope.Sdk&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.Sdk)](https://www.nuget.org/packages/Cnblogs.DashScope.Sdk)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AspNetCore?style=flat&logo=nuget&label=Cnblogs.DashScope.AspNetCore&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.AspNetCore)](https://www.nuget.org/packages/Cnblogs.DashScope.AspNetCore)

# Cnblogs.DashScopeSDK

由博客园维护并使用的非官方灵积服务 SDK。

使用前注意：当前项目正在积极开发中，小版本也可能包含破坏性更改，升级前请查看对应版本 Release Note 进行迁移。

# 快速开始

## 控制台应用

安装 NuGet 包 `Cnblogs.DashScope.Sdk`。

```csharp
var client = new DashScopeClient("your-api-key");
var completion = await client.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
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
    new("user", "Please remember this number, 42"),
    new("assistant", "I have remembered this number."),
    new("user", "What was the number I metioned before?")
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
    new("user", "杭州现在天气如何？")
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
