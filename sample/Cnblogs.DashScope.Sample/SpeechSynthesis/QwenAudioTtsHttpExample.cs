using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechSynthesis;

public class QwenAudioTtsHttpExample : SpeechSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "HTTP text to speech with qwen-audio-3.0-tts-flash (workspace domain)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // The HTTP API of qwen-audio-tts models is only served on the workspace domain,
        // it needs its own client configured with the workspace id.
        Console.WriteLine("Reading workspace id from environment variable DASHSCOPE_WORKSPACE_ID");
        var workspaceId = Environment.GetEnvironmentVariable("DASHSCOPE_WORKSPACE_ID", EnvironmentVariableTarget.Process)
                          ?? Environment.GetEnvironmentVariable("DASHSCOPE_WORKSPACE_ID", EnvironmentVariableTarget.User);
        if (string.IsNullOrEmpty(workspaceId))
        {
            Console.Write("WorkspaceId > ");
            workspaceId = Console.ReadLine();
        }

        Console.WriteLine("Reading key from environment variable DASHSCOPE_KEY");
        var apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_KEY", EnvironmentVariableTarget.Process)
                     ?? Environment.GetEnvironmentVariable("DASHSCOPE_KEY", EnvironmentVariableTarget.User);
        if (string.IsNullOrEmpty(apiKey))
        {
            Console.Write("ApiKey > ");
            apiKey = Console.ReadLine();
        }

        var workspaceClient = new DashScopeClient(apiKey!, workspaceId: workspaceId);

        Console.WriteLine("Synthesizing (streaming)...");
        var file = new FileInfo("tts-qwen-audio-http.wav");
        await using (var stream = file.OpenWrite())
        {
            await foreach (var frame in workspaceClient.GetSpeechSynthesisStreamAsync(
                               new ModelRequest<SpeechSynthesisInput>
                               {
                                   Model = "qwen-audio-3.0-tts-flash",
                                   Input = new SpeechSynthesisInput
                                   {
                                       Text = "床前明月光，疑是地上霜。",
                                       Voice = "longanhuan_v3.6",
                                       Format = "wav",
                                       SampleRate = 24000,
                                       WordTimestampEnabled = true
                                   }
                               }))
            {
                // sentence-synthesis events carry base64 audio chunks, append them in order
                if (frame.Output.Audio?.Data is { Length: > 0 } data)
                {
                    var bytes = Convert.FromBase64String(data);
                    await stream.WriteAsync(bytes);
                }

                // word timestamps are complete in the sentence-end event
                if (frame.Output.Type == "sentence-end")
                {
                    foreach (var word in frame.Output.Sentence?.Words ?? Array.Empty<SpeechSynthesizerOutputWord>())
                    {
                        Console.WriteLine($"  {word.Text}: {word.BeginTime}~{word.EndTime}ms");
                    }
                }

                // the final frame carries the url of the complete audio file
                if (frame.Output.FinishReason == "stop")
                {
                    Console.WriteLine($"Audio url (24h): {frame.Output.Audio?.Url}");
                    Console.WriteLine(
                        $"Billed: {frame.Usage?.Characters} characters / {frame.Usage?.InputTokens}+{frame.Usage?.OutputTokens} tokens");
                }
            }
        }

        Console.WriteLine($"Audio saved to {file.FullName}");

        Console.WriteLine("Synthesizing (non-streaming)...");
        var response = await workspaceClient.GetSpeechSynthesisAsync(
            new ModelRequest<SpeechSynthesisInput>
            {
                Model = "qwen-audio-3.0-tts-flash",
                Input = new SpeechSynthesisInput { Text = "你好，我是千问。", Voice = "longanhuan_v3.6" }
            });
        Console.WriteLine($"Audio url (24h): {response.Output.Audio?.Url}");
    }
}
