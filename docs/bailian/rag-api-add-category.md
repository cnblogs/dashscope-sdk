> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 新增类目

> 在当前业务空间下创建新的类目，用于组织和管理数据文件。

<meta property="og:title" content="新增类目" />

## 前提 <span id="h-ragapi-add-category-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-add-category-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/addCategory`

在当前业务空间下创建新的类目，用于组织和管理数据文件。

## 请求体 <span id="h-ragapi-add-category-req" />

| 字段                 | 必填 | 类型     | 说明                       |
| ------------------ | -- | ------ | ------------------------ |
| `categoryName`     | 是  | string | 类目名称，长度 1\~20 个字符。       |
| `categoryType`     | 是  | string | 类目类型，仅支持 `UNSTRUCTURED`。 |
| `parentCategoryId` | 否  | string | 父类目 ID，创建子类目时传入。         |
| `connectorId`      | 否  | string | 连接器 ID，将类目关联到指定连接器。      |

## 请求示例 <span id="h-ragapi-add-category-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/addCategory" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "categoryName": "产品文档",
    "categoryType": "UNSTRUCTURED"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-add-category-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "message": "",
  "messageUnmodified": false,
  "requestId": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
  "data": {
    "categoryId": "cate_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
    "categoryName": "_tmp_example"
  },
  "status": 200
}
```

### 响应字段 <span id="h-ragapi-add-category-dto" />

| 字段          | 类型      | 说明                                                                         |
| ----------- | ------- | -------------------------------------------------------------------------- |
| `code`      | string  | 响应码，成功时为 `Success`。                                                        |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                          |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                       |
| `data`      | object  | 创建结果。子字段 `categoryId`（string，新创建的类目 ID）、`categoryName`（string，类目名称）。       |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。 |

## 错误码 <span id="h-ragapi-add-category-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
