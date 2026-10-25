> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询文件列表

> 查询当前业务空间下的文件列表，支持按类目、文件名和文件 ID 过滤。

<meta property="og:title" content="查询文件列表" />

## 前提 <span id="h-ragapi-list-file-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-list-file-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/listFile`

查询当前业务空间下的文件列表，支持按类目、文件名和文件 ID 过滤。

<Note>
  通过 `fileIds` 参数批量查询时，单次最多传入 20 个文件 ID。
</Note>

## 请求体 <span id="h-ragapi-list-file-req" />

| 字段           | 必填 | 类型             | 说明                                                            |
| ------------ | -- | -------------- | ------------------------------------------------------------- |
| `categoryId` | 是  | string         | 类目 ID，通过 `listCategory` 获取。                                   |
| `fileName`   | 否  | string         | 文件名（不含后缀），按精确匹配过滤。传入时需去掉扩展名，如文件 `a.md` 应传 `a`。                |
| `fileIds`    | 否  | array\<string> | 文件 ID 列表，最多 20 个，用于批量查询指定文件。                                  |
| `nextToken`  | 否  | string         | 分页游标，首次查询不传；翻页时传入上一页响应返回的 nextToken 值。                        |
| `maxResult`  | 否  | integer        | 每页返回的文件数量，默认 20。注意参数名为 maxResult（单数），传入 maxResults（复数）会被静默忽略。 |

## 请求示例 <span id="h-ragapi-list-file-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/listFile" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "categoryId": "cate_abc123"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-list-file-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "message": "",
  "messageUnmodified": false,
  "requestId": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
  "data": {
    "hasNext": false,
    "maxResult": 20,
    "totalCount": 1,
    "maxId": 338031,
    "fileList": [
      {
        "fileId": "file_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "status": "PARSE_SUCCESS",
        "fileName": "a.md",
        "fileType": "md",
        "parser": "DASHSCOPE_DOCMIND",
        "sizeBytes": 21,
        "uploadTime": "2026-04-01 16:03:45",
        "category": "cate_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "autoId": 338031,
        "parseErrorMessage": ""
      }
    ]
  },
  "status": 200
}
```

### 响应字段 <span id="h-ragapi-list-file-dto" />

| 字段          | 类型      | 说明                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| ----------- | ------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `code`      | string  | 响应码，成功时为 `Success`。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| `data`      | object  | 查询结果。子字段 `fileList`（array\<object>，文件列表。元素为对象，字段：`fileId`（string，文件 ID）、`fileName`（string，文件名）、`fileType`（string，文件类型，如 pdf、md、docx）、`category`（string，所属类目 ID）、`status`（string，文件状态，如 PARSE\_SUCCESS、PARSING、FAIL）、`sizeBytes`（integer，文件大小（字节））、`parser`（string，解析方式，如 DASHSCOPE\_DOCMIND、DOCUMENT\_PARSE\_LLM）、`uploadTime`（string，上传时间）、`parseErrorMessage`（string，解析失败时的错误信息，成功时为空）、`autoId`（integer，自增 ID））、`hasNext`（boolean，是否还有下一页）、`maxResult`（integer，本次请求的每页条数）、`totalCount`（integer，符合条件的总条数）、`nextToken`（string，下一页游标。将该值作为下次请求的 `nextToken` 即可翻页）。 |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |

## 错误码 <span id="h-ragapi-list-file-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
