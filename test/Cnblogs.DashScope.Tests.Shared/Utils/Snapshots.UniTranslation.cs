using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class UniTranslation
    {
        private const string DomainHint = "These sentences are from seller-buyer conversations on a B2C ecommerce platform. Translate them into clear, engaging customer service language.";

        public static readonly RequestSnapshot<ModelRequest<UniTranslationInput>, ModelResponse<UniTranslationOutput, UniTranslationUsage>>
            Texts = new(
                "uni-translation",
                new ModelRequest<UniTranslationInput>
                {
                    Model = "qwen-mt-uni",
                    Input = new UniTranslationInput
                    {
                        SourceTexts = new List<string>
                        {
                            "机器学习正在改变世界。",
                            "我们的应用程序接口非常稳定。",
                            "全场9折",
                            "这款商品支持七天无理由退换。"
                        },
                        SourceLang = "zh",
                        TargetLang = "en",
                        Ext = new UniTranslationInputExt
                        {
                            DomainHint = DomainHint,
                            Sensitives = new List<string> { "全场9折", "七天无理由退换" },
                            Glossary = new List<UniTranslationGlossaryTerm> { new("应用程序接口", "API"), new("机器学习", "ML") }
                        }
                    }
                },
                new ModelResponse<UniTranslationOutput, UniTranslationUsage>
                {
                    RequestId = "025ce263-8fa0-912c-8acd-2b96251a86bb",
                    Output = new UniTranslationOutput(
                        Success: true,
                        Code: 200,
                        Message: "Success",
                        RequestId: "025ce263-8fa0-912c-8acd-2b96251a86bb",
                        Data: new UniTranslationData(
                            "strings",
                            TranslatedTexts: new List<string>
                            {
                                "Machine learning is changing the world.",
                                "Our API is highly stable.",
                                "全场9折",
                                "This item is eligible for a 7-day no-questions-asked return or exchange."
                            })),
                    Usage = new UniTranslationUsage(
                        34,
                        108,
                        142,
                        new UniTranslationInputTokenDetails(0, 34, 0, 0),
                        new UniTranslationOutputTokenDetails(0, 108, 0, 0))
                });

        public static readonly RequestSnapshot<ModelRequest<UniTranslationInput>, ModelResponse<UniTranslationOutput, UniTranslationUsage>>
            Image = new(
                "uni-translation-image",
                new ModelRequest<UniTranslationInput>
                {
                    Model = "qwen-mt-uni",
                    Input = new UniTranslationInput
                    {
                        FileUrl = "https://maas-infra-public.oss-cn-hangzhou.aliyuncs.com/lzhe/test/03bc3a6a-a02b-11f1-8874-00163e79676e.jpg",
                        TargetLang = "zh",
                        Ext = new UniTranslationInputExt
                        {
                            FormatHint = "image",
                            Config = new UniTranslationConfig { ImageSegment = true }
                        }
                    }
                },
                new ModelResponse<UniTranslationOutput, UniTranslationUsage>
                {
                    RequestId = "e0c76430-1b05-9368-b5ba-fc48bdc9ed95",
                    Output = new UniTranslationOutput(
                        Success: true,
                        Code: 200,
                        Message: "Success",
                        RequestId: "e0c76430-1b05-9368-b5ba-fc48bdc9ed95",
                        Data: new UniTranslationData(
                            "image",
                            TranslatedFileUrl: "http://dashscope-a717.oss-cn-beijing.aliyuncs.com/7d/d6/20261125/ae62a8f5/9a7bcea350764d6da85684efb863f6a9.jpg?Expires=1798199640&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=hwhjyKEjXgxc7%2B7FdQ7W%2BF1%2F34s%3D")),
                    Usage = new UniTranslationUsage(
                        1887,
                        1887,
                        3774,
                        new UniTranslationInputTokenDetails(0, 0, 0, 1887),
                        new UniTranslationOutputTokenDetails(0, 0, 0, 1887))
                });

        public static readonly RequestSnapshot<ModelRequest<UniTranslationInput>, ModelResponse<UniTranslationOutput, UniTranslationUsage>>
            CreateTask = new(
                "uni-translation-create",
                new ModelRequest<UniTranslationInput>
                {
                    Model = "qwen-mt-uni",
                    Input = new UniTranslationInput
                    {
                        FileUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250224/ghwuqu/%E5%91%98%E5%B7%A5%E4%BF%A1%E6%81%AF%E8%A1%A8.xlsx",
                        TargetLang = "en"
                    }
                },
                new ModelResponse<UniTranslationOutput, UniTranslationUsage>
                {
                    RequestId = "51511758-1568-99f5-a662-5d90b0c399d1",
                    Output = new UniTranslationOutput(
                        TaskId: "6ebb1c78-cab5-45e5-964c-2d5a35712bd3",
                        TaskStatus: DashScopeTaskStatus.Pending)
                });

        public static readonly RequestSnapshot<ModelRequest<UniTranslationInput>, ModelResponse<UniTranslationOutput, UniTranslationUsage>>
            CreateDownloadFailureTask = new(
                "uni-translation-create-download-failure",
                new ModelRequest<UniTranslationInput>
                {
                    Model = "qwen-mt-uni",
                    Input = new UniTranslationInput
                    {
                        // the url answers 403, the task ends with Success = false
                        FileUrl = "https://bailian-datahub-data-origin-prod.oss-cn-hangzhou.aliyuncs.com/1005426495169178/10024405/68abd1dea7b6404d8f7d7b9f7fbd332d.1716698936847.pdf",
                        TargetLang = "en"
                    }
                },
                new ModelResponse<UniTranslationOutput, UniTranslationUsage>
                {
                    RequestId = "cdc8a785-9df1-9eb6-a5d6-285b2cb50cc9",
                    Output = new UniTranslationOutput(
                        TaskId: "05e0cf4a-e04a-4071-9123-302bf0ed089a",
                        TaskStatus: DashScopeTaskStatus.Pending)
                });

        public static readonly RequestSnapshot<DashScopeTask<UniTranslationOutput, UniTranslationUsage>>
            GetTaskSuccess = new(
                "get-task-uni-translation-success",
                new DashScopeTask<UniTranslationOutput, UniTranslationUsage>(
                    "9078580f-17ad-9862-9923-d9dc6e34b3ad",
                    new UniTranslationOutput(
                        Success: true,
                        Code: 200,
                        Message: "Success",
                        RequestId: "6ebb1c78-cab5-45e5-964c-2d5a35712bd3",
                        Data: new UniTranslationData(
                            "document",
                            TranslatedFileUrl: "http://dashscope-a717.oss-cn-beijing.aliyuncs.com/7d/0e/20261125/1af09c21/69a51579f7074c33874cc45e62da95ca.xlsx?Expires=1798199641&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=Xvz9fJMnHTQ7GZrLoFwLFC%2BvQx0%3D"),
                        TaskId: "6ebb1c78-cab5-45e5-964c-2d5a35712bd3",
                        TaskStatus: DashScopeTaskStatus.Succeeded,
                        SubmitTime: new DateTime(2026, 11, 25, 19, 54, 0, 336),
                        ScheduledTime: new DateTime(2026, 11, 25, 19, 54, 0, 401),
                        EndTime: new DateTime(2026, 11, 25, 19, 54, 1, 62)),
                    new UniTranslationUsage(
                        24,
                        65,
                        89,
                        new UniTranslationInputTokenDetails(0, 24, 0, 0),
                        new UniTranslationOutputTokenDetails(0, 65, 0, 0))));

        public static readonly RequestSnapshot<DashScopeTask<UniTranslationOutput, UniTranslationUsage>>
            GetTaskFailed = new(
                "get-task-uni-translation-failed",
                new DashScopeTask<UniTranslationOutput, UniTranslationUsage>(
                    "71efcb30-d061-97d7-b2a3-891dcecffdd0",
                    new UniTranslationOutput(
                        Success: false,
                        Code: 400,
                        Message: "BadRequest.InputDownloadFailed: download HTTP 403 for https://bailian-datahub-data-origin-prod.oss-cn-hangzhou.aliyuncs.com/1005426495169178/10024405/68abd1dea7b6404d8f7d7b9f7fbd332d.1716698936847.pdf",
                        RequestId: "05e0cf4a-e04a-4071-9123-302bf0ed089a",
                        TaskId: "05e0cf4a-e04a-4071-9123-302bf0ed089a",
                        TaskStatus: DashScopeTaskStatus.Succeeded,
                        SubmitTime: new DateTime(2026, 11, 25, 19, 54, 0, 446),
                        ScheduledTime: new DateTime(2026, 11, 25, 19, 54, 0, 502),
                        EndTime: new DateTime(2026, 11, 25, 19, 54, 0, 823)),
                    // the server reports an empty usage for a failed translation
                    new UniTranslationUsage()));
    }
}
