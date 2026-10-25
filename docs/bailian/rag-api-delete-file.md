> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 删除文件

> 永久删除指定的文件。删除后文件数据将无法恢复。

<meta property="og:title" content="删除文件" />

## 前提 <span id="h-ragapi-delete-file-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-delete-file-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/deleteFile`

永久删除指定的文件。删除后文件数据将无法恢复。

<Warning>
  此操作不可逆。删除后文件数据将被永久移除，无法恢复。如果该文件已被知识库引用，删除后相关的文档索引也将失效。
</Warning>

## 请求体 <span id="h-ragapi-delete-file-req" />

| 字段       | 必填 | 类型     | 说明                      |
| -------- | -- | ------ | ----------------------- |
| `fileId` | 是  | string | 文件 ID，通过 `listFile` 获取。 |

## 请求示例 <span id="h-ragapi-delete-file-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/deleteFile" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "fileId": "file_abc123"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-delete-file-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {},
  "requestId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

### 响应字段 <span id="h-ragapi-delete-file-dto" />

| 字段          | 类型      | 说明                                                                         |
| ----------- | ------- | -------------------------------------------------------------------------- |
| `code`      | string  | 响应码，成功时为 `Success`。                                                        |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                          |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                       |
| `data`      | object  | 删除结果。子字段 `fileId`（string，已删除的文件 ID）、`status`（string，删除后状态，为 `DELETED`）。    |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。 |

## 错误码 <span id="h-ragapi-delete-file-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
