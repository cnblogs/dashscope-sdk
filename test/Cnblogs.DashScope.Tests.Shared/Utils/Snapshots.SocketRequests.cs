using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public partial class Snapshots
{
    public static class SpeechSynthesizer
    {
        private const string GroupName = "speech-synthesizer";

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>>
            RunTask = new(
                GroupName,
                "run-task",
                new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                {
                    Header = new DashScopeWebSocketRequestHeader
                    {
                        Action = "run-task",
                        Streaming = "duplex",
                        TaskId = "439e0616-2f5b-44e0-8872-0002a066a49c"
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                        {
                            Task = "tts",
                            TaskGroup = "audio",
                            Function = "SpeechSynthesizer",
                            Model = "cosyvoice-v1",
                            Parameters = new SpeechSynthesizerParameters
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

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>>
            ContinueTask = new(
                GroupName,
                "continue-task",
                new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                {
                    Header = new DashScopeWebSocketRequestHeader
                    {
                        Action = "continue-task",
                        TaskId = "439e0616-2f5b-44e0-8872-0002a066a49c",
                        Streaming = null
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                        {
                            Input = new SpeechSynthesizerInput { Text = "代码改变世界" }
                        }
                });

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>>
            FinishTask =
                new(
                    GroupName,
                    "finish-task",
                    new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                    {
                        Header = new DashScopeWebSocketRequestHeader
                        {
                            Action = "finish-task",
                            TaskId = "439e0616-2f5b-44e0-8872-0002a066a49c",
                            Streaming = null
                        },
                        Payload =
                            new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput,
                                SpeechSynthesizerParameters> { Input = new SpeechSynthesizerInput() }
                    });

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechSynthesizerOutput>> TaskStarted =
            new(
                GroupName,
                "task-started",
                new DashScopeWebSocketResponse<SpeechSynthesizerOutput>(
                    new DashScopeWebSocketResponseHeader(
                        "439e0616-2f5b-44e0-8872-0002a066a49c",
                        "task-started",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes(null)),
                    new DashScopeWebSocketResponsePayload<SpeechSynthesizerOutput>(null, null)));

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechSynthesizerOutput>> TaskFinished =
            new(
                GroupName,
                "task-finished",
                new DashScopeWebSocketResponse<SpeechSynthesizerOutput>(
                    new DashScopeWebSocketResponseHeader(
                        "439e0616-2f5b-44e0-8872-0002a066a49c",
                        "finished-task",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes(null)),
                    new DashScopeWebSocketResponsePayload<SpeechSynthesizerOutput>(null, null)));

        public static readonly SocketMessageSnapshot TaskFailed = new(GroupName, "task-failed");
        public static readonly SocketMessageSnapshot ResultGenerated = new(GroupName, "result-generated");
    }
}
