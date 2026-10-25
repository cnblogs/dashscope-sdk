> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 更新模型限流

> 调用 POST /api/v1/models/limits 接口更新指定业务空间下模型的限流配额（QPM / TPM），支持"合并覆盖"与"删除限流"两种操作。

<meta property="og:title" content="更新模型限流" />

## 前提条件 <span id="uml0000a010" /> <span id="prerequisite-section-uml" />

已创建 API Key 并配置为环境变量 `DASHSCOPE_API_KEY`。配置方法请参见[配置API Key到环境变量](/zh/model-studio/get-api-key)。

## 请求说明 <span id="43f31e8630lzd" /> <span id="request-section-uml" />

- <strong>HTTP 方法</strong>：POST
- <strong>请求地址</strong>

  > 调用时请将 `{WorkspaceId}` 替换为您的业务空间 ID。

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30.01%" }} /><col style={{ width: "69.99%" }} /></colgroup><thead><tr><th><p><strong>地域</strong></p></th><th><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td><p>华北2（北京）</p></td><td><p><code>https\://\{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models/limits</code></p></td></tr></tbody></table>
- <strong>认证方式</strong>

  在请求 Header 中设置 `Authorization: Bearer {API_KEY}`。

## 请求参数 <span id="uml0000a100" /> <span id="request-params-section-uml" />

所有参数均通过请求 Body 传递。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26%" }} /><col style={{ width: "12%" }} /><col style={{ width: "8%" }} /><col style={{ width: "54%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>models</p></td><td><p>Array\[Object]</p></td><td><p>是</p></td><td><p>逐模型限流项列表。必填，数量需为 1\~200。示例：<code>\[\{"model":"qwen-plus","request\_limit":60}]</code></p></td></tr><tr><td><p>models\[].model</p></td><td><p>String</p></td><td><p>是</p></td><td><p>模型 ID，不能为空。如 <code>qwen-plus</code>、<code>qwen3.7-plus</code>。</p></td></tr><tr><td><p>models\[].operation\_type</p></td><td><p>String</p></td><td><p>否</p></td><td><p>操作类型。可选值：</p><ul><li><p><code>OVERLAY</code>：合并覆盖（只覆盖传入字段，未传字段回填当前值）。</p></li><li><p><code>DELETE</code>：删除限流（此时限流值字段需全部为空）。</p></li></ul><p>默认值：<code>OVERLAY</code>。</p></td></tr><tr><td><p>models\[].request\_limit</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>请求次数上限（QPM），非负。每分钟最大请求数，单位次/分钟。<code>operation\_type=DELETE</code> 时必须为空。默认值：null（不变更）。示例：<code>60</code></p></td></tr><tr><td><p>models\[].request\_limit\_period</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>请求限流时间周期，正整数，单位秒，如 <code>60</code>=1 分钟。默认值：null（不变更）。</p></td></tr><tr><td><p>models\[].usage\_limit</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>用量上限（TPM），非负。每分钟最大 Token 用量。<code>operation\_type=DELETE</code> 时必须为空。默认值：null（不变更）。示例：<code>100000</code></p></td></tr><tr><td><p>models\[].usage\_limit\_period</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>用量限流时间周期，正整数，单位秒，如 <code>60</code>=1 分钟。默认值：null（不变更）。</p></td></tr></tbody></table>

## 返回参数 <span id="uml0000a200" /> <span id="response-params-section-uml" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "12%" }} /><col style={{ width: "68%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求 ID。示例：<code>d5f5201f-ee7a-9e3d-8569-bc0eedec21f9</code></p></td></tr><tr><td><p>code</p></td><td><p>String</p></td><td><p>错误码，成功为 <code>null</code>。</p></td></tr><tr><td><p>message</p></td><td><p>String</p></td><td><p>错误信息，成功为 <code>null</code>。</p></td></tr><tr><td><p>success</p></td><td><p>Boolean</p></td><td><p>是否成功。示例：<code>true</code></p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>业务数据，本接口为 <code>null</code>。</p></td></tr></tbody></table>

## TPM 豁免场景 <span id="uml0000a600" /> <span id="tpm-exempt-section-uml" />

若需"只限制 RPM、豁免 TPM"，推荐两步法——先调用本接口对目标模型传 `{"model":"X","operation_type":"DELETE"}` 删除全部限流，再传 `{"model":"X","operation_type":"OVERLAY","request_limit":60,"request_limit_period":60}` 只设置 QPM。两步非原子，中间有短暂无限制窗口。

## 请求示例 <span id="uml0000a300" /> <span id="example-section-uml" />

<strong>示例一：设置 QPM=60、TPM=100000</strong>

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/models/limits" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json' \
    --data '{
        "models": [
            {
                "model": "qwen-plus",
                "operation_type": "OVERLAY",
                "request_limit": 60,
                "request_limit_period": 60,
                "usage_limit": 100000,
                "usage_limit_period": 60
            }
        ]
    }'
```

<strong>示例二：删除限流</strong>

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/models/limits" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json' \
    --data '{
        "models": [
            {"model": "qwen-plus", "operation_type": "DELETE"}
        ]
    }'
```

## 返回示例 <span id="uml0000a400" /> <span id="response-example-section-uml" />

<strong>成功示例</strong>

```json
{
    "code": null,
    "message": null,
    "success": true,
    "output": null,
    "request_id": "d5f5201f-ee7a-9e3d-8569-bc0eedec21f9"
}
```

<strong>失败示例</strong>

```json
{
    "code": "InvalidParameter",
    "message": "Cannot set TPM without QPM for model qwen-plus: no existing QPM limit, please set QPM first",
    "success": false,
    "output": null,
    "request_id": "d5f5201f-ee7a-9e3d-8569-bc0eedec21f9"
}
```

## 错误码 <span id="b1c2d3e4f5a500" /> <span id="error-section-uml" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
