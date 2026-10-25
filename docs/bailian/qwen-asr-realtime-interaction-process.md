> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# WebSocket 接入指南

> Qwen-ASR-Realtime 实时语音识别服务通过 WebSocket 协议，接收实时音频流并实时转写。支持 VAD 模式 和 Manual 模式 交互流程。

<meta property="og:title" content="WebSocket 接入指南" />

<strong>用户指南：</strong>关于模型介绍和选型建议请参见[语音识别](/zh/model-studio/asr-model)，示例代码请参见[实时语音识别](/zh/model-studio/real-time-speech-recognition-user-guide)。

## 接口地址 <span id="07bfd546805pn" />

WebSocket URL 固定如下，通过查询参数 `model` 指定要调用的模型名称（将 `<model_name>` 替换为实际的模型）：

<Tabs>
  <Tab title="华北2（北京）">
    `wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/realtime?model=<model_name>`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `wss://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api-ws/v1/realtime?model=<model_name>`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

<Tip>
  URL 必须使用 `wss://` 协议。Authorization 在请求头中设置（参见[请求头](/zh/model-studio/qwen-asr-realtime-interaction-process#b02603aacf7e9)），模型通过 URL 查询参数 `model` 指定。
</Tip>

## 请求头 <span id="b02603aacf7e9" />

请求头中需添加如下信息：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "8.54%" }} /><col style={{ width: "10.31%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Authorization</p></td><td><p>string</p></td><td><p>是</p></td><td><p>鉴权令牌，格式为 <code>Bearer \<your\_api\_key></code>，将 <code>\<your\_api\_key></code> 替换为实际的 API Key。</p></td></tr><tr><td><p>user-agent</p></td><td><p>string</p></td><td><p>否</p></td><td><p>客户端标识，便于服务端追踪来源。</p></td></tr><tr><td><p>X-DashScope-WorkSpace</p></td><td><p>string</p></td><td><p>否</p></td><td><p>阿里云百炼<a href="/zh/model-studio/use-workspace">业务空间ID</a>。</p></td></tr><tr><td><p>X-DashScope-DataInspection</p></td><td><p>string</p></td><td><p>否</p></td><td><p>是否启用数据合规检测功能。默认不传或设为<code>enable</code>。如非必要，请勿启用该参数。</p></td></tr></tbody></table>

<Tip>
  Authorization 鉴权在 WebSocket 握手阶段验证。如果 API Key 无效或缺失，握手将失败并返回 HTTP 401/403 错误。
</Tip>

## 交互流程 <span id="qwen-asr-flow-h2" />

客户端事件和服务端事件的详细说明，请参见[客户端事件](/zh/model-studio/qwen-asr-realtime-client-events)和[服务端事件](/zh/model-studio/qwen-asr-realtime-server-events)。

支持两种交互模式：

- <strong>VAD 模式（默认）</strong>：服务端自动检测语音的起点和终点（断句），适用于实时对话、会议记录等场景。
- <strong>Manual 模式</strong>：由客户端控制断句，适用于客户端能明确判断语句边界的场景，如聊天软件中的发送语音。

### VAD 模式（默认） <span id="9b49887720jcw" />

服务端自动检测语音的起点和终点（断句）。开发者只需持续发送音频流，服务端会在检测到一句话结束时自动返回最终识别结果。此模式适用于实时对话、会议记录等场景。

<strong>启用方式：</strong>配置客户端`session.update`事件的`session.turn_detection`参数。

<img src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9052835871/CAEQaxiBgICW9b6H3RkiIGM5MDgwMTNkMjBjMDRlNTNiOGZlODNjZGJhNDQ3NGJm5812623_20251022102739.334.svg" alt="image" />

- 客户端通过发送`input_audio_buffer.append`事件将音频追加到缓冲区。
- 服务端在检测到语音时返回`input_audio_buffer.speech_started`事件。

  <strong>注意</strong>：如果客户端尚未收到该事件，就直接发送`session.finish`结束会话，服务端会直接返回`session.finished`事件，随后客户端需主动断开连接。
- 客户端继续发送`input_audio_buffer.append`事件提交音频。
- 客户端在音频提交完后，发送`session.finish`事件通知服务端结束当前会话。

  <Warning>
    在 VAD 模式下，推完音频后必须先发送`session.finish`事件再关闭连接。如果客户端直接关闭 WebSocket 连接而未发送该事件，服务端将丢弃当前 in\_progress item，`conversation.item.input_audio_transcription.completed` 等事件将不会到达。建议在调用 `ws.Close()` 之前，先发送 `{"type":"session.finish"}`，并等待收到`session.finished`事件后再关闭连接。
  </Warning>
- 服务端在检测到语音结束时返回`input_audio_buffer.speech_stopped`事件。
- 服务端返回`input_audio_buffer.committed`事件。
- 服务端返回`conversation.item.created`事件。
- 服务端返回`conversation.item.input_audio_transcription.text`事件，其中包含语音识别实时结果。
- 服务端返回`conversation.item.input_audio_transcription.completed`事件，其中包含语音识别最终结果。
- 服务端返回`session.finished`事件，通知客户端识别结束，此时客户端需要主动断开连接。

### Manual 模式 <span id="ee09a3493fsuc" />

由客户端控制断句。客户端需要发送完一整句话的音频后，再发送一个`input_audio_buffer.commit`事件来通知服务端。此模式适用于客户端能明确判断语句边界的场景，如聊天软件中的发送语音。

<strong>启用方式：</strong>将客户端`session.update`事件的`session.turn_detection`设为null。

<img src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9052835871/CAEQaxiBgMDUp8qH3RkiIGEyYTc0NTI1ZmQ1OTQ5NjliNWE0OTYwYTAwMDBlMjBm5812623_20251022102739.334.svg" alt="image" />

- 客户端通过发送`input_audio_buffer.append`事件将音频追加到缓冲区。
- 客户端通过发送`input_audio_buffer.commit`事件来提交输入音频缓冲区。 该提交会在对话中创建一个新的用户消息项。
- 客户端发送`session.finish`事件通知服务端结束当前会话。
- 服务端返回`input_audio_buffer.committed`事件进行响应。
- 服务端返回`conversation.item.input_audio_transcription.text`事件，其中包含语音识别实时结果。
- 服务端返回`conversation.item.input_audio_transcription.completed`事件，其中包含语音识别最终结果。
- 服务端返回`session.finished`事件，通知客户端识别结束，此时客户端需要主动断开连接。
