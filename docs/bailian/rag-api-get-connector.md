> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询连接器

> 查询指定连接器的详细信息。可通过连接器 ID 或名称查询，至少传入其中一个参数。

<meta property="og:title" content="查询连接器" />

## 前提 <span id="h-ragapi-get-connector-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-get-connector-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/getConnector`

查询指定连接器的详细信息。可通过连接器 ID 或名称查询，至少传入其中一个参数。

<Note>
  `connectorId` 和 `connectorName` 至少需要传入一个。两者都传入时，以 `connectorId` 为准。
</Note>

## 请求体 <span id="h-ragapi-get-connector-req" />

| 字段              | 必填 | 类型     | 说明                                    |
| --------------- | -- | ------ | ------------------------------------- |
| `connectorId`   | 否  | string | 连接器 ID，与 connectorName 至少传入一个。        |
| `connectorName` | 否  | string | 连接器名称，与 connectorId 至少传入一个，最长 20 个字符。 |

## 请求示例 <span id="h-ragapi-get-connector-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/getConnector" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "connectorId": "conn_abc123"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-get-connector-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "message": "",
  "messageUnmodified": false,
  "requestId": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
  "data": {
    "connectorId": "conn_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
    "connectorName": "默认文件连接器",
    "connectorType": "FILE",
    "connectorSubType": "UNSTRUCTURED",
    "description": "默认文件类型连接器",
    "gmtCreate": "2026-03-16 16:13:26",
    "gmtModified": "2026-03-16 16:13:26"
  },
  "status": 200
}
```

### 响应字段 <span id="h-ragapi-get-connector-dto" />

| 字段          | 类型      | 说明                                                                                                                                                                                                                                                                                                    |
| ----------- | ------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `code`      | string  | 响应码，成功时为 `Success`。                                                                                                                                                                                                                                                                                   |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                                                                                                                                                                                                                                                     |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                                                                                                                                                                                                                                                  |
| `data`      | object  | 连接器详情。子字段 `connectorId`（string，连接器 ID）、`connectorType`（string，连接器类型，当前仅支持 FILE）、`connectorName`（string，连接器名称）、`description`（string，连接器描述）、`connectorSubType`（string，连接器子类型，如 `UNSTRUCTURED`）、`gmtCreate`（string，创建时间，格式 `YYYY-MM-DD HH:mm:ss`）、`gmtModified`（string，最后修改时间，格式 `YYYY-MM-DD HH:mm:ss`）。 |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。                                                                                                                                                                                                                            |

## 错误码 <span id="h-ragapi-get-connector-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
