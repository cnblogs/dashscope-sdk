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
                    new DashScopeBatchMetadata()
                    {
                        DsName = "测试任务",
                        DsDescription = "任务描述",
                        DsBatchFinishCallback = "https://www.cnblogs.com"
                    }));
    }
}
