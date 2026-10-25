> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 模型部署 API 参考

> 本文档以千问模型的部署为例进行说明，使用 API（HTTP）调用方式帮助您使用阿里云百炼提供的模型部署功能。

<meta property="og:title" content="模型部署 API 参考" />

<Tip>
  模型部署 API 的开放地域因部署方式而异——模型单元（MU）与 DTU 独占算力部署支持华北2（北京）；预置吞吐（PTU）等部署方式支持北京与新加坡地域。
</Tip>

## 前提条件 <span id="654cb090c6wkd" />

- 您已经阅读了[模型部署](/zh/model-studio/model-deployment-introduction)和[使用 API 进行模型部署](/zh/model-studio/model-deployment-quick-start)的相关内容，掌握了模型部署 API 的使用方法，并熟悉了在阿里云百炼平台上进行模型部署的基本步骤。
- 已配置百炼的 API-KEY， 请参考[获取与配置 API Key](/zh/model-studio/get-api-key)。
- DTU（DTU 独占算力）部署使用同一组部署接口，请求中以 `plan="dtu"` 区分部署形式。DTU 部署的请求需使用部署所在业务空间、地域对应的 `maas.aliyuncs.com` 业务空间域名（例如 `ws-vxxx.cn-beijing.maas.aliyuncs.com`）和该业务空间的 API Key，而不是 `dashscope.aliyuncs.com`。
- 请求域名因地域而异：北京地域使用 `dashscope.aliyuncs.com`，新加坡地域使用 `dashscope-intl.aliyuncs.com`；业务空间专属域名（`{WorkspaceId}.{region}.maas.aliyuncs.com`）各地域均可用。各地域完整域名信息请参见[各地域接入信息](/zh/model-studio/regions#h2_cc92e61c)。
- DTU 部署仅支持预付费（`charge_type="pre_paid"`），不支持后付费创建；续费与自动续费变更目前仅支持在控制台操作。DTU 接口不支持 `AssumedRoleUser` 角色身份。使用 DTU 前需在控制台开通 DTU 功能并申请资源额度，详见[DTU 独占算力部署](/zh/model-studio/dtu-model-deployment)。

## 获取可以部署的模型列表 <span id="dc35528058t0d" />

```bash
GET https://dashscope.aliyuncs.com/api/v1/deployments/models
```

### 请求示例 <span id="4d68f79558lqs" />

通过下面的命令可以查询支持部署的模型，推荐使用`version=v1.0`获取包含部署方案和模板信息的完整响应。

```bash
curl "https://dashscope.aliyuncs.com/api/v1/deployments/models?page_no=1&page_size=100&version=v1.0&model_source=base" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json'
```

查询用户微调模型：

```bash
curl "https://dashscope.aliyuncs.com/api/v1/deployments/models?page_no=1&page_size=100&version=v1.0&model_source=custom" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json'
```

查询 DTU 部署方案（传 `plan=dtu` 时仅返回 DTU 预付费模板，无需 `version` 参数；DTU 需使用业务空间域名）：

```bash
curl "https://ws-vxxx.cn-beijing.maas.aliyuncs.com/api/v1/deployments/models?plan=dtu&model_source=base&page_no=1&page_size=100" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="f37ec75058pia" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.98%" }} /><col style={{ width: "7.99%" }} /><col style={{ width: "12.03%" }} /><col style={{ width: "9.96%" }} /><col style={{ width: "54.04%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>page\_no</p></td><td><p>Number</p></td><td><p>query</p></td><td><p>否</p></td><td><p>页码，默认值为1。</p></td></tr><tr><td><p>page\_size</p></td><td><p>Number</p></td><td><p>query</p></td><td><p>否</p></td><td><p>页大小，默认为50，最大值为100，最小值为1。</p></td></tr><tr><td><p>model\_source</p></td><td><p>String</p></td><td><p>query</p></td><td><p>否</p></td><td><p>模型来源。<code>base</code>表示系统模型（默认），<code>custom</code>表示用户微调模型。</p></td></tr><tr><td><p>version</p></td><td><p>String</p></td><td><p>query</p></td><td><p>否</p></td><td><p>API 版本，推荐使用<code>v1.0</code>。使用<code>v1.0</code>时，响应中将包含完整的部署方案和模板信息。</p></td></tr><tr><td><p>plan</p></td><td><p>String</p></td><td><p>query</p></td><td><p>否</p></td><td><p>查询 DTU 部署方案时传<code>dtu</code>，仅返回 DTU 预付费模板（返回结构见下文 DTU 模板字段说明）。省略时保持原有模型目录行为，不返回 DTU 方案。</p></td></tr></tbody></table>

### 响应示例&参数 <span id="4d68f79858ncx" />

<Accordion title="点击此处查看">
  命令执行完成后，获得以下结果：

  ```json expandable
  {
      "request_id": "f7da015c-ea90-4d96-af89-2f8d7604026a",
      "output": {
          "page_no": 1,
          "page_size": 100,
          "total": 5,
          "models": [
              {
                  "model_name": "qwen3-8b",
                  "plans": [
                      {
                          "plan": "mu",
                          "templates": [
                              {
                                  "template_id": "MU1",
                                  "template_name": "单机部署-标准推理型",
                                  "template_type": "COUPLED",
                                  "template_version": "v1",
                                  "template_desc": "适用于标准推理场景",
                                  "roles": {
                                      "unified": {
                                          "model_unit_spec": "MU1",
                                          "capacity_unit_per_instance": 4
                                      }
                                  }
                              },
                              {
                                  "template_id": "MU1-PD",
                                  "template_name": "PD分离部署-标准推理型",
                                  "template_type": "SEPERATED",
                                  "template_version": "v1",
                                  "template_desc": "适用于PD分离推理场景",
                                  "roles": {
                                      "prefill": {
                                          "model_unit_spec": "MU1",
                                          "capacity_unit_per_instance": 4
                                      },
                                      "decode": {
                                          "model_unit_spec": "MU1",
                                          "capacity_unit_per_instance": 4
                                      }
                                  }
                              }
                          ]
                      },
                      {
                          "plan": "lora"
                      }
                  ]
              }
          ]
      }
  }
  ```

  #### 响应参数 <span id="488bb28058j01" />

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28.01%" }} /><col style={{ width: "14.99%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>models</p></td><td><p>Array</p></td><td><p>可部署模型列表。</p></td></tr><tr><td><p>models\[].model\_name</p></td><td><p>String</p></td><td><p>模型名称。</p></td></tr><tr><td><p>models\[].plans</p></td><td><p>Array</p></td><td><p>该模型支持的部署方案列表。使用<code>version=v1.0</code>时返回。</p></td></tr><tr><td><p>models\[].plans\[].plan</p></td><td><p>String</p></td><td><p>部署方案类型：<code>mu</code>（模型单元）、<code>cu</code>（算力单元）、<code>ptu\_v2</code>（预置吞吐量V2）、<code>lora</code>（LoRA共享部署）。</p></td></tr><tr><td><p>models\[].plans\[].templates</p></td><td><p>Array</p></td><td><p>部署模板列表（<code>plan=mu</code>时返回）。</p></td></tr><tr><td><p>page\_no</p></td><td><p>Number</p></td><td><p>查询页码。</p></td></tr><tr><td><p>page\_size</p></td><td><p>Number</p></td><td><p>查询页大小。</p></td></tr><tr><td><p>total</p></td><td><p>Long</p></td><td><p>满足查询条件的所有模型个数。</p></td></tr></tbody></table>

  #### 模板字段说明（templates） <span id="na10h1" />

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28.01%" }} /><col style={{ width: "14.99%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>template\_id</p></td><td><p>String</p></td><td><p>模板 ID，在<span>创建模型部署任务</span>时作为<code>template\_id</code>参数传入。</p></td></tr><tr><td><p>template\_name</p></td><td><p>String</p></td><td><p>模板显示名称。</p></td></tr><tr><td><p>template\_type</p></td><td><p>String</p></td><td><p>模板类型：<code>COUPLED</code>（非 PD 分离，使用<code>capacity</code>参数）、<code>SEPERATED</code>（PD 分离，使用<code>prefill\_capacity</code>和<code>decode\_capacity</code>参数）。</p></td></tr><tr><td><p>template\_version</p></td><td><p>String</p></td><td><p>模板版本。</p></td></tr><tr><td><p>template\_desc</p></td><td><p>String</p></td><td><p>模板描述。</p></td></tr><tr><td><p>roles</p></td><td><p>Object</p></td><td><p>节点角色配置。COUPLED 模式包含<code>unified</code>节点，SEPERATED 模式包含<code>prefill</code>和<code>decode</code>节点。</p></td></tr></tbody></table>

  #### roles 节点字段说明 <span id="na18h1" />

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28.01%" }} /><col style={{ width: "14.99%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>model\_unit\_spec</p></td><td><p>String</p></td><td><p>模型单元规格。</p></td></tr><tr><td><p>capacity\_unit\_per\_instance</p></td><td><p>Number</p></td><td><p>单实例容量单元数，即 base\_capacity。创建部署时<code>capacity</code>必须是该值的整数倍。</p></td></tr></tbody></table>

  #### DTU 模板字段说明（plan=dtu 时返回） <span id="na20h1" />

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28.01%" }} /><col style={{ width: "14.99%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>charge\_type</p></td><td><p>String</p></td><td><p>固定为<code>pre\_paid</code>（预付费）。</p></td></tr><tr><td><p>sku\_code</p></td><td><p>String</p></td><td><p>创建部署时使用的商品 SKU。</p></td></tr><tr><td><p>template\_code</p></td><td><p>String</p></td><td><p>创建部署时使用的部署模板，需与<code>sku\_code</code>为同一模板项。</p></td></tr><tr><td><p>input\_tpm\_quota</p></td><td><p>Object</p></td><td><p>输入 TPM 的可购买范围与步长，字段见下表。</p></td></tr><tr><td><p>output\_tpm\_quota</p></td><td><p>Object</p></td><td><p>输出 TPM 的可购买范围与步长，字段见下表。</p></td></tr><tr><td><p>display\_meta</p></td><td><p>Object</p></td><td><p>方案名称、描述、标签等展示信息，有值时返回。</p></td></tr><tr><td><p>standard\_workload</p></td><td><p>Object</p></td><td><p>模板提供的标准工况（TPM 单位与工况档位），有值时返回。</p></td></tr></tbody></table>

  `input_tpm_quota` 与 `output_tpm_quota` 分别表示输入、输出 TPM 的购买规则，容量单位为 tokens/min，包含以下字段：

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28.01%" }} /><col style={{ width: "14.99%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>init</p></td><td><p>Integer</p></td><td><p>该维度的最低起购 TPM，购买容量不得小于此值。</p></td></tr><tr><td><p>max</p></td><td><p>Integer</p></td><td><p>当前可购买的 TPM 上限。<strong>任一维度 max=0 表示当前没有可购买额度，请联系商务申请资源；0 不代表不限量。</strong></p></td></tr><tr><td><p>step</p></td><td><p>Integer</p></td><td><p>该维度的购买步长，购买的 TPM 必须是该步长的整数倍（不是在 init 上累加的偏移量）。</p></td></tr></tbody></table>

  创建或扩缩容 DTU 部署时，传入的 `dtu_capacity.input_tpm` 与 `dtu_capacity.output_tpm` 须同时满足：

  - 输入、输出 TPM 分别位于各自的 \[init, max] 范围内；
  - 输入 TPM 是 `input_tpm_quota.step` 的整数倍，输出 TPM 是 `output_tpm_quota.step` 的整数倍；
  - <strong>输入、输出的步长倍数必须相同</strong>：`input_tpm / input_tpm_quota.step = output_tpm / output_tpm_quota.step = n`（n 为正整数），不能独立选择不同倍数。

  例如，输入范围为 `init=10000, max=100000, step=10000`、输出范围为 `init=1000, max=10000, step=1000` 时：购买输入 30000、输出 3000（均为 3 倍，满足）；购买输入 30000、输出 2000（倍数 3 和 2，不满足）；购买输入 15000、输出 1500（非步长整数倍，不满足）。
</Accordion>

## 创建模型部署任务 <span id="13f7a7d05829h" />

```bash
POST https://dashscope.aliyuncs.com/api/v1/deployments
```

### 请求示例 <span id="13f81d0d58x09" />

<Tabs>
  <Tab title="DTU 独占算力部署">
    预付费，按输入/输出 TPM 购买容量（需使用业务空间域名；`sku_code`、`template_code` 及容量范围通过获取可以部署的模型列表 `plan=dtu` 查询）。DTU 为模型单元（MU）的继任方案，新购独占部署优先选择 DTU：

    ```bash
    curl "https://ws-vxxx.cn-beijing.maas.aliyuncs.com/api/v1/deployments" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
        "model_name": "your-supported-model",
        "display_name": "业务模型服务",
        "suffix": "production",
        "plan": "dtu",
        "sku_code": "your-prepaid-sku",
        "template_code": "your-template",
        "charge_type": "pre_paid",
        "pre_paid_info": {
            "pricing_cycle": "Month",
            "duration": 1,
            "auto_renewal": false
        },
        "dtu_capacity": {"input_tpm": 10000, "output_tpm": 10000}
    }'
    ```
  </Tab>

  <Tab title="模型单元部署（COUPLED 模式）">
    使用 `template_id` 指定 MU1 规格，`capacity` 为资源单元数量：

    ```bash
    curl "https://dashscope.aliyuncs.com/api/v1/deployments" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
        "model_name": "qwen3-8b",
        "plan": "mu",
        "template_id": "MU1",
        "capacity": 4
    }'
    ```
  </Tab>

  <Tab title="模型单元部署（PD 分离模式）">
    使用 `MU1-PD` 模板，分别指定 `prefill_capacity` 和 `decode_capacity`：

    ```bash
    curl "https://dashscope.aliyuncs.com/api/v1/deployments" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
        "model_name": "qwen3-8b",
        "plan": "mu",
        "template_id": "MU1-PD",
        "prefill_capacity": 4,
        "decode_capacity": 8
    }'
    ```
  </Tab>

  <Tab title="PTU v2 部署">
    按 Token 用量计费：

    ```bash
    curl "https://dashscope.aliyuncs.com/api/v1/deployments" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
        "model_name": "qwen3-8b",
        "plan": "ptu_v2"
    }'
    ```
  </Tab>

  <Tab title="LoRA 共享部署">
    按 Token 用量计费：

    ```bash
    curl "https://dashscope.aliyuncs.com/api/v1/deployments" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
        "model_name": "qwen3-8b-ft-202511132025-0260",
        "plan": "lora"
    }'
    ```
  </Tab>
</Tabs>

### 请求参数 <span id="13f7a7d4582to" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "12.03%" }} /><col style={{ width: "7.99%" }} /><col style={{ width: "9.96%" }} /><col style={{ width: "7.99%" }} /><col style={{ width: "62.03%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>model\_name</p></td><td><p>String</p></td><td><p>body</p></td><td><p>是</p></td><td><p>待部署的模型名称。</p></td></tr><tr><td><p>plan</p></td><td><p>String</p></td><td><p>body</p></td><td><p>是</p></td><td><p>部署方案：<code>dtu</code>（DTU 独占算力，仅预付费）、<code>mu</code>（模型单元）、<code>cu</code>（算力单元）、<code>ptu\_v2</code>（预置吞吐量V2）、<code>lora</code>（LoRA共享部署）。</p></td></tr><tr><td><p>template\_id</p></td><td><p>String</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>部署模板 ID，对应获取可以部署的模型列表接口返回的 <code>template\_id</code>。设置 <code>plan</code> 为 <code>mu</code> 时必须填写。推荐使用此参数指定模型单元规格。</p></td></tr><tr><td><p>deploy\_spec（旧）</p></td><td><p>String</p></td><td><p>body</p></td><td><p>否</p></td><td><p>模型单元规格，已废弃。</p></td></tr><tr><td><p>capacity</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>资源单元数量。COUPLED 模式（非 PD 分离）必填，必须是 <code>capacity\_unit\_per\_instance</code> 的整数倍。按 Token 用量计费（<code>plan</code> 为 <code>lora</code>）时该参数设置无效但必须填写。</p></td></tr><tr><td><p>prefill\_capacity</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>Prefill 容量。PD 分离模式（<code>template\_type</code> 为 <code>SEPERATED</code>）时必填。</p></td></tr><tr><td><p>decode\_capacity</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>Decode 容量。PD 分离模式（<code>template\_type</code> 为 <code>SEPERATED</code>）时必填。</p></td></tr><tr><td><p>name</p></td><td><p>String</p></td><td><p>body</p></td><td><p>否</p></td><td><p>服务显示名称。</p></td></tr><tr><td><p>suffix</p></td><td><p>String</p></td><td><p>body</p></td><td><p>否</p></td><td><p>模型部署后将生成新的模型名称，suffix 用于指定新模型名称的后缀，最大长度为8个字符且需全局唯一。每个模型在首次部署时可以不指定后缀，多次部署同一模型时必须设置。</p></td></tr><tr><td><p>charge\_type</p></td><td><p>String</p></td><td><p>body</p></td><td><p>否</p></td><td><p>付费方式：<code>post\_paid</code>（后付费，默认）、<code>pre\_paid</code>（预付费）。</p></td></tr><tr><td><p>pre\_paid\_info</p></td><td><p>Object</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>预付费信息，<code>charge\_type</code> 为 <code>pre\_paid</code> 时必填。包含字段：<code>auto\_renewal</code>（Boolean，是否自动续费）、<code>duration</code>（Number，购买时长/月）、<code>auto\_renewal\_duration</code>（Number，自动续费时长）。</p></td></tr><tr><td><p>pre\_paid\_info.auto\_renewal</p></td><td><p>Boolean</p></td><td><p>body</p></td><td><p>是</p></td><td><p>是否自动续费</p></td></tr><tr><td><p>pre\_paid\_info.duration</p></td><td><p>Integer</p></td><td><p>body</p></td><td><p>是</p></td><td><p>购买时长（月）</p></td></tr><tr><td><p>pre\_paid\_info.auto\_renewal\_duration</p></td><td><p>Integer</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>自动续费时长（autoRenewal=true时必填）</p></td></tr><tr><td><p>enable\_thinking</p></td><td><p>Boolean</p></td><td><p>body</p></td><td><p>否</p></td><td><p>是否启用思考模式，部分模型支持。</p></td></tr><tr><td><p>max\_context\_length</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>否</p></td><td><p>最大上下文长度，部分模型支持。</p></td></tr><tr><td><p>quantization\_precision</p></td><td><p>String</p></td><td><p>body</p></td><td><p>否</p></td><td><p>量化精度。</p></td></tr><tr><td><p>qpm\_limit</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>否</p></td><td><p>QPM 限流，每分钟请求数限制。仅 <code>plan</code> 为 <code>mu</code> 的部分模型支持。</p></td></tr><tr><td><p>tpm\_limit</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>否</p></td><td><p>TPM 限流，每分钟 Token 使用量限制。仅 <code>plan</code> 为 <code>mu</code> 的部分模型支持。</p></td></tr><tr><td><p>display\_name</p></td><td><p>String</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>服务展示名称，最多 50 个字符。<code>plan</code> 为 <code>dtu</code> 时使用（其他方案使用 <code>name</code>）。</p></td></tr><tr><td><p>sku\_code</p></td><td><p>String</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>当前模型可用的预付费 SKU，<code>plan</code> 为 <code>dtu</code> 时必填。</p></td></tr><tr><td><p>template\_code</p></td><td><p>String</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>与模型和 SKU 匹配的部署模板，<code>plan</code> 为 <code>dtu</code> 时必填。</p></td></tr><tr><td><p>dtu\_capacity</p></td><td><p>Object</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>DTU 购买容量，<code>plan</code> 为 <code>dtu</code> 时必填，须同时提供 <code>input\_tpm</code> 与 <code>output\_tpm</code>，规则见获取可以部署的模型列表的 TPM 购买范围与步长。</p></td></tr><tr><td><p>dtu\_capacity.input\_tpm</p></td><td><p>Integer</p></td><td><p>body</p></td><td><p>是</p></td><td><p>购买的输入 TPM（tokens/min），非负整数，最大值为 2147483647。</p></td></tr><tr><td><p>dtu\_capacity.output\_tpm</p></td><td><p>Integer</p></td><td><p>body</p></td><td><p>是</p></td><td><p>购买的输出 TPM（tokens/min），非负整数，最大值为 2147483647。</p></td></tr><tr><td><p>pre\_paid\_info.pricing\_cycle</p></td><td><p>String</p></td><td><p>body</p></td><td><p>是</p></td><td><p>计费周期，DTU 固定传 <code>Month</code>（省略或传其他值将返回 InvalidParameter）。</p></td></tr><tr><td><p>pre\_paid\_info.auto\_renewal\_cycle</p></td><td><p>String</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>自动续费周期，开启自动续费时必填，应符合商品规则。</p></td></tr></tbody></table>

<Note>
  `plan` 为 `dtu` 时的补充说明：`suffix` 最多 24 个字符，生成的完整部署标识不超过 100 个字符；不支持 `name`、`random_suffix`、`deploy_spec`、MU/PTU 容量字段、模型路由、限流或发布参数，限流请在部署就绪后通过修改限流接口设置；创建成功仅表示请求已受理（预付费订单处理中），需轮询详情至 `RUNNING`；续费与自动续费变更请在控制台操作。
</Note>

### 支持的模型 <span id="184f7297e8ekv" />

<Accordion title="模型单元 部署模式下，限流、推理加速等功能的 支持情况 。" defaultOpen>
  <Tabs>
    <Tab title="文本生成">
      <Tabs>
        <Tab title="千问">
          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型代码</strong></p></th><th><p><strong>模型单元规格</strong></p></th><th><p><strong>小时单价（元）</strong></p> <p><strong>最小计费：分钟</strong></p></th><th><p><strong>包月单价（元）</strong></p> <p><strong>最小计费：天</strong></p></th></tr></thead><tbody><tr><td rowSpan={4}><p>千问3.6-35B-A3B</p></td><td rowSpan={4}><p>qwen3.6-35b-a3b</p></td><td><p>MU1 x 8</p></td><td><p>¥432</p></td><td><p>¥208,944</p></td></tr><tr><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr><tr><td><p>MU3 x 8</p></td><td><p>¥1,096</p></td><td><p>¥527,752</p></td></tr><tr><td><p>MU9 x 1</p></td><td><p>¥51</p></td><td><p>¥24,600</p></td></tr><tr><td><p>千问3.6-27B</p></td><td><p>qwen3.6-27b</p></td><td><p>MU9 x 1</p></td><td><p>¥51</p></td><td><p>¥24,600</p></td></tr><tr><td rowSpan={2}><p>千问3.6-Flash-2026-04-16</p></td><td rowSpan={2}><p>qwen3.6-flash-2026-04-16</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td><p>MU3 x 8</p></td><td><p>¥1,096</p></td><td><p>¥527,752</p></td></tr><tr><td><p>千问3.6-Plus-2026-04-02</p></td><td><p>qwen3.6-plus-2026-04-02</p></td><td><p>MU1 x 8</p> <p>MU1 x 16（PD分离模式）</p></td><td><p>¥432</p> <p>PD分离模式：¥864</p></td><td><p>¥208,944</p> <p>PD分离模式：¥417,888</p></td></tr><tr><td colSpan={5}><p /></td></tr><tr><td rowSpan={2}><p>千问3.5-397B-A17B</p></td><td rowSpan={2}><p>qwen3.5-397b-a17b</p></td><td><p>MU3 x 8</p> <p>MU3 x 16（PD分离模式）</p></td><td><p>¥1,096</p> <p>PD分离模式：¥2,192</p></td><td><p>¥527,752</p> <p>PD分离模式：¥1,055,504</p></td></tr><tr><td><p>MU6 x 16</p></td><td><p>¥400</p></td><td><p>¥193,424</p></td></tr><tr><td rowSpan={2}><p>千问3.5-122B-A10B</p></td><td rowSpan={2}><p>qwen3.5-122b-a10b</p></td><td><p>MU1 x 4</p></td><td><p>¥216</p></td><td><p>¥104,472</p></td></tr><tr><td><p>MU6 x 16</p></td><td><p>¥400</p></td><td><p>¥193,424</p></td></tr><tr><td rowSpan={4}><p>千问3.5-35B-A3B</p></td><td rowSpan={4}><p>qwen3.5-35b-a3b</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr><tr><td><p>MU3 x 8</p></td><td><p>¥1,096</p></td><td><p>¥527,752</p></td></tr><tr><td><p>MU9 x 1</p></td><td><p>¥51</p></td><td><p>¥24,600</p></td></tr><tr><td rowSpan={4}><p>千问3.5-27B</p></td><td rowSpan={4}><p>qwen3.5-27b</p></td><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr><tr><td><p>MU3 x 8</p></td><td><p>¥1,096</p></td><td><p>¥527,752</p></td></tr><tr><td><p>MU8 x 1</p></td><td><p>¥47</p></td><td><p>¥22,400</p></td></tr><tr><td><p>MU9 x 1</p></td><td><p>¥51</p></td><td><p>¥24,600</p></td></tr><tr><td><p>千问3.5-9B</p></td><td><p>qwen3.5-9b</p></td><td><p>MU2 x 2</p></td><td><p>¥126</p></td><td><p>¥60,072</p></td></tr><tr><td><p>千问3.5-Flash-2026-02-23</p></td><td><p>qwen3.5-flash-2026-02-23</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td rowSpan={3}><p>千问3.5-Plus-2026-02-15</p></td><td rowSpan={3}><p>qwen3.5-plus-2026-02-15</p></td><td><p>MU1 x 8</p> <p>MU1 x 16（PD分离模式）</p></td><td><p>¥432</p> <p>PD分离模式：¥864</p></td><td><p>¥208,944</p> <p>PD分离模式：¥417,888</p></td></tr><tr><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr><tr><td><p>MU3 x 8</p> <p>MU3 x 16（PD分离模式）</p></td><td><p>¥1,096</p> <p>PD分离模式：¥2,192</p></td><td><p>¥527,752</p> <p>PD分离模式：¥1,055,504</p></td></tr><tr><td colSpan={5}><p /></td></tr><tr><td rowSpan={3}><p>千问3-235B-A22B-Instruct-2507</p></td><td rowSpan={3}><p>qwen3-235b-a22b-instruct-2507</p></td><td><p>MU1 x 4</p></td><td><p>¥216</p></td><td><p>¥104,472</p></td></tr><tr><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr><tr><td><p>MU3 x 8</p></td><td><p>¥1,096</p></td><td><p>¥527,752</p></td></tr><tr><td><p>千问3-32B</p></td><td><p>qwen3-32b</p></td><td><p>MU6 x 16</p></td><td><p>¥400</p></td><td><p>¥193,424</p></td></tr><tr><td><p>千问3-30B-A3B-Thinking-2507</p></td><td><p>qwen3-30b-a3b-thinking-2507</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td rowSpan={2}><p>千问3-8B</p></td><td rowSpan={2}><p>qwen3-8b</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td><p>MU2 x 2</p></td><td><p>¥126</p></td><td><p>¥60,072</p></td></tr><tr><td rowSpan={2}><p>千问3-4B</p></td><td rowSpan={2}><p>qwen3-4b</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td><p>MU5 x 1</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr><tr><td rowSpan={2}><p>千问3-Embedding-0.6B</p></td><td rowSpan={2}><p>qwen3-embedding-0.6b</p></td><td><p>MU5 x 1</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr><tr><td><p>MU6 x 1</p></td><td><p>¥25</p></td><td><p>¥12,089</p></td></tr><tr><td><p>千问3-MoE-Rerank-0.6B</p></td><td><p>qwen3-moe-rerank-0.6b</p></td><td><p>MU5 x 1</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr><tr><td rowSpan={2}><p>千问3-Rerank-0.6B</p></td><td rowSpan={2}><p>qwen3-rerank-0.6b</p></td><td><p>MU5 x 1</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr><tr><td><p>MU6 x 1</p></td><td><p>¥25</p></td><td><p>¥12,089</p></td></tr><tr><td rowSpan={2}><p>千问3-Max-2025-09-23</p></td><td rowSpan={2}><p>qwen3-max-2025-09-23</p></td><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr><tr><td><p>MU3 x 8</p></td><td><p>¥1,096</p></td><td><p>¥527,752</p></td></tr><tr><td><p>千问3-Rerank</p></td><td><p>qwen3-rerank</p></td><td><p>MU5 x 1</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr><tr><td colSpan={5}><p /></td></tr><tr><td><p>千问2.5-开源版-72B</p></td><td><p>qwen2.5-72b-instruct</p></td><td><p>MU1 x 8</p></td><td><p>¥432</p></td><td><p>¥208,944</p></td></tr><tr><td><p>千问2.5-开源版-14B</p></td><td><p>qwen2.5-14b-instruct</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td rowSpan={2}><p>千问2.5-开源版-7B</p></td><td rowSpan={2}><p>qwen2.5-7b-instruct</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td><p>MU5 x 1</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr><tr><td colSpan={5}><p /></td></tr><tr><td><p>千问-Plus-2025-07-28</p></td><td><p>qwen-plus-2025-07-28</p></td><td><p>MU1 x 4</p> <p>MU1 x 16（PD分离模式）</p></td><td><p>¥216</p> <p>PD分离模式：¥864</p></td><td><p>¥104,472</p> <p>PD分离模式：¥417,888</p></td></tr><tr><td><p>千问-Plus-2025-12-01</p></td><td><p>qwen-plus-2025-12-01</p></td><td><p>MU1 x 4</p></td><td><p>¥216</p></td><td><p>¥104,472</p></td></tr><tr><td><p>Qwen-Plus-Character-2025-11-06</p></td><td><p>qwen-plus-character-2025-11-06</p></td><td><p>MU1 x 4</p></td><td><p>¥216</p></td><td><p>¥104,472</p></td></tr></tbody></table>
        </Tab>

        <Tab title="GLM">
          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型代码</strong></p></th><th><p><strong>模型单元规格</strong></p></th><th><p><strong>小时单价（元）</strong></p> <p><strong>最小计费：分钟</strong></p></th><th><p><strong>包月单价（元）</strong></p> <p><strong>最小计费：天</strong></p></th></tr></thead><tbody><tr><td rowSpan={3}><p>GLM-5.1</p></td><td rowSpan={3}><p>glm-5.1</p></td><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr><tr><td><p>MU3 x 16（PD分离模式）</p></td><td><p>PD分离模式：¥2,192</p></td><td><p>PD分离模式：¥1,055,504</p></td></tr><tr><td><p>MU6 x 16</p></td><td><p>¥400</p></td><td><p>¥193,424</p></td></tr><tr><td><p>GLM-5</p></td><td><p>glm-5</p></td><td><p>MU3 x 16（PD分离模式）</p></td><td><p>PD分离模式：¥2,192</p></td><td><p>PD分离模式：¥1,055,504</p></td></tr><tr><td><p>GLM-4.7</p></td><td><p>glm-4.7</p></td><td><p>MU6 x 32（PD分离模式）</p></td><td><p>PD分离模式：¥800</p></td><td><p>PD分离模式：¥386,848</p></td></tr></tbody></table>
        </Tab>

        <Tab title="DeepSeek">
          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型代码</strong></p></th><th><p><strong>模型单元规格</strong></p></th><th><p><strong>小时单价（元）</strong></p> <p><strong>最小计费：分钟</strong></p></th><th><p><strong>包月单价（元）</strong></p> <p><strong>最小计费：天</strong></p></th></tr></thead><tbody><tr><td rowSpan={2}><p>DeepSeek-v4-Flash</p></td><td rowSpan={2}><p>deepseek-v4-flash</p></td><td><p>MU1 x 8</p></td><td><p>¥432</p></td><td><p>¥208,944</p></td></tr><tr><td><p>MU3 x 8</p></td><td><p>¥1,096</p></td><td><p>¥527,752</p></td></tr><tr><td><p>DeepSeek-v3.2</p></td><td><p>deepseek-v3.2</p></td><td><p>MU2 x 16（PD分离模式）</p></td><td><p>PD分离模式：¥1,008</p></td><td><p>PD分离模式：¥480,576</p></td></tr></tbody></table>
        </Tab>

        <Tab title="更多模型">
          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型代码</strong></p></th><th><p><strong>模型单元规格</strong></p></th><th><p><strong>小时单价（元）</strong></p> <p><strong>最小计费：分钟</strong></p></th><th><p><strong>包月单价（元）</strong></p> <p><strong>最小计费：天</strong></p></th></tr></thead><tbody><tr><td><p>Kimi-K2.5</p></td><td><p>kimi-k2.5</p></td><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr></tbody></table>
        </Tab>
      </Tabs>

      模型类型：

      - Instruct - 模型部署后以<strong>非思考模式</strong>进行推理。
      - Thinking - 模型部署后以思考模式进行推理。

      模型部署类型：

      - PD 分离模式 - <strong>降低首 Token 延迟、提高吞吐。</strong> 该部署模式部署的模型在进行模型推理时，将首 Token 计算（Prefill）和后续 Token 计算（Decode）两个计算阶段，拆到不同的计算节点执行。
    </Tab>

    <Tab title="多模态">
      <Tabs>
        <Tab title="千问VL">
          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型代码</strong></p></th><th><p><strong>模型单元规格</strong></p></th><th><p><strong>小时单价（元）</strong></p> <p><strong>最小计费：分钟</strong></p></th><th><p><strong>包月单价（元）</strong></p> <p><strong>最小计费：天</strong></p></th></tr></thead><tbody><tr><td rowSpan={3}><p>千问3-VL-235B-A22B-Thinking</p></td><td rowSpan={3}><p>qwen3-vl-235b-a22b-thinking</p></td><td><p>MU1 x 8</p></td><td><p>¥432</p></td><td><p>¥208,944</p></td></tr><tr><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr><tr><td><p>MU3 x 8</p></td><td><p>¥1,096</p></td><td><p>¥527,752</p></td></tr><tr><td rowSpan={2}><p>千问3-VL-32B-Instruct</p></td><td rowSpan={2}><p>qwen3-vl-32b-instruct</p></td><td><p>MU2 x 8</p></td><td><p>¥504</p></td><td><p>¥240,288</p></td></tr><tr><td><p>MU3 x 8</p></td><td><p>¥1,096</p></td><td><p>¥527,752</p></td></tr><tr><td rowSpan={2}><p>千问3-VL-8B-Instruct</p></td><td rowSpan={2}><p>qwen3-vl-8b-instruct</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td><p>MU5 x 1</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr><tr><td><p>千问3-VL-4B-Instruct</p></td><td><p>qwen3-vl-4b-instruct</p></td><td><p>MU1 x 2</p></td><td><p>¥108</p></td><td><p>¥52,236</p></td></tr><tr><td><p>千问3-VL-2B-Instruct</p></td><td><p>qwen3-vl-2b-instruct</p></td><td><p>MU5 x 1</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr><tr><td><p>千问3-VL-Embedding-2B</p></td><td><p>qwen3-vl-embedding-2b</p></td><td><p>MU5 x 1</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr><tr><td><p>千问3-VL-Flash-2025-10-15</p></td><td><p>qwen3-vl-flash-2025-10-15</p></td><td><p>MU1 x 4</p></td><td><p>¥216</p></td><td><p>¥104,472</p></td></tr><tr><td><p>千问3-VL-Plus-2025-09-23</p></td><td><p>qwen3-vl-plus-2025-09-23</p></td><td><p>MU1 x 4</p></td><td><p>¥216</p></td><td><p>¥104,472</p></td></tr><tr><td colSpan={5}><p /></td></tr><tr><td><p>千问VL-Max-2025-08-13</p></td><td><p>qwen-vl-max-2025-08-13</p></td><td><p>MU6 x 4</p></td><td><p>¥100</p></td><td><p>¥48,356</p></td></tr></tbody></table>
        </Tab>

        <Tab title="千问 Omni">
          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型代码</strong></p></th><th><p><strong>模型单元规格</strong></p></th><th><p><strong>小时单价（元）</strong></p> <p><strong>最小计费：分钟</strong></p></th><th><p><strong>包月单价（元）</strong></p> <p><strong>最小计费：天</strong></p></th></tr></thead><tbody><tr><td><p>千问3.5-Omni-Flash</p></td><td><p>qwen3.5-omni-flash</p></td><td><p>MU9 x 1</p></td><td><p>¥51</p></td><td><p>¥24,600</p></td></tr></tbody></table>
        </Tab>
      </Tabs>

      模型类型：

      - Instruct - 模型部署后以<strong>非思考模式</strong>进行推理。
      - Thinking - 模型部署后以思考模式进行推理。
      - Instruct/Thinking - 可在模型部署时<strong>选择是否开启思考模式</strong>。
    </Tab>

    <Tab title="语音合成">
      <strong>CosyVoice</strong>

      <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "19.99%" }} /><col style={{ width: "19.99%" }} /><col style={{ width: "19.99%" }} /><col style={{ width: "19.99%" }} /><col style={{ width: "20.04%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型代码</strong></p></th><th><p><strong>模型单元规格</strong></p></th><th><p><strong>小时单价（元）</strong></p></th><th><p><strong>包月单价（元）</strong></p></th></tr></thead><tbody><tr><td><p>cosyvoice-v3-flash</p></td><td><p>cosyvoice-v3-flash</p></td><td><p>MU5</p></td><td><p>¥21</p></td><td><p>¥10,139</p></td></tr></tbody></table>
    </Tab>
  </Tabs>
</Accordion>

### 响应示例&参数 <span id="28762b1a7bbjn" />

<Accordion title="点击此处查看">
  命令执行完成后，获得以下结果：

  ```json expandable
  {
      "request_id": "f2ae64f7-83cc-410c-bc0b-840443f7eb86",
      "output": {
          "deployed_model": "qwen3-8b-20260115-abcd",
          "name": "我的模型服务",
          "model_name": "qwen3-8b",
          "base_model": "qwen3-8b",
          "status": "PENDING",
          "capacity": 4,
          "ready_capacity": 0,
          "base_capacity": 4,
          "workspace_id": "llm-8v53e*******",
          "charge_type": "post_paid",
          "plan": "mu",
          "model_unit_spec": "MU1",
          "creator": "16542902******",
          "modifier": "16542902******",
          "qpm_limit": 100000000,
          "tpm_limit": 3600000,
          "mu_capacity": {
              "unified": {
                  "base_capacity": 4,
                  "model_unit_spec": "MU1",
                  "ready_capacity": 0,
                  "capacity": 4
              }
          },
          "gmt_create": "2026-01-15T10:00:00",
          "gmt_modified": "2026-01-15T10:00:00"
      }
  }
  ```

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.98%" }} /><col style={{ width: "23.97%" }} /><col style={{ width: "53.05%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的 ID。</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>本次部署任务的详细信息。</p></td></tr><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>新模型的唯一标识，在发起模型调用请求时需要在 SDK 参数传入。</p></td></tr><tr><td><p>name</p></td><td><p>String</p></td><td><p>服务显示名称。</p></td></tr><tr><td><p>model\_name</p></td><td><p>String</p></td><td><p>部署任务使用的模型名称。</p></td></tr><tr><td><p>base\_model</p></td><td><p>String</p></td><td><p>部署任务使用的模型对应的基础模型 ID。</p></td></tr><tr><td><p>status</p></td><td><p>String</p></td><td><p>部署任务的状态。</p><ul><li><p>PENDING：正在创建部署任务。</p></li><li><p>DEPLOYING：部署中。</p></li><li><p>WAIT\_PRE\_PAID\_BILLING\_TO\_DEPLOYING：创建处于预付费订单处理阶段（DTU）。</p></li><li><p>WAIT\_PRE\_PAID\_BILLING\_TO\_SCALING：扩缩容处于预付费订单处理阶段（DTU）。</p></li><li><p>RUNNING：部署任务正在运行，可正常处理请求。</p></li><li><p>SCALING：扩缩容中。</p></li><li><p>UPDATING：正在更新部署任务。</p></li><li><p>UPDATING\_PAUSED：更新已暂停。</p></li><li><p>SUSPENDING：暂停中（欠费）。</p></li><li><p>SUSPENDED：已暂停（欠费）。</p></li><li><p>STOPPING：停止中。</p></li><li><p>STOPPED：已停止，不再计费。</p></li><li><p>STARTING：启动中（从 STOPPED/FAILED 状态重新启动）。</p></li><li><p>FAILED：部署任务创建或更新失败。</p></li><li><p>DELETING：正在删除部署任务。</p></li></ul></td></tr><tr><td><p>capacity</p></td><td><p>Number</p></td><td><p>部署任务的目标资源单元数量。</p></td></tr><tr><td><p>ready\_capacity</p></td><td><p>Number</p></td><td><p>已就绪并可立即处理请求的资源单元数量。</p></td></tr><tr><td><p>base\_capacity</p></td><td><p>Number</p></td><td><p>单实例基础容量。</p></td></tr><tr><td><p>workspace\_id</p></td><td><p>String</p></td><td><p>部署任务所属的业务空间 ID。</p></td></tr><tr><td><p>charge\_type</p></td><td><p>String</p></td><td><p><code>post\_paid</code>：后付费；<code>pre\_paid</code>：预付费。</p></td></tr><tr><td><p>plan</p></td><td><p>String</p></td><td><p>部署任务的计费模式。</p></td></tr><tr><td><p>model\_unit\_spec</p></td><td><p>String</p></td><td><p>模型单元规格。</p></td></tr><tr><td><p>gmt\_create</p></td><td><p>String</p></td><td><p>创建部署任务的时间。</p></td></tr><tr><td><p>gmt\_modified</p></td><td><p>String</p></td><td><p>修改部署任务的时间。</p></td></tr><tr><td><p>creator</p></td><td><p>String</p></td><td><p>该部署任务创建人 UID。</p></td></tr><tr><td><p>modifier</p></td><td><p>String</p></td><td><p>对该部署任务进行最后一次操作的账号 UID。</p></td></tr><tr><td><p>fail\_reason</p></td><td><p>String</p></td><td><p>失败原因（仅 FAILED 状态返回）。</p></td></tr><tr><td><p>enable\_thinking</p></td><td><p>Boolean</p></td><td><p>是否开启思考模式，部分模型支持。</p></td></tr><tr><td><p>max\_context\_length</p></td><td><p>Number</p></td><td><p>最大上下文长度限制。</p></td></tr><tr><td><p>qpm\_limit</p></td><td><p>Number</p></td><td><p>QPM 限流，每分钟请求数。</p></td></tr><tr><td><p>tpm\_limit</p></td><td><p>Number</p></td><td><p>TPM 限流，每分钟 Token 使用量。</p></td></tr><tr><td><p>quantization\_precision</p></td><td><p>String</p></td><td><p>量化精度。</p></td></tr><tr><td colSpan={3}><p>仅 DTU（dtu）部署方式响应</p></td></tr><tr><td><p>dtu\_capacity</p></td><td><p>Object</p></td><td><p>部署记录中的购买容量；扩缩容期间可能仍为原容量，不能据此认为目标已生效。</p></td></tr><tr><td><p>dtu\_capacity.input\_tpm</p></td><td><p>Integer</p></td><td><p>购买的输入 Token 容量（tokens/min）。</p></td></tr><tr><td><p>dtu\_capacity.output\_tpm</p></td><td><p>Integer</p></td><td><p>购买的输出 Token 容量（tokens/min）。</p></td></tr><tr><td><p>pre\_paid\_info.pricing\_cycle</p></td><td><p>String</p></td><td><p>返回的购买计费周期（如 <code>Month</code>）；可能为 <code>null</code>，表示本响应未提供该值，不代表下单失败或购买时长为 0。</p></td></tr><tr><td><p>pre\_paid\_info.auto\_renewal\_cycle</p></td><td><p>String</p></td><td><p>返回的自动续费周期，有值时返回。</p></td></tr><tr><td><p>pre\_paid\_instance\_id</p></td><td><p>String</p></td><td><p>预付费实例标识，有值时返回。不是部署标识，不能替代 <code>deployed\_model</code>。</p></td></tr><tr><td colSpan={3}><p>仅模型单元（mu）部署方式响应</p></td></tr><tr><td><p>mu\_capacity</p></td><td><p>Object</p></td><td><p>MU 容量详情。COUPLED 模式包含 <code>unified</code> 节点，SEPERATED 模式包含 <code>prefill</code> 和 <code>decode</code> 节点。每个节点包含：base\_capacity、model\_unit\_spec、ready\_capacity、capacity。</p></td></tr><tr><td><p>pre\_paid\_info</p></td><td><p>Object</p></td><td><p>预付费信息。</p></td></tr><tr><td><p>pre\_paid\_info.auto\_renewal</p></td><td><p>Boolean</p></td><td><p>是否自动续费</p></td></tr><tr><td><p>pre\_paid\_info.duration</p></td><td><p>Integer</p></td><td><p>购买时长（月）</p></td></tr><tr><td><p>pre\_paid\_info.auto\_renewal\_duration</p></td><td><p>Integer</p></td><td><p>自动续费时长</p></td></tr><tr><td><p>pre\_paid\_instance\_id</p></td><td><p>String</p></td><td><p>预付费实例 ID。</p></td></tr><tr><td><p>pre\_paid\_gmt\_expired</p></td><td><p>String</p></td><td><p>预付费过期时间。</p></td></tr><tr><td colSpan={3}><p>仅预置吞吐量V2（ptu\_v2）部署方式响应</p></td></tr><tr><td><p>ptu\_capacity</p></td><td><p>Object</p></td><td><p>PTU V2 容量信息。包含字段：</p><ul><li><p><code>input\_tpm\_quota</code>（Number，输入 TPM 配额）</p></li><li><p><code>output\_tpm\_quota</code>（Number，输出 TPM 配额）</p></li></ul></td></tr><tr><td><p>pre\_paid\_info</p></td><td><p>Object</p></td><td><p>预付费信息。</p></td></tr><tr><td><p>pre\_paid\_info.auto\_renewal</p></td><td><p>Boolean</p></td><td><p>是否自动续费</p></td></tr><tr><td><p>pre\_paid\_info.duration</p></td><td><p>Integer</p></td><td><p>购买时长（月）</p></td></tr><tr><td><p>pre\_paid\_info.auto\_renewal\_duration</p></td><td><p>Integer</p></td><td><p>自动续费时长</p></td></tr><tr><td><p>pre\_paid\_instance\_id</p></td><td><p>String</p></td><td><p>预付费实例 ID。</p></td></tr><tr><td><p>pre\_paid\_gmt\_expired</p></td><td><p>String</p></td><td><p>预付费过期时间。</p></td></tr></tbody></table>

  DTU 部署的 `qpm_limit`、`tpm_limit` 为网关限流设置（64 位整数，可能超过修改接口的请求上限），默认 `qpm_limit=600000`、`tpm_limit=output_tpm × 100`，不等于购买的输入/输出 TPM，也不表示实际推理吞吐保证。`gmt_create`、`gmt_modified` 字符串不包含时区偏移，请勿自行当作 UTC 时间。
</Accordion>

## 查询模型部署任务 <span id="be373c3fb0bx2" />

```bash
GET https://dashscope.aliyuncs.com/api/v1/deployments/{deployed_model}
```

### 请求示例 <span id="f6eee4dbadjhp" />

```bash
curl "https://dashscope.aliyuncs.com/api/v1/deployments/qwen3-8b-20260115-abcd" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="e3998e5959dl5" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.98%" }} /><col style={{ width: "7.99%" }} /><col style={{ width: "12.03%" }} /><col style={{ width: "9.96%" }} /><col style={{ width: "54.04%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>部署模型的唯一标识，可通过创建模型部署任务或列举模型部署任务获取。</p></td></tr></tbody></table>

### 响应示例&参数 <span id="7c4cfdd7f6jlh" />

<Accordion title="点击此处查看">
  命令执行完成后，获得以下结果：

  ```json expandable
  {
      "request_id": "d8b8c47b-ae32-4c41-b9f4-9a1e2b3c4d5e",
      "output": {
          "deployed_model": "qwen3-8b-20260115-abcd",
          "name": "我的模型服务",
          "model_name": "qwen3-8b",
          "base_model": "qwen3-8b",
          "status": "RUNNING",
          "capacity": 4,
          "ready_capacity": 4,
          "base_capacity": 4,
          "workspace_id": "llm-8v53e*******",
          "charge_type": "post_paid",
          "plan": "mu",
          "model_unit_spec": "MU1",
          "creator": "16542902******",
          "modifier": "16542902******",
          "qpm_limit": 100000000,
          "tpm_limit": 3600000,
          "mu_capacity": {
              "unified": {
                  "base_capacity": 4,
                  "model_unit_spec": "MU1",
                  "ready_capacity": 4,
                  "capacity": 4
              }
          },
          "gmt_create": "2026-01-15T10:00:00",
          "gmt_modified": "2026-01-15T10:30:00"
      }
  }
  ```

  #### 响应参数 <span id="be0baabef41ed" />

  响应参数与创建模型部署任务的响应参数一致，请参考创建模型部署任务的响应参数说明。
</Accordion>

## 列举模型部署任务 <span id="14574383a2j2f" />

```bash
GET https://dashscope.aliyuncs.com/api/v1/deployments
```

### 请求示例 <span id="4594b88224x2o" />

```bash
curl "https://dashscope.aliyuncs.com/api/v1/deployments?page_no=1&page_size=10" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="d43648b210xk1" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.98%" }} /><col style={{ width: "7.99%" }} /><col style={{ width: "12.03%" }} /><col style={{ width: "9.96%" }} /><col style={{ width: "54.04%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>page\_no</p></td><td><p>Number</p></td><td><p>query</p></td><td><p>否</p></td><td><p>页码，默认值为1。</p></td></tr><tr><td><p>page\_size</p></td><td><p>Number</p></td><td><p>query</p></td><td><p>否</p></td><td><p>页大小，默认为10，最大值为100。</p></td></tr><tr><td><p>plan</p></td><td><p>String</p></td><td><p>query</p></td><td><p>否</p></td><td><p>传<code>dtu</code>查询 DTU 部署；DTU 部署不支持状态过滤。</p></td></tr></tbody></table>

### 响应示例&参数 <span id="fd5f1061e9wqi" />

<Accordion title="点击此处查看">
  命令执行完成后，获得以下结果：

  ```json expandable
  {
      "request_id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
      "output": {
          "page_no": 1,
          "page_size": 10,
          "total": 2,
          "deployments": [
              {
                  "deployed_model": "qwen3-8b-20260115-abcd",
                  "name": "我的模型服务",
                  "model_name": "qwen3-8b",
                  "base_model": "qwen3-8b",
                  "status": "RUNNING",
                  "capacity": 4,
                  "ready_capacity": 4,
                  "base_capacity": 4,
                  "workspace_id": "llm-8v53e*******",
                  "charge_type": "post_paid",
                  "plan": "mu",
                  "model_unit_spec": "MU1",
                  "creator": "16542902******",
                  "modifier": "16542902******",
                  "qpm_limit": 100000000,
                  "tpm_limit": 3600000,
                  "mu_capacity": {
                      "unified": {
                          "base_capacity": 4,
                          "model_unit_spec": "MU1",
                          "ready_capacity": 4,
                          "capacity": 4
                      }
                  },
                  "gmt_create": "2026-01-15T10:00:00",
                  "gmt_modified": "2026-01-15T10:30:00"
              }
          ]
      }
  }
  ```

  #### 响应参数 <span id="66f59fe166mp8" />

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.02%" }} /><col style={{ width: "12.03%" }} /><col style={{ width: "67.95%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>page\_no</p></td><td><p>Number</p></td><td><p>查询页码。</p></td></tr><tr><td><p>page\_size</p></td><td><p>Number</p></td><td><p>查询页大小。</p></td></tr><tr><td><p>total</p></td><td><p>Long</p></td><td><p>满足查询条件的所有部署任务总数。</p></td></tr><tr><td><p>deployments</p></td><td><p>Array</p></td><td><p>部署任务列表。每个元素的字段与创建模型部署任务的<span>响应参数</span>一致，请参考创建模型部署任务的响应参数说明。</p></td></tr></tbody></table>
</Accordion>

## 更新模型部署任务 <span id="e14811ebe1tvg" />

<Tabs>
  <Tab title="扩缩容">
    <Note>
      更新模型部署任务即对部署服务进行扩缩容操作。仅当服务状态为 RUNNING 或 SCALING 时可调用，其他状态调用将返回 OperationDenied 错误。DTU 部署通过本接口传入 `dtu_capacity` 扩缩容：输入、输出 TPM 须同增同减（范围与组合限制与创建相同），成功仅表示请求已受理（状态先变为 `WAIT_PRE_PAID_BILLING_TO_SCALING`），需轮询详情至 `RUNNING` 且输入、输出容量均达到目标值。
    </Note>

    ```bash
    PUT https://dashscope.aliyuncs.com/api/v1/deployments/{deployed_model}/scale
    ```

    ### 请求示例 <span id="84a6cd6709az5" />

    COUPLED 模式扩缩容：

    ```bash
    curl -X PUT "https://dashscope.aliyuncs.com/api/v1/deployments/qwen3-8b-20260115-abcd/scale" \
        --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
        --header 'Content-Type: application/json' \
        --data '{
        "capacity": 8
    }'
    ```

    PD 分离模式扩缩容：

    ```bash
    curl -X PUT "https://dashscope.aliyuncs.com/api/v1/deployments/qwen3-8b-20260115-abcd/scale" \
        --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
        --header 'Content-Type: application/json' \
        --data '{
        "prefill_capacity": 4,
        "decode_capacity": 8
    }'
    ```

    DTU 部署扩缩容（需使用业务空间域名，请求体可省略 `plan`）：

    ```bash
    curl -X PUT "https://ws-vxxx.cn-beijing.maas.aliyuncs.com/api/v1/deployments/your-deployment-id/scale" \
        --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
        --header 'Content-Type: application/json' \
        --data '{
        "dtu_capacity": {"input_tpm": 20000, "output_tpm": 20000}
    }'
    ```

    ### 请求参数 <span id="87d0d7c0c3zv1" />

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.74%" }} /><col style={{ width: "11.13%" }} /><col style={{ width: "13.73%" }} /><col style={{ width: "10.77%" }} /><col style={{ width: "43.63%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>传参方式</p></th><th><p>必选</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>部署模型的唯一标识。</p></td></tr><tr><td><p>capacity</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>扩缩容的目标资源单元数量。COUPLED 模式（非 PD 分离）时必填，必须是 <code>base\_capacity</code> 的整数倍。</p></td></tr><tr><td><p>prefill\_capacity</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>Prefill 目标容量。PD 分离模式时必填。</p></td></tr><tr><td><p>decode\_capacity</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>Decode 目标容量。PD 分离模式时必填。</p></td></tr><tr><td><p>ptu\_capacity</p></td><td><p>Object</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>PTU V2 容量信息。包含字段：</p><ul><li><p><code>input\_tpm\_quota</code>（Number，输入 TPM 配额）</p></li><li><p><code>output\_tpm\_quota</code>（Number，输出 TPM 配额）</p></li></ul></td></tr><tr><td><p>dtu\_capacity</p></td><td><p>Object</p></td><td><p>body</p></td><td><p>条件必填</p></td><td><p>DTU 目标容量，<code>plan</code> 为 <code>dtu</code> 时必填，须同时提供 <code>input\_tpm</code> 与 <code>output\_tpm</code>（均必填）；范围与组合限制与创建时相同，输入、输出需同增同减。本接口仅修改容量，不支持同时修改模型、SKU、模板、购买周期、自动续费或限流。</p></td></tr></tbody></table>

    ### 响应示例&参数 <span id="f448b3a19f69u" />

    <Accordion title="点击此处查看">
      命令执行完成后，获得以下结果：

      ```json expandable
      {
          "request_id": "e5f6a7b8-c9d0-1234-5678-abcdef012345",
          "output": {
              "deployed_model": "qwen3-8b-20260115-abcd",
              "name": "我的模型服务",
              "model_name": "qwen3-8b",
              "base_model": "qwen3-8b",
              "status": "SCALING",
              "capacity": 8,
              "ready_capacity": 4,
              "base_capacity": 4,
              "workspace_id": "llm-8v53e*******",
              "charge_type": "post_paid",
              "plan": "mu",
              "model_unit_spec": "MU1",
              "creator": "16542902******",
              "modifier": "16542902******",
              "qpm_limit": 100000000,
              "tpm_limit": 3600000,
              "mu_capacity": {
                  "unified": {
                      "base_capacity": 4,
                      "model_unit_spec": "MU1",
                      "ready_capacity": 4,
                      "capacity": 8
                  }
              },
              "gmt_create": "2026-01-15T10:00:00",
              "gmt_modified": "2026-01-15T11:00:00"
          }
      }
      ```

      #### 响应参数 <span id="64d367c8f3fgv" />

      响应参数与创建模型部署任务的响应参数一致，请参考创建模型部署任务的响应参数说明。
    </Accordion>
  </Tab>

  <Tab title="修改 DTU 部署的限流">
    <Note>
      仅 DTU（dtu）部署支持本接口。部署须处于空闲的 RUNNING 状态；请求必须同时提供 `qpm_limit` 与 `tpm_limit` 两个正整数，范围 1-2147483647；`rpm_limit` 可作为 `qpm_limit` 的别名，请只选择一个名称。限流值不适用购买容量的步长及输入/输出相同倍数规则。
    </Note>

    ```bash
    PUT https://ws-vxxx.cn-beijing.maas.aliyuncs.com/api/v1/deployments/{deployed_model}/config
    ```

    ### 请求示例 <span id="dtu-config-req-h3" />

    ```bash
    curl -X PUT "https://ws-vxxx.cn-beijing.maas.aliyuncs.com/api/v1/deployments/your-deployment-id/config" \
        --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
        --header 'Content-Type: application/json' \
        --data '{
        "qpm_limit": 600,
        "tpm_limit": 100000
    }'
    ```

    成功时返回当前部署详情（响应参数与创建模型部署任务一致），顶层 message 为 `DTU rate limit configuration updated.`。本接口只修改限流阈值，不改变购买的输入、输出 TPM，也不支持同时更新模型或扩缩容。
  </Tab>

  <Tab title="更新 DTU 部署的模型">
    <Note>
      仅 DTU（dtu）部署支持本接口，且仅支持将微调模型更新为另一个微调模型：目标模型与当前模型的基础模型名称及版本必须相同。部署须处于空闲的 RUNNING 状态，且模板支持模型更新；相同模型、并发变更或其他不满足条件的请求会报错。
    </Note>

    ```bash
    PUT https://ws-vxxx.cn-beijing.maas.aliyuncs.com/api/v1/deployments/{deployed_model}/update
    ```

    ### 请求示例 <span id="dtu-update-req-h3" />

    ```bash
    curl -X PUT "https://ws-vxxx.cn-beijing.maas.aliyuncs.com/api/v1/deployments/your-deployment-id/update" \
        --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
        --header 'Content-Type: application/json' \
        --data '{
        "new_model_name": "your-new-finetuned-model"
    }'
    ```

    ### 请求参数 <span id="dtu-update-reqparam-h3" />

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.74%" }} /><col style={{ width: "11.13%" }} /><col style={{ width: "13.73%" }} /><col style={{ width: "10.77%" }} /><col style={{ width: "43.63%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>传参方式</p></th><th><p>必选</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>部署模型的唯一标识。</p></td></tr><tr><td><p>new\_model\_name</p></td><td><p>String</p></td><td><p>body</p></td><td><p>是</p></td><td><p>目标微调模型名称，必须是当前业务空间内可见且有权限使用的模型。</p></td></tr></tbody></table>

    <Warning>
      更新按模板允许的最小批次策略连续发布，不进行首批暂停验收。更新期间可服务容量可能下降，新旧模型可能同时提供服务，可能出现延迟增加、限流或请求失败；最小批次发布不保证无损或零停机。请在发起前确认目标模型可用，并选择可接受这些影响的时间窗口。
    </Warning>

    本接口只更新模型，不支持修改限流、容量、SKU、模板、购买信息、发布比例或暂停参数。成功响应表示请求已受理（顶层 message 为 `DTU model update request accepted. The model is published continuously without pausing. Poll deployment details to confirm completion.`），需轮询详情，确认 `status` 为 `RUNNING` 且 `model_name` 已变为目标模型；仅恢复 `RUNNING` 不足以证明更新成功——更新失败并回退时可能仍使用旧模型。
  </Tab>
</Tabs>

## 停止部署的模型 <span id="b9b44525c2xha" />

<Note>
  仅模型单元（mu）部署方式支持停止服务。仅当服务状态为 RUNNING 或 SCALING 时可调用。cu、ptu\_v2、lora、dtu 方式调用此接口将返回 UnsupportedOperation 错误。停止后服务不再计费，可通过API 详情 API 重新启动。DTU 部署不支持 API 启停。
</Note>

```bash
POST https://dashscope.aliyuncs.com/api/v1/deployments/{deployed_model}/stop
```

### 请求示例 <span id="557f02aa779ls" />

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/deployments/qwen3-8b-20260115-abcd/stop" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="0470e7a0b3crx" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.98%" }} /><col style={{ width: "7.99%" }} /><col style={{ width: "12.03%" }} /><col style={{ width: "9.96%" }} /><col style={{ width: "54.04%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>部署模型的唯一标识。</p></td></tr></tbody></table>

### 响应示例&参数 <span id="ba259639072pq" />

<Accordion title="点击此处查看">
  命令执行完成后，获得以下结果：

  ```json expandable
  {
      "request_id": "f1a2b3c4-d5e6-7890-abcd-ef1234567890",
      "output": {
          "deployed_model": "qwen3-8b-20260115-abcd",
          "name": "我的模型服务",
          "model_name": "qwen3-8b",
          "base_model": "qwen3-8b",
          "status": "STOPPING",
          "capacity": 4,
          "ready_capacity": 4,
          "base_capacity": 4,
          "workspace_id": "llm-8v53e*******",
          "charge_type": "post_paid",
          "plan": "mu",
          "model_unit_spec": "MU1",
          "creator": "16542902******",
          "modifier": "16542902******",
          "qpm_limit": 100000000,
          "tpm_limit": 3600000,
          "mu_capacity": {
              "unified": {
                  "base_capacity": 4,
                  "model_unit_spec": "MU1",
                  "ready_capacity": 4,
                  "capacity": 4
              }
          },
          "gmt_create": "2026-01-15T10:00:00",
          "gmt_modified": "2026-01-15T12:00:00"
      }
  }
  ```

  #### 响应参数 <span id="5aeb1bec25dpt" />

  响应参数与创建模型部署任务的响应参数一致，请参考创建模型部署任务的响应参数说明。
</Accordion>

## 重新启动部署的模型 <span id="fe31838d7fevi" />

<Note>
  DTU（dtu）部署暂不支持通过本接口启动服务。
</Note>

```bash
POST https://dashscope.aliyuncs.com/api/v1/deployments/{deployed_model}/start
```

### 请求示例 <span id="578af53278lpw" />

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/deployments/qwen3-8b-20260115-abcd/start" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="5f86b188550uz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.98%" }} /><col style={{ width: "7.99%" }} /><col style={{ width: "12.03%" }} /><col style={{ width: "9.96%" }} /><col style={{ width: "54.04%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>部署模型的唯一标识，可通过创建模型部署任务或列举模型部署任务获取。</p></td></tr></tbody></table>

### 响应示例&参数 <span id="2eba7d3b00hcp" />

<Accordion title="点击此处查看">
  命令执行完成后，获得以下结果：

  > 启动后状态为 `STARTING`，模型可调用后变为 `RUNNING`

  ```json expandable
  {
    "request_id": "xxx-xxx-xxx",
    "output": {
      "deployed_model": "qwen-7b-chat-ft-20240101-abcd",
      "model_name": "qwen-7b-chat-ft",
      "base_model": "qwen-7b-chat",
      "status": "STARTING",
      "capacity": 1,
      "ready_capacity": 0,
      "base_capacity": 1,
      "plan": "mu",
      "model_unit_spec": "MU5",
      "workspace_id": "workspace-xxx",
      "charge_type": "post_paid",
      "creator": "user123",
      "modifier": "user123",
      "qpm_limit": 100000000,
      "tpm_limit": 3600000,
      "mu_capacity": {
        "unified": {
          "base_capacity": 1,
          "model_unit_spec": "",
          "ready_capacity": 0,
          "capacity": 1
        }
      },
      "gmt_create": "2024-01-01T10:00:00",
      "gmt_modified": "2024-01-01T13:00:00"
    }
  }
  ```

  #### 响应参数 <span id="0ffb9c955772y" />

  响应参数与创建模型部署任务的响应参数一致，请参考创建模型部署任务的响应参数说明。
</Accordion>

## 删除模型部署任务 <span id="a526c00058g7g" />

<Note>
  仅当服务状态为 STOPPED 或 FAILED 时可调用删除。RUNNING 状态的服务需先调用API 详情接口。DTU（dtu）部署暂不支持通过本接口删除，请在控制台退订，详见[DTU 独占算力部署](/zh/model-studio/dtu-model-deployment)。
</Note>

```http
DELETE https://dashscope.aliyuncs.com/api/v1/deployments/{deployed_model}
```

### 请求示例 <span id="ng01h32" />

```bash
curl -X DELETE "https://dashscope.aliyuncs.com/api/v1/deployments/qwen3-8b-20260115-abcd" \
    --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="ng01h33" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.29%" }} /><col style={{ width: "9.96%" }} /><col style={{ width: "14.36%" }} /><col style={{ width: "11.94%" }} /><col style={{ width: "43.45%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>部署模型的唯一标识。</p></td></tr></tbody></table>

### 响应示例&参数 <span id="276c2c8778l63" />

<Accordion title="点击此处查看">
  命令执行完成后，获得以下结果：

  ```json
  {
      "request_id": "g1h2i3j4-k5l6-7890-mnop-qr1234567890",
      "output": {
          "deployed_model": "qwen3-8b-20260115-abcd",
          "name": "我的模型服务",
          "model_name": "qwen3-8b",
          "base_model": "qwen3-8b",
          "status": "DELETING",
          "capacity": 4,
          "ready_capacity": 0,
          "base_capacity": 4,
          "workspace_id": "llm-8v53e*******",
          "charge_type": "post_paid",
          "plan": "mu",
          "model_unit_spec": "MU1",
          "creator": "16542902******",
          "modifier": "16542902******",
          "gmt_create": "2026-01-15T10:00:00",
          "gmt_modified": "2026-01-15T14:00:00"
      }
  }
  ```

  #### 响应参数 <span id="52b85ccfd1fir" />

  响应参数与创建模型部署任务的响应参数一致，请参考创建模型部署任务的响应参数说明。
</Accordion>

## 异常响应 <span id="ee7c8dc058j9j" />

```json
{
    "request_id": "6fec0a3f-a1df-4f89-8c8f-33cb0db9f5b0",
    "code": "InvalidParameter",
    "message": "capacity is required when template_type is COUPLED"
}
```

### 响应参数 <span id="nh02h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "32.41%" }} /><col style={{ width: "21.45%" }} /><col style={{ width: "46.14%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统生成 ID。</p></td></tr><tr><td><p>code</p></td><td><p>String</p></td><td><p>错误码，标识错误类型。</p></td></tr><tr><td><p>message</p></td><td><p>String</p></td><td><p>错误详情，用于问题排查。</p></td></tr></tbody></table>

### 常见错误 <span id="nh03h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.04%" }} /><col style={{ width: "74.96%" }} /></colgroup><thead><tr><th><p><strong>错误原因</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p><code>InvalidParameter</code></p></td><td><p>参数错误，如缺少必填参数、参数值不合法、template\_id 和 deploy\_spec 同时传入等。</p></td></tr><tr><td><p><code>AccessDenied</code></p></td><td><p>权限不足或资源不存在。</p></td></tr><tr><td><p><code>OperationDenied</code></p></td><td><p>当前状态不允许执行该操作，如在非 RUNNING 状态下执行扩缩容，或在非 STOPPED/FAILED 状态下删除。</p></td></tr><tr><td><p><code>UnsupportedOperation</code></p></td><td><p>当前部署方案不支持该操作，如 cu 方式不支持停止服务。</p></td></tr><tr><td><p><code>StockLimit</code></p></td><td><p>资源库存不足，无法完成扩容或部署。</p></td></tr><tr><td><p><code>InternalError</code></p></td><td><p>内部错误。</p></td></tr><tr><td><p><code>NotFound</code></p></td><td><p>指定的部署任务不存在。</p></td></tr><tr><td><p><code>Conflict</code></p></td><td><p>操作冲突，如服务正在进行其他操作。</p></td></tr><tr><td><p><code>QuotaExceeded</code></p></td><td><p>配额不足（DTU 预付费购买常见），请检查可用配额或联系服务支持。</p></td></tr><tr><td><p><code>InsufficientResource</code></p></td><td><p>资源不足，无法完成部署或扩容，可调整容量或联系服务支持。</p></td></tr><tr><td><p><code>ServiceUnavailable</code></p></td><td><p>服务暂时不可用，请退避后重试。</p></td></tr></tbody></table>
