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
                            Model = "cosyvoice-v3-flash",
                            Parameters = new SpeechSynthesizerParameters
                            {
                                EnableSsml = true,
                                Format = "mp3",
                                Pitch = 1.2f,
                                Voice = "longanhuan_v3",
                                Volume = 50,
                                SampleRate = 24000,
                                Rate = 1.1f,
                                BitRate = 96,
                                Seed = 42,
                                LanguageHints = new List<string> { "zh" },
                                Instruction = "请用河南话表达。",
                                WordTimestampEnabled = true,
                                EnableAigcTag = true,
                                AigcPropagator = "cnblogs",
                                AigcPropagateId = "20260117000001",
                                HotFix = new SpeechSynthesizerHotFix(
                                    new List<Dictionary<string, string>> { new() { { "天气", "tian1 qi4" } } },
                                    new List<Dictionary<string, string>> { new() { { "今天", "金天" } } }),
                                EnableMarkdownFilter = true,
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
                        "task-finished",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes(null)),
                    new DashScopeWebSocketResponsePayload<SpeechSynthesizerOutput>(
                        new SpeechSynthesizerOutput(null),
                        new DashScopeWebSocketResponseUsage(12))));

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechSynthesizerOutput>> TaskFailed =
            new(
                GroupName,
                "task-failed",
                new DashScopeWebSocketResponse<SpeechSynthesizerOutput>(
                    new DashScopeWebSocketResponseHeader(
                        "439e0616-2f5b-44e0-8872-0002a066a49c",
                        "task-failed",
                        "InvalidParameter",
                        "Invalid aigc propagate id",
                        new DashScopeWebSocketResponseHeaderAttributes(null)),
                    new DashScopeWebSocketResponsePayload<SpeechSynthesizerOutput>(null, null)));

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechSynthesizerOutput>>
            ResultGenerated = new(
                GroupName,
                "result-generated",
                new DashScopeWebSocketResponse<SpeechSynthesizerOutput>(
                    new DashScopeWebSocketResponseHeader(
                        "439e0616-2f5b-44e0-8872-0002a066a49c",
                        "result-generated",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes(null)),
                    new DashScopeWebSocketResponsePayload<SpeechSynthesizerOutput>(
                        new SpeechSynthesizerOutput(new SpeechSynthesizerOutputSentences(
                            new SpeechSynthesizerOutputWord[]
                            {
                                new("代", 0, 1, 41, 292),
                                new("码", 1, 2, 292, 542),
                                new("改", 2, 3, 542, 626),
                                new("变", 3, 4, 793, 876),
                                new("世", 4, 5, 960, 1127),
                                new("界", 5, 6, 1210, 1419)
                            })),
                        new DashScopeWebSocketResponseUsage(12))));

        public static readonly byte[] AudioTts =
            System.IO.File.ReadAllBytes(Path.Combine("RawHttpData", "socket-speech-synthesizer.audio.mp3"))[..1000];
    }
}
