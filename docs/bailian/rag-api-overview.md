> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 概览

> RAG API 的服务地址、协议约定、请求格式、接口清单与限流规则。

<meta property="og:title" content="概览" />

通过 RAG API，您可以以编程方式管理知识库、导入文档、执行检索以及调用知识问答服务。所有接口均通过 DashScope 网关提供服务。

<Note>
  调用 API 前，请先[获取 API Key](/zh/model-studio/rag-api-authentication)。
</Note>

## 服务地址 <span id="h-ragapi-overview-endpoint" />

`{workspace_id}` 是业务空间 ID，格式如 `llm-xxxxxxxxxxxx`，在控制台[**业务空间管理**](https://bailian.console.aliyun.com/?tab=globalset#/efm/business_management)页面查看或创建。

所有接口使用统一的服务地址（Base URL）：

```text
https://{workspace_id}.cn-beijing.maas.aliyuncs.com
```

RAG API 包含三组接口，路径前缀不同：

| 接口组   | 路径前缀                      | 说明                    |
| ----- | ------------------------- | --------------------- |
| 知识库管理 | `/api/v1/indices/`        | 知识库 CRUD、文档管理、切片管理、检索 |
| 数据导入  | `/api/v1/connector/dash/` | 类目管理、文件上传、连接器配置       |
| 知识问答  | `/api/v2/apps/knowledge/` | 知识问答服务（SSE 流式）        |

例如，检索接口的完整地址为：

```text
https://llm-xxxxxxxxxxxx.cn-beijing.maas.aliyuncs.com/api/v1/indices/rag/index/retrieve
```

该检索接口的 `search_filters` 支持通过 `_operator` 和嵌套 `_conditions` 表达同一字段的多个 OR 条件，具体语法和示例请参见[知识库 SearchFilters](/zh/model-studio/how-to-use-search-filters)。在 Spring AI Alibaba 应用中，可以通过 `RestClient` 或 `WebClient` 直接调用此地址，也可以使用 SAA DashScope SDK 的独立 Pipeline Retrieve 路径。两种方式的选择和 Java 示例见[通过 Spring AI Alibaba 检索阿里云百炼知识库](/zh/model-studio/spring-ai-alibaba-integrate-knowledge-base#saa-retrieval-methods)。

## 协议约定 <span id="h-ragapi-overview-protocol" />

- 所有接口均通过 **HTTPS** 访问，不支持 HTTP。
- 请求体和响应体均为 **JSON** 格式，字符集 **UTF-8**。
- 知识库管理、文档管理、切片管理、检索、数据导入等接口统一使用 `POST` 方法；少数查询接口使用 `GET` 方法并通过 query string 传参，包括[**查询知识库列表**](/zh/model-studio/rag-api-list-indices)、[**查询文档列表**](/zh/model-studio/rag-api-list-documents)、[**查询导入任务状态**](/zh/model-studio/rag-api-get-sync-job-status)。各接口的实际方法与参数位置以接口说明为准。

## 通用请求头 <span id="h-ragapi-overview-headers" />

| Header          | 必填 | 说明                                                                                                                                               |
| --------------- | -- | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| `Authorization` | 是  | `Bearer <API-Key>`，在[控制台 API Key 页](https://bailian.console.aliyun.com/?tab=model#/api-key)获取。详见[认证方式](/zh/model-studio/rag-api-authentication)。 |
| `Content-Type`  | 是  | POST 请求为 `application/json`。                                                                                                                     |

## 通用响应格式 <span id="h-ragapi-overview-response" />

所有接口返回统一的 JSON 结构。

**成功响应：**

```json
{
  "code": "Success",
  "status_code": 200,
  "data": { },
  "success": true,
  "message": "success",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

**失败响应：**

```json
{
  "code": "Index.InvalidParameter",
  "status_code": 400,
  "message": "Required parameter(xxx) missing or invalid, please check the request parameters.",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

排查问题时，请提供 `request_id`，以便技术支持快速定位。完整错误码说明见[错误码](/zh/model-studio/rag-api-errors)。

## 分页 <span id="h-ragapi-overview-pagination" />

列表接口支持分页查询。不同接口的请求方法、参数命名与传参位置存在差异，调用时需注意区分：

| 适用接口                                                   | 方法   | 页码参数                  | 每页条数        | 传参位置         |
| ------------------------------------------------------ | ---- | --------------------- | ----------- | ------------ |
| [查询知识库列表](/zh/model-studio/rag-api-list-indices)       | GET  | `page_number`（从 1 开始） | `page_size` | Query String |
| [查询文档列表](/zh/model-studio/rag-api-list-documents)      | GET  | `page_num`            | `page_size` | Query String |
| [查询切片列表](/zh/model-studio/rag-api-list-chunks)         | POST | `page_num`            | `page_size` | Request Body |
| [查询文件详情列表](/zh/model-studio/rag-api-list-file-details) | POST | `pageNumber`（从 1 开始）  | `pageSize`  | Request Body |

<Note>
  查询知识库列表的分页参数必须通过 query string 传递（`page_number`、`page_size`，注意是 `page_number` 而非 `page_num`）；若放在请求体中，服务端会忽略并按默认值返回。
</Note>

## 接口清单 <span id="h-ragapi-overview-list" />

### 知识库 <span id="h-ragapi-overview-list-kb" />

| 接口                                                      | 说明                     |
| ------------------------------------------------------- | ---------------------- |
| [创建知识库并导入](/zh/model-studio/rag-api-create-index)       | 一步完成创建知识库和导入文件         |
| [查询知识库列表](/zh/model-studio/rag-api-list-indices)        | 分页查询当前业务空间下的所有知识库      |
| [更新知识库](/zh/model-studio/rag-api-update-index)          | 修改指定知识库的描述信息           |
| [删除知识库](/zh/model-studio/rag-api-delete-index)          | 永久删除指定的知识库及其所有文档和切片    |
| [获取知识库监控数据](/zh/model-studio/rag-api-get-index-monitor) | 查询指定知识库的存储用量和 QPS 监控数据 |

### 文档管理 <span id="h-ragapi-overview-list-docs" />

| 接口                                                     | 说明                    |
| ------------------------------------------------------ | --------------------- |
| [查询文档列表](/zh/model-studio/rag-api-list-documents)      | 查询指定知识库中的文档列表         |
| [查询文件详情列表](/zh/model-studio/rag-api-list-file-details) | 查询文档的详细信息，包括切片数量和解析状态 |
| [删除文档](/zh/model-studio/rag-api-delete-document)       | 从知识库中删除文档及其所有关联切片     |

### 切片管理 <span id="h-ragapi-overview-list-chunks" />

| 接口                                             | 说明               |
| ---------------------------------------------- | ---------------- |
| [新增切片](/zh/model-studio/rag-api-add-chunk)     | 向指定知识库添加切片内容     |
| [查询切片列表](/zh/model-studio/rag-api-list-chunks) | 分页查询知识库中的切片      |
| [更新切片](/zh/model-studio/rag-api-update-chunk)  | 更新指定切片的内容        |
| [删除切片](/zh/model-studio/rag-api-delete-chunk)  | 批量删除切片，单次最多 10 个 |

### 同步任务 <span id="h-ragapi-overview-list-sync" />

| 接口                                                     | 说明             |
| ------------------------------------------------------ | -------------- |
| [提交导入任务](/zh/model-studio/rag-api-submit-sync-job)     | 向已创建的知识库追加导入文档 |
| [查询任务状态](/zh/model-studio/rag-api-get-sync-job-status) | 查询知识库导入任务的执行状态 |

### 数据导入 <span id="h-ragapi-overview-list-import" />

| 接口                                                  | 说明                                  |
| --------------------------------------------------- | ----------------------------------- |
| [查询类目列表](/zh/model-studio/rag-api-list-category)    | 查询当前业务空间下的类目列表，支持按类型、名称过滤           |
| [新增类目](/zh/model-studio/rag-api-add-category)       | 在当前业务空间下创建新的类目                      |
| [删除类目](/zh/model-studio/rag-api-delete-category)    | 删除指定的类目                             |
| [查询文件列表](/zh/model-studio/rag-api-list-file)        | 查询当前业务空间下的文件列表，支持按类目、文件名和文件 ID 过滤   |
| [查询文件详情](/zh/model-studio/rag-api-describe-file)    | 查询指定文件的详细信息，包括大小、MD5、标签和时间戳         |
| [删除文件](/zh/model-studio/rag-api-delete-file)        | 永久删除指定的文件                           |
| [批量更新标签](/zh/model-studio/rag-api-batch-update-tag) | 批量更新多个文件的标签，支持覆盖和追加两种模式             |
| [申请上传租约](/zh/model-studio/rag-api-upload-lease)     | 上传文件到数据中心的第一步，返回 OSS 预签名 URL 和租约 ID |
| [注册文件](/zh/model-studio/rag-api-add-file)           | 上传文件的最后一步，将通过 OSS 上传的文件注册到数据中心      |
| [从 OSS 批量导入](/zh/model-studio/rag-api-oss-import)   | 从已授权的阿里云 OSS Bucket 批量导入文件到数据中心     |
| [新增连接器](/zh/model-studio/rag-api-add-connector)     | 创建新的数据连接器，用于管理数据导入来源                |
| [查询连接器](/zh/model-studio/rag-api-get-connector)     | 查询指定连接器的详细信息                        |

### 知识检索与问答 <span id="h-ragapi-overview-list-qa" />

| 接口                                       | 说明              |
| ---------------------------------------- | --------------- |
| [知识检索](/zh/model-studio/knowledgesearch) | 跨多个知识库执行联合语义检索  |
| [知识问答](/zh/model-studio/knowledgechat)   | 基于知识库的 SSE 流式问答 |

### Agent 管理 <span id="h-ragapi-overview-list-agent" />

| 接口                                                    | 说明                           |
| ----------------------------------------------------- | ---------------------------- |
| [Agent 管理概述](/zh/model-studio/rag-api-agent-overview) | RAG Agent 实例的全生命周期管理 API     |
| [创建 Agent](/zh/model-studio/rag-api-agent-create)     | 创建一个新的 RAG Agent，初始状态为 draft |
| [更新 Agent](/zh/model-studio/rag-api-agent-update)     | 更新 Agent 的名称、描述或草稿配置         |
| [发布 Agent](/zh/model-studio/rag-api-agent-deploy)     | 将 beta 草稿发布为新版本              |
| [删除 Agent](/zh/model-studio/rag-api-agent-delete)     | 软删除 Agent，标记状态为 deleted      |
| [查询 Agent 列表](/zh/model-studio/rag-api-agent-list)    | 分页查询当前租户下的 Agent 列表          |
| [查询 Agent 详情](/zh/model-studio/rag-api-agent-get)     | 获取 Agent 完整信息，支持指定版本         |
| [复制 Agent](/zh/model-studio/rag-api-agent-copy)       | 复制已有 Agent，生成新的草稿            |

## 限流 <span id="h-ragapi-overview-ratelimit" />

各接口设有请求频率限制，超出时返回 HTTP `429`。完整的限流规则见[限流策略](/zh/model-studio/rag-api-rate-limits)。
