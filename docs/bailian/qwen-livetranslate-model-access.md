> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-Livetranslate-Realtime 模型接入方式

> 介绍 Qwen-Livetranslate-Realtime 的接入入口、SDK 和事件参考。

<meta property="og:title" content="Qwen-Livetranslate-Realtime 模型接入方式" />

## 接入方式

<Note>
  Qwen-Livetranslate-Realtime 系列模型支持 AOQ（AI over QUIC）、WebRTC 和 WebSocket 三种传输协议，开发者可以根据业务场景灵活选择。详细接入流程请参见 [Realtime API](/zh/model-studio/realtime-api-overview)。
</Note>

<Tabs>
  <Tab title="AOQ">
    - [AOQ 接入](/zh/model-studio/realtime-aoq-access)
  </Tab>

  <Tab title="WebSocket">
    通过以下 SDK 文档了解 WebSocket 接入方法及调用示例。

    SDK 接入：

    - [Python SDK](/zh/model-studio/qwen-livetranslate-python-sdk)
    - [Java SDK](/zh/model-studio/qwen-livetranslate-java-sdk)
  </Tab>

  <Tab title="WebRTC">
    - [WebRTC 接入](/zh/model-studio/realtime-webrtc-access)
  </Tab>
</Tabs>

## 事件参考

模型参数和事件字段的详细定义请参见：

- [客户端事件](/zh/model-studio/live-translator-client-events)
- [服务端事件](/zh/model-studio/live-translator-server-events)
