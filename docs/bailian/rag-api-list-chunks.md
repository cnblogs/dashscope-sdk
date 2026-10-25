> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询切片列表

> 分页查询知识库中的切片。

<meta property="og:title" content="查询切片列表" />

## 前提 <span id="h-ragapi-list-chunks-prereq" />

已完成 Endpoint 与鉴权配置，详见 [API 总览](/zh/model-studio/rag-api-overview)和[认证](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-list-chunks-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/index/chunklist`

分页查询指定知识库中的切片列表。响应中 `data.nodes` 为切片数组，`data.total` 为切片总数。

<Note>
  `docId` 按文档过滤切片：文档搜索类和音视频搜索类知识库必传；数据查询类和图片问答类知识库无需传入。
</Note>

## 请求体 <span id="h-ragapi-list-chunks-req" />

| 字段         | 必填 | 类型      | 说明                                                                                  |
| ---------- | -- | ------- | ----------------------------------------------------------------------------------- |
| `indexId`  | 是  | string  | 知识库 ID，即 [createIndex](/zh/model-studio/rag-api-create-index) 接口返回的 `data.id`       |
| `pageNum`  | 否  | integer | 页码，从 1 开始，默认为 1                                                                     |
| `pageSize` | 否  | integer | 每页返回的切片数量，默认为 20，最大 100                                                             |
| `docId`    | 否  | string  | 按文档 ID 过滤，仅返回该文档下的切片。文档 ID 可通过[查询文档列表](/zh/model-studio/rag-api-list-documents)接口获取 |

## 请求示例 <span id="h-ragapi-list-chunks-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/chunklist" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "indexId": "your_index_id",
    "pageNum": 1,
    "pageSize": 10
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-list-chunks-resp" />

查询成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "total": 106,
    "nodes": [
      {
        "score": 0.0,
        "text": "这是切片的文本内容示例...",
        "metadata": {
          "_id": "llm-xxxxxxxxxxxx_your_kb_id_file_xxxxxxxx_0_0",
          "doc_id": "file_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
          "doc_name": "sample_document",
          "title": "产品概述",
          "content": "这是切片的文本内容示例...",
          "hier_title": "产品概述|功能介绍",
          "doc_url": "https://oss-example.aliyuncs.com/...",
          "file_path": "https://oss-example.aliyuncs.com/.../docJson/sample.json",
          "image_url": [],
          "_chunk_id": 0,
          "_block_id": 0,
          "pipeline_id": "your_kb_id",
          "workspace_id": "llm-xxxxxxxxxxxx",
          "_chunk_status": 0,
          "_chunk_status_message": "成功",
          "is_displayed_chunk_content": true
        }
      }
    ],
    "isDowngrade": false
  },
  "success": true,
  "message": "success",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-list-chunks-dto" />

| 字段            | 类型      | 说明                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| ------------- | ------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `code`        | string  | 响应码，成功时为 `Success`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| `status_code` | integer | HTTP 状态码                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
| `data`        | object  | 业务数据。子字段 `total`（integer，切片总数）、`nodes`（array\<object>，切片节点列表。元素字段：`score`（number，相关性分数，列表查询时通常为 0）、`text`（string，切片文本内容）、`metadata`（object，切片元数据。子字段：`_id`（string，切片全局唯一标识）、`doc_id`（string，所属文档 ID）、`doc_name`（string，所属文档名称）、`title`（string，切片标题）、`hier_title`（string，层级标题，按 \                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   | 分隔的标题路径）、`content`（string，切片内容）、`doc_url`（string，文档原始访问地址）、`file_path`（string，切片解析产物的 OSS 路径）、`image_url`（array\<string>，切片关联的图片地址）、`_chunk_id`（integer，文档内切片序号）、`_block_id`（integer，块序号）、`pipeline_id`（string，所属知识库 ID）、`workspace_id`（string，所属业务空间 ID）、`_chunk_status`（integer，切片处理状态码，0 表示成功）、`_chunk_status_message`（string，切片处理状态描述）、`is_displayed_chunk_content`（boolean，是否参与知识库检索）、`nid`（string，切片在知识库中的唯一编号）、`source`（integer，切片来源类型）））、`isDowngrade`（boolean，是否发生检索降级） |
| `success`     | boolean | 请求是否成功                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
| `message`     | string  | 响应消息                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| `status`      | string  | 请求状态，成功时为 `SUCCESS`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
