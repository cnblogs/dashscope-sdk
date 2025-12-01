using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class OcrMultilanguageSample: MultimodalSample
{
    /// <inheritdoc />
    public override string Description => "OCR Text Recognition(Multilanguage) Sample";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // upload file
        await using var file = File.OpenRead("multilanguage.jpg");
        var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", file, "multilanguage.jpg");
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
                        Task = "multi_lan",
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
File uploaded: oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-11-29/fa37bfc1-946b-4347-9c0a-fba6abccc43f/multilanguage.jpg
Text:
INTERNATIONAL
MOTHER LANGUAGE
DAY
你好!
Прив?т!
Bonjour!
Merhaba!
Ciao!
Hello!
Ola!
????
Salam!
Usage: in(4444)/out(36)/image(4420)/total(4480)
 */
