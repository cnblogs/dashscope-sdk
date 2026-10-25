> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询知识库列表

> 分页查询当前业务空间下的所有知识库，返回知识库基本信息、嵌入模型和重排序模型配置。

<meta property="og:title" content="查询知识库列表" />

## 前提 <span id="h-ragapi-list-indices-prereq" />

已获取 API Key 和业务空间 ID，并完成鉴权配置，详见[API 总览](/zh/model-studio/rag-api-overview)与[鉴权说明](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-list-indices-endpoint" />

<strong>GET</strong> `/api/v1/indices/rag/index/list`

分页查询当前业务空间下的所有知识库，返回知识库基本信息、嵌入模型和重排序模型配置。分页与过滤参数通过 query string 传递。

## 请求参数 <span id="h-ragapi-list-indices-req" />

以下参数通过 query string 传递。

| 参数              | 必填 | 类型      | 说明                                                                                                                      |
| --------------- | -- | ------- | ----------------------------------------------------------------------------------------------------------------------- |
| `page_number`   | 否  | integer | 页码，从 1 开始，默认 1                                                                                                          |
| `page_size`     | 否  | integer | 每页返回的知识库数量，取值范围 1\~100，默认 10                                                                                            |
| `pipeline_name` | 否  | string  | 按知识库名称模糊过滤，返回名称包含该值的知识库；不传时返回业务空间下所有知识库。长度 1\~20 字符，支持 Unicode letter 分类字符（含英文、中文、数字等），可含半角冒号(:)、下划线(\_)、半角句号(.)或短划线(-) |

## 请求示例 <span id="h-ragapi-list-indices-eg" />

```bash
curl -G "$BASE_URL/api/v1/indices/rag/index/list" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -d "page_number=1" \
  -d "page_size=20"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-list-indices-resp" />

查询成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "page_number": 1,
    "total_count": 1,
    "rows": [
      {
        "id": "your-kb-id",
        "name": "my-knowledge-base",
        "description": "示例知识库",
        "dataType": "unstructured",
        "structureType": "unstructured",
        "knowledgeType": null,
        "embeddingModelName": "text-embedding-v4",
        "embeddingDimension": null,
        "multimodalEmbeddingModelName": null,
        "rerankModelName": "qwen3-rerank",
        "rerankMinScore": 0.2,
        "rerankTopN": null,
        "rerankMode": null,
        "rerankInstruct": null,
        "chunkSize": 600,
        "overlapSize": 100,
        "separator": null,
        "chunkMode": null,
        "sourceType": "DATA_CENTER_CATEGORY",
        "docIds": [
          "cate_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx",
          "cate_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx_xxxxxx"
        ],
        "columns": null,
        "enableRewrite": true,
        "connectorId": null,
        "sparseSimilarityTopK": null,
        "denseSimilarityTopK": null
      }
    ],
    "page_size": 20
  },
  "success": true,
  "message": "success",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-list-indices-dto" />

| 字段            | 类型      | 说明                                                                                                                              |
| ------------- | ------- | ------------------------------------------------------------------------------------------------------------------------------- |
| `code`        | string  | 响应码，成功时为 `Success`                                                                                                              |
| `status_code` | integer | HTTP 状态码                                                                                                                        |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID                                                                                                             |
| `success`     | boolean | 请求是否成功                                                                                                                          |
| `message`     | string  | 提示信息，成功时为 `success`                                                                                                             |
| `status`      | string  | 请求状态：`SUCCESS` 或 `FAILED`                                                                                                       |
| `data`        | object  | 分页结果。子字段 `page_number`（integer，当前页码）、`total_count`（integer，知识库总数）、`page_size`（integer，当前每页条数，与请求一致）、`rows`（array，知识库列表，元素字段见下表） |

### rows 元素字段 <span id="h-ragapi-list-indices-rows" />

| 字段                             | 类型              | 说明                                                                          |
| ------------------------------ | --------------- | --------------------------------------------------------------------------- |
| `id`                           | string          | 知识库唯一标识                                                                     |
| `name`                         | string          | 知识库名称                                                                       |
| `description`                  | string          | 知识库描述信息                                                                     |
| `dataType`                     | string          | 数据类型，如 `unstructured`                                                       |
| `structureType`                | string          | 结构类型：`unstructured`（非结构化）或 `structured`（结构化）                                |
| `knowledgeType`                | string          | 知识库类型                                                                       |
| `embeddingModelName`           | string          | 向量嵌入模型，如 `text-embedding-v4`                                                |
| `embeddingDimension`           | integer         | 向量维度                                                                        |
| `multimodalEmbeddingModelName` | string          | 多模态嵌入模型名称（图片/音视频知识库）                                                        |
| `chunkSize`                    | integer         | 切片大小（字符数）                                                                   |
| `overlapSize`                  | integer         | 切片重叠大小（字符数）                                                                 |
| `separator`                    | string          | 切片分隔符                                                                       |
| `chunkMode`                    | string          | 切片模式                                                                        |
| `rerankModelName`              | string          | 重排序模型，如 `qwen3-rerank`。为空表示未启用                                              |
| `rerankMinScore`               | number          | 重排序最低分数阈值                                                                   |
| `rerankTopN`                   | integer         | 重排序返回数量                                                                     |
| `rerankMode`                   | string          | 重排序模式                                                                       |
| `rerankInstruct`               | string          | 自定义重排序指令                                                                    |
| `sourceType`                   | string          | 数据源类型，如 `DATA_CENTER_CATEGORY`                                              |
| `docIds`                       | `array<string>` | 关联的类目/文档 ID 列表                                                              |
| `columns`                      | `array<object>` | 列信息，结构化知识库时返回。子字段 `IsSearch`（boolean，是否参与知识库检索。为 `true` 时表示允许知识库在此列数据中进行搜索） |
| `structuredIndexConfig`        | object          | 数据查询或图片问答类知识库的索引配置信息                                                        |
| `connectorId`                  | string          | 关联的连接器 ID                                                                   |
| `enableRewrite`                | boolean         | 是否启用查询改写                                                                    |
| `sparseSimilarityTopK`         | integer         | 稀疏检索 TopK                                                                   |
| `denseSimilarityTopK`          | integer         | 稠密检索 TopK                                                                   |
| `knowledgeScene`               | string          | 知识库场景                                                                       |
| `workspaceId`                  | string          | 所属业务空间 ID                                                                   |
| `enableHeaders`                | boolean         | 切片是否保留标题层级信息                                                                |
| `enableVisualText`             | boolean         | 是否开启视觉文本解析                                                                  |
| `chunkOverlap`                 | integer         | 切片重叠长度                                                                      |
| `categoryIds`                  | array           | 关联的类目 ID 列表                                                                 |
| `tableIds`                     | array           | 结构化知识库关联的表 ID 列表                                                            |
| `dataSource`                   | object          | 数据源配置                                                                       |
| `sinkType`                     | string          | 向量存储类型，默认 `DEFAULT`                                                         |
| `sinkInstanceId`               | string          | 自建向量存储实例 ID，使用默认存储时为空                                                       |
| `sinkRegion`                   | string          | 向量存储所在地域                                                                    |
| `metaExtractColumns`           | array           | 元数据抽取列配置                                                                    |
| `pipelineTag`                  | string          | 知识库标签                                                                       |
| `createIndexType`              | string          | 知识库创建方式                                                                     |
| `dataSync`                     | boolean         | 是否开启数据同步                                                                    |
| `datasourceCode`               | string          | 数据源编码，结构化知识库使用                                                              |
| `table`                        | string          | 关联的数据表名，结构化知识库使用                                                            |
| `database`                     | string          | 关联的数据库名，结构化知识库使用                                                            |
| `channelType`                  | string          | 渠道类型                                                                        |

## 错误码 <span id="h-ragapi-list-indices-err" />

| HTTP 状态码 | 错误码（`code`）              | 说明                                              |
| -------- | ------------------------ | ----------------------------------------------- |
| 400      | `Index.InvalidParameter` | 请求参数无效：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`          | API Key 无效或缺失：`Invalid API-key provided.`       |
