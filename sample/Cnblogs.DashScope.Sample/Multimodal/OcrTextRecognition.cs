using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class OcrTextRecognition: MultimodalSample
{
    /// <inheritdoc />
    public override string Description => "OCR Text Recognition(Chinese and English) Sample";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
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

        Console.WriteLine("Text:");
        Console.WriteLine(completion.Output.Choices[0].Message.Content[0].Text);

        if (completion.Usage != null)
        {
            var usage = completion.Usage;
            Console.WriteLine(
                $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/total({usage.TotalTokens})");
        }
    }
}

/*
File uploaded: oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-11-29/fb524efd-e1a1-49d5-9d38-d69d09f5f5f0/webpage.jpg
Text:
OpenAI 兼容 DashScope
Python Java curl
Usage: in(154)/out(12)/image(130)/total(166)
 */
