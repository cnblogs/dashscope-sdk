using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class ModelList
    {
        // The first page with a page size of one: the smallest query, the model carries every field.
        public static readonly RequestSnapshot<DashScopeModelList> FirstNoSse = new(
            "list-models",
            new DashScopeModelList(
                "c509e0b3-ea0b-9060-815c-692abcee5ab9",
                new DashScopeModelPage(
                    383,
                    1,
                    1,
                    new List<DashScopeModel>
                    {
                        new DashScopeModel(
                            "qwen4-max",
                            "Qwen4-Max",
                            "Qwen4-Max 是通义千问新一代旗舰多模态模型，融合文本生成、图片与视频理解及深度思考能力，适用于编程开发、办公、前端设计与复杂智能体任务。模型支持联网搜索、工具调用和结构化输出，兼容 OpenAI 与 Anthropic API，帮助开发者构建具备多模态理解与任务执行能力的智能应用",
                            "qwen",
                            new List<string> { "TG", "Reasoning", "VU" },
                            "aliyun-bailian",
                            new List<string>
                            {
                                "cache",
                                "function-calling",
                                "prefix-completion",
                                "structured-outputs",
                                "web-search",
                                "batch"
                            },
                            new List<DashScopeModelPriceRange>
                            {
                                new DashScopeModelPriceRange(
                                    "Default",
                                    new List<DashScopeModelPrice>
                                    {
                                        new DashScopeModelPrice("input_token", "10", "每百万tokens", "输入", "standard"),
                                        new DashScopeModelPrice("output_token", "30", "每百万tokens", "输出", "standard"),
                                        new DashScopeModelPrice("input_token_cache", "0.5", "每百万tokens", "输入（缓存命中）", "standard"),
                                        new DashScopeModelPrice("input_token_batch", "5", "每百万tokens", "输入（Batch File）", "standard"),
                                        new DashScopeModelPrice("output_token_batch", "15", "每百万tokens", "输出（Batch File）", "standard"),
                                        new DashScopeModelPrice("input_token_cache_creation_5m", "12.5", "每百万tokens", "5分钟显式缓存创建", "standard"),
                                        new DashScopeModelPrice("input_token_cache_creation_1h", "20", "每百万tokens", "1小时显式缓存创建", "standard"),
                                        new DashScopeModelPrice("input_token_cache_read", "0.5", "每百万tokens", "显式缓存命中", "standard"),
                                        new DashScopeModelPrice("input_token_batch_chat", "10", "每百万tokens", "输入（Batch Chat)", "standard"),
                                        new DashScopeModelPrice("output_token_batch_chat", "30", "每百万tokens", "输出（Batch Chat)", "standard")
                                    })
                            },
                            "2026-11-16 10:00:00",
                            new DashScopeModelInferenceMetadata(
                                new List<string> { "Text", "Image", "Video" },
                                new List<string> { "Text" },
                                null),
                            new DashScopeModelInfo(1000000, 991808, 131072, 262144, 983616, 262144),
                            null,
                            null)
                    })));

        // The second page of two models per page, the page index and the page size go to the query.
        public static readonly RequestSnapshot<DashScopeModelList> PageNoSse = new(
            "list-models-page",
            new DashScopeModelList(
                "a021681b-b941-94f3-b4c3-9d7c7a3b5df2",
                new DashScopeModelPage(
                    383,
                    2,
                    2,
                    new List<DashScopeModel>
                    {
                        new DashScopeModel(
                            "decision-model",
                            "决策模型",
                            "面向高频业务判断的结构化决策模型，可根据文本、图片或业务状态并行完成分类、是非判断与评分，并返回概率分布和置信度，适用于工单分流、内容审核、智能体路由与结果校验等场景。",
                            "qwen-domain-model",
                            new List<string> { "TG", "VU" },
                            "aliyun-bailian",
                            new List<string>(),
                            new List<DashScopeModelPriceRange>
                            {
                                new DashScopeModelPriceRange(
                                    "Default",
                                    new List<DashScopeModelPrice>
                                    {
                                        new DashScopeModelPrice("input_token", "0.3", "每百万tokens", "输入", "standard")
                                    })
                            },
                            "2026-11-25 21:28:05",
                            new DashScopeModelInferenceMetadata(new List<string> { "Text", "Image" }, new List<string> { "Text" }, null),
                            new DashScopeModelInfo(268288, 268288, null, null, null, null),
                            null,
                            null),
                        new DashScopeModel(
                            "decision-model-preview",
                            "决策模型（预览版）",
                            "面向高频业务判断的结构化决策模型，可根据文本或业务状态并行完成分类、是非判断与评分，并返回概率分布和置信度，适用于工单分流、内容审核、智能体路由与结果校验等场景。",
                            "qwen-domain-model",
                            new List<string> { "TG" },
                            "aliyun-bailian",
                            new List<string>(),
                            new List<DashScopeModelPriceRange>
                            {
                                new DashScopeModelPriceRange(
                                    "Default",
                                    new List<DashScopeModelPrice>
                                    {
                                        new DashScopeModelPrice("input_token", "0", "每百万tokens", "输入", null)
                                    })
                            },
                            "2026-09-24 15:24:10",
                            new DashScopeModelInferenceMetadata(new List<string> { "Text" }, new List<string> { "Text" }, null),
                            new DashScopeModelInfo(65536, 65536, null, null, null, null),
                            null,
                            null)
                    })));

        // The lookup of the official docs: an exact model id answers with a single model.
        public static readonly RequestSnapshot<DashScopeModelList> ByModelNoSse = new(
            "list-models-by-model",
            new DashScopeModelList(
                "f4da9746-0ac1-9531-8a35-e6ac71423a31",
                new DashScopeModelPage(
                    1,
                    1,
                    20,
                    new List<DashScopeModel>
                    {
                        new DashScopeModel(
                            "qwen3.7-max",
                            "Qwen3.7-Max",
                            "Qwen3.7系列中规模最大、综合能力最强的Max模型，当前开放纯文本模型能力供体验。Qwen3.7是面向智能体时代的新一代旗舰模型，核心优势在于智能体能力的广度与深度：在编程、办公与生产力、长周期自主执行方面均能出色胜任各项任务。",
                            "qwen",
                            new List<string> { "Reasoning", "TG" },
                            "aliyun-bailian",
                            new List<string>
                            {
                                "batch",
                                "cache",
                                "function-calling",
                                "model-experience",
                                "prefix-completion",
                                "web-search",
                                "structured-outputs"
                            },
                            new List<DashScopeModelPriceRange>
                            {
                                new DashScopeModelPriceRange(
                                    "Default",
                                    new List<DashScopeModelPrice>
                                    {
                                        new DashScopeModelPrice("input_token", "12", "每百万tokens", "输入", "standard"),
                                        new DashScopeModelPrice("output_token", "36", "每百万tokens", "输出", "standard"),
                                        new DashScopeModelPrice("input_token_cache", "2.4", "每百万tokens", "输入（缓存命中）", "standard"),
                                        new DashScopeModelPrice("input_token_batch", "6", "每百万tokens", "输入（Batch File）", "standard"),
                                        new DashScopeModelPrice("output_token_batch", "18", "每百万tokens", "输出（Batch File）", "standard"),
                                        new DashScopeModelPrice("input_token_cache_creation_5m", "15", "每百万tokens", "5分钟显式缓存创建", "standard"),
                                        new DashScopeModelPrice("input_token_cache_read", "1.2", "每百万tokens", "显式缓存命中", "standard"),
                                        new DashScopeModelPrice("input_token_batch_chat", "12", "每百万tokens", "输入（Batch Chat)", "standard"),
                                        new DashScopeModelPrice("output_token_batch_chat", "36", "每百万tokens", "输出（Batch Chat)", "standard")
                                    })
                            },
                            "2026-05-21 14:37:11",
                            new DashScopeModelInferenceMetadata(new List<string> { "Text" }, new List<string> { "Text" }, null),
                            new DashScopeModelInfo(1000000, 991808, 131072, 262144, 983616, 131072),
                            "qwen3.7-max-2026-05-20",
                            null)
                    })));

        // Every scalar filter and the repeated array filters in one query, the names and the prices are in English.
        public static readonly RequestSnapshot<DashScopeModelList> FilteredNoSse = new(
            "list-models-filtered",
            new DashScopeModelList(
                "97eb29e7-7aaf-9ba9-b0cf-fa43eeaf7d92",
                new DashScopeModelPage(
                    3,
                    1,
                    2,
                    new List<DashScopeModel>
                    {
                        new DashScopeModel(
                            "deepseek-v4.1-flash-1017",
                            "DeepSeek-V4.1-Flash-1017",
                            "DeepSeek-V4.1-Flash-1017 is the latest iteration of V4.1-Flash, trained with self-feedback-based reinforcement learning to deliver substantial gains over its predecessor across benchmarks such as Terminal-Bench 4.0, DeepSWE v1.1, and ProgramBench, while shortening task runtime and output length to reach a new performance–cost Pareto optimum across multiple harnesses. Its ability to build complete projects from scratch is further strengthened, enabling \"wish-based\" programming tasks such as game development and 3D modeling, while retaining the same 552B-parameter MoE, native multimodal visual understanding, 1M-token context window, and 384K-token maximum output.",
                            "deepseek",
                            new List<string> { "TG", "Reasoning", "VU" },
                            "aliyun-bailian",
                            new List<string>
                            {
                                "cache",
                                "function-calling",
                                "structured-outputs",
                                "web-search"
                            },
                            new List<DashScopeModelPriceRange>
                            {
                                new DashScopeModelPriceRange(
                                    "Default",
                                    new List<DashScopeModelPrice>
                                    {
                                        new DashScopeModelPrice("input_token", "1", "Per 1M tokens", "Input", "offpeak"),
                                        new DashScopeModelPrice("input_token", "2", "Per 1M tokens", "Input", "peak"),
                                        new DashScopeModelPrice("output_token", "4", "Per 1M tokens", "Output", "offpeak"),
                                        new DashScopeModelPrice("output_token", "8", "Per 1M tokens", "Output", "peak"),
                                        new DashScopeModelPrice("input_token_cache", "0.02", "Per 1M tokens", "Input(Implicit Cache)", "offpeak"),
                                        new DashScopeModelPrice("input_token_cache", "0.04", "Per 1M tokens", "Input(Implicit Cache)", "peak")
                                    })
                            },
                            "2026-10-19 00:27:49",
                            new DashScopeModelInferenceMetadata(new List<string> { "Text", "Image" }, new List<string> { "Text" }, null),
                            new DashScopeModelInfo(1000000, 1000000, 393216, 393216, 1000000, 393216),
                            null,
                            null),
                        new DashScopeModel(
                            "deepseek-v4.1-flash",
                            "DeepSeek-V4.1-Flash",
                            "DeepSeek-V4.1-Flash is the lightweight flagship of DeepSeek's new architecture family, packing 552B total MoE parameters to deliver flagship-surpassing intelligence across key benchmarks, including out-performing DeepSeek-V4-Pro. It adopts a Causal Encoder-Decoder asymmetric design with 8B active parameters for input and 16B for output, and offers native multimodal visual understanding. KV Cache usage is cut to one-quarter of the previous generation's HBM and one-eighth of its SSD storage, dramatically lowering costs for long-context and agentic workloads. With a 1M-token context window and up to 384K-token maximum output, it balances high throughput, low latency, and exceptional cost efficiency.",
                            "deepseek",
                            new List<string> { "TG", "Reasoning", "VU" },
                            "aliyun-bailian",
                            new List<string>
                            {
                                "cache",
                                "function-calling",
                                "structured-outputs",
                                "web-search"
                            },
                            new List<DashScopeModelPriceRange>
                            {
                                new DashScopeModelPriceRange(
                                    "Default",
                                    new List<DashScopeModelPrice>
                                    {
                                        new DashScopeModelPrice("input_token", "1", "Per 1M tokens", "Input", "offpeak"),
                                        new DashScopeModelPrice("input_token", "2", "Per 1M tokens", "Input", "peak"),
                                        new DashScopeModelPrice("output_token", "4", "Per 1M tokens", "Output", "offpeak"),
                                        new DashScopeModelPrice("output_token", "8", "Per 1M tokens", "Output", "peak"),
                                        new DashScopeModelPrice("input_token_cache", "0.02", "Per 1M tokens", "Input(Implicit Cache)", "offpeak"),
                                        new DashScopeModelPrice("input_token_cache", "0.04", "Per 1M tokens", "Input(Implicit Cache)", "peak")
                                    })
                            },
                            "2026-09-13 00:13:01",
                            new DashScopeModelInferenceMetadata(new List<string> { "Text", "Image" }, new List<string> { "Text" }, null),
                            new DashScopeModelInfo(1000000, 1000000, 393216, 393216, 1000000, 393216),
                            null,
                            null)
                    })));

        // A filter nothing matches: an empty page is a success, the deployment filters are part of the query.
        public static readonly RequestSnapshot<DashScopeModelList> EmptyNoSse = new(
            "list-models-empty",
            new DashScopeModelList(
                "9db91fa8-59e4-9eeb-863c-359b8cd7b6f6",
                new DashScopeModelPage(0, 1, 2, new List<DashScopeModel>())));

        // The service rejects the paging parameter with status 200 and success false, the client turns it
        // into an exception that keeps the error detail.
        public static readonly RequestSnapshot<DashScopeError> InvalidPageNoNoSse = new(
            "list-models-invalid-page-no",
            new DashScopeError
            {
                Code = "10110400",
                Message = "Invalid request parameter: page_no",
                RequestId = "fd1de83b-4e30-9602-956b-a05e64e7ba65"
            });

        // A parameter that is not a number is rejected on the way in with status 400.
        public static readonly RequestSnapshot<DashScopeError> InvalidPageSizeNoSse = new(
            "list-models-invalid-page-size",
            new DashScopeError
            {
                Code = "10110400",
                Message = "请求参数不合法: Failed to convert value of type 'java.lang.String' to required type 'java.lang.Long'; nested exception is java.lang.NumberFormatException: For input string: \"abc\"",
                RequestId = "56799cb5-479c-9655-bbb4-8ad12753628b"
            });
    }
}
