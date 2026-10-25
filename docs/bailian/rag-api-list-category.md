> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询类目列表

> 查询当前业务空间下的类目列表，支持按类型、名称、父类目和连接器 ID 过滤。

<meta property="og:title" content="查询类目列表" />

## 前提 <span id="h-ragapi-list-category-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-list-category-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/listCategory`

查询当前业务空间下的类目列表，支持按类型、名称、父类目和连接器 ID 过滤。

## 请求体 <span id="h-ragapi-list-category-req" />

| 字段             | 必填 | 类型      | 说明                                                            |
| -------------- | -- | ------- | ------------------------------------------------------------- |
| `type`         | 是  | string  | 类目类型。当前仅支持 `UNSTRUCTURED`（非结构化数据类目）。                          |
| `parentId`     | 否  | string  | 父类目 ID，用于查询子类目。                                               |
| `categoryName` | 否  | string  | 类目名称，精确匹配。                                                    |
| `connectorId`  | 否  | string  | 连接器 ID，按连接器过滤类目。                                              |
| `nextToken`    | 否  | string  | 分页游标，首次查询不传；翻页时传入上一页响应返回的 nextToken 值。                        |
| `maxResult`    | 否  | integer | 每页返回的类目数量，默认 20。注意参数名为 maxResult（单数），传入 maxResults（复数）会被静默忽略。 |

## 请求示例 <span id="h-ragapi-list-category-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/listCategory" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "type": "UNSTRUCTURED"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-list-category-resp" />

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
    "totalCount": 2,
    "maxId": 100602,
    "categoryList": [
      {
        "categoryId": "cate_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
        "categoryName": "cscd",
        "type": "UNSTRUCTURED",
        "isDefault": false
      }
    ]
  },
  "status": 200
}
```

### 响应字段 <span id="h-ragapi-list-category-dto" />

| 字段          | 类型      | 说明                                                                                                                                                                                                                                                                       |
| ----------- | ------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `code`      | string  | 响应码，成功时为 `Success`。                                                                                                                                                                                                                                                      |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                                                                                                                                                                                                                        |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                                                                                                                                                                                                                     |
| `data`      | object  | 查询结果。子字段 `categoryList`（array\<object>，类目列表。元素为对象，字段：`categoryId`（string，类目 ID）、`categoryName`（string，类目名称）、`type`（string，类目类型，仅 `UNSTRUCTURED`）、`isDefault`（boolean，是否为默认类目））、`hasNext`（boolean，是否还有下一页）、`maxResult`（integer，本次请求的每页条数）、`totalCount`（integer，符合条件的总条数）。 |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。                                                                                                                                                                                               |

## 错误码 <span id="h-ragapi-list-category-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
