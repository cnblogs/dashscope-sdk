> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 从 OSS 批量导入

> 从已授权的阿里云 OSS Bucket 批量导入文件。使用前需通过服务关联角色（SLR）授权百炼平台访问 OSS。

<meta property="og:title" content="从 OSS 批量导入" />

## 前提 <span id="h-ragapi-oss-import-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

<Note>
  使用前需要通过\*\*服务关联角色（SLR）\*\*授权百炼平台访问您的 OSS Bucket。请在阿里云 RAM 控制台确认已创建 `AliyunServiceRoleForBailian` 服务关联角色，否则调用将返回权限错误。
</Note>

## 接口 <span id="h-ragapi-oss-import-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/addFilesFromAuthorizedOss`

从已授权的阿里云 OSS Bucket 批量导入文件。使用前需通过服务关联角色（SLR）授权百炼平台访问 OSS。

## 请求体 <span id="h-ragapi-oss-import-req" />

| 字段                      | 必填 | 类型             | 说明                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| ----------------------- | -- | -------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `categoryId`            | 是  | string         | 类目 ID，通过 `listCategory` 获取。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| `categoryType`          | 是  | string         | 类目类型，默认 `UNSTRUCTURED`。枚举：`UNSTRUCTURED`、`SESSION_FILE`。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| `ossBucket`             | 是  | string         | OSS Bucket 名称。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
| `ossRegionId`           | 是  | string         | OSS 所在地域，如 cn-beijing。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |
| `fileDetails`           | 是  | array\<object> | 要导入的文件详情列表，1\~10 个元素。元素为对象，子字段：`fileName`（string，必填，导入后的文件名称，长度 1\~500 个字符）、`ossKey`（string，必填，OSS 对象 Key（文件在 Bucket 中的路径），长度 1\~256 个字符）、`parser`（string，解析器类型，默认 `AUTO_SELECT`。`AUTO_SELECT` 自动选择解析器；`DOCMIND` 智能文档解析；`DOCMIND_DIGITAL` 电子文档解析；`DOCMIND_LLM_VERSION` 大模型文档解析；`DASH_QWEN_VL_PARSER` Qwen VL 解析（需同时传 parserConfig）；`DOCMIND_LLM_VERSION_MEDIA` 音视频解析。categoryType 为 `UNSTRUCTURED` 时按类目数据解析设置解析文件；为 `SESSION_FILE` 时系统使用默认方式解析（不支持更改））、`parserConfig`（object，解析配置，仅 parser 为 `DASH_QWEN_VL_PARSER` 时必填，其他解析方式无需传入。子字段 `modelName`（string，视觉解析模型，枚举 `qwen3-vl-plus`）、`modelPrompt`（string，解析提示词，长度 1\~1500 字符））。 |
| `tags`                  | 否  | array\<string> | 文件关联的标签列表，最多 10 个。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| `overWriteFileByOssKey` | 否  | boolean        | 当 ossKey 对应的文件已存在时是否覆盖，默认 false。                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |

## 请求示例 <span id="h-ragapi-oss-import-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/addFilesFromAuthorizedOss" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "categoryId": "cate_abc123",
    "categoryType": "UNSTRUCTURED",
    "ossBucket": "my-docs-bucket",
    "ossRegionId": "cn-beijing",
    "fileDetails": [
      {"fileName": "product-guide.pdf", "ossKey": "docs/product-guide.pdf"},
      {"fileName": "faq.docx", "ossKey": "docs/faq.docx"}
    ]
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-oss-import-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {},
  "requestId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

### 响应字段 <span id="h-ragapi-oss-import-dto" />

| 字段          | 类型      | 说明                                                                         |
| ----------- | ------- | -------------------------------------------------------------------------- |
| `code`      | string  | 响应码，成功时为 `Success`。                                                        |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                          |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                       |
| `data`      | object  | 导入结果。子字段 `fileIds`（array\<string>，导入成功的文件 ID 列表）。                          |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。 |

## 错误码 <span id="h-ragapi-oss-import-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
