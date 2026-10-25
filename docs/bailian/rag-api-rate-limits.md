> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 限流

> RAG API 各接口的请求频率限制（QPS）与超限处理建议。

<meta property="og:title" content="限流" />

为保障服务稳定性，各接口设有请求频率限制（QPS，每秒请求数）。超出限制时返回 HTTP `429 Too Many Requests`。

## 知识库管理接口 <span id="h-ragapi-ratelimit-kb" />

| 接口                                                     | 端点                             | QPS 上限 |
| ------------------------------------------------------ | ------------------------------ | ------ |
| 检索                                                     | `/rag/index/retrieve`          | 2,000  |
| [查询任务状态](/zh/model-studio/rag-api-get-sync-job-status) | `/rag/index_job/status`        | 20     |
| [查询文档列表](/zh/model-studio/rag-api-list-documents)      | `/rag/index/files`             | 15     |
| [查询文件详情](/zh/model-studio/rag-api-list-file-details)   | `/rag/list/index/file/details` | 10     |
| [创建知识库并导入](/zh/model-studio/rag-api-create-index)      | `/rag/index/create_v2`         | 10     |
| [提交导入任务](/zh/model-studio/rag-api-submit-sync-job)     | `/rag/index/job/create`        | 10     |
| [查询知识库列表](/zh/model-studio/rag-api-list-indices)       | `/rag/index/list`              | 10     |
| [更新知识库](/zh/model-studio/rag-api-update-index)         | `/rag/index/update`            | 10     |
| [删除知识库](/zh/model-studio/rag-api-delete-index)         | `/rag/index/delete`            | 10     |
| [删除文档](/zh/model-studio/rag-api-delete-document)       | `/rag/index/delete_file`       | 10     |
| [查询切片列表](/zh/model-studio/rag-api-list-chunks)         | `/pipeline/{id}/chunklist`     | 10     |
| [更新切片](/zh/model-studio/rag-api-update-chunk)          | `/rag/index/chunk/update`      | 10     |
| [删除切片](/zh/model-studio/rag-api-delete-chunk)          | `/rag/index/chunk/delete`      | 10     |
| [获取监控数据](/zh/model-studio/rag-api-get-index-monitor)   | `/rag/index/monitor`           | 1      |

## 数据导入接口 <span id="h-ragapi-ratelimit-import" />

| 接口                                                  | 端点                           | QPS 上限 |
| --------------------------------------------------- | ---------------------------- | ------ |
| [查询文件详情](/zh/model-studio/rag-api-describe-file)    | `/describeFile`              | 10     |
| [删除文件](/zh/model-studio/rag-api-delete-file)        | `/deleteFile`                | 10     |
| [申请上传租约](/zh/model-studio/rag-api-upload-lease)     | `/applyFileUploadLease`      | 10     |
| [注册文件](/zh/model-studio/rag-api-add-file)           | `/addFile`                   | 10     |
| [查询类目列表](/zh/model-studio/rag-api-list-category)    | `/listCategory`              | 5      |
| [新增类目](/zh/model-studio/rag-api-add-category)       | `/addCategory`               | 5      |
| [删除类目](/zh/model-studio/rag-api-delete-category)    | `/deleteCategory`            | 5      |
| [查询文件列表](/zh/model-studio/rag-api-list-file)        | `/listFile`                  | 5      |
| [查询连接器](/zh/model-studio/rag-api-get-connector)     | `/getConnector`              | 5      |
| [批量更新标签](/zh/model-studio/rag-api-batch-update-tag) | `/batchUpdateFileTag`        | 5      |
| [新增连接器](/zh/model-studio/rag-api-add-connector)     | `/addConnector`              | 2      |
| [OSS 批量导入](/zh/model-studio/rag-api-oss-import)     | `/addFilesFromAuthorizedOss` | 5      |

## 知识检索与问答 <span id="h-ragapi-ratelimit-qa" />

[知识检索](/zh/model-studio/knowledgesearch)（`/knowledge/search`）和[知识问答](/zh/model-studio/knowledgechat)（`/knowledge/chat`）的限流规则暂未公布，后续更新。

## 超限处理 <span id="h-ragapi-ratelimit-handling" />

触发限流时，接口返回：

```json
{
  "code": "Throttling",
  "status_code": 429,
  "message": "Requests rate limit exceeded, please try again later.",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

**建议：**

- 收到 429 后，使用指数退避策略重试（如间隔 1s → 2s → 4s）
- 批量操作控制并发数，避免瞬时突发
- 监控数据等低 QPS 接口，建议客户端缓存结果，减少调用频率

<Tip>
  如果当前限额无法满足业务需求，请通过工单申请提升配额。
</Tip>
