using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.SpeechSynthesis;

public class AudioGenerationExample : SpeechSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "Generate audio from a text prompt with qwen-audio-3.1-tts-next";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // Audio generation shares the speech synthesizer endpoint, which is only served
        // on the workspace domain, it needs its own client configured with the workspace id.
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

        Console.WriteLine("Generating audio from a text prompt...");
        var response = await workspaceClient.GetSpeechSynthesisAsync(
            new ModelRequest<SpeechSynthesisInput>
            {
                Model = "qwen-audio-3.1-tts-next",
                Input = new SpeechSynthesisInput
                {
                    TextPrompt = "一位女性清晰地说：“你好，欢迎使用。”",
                    Format = "wav",
                    SampleRate = 48000,
                    Channels = 2
                }
            });
        Console.WriteLine($"Audio url (24h): {response.Output.Audio?.Url}");
        Console.WriteLine($"Duration: {response.Output.Audio?.Duration}s, billed {response.Usage?.Duration}s");
    }
}
