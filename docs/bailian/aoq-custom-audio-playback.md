> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 自定义音频播放

> AOQ Client SDK 支持自定义音频播放功能，通过音频帧回调机制将解码后的 PCM 数据回调给应用层，由开发者自行实现音频渲染播放。

<meta property="og:title" content="自定义音频播放" />

## 功能介绍 <span id="custplay-h2-intro" />

AOQ Client SDK 内部音频模块默认会将接收到的远端音频数据通过系统扬声器/听筒播放，但在特定场景中，SDK 内部的音频播放模块可能无法满足开发需求，需要实现自定义音频播放功能，例如：

- 需要将接收到的音频数据输出到自定义的播放设备或音频处理管线。
- 需要对接收到的音频数据进行二次处理（如 AI 语音识别、音效处理等）。
- 解决音频播放设备被占用的问题。

AOQ Client SDK 支持灵活的自定义播放功能，通过音频帧回调机制，将解码后的 PCM 数据回调给应用层，由开发者自行实现音频渲染播放。

## 示例代码 <span id="custplay-h2-sample" />

暂无

## 前提条件 <span id="custplay-h2-prereqs" />

- 已创建引擎实例（调用 `createEngine`）。
- 已成功连接服务器（`onConnectionStatusChange` 回调状态为 `AoqConnectionStatusConnected`）。

## 功能实现 <span id="custplay-h2-impl" />

### 1. 开启音频播放（外部模式） <span id="custplay-h3-step1" />

调用 `startAudioPlayer` 时设置 `isExternal=true`，关闭 SDK 内部的音频渲染设备，由应用层自行处理音频播放。

```java
AoqClientEngine.AoqAudioPlaybackConfig config = new AoqClientEngine.AoqAudioPlaybackConfig();
config.isExternal = true;  // 关闭 SDK 内部播放，由应用层自行渲染
config.channel = 1;        // 声道数
engine.startAudioPlayer(config);
```

<strong>参数说明：</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>isVoipMode</p></td><td><p>boolean</p></td><td><p>false</p></td><td><p>是否启用 VoIP 模式（硬件AEC），移动端有效</p></td></tr><tr><td><p>isDefaultSpeaker</p></td><td><p>boolean</p></td><td><p>true</p></td><td><p>是否默认扬声器，移动端有效</p></td></tr><tr><td><p>isExternal</p></td><td><p>boolean</p></td><td><p>false</p></td><td><p>是否外部播放模式，true 时 SDK 不打开播放设备</p></td></tr><tr><td><p>channel</p></td><td><p>int</p></td><td><p>1</p></td><td><p>声道数</p></td></tr></tbody></table>

### 2. 设置音频帧回调监听 <span id="custplay-h3-step2" />

调用 `setAudioFrameObserver` 设置音频帧数据回调监听器，实现 `onPlaybackAudioFrame` 回调方法接收播放 PCM 数据。

```java
engine.setAudioFrameObserver(new AoqClientListener.AoqAudioFrameListener() {
    @Override
    public void onPlaybackAudioFrame(@NonNull AoqClientEngine.AoqAudioFrameData frame) {
        // 在此处理接收到的播放音频数据
        // frame.dataPtr: PCM 数据
        // frame.numOfSamples: 采样点数
        // frame.numOfChannels: 声道数
        // frame.samplesPerSec: 采样率
        // frame.bytesPerSample: 每采样点字节数
        playPcmData(frame);
    }
});
```

### 3. 开启播放数据回调 <span id="custplay-h3-step3" />

调用 `enableAudioFrameObserver` 开启播放位置的音频帧回调，指定数据源为 `AoqAudioSourcePlayback`。

```java
AoqClientEngine.AoqAudioObserverConfig observerConfig = new AoqClientEngine.AoqAudioObserverConfig();
observerConfig.sampleRate = 48000;  // 回调音频采样率
observerConfig.channels = 1;       // 回调音频声道数
observerConfig.mode = AoqClientEngine.AoqAudioObserverMode.AoqAudioObserverModeReadOnly; // 只读模式

engine.enableAudioFrameObserver(
    true,  // 开启回调
    AoqClientEngine.AoqAudioSource.AoqAudioSourcePlayback,  // 播放数据源
    observerConfig
);
```

<strong>参数说明：</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "30%" }} /><col style={{ width: "30%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>sampleRate</p></td><td><p>int</p></td><td><p>48000</p></td><td><p>回调音频采样率（Hz）</p></td></tr><tr><td><p>channels</p></td><td><p>int</p></td><td><p>1</p></td><td><p>回调音频声道数</p></td></tr><tr><td><p>mode</p></td><td><p>AoqAudioObserverMode</p></td><td><p>AoqAudioObserverModeReadOnly</p></td><td><p>读写模式</p></td></tr></tbody></table>

### 4. 实现自定义音频渲染 <span id="custplay-h3-step4" />

在 `onPlaybackAudioFrame` 回调中接收到 PCM 数据后，由应用层自行实现音频渲染播放。常见的实现方式：

- <strong>Android AudioTrack</strong>：通过 AudioTrack 将 PCM 数据写入系统音频设备播放。
- <strong>AI 语音识别</strong>：将 PCM 数据传入 ASR 引擎进行语音识别。
- <strong>音效处理</strong>：对 PCM 数据进行音效处理后再播放。
- <strong>文件存储</strong>：将接收到的音频数据保存到本地文件。

```java expandable
// 示例：使用 Android AudioTrack 播放
private AudioTrack mAudioTrack;
private volatile boolean mPlayRunning = false;

private void initAudioTrack(int sampleRate, int channels) {
    int channelConfig = (channels == 2)
        ? AudioFormat.CHANNEL_OUT_STEREO
        : AudioFormat.CHANNEL_OUT_MONO;
    int bufferSize = AudioTrack.getMinBufferSize(
        sampleRate, channelConfig, AudioFormat.ENCODING_PCM_16BIT);

    mAudioTrack = new AudioTrack(
        AudioManager.STREAM_VOICE_CALL,
        sampleRate,
        channelConfig,
        AudioFormat.ENCODING_PCM_16BIT,
        bufferSize,
        AudioTrack.MODE_STREAM);
    mAudioTrack.play();
    mPlayRunning = true;
}

private void playPcmData(AoqClientEngine.AoqAudioFrameData frame) {
    if (!mPlayRunning || mAudioTrack == null) {
        return;
    }
    if (frame.dataPtr != null && frame.dataSize > 0) {
        mAudioTrack.write(frame.dataPtr, 0, frame.dataSize);
    }
}
```

<strong>注意事项：</strong>

- `onPlaybackAudioFrame` 回调在 SDK 内部线程触发，回调中的 `frame.dataPtr` 仅在回调期间有效，异步使用需自行拷贝。
- AudioTrack.write 是阻塞操作，在回调中直接写入即可，SDK 内部会按节奏回调。
- 需要维护 `mPlayRunning` 标记，当引擎退出或停止播放时退出处理逻辑。

### 5. 停止自定义播放 <span id="custplay-h3-step5" />

当不再需要自定义播放时，先关闭音频帧回调，再停止播放设备，释放 AudioTrack 资源。

```java
// 1. 关闭播放位置的音频帧回调
AoqClientEngine.AoqAudioObserverConfig observerConfig = new AoqClientEngine.AoqAudioObserverConfig();
engine.enableAudioFrameObserver(
    false,  // 关闭回调
    AoqClientEngine.AoqAudioSource.AoqAudioSourcePlayback,
    observerConfig
);

// 2. 移除音频帧回调监听
engine.setAudioFrameObserver(null);

// 3. 停止 SDK 音频播放
engine.stopAudioPlayer();

// 4. 释放 AudioTrack 资源
mPlayRunning = false;
if (mAudioTrack != null) {
    mAudioTrack.stop();
    mAudioTrack.release();
    mAudioTrack = null;
}
```
