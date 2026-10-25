> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# CosyVoice 模型接入方式

> 介绍 CosyVoice 的接入入口、SDK 和事件参考。

<meta property="og:title" content="CosyVoice 模型接入方式" />

## 接入方式

<Note>
  CosyVoice 系列模型支持 AOQ（AI over QUIC）、WebSocket 两种传输协议，开发者可以根据业务场景灵活选择。详细接入流程请参见 [Realtime API](/zh/model-studio/realtime-api-overview)。
</Note>

<Tabs>
  <Tab title="AOQ">
    - [AOQ 接入](/zh/model-studio/realtime-aoq-access)
  </Tab>

  <Tab title="WebSocket">
    通过 [WebSocket 接入指南](/zh/model-studio/cosyvoice-websocket-api)了解连接地址、鉴权及交互流程。

    SDK 接入：

    - [Java SDK](/zh/model-studio/cosyvoice-java-sdk)
    - [Python SDK](/zh/model-studio/cosyvoice-python-sdk)
    - [Android SDK](/zh/model-studio/cosyvoice-android-sdk)
    - [iOS SDK](/zh/model-studio/cosyvoice-ios-sdk)
    - [HarmonyOS SDK](/zh/model-studio/cosyvoice-harmonyos-sdk)
  </Tab>
</Tabs>

## 事件参考

模型参数和事件字段的详细定义请参见：

- [客户端事件](/zh/model-studio/cosyvoice-client-events)
- [服务端事件](/zh/model-studio/cosyvoice-server-events)
