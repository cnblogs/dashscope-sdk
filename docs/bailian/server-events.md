> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 服务端事件

> 本文介绍 Qwen-Omni-Realtime API 的服务端事件，包括工具调用（Function Calling）相关事件。

<meta property="og:title" content="服务端事件" />

## error <span id="9e1059c1d3tcx" />

服务端返回的错误信息。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`error`。

        <strong>error</strong>`object`

        错误的详细信息。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          错误类型。

          <strong>code</strong>`string`

          错误码。

          <strong>message</strong>`string`

          错误信息。

          <strong>param</strong>`string`

          与错误相关的参数，如`session.modalities`。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
          "event_id": "event_RoUu4T8yExPMI37GKwaOC",
          "type": "error",
          "error": {
            "type": "invalid_request_error",
            "code": "invalid_value",
            "message": "Invalid modalities: ['audio']. Supported combinations are: ['text'] and ['audio', 'text'].",
            "param": "session.modalities"
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.created <span id="ee6b5eb3485hw" />

客户端连接后，服务端返回的第一个事件，包含本次连接的默认配置信息。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`session.created`。

        <strong>session</strong>`object`

        会话的配置信息。

        <Accordion title="属性" defaultOpen>
          <strong>object</strong>`string`

          固定为`realtime.session`。

          <strong>model</strong>`string`

          使用的模型。

          <strong>modalities</strong>`array`

          模型输出模态设置。

          <strong>voice</strong>`string`

          模型生成音频的音色。

          <strong>input\_audio\_format</strong>`string`

          用户输入音频的格式，当前仅支持设为`pcm`。输入音频要求为16 kHz采样率的PCM音频流。

          <strong>output\_audio\_format</strong>`string`

          模型输出音频的格式，当前仅支持设为`pcm`。输出音频为24 kHz采样率的PCM音频流。当前不支持自定义输出采样率。

          <strong>input\_audio\_transcription</strong>`object`

          语音转录的配置。

          <Accordion title="属性" defaultOpen>
            <strong>model</strong>`string`

            语音转录模型，固定为`qwen3-asr-flash-realtime`，不支持修改。
          </Accordion>

          <strong>turn\_detection</strong>`object`

          语音活动检测（VAD）的配置。

          <Accordion title="属性" defaultOpen>
            <strong>type</strong>`string`

            VAD类型。取值：`server_vad`（默认值）、`semantic_vad` 或 `semantic_vad_ec`。其中，`semantic_vad_ec` 适用于 Qwen3.8-Omni-Flash-Realtime 系列模型。详情请参见[客户端事件](/zh/model-studio/client-events)。

            <strong>threshold</strong>`float`

            VAD检测阈值。

            <strong>silence\_duration\_ms</strong>`integer`

            检测语音停止的静音持续时间。

            <strong>idle\_timeout\_ms</strong>`integer`

            静默超时时间（毫秒）。仅在 `server_vad` 模式下，使用 `qwen3.5-omni-plus-realtime` 或 `qwen3.5-omni-flash-realtime` 模型时返回。
          </Accordion>

          <strong>enable\_search</strong>`boolean`

          是否启用联网搜索功能。Qwen3.8-Omni-Flash-Realtime 和 Qwen3.5-Omni-Realtime 系列模型支持。

          <strong>search\_options</strong>`object`

          联网搜索选项配置。

          <strong>temperature</strong>`float`

          模型的温度参数。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        以下为北京地域 `qwen3.8-omni-flash-realtime` 返回的默认会话示例。

        ```json expandable
        {
            "event_id": "event_QQubXbhHMyXmKaSuE6PQ8",
            "type": "session.created",
            "session": {
                "input_audio_format": "pcm",
                "input_audio_transcription": {
                    "model": "qwen3-asr-flash-realtime"
                },
                "id": "sess_MjEctgJ4ziXPEioCzSSvK",
                "object": "realtime.session",
                "modalities": [
                    "text",
                    "audio"
                ],
                "turn_detection": {
                    "type": "server_vad",
                    "silence_duration_ms": 800,
                    "interrupt_response": true,
                    "create_response": true,
                    "threshold": 0.5,
                    "prefix_padding_ms": 300
                },
                "model": "qwen3.8-omni-flash-realtime",
                "voice": "Tina",
                "output_audio_format": "pcm"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.updated <span id="424ef2e774q9p" />

收到用户的 `session.update` 请求后，若处理成功，则返回此事件；若出错，则返回 `error` 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`session.updated`。

        <strong>session</strong>`object`

        会话的配置信息。

        <Accordion title="属性" defaultOpen>
          <strong>temperature</strong>`float`

          模型的温度参数。

          <strong>modalities</strong>`array`

          模型输出模态设置。

          <strong>voice</strong>`string`

          模型生成音频的音色。

          <strong>instructions</strong>`string`

          模型的目标与角色。

          <strong>audio</strong>`object`

          回显的音频格式配置。若客户端传入了 `session.audio.input.format` / `session.audio.output.format`，服务端将在 `session.updated` 中按相同嵌套结构回显。未使用嵌套字段的客户端，服务端事件结构保持原有行为。

          <Accordion title="属性" defaultOpen>
            <strong>audio.input.format.type</strong>`string`

            用户输入音频格式。可选值：`pcm`（默认值）、`wav`。

            <strong>audio.input.format.sample\_rate</strong>`integer`

            用户输入音频采样率，单位为 Hz。

            <strong>audio.output.format.type</strong>`string`

            模型输出音频格式。可选值：`pcm`（默认值）、`wav`。

            <strong>audio.output.format.sample\_rate</strong>`integer`

            模型输出音频采样率，单位为 Hz。
          </Accordion>

          <strong>input\_audio\_format</strong>`string`

          历史兼容字段，回显客户端配置的输入音频格式。

          <strong>output\_audio\_format</strong>`string`

          历史兼容字段，回显客户端配置的输出音频格式。

          <strong>input\_audio\_transcription</strong>`object`

          语音转录的配置。

          <Accordion title="属性" defaultOpen>
            <strong>model</strong>`string`

            语音转录模型，固定为`qwen3-asr-flash-realtime`，不支持修改。
          </Accordion>

          <strong>turn\_detection</strong>`object`

          语音活动检测（VAD）的配置。

          <Accordion title="属性" defaultOpen>
            <strong>type</strong>`string`

            VAD类型。取值：`server_vad`（默认值）、`semantic_vad` 或 `semantic_vad_ec`。其中，`semantic_vad_ec` 适用于 Qwen3.8-Omni-Flash-Realtime 系列模型。详情请参见[客户端事件](/zh/model-studio/client-events)。

            <strong>threshold</strong>`float`

            VAD检测阈值。

            <strong>silence\_duration\_ms</strong>`integer`

            检测语音停止的静音持续时间。

            <strong>idle\_timeout\_ms</strong>`integer`

            静默超时时间（毫秒）。仅在 `server_vad` 模式下，使用 `qwen3.5-omni-plus-realtime` 或 `qwen3.5-omni-flash-realtime` 模型时返回。
          </Accordion>

          <strong>enable\_search</strong>`boolean`（可选）

          是否启用联网搜索功能。Qwen3.8-Omni-Flash-Realtime 和 Qwen3.5-Omni-Realtime 系列模型支持。

          <strong>search\_options</strong>`object`（可选）

          联网搜索选项配置。

          <strong>tools</strong>`array`（可选）

          工具定义列表。以下属性描述 `type="function"` 的 Function Calling 工具。Qwen3.8-Omni-Flash-Realtime 的 MCP 配置字段见[客户端工具配置](/zh/model-studio/client-events#session-tools-mcp)；MCP 连接地址和凭证不会在 `session.updated` 中回显。

          <Accordion title="属性">
            <strong>type</strong>`string`（必选）

            固定为 `function`。

            <strong>function.name</strong>`string`（必选）

            自定义的工具函数名称，建议使用与函数相同的名称，如`get_current_weather`或`get_current_time`。

            <strong>function.description</strong>`string`（可选）

            对工具函数功能的描述，大模型会参考该字段来选择是否使用该工具函数。

            <strong>function.parameters</strong>`object`（可选）

            对工具函数入参的描述，大模型会参考该字段来进行入参的提取。如果工具函数不需要输入参数，则无需指定。

            <Accordion title="属性">
              <strong>type</strong>`string`（必选）

              固定为 `object`。

              <strong>properties</strong>`object`（可选）

              描述各入参的名称、数据类型与描述。Key 值为入参的名称，Value 值为包含数据类型（`type`）与描述（`description`）的对象。

              <strong>required</strong>`array`（可选）

              指定哪些入参为必填项。
            </Accordion>
          </Accordion>

          <strong>top\_p</strong>`float`

          核采样的概率阈值。

          <strong>top\_k</strong>`integer`

          模型生成过程中，采样候选集的大小。

          <strong>max\_tokens</strong>`integer`

          模型在本次请求返回的最大 Token 数。

          <strong>repetition\_penalty</strong>`float`

          控制模型生成时，连续序列中的重复度\*。\*

          <strong>presence\_penalty</strong>`float`

          控制模型在生成内容时的重复度。

          <strong>seed</strong>`integer`

          模型在每次请求时，运行结果一致性程度。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
            "event_id": "event_X1HsXS4b4uptp6yo1LgKd",
            "type": "session.updated",
            "session": {
                "id": "sess_Aih6vAcY5Ddt6jwFx1tCa",
                "object": "realtime.session",
                "model": "qwen3.5-omni-flash-realtime",
                "modalities": [
                    "text",
                    "audio"
                ],
                "audio": {
                    "input": {
                        "format": {
                            "type": "pcm",
                            "sample_rate": 16000
                        }
                    },
                    "output": {
                        "format": {
                            "type": "wav",
                            "sample_rate": 24000
                        }
                    }
                },
                "instructions": "你是个人助理小云，请你准确且友好地解答用户的问题，始终以乐于助人的态度回应。",
                "voice": "Tina",
                "input_audio_format": "pcm",
                "output_audio_format": "pcm",
                "input_audio_transcription": {
                    "model": "qwen3-asr-flash-realtime"
                },
                "turn_detection": {
                    // VAD 类型及各模式的支持范围参见客户端事件中的 turn_detection.type
                    "type": "server_vad",
                    "threshold": 0.1,
                    "prefix_padding_ms": 500,
                    "silence_duration_ms": 900,
                    "create_response": true,
                    "interrupt_response": true
                },
                "enable_search": false,
                "search_options": {},
                "tools": [
                    {
                        "type": "function",
                        "function": {
                            "name": "get_current_weather",
                            "description": "当你想查询指定城市的天气时非常有用。",
                            "parameters": {
                                "type": "object",
                                "properties": {
                                    "location": {"type": "string", "description": "城市名称"}
                                },
                                "required": ["location"]
                            }
                        }
                    }
                ],
                "temperature": 0.8,
                "max_response_output_token": "inf",
                "max_tokens": 16384,
                "repetition_penalty": 1.05,
                "presence_penalty": 0.0,
                "top_k": 50,
                "top_p": 1.0,
                "seed":-1
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.speech\_started <span id="d06b426b1fnun" />

在 VAD 模式下，当服务端在音频缓冲区中检测到语音开始时，会返回此事件。

> 若服务端尚未检测到语音，则每次向缓冲区添加音频时都可能触发此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`input_audio_buffer.speech_started`。

        <strong>audio\_start\_ms</strong>`integer`

        从音频开始写入缓冲区到首次检测到语音所经过的毫秒数。

        <strong>item\_id</strong>`string`

        语音停止时将创建的用户消息项的 ID。

        > 用户消息项用于将用户输入追加到对话历史，供模型后续推理与生成使用。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_Pvp8nEhsQuGCQbFJ9x58n",
            "type": "input_audio_buffer.speech_started",
            "audio_start_ms": 3647,
            "item_id": "item_YbAiGvK2H7YaS34o4R6Ba"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.speech\_stopped <span id="fd08ffdf0a2mt" />

在 VAD 模式下，当音频缓冲区中检测到语音结束时，服务端会返回此事件。

同时，服务端还会返回一个 `conversation.item.created` 事件，以创建对应的用户消息项。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`input_audio_buffer.speech_stopped`。

        <strong>audio\_end\_ms</strong>`integer`

        语音停止时刻距会话开始经过的毫秒数。

        <strong>item\_id</strong>`string`

        将创建的用户消息项的 ID。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_UhQiqNVRsgUiq4KUS5Xb5",
            "type": "input_audio_buffer.speech_stopped",
            "audio_end_ms": 4453,
            "item_id": "item_YbAiGvK2H7YaS34o4R6Ba"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.committed <span id="bd9bfdc258afy" />

当输入音频缓冲区被提交时返回此事件。

- 在VAD模式下，当检测到用户说话结束时，服务端会自动提交音频缓冲区并返回此事件。
- 在 Manual 模式下，当客户端发送`input_audio_buffer.commit`事件后，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`input_audio_buffer.committed`。

        <strong>item\_id</strong>`string`

        将创建的用户消息项的 ID。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_Iy6sUzL1nmdFgshFYxJEz",
            "type": "input_audio_buffer.committed",
            "item_id": "item_YbAiGvK2H7YaS34o4R6Ba"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.cleared <span id="1103f53e9b13o" />

客户端发送`input_audio_buffer.clear`事件后，服务端将返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`input_audio_buffer.cleared`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
          "event_id": "event_RoUu4T8yExPMI37GKwaOC",
          "type": "input_audio_buffer.cleared"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.created <span id="4f23bd31e6awz" />

当对话项创建时返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "60%" }} />

    <col style={{ width: "40%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`conversation.item.created`。

        <strong>response\_id</strong>`string`（条件必填）

        仅当 `item.type` 为 `mcp_approval_request` 时返回；生成本次 MCP 调用的父 Response ID。

        <strong>item\_id</strong>`string`（条件必填）

        仅当 `item.type` 为 `mcp_approval_request` 时返回；审批请求 item ID，与 `item.id` 相同。

        <strong>previous\_item\_id</strong>`string`（条件必填）

        仅当 `item.type` 为 `mcp_approval_request` 时返回；被审批的 `mcp_call` item ID。

        <strong>item</strong>`object`

        要添加到对话中的项。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          对话项的唯一ID。

          <strong>object</strong>`string`

          `message` 和 `function_call` 项固定为 `realtime.item`；MCP 项若返回该字段，值也为 `realtime.item`。

          <strong>status</strong>`string`

          若返回该字段，表示对话项的状态。`mcp_call` 初始项必填，值为 `in_progress`。

          <strong>role</strong>`string`

          仅消息项包含，表示消息的角色。

          <strong>content</strong>`array[object]`

          消息的内容。当 type 为 `message` 时存在。下列字段对应本页示例中的数组元素。

          <Accordion title="数组元素" defaultOpen>
            <strong>type</strong>`string`

            内容片段的类型；本页示例为 `input_audio`。
          </Accordion>

          <strong>type</strong>`string`

          对话项的类型包括 `message`（常规消息）和 `function_call`（工具调用）。Qwen3.8-Omni-Flash-Realtime 还可返回 `mcp_list_tools`、`mcp_call`、`mcp_approval_request`，事件示例见[MCP 对话项](#mcp-items)。

          <strong>name</strong>`string`

          当 type 为 `function_call` 时，被调用的函数名称。

          <strong>call\_id</strong>`string`

          当 type 为 `function_call` 时，本次函数调用的唯一 ID。

          <strong>arguments</strong>`string`

          当 type 为 `function_call` 时，函数调用的参数（JSON 字符串）。

          <Accordion title="type=mcp_list_tools" defaultOpen>
            该类型的 `item.id` 与工具发现状态事件的 `item_id` 相同。

            <strong>server\_label</strong>`string`（必填）

            MCP Server 标识。

            <strong>tools</strong>`array[object]`（必填）

            经校验和 `allowed_tools` 过滤的工具定义，发现失败时为空数组。

            <Accordion title="数组元素" defaultOpen>
              <strong>name</strong>`string`（必填）

              工具原始名称，1～64 位，仅允许字母、数字、下划线、点和连字符。

              <strong>description</strong>`string`（可选）

              MCP Server 提供的工具说明。

              <strong>input\_schema</strong>`object`（必填）

              MCP Server 提供的 JSON Schema。服务端不执行完整的 JSON Schema 语义校验，最终由 MCP Server 校验工具参数。

              <Accordion title="属性" defaultOpen>
                <strong>type</strong>`string`（必填）

                根节点固定为 `object`。

                <strong>properties</strong>`object`（可选）

                按自定义名称列出各参数。例如下方 [mcp\_list\_tools 示例](#mcp-list-tools-item) 中的 `city` 是参数名，其 `type` 和 `description` 分别说明数据类型和用途；各参数的 JSON Schema 由 MCP Server 定义。

                <strong>required</strong>`array[string]`（可选）

                必填参数名列表。

                `additionalProperties`、`$defs`、`oneOf`、`anyOf`、`allOf` 等其他标准 JSON Schema 字段按原始 schema 保留并传递。
              </Accordion>

              <strong>annotations</strong>`object`（可选）

              MCP Server 提供的 ToolAnnotations。以下是 MCP 标准中的可选提示字段，实际返回内容由 MCP Server 决定。

              <Accordion title="属性" defaultOpen>
                <strong>title</strong>`string`（可选）

                工具的显示名称。

                <strong>readOnlyHint</strong>`boolean`（可选）

                提示工具是否只读。

                <strong>destructiveHint</strong>`boolean`（可选）

                提示工具是否可能执行破坏性修改。

                <strong>idempotentHint</strong>`boolean`（可选）

                提示重复调用是否不会产生额外效果。

                <strong>openWorldHint</strong>`boolean`（可选）

                提示工具是否可能与外部系统交互。这些字段均为提示，不应作为安全判断依据。
              </Accordion>
            </Accordion>

            <strong>error</strong>`object`（可选）

            工具发现失败时出现。

            <Accordion title="属性" defaultOpen>
              <strong>type</strong>`string`（必填）

              固定为 `tool_execution_error`。

              <strong>message</strong>`string`（必填）

              面向客户端的安全错误描述，不包含上游敏感响应体。
            </Accordion>
          </Accordion>

          <Accordion title="type=mcp_call（初始项）" defaultOpen>
            <strong>object</strong>`string`（必填）

            固定为 `realtime.item`。

            <strong>status</strong>`string`（必填）

            初始为 `in_progress`。

            <strong>server\_label</strong>`string`（必填）

            实际执行工具的 MCP Server 标识。

            <strong>name</strong>`string`（必填）

            工具原始名称。

            <strong>call\_id</strong>`string`（必填）

            本次调用的唯一 ID。

            <strong>arguments</strong>`string`（必填）

            初始通常为空字符串；完整参数以后续事件及最终项为准。
          </Accordion>

          <Accordion title="type=mcp_approval_request" defaultOpen>
            审批请求的 `item.id` 应在审批回复中原样填入 `approval_request_id`。

            <strong>server\_label</strong>`string`（必填）

            待执行工具所属的 MCP Server 标识。

            <strong>name</strong>`string`（必填）

            待执行工具的原始名称。

            <strong>arguments</strong>`string`（必填）

            待审批调用的完整参数 JSON 字符串。

            <strong>call\_id</strong>`string`（必填）

            被审批的工具调用 ID。
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        示例：

        ```json expandable
        {
            "event_id": "event_JEfkrr9gO3Ny7Xcv9bGVd",
            "type": "conversation.item.created",
            "item": {
                "id": "item_YbAiGvK2H7YaS34o4R6Ba",
                "object": "realtime.item",
                "type": "message",
                "status": "in_progress",
                "role": "assistant",
                "content": [
                    {
                        "type": "input_audio"
                    }
                ]
            }
        }
        // 工具调用场景
        {
            "event_id": "event_S1hkaIQgcuQD8OEdOpGHQ",
            "type": "conversation.item.created",
            "item": {
                "id": "item_FEG9qJGNkPcdf4et3p7BV",
                "object": "realtime.item",
                "type": "function_call",
                "status": "in_progress",
                "call_id": "call_bc0a7fb7235840f69ecfe4",
                "name": "get_current_weather",
                "arguments": ""
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.delta <span id="b8g4c3d2e6f7g" />

开启输入音频转录后，此事件会在用户说话过程中高频发送，用于展示实时识别的中间结果。您可以通过拼接 `text` + `stash` 获取当前最完整的句子预览。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`conversation.item.input_audio_transcription.delta`。

        <strong>item\_id</strong>`string`

        关联的对话项 ID。

        <strong>content\_index</strong>`integer`

        包含音频的内容部分的索引。

        <strong>text</strong>`string`

        已确认的文本前缀。这是当前句子中，模型已确认不会再变更的部分。

        <strong>stash</strong>`string`

        预识别的文本后缀。这是紧跟在已确认部分之后，模型仍在处理、可能会被修正的临时草稿。

        <strong>language</strong>`string`

        被识别音频的语种。

        <strong>emotion</strong>`string`

        被识别音频的情感。可选值：`neutral`（平静）、`happy`（愉快）、`sad`（悲伤）、`angry`（愤怒）、`surprised`（惊讶）、`disgusted`（厌恶）、`fearful`（恐惧）。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_C7jzoeSFuiwOZS6tR14yx",
            "type": "conversation.item.input_audio_transcription.delta",
            "item_id": "item_ThVYhLHOdeXb4bBSvzSFF",
            "content_index": 0,
            "text": "",
            "stash": "今天天气怎么样？",
            "language": "zh",
            "emotion": "neutral",
            "obfuscation": "ABEXGYmxdmc97u"
        }
        ```

        在任何时刻，要获取当前最完整的句子预览，都需要将这两个字段拼接起来：实时预览句子 = `text` + `stash`。

        <Accordion title="点击查看示例">
          假设用户正在说："今天天气不错，阳光明媚。"

          以下是您可能会收到的事件流以及如何解读它们：

          <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "24.98%" }} /><col style={{ width: "24.98%" }} /><col style={{ width: "24.98%" }} /><col style={{ width: "25.06%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>时间点</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>用户说话进度</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>API 响应 (text 和 stash)</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>客户端 UI 应显示 (text + stash)</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>T1</p></td><td style={{ verticalAlign: "top" }}><p>"今天……"</p></td><td style={{ verticalAlign: "top" }}><p>text: ""</p> <p>stash: "今天"</p></td><td style={{ verticalAlign: "top" }}><p>今天</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>T2</p></td><td style={{ verticalAlign: "top" }}><p>"……天气……"</p></td><td style={{ verticalAlign: "top" }}><p>text: ""</p> <p>stash: "今天天气"</p></td><td style={{ verticalAlign: "top" }}><p>今天天气</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>T3</p></td><td style={{ verticalAlign: "top" }}><p>"……不错"</p></td><td style={{ verticalAlign: "top" }}><p>text: "今天"</p> <p>stash: "天气不错"</p></td><td style={{ verticalAlign: "top" }}><p>今天天气不错</p> <p>（注意，"今天"已被确认并移入text）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>T4</p></td><td style={{ verticalAlign: "top" }}><p>（短暂停顿）</p></td><td style={{ verticalAlign: "top" }}><p>text: "今天天气不错，"</p> <p>stash: ""</p></td><td style={{ verticalAlign: "top" }}><p>今天天气不错，</p> <p>（前半句完全确认）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>T5</p></td><td style={{ verticalAlign: "top" }}><p>"……阳光……"</p></td><td style={{ verticalAlign: "top" }}><p>text: "今天天气不错，"</p> <p>stash: "阳光"</p></td><td style={{ verticalAlign: "top" }}><p>今天天气不错，阳光</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>T6</p></td><td style={{ verticalAlign: "top" }}><p>"……明媚。"</p></td><td style={{ verticalAlign: "top" }}><p>text: "今天天气不错，"</p> <p>stash: "阳光明媚。"</p></td><td style={{ verticalAlign: "top" }}><p>今天天气不错，阳光明媚。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>T7</p></td><td style={{ verticalAlign: "top" }}><p>（结束说话）</p></td><td style={{ verticalAlign: "top" }}><p>-</p></td><td style={{ verticalAlign: "top" }}><p>使用 conversation.item.input\_audio\_transcription.completed 的 transcript 内容作为最终结果。</p></td></tr></tbody></table>
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.completed <span id="e1d8895b46qxo" />

此事件表示用户音频写入缓冲区后生成的转录结果。其转录由内置的语音识别模型（固定为 `qwen3-asr-flash-realtime`）处理，不支持修改。

> 语音识别模型生成的转录文本可能与 Qwen-Omni-Realtime 模型的理解存在差异，仅供参考。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`conversation.item.input_audio_transcription.completed`。

        <strong>item\_id</strong>`string`

        用户消息项的 ID。

        <strong>content\_index</strong>*integer*

        当前固定为0。

        <strong>transcript</strong>`string`

        转录的文本内容。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_FrrZcxiDfTB9LD9p4pVng",
            "type": "conversation.item.input_audio_transcription.completed",
            "item_id": "item_YbAiGvK2H7YaS34o4R6Ba",
            "content_index": 0,
            "transcript": "喂，你好。"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.failed <span id="eca1e5495bpby" />

启用输入音频转录后，若用户音频转录失败，服务端会返回此事件。此事件独立于 `error` 事件，便于客户端识别。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`conversation.item.input_audio_transcription.failed`。

        <strong>item\_id</strong>`string`

        用户消息项的 ID。

        <strong>content\_index</strong>`integer`

        当前固定为0。

        <strong>error</strong>`object`

        错误信息。

        <Accordion title="属性" defaultOpen>
          <strong>code</strong> `string`

          错误码。

          <strong>message</strong> `string`

          错误消息。

          <strong>param</strong> `string`

          错误相关的参数。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
          "type": "conversation.item.input_audio_transcription.failed",
          "item_id": "<item_id>",
          "content_index": 0,
          "error": {
            "code": "<code>",
            "message": "<message>",
            "param": "<param>"
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.created <span id="e43ca5aa9eqp6" />

当服务端生成新的模型响应时，会返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.created`。

        <strong>response</strong>`object`

        响应对象。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong> `string`

          响应的唯一 ID。

          <strong>conversation\_id</strong> `string`

          当前会话的唯一ID。

          <strong>object</strong> `string`

          对象类型，此事件下固定为`realtime.response`。

          <strong>status</strong> `string`

          响应的状态。在`[completed, failed, in_progress, or incomplete]`范围内。

          <strong>modalities</strong> `array`

          响应的模态。

          <strong>voice</strong> `string`

          模型生成音频的音色。

          <strong>output</strong> `array`

          此事件下目前为空。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_XuDavMzQN3KKepqGu3KRh",
            "type": "response.created",
            "response": {
                "id": "resp_HaVOPdbmX6vifiV5pAfJY",
                "object": "realtime.response",
                "conversation_id": "conv_FjJaccpnvwHNo9cPVuzGc",
                "status": "in_progress",
                "modalities": [
                    "text",
                    "audio"
                ],
                "voice": "Cherry",
                "output_audio_format": "pcm",
                "output": []
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.done <span id="f2333c777d9s4" />

响应生成完成后，服务端会返回此事件。事件中的 `response` 对象包含除原始音频数据外的全部输出项。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "60%" }} />

    <col style={{ width: "40%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.done`。

        <strong>response</strong>`object`

        响应对象。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong> `string`

          响应的唯一 ID。

          <strong>conversation\_id</strong> `string`

          当前会话的唯一ID。

          <strong>object</strong> `string`

          对象类型，此事件下固定为`realtime.response`。

          <strong>status</strong> `string`

          响应的状态。

          <strong>modalities</strong> `array`

          响应的模态。

          <strong>voice</strong> `string`

          模型生成音频的音色。

          <strong>output</strong> `array`

          响应的输出项数组；每个元素是对话项对象。

          <Accordion title="数组元素" defaultOpen>
            <strong>id</strong> `string`

            响应输出对应的ID。

            <strong>type</strong> `string`

            输出项的类型，可选值包括 `message`（常规消息）、`function_call`（工具调用），Qwen3.8-Omni-Flash-Realtime 还支持 `mcp_call`，事件示例见[MCP 对话项](#mcp-items)。

            <strong>object</strong> `string`

            `message` 和 `function_call` 项固定为 `realtime.item`；MCP 项若返回该字段，值也为 `realtime.item`。

            <strong>status</strong> `string`

            输出项的状态。

            <strong>role</strong> `string`

            仅消息项包含，表示消息的角色。

            <strong>content</strong> `array`

            输出项的内容。当 type 为 `message` 时存在；每个元素是输出内容对象。

            <Accordion title="数组元素" defaultOpen>
              <strong>type</strong> `string`

              输出内容的类型。输出为纯文本时，为`text`；输出包含音频时，为`audio`。

              <strong>text</strong> `string`

              输出的文本内容。

              <strong>transcript</strong> `string`

              音频转录为文字后的内容。
            </Accordion>

            <strong>name</strong> `string`

            当 type 为 `function_call` 时，被调用的函数名称。

            <strong>call\_id</strong> `string`

            当 type 为 `function_call` 时，函数调用的唯一 ID。

            <strong>arguments</strong> `string`

            当 type 为 `function_call` 时，函数调用的完整参数（JSON 字符串）。

            <Accordion title="type=mcp_call（最终项）" defaultOpen>
              <strong>status</strong>`string`（必填）

              `completed` 或 `failed`。

              <strong>server\_label</strong>`string`（必填）

              实际执行工具的 MCP Server 标识。

              <strong>name</strong>`string`（必填）

              工具原始名称。

              <strong>call\_id</strong>`string`（必填）

              本次调用的唯一 ID。

              <strong>arguments</strong>`string`（必填）

              完整参数 JSON 字符串。

              <strong>output</strong>`string`（可选）

              MCP `tools/call.result` 对象序列化后的 JSON 字符串；部分失败场景也可能出现。

              <strong>error</strong>`object`（可选）

              调用失败时出现。

              <Accordion title="属性" defaultOpen>
                <strong>type</strong>`string`（必填）

                固定为 `tool_execution_error`。

                <strong>message</strong>`string`（必填）

                面向客户端的安全错误描述，不包含上游敏感响应体。
              </Accordion>

              MCP Server 返回 `isError=true` 时最终状态为 `failed`，也可能保留原始 `output`。
            </Accordion>
          </Accordion>

          <strong>usage</strong> `object`

          本次响应的 Token 消耗信息。

          <Accordion title="属性">
            <strong>total\_tokens</strong> `integer`

            本次响应消耗的总 Token 数。

            <strong>input\_tokens</strong> `integer`

            输入 Token 数。

            <strong>output\_tokens</strong> `integer`

            输出 Token 数。

            <strong>input\_tokens\_details</strong> `object`

            输入 Token 的分项详情，包含 `text_tokens`（文本 Token 数）和 `audio_tokens`（音频 Token 数）。

            <strong>output\_tokens\_details</strong> `object`

            输出 Token 的分项详情，包含 `text_tokens`（文本 Token 数）和 `audio_tokens`（音频 Token 数）。

            <strong>plugins</strong> `object`（可选）

            插件使用计量信息。启用联网搜索（`enable_search`）时返回。

            <Accordion title="属性">
              <strong>search</strong> `object`

              联网搜索计量信息。

              <Accordion title="属性">
                <strong>count</strong> `integer`

                搜索次数。

                <strong>strategy</strong> `string`

                搜索策略。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        示例：

        ```json expandable
        {
            "event_id": "event_CSaxRRYLvbrfexDXAEuDG",
            "type": "response.done",
            "response": {
                "id": "resp_HaVOPdbmX6vifiV5pAfJY",
                "object": "realtime.response",
                "conversation_id": "conv_FjJaccpnvwHNo9cPVuzGc",
                "status": "completed",
                "modalities": [
                    "text",
                    "audio"
                ],
                "voice": "Cherry",
                "output_audio_format": "pcm",
                "output": [
                    {
                        "id": "item_Ls6MtCUWO7LM4E59QziNv",
                        "object": "realtime.item",
                        "type": "message",
                        "status": "completed",
                        "role": "assistant",
                        "content": [
                            {
                                "type": "audio",
                                "transcript": "你好呀！有什么我可以帮你的吗？"
                            }
                        ]
                    }
                ],
                "usage": {
                    "total_tokens": 377,
                    "input_tokens": 336,
                    "output_tokens": 41,
                    "input_tokens_details": {
                        "text_tokens": 228,
                        "audio_tokens": 108
                    },
                    "output_tokens_details": {
                        "text_tokens": 9,
                        "audio_tokens": 32
                    },
                    "plugins": {
                        "search": {
                            "count": 1,
                            "strategy": "agent"
                        }
                    }
                }
            }
        }
        // 工具调用场景
        {
            "event_id": "event_T1EFAJp43X2DWtDRmxTtx",
            "type": "response.done",
            "response": {
                "id": "resp_TucN5QgymL5MA8vkJvFlS",
                "object": "realtime.response",
                "conversation_id": "conv_SEDZESRlefT8WvLSmEn6E",
                "status": "completed",
                "modalities": ["text", "audio"],
                "voice": "Ethan",
                "output_audio_format": "pcm",
                "output": [
                    {
                        "id": "item_FEG9qJGNkPcdf4et3p7BV",
                        "object": "realtime.item",
                        "type": "function_call",
                        "status": "completed",
                        "call_id": "call_bc0a7fb7235840f69ecfe4",
                        "name": "get_current_weather",
                        "arguments": " {\"location\": \"杭州\"}"
                    }
                ],
                "usage": {
                    "total_tokens": 567,
                    "input_tokens": 524,
                    "output_tokens": 43,
                    "input_tokens_details": {
                        "text_tokens": 487,
                        "audio_tokens": 37
                    },
                    "output_tokens_details": {
                        "text_tokens": 43
                    }
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.text.delta <span id="0c54be63e0c3w" />

当输出模态仅包含文本，且模型增量生成新的文本时，服务端将返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.text.delta`。

        <strong>delta</strong>`string`

        返回的增量文本。

        <strong>response\_id</strong>`string`

        回复的ID。

        <strong>item\_id</strong>`string`

        消息项ID，可以关联同一个消息项。

        <strong>output\_index</strong>`integer`

        响应中输出项的索引, 目前固定为 0。

        <strong>content\_index</strong>`integer`

        响应中输出项中内部部分的索引, 目前固定为 0。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "delta": "喂",
            "event_id": "event_TH49MauuPmRo1RGaMSlP7",
            "type": "response.text.delta",
            "response_id": "resp_PrRSvPVpnCExdUOGHHLuP",
            "item_id": "item_L8IRm9kRXFpxoOjDqDC96",
            "output_index": 0,
            "content_index": 0
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.text.done <span id="d675635a94jfb" />

当输出模态仅包含文本，且模型生成的文本结束时，服务端将返回此事件。

> 当响应中断、不完整或取消时，也会返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.text.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引。

        <strong>content\_index</strong>`integer`

        响应输出项的索引。

        <strong>text</strong> `string`

        模型输出的完整文本。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
          "event_id": "event_B1lIeE2Nac33zn5V7h2mm",
          "type": "response.text.done",
          "response_id": "resp_B1lIdtjF4Noqpn5NOjznj",
          "item_id": "item_B1lIdJsAJlJiFs8ztWpJt",
          "output_index": 0,
          "content_index": 0,
          "text": "How can I assist you today?"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio.delta <span id="a25cc50a15car" />

当输出模态包含音频，且模型增量生成新的音频数据时，服务端将返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio.delta`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引。

        <strong>content\_index</strong>`integer`

        响应输出项的索引。

        <strong>delta</strong> `string`

        模型增量输出的音频数据，使用Base64编码。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
          "event_id": "event_B1osWMZBtrEQbiIwW0qHQ",
          "type": "response.audio.delta",
          "response_id": "resp_P79OOMs8LnrXVpiIHUCKR",
          "item_id": "item_OFaPGtzfWCPyGzxnuEX9i",
          "output_index": 0,
          "content_index": 0,
          "delta": "{base64 audio}"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio.done <span id="9e8eb59c67qnt" />

当输出模态包含音频，且模型完成生成音频数据时，服务端将返回此事件。

> 当响应中断、不完整或取消时，也会返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引。

        <strong>content\_index</strong>`integer`

        响应输出项的索引。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_Le1TDl7VfyHQxl47DtGxI",
            "type": "response.audio.done",
            "response_id": "resp_HaVOPdbmX6vifiV5pAfJY",
            "item_id": "item_Ls6MtCUWO7LM4E59QziNv",
            "output_index": 0,
            "content_index": 0
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio\_transcript.delta <span id="35396453cfood" />

当输出模态包含音频，且模型增量生成新的音频对应的文本时，服务端将返回 `response.audio_transcript.delta` 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio_transcript.delta`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引。

        <strong>content\_index</strong>`integer`

        响应输出项的索引。

        <strong>delta</strong>`string`

        增量文本。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_BksW7fOwnyavZdDxIzZYM",
            "type": "response.audio_transcript.delta",
            "response_id": "resp_HaVOPdbmX6vifiV5pAfJY",
            "item_id": "item_Ls6MtCUWO7LM4E59QziNv",
            "output_index": 0,
            "content_index": 0,
            "delta": "有什么"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio\_transcript.done <span id="f4d1698567bsm" />

当输出模态包含音频，且模型完成音频转录后，服务端将返回 `response.audio_transcript.done` 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio_transcript.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引。

        <strong>content\_index</strong>`integer`

        响应输出项的索引。

        <strong>transcript</strong>`string`

        完整文本。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_X49tL2WerT4WjxcmH16lS",
            "type": "response.audio_transcript.done",
            "response_id": "resp_HaVOPdbmX6vifiV5pAfJY",
            "item_id": "item_Ls6MtCUWO7LM4E59QziNv",
            "output_index": 0,
            "content_index": 0,
            "transcript": "你好呀！有什么我可以帮你的吗？"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.function\_call\_arguments.delta <span id="sv01-fc-args-delta-h2" />

当模型以流式方式生成函数调用的参数字符串时，每产生一段新内容，服务端推送一次本事件。客户端应按接收顺序将各事件中的 `delta` 字段拼接，得到与当前进度一致的参数文本；完整内容以随后的 `response.function_call_arguments.done` 为准。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.function_call_arguments.delta`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        该响应中输出项的索引。

        <strong>call\_id</strong>`string`

        本次函数调用的唯一 ID，与同一轮中的 `done` 事件保持一致。

        <strong>delta</strong>`string`

        本段新增的参数字符串片段（增量）。需按顺序拼接。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_SlKoJyEbPEqLq14DSM1u5",
            "type": "response.function_call_arguments.delta",
            "response_id": "resp_JnTOsWXlFhKcFohZbtfz6",
            "item_id": "item_Rhcms7CauTNsQprV5S4Hr",
            "output_index": 0,
            "call_id": "call_2be200f4cafe419b9530dd",
            "delta": " {\"location\": \"北京\"}"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.function\_call\_arguments.done <span id="sv02-fc-args-done-h2" />

函数调用参数已全部生成完毕。本事件中的 `arguments` 为完整的参数字符串。客户端可在收到本事件后解析参数并调用本地工具函数；应以本事件中的完整 `arguments` 为准，而非 `delta` 拼接结果。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.function_call_arguments.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        该响应中输出项的索引。

        <strong>call\_id</strong>`string`

        本次函数调用的唯一 ID。

        <strong>name</strong>`string`

        被调用的函数名称。

        <strong>arguments</strong>`string`

        函数调用的完整参数，一般以 JSON 字符串形式表示。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_X6suLyuL5agdH7r6koesM",
            "type": "response.function_call_arguments.done",
            "response_id": "resp_JnTOsWXlFhKcFohZbtfz6",
            "item_id": "item_Rhcms7CauTNsQprV5S4Hr",
            "output_index": 0,
            "name": "get_current_weather",
            "call_id": "call_2be200f4cafe419b9530dd",
            "arguments": " {\"location\": \"北京\"}"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.output\_item.added <span id="dae2260d40qtu" />

在响应生成过程中创建新项目时，服务端返回此事件。项目类型可以是 `message`（常规消息）、`function_call`（工具调用），Qwen3.8-Omni-Flash-Realtime 还支持 `mcp_call`，事件示例见[MCP 对话项](#mcp-items)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.output_item.added`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引。

        <strong>item</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          输出项的唯一ID。

          <strong>object</strong>`string`

          `message` 和 `function_call` 项固定为 `realtime.item`；MCP 项若返回该字段，值也为 `realtime.item`。

          <strong>status</strong>`string`

          输出项的状态。

          <strong>role</strong>`string`

          仅消息项包含，表示消息的角色。

          <strong>content</strong>`array`

          消息的内容。当 type 为 `message` 时存在。本页示例中初始为空数组；完成项的内容见 `response.output_item.done` 示例。

          <strong>type</strong>`string`

          输出项的类型。可选值包括 `message`（常规消息）、`function_call`（工具调用），Qwen3.8-Omni-Flash-Realtime 还支持 `mcp_call`，事件示例见[MCP 对话项](#mcp-items)。

          <strong>name</strong>`string`

          当 type 为 `function_call` 时，被调用的函数名称。

          <strong>call\_id</strong>`string`

          当 type 为 `function_call` 时，本次函数调用的唯一 ID。

          <strong>arguments</strong>`string`

          当 type 为 `function_call` 时，函数调用的参数（JSON 字符串）。在 added 事件中初始为空字符串。

          <Accordion title="type=mcp_call（初始项）" defaultOpen>
            <strong>object</strong>`string`（必填）

            固定为 `realtime.item`。

            <strong>status</strong>`string`（必填）

            初始为 `in_progress`。

            <strong>server\_label</strong>`string`（必填）

            实际执行工具的 MCP Server 标识。

            <strong>name</strong>`string`（必填）

            工具原始名称。

            <strong>call\_id</strong>`string`（必填）

            本次调用的唯一 ID。

            <strong>arguments</strong>`string`（必填）

            初始通常为空字符串；完整参数以 `response.mcp_call_arguments.done` 和最终项为准。
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
            "event_id": "event_DsCO341DEVtiATtCB6BUY",
            "type": "response.output_item.added",
            "response_id": "resp_HaVOPdbmX6vifiV5pAfJY",
            "output_index": 0,
            "item": {
                "id": "item_Ls6MtCUWO7LM4E59QziNv",
                "object": "realtime.item",
                "type": "message",
                "status": "in_progress",
                "role": "assistant",
                "content": []
            }
        }
        // 工具调用场景
        {
            "event_id": "event_HXmKt5pGoiRtXx7Hq7zpN",
            "type": "response.output_item.added",
            "response_id": "resp_TucN5QgymL5MA8vkJvFlS",
            "output_index": 0,
            "item": {
                "id": "item_FEG9qJGNkPcdf4et3p7BV",
                "object": "realtime.item",
                "type": "function_call",
                "status": "in_progress",
                "call_id": "call_bc0a7fb7235840f69ecfe4",
                "name": "get_current_weather",
                "arguments": ""
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.output\_item.done <span id="f580421f45w3h" />

当新的项目输出完成时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.output_item.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引。

        <strong>item</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          输出项的唯一ID。

          <strong>object</strong>`string`

          `message` 和 `function_call` 项固定为 `realtime.item`；MCP 项若返回该字段，值也为 `realtime.item`。

          <strong>status</strong>`string`

          输出项的状态。

          <strong>role</strong>`string`

          仅消息项包含，表示消息的角色。

          <strong>content</strong>`array[object]`

          消息的内容。当 type 为 `message` 时存在。下列字段对应本页示例中的数组元素。

          <Accordion title="数组元素" defaultOpen>
            <strong>type</strong>`string`

            内容片段的类型；本页示例为 `audio`。

            <strong>text</strong>`string`

            本页示例中的文本内容。
          </Accordion>

          <strong>type</strong>`string`

          输出项的类型。可选值包括 `message`（常规消息）、`function_call`（工具调用），Qwen3.8-Omni-Flash-Realtime 还支持 `mcp_call`，事件示例见[MCP 对话项](#mcp-items)。

          <strong>name</strong>`string`

          当 type 为 `function_call` 时，被调用的函数名称。

          <strong>call\_id</strong>`string`

          当 type 为 `function_call` 时，本次函数调用的唯一 ID。

          <strong>arguments</strong>`string`

          当 type 为 `function_call` 时，函数调用的完整参数（JSON 字符串）。

          <Accordion title="type=mcp_call（最终项）" defaultOpen>
            <strong>status</strong>`string`（必填）

            `completed` 或 `failed`。

            <strong>server\_label</strong>`string`（必填）

            实际执行工具的 MCP Server 标识。

            <strong>name</strong>`string`（必填）

            工具原始名称。

            <strong>call\_id</strong>`string`（必填）

            本次调用的唯一 ID。

            <strong>arguments</strong>`string`（必填）

            完整参数 JSON 字符串。

            <strong>output</strong>`string`（可选）

            MCP `tools/call.result` 对象序列化后的 JSON 字符串；部分失败场景也可能出现。

            <strong>error</strong>`object`（可选）

            调用失败时出现。

            <Accordion title="属性" defaultOpen>
              <strong>type</strong>`string`（必填）

              固定为 `tool_execution_error`。

              <strong>message</strong>`string`（必填）

              面向客户端的安全错误描述，不包含上游敏感响应体。
            </Accordion>

            MCP Server 返回 `isError=true` 时最终状态为 `failed`，也可能保留原始 `output`。
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
            "event_id": "event_MEu5nlLw1LsOguHiehIP8",
            "type": "response.output_item.done",
            "response_id": "resp_HaVOPdbmX6vifiV5pAfJY",
            "output_index": 0,
            "item": {
                "id": "item_Ls6MtCUWO7LM4E59QziNv",
                "object": "realtime.item",
                "type": "message",
                "status": "completed",
                "role": "assistant",
                "content": [
                    {
                        "type": "audio",
                        "text": "你好呀！有什么我可以帮你的吗？"
                    }
                ]
            }
        }
        // 工具调用场景
        {
            "event_id": "event_FHspdfAnCyjuME3mmAwSY",
            "type": "response.output_item.done",
            "response_id": "resp_TucN5QgymL5MA8vkJvFlS",
            "output_index": 0,
            "item": {
                "id": "item_FEG9qJGNkPcdf4et3p7BV",
                "object": "realtime.item",
                "type": "function_call",
                "status": "completed",
                "call_id": "call_bc0a7fb7235840f69ecfe4",
                "name": "get_current_weather",
                "arguments": " {\"location\": \"杭州\"}"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.content\_part.added <span id="de7fa0b877j25" />

在响应生成过程中，向助手消息项中添加新内容部分时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.content_part.added`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引，目前固定为 0。

        <strong>content\_index</strong>`integer`

        响应输出项中内部部分的索引, 目前固定为 0。

        <strong>part</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          内容部分的类型。

          <strong>text</strong>`string`

          内容部分的文本。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_AVBOmrgY3C8bjlRajfSUT",
            "type": "response.content_part.added",
            "response_id": "resp_HaVOPdbmX6vifiV5pAfJY",
            "item_id": "item_Ls6MtCUWO7LM4E59QziNv",
            "output_index": 0,
            "content_index": 0,
            "part": {
                "type": "audio",
                "text": ""
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.content\_part.done <span id="011ad54242wft" />

在助手消息项中的内容部分完成流式传输时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.content_part.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引，目前固定为 0。

        <strong>content\_index</strong>`integer`

        该项内容数组中内容部分的索引，目前固定为 0。

        <strong>part</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          内容部分的类型。

          <strong>text</strong>`string`

          内容部分的文本。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_Il8HD19v58Qr5IBkw7LtN",
            "type": "response.content_part.done",
            "response_id": "resp_HaVOPdbmX6vifiV5pAfJY",
            "item_id": "item_Ls6MtCUWO7LM4E59QziNv",
            "output_index": 0,
            "content_index": 0,
            "part": {
                "type": "audio",
                "text": "你好呀！有什么我可以帮你的吗？"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## MCP 事件 <span id="qwen38-server" />

本节介绍 `qwen3.8-omni-flash-realtime` 的 WebSocket 事件与字段。基础事件与字段见前述事件说明。表中“必填”表示所属对象出现时必须提供。ID 均为不透明字符串，不应依赖其长度、前缀或生成规则。`arguments`、`output` 的外层类型为 string，读取内容时需要再次解析 JSON。MCP 连接、工具发现和调用受服务配额及超时限制。

### mcp\_list\_tools.\*

以下三个事件具有相同的字段结构：

| 事件 type                       | 触发时机                        |
| ----------------------------- | --------------------------- |
| mcp\_list\_tools.in\_progress | 开始发现某个 MCP Server 的工具       |
| mcp\_list\_tools.completed    | 工具发现成功并完成 allowed\_tools 过滤 |
| mcp\_list\_tools.failed       | 工具发现失败、超时或结果超过服务限制          |

| 字段路径      | 类型     | 必填 | 说明                                 |
| --------- | ------ | -- | ---------------------------------- |
| event\_id | string | 是  | 本条服务端事件的唯一 ID                      |
| type      | string | 是  | 上表中的固定事件类型                         |
| item\_id  | string | 是  | 本次工具发现对应的 mcp\_list\_tools item ID |

`mcp_list_tools.in_progress` 可能早于 `session.updated` 到达。对于 completed 和 failed，服务端会先发送包含最终工具列表或错误的 `conversation.item.created`，再发送相同 item\_id 的状态事件。

示例：

```json
{
  "event_id": "opaque_event_id",
  "type": "mcp_list_tools.completed",
  "item_id": "opaque_item_id"
}
```

### response.mcp\_call\_arguments.delta

| 字段路径          | 类型      | 必填 | 说明                                      |
| ------------- | ------- | -- | --------------------------------------- |
| event\_id     | string  | 是  | 本条服务端事件的唯一 ID                           |
| type          | string  | 是  | 固定为 response.mcp\_call\_arguments.delta |
| response\_id  | string  | 是  | 父 Response ID                           |
| item\_id      | string  | 是  | 当前 mcp\_call item ID                    |
| output\_index | integer | 是  | 当前 item 在父 Response output 数组中的零基索引     |
| delta         | string  | 是  | 工具参数 JSON 字符串的本次增量片段，按事件顺序拼接            |
| obfuscation   | string  | 否  | 可选混淆字符串；客户端可以忽略，不影响 delta 拼接和参数解析       |

示例：

```json
{
  "event_id": "opaque_event_id",
  "type": "response.mcp_call_arguments.delta",
  "response_id": "opaque_response_id",
  "item_id": "opaque_item_id",
  "output_index": 0,
  "delta": "{\"city\":\"杭",
  "obfuscation": "opaque-value"
}
```

### response.mcp\_call\_arguments.done

| 字段路径          | 类型      | 必填 | 说明                                     |
| ------------- | ------- | -- | -------------------------------------- |
| event\_id     | string  | 是  | 本条服务端事件的唯一 ID                          |
| type          | string  | 是  | 固定为 response.mcp\_call\_arguments.done |
| response\_id  | string  | 是  | 父 Response ID                          |
| item\_id      | string  | 是  | 当前 mcp\_call item ID                   |
| output\_index | integer | 是  | 当前 item 在父 Response output 数组中的零基索引    |
| arguments     | string  | 是  | 完整工具参数，内容为 JSON 字符串；客户端应以本字段为准         |

示例：

```json
{
  "event_id": "opaque_event_id",
  "type": "response.mcp_call_arguments.done",
  "response_id": "opaque_response_id",
  "item_id": "opaque_item_id",
  "output_index": 0,
  "arguments": "{\"city\":\"杭州\"}"
}
```

### response.mcp\_call.\*

| 事件 type                         | 触发时机                              |
| ------------------------------- | --------------------------------- |
| response.mcp\_call.in\_progress | 参数生成完成，且审批通过或无需审批，开始调用 MCP Server |
| response.mcp\_call.completed    | MCP 工具调用成功                        |
| response.mcp\_call.failed       | 连接、协议、工具业务错误、审批拒绝、审批超时、调用超时或取消    |

三个事件均使用以下字段：

| 字段路径          | 类型      | 必填 | 说明                                  |
| ------------- | ------- | -- | ----------------------------------- |
| event\_id     | string  | 是  | 本条服务端事件的唯一 ID                       |
| type          | string  | 是  | 上表中的固定事件类型                          |
| item\_id      | string  | 是  | 当前 mcp\_call item ID                |
| output\_index | integer | 是  | 当前 item 在父 Response output 数组中的零基索引 |

示例：

```json
{
  "event_id": "opaque_event_id",
  "type": "response.mcp_call.in_progress",
  "item_id": "opaque_item_id",
  "output_index": 0
}
```

## MCP 对话项 <span id="mcp-items" />

以下对象是事件中 `item` 的取值类型，适用于 Qwen3.8-Omni-Flash-Realtime。

### mcp\_list\_tools <span id="mcp-list-tools-item" />

该对象通过 `conversation.item.created.item` 返回；字段见 [conversation.item.created](#4f23bd31e6awz) 的 `type=mcp_list_tools` 分支。

示例：

```json
{
  "event_id": "opaque_event_id",
  "type": "conversation.item.created",
  "item": {
    "id": "opaque_item_id",
    "type": "mcp_list_tools",
    "server_label": "amap",
    "tools": [
      {
        "name": "maps_weather",
        "description": "查询天气",
        "input_schema": {
          "type": "object",
          "properties": {
            "city": {
              "type": "string",
              "description": "城市名"
            }
          },
          "required": ["city"]
        }
      }
    ]
  }
}
```

### 初始 mcp\_call <span id="mcp-call-initial" />

模型选择 MCP 工具时，该对象出现在 `response.output_item.added.item` 中，也可能通过 `conversation.item.created.item` 返回；字段见 [response.output\_item.added](#dae2260d40qtu) 和 [conversation.item.created](#4f23bd31e6awz) 的对应 `item` 分支。

`response.output_item.added` 示例：

```json
{
  "event_id": "opaque_event_id",
  "type": "response.output_item.added",
  "response_id": "opaque_response_id",
  "output_index": 0,
  "item": {
    "id": "opaque_item_id",
    "object": "realtime.item",
    "type": "mcp_call",
    "status": "in_progress",
    "call_id": "opaque_call_id",
    "server_label": "amap",
    "name": "maps_weather",
    "arguments": ""
  }
}
```

### mcp\_approval\_request <span id="mcp-approval-item" />

该对象通过 `conversation.item.created` 返回；事件顶层字段和审批请求 `item` 字段见 [conversation.item.created](#4f23bd31e6awz)。

示例：

```json
{
  "event_id": "opaque_event_id",
  "type": "conversation.item.created",
  "response_id": "opaque_response_id",
  "item_id": "opaque_approval_id",
  "previous_item_id": "opaque_item_id",
  "item": {
    "id": "opaque_approval_id",
    "type": "mcp_approval_request",
    "server_label": "amap",
    "name": "maps_weather",
    "arguments": "{\"city\":\"杭州\"}",
    "call_id": "opaque_call_id"
  }
}
```

### 最终 mcp\_call <span id="mcp-call-final" />

MCP 调用进入终态后，该对象出现在 `response.output_item.done.item` 中，并进入父 `response.done.response.output` 的最终快照；字段见 [response.output\_item.done](#f580421f45w3h) 和 [response.done](#f2333c777d9s4) 的对应分支。

成功示例：

```json
{
  "event_id": "opaque_event_id",
  "type": "response.output_item.done",
  "response_id": "opaque_response_id",
  "output_index": 0,
  "item": {
    "id": "opaque_item_id",
    "type": "mcp_call",
    "status": "completed",
    "call_id": "opaque_call_id",
    "server_label": "amap",
    "name": "maps_weather",
    "arguments": "{\"city\":\"杭州\"}",
    "output": "{\"content\":[{\"type\":\"text\",\"text\":\"...\"}],\"isError\":false}"
  }
}
```

失败示例：

```json
{
  "event_id": "opaque_event_id",
  "type": "response.output_item.done",
  "response_id": "opaque_response_id",
  "output_index": 0,
  "item": {
    "id": "opaque_item_id",
    "type": "mcp_call",
    "status": "failed",
    "call_id": "opaque_call_id",
    "server_label": "amap",
    "name": "maps_weather",
    "arguments": "{\"city\":\"杭州\"}",
    "error": {
      "type": "tool_execution_error",
      "message": "MCP tool call failed (call_timeout)."
    }
  }
}
```

### MCP 错误 <span id="mcp-error" />

MCP 错误可能发生在工具发现或工具调用阶段。工具发现失败时的 `error` 字段见 [conversation.item.created](#4f23bd31e6awz) 的 `mcp_list_tools` 分支；工具调用失败时见 [response.output\_item.done](#f580421f45w3h) 的 `mcp_call` 分支。

> 相关文档：[实时（Qwen-Omni-Realtime）](/zh/model-studio/realtime)。
