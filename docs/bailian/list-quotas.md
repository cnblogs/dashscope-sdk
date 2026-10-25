> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询模型限额

> 调用 GET /api/v1/models/limits 接口查询当前 API Key 下各模型的限流配额，包括请求频率限制和用量限制，用于了解和规划 API 调用量。

<meta property="og:title" content="查询模型限额" />

## 前提条件 <span id="b1c2d3e4f5a010" />

已创建 API Key 并配置为环境变量 `DASHSCOPE_API_KEY`。配置方法请参见[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## 请求说明 <span id="43f31e8630lzd" />

- <strong>HTTP 方法</strong>：GET
- <strong>请求地址</strong>

  > 调用时请将 `{WorkspaceId}` 替换为您的业务空间 ID。

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th><p><strong>地域</strong></p></th><th><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td><p>华北2（北京）</p></td><td><p><code>https\://\{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models/limits</code></p></td></tr></tbody></table>
- <strong>认证方式</strong>

  在请求 Header 中设置 `Authorization: Bearer {API_KEY}`。

## 请求参数 <span id="b1c2d3e4f5a100" />

所有参数均通过 Query String 传递。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15%" }} /><col style={{ width: "12%" }} /><col style={{ width: "8%" }} /><col style={{ width: "65%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>name</p></td><td><p>String</p></td><td><p>否</p></td><td><p>按模型名称进行模糊搜索。示例：<code>qwen</code></p></td></tr><tr><td><p>model</p></td><td><p>String</p></td><td><p>否</p></td><td><p>按模型 ID 进行精确查询。示例：<code>qwen3.8-max</code></p></td></tr><tr><td><p>page\_no</p></td><td><p>Integer</p></td><td><p>否</p></td><td><p>页码，从 1 开始。默认值：<code>1</code>。</p></td></tr><tr><td><p>page\_size</p></td><td><p>Integer</p></td><td><p>否</p></td><td><p>每页返回的模型数量。默认值：<code>20</code>。</p></td></tr></tbody></table>

## 返回参数 <span id="b1c2d3e4f5a200" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "12%" }} /><col style={{ width: "53%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求 ID，用于问题排查。</p></td></tr><tr><td><p>output.total</p></td><td><p>Number</p></td><td><p>符合条件的模型总数。</p></td></tr><tr><td><p>output.page\_no</p></td><td><p>Number</p></td><td><p>当前页码。</p></td></tr><tr><td><p>output.page\_size</p></td><td><p>Number</p></td><td><p>每页条数。</p></td></tr><tr><td><p>output.quotas\[].model</p></td><td><p>String</p></td><td><p>模型 ID。</p></td></tr><tr><td><p>output.quotas\[].workspace\_id</p></td><td><p>String</p></td><td><p>业务空间 ID。</p></td></tr><tr><td><p>output.quotas\[].model\_limit</p></td><td><p>Object</p></td><td><p>账号级别限流，即该模型在当前账号下的总限流上限。包含以下字段：</p><ul><li><p><code>request\_limit</code>（Number）：请求频率限制值。</p></li><li><p><code>request\_limit\_period</code>（Number）：请求限流的时间周期，单位秒。值为 <code>60</code> 表示每分钟（RPM），值为 <code>1</code> 表示每秒（QPS）。</p></li><li><p><code>usage\_limit</code>（Number）：用量限制数。值为 <code>null</code> 表示无用量限制。</p></li><li><p><code>usage\_limit\_field</code>（String）：用量限制类型，如 <code>total\_tokens</code>。</p></li><li><p><code>usage\_limit\_period</code>（Number）：用量限制的时间周期，单位秒。</p></li><li><p><code>async\_user\_queue\_limit</code>（Number）：异步任务排队数上限。</p></li><li><p><code>async\_user\_concurrency\_limit</code>（Number）：异步任务最大并发数。</p></li></ul></td></tr><tr><td><p>output.quotas\[].workspace\_limit</p></td><td><p>Object</p></td><td><p>业务空间级别限流。您可以为每个业务空间单独设置限流，但所有业务空间的总限流不能超过 <code>model\_limit</code>。值为 <code>null</code> 表示未设置业务空间级别限制。包含的字段与 <code>model\_limit</code> 相同。</p></td></tr></tbody></table>

## 请求示例 <span id="b1c2d3e4f5a300" />

<strong>示例一：查询所有模型的限额</strong>

```bash
curl "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models/limits?page_no=1&page_size=100" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header "Content-Type: application/json"
```

<strong>示例二：查询特定模型的限额</strong>

```bash
curl "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models/limits?model=qwen3.8-max" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header "Content-Type: application/json"
```

## 返回示例 <span id="b1c2d3e4f5a400" />

```json expandable
{
    "code": null,
    "message": null,
    "success": true,
    "output": {
        "total": 453,
        "page_no": 1,
        "page_size": 10,
        "quotas": [
            {
                "model": "qwen3.8-max",
                "workspace_id": "llm-8l75168qayuhsr3o",
                "model_limit": {
                    "request_limit": 500,
                    "request_limit_period": 1,
                    "usage_limit": 500000,
                    "usage_limit_field": "total_tokens",
                    "usage_limit_period": 6,
                    "async_user_queue_limit": null,
                    "async_user_concurrency_limit": null
                },
                "workspace_limit": null
            },
            {
                "model": "wan2.6-i2v-flash",
                "workspace_id": "llm-8l75168qayuhsr3o",
                "model_limit": {
                    "request_limit": 5,
                    "request_limit_period": 1,
                    "usage_limit": null,
                    "usage_limit_field": null,
                    "usage_limit_period": null,
                    "async_user_queue_limit": 500,
                    "async_user_concurrency_limit": 5
                },
                "workspace_limit": null
            },
            {
                "model": "qwen-image-3.0-pro",
                "workspace_id": "llm-8l75168qayuhsr3o",
                "model_limit": {
                    "request_limit": 2,
                    "request_limit_period": 60,
                    "usage_limit": 1000000,
                    "usage_limit_field": "total_tokens",
                    "usage_limit_period": 60,
                    "async_user_queue_limit": null,
                    "async_user_concurrency_limit": null
                },
                "workspace_limit": null
            }
        ]
    },
    "request_id": "2043b55f-f0d2-95ee-a449-234e1ee57042"
}
```

以上示例中：

- `qwen3.8-max`：每秒最多 500 次请求，每 6 秒最多使用 500,000 Token，未设置业务空间级别限制。
- `wan2.6-i2v-flash`：每秒最多 5 次请求，无用量限制，异步任务最大排队数 500、最大并发数 5。
- `qwen-image-3.0-pro`：每分钟最多 2 次请求，每分钟最多使用 1,000,000 Token，未设置业务空间级别限制。

## 错误码 <span id="b1c2d3e4f5a500" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
