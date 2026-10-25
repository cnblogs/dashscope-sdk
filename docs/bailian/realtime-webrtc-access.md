> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# WebRTC 接入

> 本文介绍通过 WebRTC 接入 Realtime API 的通用流程：准备鉴权、初始化连接对象、配置媒体方向、完成 SDP 交换、按目标模型协议交互，以及释放资源。

<meta property="og:title" content="WebRTC 接入" />

<span id="conn-rtc-title" />

WebRTC 使用媒体轨道传输音视频，使用 DataChannel 传输模型事件和文本。Web 端可使用浏览器原生 API，其他端可使用支持标准 WebRTC 的库。百炼不提供专用 WebRTC SDK。

## 前提条件

- 完成[接入概览](/zh/model-studio/realtime-connect-model)中的准备工作。

- 在 [Realtime API 概述](/zh/model-studio/realtime-api-overview)中确认目标模型支持 WebRTC。当前支持范围包含实时全模态、实时语音对话、实时语音翻译和多模态交互套件；语音合成、语音识别模型应选择支持它们的 AOQ 或 WebSocket。

- 业务 AppServer 已准备好匹配地域与业务空间的 API Key，用于代理 SDP 交换。鉴权字段见 [Token 鉴权](/zh/model-studio/realtime-token-authentication)。

<Note>
  浏览器采集媒体时，使用安全上下文并申请所需的麦克风或摄像头权限；接收音频时处理浏览器自动播放限制。
</Note>

## 整体流程 <span id="conn-rtc-seq-title" />

<Image src="https://g-adoc.alcasset.com/media/maas_docs/sfm-cn/zh/images/6a4b3c2d1e0f92f8.png" alt="WebRTC 接入时序图" width={600} noZoom={false} />

1. 业务 AppServer 准备 API Key 和目标模型参数。
2. 客户端创建连接对象并注册回调。
3. 配置媒体方向及 DataChannel，暂不挂载上行媒体轨道。
4. 通过 AppServer 交换 SDP，等待连接及事件通道就绪。
5. 按模型协议完成配置，收到确认后挂载所需上行媒体轨道。
6. 完成交互后停止采集、关闭连接并释放资源。

<Note>
  - 音视频通过 WebRTC 媒体轨道传输；应用负责获取本地媒体轨道、绑定远端播放器，并按目标模型的事件定义收发 DataChannel 消息。

  - WebRTC 的鉴权在 SDP 交换时完成，无需先获取 AOQ Token。浏览器通过业务 AppServer 完成鉴权请求与 SDP 转发，避免将长期 API Key 放入前端代码。

  - 连接成功表示传输通道已建立，但仍需等待目标模型初始化成功后再发送媒体。例如，Realtime 模型需要等待 `session.updated`。因此，客户端应在模型初始化成功后，才能开启所需的上行媒体。
</Note>

## 1. 准备鉴权和连接参数

业务 AppServer 根据目标模型、地域和业务空间配置 SDP 交换请求。标准 Realtime 建连参数如下；应用专属接入参数以对应最佳实践为准。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "32%" }} />

    <col style={{ width: "68%" }} />
  </colgroup>

  <thead>
    <tr><th style={{ verticalAlign: "top" }}>配置项</th><th style={{ verticalAlign: "top" }}>值</th></tr>
  </thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>方法</td><td style={{ verticalAlign: "top" }}><code>POST</code></td></tr>
    <tr><td style={{ verticalAlign: "top" }}>地址</td><td style={{ verticalAlign: "top" }}><code>https\://{endpoint}/api/v1/webrtc/realtime?model={model_name}</code></td></tr>
    <tr><td style={{ verticalAlign: "top" }}><code>Content-Type</code></td><td style={{ verticalAlign: "top" }}><code>application/sdp</code></td></tr>
    <tr><td style={{ verticalAlign: "top" }}><code>Authorization</code></td><td style={{ verticalAlign: "top" }}><code>Bearer \<API\_KEY></code></td></tr>
    <tr><td style={{ verticalAlign: "top" }}>请求体</td><td style={{ verticalAlign: "top" }}>Offer SDP</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>成功响应</td><td style={{ verticalAlign: "top" }}>Answer SDP</td></tr>
  </tbody>
</table>

完整地域、地址和鉴权说明见 [Token 鉴权](/zh/model-studio/realtime-token-authentication)。业务 AppServer 的接口由业务自行实现，不属于百炼 SDK API。

## 2. 初始化连接对象并注册回调

Web 端创建 `RTCPeerConnection`，其他端执行对应库的初始化操作。建立连接前注册以下处理逻辑：

- 连接状态变化：判断连接是否成功，以及失败或断开。

- DataChannel 建立、打开、消息和关闭：承载模型事件。

- 远端媒体轨道：接收并播放音频，或处理目标应用支持的其他媒体。

以下代码片段共享同一作用域，按章节组合到一个脚本中，在所有变量、函数和回调注册完成后再由用户操作调用 `startSession()`。示例每次创建一条连接；结束后再次通话需重新创建这些对象。

```javascript
const pc = new RTCPeerConnection({ iceServers: [ ] });

const channels = new Set();
const senders = new Map();
const remoteAudio = document.createElement("audio");
remoteAudio.controls = true; // 自动播放被阻止时，用户可手动播放。
remoteAudio.autoplay = true;
document.body.appendChild(remoteAudio);

let localStream = null;
let eventChannel = null;
let stopped = false;
let started = false;
let sessionCreated = false;
let updateSent = false;
let modelReady = false;
let mediaStarted = false;
const requestController = new AbortController();
```

注册消息监听应早于模型初始化，避免遗漏服务端主动发送的会话事件。不要只监听客户端创建的通道：服务端会通过名为 `txt` 的通道推送事件，应处理 `datachannel` 回调。

## 3. 配置媒体方向和 DataChannel

### 选择四个媒体方向

以下方向均以客户端为视角。WebRTC 通过媒体收发器协商发送与接收能力：双向为 `sendrecv`，仅发送为 `sendonly`，仅接收为 `recvonly`。最终可用能力还取决于服务端 SDP 应答及模型支持范围。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "32%" }} />

    <col style={{ width: "68%" }} />
  </colgroup>

  <thead>
    <tr><th style={{ verticalAlign: "top" }}>方向</th><th style={{ verticalAlign: "top" }}>配置内容</th></tr>
  </thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>推音频</td><td style={{ verticalAlign: "top" }}>协商音频发送能力，准备麦克风或外部音频轨道；模型就绪后挂载</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>拉音频</td><td style={{ verticalAlign: "top" }}>协商音频接收能力，在远端轨道回调中绑定音频播放</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>推视频</td><td style={{ verticalAlign: "top" }}>目标模型支持视觉输入时协商视频发送能力，准备摄像头或外部视频轨道</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>拉视频</td><td style={{ verticalAlign: "top" }}>仅在目标模型/应用明确支持视频输出时协商接收能力，并实现渲染</td></tr>
  </tbody>
</table>

例如，语音对话一般使用双向音频，带视觉输入的语音对话还需要视频上行。Offer SDP 需要包含音频媒体段 `m=audio`；不能仅因业务无需播放音频就删除该媒体段。

### 各模型如何选择

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "30%" }} />

    <col style={{ width: "18%" }} />

    <col style={{ width: "18%" }} />

    <col style={{ width: "17%" }} />

    <col style={{ width: "17%" }} />
  </colgroup>

  <thead>
    <tr><th style={{ verticalAlign: "top" }}>模型/应用</th><th style={{ verticalAlign: "top" }}>推音频</th><th style={{ verticalAlign: "top" }}>拉音频</th><th style={{ verticalAlign: "top" }}>推视频</th><th style={{ verticalAlign: "top" }}>拉视频</th></tr>
  </thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>Qwen-Omni-Realtime</td><td style={{ verticalAlign: "top" }}>语音输入时开启</td><td style={{ verticalAlign: "top" }}>需要语音回复时开启</td><td style={{ verticalAlign: "top" }}>需要视觉输入时开启</td><td style={{ verticalAlign: "top" }}>不支持开启</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>Qwen-Audio-Realtime</td><td style={{ verticalAlign: "top" }}>语音对话时开启</td><td style={{ verticalAlign: "top" }}>需要语音回复时开启</td><td style={{ verticalAlign: "top" }}>不支持开启</td><td style={{ verticalAlign: "top" }}>不支持开启</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>Qwen-LiveTranslate-Realtime</td><td style={{ verticalAlign: "top" }}>语音输入时开启</td><td style={{ verticalAlign: "top" }}>需要语音译文时开启</td><td style={{ verticalAlign: "top" }}>按版本及场景配置</td><td style={{ verticalAlign: "top" }}>不支持开启</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>multimodal-dialog</td><td style={{ verticalAlign: "top" }}>按应用输入配置</td><td style={{ verticalAlign: "top" }}>按应用输出配置</td><td style={{ verticalAlign: "top" }}>按应用能力配置</td><td style={{ verticalAlign: "top" }}>仅在应用明确支持时配置</td></tr>
  </tbody>
</table>

该表描述业务需要的媒体方向，不承诺所有方向组合都可单独协商。实际配置以目标模型和当前服务端协商能力为准。

### 准备媒体但延后上行

在创建 Offer 前预先协商所需媒体方向。对需要等待初始化确认的模型，先保留发送能力而不挂载真实采集轨道；模型就绪后再通过对应 sender 的 `replaceTrack` 等操作开始发送。接收媒体的处理逻辑可以提前注册。

音频发送和接收、视频发送和接收应分别按需配置，不要把打开麦克风、播放音频和模型初始化合并为一个操作。

### 媒体配置示例

下面以双向语音、可选视频上行为例。`media` 的四个开关描述业务需要；拉视频仅在目标应用明确支持时开启。`addTransceiver` 预先协商方向，采集到的轨道先保存在本地，直到模型就绪后再挂载到 sender。

```javascript
const media = {
  sendAudio: true,
  receiveAudio: true,
  sendVideo: false,
  receiveVideo: false,
};

function mediaDirection(send, receive) {
  if (send && receive) return "sendrecv";
  if (send) return "sendonly";
  if (receive) return "recvonly";
  return "inactive";
}

async function configureMedia() {
  for (const kind of ["audio", "video"]) {
    const send = kind === "audio" ? media.sendAudio : media.sendVideo;
    const receive = kind === "audio" ? media.receiveAudio : media.receiveVideo;
    // 保留 m=audio；具体方向组合须由目标服务支持。
    if (kind === "audio" || send || receive) {
      const transceiver = pc.addTransceiver(kind, {
        direction: mediaDirection(send, receive),
      });
      if (send) senders.set(kind, transceiver.sender);
    }
  }
  if (!media.sendAudio && !media.sendVideo) return;
  const stream = await navigator.mediaDevices.getUserMedia({
    audio: media.sendAudio,
    video: media.sendVideo ? {
      width: { ideal: 640 }, height: { ideal: 480 },
      frameRate: { ideal: 2, max: 2 },
    } : false,
  });
  // 用户可能在权限弹窗期间结束通话。
  if (stopped) {
    stream.getTracks().forEach(track => track.stop());
    throw new Error("会话已结束");
  }
  localStream = stream;
  // 此时 sender.track 仍为空，不向模型发送采集数据。
}
```

视频分辨率和帧率按目标模型要求设置；需要将本地预览与上行帧率分离时，可参考最佳实践中的 Canvas 降帧实现。

### 创建事件通道

在创建 Offer 前创建 DataChannel，使 SDP 包含数据通道协商。客户端创建通道及服务端创建通道的处理方式，参见下方 WebRTC 最佳实践。

DataChannel 负责发送模型初始化、文本和控制事件，并接收模型结果、状态和错误。媒体轨道负责音视频传输。

```javascript
// bindDataChannel 的完整实现见“Data 通道发送和接收事件”。
pc.ondatachannel = ({ channel }) => bindDataChannel(channel);
const clientChannel = pc.createDataChannel("oai-events");
bindDataChannel(clientChannel);
```

## 4. 建立连接 <span id="conn-rtc-connect-title" />

按以下顺序完成建连：

1. 调用 `createOffer` 和 `setLocalDescription`。
2. 按服务端支持的 ICE 方式完成候选信息收集；本示例等待 ICE 收集完成后发送本地 SDP。
3. 将本地 SDP 发送到业务 AppServer，由 AppServer 携带 API Key 请求百炼 SDP 交换接口。
4. 检查 HTTP 状态，成功后以返回的 Answer SDP 调用 `setRemoteDescription`。
5. 等待连接状态成功，并确认用于发送模型事件的 DataChannel 已打开。

SDP 交换成功或 `setRemoteDescription` 完成，只表示完成相应协商步骤，不能直接当作媒体连接已经成功。DataChannel 未处于 `open` 状态时也不能发送模型事件。

### 建连代码及状态回调

监听 `connectionstatechange` 判断连接状态，监听 `icegatheringstatechange` 等待完整的本地 SDP。模型初始化还需要 DataChannel 的 `open` 和模型的就绪事件，不能仅依靠 SDP 交换完成判断。

```javascript
pc.onconnectionstatechange = () => {
  console.log("WebRTC 状态：", pc.connectionState);
  if (pc.connectionState === "connected") {
    tryInitializeModel();
  } else if (["failed", "disconnected", "closed"].includes(pc.connectionState)) {
    // 本示例选择结束连接；业务可按需要设计断线恢复策略。
    endSession();
  }
};

function waitForIceComplete() {
  return new Promise((resolve, reject) => {
    const timer = setTimeout(() => finish(new Error("ICE 收集超时")), 15000);
    function finish(error) {
      clearTimeout(timer);
      pc.removeEventListener("icegatheringstatechange", check);
      requestController.signal.removeEventListener("abort", cancel);
      error ? reject(error) : resolve();
    }
    function check() {
      if (pc.iceGatheringState === "complete") finish();
    }
    function cancel() { finish(new Error("会话已结束")); }
    pc.addEventListener("icegatheringstatechange", check);
    requestController.signal.addEventListener("abort", cancel, { once: true });
    if (stopped) cancel();
    else check();
  });
}

function normalizeAnswerSdp(sdp) {
  return String(sdp).trim().replace(/\r?\n/g, "\r\n") + "\r\n";
}

async function startSession() {
  if (started || stopped) return;
  started = true;
  try {
    await configureMedia();
    if (stopped) return;
    const offer = await pc.createOffer();
    await pc.setLocalDescription(offer);
    await waitForIceComplete();

    // 这是业务自行实现的同源接口，不是百炼官方接口。
    // AppServer 为目标模型转发 SDP，附带 API Key，并原样返回 Answer SDP。
    const response = await fetch("/api/realtime/sdp", {
      method: "POST",
      headers: { "Content-Type": "application/sdp" },
      body: pc.localDescription.sdp,
      signal: requestController.signal,
    });
    if (!response.ok) {
      throw new Error(`SDP 交换失败：${response.status} ${await response.text()}`);
    }
    const answerSdp = normalizeAnswerSdp(await response.text());
    if (stopped) return;
    await pc.setRemoteDescription({ type: "answer", sdp: answerSdp });
    // 后续由 connectionstatechange、DataChannel 和模型事件驱动。
  } catch (error) {
    if (!stopped) console.error("建连失败：", error);
    endSession();
  }
}
```

业务 AppServer 应按第 1 节的地址和请求头转发 Offer；示例接口约定返回纯 SDP 文本。不要将含有 HTTP 响应头、日志或 JSON 包装的内容直接传给 `setRemoteDescription`。

## 5. 按模型协议交互并收发媒体 <span id="conn-rtc-config-title" />

### 初始化模型

连接及事件通道可用后，按目标模型协议处理初始化。应用需要保存已收到的服务端初始化事件，并结合通道状态推进流程，避免依赖回调到达的固定顺序。

例如，使用 `session.update` 配置会话的模型，应按其协议构造事件，等待配置确认后再发送输入。音色、输出模态、VAD、采样率等参数都应来自当前模型文档。多模态交互套件使用其自己的事件协议。

`session.created` 表示会话创建，不能统一视为配置已生效。对于要求确认会话配置的模型，应等待 `session.updated`。其他模型或应用使用各自定义的就绪条件。

各模型事件文档可从 [WebSocket 接入概览](/zh/model-studio/realtime-websocket-overview)中的模型导航进入。这里只复用模型事件定义，媒体传输方式和 WebRTC 限制仍以 WebRTC 接入说明为准。

### 模型事件示例（Omni）

以下示例单独演示 Omni 的 `session.created → session.update → session.updated` 流程。切换其他模型时，替换初始化事件构造和回复处理逻辑，遵循后文链接的模型事件定义。收到 `session.updated` 确认配置完成后，再挂载上行媒体轨道。

```javascript
function tryInitializeModel() {
  if (stopped || updateSent || !sessionCreated ||
      pc.connectionState !== "connected" ||
      eventChannel?.readyState !== "open") return;
  sendModelEvent({
    event_id: `event_${crypto.randomUUID()}`,
    type: "session.update",
    session: {
      modalities: media.receiveAudio ? ["text", "audio"] : ["text"],
      input_audio_format: "pcm",
      output_audio_format: "pcm",
      turn_detection: { type: "server_vad", threshold: 0.5,
                        silence_duration_ms: 800 },
    },
  });
  updateSent = true;
}

async function handleModelEvent(event, channel) {
  if (stopped) return;
  if (event.type === "session.created") {
    sessionCreated = true;
    eventChannel = channel; // 沿收到会话事件的通道发送配置。
    tryInitializeModel();
  } else if (event.type === "session.updated" && updateSent) {
    modelReady = true;
    await startSendingMedia();
  } else if (event.type === "error") {
    console.error("模型错误：", event.error);
    endSession();
  } else {
    // 按所选模型定义展示文本、转录或响应状态；音频从媒体轨道接收。
    console.log("模型事件：", event);
  }
}
```

### 开始媒体收发 <span id="conn-rtc-media-title" />

满足模型就绪条件后，挂载需要上行的音频、视频轨道。通过远端轨道回调处理模型输出，并持续解析 DataChannel 返回的事件。

- 音频通过 RTP 媒体轨道传输，无需发送 `input_audio_buffer.append`。

- 画面通过视频轨道传输；当前 WebRTC 接入不支持 `input_image_buffer.append`。

- 文本、状态、控制和错误通过 DataChannel 处理。

```javascript
async function startSendingMedia() {
  if (stopped || !modelReady || mediaStarted) return;
  mediaStarted = true;

  for (const track of localStream?.getTracks() ?? []) {

    if (stopped) return;
    const sender = senders.get(track.kind);
    if (sender) await sender.replaceTrack(track);
  }
}

// 在 startSession() 前注册。无 streams 的轨道也能正确接收。
const remoteAudioStream = new MediaStream();
const remoteVideo = media.receiveVideo ? document.createElement("video") : null;
if (remoteVideo) {
  remoteVideo.autoplay = true;
  remoteVideo.playsInline = true;
  remoteVideo.controls = true;
  document.body.appendChild(remoteVideo);
}
pc.ontrack = ({ track }) => {
  if (stopped) return;
  if (track.kind === "audio" && media.receiveAudio) {
    remoteAudioStream.addTrack(track);
    remoteAudio.srcObject = remoteAudioStream;
    remoteAudio.play().catch(() => {
      console.info("自动播放受限，请点击音频控件播放");
    });
  }
  if (track.kind === "video" && remoteVideo) {
    remoteVideo.srcObject = new MediaStream([track]);
    remoteVideo.play().catch(() => console.info("请点击视频控件播放"));
  }
};
```

## 6. 结束交互并释放资源

不再使用连接时，由客户端业务层停止应用持有的本地采集轨道，关闭 DataChannel 和 `RTCPeerConnection`，释放播放器等资源并清理业务状态。

连接失败或异常断开时，撤销模型就绪状态。重新建立连接后按目标模型协议初始化，避免沿用上一条连接的会话状态。

```javascript
function endSession() {
  if (stopped) return;
  stopped = true;
  modelReady = false;
  sessionCreated = false;
  updateSent = false;
  mediaStarted = false;
  requestController.abort(); // 取消未完成的 SDP 请求/ICE 等待。
  localStream?.getTracks().forEach(track => track.stop());
  localStream = null;
  channels.forEach(channel => channel.close());
  channels.clear();
  eventChannel = null;
  pc.close();
  senders.clear();
  remoteAudioStream.getTracks().forEach(track => track.stop());
  remoteAudio.pause();
  remoteAudio.srcObject = null;
  remoteAudio.remove();
  if (remoteVideo) {
    remoteVideo.srcObject?.getTracks().forEach(track => track.stop());
    remoteVideo.pause();
    remoteVideo.srcObject = null;
    remoteVideo.remove();
  }
}
```

在用户点击结束按钮或页面退出时调用 `endSession()`。如果业务额外启用了最佳实践中的 Canvas 降帧或录制，还需取消动画循环、停止 Canvas 媒体轨道及 `MediaRecorder`。

## Data 通道发送和接收事件

客户端通过 WebRTC DataChannel 发送和接收模型事件。发送前确认通道处于 `open` 状态，使用 `send` 发送序列化后的模型事件；接收时在对应通道的 `message` 回调中解析消息，并处理服务端创建的事件通道。

发送事件的名称、字段、参数和调用时机遵循目标模型的**客户端事件定义**；收到消息后的解析方式、状态变更、结果和错误处理遵循该模型的**服务端事件定义**。初始化事件根据模型要求按需发送，后续文本和控制事件按照业务需要发送。

### Data 通道代码示例

同一个绑定函数处理客户端创建和服务端创建的通道；它只负责收发与 JSON 解析，模型语义交给第 5 节的 `handleModelEvent`。`sendModelEvent` 可用于发送模型定义的初始化、文本或控制事件，但应在对应模型允许的时机调用。

```javascript
function sendModelEvent(event, channel = eventChannel) {
  if (stopped || pc.connectionState !== "connected" ||
      channel?.readyState !== "open") {
    throw new Error("Data 通道尚不可用");
  }
  channel.send(JSON.stringify(event));
}

function bindDataChannel(channel) {
  channels.add(channel);
  channel.onopen = () => {
    if (stopped) return;
    console.log("DataChannel 已打开：", channel.label);
    tryInitializeModel();
  };
  channel.onmessage = ({ data }) => {
    if (stopped) return;
    let event;
    try { event = JSON.parse(data); }
    catch (error) {
      console.warn("无法解析模型事件：", error);
      return;
    }
    if (!event || typeof event !== "object") return;
    handleModelEvent(event, channel).catch(error => {
      if (!stopped) console.error("模型事件处理失败：", error);
      endSession();
    });
  };
  channel.onerror = error => console.error("DataChannel 错误：", error);
  channel.onclose = () => {
    channels.delete(channel);
    if (channel === eventChannel) endSession();
  };
  if (channel.readyState === "open") channel.onopen();
}
```

将上述片段放在同一脚本中，完成全部声明和回调注册后，在业务的开始按钮回调中调用 `startSession()`，结束按钮回调中调用 `endSession()`。这两个函数为本文示例函数，不是 WebRTC 内置 API。

### 模型事件定义

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "26%" }} />

    <col style={{ width: "37%" }} />

    <col style={{ width: "37%" }} />
  </colgroup>

  <thead>
    <tr><th style={{ verticalAlign: "top" }}>模型/应用</th><th style={{ verticalAlign: "top" }}>客户端事件定义（发送）</th><th style={{ verticalAlign: "top" }}>服务端事件定义（接收）</th></tr>
  </thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>Qwen-Omni-Realtime</td><td style={{ verticalAlign: "top" }}><Link href="/zh/model-studio/client-events">客户端事件</Link></td><td style={{ verticalAlign: "top" }}><Link href="/zh/model-studio/server-events">服务端事件</Link></td></tr>
    <tr><td style={{ verticalAlign: "top" }}>Qwen-Audio-Realtime</td><td style={{ verticalAlign: "top" }}><Link href="/zh/model-studio/fun-audiochat-client-events">客户端事件</Link></td><td style={{ verticalAlign: "top" }}><Link href="/zh/model-studio/qwen-audio-realtime-server-events">服务端事件</Link></td></tr>
    <tr><td style={{ verticalAlign: "top" }}>Qwen-LiveTranslate-Realtime</td><td style={{ verticalAlign: "top" }}><Link href="/zh/model-studio/live-translator-client-events">客户端事件</Link></td><td style={{ verticalAlign: "top" }}><Link href="/zh/model-studio/live-translator-server-events">服务端事件</Link></td></tr>
    <tr><td style={{ verticalAlign: "top" }}>multimodal-dialog</td><td style={{ verticalAlign: "top" }}><Link href="/zh/model-studio/multimodal-interaction-protocol">交互协议中的 Input Message</Link></td><td style={{ verticalAlign: "top" }}><Link href="/zh/model-studio/multimodal-interaction-protocol">交互协议中的 Output Message</Link></td></tr>
  </tbody>
</table>

多模态交互套件的双向事件定义在同一篇交互协议中。请按实际接入的模型版本选择事件定义，不同模型之间不共用一套固定事件。

模型事件的发送与接收还应遵循以下规则：

- 按模型定义识别事件类型和关联标识。例如，Realtime 模型事件通常使用  `type`；多模态交互套件的客户端事件使用  `header.action`，服务端事件使用  `header.event`，并通过  `task_id`  关联任务。

- 模型要求的初始化确认、输入提交、响应取消和任务结束等操作，均在 Data 通道中按对应协议处理；它们属于模型交互语义，与断开传输连接是不同操作。

- 音视频数据通过媒体轨道传输。引用模型事件文档时，媒体封装和可用交互模式仍遵循本文的接入方式，不直接套用 WebSocket 的媒体发送方式。

## 最佳实践 <span id="conn-rtc-best-practices" />

- [通过 WebRTC 使用 Omni 实现实时通话](/zh/model-studio/best-practice-webrtc-omni-realtime)：业务界面、录制和 Canvas 降帧示例；模型初始化及媒体挂载遵循本文时序。

- [通过 WebRTC 使用多模态交互套件实现实时通话](/zh/model-studio/best-practice-webrtc-multimodal-dialog)：应用接入及对应事件流程。
