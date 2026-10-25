> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 注册文件

> 上传文件的最后一步。在通过 OSS 上传文件内容后，调用此接口将文件注册到数据中心。

<meta property="og:title" content="注册文件" />

## 前提 <span id="h-ragapi-add-file-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-add-file-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/addFile`

上传文件的最后一步。在通过 OSS 上传文件内容后，调用此接口将文件注册到数据中心。完整上传流程为：1) 调用 applyFileUploadLease 获取租约 → 2) 使用返回的 URL 通过 PUT 上传文件到 OSS → 3) 调用本接口注册文件。

<Warning>
  类目 ID 的参数名为 `category`，**不是** `categoryId`。这与 [`listFile`](/zh/model-studio/rag-api-list-file)、[`addFilesFromAuthorizedOss`](/zh/model-studio/rag-api-oss-import) 等接口使用的 `categoryId` 参数名不同，请注意区分。
</Warning>

## 请求体 <span id="h-ragapi-add-file-req" />

| 字段                | 必填 | 类型             | 说明                                                                                                                                                                                                                                                                          |
| ----------------- | -- | -------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `leaseId`         | 是  | string         | 上传租约 ID，即 `applyFileUploadLease` 接口返回的 `fileUploadLeaseId`。                                                                                                                                                                                                                 |
| `category`        | 是  | string         | 类目 ID（注意：此字段名为 category，不是 categoryId），通过 `listCategory` 获取。可传入 `default` 使用系统默认类目；categoryType 为 `SESSION_FILE` 时传入 `default` 即可。                                                                                                                                          |
| `categoryType`    | 否  | string         | 类目类型，默认 `UNSTRUCTURED`。`UNSTRUCTURED`：类目，用于构建知识库；`SESSION_FILE`：用于智能体应用会话交互的文件。使用 `SESSION_FILE` 时，applyFileUploadLease 的 categoryType 也须传 `SESSION_FILE`；此类文件仅当前会话有效，关闭会话后过期（最长 7 天），不支持长期保存。                                                                              |
| `parser`          | 是  | string         | 解析器类型。`AUTO_SELECT` 自动选择解析器；`DOCMIND` 智能文档解析；`DOCMIND_DIGITAL` 电子文档解析；`DOCMIND_LLM_VERSION` 大模型文档解析；`DASH_QWEN_VL_PARSER` Qwen VL 解析（需同时传 parserConfig）；`DOCMIND_LLM_VERSION_MEDIA` 音视频解析。categoryType 为 `UNSTRUCTURED` 时按类目数据解析设置解析文件；为 `SESSION_FILE` 时系统使用默认方式解析（不支持更改）。 |
| `tags`            | 否  | array\<string> | 文件关联的标签列表，默认为空（不设置标签）。最多传入 100 个标签，所有标签字符长度总和不超过 700，单个标签最多 32 个字符。                                                                                                                                                                                                         |
| `originalFileUrl` | 否  | string         | 为文件关联一个 URL。构建文档搜索类知识库时系统记录该链接，与智能体应用对话时随该文件召回结果返回（通过 `docUrl` 字段）。智能体应用须开启知识库并启用“展示回答来源”，否则此参数不生效。                                                                                                                                                                         |
| `parserConfig`    | 否  | object         | 解析配置，仅 parser 为 `DASH_QWEN_VL_PARSER` 时必填，其他解析方式无需传入。子字段 `modelName`（string，视觉解析模型，枚举 `qwen3-vl-plus`）、`modelPrompt`（string，解析提示词，长度 1\~1500 字符）。                                                                                                                           |

## 请求示例 <span id="h-ragapi-add-file-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/addFile" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "leaseId": "lease_abc123",
    "category": "cate_abc123",
    "categoryType": "UNSTRUCTURED",
    "parser": "AUTO_SELECT",
    "tags": ["产品", "指南"]
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-add-file-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "fileId": "file_abc123"
  },
  "requestId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

### 响应字段 <span id="h-ragapi-add-file-dto" />

| 字段          | 类型      | 说明                                                                                                     |
| ----------- | ------- | ------------------------------------------------------------------------------------------------------ |
| `code`      | string  | 响应码，成功时为 `Success`。                                                                                    |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                                                      |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                                                   |
| `data`      | object  | 注册结果。子字段 `fileId`（string，注册成功后的文件 ID）、`parser`（string，实际使用的解析器）、`status`（string，文件注册状态，成功为 `SUCCESS`）。 |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。                             |

## 错误码 <span id="h-ragapi-add-file-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
