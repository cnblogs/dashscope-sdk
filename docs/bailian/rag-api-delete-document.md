> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 删除文档

> 从知识库中删除文档及其所有关联切片。

<meta property="og:title" content="删除文档" />

## 前提 <span id="h-ragapi-delete-document-prereq" />

已完成 Endpoint 与鉴权配置，详见 [API 总览](/zh/model-studio/rag-api-overview)和[认证](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-delete-document-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/index/delete_file`

从指定知识库中删除文档及其所有关联切片。

<Warning>
  此操作不可逆。此接口使用 snake\_case 参数名 `index_id`、`doc_ids`，与其他接口的 camelCase（`indexId`）不同。传 `IndexId` 或 `indexId` 都会返回 `Index.InvalidParameter`。

  参数缺失时服务端返回的 `Required parameter(IndexId) missing or invalid`，其中 `IndexId` 是服务端内部字段名，**不是**请求参数名，请仍按 `index_id` 传参。
</Warning>

## 请求体 <span id="h-ragapi-delete-document-req" />

| 字段         | 必填 | 类型             | 说明           |
| ---------- | -- | -------------- | ------------ |
| `index_id` | 是  | string         | 知识库 ID       |
| `doc_ids`  | 是  | array\<string> | 要删除的文档 ID 列表 |

## 请求示例 <span id="h-ragapi-delete-document-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/delete_file" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "index_id": "your_index_id",
    "doc_ids": ["file_xxx"]
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-delete-document-resp" />

删除成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "deleted": []
  },
  "success": true,
  "message": "success",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-delete-document-dto" />

| 字段            | 类型      | 说明                                              |
| ------------- | ------- | ----------------------------------------------- |
| `code`        | string  | 响应码，成功时为 `Success`                              |
| `status_code` | integer | HTTP 状态码                                        |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID                             |
| `data`        | object  | 业务数据。子字段 `deleted`（array\<string>，已删除的文档 ID 列表） |
| `success`     | boolean | 操作是否成功                                          |
| `message`     | string  | 响应消息，成功时为 `success`                             |
| `status`      | string  | 请求状态：`SUCCESS` 或 `FAILED`                       |

## 错误码 <span id="h-ragapi-delete-document-errors" />

| HTTP 状态码 | 错误码                      | 错误信息                                     | 说明                 |
| -------- | ------------------------ | ---------------------------------------- | ------------------ |
| 400      | `Index.InvalidParameter` | `Required parameter missing or invalid.` | 请求参数无效             |
| 401      | `InvalidApiKey`          | `Invalid API-key provided.`              | 鉴权失败，API Key 无效或缺失 |
