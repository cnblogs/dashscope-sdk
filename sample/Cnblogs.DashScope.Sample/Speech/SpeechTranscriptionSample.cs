using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sample;

namespace Cnblogs.DashScope.Sample.Speech;

public class SpeechTranscriptionSample : SpeechSample
{
    /// <inheritdoc />
    public override string Description => "Non-realtime speech transcription (Paraformer / Fun-ASR filetrans)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.Write("Audio URL (public HTTP/HTTPS) > ");
        var audioUrl = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(audioUrl))
        {
            audioUrl = "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/audio/paraformer/hello_world_female2.wav";
            Console.WriteLine($"Using default: {audioUrl}");
        }

        var submit = await client.CreateSpeechTranscriptionTaskAsync(
            new ModelRequest<SpeechTranscriptionInput, ISpeechTranscriptionParameters>
            {
                Model = "paraformer-v2",
                Input = new SpeechTranscriptionInput { FileUrls = new[] { audioUrl } },
                Parameters = new SpeechTranscriptionParameters
                {
                    ChannelId = new[] { 0 },
                    LanguageHints = new[] { "zh", "en" }
                }
            });

        Console.WriteLine($"Task submitted: {submit.Output.TaskId} ({submit.Output.TaskStatus})");

        DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage> task;
        do
        {
            await Task.Delay(1000);
            task = await client.GetSpeechTranscriptionTaskAsync(submit.Output.TaskId);
            Console.WriteLine($"Status: {task.Output.TaskStatus}");
        }
        while (task.Output.TaskStatus is DashScopeTaskStatus.Pending or DashScopeTaskStatus.Running);

        var result = task.Output.Results?.FirstOrDefault();
        if (result?.TranscriptionUrl is null)
        {
            Console.WriteLine($"Failed: {result?.Code} {result?.Message}");
            return;
        }

        var transcription = await client.GetSpeechTranscriptionResultAsync(result.TranscriptionUrl);
        Console.WriteLine($"Text: {transcription.Transcripts?.FirstOrDefault()?.Text}");
        if (task.Usage != null)
        {
            Console.WriteLine($"Usage duration: {task.Usage.Duration}s");
        }
    }
}
