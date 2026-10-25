> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 客户端事件

> Qwen-Omni-Realtime API的客户端事件参考。

<meta property="og:title" content="客户端事件" />

<style>.markdown-body:has(#omni-session-update) \{ overflow: clip; } #omni-session-update td:nth-child(2) .help-code-block \{ position: sticky; top: 128px; } #omni-session-update td:nth-child(2) .help-code-block pre \{ max-height: calc(100vh - 176px); max-height: calc(100dvh - 176px); overflow: auto; } .adoc-mdx-content:has(#omni-session-update) .api-example-panel .code-block \{ margin-top: 16px; } .markdown-body:has(#omni-session-update) table td:nth-child(2) .help-code-block \{ margin-top: 0; } #qwen38-mcp-approval-response \{ scroll-margin-top: 128px; }</style>

## session.update <span id="26a8302028sjm" />

建立 WebSocket 连接后，发送此事件更新会话的默认配置。服务端收到 `session.update` 事件后校验参数，若参数不合法则返回错误，若参数合法则应用更改并返回会话配置。

<table id="omni-session-update" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为 `session.update`。

        <strong>session</strong>`object`（可选）

        会话配置。

        Qwen3.8-Omni-Flash-Realtime 更新会话时必须提供 `session`。全部输入 Token 总数上限为 196608，输入长度计算规则与 Qwen3.5-Omni-Realtime 一致。

        <Accordion title="属性" defaultOpen>
          <strong>modalities</strong>`array`（可选）

          模型输出模态设置，可选值：

          - \["text"]

            仅输出文本。
          - \["text","audio"]（默认值）

            同时输出文本和音频。

          <strong>voice</strong>`string`（可选）

          模型生成音频的音色，支持的音色参见[音色列表](/zh/model-studio/realtime#f4c9fd97f221z)。

          默认音色：

          - Qwen3.8-Omni-Flash-Realtime：`Tina`
          - Qwen3.5-Omni-Realtime 系列模型：`Tina`
          - qwen3-omni-flash-realtime-2025-09-15：`Cherry`

          <strong>audio</strong>`object`（可选）

          <span id="session-audio" />

          <span id="qwen38-client" />

          输入和输出音频配置。未配置时沿用现有默认行为。

          <Accordion title="属性" defaultOpen>
            <strong>input</strong>`object`（可选）

            用户输入音频配置。

            <Accordion title="属性" defaultOpen>
              <strong>format</strong>`object`（可选）

              输入音频的格式和采样率。建议在会话 IDLE 阶段、发送首段音频前完成配置，音频输入开始后不可修改。

              <Accordion title="属性" defaultOpen>
                <strong>type</strong>`string`（可选）

                `qwen3.5-omni-plus-realtime`、`qwen3.5-omni-flash-realtime` 可选 `pcm`（默认值，单声道、16 bit 裸 PCM）或 `wav`（WAV 容器封装的单声道、16 bit PCM）。Qwen3.8-Omni-Flash-Realtime 的多通道输入只能使用 `pcm`，默认值为 `pcm`。

                <strong>sample\_rate</strong>`integer`（可选）

                `qwen3.5-omni-plus-realtime`、`qwen3.5-omni-flash-realtime` 可选 8000、16000（默认值）、24000、48000 Hz。Qwen3.8-Omni-Flash-Realtime 的多通道输入只能使用 16000 Hz，默认值为 16000 Hz。

                <span id="session-audio-multichannel" />以下四个字段适用于 Qwen3.8-Omni-Flash-Realtime 的 WebSocket 多通道输入：

                <strong>sample\_format</strong>`string`（可选）

                PCM 采样格式，只能为 `s16le`，默认值为 `s16le`。

                <strong>channels</strong>`integer`（可选）

                输入声道数，仅支持 1（默认值）、2、4。2、4 声道空间音频的输入 Token 数均为普通音频的 2 倍，详见[Token 计算](/zh/model-studio/realtime#cfba3898e4d0h)。

                <strong>packing</strong>`string`（可选）

                多通道样本排列方式，只能为 `interleaved`，默认值为 `interleaved`。

                <strong>channel\_layout</strong>`string`（可选）

                按 `channels` 确定默认值：1 声道为 `mono`，2 声道为 `raw_mic_array`，4 声道为 `foa_ambix`。显式提供时必须与 `channels` 匹配。

                多通道输入必须使用 PCM、16000 Hz、s16le 和 interleaved；字段省略时使用上述默认值，一旦提供则必须符合类型和值约束，不得以 `null` 代替省略。
              </Accordion>
            </Accordion>

            <strong>output</strong>`object`（可选）

            模型输出音频配置。

            <Accordion title="属性" defaultOpen>
              <strong>format</strong>`object`（可选）

              模型输出音频的格式和采样率。建议在会话建立初期、尚未开始音频交互前完成配置。

              <Accordion title="属性" defaultOpen>
                <strong>type</strong>`string`（可选）

                `qwen3.5-omni-plus-realtime`、`qwen3.5-omni-flash-realtime` 可选 `pcm`（默认值，单声道、16 bit 裸 PCM）或 `wav`（WAV 容器封装的单声道、16 bit PCM）。

                <strong>sample\_rate</strong>`integer`（可选）

                `qwen3.5-omni-plus-realtime`、`qwen3.5-omni-flash-realtime` 可选 8000、16000、24000（默认值）、48000 Hz。
              </Accordion>

              <span id="session-audio-voice" /><strong>voice</strong>`string`（可选）

              Qwen3.8-Omni-Flash-Realtime 的输出音色，默认为 `Tina`。支持的音色及对应的 `voice` 参数值见[音色列表](/zh/model-studio/omni-voice-list#qwen38-voices)。`session.voice` 是兼容旧接入的字段，新接入建议使用 `session.audio.output.voice`。如果两个字段都设置，输出音频使用 `session.audio.output.voice` 指定的音色。
            </Accordion>
          </Accordion>

          <strong>input\_audio\_transcription</strong>`object｜null`（可选）

          <span id="session-input-audio-transcription" />

          输入音频转录配置。设为 `null` 可关闭。

          <Accordion title="属性" defaultOpen>
            <strong>model</strong>`string`（必选）

            固定为 `qwen3-asr-flash-realtime`；设置 `{"model":"qwen3-asr-flash-realtime"}` 可启用输入音频转录，不支持修改模型值。Python SDK 的 `enable_input_audio_transcription` 是 `update_session` 方法参数，不是 `session.update` 的顶层字段。
          </Accordion>

          <strong>input\_audio\_format</strong>`string`（可选）

          历史兼容字段，新增接入建议使用 `audio.input.format` 同时配置输入格式和采样率。

          <strong>output\_audio\_format</strong>`string`（可选）

          历史兼容字段，新增接入建议使用 `audio.output.format` 同时配置输出格式和采样率。

          <strong>smooth\_output</strong>`boolean｜null`（可选）

          <strong>仅在使用 `qwen3-omni-flash-realtime-2025-09-15` 时生效。</strong>

          控制回复风格。可选值：

          - `true`（默认值）：口语化风格。
          - `false`：书面化、正式风格。

            > 难以朗读的内容可能效果不好。
          - `null`：模型自动选择口语化或书面化风格。

          <strong>instructions</strong>`string`（可选）

          系统消息，用于设定模型的目标或角色。

          <strong>turn\_detection</strong>`object`（可选）

          语音活动检测（VAD）配置。设为 `null` 可禁用 VAD，改为手动触发模型响应。若不提供此字段，系统将使用默认参数启用 VAD。

          <Accordion title="属性">
            <strong>type</strong>`string`（可选）

            VAD 类型，可选值：

            - `server_vad`（默认值）：基于声学特征检测语音结束。
            - `semantic_vad`：基于语义有效性检测语音结束，可过滤回应语、背景音等无意义声音。Qwen3.8-Omni-Flash-Realtime 和 Qwen3.5-Omni-Realtime 系列模型支持。
            - `semantic_vad_ec`：适用于客服和商务场景的语义有效性检测。相比 `semantic_vad`，更易于检测到用户说话并打断模型回复，同样可过滤背景音等无意义声音。Qwen3.8-Omni-Flash-Realtime 系列模型支持。

            <strong>threshold</strong>`float`（可选）

            VAD 灵敏度。值越低，VAD 越灵敏，越容易将微弱声音（包括背景噪音）识别为语音；值越高，越不灵敏，需要更清晰、音量更大的语音才能触发。

            取值范围：`[-1.0, 1.0]`，默认值为 0.5。

            <strong>silence\_duration\_ms</strong>`integer`（可选）

            语音结束后需保持静音的最短时间（毫秒），超时即触发模型响应。值越低，响应越快，但可能在短暂停顿时误触发。

            取值范围：\[200, 6000]，默认值为 800。

            <strong>idle\_timeout\_ms</strong>`integer`（可选）

            <strong>仅在使用 `qwen3.5-omni-plus-realtime` 或 `qwen3.5-omni-flash-realtime` 模型且 VAD 类型为 `server_vad` 时生效。</strong>

            静默超时时间（毫秒）。服务端完成音频播报且用户持续静默超过该时间（未触发 `speech.started`）后，模型将主动触发一轮响应，基于当前上下文引导用户继续对话。超时计时从上一条模型响应的音频播放完毕后开始。

            取值范围：\[5000, 30000]。
          </Accordion>

          <strong>enable\_search</strong>`boolean`（可选）

          适用于 Qwen3.8-Omni-Flash-Realtime 和 Qwen3.5-Omni-Realtime 系列模型。

          是否启用联网搜索。设为 `true` 启用，默认为 `false`。启用后，模型可自主判断是否需要联网搜索来回答用户问题。

          > tools 和 enable\_search 不兼容，不可同时开启。

          <strong>search\_options</strong>`object`（可选）

          联网搜索选项配置，需先启用 `enable_search` 才生效。

          <Accordion title="属性">
            <strong>enable\_source</strong>`boolean`（可选）

            是否在响应中返回搜索结果来源列表。设为 `true` 启用。
          </Accordion>

          <strong>video</strong>`object`（可选）

          <span id="session-video" />

          Qwen3.8-Omni-Flash-Realtime 的视频输入配置。

          <Accordion title="属性" defaultOpen>
            <strong>input</strong>`object`（可选）

            视频输入参数。

            <Accordion title="属性" defaultOpen>
              <strong>representation\_compact</strong>`string`（可选）

              视频输入表征聚合方式；会话初始默认 `none`，保留完整的细粒度视频输入表征。`normal` 聚合表征以降低计算开销，适用于对视觉细节要求不高的场景；相同视频输入的 Token 数为 `none` 模式的 1/4，详见[Token 计算](/zh/model-studio/realtime#cfba3898e4d0h)。应在发送首段音频前设置，音频输入开始后不可修改。
            </Accordion>
          </Accordion>

          <strong>tools</strong>`array`（可选）

          <span id="session-tools" />

          工具定义列表。配置后模型可根据用户输入自主决定是否调用工具。Qwen3.8-Omni-Flash-Realtime 可在同一会话中同时配置 Function Calling 和 MCP 工具，数组元素按 `type` 区分；`tools` 与 `enable_search` 不可同时开启。

          <Accordion title="Function Calling（type=function）" defaultOpen>
            <strong>type</strong>`string`（必选）

            固定为 `function`。

            <strong>function</strong>`object`（必选）

            自定义函数定义。

            <Accordion title="属性" defaultOpen>
              <strong>name</strong>`string`（必选）

              工具函数名称，建议与函数名保持一致，例如 `get_current_weather` 或 `get_current_time`。

              <strong>description</strong>`string`（可选）

              对工具函数功能的描述，模型据此判断是否调用该工具。

              <strong>parameters</strong>`object`（可选）

              对工具函数入参的描述，模型据此提取所需入参。函数无需入参时可不指定。

              <Accordion title="属性" defaultOpen>
                <strong>type</strong>`string`（必选）

                固定为 `object`。

                <strong>properties</strong>`object`（可选）

                按名称列出函数的入参定义。右侧示例中的 `location` 是自定义入参名称，其 `type` 和 `description` 分别描述数据类型和用途。

                <strong>required</strong>`array`（可选）

                指定哪些入参为必填项。
              </Accordion>
            </Accordion>
          </Accordion>

          <span id="session-tools-mcp" />

          <Accordion title="MCP（type=mcp）" defaultOpen>
            字段标为“必选”表示该 MCP 数组元素出现时必须提供。每次新增或更新 MCP 配置都必须同时提供 `server_label` 和 `server_url`，不能只传 `server_label` 复用已有配置；仅在没有活动 Response 时更新。MCP 连接、工具发现和调用受服务配额及超时限制，详见[MCP 调用限制](/zh/model-studio/omni-realtime-interaction-process#qwen38-mcp-limits)。

            <strong>type</strong>`string`（必选）

            固定为 `mcp`。

            <strong>server\_label</strong>`string`（必选）

            MCP Server 的会话内唯一标识，1～64 位，仅允许字母、数字、下划线和连字符。

            <strong>server\_url</strong>`string`（必选）

            MCP Streamable HTTP 地址，必须为公网 HTTPS 443 地址，最长 4096 字符；不得包含用户名、密码或 URL fragment，域名解析结果必须为公网地址。

            <strong>authorization</strong>`string`（可选）

            出站 Authorization 值，最长 8192 字符，仅允许可打印 ASCII 字符。

            <strong>headers</strong>`object`（可选）

            额外出站 HTTP Header，最多 16 个键值对。名称和值均为字符串。名称长 1～128 位，仅允许标准 HTTP Header 名称字符；值最长 8192 字符，仅允许可打印 ASCII 字符。

            名称不得使用 `mcp-`、`proxy-`、`x-forwarded-` 前缀，也不得为 `host`、`authorization`、`connection`、`content-length`、`transfer-encoding`、`accept`、`content-type`、`forwarded`、`cookie`、`origin`、`upgrade`、`te`、`trailer`。

            <strong>allowed\_tools</strong>`array[string]`（可选）

            工具发现后的白名单过滤。省略表示允许全部，空数组表示不暴露任何工具。每个工具名 1～64 位，仅允许字母、数字、下划线、点和连字符。

            <strong>require\_approval</strong>`string`（可选）

            工具调用审批策略，可选 `always`（默认）或 `never`。

            `server_url`、`authorization` 和 `headers` 仅用于服务端连接，不在 `session.updated` 中回显；客户端不应依赖该事件重建这些敏感配置。
          </Accordion>

          <strong>temperature</strong>`float`（可选）

          采样温度，控制输出内容的多样性。值越高，输出越多样；值越低，输出越确定。

          取值范围：\[0, 2)。

          由于 temperature 和 top\_p 均可控制多样性，建议只设置其中一个。

          默认值：

          - `qwen3.8-omni-flash-realtime`：0.6
          - Qwen3.5-Omni-Realtime 系列模型：0.7
          - `qwen3-omni-flash-realtime-2025-09-15`：0.9

          <strong>top\_p</strong>`float`（可选）

          核采样概率阈值，控制输出内容的多样性。值越高，输出越多样；值越低，输出越确定。

          取值范围：(0, 1.0]。

          由于 temperature 和 top\_p 均可控制多样性，建议只设置其中一个。

          默认值：

          - `qwen3.8-omni-flash-realtime`：0.95
          - Qwen3.5-Omni-Realtime 系列模型：0.8
          - `qwen3-omni-flash-realtime-2025-09-15`：1.0

          <strong>top\_k</strong>`integer`（可选）

          采样候选集大小。例如设为 50 时，每步生成仅从得分最高的 50 个 Token 中采样。值越大，随机性越高；值越小，确定性越高。设为 `null` 或大于 100 时，禁用 top\_k 策略，仅 `top_p` 生效。

          取值需大于或等于 0。

          默认值：

          - `qwen3.8-omni-flash-realtime`：20
          - Qwen3.5-Omni-Realtime 系列模型：20
          - `qwen3-omni-flash-realtime-2025-09-15`：50

          <strong>max\_tokens</strong>`integer`（可选）

          本次请求返回的最大 Token 数。

          > `max_tokens` 不影响模型的生成过程。若模型生成的 Token 数超过 `max_tokens`，响应将被截断。

          `qwen3.8-omni-flash-realtime` 的取值范围为 \[1, 65536]。其他模型的默认值和最大值均为模型的最大输出长度，各模型的最大输出长度参见[模型列表](/zh/model-studio/models)。

          适用于需要限制输出长度的场景，如生成摘要或关键词、控制成本、缩短响应时间等。

          <strong>repetition\_penalty</strong>`float`（可选）

          控制生成内容中连续序列的重复度。值越高，重复惩罚越强；1.0 表示不做惩罚。`qwen3.8-omni-flash-realtime` 支持取值 0；其他模型取值需大于 0，无严格上限。

          默认值：

          - `qwen3.8-omni-flash-realtime`：0
          - Qwen3.5-Omni-Realtime 系列模型：1.0
          - `qwen3-omni-flash-realtime-2025-09-15`：1.05

          <strong>presence\_penalty</strong> `float`（可选）

          控制生成内容的重复度。

          取值范围：\[-2.0, 2.0]。正数降低重复度，负数增加重复度。

          默认值：

          - `qwen3.8-omni-flash-realtime`：1
          - Qwen3.5-Omni-Realtime 系列模型：1.5
          - `qwen3-omni-flash-realtime-2025-09-15`：0.0

          适用场景：

          较高值适合创意写作、头脑风暴等需要多样性和创造性的场景。

          较低值适合技术文档等需要一致性和专业术语的场景。

          <strong>seed</strong>`integer`（可选）

          提高生成过程的确定性，常用于在相同参数下复现相同结果。

          每次调用时传入相同的 seed 值并保持其他参数不变，模型将尽可能返回相同的结果。

          取值范围：0 到 2<sup>31</sup>−1，默认值为 -1。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
            "event_id": "event_ToPZqeobitzUJnt3QqtWg",
            "type": "session.update",
            "session": {
                "modalities": [
                    "text",
                    "audio"
                ],
                "model": "qwen3.5-omni-flash-realtime",
                "voice": "Tina",
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
                "instructions": "你是某五星级酒店的AI客服专员，请准确且友好地解答客户关于房型、设施、价格、预订政策的咨询。请始终以专业和乐于助人的态度回应，杜绝提供未经证实或超出酒店服务范围的信息。",
                "turn_detection": {
                    "type": "server_vad",
                    "threshold": 0.5,
                    "silence_duration_ms": 800
                },
                "enable_search": false,
                "tools": [
                    {
                        "type": "function",
                        "function": {
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
                    }
                ],
                "seed": 1314,
                "max_tokens": 16384,
                "repetition_penalty": 1.05,
                "presence_penalty": 0.0,
                "top_k": 50,
                "top_p": 1.0,
                "temperature": 0.9
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.create <span id="b4b869840ce6m" />

`response.create` 事件用于指示服务端生成模型响应。VAD 模式下，服务端通常根据语音活动自动开始响应；工具调用后的续答仍按下述流程显式触发。Function Calling 场景中，客户端通过 `conversation.item.create` 回传工具结果后，发送此事件触发模型续答。Qwen3.8-Omni-Flash-Realtime 的 MCP 工具由服务端执行；如需基于 MCP 结果续答，应在本次 MCP 调用所在 Response 的 `response.done` 事件到达后发送一次 `response.create`，不附带 MCP 结果。

服务端以 `response.created` 事件开始响应，随后发送一个或多个项和内容事件（如 `conversation.item.created` 和 `response.content_part.added`），最后以 `response.done` 事件表示响应完成。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`（必选）

        事件类型，固定为 `response.create`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "type": "response.create",
            "event_id": "event_1718624400000"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.cancel <span id="38f40cb6447up" />

客户端发送此事件取消正在进行的响应。若当前无响应可取消，服务端将返回错误事件。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`（必选）

        事件类型，固定为 `response.cancel`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_B4o9RHSTWobB5OQdEHLTo",
            "type": "response.cancel"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.append <span id="2ea4b5e41fhjd" />

将音频字节追加到输入音频缓冲区。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`（必选）

        事件类型，固定为 `input_audio_buffer.append`。

        <strong>audio</strong>`string`（必选）

        Base64 编码的音频数据。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_B4o9RHSTWobB5OQdEHLTo",
            "type": "input_audio_buffer.append",
            "audio": "UklGR..."
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.commit <span id="1cbea5fa7fkfl" />

提交输入音频缓冲区，在对话中创建新的用户消息项。若音频缓冲区为空，服务端将返回错误事件。

- [VAD 模式](/zh/model-studio/realtime)：服务端自动提交音频缓冲区，客户端无需发送此事件。
- [Manual 模式](/zh/model-studio/realtime)：客户端必须提交音频缓冲区才能创建用户消息项。

提交音频缓冲区不会触发模型响应，服务端将以 `input_audio_buffer.committed` 事件响应。

> 若客户端已发送过 [input\_image\_buffer.append](/zh/model-studio/client-events#c28ed38410nfw) 事件，input\_audio\_buffer.commit 事件将同时提交图像缓冲区。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`（必选）

        事件类型，固定为 `input_audio_buffer.commit`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_B4o9RHSTWobB5OQdEHLTo",
            "type": "input_audio_buffer.commit"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.clear <span id="3b44f2f2abakw" />

清除音频缓冲区中的字节。服务端以 `input_audio_buffer.cleared` 事件响应。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`（必选）

        事件类型，固定为 `input_audio_buffer.clear`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_audio_buffer.clear"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_image\_buffer.append <span id="c28ed38410nfw" />

将图像数据添加到图像缓冲区。图像可来自本地文件，也可从视频流实时采集。

图片输入限制如下：

- 图像格式必须为 JPG 或 JPEG。建议分辨率为 480p 或 720p 以获得最佳性能，最高不超过 1080p。
- 单张图片经Base64编码后不得超过256KB，建议编码前原始图片大小不超过190KB。
- 图片数据需经过 Base64 编码。
- 建议以 1 张/秒的频率向服务端发送图像。
- 发送 input\_image\_buffer.append 事件前，至少已发送过一次 input\_audio\_buffer.append 事件。

> 图像缓冲区与音频缓冲区通过 [input\_audio\_buffer.commit](/zh/model-studio/client-events#1cbea5fa7fkfl) 事件一起提交。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`（必选）

        事件类型，固定为 `input_image_buffer.append`。

        <strong>image</strong>`string`（必选）

        Base64 编码的图像数据。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_image_buffer.append",
            "image": "xxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.create <span id="tc03itemcreateh" />

客户端发送此事件创建对话项，包括 Function Calling 的工具执行结果，以及 Qwen3.8-Omni-Flash-Realtime 的 MCP 审批回复。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>event\_id</strong>`string`（可选）

        客户端生成的事件 ID，用于日志追踪。

        <strong>type</strong>`string`（必选）

        事件类型，固定为 `conversation.item.create`。

        <strong>item</strong>`object`（必选）

        要创建的对话项，不能为空。

        服务端返回的审批请求 ID、对话项 ID 和事件 ID 均为不透明字符串，不应依赖其长度、前缀或生成规则。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`（可选）

          对话项 ID。可预先指定以对齐本地状态；若未提供，由服务端生成。

          <span id="qwen38-mcp-approval-response" />

          <strong>type</strong>`string`（必选）

          对话项类型：

          - `function_call_output`：Function Calling 的工具执行结果。客户端执行工具后，以此类型回传结果，再发送 `response.create`。
          - `mcp_approval_response`：Qwen3.8-Omni-Flash-Realtime 的 MCP 审批回复。MCP 工具由服务端执行；如需基于结果续答，等待本次 MCP 调用所在 Response 的 `response.done` 事件，再发送一次不附带 MCP 结果的 `response.create`。

          <strong>call\_id</strong>`string`（条件必选）

          仅 `function_call_output` 必填；取 `response.function_call_arguments.done` 事件中返回的 `call_id`。

          <strong>output</strong>`string`（条件必选）

          仅 `function_call_output` 必填；工具函数的执行结果。

          <strong>approval\_request\_id</strong>`string`（条件必选）

          仅 `mcp_approval_response` 必填。当 MCP 配置的 `require_approval` 为 `always` 或省略时，服务端可能发送 `mcp_approval_request`。客户端通过 `conversation.item.create` 回复审批结果，此字段必须精确匹配一个尚未处理的审批请求的 `item.id`。

          <strong>approve</strong>`boolean`（条件必选）

          仅 `mcp_approval_response` 必填；`true` 允许执行，`false` 拒绝且本次调用进入 `failed`。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        **Function Calling 工具结果**

        ```json
        {
            "event_id": "event_55099cddb51b4f208cb95d1a994eef80",
            "type": "conversation.item.create",
            "item": {
                "id": "item_2a80d7682b4e473c9c2154da135041e9",
                "type": "function_call_output",
                "call_id": "call_62c24725afdb4c2680ac54",
                "output": "北京今天天气为霾转晴，气温4/-4℃，微风"
            }
        }
        ```

        **MCP 审批回复（Qwen3.8-Omni-Flash-Realtime）**

        ```json
        {
          "event_id": "event_client_xxx",
          "type": "conversation.item.create",
          "item": {
            "type": "mcp_approval_response",
            "approval_request_id": "opaque_approval_id",
            "approve": true
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

> 另请参见： [实时（Qwen-Omni-Realtime）](/zh/model-studio/realtime) 。
