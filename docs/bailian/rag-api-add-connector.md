> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 新增连接器

> 创建新的数据连接器，用于管理数据导入来源。

<meta property="og:title" content="新增连接器" />

## 前提 <span id="h-ragapi-add-connector-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-add-connector-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/addConnector`

创建新的数据连接器，用于管理数据导入来源。

<Note>
  当前 `connectorType` 仅支持 `FILE` 类型。`storeType` 为 `CUSTOM` 时需要额外传入 `regionId` 和 `bucketName`。
</Note>

## 请求体 <span id="h-ragapi-add-connector-req" />

| 字段                    | 必填 | 类型     | 说明                                                                                                                                                                                                 |
| --------------------- | -- | ------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `connectorType`       | 是  | string | 连接器类型，当前仅支持 `FILE`。                                                                                                                                                                                |
| `connectorName`       | 是  | string | 连接器名称，长度 1\~20 个字符。                                                                                                                                                                                |
| `description`         | 是  | string | 连接器描述信息，长度 1\~200 个字符。                                                                                                                                                                             |
| `fileConnectorConfig` | 是  | object | 文件连接器配置。子字段 `storeType`（string，必填，存储类型：`PLATFORM`（平台托管）或 `CUSTOM`（自定义 OSS））、`regionId`（string，OSS 地域 ID，当 storeType 为 `CUSTOM` 时需要）、`bucketName`（string，OSS Bucket 名称，当 storeType 为 `CUSTOM` 时需要）。 |

## 请求示例 <span id="h-ragapi-add-connector-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/addConnector" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "connectorType": "FILE",
    "connectorName": "产品文档连接器",
    "description": "用于导入产品文档的文件连接器",
    "fileConnectorConfig": {
      "storeType": "PLATFORM"
    }
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-add-connector-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "connectorId": "conn_abc123"
  },
  "requestId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

### 响应字段 <span id="h-ragapi-add-connector-dto" />

| 字段          | 类型      | 说明                                                                         |
| ----------- | ------- | -------------------------------------------------------------------------- |
| `code`      | string  | 响应码，成功时为 `Success`。                                                        |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                          |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                       |
| `data`      | object  | 创建结果。子字段 `connectorId`（string，新创建的连接器 ID）。                                 |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。 |

## 错误码 <span id="h-ragapi-add-connector-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
