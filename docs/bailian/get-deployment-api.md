> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 部署模型管理

> 部署模型管理接口， 适用于文本、图像、视频、语音等所有模型类型 。支持查询部署状态、获取可部署模型列表与部署列表、修改限流、扩缩容及删除部署。

<meta property="og:title" content="部署模型管理" />

## 适用范围 <span id="634380ea1e3am" />

- <strong>适用地域</strong>：本文描述的功能仅在<strong>华北2（北京）地域</strong>可用，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
- 已成功[获取 API Key](/zh/model-studio/get-api-key)并[配置到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- 已阅读[模型部署](/zh/model-studio/model-deployment-introduction)和[使用 API 进行模型部署](/zh/model-studio/model-deployment-quick-start)，了解模型部署的基本步骤。

<Note>DTU 计费模式的部署使用同一组部署接口创建与管理，请求中以 `plan="dtu"` 区分部署形式（创建、查询、扩缩容、修改限流与更新模型），DTU 相关参数、状态与域名说明详见 [模型部署 API 参考](/zh/model-studio/model-deployment-api-reference)。续费与自动续费变更请在控制台操作，详见 [DTU 独占算力部署](/zh/model-studio/dtu-model-deployment)。</Note>

## 查询部署模型状态 <span id="09893a6ec96xp" />

查询指定部署模型的详细信息和运行状态。可轮询此接口，当任务状态`status`变为<strong>RUNNING</strong>时，表示模型已部署成功。

<Note>
  模型部署过程预计需要 <strong>5～10分钟</strong>。
</Note>

### 地址 <span id="8d5ac8fa87gs1" />

```http
GET https://dashscope.aliyuncs.com/api/v1/deployments/{deployed_model}
```

### 请求示例 <span id="47484707ccxid" />

通过以下命令可以查询指定专属服务的详细信息：

```bash
curl "https://dashscope.aliyuncs.com/api/v1/deployments/qwen-plus-202305099980-fac9-sample" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="3fb55f3ca72xp" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.66%" }} /><col style={{ width: "12.1%" }} /><col style={{ width: "14.99%" }} /><col style={{ width: "11.75%" }} /><col style={{ width: "38.5%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>部署模型的唯一标识，可通过<a href="/zh/model-studio/create-deployment-api">部署模型</a>或查询部署模型列表接口获取。</p></td></tr></tbody></table>

### 响应示例 <span id="1c84778940gx9" />

关注`status`字段。当状态变为 <strong>RUNNING</strong> 时，表示模型已部署成功，可以开始调用。

```json
{
  "request_id": "66a855f0-a6fe-4b05-9786-fb30c7c6782d",
  "output": {
    "deployed_model": "emo-35b3f106-sample01",
    "gmt_create": "2025-06-17T11:00:38",
    "gmt_modified": "2025-06-17T11:06:13",
    "status": "RUNNING",
    "model_name": "emo",
    "base_model": "emo",
    "base_capacity": 1,
    "capacity": 1,
    "ready_capacity": 1,
    "workspace_id": "llm-v71tlv3***",
    "charge_type": "post_paid",
    "creator": "175805416***",
    "modifier": "175805416***"
  }
}
```

### 响应参数 <span id="8567c7ddfbosd" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "32.38%" }} /><col style={{ width: "21.49%" }} /><col style={{ width: "46.13%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求的唯一标识符。</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>任务详情。</p></td></tr><tr><td><p>output.deployed\_model</p></td><td><p>String</p></td><td><p>部署模型的唯一标识。用于查询模型部署状态和调用模型。</p></td></tr><tr><td><p>output.model\_name</p></td><td><p>String</p></td><td><p>模型标识名。</p></td></tr><tr><td><p>output.base\_model</p></td><td><p>String</p></td><td><p>使用的基准模型。</p></td></tr><tr><td><p>output.status</p></td><td><p>String</p></td><td><p>部署状态：</p> <ul><li><p>PENDING：正在创建部署任务。</p></li> <li><p>RUNNING：部署任务正在运行，模型可正常处理请求。</p></li> <li><p>UPDATING：变配中。</p></li> <li><p>UPDATING\_FAILED：变配失败。</p></li> <li><p>DELETING：正在删除部署任务。</p></li> <li><p>DELETED：已删除。</p></li> <li><p>FAILED：部署任务创建或更新失败。</p></li> <li><p>STOPPED：部署任务已停止，不会被计费。</p></li> <li><p>ARREARS\_DOWN：欠费停服。</p></li> <li><p>ARREARS\_RECOVERING：欠费停服恢复中。</p></li> <li><p>OFFLINING：服务下线中。</p></li></ul></td></tr><tr><td><p>output.base\_capacity</p></td><td><p>Number</p></td><td><p>基础资源单元数量。</p></td></tr><tr><td><p>output.capacity</p></td><td><p>Number</p></td><td><p>当前资源单元数量。</p></td></tr><tr><td><p>output.ready\_capacity</p></td><td><p>Number</p></td><td><p>已就绪的资源单元数量。</p></td></tr><tr><td><p>output.workspace\_id</p></td><td><p>String</p></td><td><p>阿里云百炼 API Key 所属的业务空间 ID。请参见<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">获取Workspace ID</a>。</p></td></tr><tr><td><p>output.charge\_type</p></td><td><p>String</p></td><td><p>付费模式。post\_paid 表示后付费。</p></td></tr><tr><td><p>output.gmt\_create</p></td><td><p>String</p></td><td><p>部署任务创建时间。</p></td></tr><tr><td><p>output.gmt\_modified</p></td><td><p>String</p></td><td><p>部署任务更新时间。</p></td></tr><tr><td><p>output.creator</p></td><td><p>String</p></td><td><p>创建人的阿里云账号 ID。</p></td></tr><tr><td><p>output.modifier</p></td><td><p>String</p></td><td><p>修改人的阿里云账号 ID。</p></td></tr><tr><td><p>output.plan</p></td><td><p>String</p></td><td><p>部署方式。</p></td></tr></tbody></table>

## 获取可部署模型列表 <span id="9e9287b2fd629" />

获取百炼平台上<strong>有资格部署的候选模型</strong>清单，即哪些模型可以用来创建部署。

### 地址 <span id="28112bfa68m02" />

```http
GET https://dashscope.aliyuncs.com/api/v1/deployments/models
```

### 请求示例 <span id="23ff587743990" />

通过下面的命令可以查询支持部署的模型，推荐使用`version=v1.0`获取包含部署方案和模板信息的完整响应。

```bash
curl "https://dashscope.aliyuncs.com/api/v1/deployments/models?page_no=1&page_size=100&version=v1.0&model_source=base" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json'
```

查询用户调优模型：

```bash
curl "https://dashscope.aliyuncs.com/api/v1/deployments/models?page_no=1&page_size=100&version=v1.0&model_source=custom" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="74d15d8f07mgq" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "10%" }} /><col style={{ width: "10%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>page\_no</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>页码，默认值为1。</p></td></tr><tr><td><p>page\_size</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>页大小，默认为50，最大值为100，最小值为1。</p></td></tr><tr><td><p>model\_source</p></td><td><p>String</p></td><td><p>否</p></td><td><p>模型来源。<code>base</code>表示系统模型（默认），<code>custom</code>表示用户调优模型。</p></td></tr><tr><td><p>version</p></td><td><p>String</p></td><td><p>否</p></td><td><p>API 版本，推荐使用<code>v1.0</code>。使用<code>v1.0</code>时，响应中将包含完整的部署方案和模板信息。</p></td></tr></tbody></table>

### 响应示例 <span id="365997715fjsj" />

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

### 响应参数 <span id="a553eb467d50a" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "10%" }} /><col style={{ width: "40%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>models</p></td><td><p>Array</p></td><td><p>可部署模型列表。</p></td></tr><tr><td><p>models\[].model\_name</p></td><td><p>String</p></td><td><p>模型名称。</p></td></tr><tr><td><p>models\[].plans</p></td><td><p>Array</p></td><td><p>该模型支持的部署方案列表。使用<code>version=v1.0</code>时返回。</p></td></tr><tr><td><p>models\[].plans\[].plan</p></td><td><p>String</p></td><td><p>部署方案类型：<code>mu</code>（模型单元）、<code>cu</code>（算力单元）、<code>ptu</code>（预置吞吐量）、<code>lora</code>（LoRA共享部署）。</p></td></tr><tr><td><p>models\[].plans\[].templates</p></td><td><p>Array</p></td><td><p>部署模板列表（<code>plan=mu</code>时返回）。</p></td></tr><tr><td><p>models\[].plans\[].templates\[].template\_id</p></td><td><p>String</p></td><td><p>模板 ID，在<a href="/zh/model-studio/model-deployment-api">创建模型部署任务</a>时作为<code>deploy\_spec</code>参数传入。</p></td></tr><tr><td><p>models\[].plans\[].templates\[].template\_name</p></td><td><p>String</p></td><td><p>模板显示名称。</p></td></tr><tr><td><p>models\[].plans\[].templates\[].template\_type</p></td><td><p>String</p></td><td><p>模板类型：<code>COUPLED</code>（非 PD 分离，使用<code>capacity</code>参数）、<code>SEPERATED</code>（PD 分离，使用<code>prefill\_capacity</code>和<code>decode\_capacity</code>参数）。</p></td></tr><tr><td><p>models\[].plans\[].templates\[].template\_version</p></td><td><p>String</p></td><td><p>模板版本。</p></td></tr><tr><td><p>models\[].plans\[].templates\[].template\_desc</p></td><td><p>String</p></td><td><p>模板描述。</p></td></tr><tr><td><p>models\[].plans\[].templates\[].roles</p></td><td><p>Object</p></td><td><p>节点角色配置。COUPLED 模式包含<code>unified</code>节点，SEPERATED 模式包含<code>prefill</code>和<code>decode</code>节点。</p></td></tr><tr><td><p>models\[].plans\[].templates\[].roles.\{role}.model\_unit\_spec</p></td><td><p>String</p></td><td><p>模型单元规格。</p></td></tr><tr><td><p>models\[].plans\[].templates\[].roles.\{role}.capacity\_unit\_per\_instance</p></td><td><p>Number</p></td><td><p>单实例容量单元数，即 base\_capacity。创建部署时<code>capacity</code>必须是该值的整数倍。</p></td></tr><tr><td><p>page\_no</p></td><td><p>Number</p></td><td><p>查询页码。</p></td></tr><tr><td><p>page\_size</p></td><td><p>Number</p></td><td><p>查询页大小。</p></td></tr><tr><td><p>total</p></td><td><p>Long</p></td><td><p>满足查询条件的所有模型个数。</p></td></tr></tbody></table>

## 获取已部署模型列表 <span id="ce0559e5a4fd2" />

获取<strong>已创建的部署实例</strong>列表，即当前工作空间下实际运行或已部署的模型服务。

### 地址 <span id="e23d1647eb97y" />

```http
GET https://dashscope.aliyuncs.com/api/v1/deployments
```

### 请求示例 <span id="efc71e867awoi" />

通过以下命令可以获取专属服务列表：

```bash
curl "https://dashscope.aliyuncs.com/api/v1/deployments?page_no=1&page_size=100" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="baf571b2e1761" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.4%" }} /><col style={{ width: "8.06%" }} /><col style={{ width: "11.65%" }} /><col style={{ width: "9.64%" }} /><col style={{ width: "54.25%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>page\_no</p></td><td><p>Number</p></td><td><p>query</p></td><td><p>否</p></td><td><p>页码，默认值为1。</p></td></tr><tr><td><p>page\_size</p></td><td><p>Number</p></td><td><p>query</p></td><td><p>否</p></td><td><p>页大小，默认为50，最大值为200，最小值为1。</p></td></tr></tbody></table>

### 响应示例 <span id="dd0b4db7ffhz6" />

命令执行完成后，返回以下结果：

```json
{
  "request_id": "7efdd3a7-a90d-96c6-b477-70055d59edf7",
  "output": {
    "page_no": 1,
    "page_size": 10,
    "total": 1,
    "deployments": [
      {
        "deployed_model": "emo-35b3f106-sample01",
        "gmt_create": "2025-06-17T11:00:38",
        "gmt_modified": "2025-06-17T11:06:13",
        "status": "RUNNING",
        "model_name": "emo",
        "base_model": "emo",
        "base_capacity": 1,
        "capacity": 1,
        "ready_capacity": 1,
        "workspace_id": "llm-v71tlv3d***",
        "charge_type": "post_paid",
        "creator": "175805416***",
        "modifier": "175805416***"
      }
    ]
  }
}
```

### 响应参数 <span id="b0bf8aed91j5n" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "32.38%" }} /><col style={{ width: "21.49%" }} /><col style={{ width: "46.13%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码。</p></td></tr><tr><td><p>output.page\_no</p></td><td><p>Number</p></td><td><p>当前页码。</p></td></tr><tr><td><p>output.page\_size</p></td><td><p>Number</p></td><td><p>每页数量。</p></td></tr><tr><td><p>output.total</p></td><td><p>Number</p></td><td><p>部署总数。</p></td></tr><tr><td><p>output.deployments</p></td><td><p>Array</p></td><td><p>部署模型列表，每个元素的字段与<a href="/zh/model-studio/get-deployment-api#09893a6ec96xp">查询部署模型状态</a>的 output 字段一致。</p></td></tr></tbody></table>

## 修改部署模型的限流 <span id="6d45b3880clmz" />

修改指定部署模型的 RPM（每分钟请求数）和 TPM（每分钟 Token 数）限流设置。

<Note>
  仅模型单元部署方式的[部分模型](/zh/model-studio/create-deployment-api)支持修改设置 rpm 和 tpm。
</Note>

### 地址 <span id="4298a93855qnc" />

```http
PUT https://dashscope.aliyuncs.com/api/v1/deployments/{deployed_model}/update
```

### 请求示例 <span id="c6c9b012e9idg" />

通过以下命令可以修改指定部署的限流设置：

```bash
curl -X PUT "https://dashscope.aliyuncs.com/api/v1/deployments/qwen-plus-2025-12-01-b6d61c71/update" \
--header "Authorization: Bearer $DASHSCOPE_API_KEY" \
--header 'Content-Type: application/json' \
--data '{
    "rpm_limit": 1000,
    "tpm_limit": 200
}'
```

### 请求参数 <span id="11a1929f74zjo" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.68%" }} /><col style={{ width: "12.1%" }} /><col style={{ width: "15%" }} /><col style={{ width: "11.76%" }} /><col style={{ width: "38.46%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>部署模型的唯一标识，可通过<a href="/zh/model-studio/create-deployment-api">部署模型</a>或查询部署模型列表接口获取。</p></td></tr><tr><td><p>rpm\_limit</p></td><td><p>Number</p></td><td><p>body</p></td><td rowSpan={2}><p>至少填写一个参数</p></td><td><p>Requests per minute，每分钟请求数。</p></td></tr><tr><td><p>tpm\_limit</p></td><td><p>Number</p></td><td><p>body</p></td><td><p>Token per minute，每分钟 Token 使用量。</p></td></tr></tbody></table>

### 响应示例 <span id="11a4eece2eole" />

命令执行完成后，返回如下结果：

```json
{
    "request_id": "1d121fd9-876c-40ad-bc40-a9e68ef3b986",
    "output":
    {
        "deployed_model": "qwen-plus-2025-12-01-b6d61c71",
        "gmt_create": "2026-01-07T13:52:44",
        "gmt_modified": "2026-01-07T14:01:41",
        "status": "PENDING",
        "model_name": "qwen-plus-2025-12-01",
        "base_model": "qwen-plus-2025-12-01",
        "base_capacity": 4,
        "capacity": 4,
        "ready_capacity": 0,
        "workspace_id": "llm-8v53e*******",
        "charge_type": "post_paid",
        "creator": "16542902******",
        "modifier": "16542902********",
        "plan": "mu",
        "model_unit_spec": "MU1",
        "enable_thinking": true,
        "max_context_length": 1,
        "rpm_limit": 1000,
        "tpm_limit": 200
    }
}
```

### 响应参数 <span id="c8690122eepu5" />

除[查询部署模型状态](/zh/model-studio/get-deployment-api#09893a6ec96xp)中列出的基础字段外，还可能包含以下字段：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "32.38%" }} /><col style={{ width: "21.49%" }} /><col style={{ width: "46.13%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>output.plan</p></td><td><p>String</p></td><td><p>部署方式。</p></td></tr><tr><td><p>output.model\_unit\_spec</p></td><td><p>String</p></td><td><p>模型单元部署模板，如 MU1。</p></td></tr><tr><td><p>output.enable\_thinking</p></td><td><p>Boolean</p></td><td><p>是否开启思考模式。</p></td></tr><tr><td><p>output.max\_context\_length</p></td><td><p>Number</p></td><td><p>最大上下文长度。</p></td></tr><tr><td><p>output.rpm\_limit</p></td><td><p>Number</p></td><td><p>每分钟请求数限制。</p></td></tr><tr><td><p>output.tpm\_limit</p></td><td><p>Number</p></td><td><p>每分钟 Token 数限制。</p></td></tr></tbody></table>

## 扩缩容部署服务 <span id="870260a9eeeyz" />

通过更新操作调整专属服务使用的资源单元数量。

### 地址 <span id="f8bc6bdbda9f3" />

```http
PUT https://dashscope.aliyuncs.com/api/v1/deployments/{deployed_model}/scale
```

### 请求示例 <span id="eddff8dbb963q" />

通过以下命令可以将指定的服务进行扩缩容：

```bash
curl --request PUT "https://dashscope.aliyuncs.com/api/v1/deployments/emo-35b3f106-sample01/scale" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
                "capacity":2
            }'
```

### 请求参数 <span id="6d783b4ffa9mv" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "10%" }} /><col style={{ width: "10%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>是</p></td><td><p>部署模型的唯一标识（path 参数），可通过<a href="/zh/model-studio/create-deployment-api">部署模型</a>或查询部署模型列表接口获取。</p></td></tr><tr><td><p>capacity</p></td><td><p>Integer</p></td><td><p>条件必选</p></td><td><p>仅<code>"plan": "mu"</code>时生效。具体支持情况请参考<a href="/zh/model-studio/create-deployment-api">模型单元部署的功能支持情况</a>。</p> <p>更新后模型所使用的资源单元数量，<strong>必须</strong>是<code>base\_capacity</code>的整数倍。</p></td></tr><tr><td><p>ptu\_capacity</p></td><td><p>Object</p></td><td><p>条件必选</p></td><td><p>仅<code>"plan": "ptu"</code>时生效。具体支持情况请参考<a href="/zh/model-studio/create-deployment-api">PTU部署的功能支持情况</a>。</p> <p>样例：<code>"ptu\_capacity": \{ "input\_tpm": 10000, "output\_tpm": 1000 }</code>。</p></td></tr><tr><td><p>ptu\_capacity.input\_tpm</p></td><td><p>Number</p></td><td><p>条件必选</p></td><td><p>仅<code>"plan": "ptu"</code>时生效。具体支持情况请参考<a href="/zh/model-studio/create-deployment-api">PTU部署的功能支持情况</a>。</p> <p>部署的模型每分钟支持的最大输入 Token 量（input tokens per minute）。所有模型支持。</p></td></tr><tr><td><p>ptu\_capacity.output\_tpm</p></td><td><p>Number</p></td><td><p>条件必选</p></td><td><p>仅<code>"plan": "ptu"</code>时生效。具体支持情况请参考<a href="/zh/model-studio/create-deployment-api">PTU部署的功能支持情况</a>。</p> <p>部署的模型每分钟支持的最大输出 Token 量（output tokens per minute）。所有模型支持。</p></td></tr><tr><td><p>ptu\_capacity.thinking\_output\_tpm</p></td><td><p>Number</p></td><td><p>否</p></td><td><p>仅<code>"plan": "ptu"</code>时生效。具体支持情况请参考<a href="/zh/model-studio/create-deployment-api">PTU部署的功能支持情况</a>。</p> <p>部署的模型每分钟支持的预置思考最大输出 Token 量（thinking output tokens per minute）。部分模型支持。</p></td></tr></tbody></table>

### 响应示例 <span id="92f969029777u" />

命令执行完成后，返回以下结果：

```json
{
  "request_id": "6c6b7676-3fea-423b-bc26-c9e2337e1142",
  "output": {
    "deployed_model": "emo-35b3f106-sample01",
    "gmt_create": "2025-06-17T11:00:38",
    "gmt_modified": "2025-06-17T11:42:02.311",
    "status": "UPDATING",
    "model_name": "emo",
    "base_model": "emo",
    "base_capacity": 1,
    "capacity": 2,
    "ready_capacity": 1,
    "workspace_id": "llm-v71tlv3dezezp2en",
    "charge_type": "post_paid",
    "creator": "17580541***",
    "modifier": "17580541***"
  }
}
```

### 响应参数 <span id="8ccb319fa1l7f" />

响应字段与[查询部署模型状态](/zh/model-studio/get-deployment-api#09893a6ec96xp)一致，请参考查询部署模型状态的响应参数。

## 删除部署 <span id="4984b6ab4bz05" />

删除指定的部署模型，释放对应的计算资源。

<Tip>
  执行该操作后，模型部署服务将立即下线且<strong>不可恢复</strong>：

  1. 模型将无法调用。
  2. 部署服务停止计费。
</Tip>

### 地址 <span id="7de1d2d10f3tn" />

```http
DELETE https://dashscope.aliyuncs.com/api/v1/deployments/{deployed_model}
```

### 请求示例 <span id="8d81e6bebbfgm" />

通过以下命令可以删除指定的部署任务。

```bash
curl --request DELETE "https://dashscope.aliyuncs.com/api/v1/deployments/emo-35b3f106-sample01" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json'
```

### 请求参数 <span id="f5f3c56c6coq7" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.25%" }} /><col style={{ width: "9.95%" }} /><col style={{ width: "14.39%" }} /><col style={{ width: "11.9%" }} /><col style={{ width: "43.51%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>deployed\_model</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>部署模型的唯一标识，可通过<a href="/zh/model-studio/create-deployment-api">部署模型</a>或查询部署模型列表接口获取。</p></td></tr></tbody></table>

### 响应示例 <span id="c4d4a4d586yli" />

关注`status`字段。当状态变为 <strong>DELETING</strong> 时，表示正在删除部署任务。

```json
{
  "request_id": "5378b78b-8564-481f-a3e0-580e551df22c",
  "output": {
    "deployed_model": "emo-35b3f106-sample01",
    "gmt_create": "2025-06-17T11:00:38",
    "gmt_modified": "2025-06-17T11:42:02",
    "status": "DELETING",
    "model_name": "emo",
    "base_model": "emo",
    "base_capacity": 1,
    "capacity": 2,
    "ready_capacity": 1,
    "workspace_id": "llm-v71tlv3***",
    "charge_type": "post_paid",
    "creator": "175805416***",
    "modifier": "175805416***"
  }
}
```

之后，再调用[查询部署模型状态](/zh/model-studio/get-deployment-api#09893a6ec96xp)进行验证。若返回如下内容，说明部署的服务已不存在，删除成功。

```json
{
    "request_id": "eb619064-0c4f-4d29-aa49-xxxxxx",
    "message": "Not found.",
    "code": "NotFound"
}
```

### 响应参数 <span id="9d4ea4f7e6rfw" />

响应字段与[查询部署模型状态](/zh/model-studio/get-deployment-api#09893a6ec96xp)一致，请参考查询部署模型状态的响应参数。

## 异常响应 <span id="3a46ae96013z6" />

### 响应示例 <span id="081e84fe38xxt" />

```json
{
    "request_id": "ca218d57-b91b-46b2-bd35-c41c6287bcf4",
    "message": "Model: qwen-plus-20230703-cx7f not found!",
    "code": "NotFound"
}
```

### 响应参数 <span id="208c7caff7i9c" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "32.38%" }} /><col style={{ width: "21.49%" }} /><col style={{ width: "46.13%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码。</p></td></tr><tr><td><p>code</p></td><td><p>String</p></td><td><p>错误码。</p></td></tr><tr><td><p>message</p></td><td><p>String</p></td><td><p>错误信息。</p></td></tr></tbody></table>

当请求出错时，可能返回以下错误：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.18%" }} /><col style={{ width: "35.82%" }} /><col style={{ width: "42%" }} /></colgroup><thead><tr><th><p><strong>错误码</strong></p></th><th><p><strong>错误信息</strong></p></th><th><p><strong>错误原因</strong></p></th></tr></thead><tbody><tr><td><p><strong>NotFound</strong></p></td><td><p>Model: xxx not found!</p></td><td><ul><li><p>创建部署任务时指定了不存在的模型。</p></li> <li><p>查询/更新/删除部署任务时指定了不存在的模型。</p></li></ul></td></tr><tr><td><p><strong>Conflict</strong></p></td><td><p>Deployed model xxx already exists, please specify a suffix.</p></td><td><p>创建部署任务时使用了已使用过的suffix。</p></td></tr><tr><td><p><strong>InvalidParameter</strong></p></td><td><p>Invalid capacity (xx), capacity must be larger than or equal to 0 and multiples of 1 and less than 1000!</p></td><td><p>创建/更新部署任务时指定了无效的算力单元数量。</p></td></tr></tbody></table>
