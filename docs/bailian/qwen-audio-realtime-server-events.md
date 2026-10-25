> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-Audio 实时语音对话服务端事件

> Qwen-Audio Realtime API 的服务端事件参考。所有服务端事件均包含 event_id （服务端自动生成）和 type （事件类型）公共字段。

<meta property="og:title" content="Qwen-Audio 实时语音对话服务端事件" />

<strong>用户指南</strong>：[实时语音对话（Qwen-Audio-Realtime）](https://help.aliyun.com/zh/model-studio/fun-audiochat-realtime)。如需了解事件交互时序，请参见[WebSocket API](/zh/model-studio/fun-audiochat-realtime-websocket-api)。

## error <span id="fs66a1b1c1d2f" />

<strong>说明</strong>：请求错误或服务异常时返回。客户端错误（`invalid_request_error`）不中断连接；服务端错误（`server_error`）将终止连接。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `error`。

        <strong>error</strong>`object`

        错误的详细信息。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          错误类型，如 `invalid_request_error`（客户端错误）或 `server_error`（服务端错误）。

          <strong>code</strong>`string`

          错误码。

          <strong>message</strong>`string`

          错误信息。

          <strong>param</strong>`string`

          与错误相关的参数。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "error",
            "error": {
                "type": "invalid_request_error",
                "code": "invalid_value",
                "message": "Cannot create response while another response is in progress.",
                "param": "response.create"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.created <span id="fs66b1c1d1e2g" />

<strong>说明</strong>：连接建立后服务端发送的首个事件，携带会话默认配置。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%"  , overflowWrap: "anywhere" }}>
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

        事件类型，固定为 `session.created`。

        <strong>session</strong>`object`

        会话的配置信息。

        <Accordion title="属性" defaultOpen>
          <strong>object</strong>`string`

          固定为 `realtime.session`。

          <strong>model</strong>`string`

          使用的模型名称。

          <strong>modalities</strong>`array`

          模型输出模态设置。

          <strong>voice</strong>`string`

          模型生成音频的音色，为系统音色名称或声音复刻音色的 `voice_id`。

          <strong>input\_audio\_transcription</strong>`object`

          语音转录的配置。

          <Accordion title="属性" defaultOpen>
            <strong>model</strong>`string`

            语音转录模型，如 `fun-asr`。
          </Accordion>

          <strong>turn\_detection</strong>`object`

          轮次检测（VAD）的配置。

          <strong>id</strong>`string`

          会话唯一标识符。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "event_id": "event_KiKZC2zrhNsKFPZ5cTpyA",
            "type": "session.created",
            "session": {
                "object": "realtime.session",
                "model": "qwen-audio-3.1-realtime-plus",
                "modalities": ["text", "audio"],
                "voice": "longanqian_v3.1",
                "input_audio_transcription": {
                    "model": "fun-asr"
                },
                "turn_detection": {
                    "type": "server_vad",
                    "threshold": 0.5,
                    "silence_duration_ms": 800
                },
                "id": "sess_A1LbG2D63WELBSawRbpq8"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.updated <span id="fs66b2c2d2e3g" />

<strong>说明</strong>：收到 `session.update` 请求后，若处理成功，则返回此事件，携带更新后的完整会话配置；若出错，则返回 `error` 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `session.updated`。

        <strong>session</strong>`object`

        更新后的完整会话配置信息。结构与 `session.created` 中的 session 对象一致。
      </td>

      <td>
        ```json
        {
            "event_id": "event_FMG6kiHbILCGiqXFPA98e",
            "type": "session.updated",
            "session": {
                "id": "sess_A1LbG2D63WELBSawRbpq8",
                "object": "realtime.session",
                "model": "pre-fun-realtime-audiochat-beta-3",
                "modalities": ["text", "audio"],
                "voice": "longanqian",
                "input_audio_transcription": {
                    "model": "fun-asr"
                },
                "turn_detection": {
                    "type": "smart_turn",
                    "threshold": 0.1,
                    "silence_duration_ms": 900
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.speech\_started <span id="fs66c1d1e1f2h" />

<strong>说明</strong>：VAD 检测到语音开始（server\_vad / smart\_turn 模式）。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `input_audio_buffer.speech_started`。

        <strong>audio\_start\_ms</strong>`integer`

        语音开始的时间戳（毫秒）。

        <strong>item\_id</strong>`string`

        该段语音最终提交时将创建的 item 的 ID。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_audio_buffer.speech_started",
            "audio_start_ms": 1200,
            "item_id": "item_xxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.speech\_stopped <span id="fs66c2d2e2f3h" />

<strong>说明</strong>：VAD 检测到语音结束（server\_vad / smart\_turn 模式）。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `input_audio_buffer.speech_stopped`。

        <strong>audio\_end\_ms</strong>`integer`

        语音结束的时间戳（毫秒）。

        <strong>item\_id</strong>`string`

        将创建的用户消息项的 ID。

        <strong>reason</strong>`string`

        仅 smart\_turn 模式返回此字段。取值为 `turn_invalid` 时表示当前 turn 被判定为无效轮（无语义内容），不会触发推理。有效轮次时不返回此字段。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_audio_buffer.speech_stopped",
            "audio_end_ms": 3400,
            "item_id": "item_xxx",
            "reason": "turn_invalid"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.committed <span id="fs66c3d3e3f4h" />

<strong>说明</strong>：音频缓冲已提交为用户消息（push-to-talk 的 commit 或 VAD 自动提交）。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `input_audio_buffer.committed`。

        <strong>previous\_item\_id</strong>`string`

        前一条对话项的 ID。

        <strong>item\_id</strong>`string`

        创建的用户消息项的 ID。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_audio_buffer.committed",
            "previous_item_id": "item_xxx",
            "item_id": "item_xxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.cleared <span id="fs66c4d4e4f5h" />

<strong>说明</strong>：音频缓冲已清空（仅 push-to-talk 模式）。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `input_audio_buffer.cleared`。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_audio_buffer.cleared"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.created <span id="fs66d1e1f1g2i" />

<strong>说明</strong>：新的对话项创建成功。用户音频提交、客户端手动创建或助手响应开始时触发。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `conversation.item.created`。

        <strong>previous\_item\_id</strong>`string`

        前一条对话项的 ID。

        <strong>item</strong>`object`

        创建的对话项。

        <Accordion title="属性">
          <strong>id</strong>`string`

          对话项的唯一标识符。

          <strong>object</strong>`string`

          固定为 `realtime.item`。

          <strong>type</strong>`string`

          对话项类型：`message`（普通消息）或 `function_call`（函数调用）。

          <strong>status</strong>`string`

          对话项状态，如 `in_progress`、`completed`。

          <strong>role</strong>`string`

          消息角色，如 `user`、`assistant`。仅 `message` 类型包含。

          <strong>content</strong>`array`

          消息内容列表。仅 `message` 类型包含。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.created",
            "previous_item_id": "item_xxx",
            "item": {
                "id": "item_xxx",
                "object": "realtime.item",
                "type": "message",
                "status": "in_progress",
                "role": "assistant",
                "content": []
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.deleted <span id="fs66d2e2f2g3i" />

<strong>说明</strong>：对话项已删除。客户端发送 `conversation.item.delete` 后返回此确认事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `conversation.item.deleted`。

        <strong>item\_id</strong>`string`

        已删除的对话项 ID。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.deleted",
            "item_id": "item_xxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.retrieved <span id="fs66d3e3f3g4i" />

<strong>说明</strong>：查询对话项成功返回。客户端发送 `conversation.item.retrieve` 后返回此事件。音频类型 content 仅包含转写文本，不返回原始音频数据。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `conversation.item.retrieved`。

        <strong>item</strong>`object`

        查询到的对话项完整信息。

        <Accordion title="属性">
          <strong>id</strong>`string`

          对话项的唯一标识符。

          <strong>object</strong>`string`

          固定为 `realtime.item`。

          <strong>type</strong>`string`

          对话项类型：`message`（普通消息）或 `function_call`（函数调用）。

          <strong>role</strong>`string`

          消息角色，如 `user`、`assistant`。仅 `message` 类型包含。

          <strong>content</strong>`array`

          消息内容列表。音频类型 content 仅包含转写文本，不返回原始音频数据。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.retrieved",
            "item": {
                "id": "item_xxx",
                "object": "realtime.item",
                "type": "message",
                "role": "user",
                "content": [
                    {
                        "type": "input_audio",
                        "transcript": "你好"
                    }
                ]
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.delta <span id="fs66e1f1g1h2j" />

<strong>说明</strong>：ASR 转写增量结果，在语音识别过程中流式返回。包含情绪和语种检测信息。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `conversation.item.input_audio_transcription.delta`。

        <strong>item\_id</strong>`string`

        关联的对话项 ID。

        <strong>content\_index</strong>`integer`

        内容部分的索引。

        <strong>text</strong>`string`

        已确定的转写文本。

        <strong>stash</strong>`string`

        尚未确定的暂存文本。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.input_audio_transcription.delta",
            "item_id": "item_xxx",
            "content_index": 0,
            "text": "你好",
            "stash": "世界"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.completed <span id="fs66e2f2g2h3j" />

<strong>说明</strong>：ASR 转写最终结果。转写文本会写入对应 item 的 `transcript` 字段。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `conversation.item.input_audio_transcription.completed`。

        <strong>item\_id</strong>`string`

        关联的对话项 ID。

        <strong>content\_index</strong>`integer`

        内容部分的索引。

        <strong>transcript</strong>`string`

        完整的转写文本。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.input_audio_transcription.completed",
            "item_id": "item_xxx",
            "content_index": 0,
            "transcript": "你好世界"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.failed <span id="fs66e3f3g3h4j" />

<strong>说明</strong>：ASR 转写失败。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `conversation.item.input_audio_transcription.failed`。

        <strong>item\_id</strong>`string`

        关联的对话项 ID。

        <strong>content\_index</strong>`integer`

        内容部分的索引。

        <strong>error</strong>`object`

        错误的详细信息。

        <Accordion title="属性">
          <strong>type</strong>`string`

          错误类型，如 `transcription_error`。

          <strong>code</strong>`string`

          错误码，如 `transcription_failed`。

          <strong>message</strong>`string`

          错误信息。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.input_audio_transcription.failed",
            "item_id": "item_xxx",
            "content_index": 0,
            "error": {
                "type": "transcription_error",
                "code": "transcription_failed",
                "message": "ASR transcription failed"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.ambient\_audio\_transcription.delta <span id="fs66e4f4g4h5j" />

<strong>说明</strong>：<strong>仅 smart\_turn 模式。</strong>环境音频转写增量结果。当 VAD 检测到语音活动但语义判定为非有效轮次（如噪声、“嗯”、“啊”等无语义内容）时，将 ASR 识别结果以 ambient 事件透传给客户端。该事件不会关联到对话上下文中的任何 item，`item_id` 为独立生成的临时 ID。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `conversation.item.ambient_audio_transcription.delta`。

        <strong>item\_id</strong>`string`

        独立生成的临时 ID，不关联到对话上下文中的任何 item。

        <strong>content\_index</strong>`integer`

        内容部分的索引。

        <strong>text</strong>`string`

        已确定的转写文本。

        <strong>stash</strong>`string`

        尚未确定的暂存文本。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.ambient_audio_transcription.delta",
            "item_id": "item_xxx",
            "content_index": 0,
            "text": "嗯",
            "stash": ""
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.ambient\_audio\_transcription.completed <span id="fs66e5f5g5h6j" />

<strong>说明</strong>：<strong>仅 smart\_turn 模式。</strong>环境音频转写最终结果。与 delta 事件配对，表示一段环境音频的转写结束。该转写结果不会写入对话上下文。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `conversation.item.ambient_audio_transcription.completed`。

        <strong>item\_id</strong>`string`

        独立生成的临时 ID，不关联到对话上下文。

        <strong>content\_index</strong>`integer`

        内容部分的索引。

        <strong>transcript</strong>`string`

        完整的转写文本。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.ambient_audio_transcription.completed",
            "item_id": "item_xxx",
            "content_index": 0,
            "transcript": "嗯",
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.created <span id="fs66f1g1h1i2k" />

<strong>说明</strong>：一轮模型推理开始。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.created`。

        <strong>response</strong>`object`

        响应对象。

        <Accordion title="属性">
          <strong>id</strong>`string`

          响应的唯一标识符。

          <strong>object</strong>`string`

          固定为 `realtime.response`。

          <strong>status</strong>`string`

          响应状态，如 `in_progress`。

          <strong>modalities</strong>`array`

          模型输出模态设置。

          <strong>voice</strong>`string`

          模型生成音频的音色，为系统音色名称或声音复刻音色的 `voice_id`。

          <strong>output</strong>`array`

          响应的输出项列表，初始为空数组。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.created",
            "response": {
                "id": "resp_xxx",
                "object": "realtime.response",
                "status": "in_progress",
                "modalities": ["text", "audio"],
                "voice": "longanqian",
                "output": []
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.output\_item.added <span id="fs66f2g2h2i3k" />

<strong>说明</strong>：响应中新增一个输出项。普通回复的输出项类型为 `message`；Function Calling 的输出项类型为 `function_call`。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.output_item.added`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>item</strong>`object`

        新增的输出项。

        <Accordion title="属性">
          <strong>id</strong>`string`

          输出项的唯一标识符。

          <strong>object</strong>`string`

          固定为 `realtime.item`。

          <strong>type</strong>`string`

          输出项类型：`message`（普通消息）或 `function_call`（函数调用）。

          <strong>status</strong>`string`

          输出项状态，如 `in_progress`。

          <strong>role</strong>`string`

          消息角色，固定为 `assistant`。仅 `message` 类型包含。

          <strong>content</strong>`array`

          消息内容列表。仅 `message` 类型包含。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.output_item.added",
            "response_id": "resp_xxx",
            "output_index": 0,
            "item": {
                "id": "item_xxx",
                "object": "realtime.item",
                "type": "message",
                "status": "in_progress",
                "role": "assistant",
                "content": []
            }
        }
        ```

        Function Call 输出项示例：

        当输出项为函数调用时，`response.output_item.added` / `conversation.item.created` / `response.output_item.done` 中的 `item` 结构如下：

        ```json
        {
            "id": "item_xxx",
            "object": "realtime.item",
            "type": "function_call",
            "status": "completed",
            "call_id": "call_xxx",
            "name": "get_weather",
            "arguments": "{\"city\":\"杭州\"}"
        }
        ```

        <Note>
          一轮响应可包含多个 `function_call`，也可能同时包含普通 `message` 输出和 `function_call` 输出。Function Call 部分不会送入 TTS 播报。
        </Note>
      </td>
    </tr>
  </tbody>
</table>

## response.content\_part.added <span id="fs66f3g3h3i4k" />

<strong>说明</strong>：输出项中新增一个内容部分。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.content_part.added`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>content\_index</strong>`integer`

        内容部分在输出项中的索引。

        <strong>part</strong>`object`

        新增的内容部分。

        <Accordion title="属性">
          <strong>type</strong>`string`

          内容类型，如 `audio`、`text`。

          <strong>text</strong>`string`

          文本内容，初始为空字符串。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.content_part.added",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
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

## response.text.delta <span id="fs66g1h1i1j2l" />

<strong>说明</strong>：纯文本模式下的文本增量事件，流式返回文本片段。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.text.delta`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>content\_index</strong>`integer`

        内容部分在输出项中的索引。

        <strong>delta</strong>`string`

        文本增量片段。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.text.delta",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
            "output_index": 0,
            "content_index": 0,
            "delta": "你好"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.text.done <span id="fs66sp02a1b2d" />

<strong>说明</strong>：纯文本模式下的文本输出完成事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.text.done`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>content\_index</strong>`integer`

        内容部分在输出项中的索引。

        <strong>text</strong>`string`

        完整的文本输出。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.text.done",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
            "output_index": 0,
            "content_index": 0,
            "text": "你好，有什么可以帮助你的吗？"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio\_transcript.delta <span id="fs66g2h2i2j3l" />

<strong>说明</strong>：音频模式下的文字字幕增量事件，流式返回字幕片段。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.audio_transcript.delta`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>content\_index</strong>`integer`

        内容部分在输出项中的索引。

        <strong>delta</strong>`string`

        字幕增量片段。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.audio_transcript.delta",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
            "output_index": 0,
            "content_index": 0,
            "delta": "你好"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio\_transcript.done <span id="fs66sp04a1b2d" />

<strong>说明</strong>：音频模式下的字幕输出完成事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.audio_transcript.done`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>content\_index</strong>`integer`

        内容部分在输出项中的索引。

        <strong>transcript</strong>`string`

        完整的字幕文本。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.audio_transcript.done",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
            "output_index": 0,
            "content_index": 0,
            "transcript": "你好，有什么可以帮助你的吗？"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio.delta <span id="fs66g3h3i3j4l" />

<strong>说明</strong>：音频模式下的音频数据增量事件。`delta` 字段为 Base64 编码的 PCM 音频数据。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.audio.delta`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>content\_index</strong>`integer`

        内容部分在输出项中的索引。

        <strong>delta</strong>`string`

        Base64 编码的 PCM 音频数据片段。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.audio.delta",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
            "output_index": 0,
            "content_index": 0,
            "delta": "<base64 编码的音频数据>"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio.done <span id="fs66sp06a1b2d" />

<strong>说明</strong>：音频模式下的音频输出完成事件，不包含音频数据。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.audio.done`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>content\_index</strong>`integer`

        内容部分在输出项中的索引。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.audio.done",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
            "output_index": 0,
            "content_index": 0
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.content\_part.done <span id="fs66h1i1j1k2m" />

<strong>说明</strong>：输出项中的内容部分输出完成。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.content_part.done`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>content\_index</strong>`integer`

        内容部分在输出项中的索引。

        <strong>part</strong>`object`

        完成的内容部分。

        <Accordion title="属性">
          <strong>type</strong>`string`

          内容类型，如 `audio`、`text`。

          <strong>text</strong>`string`

          文本内容或音频字幕文本。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.content_part.done",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
            "output_index": 0,
            "content_index": 0,
            "part": {
                "type": "audio",
                "text": "你好，有什么可以帮助你的吗？"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.output\_item.done <span id="fs66h2i2j2k3m" />

<strong>说明</strong>：响应中的输出项输出完成。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.output_item.done`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>item</strong>`object`

        完成的输出项完整信息。

        <Accordion title="属性">
          <strong>id</strong>`string`

          输出项的唯一标识符。

          <strong>object</strong>`string`

          固定为 `realtime.item`。

          <strong>type</strong>`string`

          输出项类型：`message`（普通消息）或 `function_call`（函数调用）。

          <strong>status</strong>`string`

          输出项状态，如 `completed`。

          <strong>role</strong>`string`

          消息角色，固定为 `assistant`。仅 `message` 类型包含。

          <strong>content</strong>`array`

          消息内容列表。仅 `message` 类型包含。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.output_item.done",
            "response_id": "resp_xxx",
            "output_index": 0,
            "item": {
                "id": "item_xxx",
                "object": "realtime.item",
                "type": "message",
                "status": "completed",
                "role": "assistant",
                "content": [
                    {
                        "type": "text",
                        "text": "你好，有什么可以帮助你的吗？"
                    }
                ]
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.function\_call\_arguments.delta <span id="fs66i1j1k1l2n" />

<strong>说明</strong>：Function Calling 参数增量。模型决定调用工具时，服务端会先发送 `response.output_item.added`（`item.type=function_call`）和对应的 `conversation.item.created`，随后通过本事件流式输出参数片段。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.function_call_arguments.delta`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>call\_id</strong>`string`

        函数调用的唯一标识符。

        <strong>delta</strong>`string`

        函数调用参数的增量片段（JSON 字符串片段）。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.function_call_arguments.delta",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
            "output_index": 0,
            "call_id": "call_xxx",
            "delta": "{\"city"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.function\_call\_arguments.done <span id="fs66i2j2k2l3n" />

<strong>说明</strong>：Function Calling 参数输出完成。收到该事件后，客户端应执行对应工具，并通过 `conversation.item.create` 写入 `function_call_output`，随后发送 `response.create` 触发二轮推理。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.function_call_arguments.done`。

        <strong>response\_id</strong>`string`

        关联的响应 ID。

        <strong>item\_id</strong>`string`

        关联的输出项 ID。

        <strong>output\_index</strong>`integer`

        输出项在响应中的索引。

        <strong>call\_id</strong>`string`

        函数调用的唯一标识符。

        <strong>name</strong>`string`

        调用的函数名称。

        <strong>arguments</strong>`string`

        完整的函数调用参数（JSON 字符串）。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "response.function_call_arguments.done",
            "response_id": "resp_xxx",
            "item_id": "item_xxx",
            "output_index": 0,
            "call_id": "call_xxx",
            "name": "get_weather",
            "arguments": "{\"city\":\"杭州\"}"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.done <span id="fs66j1k1l1m2o" />

<strong>说明</strong>：一轮推理完成。`status` 表示结束原因。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `response.done`。

        <strong>response</strong>`object`

        完整的响应对象。

        <Accordion title="属性">
          <strong>id</strong>`string`

          响应的唯一标识符。

          <strong>object</strong>`string`

          固定为 `realtime.response`。

          <strong>status</strong>`string`

          响应的结束状态。可选值：

          - `completed`：正常完成。
          - `cancelled`：被打断取消。`status_details.reason` 为 `turn_detected`（VAD 打断）或 `client_cancelled`（客户端取消）。
          - `failed`：LLM 或 TTS 错误。

          <strong>status\_details</strong>`object`

          状态详情，仅在 `cancelled` 或 `failed` 时存在。

          <Accordion title="属性">
            <strong>type</strong>`string`

            状态类型，如 `cancelled`。

            <strong>reason</strong>`string`

            取消原因：`turn_detected`（VAD 打断）或 `client_cancelled`（客户端取消）。
          </Accordion>

          <strong>modalities</strong>`array`

          模型输出模态设置。

          <strong>voice</strong>`string`

          模型生成音频的音色，为系统音色名称或声音复刻音色的 `voice_id`。

          <strong>output</strong>`array`

          响应的输出项列表，包含完整的 item 对象。

          <strong>usage</strong>`object`

          本次响应的 Token 消耗信息。仅在 `status` 为 `completed` 时返回。

          <Accordion title="属性">
            <strong>total\_tokens</strong>`integer`

            本次响应消耗的总 Token 数。

            <strong>input\_tokens</strong>`integer`

            输入 Token 数。

            <strong>output\_tokens</strong>`integer`

            输出 Token 数。

            <strong>input\_tokens\_details</strong>`object`

            输入 Token 的分项详情，包含 `text_tokens`（文本 Token 数）和 `audio_tokens`（音频 Token 数，仅在使用音频模态时存在）。

            <strong>output\_tokens\_details</strong>`object`

            输出 Token 的分项详情，包含 `text_tokens`（文本 Token 数）和 `audio_tokens`（音频 Token 数，仅在使用音频模态时存在）。

            <strong>plugins</strong>`object`（可选）

            插件使用计量信息。启用联网搜索（`enable_search`）时返回。

            <Accordion title="属性">
              <strong>search</strong>`object`

              联网搜索计量信息。

              <Accordion title="属性">
                <strong>count</strong>`integer`

                搜索次数。

                <strong>strategy</strong>`string`

                搜索策略。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json 正常完成 expandable
          {
              "event_id": "event_xxx",
              "type": "response.done",
              "response": {
                  "id": "resp_xxx",
                  "object": "realtime.response",
                  "status": "completed",
                  "modalities": ["text", "audio"],
                  "voice": "longanqian",
                  "output": [
                      {
                          "id": "item_xxx",
                          "object": "realtime.item",
                          "type": "message",
                          "status": "completed",
                          "role": "assistant",
                          "content": [
                              {
                                  "type": "audio",
                                  "transcript": "你好，有什么可以帮助你的吗？"
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
                      }
                  }
              }
          }
          ```

          ```json 被打断取消
          {
              "event_id": "event_xxx",
              "type": "response.done",
              "response": {
                  "id": "resp_xxx",
                  "status": "cancelled",
                  "status_details": {
                      "type": "cancelled",
                      "reason": "turn_detected"
                  }
              }
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## voiceprint\_audio\_list.in\_progress <span id="fs66vp1a1b2d" />

<strong>说明</strong>：声纹注册流程异步进行中。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `voiceprint_audio_list.in_progress`。

        <strong>item\_id</strong>`string`

        声纹注册任务的唯一标识符。
      </td>

      <td>
        ```json
        {
            "event_id": "event_PaEcN7CCrlhE8q4MM2yND",
            "type": "voiceprint_audio_list.in_progress",
            "item_id": "vp_Y12cA986j1KZ9O9YmAXOA"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## voiceprint\_audio\_list.completed <span id="fs66vp2a1b2d" />

<strong>说明</strong>：声纹注册流程已完成。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `voiceprint_audio_list.completed`。

        <strong>item\_id</strong>`string`

        声纹注册任务的唯一标识符。
      </td>

      <td>
        ```json
        {
            "event_id": "event_PaEcN7CCrlhE8q4MM2yND",
            "type": "voiceprint_audio_list.completed",
            "item_id": "vp_Y12cA986j1KZ9O9YmAXOA"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## voiceprint\_audio\_list.failed <span id="fs66vp3a1b2d" />

<strong>说明</strong>：声纹注册失败，不阻塞正常对话调用。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为 `voiceprint_audio_list.failed`。

        <strong>item\_id</strong>`string`

        声纹注册任务的唯一标识符。

        <strong>reason</strong>`string`

        失败原因描述。
      </td>

      <td>
        ```json
        {
            "event_id": "event_PaEcN7CCrlhE8q4MM2yND",
            "type": "voiceprint_audio_list.failed",
            "item_id": "vp_Y12cA986j1KZ9O9YmAXOA",
            "reason": ""
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
