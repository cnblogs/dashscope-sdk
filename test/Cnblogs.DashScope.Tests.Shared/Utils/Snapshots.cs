using System.Text.Json;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class Tokenization
    {
        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                ModelResponse<TokenizationOutput, TokenizationUsage>> TokenizeNoSse = new(
                "tokenization",
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Input = new TextGenerationInput
                    {
                        Messages =
                            new List<TextChatMessage> { TextChatMessage.User("代码改变世界") }.AsReadOnly()
                    },
                    Model = "qwen-max",
                    Parameters = new TextGenerationParameters { Seed = 1234 }
                },
                new ModelResponse<TokenizationOutput, TokenizationUsage>
                {
                    Output = new TokenizationOutput(
                        new List<int>
                        {
                            46100,
                            101933,
                            99489
                        },
                        new List<string>
                        {
                            "代码",
                            "改变",
                            "世界"
                        }),
                    Usage = new TokenizationUsage(3),
                    RequestId = "6615ba01-081d-9147-93ff-7bd26f3adf93"
                });
    }

    public static class ImageSynthesis
    {
        public static readonly
            RequestSnapshot<ModelRequest<ImageSynthesisInput, IImageSynthesisParameters>,
                ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>> CreateTask = new(
                "image-synthesis",
                new ModelRequest<ImageSynthesisInput, IImageSynthesisParameters>
                {
                    Model = "wanx-v1",
                    Input = new ImageSynthesisInput { Prompt = "一只奔跑的猫" },
                    Parameters = new ImageSynthesisParameters
                    {
                        N = 4,
                        Seed = 42,
                        Size = "1024*1024",
                        Style = "<sketch>"
                    }
                },
                new ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>
                {
                    Output = new ImageSynthesisOutput
                    {
                        TaskId = "9e2b6ef6-285d-4efa-8651-4dbda7d571fa",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "33da8e6b-1309-9a44-be83-352165959608"
                });

        public static readonly RequestSnapshot<ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>,
                ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>>
            CreateTranslationTask = new(
                "image2image-synthesis-translation",
                new ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>()
                {
                    Model = "qwen-mt-image",
                    Input = new Image2ImageSynthesisInput()
                    {
                        SourceLang = "zh",
                        TargetLang = "en",
                        ImageUrl =
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250916/ordhsk/1.webp",
                        Ext = new Image2ImageSynthesisInputExt()
                        {
                            DomainHint =
                                "These sentences are from seller-buyer conversations on a B2C ecommerce platform. Translate them into clear, engaging customer service language, ensuring the translation is appropriate for handling potential issues or disputes.",
                            Sensitives = new List<string>() { "基础" },
                            Terminologies =
                                new List<Image2ImageSynthesisInputExtTerm> { new("一体化", "unified") },
                            Config = new Image2ImageSynthesisConfig() { ImageSegment = false }
                        }
                    }
                },
                new ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>()
                {
                    Output = new Image2ImageSynthesisOutput()
                    {
                        TaskId = "ca8254b2-5269-48ef-9186-860a8341132d", TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "d86ac0b7-08b2-9730-8a25-7b2a76b043e3",
                });

        public static readonly RequestSnapshot<ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>,
                ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>>
            CreateImageEditTask = new(
                "image-edit-instruction",
                new ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>()
                {
                    Model = "wan2.5-i2i-preview",
                    Input = new Image2ImageSynthesisInput()
                    {
                        Prompt = "将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
                        Images = new List<string>
                        {
                            "https://img.alicdn.com/imgextra/i3/O1CN0157XGE51l6iL9441yX_!!6000000004770-49-tps-1104-1472.webp",
                            "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"
                        }
                    },
                    Parameters = new Image2ImageSynthesisParameters() { N = 1 }
                },
                new ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>()
                {
                    Output = new Image2ImageSynthesisOutput
                    {
                        TaskId = "bbed0f78-5a64-4a2a-8efb-47a258a79c21",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "acdb3ccf-4b51-9e46-833d-00879e38418b"
                });

        public static readonly RequestSnapshot<ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>,
                ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>>
            CreateImageEditFunctionTask = new(
                "image-edit-function",
                new ModelRequest<Image2ImageSynthesisInput, IImage2ImageSynthesisParameters>()
                {
                    Model = "wanx2.1-imageedit",
                    Input = new Image2ImageSynthesisInput()
                    {
                        Function = "description_edit",
                        Prompt = "把她的头发修改为红色。",
                        BaseImageUrl = "http://wanx.alicdn.com/material/20250318/description_edit_2.png"
                    },
                    Parameters = new Image2ImageSynthesisParameters() { N = 1 }
                },
                new ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>()
                {
                    Output = new Image2ImageSynthesisOutput
                    {
                        TaskId = "40a6b9bb-720c-40a1-b09f-fcde713d2d9f",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "7b230e32-b8f3-9032-91ab-85e48e11d5a0"
                });

        public static readonly RequestSnapshot<ModelRequest<Image2ImageSynthesisInput>,
                ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>>
            TranslateImage = new(
                "image2image-synthesis-translation-sync",
                new ModelRequest<Image2ImageSynthesisInput>()
                {
                    Model = "qwen-mt-image-2.0",
                    Input = new Image2ImageSynthesisInput()
                    {
                        ImageUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250916/ordhsk/1.webp",
                        SourceLang = "zh",
                        TargetLang = "en"
                    }
                },
                new ModelResponse<Image2ImageSynthesisOutput, ImageSynthesisUsage>()
                {
                    Output = new Image2ImageSynthesisOutput()
                    {
                        ImageUrl = "http://dashscope-a717.oss-cn-beijing.aliyuncs.com/1d/e6/20261117/e3175e94/676688aa-2131-4b82-94d3-5653bccd0298.jpg?Expires=1795011286&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=l5QHEmUN15K0clC8%2Ftbvr7NMeWo%3D"
                    },
                    Usage = new ImageSynthesisUsage(1),
                    RequestId = "518f1436-0a80-9003-a443-bd5cca36e28f"
                });
    }

    public static class VideoSynthesis
    {
        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateT2vTask = new(
                "video-synthesis-t2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "happyhorse-1.1-t2v",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "一座由硬纸板和瓶盖搭建的微型城市，在夜晚焕发出生机。一列硬纸板火车缓缓驶过，小灯点缀其间，照亮前路。"
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "480P",
                        Ratio = "16:9",
                        Duration = 3,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "5ab34e25-bb2b-48b6-9fad-28744602785c",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "72c7f4f1-633b-9fdd-bbd3-f81588439772"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateI2vTask = new(
                "video-synthesis-i2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "happyhorse-1.1-i2v",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "一只猫在草地上奔跑",
                        Media = new List<VideoSynthesisMedia> { new("first_frame", "https://cdn.translate.alibaba.com/r/wanx-demo-1.png") }
                    },
                    Parameters = new VideoSynthesisParameters { Resolution = "480P", Duration = 3, Watermark = false }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "0112b6d1-15ce-4697-9186-bee4ce7c8838",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "ce057ed2-e146-98cb-a464-172fd939ad04"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateStyleTransformTask = new(
                "video-synthesis-style-transform",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "video-style-transform",
                    Input = new VideoSynthesisInput
                    {
                        VideoUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/viwndw/%E5%8E%9F%E8%A7%86%E9%A2%91.mp4"
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Style = 0,
                        VideoFps = 15,
                        AnimateEmotion = false,
                        MinLen = 540,
                        UseSR = true
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "c345318f-da61-4e02-a7d8-126e0c9cd11a",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "915d285b-3e57-9ef3-8608-8d90862186ab"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateWan3T2vTask = new(
                "video-synthesis-wan3-t2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wan3.0-video",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "一只小猫在月光下的屋顶上奔跑，城市的霓虹灯在远处闪烁，电影级画质，流畅运镜。"
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "480P",
                        Ratio = "adaptive",
                        Duration = 5,
                        Audio = false,
                        Seed = 42,
                        PromptExtend = false,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "21725ab8-8dfc-4242-b6e4-f0d7feaa06ab",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "584e1b8a-6a2b-93c9-8d75-5084d062cf89"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateWan27T2vTask = new(
                "video-synthesis-wan27-t2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wan2.7-t2v",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "一只小猫在月光下奔跑",
                        NegativePrompt = "花朵",
                        AudioUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3"
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "720P",
                        Ratio = "9:16",
                        Duration = 5,
                        PromptExtend = false,
                        Watermark = false,
                        Seed = 42
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "865dbcde-9cd1-403d-920a-f2e977968d14",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "67d33b3e-4505-9011-8fda-ff36020ed0e5"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateWan27I2vTask = new(
                "video-synthesis-wan27-i2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wan2.7-i2v-2026-04-25",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "写实风格,一只小黑猫好奇地仰望天空,镜头从平视角度逐渐升高,最后以俯视角度捕捉到它好奇的眼神。",
                        NegativePrompt = "低分辨率、错误、最差质量",
                        Media = new List<VideoSynthesisMedia>
                        {
                            new("first_frame", "https://wanx.alicdn.com/material/20250318/first_frame.png"),
                            new("last_frame", "https://wanx.alicdn.com/material/20250318/last_frame.png")
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "720P",
                        Duration = 5,
                        PromptExtend = false,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "c32a747e-2cdd-4f4f-a071-64519ed9caee",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "196a7be8-484d-9156-826b-58c27cbe3683"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateWan27R2vTask = new(
                "video-synthesis-wan27-r2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wan2.7-r2v-2026-06-12",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。",
                        Media = new List<VideoSynthesisMedia>
                        {
                            new(
                                "reference_image",
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/sjuytr/wan-r2v-object-girl.jpg",
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/gbqewz/wan-r2v-girl-voice.mp3"),
                            new(
                                "reference_video",
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/isllrq/wan-r2v-boy-voice.mp3"),
                            new("reference_image", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/rtjeqf/wan-r2v-object3.png"),
                            new("reference_image", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png"),
                            new("reference_image", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png")
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "720P",
                        Ratio = "16:9",
                        Duration = 5,
                        PromptExtend = false,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "78db84c4-9950-4a33-9025-c6ab9c7cbe1e",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "b0077fa0-15b2-9b43-9b61-cd311146e89a"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateWan27VideoEditTask = new(
                "video-synthesis-wan27-videoedit",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wan2.7-videoedit",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "将视频中女孩的衣服替换为图片中的衣服",
                        NegativePrompt = "低分辨率、错误、最差质量",
                        Media = new List<VideoSynthesisMedia>
                        {
                            new("video", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260403/nlspwm/T2VA_22.mp4"),
                            new("reference_image", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260402/fwjpqf/wan2.7-videoedit-change-clothes.png")
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "720P",
                        Ratio = "16:9",
                        AudioSetting = "origin",
                        PromptExtend = false,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "38724c2e-d85c-4777-93a9-8f3d623b4437",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "028419bc-bc95-9392-b907-c7f0d6a69829"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateWan26T2vTask = new(
                "video-synthesis-wan26-t2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wan2.6-t2v",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "一只小猫将军骑着战马站在悬崖上，远处的雪山上空乌云密布。",
                        NegativePrompt = "低分辨率、错误、最差质量",
                        AudioUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3"
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Size = "1280*720",
                        Duration = 5,
                        ShotType = "multi",
                        PromptExtend = true,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "8efca47c-a0d8-4a4e-9ead-516e5bba6731",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "4e4fe189-9f94-908b-82c7-d31fc2cfc2a7"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateWan26I2vTask = new(
                "video-synthesis-wan26-i2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wan2.6-i2v-flash",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "一只由喷漆画成的少年从混凝土墙上活过来，摆出充满活力的说唱歌手姿势。",
                        ImgUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/wpimhv/rap.png"
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "720P",
                        Duration = 5,
                        Audio = false,
                        PromptExtend = false,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "6732b64a-4754-4077-bd7c-dbf291e8b313",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "4ac184c7-92b1-9a2a-b4ea-8459e429dd60"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateWan26R2vTask = new(
                "video-synthesis-wan26-r2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wan2.6-r2v-flash",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "Character2 坐在靠窗的椅子上，手持 character3，在 character4 旁演奏一首舒缓的美国乡村民谣。Character1 对 Character2 开口说道：“听起来不错”",
                        ReferenceUrls = new List<string>
                        {
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/hfugmr/wan-r2v-role1.mp4",
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png",
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Size = "1280*720",
                        Duration = 5,
                        Audio = true,
                        ShotType = "multi",
                        PromptExtend = false,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "7fb6e793-3507-4b94-8eef-68e960faee86",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "adcf9408-ddc6-9c9e-a27a-490b108b49a2"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateVaceImageReferenceTask = new(
                "video-synthesis-vace-image-reference",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wanx2.1-vace-plus",
                    Input = new VideoSynthesisInput
                    {
                        Function = "image_reference",
                        Prompt = "视频中，一位女孩自晨雾缭绕的古老森林深处款款走出，她步伐轻盈，镜头捕捉她每一个灵动瞬间。当女孩站定，环顾四周葱郁林木时，她脸上绽放出惊喜与喜悦交织的笑容。",
                        RefImagesUrl = new List<string>
                        {
                            "http://wanx.alicdn.com/material/20250318/image_reference_2_5_16.png",
                            "http://wanx.alicdn.com/material/20250318/image_reference_1_5_16.png"
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        PromptExtend = true,
                        ObjOrBg = new List<string> { "obj", "bg" },
                        Size = "1280*720",
                        Seed = 42,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "c7c80870-110d-42a8-a1db-5fb333946c28",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "3932b8a7-afd0-9b4e-9a28-335a37fe9f6d"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateVaceVideoRepaintingTask = new(
                "video-synthesis-vace-video-repainting",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "wanx2.1-vace-plus",
                    Input = new VideoSynthesisInput
                    {
                        Function = "video_repainting",
                        Prompt = "视频展示了一辆黑色的蒸汽朋克风格汽车，绅士驾驶着，车辆装饰着齿轮和铜管。背景是蒸汽驱动的糖果工厂和复古元素，画面复古与趣味。",
                        VideoUrl = "http://wanx.alicdn.com/material/20250318/video_repainting_1.mp4"
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        PromptExtend = false,
                        ControlCondition = "depth",
                        Strength = 1,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "fec80098-b52d-47ee-a709-cf1070ce7b72",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "f44f9b2c-254e-9ac9-b1ca-48f79e746544"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreatePixverseT2vTask = new(
                "video-synthesis-pixverse-t2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "pixverse/pixverse-c1-t2v",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。"
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Size = "1280*720",
                        Duration = 5,
                        Audio = true,
                        Watermark = true,
                        Seed = 12345
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "ec3c3226-79c9-4b2a-8fb2-0cf79d26a23b",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "117999d0-d889-9b04-bade-2d5307fa41b7"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreatePixverseI2vTask = new(
                "video-synthesis-pixverse-i2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "pixverse/pixverse-v6-it2v",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "镜头从海龟下方缓缓上移，海龟悠然游动，腹部细节清晰可见。",
                        Media = new List<VideoSynthesisMedia>
                        {
                            new("image_url", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260121/zlpocv/wan-i2v-haigui.webp")
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "720P",
                        Duration = 5,
                        Audio = false,
                        ShotType = "single",
                        Watermark = true
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "17e1b649-6094-4200-a638-a1603fcc26a3",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "e914a682-f20f-9338-91cf-2b984c3ab462"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreatePixverseKf2vTask = new(
                "video-synthesis-pixverse-kf2v",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "pixverse/pixverse-c1-kf2v",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "一只小猫从窗台向下跳跃，轻盈地落在沙发上，然后好奇地环顾四周。",
                        Media = new List<VideoSynthesisMedia>
                        {
                            new("first_frame", "https://wanx.alicdn.com/material/20250318/first_frame.png"),
                            new("last_frame", "https://wanx.alicdn.com/material/20250318/last_frame.png")
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "720P",
                        Duration = 5,
                        Watermark = true
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "763016e3-a0f5-49f8-8af0-edc884e3fcf6",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "385e79ca-3177-9e35-b5a3-837779b0707e"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreatePixverseR2vOmniTask = new(
                "video-synthesis-pixverse-r2v-omni",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "pixverse/pixverse-v6-r2v-omni",
                    Input = new VideoSynthesisInput
                    {
                        Prompt = "@男人 在@咖啡厅 中弹吉他 参考@参考视频",
                        Media = new List<VideoSynthesisMedia>
                        {
                            new(
                                "video_url",
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                                RefName: "参考视频"),
                            new(
                                "image_url",
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260320/knsple/wan-r2v-role-frame.jpg",
                                RefName: "男人"),
                            new(
                                "image_url",
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png",
                                RefName: "咖啡厅")
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "720P",
                        AspectRatio = "16:9",
                        Duration = 0,
                        Audio = true,
                        Watermark = false
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "f912ec38-68f1-440f-9aa7-f58b43c0c7d5",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "d56eef61-bf0f-917f-ab07-eddb8ab898c9"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreatePixverseLipsyncTask = new(
                "video-synthesis-pixverse-lipsync",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "pixverse/pixverse-lipsync",
                    Input = new VideoSynthesisInput
                    {
                        Media = new List<VideoSynthesisMedia>
                        {
                            new("video_url", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/pvegot/input_video_01.mp4"),
                            new("lip_sync_tts_speaker_id", Id: "auto"),
                            new("lip_sync_tts_content", Content: "保持热爱持续开心，琐碎小事不值得烦心")
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Watermark = true
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "32b7a273-f904-45d7-b91f-9e9e30edf304",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "afd3fca8-1769-98d5-868e-cb3f07cb4b78"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreatePixverseMotionControlTask = new(
                "video-synthesis-pixverse-motioncontrol",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "pixverse/pixverse-motioncontrol",
                    Input = new VideoSynthesisInput
                    {
                        Media = new List<VideoSynthesisMedia>
                        {
                            new("image_url", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/adsyrp/move_input_image.jpeg"),
                            new("video_url", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/kaakcn/move_input_video.mp4")
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Resolution = "720P",
                        Watermark = true
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "929ff38c-3f30-47ac-b1f8-96734fdfd212",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "6efb4ab6-10da-9d1f-ad67-2a0ec7657978"
                });

        public static readonly RequestSnapshot<ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>,
                ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>>
            CreatePixverseUpscaleTask = new(
                "video-synthesis-pixverse-upscale",
                new ModelRequest<VideoSynthesisInput, IVideoSynthesisParameters>()
                {
                    Model = "pixverse/pixverse-upscale",
                    Input = new VideoSynthesisInput
                    {
                        Media = new List<VideoSynthesisMedia>
                        {
                            new("video_url", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/wqefue/mix_input_video.mp4")
                        }
                    },
                    Parameters = new VideoSynthesisParameters
                    {
                        Seed = 12345,
                        Watermark = true
                    }
                },
                new ModelResponse<VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new VideoSynthesisOutput
                    {
                        TaskId = "2e4c6c6d-9223-4899-ab45-9594b6db8721",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "03bf5c87-59f2-9b77-8727-41e3df30d313"
                });
    }

    public static class Image2VideoSynthesis
    {
        public static readonly RequestSnapshot<ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters>,
                ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateKf2vTask = new(
                "image2video-synthesis-kf2v",
                new ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters>()
                {
                    Model = "wan2.2-kf2v-flash",
                    Input = new Image2VideoSynthesisInput
                    {
                        Prompt = "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。",
                        NegativePrompt = "低分辨率、错误、最差质量",
                        FirstFrameUrl = "https://wanx.alicdn.com/material/20250318/first_frame.png",
                        LastFrameUrl = "https://wanx.alicdn.com/material/20250318/last_frame.png"
                    },
                    Parameters = new Image2VideoSynthesisParameters
                    {
                        Resolution = "480P",
                        PromptExtend = false,
                        Watermark = false
                    }
                },
                new ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new Image2VideoSynthesisOutput
                    {
                        TaskId = "5e0ddd64-f136-48bf-9515-cbb0175f36f2",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "30d7cee5-bff3-9f65-bfc7-202a3bdd27a2"
                });

        public static readonly RequestSnapshot<ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters>,
                ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateAnimateMoveTask = new(
                "image2video-synthesis-animate-move",
                new ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters>()
                {
                    Model = "wan2.2-animate-move",
                    Input = new Image2VideoSynthesisInput
                    {
                        ImageUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/adsyrp/move_input_image.jpeg",
                        VideoUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/kaakcn/move_input_video.mp4",
                        Watermark = true
                    },
                    Parameters = new Image2VideoSynthesisParameters
                    {
                        Mode = "wan-std",
                        CheckImage = true
                    }
                },
                new ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new Image2VideoSynthesisOutput
                    {
                        TaskId = "80d7a233-1eb7-4888-8fa3-dbc66e544dd9",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "ccd3705b-ed4f-9191-a19d-c10326b269c6"
                });

        public static readonly RequestSnapshot<ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters>,
                ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateS2vTask = new(
                "image2video-synthesis-s2v",
                new ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters>()
                {
                    Model = "wan2.2-s2v",
                    Input = new Image2VideoSynthesisInput
                    {
                        ImageUrl = "https://img.alicdn.com/imgextra/i3/O1CN011FObkp1T7Ttowoq4F_!!6000000002335-0-tps-1440-1797.jpg",
                        AudioUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250825/iaqpio/input_audio.MP3"
                    },
                    Parameters = new Image2VideoSynthesisParameters
                    {
                        Resolution = "480P"
                    }
                },
                new ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new Image2VideoSynthesisOutput
                    {
                        TaskId = "cc7c958d-6c4e-48e8-a7dc-2f1a24d448c9",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "1e62b5f7-8b7a-9767-ae5e-3d1caff94e44"
                });

        public static readonly RequestSnapshot<ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters>,
                ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>>
            CreateVideoretalkTask = new(
                "image2video-synthesis-videoretalk",
                new ModelRequest<Image2VideoSynthesisInput, IImage2VideoSynthesisParameters>()
                {
                    Model = "videoretalk",
                    Input = new Image2VideoSynthesisInput
                    {
                        VideoUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/ysurjs/mix_output_std.mp4",
                        AudioUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/aumwir/stella2-%E6%9C%89%E5%A3%B0%E4%B9%A67.wav",
                        RefImageUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/bhkfor/mix_input_image.jpeg"
                    },
                    Parameters = new Image2VideoSynthesisParameters
                    {
                        VideoExtension = false,
                        QueryFaceThreshold = 170
                    }
                },
                new ModelResponse<Image2VideoSynthesisOutput, VideoSynthesisUsage>
                {
                    Output = new Image2VideoSynthesisOutput
                    {
                        TaskId = "8565bf52-49b3-4884-9b1e-e13a76faa59b",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "d3fbeccd-34ec-91bb-b20d-5419d95946e4"
                });
    }

    public static class FaceDetect
    {
        public static readonly RequestSnapshot<ModelRequest<FaceDetectInput>,
                ModelResponse<FaceDetectOutput, ImageSynthesisUsage>>
            DetectNoSse = new(
                "face-detect",
                new ModelRequest<FaceDetectInput>
                {
                    Model = "wan2.2-s2v-detect",
                    Input = new FaceDetectInput
                    {
                        ImageUrl = "https://img.alicdn.com/imgextra/i3/O1CN011FObkp1T7Ttowoq4F_!!6000000002335-0-tps-1440-1797.jpg"
                    }
                },
                new ModelResponse<FaceDetectOutput, ImageSynthesisUsage>
                {
                    Output = new FaceDetectOutput
                    {
                        CheckPass = true,
                        Humanoid = true
                    },
                    Usage = new ImageSynthesisUsage(1),
                    RequestId = "f976200c-7a15-9f65-b9bd-f70620adff85"
                });
    }

    public static class ImageGeneration
    {
        public static readonly
            RequestSnapshot<ModelRequest<ImageGenerationInput, IMultimodalParameters>,
                ModelResponse<ImageGenerationOutput, MultimodalTokenUsage>> CreateTaskNoSse = new(
                "image-generation",
                new ModelRequest<ImageGenerationInput, IMultimodalParameters>
                {
                    Model = "wanx-style-repaint-v1",
                    Input = new ImageGenerationInput
                    {
                        ImageUrl =
                            "https://public-vigen-video.oss-cn-shanghai.aliyuncs.com/public/dashscope/test.png",
                        StyleIndex = 3
                    }
                },
                new ModelResponse<ImageGenerationOutput, MultimodalTokenUsage>
                {
                    Output = new ImageGenerationOutput
                    {
                        TaskId = "c4f94e00-5899-431b-9579-eb1ebe686379", TaskStatus = DashScopeTaskStatus.Pending,
                    },
                    RequestId = "565ff453-bcf7-99ec-9fbe-b99bb8caab07"
                });
    }

    public static class BackgroundGeneration
    {
        public static readonly
            RequestSnapshot<ModelRequest<BackgroundGenerationInput, IBackgroundGenerationParameters>,
                ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>> CreateTaskNoSse = new(
                "background-generation",
                new ModelRequest<BackgroundGenerationInput, IBackgroundGenerationParameters>
                {
                    Input = new BackgroundGenerationInput
                    {
                        BaseImageUrl =
                            "http://inner-materials.oss-cn-beijing.aliyuncs.com/graphic_design/jianguo/lllcho.lc/test_data/demo_example/demo/hailuo_2236873898_2.png",
                        RefImageUrl =
                            "http://inner-materials.oss-cn-beijing.aliyuncs.com/graphic_design/jianguo/lllcho.lc/test_data/demo_example/demo/d1faf4f26c8c4ea798d043a8bf3784bb_2.png",
                        RefPrompt = "远处有朝阳升起",
                        Title = "分享好时光",
                        SubTitle = "只为不一样的你",
                        NegRefPrompt = "低质量的，模糊的，错误的"
                    },
                    Model = "wanx-background-generation-v2",
                    Parameters = new BackgroundGenerationParameters
                    {
                        N = 2,
                        NoiseLevel = 300,
                        RefPromptWeight = 0.5f
                    }
                },
                new ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>
                {
                    RequestId = "a010df33-effc-9e32-aaa0-e55fffa40ef5",
                    Output = new BackgroundGenerationOutput
                    {
                        TaskId = "b2e98d78-c79b-431c-b2d7-c7bcd54465da",
                        TaskStatus = DashScopeTaskStatus.Pending
                    }
                });
    }

    public static class Upload
    {
        // get-upload-policy.response.body.txt must be CRLF
        public static readonly RequestSnapshot<DashScopeTemporaryUploadPolicy> GetPolicyNoSse = new(
            "get-upload-policy",
            new DashScopeTemporaryUploadPolicy(
                "b744f4f8-1a9c-9c6b-950d-0d327e331f2f",
                new DashScopeTemporaryUploadPolicyData(
                    "eyJleHBpcmF0aW9uIjoiMjAyNS0wNy0xMlQxMjoxMDoyNC40ODhaIiwiY29uZGl0aW9ucyI6W1siY29udGVudC1sZW5ndGgtcmFuZ2UiLDAsMTA3Mzc0MTgyNF0sWyJzdGFydHMtd2l0aCIsIiRrZXkiLCJkYXNoc2NvcGUtaW5zdGFudFwvNTJhZmUwNzdmYjQ4MjVjNmQ3NDQxMTc1OGNiMWFiOThcLzIwMjUtMDctMTJcL2I3NDRmNGY4LTFhOWMtOWM2Yi05NTBkLTBkMzI3ZTMzMWYyZiJdLHsiYnVja2V0IjoiZGFzaHNjb3BlLWZpbGUtbWdyIn0seyJ4LW9zcy1vYmplY3QtYWNsIjoicHJpdmF0ZSJ9LHsieC1vc3MtZm9yYmlkLW92ZXJ3cml0ZSI6InRydWUifV19",
                    "n3dNX/aD3+WAly0QgzsURfiIk00=",
                    "dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-07-12/b744f4f8-1a9c-9c6b-950d-0d327e331f2f",
                    "https://dashscope-file-mgr.oss-cn-beijing.aliyuncs.com",
                    300,
                    1024,
                    999999999,
                    "accessKeyId",
                    "private",
                    "true")));

        // upload-temporary-file.request.body.txt must be CRLF
        public static readonly RequestSnapshot UploadTemporaryFileNoSse = new("upload-temporary-file")
        {
            Boundary = "5aa22a67-eae4-4c54-8f62-c486fefd11a5"
        };
    }

    // Kept as a literal instead of generating from a .NET type: schema generation libraries
    // (JsonSchema.Net / M.E.AI.Abstractions) would bump System.Text.Json above the version
    // pinned by Cnblogs.DashScope.Core for net6.0 test builds, altering request serialization.
    public static readonly JsonElement GetCurrentWeatherParametersSchema = JsonDocument.Parse(
        "{\"type\":\"object\",\"properties\":{\"location\":{\"type\":\"string\",\"description\":\"要获取天气的省市名称，例如浙江省杭州市\"}},\"required\":[\"location\"]}")
        .RootElement.Clone();
}
