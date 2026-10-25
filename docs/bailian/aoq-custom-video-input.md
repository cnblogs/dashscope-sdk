> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 自定义视频输入

> 介绍 AOQ Client SDK 自定义视频输入的两种模式：原始帧模式和编码帧模式，以及各模式的配置方法和示例代码。

<meta property="og:title" content="自定义视频输入" />

## 功能介绍 <span id="custvid-h2-intro" />

AOQ Client SDK 内部视频模块可满足应用中对基本视频功能的需求，但在特定场景中，SDK 内部的视频采集模块可能无法满足开发需求，需要实现自定义视频采集功能，例如：

- 解决摄像头设备被占用或不兼容问题。
- 需要从定制的采集系统、视频文件中获取视频数据后交给 SDK 传输。
- 需要将 AI 生成的画面、屏幕录制、虚拟摄像头等内容通过 SDK 推流传输。

AOQ Client SDK 支持两种自定义视频采集模式：

- <strong>原始帧模式</strong>：自行采集原始视频帧（BGRA、I420、NV12、NV21 等格式），通过 `pushExternalVideoCapturedFrame` 推送给 SDK 进行编码和传输。SDK 内部完成编码、传输等完整流程。
- <strong>编码帧模式</strong>：自行完成视频编码（目前支持 JPEG），通过 `pushExternalVideoEncodedFrame` 直推已编码数据给 SDK，跳过 SDK 内部编码器，直接打包发送。

## 示例代码 <span id="custvid-h2-sample" />

暂无

## 前提条件 <span id="custvid-h2-prereq" />

- 已创建引擎实例（调用 `createEngine`）。
- 已成功连接服务器（`onConnectionStatusChange` 回调状态为 `AoqConnectionStatusConnected`）。

## 功能实现 <span id="custvid-h2-impl" />

根据业务场景选择以下两种模式之一。两种模式不可混用：同一时间只能使用其中一种推送接口。

## 模式一：原始帧模式 <span id="custvid-h2-rawmode" />

自行采集原始视频帧（BGRA、I420、NV12、NV21 等格式），推送给 SDK 进行编码和传输。SDK 内部完成编码、传输等完整流程。

### 1. 配置视频编码参数 <span id="custvid-h2-raw-step1" />

SDK 内部编码器会对推送的原始帧进行编码，可根据业务需要调整编码参数。

```java
AoqClientEngine.AoqVideoCodecConfig config = new AoqClientEngine.AoqVideoCodecConfig();
config.width            = 1280;
config.height           = 720;
config.fps              = 2;
config.bitrate          = 500000;   // 起始码率 500kbps
config.minBitrate       = 128000;   // 最小码率 128kbps
config.keyframeInterval = 2;
// isExternal 保持默认 false，SDK 内部编码

engine.setVideoEncoderConfig(config);
```

<strong>参数说明：</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>trackType</p></td><td><p>AoqTrackType</p></td><td><p>AoqTrackTypeVideo</p></td><td><p>视频轨道类型</p></td></tr><tr><td><p>codecType</p></td><td><p>AoqEncoderType</p></td><td><p>AoqEncoderTypeVideoH264</p></td><td><p>编码器类型</p></td></tr><tr><td><p>width</p></td><td><p>int</p></td><td><p>720</p></td><td><p>编码宽度（像素）</p></td></tr><tr><td><p>height</p></td><td><p>int</p></td><td><p>1280</p></td><td><p>编码高度（像素）</p></td></tr><tr><td><p>fps</p></td><td><p>int</p></td><td><p>5</p></td><td><p>帧率</p></td></tr><tr><td><p>bitrate</p></td><td><p>int</p></td><td><p>500000</p></td><td><p>起始码率（bps）</p></td></tr><tr><td><p>minBitrate</p></td><td><p>int</p></td><td><p>128000</p></td><td><p>最小码率（bps）</p></td></tr><tr><td><p>keyframeInterval</p></td><td><p>int</p></td><td><p>2</p></td><td><p>关键帧间隔（秒）</p></td></tr><tr><td><p>isExternal</p></td><td><p>boolean</p></td><td><p>false</p></td><td><p>原始帧模式保持 false</p></td></tr><tr><td><p>mirrorMode</p></td><td><p>AoqMirrorMode</p></td><td><p>AoqMirrorModeDisabled</p></td><td><p>镜像模式</p></td></tr><tr><td><p>orientationMode</p></td><td><p>AoqOrientationMode</p></td><td><p>AoqOrientationModeAuto</p></td><td><p>画面方向模式</p></td></tr></tbody></table>

### 2. 以外部采集模式启动视频采集 <span id="custvid-h2-raw-step2" />

调用 `startVideoCapture` 并设置 `isExternal=true`，告知 SDK 不打开摄像头，由外部源提供视频帧。这是原始帧模式的前置条件，未调用则 SDK 不会消费推送的帧数据。

```java
AoqClientEngine.AoqVideoCaptureConfig config = new AoqClientEngine.AoqVideoCaptureConfig();
config.isExternal = true;  // 不打开摄像头，由外部源推送视频帧
// isExternal=true 时 width/height/fps 无效，实际分辨率和帧率由推送数据决定
int ret = engine.startVideoCapture(config);
```

<strong>参数说明：</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>width</p></td><td><p>int</p></td><td><p>1280</p></td><td><p>采集宽度（<code>isExternal=true</code> 时无效）</p></td></tr><tr><td><p>height</p></td><td><p>int</p></td><td><p>720</p></td><td><p>采集高度（<code>isExternal=true</code> 时无效）</p></td></tr><tr><td><p>fps</p></td><td><p>int</p></td><td><p>15</p></td><td><p>采集帧率（<code>isExternal=true</code> 时无效）</p></td></tr><tr><td><p>isExternal</p></td><td><p>boolean</p></td><td><p>false</p></td><td><p>true：不打开摄像头，由外部源推送帧数据</p></td></tr><tr><td><p>cameraDirection</p></td><td><p>AoqCameraDirection</p></td><td><p>AoqCameraDirectionFront</p></td><td><p>摄像头方向（<code>isExternal=true</code> 时无效）</p></td></tr></tbody></table>

### 3. 推送原始视频帧 <span id="custvid-h2-raw-step3" />

调用 `pushExternalVideoCapturedFrame` 接口，将采集到的原始视频帧传入 SDK。SDK 内部完成编码和传输。

支持的视频帧格式：BGRA、I420、NV12、NV21、RGBA。Apple 平台额外支持 CVPixelBuffer 零拷贝格式。

#### 3.1 BGRA 格式 <span id="custvid-h2-raw-bgra" />

BGRA 为打包格式，每个像素 4 字节（Blue、Green、Red、Alpha），一帧数据量 = width x height x 4。

```java
// 构造 BGRA 视频帧
AoqClientEngine.AoqVideoFrame frame = new AoqClientEngine.AoqVideoFrame();
frame.format    = AoqClientEngine.AoqVideoPixelFormat.AoqVideoPixelFormatBGRA;
frame.width     = 1280;
frame.height    = 720;

frame.data      = bgraBytes; // byte[]，长度 = width * height * 4

frame.timeStamp = System.currentTimeMillis();

int ret = engine.pushExternalVideoCapturedFrame(
    AoqClientEngine.AoqTrackType.AoqTrackTypeVideo, frame);
```

#### 3.2 I420 格式 <span id="custvid-h2-raw-i420" />

I420 为三平面格式（Y、U、V 分离），Y 平面大小 = width x height，U/V 平面各为 (width/2) x (height/2)。

```java
// 构造 I420 视频帧
AoqClientEngine.AoqVideoFrame frame = new AoqClientEngine.AoqVideoFrame();
frame.format    = AoqClientEngine.AoqVideoPixelFormat.AoqVideoPixelFormatI420;
frame.width     = 1280;
frame.height    = 720;

frame.dataY     = yPlane;  // byte[]，长度 = width * height

frame.dataU     = uPlane;  // byte[]，长度 = (width/2) * (height/2)

frame.dataV     = vPlane;  // byte[]，长度 = (width/2) * (height/2)

frame.strideY   = 1280;    // Y 平面行字节数
frame.strideU   = 640;     // U 平面行字节数
frame.strideV   = 640;     // V 平面行字节数
frame.timeStamp = System.currentTimeMillis();

int ret = engine.pushExternalVideoCapturedFrame(
    AoqClientEngine.AoqTrackType.AoqTrackTypeVideo, frame);
```

#### 3.3 NV12 / NV21 格式 <span id="custvid-h2-raw-nv12" />

NV12 和 NV21 为半平面格式，Y 平面 + UV 交错平面。NV12 为 UV 交替排列，NV21 为 VU 交替排列。数据量 = width x height x 3 / 2，打包在 `data` 字段中。

```java
// 构造 NV12 视频帧（NV21 同理，修改 format 即可）
AoqClientEngine.AoqVideoFrame frame = new AoqClientEngine.AoqVideoFrame();
frame.format    = AoqClientEngine.AoqVideoPixelFormat.AoqVideoPixelFormatNV12;
frame.width     = 1280;
frame.height    = 720;

frame.data      = nv12Bytes; // byte[]，长度 = width * height * 3 / 2

frame.timeStamp = System.currentTimeMillis();

int ret = engine.pushExternalVideoCapturedFrame(
    AoqClientEngine.AoqTrackType.AoqTrackTypeVideo, frame);
```

#### 3.4 CVPixelBuffer 格式（Apple 平台） <span id="custvid-h2-raw-cvpixel" />

iOS / macOS 平台支持直接传递 `CVPixelBufferRef`，实现零拷贝传输，避免内存拷贝带来的性能开销。

```swift
// iOS / macOS 平台
let frame = AoqVideoFrame()
frame.format      = .cvPixelBuffer
frame.width       = 1280
frame.height      = 720
frame.pixelBuffer = pixelBuffer  // CVPixelBufferRef
frame.timeStamp   = Int64(Date().timeIntervalSince1970 * 1000)

// SDK 内部异步持有 pixelBuffer，需要额外 +1 引用计数
// SDK 消费完毕后会自行释放
let _ = Unmanaged.passRetained(pixelBuffer)

engine.pushExternalVideoCapturedFrame(.video, frame: frame)
```

### 4. 停止原始帧采集 <span id="custvid-h2-raw-step4" />

当不再需要推送视频帧时，先停止推帧定时器，再调用 `stopVideoCapture` 关闭视频采集。

```java
// 1. 停止推帧定时器
stopExternalFramePush();
// 2. 停止视频采集
engine.stopVideoCapture();
```

## 模式二：编码帧模式 <span id="custvid-h2-encmode" />

自行完成视频编码（目前支持 JPEG），直推已编码数据给 SDK，跳过 SDK 内部编码器，直接打包发送。此模式<strong>不需要调用</strong> `startVideoCapture` 等采集相关接口。

### 1. 配置视频编码参数并启用外部编码 <span id="custvid-h2-enc-step1" />

调用 `setVideoEncoderConfig` 并设置 `isExternal=true`，告知 SDK 跳过内部编码器，由外部提供已编码数据。

```java
AoqClientEngine.AoqVideoCodecConfig config = new AoqClientEngine.AoqVideoCodecConfig();
config.width      = 1280;
config.height     = 720;
config.fps        = 2;
config.isExternal = true;  // 跳过内部编码，由外部推送已编码帧

engine.setVideoEncoderConfig(config);
```

设置完成后即可直接推送编码帧，无需调用 `startVideoCapture`。

### 2. 推送编码视频帧 <span id="custvid-h2-enc-step2" />

调用 `pushExternalVideoEncodedFrame` 接口，将已编码的视频数据直传给 SDK。目前仅支持 JPEG 编码格式。

```java
// 从 Bitmap 生成 JPEG 数据
android.graphics.Bitmap bmp = android.graphics.Bitmap.createBitmap(
    width, height, android.graphics.Bitmap.Config.ARGB_8888);
// ... 填充 Bitmap 内容 ...

java.io.ByteArrayOutputStream baos = new java.io.ByteArrayOutputStream();
bmp.compress(android.graphics.Bitmap.CompressFormat.JPEG, 85, baos);
bmp.recycle();

// 构造编码帧并推送
AoqClientEngine.AoqVideoEncodedFrame frame = new AoqClientEngine.AoqVideoEncodedFrame();
frame.codec     = AoqClientEngine.AoqVideoCodecType.AoqVideoCodecTypeJPEG;
frame.data      = baos.toByteArray();
frame.width     = width;
frame.height    = height;
frame.timeStamp = System.currentTimeMillis();

int ret = engine.pushExternalVideoEncodedFrame(
    AoqClientEngine.AoqTrackType.AoqTrackTypeVideo, frame);
```

<strong>AoqVideoEncodedFrame 参数说明：</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>codec</p></td><td><p>AoqVideoCodecType</p></td><td><p>AoqVideoCodecTypeJPEG</p></td><td><p>编码格式，目前仅支持 JPEG</p></td></tr><tr><td><p>data</p></td><td><p>byte\[]</p></td><td><p>null</p></td><td><p>编码后的数据</p></td></tr><tr><td><p>width</p></td><td><p>int</p></td><td><p>0</p></td><td><p>画面宽度（像素）</p></td></tr><tr><td><p>height</p></td><td><p>int</p></td><td><p>0</p></td><td><p>画面高度（像素）</p></td></tr><tr><td><p>timeStamp</p></td><td><p>long</p></td><td><p>0</p></td><td><p>时间戳（毫秒），为 0 时 SDK 使用本地时钟补充</p></td></tr></tbody></table>

### 3. 停止编码帧推送 <span id="custvid-h2-enc-step3" />

编码帧模式无需管理采集设备，停止推帧定时器即可。

```java
stopExternalFramePush();
```

## 视频帧格式参考 <span id="custvid-h2-ref" />

### AoqVideoFrame（原始帧模式使用） <span id="custvid-h2-ref-videoframe" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>format</p></td><td><p>AoqVideoPixelFormat</p></td><td><p>像素格式</p></td></tr><tr><td><p>width</p></td><td><p>int</p></td><td><p>画面宽度（像素）</p></td></tr><tr><td><p>height</p></td><td><p>int</p></td><td><p>画面高度（像素）</p></td></tr><tr><td><p>data</p></td><td><p>byte\[]</p></td><td><p>打包格式数据（NV12/NV21/BGRA/RGBA）</p></td></tr><tr><td><p>dataY</p></td><td><p>byte\[]</p></td><td><p>I420 Y 平面数据</p></td></tr><tr><td><p>dataU</p></td><td><p>byte\[]</p></td><td><p>I420 U 平面数据</p></td></tr><tr><td><p>dataV</p></td><td><p>byte\[]</p></td><td><p>I420 V 平面数据</p></td></tr><tr><td><p>strideY</p></td><td><p>int</p></td><td><p>I420 Y 平面行字节数</p></td></tr><tr><td><p>strideU</p></td><td><p>int</p></td><td><p>I420 U 平面行字节数</p></td></tr><tr><td><p>strideV</p></td><td><p>int</p></td><td><p>I420 V 平面行字节数</p></td></tr><tr><td><p>textureId</p></td><td><p>int</p></td><td><p>Android 纹理 ID（TextureOES/Texture2D）</p></td></tr><tr><td><p>transformMatrix</p></td><td><p>float\[]</p></td><td><p>纹理变换矩阵（4x4 行优先）</p></td></tr><tr><td><p>eglContext</p></td><td><p>EGLContext</p></td><td><p>Android 共享 EGL context（纹理模式使用）</p></td></tr><tr><td><p>pixelBuffer</p></td><td><p>CVPixelBufferRef</p></td><td><p>Apple 零拷贝 CVPixelBuffer（仅 iOS/macOS）</p></td></tr><tr><td><p>timeStamp</p></td><td><p>long</p></td><td><p>时间戳（毫秒），为 0 时 SDK 用本地时钟补充</p></td></tr></tbody></table>

### AoqVideoPixelFormat 枚举值 <span id="custvid-h2-ref-pixelfmt" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqVideoPixelFormatUnknown</p></td><td><p>0</p></td><td><p>未知格式</p></td></tr><tr><td><p>AoqVideoPixelFormatI420</p></td><td><p>1</p></td><td><p>I420 三平面格式</p></td></tr><tr><td><p>AoqVideoPixelFormatNV12</p></td><td><p>2</p></td><td><p>NV12 半平面格式（UV 交替）</p></td></tr><tr><td><p>AoqVideoPixelFormatNV21</p></td><td><p>3</p></td><td><p>NV21 半平面格式（VU 交替）</p></td></tr><tr><td><p>AoqVideoPixelFormatBGRA</p></td><td><p>4</p></td><td><p>BGRA 打包格式</p></td></tr><tr><td><p>AoqVideoPixelFormatRGBA</p></td><td><p>5</p></td><td><p>RGBA 打包格式</p></td></tr><tr><td><p>AoqVideoPixelFormatCVPixelBuffer</p></td><td><p>6</p></td><td><p>Apple CVPixelBuffer（仅 iOS/macOS）</p></td></tr><tr><td><p>AoqVideoPixelFormatTextureOES</p></td><td><p>7</p></td><td><p>Android OES 外部纹理</p></td></tr><tr><td><p>AoqVideoPixelFormatTexture2D</p></td><td><p>8</p></td><td><p>Android 2D 纹理</p></td></tr></tbody></table>

### AoqVideoEncodedFrame（编码帧模式使用） <span id="custvid-h2-ref-encframe" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>codec</p></td><td><p>AoqVideoCodecType</p></td><td><p>编码格式</p></td></tr><tr><td><p>data</p></td><td><p>byte\[]</p></td><td><p>编码后的数据</p></td></tr><tr><td><p>width</p></td><td><p>int</p></td><td><p>画面宽度（像素）</p></td></tr><tr><td><p>height</p></td><td><p>int</p></td><td><p>画面高度（像素）</p></td></tr><tr><td><p>timeStamp</p></td><td><p>long</p></td><td><p>时间戳（毫秒），为 0 时 SDK 用本地时钟补充</p></td></tr></tbody></table>

### AoqVideoCodecType 枚举值 <span id="custvid-h2-ref-codectype" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqVideoCodecTypeJPEG</p></td><td><p>0</p></td><td><p>JPEG 编码格式</p></td></tr></tbody></table>

## 注意事项 <span id="custvid-h2-notes" />

- 原始帧模式：必须先调用 `startVideoCapture(isExternal=true)` 再推送帧，否则 SDK 返回参数错误。
- 编码帧模式：只需调用 `setVideoEncoderConfig(isExternal=true)` 即可推送，<strong>不需要</strong>调用 `startVideoCapture`。
- 原始帧模式与编码帧模式不可混用：同一时间只能使用其中一种推送接口。
- 编码帧模式目前仅支持 JPEG 格式。
- 视频帧数据在推送后由 SDK 内部管理生命周期，调用方无需在推送后继续持有数据引用。
