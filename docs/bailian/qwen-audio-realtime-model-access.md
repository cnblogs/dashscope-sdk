> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-Audio-Realtime 模型接入方式

> 介绍 Qwen-Audio-Realtime 的接入入口、SDK 和事件参考。

<meta property="og:title" content="Qwen-Audio-Realtime 模型接入方式" />

## 接入方式

<Note>
  Qwen-Audio-3.1-Realtime-Plus、Qwen-Audio-3.0-Realtime-Plus 和 Qwen-Audio-3.0-Realtime-Flash 模型支持 AOQ（AI over QUIC）、WebRTC 和 WebSocket 三种传输协议，开发者可以根据业务场景灵活选择。详细接入流程请参见 [Realtime API](/zh/model-studio/realtime-api-overview)。
</Note>

<Tabs>
  <Tab title="AOQ">
    - [AOQ 接入](/zh/model-studio/realtime-aoq-access)
  </Tab>

  <Tab title="WebSocket">
    通过 [WebSocket 接入指南](/zh/model-studio/fun-audiochat-realtime-websocket-api)了解连接地址、鉴权及交互流程。

    SDK 接入：

    - [Android SDK](/zh/model-studio/android-sdk-for-qwen-audio-realtime-service)
    - [iOS SDK](/zh/model-studio/ios-sdk-for-qwen-audio-realtime-service)
    - [HarmonyOS SDK](/zh/model-studio/harmonyos-sdk-for-qwen-audio-realtime-service)
  </Tab>

  <Tab title="WebRTC">
    - [WebRTC 接入](/zh/model-studio/realtime-webrtc-access)
  </Tab>
</Tabs>

## 事件参考

模型参数和事件字段的详细定义请参见：

- [客户端事件](/zh/model-studio/fun-audiochat-client-events)
- [服务端事件](/zh/model-studio/qwen-audio-realtime-server-events)
