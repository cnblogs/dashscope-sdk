using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.VoiceCustomization;

public class VoiceCustomizationExample : VoiceCustomizationSample
{
    /// <inheritdoc />
    public override string Description => "Clone and design voices, then manage them (workspace domain)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // The voice customization API is only served on the workspace domain,
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
        var cloneVoiceId = string.Empty;
        try
        {
            Console.WriteLine("Cloning a voice from the sample audio...");
            var clone = await workspaceClient.CustomizeVoiceAsync(
                new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                {
                    Model = "voice-enrollment",
                    Input = new VoiceCustomizationInput
                    {
                        Action = "create_voice",
                        TargetModel = "qwen-audio-3.0-tts-flash",
                        Prefix = "sdksample",
                        Audio = "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/audio/cosyvoice/cosyvoice-zeroshot-sample.wav",
                        LanguageHints = new List<string> { "zh" }
                    }
                });
            cloneVoiceId = clone.Output.VoiceId!;
            Console.WriteLine($"Cloned voice: {cloneVoiceId}");

            Console.WriteLine("Designing a voice from a text description...");
            var design = await workspaceClient.CustomizeVoiceAsync(
                new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                {
                    Model = "voice-enrollment",
                    Input = new VoiceCustomizationInput
                    {
                        Action = "create_voice",
                        TargetModel = "cosyvoice-v3.5-plus",
                        VoicePrompt = "沉稳的中年男性，音色低沉浑厚",
                        PreviewText = "各位听众朋友们大家好，欢迎收听本期节目",
                        Prefix = "sdksample",
                        LanguageHints = new List<string> { "zh" }
                    },
                    Parameters = new VoiceCustomizationParameters { SampleRate = 24000, ResponseFormat = "wav" }
                });
            var designVoiceId = design.Output.VoiceId!;
            Console.WriteLine($"Designed voice: {designVoiceId}");
            var preview = new FileInfo("voice-preview.wav");
            File.WriteAllBytes(preview.FullName, Convert.FromBase64String(design.Output.PreviewAudio!.Data!));
            Console.WriteLine($"Preview audio saved to {preview.FullName}");

            try
            {
                Console.WriteLine("Listing voices with the sdksample prefix...");
                var list = await workspaceClient.CustomizeVoiceAsync(
                    new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                    {
                        Model = "voice-enrollment",
                        Input = new VoiceCustomizationInput
                        {
                            Action = "list_voice",
                            Prefix = "sdksample",
                            PageSize = 10,
                            PageIndex = 0
                        }
                    });
                foreach (var voice in list.Output.VoiceList ?? new List<VoiceCustomizationVoice>())
                {
                    var description = string.IsNullOrEmpty(voice.VoicePrompt)
                        ? string.Empty
                        : $" ({voice.VoicePrompt})";
                    Console.WriteLine($"  {voice.VoiceId}: {voice.Status}{description}");
                }

                Console.WriteLine("Querying the cloned voice...");
                var query = await workspaceClient.CustomizeVoiceAsync(
                    new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                    {
                        Model = "voice-enrollment",
                        Input = new VoiceCustomizationInput { Action = "query_voice", VoiceId = cloneVoiceId }
                    });
                Console.WriteLine($"  status: {query.Output.Status}, created at {query.Output.GmtCreate}");
            }
            finally
            {
                // designed voices cannot be updated, just delete them
                await workspaceClient.CustomizeVoiceAsync(
                    new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                    {
                        Model = "voice-enrollment",
                        Input = new VoiceCustomizationInput { Action = "delete_voice", VoiceId = designVoiceId }
                    });
                Console.WriteLine($"Deleted designed voice: {designVoiceId}");
            }
        }
        finally
        {
            if (string.IsNullOrEmpty(cloneVoiceId) == false)
            {
                await workspaceClient.CustomizeVoiceAsync(
                    new ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>
                    {
                        Model = "voice-enrollment",
                        Input = new VoiceCustomizationInput { Action = "delete_voice", VoiceId = cloneVoiceId }
                    });
                Console.WriteLine($"Deleted cloned voice: {cloneVoiceId}");
            }
        }
    }
}
