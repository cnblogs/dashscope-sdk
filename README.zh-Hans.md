[English](https://github.com/cnblogs/dashscope-sdk/blob/main/README.md) | 简体中文

![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.Sdk?style=flat&logo=nuget&label=Cnblogs.DashScope.Sdk&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.Sdk)
![NuGet Version](https://img.shields.io/nuget/v/Cnblogs.DashScope.AspNetCore?style=flat&logo=nuget&label=Cnblogs.DashScope.AspNetCore&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FCnblogs.DashScope.AspNetCore)

# Cnblogs.DashScopeSDK

由博客园维护并使用的非官方灵积服务 SDK

# 使用方法

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
- 通义千问 VL 和通义千问 Audio(`qwen-vl-max`， `qwen-audio`) - `dashScopeClient.GetQWenMultimodalCompletionAsync` and `dashScopeClient.GetQWenMultimodalCompletionStreamAsync`
- 通义万相系列
    - 文生图 - `CreateWanxImageSynthesisTaskAsync()` and `GetWanxImageSynthesisTaskAsync()`
    - 人像风格重绘 - `CreateWanxImageGenerationTaskAsync` and `GetWanxImageGenerationTaskAsync()`
    - 图像背景生成 - `CreateWanxBackgroundGenerationTaskAsync` and `GetWanxBackgroundGenerationTaskAsync`
