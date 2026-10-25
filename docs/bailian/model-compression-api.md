> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 模型压缩

> 通过量化等方式压缩模型，降低推理成本。

<meta property="og:title" content="模型压缩" />

## 概述 <span id="h2_overview_mc" />

模型压缩 API 通过量化（quantization）等技术压缩自定义全参调优模型，降低推理显存占用、提升吞吐。当前压缩功能仅支持量化，覆盖从查询模板、创建任务、轮询状态、获取日志到取消/删除任务的完整生命周期。

<strong>典型流程</strong>：

- 列举可量化模型及配置模板 → 获取 `template_id` 与可量化的 `model`
- 创建压缩任务 → 拿到 `job_id`
- 轮询 查询压缩任务 / 获取压缩任务日志 → 直到 `SUCCEEDED` / `FAILED` / `CANCELED`
- `SUCCEEDED` 后用 `quantized_output` 创建部署；不再需要时 取消压缩任务 / 删除压缩任务

所有接口域名：`https://dashscope.aliyuncs.com`。鉴权统一使用 `Authorization: Bearer ${YOUR_API_KEY}`，POST 请求需附带 `Content-Type: application/json`。

任务对象字段含义与状态机见 压缩任务对象；错误码统一见文末[错误码](/zh/model-studio/model-compression-api#ec_fpebohap72z5w)。

## 快速开始 <span id="h2_q_title" />

> 当前模型压缩 API 仅在北京 Region 开放。如您使用其他 Region，请通过该 Region 的百炼控制台完成模型压缩操作。

模型压缩 API 提供从查询模板、创建任务、轮询状态、获取日志到取消/删除任务的完整 RESTful 接口。本文档面向开发者通过 OpenAPI 或 SDK 集成压缩能力。控制台介绍见相关文档。

### 前提条件 <span id="q_za2r507ml5rgy" />

在调用本文档接口前，请先完成：

1. 已开通阿里云百炼服务并完成实名认证。
2. 当前工作空间至少有一个基于 `qwen3.5-flash-2026-02-23` 的<strong>自定义全参调优模型</strong>（通过调优任务接口完成 ）。当前压缩功能仅支持该模型，LoRA 模型和已量化的模型不支持。
3. 已获取 API Key（参考 [获取 API Key](/zh/model-studio/get-api-key)）。

> 压缩产出的模型支持的部署单元规格由所选量化模板决定，部署数量在百炼控制台<strong>专属部署</strong>中配置。当前压缩功能限时免费。

### 接口列表 <span id="q_fls8q3igledk3" />

所有接口域名：`https://dashscope.aliyuncs.com`

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /></colgroup><thead><tr><th><p>#</p></th><th><p>方法</p></th><th><p>路径</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>1</p></td><td><p>GET</p></td><td><p><code>/api/v1/fine-tunes/compress/templates</code></p></td><td><p>列举可量化模型及配置模板</p></td></tr><tr><td><p>2</p></td><td><p>POST</p></td><td><p><code>/api/v1/fine-tunes/compress/jobs</code></p></td><td><p>创建压缩任务</p></td></tr><tr><td><p>3</p></td><td><p>GET</p></td><td><p><code>/api/v1/fine-tunes/compress/jobs</code></p></td><td><p>列举压缩任务</p></td></tr><tr><td><p>4</p></td><td><p>GET</p></td><td><p><code>/api/v1/fine-tunes/compress/jobs/\{job\_id}</code></p></td><td><p>查询压缩任务详情</p></td></tr><tr><td><p>5</p></td><td><p>GET</p></td><td><p><code>/api/v1/fine-tunes/compress/jobs/\{job\_id}/logs</code></p></td><td><p>获取压缩任务日志</p></td></tr><tr><td><p>6</p></td><td><p>POST</p></td><td><p><code>/api/v1/fine-tunes/compress/jobs/\{job\_id}/cancel</code></p></td><td><p>取消压缩任务</p></td></tr><tr><td><p>7</p></td><td><p>DELETE</p></td><td><p><code>/api/v1/fine-tunes/compress/jobs/\{job\_id}</code></p></td><td><p>删除压缩任务</p></td></tr></tbody></table>

### 鉴权 <span id="q_ceimef3vw89bf" />

所有接口通过 HTTP Header 携带 API Key：

```plaintext
Authorization: Bearer ${YOUR_API_KEY}
```

`Content-Type: application/json` 适用于 POST 请求体场景。

### 5 分钟上手 <span id="q_cb3mh299fm6tg" />

<Tabs>
  <Tab title="HTTP">
    安装请求库：

    ```bash
    pip install requests
    ```

    创建任务 → 轮询 → 拿到产出模型的完整示例：

    ```python expandable
    import requests, time

    API_KEY = "YOUR_API_KEY"
    BASE = "https://dashscope.aliyuncs.com/api/v1/fine-tunes/compress"
    HEADERS = {"Authorization": f"Bearer {API_KEY}", "Content-Type": "application/json"}

    # 1. 创建压缩任务
    resp = requests.post(f"{BASE}/jobs", headers=HEADERS, json={
        "model": "qwen3.5-flash-2026-02-23-ft-***",  # 自定义调优模型 ID
        "template_id": "quant-flash-nvfp4-mlp-nomtp",          # 通过 GET /templates 获取
        "output_model_suffix": "test",                          # 最多 8 字符，仅小写字母和数字
    }).json()
    job_id = resp["output"]["job_id"]
    print("Job:", job_id)

    # 2. 轮询直到终态
    status = resp["output"]["status"]
    while status not in ("SUCCEEDED", "FAILED", "CANCELED"):
        time.sleep(30)
        resp = requests.get(f"{BASE}/jobs/{job_id}", headers=HEADERS).json()
        status = resp["output"]["status"]
        print("Status:", status)

    # 3. 处理结果
    if status == "SUCCEEDED":
        print("Quantized model:", resp["output"]["quantized_output"])
        # 用 quantized_output 调模型部署接口部署
    elif status == "FAILED":
        print("Error:", resp["output"]["error"])
    ```

    列举任务与获取日志：

    ```python
    # 列举所有压缩成功的任务
    resp = requests.get(f"{BASE}/jobs", headers=HEADERS, params={"status": "SUCCEEDED", "page_size": 20}).json()
    for j in resp["output"]["jobs"]:
        print(j["job_id"], j["template_name"], j["quantized_output"])

    # 获取任务日志
    resp = requests.get(f"{BASE}/jobs/{job_id}/logs", headers=HEADERS, params={"offset": 0, "line": 100}).json()
    for line in resp["output"]["logs"]:
        print(line)
    ```
  </Tab>
</Tabs>

<strong>产出模型的命名规则</strong>：

```plaintext
quantized_output = {base_model}-{output_model_suffix}-{job_id}
```

例如 `base_model` 为 `qwen3.5-flash-2026-02-23`，`suffix` 为 `test`，`job_id` 为 `quant-202604111200-a1b2`，产出模型 ID 为：

```plaintext
qwen3.5-flash-2026-02-23-test-quant-202604111200-a1b2
```

各接口的 cURL 写法见各接口详情页。

## 压缩任务对象 <span id="h2_o_title" />

> 当前模型压缩 API 仅在北京 Region 开放。如您使用其他 Region，请通过该 Region 的百炼控制台完成模型压缩操作。

### 对象属性 <span id="o_6fwu4k7zswtd7" />

<strong>响应参数</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /></colgroup><thead><tr><th><p>字段</p></th><th><p>类型</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>任务 ID</p></td></tr><tr><td><p>job\_name</p></td><td><p>String</p></td><td><p>任务名称</p></td></tr><tr><td><p>job\_description</p></td><td><p>String</p></td><td><p>任务描述</p></td></tr><tr><td><p>status</p></td><td><p>String</p></td><td><p>任务状态（详见任务状态）</p></td></tr><tr><td><p>model</p></td><td><p>String</p></td><td><p>源模型 ID</p></td></tr><tr><td><p>base\_model</p></td><td><p>String</p></td><td><p>基础模型 ID</p></td></tr><tr><td><p>template\_id</p></td><td><p>String</p></td><td><p>使用的压缩模板 ID</p></td></tr><tr><td><p>template\_name</p></td><td><p>String</p></td><td><p>模板名称</p></td></tr><tr><td><p>template\_description</p></td><td><p>String</p></td><td><p>模板描述</p></td></tr><tr><td><p>training\_type</p></td><td><p>String</p></td><td><p>任务类型，固定为 <code>quantization</code></p></td></tr><tr><td><p>compress\_type</p></td><td><p>String</p></td><td><p>压缩类型，同 <code>training\_type</code>，固定为 <code>quantization</code></p></td></tr><tr><td><p>hyper\_parameters</p></td><td><p>Object</p></td><td><p>实际生效的超参（仅返回用户可见参数）</p></td></tr><tr><td><p>custom\_calibration\_file\_ids</p></td><td><p>Array\<String></p></td><td><p>自定义校准数据集文件 ID 列表</p></td></tr><tr><td><p>quantized\_output</p></td><td><p>String</p></td><td><p>量化后产出的模型 ID（仅 SUCCEEDED 时有值）</p></td></tr><tr><td><p>create\_time</p></td><td><p>String</p></td><td><p>任务创建时间</p></td></tr><tr><td><p>start\_time</p></td><td><p>String</p></td><td><p>任务开始执行时间（PENDING/QUEUING 时为 null）</p></td></tr><tr><td><p>end\_time</p></td><td><p>String</p></td><td><p>任务完成时间（终态时有值）</p></td></tr><tr><td><p>error</p></td><td><p>Object</p></td><td><p>失败时的错误信息，含 <code>code</code> 和 <code>message</code>；成功时为 null</p></td></tr><tr><td><p>group</p></td><td><p>String</p></td><td><p>任务分组，固定为 <code>quantization</code></p></td></tr><tr><td><p>usage</p></td><td><p>Integer</p></td><td><p>GPU 时长（秒），SUCCEEDED 或 CANCELED 时出现</p></td></tr></tbody></table>

### 任务状态 <span id="o_0da5nsncdx0ya" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p>状态</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p><code>PENDING</code></p></td><td><p>任务已创建，等待调度</p></td></tr><tr><td><p><code>QUEUING</code></p></td><td><p>已进入调度队列，等待 GPU 资源</p></td></tr><tr><td><p><code>RUNNING</code></p></td><td><p>任务执行中</p></td></tr><tr><td><p><code>CANCELING</code></p></td><td><p>已发起取消，等待终止</p></td></tr><tr><td><p><code>SUCCEEDED</code></p></td><td><p>任务成功，<code>quantized\_output</code> 字段返回产出模型 ID</p></td></tr><tr><td><p><code>FAILED</code></p></td><td><p>任务失败，<code>error.code</code> / <code>error.message</code> 字段返回原因</p></td></tr><tr><td><p><code>CANCELED</code></p></td><td><p>任务已取消</p></td></tr></tbody></table>

<strong>状态流转</strong>：

```plaintext
PENDING ─→ QUEUING ─→ RUNNING ─→ SUCCEEDED
                  │           │
                  ↓           ↓
                CANCELING ─→ FAILED / CANCELED
```

## 列举可量化模型及配置模板 <span id="t1_78wu7kps453jg" />

列出当前用户所有可量化的自定义调优模型，及每个模型对应的压缩模板。模板绑定在模型上，不同模型架构 × 精度 × 目标 MU 规格的组合对应不同模板。

> 仅返回当前用户基于基础模型做 SFT/DPO/CPT 全参调优的自定义模型。LoRA 调优模型和已量化模型不会出现在结果中。

<strong>地址</strong>

```plaintext
GET /api/v1/fine-tunes/compress/templates
```

<strong>请求参数</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>必选</p></th><th><p>默认</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>model</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>按模型 ID 过滤；传基础模型名时返回基于该基础模型的所有自定义模型</p></td></tr><tr><td><p>lang</p></td><td><p>String</p></td><td><p>否</p></td><td><p>zh-CN</p></td><td><p>响应语言：<code>zh-CN</code> / <code>en-US</code>（详见多语言支持）</p></td></tr></tbody></table>

<strong>请求示例</strong>

```bash
curl "https://dashscope.aliyuncs.com/api/v1/fine-tunes/compress/templates" \
  -H "Authorization: Bearer ${API_KEY}"
```

<strong>响应示例（最小）</strong>

```json
{
  "request_id": "uuid-string",
  "output": {
    "base_models": ["qwen3.5-flash-2026-02-23"],
    "custom_models": [
      {
        "model": "qwen3.5-flash-2026-02-23-ft-***",
        "model_name": "我的SFT调优模型",
        "base_model": "qwen3.5-flash-2026-02-23",
        "templates": [
          {
            "template_id": "quant-flash-nvfp4-mlp-nomtp",
            "template_name": "W4A4 NVFP4高性能压缩-MU5/MU8/MU9",
            "description": "在更低比特压缩下兼顾高精度与高性能，进一步降低显存占用并提升推理吞吐。",
            "compress_type": "quantization",
            "hyper_parameters": []
          }
        ]
      }
    ]
  }
}
```

<Accordion title="响应示例（完整：含可调超参）">
  ```json expandable
  {
    "request_id": "uuid-string",
    "output": {
      "base_models": ["qwen3.5-flash-2026-02-23"],
      "custom_models": [
        {
          "model": "qwen3.5-flash-2026-02-23-ft-***",
          "model_name": "我的SFT调优模型",
          "base_model": "qwen3.5-flash-2026-02-23",
          "templates": [
            {
              "template_id": "quant-flash-nvfp4-mlp-nomtp",
              "template_name": "W4A4 NVFP4高性能压缩-MU5/MU8/MU9",
              "description": "在更低比特压缩下兼顾高精度与高性能，进一步降低显存占用并提升推理吞吐。",
              "compress_type": "quantization",
              "hyper_parameters": [
                {
                  "name": "calib_input",
                  "type": "string",
                  "display_name": "校准输入",
                  "description": "是否启用校准输入",
                  "support_values": ["true"],
                  "defaultValue": "true",
                  "recommend_value": "true",
                  "required": false
                }
              ]
            }
          ]
        }
      ]
    }
  }
  ```
</Accordion>

<strong>响应参数</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /></colgroup><thead><tr><th><p>字段</p></th><th><p>类型</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>base\_models</p></td><td><p>Array\<String></p></td><td><p>支持压缩的基础模型名称列表</p></td></tr><tr><td><p>custom\_models\[].model</p></td><td><p>String</p></td><td><p>模型 ID</p></td></tr><tr><td><p>custom\_models\[].model\_name</p></td><td><p>String</p></td><td><p>模型展示名称</p></td></tr><tr><td><p>custom\_models\[].base\_model</p></td><td><p>String</p></td><td><p>基础模型名称</p></td></tr><tr><td><p>custom\_models\[].templates</p></td><td><p>Array</p></td><td><p>该模型支持的压缩配置模板列表，继承其基础模型的模板</p></td></tr><tr><td><p>templates\[].template\_id</p></td><td><p>String</p></td><td><p>模板 ID，创建压缩任务时作为 template\_id 参数传入</p></td></tr><tr><td><p>templates\[].template\_name</p></td><td><p>String</p></td><td><p>模板名称（支持多语言，根据 <code>lang</code> 参数返回对应语言版本）</p></td></tr><tr><td><p>templates\[].description</p></td><td><p>String</p></td><td><p>模板描述（支持多语言，根据 <code>lang</code> 参数返回对应语言版本）</p></td></tr><tr><td><p>templates\[].compress\_type</p></td><td><p>String</p></td><td><p>压缩类型，固定为 <code>quantization</code></p></td></tr><tr><td><p>templates\[].hyper\_parameters</p></td><td><p>Array</p></td><td><p>可调超参数；空数组表示无可调超参</p></td></tr><tr><td><p>hyper\_parameters\[].name</p></td><td><p>String</p></td><td><p>参数名（创建任务时作为 Key 使用）</p></td></tr><tr><td><p>hyper\_parameters\[].type</p></td><td><p>String</p></td><td><p>类型：<code>number</code>（数值型，配合 <code>data\_range</code>/<code>step</code>）/ <code>string</code>（枚举型，配合 <code>support\_values</code>）</p></td></tr><tr><td><p>hyper\_parameters\[].display\_name</p></td><td><p>String</p></td><td><p>参数展示名称（支持多语言，根据 <code>lang</code> 参数返回对应语言版本）</p></td></tr><tr><td><p>hyper\_parameters\[].description</p></td><td><p>String</p></td><td><p>参数描述（支持多语言，根据 <code>lang</code> 参数返回对应语言版本）</p></td></tr><tr><td><p>hyper\_parameters\[].defaultValue</p></td><td><p>String</p></td><td><p>默认值</p></td></tr><tr><td><p>hyper\_parameters\[].recommend\_value</p></td><td><p>String</p></td><td><p>推荐值</p></td></tr><tr><td><p>hyper\_parameters\[].required</p></td><td><p>Boolean</p></td><td><p>是否必传</p></td></tr><tr><td><p>hyper\_parameters\[].support\_values</p></td><td><p>Array\<String></p></td><td><p>枚举值列表（仅 <code>type=string</code> 时存在），如 <code>\["instruct", "think", "hybrid"]</code></p></td></tr><tr><td><p>hyper\_parameters\[].data\_range</p></td><td><p>Array\<String></p></td><td><p>数值范围（仅 <code>type=number</code> 时存在），如 <code>\["64","256"]</code> 表示取值范围 64\~256</p></td></tr><tr><td><p>hyper\_parameters\[].step</p></td><td><p>Integer</p></td><td><p>步长（仅 <code>type=number</code> 时存在）</p></td></tr></tbody></table>

## 创建压缩任务 <span id="c_8g5b8dzd707n4" />

<strong>地址</strong>

```plaintext
POST /api/v1/fine-tunes/compress/jobs
```

<strong>请求参数</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>必选</p></th><th><p>默认</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>model</p></td><td><p>String</p></td><td><p>是</p></td><td><p>-</p></td><td><p>源模型 ID，可通过接口获取</p></td></tr><tr><td><p>template\_id</p></td><td><p>String</p></td><td><p>是</p></td><td><p>-</p></td><td><p>压缩模板 ID，可通过接口获取</p></td></tr><tr><td><p>job\_name</p></td><td><p>String</p></td><td><p>否</p></td><td><p>自动生成</p></td><td><p>任务名称，同一用户下不允许重复；最多 50 字符</p></td></tr><tr><td><p>job\_description</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>任务描述；最多 200 字符</p></td></tr><tr><td><p>hyper\_parameters</p></td><td><p>Object</p></td><td><p>否</p></td><td><p>模板默认值</p></td><td><p>超参覆盖（Key-Value），只传想覆盖的项</p></td></tr><tr><td><p>custom\_calibration\_file\_ids</p></td><td><p>Array\<String></p></td><td><p>否</p></td><td><p>-</p></td><td><p>自定义校准数据集文件 ID 列表（数据集组 ID，格式 <code>file-\{32hex}</code>）；仅当 <code>calib\_input=true</code> 时生效。数据集需先在<strong>数据管理</strong>中创建并发布</p></td></tr><tr><td><p>output\_model\_suffix</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>量化产出模型名后缀；最多 8 字符，仅小写字母和数字。输出模型名格式：<code>\{base\_model}-\{suffix}-\{job\_id}</code></p></td></tr></tbody></table>

<strong>请求示例</strong>

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/fine-tunes/compress/jobs" \
  -H "Authorization: Bearer ${API_KEY}" \
  -H "Content-Type: application/json" \
  -d '{
    "job_name": "qwen3.5-flash 压缩任务",
    "model": "qwen3.5-flash-2026-02-23-ft-***",
    "template_id": "quant-flash-nvfp4-mlp-nomtp",
    "custom_calibration_file_ids": ["file-***"],
    "output_model_suffix": "test"
  }'
```

<strong>响应示例</strong>

```json
{
  "request_id": "uuid-string",
  "output": {
    "job_id": "quant-202604111200-a1b2",
    "job_name": "qwen3.5-flash 压缩任务",
    "status": "PENDING",
    "model": "qwen3.5-flash-2026-02-23-ft-***",
    "base_model": "qwen3.5-flash-2026-02-23",
    "template_id": "quant-flash-nvfp4-mlp-nomtp",
    "template_name": "W4A4 NVFP4高性能压缩-MU5/MU8/MU9",
    "training_type": "quantization",
    "compress_type": "quantization",
    "hyper_parameters": {},
    "custom_calibration_file_ids": ["file-***"],
    "quantized_output": null,
    "create_time": "2026-04-11 12:00:00",
    "start_time": null,
    "end_time": null,
    "error": null,
    "group": "quantization"
  }
}
```

<strong>响应参数</strong>：字段含义同请求参数。

## 列举压缩任务 <span id="l_4e0fwlwz5feyp" />

支持按状态、模型、模板、量化规格、算法、时间范围、任务名/ID 等过滤，支持创建时间排序和分页。

<strong>地址</strong>

```plaintext
GET /api/v1/fine-tunes/compress/jobs
```

<strong>请求参数</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>必选</p></th><th><p>默认</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>status</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>按状态过滤（如 RUNNING、SUCCEEDED）</p></td></tr><tr><td><p>model</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>按源模型 ID 过滤</p></td></tr><tr><td><p>template\_id</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>按模板 ID 过滤</p></td></tr><tr><td><p>quant\_spec</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>按量化规格过滤（如 <code>w4a16</code>、<code>w8a8</code>）</p></td></tr><tr><td><p>quant\_method</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>按量化算法过滤（如 <code>gptq</code>、<code>awq</code>、<code>fp8</code>）</p></td></tr><tr><td><p>start\_time</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>任务开始时间不早于该值。格式：<code>yyyy-MM-dd HH:mm:ss</code> / ISO-8601 / <code>yyyy-MM-dd</code></p></td></tr><tr><td><p>end\_time</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>任务结束时间不晚于该值，格式同 <code>start\_time</code></p></td></tr><tr><td><p>job\_name</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>按任务名称模糊匹配</p></td></tr><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>按任务 ID 模糊匹配</p></td></tr><tr><td><p>search\_key</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p>搜索关键字。不传 <code>select\_key</code> 时同时模糊匹配 <code>job\_id</code> 和 <code>job\_name</code>；配合 <code>select\_key</code> 时仅匹配指定字段</p></td></tr><tr><td><p>select\_key</p></td><td><p>String</p></td><td><p>否</p></td><td><p>-</p></td><td><p><code>search\_key</code> 的搜索字段，可选 <code>job\_id</code> / <code>job\_name</code></p></td></tr><tr><td><p>sort\_by</p></td><td><p>String</p></td><td><p>否</p></td><td><p>create\_time</p></td><td><p>排序字段，目前仅支持 <code>create\_time</code></p></td></tr><tr><td><p>sort\_order</p></td><td><p>String</p></td><td><p>否</p></td><td><p>desc</p></td><td><p>排序方向，<code>asc</code> 升序 / <code>desc</code> 降序</p></td></tr><tr><td><p>page\_no</p></td><td><p>Integer</p></td><td><p>否</p></td><td><p>1</p></td><td><p>页码</p></td></tr><tr><td><p>page\_size</p></td><td><p>Integer</p></td><td><p>否</p></td><td><p>10</p></td><td><p>每页数量，最大 100</p></td></tr></tbody></table>

<strong>请求示例</strong>

```bash
# 综合搜索：按状态 + 算法 + 分页
curl "https://dashscope.aliyuncs.com/api/v1/fine-tunes/compress/jobs?status=SUCCEEDED&quant_method=gptq&page_size=10" \
  -H "Authorization: Bearer ${API_KEY}"

# 时间范围 + 按任务名搜索 + 创建时间升序
curl "https://dashscope.aliyuncs.com/api/v1/fine-tunes/compress/jobs?start_time=2026-04-01&end_time=2026-04-30&search_key=qwen3&select_key=job_name&sort_by=create_time&sort_order=asc" \
  -H "Authorization: Bearer ${API_KEY}"
```

<strong>响应示例</strong>

```json expandable
{
  "request_id": "uuid-string",
  "output": {
    "total": 42,
    "page_no": 1,
    "page_size": 10,
    "jobs": [
      {
        "job_id": "quant-202604111200-a1b2",
        "job_name": "qwen3.5-flash 压缩任务",
        "status": "SUCCEEDED",
        "model": "qwen3.5-flash-2026-02-23-ft-***",
        "base_model": "qwen3.5-flash-2026-02-23",
        "template_id": "quant-flash-nvfp4-mlp-nomtp",
        "template_name": "W4A4 NVFP4高性能压缩-MU5/MU8/MU9",
        "training_type": "quantization",
        "compress_type": "quantization",
        "custom_calibration_file_ids": ["file-***"],
        "quantized_output": "qwen3.5-flash-2026-02-23-test-quant-202604111200-a1b2",
        "create_time": "2026-04-11 12:00:00",
        "start_time": "2026-04-11 12:02:30",
        "end_time": "2026-04-11 13:02:30",
        "group": "quantization",
        "usage": 3600
      }
    ]
  }
}
```

<strong>响应参数</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /></colgroup><thead><tr><th><p>字段</p></th><th><p>类型</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>total</p></td><td><p>Integer</p></td><td><p>符合条件的任务总数</p></td></tr><tr><td><p>page\_no</p></td><td><p>Integer</p></td><td><p>当前页码</p></td></tr><tr><td><p>page\_size</p></td><td><p>Integer</p></td><td><p>每页数量</p></td></tr><tr><td><p>jobs</p></td><td><p>Array</p></td><td><p>任务列表，字段含义同创建压缩任务响应参数</p></td></tr></tbody></table>

## 查询压缩任务 <span id="g_vzzghk5111bgu" />

<strong>地址</strong>

```plaintext
GET /api/v1/fine-tunes/compress/jobs/{job_id}
```

<strong>请求示例</strong>

```bash
curl "https://dashscope.aliyuncs.com/api/v1/fine-tunes/compress/jobs/quant-202604111200-a1b2" \
  -H "Authorization: Bearer ${API_KEY}"
```

<strong>响应示例</strong>

```json
{
  "request_id": "uuid-string",
  "output": {
    "job_id": "quant-202604111200-a1b2",
    "job_name": "qwen3.5-flash 压缩任务",
    "job_description": "...",
    "status": "SUCCEEDED",
    "model": "qwen3.5-flash-2026-02-23-ft-***",
    "base_model": "qwen3.5-flash-2026-02-23",
    "template_id": "quant-flash-nvfp4-mlp-nomtp",
    "template_name": "W4A4 NVFP4高性能压缩-MU5/MU8/MU9",
    "template_description": "在更低比特压缩下兼顾高精度与高性能，进一步降低显存占用并提升推理吞吐。",
    "training_type": "quantization",
    "compress_type": "quantization",
    "hyper_parameters": {},
    "custom_calibration_file_ids": ["file-***"],
    "quantized_output": "qwen3.5-flash-2026-02-23-test-quant-202604111200-a1b2",
    "create_time": "2026-04-11 12:00:00",
    "start_time": "2026-04-11 12:02:30",
    "end_time": "2026-04-11 13:02:30",
    "error": null,
    "group": "quantization",
    "usage": 3600
  }
}
```

<strong>响应参数</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /></colgroup><thead><tr><th><p>字段</p></th><th><p>类型</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>任务 ID，可通过创建压缩任务或列举压缩任务接口获取</p></td></tr><tr><td><p>job\_name</p></td><td><p>String</p></td><td><p>任务名称</p></td></tr><tr><td><p>job\_description</p></td><td><p>String</p></td><td><p>任务描述</p></td></tr><tr><td><p>status</p></td><td><p>String</p></td><td><p>任务状态（详见任务状态）</p></td></tr><tr><td><p>model</p></td><td><p>String</p></td><td><p>源模型 ID</p></td></tr><tr><td><p>base\_model</p></td><td><p>String</p></td><td><p>基础模型 ID</p></td></tr><tr><td><p>template\_id</p></td><td><p>String</p></td><td><p>使用的压缩模板 ID</p></td></tr><tr><td><p>template\_name</p></td><td><p>String</p></td><td><p>模板名称</p></td></tr><tr><td><p>template\_description</p></td><td><p>String</p></td><td><p>模板描述</p></td></tr><tr><td><p>training\_type</p></td><td><p>String</p></td><td><p>任务类型，固定为 <code>quantization</code></p></td></tr><tr><td><p>compress\_type</p></td><td><p>String</p></td><td><p>压缩类型，同 <code>training\_type</code>，固定为 <code>quantization</code></p></td></tr><tr><td><p>hyper\_parameters</p></td><td><p>Object</p></td><td><p>实际生效的超参（仅返回用户可见参数）</p></td></tr><tr><td><p>custom\_calibration\_file\_ids</p></td><td><p>Array\<String></p></td><td><p>自定义校准数据集文件 ID 列表</p></td></tr><tr><td><p>quantized\_output</p></td><td><p>String</p></td><td><p>量化后产出的模型 ID（仅 SUCCEEDED 时有值），可用于<a href="/zh/model-studio/create-deployment-api">部署模型</a>接口进行模型部署</p></td></tr><tr><td><p>create\_time</p></td><td><p>String</p></td><td><p>任务创建时间</p></td></tr><tr><td><p>start\_time</p></td><td><p>String</p></td><td><p>任务开始执行时间（PENDING/QUEUING 时为 null）</p></td></tr><tr><td><p>end\_time</p></td><td><p>String</p></td><td><p>任务完成时间（终态时有值）</p></td></tr><tr><td><p>error</p></td><td><p>Object</p></td><td><p>失败时的错误信息，含 <code>code</code> 和 <code>message</code>；成功时为 null</p></td></tr><tr><td><p>group</p></td><td><p>String</p></td><td><p>任务分组，固定为 <code>quantization</code></p></td></tr><tr><td><p>usage</p></td><td><p>Integer</p></td><td><p>GPU 时长（秒），SUCCEEDED 或 CANCELED 时出现</p></td></tr></tbody></table>

## 获取压缩任务日志 <span id="gl_qyulnp3iq45e9" />

<strong>地址</strong>

```plaintext
GET /api/v1/fine-tunes/compress/jobs/{job_id}/logs
```

其中 `{job_id}` 为压缩任务 ID，可通过创建压缩任务或列举压缩任务接口获取。

<strong>请求参数</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>必选</p></th><th><p>默认</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>offset</p></td><td><p>Integer</p></td><td><p>否</p></td><td><p>0</p></td><td><p>忽略前 N 行，从第 N+1 行开始读取</p></td></tr><tr><td><p>line</p></td><td><p>Integer</p></td><td><p>否</p></td><td><p>100</p></td><td><p>读取行数，上限 1000</p></td></tr></tbody></table>

<strong>请求示例</strong>

```bash
curl "https://dashscope.aliyuncs.com/api/v1/fine-tunes/compress/jobs/quant-202604111200-a1b2/logs?offset=0&line=50" \
  -H "Authorization: Bearer ${API_KEY}"
```

<strong>响应示例</strong>

```json
{
  "request_id": "uuid-string",
  "output": {
    "total": 15,
    "logs": [
      "2026-04-11 12:02:35 - INFO - Starting quantization...",
      "2026-04-11 12:30:00 - INFO - Quantization progress: 100%",
      "2026-04-11 12:35:00 - INFO - Quantization succeeded!"
    ]
  }
}
```

<strong>日志展示规则</strong>

- 任务传入自定义校准数据集时，日志中包含数据处理完成标记 `data process succeeded, start to quantization`
- 日志接口已过滤系统内部标记，仅返回用户可读的压缩进度信息

## 取消压缩任务 <span id="cc_mkwu5hm0tasls" />

仅允许取消 `PENDING`、`QUEUING` 或 `RUNNING` 状态的任务。取消为异步操作，任务会先进入 `CANCELING` 过渡态，最终转为 `CANCELED`。

<strong>地址</strong>

```plaintext
POST /api/v1/fine-tunes/compress/jobs/{job_id}/cancel
```

其中 `{job_id}` 为压缩任务 ID，可通过创建压缩任务或列举压缩任务接口获取。

<strong>请求示例</strong>

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/fine-tunes/compress/jobs/quant-202604111200-a1b2/cancel" \
  -H "Authorization: Bearer ${API_KEY}"
```

<strong>响应示例</strong>

```json
{
  "request_id": "uuid-string",
  "output": { "status": "success" }
}
```

## 删除压缩任务 <span id="d_5erfjcj0gux3o" />

仅允许删除终态（`SUCCEEDED` / `FAILED` / `CANCELED`）的任务。删除任务记录<strong>不会</strong>删除已产出的量化模型（`quantized_output`）。

<strong>地址</strong>

```plaintext
DELETE /api/v1/fine-tunes/compress/jobs/{job_id}
```

其中 `{job_id}` 为压缩任务 ID，可通过创建压缩任务或列举压缩任务接口获取。

<strong>请求示例</strong>

```bash
curl -X DELETE "https://dashscope.aliyuncs.com/api/v1/fine-tunes/compress/jobs/quant-202604111200-a1b2" \
  -H "Authorization: Bearer ${API_KEY}"
```

<strong>响应示例</strong>

```json
{
  "request_id": "uuid-string",
  "output": { "status": "success" }
}
```

## 错误码 <span id="ec_fpebohap72z5w" />

### 通用错误码 <span id="ec_4madzafhxuwby" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /></colgroup><thead><tr><th><p>错误码</p></th><th><p>HTTP</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>请求参数不合法</p></td></tr><tr><td><p><code>MissingParameter</code></p></td><td><p>400</p></td><td><p>缺少必选参数</p></td></tr><tr><td><p><code>Unauthorized</code></p></td><td><p>401</p></td><td><p>认证失败</p></td></tr><tr><td><p><code>Forbidden</code></p></td><td><p>403</p></td><td><p>无权限访问</p></td></tr><tr><td><p><code>ResourceNotFound</code></p></td><td><p>404</p></td><td><p>资源不存在</p></td></tr><tr><td><p><code>UnsupportedOperation</code></p></td><td><p>400</p></td><td><p>资源状态不允许该操作（如取消已终态任务）</p></td></tr><tr><td><p><code>QuotaExceeded</code></p></td><td><p>429</p></td><td><p>配额超限</p></td></tr><tr><td><p><code>InternalError</code></p></td><td><p>500</p></td><td><p>服务内部错误</p></td></tr></tbody></table>

### 业务错误码 <span id="ec_gddlvbl39pmfk" />

以下业务错误码按场景分类列出。`对外 Code` 为接口实际返回的 `code` 字段值。

<strong>参数校验类</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "16.666667%" }} /><col style={{ width: "58.333333%" }} /></colgroup><thead><tr><th><p>对外 Code</p></th><th><p>HTTP</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>缺少必选参数 <code>model</code></p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>缺少必选参数 <code>template\_id</code></p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>不支持对基础模型直接量化</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>指定的配置模板不存在</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>当前模型不支持该压缩模板</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>模型不支持量化</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>LoRA 调优模型不支持量化</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>模型数据不可用</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>任务名称包含不支持的字符</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p><code>output\_model\_suffix</code> 超过 8 字符</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>源模型尚未就绪</p></td></tr><tr><td><p><code>AccessDenied</code></p></td><td><p>403</p></td><td><p>无权使用该压缩模板</p></td></tr></tbody></table>

<strong>超参数校验类</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "16.666667%" }} /><col style={{ width: "58.333333%" }} /></colgroup><thead><tr><th><p>对外 Code</p></th><th><p>HTTP</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>必选超参数未传</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>传入了未知超参数</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>超参数值不在枚举值列表中</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>超参数值超出数值范围</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>超参数值不是合法数字</p></td></tr></tbody></table>

<strong>任务查询类</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "16.666667%" }} /><col style={{ width: "58.333333%" }} /></colgroup><thead><tr><th><p>对外 Code</p></th><th><p>HTTP</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p><code>NotFound</code></p></td><td><p>404</p></td><td><p>指定的压缩任务不存在</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>缺少必选参数 <code>job\_id</code></p></td></tr></tbody></table>

<strong>分页与时间参数类</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "16.666667%" }} /><col style={{ width: "58.333333%" }} /></colgroup><thead><tr><th><p>对外 Code</p></th><th><p>HTTP</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>页码参数不合法（须 ≥ 1）</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>每页数量不合法（须 1\~100）</p></td></tr><tr><td><p><code>InvalidParameter</code></p></td><td><p>400</p></td><td><p>时间格式不合法</p></td></tr></tbody></table>

### 错误响应示例 <span id="ec_54u7ht5qs9k9u" />

```json
{
  "request_id": "uuid-string",
  "code": "InvalidParameter",
  "message": "The specified model 'xxx-lora-yyy' is a LoRA model and not supported for quantization."
}
```
