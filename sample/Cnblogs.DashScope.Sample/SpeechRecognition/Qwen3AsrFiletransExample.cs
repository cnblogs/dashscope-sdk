using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechRecognition;

public class Qwen3AsrFiletransExample : SpeechRecognitionSample
{
    /// <inheritdoc />
    public override string Description => "Asynchronous transcription of a long audio file with qwen3-asr-flash-filetrans (submit and poll)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Submitting transcription task...");
        var task = await client.CreateSpeechTranscriptionTaskAsync(
            new ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>()
            {
                Model = "qwen3-asr-flash-filetrans",
                Input = new SpeechTranscriptionInput()
                {
                    FileUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3"
                },
                Parameters = new SpeechTranscriptionParameters()
                {
                    Language = "zh",
                    // word level timestamps instead of sentence level ones
                    EnableWords = true
                }
            });

        var taskId = task.Output.TaskId;
        Console.WriteLine($"Task submitted: {taskId}");

        // poll until the task leaves the queue / finishes
        DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>? result;
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            result = await client.GetSpeechTranscriptionTaskAsync(taskId);
            Console.WriteLine($"Task status: {result.Output.TaskStatus}");
            if (result.Output.TaskStatus is not (DashScopeTaskStatus.Pending or DashScopeTaskStatus.Running))
            {
                break;
            }
        }

        Console.WriteLine($"Duration: {result.Usage?.Seconds}s");
        Console.WriteLine($"Transcription url (valid for 24 hours): {result.Output.Result?.TranscriptionUrl}");
    }
}
