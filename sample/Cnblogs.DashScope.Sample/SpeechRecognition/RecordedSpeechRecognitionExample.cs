using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechRecognition;

public class RecordedSpeechRecognitionExample : SpeechRecognitionSample
{
    /// <inheritdoc />
    public override string Description => "Recorded speech recognition with qwen-audio-3.1-asr-flash (instant hot words and word timestamps)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Recognizing...");
        var response = await client.GetMultimodalGenerationAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "qwen-audio-3.1-asr-flash",
                Input = new MultimodalInput()
                {
                    Messages =
                    [
                        MultimodalMessage.User(
                        [
                            MultimodalMessageContent.InputAudioContent(
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3")
                        ])
                    ]
                },
                Parameters = new MultimodalParameters()
                {
                    Format = "mp3",
                    // instant hot words, the weight is 1~5 or 50
                    Vocabulary = new Dictionary<string, int> { ["青海"] = 5, ["楼兰"] = 5 },
                    // speaker diarization (qwen-audio-3.1-asr-flash only), returns Output.Sentences with SpeakerId
                    // SpeakerDiarizationEnabled = true
                    // keep dialect expressions instead of transcribing them into Mandarin
                    // KeepDialect = true
                }
            });

        var sentence = response.Output.Sentence!;
        Console.WriteLine($"Text: {response.Output.Text}");
        Console.WriteLine($"Sentence {sentence.SentenceId}: {sentence.BeginTime}ms ~ {sentence.EndTime}ms");
        foreach (var word in sentence.Words!)
        {
            Console.WriteLine($"  {word.Text} [{word.BeginTime}ms ~ {word.EndTime}ms] {word.Punctuation}");
        }

        Console.WriteLine($"Duration: {response.Usage?.Duration}s, Tokens: {response.Usage?.TotalTokens}");
    }
}
