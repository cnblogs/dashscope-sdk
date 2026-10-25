> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Realtime API 概述

> Realtime API 提供多种传输协议，针对性能、延迟、弱网对抗、接入成本等不同需求进行优化，供开发者灵活选择。

<meta property="og:title" content="Realtime API 概述" />

## 协议选型 <span id="rtov-protocol-selection" />

Realtime API 支持 <strong>AOQ（AI over QUIC）</strong>、<strong>WebRTC</strong> 和 <strong>WebSocket</strong> 三种传输协议，开发者可以根据业务场景灵活选择。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "25%" }} />

    <col style={{ width: "25%" }} />

    <col style={{ width: "25%" }} />

    <col style={{ width: "25%" }} />
  </colgroup>

  <thead>
    <tr>
      <th style={{ verticalAlign: "top" }}>
        <strong>维度</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>AOQ</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>WebRTC</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>WebSocket</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        适用场景
      </td>

      <td style={{ verticalAlign: "top" }}>
        AI 多模态实时交互、弱网场景、混合数据传输
      </td>

      <td style={{ verticalAlign: "top" }}>
        浏览器端互动、传统音视频通话
      </td>

      <td style={{ verticalAlign: "top" }}>
        服务端集成、快速原型验证
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        浏览器兼容性
      </td>

      <td style={{ verticalAlign: "top" }}>
        不支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        原生支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        原生支持
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        接入难度
      </td>

      <td style={{ verticalAlign: "top" }}>
        低
      </td>

      <td style={{ verticalAlign: "top" }}>
        中等
      </td>

      <td style={{ verticalAlign: "top" }}>
        极低
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        弱网对抗
      </td>

      <td style={{ verticalAlign: "top" }}>
        极致
      </td>

      <td style={{ verticalAlign: "top" }}>
        良好
      </td>

      <td style={{ verticalAlign: "top" }}>
        差
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        数据类型
      </td>

      <td style={{ verticalAlign: "top" }}>
        音视频 + 文本
      </td>

      <td style={{ verticalAlign: "top" }}>
        音视频 + 文本
      </td>

      <td style={{ verticalAlign: "top" }}>
        文本/音频/图像
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        建连速度
      </td>

      <td style={{ verticalAlign: "top" }}>
        快
      </td>

      <td style={{ verticalAlign: "top" }}>
        慢
      </td>

      <td style={{ verticalAlign: "top" }}>
        慢
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        回声消除/降噪
      </td>

      <td style={{ verticalAlign: "top" }}>
        内置
      </td>

      <td style={{ verticalAlign: "top" }}>
        内置
      </td>

      <td style={{ verticalAlign: "top" }}>
        无，需客户端自行处理
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        AI 场景适配
      </td>

      <td style={{ verticalAlign: "top" }}>
        原生为 AI 多模态数据特征深度定制
      </td>

      <td style={{ verticalAlign: "top" }}>
        传统设计，AI 场景需额外适配
      </td>

      <td style={{ verticalAlign: "top" }}>
        基础，适合纯文本或低实时性场景
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        端侧平台支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        Android / iOS / HarmonyOS
      </td>

      <td style={{ verticalAlign: "top" }}>
        浏览器、移动端
      </td>

      <td style={{ verticalAlign: "top" }}>
        全平台（任何支持 WebSocket 的环境）
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        Token Plan
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>
    </tr>
  </tbody>
</table>

开发者可根据实际需求选择协议方案：

- <strong>AOQ 方案</strong>：适合对延迟、弱网对抗、多模态数据传输有极致要求的 AI 实时交互场景，同时内置回声消除和降噪能力，尤其是移动端原生应用。
- <strong>WebRTC 方案</strong>：适合需要浏览器原生支持、已有 WebRTC 基础设施的传统音视频通话场景，内置回声消除和降噪能力。
- <strong>WebSocket 方案</strong>：适合服务端集成、快速原型验证、对接入门槛要求极低的场景。通过 DashScope SDK 可快速实现实时语音对话。

## 模型/应用支持力度 <span id="rtov-s02h2" />

不同协议对模型和应用的支持情况如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "15%" }} />

    <col style={{ width: "25%" }} />

    <col style={{ width: "20%" }} />

    <col style={{ width: "20%" }} />

    <col style={{ width: "20%" }} />
  </colgroup>

  <thead>
    <tr>
      <th style={{ verticalAlign: "top" }}>
        <strong>模型/应用类型</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>模型</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>AOQ</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>WebRTC</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>WebSocket</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        [实时全模态](/zh/model-studio/realtime)
      </td>

      <td style={{ verticalAlign: "top" }}>
        qwen3.8-omni-flash-realtime、qwen3.5-omni-plus-realtime、qwen3.5-omni-flash-realtime
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>
    </tr>

    <tr>
      <td rowSpan={2} style={{ verticalAlign: "top" }}>
        [实时语音合成](/zh/model-studio/realtime-tts-user-guide)
      </td>

      <td style={{ verticalAlign: "top" }}>
        qwen-audio-3.1-tts-plus、qwen-audio-3.1-tts-flash、qwen-audio-3.0-tts-flash、qwen-audio-3.0-tts-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        不支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        CosyVoice系列模型
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        不支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        [实时语音识别](/zh/model-studio/real-time-speech-recognition-user-guide)
      </td>

      <td style={{ verticalAlign: "top" }}>
        qwen-audio-3.1-asr-flash-message、qwen-audio-3.1-asr-flash-streaming、qwen-audio-3.0-asr-flash-streaming、Fun-ASR-Realtime系列模型
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        不支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        [实时语音对话](https://help.aliyun.com/zh/model-studio/fun-audiochat-realtime)
      </td>

      <td style={{ verticalAlign: "top" }}>
        qwen-audio-3.1-realtime-plus、qwen-audio-3.0-realtime-plus、qwen-audio-3.0-realtime-flash
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        [实时语音翻译](/zh/model-studio/qwen3-5-livetranslate-flash-realtime)
      </td>

      <td style={{ verticalAlign: "top" }}>
        Qwen-Livetranslate-Realtime 系列模型
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        [多模态开发套件](/zh/model-studio/multimodal-products-overview)
      </td>

      <td style={{ verticalAlign: "top" }}>
        multimodal-dialog
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持
      </td>
    </tr>
  </tbody>
</table>

<Note>
  模型的名称、上下文、价格、快照版本等信息请参见[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/)；并发限流条件请参考[限流](/zh/model-studio/rate-limit)。
</Note>

## 最佳实践 <span id="0a7555f0aasb3" />

- [通过WebRTC使用qwen3.8-omni-flash-realtime实现实时通话](/zh/model-studio/best-practice-webrtc-omni-realtime)
- [通过AOQ使用qwen3.8-omni-flash-realtime实现实时通话](/zh/model-studio/best-practice-aoq-omni-realtime)
- [使用 AOQ 接入 qwen3.8-omni-flash-realtime 实现按键语音对话](/zh/model-studio/use-aoq-to-access-qwen3-5-omni-plus-realtime-to-realize-key-voice-dialogue)
- [使用 AOQ 接入 qwen-audio-3.0-tts-flash 实现语音合成](/zh/model-studio/speech-synthesis-using-aoq-access-qwen-audio-3-0-tts-flash)
- [使用 AOQ 接入 fun-asr-realtime 实现实时语音识别](/zh/model-studio/real-time-speech-recognition-using-aoq-access-fun-asr-realtime)
- [使用 AOQ 接入 qwen-audio-3.1-realtime-plus 实现实时语音对话](/zh/model-studio/real-time-voice-conversation-using-aoq-access-qwen-audio-3-0-realtime-plus)
- [通过WebRTC使用多模态交互套件实现实时通话](/zh/model-studio/best-practice-webrtc-multimodal-dialog)
