> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-Audio 实时语音对话客户端事件

> Qwen-Audio Realtime API的客户端事件参考。

<meta property="og:title" content="Qwen-Audio 实时语音对话客户端事件" />

<strong>用户指南</strong>：[实时语音对话（Qwen-Audio-Realtime）](https://help.aliyun.com/zh/model-studio/fun-audiochat-realtime)。如需了解事件交互时序，请参见[WebSocket API](/zh/model-studio/fun-audiochat-realtime-websocket-api)。

## session.update <span id="fc65a1b2c3d5f" />

<strong>说明</strong>：建立连接后，发送此事件更新会话的默认配置。仅包含需要变更的字段，未传字段保持原值。服务端收到后校验参数，若参数不合法则返回错误，若参数合法则应用更改并返回完整配置。

<Note>
  `turn_detection` 仅在首次发送音频之前（IDLE 状态）允许修改。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
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

        <Accordion title="属性" defaultOpen>
          <strong>modalities</strong>`array`（可选）

          模型输出模态设置，可选值：

          - `["text"]`

            仅输出文本。
          - `["audio", "text"]`（默认值）

            同时输出文本和音频。

          <strong>voice</strong>`string`（可选）

          TTS 音色名称，3.1 Plus 的默认值为 `longanqian_v3.1`，3.0 Plus/Flash 的默认值为 `longanqian`。支持系统音色和声音复刻音色，仅可在第一次 `session.update` 中设置，后续传入将被忽略。

          - <strong>3.1 Plus 新增系统音色</strong>：`longanqian_v3.1`、`longanhuan_v3.1`、`longanlingxin_v3.1`、`longanfengyue_v3.1`、`xunanchuan_v3.1`、`beth_v3.1`、`betty_v3.1`、`cally_v3.1`。
          - <strong>3.1 Plus、3.0 Plus/Flash 共有系统音色</strong>：可选值：`longanqian`、`longanlingxin`、`longanlingxi`、`longanxiaoxin`、`longanlufeng`。
          - <strong>声音复刻音色</strong>：通过声音复刻 API 创建，将返回的 `voice_id` 填入此参数。详见[音色配置](https://help.aliyun.com/zh/model-studio/fun-audiochat-realtime#fc60h311)。

          连接 `qwen-audio-3.1-realtime-plus` 后，在首次 `session.update` 中设置音色，例如：

          ```json
          {
              "type": "session.update",
              "session": {
                  "voice": "longanqian_v3.1"
              }
          }
          ```

          <strong>enable\_speech\_emotion</strong>`boolean`（可选）

          是否开启情绪增强功能。开启后，回复音色的情绪变化更明显。默认值：`true`。可选值：`true`、`false`。

          <strong>instructions</strong>`string`（可选）

          系统指令，用于设定模型的角色身份、回答风格和行为偏好。对整个会话生效。

          <strong>input\_audio\_format</strong>`string`（可选）

          输入音频格式。当前仅支持 `pcm`（16kHz 16bit 单声道），为默认值。仅在首次发送音频之前（IDLE 状态）允许修改。

          <strong>output\_audio\_format</strong>`string`（可选）

          输出音频格式。当前仅支持 `pcm`（24kHz 16bit 单声道），为默认值。

          <strong>input\_audio\_transcription</strong>`object`（可选）

          输入音频转写配置。适用于 `qwen-audio-3.1-realtime-plus`。

          <Accordion title="属性">
            <strong>language</strong>`string`（可选）

            输入音频的源语种。

            可选值：

            - `zh`：中文
            - `en`：英语
            - `ja`：日语
            - `ko`：韩语
            - `vi`：越南语
            - `th`：泰语
            - `id`：印尼语
            - `ms`：马来语
            - `tl`：菲律宾语
            - `hi`：印地语
            - `ar`：阿拉伯语
            - `fr`：法语
            - `de`：德语
            - `es`：西班牙语
            - `pt`：葡萄牙语
            - `ru`：俄语
            - `it`：意大利语
            - `nl`：荷兰语
            - `sv`：瑞典语
            - `da`：丹麦语
            - `fi`：芬兰语
            - `no`：挪威语
            - `el`：希腊语
            - `pl`：波兰语
            - `cs`：捷克语
            - `hu`：匈牙利语
            - `ro`：罗马尼亚语
            - `bg`：保加利亚语
            - `hr`：克罗地亚语
            - `sk`：斯洛伐克语
          </Accordion>

          <strong>output\_audio</strong>`object`（可选）

          输出音频配置。适用于 `qwen-audio-3.1-realtime-plus`。

          <Accordion title="属性">
            <strong>language</strong>`string`（可选）

            输出音频的目标语种。

            该参数作用于语音合成（TTS）：会尽可能让语音输出使用目标语种，但不保证一定生效，也不会改变模型回复文本的语种。如需模型生成目标语种的文本，请在提示词中明确要求，例如通过 `instructions` 指定回答语言。

            可选值：

            - `zh`：中文
            - `en`：英语
            - `fr`：法语
            - `de`：德语
            - `ja`：日语
            - `ko`：韩语
            - `ru`：俄语
            - `pt`：葡萄牙语
            - `th`：泰语
            - `id`：印尼语
            - `vi`：越南语
            - `es`：西班牙语
            - `it`：意大利语
            - `ms`：马来语
            - `fil`：菲律宾语
            - `ar`：阿拉伯语
          </Accordion>

          <strong>max\_history\_turns</strong>`integer`（可选）

          允许单次请求的最大历史 QA 轮数。取值范围为 1-50，默认值为 20。

          <strong>enable\_search</strong>`boolean`（可选）

          <strong>适用于 qwen-audio-3.1-realtime-plus、qwen-audio-3.0-realtime-plus 和 qwen-audio-3.0-realtime-flash 模型。</strong>

          是否开启联网搜索。默认值：`false`。开启后，模型可根据实时问题自主判断是否进行联网搜索。

          > `tools` 与 `enable_search` 不可同时启用。

          <strong>search\_options</strong>`object`（可选）

          联网搜索配置，仅在 `enable_search` 为 `true` 时生效。

          <Accordion title="属性">
            <strong>enable\_source</strong>`boolean`（可选）

            是否返回搜索结果来源。设置为 `true` 时返回。
          </Accordion>

          <strong>tools</strong>`array`（可选）

          Function Calling 工具定义列表。配置后模型可根据用户输入自主决定是否调用工具。

          <Accordion title="属性">
            <strong>type</strong>`string`<strong>（必选）</strong>

            固定为 `function`。

            <strong>function.name</strong>`string`<strong>（必选）</strong>

            工具函数名称。

            <strong>function.description</strong>`string`（可选）

            对工具函数功能的描述，模型据此判断是否调用该工具。

            <strong>function.parameters</strong>`object`（可选）

            对工具函数入参的描述，模型据此提取所需入参。若函数无需入参，可不指定。

            <Accordion title="属性">
              <strong>type</strong>`string`<strong>（必选）</strong>

              固定为 `object`。

              <strong>properties</strong>`object`（可选）

              描述各入参的名称、数据类型与说明。

              <strong>required</strong>`array`（可选）

              指定哪些入参为必填项。
            </Accordion>
          </Accordion>

          <strong>turn\_detection</strong>`object|null`（可选）

          轮次检测配置。设为 `null` 可切换为 push-to-talk 模式（手动提交音频并触发推理）。若不提供此字段，系统将使用默认参数启用 VAD。

          <Accordion title="属性">
            <strong>type</strong>`string`（可选）

            VAD 类型，可选值：

            - `server_vad`（默认值）：基于声学特征检测语音起止，自动触发推理。
            - `smart_turn`：融合声学感知与语义理解的智能轮次检测，通过声学与语义双重判断轮次边界。无语义的声音（如”嗯”、”啊”）不会触发对话轮或打断模型播报。

            <strong>threshold</strong>`float`（可选）

            VAD灵敏度，仅在 server\_vad 模式下生效（smart\_turn 模式下无效）。值越低，VAD越灵敏，越容易将微弱声音（包括背景噪音）识别为语音；值越高，越不灵敏，需要更清晰、音量更大的语音才能触发。

            取值范围为\[-1.0, 1.0]，默认值为 0.5。

            <strong>silence\_duration\_ms</strong>`integer`（可选）

            语音结束后需保持静音的最短时间（毫秒），仅在 server\_vad 模式下生效（smart\_turn 模式下无效）。超时即触发模型响应。值越低，响应越快，但可能在短暂停顿时误触发。

            取值范围为\[200, 6000]，默认值为 800。对话场景推荐 400-800。

            <strong>voiceprint\_audio\_urls</strong>`array`（可选）

            <strong>仅在 smart\_turn 模式下生效。</strong>目标用户预录音频的公网可访问 URL 列表，用于说话人增强。传入后，模型将在双工对话中精准锁定目标说话人，有效忽略旁人声音与背景噪声。最多支持 5 个 URL。音频格式要求：16kHz PCM 或 WAV。

            <Tip>
              该参数仅在<strong>第一次</strong>发送 `session.update` 事件时可配置，后续传入该字段将被忽略。
            </Tip>
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "type": "session.update",
            "session": {
                "modalities": [
                    "text",
                    "audio"
                ],
                "voice": "longanqian",
                "turn_detection": {
                    "type": "server_vad",
                    "threshold": 0.5,
                    "silence_duration_ms": 800
                }
            }
        }
        ```

        <strong>语言配置（qwen-audio-3.1-realtime-plus）</strong>

        连接 `qwen-audio-3.1-realtime-plus` 后，可发送以下 `session.update` 事件，分别将输入音频源语种设置为中文、输出音频目标语种设置为英语。

        ```json
        {
            "type": "session.update",
            "session": {
                "input_audio_transcription": {
                    "language": "zh"
                },
                "output_audio": {
                    "language": "en"
                }
            }
        }
        ```

        Function Calling：

        ```json height="500" expandable
        {
            "type": "session.update",
            "session": {
                "modalities": [
                    "text",
                    "audio"
                ],
                "voice": "longanqian",
                "tools": [
                    {
                        "type": "function",
                        "function": {
                            "name": "get_weather",
                            "description": "查询指定城市天气",
                            "parameters": {
                                "type": "object",
                                "properties": {
                                    "city": {
                                        "type": "string",
                                        "title": "城市"
                                    }
                                },
                                "required": ["city"]
                            }
                        }
                    }
                ],
                "turn_detection": {
                    "type": "server_vad",
                    "threshold": 0.5,
                    "silence_duration_ms": 800
                }
            }
        }
        ```

        声纹注册（voiceprint\_audio\_urls）：

        ```json
        {
            "type": "session.update",
            "session": {
                "turn_detection": {
                    "type": "smart_turn",
                    "voiceprint_audio_urls": [
                        "https://example.com/speaker1.pcm",
                        "https://example.com/speaker2.wav"
                    ]
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.append <span id="fc65b1c3d4e6g" />

<strong>说明</strong>：追加音频数据到输入缓冲区。应持续、高频发送（如每 20\~40 ms 一帧）。此事件无服务端确认回包。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为 `input_audio_buffer.append`。

        <strong>audio</strong>`string`<strong>（必选）</strong>

        Base64 编码的音频数据。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "type": "input_audio_buffer.append",
            "audio": "<base64 编码的音频数据>"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.commit <span id="fc65b2c4d5e7h" />

<strong>说明</strong>：<strong>仅 push-to-talk 模式。</strong>将缓冲区音频提交为一条用户消息。提交后不会自动触发推理，需要发送 `response.create` 手动触发。

server\_vad / smart\_turn 模式下此事件被忽略。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为 `input_audio_buffer.commit`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "type": "input_audio_buffer.commit"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.clear <span id="fc65b3c5d6e8i" />

<strong>说明</strong>：<strong>仅 push-to-talk 模式。</strong>清空缓冲区中尚未提交的音频。server\_vad / smart\_turn 模式下此事件被忽略。服务端以 `input_audio_buffer.cleared` 事件响应。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为 `input_audio_buffer.clear`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "type": "input_audio_buffer.clear"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.create <span id="fc65c1d2e3f5h" />

<strong>说明</strong>：手动向对话上下文插入一条对话项。可用于注入历史上下文、补充文本信息，或写回 Function Calling 的工具执行结果。

<Note>
  若 `item.id` 已存在于对话中，会返回错误并拒绝创建。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为 `conversation.item.create`。

        <strong>previous\_item\_id</strong>`string`（可选）

        指定新项插入到哪条对话项之后。不传时追加到对话末尾。

        <strong>item</strong>`object`<strong>（必选）</strong>

        要创建的对话项。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`（可选）

          对话项的唯一标识符。不传时由服务端自动生成。若指定的 ID 已存在于对话中，会返回错误。

          <strong>type</strong>`string`<strong>（必选）</strong>

          对话项类型，可选值：

          - `message`：普通对话消息。
          - `function_call`：函数调用请求。通常由服务端生成，客户端也可用于补充历史上下文。
          - `function_call_output`：工具执行结果。客户端收到 `function_call` 后执行工具，并用该类型写回结果。

          <strong>role</strong>`string`（`message` 类型必选）

          消息角色，可选值：`system`、`user`、`assistant`。

          <strong>content</strong>`array`（`message` 类型必选）

          消息内容列表。每个元素包含 `type` 和对应的数据字段。

          <Accordion title="各 role 支持的 content 类型">
            <strong>system</strong>

            `input_text`：系统消息，必填字段 `text`。

            <strong>user</strong>

            - `input_text`：用户文本输入，必填字段 `text`。
            - `input_audio`：用户音频输入，必填字段 `audio`（Base64 编码）。

            <strong>assistant</strong>

            `output_text`：助手文本输出，必填字段 `text`。
          </Accordion>

          <strong>call\_id</strong>`string`（`function_call` / `function_call_output` 类型必选）

          函数调用的唯一标识符，用于关联请求和结果。

          <strong>name</strong>`string`（`function_call` 类型必选）

          要调用的函数名称。

          <strong>arguments</strong>`string`（`function_call` 类型必选）

          函数调用参数，JSON 字符串格式。

          <strong>output</strong>`string`（`function_call_output` 类型必选）

          工具执行结果，JSON 字符串格式。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        注入用户文本消息：

        ```json
        {
            "type": "conversation.item.create",
            "previous_item_id": "item_xxx",
            "item": {
                "id": "my_item_001",
                "type": "message",
                "role": "user",
                "content": [
                    {
                        "type": "input_text",
                        "text": "请帮我总结一下上次的对话"
                    }
                ]
            }
        }
        ```

        写回 Function Calling 结果：

        ```json
        {
            "type": "conversation.item.create",
            "item": {
                "type": "function_call_output",
                "call_id": "call_xxx",
                "output": "{\"temperature\":18,\"condition\":\"晴\"}"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.retrieve <span id="fc65c2d3e4f6h" />

<strong>说明</strong>：查询服务端存储的某条对话项。返回的音频类型 content 仅包含转写文本（`transcript`），不包含原始音频数据。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为 `conversation.item.retrieve`。

        <strong>item\_id</strong>`string`<strong>（必选）</strong>

        要查询的对话项 ID。服务端以 `conversation.item.retrieved` 事件返回查询结果。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "type": "conversation.item.retrieve",
            "item_id": "item_xxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.delete <span id="fc65c3d4e5f7h" />

<strong>说明</strong>：从对话上下文中删除指定对话项。服务端以 `conversation.item.deleted` 事件确认。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为 `conversation.item.delete`。

        <strong>item\_id</strong>`string`<strong>（必选）</strong>

        要删除的对话项 ID。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "type": "conversation.item.delete",
            "item_id": "item_xxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.create <span id="fc65d1e2f3g5i" />

<strong>说明</strong>：显式触发一次模型推理。各模式下的行为如下：

- <strong>push-to-talk 模式</strong>：必须手动调用，需先通过 `input_audio_buffer.commit` 提交缓冲区音频或写入 `function_call_output` 后触发。当前有响应正在生成时不允许重复触发。
- <strong>server\_vad 模式</strong>：通常由服务端自动触发。当前无响应正在生成时，客户端也允许手动调用；有响应正在生成时不允许重复触发。
- <strong>smart\_turn 模式</strong>：等待用户下一轮输入时允许调用。当前处于一个 turn 内时（收到 `input_audio_buffer.speech_started` 到 `response.done` 期间）不允许重复触发。

`response` 字段可选，用于覆盖本轮推理的会话默认配置。Function Calling 场景下，客户端写入 `function_call_output` 后也通过该事件触发二轮推理。

<Note>
  server\_vad / smart\_turn 模式下，手动触发的推理仍可能被新语音打断。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为 `response.create`。

        <strong>response</strong>`object`（可选）

        用于覆盖本轮推理的会话默认配置。不传时使用当前会话配置。

        <Accordion title="属性">
          <strong>modalities</strong>`array`（可选）

          覆盖本轮的输出模态设置。可选值与 `session.update` 中的 `modalities` 一致。

          <strong>voice</strong>`string`（可选）

          覆盖本轮的 TTS 音色。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "type": "response.create",
            "response": {
                "modalities": ["audio", "text"]
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.cancel <span id="fc65d2e3f4g6i" />

<strong>说明</strong>：取消当前正在进行的推理。已输出的部分文本会写入 item 链表，服务端返回 `status=cancelled` 的 `response.done`。

若当前无进行中的推理，返回错误。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为 `response.cancel`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "type": "response.cancel"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
