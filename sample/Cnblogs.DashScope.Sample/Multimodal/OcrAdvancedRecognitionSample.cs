using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class OcrAdvancedRecognitionSample : ISample
{
    /// <inheritdoc />
    public string Description => "OCR Advanced Recognition Task Sample";

    /// <inheritdoc />
    public async Task RunAsync(IDashScopeClient client)
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
                Input = new MultimodalInput() { Messages = messages },
                Parameters = new MultimodalParameters()
                {
                    OcrOptions = new MultimodalOcrOptions() { Task = "advanced_recognition" }
                }
            });

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

        if (completion.Usage != null)
        {
            var usage = completion.Usage;
            Console.WriteLine(
                $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/total({usage.TotalTokens})");
        }
    }
}

/*
File uploaded: oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-11-29/90f86409-6868-4e34-83e1-efce3c72477c/webpage.jpg
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

Usage: in(175)/out(186)/image(142)/total(361)
 */
