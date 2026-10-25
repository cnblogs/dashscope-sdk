> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 文本生成-部署模型

> 将训练好的文本模型发布为在线 API 服务。

<meta property="og:title" content="文本生成-部署模型" />

## 适用范围 <span id="txt-dep-prereq-title" />

- <strong>适用地域</strong>：模型部署 API 的开放地域因部署方式而异——模型单元（MU）与 DTU 独占算力部署支持<strong>华北2（北京）</strong>；预置吞吐（PTU）等部署方式支持<strong>北京与新加坡</strong>地域。如您使用的地域未开放对应部署方式，请通过该地域的百炼控制台完成模型部署操作。
- <strong>开通账号权限</strong>：若使用[阿里云子账号](/zh/model-studio/permission-management-overview)（[RAM用户](https://help.aliyun.com/zh/ram/user-guide/overview-of-ram-users)），需要为子账号授予模型调用、训练和部署[权限](/zh/model-studio/use-workspace)。
- <strong>配置环境变量</strong>：已成功[获取 API Key](/zh/model-studio/get-api-key)，并[配置到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- <strong>阅读部署文档</strong>：建议先阅读[专属部署概述](/zh/model-studio/model-deployment-introduction)和[使用 API 创建模型部署任务](/zh/model-studio/model-deployment-quick-start)，了解模型部署的使用方法和基本步骤。

## 部署模型 <span id="txt-dep-main-h2" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://dashscope.aliyuncs.com/api/v1/deployments`
  </Tab>

  <Tab title="新加坡">
    `POST https://dashscope-intl.aliyuncs.com/api/v1/deployments`
  </Tab>
</Tabs>

> 请求域名因地域而异：北京地域使用 `dashscope.aliyuncs.com`，新加坡地域使用 `dashscope-intl.aliyuncs.com`；业务空间专属域名（`{WorkspaceId}.{region}.maas.aliyuncs.com`）各地域均可用。各地域完整域名信息请参见[各地域接入信息](/zh/model-studio/regions#h2_cc92e61c)。

> Windows CMD 请将`$DASHSCOPE_API_KEY`替换为`%DASHSCOPE_API_KEY%`，PowerShell请替换为 `$env:DASHSCOPE_API_KEY`

### 请求参数 <span id="txt-dep-req-header-h4" />

##### 请求头（Headers） <span id="txt-dep-req-headers-h5" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "12%" }} /><col style={{ width: "12%" }} /><col style={{ width: "56%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Content-Type</p></td><td><p>String</p></td><td><p>是</p></td><td><p>固定值：<code>application/json</code>。</p></td></tr><tr><td><p>Authorization</p></td><td><p>String</p></td><td><p>是</p></td><td><p>API Key 鉴权，格式为 <code>Bearer sk-xxxx</code>。</p></td></tr></tbody></table>

##### 请求体（Request Body） <span id="txt-dep-req-body-h5" />

请求体参数因部署方式（plan）而异，各计费方式的完整请求体参数与部署示例如下：

<Tabs>
  <Tab title="按DTU独占算力计费">
    DTU 独占算力部署为预付费（按月）计费，按输入/输出 TPM 购买容量，独占 GPU 资源，是模型单元（MU）的继任方案，新购独占部署优先选择。使用前需在控制台开通 DTU 功能并申请资源额度，详见[DTU 独占算力部署](/zh/model-studio/dtu-model-deployment)。

    > DTU 部署的 API 请求需使用部署所在业务空间的 `maas.aliyuncs.com` 域名（例如 `ws-vxxx.cn-beijing.maas.aliyuncs.com`）和该业务空间的 API Key；`sku_code`、`template_code` 与容量范围通过可部署模型列表接口 `plan=dtu` 查询。完整参数、扩缩容、修改限流与更新模型等接口详见[模型部署 API 参考](/zh/model-studio/model-deployment-api-reference)。

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "12%" }} /><col style={{ width: "12%" }} /><col style={{ width: "56%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>model\_name</p></td><td><p>String</p></td><td><p>是</p></td><td><p>待部署的模型名称，对应[我的模型](https://bailian.console.aliyun.com/model/custom)中的模型 ID。也可通过[创建训练任务](/zh/model-studio/create-fine-tuning-job-api)接口的输出获取。</p></td></tr><tr><td><p>plan</p></td><td><p>String</p></td><td><p>是</p></td><td><p>部署方案，决定计费方式：<code>dtu</code>（DTU 独占算力）、<code>mu</code>（模型单元）、<code>cu</code>（算力单元）、<code>ptu\_v2</code>（预置吞吐量 V2）、<code>lora</code>（LoRA 共享部署，按 Token 用量计费）。</p></td></tr><tr><td><p>display\_name</p></td><td><p>String</p></td><td><p>是</p></td><td><p>服务展示名称，最多 50 个字符（其他部署方式使用 <code>name</code>）。</p></td></tr><tr><td><p>suffix</p></td><td><p>String</p></td><td><p>否</p></td><td><p>部署标识后缀，最多 24 个字符；生成的完整部署标识不超过 100 个字符。省略时自动生成。</p></td></tr><tr><td><p>sku\_code</p></td><td><p>String</p></td><td><p>是</p></td><td><p>当前模型可用的预付费 SKU，通过可部署模型列表接口 <code>plan=dtu</code> 查询。</p></td></tr><tr><td><p>template\_code</p></td><td><p>String</p></td><td><p>是</p></td><td><p>与模型和 SKU 匹配的部署模板，与 <code>sku\_code</code> 同为查询所得。</p></td></tr><tr><td><p>charge\_type</p></td><td><p>String</p></td><td><p>是</p></td><td><p>付费方式，DTU 固定传 <code>pre\_paid</code>（预付费）。</p></td></tr><tr><td><p>dtu\_capacity</p></td><td><p>Object</p></td><td><p>是</p></td><td><p>DTU 购买容量，须同时提供 <code>input\_tpm</code> 与 <code>output\_tpm</code>，购买范围与步长规则见[模型部署 API 参考](/zh/model-studio/model-deployment-api-reference)。</p></td></tr><tr><td><p>dtu\_capacity.input\_tpm</p></td><td><p>Integer</p></td><td><p>是</p></td><td><p>购买的输入 TPM（tokens/min），非负整数，最大值为 2147483647，须为查询所得步长的整数倍。</p></td></tr><tr><td><p>dtu\_capacity.output\_tpm</p></td><td><p>Integer</p></td><td><p>是</p></td><td><p>购买的输出 TPM（tokens/min），非负整数，最大值为 2147483647，与输入 TPM 同倍数。</p></td></tr><tr><td><p>pre\_paid\_info</p></td><td><p>Object</p></td><td><p>是</p></td><td><p>预付费购买及自动续费设置。包含字段：<code>pricing\_cycle</code>（固定传 <code>Month</code>）、<code>duration</code>（购买时长/月）、<code>auto\_renewal</code>（是否自动续费）、<code>auto\_renewal\_duration</code>（开启自动续费时的续费时长/月）。</p></td></tr></tbody></table>

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

  <Tab title="按模型单元计费">
    选择按模型单元计费，计费模式为按模型单元的使用时长收费，适用于模型调优后的大规模推理业务，资源专属，性能和成本灵活可调。

    > 执行以下部署命令后，即便您还没有调用模型，模型部署服务仍将在部署成功后开始计费。建议您先确认服务计费规则，再执行部署命令。

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "12%" }} /><col style={{ width: "12%" }} /><col style={{ width: "56%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>model\_name</p></td><td><p>String</p></td><td><p>是</p></td><td><p>待部署的模型名称，对应[我的模型](https://bailian.console.aliyun.com/model/custom)中的模型 ID。也可通过[创建训练任务](/zh/model-studio/create-fine-tuning-job-api)接口的输出获取。</p></td></tr><tr><td><p>plan</p></td><td><p>String</p></td><td><p>是</p></td><td><p>部署方案，决定计费方式：<code>dtu</code>（DTU 独占算力）、<code>mu</code>（模型单元）、<code>cu</code>（算力单元）、<code>ptu\_v2</code>（预置吞吐量 V2）、<code>lora</code>（LoRA 共享部署，按 Token 用量计费）。</p></td></tr><tr><td><p>name</p></td><td><p>String</p></td><td><p>否</p></td><td><p>模型的控制台显示名称。如果不传该参数，将自动使用 <code>model\_name</code> 的值作为部署名称。</p></td></tr><tr><td><p>suffix</p></td><td><p>String</p></td><td><p>否</p></td><td><p>模型部署后将生成新的模型名称，suffix 用于指定新模型名称的后缀，最大长度为 8 个字符且需全局唯一。每个模型在首次部署时可以不指定后缀，多次部署同一模型时必须设置。</p></td></tr><tr><td><p>template\_id</p></td><td><p>String</p></td><td><p>条件必填</p></td><td><p>部署模板 ID，对应可部署模型列表接口返回的 <code>template\_id</code>。<code>plan</code> 为 <code>mu</code> 时必须填写，推荐使用此参数指定模型单元规格（如 <code>MU1</code>）。</p></td></tr><tr><td><p>deploy\_spec（旧）</p></td><td><p>String</p></td><td><p>否</p></td><td><p>模型单元规格，已废弃，请改用 <code>template\_id</code>。</p></td></tr><tr><td><p>capacity</p></td><td><p>Integer</p></td><td><p>条件必填</p></td><td><p>部署使用的资源单元数量，需为 <code>base\_capacity</code> 的整数倍。不同规格的取值约束不同，例如 <code>MU2</code> 必须为 8 的倍数，<code>MU5</code> 可填 1。</p></td></tr><tr><td><p>charge\_type</p></td><td><p>String</p></td><td><p>否</p></td><td><p>付费方式：<code>post\_paid</code>（后付费，默认）、<code>pre\_paid</code>（预付费）。</p></td></tr><tr><td><p>enable\_thinking</p></td><td><p>Boolean</p></td><td><p>否</p></td><td><p>是否启用思考模式。部分模型支持，可设置为 <code>true</code> 或 <code>false</code>。</p></td></tr><tr><td><p>max\_context\_length</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>最大上下文长度。部分模型支持。样例：<code>"max\_context\_length": 131072</code>。</p></td></tr><tr><td><p>rpm\_limit</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>每分钟请求数（requests per minute）限流。部分模型支持。</p></td></tr><tr><td><p>tpm\_limit</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>每分钟 Token 使用量（tokens per minute）限流。部分模型支持。</p></td></tr></tbody></table>

    ```bash
    curl "https://dashscope.aliyuncs.com/api/v1/deployments" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
        "name": "my_qwen_plus",
        "model_name": "qwen-plus-2025-12-01",
        "plan": "mu",
        "template_id": "MU1",
        "enable_thinking": true,
        "capacity": 4,
        "charge_type": "post_paid",
        "max_context_length": 10000,
        "rpm_limit": 500,
        "tpm_limit": 1000
    }'
    ```
  </Tab>

  <Tab title="按Token用量计费">
    选择按 Token 用量计费，适用于高性价比诉求且对并发和延迟要求不高的场景。该模式价格优势最高，吞吐/并发和生成速度均由平台预置，用户不可调。

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "12%" }} /><col style={{ width: "12%" }} /><col style={{ width: "56%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>model\_name</p></td><td><p>String</p></td><td><p>是</p></td><td><p>待部署的模型名称，对应[我的模型](https://bailian.console.aliyun.com/model/custom)中的模型 ID。也可通过[创建训练任务](/zh/model-studio/create-fine-tuning-job-api)接口的输出获取。</p></td></tr><tr><td><p>plan</p></td><td><p>String</p></td><td><p>是</p></td><td><p>部署方案，决定计费方式：<code>dtu</code>（DTU 独占算力）、<code>mu</code>（模型单元）、<code>cu</code>（算力单元）、<code>ptu\_v2</code>（预置吞吐量 V2）、<code>lora</code>（LoRA 共享部署，按 Token 用量计费）。</p></td></tr><tr><td><p>name</p></td><td><p>String</p></td><td><p>否</p></td><td><p>模型的控制台显示名称。如果不传该参数，将自动使用 <code>model\_name</code> 的值作为部署名称。</p></td></tr><tr><td><p>suffix</p></td><td><p>String</p></td><td><p>否</p></td><td><p>模型部署后将生成新的模型名称，suffix 用于指定新模型名称的后缀，最大长度为 8 个字符且需全局唯一。</p></td></tr><tr><td><p>capacity</p></td><td><p>Integer</p></td><td><p>条件必填</p></td><td><p>按 Token 用量计费时该参数设置无效但必须填写，固定传 1。</p></td></tr></tbody></table>

    ```bash
    curl "https://dashscope.aliyuncs.com/api/v1/deployments" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
        "model_name": "qwen3-8b-ft-202511132025-0260",
        "plan": "lora",
        "capacity": 1,
        "name": "qwen3-8b-ft"
    }'
    ```
  </Tab>

  <Tab title="按预置吞吐计费">
    按预置吞吐计费模式按预置吞吐的使用时长收费，适用于追求稳定吞吐保障和高并发低延迟、且流量可预估的场景。该模式下，吞吐/并发和生成速度均为平台预置，用户不可调。

    > 执行以下部署命令后，即便您还没有调用模型，模型部署服务仍将在部署成功后开始计费。建议您先确认服务计费规则，再执行部署命令。

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "12%" }} /><col style={{ width: "12%" }} /><col style={{ width: "56%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>model\_name</p></td><td><p>String</p></td><td><p>是</p></td><td><p>待部署的模型名称，对应[我的模型](https://bailian.console.aliyun.com/model/custom)中的模型 ID。也可通过[创建训练任务](/zh/model-studio/create-fine-tuning-job-api)接口的输出获取。</p></td></tr><tr><td><p>plan</p></td><td><p>String</p></td><td><p>是</p></td><td><p>部署方案，决定计费方式：<code>dtu</code>（DTU 独占算力）、<code>mu</code>（模型单元）、<code>cu</code>（算力单元）、<code>ptu\_v2</code>（预置吞吐量 V2）、<code>lora</code>（LoRA 共享部署，按 Token 用量计费）。</p></td></tr><tr><td><p>name</p></td><td><p>String</p></td><td><p>否</p></td><td><p>模型的控制台显示名称。如果不传该参数，将自动使用 <code>model\_name</code> 的值作为部署名称。</p></td></tr><tr><td><p>suffix</p></td><td><p>String</p></td><td><p>否</p></td><td><p>模型部署后将生成新的模型名称，suffix 用于指定新模型名称的后缀，最大长度为 8 个字符且需全局唯一。</p></td></tr><tr><td><p>ptu\_capacity</p></td><td><p>Object</p></td><td><p>否</p></td><td><p>预置吞吐量配置，包含 <code>input\_tpm</code>（每分钟最大输入 Token 量）、<code>output\_tpm</code>（每分钟最大输出 Token 量），部分模型另支持 <code>thinking\_output\_tpm</code>（预置思考最大输出 Token 量）。如果不填写该参数，将默认按照 <code>10,000 input\_tpm</code> 和 <code>1,000 output\_tpm</code> 进行设置。</p></td></tr></tbody></table>

    ```bash
    curl "https://dashscope.aliyuncs.com/api/v1/deployments" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
        "name": "my_qwen_flash",
        "model_name": "qwen-flash-2025-07-28",
        "plan": "ptu_v2",
        "ptu_capacity": {
            "input_tpm": 10000,
            "output_tpm": 1000
        }
    }'
    ```
  </Tab>
</Tabs>

## 部署排障与性能调优 <span id="txt-dep-tune-h2" />

### max-num-seqs 参数不可配置 <span id="txt-dep-tune-seqs-h3" />

百炼模型部署在<strong>按预置吞吐计费</strong>和<strong>按模型单元计费</strong>两种计费方式下，吞吐、并发和生成速度均为平台预置，不支持直接调整 vLLM 内部的 `max-num-seqs` 等推理引擎参数，本接口的请求参数中也不存在该参数。

<strong>按模型单元计费</strong>方式下，可通过选择<strong>模型单元类型</strong>（对应请求参数 `template_id`）和调整<strong>部署副本数</strong>（对应请求参数 `capacity`）间接控制吞吐能力。

### 部署模板与资源隔离 <span id="txt-dep-tune-template-h3" />

当前部署模板仅支持单机部署（<strong>单机部署-增强型通用推理</strong>），不支持多卡实例隔离。如需资源隔离，请使用<strong>按模型单元计费</strong>方式，该方式下算力资源为业务专属。

### 限流错误处理 <span id="txt-dep-tune-throttle-h3" />

当并发请求超出限流阈值时，接口返回 HTTP `429`，错误码为 `Throttling.RateQuota`，错误信息为 `Requests rate limit exceeded, please try again later`。处理方式：

- <strong>按预置吞吐计费</strong>：调整 `ptu_capacity` 中的 `input_tpm` 和 `output_tpm`（控制台对应 <strong>Input kTPM</strong> 和 <strong>Output kTPM</strong>），或降低请求频率。该方式下溢出策略可选<strong>自动溢出</strong>（切换为按量付费）或<strong>仅使用 PTU 容量</strong>（超出容量的请求直接返回 `429`）。
- <strong>按模型单元计费</strong>：调整 `rpm_limit` 和 `tpm_limit`，或降低请求频率。

### 上下文长度调优 <span id="txt-dep-tune-ctx-h3" />

<strong>按模型单元计费</strong>方式下可通过请求参数 `max_context_length` 配置<strong>最长上下文</strong>，取值范围 1～262144（实际上限取决于所部署模型的能力）。该参数限制单次请求的上下文规模，从而限制单请求的内存占用，可降低 OOM 风险。

处理大量图片任务时，请结合图片尺寸和单请求图片数量设置该参数。

### 响应参数 <span id="txt-dep-resp-header-h4" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24%" }} /><col style={{ width: "12%" }} /><col style={{ width: "64%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求的唯一标识符。</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>任务详情。</p></td></tr><tr><td><p>output.deployed\_model</p></td><td><p>String</p></td><td><p>部署模型的唯一标识。用于查询模型部署状态和调用模型。</p></td></tr><tr><td><p>output.name</p></td><td><p>String</p></td><td><p>部署的显示名称。</p></td></tr><tr><td><p>output.model\_name</p></td><td><p>String</p></td><td><p>模型标识名。</p></td></tr><tr><td><p>output.base\_model</p></td><td><p>String</p></td><td><p>使用的基准模型。</p></td></tr><tr><td><p>output.status</p></td><td><p>String</p></td><td><p>部署状态：<code>PENDING</code>（正在创建）、<code>DEPLOYING</code>（部署中）、<code>WAIT\_PRE\_PAID\_BILLING\_TO\_DEPLOYING</code>（创建处于预付费订单处理阶段，DTU）、<code>WAIT\_PRE\_PAID\_BILLING\_TO\_SCALING</code>（扩缩容处于预付费订单处理阶段，DTU）、<code>UPDATING</code>（正在更新）、<code>RUNNING</code>（运行中，可正常处理请求）、<code>SCALING</code>（扩缩容中）、<code>STOPPED</code>（已停止，不计费）、<code>DELETING</code>（删除中）、<code>FAILED</code>（创建或更新失败）。</p></td></tr><tr><td><p>output.workspace\_id</p></td><td><p>String</p></td><td><p>API Key 所属业务空间 ID。请参见[获取 Workspace ID](/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h)。</p></td></tr><tr><td><p>output.charge\_type</p></td><td><p>String</p></td><td><p>付费模式：<code>post\_paid</code>（后付费）、<code>pre\_paid</code>（预付费）。</p></td></tr><tr><td><p>output.creator</p></td><td><p>String</p></td><td><p>创建人的阿里云账号 ID。</p></td></tr><tr><td><p>output.modifier</p></td><td><p>String</p></td><td><p>修改人的阿里云账号 ID。</p></td></tr><tr><td><p>output.plan</p></td><td><p>String</p></td><td><p>部署方式。</p></td></tr><tr><td><p>output.gmt\_create</p></td><td><p>String</p></td><td><p>部署任务创建时间。</p></td></tr><tr><td><p>output.gmt\_modified</p></td><td><p>String</p></td><td><p>部署任务更新时间。</p></td></tr><tr><td colSpan={3}><p>仅模型单元（mu）部署方式返回</p></td></tr><tr><td><p>output.base\_capacity</p></td><td><p>Number</p></td><td><p>基础模型运行所需的最小资源单元数量。</p></td></tr><tr><td><p>output.ready\_capacity</p></td><td><p>Number</p></td><td><p>已就绪并可立即处理请求的资源单元数量。</p></td></tr><tr><td><p>output.capacity</p></td><td><p>Number</p></td><td><p>部署使用的资源单元数量。</p></td></tr><tr><td><p>output.model\_unit\_spec</p></td><td><p>String</p></td><td><p>模型单元规格。</p></td></tr><tr><td><p>output.enable\_thinking</p></td><td><p>Boolean</p></td><td><p>是否开启思考模式。</p></td></tr><tr><td><p>output.max\_context\_length</p></td><td><p>Number</p></td><td><p>最大上下文长度限制。</p></td></tr><tr><td><p>output.rpm\_limit</p></td><td><p>Number</p></td><td><p>每分钟请求数。</p></td></tr><tr><td><p>output.tpm\_limit</p></td><td><p>Number</p></td><td><p>每分钟 Token 使用量。</p></td></tr><tr><td colSpan={3}><p>仅预置吞吐（ptu\_v2）部署方式返回</p></td></tr><tr><td><p>output.ptu\_capacity</p></td><td><p>Object</p></td><td><p>预置吞吐量配置，包含 <code>input\_tpm</code>（每分钟最大输入 Token 量）、<code>output\_tpm</code>（每分钟最大输出 Token 量），部分模型另支持 <code>thinking\_output\_tpm</code>（预置思考最大输出 Token 量）。</p></td></tr><tr><td colSpan={3}><p>仅 DTU（dtu）部署方式返回</p></td></tr><tr><td><p>output.dtu\_capacity</p></td><td><p>Object</p></td><td><p>购买的输入/输出 TPM 容量，包含 <code>input\_tpm</code> 与 <code>output\_tpm</code>。</p></td></tr><tr><td><p>output.pre\_paid\_info</p></td><td><p>Object</p></td><td><p>预付费及自动续费信息。</p></td></tr><tr><td colSpan={3}><p>DTU 完整响应字段说明见[模型部署 API 参考](/zh/model-studio/model-deployment-api-reference)</p></td></tr><tr><td colSpan={3}><p>调用失败时返回</p></td></tr><tr><td><p>code</p></td><td><p>String</p></td><td><p>错误码。调用失败时返回。</p></td></tr><tr><td><p>message</p></td><td><p>String</p></td><td><p>错误详情描述。调用失败时返回。</p></td></tr></tbody></table>

<Tabs>
  <Tab title="成功响应示例">
    重点关注：`output.deployed_model`（部署模型的唯一标识）、`output.status`（部署状态）。

    ```json
    {
        "request_id": "f2ae64f7-83cc-410c-bc0b-840443f7eb86",
        "output": {
            "deployed_model": "qwen-plus-2025-12-01-mu-xxxx",
            "gmt_create": "2025-06-17T11:00:38.68",
            "gmt_modified": "2025-06-17T11:00:38.68",
            "status": "PENDING",
            "model_name": "qwen-plus-2025-12-01",
            "base_model": "qwen-plus",
            "model_unit_spec": "MU1",
            "enable_thinking": true,
            "max_context_length": 10000,
            "rpm_limit": 500,
            "tpm_limit": 1000,
            "base_capacity": 1,
            "ready_capacity": 0,
            "workspace_id": "llm-v71tlv3d***",
            "charge_type": "post_paid",
            "creator": "175805416***",
            "modifier": "175805416***",
            "plan": "mu"
        }
    }
    ```
  </Tab>

  <Tab title="错误响应示例">
    ```json
    {
        "code": "InvalidParameter",
        "request_id": "BE213CDD-8A5C-59EE-9A67-055EAB0CB59B",
        "message": "The model xxx does not exist or is not deployable"
    }
    ```
  </Tab>
</Tabs>

## 下一步 <span id="txt-dep-next-h2" />

部署为异步操作，调用本接口后可通过[查询和管理部署](/zh/model-studio/get-deployment-api)接口查询部署状态。
