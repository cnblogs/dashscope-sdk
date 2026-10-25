> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 删除知识库

> 永久删除指定的知识库及其所有文档和切片。删除后无法恢复，请在调用前确认目标知识库已无业务依赖。

<meta property="og:title" content="删除知识库" />

## 前提 <span id="h-ragapi-delete-index-prereq" />

已获取 API Key 和业务空间 ID，并完成鉴权配置，详见[API 总览](/zh/model-studio/rag-api-overview)与[鉴权说明](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-delete-index-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/index/delete`

永久删除指定的知识库。删除后，该知识库下的所有文档、切片和索引数据将被一并移除，且无法恢复。请在调用前确认操作。

<Warning>
  此操作不可逆。删除后，该知识库下的所有文档、切片和索引数据将被一并移除，且无法恢复。请在调用前确认目标知识库已无业务依赖。
</Warning>

## 请求体 <span id="h-ragapi-delete-index-req" />

| 字段         | 必填 | 类型     | 说明                                                                                   |
| ---------- | -- | ------ | ------------------------------------------------------------------------------------ |
| `index_id` | 是  | string | 要删除的知识库 ID，即[创建知识库并导入](/zh/model-studio/rag-api-create-index)接口返回的 `data.pipelineId` |

## 请求示例 <span id="h-ragapi-delete-index-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/delete" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{"index_id": "your-kb-id"}'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-delete-index-resp" />

删除成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "success": true,
  "message": "success",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-delete-index-dto" />

| 字段            | 类型      | 说明                  |
| ------------- | ------- | ------------------- |
| `code`        | string  | 响应码，成功时为 `Success`  |
| `status_code` | integer | HTTP 状态码            |
| `success`     | boolean | 操作是否成功              |
| `message`     | string  | 响应消息，成功时为 `success` |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID |
| `status`      | string  | 请求状态，成功时为 `SUCCESS` |
