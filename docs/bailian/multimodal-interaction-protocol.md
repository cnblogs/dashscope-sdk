> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 接入指南

> 本文介绍基于 WebSocket 协议的实时多模态交互 API。WebSocket协议延迟低、资源占用少，是首选接入方案。

<meta property="og:title" content="接入指南" />

WebSocket是一种支持全双工通信的网络协议。客户端和服务器通过一次握手建立持久连接，双方可以互相主动推送数据，因此在实时性和效率方面具有显著优势。

对于常用编程语言，有许多现成的WebSocket库和示例可供参考，例如：

- Go：`gorilla/websocket`
- PHP：`Ratchet`
- Node.js：`ws`

建议您先了解WebSocket的基本原理和技术细节，再参照本文进行开发。

<Note>
  对于RTOS或某些旧版Linux系统，建立安全Websocket 通信所依赖的 TLS 隧道，需要做如下配置：

  - TLS 版本要求 TLS1.2 或以上
  - 开启 SNI（SERVER NAME INDICATION）
  - 配置 CA 证书（[GlobalSign Root CA - R3](https://secure.globalsign.net/cacert/Root-R3.crt)）（也可至GlobalSign官网下载）
</Note>

## 前提条件 <span id="39976381bblbp" />

已开通服务并[获取与配置 API Key](/zh/model-studio/get-api-key)。请[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)，而非硬编码在代码中，防范因代码泄露导致的安全风险。

<Note>
  对于客户端调用的场景，在客户端处理API Key有安全风险，建议从服务端用API Key获取临时鉴权Token，再把Token下发给客户端使用。具体方法请参考：[生成临时API Key](/zh/model-studio/generate-temporary-api-key)。
</Note>

## 调用时序图 <span id="6d25d6f8ddbtd" />

<strong>\[关键流程]</strong> 服务端返回`Started`消息表示会话创建成功，但客户端<strong>禁止</strong>立即发送音频。客户端必须等待并接收到`DialogStateChanged`事件且`state`为`Listening`后，方可开始发送音频流。

![image](https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8963248471/p958632.png)

## 服务地址 <span id="aded0e18ae7we" />

```plaintext
wss://dashscope.aliyuncs.com/api-ws/v1/inference
```

## 鉴权 <span id="f33e29403a9i8" />

需要在发起初始的WebSocket握手（HTTP Upgrade）请求时，把API Key放在HTTP Header里（需要将your\_api\_key替换为真实的API Key）：

```plaintext
"Authorization": "Bearer your_api_key"
```

## 语音交互 <span id="34b63cd8b0qvr" />

多模态交互应用开启了<strong>语音交互</strong>后，支持语音识别和语音合成。

语音识别支持的模型包括：[Paraformer实时语音识别](/zh/model-studio/paraformer-real-time-speech-recognition-api-reference)（Paraformer），[FUN-ASR实时语音识别](/zh/model-studio/fun-asr-real-time-speech-recognition-api-reference)（FunASR），[Qwen-Audio-3.0-ASR-Flash-Streaming](/zh/model-studio/fun-asr-realtime-java-sdk)，[千问3-ASR-Flash-Realtime](/zh/model-studio/real-time-speech-recognition-user-guide)（qwen3-asr-flash-realtime），多模态交互轻量版语音识别（AppSpecificASR-Realtime）。

语音合成支持的模型包括：[语音合成CosyVoice-v2大模型](/zh/model-studio/cosyvoice-large-model-for-speech-synthesis)（cosyvoice-v2），[语音合成CosyVoice-v3-Flash大模型](/zh/model-studio/text-to-speech)（cosyvoice-v3-flash），[语音合成CosyVoice-v3-plus大模型](/zh/model-studio/text-to-speech)（cosyvoice-v3-plus），[语音合成CosyVoice-v3.5-Flash大模型](/zh/model-studio/text-to-speech)（cosyvoice-v3.5-flash），[语音合成CosyVoice-v3.5-Plus大模型](/zh/model-studio/text-to-speech)（cosyvoice-v3.5-plus），[Qwen-Audio-3.0-TTS-Plus](/zh/model-studio/realtime-tts-user-guide)（qwen-audio-3.0-tts-plus）、[Qwen-Audio-3.0-TTS-Flash](/zh/model-studio/realtime-tts-user-guide)（qwen-audio-3.0-tts-flash），[千问3-TTS-Flash-Realtime](/zh/model-studio/realtime-tts-user-guide)（qwen3-tts），[千问3-TTS-Instruct-Flash-Realtime](/zh/model-studio/realtime-tts-user-guide)（qwen3-tts-instruct），[千问3-声音设计](/zh/model-studio/qwen-tts-voice-design)（qwen3-tts-vd），[千问3-声音复刻](/zh/model-studio/qwen-tts-voice-cloning)（qwen3-tts-vc），[Sambert语音合成](/zh/model-studio/sambert-speech-synthesis)（sambert），多模态交互轻量版语音合成（AppSpecificTTS）。

语音合成支持的音色，可以在控制台上选择了模型后，点击右侧语音交互体验区域的右上角查看音色列表。

官方音色也可以参考官方文档：[支持的音色](/zh/model-studio/realtime-tts-user-guide#bac280ddf5a1u)，sambert支持的音色参考[Sambert音色列表](/zh/model-studio/sambert-java-sdk)（去掉开头的"sambert-"和末尾的"-v1"后就是voice的取值）。

使用复刻音色时，确认复刻音色状态为"OK"后才能使用。查询方法参考[查询特定音色](/zh/model-studio/cosyvoice-clone-design-api)。

## 消息类型 <span id="e3a7c0fbdfzuh" />

### 二进制消息（Binary Message） <span id="ef6ec1325br4c" />

当前二进制消息仅包含音频数据。

#### 上传音频 <span id="78981c8e11kef" />

上传音频时，将原始音频直接转为二进制流即可，无需额外处理。

上传的语音识别音频需满足：16bit（采样位深）、单声道、有符号、little-endian PCM编码，采样率参考[Start](/zh/model-studio/multimodal-interaction-protocol#5b2363d40585y)消息的参数parameters.upstream.sample\_rate的取值说明。

如果希望减少网络流量和带宽占用，用户可以把PCM音频编码为Opus格式，同时设置上传音频格式为raw-opus。

上传音频时，根据[Start](/zh/model-studio/multimodal-interaction-protocol#5b2363d40585y)消息中`upstream.mode`设置不同，采取的措施也不同：

- mode为 `tap2talk` 或 `duplex`：客户端需持续上传音频，服务端自动检测语音活动。建议每100ms上传一次数据，间隔太长或太短会对延时和处理效率造成负面影响。

  - 音频上传速率计算公式：`数据每次上传的字节数 = 采样率 * 采样位深/8 * 时间间隔（ms）/ 1000`
  - 以16kHz采样率、16bit位深、100ms间隔为例，每次应上传 `16000 * (16/8) * 100 / 1000 = 3200` 字节的PCM数据。
- mode为 `push2talk`：客户端无需持续上传音频，但需通过[SendSpeech](/zh/model-studio/multimodal-interaction-protocol#cec23c1556u8x)和[StopSpeech](/zh/model-studio/multimodal-interaction-protocol#d11933e7b5jz0)通知服务端音频识别的开始和结束。发送[SendSpeech](/zh/model-studio/multimodal-interaction-protocol#cec23c1556u8x)后需立即上传音频，否则会增加处理时间。

#### 下发音频 <span id="f7aa2ba72cj1n" />

服务端将大模型回复发送至TTS生成语音然后下发给客户端：

- 下发音频为16bit单声道，采样率和编码由[Start](/zh/model-studio/multimodal-interaction-protocol#5b2363d40585y)消息参数定义。
- 下发速度取决于TTS服务性能，通常快于播放速度。
- 音频下发前发送[RespondingStarted](/zh/model-studio/multimodal-interaction-protocol#0693e9d4a5f91)事件；结束后发送[RespondingEnded](/zh/model-studio/multimodal-interaction-protocol#402cbee340r1m)事件。
- 客户端需在播放完成后上报[LocalRespondingEnded](/zh/model-studio/multimodal-interaction-protocol#f4b1a6bf22ymq)，通知服务端播放结束。

### 文本消息（Text Message） <span id="48b2452228o2z" />

文本消息是JSON格式字符串，按传递方向分为两类：

- <strong>输入消息（Input Message）</strong>：客户端发送给服务端的指令（directive），表示客户端希望服务端执行特定动作。
- <strong>输出消息（Output Message）</strong>：服务端发送给客户端的事件（event），表示服务端动作执行结果或进展。

文本消息标记交互流程的关键节点，控制流程并传输关键信息。通过[时序图](/zh/model-studio/multimodal-interaction-protocol#6d25d6f8ddbtd)可了解不同消息的交互时序。

文本消息包含两部分：`header`和`payload`。

- `payload`：内容随[消息类型](/zh/model-studio/multimodal-interaction-protocol#408ddb268eyt7)变化。
- `header`：内容固定，包含以下参数：

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.75%" }} /><col style={{ width: "11.91%" }} /><col style={{ width: "15.26%" }} /><col style={{ width: "56.08%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>task\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>本次连接唯一标识，用于在工程链路上跟踪任务执行。由客户端生成，格式建议为36位uuid字符串，格式示例："f894c16f-f20e-4c1d-837e-89e0fbc63a43"</p></td></tr><tr><td><p>streaming</p></td><td><p>string</p></td><td><p>是</p></td><td><p>输入输出类型，对于多模态交互必须为 "duplex" ，表示流式输入，流式输出</p></td></tr><tr><td><p>action</p></td><td><p>string</p></td><td><p>是</p></td><td><p>模型输入消息类型：</p><ul><li><p>run-task: 任务的第一个输入消息</p></li><li><p>finish-task: 任务的最后一个输入消息</p></li><li><p>continue-task: 任务中其他输入消息</p></li></ul></td></tr></tbody></table>

## 连接保活策略 <span id="8e82725ed5vag" />

百炼平台规定，如果在<strong>任意连续的60秒内</strong>服务端没有向客户端发送任何消息，则认为调用发生错误，WebSocket连接将被服务端主动断开并返回`ResponseTimeout`错误。

若客户端需在无交互时保持连接，应定期发送心跳消息（HeartBeat）。服务端会回应心跳，确保连接活跃，避免超时关闭。

移动端和C++ SDK已内置心跳保活逻辑，用户无需手动发送。

## 文本消息类型 <span id="408ddb268eyt7" />

### 开始会话 <span id="92d1f462e5xrm" />

#### Start - Input Message <span id="5b2363d40585y" />

请求开始会话消息。服务收到Start消息后，向客户端发送<strong>Started</strong>消息。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.45%" }} /><col style={{ width: "15.18%" }} /><col style={{ width: "11.98%" }} /><col style={{ width: "11.19%" }} /><col style={{ width: "48.2%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>task\_group</p></td><td /><td><p>string</p></td><td><p>是</p></td><td><p>任务组名称，固定为"aigc"，请直接复制使用</p></td></tr><tr><td><p>task</p></td><td /><td><p>string</p></td><td><p>是</p></td><td><p>任务名称，固定为"multimodal-generation"，请直接复制使用</p></td></tr><tr><td><p>function</p></td><td /><td><p>string</p></td><td><p>是</p></td><td><p>调用功能，固定为"generation"，请直接复制使用</p></td></tr><tr><td><p>model</p></td><td /><td><p>string</p></td><td><p>是</p></td><td><p>阿里云百炼模型名称，固定为"multimodal-dialog"，请直接复制使用</p></td></tr><tr><td rowSpan={4}><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：Start</p></td></tr><tr><td><p>workspace\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>客户在阿里云百炼业务空间ID（<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>），可在<strong>多模态交互开发套件控制台</strong>，点击左下角业务空间名称，"业务空间详情"中查看，目前仅支持主账号默认业务空间。</p></td></tr><tr><td><p>app\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>客户创建的应用ID（<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#2612f896detsz">APP ID</a>），可在<strong>多模态交互开发套件</strong>控制台的"我的应用"页面查看。</p></td></tr><tr><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>对话ID，默认不填时是开启新会话，服务端会自动生成并在事件中下发，格式示例："12345678-1234-1234-1234-1234567890ab"，共36个字符。当希望继续之前的对话时，把当时服务端下发的dialog\_id在这里传入</p></td></tr><tr><td rowSpan={4}><p>parameters</p></td><td><p>upstream</p></td><td><p>object</p></td><td><p>是</p></td><td><p>参数说明参考下方 <a href="/zh/model-studio/multimodal-interaction-protocol">parameters.upstream的参数说明</a>表格</p></td></tr><tr><td><p>downstream</p></td><td><p>object</p></td><td><p>否</p></td><td><p>参数说明参考下方 <a href="/zh/model-studio/multimodal-interaction-protocol">parameters.downstream的参数说明</a>表格</p></td></tr><tr><td><p>client\_info</p></td><td><p>object</p></td><td><p>是</p></td><td><p>参数说明参考下方 <a href="/zh/model-studio/multimodal-interaction-protocol">parameters.client\_info的参数说明</a>表格</p></td></tr><tr><td><p>biz\_params</p></td><td><p>object</p></td><td><p>否</p></td><td><p>参数说明参考下方 <a href="/zh/model-studio/multimodal-interaction-protocol">parameters.biz\_params的参数说明</a>表格</p></td></tr></tbody></table>

<strong>parameters.upstream</strong>的参数说明如下<strong>：</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.13%" }} /><col style={{ width: "9.29%" }} /><col style={{ width: "8.36%" }} /><col style={{ width: "73.22%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>是</p></td><td><p>上行类型：</p><p>AudioOnly 仅语音通话</p></td></tr><tr><td><p>mode</p></td><td><p>string</p></td><td><p>否</p></td><td><p>客户端使用的模式，默认<strong>tap2talk。</strong></p><p>可选项：</p><ul><li><p>push2talk: 客户端控制模式。</p></li><li><p>tap2talk: 点击模式。</p></li><li><p>duplex: 双工模式。</p></li></ul><p>三种模式的对比可以参考下方的<a href="/zh/model-studio/multimodal-interaction-protocol#5e82453407pum">客户端使用的三种模式对比</a>表格。</p></td></tr><tr><td><p>audio\_format</p></td><td><p>string</p></td><td><p>否</p></td><td><p>音频格式，支持pcm，raw-opus，默认为pcm</p></td></tr><tr><td><p>sample\_rate</p></td><td><p>int</p></td><td><p>否</p></td><td><p>语音识别的采样率，支持范围：</p><ul><li><p>8000</p></li><li><p>16000</p></li><li><p>24000</p></li><li><p>48000</p></li></ul><p>默认为16000</p></td></tr><tr><td><p>vocabulary\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>热词id，设置该参数时会覆盖管控台热词配置。当管控台提供的热词不能满足客户需求时，可以考虑用Open API程序化管理热词，参见<a href="https://next.api.aliyun.com/document/MultimodalDialog/2025-09-03/Vocabulary">热词API文档</a>。</p></td></tr><tr><td><p>language</p></td><td><p>string</p></td><td><p>否</p></td><td><p>语音识别语种，默认和控制台选择的语言保持一致。设置多个语种时，使用英文逗号分隔，例如：<code>zh,en</code>。</p></td></tr></tbody></table>

<strong>parameters.downstream</strong>的参数说明如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.13%" }} /><col style={{ width: "9.29%" }} /><col style={{ width: "8.36%" }} /><col style={{ width: "73.22%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>voice</p></td><td><p>string</p></td><td><p>否</p></td><td><p>合成语音的音色，支持范围取决于用户在管控台选择的语音合成模型</p></td></tr><tr><td><p>sample\_rate</p></td><td><p>int</p></td><td><p>否</p></td><td><p>合成语音的采样率，支持范围：</p><ul><li><p>8000</p></li><li><p>16000</p></li><li><p>24000</p></li><li><p>48000</p></li></ul><p>默认为24000。</p><p>千问-TTS、千问3-TTS模型仅支持24000。</p></td></tr><tr><td><p>audio\_format</p></td><td><p>string</p></td><td><p>否</p></td><td><p>音频格式，支持pcm，opus，mp3，raw-opus，默认为pcm。</p><p>千问-TTS模型仅支持pcm。</p><p>注意：opus 和 raw-opus的区别是opus格式的每一包数据都有额外ogg封装（<a href="https://www.rfc-editor.org/rfc/rfc7845.html">RFC 7845</a>）</p></td></tr><tr><td><p>frame\_size</p></td><td><p>int</p></td><td><p>否</p></td><td><p>合成音频的帧大小，取值范围：</p><ul><li><p>10</p></li><li><p>20</p></li><li><p>40</p></li><li><p>60</p></li><li><p>100</p></li><li><p>120</p></li></ul><p>默认值为60，单位ms</p><p>只在合成音频格式为opus或raw-opus时生效</p></td></tr><tr><td><p>volume</p></td><td><p>int</p></td><td><p>否</p></td><td><p>合成音频的音量，取值范围0-100，默认50</p></td></tr><tr><td><p>speech\_rate</p></td><td><p>int</p></td><td><p>否</p></td><td><p>合成音频的语速，取值范围50-200，表示默认语速的50%-200%，默认100</p></td></tr><tr><td><p>pitch\_rate</p></td><td><p>int</p></td><td><p>否</p></td><td><p>合成音频的声调，取值范围50-200，默认100</p></td></tr><tr><td><p>bit\_rate</p></td><td><p>int</p></td><td><p>否</p></td><td><p>合成音频的比特率，取值范围：6\~510kbps，默认值为32，单位kbps，只在合成音频格式为mp3, opus或raw-opus时生效</p></td></tr><tr><td><p>intermediate\_text</p></td><td><p>string</p></td><td><p>否</p></td><td><p>控制返回给用户哪些中间文本：</p><ul><li><p>transcript：返回用户语音识别结果</p></li><li><p>dialog：返回对话系统回答中间结果</p></li></ul><p>可以设置多种，以逗号分隔，默认为transcript</p></td></tr><tr><td><p>word\_timestamp\_enabled</p></td><td><p>boolean</p></td><td><p>否</p></td><td><p>是否下发tts合成音频对应的时间戳。如果设置为true，会在RespondingContent.extra\_info下返回时间戳信息，用于客户端显示字幕等，默认为false。</p><p>必须在intermediate\_text有指定dialog的情况下才会返回；</p><p>只有<a href="/zh/model-studio/cosyvoice-voice-list">CosyVoice音色列表</a>中表明支持时间戳的音色和复刻音色才会返回。</p></td></tr><tr><td><p>transmit\_rate\_limit</p></td><td><p>int</p></td><td><p>否</p></td><td><p>下发音频发送速率限制，单位：字节每秒</p></td></tr><tr><td><p>incremental\_response</p></td><td><p>boolean</p></td><td><p>否</p></td><td><p>是否增量返回大模型结果，true为增量，false为全量。默认为false，全量下发</p></td></tr><tr><td><p>instruction</p></td><td><p>string</p></td><td><p>否</p></td><td><p>设置指令，用于控制方言、情感等合成效果。该功能适用的模型以及在不同模型的格式要求请参见<a href="/zh/model-studio/realtime-tts-user-guide#12884a10929p9">指令控制</a>。</p></td></tr><tr><td><p>language</p></td><td><p>string</p></td><td><p>否</p></td><td><p>语音合成语种，默认和控制台选择的语言保持一致。</p></td></tr></tbody></table>

<strong>parameters.client\_info</strong>的参数说明如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "8.36%" }} /><col style={{ width: "8.67%" }} /><col style={{ width: "9.91%" }} /><col style={{ width: "8.2%" }} /><col style={{ width: "64.86%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>user\_id</p></td><td /><td><p>string</p></td><td><p>是</p></td><td><p>终端用户ID，客户根据自己业务规则生成，用来针对不同终端用户实现定制化功能。最大长度36个字符。</p></td></tr><tr><td><p>device</p></td><td><p>uuid</p></td><td><p>string</p></td><td><p>否</p></td><td><p>客户端全局唯一的ID，需要用户自己生成并传入SDK，最大长度40个字符。一个终端用户可以有多个设备，那么每一个设备的uuid都不同，但user\_id相同。</p></td></tr><tr><td><p>network</p></td><td><p>ip</p></td><td><p>string</p></td><td><p>否</p></td><td><p>调用方公网IP</p></td></tr><tr><td rowSpan={3}><p>location</p></td><td><p>latitude</p></td><td><p>string</p></td><td><p>否</p></td><td><p>调用方纬度信息，在需要客户端精确位置的业务场景提交</p></td></tr><tr><td><p>longitude</p></td><td><p>string</p></td><td><p>否</p></td><td><p>调用方经度信息，在需要客户端精确位置的业务场景提交</p></td></tr><tr><td><p>city\_name</p></td><td><p>string</p></td><td><p>否</p></td><td><p>调用方所在城市，指明客户端粗略位置</p></td></tr></tbody></table>

<strong>parameters.biz\_params</strong>的参数说明如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "11.46%" }} /><col style={{ width: "9.6%" }} /><col style={{ width: "9.29%" }} /><col style={{ width: "69.65%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>user\_defined\_params</p></td><td><p>json object</p></td><td><p>否</p></td><td><p>设置需要透传给agent 和 mcp 服务的参数，各类agent传递的参数参考<a href="/zh/model-studio/official-agent">调用官方Agent</a>文档说明， mcp 传递参数依照 mcp 服务本身所需的参数。可以在extra\_config子节点中设置对话扩展参数，目前支持：1. enable\_web\_search，表示是否开启联网搜索。这里的设置优先级更高，会覆盖管控台配置；2. agent\_timeout，表示agent连接超时的时间，传值范围是10秒到120秒。如果不传，则默认为10秒。</p></td></tr><tr><td><p>user\_prompt\_params</p></td><td><p>json object</p></td><td><p>否</p></td><td><p>用于设置用户自定义prompt变量，由用户自定义设置json中的key和value。管控台上配置自定义prompt变量的方法参考<a href="/zh/model-studio/multimodal-app-configuration#cbbc5f73caixw">应用配置-提示词</a></p></td></tr><tr><td><p>user\_query\_params</p></td><td><p>json object</p></td><td><p>否</p></td><td><p>用于设置用户自定义对话变量，由用户自定义设置json中的key和value。管控台上配置自定义对话变量的方法参考<a href="/zh/model-studio/multimodal-app-configuration#6abc9330e2yx9">应用配置-对话变量</a></p></td></tr></tbody></table>

##### 客户端使用的三种模式对比： <span id="5e82453407pum" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.76%" }} /><col style={{ width: "26.16%" }} /><col style={{ width: "26.16%" }} /><col style={{ width: "24.92%" }} /></colgroup><thead><tr><th><p><strong>对比项</strong></p></th><th><p><strong>push2talk</strong></p></th><th><p><strong>tap2talk</strong></p></th><th><p><strong>duplex</strong></p></th></tr></thead><tbody><tr><td><p>类型</p></td><td><p>客户端控制模式</p></td><td><p>点击模式</p></td><td><p>双工模式</p></td></tr><tr><td><p>音频上传方式</p></td><td><p>按需</p></td><td><p>持续</p><p>Listening状态超过20秒不上传音频即报错</p></td><td><p>持续</p><p>任何状态超过20秒不上传音频都报错</p></td></tr><tr><td><p>VAD检测方</p></td><td><p>客户端</p></td><td><p>服务端</p></td><td><p>服务端</p></td></tr><tr><td><p>打断方式</p></td><td><p><a href="/zh/model-studio/multimodal-interaction-protocol#96541eff55v86">RequestToSpeak</a>消息打断</p></td><td><p><a href="/zh/model-studio/multimodal-interaction-protocol#96541eff55v86">RequestToSpeak</a>消息打断</p></td><td><p>语音打断</p></td></tr><tr><td><p>使用场景</p></td><td><p>由用户控制开始/结束客户端语音发送和识别，适用于按键说话，松开停止说话的场景。</p></td><td><p>客户端需持续上传音频，服务端自动检测语音活动的场景。但不支持用户语音打断大模型输出，只能发送RequestToSpeak打断消息。</p></td><td><p>客户端需持续上传音频，服务端自动检测语音活动的场景。用户随时可以说话打断大模型输出。</p></td></tr></tbody></table>

示例如下：

```json expandable
{
    "header": {
        "action":"run-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "task_group":"aigc",
        "task":"multimodal-generation", // 任务类型：多模态生成
        "function":"generation",
        "model":"multimodal-dialog", // 模型名称：多模态对话（注意与task不同）
        "input":{
          "directive": "Start",
          "workspace_id": "llm-***********",
          "app_id": "****************"
        },
        "parameters":{
          "upstream":{
            "type": "AudioOnly",
            "mode": "duplex"
          },
          "downstream":{
            "voice": "longxiaochun_v2",
            "sample_rate": 24000
          },
          "client_info":{
            "user_id": "bin********207",
            "device":{
              "uuid": "432k*********k449"
            },
            "network":{
              "ip": "203.0.113.10"
            },
            "location":{
              "city_name": "北京市"
            }
          },
          "biz_params":{
            "user_defined_params": {
                "extra_config": {
                    "enable_web_search": false,
                    "agent_timeout": 10
                },
                "agent_id_xxxxx": {
                    "name": "value"
                },
                "mcp_server_id_xxxxx":{
                    "name": "value"
                }
            },
            "user_prompt_params": {
                "name": "value"
            },
            "user_query_params": {
                "name": "value"
            }
          }
        }
    }
}
```

#### Started - Output Message <span id="06ba46fd89wqt" />

<Note>
  SDK接收到<strong>Started</strong>之后，先不可以向服务发送音频，应等待<strong>DialogStateChanged</strong>消息，确认切换到Listening状态之后才发送音频。
</Note>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.26%" }} /><col style={{ width: "9.58%" }} /><col style={{ width: "8.07%" }} /><col style={{ width: "73.09%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>事件名称：Started</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

Started返回消息样例

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "Started",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

### 结束会话 <span id="3a4e21be9563m" />

#### Stop - Input Message <span id="ed70d4193bihk" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "8.83%" }} /><col style={{ width: "9.69%" }} /><col style={{ width: "7.97%" }} /><col style={{ width: "10.12%" }} /><col style={{ width: "63.39%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：Stop</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

Stop请求样例

```json
{
    "header": {
        "action":"finish-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "input":{
          "directive": "Stop",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

#### Stopped - Output Message <span id="4dda641432be2" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.47%" }} /><col style={{ width: "10.66%" }} /><col style={{ width: "7.21%" }} /><col style={{ width: "9.9%" }} /><col style={{ width: "62.76%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：Stopped</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "Stopped",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

### 服务端下发状态切换事件 <span id="8fc863293amcu" />

#### DialogStateChanged - Output Message <span id="7e67f7ec50f6z" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.15%" }} /><col style={{ width: "9.47%" }} /><col style={{ width: "7.97%" }} /><col style={{ width: "8.83%" }} /><col style={{ width: "64.58%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：DialogStateChanged</p></td></tr><tr><td /><td><p>state</p></td><td><p>string</p></td><td><p>是</p></td><td><p>AI交互状态，取值范围：Listening，Thinking， Responding</p><p>注意：其中状态Listening 只是表示SDK可以发语音给服务端，不代表客户端是否开麦</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "DialogStateChanged",
          "state": "Listening",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

### 请求上传语音 <span id="96541eff55v86" />

#### RequestToSpeak - Input Message <span id="edd2de91d9zcb" />

当前状态不是Listening，而用户又想说话时，先提交此事件打断大模型的回答，等待服务端应答。

具体触发此事件的用户行为根据交互类型有所不同，比如用户按下按钮、语音打断（依赖双工模块）等。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "8.72%" }} /><col style={{ width: "9.9%" }} /><col style={{ width: "7.53%" }} /><col style={{ width: "8.93%" }} /><col style={{ width: "64.92%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：RequestToSpeak</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "action":"continue-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "input":{
          "directive": "RequestToSpeak",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

#### RequestAccepted - Output Message 请求被准许 <span id="532ee1c9efeii" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.04%" }} /><col style={{ width: "10.01%" }} /><col style={{ width: "7.97%" }} /><col style={{ width: "9.04%" }} /><col style={{ width: "63.94%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：RequestAccepted</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "RequestAccepted",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

### 上传语音指令 <span id="52da7f14ec2ga" />

#### SendSpeech - Input Message <span id="cec23c1556u8x" />

当Start消息指定模式为push2talk，需要客户端告知服务端用户何时开始说话。在Listening状态下，当用户按下按键时应上报SendSpeech消息，通知服务端即将开始上传语音，语音数据应紧接着此事件之后发送。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.26%" }} /><col style={{ width: "8.5%" }} /><col style={{ width: "8.5%" }} /><col style={{ width: "9.04%" }} /><col style={{ width: "64.7%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：SendSpeech</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "action":"continue-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "input":{
          "directive": "SendSpeech",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

#### StopSpeech - Input Message <span id="d11933e7b5jz0" />

当Start消息指定模式为push2talk的时候，用户说完话松开按键时，客户端必须使用StopSpeech消息通知服务端结束语音指令输入。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "8.83%" }} /><col style={{ width: "10.44%" }} /><col style={{ width: "7.75%" }} /><col style={{ width: "9.15%" }} /><col style={{ width: "63.83%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：StopSpeech</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "action":"continue-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "input":{
          "directive": "StopSpeech",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

#### CancelSpeech - Input Message <span id="033cbf278074j" />

当Start消息指定模式为push2talk或tap2talk的时候，在语音输入过程中，用户可以使用CancelSpeech消息结束语音输入。服务会结束识别，回到空闲状态。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "8.83%" }} /><col style={{ width: "10.44%" }} /><col style={{ width: "7.75%" }} /><col style={{ width: "9.15%" }} /><col style={{ width: "63.83%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：CancelSpeech</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "action":"continue-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "input":{
          "directive": "CancelSpeech",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

### 语音识别开始/结束 <span id="d594e7d5853wk" />

#### SpeechStarted - Output Message <span id="4dad1fc3265ro" />

当服务端检测到asr语音起点时下发此事件。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.9%" }} /><col style={{ width: "9.69%" }} /><col style={{ width: "8.93%" }} /><col style={{ width: "9.26%" }} /><col style={{ width: "62.22%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：SpeechStarted</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "SpeechStarted",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

#### SpeechEnded - Output Message <span id="e5e95eb9a3uy2" />

当服务端检测到asr语音尾点时下发此事件，如果客户端还在上传音频，则收到此事件后应停止上传音频。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.58%" }} /><col style={{ width: "10.33%" }} /><col style={{ width: "7.97%" }} /><col style={{ width: "8.72%" }} /><col style={{ width: "63.4%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：SpeechEnded</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "SpeechEnded",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

### 指定应答内容 <span id="d8cb5ffb9aemh" />

#### RequestToRespond - Input Message <span id="ea3b4279e717m" />

在Listening状态时，通知服务端与用户主动交互，服务端会根据type字段把在text字段中上传的文本直接转换为语音下发，或调用大模型，返回的结果再转换为语音下发。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "11.17%" }} /><col style={{ width: "11%" }} /><col style={{ width: "8.94%" }} /><col style={{ width: "9.1%" }} /><col style={{ width: "59.79%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td rowSpan={4}><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：RequestToRespond</p></td></tr><tr><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr><tr><td><p>type</p></td><td><p>string</p></td><td><p>是</p></td><td><p>服务应该采取的交互类型，目前支持两种：</p><ul><li><p>transcript：表示直接把文本转语音</p></li><li><p>prompt：表示把文本发送给大模型让其进行回答</p></li></ul></td></tr><tr><td><p>text</p></td><td><p>string</p></td><td><p>是</p></td><td><p>要处理的文本，非null。</p><ul><li><p>调用部分agent时，text可以是""空字符串，服务端只需要使用parameters中的images或者biz\_params参数即可处理。具体参考<a href="/zh/model-studio/official-agent">调用官方Agent</a>。</p></li></ul></td></tr><tr><td rowSpan={3}><p>parameters</p></td><td><p>history</p></td><td><p>list\[]</p></td><td><p>否</p></td><td><p>客户自己组织的问答历史，会覆盖服务内默认维护的问答历史，影响后续所有问答。</p></td></tr><tr><td><p>images</p></td><td><p>list\[]</p></td><td><p>否</p></td><td><p>需要分析的图片信息</p></td></tr><tr><td><p>biz\_params</p></td><td><p>object</p></td><td><p>否</p></td><td><p>参数说明参考下方 <a href="/zh/model-studio/multimodal-interaction-protocol">parameters.biz\_params的参数说明</a>表格</p></td></tr></tbody></table>

<strong>parameters.history</strong>内每条记录的参数说明如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.04%" }} /><col style={{ width: "9.42%" }} /><col style={{ width: "12.03%" }} /><col style={{ width: "65.51%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>role</p></td><td><p>string</p></td><td><p>是</p></td><td><p>本记录是哪个角色说的，只支持user, assistant两种。</p><p>注意：不支持设为system</p></td></tr><tr><td><p>content</p></td><td><p>string</p></td><td><p>是</p></td><td><p>具体文本内容</p></td></tr></tbody></table>

<strong>parameters.images</strong>内每个图片的参数说明如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.04%" }} /><col style={{ width: "9.42%" }} /><col style={{ width: "12.03%" }} /><col style={{ width: "65.51%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>type</p></td><td><p>string</p></td><td><p>是</p></td><td><p>图片类型，支持两种：base64/url</p></td></tr><tr><td><p>value</p></td><td><p>string</p></td><td><p>是</p></td><td><p>图片内容。</p><ul><li><p>当type为base64时，这里是图片的base64字符串。</p></li><li><p>当type为url时，这里是图片的url地址。</p></li></ul></td></tr></tbody></table>

<strong>parameters.biz\_params</strong>的参数说明如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "11.45%" }} /><col style={{ width: "9.57%" }} /><col style={{ width: "9.28%" }} /><col style={{ width: "69.7%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>videos</p></td><td><p>list\[]</p></td><td><p>否</p></td><td><p>用来控制进入和退出视频通话。</p><p>示例如下：</p><p>payload.biz\_params.videos.action为connect，表示进入视频模式。</p><p>payload.biz\_params.videos.action为exit，表示退出视频模式。</p></td></tr><tr><td><p>其他参数</p></td><td /><td><p>否</p></td><td><p>与<a href="/zh/model-studio/multimodal-interaction-protocol#5b2363d40585y">Start</a>消息中<code>parameters.biz\_params</code>相同，传递对话系统自定义参数。RequestToRespond的<code>biz\_params</code>参数只在本次请求中生效。</p></td></tr></tbody></table>

<Note>
  除了videos参数外，`parameters.biz_params`与[Start](/zh/model-studio/multimodal-interaction-protocol#5b2363d40585y)消息中的`parameters.biz_params`相同，传递对话系统自定义参数。RequestToRespond的`biz_params`参数只在本次请求中生效。
</Note>

```json expandable
{
    "header": {
        "action":"continue-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "input":{
          "directive": "RequestToRespond",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb",
          "type": "prompt",
          "text": "你好，你有什么想聊的呢"
        },
        "parameters":{
          "images":[{
              "type": "base64",
              "value": "aGVsbG8gd29ybGQ="
          }],
          "biz_params":{
            "user_defined_params":{},
            "videos": [
                  {
                    "action": "connect/exit",
                    "type": "voicechat_video_channel"
                  }
                ]
          }
        }
    }
}
```

### AI语音应答状态 <span id="125b3f72ca808" />

#### RespondingStarted - Output Message <span id="0693e9d4a5f91" />

AI语音应答开始，sdk要准备接收服务端下发的语音数据

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "8.93%" }} /><col style={{ width: "10.87%" }} /><col style={{ width: "7.97%" }} /><col style={{ width: "8.5%" }} /><col style={{ width: "63.73%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：RespondingStarted</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "RespondingStarted",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

#### RespondingEnded - Output Message <span id="402cbee340r1m" />

AI语音应答结束

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "8.83%" }} /><col style={{ width: "10.33%" }} /><col style={{ width: "7.86%" }} /><col style={{ width: "9.69%" }} /><col style={{ width: "63.29%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：RespondingEnded</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "RespondingEnded",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

### 客户端播放事件 <span id="04875c3cf6yua" />

#### LocalRespondingStarted - Input Message <span id="0ebcb0b0714eh" />

客户端开始播放服务端下发的音频

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.47%" }} /><col style={{ width: "11.3%" }} /><col style={{ width: "9.04%" }} /><col style={{ width: "9.8%" }} /><col style={{ width: "60.39%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：LocalRespondingStarted</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "action":"continue-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "input":{
          "directive": "LocalRespondingStarted",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

#### LocalRespondingEnded - Input Message <span id="f4b1a6bf22ymq" />

客户端播放服务端下发的音频完成，服务端根据此消息判断端侧语音播放结束，结束当前问答，重新切换到Listening状态。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "8.93%" }} /><col style={{ width: "10.23%" }} /><col style={{ width: "9.04%" }} /><col style={{ width: "10.23%" }} /><col style={{ width: "61.57%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：LocalRespondingEnded</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>对话ID</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "action":"continue-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "input":{
          "directive": "LocalRespondingEnded",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

### 文本下发事件 <span id="588b9df38azvr" />

#### SpeechContent - Output Message <span id="07fb23a571ekv" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "11.19%" }} /><col style={{ width: "11.84%" }} /><col style={{ width: "9.9%" }} /><col style={{ width: "9.58%" }} /><col style={{ width: "57.49%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：SpeechContent</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr><tr><td /><td><p>text</p></td><td><p>string</p></td><td><p>是</p></td><td><p>用户语音识别出的文本，流式返回，每次返回从识别开始到当前的完整识别结果。例如，您会先收到<code>"text": "你好"</code>，接着收到<code>"text": "你好世界"</code>。</p></td></tr><tr><td /><td><p>finished</p></td><td><p>bool</p></td><td><p>是</p></td><td><p>输出是否结束</p></td></tr></tbody></table>

示例如下：

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "SpeechContent",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb",
          "text": "一二三",
          "finished": false
        }
    }
}
```

#### RespondingContent - Output Message <span id="3b421d978fco5" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "11.19%" }} /><col style={{ width: "14.75%" }} /><col style={{ width: "9.69%" }} /><col style={{ width: "10.76%" }} /><col style={{ width: "53.61%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：RespondingContent</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话ID</p></td></tr><tr><td /><td><p>round\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>本轮交互的ID</p></td></tr><tr><td /><td><p>llm\_request\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>调用llm的request\_id</p></td></tr><tr><td /><td><p>text</p></td><td><p>string</p></td><td><p>是</p></td><td><p>系统对外输出的文本，流式全量输出</p></td></tr><tr><td /><td><p>spoken</p></td><td><p>string</p></td><td><p>是</p></td><td><p>合成语音时使用的文本，流式全量输出</p></td></tr><tr><td /><td><p>finished</p></td><td><p>bool</p></td><td><p>是</p></td><td><p>输出是否结束</p></td></tr><tr><td /><td><p>extra\_info</p></td><td><p>object</p></td><td><p>否</p></td><td><p>其他扩展信息，目前支持：</p><ul><li><p>commands: 命令字符串，<strong>此字段为JSON字符串，需要进行二次解析。</strong>各类agent使用的命令字符串可以参考<a href="/zh/model-studio/official-agent">调用官方Agent</a>的说明。</p></li><li><p>agent\_info: 智能体信息</p></li><li><p>tool\_calls: 插件返回的信息</p></li><li><p>word\_timestamps: tts对应文本时间戳信息</p></li></ul><p>如果没有扩展信息要提交则省略此字段。</p></td></tr></tbody></table>

示例如下：

```json expandable
{
    "header": {
        "event": "result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output": {
            "event": "RespondingContent",
            "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb",
            "text": "您输入了数字序列"12345"。如果您有关于这些数字的问题或者需要我用它们来完成某项任务，请告诉我更多的细节，我会尽力帮助您。",
            "spoken": "您输入了数字序列"12345"。如果您有关于这些数字的问题或者需要我用它们来完成某项任务，请告诉我更多的细节，我会尽力帮助您。",
            "finished": true,
            "extra_info": {
                "commands": "[{\"name\":\"VOLUME_SET\",\"params\":[{\"name\":\"series\",\"normValue\":\"70\",\"value\":\"70\"}]}]",
                "tool_calls": [
                    {
                        "id": "",
                        "type": "function",
                        "function": {
                            "name": "function_name",
                            "arguments": "{\"id\": \"123\", \"name\": \"test\"}",
                            "outputs": "{\"result\": \"success\"}",
                            "status": {
                                "code": 200,
                                "message": "Success."
                            }
                        }
                    }
                ]
            }
        }
    }
}
```

### 客户端更新事件 <span id="39b223a2f6y9y" />

#### UpdateInfo - Input Message <span id="649c4ce759ep7" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "11.95%" }} /><col style={{ width: "11.95%" }} /><col style={{ width: "11.41%" }} /><col style={{ width: "11.19%" }} /><col style={{ width: "9.15%" }} /><col style={{ width: "44.35%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>三级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>input</p></td><td><p>directive</p></td><td /><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：UpdateInfo</p></td></tr><tr><td /><td><p>dialog\_id</p></td><td /><td><p>string</p></td><td><p>否</p></td><td><p>对话ID</p></td></tr><tr><td><p>parameters</p></td><td><p>images</p></td><td /><td><p>list\[]</p></td><td><p>否</p></td><td><p>图片数据</p></td></tr><tr><td /><td><p>client\_info</p></td><td><p>status</p></td><td><p>object</p></td><td><p>否</p></td><td><p>客户端当前状态</p></td></tr><tr><td /><td><p>biz\_params</p></td><td /><td><p>object</p></td><td><p>否</p></td><td><p>与Start消息中biz\_params相同，传递对话系统自定义参数。UpdateInfo指令中biz\_params下面每个子项会全量替换Start指令中biz\_params下面的同名项，并在本次连接后续所有对话中生效。</p></td></tr><tr><td /><td><p>upstream</p></td><td><p>language</p></td><td><p>String</p></td><td><p>否</p></td><td><p>语音识别语种更新，不配置则语种不变。设置多个语种时，使用英文逗号分隔，例如：<code>zh,en</code>。</p></td></tr><tr><td /><td><p>downstream</p></td><td><p>language</p></td><td><p>String</p></td><td><p>否</p></td><td><p>语音合成语种更新，不配置则语种不变。</p></td></tr></tbody></table>

示例如下：

```json expandable
{
    "header": {
        "action":"continue-task",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
        "streaming":"duplex"
    },
    "payload": {
        "input":{
          "directive": "UpdateInfo",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        },
        "parameters":{
          "images":[{
              "type": "base64",
              "value": "base64String"
          }],
          "client_info": {
              "status": {
                  "bluetooth_announcement": {
                      "status": "stopped"
                  },
                  "stream_media_playback": {
                      "status": "stopped"
                  },
                  "phone_ringing": {
                      "status": "stopped"
                  },
                  "stream_media_playback_qq": {
                      "status": "stopped"
                  }
              }
          },
          "biz_params":{
          }
        }
    }
}
```

### 心跳事件 <span id="adedb03b40fzw" />

可以通过定期向服务端发送此消息，避免连接超时断开。考虑到预留网络延迟和处理时间的buffer，建议发送频率50秒一次。服务端会回应相同消息，客户端收到不需要做任何处理，忽略即可。

#### HeartBeat - Input Message <span id="ab649a20bagsl" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.17%" }} /><col style={{ width: "17.49%" }} /><col style={{ width: "14.68%" }} /><col style={{ width: "8.58%" }} /><col style={{ width: "43.07%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>input</p></td><td><p>directive</p></td><td><p>string</p></td><td><p>是</p></td><td><p>指令名称：<strong>HeartBeat</strong></p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>否</p></td><td><p>对话id</p></td></tr></tbody></table>

示例：

```json
{
  "header": {
    "action": "continue-task",
    "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43",
    "streaming": "duplex"
  },
  "payload": {
    "input": {
      "directive": "HeartBeat",
      "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
    }
  }
}
```

#### HeartBeat - Output Message <span id="696a99197ejce" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.17%" }} /><col style={{ width: "17.49%" }} /><col style={{ width: "14.68%" }} /><col style={{ width: "8.58%" }} /><col style={{ width: "43.07%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>output</p></td><td><p>event</p></td><td><p>string</p></td><td><p>是</p></td><td><p>事件名称：<strong>HeartBeat</strong></p></td></tr><tr><td /><td><p>dialog\_id</p></td><td><p>string</p></td><td><p>是</p></td><td><p>对话id</p></td></tr></tbody></table>

示例：

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "HeartBeat",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb"
        }
    }
}
```

### 错误事件 <span id="61f76efaaci3b" />

#### Error - Output Message <span id="480a36a0b3w8k" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "10.37%" }} /><col style={{ width: "15.02%" }} /><col style={{ width: "8.05%" }} /><col style={{ width: "9.44%" }} /><col style={{ width: "57.12%" }} /></colgroup><thead><tr><th><p><strong>一级参数</strong></p></th><th><p><strong>二级参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td rowSpan={3}><p>output</p></td><td><p>error\_code</p></td><td><p>int</p></td><td><p>是</p></td><td><p>错误码</p></td></tr><tr><td><p>error\_name</p></td><td><p>string</p></td><td><p>是</p></td><td><p>错误名称</p></td></tr><tr><td><p>error\_message</p></td><td><p>string</p></td><td><p>是</p></td><td><p>错误消息</p></td></tr></tbody></table>

```json
{
    "header": {
        "event":"result-generated",
        "task_id": "f894c16f-f20e-4c1d-837e-89e0fbc63a43"
    },
    "payload": {
        "output":{
          "event": "Error",
          "dialog_id": "dd84xxxx-xxxx-xxxx-xxxx-xxxxb7bb",
          "error_code": 500,
          "error_name": "InternalLLMError",
          "error_message": "Internal LLM error"
        }
    }
}
```

### 错误码 <span id="d4ae67ae2cgzn" />

如遇报错问题，请参见[多模态交互套件-错误码](/zh/model-studio/multimodal-error-code)进行排查。

若问题仍未解决，请联系技术支持，反馈遇到的问题，并提供完整的request\_id和dialog\_id，以便进一步排查问题。

## 术语说明 <span id="266e769fd83o8" />

<strong>VAD</strong>：Voice Activity Detection，语音活动检测。

<strong>ASR</strong>：Automatic Speech Recognition，自动语音识别。

<strong>TTS</strong>：Text-to-Speech，文本转语音，语音合成。

<strong>LLM</strong>：Large Language Model，大语言模型。
