> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 实时语音识别（Qwen-ASR-Realtime）客户端事件

> 本文档介绍在与 Qwen-ASR Realtime API 的 WebSocket 会话中，客户端向服务端发送的事件。

<meta property="og:title" content="实时语音识别（Qwen-ASR-Realtime）客户端事件" />

<strong>用户指南：</strong>模型介绍、功能特性和完整示例代码请参见[实时语音识别](/zh/model-studio/real-time-speech-recognition-user-guide)

## session.update <span id="af43722339yva" />

用于更新会话配置，建议在 WebSocket 连接建立后首先发送该事件。建议在WebSocket连接建立成功后，立即发送此事件作为交互的第一步。如果未发送，系统将使用默认配置。

服务端成功处理此事件后，会发送`服务端事件`事件作为确认。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30.77%" }} /><col style={{ width: "16.59%" }} /><col style={{ width: "19.95%" }} /><col style={{ width: "32.69%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必须</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件类型。固定为<code>session.update</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件ID。</p></td></tr><tr><td><p>session</p></td><td><p>object</p></td><td><p>是</p></td><td><p>包含会话配置的对象。</p></td></tr><tr><td><p>session.input\_audio\_format</p></td><td><p>string</p></td><td><p>否</p></td><td><div><p>音频格式。支持<code>pcm</code>和<code>opus</code>。</p><p>默认值：<code>pcm</code>。</p></div></td></tr><tr><td><p>session.sample\_rate</p></td><td><p>integer</p></td><td><p>否</p></td><td><div><p>音频采样率（Hz）。支持<code>16000</code>和<code>8000</code>。</p><p>默认值：<code>16000</code>。</p><p>设置为 <code>8000</code> 时，服务端会先升采样到16000Hz再进行识别，可能引入微小延迟。建议仅在源音频为8000Hz（如电话线路）时使用。</p></div></td></tr><tr><td><p>session.input\_audio\_transcription</p></td><td><p>object</p></td><td><p>否</p></td><td><p>语音识别相关配置。</p></td></tr><tr><td><p>session.input\_audio\_transcription.language</p></td><td><p>string</p></td><td><p>否</p></td><td><div><p>音频源语言。</p><ul><li><p>zh：中文（普通话、四川话、闽南语、吴语）</p></li><li><p>yue：粤语</p></li><li><p>en：英文</p></li><li><p>ja：日语</p></li><li><p>de：德语</p></li><li><p>ko：韩语</p></li><li><p>ru：俄语</p></li><li><p>fr：法语</p></li><li><p>pt：葡萄牙语</p></li><li><p>ar：阿拉伯语</p></li><li><p>it：意大利语</p></li><li><p>es：西班牙语</p></li><li><p>hi：印地语</p></li><li><p>id：印尼语</p></li><li><p>th：泰语</p></li><li><p>tr：土耳其语</p></li><li><p>uk：乌克兰语</p></li><li><p>vi：越南语</p></li><li><p>cs：捷克语</p></li><li><p>da：丹麦语</p></li><li><p>fil：菲律宾语</p></li><li><p>fi：芬兰语</p></li><li><p>is：冰岛语</p></li><li><p>ms：马来语</p></li><li><p>no：挪威语</p></li><li><p>pl：波兰语</p></li><li><p>sv：瑞典语</p></li></ul></div></td></tr><tr><td><p>session.turn\_detection</p></td><td><p>object</p></td><td><p>否</p></td><td><p>VAD（Voice Activity Detection，语音活动检测）配置。</p><p>它是启用/关闭VAD模式的开关：若将它设为null，则将关闭<a href="/zh/model-studio/qwen-asr-realtime-interaction-process#9b49887720jcw">VAD 模式</a>，启用<a href="/zh/model-studio/qwen-asr-realtime-interaction-process#ee09a3493fsuc">Manual 模式</a>；反之则相反。</p></td></tr><tr><td><p>session.turn\_detection.type</p></td><td><p>string</p></td><td><p>否，<code>turn\_dection</code>存在时必须</p></td><td><p>固定为 <code>server\_vad</code>。</p></td></tr><tr><td><p>session.turn\_detection.threshold</p></td><td><p>float</p></td><td><p>否</p></td><td><div><p>VAD检测阈值。推荐将该值设为<code>0.0</code>。</p><p>默认值：<code>0.2</code>。</p><p>取值范围：<code>\[-1, 1]</code>。</p><p>较低的阈值会提高 VAD 的灵敏度，可能将背景噪音误判为语音。较高的阈值则降低灵敏度，有助于在嘈杂环境中减少误触发。</p></div></td></tr><tr><td><p>session.turn\_detection.silence\_duration\_ms</p></td><td><p>integer</p></td><td><p>否</p></td><td><div><p>VAD断句检测阈值（ms）。静音持续时长超过该阈值将被认为是语句结束。推荐将该值设为<code>400</code>。</p><p>默认值：<code>800</code>。</p><p>取值范围：<code>\[200, 6000]</code>。</p><p>较低的值（如 300ms）可使模型更快响应，但可能导致在自然停顿处发生不合理的断句。较高的值（如 1200ms）可更好地处理长句内的停顿，但会增加整体响应延迟。</p></div></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
            "event_id": "event_123",
            "type": "session.update",
            "session": {
                "input_audio_format": "pcm",
                "sample_rate": 16000,
                "input_audio_transcription": {
                    "language": "zh"
                },
                "turn_detection": {
                    "type": "server_vad",
                    "threshold": 0.0,
                    "silence_duration_ms": 400
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.append <span id="a42f8e9111n72" />

用于将音频数据块追加到服务端的输入缓冲区。这是流式发送音频的核心事件。

<strong>不同场景下的区别：</strong>

- <strong>VAD 模式</strong>：音频缓冲区用于语音活动检测，服务端会自动决定何时提交音频进行识别。
- <strong>非VAD模式</strong>：客户端可以控制每个事件中的音频数据量，单个 `input_audio_buffer.append` 事件中的 `audio` 字段内容最大为 15 MiB。建议流式发送较小的音频块以获得更快的响应。

<strong>重要提示</strong>：服务端不会对`input_audio_buffer.append`事件发送任何确认响应。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30.4%" }} /><col style={{ width: "16.86%" }} /><col style={{ width: "19.98%" }} /><col style={{ width: "32.76%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必须</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件类型。固定为<code>input\_audio\_buffer.append</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件ID。</p></td></tr><tr><td><p>audio</p></td><td><p>string</p></td><td><p>是</p></td><td><p>Base64编码的音频数据。</p></td></tr></tbody></table>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
          "event_id": "event_2728",
          "type": "input_audio_buffer.append",
          "audio": "<audio> by base64"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.commit <span id="d6d5cd90f3q4c" />

非VAD模式下，用于手动触发识别。此事件通知服务端，客户端已发送完一段完整的语音，将当前缓冲区内的所有音频数据作为一个整体进行识别。

<strong>禁用场景：</strong>VAD模式。

服务端成功处理后，会发送`input_audio_buffer.committed`事件作为确认响应。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30.02%" }} /><col style={{ width: "17.23%" }} /><col style={{ width: "19.51%" }} /><col style={{ width: "33.24%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必须</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件类型。固定为<code>input\_audio\_buffer.commit</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件ID。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
          "event_id": "event_789",
           "type": "input_audio_buffer.commit"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.finish <span id="147ce70052d4z" />

用于结束当前会话。

服务端响应流程：

- 已检测到语音：服务端完成最后的语音识别后，发送包含识别结果的`conversation.item.input_audio_transcription.completed`事件，随后发送[session.finished](/zh/model-studio/qwen-asr-realtime-server-events#6eaa77339djdv)事件作为会话结束标识。
- 未检测到语音：服务端直接发送`session.finished`事件。

客户端监听到`session.finished`事件后，需主动断开连接。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "29.62%" }} /><col style={{ width: "17.41%" }} /><col style={{ width: "19.72%" }} /><col style={{ width: "33.25%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必须</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件类型。固定为<code>session.finish</code>。</p></td></tr><tr><td><p>event\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件ID。</p></td></tr></tbody></table>
      </td>

      <td>
        ```json
        {
          "event_id": "event_341",
          "type": "session.finish",
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
