> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询模型列表

> 调用 GET /api/v1/models 接口查询百炼平台上可用的模型列表，支持按模型作者、模态类型、模型能力、部署模式等条件筛选，并获取模型的定价和上下文长度等信息。

<meta property="og:title" content="查询模型列表" />

## 前提条件 <span id="a1b2c3d4e5f010" />

已创建 API Key 并配置为环境变量 `DASHSCOPE_API_KEY`。配置方法请参见[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## 请求说明 <span id="a1b2c3d4e5f020" />

<strong>HTTP 方法</strong>：GET

<strong>请求地址</strong>

调用时请将 `{WorkspaceId}` 替换为业务空间 ID。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th><p><strong>地域</strong></p></th><th><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td><p>华北2（北京）</p></td><td><p><code>https\://\{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models</code></p></td></tr><tr><td><p>新加坡</p></td><td><p><code>[https://dashscope-intl.aliyuncs.com/api/v1/models](https://dashscope-intl.aliyuncs.com/api/v1/models)</code></p></td></tr><tr><td><p>中国香港</p></td><td><p><code>[https://cn-hongkong.dashscope.aliyuncs.com/api/v1/models](https://cn-hongkong.dashscope.aliyuncs.com/api/v1/models)</code></p></td></tr><tr><td><p>日本（东京）</p></td><td><p><code>https\://\{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/api/v1/models</code></p></td></tr><tr><td><p>德国（法兰克福）</p></td><td><p><code>https\://\{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1/models</code></p></td></tr><tr><td><p>美国（弗吉尼亚）</p></td><td><p><code>https\://\{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/models</code></p></td></tr></tbody></table>

<strong>认证方式</strong>

在请求 Header 中设置 `Authorization: Bearer {API_KEY}`。

<Columns cols={2}>
  <Column>
    ## 请求参数 <span id="a1b2c3d4e5f100" />

    所有参数均通过 Query String 传递。

    <strong>name</strong> · `String` · 可选

    按模型名称进行模糊搜索。示例：`qwen`

    <strong>model</strong> · `String` · 可选

    按模型 ID 进行精确查询。示例：`qwen3.8-max`

    <strong>language</strong> · `String` · 可选

    返回结果的语言。可选值：`zh-CN`（中文）、`en-US`（英文）。

    <strong>page\_no</strong> · `Integer` · 可选

    页码，从 1 开始。默认值：`1`。

    <strong>page\_size</strong> · `Integer` · 可选

    每页返回的模型数量。默认值：`20`。

    <strong>providers</strong> · `Array[String]` · 可选

    按模型供应商筛选。

    <Accordion title="providers 可选值">
      - `qwen`：阿里巴巴-千问
      - `zhipu-ai`：智谱AI
      - `wan`：阿里巴巴-万相
      - `qwen-domain-model`：阿里巴巴-领域模型
      - `mini-max`：MiniMax
      - `moonshot-ai`：月之暗面
      - `deepseek`：DeepSeek
      - `happyhorse`：阿里巴巴-HappyHorse
      - `kling`：可灵AI
      - `pixverse`：PixVerse
      - `vidu`：Vidu
      - `tripo`：Tripo
      - `xiaomi`：小米MiMo
    </Accordion>

    <strong>inference\_providers</strong> · `Array[String]` · 可选

    按推理服务供应商筛选。可选值：

    <Accordion title="inference_providers 可选值">
      - `aliyun-bailian`：阿里云百炼
      - `alibaba-cloud-modelstudio`：Alibaba Cloud Model Studio
      - `siliconflow`：硅基流动
      - `moonshot`：Kimi
      - `mini-max`：MiniMax
      - `kling`：Kling AI
      - `vidu`：Vidu
      - `pixverse`：PixVerse
      - `vanchin`：快手万擎
      - `xiaomi`：小米MiMo
      - `zhipu-ai`：Zhipu AI
      - `tripo`：Tripo
    </Accordion>

    <strong>capabilities</strong> · `Array[String]` · 可选

    按模型类型筛选。

    <Accordion title="capabilities 可选值">
      - `Reasoning`：深度思考
      - `VU`：视觉理解
      - `IG`：图片生成
      - `VG`：视频生成
      - `ASR`：语音识别
      - `TTS`：语音合成
      - `ME`：多模态向量
      - `Realtime-Omni`：实时全模态
      - `Multimodal-Omni`：全模态
      - `Realtime-Text-to-Speech`：实时语音合成
      - `TG`：文本生成
      - `TR`：文本向量
      - `Realtime-ASR`：实时语音识别
      - `Realtime-Audio-Translate`：实时语音翻译
      - `3D-generation`：3D 生成
      - `Realtime-Chatting`：实时语音对话

      传入多个值时重复参数名，例如 `capabilities=TG&capabilities=Reasoning`，返回支持文本生成或深度思考的模型。
    </Accordion>

    <strong>features</strong> · `Array[String]` · 可选

    按模型能力筛选。

    <Accordion title="features 可选值">
      - `model-experience`：模型体验
      - `function-calling`：function calling
      - `structured-outputs`：结构化输出
      - `web-search`：联网搜索
      - `prefix-completion`：前缀续写
      - `cache`：cache缓存
      - `batch`：批量推理
      - `fine-tuning`：模型调优
    </Accordion>

    <strong>context\_window</strong> · `Integer` · 可选

    按上下文长度筛选，返回上下文长度严格小于该值的模型。示例：`20000`。

    <strong>service\_site</strong> · `String` · 可选

    按部署模式筛选。不传时返回所有部署模式的模型。可选值：

    <Accordion title="service_site 可选值">
      - `global`：全球
      - `international`：国际
      - `asia-pacific-china`：中国
      - `cn-hongkong`：中国香港
      - `european-union`：欧盟
      - `united-states`：美国
      - `japan`：日本
    </Accordion>

    <strong>supports</strong> · `Array[String]` · 可选

    按模型支持的应用场景筛选。默认值：`inference`。可选值：

    <Accordion title="supports 可选值">
      - `inference`：支持推理的模型
      - `deploy`：支持部署的模型
    </Accordion>

    <strong>deployment\_methods</strong> · `Array[String]` · 可选

    按部署方式筛选。可选值：

    <Accordion title="deployment_methods 可选值">
      - `ptu`：预置吞吐量（Provisioned Throughput Unit）
    </Accordion>

    <strong>deployment\_ptu\_service\_tiers</strong> · `Array[String]` · 可选

    按 PTU 类型筛选，使用时需同时设置 `deployment_methods` 包含 `ptu`。
  </Column>

  <Column>
    ## 请求示例 <span id="a1b2c3d4e5f400" />

    <Tabs>
      <Tab title="查询所有模型">
        不设置筛选条件。示例查询第一页；如需获取所有模型，请递增 `page_no`，直到获取完 `output.total` 指示的模型总数。

        <CodeGroup>
          ```bash title="curl"
          curl --get "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models" \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header "Content-Type: application/json" \
              --data-urlencode "page_no=1" \
              --data-urlencode "page_size=20"
          ```

          ```python title="Python SDK"
          import os
          from dashscope import Models

          resp = Models.list(
              page=1,
              page_size=20,
              api_key=os.getenv('DASHSCOPE_API_KEY'),
          )
          print(resp.output)
          ```
        </CodeGroup>

        <Note>
          DashScope Python SDK 的 `Models.list()` 方法查询模型时仅支持 `page` 与 `page_size` 两个分页参数，不支持按 `capabilities`、`providers`、`features` 等条件筛选。如需筛选模型，请使用 HTTP API。Python 示例查询第一页，递增 `page` 可获取后续页面。
        </Note>
      </Tab>

      <Tab title="按模型 ID 查询">
        设置 `model=qwen3.8-max`，按模型 ID 精确查询。

        ```bash
        curl --get "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models" \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header "Content-Type: application/json" \
            --data-urlencode "model=qwen3.8-max" \
            --data-urlencode "page_no=1" \
            --data-urlencode "page_size=20"
        ```
      </Tab>

      <Tab title="查询所有文本生成模型">
        设置 `capabilities=TG`，查询文本生成模型。示例查询第一页，递增 `page_no` 可获取后续页面。

        ```bash
        curl --get "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models" \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header "Content-Type: application/json" \
            --data-urlencode "capabilities=TG" \
            --data-urlencode "page_no=1" \
            --data-urlencode "page_size=20"
        ```
      </Tab>

      <Tab title="查询 Qwen 系列的推理模型">
        设置 `providers=qwen` 和 `capabilities=Reasoning`，查询 Qwen 系列的推理模型。示例查询第一页，递增 `page_no` 可获取后续页面。

        ```bash
        curl --get "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/models" \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header "Content-Type: application/json" \
            --data-urlencode "providers=qwen" \
            --data-urlencode "capabilities=Reasoning" \
            --data-urlencode "page_no=1" \
            --data-urlencode "page_size=100"
        ```
      </Tab>
    </Tabs>
  </Column>
</Columns>

<Columns cols={2}>
  <Column>
    ## 返回参数 <span id="a1b2c3d4e5f300" />

    <strong>request\_id</strong> · `String`

    请求 ID，用于问题排查。

    <strong>output.total</strong> · `Number`

    符合条件的模型总数。

    <strong>output.page\_no</strong> · `Number`

    当前页码。

    <strong>output.page\_size</strong> · `Number`

    每页条数。

    <Accordion title="output.models[]">
      <strong>model</strong> · `String`

      模型 ID，用于 API 调用时指定模型。

      <strong>name</strong> · `String`

      模型的名称。

      <strong>description</strong> · `String`

      模型的功能描述。

      <strong>provider</strong> · `String`

      模型作者，如 `qwen`。

      <strong>inference\_provider</strong> · `String`

      推理服务供应商，如 `aliyun-bailian`。

      <strong>capabilities</strong> · `Array[String]`

      模型支持的模态类型，取值与请求参数 `capabilities` 一致。

      <strong>features</strong> · `Array[String]`

      模型支持的能力列表，取值与请求参数 `features` 一致。

      <strong>published\_time</strong> · `String`

      模型发布时间，格式：`yyyy-MM-dd HH:mm:ss`。可能为 `null`。

      <strong>inference\_metadata</strong> · `Object`

      模型的输入输出模态信息，包含以下字段：

      - `request_modality`（Array\[String]）：输入模态，可选值为 `Text`、`Image`、`Audio`、`Video`。
      - `response_modality`（Array\[String]）：输出模态，可选值同上。

      <strong>model\_info</strong> · `Object`

      模型的上下文长度信息，包含以下字段（值为 `null` 表示无限制或不适用）：

      - `context_window`（Integer）：上下文窗口总长度
      - `max_input_tokens`（Integer）：最大输入 Token 数
      - `max_output_tokens`（Integer）：最大输出 Token 数
      - `max_reasoning_tokens`（Integer）：最大推理 Token 数
      - `reasoning_max_input_tokens`（Integer）：最大输入（思考）
      - `reasoning_max_output_tokens`（Integer）：最大输出（思考）

      <strong>prices</strong> · `Array[Object]`

      模型定价信息。每个元素包含：

      - `range_name`（String）：分段名称。如无分段计费则为 `Default`，如有分段则为具体区间（如 `32k<Input<=128k`）。
      - `prices`（Array\[Object]）：价格列表，每项包含 `type`（计费项）、`price`（单价）、`price_unit`（单位，如"每百万tokens"）、`price_name`（价格描述，如"输入"）。

      <strong>equivalent\_snapshot</strong> · `String`

      对应的快照模型

      <strong>inference\_offline\_info</strong> · `Object`

      推理场景下线信息

      <strong>inference\_offline\_info.\$.offline\_time</strong> · `Date`

      预计下线时间
    </Accordion>
  </Column>

  <Column>
    ## 返回示例 <span id="a1b2c3d4e5f500" />

    以下示例用于展示返回结构，模型信息和价格以实际返回结果为准。

    ```json expandable
    {
        "code": null,
        "message": null,
        "success": true,
        "output": {
            "total": 168,
            "page_no": 1,
            "page_size": 10,
            "models": [
                {
                    "model": "qwen3.8-max",
                    "name": "通义千问3.8-Max",
                    "description": "通义千问3系列Max模型，适配场景更加复杂的智能体需求。",
                    "features": [
                        "function-calling",
                        "structured-outputs",
                        "web-search"
                    ],
                    "prices": [
                        {
                            "prices": [
                                {
                                    "type": "input_token",
                                    "price": "2",
                                    "price_unit": "每百万tokens",
                                    "price_name": "输入"
                                },
                                {
                                    "type": "output_token",
                                    "price": "8",
                                    "price_unit": "每百万tokens",
                                    "price_name": "输出"
                                }
                            ],
                            "range_name": "Default"
                        }
                    ],
                    "published_time": "2025-11-11 12:00:00",
                    "inference_metadata": {
                        "response_modality": ["Text"],
                        "request_modality": ["Text"]
                    },
                    "model_info": {
                        "context_window": 131072,
                        "max_input_tokens": 130048,
                        "max_output_tokens": 16384,
                        "max_reasoning_tokens": null
                    }
                },
                {
                    "model": "qwen-image-3.0-pro",
                    "name": "Qwen-Image-3.0-Pro",
                    "description": "通义千问图像生成模型Max系列。",
                    "features": ["model-experience"],
                    "prices": [
                        {
                            "prices": [
                                {
                                    "type": "image_number",
                                    "price": "0.075",
                                    "price_unit": "每张",
                                    "price_name": "图像生成"
                                }
                            ],
                            "range_name": "Default"
                        }
                    ],
                    "published_time": null,
                    "inference_metadata": {
                        "response_modality": ["Image"],
                        "request_modality": ["Text"]
                    },
                    "model_info": {
                        "context_window": null,
                        "max_input_tokens": null,
                        "max_output_tokens": null,
                        "max_reasoning_tokens": null
                    }
                }
            ]
        },
        "request_id": "d5f5201f-ee7a-9e3d-8569-bc0eedec21f9"
    }
    ```
  </Column>
</Columns>

## 错误码 <span id="a1b2c3d4e5f600" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
