using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class OpenAiCompatibleBatches
    {
        public static readonly RequestSnapshot<DashScopeCreateBatchRequest, DashScopeBatch> CreateBatchNoSse =
            new(
                "create-batch-compatible",
                new DashScopeCreateBatchRequest()
                {
                    InputFileId = "file-batch-61875e1242854691b8bfd17d",
                    Endpoint = "/v1/chat/ds-test",
                    CompletionWindow = "24h",
                    Metadata = new DashScopeBatchMetadata()
                    {
                        DsName = "测试任务",
                        DsDescription = "任务描述",
                        DsBatchFinishCallback = "https://www.cnblogs.com"
                    }
                },
                new DashScopeBatch(
                    "batch_fa05d570-fa04-4935-8b80-2b9c1a4b3ee4",
                    "batch",
                    "/v1/chat/ds-test",
                    null,
                    "file-batch-61875e1242854691b8bfd17d",
                    "24h",
                    "validating",
                    null,
                    null,
                    1776414394,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    new DashScopeBatchRequestCounts(0, 0, 0),
                    new DashScopeBatchMetadata
                    {
                        DsName = "测试任务",
                        DsDescription = "任务描述",
                        DsBatchFinishCallback = "https://www.cnblogs.com"
                    }));

        public static readonly RequestSnapshot<DashScopeBatch> GetInvalidBatchNoSse =
            new(
                "get-invalid-batch-compatible",
                new DashScopeBatch(
                    "batch_fa05d570-fa04-4935-8b80-2b9c1a4b3ee4",
                    "batch",
                    "/v1/chat/ds-test",
                    new DashScopeBatchErrorList(
                        "list",
                        new List<DashScopeBatchErrorData>()
                        {
                            new(
                                "url_not_found",
                                "The provided url '/v1/chat/completion' is not supported by the Batch API.",
                                "url",
                                1)
                        }),
                    "file-batch-61875e1242854691b8bfd17d",
                    "24h",
                    "failed",
                    null,
                    null,
                    1776414394,
                    null,
                    null,
                    null,
                    null,
                    1776414394,
                    null,
                    null,
                    null,
                    new DashScopeBatchRequestCounts(0, 0, 0),
                    new DashScopeBatchMetadata()
                    {
                        DsName = "测试任务",
                        DsBatchFinishCallback = "https://www.cnblogs.com",
                        DsDescription = "任务描述"
                    }));

        public static readonly RequestSnapshot<DashScopeBatch> GetInProgressBatchNoSse =
            new(
                "get-inprogress-batch-compatible",
                new DashScopeBatch(
                    "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
                    "batch",
                    "/v1/chat/completions",
                    null,
                    "file-batch-c015426ce301481bb13c76b4",
                    "24h",
                    "in_progress",
                    null,
                    null,
                    1776420698,
                    1776420698,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    new DashScopeBatchRequestCounts(2, 0, 0),
                    new DashScopeBatchMetadata()
                    {
                        DsName = "测试任务",
                        DsBatchFinishCallback = "https://www.cnblogs.com",
                        DsDescription = "任务描述"
                    }));

        public static readonly RequestSnapshot<DashScopeBatch> GetCancelledBatchNoSse =
            new(
                "get-cancelled-batch-compatible",
                new DashScopeBatch(
                    "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
                    "batch",
                    "/v1/chat/completions",
                    null,
                    "file-batch-c015426ce301481bb13c76b4",
                    "24h",
                    "cancelled",
                    null,
                    "file-batch_output-e034f08204314479a8ac4f18",
                    1776420698,
                    1776420698,
                    null,
                    null,
                    null,
                    null,
                    null,
                    1776423809,
                    1776423809,
                    new DashScopeBatchRequestCounts(2, 0, 2),
                    new DashScopeBatchMetadata()
                    {
                        DsName = "测试任务",
                        DsBatchFinishCallback = "https://www.cnblogs.com",
                        DsDescription = "任务描述"
                    }));

        public static readonly RequestSnapshot<DashScopeBatch> GetCompletedBatchNoSse =
            new(
                "get-completed-batch-compatible",
                new DashScopeBatch(
                    "batch_5b57e198-203e-49c8-bcc3-65e997d51754",
                    "batch",
                    "/v1/chat/completions",
                    null,
                    "file-batch-c015426ce301481bb13c76b4",
                    "24h",
                    "completed",
                    "file-batch_output-c5f57bf1fbc749cdae9676ba",
                    null,
                    1776423924,
                    1776423924,
                    null,
                    1776433053,
                    1776433053,
                    null,
                    null,
                    null,
                    null,
                    new DashScopeBatchRequestCounts(2, 2, 0),
                    new DashScopeBatchMetadata()
                    {
                        DsName = "测试任务",
                        DsBatchFinishCallback = "https://www.cnblogs.com",
                        DsDescription = "任务描述"
                    }));

        public static readonly RequestSnapshot<DashScopeBatchList> EmptyBatchListNoSse =
            new(
                "list-batches-empty-compatible",
                new DashScopeBatchList("list", null, null, null, false));

        public static readonly RequestSnapshot<DashScopeBatchList> SearchByDsNameBatchListNoSse =
            new(
                "list-batches-search-ds-name-compatible",
                new DashScopeBatchList(
                    "list",
                    new List<DashScopeBatch>()
                    {
                        new(
                            "batch_5b57e198-203e-49c8-bcc3-65e997d51754",
                            "batch",
                            "/v1/chat/completions",
                            null,
                            "file-batch-c015426ce301481bb13c76b4",
                            "24h",
                            "completed",
                            "file-batch_output-c5f57bf1fbc749cdae9676ba",
                            null,
                            1776423924,
                            1776423924,
                            null,
                            1776433053,
                            1776433053,
                            null,
                            null,
                            null,
                            null,
                            new DashScopeBatchRequestCounts(2, 2, 0),
                            new DashScopeBatchMetadata()
                            {
                                DsName = "测试任务",
                                DsBatchFinishCallback = "https://www.cnblogs.com",
                                DsDescription = "任务描述"
                            }),
                        new(
                            "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
                            "batch",
                            "/v1/chat/completions",
                            null,
                            "file-batch-c015426ce301481bb13c76b4",
                            "24h",
                            "cancelled",
                            null,
                            "file-batch_output-e034f08204314479a8ac4f18",
                            1776420698,
                            1776420698,
                            null,
                            null,
                            null,
                            null,
                            null,
                            1776423809,
                            1776423809,
                            new DashScopeBatchRequestCounts(2, 0, 2),
                            new DashScopeBatchMetadata()
                            {
                                DsName = "测试任务",
                                DsBatchFinishCallback = "https://www.cnblogs.com",
                                DsDescription = "任务描述"
                            })
                    },
                    "batch_5b57e198-203e-49c8-bcc3-65e997d51754",
                    "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
                    true));

        public static readonly RequestSnapshot<DashScopeBatchList> SearchByDsNameSecondPageNoSse =
            new(
                "list-batches-search-ds-name-second-page-compatible",
                new DashScopeBatchList(
                    "list",
                    new List<DashScopeBatch>()
                    {
                        new(
                            "batch_ee667cab-f2ee-409d-94f9-23618ca54016",
                            "batch",
                            "/v1/chat/completions",
                            new DashScopeBatchErrorList(
                                "list",
                                new List<DashScopeBatchErrorData>()
                                {
                                    new(
                                        "mismatched_test_url",
                                        "The provided URL '/v1/chat/completions' does not match the test model 'batch-test-model'.",
                                        "url",
                                        1),
                                    new(
                                        "mismatched_test_url",
                                        "The provided URL '/v1/chat/completions' does not match the test model 'batch-test-model'.",
                                        "url",
                                        2),
                                }),
                            "file-batch-1ba2f488428d4daa9e7058a2",
                            "24h",
                            "failed",
                            null,
                            null,
                            1776420656,
                            null,
                            null,
                            null,
                            null,
                            1776420656,
                            null,
                            null,
                            null,
                            new DashScopeBatchRequestCounts(0, 0, 0),
                            new DashScopeBatchMetadata()
                            {
                                DsName = "测试任务",
                                DsDescription = "任务描述",
                                DsBatchFinishCallback = "https://www.cnblogs.com",
                            })
                    },
                    "batch_ee667cab-f2ee-409d-94f9-23618ca54016",
                    "batch_ee667cab-f2ee-409d-94f9-23618ca54016",
                    true));

        public static readonly RequestSnapshot<DashScopeBatchList> FilterBatchListByStatusNoSse =
            new(
                "list-batches-search-status-compatible",
                new DashScopeBatchList(
                    "list",
                    new List<DashScopeBatch>()
                    {
                        new(
                            "batch_5b57e198-203e-49c8-bcc3-65e997d51754",
                            "batch",
                            "/v1/chat/completions",
                            null,
                            "file-batch-c015426ce301481bb13c76b4",
                            "24h",
                            "completed",
                            "file-batch_output-c5f57bf1fbc749cdae9676ba",
                            null,
                            1776423924,
                            1776423924,
                            null,
                            1776433053,
                            1776433053,
                            null,
                            null,
                            null,
                            null,
                            new DashScopeBatchRequestCounts(2, 2, 0),
                            new DashScopeBatchMetadata()
                            {
                                DsName = "测试任务",
                                DsBatchFinishCallback = "https://www.cnblogs.com",
                                DsDescription = "任务描述"
                            }),
                        new(
                            "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
                            "batch",
                            "/v1/chat/completions",
                            null,
                            "file-batch-c015426ce301481bb13c76b4",
                            "24h",
                            "cancelled",
                            null,
                            "file-batch_output-e034f08204314479a8ac4f18",
                            1776420698,
                            1776420698,
                            null,
                            null,
                            null,
                            null,
                            null,
                            1776423809,
                            1776423809,
                            new DashScopeBatchRequestCounts(2, 0, 2),
                            new DashScopeBatchMetadata()
                            {
                                DsName = "测试任务",
                                DsBatchFinishCallback = "https://www.cnblogs.com",
                                DsDescription = "任务描述"
                            })
                    },
                    "batch_5b57e198-203e-49c8-bcc3-65e997d51754",
                    "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
                    true));

        public static readonly RequestSnapshot<DashScopeBatchList> FilterBatchListByTimeNoSse =
            new(
                "list-batches-filter-by-time-compatible",
                new DashScopeBatchList(
                    "list",
                    new List<DashScopeBatch>()
                    {
                        new(
                            "batch_fa05d570-fa04-4935-8b80-2b9c1a4b3ee4",
                            "batch",
                            "/v1/chat/ds-test",
                            new DashScopeBatchErrorList(
                                "list",
                                new List<DashScopeBatchErrorData>()
                                {
                                    new(
                                        "url_not_found",
                                        "The provided url '/v1/chat/completion' is not supported by the Batch API.",
                                        "url",
                                        1)
                                }),
                            "file-batch-61875e1242854691b8bfd17d",
                            "24h",
                            "failed",
                            null,
                            null,
                            1776414394,
                            null,
                            null,
                            null,
                            null,
                            1776414394,
                            null,
                            null,
                            null,
                            new DashScopeBatchRequestCounts(0, 0, 0),
                            new DashScopeBatchMetadata()
                            {
                                DsName = "测试任务",
                                DsBatchFinishCallback = "https://www.cnblogs.com",
                                DsDescription = "任务描述"
                            })
                    },
                    "batch_fa05d570-fa04-4935-8b80-2b9c1a4b3ee4",
                    "batch_fa05d570-fa04-4935-8b80-2b9c1a4b3ee4",
                    false));

        public static readonly RequestSnapshot<DashScopeBatchList> FilterBatchListByInputFileIdsNoSse =
            new(
                "list-batches-filter-by-input-file-id-compatible",
                new DashScopeBatchList(
                    "list",
                    new List<DashScopeBatch>()
                    {
                        new(
                            "batch_5b57e198-203e-49c8-bcc3-65e997d51754",
                            "batch",
                            "/v1/chat/completions",
                            null,
                            "file-batch-c015426ce301481bb13c76b4",
                            "24h",
                            "completed",
                            "file-batch_output-c5f57bf1fbc749cdae9676ba",
                            null,
                            1776423924,
                            1776423924,
                            null,
                            1776433053,
                            1776433053,
                            null,
                            null,
                            null,
                            null,
                            new DashScopeBatchRequestCounts(2, 2, 0),
                            new DashScopeBatchMetadata()
                            {
                                DsName = "测试任务",
                                DsBatchFinishCallback = "https://www.cnblogs.com",
                                DsDescription = "任务描述"
                            }),
                        new(
                            "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
                            "batch",
                            "/v1/chat/completions",
                            null,
                            "file-batch-c015426ce301481bb13c76b4",
                            "24h",
                            "cancelled",
                            null,
                            "file-batch_output-e034f08204314479a8ac4f18",
                            1776420698,
                            1776420698,
                            null,
                            null,
                            null,
                            null,
                            null,
                            1776423809,
                            1776423809,
                            new DashScopeBatchRequestCounts(2, 0, 2),
                            new DashScopeBatchMetadata()
                            {
                                DsName = "测试任务",
                                DsBatchFinishCallback = "https://www.cnblogs.com",
                                DsDescription = "任务描述"
                            })
                    },
                    "batch_5b57e198-203e-49c8-bcc3-65e997d51754",
                    "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
                    false));

        public static readonly RequestSnapshot<DashScopeBatch> CancelBatchNoSse =
            new(
                "cancel-batch-compatible",
                new(
                    "batch_2ecfc43e-439e-4443-bfe8-9a36b89456d0",
                    "batch",
                    "/v1/chat/completions",
                    null,
                    "file-batch-c015426ce301481bb13c76b4",
                    "24h",
                    "cancelling",
                    null,
                    null,
                    1776420698,
                    1776420698,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    new DashScopeBatchRequestCounts(2, 0, 0),
                    new DashScopeBatchMetadata()
                    {
                        DsName = "测试任务",
                        DsBatchFinishCallback = "https://www.cnblogs.com",
                        DsDescription = "任务描述"
                    }));
    }
}
