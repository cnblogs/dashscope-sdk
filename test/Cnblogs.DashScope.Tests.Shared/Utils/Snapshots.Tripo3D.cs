using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class Tripo3D
    {
        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateT2vTask = new(
                "tripo-t2v",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-P1.0",
                    Input = new Tripo3DInput { Prompt = "一只可爱的猫" },
                    Parameters = new Tripo3DParameters { TextureQuality = "standard" }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "c9416579-1e16-4cc4-a557-2ea4ca41da97",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "92c0dd59-836a-94b7-96b0-62c9b41f3bd8"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateMi2vTask = new(
                "tripo-mi2v",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-P1.0",
                    Input = new Tripo3DInput
                    {
                        Images = new List<Tripo3DImage>
                        {
                            new(
                                "png",
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/liafix/tripo-images-1.png"),
                            new(),
                            new(
                                "png",
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/zjqhyn/tripo-images-3.png"),
                            new()
                        }
                    },
                    Parameters = new Tripo3DParameters { TextureQuality = "standard" }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "ba7afd61-8c83-4a25-bc2c-f64ea9e103bf",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "8349b960-28ee-9972-96ce-3288a13dfca1"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateImageToSplatTask = new(
                "tripo-image-to-splat",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-ImageToSplat",
                    Input = new Tripo3DInput
                    {
                        Image =
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/cfbxhg/tripo-single.jpg"
                    }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "024222b1-8dd7-4855-a7f1-d89c438894fe",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "59f375e0-e7fb-980c-8f10-2015e19be1cc"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateRigCheckTask = new(
                "tripo-rig-check",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-RigCheck",
                    Input = new Tripo3DInput
                    {
                        ModelUrl =
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260507/ffaaxt/tripo-text-to-3d-result.glb"
                    }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "06159c67-1519-4472-9c79-fe384c53d99a",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "d7077702-950a-9eaa-8dca-fd6a336f5f4f"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateTextureTask = new(
                "tripo-texture",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-Texture",
                    Input = new Tripo3DInput
                    {
                        ModelUrl =
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260507/ffaaxt/tripo-text-to-3d-result.glb"
                    },
                    Parameters = new Tripo3DParameters
                    {
                        TextureQuality = "standard",
                        TexturePrompt = new Tripo3DTexturePrompt { Text = "橘白相间的短毛猫" }
                    }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "39df7cab-8ef5-4c27-9316-8ae1805b65c2",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "395a3475-b705-90d2-b8a8-cf4b1e8e2f07"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateSegmentationTask = new(
                "tripo-segmentation",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-Segmentation",
                    Input = new Tripo3DInput
                    {
                        ModelUrl =
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260507/ffaaxt/tripo-text-to-3d-result.glb"
                    },
                    Parameters = new Tripo3DParameters { ModelVersion = "v1.0-20250506" }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "1548f006-97a0-4dfd-9b8f-b2e8fc1ad1ae",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "84736a57-862e-9597-800b-a31f4e9d2aec"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateSmartSegmentationTask = new(
                "tripo-smart-segmentation",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-SmartSegmentation",
                    Input = new Tripo3DInput
                    {
                        SegType = "model",
                        ModelUrl =
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260507/ffaaxt/tripo-text-to-3d-result.glb"
                    },
                    Parameters = new Tripo3DParameters
                    {
                        Transform = new List<double> { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 }
                    }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "f88bbfb4-727b-43d6-99c4-752998dd74e3",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "0928c20f-33a1-93f3-b2d2-768f5320300f"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateRetopologyTask = new(
                "tripo-retopology",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-Retopology",
                    Input = new Tripo3DInput
                    {
                        ModelUrl =
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260507/ffaaxt/tripo-text-to-3d-result.glb"
                    },
                    Parameters = new Tripo3DParameters
                    {
                        ModelVersion = "v2.0",
                        FaceLimit = 5000,
                        Quad = true
                    }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "ca7b7958-375c-4e3b-b485-a444f07e6084",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "de0bf0fe-1b03-92db-bbdd-8875463049e5"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateConvertFormatTask = new(
                "tripo-convert-format",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-ConvertFormat",
                    Input = new Tripo3DInput
                    {
                        OriginalModelTaskId = "c9416579-1e16-4cc4-a557-2ea4ca41da97",
                        Format = "FBX"
                    },
                    Parameters = new Tripo3DParameters
                    {
                        Quad = true,
                        FaceLimit = 5000,
                        FbxPreset = "blender"
                    }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "36b7ac63-24ca-4062-93d9-7539fa6d2b1c",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "bbd10290-46d0-9501-b288-292a987c721f"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateMeshCompletionTask = new(
                "tripo-mesh-completion",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-MeshCompletion",
                    Input = new Tripo3DInput { OriginalModelTaskId = "1548f006-97a0-4dfd-9b8f-b2e8fc1ad1ae" },
                    Parameters = new Tripo3DParameters { CompletionMode = "quick_cap" }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "d68fd695-06e4-4074-8007-af5dca4ca5d1",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "be34ecea-5cdd-9df0-9680-a0a35d5d27c1"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateAutoRigTask = new(
                "tripo-auto-rig",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-AutoRig",
                    Input = new Tripo3DInput
                    {
                        ModelUrl =
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260507/ffaaxt/tripo-text-to-3d-result.glb"
                    },
                    Parameters = new Tripo3DParameters
                    {
                        RigType = "quadruped",
                        ModelVersion = "v2.5-20260210"
                    }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "c2f62af4-14a5-4636-8c36-0b15c4b0189c",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "7686d3dd-3c41-97a6-9e36-270785e5faed"
                });

        public static readonly
            RequestSnapshot<ModelRequest<Tripo3DInput, ITripo3DParameters>,
                ModelResponse<Tripo3DOutput, Tripo3DUsage>> CreateAnimationRetargetTask = new(
                "tripo-animation-retarget",
                new ModelRequest<Tripo3DInput, ITripo3DParameters>
                {
                    Model = "Tripo/Tripo-AnimationRetarget",
                    Input = new Tripo3DInput { OriginalModelTaskId = "c2f62af4-14a5-4636-8c36-0b15c4b0189c" },
                    Parameters = new Tripo3DParameters
                    {
                        Animations = new List<string> { "preset:quadruped:walk" },
                        OutFormat = "fbx"
                    }
                },
                new ModelResponse<Tripo3DOutput, Tripo3DUsage>
                {
                    Output = new Tripo3DOutput
                    {
                        TaskId = "ea9860ad-7054-4e19-99e6-206c9fe51314",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "ff01b600-3507-9565-ae6c-b6e641f2fd14"
                });
    }
}
