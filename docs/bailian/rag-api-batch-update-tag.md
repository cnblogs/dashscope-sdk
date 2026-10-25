> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 批量更新文件标签

> 批量更新多个文件的标签，支持覆盖和追加两种模式。

<meta property="og:title" content="批量更新文件标签" />

## 前提 <span id="h-ragapi-batch-update-tag-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-batch-update-tag-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/batchUpdateFileTag`

批量更新多个文件的标签，支持覆盖和追加两种模式。

<Note>
  `updateMode` 的两种模式：`OVERWRITE` 会替换文件的全部现有标签；`APPEND` 在现有标签基础上追加。
</Note>

## 请求体 <span id="h-ragapi-batch-update-tag-req" />

| 字段           | 必填 | 类型             | 说明                                                                                                                                                    |
| ------------ | -- | -------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------- |
| `fileInfos`  | 是  | array\<object> | 文件标签信息列表，1\~20 个元素。元素为对象，必填子字段：`fileId`（string，文件 ID，通过 `listFile` 获取）、`tags`（array\<string>，文件关联的标签列表。最多传入 100 个标签，所有标签字符长度总和不超过 700，单个标签最多 32 个字符）。 |
| `updateMode` | 否  | string         | 更新模式：`OVERWRITE`（覆盖现有标签）、`APPEND`（追加标签）。                                                                                                              |

## 请求示例 <span id="h-ragapi-batch-update-tag-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/batchUpdateFileTag" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "fileInfos": [
      {"fileId": "file_abc123", "tags": ["产品", "V2"]},
      {"fileId": "file_def456", "tags": ["FAQ"]}
    ],
    "updateMode": "OVERWRITE"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-batch-update-tag-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {},
  "requestId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

### 响应字段 <span id="h-ragapi-batch-update-tag-dto" />

| 字段          | 类型      | 说明                                                                                                               |
| ----------- | ------- | ---------------------------------------------------------------------------------------------------------------- |
| `code`      | string  | 响应码，成功时为 `Success`。                                                                                              |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                                                                |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                                                             |
| `data`      | object  | 逐个文件的更新结果。子字段 `results`（array\<object>，文件更新结果列表。元素为对象，字段：`fileId`（string，文件 ID）、`success`（boolean，该文件标签是否更新成功））。 |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。                                       |

## 错误码 <span id="h-ragapi-batch-update-tag-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
