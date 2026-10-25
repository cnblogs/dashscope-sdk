> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 提交导入任务

> 向已创建的知识库追加导入文档，返回导入任务 ID（ingestionId），可通过查询任务状态接口跟踪执行进度。

<meta property="og:title" content="提交导入任务" />

## 前提 <span id="h-ragapi-submit-sync-job-prereq" />

已获取 API Key 和业务空间 ID，并完成鉴权配置，详见[API 总览](/zh/model-studio/rag-api-overview)与[鉴权说明](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-submit-sync-job-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/index/job/create`

向已创建的知识库追加导入文档。

## 请求体 <span id="h-ragapi-submit-sync-job-req" />

| 字段              | 必填 | 类型              | 说明                                                                                                                                                                                     |
| --------------- | -- | --------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `indexId`       | 是  | string          | 知识库 ID                                                                                                                                                                                 |
| `sourceType`    | 否  | string          | 数据来源类型。`DATA_CENTER_CATEGORY` 表示按类目导入（需指定 categoryIds），`DATA_CENTER_FILE` 表示按文件导入（需指定 docIds）。不传此参数时，默认导入整个数据中心的全部文件，请谨慎使用                                                             |
| `categoryIds`   | 否  | `array<string>` | 类目 ID 列表。当 sourceType 为 `DATA_CENTER_CATEGORY` 时必填                                                                                                                                     |
| `docIds`        | 否  | `array<string>` | 文件 ID 列表。当 sourceType 为 `DATA_CENTER_FILE` 时必填。注意参数名为 `docIds`，不是 `documentIds` 或 `fileIds`；传错名称时服务端会忽略该参数并返回 `Index.InvalidParameter`（Required parameter(dataSource.fileIds) missing） |
| `chunkMode`     | 否  | string          | 切片模式，用于对本次导入的文件单独设定分段策略（与知识库创建时的全局配置独立）。`h1`\~`h5` 按对应层级的标题切分（`h1` 为一级标题，依此类推，最深支持到 `h5` 五级标题），`length` 按固定长度切分，`page` 按页切分，`regex` 按自定义正则切分                                           |
| `chunkSize`     | 否  | integer         | 切片大小（字符数），取值范围 1\~6000。chunkMode 为 `length` 时必填；chunkMode 为 `h1`\~`h5` 时若指定则一并考虑（未传入时使用默认值 500）                                                                                        |
| `overlapSize`   | 否  | integer         | 切片重叠大小（字符数），取值范围 0\~1024。仅 chunkMode 为 `length` 时生效，按标题切分（`h1`\~`h5`）时不生效                                                                                                              |
| `separator`     | 否  | string          | 切片分隔符，仅 chunkMode 为 `regex` 时生效；当 chunkMode 为 `regex` 时必填，否则返回 `Index.InvalidParameter`（Separator can't be null）                                                                       |
| `enableHeaders` | 否  | boolean         | 是否启用标题提取，默认 false                                                                                                                                                                      |

## 请求示例 <span id="h-ragapi-submit-sync-job-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/job/create" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "indexId": "your-kb-id",
    "categoryIds": ["your-category-id"]
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-submit-sync-job-resp" />

提交成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "updated_at": 1782191904527,
    "created_at": 1782191904527,
    "ingestionId": "your-ingestion-id",
    "pipelineId": "your-kb-id",
    "status": "PENDING"
  },
  "success": true,
  "message": "success",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-submit-sync-job-dto" />

| 字段            | 类型      | 说明                                                                                                      |
| ------------- | ------- | ------------------------------------------------------------------------------------------------------- |
| `code`        | string  | 响应码，成功时为 `Success`                                                                                      |
| `status_code` | integer | HTTP 状态码                                                                                                |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID                                                                                     |
| `success`     | boolean | 请求是否成功                                                                                                  |
| `message`     | string  | 提示信息，成功时为 `success`                                                                                     |
| `status`      | string  | 请求状态：`SUCCESS` 或 `FAILED`                                                                               |
| `data`        | object  | 响应数据。子字段 `pipelineId`（string，知识库 ID）、`ingestionId`（string，导入任务 ID）、`status`（string，任务状态，提交后为 `PENDING`） |

## 错误码 <span id="h-ragapi-submit-sync-job-err" />

| HTTP 状态码 | 错误码（`code`）              | 说明                                              |
| -------- | ------------------------ | ----------------------------------------------- |
| 400      | `Index.InvalidParameter` | 请求参数无效：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`          | API Key 无效或缺失：`Invalid API-key provided.`       |
