> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# WebSocket 接入概览

> 了解 WebSocket 的接入地址、鉴权、Realtime 与 Inference 交互流程及各模型的事件差异。

<meta property="og:title" content="WebSocket 接入概览" />

## 前提条件

- 已开通目标模型或应用，并确认其支持的地域。
- 已获取与调用地域、业务空间匹配的 API Key，参见[Token 鉴权](/zh/model-studio/realtime-token-authentication)。
- 使用业务空间专属域名时，已获取 [Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。

连接协议的选型和支持范围参见 [Realtime API 概述](/zh/model-studio/realtime-api-overview)。

## 请求头

在 WebSocket 握手请求中设置 `Authorization`。其余请求头按对应模型的参数说明使用。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{width: "27%"}} />

    <col style={{width: "13%"}} />

    <col style={{width: "60%"}} />
  </colgroup>

  <thead>
    <tr>
      <th style={{verticalAlign: "top"}}>
        请求头
      </th>

      <th style={{verticalAlign: "top"}}>
        是否必需
      </th>

      <th style={{verticalAlign: "top"}}>
        说明
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{verticalAlign: "top"}}>
        <code>Authorization</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        是
      </td>

      <td style={{verticalAlign: "top"}}>
        使用 <code>Bearer \<API\_KEY></code> 格式传递 API Key。
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        <code>user-agent</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        否
      </td>

      <td style={{verticalAlign: "top"}}>
        标识调用客户端。
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        <code>X-DashScope-WorkSpace</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        按模型要求
      </td>

      <td style={{verticalAlign: "top"}}>
        指定业务空间 ID，具体使用方式见对应模型的请求头说明。
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        <code>X-DashScope-DataInspection</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        按模型要求
      </td>

      <td style={{verticalAlign: "top"}}>
        数据合规检查配置，支持范围与取值见对应模型的请求头说明。
      </td>
    </tr>
  </tbody>
</table>

## 接入地址

使用 `wss://` 协议，根据目标模型选择 API 路径和模型名的传递位置。下表列出各模型的接入方式，支持的模型及地域见对应模型文档。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{width: "40%"}} />

    <col style={{width: "30%"}} />

    <col style={{width: "30%"}} />
  </colgroup>

  <thead>
    <tr>
      <th style={{verticalAlign: "top"}}>
        模型系列
      </th>

      <th style={{verticalAlign: "top"}}>
        API 路径
      </th>

      <th style={{verticalAlign: "top"}}>
        模型名传递位置
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-Omni-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>/api-ws/v1/realtime</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        URL 查询参数 <code>model</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-Audio-TTS/CosyVoice
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>/api-ws/v1/inference</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>run-task</code> 的 <code>payload.model</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-TTS-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>/api-ws/v1/realtime</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        URL 查询参数 <code>model</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-Audio-ASR/Fun-ASR/Paraformer
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>/api-ws/v1/inference</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>run-task</code> 的 <code>payload.model</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-ASR-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>/api-ws/v1/realtime</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        URL 查询参数 <code>model</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-Audio-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>/api-ws/v1/realtime</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        URL 查询参数 <code>model</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-LiveTranslate-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>/api-ws/v1/realtime</code>
      </td>

      <td style={{verticalAlign: "top"}}>
        URL 查询参数 <code>model</code>
      </td>
    </tr>
  </tbody>
</table>

业务空间专属域名：

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{width: "25%"}} />

    <col style={{width: "75%"}} />
  </colgroup>

  <thead>
    <tr>
      <th style={{verticalAlign: "top"}}>
        地域
      </th>

      <th style={{verticalAlign: "top"}}>
        业务空间专属域名
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{verticalAlign: "top"}}>
        华北 2（北京）
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>\{WorkspaceId}.cn-beijing.maas.aliyuncs.com</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        新加坡
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>\{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com</code>
      </td>
    </tr>
  </tbody>
</table>

将 `{WorkspaceId}` 替换为实际业务空间 ID。例如：

```text
wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/realtime?model=qwen3.8-omni-flash-realtime
wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference
```

Qwen-TTS-Realtime 也可使用公共域名：北京为 `dashscope.aliyuncs.com`，新加坡为 `dashscope-intl.aliyuncs.com`，API 路径仍为 `/api-ws/v1/realtime`。API Key 必须与调用地域匹配。

## 公共交互流程

`/api-ws/v1/realtime` 使用会话制协议，`/api-ws/v1/inference` 使用任务制协议。

### Realtime 协议

客户端建立连接后，通过 `session.update` 配置会话，收到 `session.updated` 后发送输入。提交输入、触发响应和结束会话的方式由目标模型决定。

<Image src="https://g-adoc.alcasset.com/media/maas_docs/sfm-cn/zh/images/6a4b3c2d1e0f92d7.png" alt="Realtime 协议的会话配置、输入输出和结束流程" width={560} />

1. **建立连接**：在 URL 的 `model` 参数中指定模型，在请求头中携带 API Key。握手成功后，服务端发送 `session.created`。
2. **配置会话**：发送 `session.update`，设置模型支持的音频格式、输出模态、音色或语音检测参数，等待 `session.updated`。各模型支持的会话参数见对应的客户端事件文档。
3. **发送输入**：音频通过 `input_audio_buffer.append` 发送 Base64 数据；Qwen-TTS 通过 `input_text_buffer.append` 发送文本。支持图像输入的模型使用 `input_image_buffer.append`，并遵循对应模型对图片和音频时序的要求。
4. **提交并获取结果**：自动模式下由服务端触发处理；手动模式下按输入类型发送 `input_audio_buffer.commit` 或 `input_text_buffer.commit`。是否还需 `response.create`，以及应监听哪些结果事件，参见下方差异对照。
5. **结束会话**：支持结束事件的模型使用 `session.finish`，等待 `session.finished` 和剩余结果；其他模型在结果接收完成后关闭 WebSocket 连接。`response.done` 表示一次响应结束，不表示整个连接关闭；纯语音识别以转录完成事件标识识别结束。

### Inference 协议

客户端发送 `run-task` 并等待 `task-started` 后，才开始传输输入。语音识别发送二进制音频帧，语音合成通过 `continue-task` 发送文本；输入结束后发送 `finish-task`，等待 `task-finished`。

<Image src="https://g-adoc.alcasset.com/media/maas_docs/sfm-cn/zh/images/6a4b3c2d1e0f92eb.png" alt="Inference 协议的语音识别与语音合成任务流程" width={560} />

1. **建立连接**：使用 `/api-ws/v1/inference`，在请求头中携带 API Key。
2. **启动任务**：发送 `run-task`，在 `payload.model` 中指定模型，并设置输入格式等参数。为任务生成唯一的 `header.task_id`，等待 `task-started`。
3. **传输输入并接收结果**：
   - 语音识别：发送二进制音频帧，通过 `result-generated` 获取识别结果。
   - 语音合成：通过 `continue-task` 的 `payload.input.text` 发送待合成文本，接收二进制音频帧；模型还可能通过 `result-generated` 返回时间戳等信息。
4. **结束任务**：发送 `finish-task` 后继续接收剩余结果，直到收到 `task-finished`。同一任务的 `run-task`、`continue-task` 和 `finish-task` 必须使用相同的 `header.task_id`。之后关闭连接，或按对应模型的要求复用连接。

## 各模型差异

### Realtime 模型

下表列出输入触发和会话结束方式。VAD 类型、音频格式及其他参数的完整取值见对应模型的事件文档。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{width: "22%"}} />

    <col style={{width: "53%"}} />

    <col style={{width: "25%"}} />
  </colgroup>

  <thead>
    <tr>
      <th style={{verticalAlign: "top"}}>
        模型
      </th>

      <th style={{verticalAlign: "top"}}>
        输入与触发方式
      </th>

      <th style={{verticalAlign: "top"}}>
        结束方式
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen3.8-Omni / Qwen3.5-Omni
      </td>

      <td style={{verticalAlign: "top"}}>
        VAD 模式自动触发响应；Manual 模式先发送 <code>input\_audio\_buffer.commit</code>，再发送 <code>response.create</code>。
      </td>

      <td style={{verticalAlign: "top"}}>
        接收完结果后关闭连接。
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-TTS-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>server\_commit</code> 模式自动提交文本；<code>commit</code> 模式发送 <code>input\_text\_buffer.commit</code>，无需 <code>response.create</code>。
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>session.finish → session.finished</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-ASR-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        VAD 模式自动处理；Manual 模式发送 <code>input\_audio\_buffer.commit</code>，无需 <code>response.create</code>。
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>session.finish → session.finished</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-Audio-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        自动模式由服务端判断轮次；Manual 模式先发送 <code>input\_audio\_buffer.commit</code>，再发送 <code>response.create</code>。
      </td>

      <td style={{verticalAlign: "top"}}>
        接收完结果后关闭连接。
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen3.8-LiveTranslate
      </td>

      <td style={{verticalAlign: "top"}}>
        使用 <code>output\_modalities</code> 配置输出模态，通过 <code>audio.input.turn\_detection</code> 配置轮次检测。持续发送音频，输入结束后发送 <code>session.finish</code>。
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>session.finish → session.finished</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen3.5-LiveTranslate
      </td>

      <td style={{verticalAlign: "top"}}>
        使用 <code>modalities</code> 和 <code>turn\_detection</code> 配置会话。Manual 模式发送 <code>input\_audio\_buffer.commit</code> 后自动生成响应，无需 <code>response.create</code>。
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>session.finish → session.finished</code>
      </td>
    </tr>
  </tbody>
</table>

主要输出事件如下，具体返回的事件取决于配置的输出模态。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{width: "25%"}} />

    <col style={{width: "75%"}} />
  </colgroup>

  <thead>
    <tr>
      <th style={{verticalAlign: "top"}}>
        模型
      </th>

      <th style={{verticalAlign: "top"}}>
        主要输出事件
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-Omni-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>response.text.delta</code> / <code>response.audio\_transcript.delta</code> / <code>response.audio.delta</code> / <code>response.done</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-TTS-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>response.audio.delta</code> / <code>response.done</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-ASR-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>conversation.item.input\_audio\_transcription.text</code> / <code>conversation.item.input\_audio\_transcription.completed</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen-Audio-Realtime
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>response.audio\_transcript.delta</code> / <code>response.audio.delta</code> / <code>response.done</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen3.8-LiveTranslate
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>response.text.delta</code> / <code>response.audio\_transcript.delta</code> / <code>response.audio.delta</code> / <code>response.done</code>
      </td>
    </tr>

    <tr>
      <td style={{verticalAlign: "top"}}>
        Qwen3.5-LiveTranslate
      </td>

      <td style={{verticalAlign: "top"}}>
        <code>response.text.text</code> / <code>response.audio\_transcript.text</code> / <code>response.audio.delta</code> / <code>response.done</code>
      </td>
    </tr>
  </tbody>
</table>

<Note>
  翻译模型的事件名与版本有关：Qwen3.8-LiveTranslate 使用 `.delta`，Qwen3.5-LiveTranslate 使用 `.text`。例如，音频对应的译文分别通过 `response.audio_transcript.delta` 和 `response.audio_transcript.text` 返回。
</Note>

### Inference 模型

Qwen-Audio-TTS/CosyVoice 使用 `run-task → task-started → continue-task → finish-task → task-finished` 的任务流程；Qwen-Audio-ASR/Fun-ASR/Paraformer 语音识别在 `task-started` 后发送二进制音频帧。两类模型均通过 `task-failed` 报告任务错误。

## 各模型详细接入

<span id="其他模型与应用" />

### 实时全模态

- [实时全模态](/zh/model-studio/realtime#bdaa43cdd7hsd)

### 实时语音合成

#### Qwen-Audio-TTS

- [WebSocket 接入指南](/zh/model-studio/qwen-audio-tts-websocket-api)
- [Java SDK](/zh/model-studio/qwen-audio-tts-java-sdk)
- [Python SDK](/zh/model-studio/qwen-audio-tts-python-sdk)
- [Android SDK](/zh/model-studio/qwen-audio-tts-android-sdk)
- [iOS SDK](/zh/model-studio/qwen-audio-tts-ios-sdk)
- [HarmonyOS SDK](/zh/model-studio/qwen-audio-tts-harmonyos-sdk)

#### CosyVoice

- [WebSocket 接入指南](/zh/model-studio/cosyvoice-websocket-api)
- [Java SDK](/zh/model-studio/cosyvoice-java-sdk)
- [Python SDK](/zh/model-studio/cosyvoice-python-sdk)
- [Android SDK](/zh/model-studio/cosyvoice-android-sdk)
- [iOS SDK](/zh/model-studio/cosyvoice-ios-sdk)
- [HarmonyOS SDK](/zh/model-studio/cosyvoice-harmonyos-sdk)

#### Qwen-TTS-Realtime

- [WebSocket 接入指南](/zh/model-studio/interactive-process-of-qwen-tts-realtime-synthesis)
- [Python SDK](/zh/model-studio/qwen-tts-realtime-python-sdk)
- [Java SDK](/zh/model-studio/qwen-tts-realtime-java-sdk)

#### Sambert

- [WebSocket 接入指南](/zh/model-studio/sambert-websocket-api)
- [Java SDK](/zh/model-studio/sambert-java-sdk)
- [Python SDK](/zh/model-studio/sambert-python-sdk)
- [Android SDK](/zh/model-studio/sambert-android-sdk)
- [iOS SDK](/zh/model-studio/sambert-ios-sdk)
- [HarmonyOS SDK](/zh/model-studio/sambert-harmonyos-sdk)

### 实时语音识别

#### Qwen-Audio-3.1-ASR-Flash-Message

- [WebSocket 接入指南](/zh/model-studio/qwen-asr-message-websocket-api)
- [Python SDK](/zh/model-studio/qwen-asr-message-python-sdk)
- [Java SDK](/zh/model-studio/qwen-asr-message-java-sdk)
- [Android SDK](/zh/model-studio/qwen-asr-message-android-sdk)
- [iOS SDK](/zh/model-studio/qwen-asr-message-ios-sdk)
- [HarmonyOS SDK](/zh/model-studio/qwen-asr-message-harmonyos-sdk)

#### Qwen-Audio-ASR-Streaming

- [WebSocket 接入指南](/zh/model-studio/qwen-audio-asr-streaming-websocket-api)
- [Python SDK](/zh/model-studio/qwen-audio-asr-streaming-python-sdk)
- [Java SDK](/zh/model-studio/qwen-audio-asr-streaming-java-sdk)
- [Android SDK](/zh/model-studio/qwen-audio-asr-streaming-android-sdk)
- [iOS SDK](/zh/model-studio/qwen-audio-asr-streaming-ios-sdk)
- [HarmonyOS SDK](/zh/model-studio/qwen-audio-asr-streaming-harmonyos-sdk)

#### Fun-ASR-Realtime

- [WebSocket 接入指南](/zh/model-studio/fun-asr-realtime-websocket-api)
- [Python SDK](/zh/model-studio/fun-asr-realtime-python-sdk)
- [Java SDK](/zh/model-studio/fun-asr-realtime-java-sdk)
- [Android SDK](/zh/model-studio/android-sdk-for-fun-asr-real-time-service)
- [iOS SDK](/zh/model-studio/ios-sdk-for-fun-asr-real-time-service)
- [HarmonyOS SDK](/zh/model-studio/harmonyos-sdk-for-fun-asr-real-time-service)

#### Qwen-ASR-Realtime

- [WebSocket 接入指南](/zh/model-studio/qwen-asr-realtime-interaction-process)
- [Python SDK](/zh/model-studio/qwen-asr-realtime-python-sdk)
- [Java SDK](/zh/model-studio/qwen-asr-realtime-java-sdk)

#### Paraformer

- [WebSocket 接入指南](/zh/model-studio/websocket-for-paraformer-real-time-service)
- [Python SDK](/zh/model-studio/paraformer-real-time-speech-recognition-python-sdk)
- [Java SDK](/zh/model-studio/paraformer-real-time-speech-recognition-java-sdk)
- [Android SDK](/zh/model-studio/android-sdk-for-paraformer-real-time-service)
- [iOS SDK](/zh/model-studio/ios-sdk-for-paraformer-real-time-service)
- [HarmonyOS SDK](/zh/model-studio/harmonyos-sdk-for-paraformer-real-time-service)

### 实时语音对话

#### Qwen-Audio-Realtime

- [WebSocket 接入指南](/zh/model-studio/fun-audiochat-realtime-websocket-api)
- [Android SDK](/zh/model-studio/android-sdk-for-qwen-audio-realtime-service)
- [iOS SDK](/zh/model-studio/ios-sdk-for-qwen-audio-realtime-service)
- [HarmonyOS SDK](/zh/model-studio/harmonyos-sdk-for-qwen-audio-realtime-service)

### 实时音视频翻译

#### Qwen-Livetranslate-Realtime

- [Python SDK](/zh/model-studio/qwen-livetranslate-python-sdk)

- [Java SDK](/zh/model-studio/qwen-livetranslate-java-sdk)

- [实时语音翻译](/zh/model-studio/qwen3-5-livetranslate-flash-realtime)

### 多模态交互套件

- [多模态开发套件](/zh/model-studio/multimodal-interaction-protocol)

## 错误处理

- **握手失败**：根据 HTTP 状态码和错误信息检查连接地址、API Key、业务空间及模型权限。遇到 401/403 时，优先核对鉴权配置。
- **模型不存在或未开通**：核对模型名、服务开通状态及调用地域。
- **Realtime 错误**：处理 `error` 事件，读取错误类型和消息。根据错误信息调整请求中的事件类型或参数。
- **Inference 错误**：收到 `task-failed` 表示任务失败，可通过 `header.error_code` 和 `header.error_message` 获取失败原因。

修正配置或处理网络中断后，重新建立连接，并按对应协议重新配置会话或创建任务。错误码说明参见[错误码](/zh/model-studio/error-code)。
