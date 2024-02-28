# Cnblogs.DashScopeSDK

An unofficial DashScope SDK maintained by Cnblogs.

# Usage

## Console App

Install Cnblogs.DashScope.Sdk package.

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

# Supported API

- Text Embedding API
- Text Generation API(qwen-turbo, qwen-max, etc.)
- Multimodal Generation API(qwen-vl-max, etc.)
