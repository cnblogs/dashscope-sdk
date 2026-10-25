> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Anthropic兼容-Messages

> 通过兼容 Anthropic 格式的 Messages API 调用模型，查看输入输出参数说明及调用示例。

<meta property="og:title" content="Anthropic兼容-Messages" />

<style>@media (min-width: 1024px) \{ .markdown-body:has(#anthropic-messages-request-example) \{ overflow: clip; } .markdown-body :is(#anthropic-messages-request-example, #anthropic-messages-response-example, #anthropic-messages-stream-example) \{ position: sticky; top: 128px; max-height: calc(100vh - 160px); max-height: calc(100dvh - 160px); overflow: auto; scrollbar-gutter: stable; } }</style>

通过修改以下配置，即可将原有的 Anthropic 应用迁移至阿里云百炼：

- `api_key`：替换为[百炼 API Key](/zh/model-studio/get-api-key)。
- `base_url`：替换为百炼的兼容端点地址（见下方接入信息）。
- `model`：替换为百炼支持的模型名称（例如 `qwen3.7-plus`）。

<Tip>
  阿里云百炼为华北2（北京）、新加坡、中国香港地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

<Tabs>
  <Tab title="华北2（北京）">
    SDK 调用配置的 `base_url`：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic`

    HTTP 请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages`
  </Tab>

  <Tab title="新加坡">
    SDK 调用配置的 `base_url`：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/apps/anthropic`

    HTTP 请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/apps/anthropic/v1/messages`
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    SDK 调用配置的 `base_url`：`https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/apps/anthropic`

    HTTP 请求地址：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/apps/anthropic/v1/messages`
  </Tab>

  <Tab title="德国（法兰克福）">
    SDK 调用配置的 `base_url`：`https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/apps/anthropic`

    HTTP 请求地址：`POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/apps/anthropic/v1/messages`
  </Tab>

  <Tab title="日本（东京）">
    SDK 调用配置的 `base_url`：`https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/apps/anthropic`

    HTTP 请求地址：`POST https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/apps/anthropic/v1/messages`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

认证方式：通过 `x-api-key` 请求头或 `Authorization: Bearer` 请求头传入[百炼 API Key](/zh/model-studio/get-api-key)，二者选其一即可。

## 与 Anthropic 官方 API 的主要差异 <span id="bfr1diff0a1h2" />

以下差异点汇总自本文正文，从 Anthropic 官方迁移时请重点确认：

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "75%" }} /></colgroup><tbody><tr><td style={{ verticalAlign: "top" }}><p><strong>差异项</strong></p></td><td style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>接入地址（Base URL）</p></td><td style={{ verticalAlign: "top" }}><p><code>base\_url</code> 需替换为百炼兼容端点（形如 <code>https\://\{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic</code>，各地域地址见上方接入信息），其中 <code>\{WorkspaceId}</code> 需替换为真实的业务空间 ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>鉴权方式</p></td><td style={{ verticalAlign: "top" }}><p><code>api\_key</code> 需替换为百炼 API Key；支持通过 <code>x-api-key</code> 或 <code>Authorization: Bearer</code> 请求头传入，二者选其一即可。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>模型名称</p></td><td style={{ verticalAlign: "top" }}><p><code>model</code> 需替换为百炼支持的模型名称（例如 <code>qwen3.7-plus</code>），完整列表见下方 <code>model</code> 参数说明。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>temperature 取值范围</p></td><td style={{ verticalAlign: "top" }}><p>百炼取值范围为 \[0, 2)，与 Anthropic 官方的 \[0.0, 1.0] 不同，迁移时请确认该参数取值。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>接口范围</p></td><td style={{ verticalAlign: "top" }}><p>仅提供 Messages 接口（<code>/v1/messages</code>），不提供模型列表接口（<code>/v1/models</code>）；客户端的模型发现请求会返回 404，处理方式见下方常见问题。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>扩展参数</p></td><td style={{ verticalAlign: "top" }}><p><code>output\_config</code>（结构化输出与思考强度 <code>effort</code>）为百炼平台扩展参数，官方 SDK 类型定义中不包含，需在请求体中透传（见右侧“结构化输出”示例）；<code>thinking.budget\_tokens</code> 即将废弃，新接入建议使用 <code>output\_config.effort</code> 控制思考强度。</p></td></tr></tbody></table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## 请求体 <span id="ac3a4b5c6d7f2" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称，支持范围如下。

        <Accordion title="支持的模型列表">
          <strong>千问Max</strong>：qwen3.8-max、qwen3.8-max-0902、qwen3.7-max、qwen3.7-max-2026-05-20、qwen3.7-max-2026-06-08

          <strong>千问Plus</strong>：qwen3.7-plus、qwen3.7-plus-2026-05-26、qwen3.6-plus、qwen3.6-plus-2026-04-02、qwen3.5-plus、qwen3.5-plus-2026-04-20、qwen3.5-plus-2026-02-15、qwen-plus、qwen-plus-latest

          <strong>千问Flash</strong>：qwen3.8-flash、qwen3.7-flash、qwen3.7-flash-2026-07-15、qwen3.6-flash、qwen3.6-flash-2026-04-16、qwen3.5-flash、qwen3.5-flash-2026-02-23、qwen-flash、qwen-flash-2025-07-28

          <strong>千问Coder</strong>：qwen3-coder-flash

          <strong>千问VL</strong>：qwen3-vl-plus

          <strong>千问开源模型</strong>：qwen3.6-27b、qwen3.5-397b-a17b、qwen3.5-122b-a10b、qwen3.5-27b、qwen3.5-35b-a3b、qwen3.8-2.4t-a95b、qwen3.8-27b

          <strong>第三方模型</strong>

          deepseek-v4.1-flash、deepseek-v4-pro、deepseek-v4-pro-0813、deepseek-v4-flash-0731、kimi-k3、kimi-k2.7-code、kimi-k2.6、kimi-k2.5、glm-5.3、glm-5.2、glm-5.1、glm-5、MiniMax-M2.5、deepseek-v4.1-flash-1017
        </Accordion>

        <strong>max\_tokens</strong> `integer` <strong>（必选）</strong>

        - deepseek-v4.1-flash、deepseek-v4-pro、deepseek-v4-pro-0813、deepseek-v4-flash-0731、qwen3.8-max、qwen3.8-flash：模型回复内容和思维链内容之和的最大Token数，模型输出超过此值时生成将提前停止，`stop_reason` 为 `max_tokens`。

          > `max_tokens` 限制模型回复内容+思考过程的长度。开启深度思考时，`max_tokens` > `thinking.budget_tokens`
        - glm-5.3：`max_tokens` 为模型回复内容和思维链内容之和的最大Token数，模型输出超过此值时生成将提前停止，`stop_reason` 为 `max_tokens`。glm-5.3 会忽略 `thinking.budget_tokens` 参数。
        - glm-5.2：不传入 `thinking.budget_tokens` 参数时，`max_tokens` 为模型回复内容和思维链内容之和的最大Token数，模型输出超过此值时生成将提前停止，`stop_reason` 为 `max_tokens`；传入 `thinking.budget_tokens` 参数时，`max_tokens` 仅为模型回复内容的最大Token数，思考部分的 Token 数由 `thinking.budget_tokens` 单独控制。
        - 其他模型：模型回复内容的最大 Token 数。若生成内容超过此值，生成将提前停止，`stop_reason` 为 `max_tokens`。

          > `max_tokens` 不限制思考过程的长度。开启深度思考时，思考部分的 Token 数由 `thinking.budget_tokens` 单独控制。

        <strong>system</strong> `string 或 array` （可选）

        系统提示词，用于设定模型的角色或行为。

        传入字符串等价于单个 `type="text"` 的内容块。当需要为系统提示词标记显式缓存断点（参见右侧"显式缓存"示例）时，必须传入数组形式。

        <Accordion title="属性">
          <strong>type</strong> `string` <strong>（必选）</strong>

          固定为 `text`。

          <strong>text</strong> `string` <strong>（必选）</strong>

          系统提示词文本。

          <strong>cache\_control</strong> `object` （可选）

          在该内容块上标记显式缓存断点（参见右侧"显式缓存"示例），命中后第二次及之后的请求按缓存读取计费。仅包含字段 `type`，取值固定为 `ephemeral`。
        </Accordion>

        <strong>messages</strong> `array` <strong>（必选）</strong>

        <Accordion title="messages 数组元素">
          <strong>role</strong> `string` <strong>（必选）</strong>

          消息角色，可选值：`user`、`assistant`、`system`。

          <strong>content</strong> `string 或 array` <strong>（必选）</strong>

          消息内容。可以是纯文本字符串，也可以是结构化内容数组。`content` 为字符串时，等价于单个 `type="text"` 的内容块。

          <Accordion title="content 数组元素类型">
            <strong>文本信息</strong>

            <Accordion title="属性">
              <strong>type</strong> `string` <strong>（必选）</strong>

              固定为 `text`。

              <strong>text</strong> `string` <strong>（必选）</strong>

              文本内容。

              <strong>cache\_control</strong> `object` （可选）

              在该文本块上标记显式缓存断点（参见右侧"显式缓存"示例）。仅包含字段 `type`，取值固定为 `ephemeral`。
            </Accordion>

            <strong>图片信息</strong>（需使用视觉模型）

            <Accordion title="属性">
              <strong>type</strong> `string` <strong>（必选）</strong>

              固定为 `image`。

              <strong>source</strong> `object` <strong>（必选）</strong>

              图片数据来源。

              <Accordion title="属性">
                <strong>type</strong> `string` <strong>（必选）</strong>

                取值：`url`（公网图片地址）、`base64`（Base64 编码）。

                <strong>url</strong> `string`

                图片的公网地址。当 `type` 为 `url` 时必填。

                <strong>media\_type</strong> `string`

                图片的 MIME 类型，如 `image/jpeg`。当 `type` 为 `base64` 时必填。

                <strong>data</strong> `string`

                Base64 编码的图片数据。当 `type` 为 `base64` 时必填。
              </Accordion>
            </Accordion>

            <strong>视频信息</strong>（需使用视觉模型）

            <Accordion title="属性">
              <strong>type</strong> `string` <strong>（必选）</strong>

              固定为 `video`。

              <strong>source</strong> `object` <strong>（必选）</strong>

              视频数据来源。

              <Accordion title="属性">
                <strong>type</strong> `string` <strong>（必选）</strong>

                取值：`url`（公网视频地址）、`base64`（Base64 编码）。

                <strong>url</strong> `string`

                视频的公网地址。当 `type` 为 `url` 时必填。

                <strong>media\_type</strong> `string`

                视频的 MIME 类型，如 `video/mp4`。当 `type` 为 `base64` 时必填。

                <strong>data</strong> `string`

                Base64 编码的视频数据。当 `type` 为 `base64` 时必填。
              </Accordion>
            </Accordion>

            <strong>工具调用信息</strong>（assistant 角色，模型返回的工具调用指令）

            <Accordion title="属性">
              <strong>type</strong> `string` <strong>（必选）</strong>

              固定为 `tool_use`。

              <strong>id</strong> `string` <strong>（必选）</strong>

              工具调用的唯一标识，用于在后续 `tool_result` 中关联结果。

              <strong>name</strong> `string` <strong>（必选）</strong>

              被调用的工具名称。

              <strong>input</strong> `object` <strong>（必选）</strong>

              工具调用的入参，结构由 `tools` 中对应工具的 `input_schema` 决定。

              <strong>cache\_control</strong> `object` （可选）

              在该块上标记显式缓存断点（参见右侧"显式缓存"示例）。仅包含字段 `type`，取值固定为 `ephemeral`。工具调用内容本身会参与缓存前缀。
            </Accordion>

            <strong>工具结果信息</strong>（user 角色，工具执行结果回传给模型）

            <Accordion title="属性">
              <strong>type</strong> `string` <strong>（必选）</strong>

              固定为 `tool_result`。

              <strong>tool\_use\_id</strong> `string` <strong>（必选）</strong>

              对应 `tool_use` 信息中的 `id`。

              <strong>content</strong> `string` <strong>（必选）</strong>

              工具执行返回的内容。

              <strong>cache\_control</strong> `object` （可选）

              在该工具结果块上标记显式缓存断点（参见右侧"显式缓存"示例）。仅包含字段 `type`，取值固定为 `ephemeral`。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>stream</strong> `boolean` （可选）

        是否启用流式输出，默认为 `false`。

        <strong>temperature</strong> `number` （可选）

        控制生成文本的多样性，取值范围 \[0, 2)。值越大，生成结果越随机。

        <Note>
          该范围与 Anthropic 官方的 \[0.0, 1.0] 不同，从 Anthropic 迁移时请确认该参数取值。
        </Note>

        <strong>top\_p</strong> `number` （可选）

        核采样的概率阈值，控制生成文本的多样性。

        > `temperature` 与 `top_p` 均可控制生成文本的多样性，建议只设置其中一个值。更多说明请参见[概述](/zh/model-studio/text-generation)。

        <strong>top\_k</strong> `integer` （可选）

        生成过程中采样候选集的大小。

        <strong>stop\_sequences</strong> `array` （可选）

        指定停止生成的文本序列。模型生成到该序列前会停止输出，且不包含该序列本身。

        <Note>
          命中后，响应的 `stop_reason` 为 `stop_sequence`，并在响应的 `stop_sequence` 字段中回填命中的序列。
        </Note>

        <strong>thinking</strong> `object` （可选）

        深度思考配置。开启后，模型会在生成回复前先进行推理，以提升回答准确度。开启后，响应会包含 `thinking` 类型的内容块。

        未传入该参数时，是否进行思考由模型默认行为决定：qwen3.8-max、qwen3.8-flash、deepseek-v4 系列、glm 系列默认开启思考；kimi-k2.6、kimi-k2.5 默认关闭思考；kimi-k2.7-code、MiniMax-M2.5 仅支持思考模式（无法关闭）。各模型对思考模式的支持情况与默认开关，请参见[深度思考](/zh/model-studio/deep-thinking)。

        <Accordion title="属性">
          <strong>type</strong> `string` <strong>（必选）</strong>

          可选值：`enabled`（开启思考模式）、`disabled`（关闭思考模式）。

          <strong>budget\_tokens</strong> `integer` （可选，<strong>即将废弃</strong>）

          > 该参数即将废弃，并将在后续模型中逐步停止支持，新接入建议使用 `effort`控制模型的思考强度。

          思考过程可使用的最大 Token 数，与 `max_tokens` 互不重叠：本参数限制思考，`max_tokens` 限制最终回复。预算越大，在复杂问题上的分析越充分。当 `type` 为 `enabled` 时生效。
        </Accordion>

        <strong>tools</strong> `array` （可选）

        工具定义数组，用于 Function Call 场景。

        <Accordion title="tools 数组元素">
          <strong>name</strong> `string` <strong>（必选）</strong>

          工具名称。

          <strong>description</strong> `string` （可选）

          工具的功能描述。

          <strong>input\_schema</strong> `object` <strong>（必选）</strong>

          工具输入参数的 JSON Schema 定义。
        </Accordion>

        除上述自定义工具外，`tools` 还支持百炼服务端联网搜索工具，在 `tools` 中声明即可启用，无需客户端执行检索。

        <Accordion title="联网搜索工具">
          <strong>type</strong> `string` <strong>（必选）</strong>

          工具类型，取值为 `web_search_20250305` 等带日期后缀的形式。日期后缀无需固定，与客户端发送的值保持一致即可。

          <strong>name</strong> `string` <strong>（必选）</strong>

          固定为 `web_search`。

          <strong>max\_uses</strong> `integer` （可选）

          单次请求最多检索次数。
        </Accordion>

        > 使用 Anthropic SDK 或 HTTP 直接调用时，<strong>必须</strong>在 `system` 中传入客户端标识 `x-anthropic-billing-header: cc_entrypoint=cli;`，否则联网搜索不生效。面向 Claude Code 等 Anthropic 官方客户端时无需额外配置。详见 [联网搜索](/zh/model-studio/web-search)。

        > 支持以下模型：Qwen3.8 Max、Flash 与开源系列（不含 Omni）；Qwen3.7 Max、Plus、Flash 系列；Qwen3.6-Plus、Qwen3.6-Flash 系列；Qwen3.5-Plus、Qwen3.5-Flash 系列；deepseek-v4-pro、deepseek-v4-pro-0813、deepseek-v4-flash-0731、deepseek-v4.1-flash-1017；glm-5.2；kimi-k3。联网搜索的计费与 agent 策略相同，参见 [计费说明](/zh/model-studio/web-search#92ce83df3a599)。

        `deepseek-v4.1-flash-1017` 还支持网页抓取工具 `web_fetch`。Anthropic 兼容的请求要求及示例请参见[网页抓取](./web-extractor)。

        <strong>tool\_choice</strong> `object` （可选）

        工具选择策略。支持以下值：

        - `{"type": "auto"}`：模型自行决定是否调用工具（默认）。
        - `{"type": "any"}`：强制模型调用任意一个工具。
        - `{"type": "none"}`：禁止模型调用工具。
        - `{"type": "tool", "name": "tool_name"}`：强制模型调用指定工具。

        <strong>output\_config</strong> `object` （可选）

        输出参数设置。

        <Accordion title="属性">
          <strong>effort</strong> `string` （可选）

          控制模型的推理力度。

          - glm-5.3（默认值为 `max`）：

            可选值：

            - `low`：低力度推理
            - `high`：高力度推理
            - `max`：最大力度推理

            传入其它取值会返回错误。
          - glm-5.2、deepseek-v4-pro（阿里云直供）（默认值为 `max`）：

            可选值：

            - `high`：高力度推理
            - `max`：最大力度推理

            `low`和`medium`映射为`high`，`xhigh`映射为`max`。
          - qwen3.8-max/qwen3.8-flash（默认值为 `xhigh`）：

            可选值：

            - `xhigh`：高力度推理
            - `medium`：中力度推理
            - `low`：低力度推理

            `max` 、`high`映射为 `xhigh`。

          <strong>format</strong> `object` （可选）

          结构化输出配置。开启后，模型将输出 JSON 字符串。不同模型的支持力度不同：

          `deepseek-v4.1-flash-1017` 仅支持 JSON Object，不支持 JSON Schema。

          - <strong>严格结构化输出</strong>：适用于 qwen3.8 系列、qwen3.7 系列、deepseek 系列（不含 `deepseek-v4.1-flash-1017`）、glm 系列模型。模型严格按照传入的 JSON Schema 进行强约束输出，确保字段类型与层级完全一致。
          - <strong>普通结构化输出</strong>：适用于上述以外的其他模型。Schema 的具体字段约束默认不生效，API 会自动将其转换为普通 JSON 模式（仅保证输出为合法的 JSON 字符串）。触发普通 JSON 模式时，请求必须同时满足以下两点约束：1、显式传入 `output_config` 参数；2、`system` 或 `messages` 的内容中必须包含不区分大小写的 "JSON" 关键词。若提示词中未包含 "JSON" 关键词，API将抛出异常：`'messages' must contain the word 'json' in some form`。

          <Accordion title="属性">
            <strong>type</strong> `string` <strong>（必选）</strong>

            取值固定为 `json_schema`。

            <strong>schema</strong> `object` <strong>（必选）</strong>

            JSON Schema 对象，遵循标准 JSON Schema 规范。需包含 `type`（数据类型）、`properties`（字段定义）、`required`（必填字段名数组）、`additionalProperties`（必须设为 `false`）等字段。
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <div id="anthropic-messages-request-example">
          <Tabs>
            <Tab title="基础调用">
              <CodeGroup dropdown>
                ```python Python
                import anthropic
                import os

                client = anthropic.Anthropic(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                )

                message = client.messages.create(
                    model="qwen3.8-max",
                    max_tokens=1024,
                    system="You are a helpful assistant",
                    messages=[
                        {
                            "role": "user",
                            "content": "你是谁？"
                        }
                    ],
                    thinking={"type": "disabled"},
                )

                print(message.content[0].text)
                ```

                ```typescript TypeScript
                import Anthropic from "@anthropic-ai/sdk";

                const anthropic = new Anthropic({
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                });

                async function main() {
                  const message = await anthropic.messages.create({
                    model: "qwen3.8-max",
                    max_tokens: 1024,
                    system: "You are a helpful assistant",
                    messages: [{
                      role: "user",
                      content: "你是谁？"
                    }],
                    thinking: { type: "disabled" },
                  });

                  console.log(message.content[0].text);
                }

                main().catch(console.error);
                ```

                ```bash curl
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages" \
                  -H "Content-Type: application/json" \
                  -H "x-api-key: $DASHSCOPE_API_KEY" \
                  -d '{
                    "model": "qwen3.8-max",
                    "max_tokens": 1024,
                    "system": "You are a helpful assistant",
                    "messages": [
                        {
                            "role": "user",
                            "content": "你是谁？"
                        }
                    ],
                    "thinking": {"type": "disabled"}
                }'
                ```
              </CodeGroup>
            </Tab>

            <Tab title="流式输出">
              <CodeGroup dropdown>
                ```python Python
                import anthropic
                import os

                client = anthropic.Anthropic(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                )

                stream = client.messages.create(
                    model="qwen3.8-max",
                    max_tokens=1024,
                    stream=True,
                    messages=[
                        {
                            "role": "user",
                            "content": "请简单介绍一下人工智能。"
                        }
                    ],
                    thinking={"type": "disabled"},
                )

                for chunk in stream:
                    if chunk.type == "content_block_delta":
                        if hasattr(chunk.delta, 'text'):
                            print(chunk.delta.text, end="", flush=True)
                ```

                ```typescript TypeScript expandable
                import Anthropic from "@anthropic-ai/sdk";

                async function main() {
                  const anthropic = new Anthropic({
                    apiKey: process.env.DASHSCOPE_API_KEY,
                    baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                  });

                  const stream = await anthropic.messages.create({
                    model: "qwen3.8-max",
                    max_tokens: 1024,
                    stream: true,
                    messages: [{
                      role: "user",
                      content: "请简单介绍一下人工智能。"
                    }],
                    thinking: { type: "disabled" },
                  });

                  for await (const chunk of stream) {
                    if (chunk.type === "content_block_delta" && 'text' in chunk.delta) {
                      process.stdout.write(chunk.delta.text);
                    }
                  }
                }

                main().catch(console.error);
                ```

                ```bash curl
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages" \
                  -H "Content-Type: application/json" \
                  -H "x-api-key: $DASHSCOPE_API_KEY" \
                  --no-buffer \
                  -d '{
                    "model": "qwen3.8-max",
                    "max_tokens": 1024,
                    "stream": true,
                    "messages": [
                        {
                            "role": "user",
                            "content": "请简单介绍一下人工智能。"
                        }
                    ],
                    "thinking": {"type": "disabled"}
                }'
                ```
              </CodeGroup>
            </Tab>

            <Tab title="深度思考">
              <CodeGroup dropdown>
                ```python Python expandable
                import anthropic
                import os

                client = anthropic.Anthropic(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                )

                stream = client.messages.create(
                    model="qwen3.8-max",
                    max_tokens=2048,
                    stream=True,
                    thinking={
                        "type": "enabled",
                        "budget_tokens": 1024
                    },
                    messages=[
                        {
                            "role": "user",
                            "content": "分析一下量子计算的发展前景。"
                        }
                    ]
                )

                for chunk in stream:
                    if chunk.type == "content_block_delta":
                        if hasattr(chunk.delta, 'thinking'):
                            print(chunk.delta.thinking, end="", flush=True)
                        elif hasattr(chunk.delta, 'text'):
                            print(chunk.delta.text, end="", flush=True)
                ```

                ```typescript TypeScript expandable
                import Anthropic from "@anthropic-ai/sdk";

                async function main() {
                  const anthropic = new Anthropic({
                    apiKey: process.env.DASHSCOPE_API_KEY,
                    baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                  });

                  const stream = await anthropic.messages.create({
                    model: "qwen3.8-max",
                    max_tokens: 2048,
                    stream: true,
                    thinking: { type: "enabled", budget_tokens: 1024 },
                    messages: [{
                      role: "user",
                      content: "分析一下量子计算的发展前景。"
                    }]
                  });

                  for await (const chunk of stream) {
                    if (chunk.type === "content_block_delta") {
                      if ('thinking' in chunk.delta) {
                        process.stdout.write(chunk.delta.thinking);
                      } else if ('text' in chunk.delta) {
                        process.stdout.write(chunk.delta.text);
                      }
                    }
                  }
                }

                main().catch(console.error);
                ```

                ```bash curl
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages" \
                  -H "Content-Type: application/json" \
                  -H "x-api-key: $DASHSCOPE_API_KEY" \
                  -d '{
                    "model": "qwen3.8-max",
                    "max_tokens": 2048,
                    "stream": true,
                    "thinking": {
                        "type": "enabled",
                        "budget_tokens": 1024
                    },
                    "messages": [
                        {
                            "role": "user",
                            "content": "分析一下量子计算的发展前景。"
                        }
                    ]
                }'
                ```
              </CodeGroup>
            </Tab>

            <Tab title="图片理解">
              <CodeGroup dropdown>
                ```python Python expandable
                import anthropic
                import os

                client = anthropic.Anthropic(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                )

                stream = client.messages.create(
                    model="qwen3.8-max",
                    max_tokens=1024,
                    stream=True,
                    messages=[
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "image",
                                    "source": {
                                        "type": "url",
                                        "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250414/mqqmiy/animal_01.jpg",
                                    },
                                },
                                {
                                    "type": "text",
                                    "text": "描述这张图片的内容。"
                                },
                            ],
                        }
                    ],
                    thinking={"type": "disabled"},
                )

                for chunk in stream:
                    if chunk.type == "content_block_delta":
                        if hasattr(chunk.delta, 'text'):
                            print(chunk.delta.text, end="", flush=True)
                ```

                ```typescript TypeScript expandable
                import Anthropic from "@anthropic-ai/sdk";

                async function main() {
                  const anthropic = new Anthropic({
                    apiKey: process.env.DASHSCOPE_API_KEY,
                    baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                  });

                  const stream = await anthropic.messages.create({
                    model: "qwen3.8-max",
                    max_tokens: 1024,
                    stream: true,
                    messages: [{
                      role: "user",
                      content: [
                        {
                          type: "image",
                          source: {
                            type: "url",
                            url: "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250414/mqqmiy/animal_01.jpg",
                          },
                        },
                        { type: "text", text: "描述这张图片的内容。" },
                      ],
                    }],
                    thinking: { type: "disabled" },
                  });

                  for await (const chunk of stream) {
                    if (chunk.type === "content_block_delta" && 'text' in chunk.delta) {
                      process.stdout.write(chunk.delta.text);
                    }
                  }
                }

                main().catch(console.error);
                ```

                ```bash curl expandable
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages" \
                  -H "Content-Type: application/json" \
                  -H "x-api-key: $DASHSCOPE_API_KEY" \
                  -d '{
                    "model": "qwen3.8-max",
                    "max_tokens": 1024,
                    "stream": true,
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "image",
                                    "source": {
                                        "type": "url",
                                        "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250414/mqqmiy/animal_01.jpg"
                                    }
                                },
                                {
                                    "type": "text",
                                    "text": "描述这张图片的内容。"
                                }
                            ]
                        }
                    ],
                    "thinking": {"type": "disabled"}
                }'
                ```
              </CodeGroup>
            </Tab>

            <Tab title="视频理解">
              <CodeGroup dropdown>
                ```python Python expandable
                import anthropic
                import os

                client = anthropic.Anthropic(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                )

                stream = client.messages.create(
                    model="qwen3.8-max",
                    max_tokens=1024,
                    stream=True,
                    messages=[
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "video",
                                    "source": {
                                        "type": "url",
                                        "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251208/zpupby/3e81ef38-98f0-4d55-bbb6-259334ca18d0.mp4",
                                    },
                                },
                                {
                                    "type": "text",
                                    "text": "描述这段视频的内容。"
                                },
                            ],
                        }
                    ],
                    thinking={"type": "disabled"},
                )

                for chunk in stream:
                    if chunk.type == "content_block_delta":
                        if hasattr(chunk.delta, 'text'):
                            print(chunk.delta.text, end="", flush=True)
                ```

                ```typescript TypeScript expandable
                import Anthropic from "@anthropic-ai/sdk";

                async function main() {
                  const anthropic = new Anthropic({
                    apiKey: process.env.DASHSCOPE_API_KEY,
                    baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                  });

                  const stream = await anthropic.messages.create({
                    model: "qwen3.8-max",
                    max_tokens: 1024,
                    stream: true,
                    messages: [{
                      role: "user",
                      content: [
                        {
                          type: "video",
                          source: {
                            type: "url",
                            url: "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251208/zpupby/3e81ef38-98f0-4d55-bbb6-259334ca18d0.mp4",
                          },
                        },
                        { type: "text", text: "描述这段视频的内容。" },
                      ],
                    }],
                    thinking: { type: "disabled" },
                  });

                  for await (const chunk of stream) {
                    if (chunk.type === "content_block_delta" && 'text' in chunk.delta) {
                      process.stdout.write(chunk.delta.text);
                    }
                  }
                }

                main().catch(console.error);
                ```

                ```bash curl expandable
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages" \
                  -H "Content-Type: application/json" \
                  -H "x-api-key: $DASHSCOPE_API_KEY" \
                  -d '{
                    "model": "qwen3.8-max",
                    "max_tokens": 1024,
                    "stream": true,
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "video",
                                    "source": {
                                        "type": "url",
                                        "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251208/zpupby/3e81ef38-98f0-4d55-bbb6-259334ca18d0.mp4"
                                    }
                                },
                                {
                                    "type": "text",
                                    "text": "描述这段视频的内容。"
                                }
                            ]
                        }
                    ],
                    "thinking": {"type": "disabled"}
                }'
                ```
              </CodeGroup>
            </Tab>

            <Tab title="Function Call">
              <CodeGroup dropdown>
                ```python Python expandable
                import anthropic
                import os

                client = anthropic.Anthropic(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                )

                tools = [
                    {
                        "name": "get_weather",
                        "description": "获取指定城市的天气信息",
                        "input_schema": {
                            "type": "object",
                            "properties": {
                                "city": {
                                    "type": "string",
                                    "description": "城市名称"
                                }
                            },
                            "required": ["city"]
                        }
                    }
                ]

                message = client.messages.create(
                    model="qwen3.8-max",
                    max_tokens=1024,
                    tools=tools,
                    messages=[
                        {
                            "role": "user",
                            "content": "杭州今天天气怎么样？"
                        }
                    ]
                )

                print(message.content)
                ```

                ```typescript TypeScript expandable
                import Anthropic from "@anthropic-ai/sdk";

                async function main() {
                  const anthropic = new Anthropic({
                    apiKey: process.env.DASHSCOPE_API_KEY,
                    baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                  });

                  const message = await anthropic.messages.create({
                    model: "qwen3.8-max",
                    max_tokens: 1024,
                    tools: [
                      {
                        name: "get_weather",
                        description: "获取指定城市的天气信息",
                        input_schema: {
                          type: "object",
                          properties: {
                            city: { type: "string", description: "城市名称" }
                          },
                          required: ["city"],
                        },
                      },
                    ],
                    messages: [{
                      role: "user",
                      content: "杭州今天天气怎么样？"
                    }],
                  });

                  console.log(JSON.stringify(message.content, null, 2));
                }

                main().catch(console.error);
                ```

                ```bash curl expandable
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages" \
                  -H "Content-Type: application/json" \
                  -H "x-api-key: $DASHSCOPE_API_KEY" \
                  -d '{
                    "model": "qwen3.8-max",
                    "max_tokens": 1024,
                    "tools": [
                        {
                            "name": "get_weather",
                            "description": "获取指定城市的天气信息",
                            "input_schema": {
                                "type": "object",
                                "properties": {
                                    "city": {
                                        "type": "string",
                                        "description": "城市名称"
                                    }
                                },
                                "required": ["city"]
                            }
                        }
                    ],
                    "messages": [
                        {
                            "role": "user",
                            "content": "杭州今天天气怎么样？"
                        }
                    ]
                }'
                ```
              </CodeGroup>
            </Tab>

            <Tab title="联网搜索">
              <CodeGroup dropdown>
                ```python Python expandable
                import anthropic
                import os

                client = anthropic.Anthropic(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                )

                message = client.messages.create(
                    model="qwen3.8-max",
                    max_tokens=2048,
                    # 传入客户端标识，否则联网搜索不生效
                    system=[{"type": "text", "text": "x-anthropic-billing-header: cc_entrypoint=cli;"}],
                    messages=[{"role": "user", "content": "杭州明天天气如何"}],
                    tools=[
                        {
                            "type": "web_search_20250305",
                            "name": "web_search",
                            "max_uses": 8
                        }
                    ],
                    thinking={"type": "disabled"},
                )

                for block in message.content:
                    print(f"[{block.type}]")
                    if block.type == "server_tool_use":
                        print(f"  name: {block.name}, queries: {block.input.get('queries')}")
                    elif block.type == "web_search_tool_result":
                        for result in block.content:
                            print(f"  {result.title} - {result.url}")
                    elif block.type == "text":
                        print(block.text)

                print(f"检索次数: {message.usage.server_tool_use.web_search_requests}")
                ```

                ```typescript TypeScript expandable
                import Anthropic from "@anthropic-ai/sdk";

                async function main() {
                  const anthropic = new Anthropic({
                    apiKey: process.env.DASHSCOPE_API_KEY,
                    baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                  });

                  const message = await anthropic.messages.create({
                    model: "qwen3.8-max",
                    max_tokens: 2048,
                    // 传入客户端标识，否则联网搜索不生效
                    system: [{ type: "text", text: "x-anthropic-billing-header: cc_entrypoint=cli;" }],
                    messages: [{ role: "user", content: "杭州明天天气如何" }],
                    tools: [
                      {
                        type: "web_search_20250305",
                        name: "web_search",
                        max_uses: 8,
                      },
                    ],
                    thinking: { type: "disabled" },
                  });

                  for (const block of message.content) {
                    console.log(`[${block.type}]`);
                    if (block.type === "server_tool_use") {
                      console.log(`  name: ${block.name}, queries: ${block.input?.queries}`);
                    } else if (block.type === "web_search_tool_result") {
                      for (const result of block.content) {
                        console.log(`  ${result.title} - ${result.url}`);
                      }
                    } else if (block.type === "text") {
                      console.log(block.text);
                    }
                  }

                  console.log(`检索次数: ${message.usage.server_tool_use?.web_search_requests}`);
                }

                main().catch(console.error);
                ```

                ```bash curl
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages" \
                  -H "Content-Type: application/json" \
                  -H "x-api-key: $DASHSCOPE_API_KEY" \
                  -d '{
                    "model": "qwen3.8-max",
                    "max_tokens": 2048,
                    "system": [{"type": "text", "text": "x-anthropic-billing-header: cc_entrypoint=cli;"}],
                    "messages": [{"role": "user", "content": "杭州明天天气如何"}],
                    "tools": [{"type": "web_search_20250305", "name": "web_search", "max_uses": 8}],
                    "thinking": {"type": "disabled"}
                }'
                ```
              </CodeGroup>
            </Tab>

            <Tab title="显式缓存">
              <CodeGroup dropdown>
                ```python Python expandable
                import anthropic
                import os

                client = anthropic.Anthropic(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                )

                # 模拟代码仓库内容，需达到最小可缓存长度（1024 Token）
                long_text_content = "<Your Code Here>" * 400

                def get_completion(user_input):
                    response = client.messages.create(
                        # 选择支持显式缓存的模型
                        model="qwen3.8-max",
                        max_tokens=1024,
                        system=[
                            {
                                "type": "text",
                                "text": long_text_content,
                                # 在 text 块上添加 cache_control 即标记缓存断点；也可放在 messages 数组的 content 块上
                                "cache_control": {"type": "ephemeral"},
                            }
                        ],
                        messages=[
                            {"role": "user", "content": user_input},
                        ],
                    )
                    return response

                # 第一次请求：创建缓存
                first = get_completion("这段代码的内容是什么")
                print(f"创建缓存 Token：{first.usage.cache_creation_input_tokens}")
                print(f"命中缓存 Token：{first.usage.cache_read_input_tokens}")
                print("=" * 20)
                # 第二次请求：长内容相同，仅修改提问 → 命中缓存
                second = get_completion("这段代码可以怎么优化")
                print(f"创建缓存 Token：{second.usage.cache_creation_input_tokens}")
                print(f"命中缓存 Token：{second.usage.cache_read_input_tokens}")
                ```

                ```typescript TypeScript expandable
                import Anthropic from "@anthropic-ai/sdk";

                const client = new Anthropic({
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                });

                // 模拟代码仓库内容，需达到最小可缓存长度（1024 Token）
                const longTextContent = "<Your Code Here>".repeat(400);

                async function getCompletion(userInput) {
                  return client.messages.create({
                    // 选择支持显式缓存的模型
                    model: "qwen3.8-max",
                    max_tokens: 1024,
                    system: [
                      {
                        type: "text",
                        text: longTextContent,
                        // 在 text 块上添加 cache_control 即标记缓存断点；也可放在 messages 数组的 content 块上
                        cache_control: { type: "ephemeral" },
                      },
                    ],
                    messages: [{ role: "user", content: userInput }],
                  });
                }

                // 第一次请求：创建缓存
                const first = await getCompletion("这段代码的内容是什么");
                console.log(`创建缓存 Token：${first.usage.cache_creation_input_tokens}`);
                console.log(`命中缓存 Token：${first.usage.cache_read_input_tokens}`);
                console.log("=".repeat(20));
                // 第二次请求：长内容相同，仅修改提问 → 命中缓存
                const second = await getCompletion("这段代码可以怎么优化");
                console.log(`创建缓存 Token：${second.usage.cache_creation_input_tokens}`);
                console.log(`命中缓存 Token：${second.usage.cache_read_input_tokens}`);
                ```

                ```bash curl
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages" \
                  -H "Content-Type: application/json" \
                  -H "x-api-key: $DASHSCOPE_API_KEY" \
                  -d '{
                    "model": "qwen3.8-max",
                    "max_tokens": 1024,
                    "system": [
                      {
                        "type": "text",
                        "text": "<请在此处放置长度 ≥ 1024 Token 的可缓存内容>",
                        "cache_control": {"type": "ephemeral"}
                      }
                    ],
                    "messages": [
                      {"role": "user", "content": "这段代码的内容是什么"}
                    ]
                }'
                ```
              </CodeGroup>
            </Tab>

            <Tab title="结构化输出">
              <CodeGroup dropdown>
                ```python Python expandable
                import anthropic
                import os

                client = anthropic.Anthropic(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                )

                message = client.messages.create(
                    model="deepseek-v4-pro",
                    max_tokens=1024,
                    messages=[
                        {
                            "role": "user",
                            "content": "提取以下邮件的关键信息：张三 (zhangsan@example.com) 对企业版方案感兴趣，希望预约下周二下午 2 点的演示。"
                        }
                    ],
                    extra_body={
                        "output_config": {
                            "format": {
                                "type": "json_schema",
                                "schema": {
                                    "type": "object",
                                    "properties": {
                                        "name": {"type": "string"},
                                        "email": {"type": "string"},
                                        "plan_interest": {"type": "string"},
                                        "demo_requested": {"type": "boolean"}
                                    },
                                    "required": ["name", "email", "plan_interest", "demo_requested"],
                                    "additionalProperties": False
                                }
                            }
                        }
                    },
                )

                # deepseek-v4-pro 模型会返回 thinking 块，需要找到 type='text' 的内容块
                text_block = next(block for block in message.content if block.type == "text")
                print(text_block.text)
                ```

                ```typescript TypeScript expandable
                import Anthropic from "@anthropic-ai/sdk";

                const anthropic = new Anthropic({
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic",
                });

                async function main() {
                  // output_config 是百炼平台扩展参数，SDK 类型定义中不包含该字段，
                  // 通过交叉类型扩展官方参数类型进行透传，避免不安全的类型断言
                  type MessageCreateParamsWithOutputConfig =
                    Anthropic.MessageCreateParamsNonStreaming & {
                      output_config: {
                        format: {
                          type: "json_schema";
                          schema: Record<string, unknown>;
                        };
                      };
                    };

                  const params: MessageCreateParamsWithOutputConfig = {
                    model: "deepseek-v4-pro",
                    max_tokens: 1024,
                    messages: [{
                      role: "user",
                      content: "提取以下邮件的关键信息：张三 (zhangsan@example.com) 对企业版方案感兴趣，希望预约下周二下午 2 点的演示。"
                    }],
                    output_config: {
                      format: {
                        type: "json_schema",
                        schema: {
                          type: "object",
                          properties: {
                            name: { type: "string" },
                            email: { type: "string" },
                            plan_interest: { type: "string" },
                            demo_requested: { type: "boolean" }
                          },
                          required: ["name", "email", "plan_interest", "demo_requested"],
                          additionalProperties: false
                        }
                      }
                    }
                  };
                  const message = await anthropic.messages.create(params);

                  // deepseek-v4-pro 模型会返回 thinking 块，需要找到 type='text' 的内容块
                  const textBlock = message.content.find(
                    (block): block is Anthropic.TextBlock => block.type === "text"
                  );
                  console.log(textBlock?.text);
                }

                main().catch(console.error);
                ```

                ```bash curl expandable
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/apps/anthropic/v1/messages" \
                  -H "Content-Type: application/json" \
                  -H "x-api-key: $DASHSCOPE_API_KEY" \
                  -d '{
                    "model": "deepseek-v4-pro",
                    "max_tokens": 1024,
                    "messages": [
                        {
                            "role": "user",
                            "content": "提取以下邮件的关键信息：张三 (zhangsan@example.com) 对企业版方案感兴趣，希望预约下周二下午 2 点的演示。"
                        }
                    ],
                    "output_config": {
                        "format": {
                            "type": "json_schema",
                            "schema": {
                                "type": "object",
                                "properties": {
                                    "name": {"type": "string"},
                                    "email": {"type": "string"},
                                    "plan_interest": {"type": "string"},
                                    "demo_requested": {"type": "boolean"}
                                },
                                "required": ["name", "email", "plan_interest", "demo_requested"],
                                "additionalProperties": false
                            }
                        }
                    }
                }'
                ```
              </CodeGroup>
            </Tab>
          </Tabs>
        </div>
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
        ## 非流式响应 <span id="ag1a2b3c4d5f0" />

        <strong>id</strong> `string`

        消息的唯一标识。

        <strong>type</strong> `string`

        固定为 `message`。

        <strong>role</strong> `string`

        固定为 `assistant`。

        <strong>model</strong> `string`

        使用的模型名称。

        <strong>content</strong> `array`

        内容数组。

        <Accordion title="content 数组元素类型">
          <strong>文本信息</strong>

          <Accordion title="属性">
            <strong>type</strong> `string`

            固定为 `text`。

            <strong>text</strong> `string`

            模型生成的文本回复。
          </Accordion>

          <strong>思考信息</strong>（开启深度思考时返回）

          <Accordion title="属性">
            <strong>type</strong> `string`

            固定为 `thinking`。

            <strong>thinking</strong> `string`

            模型在生成最终回复前的思考过程。

            <strong>signature</strong> `string`

            当前固定为空字符串。
          </Accordion>

          <strong>工具调用信息</strong>（Function Call 场景）

          <Accordion title="属性">
            <strong>type</strong> `string`

            固定为 `tool_use`。

            <strong>id</strong> `string`

            工具调用的唯一标识，用于在后续 `tool_result` 中关联结果。

            <strong>name</strong> `string`

            被调用的工具名称。

            <strong>input</strong> `object`

            工具调用的入参。
          </Accordion>

          <strong>服务端工具调用信息</strong>（联网搜索场景，模型返回的服务端检索请求）

          <Accordion title="属性">
            <strong>type</strong> `string`

            固定为 `server_tool_use`。

            <strong>id</strong> `string`

            服务端工具调用唯一标识。

            <strong>name</strong> `string`

            工具名称，如 `web_search`。

            <strong>input</strong> `object`

            入参，含 `queries` 数组（模型生成的检索查询）。

            <strong>caller</strong> `object` （可选）

            调用方信息，`type` 为 `direct` 表示由模型直接发起调用。
          </Accordion>

          <strong>联网搜索结果信息</strong>（联网搜索场景，服务端返回的检索结果）

          <Accordion title="属性">
            <strong>type</strong> `string`

            固定为 `web_search_tool_result`。

            <strong>tool\_use\_id</strong> `string`

            对应 `server_tool_use` 中的 `id`。

            <strong>content</strong> `array`

            检索结果数组，元素为 `web_search_result` 对象。

            <Accordion title="web_search_result 属性">
              <strong>type</strong> `string`

              固定为 `web_search_result`。

              <strong>title</strong> `string`

              网页标题。

              <strong>url</strong> `string`

              网页地址。

              <strong>encrypted\_content</strong> `string`

              加密的网页内容。

              <strong>page\_age</strong> `string 或 null`

              网页时效，可为 `null`。
            </Accordion>

            <strong>caller</strong> `object` （可选）

            调用方信息，`type` 为 `direct` 表示由模型直接发起调用。
          </Accordion>
        </Accordion>

        <strong>stop\_reason</strong> `string`

        停止原因。可选值：`end_turn`（正常结束）、`max_tokens`（达到 Token 上限）、`tool_use`（工具调用）。

        <strong>stop\_sequence</strong> `string`

        固定为 `null`。

        <strong>usage</strong> `object`

        Token 用量统计。

        <Note>
          流式调用中，`message_start` 事件的 `usage` 仅包含 `input_tokens` 和 `output_tokens`；完整字段（含 `cache_creation_input_tokens`、`cache_read_input_tokens` 和 `server_tool_use`）在 `message_delta` 事件中返回。
        </Note>

        <Accordion title="属性">
          <strong>input\_tokens</strong> `integer`

          输入 Token 数量。

          <strong>output\_tokens</strong> `integer`

          输出 Token 数量。

          <strong>cache\_creation\_input\_tokens</strong> `integer`

          缓存创建消耗的输入 Token 数量。

          <strong>cache\_read\_input\_tokens</strong> `integer`

          缓存读取消耗的输入 Token 数量。

          <strong>server\_tool\_use</strong> `object` （可选）

          服务端工具用量统计。仅在请求中使用了服务端工具（如联网搜索）时返回。

          <Accordion title="属性">
            <strong>web\_search\_requests</strong> `integer`

            联网搜索的检索次数。
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <div id="anthropic-messages-response-example">
          <Tabs>
            <Tab title="基础响应">
              ```json
              {
                "id": "msg_e2898f19-fc0e-4cb3-bd9b-5b7dc4ea3bc9",
                "type": "message",
                "role": "assistant",
                "model": "qwen3.8-max",
                "content": [
                  {
                    "type": "thinking",
                    "thinking": "让我分析一下这个问题...",
                    "signature": ""
                  },
                  {
                    "type": "text",
                    "text": "你好！我是通义千问..."
                  }
                ],
                "stop_reason": "end_turn",
                "stop_sequence": null,
                "usage": {
                  "input_tokens": 22,
                  "output_tokens": 223,
                  "cache_creation_input_tokens": 0,
                  "cache_read_input_tokens": 0
                }
              }
              ```
            </Tab>

            <Tab title="联网搜索响应">
              ```json
              {
                "id": "msg_2db0950e-956e-9528-b75f-0b047ef86c03",
                "type": "message",
                "role": "assistant",
                "model": "qwen3.8-max",
                "content": [
                  {
                    "type": "server_tool_use",
                    "id": "srvtoolu_1bffa8e70882403cb14499af",
                    "name": "web_search",
                    "input": {
                      "queries": ["杭州明天天气"]
                    },
                    "caller": {"type": "direct"}
                  },
                  {
                    "type": "web_search_tool_result",
                    "tool_use_id": "srvtoolu_1bffa8e70882403cb14499af",
                    "content": [
                      {
                        "type": "web_search_result",
                        "title": "17日（明天）",
                        "url": "https://www.weather.com.cn/weather/101210101.shtml",
                        "encrypted_content": "eyJ0aXRsZSI6ICIxN+aXpe+8iOaYjuWkqe+8iSIs...",
                        "page_age": null
                      },
                      {
                        "type": "web_search_result",
                        "title": "杭州-天气预报",
                        "url": "https://www.nmc.cn/publish/forecast/AZJ/hangzhou.html",
                        "encrypted_content": "eyJ0aXRsZSI6ICLmna3lt54t5aSp5rCU6aKE5oqlIiw...",
                        "page_age": null
                      }
                    ],
                    "caller": {"type": "direct"}
                  },
                  {
                    "type": "text",
                    "text": "杭州明天（10月17日）的天气预计为多云转晴，气温在 22℃ 到 30℃ 之间..."
                  }
                ],
                "stop_reason": "end_turn",
                "stop_sequence": null,
                "usage": {
                  "input_tokens": 1920,
                  "output_tokens": 122,
                  "cache_creation_input_tokens": 0,
                  "cache_read_input_tokens": 0,
                  "server_tool_use": {
                    "web_search_requests": 1
                  }
                },
                "x_tools": {
                  "web_search": {
                    "count": 1
                  }
                }
              }
              ```
            </Tab>
          </Tabs>
        </div>
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
        ## 流式响应 <span id="ah1a2b3c4d5e8" />

        <strong>message\_start</strong>

        流的第一个事件，标记消息开始。

        <Accordion title="属性">
          <strong>type</strong> `string`

          固定为 `message_start`。

          <strong>message</strong> `object`

          初始消息对象，`content` 为空数组，`usage` 仅含 `input_tokens` 和 `output_tokens`。
        </Accordion>

        <strong>content\_block\_start</strong>

        每个内容块开始时发送，标记新内容块的索引和类型。

        <Accordion title="属性">
          <strong>type</strong> `string`

          固定为 `content_block_start`。

          <strong>index</strong> `integer`

          内容块索引，从 0 开始，对应该消息 `content` 数组中的位置。

          <strong>content\_block</strong> `object`

          内容块的初始对象。`type` 取值为 `text`、`thinking`、`tool_use`、`server_tool_use` 或 `web_search_tool_result`。`tool_use` 类型在此事件中 `input` 为空对象，完整入参由后续 `content_block_delta` 增量拼接。`web_search_tool_result` 在此事件中携带完整 `content` 数组，非增量拼接。
        </Accordion>

        <strong>content\_block\_delta</strong>

        内容块的增量更新事件。同一内容块会发送多个该事件。

        <Accordion title="属性">
          <strong>type</strong> `string`

          固定为 `content_block_delta`。

          <strong>index</strong> `integer`

          所属内容块索引。

          <strong>delta</strong> `object`

          增量对象，`type` 取值：

          - `text_delta`：文本增量，含 `text` 字段。
          - `thinking_delta`：思考增量，含 `thinking` 字段。
          - `signature_delta`：签名增量，含 `signature` 字段（当前固定为空字符串）。
          - `input_json_delta`：工具调用入参增量，含 `partial_json` 字段。
        </Accordion>

        <strong>content\_block\_stop</strong>

        内容块结束事件。

        <Accordion title="属性">
          <strong>type</strong> `string`

          固定为 `content_block_stop`。

          <strong>index</strong> `integer`

          结束的内容块索引。
        </Accordion>

        <strong>message\_delta</strong>

        消息级更新事件，在所有内容块结束后发送，包含停止原因和完整的 Token 用量统计。

        <Accordion title="属性">
          <strong>type</strong> `string`

          固定为 `message_delta`。

          <strong>delta</strong> `object`

          包含 `stop_reason` 和 `stop_sequence`，取值参见上方非流式响应表格。

          <strong>usage</strong> `object`

          完整的 Token 用量统计，包含 `input_tokens`、`output_tokens`、`cache_creation_input_tokens`、`cache_read_input_tokens`。
        </Accordion>

        <strong>message\_stop</strong>

        流的最后一个事件，标记消息结束。

        <Accordion title="属性">
          <strong>type</strong> `string`

          固定为 `message_stop`。

          此外，流式响应还会定期发送 <strong>ping</strong> 事件（`{"type":"ping"}`）用于保持连接活跃，客户端可忽略。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <div id="anthropic-messages-stream-example">
          <strong>流式响应示例</strong>

          ```json
          {"type":"message_start","message":{"id":"msg_xxx","type":"message","role":"assistant","model":"qwen3.8-max","content":[],"usage":{"input_tokens":15,"output_tokens":0}}}
          {"type":"content_block_start","index":0,"content_block":{"type":"thinking","thinking":"","signature":""}}
          {"type":"content_block_delta","index":0,"delta":{"type":"thinking_delta","thinking":"Here's a thinking process:\n\n1. **Analyze User Input:**\n   - **Topic:** 人工智能 (Artificial Intelligence / AI)\n   - **Request:** 请简单介绍一下人工智能。"}}
          {"type":"content_block_delta","index":0,"delta":{"type":"signature_delta","signature":""}}
          {"type":"content_block_stop","index":0}
          {"type":"content_block_start","index":1,"content_block":{"type":"text","text":""}}
          {"type":"content_block_delta","index":1,"delta":{"type":"text_delta","text":"人工智能（Artificial Intelligence，简称AI）是计算机科学的重要分支..."}}
          {"type":"content_block_stop","index":1}
          {"type":"message_delta","delta":{"stop_reason":"end_turn","stop_sequence":null},"usage":{"input_tokens":15,"output_tokens":1078,"cache_creation_input_tokens":0,"cache_read_input_tokens":0}}
          {"type":"message_stop"}
          ```
        </div>
      </td>
    </tr>
  </tbody>
</table>

## 常见问题 <span id="faq-ccbaseurl-h2" />

<strong>在 Claude Desktop 或 Claude Code 中配置后，连接测试报错</strong>`Model discovery — Gateway /v1/models returned HTTP 404`<strong>，或请求地址出现</strong>`/v1/v1/models`<strong>，如何解决？</strong>

Claude Desktop、Claude Code 等客户端的模型发现（model discovery）功能会在配置的 base URL 后自动追加 `/v1/models`。请按以下两点排查：

- <strong>base URL 不要以</strong>`/v1/`<strong>结尾</strong>：应填写到 `/apps/anthropic` 为止（例如华北2（北京）填 `https://dashscope.aliyuncs.com/apps/anthropic`，其余地域的地址见上方“接入信息”）。若误填为 `.../apps/anthropic/v1/`，客户端追加 `/v1/models` 后会形成 `/v1/v1/models` 的重复路径，导致 HTTP 404。因此出现 404 时，请先检查实际请求地址是否出现 `/v1/v1/` 重复，若有则去掉 base URL 末尾的 `/v1/`。
- <strong>手动添加模型以跳过自动发现</strong>：百炼 Anthropic 兼容端点仅提供 Messages 接口（`/v1/messages`），不提供模型列表接口（`/v1/models`），因此模型发现请求本身也会返回 404。请在客户端的 Models 中手动添加模型（例如 `qwen3.7-plus`）以跳过自动发现。
