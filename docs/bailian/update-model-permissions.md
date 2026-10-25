> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 更新模型授权

> 调用 POST /api/v1/models/permissions 接口更新指定业务空间下模型的授权（推理 / 微调 / 部署），支持逐模型授权与"一键授权"两种模式。

<meta property="og:title" content="更新模型授权" />

## 前提条件 <span id="ump0000a010" /> <span id="prerequisite-section-ump" />

已创建 API Key 并配置为环境变量 `DASHSCOPE_API_KEY`。配置方法请参见[配置API Key到环境变量](/zh/model-studio/get-api-key)。

## 请求说明 <span id="43f31e8630lzd" /> <span id="request-section-ump" />

- <strong>HTTP 方法</strong>：POST
- <strong>请求地址</strong>

  > 调用时请将 `{WorkspaceId}` 替换为您的业务空间 ID。

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th><p><strong>地域</strong></p></th><th><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td><p>华北2（北京）</p></td><td><p><code>https\://\{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models/permissions</code></p></td></tr></tbody></table>
- <strong>认证方式</strong>

  在请求 Header 中设置 `Authorization: Bearer {API_KEY}`。

## 请求参数 <span id="ump0000a100" /> <span id="request-params-section-ump" />

所有参数均通过请求 Body 传递。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22%" }} /><col style={{ width: "12%" }} /><col style={{ width: "12%" }} /><col style={{ width: "54%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>models</p></td><td><p>Array\[Object]</p></td><td><p>条件必填</p></td><td><p>逐模型授权项列表。当 <code>access\_all\_entities</code> 为 <code>OPEN</code>/<code>CLOSE</code> 时可空；为 <code>KEEP</code> 或不传时必填，且数量需为 1\~20。示例：<code>\[\{"model":"qwen-plus","inference":true}]</code></p></td></tr><tr><td><p>models\[].model</p></td><td><p>String</p></td><td><p>是</p></td><td><p>模型 ID，不能为空。如 <code>qwen-plus</code>、<code>qwen-max</code>。</p></td></tr><tr><td><p>models\[].inference</p></td><td><p>Boolean</p></td><td><p>否</p></td><td><p>模型调用权限。<code>true</code>=授权推理，<code>false</code>=取消授权，不传=保持现状。默认值：null（不变更）。</p></td></tr><tr><td><p>models\[].finetune</p></td><td><p>Boolean</p></td><td><p>否</p></td><td><p>模型训练权限。<code>true</code>=授权微调，<code>false</code>=取消，不传=保持现状。默认值：null（不变更）。</p></td></tr><tr><td><p>models\[].deploy</p></td><td><p>Boolean</p></td><td><p>否</p></td><td><p>模型部署权限。<code>true</code>=授权部署，<code>false</code>=取消，不传=保持现状。默认值：null（不变更）。</p></td></tr><tr><td><p>access\_all\_entities</p></td><td><p>String</p></td><td><p>否</p></td><td><p>一键授权开关。可选值：</p><ul><li><p><code>OPEN</code>：一键授权当前空间全部推理模型（含后续新增）。</p></li><li><p><code>CLOSE</code>：取消一键授权并清空历史推理授权。</p></li><li><p><code>KEEP</code>：保持原逐模型授权行为（缺省）。</p></li></ul><p>传 <code>OPEN</code>/<code>CLOSE</code> 时 <code>models</code> 可不填。默认值：<code>KEEP</code>。示例：<code>OPEN</code></p></td></tr></tbody></table>

## 返回参数 <span id="ump0000a200" /> <span id="response-params-section-ump" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "12%" }} /><col style={{ width: "68%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求 ID。示例：<code>d5f5201f-ee7a-9e3d-8569-bc0eedec21f9</code></p></td></tr><tr><td><p>code</p></td><td><p>String</p></td><td><p>错误码，成功为 <code>null</code>。</p></td></tr><tr><td><p>message</p></td><td><p>String</p></td><td><p>错误信息，成功为 <code>null</code>。</p></td></tr><tr><td><p>success</p></td><td><p>Boolean</p></td><td><p>是否成功。示例：<code>true</code></p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>业务数据，本接口为 <code>null</code>。</p></td></tr></tbody></table>

## 请求示例 <span id="ump0000a300" /> <span id="example-section-ump" />

<strong>示例一：逐模型授权</strong>

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/models/permissions" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json' \
    --data '{
        "models": [
            {"model": "qwen-plus", "inference": true, "fine_tune": true, "deploy": false},
            {"model": "qwen-max", "inference": true}
        ]
    }'
```

<strong>示例二：一键授权</strong>

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/models/permissions" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json' \
    --data '{"access_all_entities": "OPEN"}'
```

## 返回示例 <span id="ump0000a400" /> <span id="response-example-section-ump" />

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
    "message": "Model qwen-xxx is not authorized for Inference",
    "success": false,
    "output": null,
    "request_id": "d5f5201f-ee7a-9e3d-8569-bc0eedec21f9"
}
```

## 错误码 <span id="b1c2d3e4f5a500" /> <span id="error-section-ump" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
