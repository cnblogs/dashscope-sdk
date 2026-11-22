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

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            Canceled = new(
                "get-task-canceled",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "2df539f3-284f-9572-ae69-a9afc8685ee5",
                    new VideoSynthesisOutput
                    {
                        TaskId = "b85af63a-643f-4733-bbc5-9a8e2452f5e7",
                        TaskStatus = DashScopeTaskStatus.Canceled,
                        SubmitTime = new DateTime(2026, 11, 18, 14, 6, 41, 112)
                    }));

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
                        SR: "480",
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
                        SR: "480")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            StyleTransformSuccess = new(
                "get-task-video-synthesis-style-transform",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "1266b4b8-53b2-99c6-8183-1bf3d7b31352",
                    new VideoSynthesisOutput
                    {
                        TaskId = "c345318f-da61-4e02-a7d8-126e0c9cd11a",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 13, 41, 41, 548),
                        ScheduledTime = new DateTime(2026, 11, 18, 13, 41, 41, 609),
                        EndTime = new DateTime(2026, 11, 18, 13, 43, 38, 442),
                        OutputVideoUrl = "https://vigen-invi.oss-cn-shanghai.aliyuncs.com/service_dashscope/VideoStyle/2026-11-18/public/8272f6d6-8f1a-487e-adcd-20b2a4e32ea3/result-4271cd31-2556-457f-8d31-7c3b8ef2423c.mp4?OSSAccessKeyId=LTAI5t7aiMEUzu1F2xPMCdFj&Expires=1795067018&Signature=DCHgT7Ej8iL78HAsXlFPgQTQqyM%3D"
                    },
                    new VideoSynthesisUsage(
                        Duration: 3,
                        SR: "540")));

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
                        SR: "480",
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
                        SR: "720",
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
                        SR: "720",
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
                        ActualPrompt = string.Empty,
                        VideoUrl = "https://dashscope-result-wlcb-acdr-1.oss-cn-wulanchabu-acdr-1.aliyuncs.com/1d/0e/20261118/bb428675/fec80098-b52d-47ee-a709-cf1070ce7b72.mp4?Expires=1795021387&OSSAccessKeyId=LTAI5tKPD3TMqf2Lna1fASuh&Signature=9q2Z0PHjZN6wOo33RQcg0eB60HM%3D"
                    },
                    new VideoSynthesisUsage(
                        VideoDuration: 5,
                        VideoRatio: "standard",
                        VideoCount: 1)));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            PixverseT2vSuccess = new(
                "get-task-video-synthesis-pixverse-t2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "bb43147a-27e6-96e7-ac7c-69bcc44ea265",
                    new VideoSynthesisOutput
                    {
                        TaskId = "ec3c3226-79c9-4b2a-8fb2-0cf79d26a23b",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 10, 50, 37, 870),
                        ScheduledTime = new DateTime(2026, 11, 19, 10, 50, 37, 951),
                        EndTime = new DateTime(2026, 11, 19, 10, 51, 20, 764),
                        OrigPrompt = "下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。",
                        VideoUrl = "https://media.pixverseai.cn/pixverse%2Fmp4%2Fmedia%2Fweb%2Fec3c3226-79c9-4b2a-8fb2-0cf79d26a23b_seed12345.mp4"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        VideoCount: 1,
                        Audio: true,
                        ShotType: "single",
                        Size: "1280*720",
                        Fps: 24,
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            PixverseI2vSuccess = new(
                "get-task-video-synthesis-pixverse-i2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "78415d1b-e4f7-94cf-a7b4-774b92294f8a",
                    new VideoSynthesisOutput
                    {
                        TaskId = "17e1b649-6094-4200-a638-a1603fcc26a3",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 10, 50, 38, 57),
                        ScheduledTime = new DateTime(2026, 11, 19, 10, 50, 38, 102),
                        EndTime = new DateTime(2026, 11, 19, 10, 51, 30, 810),
                        OrigPrompt = "镜头从海龟下方缓缓上移，海龟悠然游动，腹部细节清晰可见。",
                        VideoUrl = "https://media.pixverseai.cn/pixverse%2Fmp4%2Fmedia%2Fweb%2F17e1b649-6094-4200-a638-a1603fcc26a3_seed1304339824.mp4"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        VideoCount: 1,
                        Audio: false,
                        ShotType: "single",
                        Size: "992*944",
                        Fps: 24,
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            PixverseKf2vSuccess = new(
                "get-task-video-synthesis-pixverse-kf2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "8099ec19-71b0-9759-8b7f-b754af4bc0c9",
                    new VideoSynthesisOutput
                    {
                        TaskId = "763016e3-a0f5-49f8-8af0-edc884e3fcf6",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 10, 50, 38, 174),
                        ScheduledTime = new DateTime(2026, 11, 19, 10, 50, 38, 227),
                        EndTime = new DateTime(2026, 11, 19, 10, 51, 28, 164),
                        OrigPrompt = "一只小猫从窗台向下跳跃，轻盈地落在沙发上，然后好奇地环顾四周。",
                        VideoUrl = "https://media.pixverseai.cn/pixverse%2Fmp4%2Fmedia%2Fweb%2F763016e3-a0f5-49f8-8af0-edc884e3fcf6_seed1776940619.mp4"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        VideoCount: 1,
                        Audio: false,
                        Size: "1104*848",
                        Fps: 24,
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            PixverseR2vOmniSuccess = new(
                "get-task-video-synthesis-pixverse-r2v-omni",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "776612aa-87e6-9a6c-8ec4-8e338c94fc7e",
                    new VideoSynthesisOutput
                    {
                        TaskId = "f912ec38-68f1-440f-9aa7-f58b43c0c7d5",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 10, 50, 38, 283),
                        ScheduledTime = new DateTime(2026, 11, 19, 10, 50, 38, 328),
                        EndTime = new DateTime(2026, 11, 19, 10, 53, 48, 949),
                        OrigPrompt = "@1 在@2 中弹吉他 参考@3",
                        VideoUrl = "https://media.pixverseai.cn/pixverse/mp4/media/web/ori/be5a1862-48c1-4200-b22b-de687ee74100_seed1018789942.mp4"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        VideoCount: 1,
                        Audio: true,
                        Size: "1280*720",
                        Fps: 24,
                        ReferenceType: "video",
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            PixverseLipsyncSuccess = new(
                "get-task-video-synthesis-pixverse-lipsync",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "6156f62c-b5a7-9888-95a5-a0dbfcc0d33c",
                    new VideoSynthesisOutput
                    {
                        TaskId = "32b7a273-f904-45d7-b91f-9e9e30edf304",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 10, 57, 57, 346),
                        ScheduledTime = new DateTime(2026, 11, 19, 10, 57, 57, 406),
                        EndTime = new DateTime(2026, 11, 19, 10, 58, 35, 662),
                        VideoUrl = "https://media.pixverseai.cn/pixverse/mp4/media/web/b826cb98-4acd-4780-9377-0e4943ecdff7_seed0.mp4"
                    },
                    new VideoSynthesisUsage(
                        Duration: 4,
                        VideoCount: 1,
                        Size: "1080*1920",
                        Fps: 24,
                        SR: string.Empty)));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            PixverseMotionControlSuccess = new(
                "get-task-video-synthesis-pixverse-motioncontrol",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "43feccd8-729a-94de-8e45-cb6c4bba53d7",
                    new VideoSynthesisOutput
                    {
                        TaskId = "929ff38c-3f30-47ac-b1f8-96734fdfd212",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 10, 57, 57, 532),
                        ScheduledTime = new DateTime(2026, 11, 19, 10, 57, 57, 580),
                        EndTime = new DateTime(2026, 11, 19, 10, 59, 1, 971),
                        VideoUrl = "https://media.pixverseai.cn/pixverse/mp4/media/web/1754dc88-7141-4637-a9f6-40fb5bd1dd2d_seed571987627.mp4"
                    },
                    new VideoSynthesisUsage(
                        Duration: 6,
                        VideoCount: 1,
                        Size: "1280*704",
                        Fps: 24,
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            PixverseUpscaleSuccess = new(
                "get-task-video-synthesis-pixverse-upscale",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "b46ea5fa-2ffc-9d5b-b1f8-168aaa5f8f47",
                    new VideoSynthesisOutput
                    {
                        TaskId = "2e4c6c6d-9223-4899-ab45-9594b6db8721",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 10, 57, 57, 632),
                        ScheduledTime = new DateTime(2026, 11, 19, 10, 57, 57, 694),
                        EndTime = new DateTime(2026, 11, 19, 10, 58, 29, 988),
                        VideoUrl = "https://media.pixverseai.cn/pixverse/mp4/media/web/6c15464c-e9b4-4cb8-ad75-37a012b93799_seed12345.mp4"
                    },
                    new VideoSynthesisUsage(
                        Duration: 6,
                        VideoCount: 1,
                        Size: "3840*2156",
                        Fps: 24,
                        SR: "4K")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            KlingT2vSuccess = new(
                "get-task-video-synthesis-kling-t2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "cee41c8a-8340-95f6-abc9-1196b84b395d",
                    new VideoSynthesisOutput
                    {
                        TaskId = "aa1eb0aa-7966-4d06-a5c1-fe2f6062e95e",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 11, 42, 11, 60),
                        ScheduledTime = new DateTime(2026, 11, 19, 11, 42, 11, 97),
                        EndTime = new DateTime(2026, 11, 19, 11, 45, 38, 427),
                        VideoUrl = "https://v4-fdl.kechuangai.com/ksc2/L25npD48RZSRKFPEhutjcjFHd2WKpYV-_P9LpMHoyqgYOtCYhv2ImJb1QuKEgP9tQorWmHd-0nPbXvrdy-A-ZmyMuDFrdgm3ngZKrsiLXrBFsyQZxd_SHQWw0WMMpchJA15ikMhEi2VS9LAzuCGg4VMW3xWxgiBDVmb0VqvOF_x1Ja__0bIuWiAQ4VjTl2GU72avFJ3zcTymmNzDxVgtHw.mp4?cacheKey=ChtzZWN1cml0eS5rbGluZy5tZXRhX2VuY3J5cHQSsAHsSJKVhMmxeWCu2ROBeEzRfXETiY57G2nyVgDIYiVocFq9zAIjfygJz1kloguAJx_12ir2zzQBMEythLO5iURobmLdWPUOi0iqRbkx3KH7p-6d0qsabazcoHdD3Awiz0bQELs3OP2mm7r2J2litbS5aDDeoxLp3hjXqhenvj4SqgiCMFM7PNi_TKIbCeWefHCNmlQoDq84z-UZNRNJG_CiMca1zpPRU2xYh6IbTZo4XxoSLL46pEJbTw3u2hySdPDYoj5mIiDVUCHG8G74-SvgSprmjbjVUqulD7xIJusIEXr3FtScpygFMAE&x-kcdn-pid=112757&pkey=AAWjruSK2QK18q9vBU1xcoKLXoPuTYmrAA4zPf5QYQ7mpF62NqycHN982LHqiEAC6XWOMww1FWcWfrln-C-kyNsjr9KE2KmbIaA8WSYpXA3Fwn12PfObM0f5rjGimEQS-Tk",
                        WatermarkVideoUrl = "https://v4-fdl.kechuangai.com/ksc2/yo6nh4voRc1uBULV6P3ZEbCVTwTu6PEmiXsyO11ZtS3Zk25d2izVFh7du5D2brKwBsu1b1lT1fS4ZwWfM3ujovT5YZBc45LsUNmJJIMhcAZHY9XfhQe90DedOU4U4q_hPluxm8xePJcNDLu3KLJJ0f479XQEwB_2wIQXNxSj-kY_6icspJb9ubjmuVUtB_gpyA-IVPzq7vTHWeWfmdc2RA.mp4?cacheKey=ChtzZWN1cml0eS5rbGluZy5tZXRhX2VuY3J5cHQSsAFAqVcbpKZfcO1975EREZ9qH82D78wAWI1sLS23bduEVSTAxM_Gl4yGriWLh5tjY-cHP9S_YyW-F2T4sbeUhcBycWCiDj7hLeoP1WB-2iXdKdoRl4fExv9iOThHJbli9SCBWCpdfJzKCxv0k_rVxBGDr2xWLLP19E6VBGVJhfZqC3eu-ZxsFPWyWtOtedkJP0qMCI0SLJ0v-_rmR8S53Jo3zLzlhzeDoLKkylV7N9qBVRoSf6vJMx190TgGD4GP2nZFxCPDIiC651sNPTyQufHEp-cM2rCUMYncBiprc0SFRuNw6GXEWygFMAE&x-kcdn-pid=112757&pkey=AAUd_Bq5GbJmhvaskHMLEqGkgGHX7dqzU99ggkvTWvjonXWK0SD2hvIrPk43aKYf9sj1BUbqGFUIBOzvDMq9uS9nohUjHr31k4JkH0Y8ZHWaaoucbnbb_6lLs1CNpilND7g",
                    },
                    new VideoSynthesisUsage(
                        Duration: 3,
                        VideoCount: 1,
                        Size: "1280*720",
                        Fps: 24,
                        Audio: false,
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            KlingR2vSuccess = new(
                "get-task-video-synthesis-kling-r2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "bbd0baa7-f311-91df-80b3-a836cae9cb37",
                    new VideoSynthesisOutput
                    {
                        TaskId = "791b5968-137b-4053-9903-6ab73e692d7d",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 11, 42, 11, 211),
                        ScheduledTime = new DateTime(2026, 11, 19, 11, 42, 11, 247),
                        EndTime = new DateTime(2026, 11, 19, 11, 44, 8, 159),
                        VideoUrl = "https://v2-fdl.kechuangai.com/ksc2/3ANfNgRF5_uta5CzcCFb0E5O3HGZksaDo5sBxgclgzaM4t3kWQUdgE3yZVEhT4TK-j3quC-rQemzYeYEZosTNAPvvycYXiXuqOqvCZev4duq81glcAFCvmjGb0XzGqEnA9mukUVmH3_YOV1mBCuNj3SpSRwX6nXnmUUj2abQ2GEN3JT7Uqh9h21rM1AEKJ7-NLEf5vNUcO3xNWiiaEpm7w.mp4?cacheKey=ChtzZWN1cml0eS5rbGluZy5tZXRhX2VuY3J5cHQSsAFZzvgiVtdeyN2HZlvx7viIsNjcvQFuoNnzmwWYqLybf1geu6VBUQtrDrlDfPc5CjOrAZJM6MYfEdYVH1e1IQ_apN9K8P0KkSR0Lhm1IVFEhyNQOwmt_1xHreu-r-5ILyy8-kVi6LtRTkN4sMuUEYIdR-8XHw0cBedgEO_16TFV6ySaHp7CysYI7bTem1wkQknl9KegzBN-eUbJAqyTnaVS5FBNoNVKHNh52on0djzfjhoSiK-SiyTQu2yvu8rTuMxr8vOaIiAhCxfnAZ51JvlqP7OLIG1UnNBOjoCPPlOvp9rTMI-7HSgFMAE&x-kcdn-pid=112757&pkey=AAUSAZ4xRadHdS245XNG1xUi_m1RQWgVHGvLorL_Cy-EJ8vGYbQEWJGrSetyXa8PxEGos14uMxCJIGgNP322qrJr7W6472JZCh4lftEpwY4uWuNUKXcBsxeo4jdmYRJJPnY"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        VideoCount: 1,
                        Size: "1280*720",
                        Fps: 24,
                        Audio: false,
                        ReferenceType: "video",
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            ViduT2vSuccess = new(
                "get-task-video-synthesis-vidu-t2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "1d2772f5-d216-98d3-94d1-ab60a9897d41",
                    new VideoSynthesisOutput
                    {
                        TaskId = "4942eaa6-5145-4c6d-bf7c-cbeb8bf21d9d",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 11, 42, 11, 325),
                        ScheduledTime = new DateTime(2026, 11, 19, 11, 42, 11, 481),
                        EndTime = new DateTime(2026, 11, 19, 11, 42, 54, 663),
                        OrigPrompt = "一只小猫在月光下的屋顶上奔跑，城市的霓虹灯在远处闪烁，电影级画质，流畅运镜。",
                        VideoUrl = "https://prod-ss-vidu.s3.oss-cn-beijing.aliyuncs.com/infer_04/tasks/26/1119/03/1020844169716834304/creation-01/final_video.mp4?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=LTAI5t9wejmA3ETdEwa19PH5%2F20261119%2Fcn-beijing%2Fs3%2Faws4_request&X-Amz-Date=20261119T034253Z&X-Amz-Expires=86400&X-Amz-SignedHeaders=host&response-cache-control=max-age%3D86400&response-content-disposition=attachment%3Bfilename%3Dgeneral-6-2026-11-19T03%253A42%253A40Z.mp4&x-id=GetObject&X-Amz-Signature=d5081809e800b449aa2053da48ad3d71cb09d48f5fdcd1a3ef761e21c25e0dbe"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        OutputVideoDuration: 5,
                        VideoCount: 1,
                        Size: "1280*720",
                        Fps: 24,
                        Audio: false,
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            ViduI2vSuccess = new(
                "get-task-video-synthesis-vidu-i2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "58559042-4dac-9b62-b330-4b4c376979ce",
                    new VideoSynthesisOutput
                    {
                        TaskId = "b509858a-8290-47d7-9b6d-01a351b16f8d",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 11, 42, 11, 437),
                        ScheduledTime = new DateTime(2026, 11, 19, 11, 42, 11, 483),
                        EndTime = new DateTime(2026, 11, 19, 11, 42, 44, 673),
                        OrigPrompt = "镜头从海龟下方缓缓上移，海龟悠然游动，腹部细节清晰可见。",
                        VideoUrl = "https://prod-ss-vidu.s3.oss-cn-beijing.aliyuncs.com/infer_08/tasks/26/1119/03/1020844169716830208/creation-01/final_video.mp4?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=LTAI5t9wejmA3ETdEwa19PH5%2F20261119%2Fcn-beijing%2Fs3%2Faws4_request&X-Amz-Date=20261119T034244Z&X-Amz-Expires=86400&X-Amz-SignedHeaders=host&response-cache-control=max-age%3D86400&response-content-disposition=attachment%3Bfilename%3Dgeneral-6-2026-11-19T03%253A42%253A42Z.mp4&x-id=GetObject&X-Amz-Signature=09f27f0f1b6683c7e4782e766a63899ff26f95d46016b6c9dc284f7ae52c49fe"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        OutputVideoDuration: 5,
                        VideoCount: 1,
                        Size: "988*932",
                        Fps: 24,
                        Audio: false,
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            ViduKf2vSuccess = new(
                "get-task-video-synthesis-vidu-kf2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "a18bace6-4c3b-950f-9dc3-d40b192245a7",
                    new VideoSynthesisOutput
                    {
                        TaskId = "e7542177-6790-48c9-a2cf-90b811a26a65",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 11, 42, 11, 549),
                        ScheduledTime = new DateTime(2026, 11, 19, 11, 42, 11, 605),
                        EndTime = new DateTime(2026, 11, 19, 11, 43, 7, 886),
                        OrigPrompt = "一只小猫从窗台向下跳跃，轻盈地落在沙发上，然后好奇地环顾四周。",
                        VideoUrl = "https://prod-ss-vidu.s3.oss-cn-beijing.aliyuncs.com/infer_28/tasks/26/1119/03/1020844170610249728/creation-01/final_video.mp4?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=LTAI5t9wejmA3ETdEwa19PH5%2F20261119%2Fcn-beijing%2Fs3%2Faws4_request&X-Amz-Date=20261119T034306Z&X-Amz-Expires=86400&X-Amz-SignedHeaders=host&response-cache-control=max-age%3D86400&response-content-disposition=attachment%3Bfilename%3Dgeneral-6-2026-11-19T03%253A42%253A59Z.mp4&x-id=GetObject&X-Amz-Signature=dd0758a2d8ad353c19f2b22bd5763a604d58635cf1d4697fc5f5042f0f796e28"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        OutputVideoDuration: 5,
                        VideoCount: 1,
                        Size: "1106*832",
                        Fps: 24,
                        Audio: false,
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            ViduR2vSuccess = new(
                "get-task-video-synthesis-vidu-r2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "33f2be4f-6751-95ac-8a02-c44b51e3531b",
                    new VideoSynthesisOutput
                    {
                        TaskId = "b2f40c2b-7b1e-461d-8c37-9364b5ae72e2",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 11, 42, 11, 663),
                        ScheduledTime = new DateTime(2026, 11, 19, 11, 42, 11, 738),
                        EndTime = new DateTime(2026, 11, 19, 11, 43, 36, 908),
                        OrigPrompt = "参考视频中的男人坐在靠窗的椅子上，手持吉他，在咖啡厅旁演奏一首舒缓的美国乡村民谣。",
                        VideoUrl = "https://prod-ss-vidu.s3.oss-cn-beijing.aliyuncs.com/infer_40/tasks/26/1119/03/1020844170715095040/creation-01/video.mp4?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=LTAI5t9wejmA3ETdEwa19PH5%2F20261119%2Fcn-beijing%2Fs3%2Faws4_request&X-Amz-Date=20261119T034336Z&X-Amz-Expires=86400&X-Amz-SignedHeaders=host&response-cache-control=max-age%3D86400&response-content-disposition=attachment%3Bfilename%3Dgeneral-6-2026-11-19T03%253A43%253A36Z.mp4&x-id=GetObject&X-Amz-Signature=9985bd3a9b879fdff741034543fd04ca5a6359662b230b778ad3d15e226ee458"
                    },
                    new VideoSynthesisUsage(
                        Duration: 5,
                        OutputVideoDuration: 5,
                        VideoCount: 1,
                        Size: "1280*720",
                        Fps: 24,
                        Audio: false,
                        ReferenceType: "image,video",
                        SR: "720")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            MinimaxT2vSuccess = new(
                "get-task-video-synthesis-minimax-t2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "4d6c8bc0-ffe2-9bdf-8ca6-99712a1f7805",
                    new VideoSynthesisOutput
                    {
                        TaskId = "608c6e28-a467-49df-b373-20ad6a3fd566",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 11, 42, 11, 768),
                        ScheduledTime = new DateTime(2026, 11, 19, 11, 42, 11, 822),
                        EndTime = new DateTime(2026, 11, 19, 11, 43, 42, 322),
                        VideoUrl = "https://algeng-video-infer.oss-cn-shanghai.aliyuncs.com/inference_output%2Frollout%2F2026-11-19%2Fc7fd8fe9-6295-4d7b-91a5-9a935639dbc6%2Foutput_aigc.mp4?Expires=1795146221&OSSAccessKeyId=LTAI5tA8yjKg7HvBRDpzZ8Nk&Signature=R6NCT95pGeuWZ5twe9IsGZuUmQY%3D"
                    },
                    new VideoSynthesisUsage(
                        Duration: 4,
                        InputSeconds: 0,
                        OutputSeconds: 4,
                        TotalSeconds: 4,
                        ImageCount: 0,
                        VideoCount: 1,
                        Size: "1344*768",
                        SR: "768")));

        public static readonly RequestSnapshot<DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>>
            MinimaxR2vSuccess = new(
                "get-task-video-synthesis-minimax-r2v",
                new DashScopeTask<VideoSynthesisOutput, VideoSynthesisUsage>(
                    "c480b6a6-2a6c-9fb8-a4d9-4a57244e9229",
                    new VideoSynthesisOutput
                    {
                        TaskId = "f791c844-5c97-4a16-aa70-18b42a99df34",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 19, 11, 42, 11, 876),
                        ScheduledTime = new DateTime(2026, 11, 19, 11, 42, 11, 940),
                        EndTime = new DateTime(2026, 11, 19, 11, 46, 16, 209),
                        VideoUrl = "https://algeng-video-infer.oss-cn-shanghai.aliyuncs.com/inference_output%2Frollout%2F2026-11-19%2F03a992ee-26d2-468d-9603-9f524e1e83e0%2Foutput_aigc.mp4?Expires=1795146375&OSSAccessKeyId=LTAI5tA8yjKg7HvBRDpzZ8Nk&Signature=sZSas%2Bwvm3lSjtNeQQJPYkvh5YM%3D"
                    },
                    new VideoSynthesisUsage(
                        Duration: 10,
                        InputSeconds: 5,
                        OutputSeconds: 5,
                        TotalSeconds: 10,
                        ImageCount: 0,
                        VideoCount: 1,
                        Size: "1344*768",
                        SR: "768")));

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
                        SR: "480")));

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

        public static readonly RequestSnapshot<DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>>
            S2vSuccess = new(
                "get-task-image2video-synthesis-s2v",
                new DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>(
                    "e0d72066-bb2b-9617-a331-436eb9f7788a",
                    new Image2VideoSynthesisOutput
                    {
                        TaskId = "cc7c958d-6c4e-48e8-a7dc-2f1a24d448c9",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 12, 26, 4, 613),
                        ScheduledTime = new DateTime(2026, 11, 18, 12, 26, 14, 457),
                        EndTime = new DateTime(2026, 11, 18, 12, 29, 9, 72),
                        Results = new Image2VideoSynthesisResult(
                            "http://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/1d/26/20261118/default/547a6d7d2d97408783fe93e1c614e98d_wologo.mp4?Expires=1795062548&OSSAccessKeyId=LTAI5tGx7yvUcG32VzcwNgQ6&Signature=35A31jMjFLVI6%2BvyA%2B%2BX7nsBoiQ%3D")
                    },
                    new VideoSynthesisUsage(
                        Duration: 18.13,
                        Size: "512*640",
                        Fps: 16,
                        VideoCount: 1,
                        SR: "480")));

        public static readonly RequestSnapshot<DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>>
            VideoretalkSuccess = new(
                "get-task-image2video-synthesis-videoretalk",
                new DashScopeTask<Image2VideoSynthesisOutput, VideoSynthesisUsage>(
                    "6ec19dce-c2f2-9274-9690-0bf91f633693",
                    new Image2VideoSynthesisOutput
                    {
                        TaskId = "8565bf52-49b3-4884-9b1e-e13a76faa59b",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 18, 13, 21, 59, 789),
                        ScheduledTime = new DateTime(2026, 11, 18, 13, 22, 9, 327),
                        EndTime = new DateTime(2026, 11, 18, 13, 23, 3, 760),
                        VideoUrl = "http://dashscope-7c2c.oss-cn-shanghai.aliyuncs.com/1d/e9/20261118/62d535c9/8565bf52-49b3-4884-9b1e-e13a76faa59b.mp4?Expires=1795065783&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=4R8OokDpzAK1T2SUeXIoddpc1xI%3D"
                    },
                    new VideoSynthesisUsage(
                        VideoDuration: 5.33,
                        Size: "1280*704",
                        VideoRatio: "standard",
                        Fps: 15)));

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
                                                "https://dashscope-a717.oss-accelerate.aliyuncs.com/1d/f4/20261117/3c51cb34/4951352-7dVipNsc_6bd120c2813c.png?Expires=1795008096&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=a9K2hO2w1RM4LrUKMbZLX5l9AJ4%3D") with { Type = "image" }
                                        }))
                            }
                    },
                    new MultimodalTokenUsage() { ImageCount = 1, Size = "1280*1280", TotalTokens = 0 }));

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
        public static readonly RequestSnapshot<DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>>
            KlingT2iSuccess = new(
                "get-task-kling-t2i",
                new DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>(
                    "7e0d35d3-10c4-945a-9bd8-17da414bc959",
                    new ImageGenerationOutput
                    {
                        TaskId = "828024c0-d65b-4dd8-9ff7-51f3ac489497",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 15, 1, 6, 278),
                        ScheduledTime = new DateTime(2026, 11, 21, 15, 1, 6, 321),
                        EndTime = new DateTime(2026, 11, 21, 15, 2, 8, 620),
                        Choices = new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent("https://p4-fdl.klingai.com/ksc2/73v8MUoOAM2U7lha-H_No3OWSUeIqlKM2pARnHgfRo5pQBQC6CGsOMAhHgAP6hYhHbJOC5afPuWu6uOytqtTw9NiYncnuAt2H_0OSyY-KeYEWvKUEcw0zK4CiANd2l-8CU1yqZfxuuhgbRo8fXx5cxtRRoV-24RbEvDG_6F9e9RVLGKskFbg3xitmfBu5XMjpmerRuAowRzTJNQ3rokFbw.png?cacheKey=ChtzZWN1cml0eS5rbGluZy5tZXRhX2VuY3J5cHQSsAFdvhKQvQ4whCpjr5G_NqLQeOWBO3TbDAEVK9_jXJA-lcqquLhrsIQ_WRKyplIo6EWLwGQPYrg8haFv_iKbA5zvz1s1_gagzzU8B-fcCm8CQglig1ygzNc_RE77mi6dB9_7SKOsPATurnOqjtf4ljOgBssuO3m-H26MBirssPmR4I266KVSuxaKbiuA__XlnIPmYUSw2rbn3pMKtAAssiZo49EKzaihJiT8sMDwGxI75hoSK9DDR3Zts0AO39BBzVqoebvyIiCfuaWM1AAixwiQeFkN9zwdXzSIeMwZ1UvA9CLqxa_06CgFMAE&x-kcdn-pid=112757&pkey=AAVfS5OwnBVDbUT-uiRtnNFdnjv1n_RAM3upsoJJBJeqsVGCfOHYqbjdvOxOqSlf0Y13Z5BplwzkoLwS4wwdV7UnbzZWgYIkTbHUKDU844HxU044d7UFIUjEEtV97oze630") with { Type = "image" }
                                    }))
                        }
                    },
                    new MultimodalTokenUsage { ImageCount = 1, Size = "1024*1024", SR = "1080" }));

        public static readonly RequestSnapshot<DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>>
            KlingOmniSeriesSuccess = new(
                "get-task-kling-omni-series",
                new DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>(
                    "7ae7ac03-16a6-9926-820a-4bdeaa695682",
                    new ImageGenerationOutput
                    {
                        TaskId = "f3715c6a-0fc6-4f63-87ff-c86bfb5b10c9",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 15, 1, 11, 928),
                        ScheduledTime = new DateTime(2026, 11, 21, 15, 1, 11, 978),
                        EndTime = new DateTime(2026, 11, 21, 15, 2, 4, 147),
                        Choices = new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent("https://p2-fdl.klingai.com/ksc2/7swaay4I19c0fTiSERssVIwXH7sXHKiiNaVeftnE53NKzvR-mqvEWoePABEnDx6_7O__rKty3RfkenwzPIbATxVuCZbQGmZiiPPmrxx0YilzU5QfcBlkqunMrd1ao1aqcXcXNBXI2WRhFMxLSs90DqStYZ2ba-Nfvw9mLlspbKE5OR2oznOFQ4XI8wOcH04niwHr6RXL5HIWmIPC7YpfCA.png?cacheKey=ChtzZWN1cml0eS5rbGluZy5tZXRhX2VuY3J5cHQSsAE4ADur2lcm61jAh726iWB-AzytDDBnGeDuAEj3LprR9WYpeYwrog0GI1qWxEP0ym54vwtz32LFHJZARoBZ-iNsg2kb4UWt5f5V3qzLC7jmxgad8ALqalJD3WqrBpvoaq9IUhHypP3kX9dx0Ngi0btkKlj8Gvjz4UFM8yZvi5U9oZIkWI7HwqFyTf9zhKNv6a6ck39jLZG8qGFMod4yyDoorwToSFn3XX3HzVcDOl-R8xoSoUWhYgsuBTfVWszBdm10UXP1IiBTH1DntztPbZWcpCCAbmyRc11j1T_xim2Rh7U_DgT9RygFMAE&x-kcdn-pid=112757&pkey=AAXIqezdg_YOIV1IdLXeT1T798Ju09OUJ_ecHzbO9TXRWnzfEm9aEBAFPsSqPzHTMLRok4M_7gpCE-Wn15s1sEzyKqCHN2vmPNUZJmepONI5RzEGytULyVn951jgeDElNSQ") with { Type = "image" },
                                        MultimodalMessageContent.ImageContent("https://p2-fdl.klingai.com/ksc2/I5jFCMJI8nh-KXq3JbXco8PsSnv6kVHgc3iDOMTtXk8v3RBLXOguZVljuzecU3xMbT-6bZYa3WbwR4Tk3erWqA-ScX9m8CgFaqpuKcQ83jl5H-huav6SyGwYb_Og3U6wuujitr87rXlINiH_HeqHZnbsyFILmgX9OkO4dNRb96isMVh5_uJXaokZfnr9LgSO1_UqG53hxcQEmuP2OQvT7A.png?cacheKey=ChtzZWN1cml0eS5rbGluZy5tZXRhX2VuY3J5cHQSsAEQggNPNH7rQcesi0fOAG-3xnwCoVWTBHJhQkwqtdH3W4XfgILvEByOgKP_qU0htqtEDQjcSsHf0WWyYQs6l8-OWl2q2jmtsa7Z6JmUvGQbmll_yt6R_kl0hqc2dhOM8j413psRjOiimLz8lcs1daEpXWXdZ-Jjgce3WfXmA3092zyhROxT5QeqB4WkShLeh643HLWisHTaJg1a89eVMM-L2BP5ENqkXOLx7kcJEElHoxoSYF8ADrg3uvuiuzhTSVrQUvw0IiCN6cXv2SRd43IYiy1evqnXYZ00W9X_uNt9GiKUDY2ZCigFMAE&x-kcdn-pid=112757&pkey=AAWw5ywIp-RJ_zNcUX55EPRiLAD8p82JKfh0QmKwpycF5m7vV47WGM62ZPyLfXS5w8AuGFlEJA5f9wt-16KKiuQL11t6MlkHjaOSysjTwQRAJ-7Nb6a21uYNzZ4BFXpLU70") with { Type = "image" },
                                        MultimodalMessageContent.ImageContent("https://p4-fdl.klingai.com/ksc2/bg0oZ6pmmTxxqjn45W0p1H0ZqPfYgcrLuSw-HYyzvAsi2M2qYcLp58T8x92O-0Aby_-aMp5qRc9a8UBPUQNJtR1gTGiCu9LTB3tYK2CqANwxkzjJPl86WkHRzeGYfH2ITcG0QGccUK-_fBfv8L7EnN2ovMHwdHXjj5ajCefrc3F2HaGHdIa6kAkrgdlGeh-EkOKDwsIfOO9s7MBx78RQAA.png?cacheKey=ChtzZWN1cml0eS5rbGluZy5tZXRhX2VuY3J5cHQSsAHmJfEjOHtD5s4p7RxUem6yUyuO9axq7QZvCzXVNPy0VppT11_2bA0g8262qc4U5bXdQT3tsqP7F1le6t_Snnt5TgdLuysczwAsa4q-Vsu9SdOTYEobnkOSDQk5faJAY3kNYOtqP8dQLJtHoWxw21cBWRKg_-dHEQlisl55n1cwDKb3WV7aIFoGARlSeYifNvbfahG3eH_NagIb65FiqgkEXkaTVybyKkhZTbEAbZILqRoSOFvh80_9xnI_XufDmu_AA0PdIiAumxBTFovsDiMcDTPEn0smLgszM3PHwxG7X-S-ATl3vigFMAE&x-kcdn-pid=112757&pkey=AAVJW0wLkoozaOykO7kb7t41j7BJPkXYisUSe2GO8DcTOYKzBqDyoE3X2kVgshvA0T0tXZhUtjwFW_k7a7qtsjLM65nVKnwGj20D96PusWhP7-GbIJQVlCXYK8qKFXTVuSU") with { Type = "image" },
                                        MultimodalMessageContent.ImageContent("https://p2-fdl.klingai.com/ksc2/h9rbZxMda0PixyraYSy5y8eKTwYl9ofYMHTXSCmzUMbdODZCC4dMCBaqLE_lYKLH-uDbr7qLFdZN5MT9sQ4Wcu13EYFDZY-nty3v3E6_CA7VfLM3Es6f07GUVB4EV0ZMRxWrKozYgXaGlp5H_t3qqhL1wm-T1an-iQYBeJhwa5EyFKfZJns0imb5ZdYiR09JtCgbmG1-wSq4YHmnOucrEw.png?cacheKey=ChtzZWN1cml0eS5rbGluZy5tZXRhX2VuY3J5cHQSsAEy_8vfMa4uwrQ3R6g0AHbmO2tVvzrNxsxwBlynKlkbtJlP8BfTj-oSdiCHXDF11A5GhizVHLc6ofY9mnI5sW2Yvn-VQssjGDf_9zdiyC5g2x_0r5-crbxkL9VjO-nqfIJ-CK3302-JAenTu75YigAB13O9VrzQch3AEBCAthM8Bg1Qj0W6mNSP6LuaTnt6mTzhasH3s797rMAXFImJv7VFQNSKrma8CRN24xts7p-8RRoSbgFFnrpiqTZzapT733K7iU3CIiBBo_ABdnucagy_buBwxDxJzegcAb00D4_ORg7TqS4VaCgFMAE&x-kcdn-pid=112757&pkey=AAUYzjUbn7djOofswqKP7O8UuT2z0K1OnnV0DTTOffw0np0vAPyYyLHfX8lwXE6FH-iYJhiDQcRRLJqoYFSlAMvS5Znf_hDjhDedLkEs4x7qWCW3BaJPHy8SIqRTovnQ5SM") with { Type = "image" }
                                    }))
                        }
                    },
                    new MultimodalTokenUsage { ImageCount = 4, Size = "1024*1024", SR = "1080" }));

        public static readonly RequestSnapshot<DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>>
            ViduR2iSuccess = new(
                "get-task-vidu-r2i",
                new DashScopeTask<ImageGenerationOutput, MultimodalTokenUsage>(
                    "b6f8ba9b-4ce1-91fd-9e49-3a73e2f56bfd",
                    new ImageGenerationOutput
                    {
                        TaskId = "ed3c0056-0e93-4738-b1a1-9a798db4874e",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 15, 1, 16, 574),
                        ScheduledTime = new DateTime(2026, 11, 21, 15, 1, 16, 626),
                        EndTime = new DateTime(2026, 11, 21, 15, 2, 2, 962),
                        Choices = new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent("https://prod-ss-vidu.s3.oss-cn-beijing.aliyuncs.com/infer_96/tasks/26/1121/07/1021619047608938496/creation-01/image.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=LTAI5t9wejmA3ETdEwa19PH5%2F20261121%2Fcn-beijing%2Fs3%2Faws4_request&X-Amz-Date=20261121T070201Z&X-Amz-Expires=86400&X-Amz-SignedHeaders=host&response-cache-control=max-age%3D86400&response-content-disposition=attachment%3Bfilename%3D-1-2026-11-21T07%253A02%253A01Z.png&x-id=GetObject&X-Amz-Signature=ca6d964f1c8c3d7294765e33108faeaf68f3c20fc47715625f16b1f30c98f4d6") with { Type = "image" }
                                    }))
                        }
                    },
                    new MultimodalTokenUsage { ImageCount = 1, Size = "2048*2048", SR = "2K" }));
        public static readonly RequestSnapshot<DashScopeTask<Image2ImageSynthesisOutput, ImageSynthesisUsage>>
            SketchToImageSuccess = new(
                "get-task-sketch-to-image",
                new DashScopeTask<Image2ImageSynthesisOutput, ImageSynthesisUsage>(
                    "5bddc429-dcb4-9577-a1ec-8430109a7357",
                    new Image2ImageSynthesisOutput
                    {
                        TaskId = "61d36e35-d2d8-4383-86ed-59128697a974",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 16, 0, 56, 100),
                        ScheduledTime = new DateTime(2026, 11, 21, 16, 0, 56, 155),
                        EndTime = new DateTime(2026, 11, 21, 16, 1, 8, 574),
                        Results = new List<ImageSynthesisResult>
                        {
                            new(Url: "https://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/1d/b0/20261121/96f6710c/61e2d0ef-dd48-4e01-92d6-d3e125617563-1.png?Expires=1795334467&OSSAccessKeyId=STS.NY843gZC4o7DtXpiuJqVsFn8D&Signature=blhUTZ%2BoFfL%2Fwg12yge1fSUtzW8%3D&security-token=CAISzwJ1q6Ft5B2yfSjIr5qNf4nTt5wV2PWvdn7BjXUffdlfqauTpjz2IHhMeXFoBO8fsf8%2Bnm9S7%2F0flrh6SJtIXleCbMdo9JNJ8A%2BsedJCJip%2BJeBW5qe%2BEE2%2FVjTZvqaLEcibIfrZfvCyESOm8gZ43br9cxi7QlWhKufnoJV7b9MRLGLaBHg8c7UwHAZ5r9IAPnb8LOukNgWQ4lDdF011oAFx%2BwgdgOadupTCt0SF0wCjkbRJ%2FNurecWeApMybMslYbCcx%2Fdrc6fN6ilU5iVR%2Bb1%2B5K4%2Bom%2Bf44nFXgYIvE7XabWOq4YyNnJ%2BfqU8Hq9CoL3znOF1peXC0pztzBpd5nP4oKJ51OpKqeSz%2B4PBXr5RHd6TsGRYyn00p9ffh6I1s1PaxTK7alsbIAuUI3bCRP%2FUhCFpFv5kLKktZQZLssUUla4R5TGOWbIp%2B0LLGN3xuRqAARLDijWlv0g2D20vpBBJMBLF6c8XraUJwPDUz6w2zwxlgZ3CYTJQurNh5JIUT9362AAmg2WVEW1GIAWpmfiDm8SwCPUMSdFheIdOns66HbFk8zSH8MsuQ%2BVwQtFAWgUjnXPloecdS9dP%2FtWnYPdufsOdc6efzekXE66pBnPNh962IAA%3D")
                        },
                        TaskMetrics = new DashScopeTaskMetrics(1, 1, 0)
                    },
                    new ImageSynthesisUsage(1)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DT2vSuccess = new(
                "get-task-tripo-t2v",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "c400ff73-fdb7-9fd3-85a1-0be3285adc46",
                    new Tripo3DOutput
                    {
                        TaskId = "c9416579-1e16-4cc4-a557-2ea4ca41da97",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 29, 50, 504),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 29, 50, 563),
                        EndTime = new DateTime(2026, 11, 21, 18, 30, 28, 721),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/c41d5e54-c05f-4ceb-a648-4b5116563a96/tripo_pbr_model_c41d5e54-c05f-4ceb-a648-4b5116563a96.glb?auth_key=1795264227-ddztwihd-0-c2e86529f76228cce1fe52977804ef15",
                                PbrModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/c41d5e54-c05f-4ceb-a648-4b5116563a96/tripo_pbr_model_c41d5e54-c05f-4ceb-a648-4b5116563a96.glb?auth_key=1795264227-tdpjuqpu-0-1d56cf15eeaf9822ab0f18c83064db37",
                                RenderedImageUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/c41d5e54-c05f-4ceb-a648-4b5116563a96/legacy_mesh.webp?auth_key=1795264227-ylvxripx-0-fa1af2741ea24c30b7c795ac98b83c9a",
                                GeneratedImageUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/c41d5e54-c05f-4ceb-a648-4b5116563a96/text2image_c41d5e54-c05f-4ceb-a648-4b5116563a96.jpeg?auth_key=1795264227-nflsgvbb-0-3cbbb0784b7b591c09f0cb81cf76d6db")
                        }
                    },
                    new Tripo3DUsage("text-to-3d", 40, TextureQuality: 10)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DMi2vSuccess = new(
                "get-task-tripo-mi2v",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "68fa0ece-863f-9a41-b5a5-8d5a735ee965",
                    new Tripo3DOutput
                    {
                        TaskId = "ba7afd61-8c83-4a25-bc2c-f64ea9e103bf",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 29, 51, 825),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 29, 51, 884),
                        EndTime = new DateTime(2026, 11, 21, 18, 30, 38, 739),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/93209c62-61d8-4545-be85-8cac10fe956e/tripo_pbr_model_93209c62-61d8-4545-be85-8cac10fe956e.glb?auth_key=1795264238-rbmcektv-0-29672ee0981196c296c94194e5b83742",
                                PbrModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/93209c62-61d8-4545-be85-8cac10fe956e/tripo_pbr_model_93209c62-61d8-4545-be85-8cac10fe956e.glb?auth_key=1795264238-qcidsofw-0-12dd80571af522d28fd5124992bb1c43",
                                RenderedImageUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/93209c62-61d8-4545-be85-8cac10fe956e/legacy_mesh.webp?auth_key=1795264238-atlzlzzy-0-fb4b0c3a84214a75fcdb261ba2f45777")
                        }
                    },
                    new Tripo3DUsage("multi-image-to-3d", 50, TextureQuality: 10)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DImageToSplatSuccess = new(
                "get-task-tripo-image-to-splat",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "0bfde5d3-5a1c-9caa-9b97-ce5209cf6fca",
                    new Tripo3DOutput
                    {
                        TaskId = "024222b1-8dd7-4855-a7f1-d89c438894fe",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 29, 52, 548),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 29, 52, 605),
                        EndTime = new DateTime(2026, 11, 21, 18, 30, 2, 986),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/69f2c10d-f098-4c42-8476-3430fd9ea67d/69f2c10d-f098-4c42-8476-3430fd9ea67d_output.splat?auth_key=1795264202-ssohnczk-0-ab7764d29dda270e7110b4838b4616f4")
                        }
                    },
                    new Tripo3DUsage("image-to-splat", 30)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DRigCheckSuccess = new(
                "get-task-tripo-rig-check",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "c441c4f7-6e81-907a-b54b-c47896ec0956",
                    new Tripo3DOutput
                    {
                        TaskId = "06159c67-1519-4472-9c79-fe384c53d99a",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 29, 53, 809),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 29, 53, 846),
                        EndTime = new DateTime(2026, 11, 21, 18, 29, 58, 508),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                Riggable: true,
                                RigType: "quadruped",
                                NormalizeTranslation: new List<double> { 0, 0, 0 },
                                NormalizeScale: 1.0019569396972656)
                        }
                    },
                    new Tripo3DUsage("rig-check", 0)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DTextureSuccess = new(
                "get-task-tripo-texture",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "66b04a80-ccd6-9d95-942e-63e32e169d22",
                    new Tripo3DOutput
                    {
                        TaskId = "39df7cab-8ef5-4c27-9316-8ae1805b65c2",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 29, 54, 509),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 29, 54, 552),
                        EndTime = new DateTime(2026, 11, 21, 18, 30, 38, 613),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/52f5d9be-0b8a-4f3d-81a3-53ff594e0927/tripo_texture_52f5d9be-0b8a-4f3d-81a3-53ff594e0927.glb?auth_key=1795264238-bxnbzigu-0-92afca80593323ab5fda13c0af908bb8",
                                RenderedImageUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/52f5d9be-0b8a-4f3d-81a3-53ff594e0927/legacy_mesh.webp?auth_key=1795264238-contamvc-0-2be8db518eaca79d6ed3ead543a2da3a",
                                NormalizeTranslation: new List<double> { 0, 0, 0 },
                                NormalizeScale: 1.0019569396972656)
                        }
                    },
                    new Tripo3DUsage("texture", 10, TextureQuality: 10)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DSegmentationSuccess = new(
                "get-task-tripo-segmentation",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "5e0a13c8-472f-9011-9450-e2328c3a5d2e",
                    new Tripo3DOutput
                    {
                        TaskId = "1548f006-97a0-4dfd-9b8f-b2e8fc1ad1ae",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 29, 55, 730),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 29, 55, 768),
                        EndTime = new DateTime(2026, 11, 21, 18, 31, 18, 662),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/7a535ae1-dfe6-43a1-8f2f-5a8f38b5e9ef/tripo_mesh_seg_7a535ae1-dfe6-43a1-8f2f-5a8f38b5e9ef.glb?auth_key=1795264278-puyzdxwh-0-ccf2720dcd12ca87a71e6c78f572398e",
                                RenderedImageUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/7a535ae1-dfe6-43a1-8f2f-5a8f38b5e9ef/legacy_mesh.webp?auth_key=1795264278-ladopzzj-0-924474b3ad31eb0def152775c3b04b90",
                                SegModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/7a535ae1-dfe6-43a1-8f2f-5a8f38b5e9ef/tripo_mesh_seg_7a535ae1-dfe6-43a1-8f2f-5a8f38b5e9ef.glb?auth_key=1795264278-puyzdxwh-0-ccf2720dcd12ca87a71e6c78f572398e",
                                NormalizeTranslation: new List<double> { 0, 0, 0 },
                                NormalizeScale: 1.0019569396972656)
                        }
                    },
                    new Tripo3DUsage("mesh-segmentation", 40)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DSmartSegmentationSuccess = new(
                "get-task-tripo-smart-segmentation",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "3e6b4df8-981b-90e0-8fb8-63beb3ecc23c",
                    new Tripo3DOutput
                    {
                        TaskId = "f88bbfb4-727b-43d6-99c4-752998dd74e3",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 29, 56, 438),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 29, 56, 479),
                        EndTime = new DateTime(2026, 11, 21, 18, 34, 25, 3),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                SegModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/a3ba65b4-b35d-44d0-a29c-2217edd0eefe/segment/tripo_mesh_seg_20d8720f-2e48-41d2-942a-0bc5c8204c88.glb?auth_key=1795343659-mhovqozh-0-e1b6f1f8f6e9497c205ddbb41c5361c1",
                                MaskUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/a3ba65b4-b35d-44d0-a29c-2217edd0eefe/mask.png?auth_key=1795343659-pctbtdeo-0-e1e45eca983c6f25c68c3ce34ccecbfa",
                                Prompt: "head, torso, front legs, hind legs, tail, ears, eyes",
                                ModelTaskId: "113bba85-c2f2-489c-8d3e-769ff67275d2",
                                SegTaskId: "20d8720f-2e48-41d2-942a-0bc5c8204c88",
                                NormalizeTranslation: new List<double> { 0, 0, 0 },
                                NormalizeScale: 1.0019569396972656)
                        }
                    },
                    new Tripo3DUsage("smart-segmentation", 55, SegType: 55)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DRetopologySuccess = new(
                "get-task-tripo-retopology",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "ecca8aa0-ff69-9e1a-a4b6-f27a0a3505a9",
                    new Tripo3DOutput
                    {
                        TaskId = "ca7b7958-375c-4e3b-b485-a444f07e6084",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 29, 57, 141),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 29, 57, 191),
                        EndTime = new DateTime(2026, 11, 21, 18, 34, 14, 167),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/6de9af19-7b59-4a99-9cb9-eaa1c0f604d3/tripo_poly_6de9af19-7b59-4a99-9cb9-eaa1c0f604d3.fbx?auth_key=1795264453-ohxbcjut-0-875376302e16c1e3302c1e64a3ce3d6d",
                                RenderedImageUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/6de9af19-7b59-4a99-9cb9-eaa1c0f604d3/legacy_mesh.webp?auth_key=1795264453-brhwcken-0-5649e5daf185f890fee9e32c5a652a17",
                                NormalizeTranslation: new List<double> { 0, 0, 0 },
                                NormalizeScale: 1.0019569396972656)
                        }
                    },
                    new Tripo3DUsage("retopology", 30, RetopologyVersion: 30)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DConvertFormatSuccess = new(
                "get-task-tripo-convert-format",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "58214b65-b651-9e29-9fb3-5f88c794f3bf",
                    new Tripo3DOutput
                    {
                        TaskId = "36b7ac63-24ca-4062-93d9-7539fa6d2b1c",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 35, 6, 904),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 35, 6, 948),
                        EndTime = new DateTime(2026, 11, 21, 18, 35, 35, 323),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/cc7b4306-a778-447f-81bd-64bbd95fe2c5/tripo_convert_cc7b4306-a778-447f-81bd-64bbd95fe2c5.fbx?auth_key=1795264534-qiohgcfm-0-0b6f7bc4bff05fea93dc6b9070608529")
                        }
                    },
                    new Tripo3DUsage("convert-3d-format", 10, ConvertLevel: 10)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DMeshCompletionSuccess = new(
                "get-task-tripo-mesh-completion",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "46b95d36-06c8-9510-9395-aad2ac67d36b",
                    new Tripo3DOutput
                    {
                        TaskId = "d68fd695-06e4-4074-8007-af5dca4ca5d1",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 35, 7, 615),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 35, 7, 657),
                        EndTime = new DateTime(2026, 11, 21, 18, 35, 19, 893),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/45ec9b73-b480-4cce-8c23-1a50b3c10aac/tripo_mesh_compl_45ec9b73-b480-4cce-8c23-1a50b3c10aac.glb?auth_key=1795264519-wvkwdeqc-0-08c335b22d98270fe89292871237026e")
                        }
                    },
                    new Tripo3DUsage("mesh-completion", 30, CompletionMode: 30)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DAutoRigSuccess = new(
                "get-task-tripo-auto-rig",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "51044fc6-5258-9cf2-96f7-f830d31518ae",
                    new Tripo3DOutput
                    {
                        TaskId = "c2f62af4-14a5-4636-8c36-0b15c4b0189c",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 35, 8, 196),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 35, 8, 238),
                        EndTime = new DateTime(2026, 11, 21, 18, 36, 10, 374),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/8b93f5dc-1d72-4b43-8a61-73b41afdc648/tripo_rigging_8b93f5dc-1d72-4b43-8a61-73b41afdc648.glb?auth_key=1795264569-motfvsqq-0-3a710e57fb1a59ed72049fceada5eaca",
                                RenderedImageUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/8b93f5dc-1d72-4b43-8a61-73b41afdc648/legacy_mesh.webp?auth_key=1795264569-vmdsqavh-0-2f4a1b4ef052905ea91a221fba0ffea5",
                                NormalizeTranslation: new List<double> { 0, 0, 0 },
                                NormalizeScale: 1.0019569396972656)
                        }
                    },
                    new Tripo3DUsage("auto-rig", 25)));

        public static readonly RequestSnapshot<DashScopeTask<Tripo3DOutput, Tripo3DUsage>>
            Tripo3DAnimationRetargetSuccess = new(
                "get-task-tripo-animation-retarget",
                new DashScopeTask<Tripo3DOutput, Tripo3DUsage>(
                    "62bb5346-047d-95fc-9824-43cbe6958204",
                    new Tripo3DOutput
                    {
                        TaskId = "ea9860ad-7054-4e19-99e6-206c9fe51314",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2026, 11, 21, 18, 36, 39, 837),
                        ScheduledTime = new DateTime(2026, 11, 21, 18, 36, 39, 874),
                        EndTime = new DateTime(2026, 11, 21, 18, 36, 45, 149),
                        Results = new List<Tripo3DResult>
                        {
                            new(
                                ModelUrl: "https://openapi.cdn.tripo3d.com/tcli_fa0e75dc986b4d7e9e3a868a9c439032/20261121/858a9bad-60af-4df5-957b-eaacd3593b6d/tripo_retarget_858a9bad-60af-4df5-957b-eaacd3593b6d.fbx?auth_key=1795264604-ouhpvawg-0-87afac7cac73b27ce4e4fe8936a9a60e")
                        }
                    },
                    new Tripo3DUsage("animation-retarget", 10)));
    }
}
