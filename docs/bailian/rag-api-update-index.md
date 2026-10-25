> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 更新知识库

> 修改指定知识库的名称、描述、重排序最低分数等基本信息。

<meta property="og:title" content="更新知识库" />

## 前提 <span id="h-ragapi-update-index-prereq" />

已获取 API Key 和业务空间 ID，并完成鉴权配置，详见[API 总览](/zh/model-studio/rag-api-overview)与[鉴权说明](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-update-index-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/index/update`

修改指定知识库的名称、描述、重排序最低分数等基本信息。`DenseSimilarityTopK`、`SparseSimilarityTopK`、`PipelineCommercialType` 等字段为 POP 专有，REST 接口不支持，请勿传入。

<Warning>
  参数名为 `id`，不是 `index_id` 或 `indexId`。请注意与其他接口的命名差异。
</Warning>

## 请求体 <span id="h-ragapi-update-index-req" />

| 字段               | 必填 | 类型     | 说明                                                                                    |
| ---------------- | -- | ------ | ------------------------------------------------------------------------------------- |
| `id`             | 是  | string | 知识库 ID。注意参数名为 `id`，不是 `index_id` 或 `indexId`                                          |
| `name`           | 否  | string | 知识库名称。长度 1\~20 字符，支持 Unicode letter 分类字符（含英文、中文、数字等），可含半角冒号(:)、下划线(\_)、半角句号(.)或短划线(-) |
| `description`    | 否  | string | 知识库描述信息                                                                               |
| `rerankMinScore` | 否  | number | 重排序最低分数阈值，取值范围 \[0, 1]。分数低于该值的切片会被过滤                                                  |

## 请求示例 <span id="h-ragapi-update-index-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/update" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "id": "your-kb-id",
    "name": "your-kb-name",
    "description": "your-kb-description",
    "rerankMinScore": 0.01
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-update-index-resp" />

更新成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "updated_at": 1782191157000,
    "created_at": 1782180218000,
    "id": "your-kb-id"
  },
  "success": true,
  "message": "success",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-update-index-dto" />

| 字段            | 类型      | 说明                                                                                                           |
| ------------- | ------- | ------------------------------------------------------------------------------------------------------------ |
| `code`        | string  | 响应码，成功时为 `Success`                                                                                           |
| `status_code` | integer | HTTP 状态码                                                                                                     |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID                                                                                          |
| `success`     | boolean | 请求是否成功                                                                                                       |
| `message`     | string  | 提示信息，成功时为 `success`                                                                                          |
| `status`      | string  | 请求状态：`SUCCESS` 或 `FAILED`                                                                                    |
| `data`        | object  | 响应数据。子字段 `id`（string，知识库 ID）、`updated_at`（integer，更新时间，毫秒级 Unix 时间戳）、`created_at`（integer，创建时间，毫秒级 Unix 时间戳） |
