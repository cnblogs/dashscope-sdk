> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-Audio-TTS 模型接入方式

> 介绍 Qwen-Audio-TTS 的接入入口、SDK 和事件参考。

<meta property="og:title" content="Qwen-Audio-TTS 模型接入方式" />

## 接入方式

<Note>
  支持通过 AOQ（AI over QUIC）和 WebSocket 协议接入。详细接入流程请参见 [Realtime API](/zh/model-studio/realtime-api-overview)。
</Note>

<Tabs>
  <Tab title="AOQ">
    - [AOQ 接入](/zh/model-studio/realtime-aoq-access)
  </Tab>

  <Tab title="WebSocket">
    通过 [WebSocket 接入指南](/zh/model-studio/qwen-audio-tts-websocket-api)了解连接地址、鉴权及交互流程。

    SDK 接入：

    - [Java SDK](/zh/model-studio/qwen-audio-tts-java-sdk)
    - [Python SDK](/zh/model-studio/qwen-audio-tts-python-sdk)
    - [Android SDK](/zh/model-studio/qwen-audio-tts-android-sdk)
    - [iOS SDK](/zh/model-studio/qwen-audio-tts-ios-sdk)
    - [HarmonyOS SDK](/zh/model-studio/qwen-audio-tts-harmonyos-sdk)
  </Tab>
</Tabs>

## 事件参考

模型参数和事件字段的详细定义请参见：

- [客户端事件](/zh/model-studio/qwen-audio-tts-client-events)
- [服务端事件](/zh/model-studio/qwen-audio-tts-server-events)
