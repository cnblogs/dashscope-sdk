> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询任务状态

> 查询指定导入任务的执行状态和文档处理详情。index_id 和 job_id 必须同时传入。

<meta property="og:title" content="查询任务状态" />

## 前提 <span id="h-ragapi-get-sync-job-status-prereq" />

已获取 API Key 和业务空间 ID，并完成鉴权配置，详见[API 总览](/zh/model-studio/rag-api-overview)与[鉴权说明](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-get-sync-job-status-endpoint" />

<strong>GET</strong> `/api/v1/indices/rag/index_job/status`

查询指定导入任务的执行状态和文档处理详情。`index_id` 和 `job_id` 必须同时传入。

<Note>
  当知识库没有进行中的导入任务时，该接口可能返回 `SystemError`。建议在[提交导入任务](/zh/model-studio/rag-api-submit-sync-job)后调用。
</Note>

## 请求参数 <span id="h-ragapi-get-sync-job-status-req" />

以下参数通过 query string 传递。

| 参数            | 必填 | 类型      | 说明                                                                                                                                                                                                                  |
| ------------- | -- | ------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `index_id`    | 是  | string  | 知识库 ID，对应创建知识库时返回的 `pipelineId`                                                                                                                                                                                     |
| `job_id`      | 是  | string  | 导入任务 ID。[提交导入任务](/zh/model-studio/rag-api-submit-sync-job)接口的返回字段 `ingestionId`，也可在[文档列表](/zh/model-studio/rag-api-list-documents)的 `ingestion_id` 字段中找到。`index_id` 和 `job_id` 必须同时传入，仅传 `index_id` 会返回 SystemError |
| `page_number` | 否  | integer | 页码，从 1 开始，默认 1。任务含较多文档时可分页查询                                                                                                                                                                                        |
| `page_size`   | 否  | integer | 每页返回的文档数量，默认 10                                                                                                                                                                                                     |

## 请求示例 <span id="h-ragapi-get-sync-job-status-eg" />

```bash
curl -G "$BASE_URL/api/v1/indices/rag/index_job/status" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -d "index_id=your-kb-id" \
  -d "job_id=your-job-id"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-get-sync-job-status-resp" />

查询成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "id": "your-job-id",
    "ingestion_status": "COMPLETED",
    "ingestion_message": "COMPLETED",
    "total_count": 4,
    "rows": [
      {
        "code": "FINISH",
        "doc_id": "file_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "doc_name": "sample_document",
        "doc_type": "pdf",
        "size": 191166,
        "status": "FINISH",
        "message": "索引构建成功",
        "ingestion_id": "your-job-id",
        "dataSync": false,
        "canDelete": true,
        "meta_extract_info": [],
        "gmt_modified": 1782209511000
      }
    ],
    "page_number": 1,
    "page_size": 10,
    "started_at": 1782209506000,
    "ended_at": 1782209512000,
    "created_at": 1782209506000,
    "updated_at": 1782209512000
  },
  "success": true,
  "message": "success",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-get-sync-job-status-dto" />

| 字段            | 类型      | 说明                        |
| ------------- | ------- | ------------------------- |
| `code`        | string  | 响应码，成功时为 `Success`        |
| `status_code` | integer | HTTP 状态码                  |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID       |
| `success`     | boolean | 请求是否成功                    |
| `message`     | string  | 提示信息，成功时为 `success`       |
| `status`      | string  | 请求状态：`SUCCESS` 或 `FAILED` |
| `data`        | object  | 任务状态与处理详情，子字段见下表          |

### data 字段 <span id="h-ragapi-get-sync-job-status-data" />

| 字段                  | 类型      | 说明                                                                                  |
| ------------------- | ------- | ----------------------------------------------------------------------------------- |
| `id`                | string  | 导入任务 ID                                                                             |
| `ingestion_status`  | string  | 任务整体状态：`PENDING`（等待中）、`RUNNING`（进行中）、`COMPLETED`（已完成）                               |
| `ingestion_message` | string  | 任务状态描述，通常与 `ingestion_status` 一致                                                    |
| `total_count`       | integer | 任务涉及的文档总数。任务进行中时可能为 0                                                               |
| `rows`              | array   | 每个文档的处理详情。结构与[查询文档列表](/zh/model-studio/rag-api-list-documents)接口的 `rows` 一致。元素字段见下表 |
| `page_number`       | integer | 当前页码                                                                                |
| `page_size`         | integer | 每页数量                                                                                |
| `started_at`        | integer | 任务开始时间（毫秒级 Unix 时间戳）                                                                |
| `ended_at`          | integer | 任务结束时间（毫秒级 Unix 时间戳）。进行中时为 `null`                                                   |
| `created_at`        | integer | 任务创建时间（毫秒级 Unix 时间戳）                                                                |
| `updated_at`        | integer | 任务最后更新时间（毫秒级 Unix 时间戳）                                                              |

### rows 元素字段 <span id="h-ragapi-get-sync-job-status-rows" />

| 字段                  | 类型      | 说明                                 |
| ------------------- | ------- | ---------------------------------- |
| `doc_id`            | string  | 文档 ID                              |
| `doc_name`          | string  | 文档名称                               |
| `doc_type`          | string  | 文档类型                               |
| `code`              | string  | 细粒度处理状态码，如 `FINISH`、`PARSE_FAILED` |
| `status`            | string  | 粗粒度处理状态                            |
| `message`           | string  | 处理状态描述                             |
| `size`              | integer | 文件大小（字节）                           |
| `ingestion_id`      | string  | 所属导入任务 ID                          |
| `connectorId`       | string  | 关联的连接器 ID，未使用连接器时为空字符串             |
| `categoryId`        | string  | 文件所属类目 ID                          |
| `categoryName`      | string  | 文件所属类目名称                           |
| `tags`              | string  | 文件标签，多个标签以逗号分隔                     |
| `dataSync`          | boolean | 该文件是否参与数据同步                        |
| `canDelete`         | boolean | 该文件是否可删除                           |
| `source_id`         | string  | 文件来源标识                             |
| `gmt_modified`      | integer | 最后修改时间，Unix 毫秒时间戳                  |
| `meta_extract_info` | array   | 元数据抽取结果                            |

## 错误码 <span id="h-ragapi-get-sync-job-status-err" />

| HTTP 状态码 | 错误码（`code`）              | 说明                                              |
| -------- | ------------------------ | ----------------------------------------------- |
| 400      | `Index.InvalidParameter` | 请求参数无效：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`          | API Key 无效或缺失：`Invalid API-key provided.`       |
