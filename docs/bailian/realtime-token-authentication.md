> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Token鉴权

> 介绍 Realtime API 的 Token 鉴权机制，包括 API Key 的获取方式以及 WebSocket、WebRTC、AOQ 三种协议的建连鉴权方法。

<meta property="og:title" content="Token鉴权" />

## 鉴权方式 <span id="tkauth-authentication-methods" />

Realtime API 使用 API Key 进行身份认证。无论选择 AOQ、WebRTC 还是 WebSocket 协议接入，均通过 HTTP 请求头中的 `Authorization` 字段携带 Bearer Token 完成身份验证。

鉴权仅发生在<strong>建连阶段</strong>，连接建立后的数据传输无需重复鉴权。

三种协议的鉴权方式对比如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /></colgroup><thead><tr><th><p><strong>协议</strong></p></th><th><p><strong>鉴权时机</strong></p></th><th><p><strong>鉴权方式</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AOQ</p></td><td><p>业务 AppServer 请求网关时</p></td><td><p>HTTP Header <code>Authorization: Bearer \<API\_KEY></code></p></td><td><p>API Key 仅在服务端使用，客户端使用网关返回的 Token</p></td></tr><tr><td><p>WebRTC</p></td><td><p>SDP 交换 HTTP 请求时</p></td><td><p>HTTP Header <code>Authorization: Bearer \<API\_KEY></code></p></td><td><p>客户端或服务端携带 API Key 发起 SDP 交换</p></td></tr><tr><td><p>WebSocket</p></td><td><p>WebSocket 连接握手时</p></td><td><p>HTTP Header <code>Authorization: Bearer \<API\_KEY></code></p></td><td><p>客户端或服务端直接携带 API Key 建连</p></td></tr></tbody></table>

## 获取 API Key <span id="tkauth-get-apikey-title" />

### 步骤 1：开通百炼服务 <span id="tkauth-get-apikey-step1-title" />

1. 访问[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/)并登录您的阿里云账号。
2. 如果是首次使用，按照页面提示完成服务开通。

### 步骤 2：创建 [API Key](/zh/model-studio/get-api-key) <span id="tkauth-get-apikey-step2-title" />

1. 在控制台左侧导航栏中，选择 <strong>API Key</strong>。
2. 点击 <strong>创建 API Key</strong>，选择关联的业务空间。
3. 创建完成后，请<strong>立即复制并妥善保存</strong> API Key。

<Tip>
  <strong>安全提示</strong>：API Key 是您访问服务的唯一凭证，请勿将其硬编码到客户端代码中或提交到代码仓库。建议通过环境变量或后端服务下发的方式管理。
</Tip>

## 建连鉴权详解 <span id="tkauth-auth-detail-title" />

### AOQ 协议鉴权 <span id="tkauth-aoq-title" />

AOQ 采用<strong>服务端代理鉴权</strong>模式：API Key 仅在业务 AppServer 侧使用，客户端通过网关返回的临时 Token 建连，避免 API Key 暴露在客户端。

<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3935914871/p1088069.jpg" alt="Token鉴权" width={557} />

通过 AOQ 接入时，请根据模型系列选择建连请求路径。具体模型的协议支持情况，请参见 [Realtime API 概述](/zh/model-studio/realtime-api-overview#rtov-s02h2)。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "65%" }} />

    <col style={{ width: "35%" }} />
  </colgroup>

  <thead><tr><th style={{ verticalAlign: "top" }}>模型系列</th><th style={{ verticalAlign: "top" }}>建连请求路径</th></tr></thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>Qwen-Omni-Realtime、Qwen-Audio-Realtime、Qwen-Livetranslate-Realtime</td><td style={{ verticalAlign: "top" }}><code>/api/v1/webrtc/realtime</code></td></tr>
    <tr><td style={{ verticalAlign: "top" }}>Qwen-Audio-TTS、CosyVoice、Qwen-Audio-ASR-Streaming、Qwen-Audio-ASR-Message、Fun-ASR-Realtime</td><td style={{ verticalAlign: "top" }}><code>/api/v1/webrtc/inference</code></td></tr>
  </tbody>
</table>

以下 Realtime 示例使用 `qwen3.8-omni-flash-realtime`。`{endpoint}` 使用华北2（北京）的 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com` 或新加坡的 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`，替换为实际业务空间 ID，并使用对应地域和业务空间的 API Key。

<CodeGroup>
  ```bash Realtime 协议
  curl -X POST \
    "https://{endpoint}/api/v1/webrtc/realtime?model=qwen3.8-omni-flash-realtime" \
    -H "Content-Type: application/json" \
    -H "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    -H "x-dashscope-rtc-transport: moq" \
    -d "{\"clientIp\": \"${CLIENT_REAL_IP}\"}"
  ```

  ```bash Inference 协议
  curl -X POST \
    "https://{endpoint}/api/v1/webrtc/inference?model=fun-asr-realtime" \
    -H "Content-Type: application/json" \
    -H "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
    -H "x-dashscope-rtc-transport: moq" \
    -d "{\"clientIp\": \"${CLIENT_REAL_IP}\"}"
  ```
</CodeGroup>

#### 参数说明 <span id="tkauth-aoq-request-fields-title" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>配置项</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>endpoint</p></td><td><p>根据业务情况选择接入域名</p></td><td><p>指定对应的接入域名，详情请参见<a href="/zh/model-studio/regions">选择地域、服务部署范围和接入域名</a>。如果使用<a href="/zh/model-studio/token-plan-overview">Token Plan</a>，接入域名中的 <code>WorkspaceId</code> 固定为 <code>token-plan</code>。</p></td></tr><tr><td><p>Content-Type</p></td><td><p><code>application/json</code></p></td><td><p>指定消息类型</p></td></tr><tr><td><p>Authorization</p></td><td><p><code>Bearer \<YOUR\_API\_KEY></code></p></td><td><p>填写API Key</p></td></tr><tr><td><p>x-dashscope-rtc-transport</p></td><td><p><code>moq</code></p></td><td><p>指定使用 AOQ 协议</p></td></tr><tr><td><p>clientIp</p></td><td><p>客户端真实公网 IP</p></td><td><p>选填。不填写时，默认使用请求百炼网关的 IP；填写后以 clientIp 为准。Realtime API 会根据客户端 IP 分配最佳的 Relay 接入点</p></td></tr></tbody></table>

#### 响应示例 <span id="tkauth-aoq-response-title" />

```json
{
    "sid": "1d06b55683db49bba67a407902f62d02:1782706970:69aecdc5...",
    "aoqTokenForClient": "ecc1a46015d5496ca4ff7a48281eb739",
    "clientRelayEndpoints": [{"endpoint": "121.199.XX.XX", "port": 8443, "route_index": 0}],
    "clientRelayCertFingerprint": "sha256/99843495...",
    "sidExpiresInSecs": 7200,
    "extraInfo": {"workspaceIdHash": "2021b6f98cea4cff"}
}
```

#### 响应参数说明 <span id="tkauth-aoq-response-fields-title" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>sid</p></td><td><p>会话唯一标识</p></td></tr><tr><td><p>aoqTokenForClient</p></td><td><p>客户端连接令牌，传给 SDK 的 token 字段</p></td></tr><tr><td><p>clientRelayEndpoints</p></td><td><p>Relay 接入点数组（endpoint + port）</p></td></tr><tr><td><p>clientRelayCertFingerprint</p></td><td><p>Relay TLS 证书指纹</p></td></tr><tr><td><p>sidExpiresInSecs</p></td><td><p>会话过期时间（秒）</p></td></tr><tr><td><p>extraInfo.workspaceIdHash</p></td><td><p>工作区 ID 哈希</p></td></tr></tbody></table>

#### AOQ Client SDK 连接示例 <span id="tkauth-aoq-sdk-title" />

<Note>
  `clientIp` 为请求体中的选填字段。不填写时，默认使用请求百炼网关的 IP 作为客户端 IP；填写后则以指定的 clientIp 为准。建议由业务 AppServer 获取客户端真实 IP 后填入，以获得最佳的 Relay 接入点。
</Note>

<CodeGroup dropdown>
  ```swift iOS (Swift)
  let resp = try JSONDecoder().decode(AllocateResponse.self, from: responseData)

  let config = AoqConnectConfig()
  config.token = resp.aoqTokenForClient
  config.sid = resp.sid
  config.certFingerprint = resp.clientRelayCertFingerprint
  config.relayEndpoints = resp.clientRelayEndpoints.enumerated().map { index, item in
      let ep = AoqRelayEndpoint()
      // route_index 缺省时按数组下标兜底
      ep.routeIndex = item.routeIndex ?? index
      ep.endpoint = item.endpoint
      ep.port = item.port
      return ep
  }
  config.workspaceIdHash = resp.extraInfo?.workspaceIdHash ?? ""

  let audioTrack = AoqTrackParam()
  audioTrack.trackType = .audio
  let dataTrack = AoqTrackParam()
  dataTrack.trackType = .data
  config.publishTracks = [audioTrack, dataTrack]
  config.subscribeTracks = [audioTrack, dataTrack]

  engine.connect(config)
  ```

  ```java Android (Java) height="500" expandable
  JSONObject obj = new JSONObject(responseText);
  AoqClientEngine.AoqConnectConfig cfg = new AoqClientEngine.AoqConnectConfig();
  cfg.token = obj.optString("aoqTokenForClient", "");
  cfg.sid = obj.optString("sid", "");
  cfg.certFingerprint = obj.optString("clientRelayCertFingerprint", "");

  JSONArray arr = obj.optJSONArray("clientRelayEndpoints");
  if (arr != null) {
      for (int i = 0; i < arr.length(); i++) {
          JSONObject o = arr.optJSONObject(i);
          AoqClientEngine.AoqRelayEndpoint ep = new AoqClientEngine.AoqRelayEndpoint();
          // route_index 缺省时按数组下标兜底
          ep.routeIndex = o.has("route_index") ? o.optInt("route_index", i) : i;
          ep.endpoint = o.optString("endpoint", "");
          ep.port = o.optInt("port", 0);
          cfg.relayEndpoints.add(ep);
      }
  }

  JSONObject ext = obj.optJSONObject("extraInfo");
  cfg.workspaceIdHash = ext != null ? ext.optString("workspaceIdHash", "") : "";

  AoqClientEngine.AoqTrackParam audio = new AoqClientEngine.AoqTrackParam();
  audio.trackType = AoqClientEngine.AoqTrackType.AoqTrackTypeAudio;
  AoqClientEngine.AoqTrackParam data = new AoqClientEngine.AoqTrackParam();
  data.trackType = AoqClientEngine.AoqTrackType.AoqTrackTypeData;
  cfg.publishTracks.add(audio);
  cfg.publishTracks.add(data);
  cfg.subscribeTracks.add(audio);
  cfg.subscribeTracks.add(data);

  engine.connect(cfg);
  ```

  ```javascript OHOS (ArkTS)
  const obj = JSON.parse(responseText) as Record<string, Object | undefined>;
  const cfg: AoqConnectConfig = {
      token: String(obj['aoqTokenForClient'] ?? ''),
      sid: String(obj['sid'] ?? ''),
      certFingerprint: String(obj['clientRelayCertFingerprint'] ?? ''),
      // route_index 缺省时按数组下标兜底
      relayEndpoints: (obj['clientRelayEndpoints'] as Array<any>).map((item, index) => ({
          routeIndex: Number(item['route_index'] ?? index),
          endpoint: String(item['endpoint'] ?? ''),
          port: Number(item['port'] ?? 0)
      })),
      workspaceIdHash: String((obj['extraInfo'] as any)?.['workspaceIdHash'] ?? ''),
      publishTracks: [
          { trackType: AoqTrackType.AoqTrackTypeAudio },
          { trackType: AoqTrackType.AoqTrackTypeData }
      ],
      subscribeTracks: [
          { trackType: AoqTrackType.AoqTrackTypeAudio },
          { trackType: AoqTrackType.AoqTrackTypeData }
      ]
  };
  engine.connect(cfg);
  ```
</CodeGroup>

### WebRTC 协议鉴权 <span id="tkauth-webrtc-title" />

WebRTC 通过 HTTP POST 请求完成 SDP 交换，鉴权在此阶段完成。客户端将 Offer SDP 发送至服务端，服务端返回 Answer SDP。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>配置项</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>请求方法</p></td><td><p>POST</p></td><td><p>-</p></td></tr><tr><td><p>请求地址</p></td><td><p><code>https\://\{endpoint}/api/v1/webrtc/realtime?model=\{model\_name}</code></p></td><td><p>使用时替换 endpoint 和 model\_name，不同模型的连接地址不同，详情请参见<a href="/zh/model-studio/realtime-connect-model#conn-rtc-title">WebRTC 接入</a></p></td></tr><tr><td><p>Content-Type</p></td><td><p><code>application/sdp</code></p></td><td><p>请求体为 SDP 字符串</p></td></tr><tr><td><p>Authorization</p></td><td><p><code>Bearer \<API\_KEY></code></p></td><td><p>填写API Key</p></td></tr><tr><td><p>响应</p></td><td><p>HTTP 200，返回 Answer SDP</p></td><td><p>失败返回 4xx</p></td></tr></tbody></table>

```javascript
const pc = new RTCPeerConnection();
const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
stream.getAudioTracks().forEach(t => pc.addTrack(t, stream));
pc.createDataChannel('oai-events');

const offer = await pc.createOffer();
await pc.setLocalDescription(offer);

// 等待 ICE 收集完成后发送
const resp = await fetch(API_URL, {
  method: 'POST',
  headers: {
    'Content-Type': 'application/sdp',
    'Authorization': `Bearer ${API_KEY}`,
  },
  body: pc.localDescription.sdp,
});
const answerSdp = await resp.text();
await pc.setRemoteDescription({ type: 'answer', sdp: answerSdp });
```

### WebSocket 协议鉴权 <span id="tkauth-websocket-title" />

WebSocket 的鉴权方式最为简单，在建立连接时直接通过 HTTP Header 携带 API Key 即可。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>配置项</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>连接地址</p></td><td><p><code>wss\://dashscope.aliyuncs.com/api-ws/v1/realtime?model=\{model\_name}</code></p></td><td><p>不同模型的连接地址不同，详情请参见<a href="/zh/model-studio/realtime-connect-model#conn-ws-title">WebSocket 接入</a></p></td></tr><tr><td><p>Authorization</p></td><td><p><code>Bearer \<API\_KEY></code></p></td><td><p>填写API Key</p></td></tr></tbody></table>

```python
import websocket, os
API_KEY = os.getenv("DASHSCOPE_API_KEY")
URL = "wss://dashscope.aliyuncs.com/api-ws/v1/realtime?model=qwen3.5-omni-plus-realtime"
ws = websocket.WebSocketApp(URL, header=["Authorization: Bearer " + API_KEY])
ws.run_forever()
```
