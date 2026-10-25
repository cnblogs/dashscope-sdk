> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询文件详情

> 查询指定文件的详细信息，包括文件大小、MD5、标签、创建和更新时间等。

<meta property="og:title" content="查询文件详情" />

## 前提 <span id="h-ragapi-describe-file-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-describe-file-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/describeFile`

查询指定文件的详细信息，包括文件大小、MD5、标签、创建和更新时间等。

## 请求体 <span id="h-ragapi-describe-file-req" />

| 字段       | 必填 | 类型     | 说明                      |
| -------- | -- | ------ | ----------------------- |
| `fileId` | 是  | string | 文件 ID，通过 `listFile` 获取。 |

## 请求示例 <span id="h-ragapi-describe-file-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/describeFile" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "fileId": "file_abc123"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-describe-file-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "message": "",
  "messageUnmodified": false,
  "requestId": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
  "data": {
    "fileId": "file_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
    "status": "PARSE_SUCCESS",
    "fileName": "a.md",
    "fileType": "md",
    "parser": "DOCMIND_DIGITAL",
    "sizeBytes": 21,
    "uploadTime": "2026-04-01 16:03:45",
    "category": "cate_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx"
  },
  "status": 200
}
```

### 响应字段 <span id="h-ragapi-describe-file-dto" />

| 字段          | 类型      | 说明                                                                                                                                                                                                                                                                                                 |
| ----------- | ------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `code`      | string  | 响应码，成功时为 `Success`。                                                                                                                                                                                                                                                                                |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                                                                                                                                                                                                                                                  |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                                                                                                                                                                                                                                               |
| `data`      | object  | 文件详情。子字段 `fileId`（string，文件 ID）、`fileName`（string，文件名）、`fileType`（string，文件类型，如 pdf、md、docx）、`category`（string，所属类目 ID）、`status`（string，文件状态。枚举：`INIT`、`PARSING`、`PARSE_SUCCESS`、`FAIL`）、`sizeBytes`（integer，文件大小（字节），最小为 1）、`parser`（string，解析方式，如 DASHSCOPE\_DOCMIND）、`uploadTime`（string，上传时间）。 |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。                                                                                                                                                                                                                         |

## 错误码 <span id="h-ragapi-describe-file-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
