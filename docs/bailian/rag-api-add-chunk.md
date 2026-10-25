> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 新增切片

> 向指定知识库添加切片内容。

<meta property="og:title" content="新增切片" />

## 前提 <span id="h-ragapi-add-chunk-prereq" />

已完成 Endpoint 与鉴权配置，详见 [API 总览](/zh/model-studio/rag-api-overview)和[认证](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-add-chunk-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/index/chunk/create`

向指定知识库添加切片内容。

<Note>
  支持文档搜索类（document）、数据查询类（table）、图片问答类（image）知识库；不支持音视频搜索类（multimedia）。数据查询和图片问答类知识库仅支持表格连接器（Excel）数据源。本接口具有幂等性，频繁调用会被限流，频率请勿超过 10 次/秒。
</Note>

## 请求体 <span id="h-ragapi-add-chunk-req" />

| 字段           | 必填 | 类型     | 说明                                                                                                                                                                                                                                                                                                                                                                                              |
| ------------ | -- | ------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `pipelineId` | 是  | string | 知识库 ID，即 [createIndex](/zh/model-studio/rag-api-create-index) 接口返回的 `data.id`                                                                                                                                                                                                                                                                                                                   |
| `dataId`     | 否  | string | 文件 ID，即 [addFile](/zh/model-studio/rag-api-add-file) 接口返回的 `fileId`。文档搜索类知识库用于指定切片所属文档                                                                                                                                                                                                                                                                                                          |
| `field`      | 是  | object | 切片内容信息，以键值对形式传入，结构因知识库类型而异。文档搜索类（document）使用固定 key，子字段 `content`（string，必填，切片正文内容，最大长度 6000）、`title`（string，选填，切片标题，最大长度 50）、`image_urls`（array\<string>，选填，切片包含的图片链接，最多 10 张）。数据查询类（table）、图片问答类（image）key 不固定，由知识库的数据源表格决定，key 为 Excel 列标题，value 为对应列的值，参与检索或回复的表头为必填；各类型取值要求：String 类型最大长度 6000；时间类型为 13 位时间戳（毫秒）；Long 类型为整数，最大 2147483647；Double 类型支持小数；image\_url 类型最多 5 张，多张用英文逗号拼接为一个字符串 |

## 请求示例 <span id="h-ragapi-add-chunk-eg" />

文档搜索类（document）知识库，使用固定 key：

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/chunk/create" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "pipelineId": "exg5ghdkag",
    "dataId": "file_2aaa9c62d6b243b5be26636fc65c279f_12326038",
    "field": {
      "content": "阿里云百炼 Knowledge Studio 支持多种文档格式的解析，包括 PDF、Word、PPT 等。",
      "title": "文档解析与切片",
      "image_urls": [
        "https://example.com/images/chunk-flow.png"
      ]
    }
  }'
```

数据查询类（table）知识库（Excel 数据源），key 由 Excel 列标题决定，参与检索或回复的表头为必填：

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/chunk/create" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "pipelineId": "exg5ghdkag",
    "dataId": "file_xxx",
    "field": {
      "Product Name": "Wireless Bluetooth Headphones",
      "Publish Time": 1752624000000,
      "Stock Quantity": 1580,
      "Unit Price": 299.99,
      "image_url": "https://example.com/images/headphones-front.jpg,https://example.com/images/headphones-side.jpg"
    }
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-add-chunk-resp" />

新增成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "success": true,
  "message": "success",
  "request_id": "56b54481-d259-9ff9-93c1-712897e5d8eb",
  "status": "SUCCESS"
}
```

### 响应字段 <span id="h-ragapi-add-chunk-dto" />

| 字段            | 类型      | 说明                     |
| ------------- | ------- | ---------------------- |
| `code`        | string  | 响应码，成功时为 `Success`     |
| `status_code` | integer | HTTP 状态码               |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID    |
| `success`     | boolean | 接口调用是否成功               |
| `message`     | string  | 响应消息，成功时为 `success`    |
| `status`      | string  | 接口返回的状态，成功时为 `SUCCESS` |

## 错误码 <span id="h-ragapi-add-chunk-errors" />

| HTTP 状态码 | 错误码                      | 错误信息                                     | 说明                 |
| -------- | ------------------------ | ---------------------------------------- | ------------------ |
| 400      | `Index.InvalidParameter` | `Required parameter missing or invalid.` | 请求参数无效             |
| 401      | `InvalidApiKey`          | `Invalid API-key provided.`              | 鉴权失败，API Key 无效或缺失 |
