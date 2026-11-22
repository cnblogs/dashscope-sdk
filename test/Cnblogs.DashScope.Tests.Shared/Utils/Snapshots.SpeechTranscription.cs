using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class SpeechTranscription
    {
        public static readonly RequestSnapshot<ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>,
                ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            CreateTask = new(
                "speech-transcription-create",
                new ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>
                {
                    Model = "qwen3-asr-flash-filetrans",
                    Input = new SpeechTranscriptionInput { FileUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3" },
                    Parameters = new SpeechTranscriptionParameters
                    {
                        ChannelId = new List<int> { 0 },
                        EnableItn = true,
                        EnableWords = true,
                        Language = "zh"
                    }
                },
                new ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>
                {
                    RequestId = "dd582e29-f2c8-9669-9497-4ea5cab1869f",
                    Output = new SpeechTranscriptionOutput(null) { TaskId = "c0ce0534-38d3-4ced-a558-fc3bea6897c9", TaskStatus = DashScopeTaskStatus.Pending }
                });

        public static readonly RequestSnapshot<DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            GetTaskSuccess = new(
                "get-task-speech-transcription-success",
                new DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>(
                    "3ebf3399-fe97-9ff5-8f2a-8de2311517fd",
                    new SpeechTranscriptionOutput(new SpeechTranscriptionResult("http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/qwen3-asr-flash-filetrans/20261122/20%3A23/387e172f-cfc9-4846-8c88-8c14dc92a6ec.json?Expires=1795436626&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=wrtJ%2Bg5jsblQZnEUjkl2rkc1Ffg%3D&response-content-disposition=attachment%3Bfilename%3D387e172f-cfc9-4846-8c88-8c14dc92a6ec.json"))
                    {
                        TaskId = "387e172f-cfc9-4846-8c88-8c14dc92a6ec",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 22, 20, 23, 45, 795),
                        ScheduledTime = new DateTime(2026, 11, 22, 20, 23, 45, 841),
                        EndTime = new DateTime(2026, 11, 22, 20, 23, 46, 506),
                    },
                    new SpeechTranscriptionUsage { Seconds = 8 }));

        public static readonly RequestSnapshot<ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>,
                ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            QwenAudioFiletransCreateTask = new(
                "speech-transcription-qwen-audio-filetrans",
                new ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>
                {
                    Model = "qwen-audio-3.1-asr-flash-filetrans",
                    Input = new SpeechTranscriptionInput
                    {
                        FileUrls = new List<string> { "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3" },
                        Context = new List<SpeechRecognitionContext>
                        {
                            new(
                                "user",
                                new List<SpeechRecognitionContextContent> { new("input_text", "请背诵《从军行》") }),
                            new(
                                "assistant",
                                new List<SpeechRecognitionContextContent> { new("text", "好的，为您朗诵《从军行》。") })
                        }
                    },
                    Parameters = new SpeechTranscriptionParameters
                    {
                        Vocabulary = new Dictionary<string, int> { ["青海"] = 5, ["楼兰"] = 5 },
                        LanguageHints = new List<string> { "zh" },
                        DiarizationEnabled = true,
                        SpeakerCount = 2
                    }
                },
                new ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>
                {
                    RequestId = "41222df0-c720-97eb-aeef-d6f6614a901f",
                    Output = new SpeechTranscriptionOutput(null) { TaskId = "feddc3df-4a44-4189-8dc9-c9ca70b381c8", TaskStatus = DashScopeTaskStatus.Pending }
                });

        public static readonly RequestSnapshot<ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>,
                ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            FunAsrCreateTask = new(
                "speech-transcription-fun-asr",
                new ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>
                {
                    Model = "fun-asr",
                    Input = new SpeechTranscriptionInput { FileUrls = new List<string> { "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3" } },
                    Parameters = new SpeechTranscriptionParameters
                    {
                        ChannelId = new List<int> { 0 },
                        SpecialWordFilter = new SpeechRecognitionSpecialWordFilter { SystemReservedFilter = false }
                    }
                },
                new ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>
                {
                    RequestId = "f45b6a8e-d4e0-9f73-97b8-f44256426da9",
                    Output = new SpeechTranscriptionOutput(null) { TaskId = "298b9c76-8aea-4920-b45e-6dfda5fb0e29", TaskStatus = DashScopeTaskStatus.Pending }
                });

        public static readonly RequestSnapshot<ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>,
                ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            ParaformerCreateTask = new(
                "speech-transcription-paraformer",
                new ModelRequest<SpeechTranscriptionInput, SpeechTranscriptionParameters>
                {
                    Model = "paraformer-v2",
                    Input = new SpeechTranscriptionInput { FileUrls = new List<string> { "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3" } },
                    Parameters = new SpeechTranscriptionParameters
                    {
                        ChannelId = new List<int> { 0 },
                        DisfluencyRemovalEnabled = true,
                        TimestampAlignmentEnabled = true,
                        LanguageHints = new List<string> { "zh" },
                        DiarizationEnabled = true,
                        SpeakerCount = 2
                    }
                },
                new ModelResponse<SpeechTranscriptionOutput, SpeechTranscriptionUsage>
                {
                    RequestId = "80e35c30-d691-92f5-b449-d777e88f3015",
                    Output = new SpeechTranscriptionOutput(null) { TaskId = "8e1aa624-f7c3-4d24-840d-ad971de12c44", TaskStatus = DashScopeTaskStatus.Pending }
                });

        public static readonly RequestSnapshot<DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            QwenAudioFiletransGetTaskSuccess = new(
                "get-task-speech-transcription-qwen-audio-filetrans-success",
                new DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>(
                    "dbb511f3-443b-973e-9925-b0703da465ac",
                    new SpeechTranscriptionOutput(
                        null,
                        new List<SpeechTranscriptionFileResult>
                        {
                        new SpeechTranscriptionFileResult(
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3",
                            TranscriptionUrl: "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/qwen-audio-3.1-asr-flash-filetrans/20261122/21%3A56/6fc73751-4137-4b0d-a975-9590ab7c0add.json?Expires=1795442162&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=A0SiYkC1Wrj78ySbbWklcK%2BPU%2Fk%3D&response-content-disposition=attachment%3Bfilename%3D6fc73751-4137-4b0d-a975-9590ab7c0add.json",
                            SubTaskStatus: "SUCCEEDED")
                        })
                    {
                        TaskId = "feddc3df-4a44-4189-8dc9-c9ca70b381c8",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 22, 21, 56, 1, 772),
                        ScheduledTime = new DateTime(2026, 11, 22, 21, 56, 1, 808),
                        EndTime = new DateTime(2026, 11, 22, 21, 56, 2, 661),
                    },
                    new SpeechTranscriptionUsage
                    {
                        Duration = 9,
                        InputTokens = 142,
                        OutputTokens = 25,
                        TotalTokens = 167,
                    }));

        public static readonly RequestSnapshot<DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            FunAsrGetTaskSuccess = new(
                "get-task-speech-transcription-fun-asr-success",
                new DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>(
                    "aed835b9-23c0-9bb4-a1a9-2d2facbc3ab0",
                    new SpeechTranscriptionOutput(
                        null,
                        new List<SpeechTranscriptionFileResult>
                        {
                        new SpeechTranscriptionFileResult(
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3",
                            TranscriptionUrl: "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/fun-asr/20261122/21%3A56/a6c02608-f826-4bc6-aee9-2c212218e727-1.json?Expires=1795442163&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=yVdWWLKg4gCO14llIQCuY3wZEG8%3D&response-content-disposition=attachment%3Bfilename%3Da6c02608-f826-4bc6-aee9-2c212218e727-1.json",
                            SubTaskStatus: "SUCCEEDED")
                        })
                    {
                        TaskId = "298b9c76-8aea-4920-b45e-6dfda5fb0e29",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 22, 21, 56, 2, 366),
                        ScheduledTime = new DateTime(2026, 11, 22, 21, 56, 2, 401),
                        EndTime = new DateTime(2026, 11, 22, 21, 56, 3, 393),
                        TaskMetrics = new DashScopeTaskMetrics(1, 1, 0),
                    },
                    new SpeechTranscriptionUsage
                    {
                        Duration = 8,
                    }));

        public static readonly RequestSnapshot<DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>>
            ParaformerGetTaskSuccess = new(
                "get-task-speech-transcription-paraformer-success",
                new DashScopeTask<SpeechTranscriptionOutput, SpeechTranscriptionUsage>(
                    "685d6e9b-88cc-969f-837e-8371a59ab93e",
                    new SpeechTranscriptionOutput(
                        null,
                        new List<SpeechTranscriptionFileResult>
                        {
                        new SpeechTranscriptionFileResult(
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3",
                            TranscriptionUrl: "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/prod/paraformer-v2/20261122/21%3A56/aa891610-8c64-4dd7-88cf-2cfaad5804aa-1.json?Expires=1795442163&OSSAccessKeyId=LTAI5tGzqbGcEmE58b221XQy&Signature=SOniBCR1vOQxeZdvwQNrciwFXgo%3D&response-content-disposition=attachment%3Bfilename%3Daa891610-8c64-4dd7-88cf-2cfaad5804aa-1.json",
                            SubTaskStatus: "SUCCEEDED")
                        })
                    {
                        TaskId = "8e1aa624-f7c3-4d24-840d-ad971de12c44",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 22, 21, 56, 2, 914),
                        ScheduledTime = new DateTime(2026, 11, 22, 21, 56, 2, 954),
                        EndTime = new DateTime(2026, 11, 22, 21, 56, 3, 879),
                        TaskMetrics = new DashScopeTaskMetrics(1, 1, 0),
                    },
                    new SpeechTranscriptionUsage
                    {
                        Duration = 9,
                    }));
    }
}
