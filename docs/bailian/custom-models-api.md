> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 模型导入

> 百炼模型导入 API 提供创建、查询、列举、删除导入任务的完整接口，支持将 OSS 中的调优模型文件导入百炼平台。

<meta property="og:title" content="模型导入" />

## 概述 <span id="mim-h2-overview" />

模型导入 API 用于将您在 OSS 中存储的调优模型文件导入到百炼平台，导入完成后即可通过创建部署接口将模型部署为可调用服务。模型导入支持全参调优（`full`）和 LoRA 调优（`lora`）两种模型类型。

完整的模型导入流程如下：创建导入任务 → 查询导入任务详情（轮询任务状态）→ 任务成功后创建部署 → 不再需要的任务可列举后删除。

> 当前模型导入 API 仅在北京 Region 开放。如您使用其他 Region，请通过该 Region 的百炼控制台完成模型导入操作。

## 前提条件 <span id="mim-h2-prereq" />

- 已配置百炼的 API-KEY，请参考[获取与配置 API Key](/zh/model-studio/get-api-key)。
- 已创建 OSS Bucket，并完成百炼平台的 OSS 授权。详见[模型导入](/zh/model-studio/model-import)中的使用前提。
- 模型文件已上传至 OSS Bucket，并符合[模型导入](/zh/model-studio/model-import)。

## 公共请求头 <span id="mim-h2-headers" />

所有接口均需在 HTTP Header 中携带以下字段：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th><p><strong>Header</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Authorization</p></td><td><p><code>Bearer $\{DASHSCOPE\_API\_KEY}</code>，API-KEY 的获取请参考<a href="/zh/model-studio/get-api-key">获取与配置 API Key</a>。</p></td></tr><tr><td><p>Content-Type</p></td><td><p><code>application/json</code></p></td></tr></tbody></table>

## 自定义模型对象 <span id="mim-h2-object" />

自定义模型对象描述了一个导入任务的完整信息。通过[创建导入任务](/zh/model-studio/custom-models-api#mim-h2-create)接口创建，可通过[查询导入任务详情](/zh/model-studio/custom-models-api#mim-h2-get)和[列举导入任务](/zh/model-studio/custom-models-api#mim-h2-list)接口获取。导入完成后可通过[创建部署](/zh/model-studio/create-deployment-api)接口进行部署。以下为自定义模型对象包含的字段：

### 对象字段 <span id="mim-h3-obj-fields" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28%" }} /><col style={{ width: "15%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求 ID。</p></td></tr><tr><td><p>output.job\_id</p></td><td><p>String</p></td><td><p>导入任务 ID，用于查询任务状态或删除任务。</p></td></tr><tr><td><p>output.model\_name</p></td><td><p>String</p></td><td><p>系统生成的模型标识，格式为基础模型名称加时间戳后缀。</p></td></tr><tr><td><p>output.display\_name</p></td><td><p>String</p></td><td><p>导入模型的显示名称。</p></td></tr><tr><td><p>output.source</p></td><td><p>String</p></td><td><p>导入来源，返回值为大写 <code>OSS</code>。</p></td></tr><tr><td><p>output.weight\_type</p></td><td><p>String</p></td><td><p>调优类型。</p></td></tr><tr><td><p>output.storage\_info</p></td><td><p>Object</p></td><td><p>导入来源的存储信息，包含 <code>bucket\_name</code> 和 <code>object\_key</code>。</p></td></tr><tr><td><p>output.status</p></td><td><p>String</p></td><td><p>任务状态。参见<a href="/zh/model-studio/custom-models-api#mim-h3-status">任务状态说明</a>。</p></td></tr><tr><td><p>output.gmt\_create</p></td><td><p>String</p></td><td><p>任务创建时间，ISO 8601 格式。示例：<code>2024-01-01T12:00:00.000+00:00</code>。</p></td></tr></tbody></table>

### 任务状态说明 <span id="mim-h3-status" />

导入任务在生命周期中可能处于以下状态：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "75%" }} /></colgroup><thead><tr><th><p><strong>状态</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>PENDING</p></td><td><p>任务已提交，等待处理。</p></td></tr><tr><td><p>RUNNING</p></td><td><p>任务正在执行中，系统正在校验和导入模型文件。</p></td></tr><tr><td><p>SUCCESSED</p></td><td><p>任务执行成功，模型已导入完成，可以通过<a href="/zh/model-studio/create-deployment-api">创建部署</a>接口进行部署。</p></td></tr><tr><td><p>FAILED</p></td><td><p>任务执行失败。可通过查询任务详情获取 <code>error\_code</code> 了解失败原因。</p></td></tr></tbody></table>

## 创建导入任务 <span id="mim-h2-create" />

提交一个模型导入任务。系统将对模型文件进行结构和安全校验，确保文件能够正常部署。

### 地址 <span id="mim-crt-h3-addr" />

```http
POST https://dashscope.aliyuncs.com/api/v1/custom_models/import
```

### 请求示例 <span id="mim-crt-h3-req" />

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/custom_models/import" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header "Content-Type: application/json" \
    --data '{
        "model_name": "qwen3-32b",
        "display_name": "我的 LoRA 调优模型",
        "source": "oss",
        "weight_type": "lora",
        "storage_info": {
            "bucket_name": "my-model-bucket",
            "object_key": "models/qwen3-32b-lora/"
        }
    }'
```

### 请求参数 <span id="mim-crt-h3-params" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "10%" }} /><col style={{ width: "10%" }} /><col style={{ width: "8%" }} /><col style={{ width: "52%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>model\_name</p></td><td><p>String</p></td><td><p>body</p></td><td><p>是</p></td><td><p>基础模型的名称。对应控制台<strong>基础模型</strong>字段。当前支持的模型请参见<a href="/zh/model-studio/model-import">模型导入</a>。示例：<code>qwen3-32b</code>。</p></td></tr><tr><td><p>display\_name</p></td><td><p>String</p></td><td><p>body</p></td><td><p>否</p></td><td><p>导入模型的显示名称，对应控制台<strong>模型名称</strong>字段。最多50个字符。不传时默认使用基础模型名称。</p></td></tr><tr><td><p>source</p></td><td><p>String</p></td><td><p>body</p></td><td><p>是</p></td><td><p>导入来源。对应控制台<strong>导入来源</strong>字段。当前仅支持 <code>oss</code>（从 OSS 导入）。响应中返回大写 <code>OSS</code>。</p></td></tr><tr><td><p>weight\_type</p></td><td><p>String</p></td><td><p>body</p></td><td><p>是</p></td><td><p>调优类型。<code>full</code> 表示全参调优模型，<code>lora</code> 表示 LoRA 调优模型。</p></td></tr><tr><td><p>storage\_info</p></td><td><p>Object</p></td><td><p>body</p></td><td><p>是</p></td><td><p>导入来源的存储信息。</p></td></tr><tr><td><p>storage\_info.bucket\_name</p></td><td><p>String</p></td><td><p>body</p></td><td><p>是</p></td><td><p>OSS Bucket 名称。对应控制台<strong>Bucket</strong>字段。</p></td></tr><tr><td><p>storage\_info.object\_key</p></td><td><p>String</p></td><td><p>body</p></td><td><p>是</p></td><td><p>模型文件所在 OSS 路径前缀，需以 <code>/</code> 结尾。示例：<code>models/qwen3-32b-lora/</code>。</p></td></tr></tbody></table>

### 响应示例 <span id="mim-crt-h3-resp" />

```json
{
    "request_id": "6c6b****-3fea-****-bc26-c9e2********",
    "output": {
        "job_id": "937b****-2a4f-****-8abe-c2fa********",
        "model_name": "qwen3-32b-offline-20240101-abc1",
        "display_name": "我的 LoRA 调优模型",
        "source": "OSS",
        "weight_type": "lora",
        "storage_info": {
            "bucket_name": "my-model-bucket",
            "object_key": "models/qwen3-32b-lora/"
        },
        "status": "PENDING",
        "gmt_create": "2024-01-01T12:00:00.000+00:00"
    }
}
```

### 响应参数 <span id="mim-crt-h3-resp-params" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28%" }} /><col style={{ width: "15%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求 ID。</p></td></tr><tr><td><p>output.job\_id</p></td><td><p>String</p></td><td><p>导入任务 ID，可用于<a href="/zh/model-studio/custom-models-api#mim-h2-get">查询导入任务详情</a>、<a href="/zh/model-studio/custom-models-api#mim-h2-list">列举导入任务</a>和<a href="/zh/model-studio/custom-models-api#mim-h2-delete">删除导入任务</a>接口。</p></td></tr><tr><td><p>output.model\_name</p></td><td><p>String</p></td><td><p>系统生成的模型标识，格式为基础模型名称加时间戳后缀。</p></td></tr><tr><td><p>output.display\_name</p></td><td><p>String</p></td><td><p>导入模型的显示名称。</p></td></tr><tr><td><p>output.source</p></td><td><p>String</p></td><td><p>导入来源，返回值为大写 <code>OSS</code>。</p></td></tr><tr><td><p>output.weight\_type</p></td><td><p>String</p></td><td><p>调优类型。</p></td></tr><tr><td><p>output.storage\_info</p></td><td><p>Object</p></td><td><p>导入来源的存储信息，包含 <code>bucket\_name</code> 和 <code>object\_key</code>。</p></td></tr><tr><td><p>output.status</p></td><td><p>String</p></td><td><p>任务状态。参见<a href="/zh/model-studio/custom-models-api#mim-h3-status">任务状态说明</a>。</p></td></tr><tr><td><p>output.gmt\_create</p></td><td><p>String</p></td><td><p>任务创建时间，ISO 8601 格式。示例：<code>2024-01-01T12:00:00.000+00:00</code>。</p></td></tr></tbody></table>

## 查询导入任务详情 <span id="mim-h2-get" />

查询指定导入任务的当前状态及详情。

### 地址 <span id="mim-get-h3-addr" />

```http
GET https://dashscope.aliyuncs.com/api/v1/custom_models/import/{job_id}
```

### 请求示例 <span id="mim-get-h3-req" />

```bash
curl "https://dashscope.aliyuncs.com/api/v1/custom_models/import/937b****-2a4f-****-8abe-c2fa********" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header "Content-Type: application/json"
```

### 请求参数 <span id="mim-get-h3-params" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "10%" }} /><col style={{ width: "10%" }} /><col style={{ width: "8%" }} /><col style={{ width: "52%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>导入任务 ID，可通过<a href="/zh/model-studio/custom-models-api#mim-h2-create">创建导入任务</a>或<a href="/zh/model-studio/custom-models-api#mim-h2-list">列举导入任务</a>接口获取。</p></td></tr></tbody></table>

### 响应示例 <span id="mim-get-h3-resp" />

```json
{
    "request_id": "ca21****-b91b-****-bd35-c41c********",
    "output": {
        "job_id": "937b****-2a4f-****-8abe-c2fa********",
        "model_name": "qwen3-32b-offline-20240101-abc1",
        "display_name": "我的 LoRA 调优模型",
        "source": "OSS",
        "storage_info": {
            "bucket_name": "my-model-bucket",
            "object_key": "models/qwen3-32b-lora/"
        },
        "status": "RUNNING",
        "gmt_create": "2024-01-01T12:00:00.000+00:00"
    }
}
```

### 响应参数 <span id="mim-get-h3-resp-params" />

响应参数与[创建导入任务](/zh/model-studio/custom-models-api#mim-h2-create)的响应参数基本一致，但不包含 `weight_type` 字段。当任务失败时，响应中将额外包含 `error_code` 字段，表示失败原因。

## 列举导入任务 <span id="mim-h2-list" />

分页查询当前工作空间下的导入任务列表。

### 地址 <span id="mim-lst-h3-addr" />

```http
GET https://dashscope.aliyuncs.com/api/v1/custom_models/import
```

### 请求示例 <span id="mim-lst-h3-req" />

```bash
curl "https://dashscope.aliyuncs.com/api/v1/custom_models/import?page_no=1&page_size=10" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header "Content-Type: application/json"
```

按状态过滤：

```bash
curl "https://dashscope.aliyuncs.com/api/v1/custom_models/import?page_no=1&page_size=10&status=SUCCESSED" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header "Content-Type: application/json"
```

### 请求参数 <span id="mim-lst-h3-params" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "10%" }} /><col style={{ width: "10%" }} /><col style={{ width: "8%" }} /><col style={{ width: "52%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>page\_no</p></td><td><p>Integer</p></td><td><p>query</p></td><td><p>否</p></td><td><p>页码，默认值为1。</p></td></tr><tr><td><p>page\_size</p></td><td><p>Integer</p></td><td><p>query</p></td><td><p>否</p></td><td><p>每页条数，默认值为10，最大值为100。</p></td></tr><tr><td><p>status</p></td><td><p>String</p></td><td><p>query</p></td><td><p>否</p></td><td><p>按任务状态过滤，参见<a href="/zh/model-studio/custom-models-api#mim-h3-status">任务状态说明</a>。</p></td></tr><tr><td><p>model\_name</p></td><td><p>String</p></td><td><p>query</p></td><td><p>否</p></td><td><p>按模型名称过滤，需传入响应中返回的系统生成名称（精确匹配）。</p></td></tr></tbody></table>

### 响应示例 <span id="mim-lst-h3-resp" />

```json expandable
{
    "request_id": "ca21****-b91b-****-bd35-c41c********",
    "output": {
        "total": 2,
        "page_no": 1,
        "page_size": 10,
        "list": [
            {
                "job_id": "937b****-2a4f-****-8abe-c2fa********",
                "model_name": "qwen3-32b-offline-20240101-abc1",
                "display_name": "我的 LoRA 调优模型",
                "status": "SUCCESSED",
                "source": "OSS",
                "storage_info": {
                    "bucket_name": "my-model-bucket",
                    "object_key": "models/qwen3-32b-lora/"
                },
                "gmt_create": "2024-01-01T12:00:00.000+00:00"
            },
            {
                "job_id": "edb0****-39ac-****-9859-8b1e********",
                "model_name": "qwen3-32b-offline-20240102-xyz4",
                "display_name": "我的全参调优模型",
                "status": "FAILED",
                "source": "OSS",
                "storage_info": {
                    "bucket_name": "my-model-bucket",
                    "object_key": "models/qwen3-32b-full/"
                },
                "error_code": "OSS获取文件失败，请检查OSS内文件",
                "gmt_create": "2024-01-02T09:00:00.000+00:00"
            }
        ]
    }
}
```

### 响应参数 <span id="mim-lst-h3-resp-params" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28%" }} /><col style={{ width: "15%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求 ID。</p></td></tr><tr><td><p>output.total</p></td><td><p>Integer</p></td><td><p>满足查询条件的任务总数。</p></td></tr><tr><td><p>output.page\_no</p></td><td><p>Integer</p></td><td><p>当前页码。</p></td></tr><tr><td><p>output.page\_size</p></td><td><p>Integer</p></td><td><p>每页条数。</p></td></tr><tr><td><p>output.list</p></td><td><p>Array</p></td><td><p>导入任务列表。每个元素的字段与<a href="/zh/model-studio/custom-models-api#mim-h2-create">创建导入任务</a>的响应参数基本一致，但不包含 <code>weight\_type</code> 字段。任务失败时额外包含 <code>error\_code</code> 字段。</p></td></tr></tbody></table>

## 删除导入任务 <span id="mim-h2-delete" />

删除指定的导入任务及其关联的模型文件。只有状态为 `SUCCESSED` 或 `FAILED` 的任务可以删除。删除成功后返回被删除任务的详情。

### 地址 <span id="mim-del-h3-addr" />

```http
DELETE https://dashscope.aliyuncs.com/api/v1/custom_models/import/{job_id}
```

### 请求示例 <span id="mim-del-h3-req" />

```bash
curl -X DELETE "https://dashscope.aliyuncs.com/api/v1/custom_models/import/937b****-2a4f-****-8abe-c2fa********" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header "Content-Type: application/json"
```

### 请求参数 <span id="mim-del-h3-params" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "10%" }} /><col style={{ width: "10%" }} /><col style={{ width: "8%" }} /><col style={{ width: "52%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>导入任务 ID，可通过<a href="/zh/model-studio/custom-models-api#mim-h2-create">创建导入任务</a>或<a href="/zh/model-studio/custom-models-api#mim-h2-list">列举导入任务</a>接口获取。</p></td></tr></tbody></table>

### 响应示例 <span id="mim-del-h3-resp" />

```json
{
    "request_id": "e22b****-b20a-****-bf23-9b53********",
    "output": {
        "job_id": "937b****-2a4f-****-8abe-c2fa********",
        "model_name": "qwen3-32b-offline-20240101-abc1",
        "display_name": "我的 LoRA 调优模型",
        "source": "OSS",
        "storage_info": {
            "bucket_name": "my-model-bucket",
            "object_key": "models/qwen3-32b-lora/"
        },
        "status": "SUCCESSED",
        "gmt_create": "2024-01-01T12:00:00.000+00:00"
    }
}
```

### 响应参数 <span id="mim-del-h3-resp-params" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28%" }} /><col style={{ width: "15%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求 ID。</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>被删除的任务详情，字段与<a href="/zh/model-studio/custom-models-api#mim-h2-create">创建导入任务</a>的响应参数基本一致，但不包含 <code>weight\_type</code> 字段。</p></td></tr></tbody></table>

## 异常响应 <span id="mim-h2-errors" />

当请求发生错误时，接口将返回如下格式的错误响应：

```json
{
    "request_id": "ca21****-b91b-****-bd35-c41c********",
    "code": "OperationDenied",
    "message": "The import job is currently running and cannot be deleted."
}
```

### 错误码 <span id="mim-err-h3-codes" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th><p><strong>错误码</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>InvalidParameter</p></td><td><p>请求参数无效。例如必填参数缺失、参数格式错误或参数值不合法。</p></td></tr><tr><td><p>NotFound</p></td><td><p>指定的资源不存在。例如 job\_id 不存在、无权访问或基础模型不支持导入。</p></td></tr><tr><td><p>OperationDenied</p></td><td><p>操作被拒绝。例如对 RUNNING 状态的任务执行删除操作。</p></td></tr><tr><td><p>InvalidApiKey</p></td><td><p>API-KEY 无效或未提供。</p></td></tr><tr><td><p>InternalError</p></td><td><p>系统内部错误，请稍后重试。</p></td></tr></tbody></table>
