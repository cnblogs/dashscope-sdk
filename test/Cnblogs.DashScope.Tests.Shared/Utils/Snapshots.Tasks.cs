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
                                new(
                                    "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/1d/d4/20240301/8d820c8d/4c48fa53-2907-499b-b9ac-76477fe8d299-1.png"),
                                new(
                                    "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/79/20240301/3ab595ad/aa3e6d8d-884d-4431-b9c2-3684edeb072e-1.png"),
                                new(
                                    "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/79/20240301/3ab595ad/aa3e6d8d-884d-4431-b9c2-3684edeb072e-1.png"),
                                new(
                                    "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/3d/20240301/3ab595ad/3fca748e-d491-458a-bb72-73649af33209-1.png")
                            },
                        TaskMetrics = new DashScopeTaskMetrics(4, 4, 0)
                    },
                    new ImageSynthesisUsage(4)));

        public static readonly RequestSnapshot<DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>>
            ImageGenerationSuccess = new(
                "get-task-image-generation-success",
                new DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>(
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
                                new(
                                    "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/viapi-video/2024-03-02/ac5d435a-9ea9-4287-8666-e1be7bbba943/20240302222213528791_style3_jxdf6o4zwy.jpg")
                            }
                    },
                    new MultimodalTokenUsage() { ImageCount = 1 }));

        public static readonly RequestSnapshot<DashScopeTask<Image2ImageSynthesisOutput, ImageSynthesisUsage>>
            ImageEditInstructionSuccess = new(
                "get-task-image-edit-instruction",
                new DashScopeTask<Image2ImageSynthesisOutput, ImageSynthesisUsage>(
                    "af40a813-8fea-90eb-9a7b-146694ffae25",
                    new Image2ImageSynthesisOutput
                    {
                        TaskId = "bbed0f78-5a64-4a2a-8efb-47a258a79c21",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 17, 23, 16, 44, 732),
                        ScheduledTime = new DateTime(2026, 11, 17, 23, 16, 44, 766),
                        EndTime = new DateTime(2026, 11, 17, 23, 17, 22, 884),
                        Results = new List<ImageSynthesisResult>
                        {
                            new(
                                Url: "https://dashscope-f4de.oss-accelerate.aliyuncs.com/1d/e5/20261117/867c2103/bbed0f78-5a64-4a2a-8efb-47a258a79c21.png?Expires=1795015042&OSSAccessKeyId=LTAI5tRcsWJEymQaTsKbKqGf&Signature=Xo9s6L7xqEV7JTqoGwEzWfsGdV8%3D",
                                OrigPrompt: "将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
                                ActualPrompt: "将图1中的闹钟提取并放置于图2餐桌花瓶的右侧，保持闹钟外观细节一致，调整光影与透视以自然融入场景，其余元素保持不变。")
                        },
                        TaskMetrics = new DashScopeTaskMetrics(1, 1, 0)
                    },
                    new ImageSynthesisUsage(1)));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            VideoT2vSuccess = new(
                "get-task-video-synthesis-t2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "ee118b91-79de-9f57-981d-e7a2d7e530ac",
                    new VideoSynthesisOutput
                    {
                        TaskId = "5ab34e25-bb2b-48b6-9fad-28744602785c",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 17, 23, 51, 29, 443),
                        ScheduledTime = new DateTime(2026, 11, 17, 23, 51, 29, 492),
                        EndTime = new DateTime(2026, 11, 17, 23, 53, 23, 931),
                        OrigPrompt = "一座由硬纸板和瓶盖搭建的微型城市，在夜晚焕发出生机。一列硬纸板火车缓缓驶过，小灯点缀其间，照亮前路。",
                        VideoUrl = "https://dashscope-a717.oss-accelerate.aliyuncs.com/1d/e3/20261117/9943c00a/33608624-metadata_5ab34e25-bb2b-48b6-9fad-28744602785c_output.mp4?Expires=1795017202&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=6WekXkTXC45Pz90cfvNqYwY9d3Y%3D"
                    },
                    new VideoSynthesisUsage(
                        Duration: 3,
                        InputVideoDuration: 0,
                        OutputVideoDuration: 3,
                        VideoCount: 1,
                        SR: 480,
                        Ratio: "16:9")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            VideoI2vSuccess = new(
                "get-task-video-synthesis-i2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "3613fbba-bd85-958b-9987-9d220bf5d933",
                    new VideoSynthesisOutput
                    {
                        TaskId = "0112b6d1-15ce-4697-9186-bee4ce7c8838",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 17, 23, 51, 30, 821),
                        ScheduledTime = new DateTime(2026, 11, 17, 23, 51, 30, 867),
                        EndTime = new DateTime(2026, 11, 17, 23, 52, 21, 618),
                        OrigPrompt = "一只猫在草地上奔跑",
                        VideoUrl = "https://dashscope-7c2c.oss-accelerate.aliyuncs.com/1d/a0/20261117/09c57414/21670930-metadata_0112b6d1-15ce-4697-9186-bee4ce7c8838_output.mp4?Expires=1795017139&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=oL8Iafui7GWja3MR42ZRjVcJYmo%3D"
                    },
                    new VideoSynthesisUsage(
                        Duration: 3,
                        InputVideoDuration: 0,
                        OutputVideoDuration: 3,
                        VideoCount: 1,
                        SR: 480)));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            Wan3T2vSuccess = new(
                "get-task-video-synthesis-wan3-t2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "86c2bf67-a850-906b-8129-098f8b961a61",
                    new VideoSynthesisOutput
                    {
                        TaskId = "21725ab8-8dfc-4242-b6e4-f0d7feaa06ab",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 0, 52, 42, 2),
                        ScheduledTime = new DateTime(2026, 11, 18, 0, 52, 42, 259),
                        EndTime = new DateTime(2026, 11, 18, 0, 53, 43, 404),
                        OrigPrompt = "一只小猫在月光下的屋顶上奔跑，城市的霓虹灯在远处闪烁，电影级画质，流畅运镜。",
                        VideoUrl = "https://dashscope-a717.oss-accelerate.aliyuncs.com/1d/8a/20261118/caee094c/799eb3175c9b42a38cc38b673d4c80fa-metadata_XrrIbb3V_c7ca0d23a496.mp4?Expires=1795020822&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=5rs8BOYZqVKQ%2Ba1W%2B1I7S4PZKAY%3D"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        InputVideoDuration: 0,
                        OutputVideoDuration: 5,
                        VideoCount: 1,
                        SR: 480,
                        Ratio: "16:9",
                        Fps: 30)));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            Wan27T2vSuccess = new(
                "get-task-video-synthesis-wan27-t2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "bf44cf6a-559d-968e-a292-b85c463b4c46",
                    new VideoSynthesisOutput
                    {
                        TaskId = "865dbcde-9cd1-403d-920a-f2e977968d14",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 0, 52, 50, 168),
                        ScheduledTime = new DateTime(2026, 11, 18, 0, 52, 50, 222),
                        EndTime = new DateTime(2026, 11, 18, 0, 54, 8, 660),
                        OrigPrompt = "一只小猫在月光下奔跑",
                        VideoUrl = "https://dashscope-f4de.oss-accelerate.aliyuncs.com/1d/5d/20261118/cd881a97/10a549f8e6284e01bef9682d62064e5b-metadata_user_d951814c59736de4.mp4?Expires=1795020847&OSSAccessKeyId=LTAI5tRcsWJEymQaTsKbKqGf&Signature=2q72B%2BR4VB7sENPL5xZNDTWueUA%3D"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        InputVideoDuration: 0,
                        OutputVideoDuration: 5,
                        VideoCount: 1,
                        SR: 720,
                        Ratio: "9:16")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            Wan26I2vSuccess = new(
                "get-task-video-synthesis-wan26-i2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "65fa2505-3e8d-9a89-8bfc-43b3a928acf2",
                    new VideoSynthesisOutput
                    {
                        TaskId = "6732b64a-4754-4077-bd7c-dbf291e8b313",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 0, 53, 7, 876),
                        ScheduledTime = new DateTime(2026, 11, 18, 0, 53, 7, 924),
                        EndTime = new DateTime(2026, 11, 18, 0, 54, 47, 506),
                        OrigPrompt = "一只由喷漆画成的少年从混凝土墙上活过来，摆出充满活力的说唱歌手姿势。",
                        VideoUrl = "https://dashscope-f4de.oss-accelerate.aliyuncs.com/1d/e2/20261118/87ff3641/3629f8f9bc07481b81d26ef3867a8bb3-metadata_9b7a89ab75ac0f2b.mp4?Expires=1795020886&OSSAccessKeyId=LTAI5tRcsWJEymQaTsKbKqGf&Signature=DMVJKbW%2BL66rlgqfF3hRBTjmsXU%3D"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        InputVideoDuration: 0,
                        OutputVideoDuration: 5,
                        VideoCount: 1,
                        SR: 720,
                        Audio: false)));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            VaceImageReferenceSuccess = new(
                "get-task-video-synthesis-vace-image-reference",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "28f675cd-2d44-98cf-b4ec-8f6807560592",
                    new VideoSynthesisOutput
                    {
                        TaskId = "c7c80870-110d-42a8-a1db-5fb333946c28",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 0, 53, 20, 433),
                        ScheduledTime = new DateTime(2026, 11, 18, 0, 53, 20, 482),
                        EndTime = new DateTime(2026, 11, 18, 0, 57, 19, 768),
                        OrigPrompt = "视频中，一位女孩自晨雾缭绕的古老森林深处款款走出，她步伐轻盈，镜头捕捉她每一个灵动瞬间。当女孩站定，环顾四周葱郁林木时，她脸上绽放出惊喜与喜悦交织的笑容。",
                        ActualPrompt = "黎明，柔光，侧光，低饱和度，冷色调，中景，中心构图，日光透过薄雾漫射，湛蓝色天空隐约可见。一位亚裔少女从晨雾弥漫的古老森林中缓步走出，约十七岁，黑发微卷垂至肩下，身穿米白色针织开衫与浅灰长裙，赤足踏在覆着青苔的湿润石径上。她脚步轻盈，裙摆随动微扬，落叶在脚边轻轻旋起。站定后微微仰首，双臂自然垂落，嘴角上扬，眼神清亮，凝望左侧一株枝干虬劲的百年橡树。林间光柱斜洒，雾气氤氲浮动，蕨类与苔藓细节清晰，背景层叠幽深，远景隐现朦胧树冠轮廓。",
                        VideoUrl = "https://dashscope-result-wlcb-acdr-1.oss-cn-wulanchabu-acdr-1.aliyuncs.com/1d/fc/20261118/bb428675/c7c80870-110d-42a8-a1db-5fb333946c28.mp4?Expires=1795021038&OSSAccessKeyId=LTAI5tKPD3TMqf2Lna1fASuh&Signature=IKJyK6tYtGBnosD77PXxJKvsg%2FU%3D"
                    },
                    new VideoSynthesisUsage(
                        VideoDuration: 5,
                        VideoRatio: "standard",
                        VideoCount: 1)));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            VaceVideoRepaintingSuccess = new(
                "get-task-video-synthesis-vace-video-repainting",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "82524ed0-eeb0-9a71-af1a-9cf8a7f88cce",
                    new VideoSynthesisOutput
                    {
                        TaskId = "fec80098-b52d-47ee-a709-cf1070ce7b72",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 0, 59, 59, 639),
                        ScheduledTime = new DateTime(2026, 11, 18, 1, 0, 8, 414),
                        EndTime = new DateTime(2026, 11, 18, 1, 3, 8, 229),
                        OrigPrompt = "视频展示了一辆黑色的蒸汽朋克风格汽车，绅士驾驶着，车辆装饰着齿轮和铜管。背景是蒸汽驱动的糖果工厂和复古元素，画面复古与趣味。",
                        ActualPrompt = "",
                        VideoUrl = "https://dashscope-result-wlcb-acdr-1.oss-cn-wulanchabu-acdr-1.aliyuncs.com/1d/0e/20261118/bb428675/fec80098-b52d-47ee-a709-cf1070ce7b72.mp4?Expires=1795021387&OSSAccessKeyId=LTAI5tKPD3TMqf2Lna1fASuh&Signature=9q2Z0PHjZN6wOo33RQcg0eB60HM%3D"
                    },
                    new VideoSynthesisUsage(
                        VideoDuration: 5,
                        VideoRatio: "standard",
                        VideoCount: 1)));

        public static readonly RequestSnapshot<DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>>
            Kf2vSuccess = new(
                "get-task-image2video-synthesis-kf2v",
                new DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>(
                    "c2c979d2-24e7-9fc2-ada2-f367361f30be",
                    new Image2VideoSynthesisOutput
                    {
                        TaskId = "5e0ddd64-f136-48bf-9515-cbb0175f36f2",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 0, 53, 25, 553),
                        ScheduledTime = new DateTime(2026, 11, 18, 0, 53, 25, 636),
                        EndTime = new DateTime(2026, 11, 18, 0, 53, 39, 952),
                        OrigPrompt = "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。",
                        VideoUrl = "https://dashscope-7c2c.oss-cn-shanghai.aliyuncs.com/1d/eb/20261118/d19149ec/5e0ddd64-f136-48bf-9515-cbb0175f36f2.mp4?Expires=1795020819&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=l93XfvpeOe%2BSYYRM4zxgtYfWe8E%3D"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        VideoCount: 1,
                        SR: 480)));

        public static readonly RequestSnapshot<DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>>
            AnimateMoveSuccess = new(
                "get-task-image2video-synthesis-animate-move",
                new DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>(
                    "2ea68517-bbb2-956b-a903-46b2f52b132d",
                    new Image2VideoSynthesisOutput
                    {
                        TaskId = "80d7a233-1eb7-4888-8fa3-dbc66e544dd9",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 0, 53, 27, 901),
                        ScheduledTime = new DateTime(2026, 11, 18, 0, 53, 36, 245),
                        EndTime = new DateTime(2026, 11, 18, 0, 56, 40, 411),
                        Results = new Image2VideoSynthesisResult(
                            "http://dashscope-7c2c.oss-cn-shanghai.aliyuncs.com/1d/80/20261118/fbc40270/80d7a233-1eb7-4888-8fa3-dbc66e544dd9.mp4?Expires=1795021000&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=ULN15PjLvg2ee%2BYnBN4hZDuVsS0%3D")
                    },
                    new VideoSynthesisUsage(
                        VideoDuration: 5.533333333333333,
                        VideoRatio: "standard")));

        public static readonly RequestSnapshot<DashScopeTask<Image2ImageSynthesisOutput, ImageSynthesisUsage>>
            ImageEditFunctionSuccess = new(
                "get-task-image-edit-function",
                new DashScopeTask<Image2ImageSynthesisOutput, ImageSynthesisUsage>(
                    "85203e20-78fc-9422-812e-0e0f99982277",
                    new Image2ImageSynthesisOutput
                    {
                        TaskId = "40a6b9bb-720c-40a1-b09f-fcde713d2d9f",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 17, 23, 10, 7, 940),
                        ScheduledTime = new DateTime(2026, 11, 17, 23, 10, 7, 989),
                        EndTime = new DateTime(2026, 11, 17, 23, 10, 23, 583),
                        Results = new List<ImageSynthesisResult>
                        {
                            new(
                                "https://dashscope-5859.oss-cn-wulanchabu-acdr-1.aliyuncs.com/1d/ca/20261117/dbdda9df/7d1f1de6-7473-4b45-bbc2-178803a1926c2722040109.png?Expires=1795014623&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=ANlWYP9gFIlSU07QE8atNjvK1UI%3D")
                        },
                        TaskMetrics = new DashScopeTaskMetrics(1, 1, 0)
                    },
                    new ImageSynthesisUsage(1)));

        public static readonly RequestSnapshot<DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>>
            Wan26T2iSuccess = new(
                "get-task-wan26-t2i",
                new DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>(
                    "c18826c5-e66b-95b2-b635-08576517b82c",
                    new ImageGenerationOutput
                    {
                        TaskId = "9d96f6fa-6713-4a9a-8f57-a4a34bdc93e2",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 17, 21, 21, 29, 551),
                        ScheduledTime = new DateTime(2026, 11, 17, 21, 21, 29, 596),
                        EndTime = new DateTime(2026, 11, 17, 21, 21, 37, 366),
                        Choices =
                            new List<MultimodalChoice>
                            {
                                new(
                                    "stop",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>()
                                        {
                                            MultimodalMessageContent.ImageContent(
                                                "https://dashscope-a717.oss-accelerate.aliyuncs.com/1d/f4/20261117/3c51cb34/4951352-7dVipNsc_6bd120c2813c.png?Expires=1795008096&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=a9K2hO2w1RM4LrUKMbZLX5l9AJ4%3D")
                                        }))
                            }
                    },
                    new MultimodalTokenUsage() { ImageCount = 1, TotalTokens = 0 }));

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
                                new(
                                    "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100905_0_02dc0bba-8b1d-4648-8b95-eb2b92fe715d.png"),
                                new(
                                    "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100905_1_e1af86ec-152a-4ebe-b2a0-b40a592043b2.png")
                            },
                        TaskMetrics = new DashScopeTaskMetrics(2, 2, 0),
                        TextResults =
                            new BackgroundGenerationTextResult(
                                new List<BackgroundGenerationTextResultUrl>
                                {
                                    new(
                                        "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100901_0_4645005c-713d-4e92-9629-b12cbe5f3671.png"),
                                    new(
                                        "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100901_1_b1979b75-c553-4d9b-9c9f-80f401a0d124.png")
                                },
                                new List<BackgroundGenerationTextResultParams>
                                {
                                    new(
                                        0,
                                        new List<BackgroundGenerationTextResultLayer>
                                        {
                                            new(
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
                                                        new(
                                                            "#521b0800",
                                                            0),
                                                        new(
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
                                            new(
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
                                            new(
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
                                                        new(
                                                            "#e6baa7ff",
                                                            0),
                                                        new(
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
                                            new(
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
                                    new(
                                        1,
                                        new List<BackgroundGenerationTextResultLayer>
                                        {
                                            new(
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
                                                        new(
                                                            "#efeae400",
                                                            0),
                                                        new(
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
                                            new(
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
                                            new(
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
                                                        new(
                                                            "#421f12ff",
                                                            0),
                                                        new(
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
                                            new(
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
