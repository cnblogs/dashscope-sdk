English | [简体中文](https://github.com/cnblogs/dashscope-sdk/blob/main/README.zh-Hans.md)

[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.Sdk?style=flat&logo=nuget&label=Cnblogs.DashScope.Sdk&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.Sdk)](https://www.nuget.org/packages/Cnblogs.DashScope.Sdk)
[![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AspNetCore?style=flat&logo=nuget&label=Cnblogs.DashScope.AspNetCore&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.AspNetCore)](https://www.nuget.org/packages/Cnblogs.DashScope.AspNetCore)

# DashScope SDK for .NET

An unofficial DashScope SDK maintained by Cnblogs.

**Warning**: this project is under active development, **Breaking Changes** may introduced without notice or major version change. Make sure you read the Release Notes before upgrading.

# Quick Start

## Using `Microsoft.Extensions.AI`

Install `Cnblogs.Extensions.AI.DashScope` Package

```csharp
var client = new DashScopeClient("your-api-key").AsChatClient("qwen-max");
var completion = await client.CompleteAsync("hello");
Console.WriteLine(completion)
```

## Console App

Install `Cnblogs.DashScope.Sdk` package.

```csharp
var client = new DashScopeClient("your-api-key");
var completion = await client.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
Console.WriteLine(completion.Output.Text);
```

## ASP.NET Core

Install the Cnblogs.DashScope.AspNetCore package.

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

`Usage`
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

# Supported APIs

- Text Embedding API - `dashScopeClient.GetTextEmbeddingsAsync()`
- Text Generation API(qwen-turbo, qwen-max, etc.) - `dashScopeClient.GetQwenCompletionAsync()` and `dashScopeClient.GetQWenCompletionStreamAsync()`
- BaiChuan Models - Use `dashScopeClient.GetBaiChuanTextCompletionAsync()`
- LLaMa2 Models - `dashScopeClient.GetLlama2TextCompletionAsync()`
- Multimodal Generation API(qwen-vl-max, etc.) - `dashScopeClient.GetQWenMultimodalCompletionAsync()` and `dashScopeClient.GetQWenMultimodalCompletionStreamAsync()`
- Wanx Models(Image generation, background generation, etc)
  - Image Synthesis - `CreateWanxImageSynthesisTaskAsync()` and `GetWanxImageSynthesisTaskAsync()`
  - Image Generation - `CreateWanxImageGenerationTaskAsync()` and `GetWanxImageGenerationTaskAsync()`
  - Background Image Generation - `CreateWanxBackgroundGenerationTaskAsync()` and `GetWanxBackgroundGenerationTaskAsync()`
- File API that used by Qwen-Long - `dashScopeClient.UploadFileAsync()` and `dashScopeClient.DeleteFileAsync`

# Examples

Visit [tests](./test) for more usage of each api.

## Single Text Completion

```csharp
var prompt = "hello"
var completion = await client.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
Console.WriteLine(completion.Output.Text);
```

## Multi-round chat

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

## Function Call

Creates a function with parameters

```csharp
string GetCurrentWeather(GetCurrentWeatherParameters parameters)
{
    // actual implementation should be different.
    return "Sunny, 14" + parameters.Unit switch
    {
        TemperatureUnit.Celsius => "℃",
        TemperatureUnit.Fahrenheit => "℉"
    };
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

Append tool information to chat messages.

```csharp
var tools = new List<ToolDefinition>()
{
    new(
        ToolTypes.Function,
        new FunctionDefinition(
            nameof(GetCurrentWeather),
            "Get the weather abount given location",
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

// send question with available tools.
var completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenMax, history, parameters);
history.Add(completion.Output.Choice[0].Message);

// model responding with tool calls.
Console.WriteLine(completion.Output.Choice[0].Message.ToolCalls[0].Function.Name); // GetCurrentWeather

// calling tool that model requests and append result into history.
var result = GetCurrentWeather(JsonSerializer.Deserialize<GetCurrentWeatherParameters>(completion.Output.Choice[0].Message.ToolCalls[0].Function.Arguments));
history.Add(ChatMessage.Tool(result, nameof(GetCurrentWeather)));

// get back answers.
completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenMax, history, parameters);
Console.WriteLine(completion.Output.Choice[0].Message.Content);
```

Append the tool calling result with `tool` role, then model will generate answers based on tool calling result.


## QWen-Long with files

Upload file first.

```csharp
var file = new FileInfo("test.txt");
var uploadedFile = await dashScopeClient.UploadFileAsync(file.OpenRead(), file.Name);
```

Using uploaded file id in messages.

```csharp
var history = new List<ChatMessage>
{
    ChatMessage.File(uploadedFile.Id),   // use array for multiple files, e.g. [file1.Id, file2.Id]
    ChatMessage.User("Summarize the content of file.")
}
var parameters = new TextGenerationParameters()
{
    ResultFormat = ResultFormats.Message
};
var completion = await client.GetQWenChatCompletionAsync(QWenLlm.QWenLong, history, parameters);
Console.WriteLine(completion.Output.Choices[0].Message.Content);
```

Delete file if needed

```csharp
var deletionResult = await dashScopeClient.DeleteFileAsync(uploadedFile.Id);
```
