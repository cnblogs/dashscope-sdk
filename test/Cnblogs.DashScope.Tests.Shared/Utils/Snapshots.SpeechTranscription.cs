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
    }
}
