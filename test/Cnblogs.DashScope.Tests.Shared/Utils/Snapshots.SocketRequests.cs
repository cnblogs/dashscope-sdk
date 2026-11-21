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

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>>
            CancelTask =
                new(
                    GroupName,
                    "cancel-task",
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
                                SpeechSynthesizerParameters>
                            {
                                Input = new SpeechSynthesizerInput { Directive = "cancel" }
                            }
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

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechSynthesizerOutput>>
            TaskFinishedAfterCancel =
                new(
                    GroupName,
                    "task-finished-cancel",
                    new DashScopeWebSocketResponse<SpeechSynthesizerOutput>(
                        new DashScopeWebSocketResponseHeader(
                            "439e0616-2f5b-44e0-8872-0002a066a49c",
                            "task-finished",
                            null,
                            null,
                            new DashScopeWebSocketResponseHeaderAttributes(null)),
                        new DashScopeWebSocketResponsePayload<SpeechSynthesizerOutput>(
                            new SpeechSynthesizerOutput(null),
                            null)));

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

    public static class Sambert
    {
        private const string GroupName = "sambert";
        private const string TaskId = "b8e4c1f0-0000-4000-8000-000000000001";

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
                        Streaming = "out",
                        TaskId = TaskId
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                        {
                            Task = "tts",
                            TaskGroup = "audio",
                            Function = "SpeechSynthesizer",
                            Model = "sambert-zhichu-v1",
                            Parameters = new SpeechSynthesizerParameters
                            {
                                Format = "wav",
                                SampleRate = 16000,
                                Volume = 50,
                                Rate = 1,
                                Pitch = 1,
                                WordTimestampEnabled = true,
                                PhonemeTimestampEnabled = true
                            },
                            Input = new SpeechSynthesizerInput { Text = "床前明月光，疑是地上霜" }
                        }
                });

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechSynthesizerOutput>> TaskStarted =
            new(
                GroupName,
                "task-started",
                new DashScopeWebSocketResponse<SpeechSynthesizerOutput>(
                    new DashScopeWebSocketResponseHeader(
                        TaskId,
                        "task-started",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes(null),
                        "db8d554dd4ce4e8aae106a4c8b5d3909"),
                    new DashScopeWebSocketResponsePayload<SpeechSynthesizerOutput>(null, null)));

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechSynthesizerOutput>> TaskFinished =
            new(
                GroupName,
                "task-finished",
                new DashScopeWebSocketResponse<SpeechSynthesizerOutput>(
                    new DashScopeWebSocketResponseHeader(
                        TaskId,
                        "task-finished",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes(null)),
                    new DashScopeWebSocketResponsePayload<SpeechSynthesizerOutput>(
                        null,
                        new DashScopeWebSocketResponseUsage(11))));

        public static readonly SocketMessageSnapshot ResultGenerated = new(GroupName, "result-generated");

        public static readonly byte[] Audio =
            System.IO.File.ReadAllBytes(Path.Combine("RawHttpData", "socket-sambert.audio.wav"))[..1000];
    }
    public static class QwenAudioTts
    {
        private const string GroupName = "qwen-audio-tts";
        private const string TaskId = "0a4da074-d1b0-4d3d-9e6e-000000000001";

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
                        TaskId = TaskId
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                        {
                            Task = "tts",
                            TaskGroup = "audio",
                            Function = "SpeechSynthesizer",
                            Model = "qwen-audio-3.1-tts-flash",
                            Parameters = new SpeechSynthesizerParameters
                            {
                                Voice = "longanhuan_v3.1",
                                Format = "mp3",
                                SampleRate = 22050,
                                Volume = 50,
                                Rate = 1.0f,
                                Pitch = 1.0f,
                                EnableSsml = false
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
                        TaskId = TaskId,
                        Streaming = null
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                        {
                            Input = new SpeechSynthesizerInput { Text = "床前明月光，疑是地上霜" }
                        }
                });

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>>
            FinishTask = new(
                GroupName,
                "finish-task",
                new DashScopeWebSocketRequest<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                {
                    Header = new DashScopeWebSocketRequestHeader
                    {
                        Action = "finish-task",
                        TaskId = TaskId,
                        Streaming = null
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechSynthesizerInput, SpeechSynthesizerParameters>
                        {
                            Input = new SpeechSynthesizerInput()
                        }
                });

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechSynthesizerOutput>> TaskStarted =
            new(
                GroupName,
                "task-started",
                new DashScopeWebSocketResponse<SpeechSynthesizerOutput>(
                    new DashScopeWebSocketResponseHeader(
                        TaskId,
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
                        TaskId,
                        "task-finished",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes(null)),
                    new DashScopeWebSocketResponsePayload<SpeechSynthesizerOutput>(
                        new SpeechSynthesizerOutput(null),
                        new DashScopeWebSocketResponseUsage(21, InputTokens: 11, OutputTokens: 55, TotalTokens: 66))));

        public static readonly byte[] AudioTts =
            System.IO.File.ReadAllBytes(Path.Combine("RawHttpData", "socket-qwen-audio-tts.audio.mp3"))[..1000];
    }

    public static class SpeechRecognition
    {
        private const string GroupName = "speech-recognition";
        private const string TaskId = "3f1a2a5c-8b4e-4c1d-9a7f-2e5b8c1d0a64";

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>>
            RunTask = new(
                GroupName,
                "run-task",
                new DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>
                {
                    Header = new DashScopeWebSocketRequestHeader
                    {
                        Action = "run-task",
                        Streaming = "duplex",
                        TaskId = TaskId
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechRecognitionInput, SpeechRecognitionParameters>
                        {
                            Task = "asr",
                            TaskGroup = "audio",
                            Function = "recognition",
                            Model = "paraformer-realtime-v2",
                            Parameters = new SpeechRecognitionParameters
                            {
                                Format = "pcm",
                                SampleRate = 16000,
                                DisfluencyRemovalEnabled = false,
                                LanguageHints = new List<string> { "zh" },
                                SemanticPunctuationEnabled = false,
                                MaxSentenceSilence = 800,
                                MultiThresholdModeEnabled = true,
                                PunctuationPredictionEnabled = true,
                                Heartbeat = false,
                                InverseTextNormalizationEnabled = true
                            }
                        }
                });

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>>
            FinishTask = new(
                GroupName,
                "finish-task",
                new DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>
                {
                    Header = new DashScopeWebSocketRequestHeader
                    {
                        Action = "finish-task",
                        Streaming = "duplex",
                        TaskId = TaskId
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechRecognitionInput, SpeechRecognitionParameters>()
                });

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechRecognitionOutput>> TaskStarted =
            new(
                GroupName,
                "task-started",
                new DashScopeWebSocketResponse<SpeechRecognitionOutput>(
                    new DashScopeWebSocketResponseHeader(
                        TaskId,
                        "task-started",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes(null)),
                    new DashScopeWebSocketResponsePayload<SpeechRecognitionOutput>(null, null)));

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechRecognitionOutput>>
            ResultGeneratedIntermediate = new(
                GroupName,
                "result-generated-7",
                new DashScopeWebSocketResponse<SpeechRecognitionOutput>(
                    new DashScopeWebSocketResponseHeader(
                        TaskId,
                        "result-generated",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes("3e5a1f88-c609-4676-93b8-a000dff32491")),
                    new DashScopeWebSocketResponsePayload<SpeechRecognitionOutput>(
                        new SpeechRecognitionOutput(new SpeechRecognitionSentence(
                            0,
                            null,
                            "博客园成立于2004年",
                            null,
                            false,
                            null,
                            null,
                            new[]
                            {
                                new SpeechRecognitionWord("博客", 0, 528, string.Empty),
                                new SpeechRecognitionWord("园成", 528, 1056, string.Empty),
                                new SpeechRecognitionWord("立于", 1056, 1584, string.Empty),
                                new SpeechRecognitionWord("2004", 1584, 2112, string.Empty),
                                new SpeechRecognitionWord("年", 2112, 2640, string.Empty)
                            })),
                        null)));

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechRecognitionOutput>>
            ResultGenerated = new(
                GroupName,
                "result-generated-9",
                new DashScopeWebSocketResponse<SpeechRecognitionOutput>(
                    new DashScopeWebSocketResponseHeader(
                        TaskId,
                        "result-generated",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes("3e5a1f88-c609-4676-93b8-a000dff32491")),
                    new DashScopeWebSocketResponsePayload<SpeechRecognitionOutput>(
                        new SpeechRecognitionOutput(new SpeechRecognitionSentence(
                            0,
                            3580,
                            "博客源成立于2004年，代码改变世界。",
                            null,
                            true,
                            null,
                            null,
                            new[]
                            {
                                new SpeechRecognitionWord("博客", 0, 463, string.Empty),
                                new SpeechRecognitionWord("源成", 463, 926, string.Empty),
                                new SpeechRecognitionWord("立于", 926, 1389, string.Empty),
                                new SpeechRecognitionWord("2004", 1389, 1852, string.Empty),
                                new SpeechRecognitionWord("年", 1852, 2315, "，"),
                                new SpeechRecognitionWord("代码", 2316, 2737, string.Empty),
                                new SpeechRecognitionWord("改变", 2737, 3158, string.Empty),
                                new SpeechRecognitionWord("世界", 3158, 3580, "。")
                            })),
                        new DashScopeWebSocketResponseUsage(0, 4))));

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechRecognitionOutput>>
            TaskFinished = new(
                GroupName,
                "task-finished",
                new DashScopeWebSocketResponse<SpeechRecognitionOutput>(
                    new DashScopeWebSocketResponseHeader(
                        TaskId,
                        "task-finished",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes("3e5a1f88-c609-4676-93b8-a000dff32491")),
                    new DashScopeWebSocketResponsePayload<SpeechRecognitionOutput>(
                        new SpeechRecognitionOutput(null),
                        null)));

        public static readonly byte[] InputPcm =
            System.IO.File.ReadAllBytes(Path.Combine("RawHttpData", "socket-speech-recognition.input.pcm"));
    }

    public static class SpeechRecognition8k
    {
        private const string GroupName = "speech-recognition-8k";

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>>
            RunTask = new(
                GroupName,
                "run-task",
                new DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>
                {
                    Header = new DashScopeWebSocketRequestHeader
                    {
                        Action = "run-task",
                        Streaming = "duplex",
                        TaskId = "7c9d3e1f-4a2b-4e8c-b6d0-1f3a5c7e9b21"
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechRecognitionInput, SpeechRecognitionParameters>
                        {
                            Task = "asr",
                            TaskGroup = "audio",
                            Function = "recognition",
                            Model = "paraformer-realtime-8k-v2",
                            Parameters = new SpeechRecognitionParameters
                            {
                                Format = "wav",
                                SampleRate = 8000
                            }
                        }
                });

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechRecognitionOutput>>
            ResultGenerated = new(
                GroupName,
                "result-generated-6",
                new DashScopeWebSocketResponse<SpeechRecognitionOutput>(
                    new DashScopeWebSocketResponseHeader(
                        "7c9d3e1f-4a2b-4e8c-b6d0-1f3a5c7e9b21",
                        "result-generated",
                        null,
                        null,
                        new DashScopeWebSocketResponseHeaderAttributes("527898de-00c2-4483-b0e7-0b7a69c365f5")),
                    new DashScopeWebSocketResponsePayload<SpeechRecognitionOutput>(
                        new SpeechRecognitionOutput(new SpeechRecognitionSentence(
                            0,
                            3580,
                            "博客园成立于2004年，代码改变世界。",
                            null,
                            true,
                            "neutral",
                            0.995f,
                            new[]
                            {
                                new SpeechRecognitionWord("博客", 0, 462, string.Empty),
                                new SpeechRecognitionWord("园成", 462, 924, string.Empty),
                                new SpeechRecognitionWord("立于", 924, 1386, string.Empty),
                                new SpeechRecognitionWord("2004", 1386, 1848, string.Empty),
                                new SpeechRecognitionWord("年", 1848, 2310, "，"),
                                new SpeechRecognitionWord("代码", 2316, 2737, string.Empty),
                                new SpeechRecognitionWord("改变", 2737, 3158, string.Empty),
                                new SpeechRecognitionWord("世界", 3158, 3580, "。")
                            })),
                        new DashScopeWebSocketResponseUsage(0, 4))));
    }

    public static class SpeechRecognitionEn
    {
        private const string GroupName = "speech-recognition-en";

        public static readonly
            SocketMessageSnapshot<DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>>
            RunTask = new(
                GroupName,
                "run-task",
                new DashScopeWebSocketRequest<SpeechRecognitionInput, SpeechRecognitionParameters>
                {
                    Header = new DashScopeWebSocketRequestHeader
                    {
                        Action = "run-task",
                        Streaming = "duplex",
                        TaskId = "9b8c7d6e-5f4a-4b3c-a2d1-0e9f8a7b6c5d"
                    },
                    Payload =
                        new DashScopeWebSocketRequestPayload<SpeechRecognitionInput, SpeechRecognitionParameters>
                        {
                            Task = "asr",
                            TaskGroup = "audio",
                            Function = "recognition",
                            Model = "paraformer-realtime-v2",
                            Parameters = new SpeechRecognitionParameters
                            {
                                Format = "mp3",
                                SampleRate = 16000,
                                DisfluencyRemovalEnabled = true,
                                LanguageHints = new List<string> { "en" }
                            }
                        }
                });
    }

    public static class SpeechRecognitionInvalid
    {
        private const string GroupName = "speech-recognition-invalid";

        public static readonly SocketMessageSnapshot<DashScopeWebSocketResponse<SpeechRecognitionOutput>> TaskFailed =
            new(
                GroupName,
                "task-failed",
                new DashScopeWebSocketResponse<SpeechRecognitionOutput>(
                    new DashScopeWebSocketResponseHeader(
                        "1a2b3c4d-5e6f-4a7b-8c9d-0e1f2a3b4c5d",
                        "task-failed",
                        "UNSUPPORTED_FORMAT",
                        "UNSUPPORTED_FORMAT",
                        new DashScopeWebSocketResponseHeaderAttributes(null)),
                    new DashScopeWebSocketResponsePayload<SpeechRecognitionOutput>(null, null)));
    }
}
