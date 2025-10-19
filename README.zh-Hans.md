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

- [文本生成](#文本生成) - QWen3, DeepSeek 等，支持推理/工具调用/网络搜索/翻译等场景
- [多模态](#多模态) - QWen-VL，QVQ 等，支持推理/视觉理解/OCR/音频理解等场景
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

通过返回结果里的 `response.Output.SearchInfo` 来获取搜索结果，这个值会在模型搜索后一次性返回，并在之后的每次返回中都附带。因此，开启增量流式输出时，不需要通过 `StringBuilder` 等方式来缓存 `SearchInfo`。

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

通过 `Parameter` 里的 `Tools` 来向模型提供可用的工具列表，模型会返回 `Tool` 角色的消息来调用工具。



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
