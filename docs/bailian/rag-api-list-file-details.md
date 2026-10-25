> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询文件详情列表

> 查询文档的详细信息，包括切片数量和解析状态。

<meta property="og:title" content="查询文件详情列表" />

## 前提 <span id="h-ragapi-list-file-details-prereq" />

已完成 Endpoint 与鉴权配置，详见 [API 总览](/zh/model-studio/rag-api-overview)和[认证](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-list-file-details-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/list/index/file/details`

查询指定知识库中文档的详细信息，包括切片数量和解析状态。

<Note>
  该接口使用 camelCase 参数命名（如 `pageNumber`、`pageSize`），与其他使用 snake\_case 的接口不同。
</Note>

## 请求体 <span id="h-ragapi-list-file-details-req" />

| 字段           | 必填 | 类型      | 说明                               |
| ------------ | -- | ------- | -------------------------------- |
| `indexId`    | 是  | string  | 知识库 ID                           |
| `pageNumber` | 否  | integer | 页码，从 1 开始，默认 1。注意使用 camelCase 命名 |
| `pageSize`   | 否  | integer | 每页返回的文档数量，默认 10，最大 10            |

## 请求示例 <span id="h-ragapi-list-file-details-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/list/index/file/details" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "indexId": "your_index_id",
    "pageNumber": 1,
    "pageSize": 10
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-list-file-details-resp" />

查询成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "page_number": 1,
    "total_count": 1,
    "rows": [
      {
        "chunkSize": 600,
        "connectorId": "conn_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "overlapSize": 100,
        "doc_type": "md",
        "gmt_modified": 1782184640000,
        "doc_id": "file_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "categoryName": "默认类目",
        "separator": " |,|，|。|？|！|\n|\\?|\\!",
        "tags": [],
        "ingestion_id": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
        "size": 21,
        "doc_name": "a",
        "chunkMode": "auto",
        "canDelete": true,
        "source_id": "cate_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "categoryId": "cate_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "status": "FINISH"
      }
    ],
    "page_size": 1
  },
  "success": true,
  "message": "success",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-list-file-details-dto" />

| 字段            | 类型      | 说明                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| ------------- | ------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `code`        | string  | 响应码，成功时为 `Success`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| `status_code` | integer | HTTP 状态码                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| `data`        | object  | 业务数据。子字段 `page_number`（integer，当前页码）、`page_size`（integer，当前每页条数，与请求一致）、`total_count`（integer，文档总数）、`rows`（array\<object>，文件详情列表。元素字段：`doc_id`（string，文档唯一标识）、`doc_name`（string，文档名称）、`doc_type`（string，文档类型，如 pdf、docx、txt）、`status`（string，文档处理状态：`INIT`（初始化）、`PARSING`（解析中）、`PARSE_SUCCESS`（解析成功）、`FAIL`（失败））、`size`（integer，文档大小，单位字节）、`gmt_modified`（integer，最后修改时间，毫秒级 Unix 时间戳）、`categoryId`（string，所属类目 ID）、`categoryName`（string，所属类目名称）、`connectorId`（string，所属连接器 ID）、`source_id`（string，数据源 ID）、`ingestion_id`（string，导入任务 ID）、`tags`（array\<string>，文档标签列表）、`canDelete`（boolean，是否可删除）、`chunkSize`（integer，切片大小）、`overlapSize`（integer，切片重叠大小）、`separator`（string，切片分隔符）、`chunkMode`（string，切片模式）、`dataSync`（boolean，该文件是否参与数据同步）、`meta_extract_info`（array，元数据抽取结果）） |
| `success`     | boolean | 请求是否成功                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| `message`     | string  | 提示信息，成功时为 `success`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| `status`      | string  | 请求状态：`SUCCESS` 或 `FAILED`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |

## 错误码 <span id="h-ragapi-list-file-details-errors" />

| HTTP 状态码 | 错误码                      | 错误信息                                     | 说明                 |
| -------- | ------------------------ | ---------------------------------------- | ------------------ |
| 400      | `Index.InvalidParameter` | `Required parameter missing or invalid.` | 请求参数无效             |
| 401      | `InvalidApiKey`          | `Invalid API-key provided.`              | 鉴权失败，API Key 无效或缺失 |
