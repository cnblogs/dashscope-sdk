> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 删除切片

> 批量删除切片，单次最多 10 个。

<meta property="og:title" content="删除切片" />

## 前提 <span id="h-ragapi-delete-chunk-prereq" />

已完成 Endpoint 与鉴权配置，详见 [API 总览](/zh/model-studio/rag-api-overview)和[认证](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-delete-chunk-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/index/chunk/delete`

批量删除指定知识库中的切片，单次最多删除 10 个。

<Warning>
  此操作不可逆，删除后切片无法恢复。单次最多删除 10 个切片。
</Warning>

## 请求体 <span id="h-ragapi-delete-chunk-req" />

| 字段           | 必填 | 类型             | 说明                     |
| ------------ | -- | -------------- | ---------------------- |
| `pipelineId` | 是  | string         | 知识库 ID                 |
| `chunkIds`   | 是  | array\<string> | 要删除的切片 ID 列表，单次最多 10 个 |

## 请求示例 <span id="h-ragapi-delete-chunk-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/chunk/delete" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "pipelineId": "your_index_id",
    "chunkIds": ["chunk_abc123", "chunk_def456"]
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-delete-chunk-resp" />

删除成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

### 响应字段 <span id="h-ragapi-delete-chunk-dto" />

| 字段            | 类型      | 说明                        |
| ------------- | ------- | ------------------------- |
| `code`        | string  | 响应码，成功时为 `Success`        |
| `status_code` | integer | HTTP 状态码                  |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID       |
| `data`        | object  | 操作成功时返回空对象                |
| `success`     | boolean | 操作是否成功                    |
| `message`     | string  | 响应消息，成功时为 `success`       |
| `status`      | string  | 请求状态：`SUCCESS` 或 `FAILED` |

## 错误码 <span id="h-ragapi-delete-chunk-errors" />

| HTTP 状态码 | 错误码                      | 错误信息                                     | 说明                 |
| -------- | ------------------------ | ---------------------------------------- | ------------------ |
| 400      | `Index.InvalidParameter` | `Required parameter missing or invalid.` | 请求参数无效             |
| 401      | `InvalidApiKey`          | `Invalid API-key provided.`              | 鉴权失败，API Key 无效或缺失 |
