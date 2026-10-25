> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 音频常用功能介绍

> AOQ Client SDK 提供了完整的音频能力，覆盖音频采集、播放、编解码配置、扬声器管理、文件混音、外部音频流注入、音频帧数据回调等核心场景。本文档基于 Android（Java）、iOS（Objective-C）、Ohos（ArkTS）三个平台的公开 API，对音频常用功能进行统一介绍。

<meta property="og:title" content="音频常用功能介绍" />

## 音频采集 <span id="audiofeat-s01h2" />

音频采集用于打开设备麦克风，将实时音频数据送入 SDK 编码推流管线。SDK 支持两种采集模式：

- <strong>内部采集</strong>（默认）：SDK 自动管理麦克风设备的打开、录音和关闭。
- <strong>外部采集</strong>：由应用自行管理麦克风，采集到的 PCM 数据通过外部音频流接口输入 SDK。

### 配置参数 <span id="audiofeat-s01ah3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>isExternal</p></td><td><p>bool</p></td><td><p>false</p></td><td><p>是否使用外部采集模式</p></td></tr><tr><td><p>isVoipMode</p></td><td><p>bool</p></td><td><p>false</p></td><td><p>是否启用 VoIP 模式（硬件 AEC），移动端有效，采集播放参数先到为准</p></td></tr><tr><td><p>channel</p></td><td><p>int</p></td><td><p>1</p></td><td><p>采集通道数，支持 1（单声道）/ 2（立体声）</p></td></tr></tbody></table>

### API 对照 <span id="audiofeat-s01bh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "27%" }} /><col style={{ width: "27%" }} /><col style={{ width: "26%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>开启采集</p></td><td><p><code>startAudioCapture(config)</code></p></td><td><p><code>startAudioCapture:config:</code></p></td><td><p><code>startAudioCapture(config)</code></p></td></tr><tr><td><p>关闭采集</p></td><td><p><code>stopAudioCapture()</code></p></td><td><p><code>stopAudioCapture</code></p></td><td><p><code>stopAudioCapture()</code></p></td></tr><tr><td><p>静音/取消静音</p></td><td><p><code>muteAudioCapture(mute)</code></p></td><td><p><code>muteAudioCapture:</code></p></td><td><p><code>muteAudioCapture(mute)</code></p></td></tr></tbody></table>

### 使用示例 <span id="audiofeat-s01ch3" />

<strong>Android</strong>

```java
AoqAudioCaptureConfig config = new AoqAudioCaptureConfig();
config.isVoipMode = true;
config.channel = 1;
engine.startAudioCapture(config);
```

<strong>iOS</strong>

```objectivec
AoqAudioCaptureConfig *config = [[AoqAudioCaptureConfig alloc] init];
config.isVoipMode = YES;
config.channel = 1;
[engine startAudioCapture:config];
```

<strong>Ohos</strong>

```javascript
const config: AoqAudioCaptureConfig = { isVoipMode: true, channel: 1 };
engine.startAudioCapture(config);
```

## 音频播放 <span id="audiofeat-s02h2" />

音频播放用于将接收到的远端音频数据渲染到本地扬声器或耳机。SDK 支持播放暂停/恢复（带淡入淡出）、打断当前轮音频通话等高级控制。

### 配置参数 <span id="audiofeat-s02ah3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>isVoipMode</p></td><td><p>bool</p></td><td><p>false</p></td><td><p>是否启用 VoIP 模式（硬件AEC），移动端有效，采集播放参数先到为准</p></td></tr><tr><td><p>isDefaultSpeaker</p></td><td><p>bool</p></td><td><p>true</p></td><td><p>是否默认使用扬声器（移动端有效，非VoIP时无效）</p></td></tr><tr><td><p>isExternal</p></td><td><p>bool</p></td><td><p>false</p></td><td><p>是否使用外部播放模式</p></td></tr><tr><td><p>channel</p></td><td><p>int</p></td><td><p>1</p></td><td><p>播放通道数，支持 1（单声道）/ 2（立体声）</p></td></tr></tbody></table>

### API 对照 <span id="audiofeat-s02bh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "26%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>开始播放</p></td><td><p><code>startAudioPlayer(config)</code></p></td><td><p><code>startAudioPlayer:config:</code></p></td><td><p><code>startAudioPlayer(config)</code></p></td></tr><tr><td><p>停止播放</p></td><td><p><code>stopAudioPlayer()</code></p></td><td><p><code>stopAudioPlayer</code></p></td><td><p><code>stopAudioPlayer()</code></p></td></tr><tr><td><p>暂停播放</p></td><td><p><code>pauseAudioPlayer(fadeMs)</code></p></td><td><p><code>pauseAudioPlayer:</code></p></td><td><p><code>pauseAudioPlayer(fadeMs)</code></p></td></tr><tr><td><p>恢复播放</p></td><td><p><code>resumeAudioPlayer(fadeMs)</code></p></td><td><p><code>resumeAudioPlayer:</code></p></td><td><p><code>resumeAudioPlayer(fadeMs)</code></p></td></tr><tr><td><p>打断通话</p></td><td><p><code>interruptAudioPlayer(trackType, fadeMs)</code></p></td><td><p><code>interruptAudioPlayer:fadeMs:</code></p></td><td><p><code>interruptAudioPlayer(trackType, fadeMs)</code></p></td></tr></tbody></table>

<Note>
  <strong>fadeMs 参数</strong>：暂停和恢复播放时的淡入/淡出时长（毫秒），设为 0 则立即切换。
</Note>

## 扬声器管理 <span id="audiofeat-s03h2" />

控制音频输出设备在扬声器和听筒之间切换。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "27%" }} /><col style={{ width: "27%" }} /><col style={{ width: "26%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>切换扬声器</p></td><td><p><code>enableSpeakerphone(enable)</code></p></td><td><p><code>enableSpeakerphone:</code></p></td><td><p><code>enableSpeakerphone(enable)</code></p></td></tr><tr><td><p>查询扬声器状态</p></td><td><p><code>isSpeakerphoneEnabled()</code></p></td><td><p><code>isSpeakerphoneEnabled</code></p></td><td><p><code>isSpeakerphoneEnabled()</code></p></td></tr></tbody></table>

<Note>
  需要在 VoIP 模式下才允许切换，非 VoIP 时，`enableSpeakerphone` 调用有 OnError(AoqECAudioDeviceEarpieceRequiresVoipMode) 错误通知。
</Note>

<Note>
  <strong>iOS 特殊行为</strong>：iPad 设备只有扬声器模式；当 AVAudioSession 不是 PlayAndRecord 类别时，也始终返回 YES。
</Note>

## 音频编解码配置 <span id="audiofeat-s04h2" />

设置音频上行（编码器）和下行（解码器）的编码格式、采样率、声道数和码率。表示推流/拉流的格式。

### 配置参数 <span id="audiofeat-s04ah3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "18%" }} /><col style={{ width: "15%" }} /><col style={{ width: "47%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>trackType</p></td><td><p>AoqTrackType</p></td><td><p>Audio</p></td><td><p>音频轨道类型，当前只支持一条音频流</p></td></tr><tr><td><p>codecType</p></td><td><p>AoqEncoderType</p></td><td><p>AudioOpus</p></td><td><p>编码类型：AudioPCM(1) 或 AudioOpus(2)</p></td></tr><tr><td><p>sampleRate</p></td><td><p>int</p></td><td><p>48000</p></td><td><p>采样率，Opus 支持 8K/16K/48K，PCM 支持 8K/16K/32K/48K</p></td></tr><tr><td><p>channel</p></td><td><p>int</p></td><td><p>1</p></td><td><p>声道数，支持 1（单声道）/ 2（立体声）</p></td></tr><tr><td><p>bitrate</p></td><td><p>int</p></td><td><p>32000</p></td><td><p>码率（bps）</p></td></tr></tbody></table>

### API 对照 <span id="audiofeat-s04bh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "27%" }} /><col style={{ width: "27%" }} /><col style={{ width: "26%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>设置编码参数</p></td><td><p><code>setAudioEncoderConfig(config)</code></p></td><td><p><code>setAudioEncoderConfig:</code></p></td><td><p><code>setAudioEncoderConfig(config)</code></p></td></tr><tr><td><p>设置解码参数</p></td><td><p><code>setAudioDecoderConfig(config)</code></p></td><td><p><code>setAudioDecoderConfig:</code></p></td><td><p><code>setAudioDecoderConfig(config)</code></p></td></tr></tbody></table>

### 支持的编码格式 <span id="audiofeat-s04ch3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "40%" }} /><col style={{ width: "15%" }} /><col style={{ width: "45%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqEncoderTypeAudioPCM</p></td><td><p>1</p></td><td><p>PCM 裸音频</p></td></tr><tr><td><p>AoqEncoderTypeAudioOpus</p></td><td><p>2</p></td><td><p>Opus 编码</p></td></tr></tbody></table>

## 音频文件混音 <span id="audiofeat-s05h2" />

支持将本地音频文件混入当前音频流中一起推流和/或本地播放。每个音频文件通过业务自分配的 `fileId` 标识，可同时管理多个文件实例。

### 混音配置参数 <span id="audiofeat-s05ah3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>fileName</p></td><td><p>String</p></td><td><p>-</p></td><td><p>音频文件路径（含文件名）</p></td></tr><tr><td><p>cycles</p></td><td><p>int</p></td><td><p>-1</p></td><td><p>循环次数，-1 表示无限循环</p></td></tr><tr><td><p>startPosMs</p></td><td><p>long</p></td><td><p>0</p></td><td><p>起始播放位置（毫秒）</p></td></tr><tr><td><p>publishVolume</p></td><td><p>int</p></td><td><p>100</p></td><td><p>推流音量 \[0-100]</p></td></tr><tr><td><p>playoutVolume</p></td><td><p>int</p></td><td><p>100</p></td><td><p>本地播放音量 \[0-100]</p></td></tr></tbody></table>

### API 对照 <span id="audiofeat-s05bh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "26%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>开始播放</p></td><td><p><code>startAudioFile(fileId, config)</code></p></td><td><p><code>startAudioFile:config:</code></p></td><td><p><code>startAudioFile(fileId, config)</code></p></td></tr><tr><td><p>停止播放</p></td><td><p><code>stopAudioFile(fileId)</code></p></td><td><p><code>stopAudioFile:</code></p></td><td><p><code>stopAudioFile(fileId)</code></p></td></tr><tr><td><p>暂停</p></td><td><p><code>pauseAudioFile(fileId)</code></p></td><td><p><code>pauseAudioFile:</code></p></td><td><p><code>pauseAudioFile(fileId)</code></p></td></tr><tr><td><p>恢复</p></td><td><p><code>resumeAudioFile(fileId)</code></p></td><td><p><code>resumeAudioFile:</code></p></td><td><p><code>resumeAudioFile(fileId)</code></p></td></tr><tr><td><p>获取文件时长</p></td><td><p><code>getAudioFileDuration(fileId)</code></p></td><td><p><code>getAudioFileDuration:</code></p></td><td><p><code>getAudioFileDuration(fileId)</code></p></td></tr><tr><td><p>获取当前位置</p></td><td><p><code>getAudioFileCurrentPosition(fileId)</code></p></td><td><p><code>getAudioFileCurrentPosition:</code></p></td><td><p><code>getAudioFileCurrentPosition(fileId)</code></p></td></tr><tr><td><p>设置播放位置</p></td><td><p><code>setAudioFilePositionMillis(fileId, pos)</code></p></td><td><p><code>setAudioFilePositionMillis:positionMillis:</code></p></td><td><p><code>setAudioFilePositionMillis(fileId, pos)</code></p></td></tr><tr><td><p>设置音量</p></td><td><p><code>setAudioFileVolume(fileId, type, vol)</code></p></td><td><p><code>setAudioFileVolume:type:volume:</code></p></td><td><p><code>setAudioFileVolume(fileId, type, vol)</code></p></td></tr><tr><td><p>获取音量</p></td><td><p><code>getAudioFileVolume(fileId, type)</code></p></td><td><p><code>getAudioFileVolume:type:</code></p></td><td><p><code>getAudioFileVolume(fileId, type)</code></p></td></tr></tbody></table>

<Note>
  <strong>音量方向（type）</strong>：`AoqAudioStreamPublish(0)` 控制推流音量；`AoqAudioStreamPlayout(1)` 控制本地播放音量。
</Note>

### 状态回调 <span id="audiofeat-s05ch3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>状态码</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqAudioFileNone</p></td><td><p>0</p></td><td><p>初始状态</p></td></tr><tr><td><p>AoqAudioFileStarted</p></td><td><p>1</p></td><td><p>已开始播放</p></td></tr><tr><td><p>AoqAudioFileStopped</p></td><td><p>2</p></td><td><p>已停止</p></td></tr><tr><td><p>AoqAudioFilePaused</p></td><td><p>3</p></td><td><p>已暂停</p></td></tr><tr><td><p>AoqAudioFileResumed</p></td><td><p>4</p></td><td><p>已恢复</p></td></tr><tr><td><p>AoqAudioFileEnded</p></td><td><p>5</p></td><td><p>播放结束</p></td></tr><tr><td><p>AoqAudioFileBuffering</p></td><td><p>6</p></td><td><p>缓冲中</p></td></tr><tr><td><p>AoqAudioFileBufferingEnd</p></td><td><p>7</p></td><td><p>缓冲结束</p></td></tr><tr><td><p>AoqAudioFileFailed</p></td><td><p>8</p></td><td><p>播放失败</p></td></tr></tbody></table>

## 外部音频流 <span id="audiofeat-s06h2" />

外部音频流允许将应用生成的 PCM 音频数据注入到 SDK 的音频管线中，支持推流和/或本地播放。典型场景包括 TTS 语音合成输出、AI 模型音频输出、背景音效等。每个外部音频流通过业务自分配的 `streamId` 标识。

### 配置参数 <span id="audiofeat-s06ah3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22%" }} /><col style={{ width: "18%" }} /><col style={{ width: "12%" }} /><col style={{ width: "48%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>trackType</p></td><td><p>AoqTrackType</p></td><td><p>Audio</p></td><td><p>音频轨道类型</p></td></tr><tr><td><p>codecType</p></td><td><p>AoqEncoderType</p></td><td><p>AudioPCM</p></td><td><p>音频流格式</p></td></tr><tr><td><p>channels</p></td><td><p>int</p></td><td><p>1</p></td><td><p>声道数</p></td></tr><tr><td><p>sampleRate</p></td><td><p>int</p></td><td><p>48000</p></td><td><p>采样率，支持 8/12/16/24/32/44.1/48/64/88.2/96/176.4/192K</p></td></tr><tr><td><p>playoutVolume</p></td><td><p>int</p></td><td><p>100</p></td><td><p>本地播放音量 \[0-100]</p></td></tr><tr><td><p>publishVolume</p></td><td><p>int</p></td><td><p>100</p></td><td><p>推流音量 \[0-100]</p></td></tr><tr><td><p>maxBufferDuration</p></td><td><p>int</p></td><td><p>600000</p></td><td><p>最大缓冲时长（毫秒），取值范围 \[100, \~]，超过时 Push 失败</p></td></tr><tr><td><p>enable3A</p></td><td><p>bool</p></td><td><p>false</p></td><td><p>输入 PCM 是否经过 3A 处理</p></td></tr></tbody></table>

### API 对照 <span id="audiofeat-s06bh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "30%" }} /><col style={{ width: "26%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>新增外部音频流</p></td><td><p><code>addAudioExternalStream(streamId, config)</code></p></td><td><p><code>addAudioExternalStream:config:</code></p></td><td><p><code>addAudioExternalStream(streamId, config)</code></p></td></tr><tr><td><p>输入音频数据</p></td><td><p><code>pushAudioExternalStreamData(streamId, data)</code></p></td><td><p><code>pushAudioExternalStreamData:data:</code></p></td><td><p><code>pushAudioExternalStreamData(streamId, data)</code></p></td></tr><tr><td><p>设置音量</p></td><td><p><code>setAudioExternalStreamVolume(streamId, type, vol)</code></p></td><td><p><code>setAudioExternalStreamVolume:type:volume:</code></p></td><td><p><code>setAudioExternalStreamVolume(streamId, type, vol)</code></p></td></tr><tr><td><p>获取音量</p></td><td><p><code>getAudioExternalStreamVolume(streamId, type)</code></p></td><td><p><code>getAudioExternalStreamVolume:type:</code></p></td><td><p><code>getAudioExternalStreamVolume(streamId, type)</code></p></td></tr><tr><td><p>清空缓存</p></td><td><p><code>clearAudioExternalStreamBuffer(streamId, fadeoutMs)</code></p></td><td><p><code>clearAudioExternalStreamBuffer:fadeoutMs:</code></p></td><td><p><code>clearAudioExternalStreamBuffer(streamId, fadeoutMs)</code></p></td></tr><tr><td><p>移除流</p></td><td><p><code>removeAudioExternalStream(streamId)</code></p></td><td><p><code>removeAudioExternalStream:</code></p></td><td><p><code>removeAudioExternalStream(streamId)</code></p></td></tr></tbody></table>

### Push 数据最佳实践 <span id="audiofeat-s06ch3" />

- 需要循环调用 `pushAudioExternalStreamData`，保证数据 push 成功
- 返回错误码 110（缓冲区满）时短暂 Sleep 30ms 后重试，不要丢弃数据
- 引擎退出前先停止推送循环，再调用 `removeAudioExternalStream`
- 实时采集每帧 10ms 长，有数据就调用 push；从文件解析每帧 40ms 长，间隔 30ms 调用 push 一次

## 音频帧数据回调 <span id="audiofeat-s07h2" />

音频帧回调允许开发者在音频管线的不同位置获取原始 PCM 数据，用于音频分析、自定义处理、录制等场景。

### 支持的数据源位置 <span id="audiofeat-s07ah3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "15%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th><p><strong>数据源</strong></p></th><th><p><strong>枚举值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Captured</p></td><td><p>0</p></td><td><p>采集后的原始音频数据（未经 3A 处理）</p></td></tr><tr><td><p>ProcessCaptured</p></td><td><p>1</p></td><td><p>经过 3A 处理后的音频数据，需要 Connect 成功后才回调数据</p></td></tr><tr><td><p>Publish</p></td><td><p>2</p></td><td><p>即将推流的音频数据（需要 Connect 成功）</p></td></tr><tr><td><p>Playback</p></td><td><p>3</p></td><td><p>即将播放的音频数据（远端下行）</p></td></tr></tbody></table>

### 回调配置参数 <span id="audiofeat-s07bh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "22%" }} /><col style={{ width: "15%" }} /><col style={{ width: "43%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>sampleRate</p></td><td><p>int</p></td><td><p>48000</p></td><td><p>回调音频的采样率</p></td></tr><tr><td><p>channels</p></td><td><p>int</p></td><td><p>1</p></td><td><p>回调音频的声道数，支持 1/2</p></td></tr><tr><td><p>mode</p></td><td><p>AoqAudioObserverMode</p></td><td><p>ReadOnly</p></td><td><p>只读(0)/读写(1) 模式</p></td></tr></tbody></table>

### 使用步骤 <span id="audiofeat-s07ch3" />

1. <strong>注册观察者</strong>：调用 `setAudioFrameObserver` 设置音频帧回调监听器
2. <strong>启用数据源</strong>：调用 `enableAudioFrameObserver` 选择需要监听的数据源位置，开启回调
3. <strong>处理回调数据</strong>：在回调函数中获取 PCM 数据

### API 对照 <span id="audiofeat-s07dh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18%" }} /><col style={{ width: "28%" }} /><col style={{ width: "30%" }} /><col style={{ width: "24%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>注册观察者</p></td><td><p><code>setAudioFrameObserver(listener)</code></p></td><td><p><code>setAudioFrameObserver:</code></p></td><td><p><code>setAudioFrameObserver(observer)</code></p></td></tr><tr><td><p>启用回调</p></td><td><p><code>enableAudioFrameObserver(enabled, source, config)</code></p></td><td><p><code>enableAudioFrameObserver:audioSource:config:</code></p></td><td><p><code>enableAudioFrameObserver(enabled, source, config)</code></p></td></tr></tbody></table>

### 回调方法 <span id="audiofeat-s07eh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15%" }} /><col style={{ width: "28%" }} /><col style={{ width: "30%" }} /><col style={{ width: "27%" }} /></colgroup><thead><tr><th><p><strong>回调</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>采集数据</p></td><td><p><code>onCapturedAudioFrame(frame)</code></p></td><td><p><code>onCapturedAudioFrame:</code></p></td><td><p><code>onCapturedAudioFrame(frame)</code></p></td></tr><tr><td><p>3A 后数据</p></td><td><p><code>onProcessCapturedAudioFrame(frame)</code></p></td><td><p><code>onProcessCapturedAudioFrame:</code></p></td><td><p><code>onProcessCapturedAudioFrame(frame)</code></p></td></tr><tr><td><p>推流数据</p></td><td><p><code>onPublishAudioFrame(trackType, frame)</code></p></td><td><p><code>onPublishAudioFrame:frame:</code></p></td><td><p><code>onPublishAudioFrame(trackType, frame)</code></p></td></tr><tr><td><p>播放数据</p></td><td><p><code>onPlaybackAudioFrame(frame)</code></p></td><td><p><code>onPlaybackAudioFrame:</code></p></td><td><p><code>onPlaybackAudioFrame(frame)</code></p></td></tr></tbody></table>

## 音频状态与路由 <span id="audiofeat-s08h2" />

SDK 自动监测音频设备的状态变化和路由切换，并通过回调通知应用层。

### 设备状态码 <span id="audiofeat-s08ah3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "10%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th><p><strong>状态码</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqAudioDeviceNone</p></td><td><p>0</p></td><td><p>初始状态</p></td></tr><tr><td><p>RecordStarting</p></td><td><p>1</p></td><td><p>采集启动中</p></td></tr><tr><td><p>RecordStarted</p></td><td><p>2</p></td><td><p>采集已启动</p></td></tr><tr><td><p>RecordStopping</p></td><td><p>3</p></td><td><p>采集停止中</p></td></tr><tr><td><p>RecordStopped</p></td><td><p>4</p></td><td><p>采集已停止</p></td></tr><tr><td><p>RecordFail</p></td><td><p>5</p></td><td><p>采集失败</p></td></tr><tr><td><p>PlayStarting</p></td><td><p>6</p></td><td><p>播放启动中</p></td></tr><tr><td><p>PlayStarted</p></td><td><p>7</p></td><td><p>播放已启动</p></td></tr><tr><td><p>PlayStopping</p></td><td><p>8</p></td><td><p>播放停止中</p></td></tr><tr><td><p>PlayStopped</p></td><td><p>9</p></td><td><p>播放已停止</p></td></tr><tr><td><p>PlayFail</p></td><td><p>10</p></td><td><p>播放失败</p></td></tr></tbody></table>

### 设备路由类型 <span id="audiofeat-s08bh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "10%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th><p><strong>路由</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Default</p></td><td><p>0</p></td><td><p>默认</p></td></tr><tr><td><p>Headset</p></td><td><p>1</p></td><td><p>有线耳机</p></td></tr><tr><td><p>Earpiece</p></td><td><p>2</p></td><td><p>听筒</p></td></tr><tr><td><p>HeadsetNoMic</p></td><td><p>3</p></td><td><p>无麦克风耳机</p></td></tr><tr><td><p>SpeakerPhone</p></td><td><p>4</p></td><td><p>扬声器</p></td></tr><tr><td><p>Usb</p></td><td><p>5</p></td><td><p>USB 设备</p></td></tr><tr><td><p>Bluetooth</p></td><td><p>6</p></td><td><p>蓝牙 SCO</p></td></tr><tr><td><p>BluetoothA2dp</p></td><td><p>7</p></td><td><p>蓝牙 A2DP</p></td></tr></tbody></table>

### 回调对照 <span id="audiofeat-s08ch3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>回调</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>设备状态变化</p></td><td><p><code>onAudioDeviceStateChanged(state)</code></p></td><td><p><code>onAudioDeviceStateChanged:</code></p></td><td><p><code>onAudioDeviceStateChanged(state, reason)</code></p></td></tr><tr><td><p>路由变化</p></td><td><p><code>onAudioDeviceRouteChanged(routeType)</code></p></td><td><p><code>onAudioDeviceRouteChanged:</code></p></td><td><p><code>onAudioDeviceRouteChanged(routeType)</code></p></td></tr><tr><td><p>设备中断</p></td><td><p><code>onAudioDeviceInterrupted(interrupt)</code></p></td><td><p><code>onAudioDeviceInterrupted:</code></p></td><td><p><code>onAudioDeviceInterrupted(interrupt)</code></p></td></tr><tr><td><p>文件状态</p></td><td><p><code>onAudioFileState(state)</code></p></td><td><p><code>onAudioFileState:</code></p></td><td><p><code>onAudioFileState(fileId, stateCode, errorCode)</code></p></td></tr></tbody></table>

## 音频错误码与警告码 <span id="audiofeat-s09h2" />

### 音频错误码 <span id="audiofeat-s09ah3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "10%" }} /><col style={{ width: "55%" }} /></colgroup><thead><tr><th><p><strong>错误码</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqErrorCodeAudio</p></td><td><p>100</p></td><td><p>通用音频错误</p></td></tr><tr><td><p>AudioExternalBufferFull</p></td><td><p>110</p></td><td><p>外部缓冲区已满</p></td></tr><tr><td><p>AudioDevice</p></td><td><p>120</p></td><td><p>设备通用错误</p></td></tr><tr><td><p>RecordingAuthFailed</p></td><td><p>121</p></td><td><p>麦克风权限失败</p></td></tr><tr><td><p>RecordingOccupied</p></td><td><p>122</p></td><td><p>麦克风被占用</p></td></tr><tr><td><p>RecordingBackgroundStart</p></td><td><p>123</p></td><td><p>后台启动录音</p></td></tr><tr><td><p>RecordingStartFail</p></td><td><p>124</p></td><td><p>录音启动失败</p></td></tr><tr><td><p>PlayoutOccupied</p></td><td><p>125</p></td><td><p>播放设备被占用</p></td></tr><tr><td><p>PlayoutBackgroundStart</p></td><td><p>126</p></td><td><p>后台启动播放</p></td></tr><tr><td><p>PlayoutStartFail</p></td><td><p>127</p></td><td><p>播放启动失败</p></td></tr><tr><td><p>EarpieceRequiresVoipMode</p></td><td><p>128</p></td><td><p>听筒需要启用 VoIP 模式</p></td></tr></tbody></table>

### 音频警告码 <span id="audiofeat-s09bh3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "10%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th><p><strong>警告码</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqWCAudio</p></td><td><p>100</p></td><td><p>通用音频警告</p></td></tr><tr><td><p>AudioHowling</p></td><td><p>101</p></td><td><p>啸叫检测</p></td></tr><tr><td><p>AudioDevice</p></td><td><p>120</p></td><td><p>设备通用警告</p></td></tr><tr><td><p>MicEnumerateError</p></td><td><p>121</p></td><td><p>麦克风枚举错误</p></td></tr><tr><td><p>MicStartTimeout</p></td><td><p>122</p></td><td><p>麦克风启动超时</p></td></tr><tr><td><p>RecordingError</p></td><td><p>123</p></td><td><p>录音错误</p></td></tr><tr><td><p>SpeakerEnumerateError</p></td><td><p>124</p></td><td><p>扬声器枚举错误</p></td></tr><tr><td><p>SpeakerStartTimeout</p></td><td><p>125</p></td><td><p>扬声器启动超时</p></td></tr><tr><td><p>PlayoutError</p></td><td><p>126</p></td><td><p>播放错误</p></td></tr></tbody></table>

## iOS 专有：AVAudioSession 控制 <span id="audiofeat-s10h2" />

iOS 平台提供了 `setAudioSessionRestriction` 接口，可精细控制 SDK 对系统 AVAudioSession 的管理权限。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th><p><strong>控制项</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>SetCategory</p></td><td><p>SDK 是否有权设置 Session 类别</p></td></tr><tr><td><p>ConfigureSession</p></td><td><p>SDK 是否有权配置 Session 参数</p></td></tr><tr><td><p>DeactivateSession</p></td><td><p>SDK 是否有权停用 Session</p></td></tr><tr><td><p>ActivateSession</p></td><td><p>SDK 是否有权激活 Session</p></td></tr></tbody></table>

通过按位组合传入 restriction 值，可限制 SDK 对 AVAudioSession 的控制范围，避免与应用层其他音频组件冲突。
