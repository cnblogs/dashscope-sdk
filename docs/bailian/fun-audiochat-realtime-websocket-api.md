> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# WebSocket 接入指南

> Qwen-Audio Realtime API 通过 WebSocket 协议提供实时语音对话能力。客户端通过发送和接收 JSON 事件与服务端交互，支持语音输入、文本输入、语音活动检测（VAD）、流式语音和文本输出等功能。

<meta property="og:title" content="WebSocket 接入指南" />

<strong>用户指南</strong>：[实时语音对话（Qwen-Audio-Realtime）](https://help.aliyun.com/zh/model-studio/fun-audiochat-realtime)。客户端事件和服务端事件的详细说明，请参见[客户端事件](/zh/model-studio/fun-audiochat-client-events)和[服务端事件](/zh/model-studio/qwen-audio-realtime-server-events)。

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

## 接口地址 <span id="fc63ep01h2" />

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
  URL 必须使用 `wss://` 协议。Authorization 在请求头中设置，模型通过 URL 查询参数 `model` 指定。
</Tip>

## 请求头 <span id="fc63hdr01h2" />

请求头中需添加如下信息：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "8.54%" }} /><col style={{ width: "10.31%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Authorization</p></td><td><p>string</p></td><td><p>是</p></td><td><p>鉴权令牌，格式为 <code>Bearer \<your\_api\_key></code>，将 <code>\<your\_api\_key></code> 替换为实际的 API Key。</p></td></tr><tr><td><p>user-agent</p></td><td><p>string</p></td><td><p>否</p></td><td><p>客户端标识，便于服务端追踪来源。</p></td></tr><tr><td><p>X-DashScope-WorkSpace</p></td><td><p>string</p></td><td><p>否</p></td><td><p>阿里云百炼业务空间 ID。</p></td></tr></tbody></table>

<Tip>
  Authorization 鉴权在 WebSocket 握手阶段验证。如果 API Key 无效或缺失，握手将失败并返回 HTTP 401/403 错误。
</Tip>

## 核心概念 <span id="fc63concept01h2" />

- <strong>Session（会话）</strong>：一次 WebSocket 连接对应一个会话，会话内维护配置和对话上下文。
- <strong>Conversation Item（对话项）</strong>：对话中的每条消息，按链表顺序组织。
- <strong>Response（响应）</strong>：一次模型推理产生的输出，包含一个或多个输出项，输出项可以是助手消息，也可以是函数调用。
- <strong>Function Call（函数调用）</strong>：模型请求客户端执行工具函数时产生的输出项。客户端执行完成后通过 `function_call_output` 写回结果，再用 `response.create` 触发下一轮推理。
- <strong>Turn Detection（轮次检测）</strong>：控制何时触发推理。

## 交互模式 <span id="fc63modes01h2" />

Qwen-Audio Realtime API 支持三种交互模式，通过 `session.update` 事件的 `turn_detection.type` 参数配置：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18%" }} /><col style={{ width: "18%" }} /><col style={{ width: "36%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>模式</strong></p></th><th><p><strong>turn\_detection.type</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>适用场景</strong></p></th></tr></thead><tbody><tr><td><p><strong>server\_vad</strong></p></td><td><p><code>server\_vad</code></p></td><td><p>服务端 VAD 检测语音起止，自动触发推理。</p></td><td><p>免提对话、语音助手</p></td></tr><tr><td><p><strong>smart\_turn</strong></p></td><td><p><code>smart\_turn</code></p></td><td><p>融合声学感知与语义理解判断轮次边界，而非仅依赖人声信号。无语义的声音（如”嗯”、”啊”）不会触发对话轮或打断模型播报。</p></td><td><p>低延迟自然对话、高质量打断</p></td></tr><tr><td><p><strong>push-to-talk</strong></p></td><td><p><code>null</code></p></td><td><p>客户端手动提交音频、手动触发推理。</p></td><td><p>按键说话、精确控制</p></td></tr></tbody></table>

## 交互流程 <span id="fc63flow01h2" />

客户端事件和服务端事件的详细说明，请参见客户端事件和服务端事件。

### server\_vad 模式 <span id="fc63flow01vadh3" />

服务端对传入的音频进行语音活动检测，检测到语音结束后自动触发推理。

<strong>启用方式：</strong>配置 `session.update` 事件的 `turn_detection.type` 为 `server_vad`。

<Tabs>
  <Tab title="一轮完整对话">
    下图展示了 server\_vad 模式下的典型交互时序：

    <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7268354871/p1088432.svg" alt="111" width={310} />

    按时间顺序，客户端与服务端的交互流程如下：

    1. 客户端建立 WebSocket 连接，服务端返回 `session.created` 事件。
    2. 客户端发送 `session.update` 配置会话参数，服务端返回 `session.updated`。
    3. 客户端持续发送 `input_audio_buffer.append` 追加音频数据。
    4. 服务端检测到语音开始，返回 `input_audio_buffer.speech_started`，同时流式返回 ASR 转写增量 `conversation.item.input_audio_transcription.delta`。
    5. 服务端检测到语音结束，返回 `input_audio_buffer.speech_stopped`、`input_audio_buffer.committed` 和 `conversation.item.created`。
    6. 服务端自动生成响应，流式返回文本和音频增量（`response.audio_transcript.delta`、`response.audio.delta`），最终返回 `response.done`。
  </Tab>

  <Tab title="用户打断">
    模型播报期间，若 VAD 检测到用户开始说话，服务端会取消当前响应（返回 `response.done`，状态为 `cancelled`），随后开始新一轮语音输入和响应。下图展示了用户打断的交互时序：

    <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7268354871/p1088435.svg" alt="111" width={352} />
  </Tab>
</Tabs>

### smart\_turn 模式 <span id="fc63flow01smarth3" />

融合声学感知与语义理解检测语音结束，可过滤回应语、背景音等无意义声音。无语义的声音通过 `conversation.item.ambient_audio_transcription.delta` 事件透传，不触发对话轮。

<strong>启用方式：</strong>配置 `session.update` 事件的 `turn_detection.type` 为 `smart_turn`。

<Tabs>
  <Tab title="一轮完整对话">
    下图展示了 smart\_turn 模式下的典型交互时序：

    <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7268354871/p1088441.svg" alt="111" width={343} />

    与 server\_vad 模式的主要区别：

    - 无语义声音（“嗯”、“啊”等）不会触发推理，而是通过 `ambient_audio_transcription` 事件返回。
    - 已判定有效的语音可能被撤回（`input_audio_buffer.speech_stopped` 返回 `reason=turn_invalid`），此时不触发推理。
    - 在等待用户下一轮输入时，客户端可显式发送 `response.create` 触发推理。
  </Tab>

  <Tab title="用户打断">
    与 server\_vad 模式的打断处理基本一致。下图展示了用户打断的交互时序：

    <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7268354871/p1088443.svg" alt="111" width={340} />
  </Tab>

  <Tab title="无效轮次">
    已判定有效的语音可能被撤回（`input_audio_buffer.speech_stopped` 返回 `reason=turn_invalid`），此时不触发推理，客户端应继续发送音频等待下一轮有效语音。下图展示了无效轮次的交互时序：

    <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7268354871/p1088444.svg" alt="111" width={366} />
  </Tab>
</Tabs>

### 说话人增强配置流程 <span id="fc63flow01vph3" />

在 smart\_turn 模式下，首次 `session.update` 中传入 `voiceprint_audio_urls` 时，服务端将异步执行声纹注册（加载目标说话人音频特征），并通过事件通知注册进度。声纹注册失败不阻塞正常对话流程。

按时间顺序，声纹注册的交互流程如下：

1. 客户端发送 `session.update`，在 `turn_detection.voiceprint_audio_urls` 中传入声纹音频 URL，服务端返回 `session.created`。
2. 服务端立即异步启动声纹注册，在 `session.updated` 返回<strong>之前</strong>先推送 `voiceprint_audio_list.in_progress` 事件，携带本次注册任务的唯一标识 `item_id`。
3. 服务端返回 `session.updated`，确认会话配置已生效。
4. 声纹注册完成后，服务端推送终态事件（`item_id` 与步骤 2 一致）：

   - 注册成功：`voiceprint_audio_list.completed`。
   - 注册失败：`voiceprint_audio_list.failed`，附带 `reason` 字段说明失败原因（如音频 URL 无法下载）。

<Note>
  `voiceprint_audio_urls` 仅在<strong>第一次</strong> `session.update` 时生效，后续传入该字段将被忽略。
</Note>

### push-to-talk 模式 <span id="fc63flow01manualh3" />

客户端手动控制音频提交和推理触发，适用于按键说话场景。

<strong>启用方式：</strong>配置 `session.update` 事件的 `turn_detection` 为 `null`。

<Tabs>
  <Tab title="一轮完整对话">
    下图展示了 push-to-talk 模式下的典型交互时序：

    <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7268354871/p1088447.svg" alt="111" width={335} />

    按时间顺序，客户端与服务端的交互流程如下：

    1. 客户端持续发送 `input_audio_buffer.append` 追加音频数据。
    2. 用户说完话后，客户端发送 `input_audio_buffer.commit` 提交缓冲区。
    3. 客户端发送 `response.create` 手动触发推理。
    4. 服务端生成响应，流式返回文本和音频。
  </Tab>

  <Tab title="用户打断">
    客户端发送 `response.cancel` 取消当前响应，服务端返回 `response.done`（状态为 `cancelled`，原因为 `client_cancelled`）。下图展示了用户打断的交互时序：

    <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7268354871/p1088449.svg" alt="111" width={319} />
  </Tab>
</Tabs>

## 各模式操作约束 <span id="fc63constraint01h2" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "23.33%" }} /><col style={{ width: "23.33%" }} /><col style={{ width: "23.33%" }} /></colgroup><thead><tr><th><p><strong>操作</strong></p></th><th><p><strong>push-to-talk</strong></p></th><th><p><strong>server\_vad</strong></p></th><th><p><strong>smart\_turn</strong></p></th></tr></thead><tbody><tr><td><p>session.update</p></td><td><p>IDLE 时全部可改；非 IDLE 时部分受限</p></td><td><p>IDLE 时全部可改；非 IDLE 时部分受限</p></td><td><p>IDLE 时全部可改；非 IDLE 时部分受限</p></td></tr><tr><td><p>input\_audio\_buffer.append</p></td><td><p>允许</p></td><td><p>允许</p></td><td><p>允许</p></td></tr><tr><td><p>input\_audio\_buffer.commit</p></td><td><p>允许</p></td><td><p>忽略</p></td><td><p>忽略</p></td></tr><tr><td><p>input\_audio\_buffer.clear</p></td><td><p>允许</p></td><td><p>忽略</p></td><td><p>忽略</p></td></tr><tr><td><p>response.create</p></td><td><p>允许（需先通过 <code>input\_audio\_buffer.commit</code> 提交缓冲区音频；当前有响应正在生成时不允许重复触发）</p></td><td><p>当前无响应正在生成时允许；有响应正在生成时不允许重复触发</p></td><td><p>等待用户下一轮输入时允许；当前处于一个 turn 内时（收到 <code>input\_audio\_buffer.speech\_started</code> 到 <code>response.done</code> 期间）不允许重复触发</p></td></tr><tr><td><p>response.cancel</p></td><td><p>允许（推理中）</p></td><td><p>允许（推理中）</p></td><td><p>允许（推理中）</p></td></tr><tr><td><p>conversation.item.create/delete/retrieve</p></td><td><p>允许</p></td><td><p>允许</p></td><td><p>允许</p></td></tr></tbody></table>

<Note>
  `turn_detection` 和 `input_audio_format` 仅在首次发送音频之前（IDLE 状态）允许修改。
</Note>

## 错误处理 <span id="fc63err01h2" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "30%" }} /><col style={{ width: "40%" }} /></colgroup><thead><tr><th><p><strong>类型</strong></p></th><th><p><strong>行为</strong></p></th><th><p><strong>示例</strong></p></th></tr></thead><tbody><tr><td><p>客户端错误（<code>invalid\_request\_error</code>）</p></td><td><p>连接保持，仅通知</p></td><td><p>参数不合法、状态不允许、item\_id 重复</p></td></tr><tr><td><p>服务端错误（<code>server\_error</code>）</p></td><td><p>连接终止</p></td><td><p>LLM 连接失败、存储故障</p></td></tr></tbody></table>
