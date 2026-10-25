> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 实时语音识别（Qwen-ASR-Realtime）服务端事件

> 本文档介绍在与 Qwen-ASR Realtime API 的 WebSocket 会话中，服务端向客户端发送的事件。

<meta property="og:title" content="实时语音识别（Qwen-ASR-Realtime）服务端事件" />

<strong>用户指南：</strong>模型介绍、功能特性和完整示例代码请参见[实时语音识别](/zh/model-studio/real-time-speech-recognition-user-guide)

## error <span id="af43722339yva" />

当服务端检测到错误（包括客户端错误和服务端错误）时，向客户端发送的事件。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "32.98%" }} /><col style={{ width: "13.26%" }} /><col style={{ width: "53.76%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>error</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>事件ID。</p></td></tr><tr><td><p>error.type</p></td><td><p>string</p></td><td><p>错误类型。</p></td></tr><tr><td><p>error.code</p></td><td><p>string</p></td><td><p>错误代码。</p></td></tr><tr><td><p>error.message</p></td><td><p>string</p></td><td><p>具体的报错信息。请按<a href="/zh/model-studio/error-code">错误码</a>所示的解决方案进行处理。</p></td></tr><tr><td><p>error.param</p></td><td><p>string</p></td><td><p>与错误相关的参数。</p></td></tr><tr><td><p>error.event\_id</p></td><td><p>string</p></td><td><p>与错误相关的事件ID。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
          "event_id": "event_B2uoU7VOt1AAITsPRPH9n",
          "type": "error",
          "error": {
            "type": "invalid_request_error",
            "code": "invalid_value",
            "message": "Invalid value: 'whisper-1xx'. Supported values are: 'whisper-1'.",
            "param": "session.input_audio_transcription.model",
            "event_id": "event_123"
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.created <span id="2c04b24bc3wlo" />

当客户端成功连接到服务端后，服务端响应的第一个事件。该事件包含服务端为此次连接设置的默认配置信息。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.06%" }} /><col style={{ width: "13.11%" }} /><col style={{ width: "53.83%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>session.created</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>事件ID。</p></td></tr><tr><td><p>session.id</p></td><td><p>string</p></td><td><p>本次 WebSocket 会话的ID。</p></td></tr><tr><td><p>session.object</p></td><td><p>string</p></td><td><p>固定为<code>realtime.session</code>。</p></td></tr><tr><td><p>session.model</p></td><td><p>string</p></td><td><p>当前调用的<a href="/zh/model-studio/real-time-speech-recognition-user-guide">模型</a>名称。</p></td></tr><tr><td><p>session.modalities</p></td><td><p>array\[string]</p></td><td><p>模型输出模态，固定为<code>\["text"]</code>。</p></td></tr><tr><td><p>session.input\_audio\_format</p></td><td><p>string</p></td><td><p>输入音频格式。</p></td></tr><tr><td><p>session.input\_audio\_transcription</p></td><td><p>object</p></td><td><p>语音识别相关配置参数。详情参见客户端<a href="/zh/model-studio/qwen-asr-realtime-client-events#af43722339yva">session.update</a>事件<code>input\_audio\_transcription</code>参数。</p></td></tr><tr><td><p>session.turn\_detection</p></td><td><p>object</p></td><td><p>VAD（Voice Activity Detection，语音活动检测）配置。</p></td></tr><tr><td><p>session.turn\_detection.type</p></td><td><p>string</p></td><td><p>固定为<code>server\_vad</code>。</p></td></tr><tr><td><p>session.turn\_detection.threshold</p></td><td><p>float</p></td><td><p>VAD检测阈值。</p></td></tr><tr><td><p>session.turn\_detection.silence\_duration\_ms</p></td><td><p>integer</p></td><td><p>VAD断句检测阈值（ms）。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
            "event_id": "event_1234",
            "type": "session.created",
            "session": {
                "id": "sess_001",
                "object": "realtime.session",
                "model": "qwen3-asr-flash-realtime",
                "modalities": ["text"],
                "input_audio_format": "pcm16",
                "input_audio_transcription": null,
                "turn_detection": {
                    "type": "server_vad",
                    "threshold": 0.5,
                    "silence_duration_ms": 200
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.updated <span id="4d6ed9dd62vmj" />

当客户端发送[session.update](/zh/model-studio/qwen-asr-realtime-client-events#af43722339yva)事件并成功被服务端处理后，服务端将发送该事件。如果处理过程中出现错误，则直接发送 error 事件。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.09%" }} /><col style={{ width: "13.04%" }} /><col style={{ width: "53.87%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>session.updated</code>。</p></td></tr></tbody></table>

        其他参数含义同[session.created](/zh/model-studio/qwen-asr-realtime-server-events#2c04b24bc3wlo)。
      </td>

      <td>
        ```json
        {
            "event_id": "event_1234",
            "type": "session.updated",
            "session": {
                "id": "sess_001",
                "object": "realtime.session",
                "model": "gpt-4o-realtime-preview-2024-12-17",
                "modalities": ["text"],
                "input_audio_format": "pcm16",
                "input_audio_transcription": null,
                "turn_detection": {
                    "type": "server_vad",
                    "threshold": 0.5,
                    "silence_duration_ms": 200
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.speech\_started <span id="038d846199nbm" />

此事件仅在 VAD 模式下发送。当服务端在音频缓冲区中检测到语音开始时发送。

该事件可能在每次音频添加到缓冲区时发生（除非已检测到语音开始）。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "13.53%" }} /><col style={{ width: "53.14%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>input\_audio\_buffer.speech\_started</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>事件ID。</p></td></tr><tr><td><p>audio\_start\_ms</p></td><td><p>integer</p></td><td><p>在会话期间，从音频开始写入缓冲区到首次检测到语音时的毫秒数。</p></td></tr><tr><td><p>item\_id</p></td><td><p>string</p></td><td><p>将创建的用户消息项的 ID。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
          "event_id": "event_B1lV7FPbgTv9qGxPI1tH4",
          "type": "input_audio_buffer.speech_started",
          "audio_start_ms": 64,
          "item_id": "item_B1lV7jWLscp4mMV8hSs8c"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.speech\_stopped <span id="3d73b074cak7k" />

此事件仅在 VAD 模式下发送。当服务端在音频缓冲区中检测到语音结束时发送。

该事件触发后，服务端将紧接着发送一个[conversation.item.created](/zh/model-studio/qwen-asr-realtime-server-events#04dabbb9b6eto)事件，包含从音频缓冲区创建的用户消息项。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "13.94%" }} /><col style={{ width: "52.73%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>input\_audio\_buffer.speech\_stopped</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>事件ID。</p></td></tr><tr><td><p>audio\_end\_ms</p></td><td><p>integer</p></td><td><p>从会话开始到语音停止的毫秒数。</p></td></tr><tr><td><p>item\_id</p></td><td><p>string</p></td><td><p>当语音停止时将创建的用户消息项的 ID。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
          "event_id": "event_B3GGEYh2orwNIdhUagZPz",
          "type": "input_audio_buffer.speech_stopped",
          "audio_end_ms": 28128,
          "item_id": "item_B3GGE8ry4yqbqJGzrVhEM"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.committed <span id="1108a3764an0e" />

- <strong>VAD模式：</strong>当客户端完成音频数据发送（通过[input\_audio\_buffer.append](/zh/model-studio/qwen-asr-realtime-client-events#a42f8e9111n72)事件）后，服务端发送该事件。
- <strong>非VAD模式：</strong>客户端完成音频数据发送（通过[input\_audio\_buffer.append](/zh/model-studio/qwen-asr-realtime-client-events#a42f8e9111n72)事件）并发送[input\_audio\_buffer.commit](/zh/model-studio/qwen-asr-realtime-client-events#d6d5cd90f3q4c)事件后，服务端发送该事件。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "14.11%" }} /><col style={{ width: "52.56%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>input\_audio\_buffer.committed</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>事件ID。</p></td></tr><tr><td><p>previous\_item\_id</p></td><td><p>string</p></td><td><p>前一个对话项的ID</p></td></tr><tr><td><p>item\_id</p></td><td><p>string</p></td><td><p>将创建的用户对话项的 ID。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
            "event_id": "event_1121",
            "type": "input_audio_buffer.committed",
            "previous_item_id": "msg_001",
            "item_id": "msg_002"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.created <span id="04dabbb9b6eto" />

当对话项（item）创建时发送该事件。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "14.19%" }} /><col style={{ width: "52.48%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>conversation.item.created</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>事件ID。</p></td></tr><tr><td><p>previous\_item\_id</p></td><td><p>string</p></td><td><p>前一个对话项的ID</p></td></tr><tr><td><p>item</p></td><td><p>object</p></td><td><p>要添加到对话中的条目。</p></td></tr><tr><td><p>item.id</p></td><td><p>string</p></td><td><p>对话项的唯一ID。</p></td></tr><tr><td><p>item.object</p></td><td><p>string</p></td><td><p>固定为 <code>realtime.item</code> 。</p></td></tr><tr><td><p>item.type</p></td><td><p>string</p></td><td><p>固定为<code>message</code>。</p></td></tr><tr><td><p>item.status</p></td><td><p>string</p></td><td><p>对话项的状态。</p></td></tr><tr><td><p>item.role</p></td><td><p>string</p></td><td><p>消息发送的角色。</p></td></tr><tr><td><p>item.content</p></td><td><p>array\[object]</p></td><td><p>消息的内容。</p></td></tr><tr><td><p>item.content.type</p></td><td><p>string</p></td><td><p>固定为<code>input\_audio</code>。</p></td></tr><tr><td><p>item.content.transcript</p></td><td><p>string</p></td><td><p>固定为<code>null</code>。完整的识别结果通过<a href="/zh/model-studio/qwen-asr-realtime-server-events#403ecacd74qqg">conversation.item.input\_audio\_transcription.completed</a>事件提供。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
          "type": "conversation.item.created",
          "event_id": "event_B3GGKbCfBZTpqFHZ0P8vg",
          "previous_item_id": "item_B3GGE8ry4yqbqJGzrVhEM",
          "item": {
            "id": "item_B3GGEPlolCqdMiVbYIf5L",
            "object": "realtime.item",
            "type": "message",
            "status": "completed",
            "role": "user",
            "content": [
              {
                "type": "input_audio",
                "transcript": null
              }
            ]
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.text <span id="ba1b5cdd79fxu" />

此事件会高频发送，用于展示实时识别结果。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "14.6%" }} /><col style={{ width: "52.07%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>conversation.item.input\_audio\_transcription.text</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>事件ID。</p></td></tr><tr><td><p>item\_id</p></td><td><p>string</p></td><td><p>关联的对话项ID。</p></td></tr><tr><td><p>content\_index</p></td><td><p>integer</p></td><td><p>包含音频的内容部分的索引。</p></td></tr><tr><td><p>language</p></td><td><p>string</p></td><td><div><p>被识别音频的语种。当请求参数<code>language</code>已指定语种时，该值与所指定的参数一致。</p><p>可能的值如下：</p><div><ul><li><p>zh：中文（普通话、四川话、闽南语、吴语）</p></li><li><p>yue：粤语</p></li><li><p>en：英文</p></li><li><p>ja：日语</p></li><li><p>de：德语</p></li><li><p>ko：韩语</p></li><li><p>ru：俄语</p></li><li><p>fr：法语</p></li><li><p>pt：葡萄牙语</p></li><li><p>ar：阿拉伯语</p></li><li><p>it：意大利语</p></li><li><p>es：西班牙语</p></li><li><p>hi：印地语</p></li><li><p>id：印尼语</p></li><li><p>th：泰语</p></li><li><p>tr：土耳其语</p></li><li><p>uk：乌克兰语</p></li><li><p>vi：越南语</p></li></ul></div></div></td></tr><tr><td><p>emotion</p></td><td><p>string</p></td><td><div><p>被识别音频的情感。支持的情感如下：</p><ul><li><p><code>surprised</code>：惊讶</p></li><li><p><code>neutral</code>：平静</p></li><li><p><code>happy</code>：愉快</p></li><li><p><code>sad</code>：悲伤</p></li><li><p><code>disgusted</code>：厌恶</p></li><li><p><code>angry</code>：愤怒</p></li><li><p><code>fearful</code>：恐惧</p></li></ul></div></td></tr><tr><td><p>text</p></td><td><p>string</p></td><td><p>已确认的文本前缀。这是当前句子中，模型已确认不会再变更的部分。</p></td></tr><tr><td><p>stash</p></td><td><p>string</p></td><td><p>预识别的文本后缀。这是紧跟在已确认部分之后，模型仍在处理、可能会被修正的临时草稿。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
          "event_id": "event_R7Pfu8QVBfP5HmpcbEFSd",
          "type": "conversation.item.input_audio_transcription.text",
          "item_id": "item_MpJQPNQzqVRc9aC9zMwSj",
          "content_index": 0,
          "language": "zh",
          "emotion": "neutral",
          "text": "",
          "stash": "北京的"
        }
        ```

        在任何时刻，要获取当前最完整的句子预览，都需要将这两个字段拼接起来：实时预览句子 = `text` + `stash`。

        <Accordion title="点击查看示例">
          假设用户正在说：“今天天气不错，阳光明媚。”

          以下是您可能会收到的事件流以及如何解读它们：

          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.98%" }} /><col style={{ width: "24.98%" }} /><col style={{ width: "24.98%" }} /><col style={{ width: "25.06%" }} /></colgroup><thead><tr><th><p><strong>时间点</strong></p></th><th><p><strong>用户说话进度</strong></p></th><th><p><strong>API 响应 (text 和 stash)</strong></p></th><th><p><strong>客户端 UI 应显示 (text + stash)</strong></p></th></tr></thead><tbody><tr><td><p>T1</p></td><td><p>“今天……”</p></td><td><p>text: ""</p><p>stash: "今天"</p></td><td><p>今天</p></td></tr><tr><td><p>T2</p></td><td><p>“……天气……”</p></td><td><p>text: ""</p><p>stash: "今天天气"</p></td><td><p>今天天气</p></td></tr><tr><td><p>T3</p></td><td><p>“……不错”</p></td><td><p>text: "今天"</p><p>stash: "天气不错"</p></td><td><p>今天天气不错</p><p>（注意，“今天”已被确认并移入text）</p></td></tr><tr><td><p>T4</p></td><td><p>（短暂停顿）</p></td><td><p>text: "今天天气不错，"</p><p>stash: ""</p></td><td><p>今天天气不错，</p><p>（前半句完全确认）</p></td></tr><tr><td><p>T5</p></td><td><p>“……阳光……”</p></td><td><p>text: "今天天气不错，"</p><p>stash: "阳光"</p></td><td><p>今天天气不错，阳光</p></td></tr><tr><td><p>T6</p></td><td><p>“……明媚。”</p></td><td><p>text: "今天天气不错，"</p><p>stash: "阳光明媚。"</p></td><td><p>今天天气不错，阳光明媚。</p></td></tr><tr><td><p>T7</p></td><td><p>（结束说话）</p></td><td><p>-</p></td><td><p>使用<a href="/zh/model-studio/qwen-asr-realtime-server-events#403ecacd74qqg">conversation.item.input\_audio\_transcription.completed</a>的transcript的内容作为最终结果。</p></td></tr></tbody></table>
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.completed <span id="403ecacd74qqg" />

向客户端发送最终识别结果。此事件标志着一个对话项（item）的结束。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.31%" }} /><col style={{ width: "14.34%" }} /><col style={{ width: "52.35%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>conversation.item.input\_audio\_transcription.completed</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>事件ID。</p></td></tr><tr><td><p>item\_id</p></td><td><p>string</p></td><td><p>关联的对话项ID。</p></td></tr><tr><td><p>content\_index</p></td><td><p>integer</p></td><td><p>包含音频的内容部分的索引。</p></td></tr><tr><td><p>language</p></td><td><p>string</p></td><td><div><p>被识别音频的语种。当请求参数<code>language</code>已指定语种时，该值与所指定的参数一致。</p><p>可能的值如下：</p><div><ul><li><p>zh：中文（普通话、四川话、闽南语、吴语）</p></li><li><p>yue：粤语</p></li><li><p>en：英文</p></li><li><p>ja：日语</p></li><li><p>de：德语</p></li><li><p>ko：韩语</p></li><li><p>ru：俄语</p></li><li><p>fr：法语</p></li><li><p>pt：葡萄牙语</p></li><li><p>ar：阿拉伯语</p></li><li><p>it：意大利语</p></li><li><p>es：西班牙语</p></li><li><p>hi：印地语</p></li><li><p>id：印尼语</p></li><li><p>th：泰语</p></li><li><p>tr：土耳其语</p></li><li><p>uk：乌克兰语</p></li><li><p>vi：越南语</p></li></ul></div></div></td></tr><tr><td><p>emotion</p></td><td><p>string</p></td><td><div><p>被识别音频的情感。支持的情感如下：</p><ul><li><p><code>surprised</code>：惊讶</p></li><li><p><code>neutral</code>：平静</p></li><li><p><code>happy</code>：愉快</p></li><li><p><code>sad</code>：悲伤</p></li><li><p><code>disgusted</code>：厌恶</p></li><li><p><code>angry</code>：愤怒</p></li><li><p><code>fearful</code>：恐惧</p></li></ul></div></td></tr><tr><td><p>transcript</p></td><td><p>string</p></td><td><p>识别结果。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
          "event_id": "event_B3GGEjPT2sLzjBM74W6kB",
          "type": "conversation.item.input_audio_transcription.completed",
          "item_id": "item_B3GGC53jGOuIFcjZkmEQ9",
          "content_index": 0,
          "language": "zh",
          "emotion": "neutral",
          "transcript": "今天天气怎么样"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.failed <span id="09bf7fb06aear" />

当输入了音频但是识别失败时，服务端发送该事件。与其他 `error` 事件分开处理，便于客户端识别相关的具体项目。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.31%" }} /><col style={{ width: "14.18%" }} /><col style={{ width: "52.51%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>conversation.item.input\_audio\_transcription.failed</code>。</p></td></tr><tr><td><p>item\_id</p></td><td><p>string</p></td><td><p>关联的对话项ID。</p></td></tr><tr><td><p>content\_index</p></td><td><p>integer</p></td><td><p>包含音频的内容部分的索引。</p></td></tr><tr><td><p>error.code</p></td><td><p>string</p></td><td><p>错误代码。</p></td></tr><tr><td><p>error.message</p></td><td><p>string</p></td><td><p>错误消息。</p></td></tr><tr><td><p>error.param</p></td><td><p>string</p></td><td><p>错误相关的参数。</p></td></tr></tbody></table>
      </td>

      <td>
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

## session.finished <span id="6eaa77339djdv" />

会话结束事件，表示当前会话中，所有音频识别已完成。

该事件只有在客户端发送`session.finish`后才会发送，客户端接收到该事件后可主动断开连接。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33%" }} /><col style={{ width: "14.27%" }} /><col style={{ width: "52.73%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>事件类型。固定为<code>session.finished</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>事件ID。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
          "event_id": "event_2239",
          "type": "session.finished"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
