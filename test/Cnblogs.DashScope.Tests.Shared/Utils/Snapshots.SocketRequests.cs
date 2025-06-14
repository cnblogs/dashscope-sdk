using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public partial class Snapshots
{
    public static class SpeechSynthesizer
    {
        private const string GroupName = "speech-synthesizer";

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>>
            RunTask = new(GroupName, "run-task", new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>()
            {
                Header = new DashScopeWebSocketRequestHeader()
                {
                    Action = "run-task",
                    Streaming = "duplex",
                    TaskId = "439e0616-2f5b-44e0-8872-0002a066a49c"
                },
                Payload = new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>()
                {
                    Task = "tts",
                    TaskGroup = "audio",
                    Function = "SpeechSynthesizer",
                    Model = "cosyvoice-v1",
                    Parameters = new SpeechSynthesizerParameters()
                    {
                        EnableSsml = true,
                        Format = "mp3",
                        Pitch = 1.2f,
                        Voice = "longxiaochun",
                        Volume = 50,
                        SampleRate = 0,
                        Rate = 1.1f,
                    }
                }
            });
    }
}
