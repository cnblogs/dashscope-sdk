using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class Tasks
    {
        public static readonly RequestSnapshot<DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>>
            Unknown = new(
                "get-task-unknown",
                new DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>(
                    "85c25460-6440-91a7-b14e-2978fe60bd0f",
                    new BatchGetEmbeddingsOutput { TaskId = "1111", TaskStatus = DashScopeTaskStatus.Unknown }));

        public static readonly RequestSnapshot<DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>>
            BatchEmbeddingSuccess = new(
                "get-task-batch-text-embedding-success",
                new DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>(
                    "0b2ebeda-a91b-948f-986a-d395cbf1d0e1",
                    new BatchGetEmbeddingsOutput
                    {
                        TaskId = "7408ef3d-a0be-4379-9e72-a6e95a569483",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        Url =
                            "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/5fc5c860/2024-11-25/c6c4456e-3c66-42ba-a52a-a16c58dda4d6_output_1732514147173.txt.gz?Expires=1732773347&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=perMNS1RdHHroUn2YnXxzTmOZtg%3D",
                        SubmitTime = new DateTime(2024, 11, 25, 13, 55, 46, 536),
                        ScheduledTime = new DateTime(2024, 11, 25, 13, 55, 46, 557),
                        EndTime = new DateTime(2024, 11, 25, 13, 55, 47, 446)
                    },
                    new TextEmbeddingTokenUsage(28)));

        public static readonly RequestSnapshot<DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>>
            ImageSynthesisRunning =
                new(
                    "get-task-running",
                    new DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>(
                        "edbd4e81-d37b-97f1-9857-d7394829dd0f",
                        new ImageSynthesisOutput
                        {
                            TaskStatus = DashScopeTaskStatus.Running,
                            TaskId = "9e2b6ef6-285d-4efa-8651-4dbda7d571fa",
                            SubmitTime = new DateTime(2024, 3, 1, 17, 38, 24, 817),
                            ScheduledTime = new DateTime(2024, 3, 1, 17, 38, 24, 831),
                            TaskMetrics = new DashScopeTaskMetrics(4, 0, 0)
                        }));

        public static readonly RequestSnapshot<DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>>
            ImageSynthesisSuccess = new(
                "get-task-image-synthesis-success",
                new DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>(
                    "6662e925-4846-9afe-a3af-0d131805d378",
                    new ImageSynthesisOutput
                    {
                        TaskId = "9e2b6ef6-285d-4efa-8651-4dbda7d571fa",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2024, 3, 1, 17, 38, 24, 817),
                        ScheduledTime = new DateTime(2024, 3, 1, 17, 38, 24, 831),
                        EndTime = new DateTime(2024, 3, 1, 17, 38, 55, 565),
                        Results =
                            new List<ImageSynthesisResult>
                            {
                                new ImageSynthesisResult(
                                    "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/1d/d4/20240301/8d820c8d/4c48fa53-2907-499b-b9ac-76477fe8d299-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=bEfLmd%2BarXgZyhxcVYOWs%2BovJb8%3D"),
                                new ImageSynthesisResult(
                                    "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/79/20240301/3ab595ad/aa3e6d8d-884d-4431-b9c2-3684edeb072e-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=fdPScmRkIXyH3TSaSaWwvVjxREQ%3D"),
                                new ImageSynthesisResult(
                                    "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/0f/20240301/3ab595ad/ecfe06b3-b91c-4950-a932-49ea1619a1f9-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=gNuVAt8iy4X8Nl2l3K4Gu4f0ydw%3D"),
                                new ImageSynthesisResult(
                                    "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/3d/20240301/3ab595ad/3fca748e-d491-458a-bb72-73649af33209-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=Mx5TueC9I9yfDno9rjzi48opHtM%3D")
                            },
                        TaskMetrics = new DashScopeTaskMetrics(4, 4, 0)
                    },
                    new ImageSynthesisUsage(4)));

        public static readonly RequestSnapshot<DashScopeTask<ImageGenerationOutput, ImageGenerationUsage>>
            ImageGenerationSuccess = new(
                "get-task-image-generation-success",
                new DashScopeTask<ImageGenerationOutput, ImageGenerationUsage>(
                    "f927c766-5079-90f8-9354-6a87d2167897",
                    new ImageGenerationOutput
                    {
                        TaskId = "c4f94e00-5899-431b-9579-eb1ebe686379",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2024, 3, 2, 22, 22, 13, 026),
                        ScheduledTime = new DateTime(2024, 3, 2, 22, 22, 13, 051),
                        EndTime = new DateTime(2024, 3, 2, 22, 22, 21),
                        StartTime = new DateTime(2024, 3, 2, 22, 22, 13),
                        StyleIndex = 3,
                        ErrorCode = 0,
                        ErrorMessage = "Success",
                        Results =
                            new List<ImageGenerationResult>
                            {
                                new ImageGenerationResult(
                                    "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/viapi-video/2024-03-02/ac5d435a-9ea9-4287-8666-e1be7bbba943/20240302222213528791_style3_jxdf6o4zwy.jpg?Expires=1709475741&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=LM26fy1Pk8rCfPzihzpUqa3Vst8%3D")
                            }
                    },
                    new ImageGenerationUsage(1)));

        public static readonly RequestSnapshot<DashScopeTask<BackgroundGenerationOutput, BackgroundGenerationUsage>>
            BackgroundGenerationSuccess = new(
                "get-task-background-generation-success",
                new DashScopeTask<BackgroundGenerationOutput, BackgroundGenerationUsage>(
                    "8b22164d-c784-9a31-bda3-3c26259d4213",
                    new BackgroundGenerationOutput
                    {
                        TaskId = "b2e98d78-c79b-431c-b2d7-c7bcd54465da",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2024, 3, 4, 10, 8, 57, 333),
                        ScheduledTime = new DateTime(2024, 3, 4, 10, 8, 57, 363),
                        EndTime = new DateTime(2024, 3, 4, 10, 9, 7, 727),
                        Results =
                            new List<BackgroundGenerationResult>
                            {
                                new BackgroundGenerationResult(
                                    "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100905_0_02dc0bba-8b1d-4648-8b95-eb2b92fe715d.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=OYstgSxWOl%2FOxYTLa2Mx3bi2RWw%3D"),
                                new BackgroundGenerationResult(
                                    "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100905_1_e1af86ec-152a-4ebe-b2a0-b40a592043b2.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=p0UXTUdXfp0tFlt0K5tDsA%2Fxl1M%3D")
                            },
                        TaskMetrics = new DashScopeTaskMetrics(2, 2, 0),
                        TextResults =
                            new BackgroundGenerationTextResult(
                                new List<BackgroundGenerationTextResultUrl>
                                {
                                    new BackgroundGenerationTextResultUrl(
                                        "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100901_0_4645005c-713d-4e92-9629-b12cbe5f3671.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=kmZGXc2s8P4uI%2BVrADITyrPz82U%3D"),
                                    new BackgroundGenerationTextResultUrl(
                                        "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100901_1_b1979b75-c553-4d9b-9c9f-80f401a0d124.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=cb1Qg%2FkIuZyI7XQqWHjP712N0ak%3D")
                                },
                                new List<BackgroundGenerationTextResultParams>
                                {
                                    new BackgroundGenerationTextResultParams(
                                        0,
                                        new List<BackgroundGenerationTextResultLayer>
                                        {
                                            new BackgroundGenerationTextResultLayer(
                                                0,
                                                "text_mask",
                                                0,
                                                0,
                                                1024,
                                                257,
                                                Color: "#521b08",
                                                Opacity: 0.8f,
                                                Radius: 0,
                                                Gradient: new BackgroundGenerationTextResultGradient(
                                                    "linear",
                                                    "pixels",
                                                    new List<BackgroundGenerationTextResultGradientColorStop>
                                                    {
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#521b0800",
                                                            0),
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#521b08ff",
                                                            1)
                                                    })
                                                {
                                                    Coords = new Dictionary<string, int>
                                                    {
                                                        { "y1", 257 },
                                                        { "x1", 0 },
                                                        { "y2", 0 },
                                                        { "x2", 0 }
                                                    }
                                                }),
                                            new BackgroundGenerationTextResultLayer(
                                                1,
                                                "text",
                                                25,
                                                319,
                                                385,
                                                77,
                                                SubType: "Title",
                                                FontWeight: "Regular",
                                                FontSize: 67,
                                                Content: "分享好时光",
                                                FontUnderLine: false,
                                                LineHeight: 1f,
                                                FontItalic: false,
                                                FontColor: "#e6baa7",
                                                TextShadow: "1px 0px #80808080",
                                                TextStroke: "1px #fffffff0",
                                                FontFamily: "站酷文艺体",
                                                Alignment: "center",
                                                FontLineThrough: false,
                                                Direction: "horizontal",
                                                Opacity: 1f),
                                            new BackgroundGenerationTextResultLayer(
                                                2,
                                                "text_mask",
                                                118,
                                                395,
                                                233,
                                                50,
                                                Color: "#e6baa7",
                                                Opacity: 1f,
                                                Radius: 37,
                                                BoxShadow: "2px 1px #80808080",
                                                Gradient: new BackgroundGenerationTextResultGradient(
                                                    "linear",
                                                    "pixels",
                                                    new List<BackgroundGenerationTextResultGradientColorStop>
                                                    {
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#e6baa7ff",
                                                            0),
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#e6baa7ff",
                                                            1)
                                                    })
                                                {
                                                    Coords = new Dictionary<string, int>
                                                    {
                                                        { "y1", 0 },
                                                        { "x1", 0 },
                                                        { "y2", 50 },
                                                        { "x2", 0 }
                                                    }
                                                }),
                                            new BackgroundGenerationTextResultLayer(
                                                3,
                                                "text",
                                                118,
                                                395,
                                                233,
                                                50,
                                                FontWeight: "Medium",
                                                FontSize: 27,
                                                Content: "只为不一样的你",
                                                FontUnderLine: false,
                                                LineHeight: 1f,
                                                FontItalic: false,
                                                SubType: "SubTitle",
                                                FontColor: "#223629",
                                                TextShadow: null,
                                                FontFamily: "阿里巴巴普惠体",
                                                Alignment: "center",
                                                Opacity: 1f,
                                                FontLineThrough: false,
                                                Direction: "horizontal")
                                        }),
                                    new BackgroundGenerationTextResultParams(
                                        1,
                                        new List<BackgroundGenerationTextResultLayer>
                                        {
                                            new BackgroundGenerationTextResultLayer(
                                                0,
                                                "text_mask",
                                                0,
                                                0,
                                                1024,
                                                257,
                                                Color: "#efeae4",
                                                Gradient: new BackgroundGenerationTextResultGradient(
                                                    "linear",
                                                    "pixels",
                                                    new List<BackgroundGenerationTextResultGradientColorStop>
                                                    {
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#efeae400",
                                                            0),
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#efeae4ff",
                                                            1)
                                                    })
                                                {
                                                    Coords = new Dictionary<string, int>
                                                    {
                                                        { "y1", 257 },
                                                        { "x1", 0 },
                                                        { "y2", 0 },
                                                        { "x2", 0 }
                                                    }
                                                },
                                                Opacity: 0.8f,
                                                Radius: 0),
                                            new BackgroundGenerationTextResultLayer(
                                                1,
                                                "text",
                                                25,
                                                319,
                                                385,
                                                77,
                                                SubType: "Title",
                                                Content: "分享好时光",
                                                FontWeight: "Regular",
                                                FontSize: 67,
                                                FontUnderLine: false,
                                                LineHeight: 1f,
                                                FontItalic: false,
                                                FontColor: "#421f12",
                                                TextStroke: "1px #fffffff0",
                                                TextShadow: "0px 2px #80808080",
                                                FontFamily: "钉钉进步体",
                                                Alignment: "center",
                                                Opacity: 1f,
                                                FontLineThrough: false,
                                                Direction: "horizontal"),
                                            new BackgroundGenerationTextResultLayer(
                                                2,
                                                "text_mask",
                                                118,
                                                395,
                                                233,
                                                50,
                                                Color: "#421f12",
                                                Gradient: new BackgroundGenerationTextResultGradient(
                                                    "linear",
                                                    "pixels",
                                                    new List<BackgroundGenerationTextResultGradientColorStop>
                                                    {
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#421f12ff",
                                                            0),
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#421f12ff",
                                                            1)
                                                    })
                                                {
                                                    Coords = new Dictionary<string, int>
                                                    {
                                                        { "y1", 0 },
                                                        { "x1", 0 },
                                                        { "y2", 50 },
                                                        { "x2", 0 }
                                                    }
                                                },
                                                Opacity: 1f,
                                                Radius: 37,
                                                BoxShadow: "0px 0px #80808080"),
                                            new BackgroundGenerationTextResultLayer(
                                                3,
                                                "text",
                                                118,
                                                395,
                                                233,
                                                50,
                                                FontWeight: "Regular",
                                                FontSize: 27,
                                                Content: "只为不一样的你",
                                                FontUnderLine: false,
                                                LineHeight: 1,
                                                FontItalic: false,
                                                SubType: "SubTitle",
                                                FontColor: "#f1eeec",
                                                TextShadow: null,
                                                FontFamily: "阿里巴巴普惠体",
                                                Alignment: "center",
                                                Opacity: 1,
                                                FontLineThrough: false,
                                                Direction: "horizontal")
                                        })
                                })
                    },
                    new BackgroundGenerationUsage(2)));

        public static readonly RequestSnapshot<DashScopeTaskOperationResponse> CancelCompletedTask = new(
            "cancel-completed-task",
            new DashScopeTaskOperationResponse(
                "4d496c94-1389-9ca9-a92a-3e732f675686",
                "UnsupportedOperation",
                "Failed to cancel the task, please confirm if the task is in PENDING status."));

        public static readonly RequestSnapshot<DashScopeTaskList> ListTasks = new(
            "list-task",
            new DashScopeTaskList(
                "fcb29ae5-a352-9e7b-901c-e53525376cde",
                new[]
                {
                    new DashScopeTaskListItem(
                        "42677",
                        "1493478651020171",
                        "1493478651020171",
                        1709260684485,
                        1709260684527,
                        1709260685184,
                        "cn-beijing",
                        "db5ce040-4548-9919-9a75-3385ee152335",
                        DashScopeTaskStatus.Succeeded,
                        "6075262c-b56d-4968-9abf-2a9784a90f3e",
                        "apikey:v1:embeddings:text-embedding:text-embedding:text-embedding-async-v2",
                        "text-embedding-async-v2")
                },
                1,
                1,
                1,
                10));
    }
}
