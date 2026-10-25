> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询文档列表

> 查询指定知识库中的文档列表，支持分页。

<meta property="og:title" content="查询文档列表" />

## 前提 <span id="h-ragapi-list-documents-prereq" />

已完成 Endpoint 与鉴权配置，详见 [API 总览](/zh/model-studio/rag-api-overview)和[认证](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-list-documents-endpoint" />

<strong>GET</strong> `/api/v1/indices/rag/index/files`

查询指定知识库中的文档列表，支持分页。

## 查询参数 <span id="h-ragapi-list-documents-params" />

| 字段            | 必填 | 类型      | 说明                                                                       |
| ------------- | -- | ------- | ------------------------------------------------------------------------ |
| `index_id`    | 是  | string  | 知识库 ID，对应[创建知识库](/zh/model-studio/rag-api-create-index)时返回的 `pipelineId` |
| `page_number` | 否  | integer | 页码，从 1 开始，默认 1                                                           |
| `page_size`   | 否  | integer | 每页返回的文档数量，默认 10，最大 100                                                   |

## 请求示例 <span id="h-ragapi-list-documents-eg" />

```bash
curl -G "$BASE_URL/api/v1/indices/rag/index/files" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -d "index_id=your_index_id" \
  -d "page_number=1" \
  -d "page_size=20"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-list-documents-resp" />

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
        "code": "FINISH",
        "connectorId": "conn_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "dataSync": false,
        "doc_type": "md",
        "gmt_modified": 1782184640000,
        "message": "索引构建成功",
        "doc_id": "file_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "categoryName": "默认类目",
        "tags": [],
        "ingestion_id": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
        "size": 21,
        "doc_name": "a",
        "canDelete": true,
        "source_id": "cate_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "meta_extract_info": [],
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

### 响应字段 <span id="h-ragapi-list-documents-dto" />

| 字段            | 类型      | 说明                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| ------------- | ------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `code`        | string  | 响应码，成功时为 `Success`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| `status_code` | integer | HTTP 状态码                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| `data`        | object  | 业务数据。子字段 `page_number`（integer，当前页码）、`total_count`（integer，文档总数）、`rows`（array\<object>，文档列表。元素字段：`doc_id`（string，文档唯一标识）、`doc_name`（string，文档名称）、`doc_type`（string，文档类型，如 `pdf`、`docx`、`txt`）、`code`（string，细粒度处理状态码，如 `FINISH`、`PARSE_FAILED`、`PIPELINE_JOB_RETRY_IMPORT_ERROR` 等）、`status`（string，粗粒度处理状态，如 `FINISH`、`PARSE_FAILED`、`INSERT_ERROR` 等，与 `code` 多数情况下一致，`code` 提供更细的错误归类）、`message`（string，处理状态描述，失败时为错误原因）、`size`（integer，文件大小，单位字节）、`gmt_modified`（integer，最后修改时间，毫秒级 Unix 时间戳）、`dataSync`（boolean，是否为增量同步数据）、`categoryId`（string，所属类目 ID）、`categoryName`（string，所属类目名称）、`connectorId`（string，所属连接器 ID）、`source_id`（string，数据源 ID）、`ingestion_id`（string，导入任务 ID）、`tags`（array\<string>，文档标签列表）、`meta_extract_info`（array，元数据抽取信息）、`canDelete`（boolean，是否可删除）） |
| `success`     | boolean | 请求是否成功                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
| `message`     | string  | 提示信息，成功时为 `success`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| `status`      | string  | 请求状态：`SUCCESS` 或 `FAILED`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |

## 错误码 <span id="h-ragapi-list-documents-errors" />

| HTTP 状态码 | 错误码                      | 错误信息                                     | 说明                 |
| -------- | ------------------------ | ---------------------------------------- | ------------------ |
| 400      | `Index.InvalidParameter` | `Required parameter missing or invalid.` | 请求参数无效             |
| 401      | `InvalidApiKey`          | `Invalid API-key provided.`              | 鉴权失败，API Key 无效或缺失 |
