using System.Text.Json;
using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class OcrKeyInformationExtractionSample : MultimodalSample
{
    /// <inheritdoc />
    public override string Description => "OCR Key Information Extraction Sample";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // upload file
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

        Console.WriteLine("Text:");
        Console.WriteLine(completion.Output.Choices[0].Message.Content[0].Text);
        Console.WriteLine("KvResults:");
        var model = completion.Output.Choices[0].Message.Content[0].OcrResult!.KvResult?.Deserialize<ReceiptModel>();
        Console.WriteLine($"Date: {model?.Date}");
        Console.WriteLine($"Code: {model?.Serials?.Code}");
        Console.WriteLine($"Serial: {model?.Serials?.Serial}");

        if (completion.Usage != null)
        {
            var usage = completion.Usage;
            Console.WriteLine(
                $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/total({usage.TotalTokens})");
        }
    }
}

internal class ReceiptModel
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


/*
File uploaded: oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-11-29/16a422bd-811b-435a-9e2d-8538784dc64d/receipt.jpg
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
