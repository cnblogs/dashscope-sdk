> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 创建知识库并导入

> 一步完成创建知识库和导入文件（合并了 CreateIndex 和 SubmitIndexJob）。创建成功后返回知识库 ID（pipelineId）与导入任务 ID（ingestionId）。

<meta property="og:title" content="创建知识库并导入" />

## 前提 <span id="h-ragapi-create-index-prereq" />

已获取 API Key 和业务空间 ID，并完成鉴权配置，详见[API 总览](/zh/model-studio/rag-api-overview)与[鉴权说明](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-create-index-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/index/create_v2`

一步完成创建知识库和导入文件（合并了 CreateIndex 和 SubmitIndexJob）。

## 请求体 <span id="h-ragapi-create-index-req" />

<Warning>
  文件 ID 参数名为 `docIds`，不是 `file_ids` 或 `fileIds`。但校验失败时错误信息中的参数名为 `file_ids`。
</Warning>

| 字段                             | 必填 | 类型              | 说明                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| ------------------------------ | -- | --------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `name`                         | 是  | string          | 知识库名称，1-20 字符                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| `description`                  | 是  | string          | 知识库描述，1-200 字符                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| `structureType`                | 是  | string          | 结构类型：`unstructured`（非结构化）或 `structured`（结构化）                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
| `knowledgeType`                | 否  | string          | 知识库类型，对应控制台的知识库类型：`document`（文档搜索）、`table`（数据查询）、`image`（图片问答）、`multimedia`（音视频搜索）。取值需与 `structureType` 匹配：`document`、`image` 和 `multimedia` 搭配 `unstructured`，`table` 搭配 `structured`。必须与 `knowledgeScene` 同时提供或同时省略；同时省略时，系统按 `structureType` 采用默认配置                                                                                                                                                                                                                                                               |
| `knowledgeScene`               | 否  | string          | 使用场景，对应控制台的使用场景，取值取决于 `knowledgeType`：`document` 支持 `basic_document_qa`（基础文档问答）、`visual_perception_qa`（视觉理解，富文本文档）、`lite_document_qa`（极速问答）；`table` 固定为 `basic_table_qa`；`image` 固定为 `image_qa`；`multimedia` 固定为 `basic_multimedia_qa`。控制台仅在文档搜索类型下提供场景选项，其余类型自动填入固定值，但通过 API 创建时仍需显式传入。必须与 `knowledgeType` 同时提供或同时省略。注意：`lite_document_qa` 要求 `sinkType` 为 `BUILT_IN`；`visual_perception_qa` 和 `image_qa` 要求通过 `multimodalEmbeddingModelName` 指定多模态向量模型。此外 `document` 还接受 `visual_document_qa`（图文并茂回复），但控制台已不再提供该入口 |
| `sinkType`                     | 是  | string          | 存储类型，默认 `DEFAULT`。`BUILT_IN` 表示使用平台内置向量存储，`knowledgeScene` 为 `lite_document_qa`（极速问答）时必须为 `BUILT_IN`                                                                                                                                                                                                                                                                                                                                                                                                                 |
| `sourceType`                   | 是  | string          | 数据源类型，如 `DATA_CENTER_FILE`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
| `embeddingModelName`           | 否  | string          | 向量嵌入模型名称，如 `text-embedding-v4`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| `multimodalEmbeddingModelName` | 否  | string          | 多模态向量模型名称，如 `qwen3-vl-embedding`。`knowledgeScene` 为 `image_qa`（图片问答）或 `visual_perception_qa`（视觉理解）时必填，缺失或取值非法会返回 `invalid multi embedding model`                                                                                                                                                                                                                                                                                                                                                                     |
| `chunkSize`                    | 否  | integer         | 文档切片大小（字符数）。建议值 300-800                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| `docIds`                       | 是  | `array<string>` | 创建知识库时同步导入的文件 ID 列表，值来自 [addFile](/zh/model-studio/rag-api-add-file) 注册文件后返回的 `fileId`，或通过 [listFile](/zh/model-studio/rag-api-list-file) 查询已有文件获取。建议导入不超过 10000 个。注意参数名为 `docIds`，不是 `file_ids` 或 `fileIds`；但校验失败时错误信息中的参数名为 `file_ids`                                                                                                                                                                                                                                                                             |
| `categoryIds`                  | 否  | `array<string>` | 创建知识库时可同步导入文件。通过指定类目 ID，可导入对应类目下的所有文件，建议导入不超过 10000 个                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| `dataSources`                  | 是  | `array<object>` | 数据源配置列表。子字段 `sourceType`（string，数据源类型，如 `DATA_CENTER_FILE`）                                                                                                                                                                                                                                                                                                                                                                                                                                                          |

## 知识库类型与使用场景 <span id="h-ragapi-create-index-scene" />

`knowledgeType` 和 `knowledgeScene` 对应控制台创建知识库第 1 步的知识库类型和使用场景。两者必须**同时提供或同时省略**，只传一个会报 `knowledgeType and knowledgeScene cannot be empty`；同时省略时，系统按 `structureType` 采用默认配置。

控制台仅在文档搜索类型下提供使用场景选项，其余三种类型的场景是固定的（控制台自动填入），但通过 API 创建时仍需显式成对传入。

| knowledgeType | 控制台名称 | structureType  | 可用的 knowledgeScene                                                                                |
| ------------- | ----- | -------------- | ------------------------------------------------------------------------------------------------- |
| `document`    | 文档搜索  | `unstructured` | `basic_document_qa`（基础文档问答）<br />`visual_perception_qa`（视觉理解，富文本文档）<br />`lite_document_qa`（极速问答） |
| `table`       | 数据查询  | `structured`   | `basic_table_qa`（固定）                                                                              |
| `image`       | 图片问答  | `unstructured` | `image_qa`（固定）                                                                                    |
| `multimedia`  | 音视频搜索 | `unstructured` | `basic_multimedia_qa`（固定）                                                                         |

<Note>
  - `knowledgeType` 与 `structureType` 不匹配时会报 `knowledgeType and structureType do not match`；`knowledgeScene` 不属于该类型时会报 `knowledgeType and knowledgeScene do not match`。
  - `lite_document_qa`（极速问答）要求 `sinkType` 为 `BUILT_IN`，使用默认的 `DEFAULT` 会报 `Lite Rag only supports BUILT_IN sink type`。
  - `visual_perception_qa` 和 `image_qa` 必须通过 `multimodalEmbeddingModelName` 指定多模态向量模型（如 `qwen3-vl-embedding`），否则报 `invalid multi embedding model`。
  - `document` 类型下还有一个 `visual_document_qa`（图文并茂回复）场景，接口仍接受该取值，但控制台已不再提供该入口，不建议在新建知识库时使用。
</Note>

图片问答的请求体示例：

```json
{
  "name": "image-kb",
  "description": "图片问答知识库",
  "structureType": "unstructured",
  "knowledgeType": "image",
  "knowledgeScene": "image_qa",
  "sinkType": "BUILT_IN",
  "sourceType": "DATA_CENTER_FILE",
  "multimodalEmbeddingModelName": "qwen3-vl-embedding",
  "docIds": ["your-file-id"],
  "dataSources": [{ "sourceType": "DATA_CENTER_FILE" }]
}
```

## 请求示例 <span id="h-ragapi-create-index-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/create_v2" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "your-kb-name",
    "description": "your-kb-description",
    "structureType": "unstructured",
    "sinkType": "DEFAULT",
    "sourceType": "DATA_CENTER_FILE",
    "embeddingModelName": "text-embedding-v4",
    "chunkSize": 600,
    "docIds": ["your-file-id-1", "your-file-id-2"],
    "dataSources": [{"sourceType": "DATA_CENTER_FILE"}]
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-create-index-resp" />

创建成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "success": true,
  "message": "success",
  "data": {
    "pipelineId": "your-kb-id",
    "ingestionId": "your-ingestion-id",
    "status": "PENDING",
    "created_at": 1783657930436,
    "updated_at": 1783657930436
  },
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-create-index-dto" />

| 字段            | 类型      | 说明                                                                                                                                                                                          |
| ------------- | ------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `code`        | string  | 响应码，成功时为 `Success`                                                                                                                                                                          |
| `status_code` | integer | HTTP 状态码                                                                                                                                                                                    |
| `success`     | boolean | 请求是否成功，成功时为 `true`                                                                                                                                                                          |
| `message`     | string  | 响应消息，成功时为 `success`                                                                                                                                                                         |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID                                                                                                                                                                         |
| `status`      | string  | 请求状态，成功时为 `SUCCESS`                                                                                                                                                                         |
| `data`        | object  | 响应数据。子字段 `pipelineId`（string，知识库 ID）、`ingestionId`（string，导入任务 ID）、`status`（string，任务状态，创建后为 `PENDING`）、`created_at`（integer，知识库创建时间，Unix 毫秒时间戳）、`updated_at`（integer，知识库最近更新时间，Unix 毫秒时间戳） |

## 错误码 <span id="h-ragapi-create-index-err" />

| HTTP 状态码 | 错误码（`code`）              | 说明                                                                                                                                 |
| -------- | ------------------------ | ---------------------------------------------------------------------------------------------------------------------------------- |
| 400      | `Index.InvalidParameter` | `description` 缺失或超长：`Required parameter(description length range[1-200]) missing or invalid, please check the request parameters.` |
| 400      | `Index.InvalidParameter` | `docIds` 缺失或为空数组：`Required parameter(file_ids) missing or invalid, please check the request parameters.`                           |
| 400      | `Index.FileEmptyError`   | `docIds` 中的文件 ID 不存在：`fetch empty file list from data center.`                                                                     |
| 401      | `InvalidApiKey`          | API Key 无效或缺失：`Invalid API-key provided.`                                                                                          |
