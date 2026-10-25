> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 创建响应

> 通过兼容 OpenAI 格式的 Responses API 调用千问模型，查看输入输出参数说明及调用示例。

<meta property="og:title" content="创建响应" />

<strong>相较于OpenAI Chat Completions API 的优势：</strong>

- <strong>内置工具</strong>：内置联网搜索、网页抓取、代码解释器、文搜图、图搜图、知识库搜索等工具，可在处理复杂任务时获得更优效果，详情参考[工具调用](/zh/model-studio/tool-calls)。
- <strong>更灵活的输入</strong>：支持直接传入字符串作为模型输入，也兼容 Chat 格式的消息数组。
- <strong>简化上下文管理</strong>：通过传递上一轮响应的 `previous_response_id`，无需手动构建完整的消息历史数组。
- <strong>便捷的上下文缓存</strong>：只需在请求头中添加 `x-dashscope-session-cache: enable`（默认值为 disable），服务端即可自动缓存对话上下文，无需改动业务代码即可降低多轮对话的推理延迟与成本，详情参考[Session 缓存](/zh/model-studio/compatibility-with-openai-responses-api#example-session-cache-title)。

## 兼容性说明与限制 <span id="0a0bff578cg0f" />

本 API 在接口设计上兼容 OpenAI，以降低开发者迁移成本，但在参数、功能和具体行为上存在差异。

<strong>核心原则：</strong>请求将仅处理本文档明确列出的参数，任何未提及的 OpenAI 参数都会被忽略。

以下是几个关键的差异点，以帮助您快速适配：

- <strong>部分参数不支持</strong>：不支持部分 OpenAI Responses API 参数，例如异步执行参数`background`（当前仅支持同步调用）等。
- <strong>思考强度控制</strong>：通过 `reasoning.effort` 参数控制模型的思考强度，具体用法请参考相应参数的说明。
- <strong>上下文限制</strong>：为预留内置工具调用与推理生成空间，Responses API 的最大输入上下文约为模型窗口大小的 80%（预留约 20% 缓冲区），超出部分将自动触发截断，不会报错中断。

<Tabs>
  <Tab title="华北2（北京）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses`
  </Tab>

  <Tab title="新加坡">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/responses`
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/responses`
  </Tab>

  <Tab title="德国（法兰克福）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/compatible-mode/v1/responses`
  </Tab>

  <Tab title="日本（东京）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1/responses`
  </Tab>

  <Tab title="中国香港">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.cn-hongkong.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.cn-hongkong.maas.aliyuncs.com/compatible-mode/v1/responses`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tip>
  阿里云百炼为华北2（北京）、新加坡、中国香港地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

<Tip>
  OpenAI兼容-Responses API 的旧版URL路径 `/api/v2/apps/protocols/compatible-mode/v1/responses` 已经停止维护，将不再保证功能可用性，请尽快迁移至新版路径 `/compatible-mode/v1/responses`。
</Tip>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## 请求体 <span id="f8eeeafac1c22" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。

        <Accordion title="支持的模型">
          `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`、`qwen3.8-omni-flash`、`qwen3.7-max`、`qwen3.7-max-2026-05-20`、`qwen3.7-max-2026-06-08`、`qwen3.7-max-2026-05-17`、`qwen3.7-max-preview`、`qwen3.7-plus`、`qwen3.7-plus-2026-05-26`、`qwen3.6-plus`、`qwen3.6-plus-2026-04-02`、`qwen3.5-plus`、`qwen3.5-plus-2026-04-20`、`qwen3.5-plus-2026-02-15`、`qwen3.7-flash`、`qwen3.7-flash-2026-07-15`、`qwen3.6-flash`、`qwen3.6-flash-2026-04-16`、`qwen3.5-flash`、`qwen3.5-flash-2026-02-23`、`qwen3.6-35b-a3b`、`qwen3.5-397b-a17b`、`qwen3.5-122b-a10b`、`qwen3.5-27b`、`qwen3.5-35b-a3b`、`deepseek-v4.1-flash`、`deepseek-v4.1-flash-1017`、`deepseek-v4-pro`、`deepseek-v4-pro-0813`、`deepseek-v4-flash-0731`、`glm-5.3`、`glm-5.2`、`kimi-k3`

          <Tip>
            非列表中阿里云百炼直供文本生成模型仅支持基础兼容能力，Agent 能力（内置工具等）受限。
          </Tip>
        </Accordion>

        <strong>input</strong> `string 或 array` <strong>（必选）</strong>

        模型输入，支持以下格式：

        - `string`：纯文本，如 `"你好"`。
        - `array`：消息数组，按对话顺序排列。

        <Accordion title="array 输入项类型">
          <strong>EasyInputMessage</strong> `object`

          通过 role 区分消息类型，通过content传递消息内容。

          <Accordion title="属性">
            <strong>role</strong> `string` <strong>（必选）</strong>

            消息角色，可选值：`user`、`assistant`、`system`、`developer`。

            <strong>content</strong> `string 或 array` <strong>（必选）</strong>

            消息内容。若输入为纯文本，则为 string 类型；若输入为结构化内容数组，则为 array 类型。role 为 `system`/`developer` 时，array 元素类型为 `input_text`；role 为 `user` 时，array 元素类型为 `input_text`、`input_image` 或 `input_file`，`qwen3.8-omni-flash` 还支持下文所述的 `input_audio` 和 `input_video`；role 为 `assistant` 时，array 元素类型为 `output_text`。

            <Accordion title="content 数组元素">
              <strong>type</strong> `string` <strong>（必选）</strong>

              内容类型。可选值：`input_text`（文本输入）、`input_image`（图片输入，仅 user 角色）、`input_file`（文件输入，仅 user 角色，支持 PDF 和图片）、`output_text`（助手回复，仅 assistant 角色）。`qwen3.8-omni-flash` 的 user 消息还支持 `input_audio` 和 `input_video`，字段说明见下文。

              <strong>text</strong> `string`

              文本内容。当 type 为 `input_text` 或 `output_text` 时必填。

              <strong>image\_url</strong> `string`

              支持 URL 或者 Base64 编码，当 type 为 `input_image` 时必填。Base64 请传入完整的 Data URI，例如：`data:image/png;base64,iVBORw0K...`。

              <strong>file\_url</strong> `string`

              文件的公网 URL。当 type 为 `input_file` 时必填。支持 PDF 文件（最大 100 MB）和图片文件（最大 20 MB）。目前 `qwen3.8-flash`、`qwen3.8-max`、`qwen3.5-ocr` 支持此类型。

              PDF 的页数上限取决于 `ocr_options` 中的 `task`：设置为 `document_parsing` 时最大 50 页；未设置 `task` 或设置为其他任务时，最大 10 页。

              <strong>音视频输入字段</strong>

              以下 `input_audio`、`input_video` 类型仅适用于 `qwen3.8-omni-flash` 的 `user` 消息。字段直接放在 content 数组元素中：

| type          | 字段                            | 说明                                                                                                                                                 |
| ------------- | ----------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------- |
| `input_audio` | `format`、`audio_url` 或 `data` | `format` 必填，例如 `wav` 或 `mp3`。`audio_url` 为音频 URL，`data` 为包含 MIME 类型前缀和 Base64 编码音频的完整 Data URI，例如 `data:audio/wav;base64,UklGR...`，两者必须二选一，不能同时传入。 |
| `input_video` | `video_url`、`fps`             | `video_url` 必填，传入视频 URL。`fps` 为可选的数字类型（`number`）抽帧参数，表示每秒抽取的帧数，例如 `2` 表示每秒抽取 2 帧，不要传字符串。                                                           |

              <strong>use\_multichannel</strong> `boolean`（可选），默认 `false`。设为 `true` 时，对双通道或四通道音频进行空间音频解析；设为 `false` 时，按单通道音频解析。在 `type="input_audio"` 的 content 数组元素中传入，与 `type`、`audio_url`、`format` 同级。

              音视频输入沿用普通文本响应格式和流式事件，流式文本使用 `response.output_text.delta`、`response.output_text.done`。

              音视频输入、文本输出示例见[Qwen3.8-Omni-Flash](/zh/model-studio/qwen-omni#qwen38-offline)。
            </Accordion>

            <strong>type</strong> `string` （可选）

            固定为 `message`。
          </Accordion>

          <strong>ResponseOutputMessage</strong> `object` （可选）

          模型的输出消息对象。可直接将上一轮响应的 output 中的 message 项传回 input，用于多轮对话场景。与 EasyInputMessage 的区别在于它携带了完整的输出结构（含 id、status 和结构化 content）。

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为 `message`。

            <strong>id</strong> `string` <strong>（必选）</strong>

            输出消息的唯一标识，来自上一轮响应。

            <strong>role</strong> `string` <strong>（必选）</strong>

            固定为 `assistant`。

            <strong>status</strong> `string` <strong>（必选）</strong>

            消息状态，可选值：`in_progress`、`completed`、`incomplete`。

            <strong>content</strong> `array` <strong>（必选）</strong>

            内容数组，元素为 output\_text 类型对象。

            <Accordion title="属性">
              <strong>type</strong> `string` <strong>（必选）</strong>

              固定为 `output_text`。

              <strong>text</strong> `string` <strong>（必选）</strong>

              回复文本。

              <strong>annotations</strong> `array` （可选）

              标注信息。
            </Accordion>
          </Accordion>

          <strong>Function Call</strong> `object` （可选）

          模型决定调用外部工具时生成的结构化指令。

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为 `function_call`。

            <strong>id</strong> `string` （可选）

            Function Call 的唯一标识，来自上一轮响应。

            <strong>name</strong> `string` <strong>（必选）</strong>

            工具函数名称。

            <strong>arguments</strong> `string` <strong>（必选）</strong>

            工具调用参数，JSON 字符串格式。

            <strong>call\_id</strong> `string` <strong>（必选）</strong>

            工具调用的标识符，需与模型返回的 `call_id` 一致。

            <strong>status</strong> `string` （可选）

            状态，可选值：`in_progress`、`completed`、`incomplete`。
          </Accordion>

          <strong>Function Call Output</strong> `object` （可选）

          工具调用的输出结果。在消息列表中<strong>必须</strong>紧跟对应的 `function_call` 消息，否则会报错。

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为 `function_call_output`。

            <strong>id</strong> `string` （可选）

            Function Call Output 的唯一标识。

            <strong>call\_id</strong> `string` <strong>（必选）</strong>

            工具调用的标识符，需与模型返回的 `call_id` 一致。

            <strong>output</strong> `string 或 array` <strong>（必选）</strong>

            工具函数的执行结果。纯文本结果传入 string；需要模型理解工具产出的图片时传入 array，一个 output 中可包含多张图片。图片 URL 作为 string 传入时按普通文本处理，不会被解析为图片。

            <Accordion title="output 数组元素">
              <strong>type</strong> `string` <strong>（必选）</strong>

              内容类型。可选值：`input_text`（文本）、`input_image`（图片）。不支持 `input_file`、`output_text` 以及音频、视频类型。

              <strong>text</strong> `string`

              文本内容。当 type 为 `input_text` 时必填。

              <strong>image\_url</strong> `string`

              支持 URL 或者 Base64 编码，当 type 为 `input_image` 时必填。Base64 请传入完整的 Data URI，例如：`data:image/png;base64,iVBORw0K...`。此处为 string 类型，不支持 `{"url": "..."}` 嵌套对象形式。
            </Accordion>

            <strong>status</strong> `string` （可选）

            状态，可选值：`in_progress`、`completed`、`incomplete`。
          </Accordion>

          <strong>Reasoning</strong> `object` （可选）

          模型的思考内容。可直接将上一轮响应的 output 中的 reasoning 项传回 input，用于在多轮对话中传递思考内容。

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为 `reasoning`。

            <strong>id</strong> `string` <strong>（必选）</strong>

            思考内容的唯一标识，来自上一轮响应。

            <strong>summary</strong> `array` <strong>（必选）</strong>

            思考摘要内容。

            <Accordion title="属性">
              <strong>type</strong> `string` <strong>（必选）</strong>

              固定为 `summary_text`。

              <strong>text</strong> `string` <strong>（必选）</strong>

              摘要文本。
            </Accordion>

            <strong>status</strong> `string` （可选）

            状态，可选值：`in_progress`、`completed`、`incomplete`。
          </Accordion>

          <strong>Web Search Call</strong> `object` （可选）

          搜索调用对象。可直接将上一轮响应的 output 中的 web\_search\_call 项传回 input，用于在多轮对话中传递搜索结果上下文。

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为 `web_search_call`。

            <strong>id</strong> `string` <strong>（必选）</strong>

            搜索调用的唯一标识，来自上一轮响应。

            <strong>status</strong> `string` <strong>（必选）</strong>

            搜索状态，可选值：`in_progress`、`searching`、`completed`、`failed`。

            <strong>action</strong> `object` <strong>（必选）</strong>

            搜索信息。仅支持 `search` 类型。

            <Accordion title="属性">
              <strong>type</strong> `string` <strong>（必选）</strong>

              搜索类型，固定为 `search`。

              <strong>queries</strong> `array` （可选）

              搜索查询词列表，元素类型为 string。

              <strong>sources</strong> `array` （可选）

              搜索结果来源列表。

              <Accordion title="属性">
                <strong>type</strong> `string` <strong>（必选）</strong>

                来源类型，固定为 `url`。

                <strong>url</strong> `string` <strong>（必选）</strong>

                来源 URL。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>instructions</strong>`string` （可选）

        作为系统指令插入到上下文的起始位置。使用 `previous_response_id` 时，上一轮指定的 `instructions` 不会传入本轮上下文。

        <strong>previous\_response\_id</strong> `string` （可选）

        上一个响应的唯一 ID，当前响应`id`有效期为7天。使用此参数可创建多轮对话，服务端会自动检索并组合该轮次的输入与输出作为上下文。当同时提供 `input` 消息数组和 `previous_response_id` 时，`input` 中的新消息会追加到历史上下文之后。不能与 `conversation` 同时使用。

        <strong>conversation</strong> `string` （可选）

        当前响应所属的会话（参考[Conversations API](/zh/model-studio/openai-compatible-conversations)）。会话中的历史项会自动作为上下文传入本次请求，本次请求的输入和输出也会在响应完成后自动添加到会话中。不能与 `previous_response_id` 同时使用。

        <strong>stream</strong> `boolean` （可选）默认值为 `false`

        是否开启流式输出。设置为 `true` 时，模型响应数据将实时流式返回给客户端。

        <strong>store</strong> `boolean` （可选）默认值为 `true`

        是否储存本次会话生成的模型响应。

        - `false`：不储存，对话内容不能被 `previous_response_id` 和后续 API 使用。
        - `true`：储存，当前模型响应可被 `previous_response_id` 和后续 API 使用。

        <strong>tools</strong> `array` （可选）

        模型在生成响应时可调用的工具数组。支持内置工具和自定义 function 工具，可混合使用。

        `deepseek-v4.1-flash-1017` 支持 `web_search`、`web_extractor` 和 `code_interpreter` 三项内置工具。

        `qwen3.8-omni-flash` 的内置工具仅支持 `web_search`，同时支持自定义 `function` 工具。

        <Accordion title="属性">
          <strong>web\_search</strong>

          联网搜索工具，允许模型搜索互联网上的最新信息。相关文档：[联网搜索](/zh/model-studio/web-search)

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为`web_search`。

            使用示例：`[{"type": "web_search"}]`
          </Accordion>

          <strong>web\_extractor</strong>

          网页抽取工具，允许模型访问并提取网页内容。当前必须配合`web_search`工具一起使用。相关文档：[网页抓取](/zh/model-studio/web-extractor)

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为`web_extractor`。

            使用示例：`[{"type": "web_search"}, {"type": "web_extractor"}]`
          </Accordion>

          <strong>code\_interpreter</strong>

          代码解释器工具，允许模型执行代码并返回结果，支持数据分析。`qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.7-max`、`qwen3.7-max-2026-05-20`、`qwen3.7-max-2026-06-08`需要同时开启思考模式。相关文档：[代码解释器](/zh/model-studio/qwen-code-interpreter)

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为`code_interpreter`。

            使用示例：`[{"type": "code_interpreter"}]`
          </Accordion>

          <strong>web\_search\_image</strong>

          根据文本描述搜索图片。相关文档：[文搜图](/zh/model-studio/web-search-image)

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为`web_search_image`。

            使用示例：`[{"type": "web_search_image"}]`
          </Accordion>

          <strong>image\_search</strong>

          根据图片搜索相似或相关图片，输入中需要包含图片的URL。相关文档：[图搜图](/zh/model-studio/image-search)

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为`image_search`。

            使用示例：`[{"type": "image_search"}]`
          </Accordion>

          <strong>file\_search</strong>

          在已上传或关联的知识库中搜索。相关文档：[知识检索](/zh/model-studio/file-search)

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为`file_search`。

            <strong>vector\_store\_ids</strong> `array`<strong>（必选）</strong>

            要检索的知识库 ID。<strong>当前仅支持传入一个知识库 ID</strong>。

            使用示例：`[{"type": "file_search", "vector_store_ids": ["your_knowledge_base_id"]}]`
          </Accordion>

          <strong>MCP调用</strong>

          通过 MCP（Model Context Protocol）调用外部服务，相关文档：[MCP](/zh/model-studio/mcp)

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            固定为`mcp`。

            <strong>server\_protocol</strong> `string` <strong>（必选）</strong>

            与 MCP 服务的通信协议，如 `"sse"`

            <strong>server\_label</strong> `string` <strong>（必选）</strong>

            服务标签，用于标识该 MCP 服务。

            <strong>server\_description</strong> `string` （可选）

            服务描述，帮助模型理解其功能与适用场景。

            <strong>server\_url</strong> `string` <strong>（必选）</strong>

            MCP 服务端点的 URL。

            <strong>headers</strong> `object` （可选）

            请求头，用于携带身份验证等信息，如 `Authorization`。

            使用示例：

            ```json
            mcp_tool = {
                "type": "mcp",
                "server_protocol": "sse",
                "server_label": "amap-maps",
                "server_description": "高德地图MCP Server现已覆盖15大核心接口，提供全场景覆盖的地理信息服务，包括生成专属地图、导航到目的地、打车、地理编码、逆地理编码、IP定位、天气查询、骑行路径规划、步行路径规划、驾车路径规划、公交路径规划、距离测量、关键词搜索、周边搜索、详情搜索等。",
                "server_url": "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/mcps/amap-maps/sse",
                "headers": {
                    "Authorization": "Bearer <your-mcp-server-token>"
                }
            }
            ```
          </Accordion>

          <strong>自定义工具</strong> <strong>function</strong>

          自定义函数工具，允许模型调用您定义的函数。当模型判断需要调用工具时，响应会返回 `function_call` 类型的输出。相关文档：[Function Calling](/zh/model-studio/qwen-function-calling)

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            必须设置为`function`。

            <strong>name</strong>`string`<strong>（必选）</strong>

            工具名称。仅允许字母、数字、下划线（`_`）和短划线（`-`），最长 64 个 Token。

            <strong>description</strong>`string`<strong>（必选）</strong>

            工具描述信息，帮助模型判断何时以及如何调用该工具。

            <strong>parameters</strong> `object` （可选）

            工具的参数描述，需要是一个合法的 [JSON Schema](https://json-schema.org/understanding-json-schema)。若`parameters`参数为空，表示该工具没有入参（如时间查询工具）。

            > 为提高工具调用的准确性，建议传入 `parameters`。

            使用示例：

            ```json
            [{
              "type": "function",
              "name": "get_weather",
              "description": "获取指定城市的天气信息",
              "parameters": {
                "type": "object",
                "properties": {
                  "city": {
                    "type": "string",
                    "description": "城市名称"
                  }
                },
                "required": ["city"]
              }
            }]
            ```
          </Accordion>
        </Accordion>

        <strong>tool\_choice</strong> `string or object` （可选）默认值为 `auto`

        控制模型如何选择和调用工具。此参数支持两种赋值格式：<strong>字符串模式</strong>和<strong>对象模式</strong>。

        <strong>字符串模式</strong>

        - `auto`：模型自动决定是否调用工具。
        - `none`：禁止模型调用任何工具。
        - `required`：强制模型调用工具（仅当 `tools` 列表中只有一个工具时可用）。

        <strong>对象模式</strong>

        为模型设定可用的工具范围，仅限在预定义的工具列表中进行选择和调用。

        <Accordion title="属性">
          <strong>mode</strong> `string` <strong>（必选）</strong>

          - `auto`：模型自动决定是否调用工具。
          - `required`：强制模型调用工具（仅当 `tools` 列表中只有一个工具时可用）。

          <strong>tools</strong> `array`<strong>（必选）</strong>

          一个包含工具定义的列表，模型将被允许调用这些工具。

          ```json
          [
            { "type": "function", "name": "get_weather" }
          ]
          ```

          <strong>type</strong>`string` <strong>（必选）</strong>

          允许的工具配置类型，固定为 `allowed_tools`。
        </Accordion>

        <strong>parallel\_tool\_calls</strong> `boolean` （可选）默认值为 `true`

        是否开启并行工具调用。相关文档：[并行工具调用](/zh/model-studio/qwen-function-calling#cb6b5c484bt4x)

        > kimi-k3 不支持通过此参数控制工具调用的并行性。设置为 `false` 也不能保证串行调用，模型仍可能在一次响应中返回多个 `function_call`。请按实际返回结果处理。

        可选值：

        - `true`：开启
        - `false`：不开启

        <strong>temperature</strong>`float`（可选）

        采样温度，控制模型生成文本的多样性。

        temperature越高，生成的文本更多样，反之，生成的文本更确定。

        取值范围： \[0, 2)

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。更多说明，请参见[概述](/zh/model-studio/text-generation)。

        <strong>top\_p</strong>`float`（可选）

        核采样的概率阈值，控制模型生成文本的多样性。

        top\_p越高，生成的文本更多样。反之，生成的文本更确定。

        取值范围：（0,1.0]

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。更多说明，请参见[概述](/zh/model-studio/text-generation)。

        <strong>enable\_thinking</strong> `boolean` （可选）

        是否开启思考模式。开启后，模型会在回复前进行思考，思考内容将通过 `reasoning` 类型的输出项返回。开启思考模式时，<strong>建议开启</strong>内置工具，以在处理复杂任务时获得最佳的模型效果。

        可选值：

        - `true`：开启
        - `false`：不开启

        不同模型的默认值：[支持的模型](/zh/model-studio/deep-thinking#78286fdc35hlw)

        > 该参数非OpenAI标准参数。Python SDK 通过 `extra_body={"enable_thinking": True}` 传递；Node.js SDK 和 curl 直接使用 `enable_thinking: true` 作为顶层参数。建议使用 `reasoning.effort` 替代，`enable_thinking` 后续将不再支持。

        <strong>reasoning</strong> `object` （可选）

        控制模型的思考强度。模型会在回复前进行思考，思考内容将通过 `reasoning` 类型的输出项返回。

        <Accordion title="属性">
          <strong>effort</strong> `string` （可选）：思考强度档位。

          支持 `none`、`minimal`、`low`、`medium`、`high`、`xhigh`、`max` 共 7 个递增档位。降低该值可加快响应速度并减少推理 Token 的消耗。

| 模型                                                                                 | 默认值     | 支持的档位                         | 其余档位的映射                                                            |
| ---------------------------------------------------------------------------------- | ------- | ----------------------------- | ------------------------------------------------------------------ |
| `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b` | `xhigh` | `none`、`low`、`medium`、`xhigh` | `minimal` 映射为 `low`，`high` 和 `max` 映射为 `xhigh`                     |
| `deepseek-v4-flash-0731`、`deepseek-v4-pro-0813`                                    | `high`  | `none`、`low`、`high`、`max`     | `minimal` 映射为 `low`，`medium` 映射为 `high`，`xhigh` 映射为 `max`          |
| `deepseek-v4.1-flash`                                                              | `high`  | `none`、`low`、`high`、`max`     | `minimal` 映射为 `low`，`medium` 和 `xhigh` 映射为 `high`，传入 `ultra` 会返回错误 |
| `glm-5.3`                                                                          | `max`   | `low`、`high`、`max`            | `minimal` 映射为 `low`，`medium` 映射为 `high`，`xhigh` 映射为 `max`          |
| `glm-5.2`、`deepseek-v4-pro`                                                        | `high`  | `none`、`high`、`max`           | `minimal`、`low` 和 `medium` 映射为 `high`，`xhigh` 映射为 `max`            |

          `glm-5.3` 推荐使用 `max` 档位。
        </Accordion>

        > `reasoning.effort` 的优先级高于 `enable_thinking`，建议优先使用 `reasoning.effort`，`enable_thinking` 后续将不再支持。

        <strong>ocr\_options</strong> `object` （可选）

        OCR 定制任务参数。仅适用于 `qwen3.5-ocr` 模型。通过此参数可调用内置的 OCR 任务（如信息抽取、文字定位等），定制任务结果通过响应中的 `ocr_result` 字段返回。

        解析 PDF 文件时，`task` 的取值决定支持的页数：设置为 `document_parsing` 时最大 50 页；未设置 `task` 或设置为其他任务时，最大 10 页。

        > 该参数非 OpenAI 标准参数。Python SDK 通过 `extra_body={"ocr_options": {...}}` 传递；Node.js SDK 和 curl 直接使用 `ocr_options` 作为顶层参数。

        <strong>max\_output\_tokens</strong> `integer`（可选）

        限制生成输出的 Token 数量。

        - Qwen3.8 系列：模型回复内容和思维链内容之和的最大Token数。
        - Qwen3.7、Qwen3.6、Qwen3.5、Qwen3-Max、DeepSeek、GLM 和 Kimi：模型回复内容的最大Token数。

        上述型号的最小值为16。生成的 Token 数达到所设置的上限时，生成将提前停止，状态为`incomplete`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Tabs>
          <Tab title="基础调用">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )

              response = client.responses.create(
                  model="qwen3.8-max",
                  input="你能做些什么？"
              )

              # 获取模型回复
              print(response.output_text)
              ```

              ```javascript Node.js
              import OpenAI from "openai";

              const openai = new OpenAI({
                  // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx"
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
              });

              async function main() {
                  const response = await openai.responses.create({
                      model: "qwen3.8-max",
                      input: "你能做些什么？"
                  });

                  // 获取模型回复
                  console.log(response.output_text);
              }

              main();
              ```

              ```bash curl
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                  "model": "qwen3.8-max",
                  "input": "你能做些什么？"
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="流式输出">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )

              stream = client.responses.create(
                  model="qwen3.8-max",
                  input="请简单介绍一下人工智能。",
                  stream=True
              )

              print("开始接收流式输出:")
              for event in stream:
                  if event.type == 'response.output_text.delta':
                      print(event.delta, end='', flush=True)
                  elif event.type == 'response.completed':
                      print("\n流式输出完成")
                      print(f"总Token数: {event.response.usage.total_tokens}")
              ```

              ```javascript Node.js expandable
              import OpenAI from "openai";

              const openai = new OpenAI({
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
              });

              async function main() {
                  const stream = await openai.responses.create({
                      model: "qwen3.8-max",
                      input: "请简单介绍一下人工智能。",
                      stream: true
                  });

                  console.log("开始接收流式输出:");
                  for await (const event of stream) {
                      if (event.type === 'response.output_text.delta') {
                          process.stdout.write(event.delta);
                      } else if (event.type === 'response.completed') {
                          console.log("\n流式输出完成");
                          console.log(`总Token数: ${event.response.usage.total_tokens}`);
                      }
                  }
              }

              main();
              ```

              ```bash curl
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              --no-buffer \
              -d '{
                  "model": "qwen3.8-max",
                  "input": "请简单介绍一下人工智能。",
                  "stream": true
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="多轮对话">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )

              # 第一轮对话
              response1 = client.responses.create(
                  model="qwen3.8-max",
                  input="我的名字是张三，请记住。"
              )
              print(f"第一轮回复: {response1.output_text}")

              # 第二轮对话 - 使用 previous_response_id 关联上下文，响应id有效期为7天
              response2 = client.responses.create(
                  model="qwen3.8-max",
                  input="你还记得我的名字吗？",
                  previous_response_id=response1.id
              )
              print(f"第二轮回复: {response2.output_text}")
              ```

              ```javascript Node.js
              import OpenAI from "openai";

              const openai = new OpenAI({
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
              });

              async function main() {
                  // 第一轮对话
                  const response1 = await openai.responses.create({
                      model: "qwen3.8-max",
                      input: "我的名字是张三，请记住。"
                  });
                  console.log(`第一轮回复: ${response1.output_text}`);

                  // 第二轮对话 - 使用 previous_response_id 关联上下文，响应id有效期为7天
                  const response2 = await openai.responses.create({
                      model: "qwen3.8-max",
                      input: "你还记得我的名字吗？",
                      previous_response_id: response1.id
                  });
                  console.log(`第二轮回复: ${response2.output_text}`);
              }

              main();
              ```
            </CodeGroup>
          </Tab>

          <Tab title="调用内置工具">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )

              response = client.responses.create(
                  model="qwen3.8-max",
                  input="帮我找一下阿里云官网，并提取首页的关键信息",
                  # 建议同时开启内置工具以取得最佳效果
                  tools=[
                      {"type": "web_search"},
                      {"type": "code_interpreter"},
                      {"type": "web_extractor"}
                  ],
              )

              # 取消以下注释查看中间过程输出
              # print(response.output)
              print(response.output_text)
              ```

              ```javascript Node.js expandable
              import OpenAI from "openai";

              const openai = new OpenAI({
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
              });

              async function main() {
                  const response = await openai.responses.create({
                      model: "qwen3.8-max",
                      input: "帮我找一下阿里云官网，并提取首页的关键信息",
                      tools: [
                          { type: "web_search" },
                          { type: "code_interpreter" },
                          { type: "web_extractor" }
                      ]
                  });

                  for (const item of response.output) {
                      if (item.type === "reasoning") {
                          console.log("模型正在思考...");
                      } else if (item.type === "web_search_call") {
                          console.log(`搜索查询: ${item.action.query}`);
                      } else if (item.type === "web_extractor_call") {
                          console.log("正在抽取网页内容...");
                      } else if (item.type === "message") {
                          console.log(`回复内容: ${item.content[0].text}`);
                      }
                  }
              }

              main();
              ```

              ```bash curl
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                  "model": "qwen3.8-max",
                  "input": "帮我找一下阿里云官网，并提取首页的关键信息",
                  "tools": [
                      {
                          "type": "web_search"
                      },
                      {
                          "type": "code_interpreter"
                      },
                      {
                          "type": "web_extractor"
                      }
                  ]
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="自定义 Function Call">
            <CodeGroup dropdown>
              ```python Python expandable
              from openai import OpenAI
              import json
              import os
              import random

              # 初始化客户端
              client = OpenAI(
                  # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )
              # 模拟用户问题
              USER_QUESTION = "北京天气咋样"
              # 定义工具列表
              tools = [
                  {
                      "type": "function",
                      "name": "get_current_weather",
                      "description": "当你想查询指定城市的天气时非常有用。",
                      "parameters": {
                          "type": "object",
                          "properties": {
                              "location": {
                                  "type": "string",
                                  "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                              }
                          },
                          "required": ["location"],
                      },
                  }
              ]

              # 模拟天气查询工具
              def get_current_weather(arguments):
                  weather_conditions = ["晴天", "多云", "雨天"]
                  random_weather = random.choice(weather_conditions)
                  location = arguments["location"]
                  return f"{location}今天是{random_weather}。"

              # 封装模型响应函数
              def get_response(input_data):
                  response = client.responses.create(
                      model="qwen3.8-max",
                      input=input_data,
                      tools=tools,
                  )
                  return response

              # 维护对话上下文
              conversation = [{"role": "user", "content": USER_QUESTION}]

              response = get_response(conversation)
              function_calls = [item for item in response.output if item.type == "function_call"]
              # 如果不需要调用工具，直接输出内容
              if not function_calls:
                  print(f"助手最终回复：{response.output_text}")
              else:
                  # 进入工具调用循环
                  while function_calls:
                      for fc in function_calls:
                          func_name = fc.name
                          arguments = json.loads(fc.arguments)
                          print(f"正在调用工具 [{func_name}]，参数：{arguments}")
                          # 执行工具
                          tool_result = get_current_weather(arguments)
                          print(f"工具返回：{tool_result}")
                          # 将工具调用和结果成对追加到上下文中
                          conversation.append(
                              {
                                  "type": "function_call",
                                  "name": fc.name,
                                  "arguments": fc.arguments,
                                  "call_id": fc.call_id,
                              }
                          )
                          conversation.append(
                              {
                                  "type": "function_call_output",
                                  "call_id": fc.call_id,
                                  "output": tool_result,
                              }
                          )
                      # 携带完整上下文再次调用模型
                      response = get_response(conversation)
                      function_calls = [
                          item for item in response.output if item.type == "function_call"
                      ]
                  print(f"助手最终回复：{response.output_text}")
              ```

              ```javascript Node.js expandable
              import OpenAI from "openai";

              // 初始化客户端
              const openai = new OpenAI({
                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey: "sk-xxx",
                apiKey: process.env.DASHSCOPE_API_KEY,
                baseURL:
                  "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              });

              // 定义工具列表
              const tools = [
                {
                  type: "function",
                  name: "get_current_weather",
                  description: "当你想查询指定城市的天气时非常有用。",
                  parameters: {
                    type: "object",
                    properties: {
                      location: {
                        type: "string",
                        description: "城市或县区，比如北京市、杭州市、余杭区等。",
                      },
                    },
                    required: ["location"],
                  },
                },
              ];

              // 模拟天气查询工具
              const getCurrentWeather = (args) => {
                const weatherConditions = ["晴天", "多云", "雨天"];
                const randomWeather =
                  weatherConditions[Math.floor(Math.random() * weatherConditions.length)];
                const location = args.location;
                return `${location}今天是${randomWeather}。`;
              };

              // 封装模型响应函数
              const getResponse = async (inputData) => {
                const response = await openai.responses.create({
                  model: "qwen3.8-max",
                  input: inputData,
                  tools: tools,
                });
                return response;
              };

              const main = async () => {
                const userQuestion = "北京天气";

                // 维护对话上下文
                const conversation = [{ role: "user", content: userQuestion }];

                let response = await getResponse(conversation);
                let functionCalls = response.output.filter(
                  (item) => item.type === "function_call"
                );
                // 如果不需要调用工具，直接输出内容
                if (functionCalls.length === 0) {
                  console.log(`助手最终回复：${response.output_text}`);
                } else {
                  // 进入工具调用循环
                  while (functionCalls.length > 0) {
                    for (const fc of functionCalls) {
                      const funcName = fc.name;
                      const args = JSON.parse(fc.arguments);
                      console.log(`正在调用工具 [${funcName}]，参数：`, args);
                      // 执行工具
                      const toolResult = getCurrentWeather(args);
                      console.log(`工具返回：${toolResult}`);
                      // 将工具调用和结果成对追加到上下文中
                      conversation.push({
                        type: "function_call",
                        name: fc.name,
                        arguments: fc.arguments,
                        call_id: fc.call_id,
                      });
                      conversation.push({
                        type: "function_call_output",
                        call_id: fc.call_id,
                        output: toolResult,
                      });
                    }
                    // 携带完整上下文再次调用模型
                    response = await getResponse(conversation);
                    functionCalls = response.output.filter(
                      (item) => item.type === "function_call"
                    );
                  }
                  console.log(`助手最终回复：${response.output_text}`);
                }
              };

              // 启动程序
              main().catch(console.error);
              ```

              ```bash curl expandable
              # 第一步：提交用户问题与工具定义，模型返回 function_call
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                  "model": "qwen3.8-max",
                  "input": [
                      {
                          "role": "user",
                          "content": "北京天气咋样"
                      }
                  ],
                  "tools": [
                      {
                          "type": "function",
                          "name": "get_current_weather",
                          "description": "当你想查询指定城市的天气时非常有用。",
                          "parameters": {
                              "type": "object",
                              "properties": {
                                  "location": {
                                      "type": "string",
                                      "description": "城市或县区，比如北京市、杭州市、余杭区等。"
                                  }
                              },
                              "required": ["location"]
                          }
                      }
                  ]
              }'

              # 第二步：在本地执行函数后，将 function_call 与 function_call_output 成对追加到 input 中，获取最终回复
              # 请将 call_id 与 arguments 替换为第一步响应中实际返回的值
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                  "model": "qwen3.8-max",
                  "input": [
                      {
                          "role": "user",
                          "content": "北京天气咋样"
                      },
                      {
                          "type": "function_call",
                          "name": "get_current_weather",
                          "arguments": "{\"location\": \"北京市\"}",
                          "call_id": "call_xxx"
                      },
                      {
                          "type": "function_call_output",
                          "call_id": "call_xxx",
                          "output": "北京市今天是晴天。"
                      }
                  ],
                  "tools": [
                      {
                          "type": "function",
                          "name": "get_current_weather",
                          "description": "当你想查询指定城市的天气时非常有用。",
                          "parameters": {
                              "type": "object",
                              "properties": {
                                  "location": {
                                      "type": "string",
                                      "description": "城市或县区，比如北京市、杭州市、余杭区等。"
                                  }
                              },
                              "required": ["location"]
                          }
                      }
                  ]
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="工具返回多模态结果">
            <CodeGroup dropdown>
              ```python Python expandable
              from openai import OpenAI
              import json
              import os

              # 初始化客户端
              client = OpenAI(
                  # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )
              # 模拟用户问题
              USER_QUESTION = "画一张小狗的照片，然后告诉我图里的小狗是什么品种、在什么场景里"
              # 定义工具列表
              tools = [
                  {
                      "type": "function",
                      "name": "generate_image",
                      "description": "当你需要根据文字描述生成图片时非常有用。",
                      "parameters": {
                          "type": "object",
                          "properties": {
                              "prompt": {
                                  "type": "string",
                                  "description": "图片内容的文字描述。",
                              }
                          },
                          "required": ["prompt"],
                      },
                  }
              ]

              # 模拟文生图工具，实际使用时请替换为真实的图片生成调用
              def generate_image(arguments):
                  print(f"正在生成图片，提示词：{arguments['prompt']}")
                  return "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"

              # 封装模型响应函数
              def get_response(input_data):
                  response = client.responses.create(
                      model="qwen3.8-max",
                      input=input_data,
                      tools=tools,
                  )
                  return response

              # 维护对话上下文
              conversation = [{"role": "user", "content": USER_QUESTION}]

              response = get_response(conversation)
              function_calls = [item for item in response.output if item.type == "function_call"]
              # 进入工具调用循环
              while function_calls:
                  for fc in function_calls:
                      arguments = json.loads(fc.arguments)
                      # 执行工具，得到图片地址
                      image_url = generate_image(arguments)
                      # 将工具调用和结果成对追加到上下文中
                      conversation.append(
                          {
                              "type": "function_call",
                              "name": fc.name,
                              "arguments": fc.arguments,
                              "call_id": fc.call_id,
                          }
                      )
                      # output 传入内容数组，模型才能理解图片内容
                      conversation.append(
                          {
                              "type": "function_call_output",
                              "call_id": fc.call_id,
                              "output": [
                                  {"type": "input_text", "text": "图片生成成功。"},
                                  {"type": "input_image", "image_url": image_url},
                              ],
                          }
                      )
                  # 携带完整上下文再次调用模型
                  response = get_response(conversation)
                  function_calls = [item for item in response.output if item.type == "function_call"]
              print(f"助手最终回复：{response.output_text}")
              ```

              ```javascript Node.js expandable
              import OpenAI from "openai";

              // 初始化客户端
              const openai = new OpenAI({
                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey: "sk-xxx",
                apiKey: process.env.DASHSCOPE_API_KEY,
                baseURL:
                  "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              });

              // 定义工具列表
              const tools = [
                {
                  type: "function",
                  name: "generate_image",
                  description: "当你需要根据文字描述生成图片时非常有用。",
                  parameters: {
                    type: "object",
                    properties: {
                      prompt: {
                        type: "string",
                        description: "图片内容的文字描述。",
                      },
                    },
                    required: ["prompt"],
                  },
                },
              ];

              // 模拟文生图工具，实际使用时请替换为真实的图片生成调用
              const generateImage = (args) => {
                console.log(`正在生成图片，提示词：${args.prompt}`);
                return "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg";
              };

              // 封装模型响应函数
              const getResponse = async (inputData) => {
                const response = await openai.responses.create({
                  model: "qwen3.8-max",
                  input: inputData,
                  tools: tools,
                });
                return response;
              };

              const main = async () => {
                const userQuestion = "画一张小狗的照片，然后告诉我图里的小狗是什么品种、在什么场景里";

                // 维护对话上下文
                const conversation = [{ role: "user", content: userQuestion }];

                let response = await getResponse(conversation);
                let functionCalls = response.output.filter(
                  (item) => item.type === "function_call"
                );
                // 进入工具调用循环
                while (functionCalls.length > 0) {
                  for (const fc of functionCalls) {
                    const args = JSON.parse(fc.arguments);
                    // 执行工具，得到图片地址
                    const imageUrl = generateImage(args);
                    // 将工具调用和结果成对追加到上下文中
                    conversation.push({
                      type: "function_call",
                      name: fc.name,
                      arguments: fc.arguments,
                      call_id: fc.call_id,
                    });
                    // output 传入内容数组，模型才能理解图片内容
                    conversation.push({
                      type: "function_call_output",
                      call_id: fc.call_id,
                      output: [
                        { type: "input_text", text: "图片生成成功。" },
                        { type: "input_image", image_url: imageUrl },
                      ],
                    });
                  }
                  // 携带完整上下文再次调用模型
                  response = await getResponse(conversation);
                  functionCalls = response.output.filter(
                    (item) => item.type === "function_call"
                  );
                }
                console.log(`助手最终回复：${response.output_text}`);
              };

              // 启动程序
              main().catch(console.error);
              ```
            </CodeGroup>
          </Tab>

          <Tab title="文档理解">
            <CodeGroup dropdown>
              ```python Python expandable
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )

              response = client.responses.create(
                  model="qwen3.5-ocr",
                  input=[
                      {
                          "role": "user",
                          "content": [
                              {
                                  "type": "input_file",
                                  "file_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260616/qmycjl/1506.02640v5.pdf",
                              },
                              {
                                  "type": "input_text",
                                  "text": "Read all the text in the file.",
                              },
                          ],
                      }
                  ],
                  extra_body={
                      "ocr_options": {"task": "document_parsing"}
                  },
              )

              print(response.output_text)
              ```

              ```javascript Node.js expandable
              import OpenAI from 'openai';

              const client = new OpenAI({
                  // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx"
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              });

              async function main() {
                  const response = await client.responses.create({
                      model: "qwen3.5-ocr",
                      input: [{
                          role: "user",
                          content: [{
                              type: "input_file",
                              file_url: "https://example.com/your-document.pdf"
                          }]
                      }],
                      ocr_options: { task: "document_parsing" }
                  });

                  // 获取定制任务结果
                  console.log(response.output[0].content[0].ocr_result);
              }

              main();
              ```

              ```bash curl
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                  "model": "qwen3.5-ocr",
                  "input": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "type": "input_file",
                                  "file_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260616/qmycjl/1506.02640v5.pdf"
                              },
                              {
                                  "type": "input_text",
                                  "text": "Read all the text in the file."
                              }
                          ]
                      }
                  ],
                  "ocr_options": {"task": "document_parsing"}
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="Session 缓存">
            在请求 Header 中设置 `x-dashscope-session-cache: enable` 开启 Session 缓存，多轮对话通过 `previous_response_id` 关联上下文。详细说明见[Session 缓存](/zh/model-studio/compatibility-with-openai-responses-api#example-session-cache-title)。

            自动生效的隐式缓存另见[上下文缓存](/zh/model-studio/context-cache)。

            <CodeGroup dropdown>
              ```python Python expandable
              import os
              from openai import OpenAI

              client = OpenAI(
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                  # 通过 default_headers 开启 Session 缓存
                  default_headers={"x-dashscope-session-cache": "enable"}
              )

              # 构造超过 1024 Token 的长文本，确保能触发缓存创建（若未达到1024 Token，后续累积对话上下文超过1024 Token时将触发缓存创建）
              long_context = "人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。" * 50

              # 第一轮对话
              response1 = client.responses.create(
                  model="qwen3.8-max",
                  input=long_context + "\n\n基于以上背景知识，请简短介绍机器学习中的随机森林算法。",
              )
              print(f"第一轮回复: {response1.output_text}")

              # 第二轮对话：通过 previous_response_id 关联上下文，缓存由服务端自动处理
              response2 = client.responses.create(
                  model="qwen3.8-max",
                  input="它和 GBDT 有什么主要区别？",
                  extra_body={"previous_response_id": response1.id},
              )
              print(f"第二轮回复: {response2.output_text}")

              # 查看缓存命中情况
              usage = response2.usage
              print(f"输入 Token: {usage.input_tokens}")
              print(f"缓存命中 Token: {usage.input_tokens_details.cached_tokens}")
              ```

              ```javascript Node.js expandable
              import OpenAI from "openai";

              const openai = new OpenAI({
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                  // 通过 defaultHeaders 开启 Session 缓存
                  defaultHeaders: {"x-dashscope-session-cache": "enable"}
              });

              // 构造超过 1024 Token 的长文本，确保能触发缓存创建（若未达到1024 Token，后续累积对话上下文超过1024 Token时将触发缓存创建）
              const longContext = "人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。".repeat(50);

              async function main() {
                  // 第一轮对话
                  const response1 = await openai.responses.create({
                      model: "qwen3.8-max",
                      input: longContext + "\n\n基于以上背景知识，请简短介绍机器学习中的随机森林算法，包括基本原理和应用场景。"
                  });
                  console.log(`第一轮回复: ${response1.output_text}`);

                  // 第二轮对话：通过 previous_response_id 关联上下文，缓存由服务端自动处理
                  const response2 = await openai.responses.create(
                      {
                          model: "qwen3.8-max",
                          input: "它和 GBDT 有什么主要区别？",
                      },
                      {
                          body: { previous_response_id: response1.id }
                      }
                  );
                  console.log(`第二轮回复: ${response2.output_text}`);

                  // 查看缓存命中情况
                  console.log(`输入 Token: ${response2.usage.input_tokens}`);
                  console.log(`缓存命中 Token: ${response2.usage.input_tokens_details.cached_tokens}`);
              }

              main();
              ```

              ```bash curl
              # 第一轮对话
              # 长文本重复 50 次以确保超过 1024 Token，触发缓存创建
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -H "x-dashscope-session-cache: enable" \
              -d '{
                  "model": "qwen3.8-max",
                  "input": "人工智能是计算机科学的一个重要分支..."
              }'

              # 第二轮对话 - 使用上一轮返回的 id 作为 previous_response_id
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -H "x-dashscope-session-cache: enable" \
              -d '{
                  "model": "qwen3.8-max",
                  "input": "它和 GBDT 有什么主要区别？",
                  "previous_response_id": "第一轮返回的响应id"
              }'
              ```
            </CodeGroup>

            <strong>缓存命中规则</strong>

            Session 缓存按精确匹配命中，规则如下：

            - <strong>不支持前缀匹配</strong>：在原有 user prompt 之后追加内容不会命中缓存。
            - <strong>不支持语义匹配</strong>：只有内容完全相同才会命中缓存，语义相同但文字不同不会命中。
            - <strong>最小可缓存 Token 数</strong>：1024 Token。低于该阈值的输入不会触发缓存创建；多轮对话中累积上下文超过 1024 Token 后才会开始创建缓存。

            您可以通过 `usage.input_tokens_details.cached_tokens` 查看命中缓存的 Token 数，通过 `cache_creation_input_tokens` 查看本次请求新创建缓存的 Token 数。
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## Response 响应对象（非流式输出） <span id="8b91df45f4k58" />

        <strong>id</strong> `string`

        本次响应的唯一标识符，为 UUID 格式的字符串，有效期为7天。可用于 `previous_response_id` 参数以创建多轮对话。

        <strong>created\_at</strong> `integer`

        本次请求的 Unix 时间戳（秒）。

        <strong>object</strong> `string`

        对象类型，固定为 `response`。

        <strong>status</strong> `string`

        响应生成的状态。枚举值：

        - `completed`：生成完成
        - `failed`：生成失败
        - `in_progress`：生成中
        - `cancelled`：已取消
        - `queued`：请求排队中
        - `incomplete`：生成不完整

        <strong>model</strong> `string`

        用于生成响应的模型 ID。

        <strong>output</strong> `array`

        模型生成的输出项数组。数组中的元素类型和顺序取决于模型的响应。

        <Accordion title="数组元素属性">
          <strong>type</strong> `string`

          输出项类型。枚举值：

          - `message`：消息类型，包含模型最终生成的回复内容。
          - `reasoning`：推理类型，设置 `reasoning.effort`（非 `none`）或开启思考模式时返回。推理 Token 会被计入 `output_tokens_details.reasoning_tokens` 中，按推理 Token 计费。
          - `function_call`：函数调用类型，使用自定义 `function` 工具时返回。需要处理函数调用并返回结果。
          - `web_search_call`：搜索调用类型，使用 `web_search` 工具时返回。
          - `code_interpreter_call`：代码执行类型，使用 `code_interpreter` 工具时返回。
          - `web_extractor_call`：网页抽取类型，使用 `web_extractor` 工具时返回。需要配合 `web_search` 工具一起使用。
          - `web_search_image_call`：文搜图调用类型，使用 `web_search_image` 工具时返回。包含搜索到的图片列表。
          - `image_search_call`：图搜图调用类型，使用 `image_search` 工具时返回。包含搜索到的相似图片列表。
          - `mcp_call`：MCP 调用类型，使用 `mcp` 工具时返回。包含 MCP 服务的调用结果。
          - `file_search_call`：知识库搜索调用类型，使用 `file_search` 工具时返回。包含知识库的检索查询和结果。

          <strong>id</strong> `string`

          输出项的唯一标识符。所有类型的输出项都包含此字段。

          <strong>role</strong> `string`

          消息角色，固定为 `assistant`。仅当 `type` 为 `message` 时存在。

          <strong>status</strong> `string`

          输出项状态。可选值：`completed`（完成）、`in_progress`（生成中）。当 `type` 不为`reasoning`时存在。

          <strong>name</strong> `string`

          工具或函数名称。当 `type` 为 `function_call`、`web_search_image_call`、`image_search_call`、`mcp_call` 时存在。

          对于 `web_search_image_call` 和 `image_search_call`，值分别固定为 `"web_search_image"` 和 `"image_search"`。

          对于 `mcp_call`，值为 MCP 服务中被调用的具体函数名（如 `amap-maps-maps_geo`）。

          <strong>arguments</strong> `string`

          工具调用的参数，JSON 字符串格式。当 `type` 为 `function_call`、`web_search_image_call`、`image_search_call`、`mcp_call` 时存在。使用前需要通过 `JSON.parse()` 解析。不同工具类型的 arguments 内容：

          - `web_search_image_call`：`{"queries": ["搜索关键词1", "搜索关键词2"]}`，其中 `queries` 为模型根据用户输入自动生成的搜索关键词列表。
          - `image_search_call`：`{"img_idx": 0, "bbox": [0, 0, 1000, 1000]}`，其中 `img_idx` 为输入图片的索引（从 0 开始），`bbox` 为搜索区域的边界框坐标 \[x1, y1, x2, y2]，坐标范围 0-1000。
          - `function_call`：按用户定义的函数参数 schema 生成的参数对象。
          - `mcp_call`：MCP 服务中被调用函数的参数对象。

          <strong>call\_id</strong> `string`

          函数调用的唯一标识符。仅当 `type` 为 `function_call` 时存在。在返回函数调用结果时，需要通过此 ID 关联请求与响应。

          <strong>content</strong> `array`

          消息内容数组。仅当 `type` 为 `message` 时存在。

          <Accordion title="数组元素属性">
            <strong>type</strong> `string`

            内容类型，固定为 `output_text`。

            <strong>text</strong> `string`

            模型生成的文本内容。

            <strong>annotations</strong> `array`

            文本注释数组。通常为空数组。
          </Accordion>

          <strong>summary</strong> `array`

          推理摘要数组。仅当 `type` 为 `reasoning` 时存在。每个元素包含 `type`（值为 `summary_text`）和 `text`（摘要文本）字段。

          <strong>action</strong> `object`

          搜索动作信息。仅当 `type` 为 `web_search_call` 时存在。

          <Accordion title="属性">
            <strong>query</strong> `string`

            搜索查询关键词。

            <strong>type</strong> `string`

            搜索类型，固定为 `search`。

            <strong>sources</strong> `array`

            搜索来源列表。每个元素包含 `type`和 `url`字段。
          </Accordion>

          <strong>code</strong> `string`

          模型生成并执行的代码。仅当 `type` 为 `code_interpreter_call` 时存在。

          <strong>outputs</strong> `array`

          代码执行输出数组。仅当 `type` 为 `code_interpreter_call` 时存在。每个元素包含 `type`（值为 `logs`）和 `logs`（代码执行日志）字段。

          <strong>container\_id</strong> `string`

          代码解释器容器标识符。仅当 `type` 为 `code_interpreter_call` 时存在。用于关联同一会话中的多次代码执行。

          <strong>goal</strong> `string`

          抽取目标描述，说明需要从网页中提取哪些信息。仅当 `type` 为 `web_extractor_call` 时存在。

          <strong>output</strong> `string`

          工具调用的输出结果，字符串格式。

          - 当 `type` 为 `web_extractor_call` 时为网页抽取的内容摘要
          - 当 `type` 为 `web_search_image_call` 或 `image_search_call` 时为 JSON 字符串，包含图片搜索结果数组，每个元素包含 `title`（图片标题）、`url`（图片 URL）和 `index`（序号）字段
          - 当 `type` 为 `mcp_call` 时为 MCP 服务返回的 JSON 字符串结果。

          <strong>urls</strong> `array`

          被抽取的网页 URL 列表。仅当 `type` 为 `web_extractor_call` 时存在。

          <strong>server\_label</strong> `string`

          MCP 服务标签。仅当 `type` 为 `mcp_call` 时存在。标识本次调用所使用的 MCP 服务。

          <strong>queries</strong> `array`

          知识库检索使用的查询列表。仅当 `type` 为 `file_search_call` 时存在。数组元素为字符串，表示模型生成的搜索查询词。

          <strong>results</strong> `array`

          知识库检索结果数组。仅当 `type` 为 `file_search_call` 时存在。

          <Accordion title="数组元素属性">
            <strong>file\_id</strong> `string`

            匹配文档的文件 ID。

            <strong>filename</strong> `string`

            匹配文档的文件名。

            <strong>score</strong> `float`

            匹配相关度评分，取值范围 0-1，值越大表示相关度越高。

            <strong>text</strong> `string`

            匹配到的文档内容片段。
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        本次请求的 Token 消耗信息。

        <Accordion title="属性">
          <strong>input\_tokens</strong> `integer`

          输入的 Token 数。[补充说明](/zh/model-studio/text-generation#e710782c79xqy)

          <strong>output\_tokens</strong> `integer`

          模型输出的 Token 数。

          <strong>total\_tokens</strong> `integer`

          消耗的总 Token 数，为 input\_tokens 与 output\_tokens 的总和。

          <strong>input\_tokens\_details</strong> `object`

          输入 Token 的细粒度分类。

          <Accordion title="属性">
            <strong>cached\_tokens</strong> `integer`

            命中缓存的 Token 数。详情请参见[上下文缓存](/zh/model-studio/context-cache)。
          </Accordion>

          <strong>output\_tokens\_details</strong> `object`

          输出 Token 的细粒度分类。

          <Accordion title="属性">
            <strong>reasoning\_tokens</strong> `integer`

            思考过程 Token 数。
          </Accordion>

          <strong>x\_details</strong> `array`

          本次请求的计费明细数组。比顶级 `usage` 字段提供更细粒度的多模态 Token 拆分。

          <Accordion title="属性">
            <strong>input\_tokens</strong> `integer`

            输入的 Token 数。[补充说明](/zh/model-studio/text-generation#e710782c79xqy)

            <strong>output\_tokens</strong> `integer`

            模型输出的 Token 数。

            <strong>total\_tokens</strong> `integer`

            消耗的总 Token 数，为 input\_tokens 与 output\_tokens 的总和。

            <strong>x\_billing\_type</strong> `string`

            固定为`response_api`。

            <strong>image\_tokens</strong> `integer`

            图像输入的 Token 数。包含图像输入时返回，等同于 `input_tokens_details.image_tokens`。

            <strong>input\_tokens\_details</strong> `object`

            输入 Token 的细粒度分类，返回字段取决于模型和输入模态。`qwen3.8-omni-flash` 的音频输入可返回 `audio_tokens`。

            <Accordion title="属性">
              <strong>text\_tokens</strong> `integer`

              文本输入的 Token 数。

              <strong>image\_tokens</strong> `integer`

              图像输入的 Token 数。

              <strong>audio\_tokens</strong> `integer`

              音频输入的 Token 数，完整路径为 `usage.x_details[].input_tokens_details.audio_tokens`。可选字段，是否返回取决于模型及输入。
            </Accordion>

            <strong>output\_tokens\_details</strong> `object`

            输出 Token 的细粒度分类。比顶级 `output_tokens_details` 多 `text_tokens` 字段（多模态输入时返回）。

            <Accordion title="属性">
              <strong>reasoning\_tokens</strong> `integer`

              思考过程 Token 数。

              <strong>text\_tokens</strong> `integer`

              文本输出的 Token 数。多模态输入时返回。
            </Accordion>

            <strong>plugins</strong> `object`

            内置工具调用统计。使用内置工具（如 `web_search`）时返回，与顶级 `x_tools` 字段内容相同。

            <Accordion title="属性">
              <strong>web\_search</strong> `object`

              联网搜索调用统计。

              <Accordion title="属性">
                <strong>count</strong> `integer`

                本次响应中联网搜索的调用次数。
              </Accordion>
            </Accordion>

            <strong>input\_tokens\_details</strong> `object`

            输入 Token 的缓存详情。启用 Session 缓存后返回；含图像输入但未命中缓存时可能返回空对象。

            <Accordion title="属性">
              <strong>cached\_tokens</strong> `integer`

              命中缓存的 Token 数。

              <strong>cache\_creation\_input\_tokens</strong> `integer`

              本次请求新创建缓存的 Token 数。

              <strong>cache\_creation</strong> `object`

              缓存创建详情。

              <Accordion title="属性">
                <strong>ephemeral\_5m\_input\_tokens</strong> `integer`

                5 分钟临时缓存新创建的 Token 数。
              </Accordion>

              <strong>cache\_type</strong> `string`

              缓存类型，固定为`ephemeral`。
            </Accordion>
          </Accordion>

          <strong>x\_tools</strong> `object`

          工具使用统计信息。当使用内置工具时，包含各工具的调用次数。

          示例：`{"web_search": {"count": 1}}`
        </Accordion>

        <strong>error</strong> `object`

        当模型生成响应失败时返回的错误对象。成功时为 `null`。

        <strong>tools</strong> `array`

        回显请求中 `tools` 参数的完整内容，结构与请求体中的 `tools` 参数相同。

        <strong>tool\_choice</strong> `string`

        回显请求中 `tool_choice` 参数的值，枚举值为 `auto`、`none`、`required`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
            "created_at": 1771165743,
            "id": "c9f9c06b-032d-4525-a422-ac8ab5eccxxx",
            "model": "qwen3.8-max",
            "object": "response",
            "output": [
                {
                    "content": [
                        {
                            "annotations": [],
                            "text": "你好！我是 Qwen3.5，阿里巴巴最新推出的通义千问大语言模型，具备强大的语言理解、逻辑推理、代码生成及多模态处理能力，旨在为用户提供精准高效的智能服务。",
                            "type": "output_text"
                        }
                    ],
                    "id": "msg_544b2907-e88e-40d2-9a83-c30d6d1f9xxx",
                    "role": "assistant",
                    "status": "completed",
                    "type": "message"
                }
            ],
            "parallel_tool_calls": false,
            "status": "completed",
            "tool_choice": "auto",
            "tools": [],
            "usage": {
                "input_tokens": 55,
                "input_tokens_details": {
                    "cached_tokens": 0
                },
                "output_tokens": 43,
                "output_tokens_details": {
                    "reasoning_tokens": 0
                },
                "total_tokens": 98,
                "x_details": [
                    {
                        "input_tokens": 55,
                        "output_tokens": 43,
                        "total_tokens": 98,
                        "x_billing_type": "response_api"
                    }
                ]
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## Response 响应 chunk 对象（流式输出） <span id="94ea60632am5g" />

        流式输出返回一系列 JSON 对象。每个对象包含 `type` 字段标识事件类型，`sequence_number` 字段标识事件顺序。`response.completed` 事件标志着流式传输的结束。

        <strong>type</strong> `string`

        事件类型标识符。枚举值：

        - `response.created`：响应创建时触发，状态为 `queued`。
        - `response.in_progress`：响应开始处理时触发，状态变为 `in_progress`。
        - `response.output_item.added`：新的输出项（如 message、`web_extractor_call`）被添加到 output 数组时触发。当 `item.type` 为 `web_extractor_call` 时，表示网页抽取工具调用开始。
        - `response.content_part.added`：输出项的 content 数组中新增内容块时触发。
        - `response.output_text.delta`：增量文本生成时触发，多次触发，`delta` 字段包含新增文本片段。
        - `response.output_text.done`：文本生成完成时触发，`text` 字段包含完整文本。
        - `response.content_part.done`：内容块完成时触发，`part` 对象包含完整内容块。
        - `response.output_item.done`：输出项生成完成时触发，`item` 对象包含完整输出项。当 `item.type` 为 `web_extractor_call` 时，表示网页抽取工具调用完成。
        - `response.reasoning_text.delta`：（开启思考模式时）推理摘要增量文本，`delta` 字段包含新增摘要片段。
        - `response.reasoning_text.done`：（开启思考模式时）推理摘要完成，`text` 字段包含完整摘要。
        - `response.custom_tool_call_input.delta`：自定义工具增量文本
        - `response.custom_tool_call_input.done`：自定义工具完成
        - `response.web_search_call.in_progress` / `searching` / `completed`：（使用 web\_search 工具时）搜索状态变化事件。
        - `response.code_interpreter_call.in_progress` / `interpreting` / `completed`：（使用 code\_interpreter 工具时）代码执行状态变化事件。
        - <strong>注意：</strong>使用 `web_extractor` 工具时，没有专门的事件类型标识符。网页抽取工具调用通过通用的 `response.output_item.added` 和 `response.output_item.done` 事件传递，通过 `item.type` 字段（值为 `web_extractor_call`）来识别。
        - `response.incomplete`：响应因 max\_output\_tokens等限制而提前结束
        - `response.mcp_call_arguments.delta` / `response.mcp_call_arguments.done`：（使用 mcp 工具时）MCP 调用参数的增量和完成事件。
        - `response.mcp_call.in_progress`：MCP 服务调用中。
        - `response.mcp_call.completed`：（使用 mcp 工具时）MCP 服务调用完成。
        - `response.file_search_call.in_progress` / `searching` / `completed`：（使用 file\_search 工具时）知识库搜索状态变化事件。
        - <strong>注意：</strong>使用 `web_search_image` 和 `image_search` 工具时，没有专门的中间状态事件。工具调用通过 `response.output_item.added`（调用开始）和 `response.output_item.done`（调用完成）事件传递。
        - `response.completed`：响应生成完成时触发，`response` 对象包含完整响应（含 usage）。此事件标志流式传输结束。

        <strong>sequence\_number</strong> `integer`

        事件序列号，从 0 开始递增。用于确保客户端按正确顺序处理事件。

        <strong>response</strong> `object`

        响应对象。出现在 `response.created`、`response.in_progress` 和 `response.completed` 事件中。在 `response.completed` 事件中包含完整的响应数据（包括 `output` 和 `usage`），其结构与非流式响应的 Response 对象一致。

        <strong>item</strong> `object`

        输出项对象。出现在 `response.output_item.added` 和 `response.output_item.done` 事件中。在 `added` 事件中为初始骨架（content 为空数组），在 `done` 事件中为完整对象。

        <Accordion title="属性">
          <strong>id</strong> `string`

          输出项的唯一标识符（如 `msg_xxx`）。

          <strong>type</strong> `string`

          输出项类型。枚举值：`message`（消息）、`reasoning`（推理）、`web_search_call`（搜索）、`web_search_image_call`（文搜图）、`image_search_call`（图搜图）、`mcp_call`（MCP 调用）、`file_search_call`（知识库搜索）。

          <strong>role</strong> `string`

          消息角色，固定为 `assistant`。仅当 type 为 `message` 时存在。

          <strong>status</strong> `string`

          生成状态。在 `added` 事件中为 `in_progress`，在 `done` 事件中为 `completed`。

          <strong>content</strong> `array`

          消息内容数组。在 `added` 事件中为空数组 `[]`，在 `done` 事件中包含完整的内容块对象（结构与 `part` 对象相同）。
        </Accordion>

        <strong>part</strong> `object`

        内容块对象。出现在 `response.content_part.added` 和 `response.content_part.done` 事件中。

        <Accordion title="属性">
          <strong>type</strong> `string`

          内容块类型，固定为 `output_text`。

          <strong>text</strong> `string`

          文本内容。在 `added` 事件中为空字符串，在 `done` 事件中为完整文本。

          <strong>annotations</strong> `array`

          文本注释数组。通常为空数组。

          <strong>logprobs</strong> `object | null`

          Token 的对数概率信息。当前固定返回 `null`。
        </Accordion>

        <strong>delta</strong> `string`

        增量文本内容。出现在 `response.output_text.delta` 事件中，包含本次新增的文本片段。客户端应将所有 `delta` 拼接以获得完整文本。

        <strong>text</strong> `string`

        完整文本内容。出现在 `response.output_text.done` 事件中，包含该内容块的完整文本，可用于校验 `delta` 拼接结果。

        <strong>item\_id</strong> `string`

        输出项的唯一标识符。用于关联同一输出项的相关事件。

        <strong>output\_index</strong> `integer`

        输出项在 `output` 数组中的索引位置。

        <strong>content\_index</strong> `integer`

        内容块在 `content` 数组中的索引位置。
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```json 基础调用 expandable
          // response.created - 响应创建
          {"response":{"id":"428c90e9-9cd6-90a6-9726-c02b08ebexxx","created_at":1769082930,"object":"response","status":"queued",...},"sequence_number":0,"type":"response.created"}

          // response.in_progress - 响应进行中
          {"response":{"id":"428c90e9-9cd6-90a6-9726-c02b08ebexxx","status":"in_progress",...},"sequence_number":1,"type":"response.in_progress"}

          // response.output_item.added - 新增输出项
          {"item":{"id":"msg_bcb45d66-fc34-46a2-bb56-714a51e8exxx","content":[],"role":"assistant","status":"in_progress","type":"message"},"output_index":0,"sequence_number":2,"type":"response.output_item.added"}

          // response.content_part.added - 新增内容块
          {"content_index":0,"item_id":"msg_bcb45d66-fc34-46a2-bb56-714a51e8exxx","output_index":0,"part":{"annotations":[],"text":"","type":"output_text","logprobs":null},"sequence_number":3,"type":"response.content_part.added"}

          // response.output_text.delta - 增量文本（多次触发）
          {"content_index":0,"delta":"人工智能","item_id":"msg_bcb45d66-fc34-46a2-bb56-714a51e8exxx","logprobs":[],"output_index":0,"sequence_number":4,"type":"response.output_text.delta"}
          {"content_index":0,"delta":"（Artificial Intelligence，","item_id":"msg_bcb45d66-fc34-46a2-bb56-714a51e8exxx","logprobs":[],"output_index":0,"sequence_number":6,"type":"response.output_text.delta"}

          // response.output_text.done - 文本完成
          {"content_index":0,"item_id":"msg_bcb45d66-fc34-46a2-bb56-714a51e8exxx","logprobs":[],"output_index":0,"sequence_number":53,"text":"人工智能（Artificial Intelligence，简称 AI）是指由计算机系统模拟人类智能行为的技术和科学...","type":"response.output_text.done"}

          // response.content_part.done - 内容块完成
          {"content_index":0,"item_id":"msg_bcb45d66-fc34-46a2-bb56-714a51e8exxx","output_index":0,"part":{"annotations":[],"text":"...完整文本...","type":"output_text","logprobs":null},"sequence_number":54,"type":"response.content_part.done"}

          // response.output_item.done - 输出项完成
          {"item":{"id":"msg_bcb45d66-fc34-46a2-bb56-714a51e8exxx","content":[{"annotations":[],"text":"...完整文本...","type":"output_text","logprobs":null}],"role":"assistant","status":"completed","type":"message"},"output_index":0,"sequence_number":55,"type":"response.output_item.done"}

          // response.completed - 响应完成（包含完整响应和 usage）
          {"response":{"id":"428c90e9-9cd6-90a6-9726-c02b08ebexxx","created_at":1769082930,"model":"qwen3.7-max","object":"response","output":[...],"status":"completed","usage":{"input_tokens":37,"output_tokens":243,"total_tokens":280,...}},"sequence_number":56,"type":"response.completed"}
          ```

          ```json 网页抓取 expandable
          id:1
          event:response.created
          :HTTP_STATUS/200
          data:{"sequence_number":0,"type":"response.created","response":{"output":[],"parallel_tool_calls":false,"created_at":1769435906,"tool_choice":"auto","model":"","id":"863df8d9-cb29-4239-a54f-3e15a2427xxx","tools":[],"object":"response","status":"queued"}}

          id:2
          event:response.in_progress
          :HTTP_STATUS/200
          data:{"sequence_number":1,"type":"response.in_progress","response":{"output":[],"parallel_tool_calls":false,"created_at":1769435906,"tool_choice":"auto","model":"","id":"863df8d9-cb29-4239-a54f-3e15a2427xxx","tools":[],"object":"response","status":"in_progress"}}

          id:3
          event:response.output_item.added
          :HTTP_STATUS/200
          data:{"sequence_number":2,"item":{"summary":[],"type":"reasoning","id":"msg_5bd0c6df-19b8-4a04-bc00-8042a224exxx"},"output_index":0,"type":"response.output_item.added"}

          id:4
          event:response.reasoning_text.delta
          :HTTP_STATUS/200
          data:{"delta":"用户想要我：\n1. 搜索阿里云官网\n2. 提取官网首页的关键信息\n\n我需要先搜索阿里云官网的URL，然后使用web_extractor工具访问官网并提取关键信息。","sequence_number":3,"output_index":0,"type":"response.reasoning_text.delta","item_id":"msg_5bd0c6df-19b8-4a04-bc00-8042a224exxx"}

          id:14
          event:response.reasoning_text.done
          :HTTP_STATUS/200
          data:{"sequence_number":13,"text":"用户想要我：\n1. 搜索阿里云官网\n2. 提取官网首页的关键信息\n\n我需要先搜索阿里云官网的URL，然后使用web_extractor工具访问官网并提取关键信息。","output_index":0,"type":"response.reasoning_text.done","item_id":"msg_5bd0c6df-19b8-4a04-bc00-8042a224exxx"}

          id:15
          event:response.output_item.done
          :HTTP_STATUS/200
          data:{"sequence_number":14,"item":{"summary":[{"type":"summary_text","text":"用户想要我：\n1. 搜索阿里云官网\n2. 提取官网首页的关键信息\n\n我需要先搜索阿里云官网的URL，然后使用web_extractor工具访问官网并提取关键信息。"}],"type":"reasoning","id":"msg_5bd0c6df-19b8-4a04-bc00-8042a224exxx"},"output_index":1,"type":"response.output_item.done"}

          id:16
          event:response.output_item.added
          :HTTP_STATUS/200
          data:{"sequence_number":15,"item":{"action":{"type":"search","query":"Web search"},"id":"msg_a8a686b1-0a57-40e1-bb55-049a89cd4xxx","type":"web_search_call","status":"in_progress"},"output_index":1,"type":"response.output_item.added"}

          id:17
          event:response.web_search_call.in_progress
          :HTTP_STATUS/200
          data:{"sequence_number":16,"output_index":1,"type":"response.web_search_call.in_progress","item_id":"msg_a8a686b1-0a57-40e1-bb55-049a89cd4xxx"}

          id:19
          event:response.web_search_call.completed
          :HTTP_STATUS/200
          data:{"sequence_number":18,"output_index":1,"type":"response.web_search_call.completed","item_id":"msg_a8a686b1-0a57-40e1-bb55-049a89cd4xxx"}

          id:20
          event:response.output_item.done
          :HTTP_STATUS/200
          data:{"sequence_number":19,"item":{"action":{"sources":[{"type":"url","url":"https://cn.aliyun.com/"},{"type":"url","url":"https://www.aliyun.com/"}],"type":"search","query":"Web search"},"id":"msg_a8a686b1-0a57-40e1-bb55-049a89cd4xxx","type":"web_search_call","status":"completed"},"output_index":1,"type":"response.output_item.done"}

          id:33
          event:response.output_item.added
          :HTTP_STATUS/200
          data:{"sequence_number":32,"item":{"urls":["https://cn.aliyun.com/"],"goal":"提取阿里云官网首页的关键信息，包括：公司定位/简介、核心产品与服务、主要业务板块、特色功能/解决方案、最新动态/活动、免费试用/优惠信息、导航菜单结构等","id":"msg_8c2cf651-48a5-460c-aa7a-bea5b09b4xxx","type":"web_extractor_call","status":"in_progress"},"output_index":3,"type":"response.output_item.added"}

          id:34
          event:response.output_item.done
          :HTTP_STATUS/200
          data:{"sequence_number":33,"item":{"output":"The useful information in https://cn.aliyun.com/ for user goal 提取阿里云官网首页的关键信息，包括：公司定位/简介、核心产品与服务、主要业务板块、特色功能/解决方案、最新动态/活动、免费试用/优惠信息、导航菜单结构等 as follows: \n\nEvidence in page: \n## 通义大模型，企业拥抱 AI 时代首选\n\n## 完整的产品体系，为企业打造技术创新的云\n\n全部云产品## 依托大模型与云计算协同发展，让 AI 触手可及\n\n全部 AI 解决方案\n\nSummary: \nAlibaba Cloud positions itself as a leading enterprise AI solution provider centered around the Tongyi large model...","urls":["https://cn.aliyun.com/"],"goal":"提取阿里云官网首页的关键信息，包括：公司定位/简介、核心产品与服务、主要业务板块、特色功能/解决方案、最新动态/活动、免费试用/优惠信息、导航菜单结构等","id":"msg_8c2cf651-48a5-460c-aa7a-bea5b09b4xxx","type":"web_extractor_call","status":"completed"},"output_index":3,"type":"response.output_item.done"}

          id:50
          event:response.output_item.added
          :HTTP_STATUS/200
          data:{"sequence_number":50,"item":{"content":[{"type":"text","text":""}],"type":"message","id":"msg_final","role":"assistant"},"output_index":5,"type":"response.output_item.added"}

          id:51
          event:response.output_text.delta
          :HTTP_STATUS/200
          data:{"delta":"我已经找到阿里云官网并提取了首页的关键信息：\n\n","sequence_number":51,"output_index":5,"type":"response.output_text.delta"}

          id:60
          event:response.completed
          :HTTP_STATUS/200
          data:{"type":"response.completed","response":{"id":"863df8d9-cb29-4239-a54f-3e15a2427xxx","status":"completed","usage":{"input_tokens":45,"output_tokens":320,"total_tokens":365}}}
          ```

          ```json 文搜图 expandable
          // 1. response.created - 响应创建
          id:1
          event:response.created
          data:{"sequence_number":0,"type":"response.created","response":{"output":[],"status":"queued",...}}

          // 2. response.in_progress - 响应进行中
          id:2
          event:response.in_progress
          data:{"sequence_number":1,"type":"response.in_progress","response":{"status":"in_progress",...}}

          // 3. response.output_item.added - 推理开始（reasoning）
          id:3
          event:response.output_item.added
          data:{"sequence_number":2,"item":{"summary":[],"type":"reasoning","id":"msg_xxx"},"output_index":0,"type":"response.output_item.added"}

          // 4. response.reasoning_text.delta - 推理摘要增量
          id:4
          event:response.reasoning_text.delta
          data:{"delta":"用户想要找一张猫咪图片。我需要使用web_search_image工具来搜索...","sequence_number":3,"output_index":0,"type":"response.reasoning_text.delta","item_id":"msg_xxx"}

          // 5. response.reasoning_text.done - 推理摘要完成
          id:10
          event:response.reasoning_text.done
          data:{"sequence_number":9,"text":"用户想要找一张猫咪图片。我需要使用web_search_image工具来搜索猫咪图片。","output_index":0,"type":"response.reasoning_text.done","item_id":"msg_xxx"}

          // 6. response.output_item.done - 推理项完成
          id:11
          event:response.output_item.done
          data:{"sequence_number":10,"item":{"summary":[{"type":"summary_text","text":"..."}],"type":"reasoning","id":"msg_xxx"},"output_index":0,"type":"response.output_item.done"}

          // 7. response.output_item.added - 文搜图工具调用开始（status: in_progress，此时已包含 name 和 arguments）
          id:12
          event:response.output_item.added
          data:{"sequence_number":11,"item":{"name":"web_search_image","arguments":"{\"queries\": [\"猫咪图片\", \"cute cat\"]}","id":"msg_xxx","type":"web_search_image_call","status":"in_progress"},"output_index":1,"type":"response.output_item.added"}

          // 8. response.output_item.done - 文搜图工具调用完成（包含完整的 output 搜索结果）
          id:13
          event:response.output_item.done
          data:{"sequence_number":12,"item":{"name":"web_search_image","output":"[{\"title\": \"可爱的小猫咪...\", \"url\": \"https://example.com/cat.jpg\", \"index\": 1}, ...]","arguments":"{\"queries\": [\"猫咪图片\", \"cute cat\"]}","id":"msg_xxx","type":"web_search_image_call","status":"completed"},"output_index":1,"type":"response.output_item.done"}

          // 9-12. 第二轮推理 + 最终消息输出（与基础调用相同）
          // response.output_item.added (reasoning) → reasoning_text.delta/done → response.output_item.done (reasoning)
          // response.output_item.added (message) → response.content_part.added → response.output_text.delta → response.output_text.done → response.content_part.done → response.output_item.done (message)

          // 13. response.completed - 响应完成
          id:118
          event:response.completed
          data:{"sequence_number":117,"type":"response.completed","response":{"output":[...],"status":"completed","usage":{"input_tokens":7895,"output_tokens":318,"total_tokens":8213,"x_tools":{"web_search_image":{"count":1}}}}}
          ```

          ```json 图搜图
          // 1-6. 推理阶段（与文搜图相同）

          // 7. response.output_item.added - 图搜图工具调用开始
          // 注意 arguments 中包含 img_idx（图片索引）和 bbox（搜索区域边界框）
          id:29
          event:response.output_item.added
          data:{"sequence_number":29,"item":{"name":"image_search","arguments":"{\"img_idx\": 0, \"bbox\": [0, 0, 1000, 1000]}","id":"msg_xxx","type":"image_search_call","status":"in_progress"},"output_index":1,"type":"response.output_item.added"}

          // 8. response.output_item.done - 图搜图工具调用完成
          id:30
          event:response.output_item.done
          data:{"sequence_number":30,"item":{"name":"image_search","output":"[{\"title\": \"水墨山背景...\", \"url\": \"https://example.com/landscape.jpg\", \"index\": 1}, ...]","arguments":"{\"img_idx\": 0, \"bbox\": [0, 0, 1000, 1000]}","id":"msg_xxx","type":"image_search_call","status":"completed"},"output_index":1,"type":"response.output_item.done"}

          // 9-12. 第二轮推理 + 最终消息输出（与基础调用相同）

          // 13. response.completed
          id:408
          event:response.completed
          data:{"sequence_number":407,"type":"response.completed","response":{"output":[...],"status":"completed","usage":{"input_tokens":8371,"output_tokens":417,"total_tokens":8788,"x_tools":{"image_search":{"count":1}}}}}
          ```

          ```json MCP expandable
          // 1-6. 推理阶段（与其他工具相同）

          // 7. response.mcp_call_arguments.delta - MCP 参数增量（MCP 独有事件）
          id:27
          event:response.mcp_call_arguments.delta
          data:{"delta":"{\"city\": \"北京\"}","sequence_number":26,"output_index":1,"type":"response.mcp_call_arguments.delta","item_id":"msg_xxx"}

          // 8. response.mcp_call_arguments.done - MCP 参数完成（MCP 独有事件）
          id:28
          event:response.mcp_call_arguments.done
          data:{"sequence_number":27,"arguments":"{\"city\": \"北京\"}","output_index":1,"type":"response.mcp_call_arguments.done","item_id":"msg_xxx"}

          // 9. response.output_item.added - MCP 工具调用开始（包含 name、server_label、arguments）
          id:29
          event:response.output_item.added
          data:{"sequence_number":28,"item":{"name":"amap-maps-maps_weather","server_label":"MCP Server","arguments":"{\"city\": \"北京\"}","id":"msg_xxx","type":"mcp_call","status":"in_progress"},"output_index":1,"type":"response.output_item.added"}

          // 10. response.mcp_call.completed - MCP 调用完成（MCP 独有事件）
          id:30
          event:response.mcp_call.completed
          data:{"sequence_number":29,"output_index":1,"type":"response.mcp_call.completed","item_id":"msg_xxx"}

          // 11. response.output_item.done - MCP 输出项完成（包含完整 output）
          id:31
          event:response.output_item.done
          data:{"sequence_number":30,"item":{"output":"{\"city\":\"北京市\",\"forecasts\":[...]}","name":"amap-maps-maps_weather","server_label":"MCP Server","arguments":"{\"city\": \"北京\"}","id":"msg_xxx","type":"mcp_call","status":"completed"},"output_index":1,"type":"response.output_item.done"}

          // 12-15. 第二轮推理 + 最终消息输出

          // 16. response.completed
          id:172
          event:response.completed
          data:{"sequence_number":171,"type":"response.completed","response":{"output":[...],"status":"completed","usage":{"input_tokens":5019,"output_tokens":539,"total_tokens":5558}}}
          ```

          ```json 知识库搜索 expandable
          // 1-6. 推理阶段（与其他工具相同）

          // 7. response.output_item.added - 知识库搜索开始（包含 queries，无 results）
          id:19
          event:response.output_item.added
          data:{"sequence_number":18,"item":{"id":"msg_xxx","type":"file_search_call","queries":["阿里云百炼X1手机","Alibaba Cloud Bailian X1 phone","百炼X1"],"status":"in_progress"},"output_index":1,"type":"response.output_item.added"}

          // 8. response.file_search_call.in_progress - 搜索进行中（file_search 独有事件）
          id:20
          event:response.file_search_call.in_progress
          data:{"sequence_number":19,"output_index":1,"type":"response.file_search_call.in_progress","item_id":"msg_xxx"}

          // 9. response.file_search_call.searching - 正在搜索（file_search 独有事件）
          id:21
          event:response.file_search_call.searching
          data:{"sequence_number":20,"output_index":1,"type":"response.file_search_call.searching","item_id":"msg_xxx"}

          // 10. response.file_search_call.completed - 搜索完成（file_search 独有事件）
          id:22
          event:response.file_search_call.completed
          data:{"sequence_number":21,"output_index":1,"type":"response.file_search_call.completed","item_id":"msg_xxx"}

          // 11. response.output_item.done - 输出项完成（包含 queries + results）
          id:23
          event:response.output_item.done
          data:{"sequence_number":22,"item":{"id":"msg_xxx","type":"file_search_call","queries":["阿里云百炼X1手机","Alibaba Cloud Bailian X1 phone","百炼X1"],"results":[{"score":0.7519,"filename":"阿里云百炼系列手机产品介绍","text":"阿里云百炼 X1 ——畅享极致视界...","file_id":"file_xxx"}],"status":"completed"},"output_index":1,"type":"response.output_item.done"}

          // 12-15. 第二轮推理 + 最终消息输出

          // 16. response.completed
          id:146
          event:response.completed
          data:{"sequence_number":145,"type":"response.completed","response":{"output":[...],"status":"completed","usage":{"input_tokens":1576,"output_tokens":722,"total_tokens":2298,"x_tools":{"file_search":{"count":1}}}}}
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## 常见问题 <span id="614711697dm4t" />

<strong>Q：如何传递多轮对话的上下文？</strong>

A：在发起新一轮对话请求时，请将上一轮模型响应成功返回的`id`作为 `previous_response_id` 参数传入。

<strong>Q：为什么响应示例中的某些字段未在本文说明？</strong>

A：如果使用OpenAI的官方SDK，它可能会根据其自身的模型结构输出一些额外的字段（通常为`null`）。这些字段是OpenAI协议本身定义的，我们的服务当前不支持，所以它们为空值。只需关注本文档中描述的字段即可。
