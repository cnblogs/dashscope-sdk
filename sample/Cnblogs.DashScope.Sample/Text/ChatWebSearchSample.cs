using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text;

public class ChatWebSearchSample: TextSample
{
    /// <inheritdoc />
    public override string Description => "Chat with web search enabled";

    /// <inheritdoc />
    public async override Task RunAsync(IDashScopeClient client)
    {
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
                            ForcedSearch = true,
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
