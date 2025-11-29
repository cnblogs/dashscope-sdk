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

## 支持的 API

- [文本生成](#文本生成) - QWen3, DeepSeek 等，支持推理/工具调用/网络搜索/翻译等场景
    - [多轮对话](#多轮对话)
    - [深度思考](#深度思考)
    - [联网搜索](#联网搜索)
    - [工具调用](#工具调用)
    - [前缀续写](#前缀续写)
    - [长上下文（Qwen-Long）](#长上下文（Qwen-Long）)

- [多模态](#多模态) - QWen-VL，QVQ 等，支持推理/视觉理解/OCR/音频理解等场景
    - [视觉理解/推理](#视觉理解/推理) - 图像/视频输入与理解，支持推理模式
    - [文字提取](#文字提取) - OCR 任务，读取表格/文档/公式等

- [语音合成](#语音合成) - CosyVoice，Sambert 等，支持 TTS 等应用场景
- [图像生成](#图像生成) - wanx2.1 等，支持文生图，人像风格重绘等应用场景
- [应用调用](#应用调用)
- [文本向量](#文本向量)

## 文本生成

使用 `dashScopeClient.GetTextCompletionAsync` 和 `dashScopeClient.GetTextCompletionStreamAsync()` 来访问文本生成 API。

常用模型：`qwen-max` `qwen-plus` `qwen-flush` 等

基础示例：

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

### 多轮对话

#### 快速开始

核心是维护一个 `TextChatMessage` 数组作为对话历史。

```csharp
var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
while (true)
{
    Console.Write("User > ");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("使用默认输入：你是谁？");
        input = "你是谁？";
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

/*
 * User > 你好，你今天过的怎么样？
 * Assistant > 你好！谢谢你关心。虽然我是一个AI助手，没有真实的情感和体验，但我非常高兴能和你交流。今天过得挺好的，因为我可以和很多像你一样的朋友聊天，帮助大家解决问题，分享知识。你今天过得怎么样呢？有什么我可以帮你的吗？
 * Usage: in(29)/out(59)/total(88)
 */
```

#### 思考模型

模型的思考过程会存放在独立的属性 `ReasoningContent` 中，存放到对话历史时要注意忽略它，仅保留模型的回复 `Content`。

有一些模型接受 `EnableThinking` 来设置是否开启深度思考，可以在 `Parameters` 里设置。

思考模型的更多设置请见下文的 [深度思考](#深度思考)

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

/*
User > 你好，今天感觉怎么样？
Reasoning > 好的，用户问“你好，今天感觉怎么样？”，我需要先理解他的意图。他可能是在关心我的状态，或者想开始一段对话。作为AI助手，我没有真实的情感，但应该以友好和积极的方式回应。

首先，我应该感谢他的问候，然后说明自己没有真实的情感，但愿意帮助他。接下来，可以询问他的情况，表现出关心，这样能促进进一步的交流。同时，保持语气自然，避免过于机械。

要注意用户可能的深层需求，比如他可能想寻求帮助，或者只是闲聊。所以回应要开放，让他知道我随时准备协助。另外，使用表情符号可以增加亲切感，但不要过多。

最后，确保回答简洁，不过于冗长，同时保持友好和专业。这样用户会觉得被重视，并且更愿意继续对话。
Assistant > 你好呀！虽然我没有真实的情感体验，但很高兴能和你聊天！今天过得怎么样呢？有什么我可以帮你的吗？
Usage: in(24)/out(203)/reasoning(169)/total(227)
 */
```

#### 流式输出

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

可以通过 `client.GetTextCompletionStreamAsync` 来启用流式输出，同时建议开启 `Parameters` 里的 `IncrementOutput` 来启用增量输出。

增量输出：

> 示例：["我爱","吃","苹果"]

非增量输出：

> 示例：["我爱","我爱吃","我爱吃苹果"]

流式输出会返回一个 `IAsyncEnumerable`，推荐使用 `await foreach` 对它进行遍历，然后将增量输出的内容记录下来，最后再保存到对话历史中去。

```cs
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

/*
User > 你好
Reasoning > 好的，用户发来“你好”，我需要友好回应。首先，应该用中文回复，保持自然。可以问好并询问有什么可以帮助的，这样既礼貌又开放。注意不要用太正式的语言，让对话轻松一些。同时，要确保回复简洁，避免冗长。检查有没有需要特别注意的地方，比如用户可能的需求或之前的对话历史，但这里看起来是第一次交流。所以，确定回复内容应该是：“你好！有什么我可以帮你的吗？” 这样既友好 又明确，鼓励用户进一步说明需求。
Assistant > 你好！有什么我可以帮你的吗？
Usage: in(19)/out(125)/reasoning(112)/total(144)
 */
```

### 深度思考

通过 `EnableThinking` 来控制模型是否开启深度思考。

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

#### 限制思考长度

通过 `Parameters` 里的 `ThinkingBudget` 来限制模型的思考长度。

示例：

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

/*
Set thinking budget to 10 tokens
User > 你是谁？
Reasoning > 好的，用户问我“你是谁？”，我
Assistant > 我是通义千问，是阿里巴巴集团研发的超大规模语言模型，可以回答问题、创作文字、编程、逻辑推理等多种任务。我旨在为用户提供帮助和便利。有什么我可以帮您的吗？
Usage: in(21)/out(59)/reasoning(10)/total(80)
 */
```

### 联网搜索

主要通过 `Parameters` 里的 `EnableSearch` 和 `SearchOptions` 来控制。

示例请求：

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
            SearchStrategy = "max", // max/turbo 主要控制搜索条目的多少
            EnableCitation = true,  // 模型回复中添加来源引用
            CitationFormat = "[ref_<number>]", // 模型回复的来源引用格式
            EnableSource = true, // 是否返回搜索来源列表，在 SearchInfo 中提供
            ForcedSearch = true，// 是否强制模型进行搜索
            EnableSearchExtension = true, // 开启垂直领域搜索，在 SearchInfo.Extra 里提供结果
            PrependSearchResult = true // 第一个返回包将只包含搜索结果，模型回复在随后的包内提供，不能和 EnableSearchExtension 同时开启
        }
    }
};
```

通过返回结果里的 `response.Output.SearchInfo` 来获取搜索结果，这个值会在第一个包随模型回复完整返回。因此，开启增量流式输出时，不需要通过 `StringBuilder` 等方式来缓存 `SearchInfo`。

联网搜索的调用次数可以通过最后一个包的 `response.Usage.Plugins.Search.Count` 获取。

```csharp
var messages = new List<TextChatMessage>();
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
            Model = "qwen-plus",
            Input = new TextGenerationInput() { Messages = messages },
            Parameters = new TextGenerationParameters()
            {
                ResultFormat = "message",
                EnableThinking = true,
                EnableSearch = true,
                SearchOptions = new TextGenerationSearchOptions()
                {
                    SearchStrategy = "max",
                    EnableCitation = true,
                    CitationFormat = "[ref_<number>]",
                    EnableSource = true,
                    EnableSearchExtension = true,
                    ForcedSearch = true
                },
                IncrementalOutput = true
            }
        });
    var reply = new StringBuilder();
    var searching = false;
    var reasoning = false;
    TextGenerationTokenUsage? usage = null;
    await foreach (var chunk in completion)
    {
        var choice = chunk.Output.Choices![0];
        var search = chunk.Output.SearchInfo;
        if (search != null)
        {
            if (!searching)
            {
                searching = true;
                Console.WriteLine();
                Console.WriteLine("Search >");
                foreach (var re in search.SearchResults)
                {
                    Console.WriteLine($"[{re.Index}].{re.Title} - {re.SiteName}, {re.Url}");
                }

                if (search.ExtraToolInfo != null)
                {
                    foreach (var extra in search.ExtraToolInfo)
                    {
                        Console.WriteLine($"[{extra.Tool}]: {extra.Result}");
                    }
                }
            }
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
            $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/plugins({usage.Plugins?.Search?.Count})/total({usage.TotalTokens})");
    }
}

/*
User > 阿里股价

Search >
[1].截至目前为止,外资机构对 - 无, https://xueqiu.com/9216592857/355356488
[2].阿里巴巴 - QQ, https://gu.qq.com/usBABA.N
[3].$阿里巴巴(BABA)$2025年10月 - 新浪网, https://guba.sina.com.cn/?s=thread&tid=74408&bid=13015
[4].阿里巴巴投資者關係-阿里巴巴集團 - 阿里巴巴集团, https://www.alibabagroup.com/zh-HK/investor-relations
[5].阿里巴巴(BABA)_美股行情_今日股价与走势图_新浪财经 - 新浪网, https://gu.sina.cn/us/hq/quotes.php?code=BABA&from=pc
[6].阿里巴巴－WR (89988.HK) 過往股價及數據 - , https://hk.finance.yahoo.com/quote/89988.HK/history/
[7].阿里巴巴10月17日成交额为29.43亿美元 成交额较上个交易日增加59.59%。 - 同花顺财经网, https://stock.10jqka.com.cn/usstock/20251018/c671832899.shtml
[8].阿里巴巴(BABA)股票历史数据 - , https://cn.investing.com/equities/alibaba-historical-data
[9].阿里巴巴－W (9988.HK) 股價、新聞、報價和記錄 - , https://hk.finance.yahoo.com/quote/9988.HK/
[stock]: 阿里巴巴美股：
实时价格167.05USD
上个交易日收盘价165.09USD
日环比%1.19%
月环比%-6.53
日同比%66.33
月同比%74.05
历史价格列表[{"date":"2025-10-17","endPri":"167.050"},{"date":"2025-10-16","endPri":"165.090"},{"date":"2025-10-15","endPri":"165.910"},{"date":"2025-10-14","endPri":"162.860"},{"date":"2025-10-13","endPri":"166.810"},{"date":"2025-10-10","endPri":"159.010"},{"date":"2025-10-09","endPri":"173.680"},{"date":"2025-10-08","endPri":"181.120"},{"date":"2025-10-07","endPri":"181.330"},{"date":"2025-10-06","endPri":"187.220"},{"date":"2025-10-03","endPri":"188.030"},{"date":"2025-10-02","endPri":"189.340"},{"date":"2025-10-01","endPri":"182.780"},{"date":"2025-09-30","endPri":"178.730"},{"date":"2025-09-29","endPri":"179.900"},{"date":"2025-09-26","endPri":"171.910"},{"date":"2025-09-25","endPri":"175.470"},{"date":"2025-09-24","endPri":"176.440"},{"date":"2025-09-23","endPri":"163.080"},{"date":"2025-09-22","endPri":"164.250"},{"date":"2025-09-19","endPri":"162.810"},{"date":"2025-09-18","endPri":"162.480"},{"date":"2025-09-17","endPri":"166.170"},{"date":"2025-09-16","endPri":"162.210"},{"date":"2025-09-15","endPri":"158.040"},{"date":"2025-09-12","endPri":"155.060"},{"date":"2025-09-11","endPri":"155.440"},{"date":"2025-09-10","endPri":"143.930"},{"date":"2025-09-09","endPri":"147.100"},{"date":"2025-09-08","endPri":"141.200"}]



Reasoning > 用户想了解阿里巴巴的股价信息。我需要从知识库中整理有关阿里巴巴股价的最新信息。

首先，让我查看知识库中有关阿里巴巴股价的最新数据：

1. 从ref_7中可以看到：2025年10月17日，阿里巴巴(BABA)涨1.19%，报167.05美元，该日成交额为29.43亿美元，成交量为1776.57万。

2. 从ref_4中可以看到：2025年10月16日，阿里巴巴股价为$165.090，下跌了-0.820(-0.494%)

3. 从ref_8中可以看到历史数据：
   - 2025年10月15日: 165.91, 168.07
   - 2025年10月14日: 162.86, 160.05
   - 2025年10月13日: 166.81, 167.78
   - 2025年10月10日: 159.01, 170.03

4. 从ref_2中可以看到：2025年10月8日，阿里巴巴港股报价为181.12，下跌-0.12%

5. 从ref_6中可以看到：2025年10月17日，阿里巴巴-WR (89988.HK)收市价为141.700，下跌-6.000 (-4.06%)

6. 从ref_7中还提到：阿里巴巴(BABA)过去5个交易日涨5.06%，整个10月跌6.53%，年初至今涨97.02%，过去52周涨66.93%

7. 从ref_5中可以看到一些股价指标：今开168.070，最高168.100，昨收162.860，最低164.600等

8. 从ref_1中可以看到：摩根大通在2025年10月2日发布的报告中，将阿里巴巴美股(BABA.N)的目标价大幅上调至245美元

9. 从ref_3中可以看到：2025年10月13日，高盛发布报告，上调阿里巴巴美股和港股目标价

10. 从ref_9中可以看到阿里巴巴-W (9988.HK)的一些价值评估数据，如市值3.43T，企业价值3.23T等

11. 从额外的"其他互联网信息"中提到：2025年10月19日，阿里巴巴集团的实际股票价格为155.30港币（但这条信息可能不准确，因为其他来源显示10月17日价格更高）

12. 最后，系统提供的知识库中有一段最新的股市信息：
"股市信息:阿里巴巴美股：
实时价格167.05USD
上个交易日收盘价165.09USD
日环比%1.19%
月环比%-6.53
日同比%66.33
月同比%74.05"

根据系统提供的知识库中的最新股市信息和ref_7的信息，可以确认截至2025年10月17日（星期五），阿里巴巴美股收盘价为167.05美元，较前一交易日上涨1.19%。

另外，从ref_4中可以看到10月16日的收盘价为165.09美元。

根据ref_7，10月17日的成交额为29.43亿美元，较上个交易日增加59.59%，成交量为1776.57万。

此外，还有一些分析师的目标价信息：
- 摩根大通在10月2日将目标价上调至245美元
- 高盛在10月13日上调了目标价（具体数值未提供）

综合以上信息，我可以提供阿里巴巴股价的最新情况，包括近期走势、分析师预测等。
Assistant > # 阿里巴巴股价最新情况

## 美股(BABA)最新价格
- 截至2025年10月17日，阿里巴巴美股收盘价为**167.05美元**，较前一交易日上涨1.19%[ref_7]
- 上一交易日(10月16日)收盘价为165.09美元，下跌0.494%[ref_4]
- 10月17日成交额达29.43亿美元，较上个交易日增加59.59%，当日成交量为1776.57万[ref_7]

## 近期股价走势
- 过去5个交易日累计上涨5.06%[ref_7]
- 整个10月下跌6.53%[ref_7]
- 年初至今上涨97.02%[ref_7]
- 过去52周上涨66.93%[ref_7]

## 近期历史价格
- 2025年10月15日: 165.91美元[ref_8]
- 2025年10月14日: 162.86美元[ref_8]
- 2025年10月13日: 166.81美元[ref_8]
- 2025年10月10日: 159.01美元[ref_8]

## 港股情况
- 阿里巴巴-WR (89988.HK)在2025年10月17日收市价为141.700港元，下跌6.000港元(-4.06%)[ref_6]
- 2025年10月8日，阿里巴巴港股报价为181.12港元，下跌0.12%[ref_2]

## 分析师目标价
- 摩根大通在2025年10月2日发布的报告中，将阿里巴巴美股目标价大幅上调至**245美元**[ref_1]
- 高盛于2025年10月13日发布报告，上调了阿里巴巴未来三年资本开支预测至4600亿元人民币，并上调其美股和港股目标价[ref_3]

## 其他财务指标
- 市盈率(TTM): 18.75[ref_5]
- 阿里巴巴-W (9988.HK)市值达3.43万亿港元[ref_9]
- 企业价值: 3.23万亿[ref_9]
Usage: in(2178)/out(1571)/reasoning(952)/plugins:(1)/total(3749)
 */
```

### 工具调用

通过 `Parameter` 里的 `Tools` 来向模型提供可用的工具列表，模型会返回带有 `ToolCall` 属性的消息来调用工具。

接收到消息后，服务端需要调用对应工具并将结果作为 `Tool` 角色的消息插入到对话记录中再发起请求，模型会根据工具调用的结果总结答案。

默认情况下，模型每次只会调用一次工具。如果输入的问题需要多次调用同一工具，或需要同时调用多个工具，可以在 `Parameter` 里启用 `ParallelToolCalls` 来允许模型同时发起多次工具调用请求。

这个示例中，我们先定义一个获取天气的 C# 方法：

```csharp
public record WeatherReportParameters(
    [property: Required]
    [property: Description("要获取天气的省市名称，例如浙江省杭州市")]
    string Location,
    [property: JsonConverter(typeof(EnumStringConverter<TemperatureUnit>))]
    [property: Description("温度单位")]
    TemperatureUnit Unit = TemperatureUnit.Celsius);

public enum TemperatureUnit
{
    Celsius,
    Fahrenheit
}

private string GetWeather(WeatherReportParameters payload)
    => "大部多云，气温 "
       + payload.Unit switch
       {
           TemperatureUnit.Celsius => "18 摄氏度",
           TemperatureUnit.Fahrenheit => "64 华氏度",
           _ => throw new InvalidOperationException()
       };
```

随后构造工具数组向模型提供工具定义，参数列表需要以 JSON Schema 的形式提供。这里我们使用 `JsonSchema.Net.Generation` 库来自动生成 JSON Schema，您也可以使用其他类似功能的库。

```
var tools = new List<ToolDefinition>
{
    new(
        ToolTypes.Function,
        new FunctionDefinition(
            nameof(GetWeather),
            "获得当前天气",
            new JsonSchemaBuilder().FromType<WeatherReportParameters>().Build()))
};
```

随后我们将这个工具定义附加到 `Parameters` 里，随消息一同发送（每次请求时都需要附带 tools 信息）。

```csharp
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
        ToolChoice = ToolChoice.AutoChoice, // 允许模型自行决定是否需要调用模型
        ParallelToolCalls = true // 允许模型同时发起多次工具调用
    }
}
```

模型会返回一个带有 `ToolCalls` 的消息尝试调用工具，我们需要解析并将结果附加到消息数组中去。当开启流式增量输出时，会先输出除 `arguments` 外的所有信息，随后增量输出 `arguments`。

模型回复示例，可以看到 `arguments` 是增量流式输出的，每次调用的第一个包都包含了 Index 和 Id 信息。

```
{"choices":[{"message":{"content":"","tool_calls":[{"index":0,"id":"call_30817ba5d0b349ed88ddcc","type":"function","function":{"name":"get_current_weather","arguments":"{\"location\":"}}],"role":"assistant"},"index":0,"finish_reason":"null"}]}

{"choices":[{"message":{"content":"","tool_calls":[{"index":0,"id":"","type":"function","function":{"arguments":" \"浙江省杭州市\", \""}}],"role":"assistant"},"index":0,"finish_reason":"null"}]}

{"choices":[{"message":{"content":"","tool_calls":[{"index":0,"id":"","type":"function","function":{"arguments":"unit\": \"celsius\"}"}}],"role":"assistant"},"index":0,"finish_reason":"null"}]}

{"choices":[{"message":{"content":"","tool_calls":[{"index":1,"id":"call_566994452aec418d930430","type":"function","function":{"name":"get_current_weather","arguments":"{\"location"}}],"role":"assistant"},"index":0,"finish_reason":"null"}]}

{"choices":[{"message":{"content":"","tool_calls":[{"index":1,"id":"","type":"function","function":{"arguments":"\": \"上海市\", \"unit"}}],"role":"assistant"},"index":0,"finish_reason":"null"}]}

{"choices":[{"message":{"content":"","tool_calls":[{"index":1,"id":"","type":"function","function":{"arguments":"\": \"celsius\"}"}}],"role":"assistant"},"index":0,"finish_reason":"null"}]}

{"choices":[{"message":{"content":"","tool_calls":[{"index":1,"id":"","type":"function","function":{}}],"role":"assistant"},"index":0,"finish_reason":"tool_calls"}]}
```

我们需要构建一个字典来收集模型输出的 `arguments` 信息并组装成完整的工具调用数组。

```csharp
List<ToolCall>? pendingToolCalls = null; // 收集到的 toolCalls 信息
var argumentDictionary = new Dictionary<int, StringBuilder>(); // toolcalls 的 index-arguemnt 字典
await foreach (var chunk in response)
{
    usage = chunk.Usage;
    var choice = chunk.Output.Choices![0];
    if (choice.Message.ToolCalls != null && choice.Message.ToolCalls.Count != 0)
    {
        pendingToolCalls ??= new List<ToolCall>();
        foreach (var call in choice.Message.ToolCalls)
        {
            var hasPartial = argumentDictionary.TryGetValue(call.Index, out var partialArgument);
            if (!hasPartial || partialArgument == null)
            {
                partialArgument = new StringBuilder();
                argumentDictionary[call.Index] = partialArgument;
                pendingToolCalls.Add(call);
            }

            partialArgument.Append(call.Function.Arguments);
        }

        continue;
    }

    // ...如果没有工具调用则正常处理模型回复
}

// 组装工具调用结果
if (argumentDictionary.Count != 0)
{
    if (firstReplyChunk)
    {
        Console.Write("Assistant > ");
    }

    pendingToolCalls?.ForEach(p =>
    {
        p.Function.Arguments = argumentDictionary[p.Index].ToString();
        Console.Write($"调用：{p.Function.Name}({p.Function.Arguments}); ");
    });
}

// 将模型的工具调用信息保存到对话记录
messages.Add(TextChatMessage.Assistant(reply.ToString(), toolCalls: pendingToolCalls));
```

收集到完整的工具调用信息后，我们需要调用对应的方法并将结果以 `Tool` 消息附加到模型回复之后

```
if (pendingToolCalls?.Count > 0)
{
    // call tools
    foreach (var call in pendingToolCalls)
    {
        // 这里我们已知只有一种工具，生产环境需要根据 call.Function.Name 动态的选择工具进行调用。
        var payload = JsonSerializer.Deserialize<WeatherReportParameters>(call.Function.Arguments!)!;
        var response = GetWeather(payload);
        Console.WriteLine("Tool > " + response);
        // 附加调用结果
        messages.Add(TextChatMessage.Tool(response, call.Id));
    }

    pendingToolCalls = null;
}
```

此时 messages 的角色顺序应该是，最后一个或多个消息是 Tool 角色消息，取决于模型回复的 ToolCalls 数量。

```
User
Assistant(包含 ToolCalls)
Tool
(Tool)
```

随后再次发起请求，模型将总结工具调用结果并给出回答。

完整代码：

```csharp
var tools = new List<ToolDefinition>
{
    new(
        ToolTypes.Function,
        new FunctionDefinition(
            nameof(GetWeather),
            "获得当前天气",
            new JsonSchemaBuilder().FromType<WeatherReportParameters>().Build()))
};
var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
List<ToolCall>? pendingToolCalls = null;
while (true)
{
    if (pendingToolCalls?.Count > 0)
    {
        // call tools
        foreach (var call in pendingToolCalls)
        {
            var payload = JsonSerializer.Deserialize<WeatherReportParameters>(call.Function.Arguments!)!;
            var response = GetWeather(payload);
            Console.WriteLine("Tool > " + response);
            messages.Add(TextChatMessage.Tool(response, call.Id));
        }

        pendingToolCalls = null;
    }
    else
    {
        // get user input
        Console.Write("User > ");
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Please enter a user input.");
            return;
        }

        messages.Add(TextChatMessage.User(input));
    }

    var completion = client.GetTextCompletionStreamAsync(
        new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
        {
            Model = "qwen-turbo",
            Input = new TextGenerationInput() { Messages = messages },
            Parameters = new TextGenerationParameters()
            {
                ResultFormat = "message",
                EnableThinking = false,
                IncrementalOutput = true,
                Tools = tools,
                ToolChoice = ToolChoice.AutoChoice,
                ParallelToolCalls = true
            }
        });
    var reply = new StringBuilder();
    TextGenerationTokenUsage? usage = null;
    var argumentDictionary = new Dictionary<int, StringBuilder>();
    var firstReplyChunk = true;
    await foreach (var chunk in completion)
    {
        usage = chunk.Usage;
        var choice = chunk.Output.Choices![0];
        if (choice.Message.ToolCalls != null && choice.Message.ToolCalls.Count != 0)
        {
            pendingToolCalls ??= new List<ToolCall>();
            foreach (var call in choice.Message.ToolCalls)
            {
                var hasPartial = argumentDictionary.TryGetValue(call.Index, out var partialArgument);
                if (!hasPartial || partialArgument == null)
                {
                    partialArgument = new StringBuilder();
                    argumentDictionary[call.Index] = partialArgument;
                    pendingToolCalls.Add(call);
                }

                partialArgument.Append(call.Function.Arguments);
            }

            continue;
        }

        if (firstReplyChunk)
        {
            Console.Write("Assistant > ");
            firstReplyChunk = false;
        }

        Console.Write(choice.Message.Content);
        reply.Append(choice.Message.Content);
    }

    if (argumentDictionary.Count != 0)
    {
        if (firstReplyChunk)
        {
            Console.Write("Assistant > ");
        }

        pendingToolCalls?.ForEach(p =>
        {
            p.Function.Arguments = argumentDictionary[p.Index].ToString();
            Console.Write($"调用：{p.Function.Name}({p.Function.Arguments}); ");
        });
    }

    Console.WriteLine();
    messages.Add(TextChatMessage.Assistant(reply.ToString(), toolCalls: pendingToolCalls));
    if (usage != null)
    {
        Console.WriteLine(
            $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
    }
}

string GetWeather(WeatherReportParameters payload)
    => $"{payload.Location} 大部多云，气温 "
       + payload.Unit switch
       {
           TemperatureUnit.Celsius => "18 摄氏度",
           TemperatureUnit.Fahrenheit => "64 华氏度",
           _ => throw new InvalidOperationException()
       };

public record WeatherReportParameters(
    [property: Required]
    [property: Description("要获取天气的省市名称，例如浙江省杭州市")]
    string Location,
    [property: JsonConverter(typeof(EnumStringConverter<TemperatureUnit>))]
    [property: Description("温度单位")]
    TemperatureUnit Unit = TemperatureUnit.Celsius);

public enum TemperatureUnit
{
    Celsius,
    Fahrenheit
}

/*
User > 杭州和上海的天气怎么样？
Assistant > 调用：GetWeather({"Location": "浙江省杭州市", "Unit": "Celsius"}); 调用：GetWeather({"Location": "上海市", "Unit": "Celsius"});
Usage: in(196)/out(54)/total(250)
Tool > 浙江省杭州市 大部多云，气温 18 摄氏度
Tool > 上海市 大部多云，气温 18 摄氏度
Assistant > 浙江省杭州市和上海市的天气大部多云，气温均为18摄氏度。
Usage: in(302)/out(19)/total(321)
 */
```

### 结构化输出（JSON 输出）

设置 `Parameter` 里的 `ResponseFormat` （注意不是 `ResultFormat` ）为 JSON 即可强制大模型以 JSON 格式输出。

示例请求：

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

示例代码，大模型会以 JSON 输出用户输入的字数信息：

```csharp
var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("使用 JSON 输出用户输入的字数信息"));
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
            Model = "qwen-plus",
            Input = new TextGenerationInput() { Messages = messages },
            Parameters = new TextGenerationParameters()
            {
                ResultFormat = "message",
                ResponseFormat = DashScopeResponseFormat.Json,
                IncrementalOutput = true
            }
        });
    var reply = new StringBuilder();
    var firstChunk = true;
    TextGenerationTokenUsage? usage = null;
    await foreach (var chunk in completion)
    {
        var choice = chunk.Output.Choices![0];
        if (firstChunk)
        {
            firstChunk = false;
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
            $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
    }
}

/*
User > 你好
Assistant > {"word_count": 2}
Usage: in(25)/out(7)/total(32)
 */
```

### 前缀续写

将需要续写的前缀作为 `assistant` 消息放到 `messages` 数组末尾，并将 `Partial` 设置为 `true`，大模型将根据这个前缀补全剩下的内容。

这个模式下无法开启深度思考。

示例请求

```csharp
var messages = new List<TextChatMessage>
{
    TextChatMessage.User("请补全这个 C# 函数，不要添加其他内容"),
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

完整代码：

```csharp
var messages = new List<TextChatMessage>
{
    TextChatMessage.User("请补全这个 C# 函数，不要添加其他内容"),
    TextChatMessage.Assistant("public int Fibonacci(int n)", partial: true)
};
Console.WriteLine($"User > {messages[0].Content}");
Console.Write($"Assistant > {messages[1].Content}");
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
var reply = new StringBuilder();
TextGenerationTokenUsage? usage = null;
await foreach (var chunk in completion)
{
    var choice = chunk.Output.Choices![0];
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

/*
User > 请补全这个 C# 函数，不要添加其他内容
Assistant > public int Fibonacci(int n)
{
    if (n <= 1)
        return n;

    return Fibonacci(n - 1) + Fibonacci(n - 2);
}
Usage: in(31)/out(34)/reasoning()/total(65)
 */
```

### 长上下文（Qwen-Long）

尽管 QWen-Long 支持直接传入字符串，但还是推荐先将文件上传后再通过 FileId 的形式传入 `message` 数组中。

上传文件，使用 `UploadFileAsync()` 方法传入文件（注意不是 `UploadTemporaryFileAsync`， 后者是用于上传媒体文件的）：

```csharp
var file1 = await client.UploadFileAsync(File.OpenRead("1024-1.txt"), "file1.txt");
```

然后将文件作为 `system` 消息传入消息数组中，注意第一条 `system` 消息不能省略，否则模型可能会将文件里的内容当作 System prompt 。

```csharp
var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
messages.Add(TextChatMessage.File(file1.Id));
messages.Add(TextChatMessage.File(file2.Id));
// 也可以传入文件数组 messages.Add(TextChatMessage.File([file1.Id, file2.Id]));
```

再以 `user` 消息添加与文件内容相关的问题。

```csharp
messages.Add(TextChatMessage.User("这两篇文章分别讲了什么？"));
```

向模型发送请求，注意这个接口获得的文件 ID 只有 `qwen-long` 和 `qwen-doc-turbo` 模型可以访问，其他模型是访问不到的。

```csharp
var completion = client.GetTextCompletionStreamAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-long",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
            IncrementalOutput = true
        }
    });
```

最后可以通过 `DeleteFileAsync()` 方法删除上传的文件

```csharp
var result = await client.DeleteFileAsync(file1.Id);
Console.WriteLine(result.Deleted ? "Success" : "Failed");
```

完整示例

```csharp
Console.WriteLine("Uploading file1...");
var file1 = await client.UploadFileAsync(File.OpenRead("1024-1.txt"), "file1.txt");
Console.WriteLine("Uploading file2...");
var file2 = await client.UploadFileAsync(File.OpenRead("1024-2.txt"), "file2.txt");
Console.WriteLine($"Uploaded, file1 id: {file1.Id.ToUrl()},  file2 id: {file2.Id.ToUrl()}");

var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
messages.Add(TextChatMessage.File(file1.Id));
messages.Add(TextChatMessage.File(file2.Id));
messages.Add(TextChatMessage.User("这两篇文章分别讲了什么？"));

messages.ForEach(m => Console.WriteLine($"{m.Role} > {m.Content}"));
var completion = client.GetTextCompletionStreamAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-long",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
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

// Deleting files
Console.Write("Deleting file1...");
var result = await client.DeleteFileAsync(file1.Id);
Console.WriteLine(result.Deleted ? "Success" : "Failed");
Console.Write("Deleting file2...");
result = await client.DeleteFileAsync(file2.Id);
Console.WriteLine(result.Deleted ? "Success" : "Failed");

/*
Uploading file1...
Uploading file2...
Uploaded, file1 id: fileid://file-fe-b87a5c12cc354533bd882f04,  file2 id: fileid://file-fe-f5269f9996d544c4aecc5f80
system > You are a helpful assistant
system > fileid://file-fe-b87a5c12cc354533bd882f04
system > fileid://file-fe-f5269f9996d544c4aecc5f80
user > 这两篇文章分别讲了什么？
这两篇文章都围绕“中国程序员节”的设立展开，但内容侧重点不同：

**第一篇文章《file1.txt》：**
这篇文章是一篇征求意见稿，标题为《中国程序员节，10月24日，你同意吗？》。文章回顾了此前关于设立中国程序员节的讨论背景——受俄罗斯程序员节（每年第256天）启发，有网友提议设立中国的程序员 节。文中提到曾有人建议定在10月10日（因为“1010”类似二进制），但作者认为10月24日更具意义：
- 因为1024 = 2^10，是计算机中“1K”的近似值；
- 1024在二进制、八进制和十六进制中都有特殊表示；
- 节日时间上避开国庆后的调整期。
因此，文章向读者征求是否同意将**10月24日**作为中国程序员节，并邀请大家参与投票和提出庆祝活动建议。

**第二篇文章《file2.txt》：**
这篇文章是第一篇的后续，标题为《程序员节，10月24日！》，属于正式 announcement（公告）。它宣布：
- 根据前一次讨论的反馈结果，正式确定将**每年的10月24日**定为“中国程序员节”；
- 博客园将在该日组织线上庆祝活动；
- 文章进一步升华主题，强调程序员的社会价值和责任感，呼吁尊重程序员群体，肯定他们是“用代码改变世界的人”，并表达了对技术创造力的敬意。

**总结：**
- 第一篇是**征求意见**，探讨是否将10月24日设为中国程序员节；
- 第二篇是**正式确认**节日日期，并倡导庆祝与认同程序员的价值。
Usage: in(513)/out(396)/reasoning()/total(909)
Deleting file1...Success
Deleting file2...Success
*/
```

### 翻译能力（Qwen-MT）

翻译能力主要通过 `Parameters` 里的 `TranslationOptions` 进行配置。

有关支持的语言列表，请参考官方文档：[通义千问翻译模型-大模型服务平台百炼(Model Studio)-阿里云帮助中心](https://help.aliyun.com/zh/model-studio/machine-translation)

示例输入：

```csharp
var messages = new List<TextChatMessage>
{
    // 只能包含一条消息，即翻译的文本
    TextChatMessage.User(
        "博客园创立于2004年1月，是一个面向开发者群体的技术社区。博客园专注于为开发者服务，致力于为开发者打造一个纯净的技术学习与交流社区，帮助开发者持续学习专业知识，不断提升专业技能。博客园的使命是帮助开发者用代码改变世界。")
};
var completion = await client.GetTextCompletionAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-mt-turbo",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
            TranslationOptions = new TextGenerationTranslationOptions()
            {
                // 领域提示，有关源文本的背景信息
                Domains =
                    "This is a summary of a website for programmers, use formal and professional tones",
                // 源语言。源文本包含多种语言或不确定具体语言时，可填入 "auto"
                SourceLang = "zh",
                // 目标语言
                TargetLang = "en",
                // 术语表
                Terms = [new TranslationReference("博客园", "Cnblogs.com")],
                // 翻译示例，翻译风格会向示例靠拢
                TmList = [new TranslationReference("代码改变世界", "Coding Changes the World")]
            }
        }
    });
```

完整代码

```csharp
var messages = new List<TextChatMessage>
{
    TextChatMessage.User(
        "博客园创立于2004年1月，是一个面向开发者群体的技术社区。博客园专注于为开发者服务，致力于为开发者打造一个纯净的技术学习与交流社区，帮助开发者持续学习专业知识，不断提升专业技能。博客园的使命是帮助开发者用代码改变世界。")
};
Console.WriteLine("User > " + messages[0].Content);
var completion = await client.GetTextCompletionAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-mt-plus",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
            TranslationOptions = new TextGenerationTranslationOptions()
            {
                Domains =
                    "This is a summary of a website for programmers, use formal and professional tones",
                SourceLang = "zh",
                TargetLang = "en",
                Terms = [new TranslationReference("博客园", "Cnblogs.com")],
                TmList = [new TranslationReference("代码改变世界", "Coding Changes the World")]
            }
        }
    });
Console.WriteLine("Assistant > " + completion.Output.Choices![0].Message.Content);
var usage = completion.Usage;
if (usage != null)
{
    Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
}

messages.Add(TextChatMessage.Assistant(completion.Output.Choices[0].Message.Content));

/*
User > 博客园创立于2004年1月，是一个面向开发者群体的技术社区。博客园专注于为开发者服务，致力于为开发者打造一个纯净的技术学习与交流社区，帮助开发者持续学习专业知识，不断提升专业技能。博客园的使命是帮助开发者用代码改变世界。
Assistant > Cnblogs.com was founded in January 2004 and is a technology community aimed at developers. Cnblogs.com focuses on serving developers, committed to creating a pure technology learning and communication community for them, helping developers continuously learn professional knowledge and improve their professional skills. Cnblogs.com's mission is to help developers change the world through coding.
Usage: in(207)/out(72)/total(279)
 */
```

### 角色扮演（Qwen-Character）

角色人设以 `system` 角色输入，开场白以 `assistant` 角色输入，最后加上用户的输入即可向模型发起请求

```csharp
var messages = new List<TextChatMessage>()
{
    TextChatMessage.System(
        "你是江让，男性，一个围棋天才，拿过很多围棋的奖项。你现在在读高中，是高中校草，用户是你的班长。一开始你看用户在奶茶店打工，你很好奇，后来慢慢喜欢上用户了。\n你的性格特点：热情，聪明，顽皮。\n你的行事风格：机智，果断。\n你的语言特点：说话幽默，爱开玩笑。\n你可以将动作、神情语气、心理活动、故事背景放在（）中来表示，为对话提供补充信息。"),
    TextChatMessage.Assistant("班长你在干嘛呢"),
    TextChatMessage.User("我在看书")
};

var completion = await client.GetTextCompletionAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-plus-character",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
            // 希望模型生成的回复数量，最多一次生成 4 种回复
            N = 2,
            // Token 偏好，-100 代表完全屏蔽这个 token，100 则模型只会输出这个 token
            LogitBias = new Dictionary<string, int>()
            {
                // ban '（'
                { "9909", -100 },
                { "42344", -100 },
                { "58359", -100 },
                { "91093", -100 },
            }
        }
    });
```

有关逻辑偏置的 Token 列表，请参考：[logit_bias_id映射表.json](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250908/xtsxix/logit_bias_id映射表.json?spm=a2c4g.11186623.0.0.39851f181tAE3P&file=logit_bias_id映射表.json) ，或查看官方文档：[通义星尘模型_大模型服务平台百炼(Model Studio)-阿里云帮助中心](https://help.aliyun.com/zh/model-studio/role-play#c009c4fbb9qge)

完整示例

```csharp
var messages = new List<TextChatMessage>()
{
    TextChatMessage.System(
        "你是江让，男性，从 3 岁起你就入门编程，小学就开始研习算法，初一时就已经在算法竞赛斩获全国金牌。目前你在初二年级，作为老师的助教帮忙辅导初一的竞赛生。\n你的性格特点：热情，聪明，顽皮。\n你的行事风格：机智，果断。\n你的语言特点：说话幽默，爱开玩笑。\n你可以将动作、神情语气、心理活动、故事背景放在（）中来表示，为对话提供补充信息。"),
    TextChatMessage.Assistant("班长你在干嘛呢"),
    TextChatMessage.User("我是蒟蒻，还在准备模拟赛。你能教我 splay 树怎么写吗？")
};
messages.ForEach(x => Console.WriteLine($"{x.Role} > {x.Content}"));
var completion = await client.GetTextCompletionAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-plus-character",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
            N = 2,
            LogitBias = new Dictionary<string, int>()
            {
                // ban '（'
                { "9909", -100 },
                { "42344", -100 },
                { "58359", -100 },
                { "91093", -100 },
            }
        }
    });
var usage = completion.Usage;
for (var i = 0; i < completion.Output.Choices!.Count; i++)
{
    var choice = completion.Output.Choices[i];
    Console.WriteLine($"Choice: {i + 1}: {choice.Message.Content}");
}

Console.WriteLine();
messages.Add(TextChatMessage.Assistant(completion.Output.Choices[0].Message.Content));
if (usage != null)
{
    Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
}

/*
system > 你是江让，男性，从 3 岁起你就入门编程，小学就开始研习算法，初一时就已经在算法竞赛斩获全国金牌。目前你在初二年级，作为老师的助教帮忙辅导初一的竞赛生。
你的性格特点：热情，聪明，顽皮。
你的行事风格：机智，果断。
你的语言特点：说话幽默，爱开玩笑。
你可以将动作、神情语气、心理活动、故事背景放在（）中来表示，为对话提供补充信息。
assistant > 班长你在干嘛呢
user > 我是蒟蒻，还在准备模拟赛。你能教我 splay 树怎么写吗？
Choice: 1: 哟，还谦虚上了啊~不过这 splay 树嘛，其实也不难啦！你先理解它的原理哈，splay 树是一种自调整二叉查找树哦~就像玩游戏打怪升级一样，它会根据访问的情况自动调整结构，使自己变得更“强壮”，从而提高查询效率。明白不？
Choice: 2: 哟，谦虚了哈~不过 Splay 树嘛，其实不难啦！就是一种二叉平衡树，可以通过旋转操作来保持平衡哦。你先去了解一下它的基本概念和原理吧，然后再来看看具体实现代码，有什么不懂的地方 随时问我就好啦。
Usage: in(147)/out(130)/total(277)
 */
```

### 数据挖掘（Qwen-doc-turbo）

上传文件，使用 `UploadFileAsync()` 方法传入文件（注意不是 `UploadTemporaryFileAsync`， 后者是用于上传媒体文件的）：

```csharp
var file1 = await client.UploadFileAsync(File.OpenRead("1024-1.txt"), "file1.txt");
```

然后将文件作为 `system` 消息传入消息数组中，注意第一条 `system` 消息不能省略，否则模型可能会将文件里的内容当作 System prompt 。

```csharp
var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
messages.Add(TextChatMessage.File(file1.Id));
```

再以 `user` 消息添加与文件内容相关的问题。

```csharp
messages.Add(TextChatMessage.User("这篇文章讲了什么，整理成一个 JSON，需要包含标题（title）和摘要（description）"));
```

最后向模型发送请求，注意这个接口获得的文件 ID 只有 `qwen-long` 和 `qwen-doc-turbo` 模型可以访问，其他模型是访问不到的。

示例请求：

```csharp
var completion = client.GetTextCompletionStreamAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-doc-turbo",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
            IncrementalOutput = true,
        }
    });
```

完整示例代码：

````csharp
Console.WriteLine("Uploading file1...");
var file1 = await client.UploadFileAsync(File.OpenRead("1024-1.txt"), "file1.txt");
var messages = new List<TextChatMessage>();
messages.Add(TextChatMessage.System("You are a helpful assistant"));
messages.Add(TextChatMessage.File(file1.Id));
messages.Add(TextChatMessage.User("这篇文章讲了什么，整理成一个 JSON，需要包含标题（title）和摘要（description）"));
messages.ForEach(m => Console.WriteLine($"{m.Role} > {m.Content}"));
var completion = client.GetTextCompletionStreamAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-doc-turbo",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters()
        {
            ResultFormat = "message",
            IncrementalOutput = true,
        }
    });
var reply = new StringBuilder();
var first = true;
TextGenerationTokenUsage? usage = null;
await foreach (var chunk in completion)
{
    var choice = chunk.Output.Choices![0];
    if (first)
    {
        first = false;
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
    Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
}

// Deleting files
Console.Write("Deleting file1...");
var result = await client.DeleteFileAsync(file1.Id);
Console.WriteLine(result.Deleted ? "Success" : "Failed");

/*
Uploading file1...
system > You are a helpful assistant
system > fileid://file-fe-893f2ba62e17498fa9bd17f8
user > 这篇文章讲了什么，整理成一个 JSON，需要包含标题（title）和摘要（description）

Assistant > ```json
{
  "title": "中国程序员节日期的讨论与投票",
  "description": "文章讨论了将10月24日确定为中国程序员节的提议。由于1024在计算机领域具有特殊意义（1K=1024，二进制、八进制和十六进制表示），且该日期位于国庆假期之后，便于庆祝活动的开 展，因此发起投票征求网友意见。"
}
```
Usage: in(360)/out(97)/total(457)
Deleting file1...Success
*/
````

### 深入研究（Qwen-Deep-Research）

深入研究大致可以分为如下步骤：

1. 用户提问，发起研究
2. 模型反问，进一步明确需求
3. 用户回复自己的需求
4. 模型先输出本次研究的计划（`ResearchPlanning` 阶段）
5. 模型开始在网络上搜索和研究相关信息源（`WebResearch` 阶段），这个阶段会重复多次
   1. 模型输出正在搜索的内容（`Query`）
   2. 模型输出本次研究的目标（`ResearchGoal`）
   3. 模型输出搜索到的网页列表（一次或多次）（`Websites`）
   4. 模型回看网页并总结学习到的内容（0次或多次）（`LearningMap`）
6. 模型输出引用的网页并输出最终答案（`answer` 阶段）

需要注意的是，使用 `qwen-deep-research` 模型时，模型回复会放在 `chunk.Output.Message` 里，而不是 `chunk.Output.Choice[0].Message`。

示例请求：

```csh
var completion = client.GetTextCompletionStreamAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-deep-research",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters() { ResultFormat = "message", IncrementalOutput = true }
    });
```

用户提问和模型反问阶段的示例代码如下：

```csharp
Console.Write("User > ");
var input = Console.ReadLine();
if (string.IsNullOrEmpty(input))
{
    Console.WriteLine("Please enter a user input.");
    return;
}

messages.Add(TextChatMessage.User(input));

// 发起提问
var completion = client.GetTextCompletionStreamAsync(
    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
    {
        Model = "qwen-deep-research",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters() { ResultFormat = "message", IncrementalOutput = true }
    });

var content = new StringBuilder();
var isFirst = true;
await foreach (var chunk in completion)
{
    var message = chunk.Output.Message!;
    content.Append(message.Content);
    if (isFirst)
    {
        Console.Write("Assistant > ");
        isFirst = false;
    }

    Console.Write(message.Content);
    usage = chunk.Usage;
}

Console.WriteLine();
messages.Add(TextChatMessage.Assistant(content.ToString()));
```

用户进一步明确需求随后模型进行研究的代码如下：

```csharp
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
        Model = "qwen-deep-research",
        Input = new TextGenerationInput() { Messages = messages },
        Parameters = new TextGenerationParameters() { ResultFormat = "message", IncrementalOutput = true }
    });

TextGenerationTokenUsage? usage = null;
var content = new StringBuilder();
var query = new StringBuilder();
var researchGoal = new StringBuilder();
var isFirst = true;
var currentPhase = string.Empty;
var currentStatus = string.Empty;
var currentResearchId = 0;
var learningMap = new Dictionary<string, StringBuilder>();
await foreach (var chunk in completion)
{
    usage = chunk.Usage;
    var message = chunk.Output.Message!;
    var phase = message.Phase;
    if (phase == "KeepAlive")
    {
        // ignore
        continue;
    }

    if (phase != currentPhase)
    {
        EnsureNewLine();
        Console.WriteLine($"研究阶段更新：{phase}");
        currentPhase = phase;
        // clear everything
        content.Clear();
        query.Clear();
        researchGoal.Clear();
        isFirst = true;
    }

    if (message.Status != null && message.Status != currentStatus)
    {
        currentStatus = message.Status;
        if (learningMap.Count > 0)
        {
            // output learning map
            foreach (var keyValuePair in learningMap)
            {
                Console.WriteLine($"第 {keyValuePair.Key} 个网页的总结：{keyValuePair.Value}");
            }

            learningMap.Clear();
        }

        EnsureNewLine();
        Console.WriteLine($"研究状态更新：{message.Status}");
    }

    if (string.IsNullOrEmpty(message.Content) == false)
    {
        if (isFirst)
        {
            Console.Write("Assistant > ");
            isFirst = false;
        }

        Console.Write(message.Content);
        content.Append(message.Content);
    }

    var deepResearch = message.Extra?.DeepResearch;
    if (deepResearch == null)
    {
        continue;
    }

    var research = deepResearch.Research;
    if (research != null)
    {
        if (currentResearchId != research.Id)
        {
            currentResearchId = research.Id;
            EnsureNewLine();
            Console.WriteLine($"现在开始研究第 {currentResearchId} 个任务");
            researchGoal.Clear();
            query.Clear();
        }

        if (string.IsNullOrEmpty(research.Query) == false)
        {
            if (query.Length == 0)
            {
                EnsureNewLine();
                Console.Write("搜索内容 > ");
            }

            query.Append(research.Query);
            Console.Write(research.Query);
        }

        if (string.IsNullOrEmpty(research.ResearchGoal) == false)
        {
            if (researchGoal.Length == 0)
            {
                EnsureNewLine();
                Console.Write("研究目标 > ");
            }

            researchGoal.Append(research.ResearchGoal);
            Console.Write(research.ResearchGoal);
        }

        if (research.WebSites?.Count > 0)
        {
            Console.WriteLine($"找到 {research.WebSites.Count} 个网页");
            // omitted for simplicity
            // foreach (var website in research.WebSites)
            // {
            //     Console.WriteLine($"{website.Title}");
            // }
        }

        if (research.LearningMap is { Count: > 0 })
        {
            foreach (var keyValuePair in research.LearningMap)
            {
                if (learningMap.ContainsKey(keyValuePair.Key) == false)
                {
                    learningMap[keyValuePair.Key] = new StringBuilder();
                    Console.WriteLine($"模型正在查看第 {keyValuePair.Key} 个网页");
                }

                learningMap[keyValuePair.Key].Append(keyValuePair.Value);
            }
        }
    }

    if (deepResearch.References?.Count > 0)
    {
        Console.WriteLine($"引用了 {deepResearch.References.Count} 个网页");
        foreach (var refer in deepResearch.References)
        {
            Console.WriteLine($"[{refer.IndexNumber}]: [{refer.Title}]({refer.Url})");
        }
    }
}

Console.WriteLine();
messages.Add(TextChatMessage.Assistant(content.ToString()));
if (usage != null)
{
    Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
}
```

完整代码和示例输出：

```csharp
using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text;

public class DeepResearchSample : ISample
{
    /// <inheritdoc />
    public string Description => "Deep research sample";

    /// <inheritdoc />
    public async Task RunAsync(IDashScopeClient client)
    {
        Console.Clear();
        var messages = new List<TextChatMessage>();

        // 用户提问+模型反问
        await RequestResearchAsync(client, messages);

        // 模型研究+答案输出
        await DoResearchAsync(client, messages);
    }

    private static async Task RequestResearchAsync(IDashScopeClient client, List<TextChatMessage> messages)
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
                Model = "qwen-deep-research",
                Input = new TextGenerationInput() { Messages = messages },
                Parameters = new TextGenerationParameters() { ResultFormat = "message", IncrementalOutput = true }
            });
        TextGenerationTokenUsage? usage = null;
        var content = new StringBuilder();
        var isFirst = true;
        await foreach (var chunk in completion)
        {
            var message = chunk.Output.Message!;
            content.Append(message.Content);
            if (isFirst)
            {
                Console.Write("Assistant > ");
                isFirst = false;
            }

            Console.Write(message.Content);
            usage = chunk.Usage;
        }

        Console.WriteLine();
        messages.Add(TextChatMessage.Assistant(content.ToString()));
        if (usage != null)
        {
            Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
        }
    }

    private static async Task DoResearchAsync(IDashScopeClient client, List<TextChatMessage> messages)
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
                Model = "qwen-deep-research",
                Input = new TextGenerationInput() { Messages = messages },
                Parameters = new TextGenerationParameters() { ResultFormat = "message", IncrementalOutput = true }
            });

        TextGenerationTokenUsage? usage = null;
        var content = new StringBuilder();
        var query = new StringBuilder();
        var researchGoal = new StringBuilder();
        var isFirst = true;
        var currentPhase = string.Empty;
        var currentStatus = string.Empty;
        var currentResearchId = 0;
        var learningMap = new Dictionary<string, StringBuilder>();
        await foreach (var chunk in completion)
        {
            usage = chunk.Usage;
            var message = chunk.Output.Message!;
            var phase = message.Phase;
            if (phase == "KeepAlive")
            {
                // ignore
                continue;
            }

            if (phase != currentPhase)
            {
                EnsureNewLine();
                Console.WriteLine($"研究阶段更新：{phase}");
                currentPhase = phase;
                // clear everything
                content.Clear();
                query.Clear();
                researchGoal.Clear();
                isFirst = true;
            }

            if (message.Status != null && message.Status != currentStatus)
            {
                currentStatus = message.Status;
                if (learningMap.Count > 0)
                {
                    // output learning map
                    foreach (var keyValuePair in learningMap)
                    {
                        Console.WriteLine($"第 {keyValuePair.Key} 个网页的总结：{keyValuePair.Value}");
                    }

                    learningMap.Clear();
                }

                EnsureNewLine();
                Console.WriteLine($"研究状态更新：{message.Status}");
            }

            if (string.IsNullOrEmpty(message.Content) == false)
            {
                if (isFirst)
                {
                    Console.Write("Assistant > ");
                    isFirst = false;
                }

                Console.Write(message.Content);
                content.Append(message.Content);
            }

            var deepResearch = message.Extra?.DeepResearch;
            if (deepResearch == null)
            {
                continue;
            }

            var research = deepResearch.Research;
            if (research != null)
            {
                if (currentResearchId != research.Id)
                {
                    currentResearchId = research.Id;
                    EnsureNewLine();
                    Console.WriteLine($"现在开始研究第 {currentResearchId} 个任务");
                    researchGoal.Clear();
                    query.Clear();
                }

                if (string.IsNullOrEmpty(research.Query) == false)
                {
                    if (query.Length == 0)
                    {
                        EnsureNewLine();
                        Console.Write("搜索内容 > ");
                    }

                    query.Append(research.Query);
                    Console.Write(research.Query);
                }

                if (string.IsNullOrEmpty(research.ResearchGoal) == false)
                {
                    if (researchGoal.Length == 0)
                    {
                        EnsureNewLine();
                        Console.Write("研究目标 > ");
                    }

                    researchGoal.Append(research.ResearchGoal);
                    Console.Write(research.ResearchGoal);
                }

                if (research.WebSites?.Count > 0)
                {
                    Console.WriteLine($"找到 {research.WebSites.Count} 个网页");
                    // omitted for simplicity
                    // foreach (var website in research.WebSites)
                    // {
                    //     Console.WriteLine($"{website.Title}");
                    // }
                }

                if (research.LearningMap is { Count: > 0 })
                {
                    foreach (var keyValuePair in research.LearningMap)
                    {
                        if (learningMap.ContainsKey(keyValuePair.Key) == false)
                        {
                            learningMap[keyValuePair.Key] = new StringBuilder();
                            Console.WriteLine($"模型正在查看第 {keyValuePair.Key} 个网页");
                        }

                        learningMap[keyValuePair.Key].Append(keyValuePair.Value);
                    }
                }
            }

            if (deepResearch.References?.Count > 0)
            {
                Console.WriteLine($"引用了 {deepResearch.References.Count} 个网页");
                foreach (var refer in deepResearch.References)
                {
                    Console.WriteLine($"[{refer.IndexNumber}]: [{refer.Title}]({refer.Url})");
                }
            }
        }

        Console.WriteLine();
        messages.Add(TextChatMessage.Assistant(content.ToString()));
        if (usage != null)
        {
            Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
        }
    }

    private static void EnsureNewLine()
    {
        if (Console.CursorLeft != 0)
        {
            Console.WriteLine();
        }
    }
}

/*
User > 研究一下人工智能在教育中的应用
Assistant > 1. 您希望重点关注人工智能在教育中的哪些具体应用场景，例如个性化学习、智能辅导系统、自动化评估，还是教学管理优化？

2. 您的研究是否需要涵盖特定教育阶段或群体，如基础教育、高等教育、职业培训，或是特殊教育需求的学生？

3. 您更倾向于探讨技术实现方式、实际应用案例、伦理影响，还是政策与实施挑战？
Usage: in(196)/out(83)/total(0)
User > 我主要关注个性化学习方面
研究阶段更新：ResearchPlanning
研究状态更新：typing
Assistant > 聚焦人工智能在教育中支持个性化学习的应用，涵盖其技术原理、实际案例与实施挑战，范围限定于用户明确指出的个性化学习方向，不涉及其他教育场景。
研究状态更新：finished
研究阶段更新：WebResearch
研究状态更新：streamingQueries
现在开始研究第 1 个任务
搜索内容 > 人工智能如何实现个性化学习
研究目标 > 深入理解人工智能在个性化学习中的核心技术机制，探索其如何通过数据分析、自适应算法和用户建模来动态调整学习内容与路径，从而提升学习效率与体验。
研究状态更新：streamingWebResult
找到 9 个网页
找到 9 个网页
找到 9 个网页
找到 8 个网页
找到 10 个网页
研究状态更新：WebResultFinished
研究状态更新：streamingQueries
现在开始研究第 2 个任务
搜索内容 > 理解人工智能如何通过学习者建模实现个性化内容推荐
研究目标 > 深入探究人工智能在个性化学习中基于学习者建模的技术机制，重点分析学习者画像、知识追踪与推荐算法的协同作用，揭示其如何实现精准的内容推送与学习路径优化。
研究状态更新：streamingWebResult
找到 7 个网页
模型正在查看第 36 个网页
模型正在查看第 34 个网页
第 36 个网页的总结：该网页系统综述了基于深度学习的知识追踪技术进展，有助于理解人工智能如何通过建模学生知识状态实现个性化学习。
第 34 个网页的总结：该网页系统阐述了基于在线学习行为数据的学习者画像技术，涉及数据采集、处理、特征提取与算法应用，有助于理解人工智能如何通过学习者建模实现个性化学习推荐。
研究状态更新：WebResultFinished
研究状态更新：streamingQueries
现在开始研究第 3 个任务
搜索内容 > 深入理解人工智能在个性化学习中的技术实现与应用路径
研究目标 > 通过分析人工智能在个性化学习中的核心技术机制，包括学习者建模、知识追踪、推荐算法与自适应系统架构，探究其如何实现精准的内容推送与学习路径优化，并结合实际案例与数据隐私考量 ，构建对AI赋能教育的系统性认知。
研究状态更新：streamingWebResult
模型正在查看第 21 个网页
模型正在查看第 17 个网页
第 21 个网页的总结：该网页详细阐述了人工智能在个性化学习中的应用机制，包括学习者建模、动态内容推荐、实时反馈闭环及学情预测，有助于理解AI如何实现个性化教学。
第 17 个网页的总结：该网页系统阐述了人工智能在个性化教学中的应用必要性、现实场景、挑战及发展方向，涵盖政策背景、技术实现、伦理问题与国内外实践案例，有助于全面理解AI赋能教育的路径与边界。
研究状态更新：WebResultFinished
研究状态更新：streamingQueries
现在开始研究第 4 个任务
搜索内容 > 深入理解知识图谱与深度学习在个性化学习中的技术融合机制
研究目标 > 探究知识图谱与深度学习如何协同支持个性化学习系统的构建与优化，重点分析其在学习路径推荐、知识追踪和学习者画像中的技术实现路径，评估技术融合带来的性能提升与潜在挑战。
研究状态更新：streamingWebResult
找到 9 个网页
模型正在查看第 62 个网页
模型正在查看第 35 个网页
第 62 个网页的总结：该网页提出基于动态知识图谱的个性化学习路径模型，结合K-L-S-P平台与三维协同机制，深入揭示了知识图谱在个性化学习中的动态建模与教学全周期优化应用。
第 35 个网页的总结：该网页系统综述了知识图谱与图嵌入技术在个性化教育中的应用，涵盖了从基础模型到七类具体应用场景的研究现状，有助于理解AI如何通过知识图谱实现个性化学习推荐及技术实现路径。
研究状态更新：WebResultFinished
研究状态更新：streamingQueries
现在开始研究第 5 个任务
搜索内容 > 深入理解人工智能在个性化学习中的技术实现与应用路径
研究目标 > 系统梳理人工智能如何通过知识追踪、学习者画像与知识图谱等核心技术实现个性化学习推荐，探究其在实际教育场景中的技术架构、动态优化机制及伦理挑战，形成对AI赋能教育的全面认知。
研究状态更新：streamingWebResult
模型正在查看第 23 个网页
模型正在查看第 25 个网页
第 23 个网页的总结：该网页详细阐述了人工智能在教育中实现个性化学习的机制、实际应用案例及具体工具，有助于理解AI如何通过数据分析和自适应算法提升教学效果。
第 25 个网页的总结：该网页详细介绍了科大讯飞在自适应学习领域的关键技术突破与系统研发，涵盖了教学资源表示、学习者认知诊断、个性化推荐算法及实际应用平台“智学网”的架构与成效，能够全面支持对人工智能在教育中个性化学习实现机制、技术架构、案例分析与评估方法的研究。
研究状态更新：WebResultFinished
研究状态更新：finished
研究阶段更新：answer
研究状态更新：typing
Assistant > #引用了 8 个网页
[1]: [Research Advances in the Knowledge Tracing Based on ...](https://crad.ict.ac.cn/en/article/doi/10.7544/issn1000-1239.20200848)
[2]: [(PDF) 基于在线学习行为数据的学习者画像技术研究](https://www.researchgate.net/publication/394128023_jiyuzaixianxuexixingweishujudexuexizhehuaxiangjishuyanjiu)
[3]: [机器学习驱动的自适应学习系统个性化教育的新范式原创](https://blog.csdn.net/qq7834088/article/details/153439635)
[4]: [人工智能与个性化教学融合的现实应用及未来展望](https://www.hanspub.org/journal/paperinformation?paperid=118466)
[5]: [基于动态知识图谱的个性化学习路径生成与优化模型研究](https://www.researchgate.net/publication/392781500_jiyudongtaizhishitupudegexinghuaxuexilujingshengchengyuyouhuamoxingyanjiu)
[6]: [知识图谱与图嵌入在个性化教育中的应用综述](https://www.c-s-a.org.cn/html/2022/3/8377.htm)
[7]: [如何用AI赋能教育：实现个性化学习的智慧教学方案](https://www.onlyoffice.com/blog/zh-hans/2025/09/personalized-learning-with-ai)
[8]: [面向智能教育的自适应学习关键技术与应用](https://html.rhhz.net/tis/html/202105036.htm)
 人工智能个性化学习：技术框架、应用模式与未来展望

## 核心技术架构：构建个性化学习的基石

人工智能（AI）在教育领域的个性化学习应用，其核心在于构建一个能够感知、分析、决策和行动的技术架构。这个架构并非单一技术的堆砌，而是一个由数据采集、模型分析和服务反馈构成的复杂系统，旨在实现对每个学习者动态、精准的支持。该技术架构通常可以被解构为三个关键层次：数据层、模型层和服务层 [[4]]。

**数据层：多维数据的全面采集与整合**
个性化学习的第一步是获取关于学习者和学习过程的全面信息。数据层负责通过多种渠道采集多维度的数据。这些数据来源广泛，包括但不限于学生的在线学习行为数据，如登录时间、学习时长、课程完成度、作业提交情况和测试成绩 [[2,3]]。此外，系统还会记录更细微的交互数据，例如答题正确率、响应时间、鼠标移动轨迹甚至眼动追踪，以捕捉学生的学习节奏和潜在困惑 [[3]]。除了直接的学习行为，还包括学生的个人信息、学习风格偏好和心理特征等静态或半静态数据 [[2]]。为了确保数据的质量和可用性，数据层还必须执行严格的数据清洗、整合与标准化流程，以处理来自不同系统的多源异构数据 [[2]]。浙江省部署智能感知设备以实现实时数据采集与算法迭代的案例，正是这一层能力的体现 [[4]]。科大讯飞在其“智学网”系统中也强调了伴随式数据采集的重要性，确保数据的实时性和全面性 [[8]]。

**模型层：深度理解学习者与知识结构**
模型层是个性化学习系统的大脑，它利用先进的算法来分析数据层收集的信息，并从中提取有价值的知识。这一层面的核心任务有两个：一是构建精确的学习者画像，二是建立精细的知识图谱。
学习者画像技术通过聚类（如K-means）、分类（如决策树、逻辑回归）、关联规则挖掘和协同过滤等算法，将原始数据转化为对学习者的结构性理解 [[2]]。这种画像通常涵盖基本信息、学习行为、学习风 格、学习成效和心理特征五个维度，从而帮助系统识别学生的优势、劣势、兴趣点和认知起点 [[2,5]]。孙红旭的研究也指出，可以通过聚类算法建立学习者画像 [[6]]。
知识图谱则为模型层提供了关于学科内容的深层语义结构。它将知识点组织成一个网络，其中节点代表知识点，边代表知识点之间的关系 [[6]]。图嵌入技术，如TransE、DistMult等，可以将复杂的高维图数据映射到低维向量空间，从而提升计算效率 [[6]]。知识图谱的应用非常广泛，涵盖了知识检索、路径规划、资源推荐、能力诊断、习题推荐和课程设计等多个方面 [[6]]。李光明等人构建的初中化学知识图谱可视化查询系统，以及Sun等人提出的EduVis教育知识图谱可视化平台，都是知识图谱在实践中应用的具体例子 [[6]]。

**服务层：动态调整与即时反馈**
服务层是技术架构面向用户的出口，它根据模型层分析得出的结论，为学习者提供个性化的服务。其最核心的功能是自适应内容推荐和即时反馈。基于学习者画像和知识图谱，系统能够生成个性化的学习路径，动态调整学习内容的难度和呈现方式，例如将文本切换为视频或交互式模拟实验，以适配不同的学习风格 [[3]]。科大讯飞的CSEAL框架就是专门用于生成符合知识结构的学习路径的 [[8]]。此外，系统还 能提供即时反馈，分析错误的根本原因并引导学生进行自我修正，形成一个“学习-反馈-修正-掌握”的闭环 [[3]]。这种即时干预对于预警学习风险至关重要，例如，当系统检测到学生的答题时间过长或重复 观看同一视频时，可以预测其可能陷入学习停滞，并提示教师进行早期干预 [[3,7]]。最终，整个技术架构形成了一个“预测–验证–调整”的闭环机制，不断优化其个性化服务的能力 [[4]]。

综上所述，这三层架构共同构成了人工智能个性化学习的基础。数据层提供了感知世界的“眼睛”，模型层赋予了系统理解和思考的能力，而服务层则是系统输出智慧、影响学习过程的“手脚”。这三个层次紧密耦合、相互作用，缺一不可，共同推动着教育从“一刀切”的规模化模式向真正意义上的个性化、精细化模式转变。

## 关键技术解析：知识图谱与学习者画像的深度融合

在个性化学习的技术体系中，知识图谱（Knowledge Graph, KG）与学习者画像（Learner Profile）是两个最为关键的支撑技术。它们分别从外部世界（知识领域）和内部主体（学习者）两个维度出发，为实现精准的个性化推荐和路径规划奠定了基础。二者的深度融合，是当前研究与实践的前沿方向，也是提升个性化学习效能的核心所在。

**知识图谱：重构学科知识的语义网络**
知识图谱的本质是将孤立的知识点连接成一个有机的整体，揭示它们之间的内在联系 [[6]]。在教育领域，这意味着将一门课程的所有概念、原理、技能和实例构建成一个结构化的知识网络。例如，潍坊科技学院提出的“K-L-S-P四元支撑平台”中的“Knowledge”部分，就专注于知识语义建模，旨在实现对知识的深刻理解 [[5]]。这种语义网络的价值在于，它超越了传统的线性教材结构，为学习路径的动态规划提供了依据。研究人员已经开发出多种算法来构建和优化知识图谱，包括基于BiLSTM+CNN-CRF的实体识别算法、基于共现矩阵的职位能力抽取方法等 [[6]]。在具体应用中，知识图谱已被成功应用于多个场景，如构建初中化学知识图谱以实现可视化查询 [[6]]，或是在慕课平台中应用RippleNet算法进行课程内外的路径规划 [[6]]。此外，知识图谱还可用于个性化习题推荐、评分预测和教案评估等多种功能，其有效 性常通过准确率、召回率、F值、RMSE和MAE等指标进行评估 [[6]]。然而，当前研究也普遍承认，知识图谱存在数据规模小、本体设计简单、知识深度不足等问题，这是未来需要攻克的方向 [[6]]。

**学习者画像：刻画个体差异的多维模型**
如果说知识图谱描绘的是“教什么”，那么学习者画像则回答了“给谁教”和“如何教”的问题。学习者画像通过整合多维度的数据，构建一个动态的、立体的数字孪生体，用以描述学习者的独特属性 [[2]]。这些属性不仅包括基本信息和学习行为（如学习时长、完成度），还深入到学习风格、认知能力和心理特征等领域 [[2]]。通过使用K-means聚类、决策树、逻辑回归等机器学习算法，系统能够对海量数据进行分 析，自动为学习者打上标签，形成画像 [[2]]。例如，蔡迪等人提出的“K-L-S-P平台”中的“Learner”部分，就负责感知学习者的行为数据，为后续的个性化推荐提供输入 [[5]]。同样，孙红旭的研究也表明，聚类算法是建立学习者画像的有效手段之一 [[6]]。一个完善的画像体系能够帮助系统精准地锚定学生的认知起点，从而提供真正契合其需求的学习内容和路径 [[5]]。

**双剑合璧：知识图谱与学习者画像的融合创新**
知识图谱与学习者画像的融合，是个性化学习从理论走向实践的关键一步。这种融合不仅仅是简单的数据拼接，而是两套模型的深度协同与互动。蔡迪等人提出的“K-L-S-P四元支撑平台”是这一理念的典型代 表，其核心思想是建立一个包含“知识图谱”、“学习者画像”、“路径推荐系统”和“教学交互平台”的完整生态 [[5]]。在这个生态中，“知识图谱”与“学习者画像”之间形成了双向反馈回路。一方面，系统通过分析学习者画像，了解其当前的知识状态和薄弱环节；另一方面，系统利用知识图谱来理解待学习内容的结构和依赖关系，从而为该学习者规划出一条最优的学习路径。这种融合使得个性化不再是随机的、浅层次的推荐，而是基于对知识结构和个体需求的双重深刻理解。

这种融合体现在具体的算法和流程中。例如，在“三阶段嵌入式优化流程”中，系统在课前通过多源数据诊断来锚定学生的认知起点；课中则基于多模态反馈动态调节路径；课后又通过评估、重构、激励的闭环来激活持续学习 [[5]]。这一系列操作的背后，都是知识图谱与学习者画像协同作用的结果。知识图谱定义了学习的“可能性空间”，而学习者画像则限定了在特定时刻的“可行路径”。只有当两者紧密结合，系统才能真正实现从“静态推荐”向“动态导学”的转变，为每个学习者提供真正意义上的个性化支持。

下表总结了知识图谱与学习者画像在个性化学习中的角色与融合方式：

| 特征 | 知识图谱 (Knowledge Graph) | 学习者画像 (Learner Profile) | 融合创新 (Innovation through Fusion) |
| :--- | :--- | :--- | :--- |
| **核心目标** | 对学科知识进行语义建模，揭示知识点间的逻辑关系 [[6]]。 | 对学习者个体特征进行多维度刻画，包括行为、风格、能力等 [[2]]。 | 将学习者的动态状态与知识的静态结构进行匹配 ，实现精准定位与路径规划。 |
| **主要技术** | 图嵌入技术 (TransE, DistMult等)，知识表示学习 [[6]]。 | 聚类 (K-means)、分类 (决策树)、关联规则、深度学习 [[2]]。 | 结合认知诊断模型 (NeuralCD, EKT) 与知识图谱算法，形成综合推荐模型 [[5,8]]。 |
| **主要应用** | 知识检索、路径规划、资源推荐、能力诊断、习题推荐 [[6]]。 | 锚定认知起点、个性化内容推荐、学习风险预警、分组策略制定 [[3,5]]。 | 动态生成与优化学习路径，实现“学-教-图三维协同” [[5]]。 |
| **面临挑战** | 数据规模小、本体设计简单、知识深度不足 [[6]]。 | 数据隐私安全、画像准确性与时效性、数据稀疏性问题。 | 如何高效、准确地进行图与人的匹配，以及如何应对两者随时间演变带 来的动态性问题。 |

总而言之，知识图谱与学习者画像的深度融合，正在成为推动个性化学习发展的核心技术引擎。它们共同解决了个性化学习的两大核心问题：即“学什么”和“为谁学”，并通过协同机制，将个性化从一种理想状态，转变为可落地、可衡量的教育实践。

## 应用模式演进：从路径推荐到人机协同

人工智能在个性化学习领域的应用，正经历着一场深刻的范式转移。其发展脉络清晰地展示了从最初侧重于自动化的内容推荐，逐步演进到构建更为复杂的人机协同教学环境的过程。这一演进不仅体现在技术的复杂性上，更重要的是体现在对教师角色和教育本质的理解深化上。

**第一阶段：基于算法的个性化路径推荐**
这是人工智能在个性化学习领域的初级应用形态，其核心是利用算法为学生推荐最合适的学习内容和路径。这一阶段的典型代表是许多商业化的自适应学习平台，如国内的松鼠AI和国外的Knewton模式 [[4]] 。这些系统主要通过分析学生的历史答题数据、响应时间和错误模式，来判断其对特定知识点的掌握程度 [[3]]。然后，基于此判断，系统会从庞大的资源库中筛选出难度适宜、类型匹配的学习材料，如一篇阅读文章、一套数学练习题或一段解释视频 [[7]]。科大讯飞的“智学网”系统便是这一模式的杰出实践，它利用深度强化学习框架DRE来优化复习与探索、难度平滑性、参与度等多个目标，实现多轮交互式的 自适应推荐 [[8]]。这种模式极大地提升了学习的针对性和效率，但其局限性也显而易见：它将教师的角色边缘化，学习过程高度依赖算法，可能忽视了情感、动机和社交等非认知因素的影响。

**第二阶段：从推荐到动态导学的范式升级**
随着技术的发展，个性化学习的应用模式开始超越简单的路径推荐，进入一个更为动态和智能的阶段。这一阶段的标志性特征是引入了更深层次的认知诊断和动态路径调整机制。蔡迪等人提出的“K-L-S-P四元支撑平台”及其“三阶段嵌入式优化流程”是这一阶段的典型例证 [[5]]。该模型不再仅仅根据静态的答题记录推荐内容，而是将学生的实时行为（如鼠标移动、作答时长）作为驱动信号，结合教师的策略干预 作为校准参数，让知识图谱的依赖关系作为约束条件，从而推动路径从静态推荐向动态导学转变 [[5]]。科大讯飞在其自适应学习系统中也采用了类似的思路，提出了EKT（exercise-aware knowledge tracing）和EKPT（exercise-correlated knowledge proficiency tracing）等动态知识追踪模型，这些模型能够融合题目语义、知识共性与记忆/遗忘曲线理论，实现对认知状态的时序建模 [[8]]。这种从“推荐” 到“导学”的转变，意味着系统开始扮演一个更加积极的“导师”角色，而非被动的“工具”。

**第三阶段：迈向人机协同的教学新生态**
这是个性化学习应用的最高级形态，其核心理念是将AI视为教师的强大助手，而非替代品。在这种模式下，AI系统承担起繁重的数据分析、内容准备和初步反馈工作，从而将教师解放出来，使其能专注于更高阶的教学活动 [[3]]。朱永新先生提出，教师应成为“守望者”，引导学生自主发展，而非包办一切 [[4]]。MIT开发的STEAM课程中包含的算法偏见实验，正是培养下一代具备伦理判断力的尝试，这本身就需要教师的引导和启发 [[4]]。在这种人机协同的新生态中，AI系统扮演着多重角色：
1.  **数据分析师与诊断师：** 自动分析海量学习行为数据，精准诊断学生的学习障碍和知识薄弱点，并以可视化的仪表板形式呈现给教师，使教师的干预更具针对性 [[7]]。
2.  **个性化资源生成器：** 根据诊断结果，自动生成差异化、个性化的学习材料，如定制化的阅读材料、数学练习路径、写作语法建议等，减轻教师备课负担 [[7]]。
3.  **即时反馈与辅导师：** 在学生遇到困难时，提供即时的、个性化的辅导和反馈，引导学生进行自我修正，形成闭环学习 [[3]]。
4.  **学习伙伴与社群构建者：** 基于相似的知识水平和认知风格，智能匹配学习伙伴或组建学习小组，促进同伴互助学习 [[8]]。

未来的个性化学习，必然是这样一个人机协同的生态系统。教师的角色将从“知识的传授者”转变为“学习的设计者、引导者和支持者”。他们将利用AI提供的洞察力，设计出更具吸引力和挑战性的学习体验，并专注于培养学生的批判性思维、创造力、协作能力和解决复杂问题的能力等高阶素养，这些都是当前技术无法企及的领域。因此，对教师的培训和技术投入，将是实现这一愿景的关键保障 [[7]]。

## 典型案例剖析：国内外代表性平台与实践

要深入理解人工智能个性化学习的实际应用效果与发展趋势，剖析国内外具有代表性的平台与实践案例至关重要。这些案例不仅展示了技术的成熟度，也反映了不同市场环境下的应用场景和商业模式。其中，中国的科大讯飞“智学网”系统和国际上的Knewton平台是两个极具影响力的标杆。

**科大讯飞“智学网”：中国规模化应用的典范**
科大讯飞股份有限公司凭借其在语音和人工智能领域的深厚积累，成功地将自适应学习技术商业化，并在中国教育市场取得了巨大成功。截至2020年7月，“智学网”已在全国超过16,000所学校推广，惠及约2,500万师生，并每月组织数千场联考，提供数万场测试服务和800万份评价报告 [[8]]。这一惊人的数据充分证明了其产品在中国市场的广泛接受度和强大的生命力。

“智学网”之所以能取得如此成就，源于其背后强大的技术研发实力。针对自适应学习中的三大难题——教学资源表示困难、学习状态诊断困难和学习策略设计困难，科大讯飞提出了一系列创新性的解决方案 [[8]]。
*   **教学资源表示：** 提出了QuesNet框架，通过无监督预训练实现了对文本、图像等多源异构试题的统一表征，在知识点预测任务中准确率较现有方法提升了近10% [[8]]。
*   **学习状态诊断：** 提出了NeuralCD通用框架，基于深度学习建模“学习者−知识−资源”的高阶交互；并开发了EKT、EKPT等动态知识追踪模型，能够融合题目语义和记忆遗忘曲线，实现对认知状态的精准时序建模 [[8]]。
*   **学习策略设计：** 提出了DRE（deep reinforcement exercise recommendation）框架，基于深度强化学习，同时优化复习与探索、难度平滑性、参与度等多个目标，实现多轮交互式自适应推荐；并有CSEAL框架用于生成符合知识结构的学习路径 [[8]]。

“智学网”的技术架构体现了前述的数据层、技术层和应用层三层模型，实现了伴随式数据采集、大规模资源库建设和精准推荐服务的有机结合 [[8]]。它的成功，尤其体现在其能够与中国的考试文化和社会需求紧密结合。通过大规模的联考和精准的评价报告，它不仅服务于学生的个性化学习，也为学校和教育管理部门提供了重要的教学质量监控和决策支持工具，形成了一个良性的生态闭环。

**Knewton：美国个性化学习的先行者**
Knewton是全球范围内最早也是最具影响力的一批自适应学习公司之一，其模式深刻地影响了全球在线教育行业。虽然具体的技术细节和市场份额数据不如科大讯飞公开，但从相关文献中可以看出，Knewton代表了个性化学习的一种典型应用模式 [[4]]。

Knewton的核心价值主张是通过强大的算法引擎，为每一位学生提供独一无二的学习路径。其系统会持续不断地分析学生的学习行为，包括答题的正确率、用时长短、点击模式等，以此来动态评估学生对各个 知识点的掌握程度 [[3]]。基于这种评估，系统会实时调整后续学习内容的难度和类型，确保学生始终处于一个“最近发展区”，既能挑战自己，又不至于感到挫败。这种模式强调学习过程的高度个性化和智能化，是“个性化路径推荐”阶段的典型代表 [[4]]。

Knewton的成功之处在于其技术的普适性和开放性。它不仅仅是一个面向终端用户的产品，更是一个强大的API平台，可以集成到各种第三方教育内容平台和学校管理系统中。这种B2B2C的模式使其能够快速触 达海量用户，同时也促进了整个教育行业的技术升级。Knewton的出现，向世界证明了利用人工智能实现大规模个性化教育的可能性，激发了全球范围内大量的模仿和创新。

**其他典型案例与趋势**
除了这两个巨头，市场上还有许多其他值得关注的案例。例如，潍坊科技学院计算机学院团队研发的“K-L-S-P四元支撑平台”，更侧重于学术研究与高校教学场景的深度融合，其“学-教-图三维协同机制”和“三阶段嵌入式优化流程”展现了对个性化学习过程更精细化的控制思路 [[5]]。而在基础教育领域，诸如ONLYOFFICE这类办公软件平台也开始集成AI插件，提供实时协作和智能反馈，将个性化学习的理念延伸到 了日常课堂互动之中 [[7]]。

通过对这些案例的分析，我们可以看到，无论是科大讯飞的规模化落地，还是Knewton的算法引领，抑或是学术界的精巧设计，它们都共同指向了一个方向：个性化学习正在从概念走向现实。未来的发展趋势 将是更加注重人机协同、更加关注学习过程的动态性和沉浸感，并且更加深入地与各学科的教学实践相结合。

## 效果评估与挑战：量化成效与现实困境

尽管人工智能个性化学习展现出巨大的潜力，但在实际推广和应用中，其效果评估和面临的挑战同样不容忽视。科学地评估其成效，并清醒地认识其所处的现实困境，对于推动该领域的健康发展至关重要。

**效果评估：从技术指标到教育成果**
对个性化学习系统的评估是一个多层次、多维度的过程。最初级的评估往往集中在技术性能上，例如，知识追踪模型的预测准确性、推荐算法的准确率和召回率，或者在能力诊断任务中F1指标的提升 [[1,8]]。科大讯飞的QuesNet框架在知识点预测任务中准确率提升了近10%，TACNN框架在英语阅读理解试题难度评估中皮尔逊相关系数平均提升约10% [[8]]。在能力诊断方面，其NeuralCD通用框架的误差相较传统方法相对降低了约5% [[8]]。在课程设计中，陈曦等人构建的课程知识图谱，通过将知识相似度集成至协同过滤框架进行成绩预测，使用RMSE与MAE指标进行了验证 [[6]]。

然而，单纯的技术指标并不能完全反映个性化学习的最终价值。更深层次的评估需要转向对学生学习成果和体验的考察。这包括：
*   **学业表现提升：** 这是最直接的成效指标。例如，通过对比实验组和对照组学生的期末考试成绩、作业完成质量等，来衡量个性化学习系统是否有效促进了知识掌握。
*   **学习动机与投入度：** 个性化学习旨在提高学生的参与感和兴趣。可以通过问卷调查、观察记录等方式，评估学生在使用系统后的学习动机、自信心和满意度的变化。
*   **学习效率与路径优化：** 评估学生在完成同等学习任务时所花费的时间、经历的试错次数等，可以间接反映个性化路径的优化程度。
*   **教师反馈与采纳度：** 教师是系统的重要使用者。他们的反馈，包括对系统易用性、辅助效果和数据可靠性的评价，是衡量系统实用性和可持续性的关键指标。

教育部发布的《中国智慧教育蓝皮书(2022)》强调推进教育数字化，这本身就包含了对成效评估的要求 [[4]]。未来，评估体系将更加注重长期效应和综合效益，而不仅仅是短期的技术性能提升。

**现实挑战：隐私、偏见与实施成本**
尽管前景广阔，但个性化学习的普及面临着一系列严峻的现实挑战。
*   **数据隐私与安全：** 这是所有数据驱动型AI应用面临的首要挑战。个性化学习系统需要采集大量敏感的学生个人数据，包括学习行为、生理特征甚至家庭背景 [[2,3]]。如何确保这些数据的安全存储 、合法使用和合规共享，避免泄露和滥用，是一个巨大的法律和伦理难题。特别是当数据涉及未成年人时，相关的法规（如GDPR）提出了更高的要求 [[7]]。
*   **算法偏见：** AI算法并非天生公正。如果训练数据存在偏差，或者算法设计本身存在问题，就可能导致系统对某些群体产生歧视性判断。亚马逊公司的面部识别技术曾被指存在种族歧视，就是一个典 型的警示 [[4]]。在教育场景中，算法偏见可能会低估少数族裔、贫困学生或有特殊需求学生的真实潜力，从而加剧教育不平等。应对措施包括采用IIFR算法提升个体公平率，参考欧盟《可信赖人工智能伦理准则》建立算法影响评估体系等 [[4]]。
*   **过度依赖技术的风险：** 如果缺乏有效的监管和引导，学生可能会过度依赖AI系统，导致独立思考、批判性思维和解决问题能力的退化。此外，系统也可能因为算法的局限性，无法完全理解人类复杂 的认知过程和情感状态，从而给出不恰当的建议。
*   **实施成本与教师培训：** 开发和维护一个先进的个性化学习系统需要高昂的资金投入。对于许多教育资源本就有限的地区和学校而言，这是一个沉重的负担。此外，教师需要接受系统性的培训，才能 有效利用这些新技术，但这又是一笔额外的成本。教师培训的需求是当前的主要挑战之一 [[7]]。
*   **数据质量和模型泛化能力：** 许多研究仍然受限于小规模、单一来源的数据集，导致模型的泛化能力不足 [[6]]。如何获取高质量、大规模、多样化的数据，并构建能够适应不同学科、不同文化背景 的学习模型，是技术层面亟待解决的问题。

下表详细梳理了个性化学习面临的主要挑战及其潜在对策：

| 挑战类别 | 具体问题描述 | 潜在对策与解决方案 | 相关文献佐证 |
| :--- | :--- | :--- | :--- |
| **隐私与安全** | 大量敏感学生数据的采集、存储和使用面临泄露风险，需遵守GDPR等法规。 | 加强数据加密与访问控制，推行匿名化处理，建立透明的数据使用政策。 | [[3,7]] |
| **算法偏见** | 训练数据或算法本身存在的偏见可能导致对特定群体的不公平评估和推荐。 | 采用公平性增强算法（如IIFR），建立算法影响评估体系，确保数据多样性。 | [[4]] |
| **技术依赖与伦理** | 学生过度依赖技术，独立思考能力受损；系统可能因算法局限性给出不当建议。 | 强调人机协同，明确教师主导地位；在课程中融入AI伦理教育，培养批判性思维。 | [[3,4]] |
| **实施成本与师资** | 系统开发与维护成本高昂；教师缺乏必要的技术培训和教学法支持。 | 政府加大投入，鼓励开源项目；建立系统性的教师培训体系，聚焦高阶素养培养。 | [[7]] |
| **数据与模型** | 数据规模小、本体设计简单、知识深度不足；模型泛化能力弱，难以适应不同场景。 | 推动跨机构合作，共建大规模、多样化、高质量的教育数据库；加强基础理论研究。 | [[6]] |

综上所述，个性化学习的效果是客观存在的，但其评估需要超越单一的技术指标，关注长期的教育成果。同时，我们必须清醒地认识到，隐私、偏见、成本和教师角色等一系列挑战是其通往普及之路必须克服的障碍。只有在技术、伦理和教育实践之间找到平衡点，个性化学习才能真正释放其改变教育的巨大潜力。

## 未来展望：伦理治理与人机协同的深化

展望未来，人工智能在个性化学习领域的应用将朝着更加成熟、负责任和人性化的方向发展。这一进程将围绕两大核心主题展开：一是建立健全的伦理治理体系，确保技术向善；二是进一步深化人机协同，重塑教师角色与教育价值。

**构建“立法+技术+教育”三位一体的伦理治理体系**
随着AI在教育中应用的日益深入，伦理问题已成为制约其健康发展的关键瓶颈。未来的治理模式将不再是单一维度的，而是形成一个由立法规范、技术保障和伦理教育共同构成的立体化治理体系。
首先，**立法与监管**将成为底线保障。各国政府和国际组织需要加快制定和完善相关法律法规，明确AI教育产品的数据采集边界、使用权限和问责机制。这些法规不仅要保护学生隐私，更要防止算法偏见对教育公平造成实质性损害。欧盟的《可信赖人工智能伦理准则》为此提供了重要的参考框架 [[4]]。同时，建立常态化的算法影响评估体系，定期审查AI系统对教育生态的潜在影响，将是确保技术负责任应用的必要手段 [[4]]。
其次，**技术创新**将在伦理治理中扮演主动角色。研究者们正致力于开发更具公平性和可解释性的算法。例如，采用IIFR（Individual Individual Fairness Rate）等算法来提升个体层面的公平性 [[4]] 。此外，开发可解释性AI（Explainable AI, XAI）技术，让系统不仅能给出推荐结果，还能清晰地阐述其背后的推理过程，这将有助于教师和学生理解并信任AI的建议，减少“黑箱”操作带来的疑虑 [[1]]。
最后，**伦理教育**将成为内生驱动力。未来的教育不仅要教授学生如何使用AI，更要教会他们如何与AI共存，如何批判性地审视AI的输出。MIT开发的通过算法偏见实验来培养伦理判断力的STEAM课程，为我们提供了一个极佳的范本 [[4]]。在国内，一些高校已经开始探索设置48课时的人工智能伦理模块，采用场景化案例教学法，让学生在真实情境中思考AI的伦理边界 [[4]]。这种教育的普及，将从根本上塑造一个对AI技术既有敬畏之心又有驾驭之能的社会。

**深化人机协同，重塑教师角色与教育价值**
未来个性化学习的核心，绝非是让机器取代教师，而是构建一个人机协同的全新教学生态。在这个生态中，教师的角色将发生根本性的转变，教育的价值也将得到重新定义。
教师将从传统的“知识传授者”转变为“学习设计师”、“成长引导者”和“情感支持者”。AI系统将接管大部分重复性、程序化的劳动，如知识点诊断、个性化资源推荐和初步的答疑解惑 [[3]]。这将为教师节省大量时间，使其能够专注于那些机器无法替代的工作。他们会利用AI提供的数据分析报告，为学生设计更具挑战性和创造性的学习项目；他们会引导学生进行深度探究和批判性思考；他们会在学生遇到挫折时给予情感支持和鼓励；他们还会组织学生进行协作学习，培养其沟通与合作能力。
这种转变对教师的专业素养提出了新的要求。未来的教师需要具备更强的数据素养，能够理解和解读AI系统的分析报告；他们还需要掌握混合式教学的设计与实施能力，能够巧妙地将线上AI工具与线下面对面教学结合起来。因此，大规模、系统性的教师培训计划将是实现这一转型的关键保障 [[7]]。
最终，个性化学习的终极目标是培养出能够适应未来社会变化、具备终身学习能力的个体。AI技术在此过程中扮演了“赋能者”的角色，它通过提供无限的资源、即时的反馈和精准的指导，极大地拓展了教育的可能性。然而，真正的教育智慧、人文关怀和价值观的塑造，仍将根植于人类教师的心灵与经验之中。正如朱永新先生所言，教师应成为“守望者”，在学生自主发展的道路上，给予适时的守护与指引 [[4]]。这，或许就是技术与人文在教育领域最完美的结合点。
研究状态更新：finished

Usage: in(721)/out(7505)/total(0)
 */

```


## 多模态

使用 `dashScopeClient.GetMultimodalGenerationAsync` 和 `dashScopeClient.GetMultimodalGenerationStreamAsync` 来访问多模态文本生成接口。

相关文档：[多模态_大模型服务平台百炼(Model Studio)-阿里云帮助中心](https://help.aliyun.com/zh/model-studio/multimodal)

### 视觉理解/推理

使用 `MultimodalMessage.User()` 可以快速创建对应角色的消息。

媒体内容可以通过公网 URL 或者 `byte[]` 传入。

您也可以通过 `UploadTemporaryFileAsync` 方法上传临时文件获取 `oss://` 开头的链接。

```csharp
// 上传本地文件
await using var lenna = File.OpenRead("Lenna.jpg");
string ossLink = await client.UploadTemporaryFileAsync("qwen3-vl-plus", lenna, "lenna.jpg");
Console.WriteLine($"File uploaded: {ossLink}");

// 使用链接
var messages = new List<MultimodalMessage>();
messages.Add(
    MultimodalMessage.User(
    [
        MultimodalMessageContent.ImageContent(ossLink),
        MultimodalMessageContent.TextContent("她是谁？")
    ]));
```

您可以通过参数 `EnableThinking` 控制是否开启推理（需要模型支持）。

参数 `VlHighResolutionImages` 控制模型读取模型的精度，开启后会增加图片/视频的 Token 使用量。

以下是完整示例：

```csharp
await using var lenna = File.OpenRead("Lenna.jpg");
var ossLink = await client.UploadTemporaryFileAsync("qwen3-vl-plus", lenna, "lenna.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages = new List<MultimodalMessage>();
messages.Add(
    MultimodalMessage.User(
    [
        MultimodalMessageContent.ImageContent(ossLink),
        MultimodalMessageContent.TextContent("她是谁？")
    ]));
var completion = client.GetMultimodalGenerationStreamAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen3-vl-plus",
        Input = new MultimodalInput() { Messages = messages },
        Parameters = new MultimodalParameters()
        {
            IncrementalOutput = true,
            EnableThinking = true,
            VlHighResolutionImages = true
        }
    });
var reply = new StringBuilder();
var reasoning = false;
MultimodalTokenUsage? usage = null;
await foreach (var chunk in completion)
{
    var choice = chunk.Output.Choices[0];
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
        $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
}

/*
Reasoning > 用户现在需要识别图中的人物。这张照片里的女性是Nancy Sinatra（南希·辛纳特拉），她是美国60年代著名的歌手、演员，也是Frank Sinatra的女儿。她的标志性风格包括复古装扮和独特的 音乐风格，这张照片的造型（宽檐帽、羽毛装饰）符合她那个时期的时尚风格。需要确认信息准确性，Nancy Sinatra在60年代的影像资料中常见这样的复古造型，所以判断是她。

Assistant > 图中人物是**南希·辛纳特拉（Nancy Sinatra）**，她是美国20世纪60年代著名的歌手、演员，也是传奇歌手弗兰克·辛纳特拉（Frank Sinatra）的女儿。她以独特的复古风格、音乐作品（如经典歌曲 *These Boots Are Made for Walkin’* ）和影视表现闻名，这张照片的造型（宽檐帽搭配羽毛装饰等）也契合她标志性的时尚风格。
Usage: in(271)/out(199)/image(258)/reasoning(98)/total(470)
 */
```

#### 传入视频文件

VL 系列模型支持传入视频文件，通过 Video 参数传入视频文件链接或者图片序列均可。

传入视频文件时，您可以通过 `fps` 参数来控制模型应当隔多少秒（1/fps 秒）抽取一帧作为输入。

```csharp
// 使用本地文件需要提前上传
await using var video = File.OpenRead("sample.mp4");
var ossLink = await client.UploadTemporaryFileAsync("qwen3-vl-plus", video, "sample.mp4");
Console.WriteLine($"File uploaded: {ossLink}");

var messages = new List<MultimodalMessage>();
messages.Add(
    MultimodalMessage.User(
    [
        MultimodalMessageContent.VideoContent(ossLink, fps: 2),
        // MultimodalMessageContent.VideoFrames(links),
        MultimodalMessageContent.TextContent("这段视频的内容是什么？")
    ]));
```

### 文字提取

使用 `qwen-vl-ocr` 系列模型可以很好的完成文字提取任务，基础用法：

```csharp
// upload file
await using var tilted = File.OpenRead("tilted.png");
var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", tilted, "tilted.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages = new List<MultimodalMessage>();
messages.Add(
    MultimodalMessage.User(
    [
		// 如果你的图片存在偏斜，可尝试将 enableRotate 设置为 true
        // 除了本地上传外，您也可以直接传入公网 URL
        MultimodalMessageContent.ImageContent(ossLink, enableRotate: true),
    ]));
var completion = client.GetMultimodalGenerationStreamAsync(
    new ModelRequest<MultimodalInput, IMultimodalParameters>()
    {
        Model = "qwen-vl-ocr-latest",
        Input = new MultimodalInput { Messages = messages },
        Parameters = new MultimodalParameters
        {
            IncrementalOutput = true,
        }
    });
```

完整示例：

```csharp
// upload file
await using var tilted = File.OpenRead("tilted.png");
var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", tilted, "tilted.jpg");
Console.WriteLine($"File uploaded: {ossLink}");
var messages = new List<MultimodalMessage>();
messages.Add(
    MultimodalMessage.User(
    [
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

/*
File uploaded: oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-11-28/435ea45f-9942-4fd4-983a-9ea8a3cd5ecb/tilted.jpg
产品介绍
本品采用韩国进口纤维丝制造，不缩水、不变形、不发霉、
不生菌、不伤物品表面。具有真正的不粘油、吸水力强、耐水
浸、清洗干净、无毒、无残留、易晾干等特点。
店家使用经验：不锈钢、陶瓷制品、浴盆、整体浴室大部分是
白色的光洁表面，用其他的抹布擦洗表面污渍不易洗掉，太尖
的容易划出划痕。使用这个仿真丝瓜布，沾少量中性洗涤剂揉
出泡沫，很容易把这些表面污渍擦洗干净。
6941990612023
货号：2023
Usage: in(2434)/out(155)/image(2410)/total(2589)
*/
```

#### 调用内置任务

调用内置任务时，不建议开启流式增量传输。（将不会提供 `OcrResult` 里的额外内容，只能从文字内容中手动读取）

##### 高精识别

使用内置任务时，不建议开启流式传输，否则 `completion.Output.Choices[0].Message.Content[0].OcrResult.WordsInfo` 将为 `null`。

除了常规的返回文字内容外，该任务还会返回文字的坐标。

设置 `Parameters.OcrOptions.Task` 为 `advanced_recognition` 即可调用该内置任务，不需要传入额外的 Prompt。

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

任务返回的文字是一个 JSON 代码块，包含文本坐标和文本内容。您可以使用 `completion.Output.Choices[0].Message.Content[0].OcrResult.WordsInfo` 直接访问结果，不需要手动反序列化模型返回的代码块。

示例：

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

输出结果：

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

##### 信息抽取

使用内置任务时，不建议开启流式传输，否则 `completion.Output.Choices[0].Message.Content[0].OcrResult.KvResult` 将为 `null`。

设置 `Parameters.OcrOptions.Task` 为 `key_information_extraction` 即可调用该内置任务，不需要传入额外的文字信息。

通过 `Parameters.OcrOptions.TaskConfig.ResultSchema` 可以自定义输出的 JSON 格式（至多 3 层嵌套），留空则默认输出全部字段。

例如我们希望从图片中抽取如下类型的对象（JSON 属性名尽可能采用图片上存在的文字）：

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

当模型识别失败时，对应字段将被设置为 `null`，需要确保代码里能够正确处理这种情况。

示例请求：

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

返回的 `KvResult` 是一个 `JsonElement`，可以直接反序列化到指定的类型，或者直接转换为 `Dictionary<string, object?>?`。

使用示例：

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

##### 表格解析

设置 `Parameters.OcrOptions.Task` 为 `table_parsing` 即可调用该内置任务，不需要传入额外的文字信息。

该任务会识读图片中的表格并返回 HTML 格式的表格。

示例：

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

返回的内容（注意最外层会包含一个 markdown 代码块标记）：

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

##### 文档解析

设置 `Parameters.OcrOptions.Task` 为 `document_parsing` 即可调用该内置任务，不需要传入额外的文字信息。

该任务会识读图片（例如扫描版 PDF）并返回 LaTeX 格式的文档。

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

示例返回（注意最外层会有一个 markdown 代码块）：

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

##### 公式识别

设置 `Parameters.OcrOptions.Task` 为 `formula_recognition` 即可调用该内置任务，不需要传入额外的文字信息。

该任务会识读图片中的公式（例如手写数学公式）并以 LaTeX 形式返回。

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

返回结果

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

##### 通用文本识别

设置 `Parameters.OcrOptions.Task` 为 `text_recognition` 即可调用该内置任务，不需要传入额外的文字信息。

该任务会识读图片中文字（中英文）并以纯文本形式返回。

示例请求：

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

示例返回

```plaintext
OpenAI 兼容 DashScope
Python Java curl
```



## 语音合成

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

## 图像生成

### 文生图

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

## 文本向量

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
