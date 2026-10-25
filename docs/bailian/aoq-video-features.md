> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 视频常用功能介绍

> AOQ Client SDK 提供了完整的视频能力，覆盖视频采集、渲染显示、编码配置、帧数据回调、外部视频输入等核心场景。本文档基于 Android（Java）、iOS（Objective-C）、Ohos（ArkTS）三个平台的公开 API，对视频常用功能进行统一介绍。

<meta property="og:title" content="视频常用功能介绍" />

## 1. 视频采集 <span id="videofeat-s1h2" />

### 1.1 功能说明 <span id="videofeat-s1s1h3" />

视频采集用于打开设备摄像头，将实时视频帧数据送入 SDK 编码推流管线。SDK 支持两种采集模式：

- <strong>内部采集（默认）</strong>：SDK 自动管理摄像头设备的打开、帧采集和关闭，支持前后置摄像头切换。
- <strong>外部采集</strong>：由应用自行管理摄像头或其他视频源，采集到的帧数据通过 `pushExternalVideoCapturedFrame` 接口输入 SDK。

### 1.2 采集配置参数 <span id="videofeat-s1s2h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "45%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>width</p></td><td><p>int</p></td><td><p>1280</p></td><td><p>采集宽度（像素），外部采集时无效</p></td></tr><tr><td><p>height</p></td><td><p>int</p></td><td><p>720</p></td><td><p>采集高度（像素），外部采集时无效</p></td></tr><tr><td><p>fps</p></td><td><p>int</p></td><td><p>15</p></td><td><p>采集帧率，外部采集时由送帧节奏决定</p></td></tr><tr><td><p>isExternal</p></td><td><p>bool</p></td><td><p>false</p></td><td><p>是否使用外部采集模式</p></td></tr><tr><td><p>cameraDirection</p></td><td><p>AoqCameraDirection</p></td><td><p>Front(0)</p></td><td><p>摄像头方向，外部采集时无效</p></td></tr></tbody></table>

### 1.3 摄像头方向枚举 <span id="videofeat-s1s3h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqCameraDirectionFront</p></td><td><p>0</p></td><td><p>前置摄像头</p></td></tr><tr><td><p>AoqCameraDirectionBack</p></td><td><p>1</p></td><td><p>后置摄像头</p></td></tr></tbody></table>

### 1.4 API 对照 <span id="videofeat-s1s4h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>开启采集</p></td><td><p>startVideoCapture(config)</p></td><td><p>startVideoCapture:config:</p></td><td><p>startVideoCapture(config)</p></td></tr><tr><td><p>关闭采集</p></td><td><p>stopVideoCapture()</p></td><td><p>stopVideoCapture</p></td><td><p>stopVideoCapture()</p></td></tr><tr><td><p>切换摄像头</p></td><td><p>switchCamera(direction)</p></td><td><p>switchCamera:</p></td><td><p>switchCamera(direction)</p></td></tr></tbody></table>

### 1.5 使用示例 <span id="videofeat-s1s5h3" />

<strong>Android</strong>

```java
AoqVideoCaptureConfig config = new AoqVideoCaptureConfig();
config.width = 1280;
config.height = 720;
config.fps = 15;
config.cameraDirection = AoqCameraDirection.AoqCameraDirectionFront;
engine.startVideoCapture(config);
```

<strong>iOS</strong>

```objectivec
AoqVideoCaptureConfig *config = [[AoqVideoCaptureConfig alloc] init];
config.width = 1280;
config.height = 720;
config.fps = 15;
config.cameraDirection = AoqCameraDirectionFront;
[engine startVideoCapture:config];
```

<strong>Ohos</strong>

```javascript
let config: AoqVideoCaptureConfig = {
  width: 1280,
  height: 720,
  fps: 15,
  cameraDirection: AoqCameraDirection.AoqCameraDirectionFront
};
engine.startVideoCapture(config);
```

## 2. 视频渲染 <span id="videofeat-s2h2" />

### 2.1 功能说明 <span id="videofeat-s2s1h3" />

视频渲染用于将本地采集或远端接收的视频帧数据显示到屏幕上。SDK 支持设置本地预览窗口和远端渲染窗口，通过 `trackType` 区分视频流（Video）和屏幕共享流（Screen）。

### 2.2 渲染模式 <span id="videofeat-s2s2h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqRenderModeAuto</p></td><td><p>0</p></td><td><p>自动模式</p></td></tr><tr><td><p>AoqRenderModeStretch</p></td><td><p>1</p></td><td><p>拉伸平铺，画面可能变形</p></td></tr><tr><td><p>AoqRenderModeFill</p></td><td><p>2</p></td><td><p>填充黑边，画面完整显示</p></td></tr><tr><td><p>AoqRenderModeCrop</p></td><td><p>3</p></td><td><p>裁剪模式，画面内容可能丢失</p></td></tr></tbody></table>

### 2.3 画布配置 <span id="videofeat-s2s3h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15%" }} /><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>view</p></td><td><p>平台视图</p></td><td><p>null</p></td><td><p>渲染视图（Android: SurfaceView/TextureView, iOS: UIView, Ohos: XComponent）</p></td></tr><tr><td><p>renderMode</p></td><td><p>AoqRenderMode</p></td><td><p>Auto(0)</p></td><td><p>渲染显示模式</p></td></tr></tbody></table>

### 2.4 API 对照 <span id="videofeat-s2s4h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>设置本地预览</p></td><td><p>setLocalView(trackType, canvas)</p></td><td><p>setLocalView:trackType:canvas:</p></td><td><p>setLocalView(trackType, canvas)</p></td></tr><tr><td><p>设置远端渲染</p></td><td><p>setRemoteView(trackType, canvas)</p></td><td><p>setRemoteView:trackType:canvas:</p></td><td><p>setRemoteView(trackType, canvas)</p></td></tr></tbody></table>

<Note>
  <strong>平台差异</strong>：Android 使用 SurfaceView 或 TextureView 作为渲染容器；iOS 使用 UIView（内部通过 AoqRenderView 封装，支持 Metal 加速）；Ohos 使用 XComponent（通过 AoqXComponentController 管理 native 渲染视图）。
</Note>

## 3. 视频编码配置 <span id="videofeat-s3h2" />

### 3.1 功能说明 <span id="videofeat-s3s1h3" />

设置视频编码参数，包括编码格式、分辨率、帧率、码率、关键帧间隔、镜像和方向等。通过 `trackType` 区分视频轨道和屏幕共享轨道的编码配置。

### 3.2 编码配置参数 <span id="videofeat-s3s2h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "45%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>trackType</p></td><td><p>AoqTrackType</p></td><td><p>Video(1)</p></td><td><p>轨道类型：Video</p></td></tr><tr><td><p>codecType</p></td><td><p>AoqEncoderType</p></td><td><p>VideoH264(3)</p></td><td><p>编码格式</p></td></tr><tr><td><p>width</p></td><td><p>int</p></td><td><p>720</p></td><td><p>编码宽度</p></td></tr><tr><td><p>height</p></td><td><p>int</p></td><td><p>1280</p></td><td><p>编码高度</p></td></tr><tr><td><p>fps</p></td><td><p>int</p></td><td><p>5</p></td><td><p>编码帧率</p></td></tr><tr><td><p>bitrate</p></td><td><p>int</p></td><td><p>500000</p></td><td><p>目标码率（bps）</p></td></tr><tr><td><p>minBitrate</p></td><td><p>int</p></td><td><p>128000</p></td><td><p>最小码率（bps）</p></td></tr><tr><td><p>keyframeInterval</p></td><td><p>int</p></td><td><p>2</p></td><td><p>关键帧间隔（秒）</p></td></tr><tr><td><p>mirrorMode</p></td><td><p>AoqMirrorMode</p></td><td><p>Disabled(0)</p></td><td><p>镜像模式</p></td></tr><tr><td><p>orientationMode</p></td><td><p>AoqOrientationMode</p></td><td><p>Auto(0)</p></td><td><p>方向模式</p></td></tr><tr><td><p>isExternal</p></td><td><p>bool</p></td><td><p>false</p></td><td><p>外部编码模式（true 时由应用推送已编码帧）</p></td></tr></tbody></table>

### 3.3 编码格式枚举 <span id="videofeat-s3s3h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqEncoderTypeVideoH264</p></td><td><p>3</p></td><td><p>H.264 编码</p></td></tr><tr><td><p>AoqEncoderTypeVideoJpeg</p></td><td><p>4</p></td><td><p>JPEG 编码（用于外部编码帧）</p></td></tr></tbody></table>

### 3.4 镜像模式 <span id="videofeat-s3s4h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqMirrorModeDisabled</p></td><td><p>0</p></td><td><p>禁用镜像</p></td></tr><tr><td><p>AoqMirrorModeEnabled</p></td><td><p>1</p></td><td><p>启用镜像</p></td></tr></tbody></table>

### 3.5 方向模式 <span id="videofeat-s3s5h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqOrientationModeAuto</p></td><td><p>0</p></td><td><p>自动方向</p></td></tr><tr><td><p>AoqOrientationModePortrait</p></td><td><p>1</p></td><td><p>竖屏方向</p></td></tr><tr><td><p>AoqOrientationModeLandscape</p></td><td><p>2</p></td><td><p>横屏方向</p></td></tr></tbody></table>

### 3.6 API 对照 <span id="videofeat-s3s6h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>设置编码参数</p></td><td><p>setVideoEncoderConfig(config)</p></td><td><p>setVideoEncoderConfig:</p></td><td><p>setVideoEncoderConfig(config)</p></td></tr></tbody></table>

## 4. 外部视频帧输入 <span id="videofeat-s4h2" />

### 4.1 功能说明 <span id="videofeat-s4s1h3" />

外部视频帧输入允许应用将自定义的视频帧数据推送到 SDK，用于外部采集或外部编码场景。支持两种推送方式：

- <strong>推送原始帧</strong>：将未编码的像素数据（I420/NV12/NV21/BGRA/RGBA 等格式）推送给 SDK，由 SDK 进行编码。
- <strong>推送已编码帧</strong>：将已编码的数据（如 JPEG）直推给 SDK，SDK 不做二次编码，直接打包发送。

通过 `trackType` 路由，`AoqTrackTypeVideo` 对应视频采集的外部帧，`AoqTrackTypeScreen` 对应屏幕共享的外部帧。

### 4.2 像素格式枚举 <span id="videofeat-s4s2h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "10%" }} /><col style={{ width: "25%" }} /><col style={{ width: "35%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th><th><p><strong>平台支持</strong></p></th></tr></thead><tbody><tr><td><p>AoqVideoPixelFormatI420</p></td><td><p>1</p></td><td><p>I420 三平面</p></td><td><p>全平台</p></td></tr><tr><td><p>AoqVideoPixelFormatNV12</p></td><td><p>2</p></td><td><p>NV12 双平面</p></td><td><p>全平台</p></td></tr><tr><td><p>AoqVideoPixelFormatNV21</p></td><td><p>3</p></td><td><p>NV21 双平面</p></td><td><p>全平台</p></td></tr><tr><td><p>AoqVideoPixelFormatBGRA</p></td><td><p>4</p></td><td><p>BGRA 打包</p></td><td><p>全平台</p></td></tr><tr><td><p>AoqVideoPixelFormatRGBA</p></td><td><p>5</p></td><td><p>RGBA 打包</p></td><td><p>全平台</p></td></tr><tr><td><p>AoqVideoPixelFormatCVPixelBuffer</p></td><td><p>6</p></td><td><p>Apple 零拷贝</p></td><td><p>仅 iOS</p></td></tr><tr><td><p>AoqVideoPixelFormatTextureOES</p></td><td><p>7</p></td><td><p>OES 纹理</p></td><td><p>仅 Android</p></td></tr><tr><td><p>AoqVideoPixelFormatTexture2D</p></td><td><p>8</p></td><td><p>2D 纹理</p></td><td><p>仅 Android</p></td></tr></tbody></table>

### 4.3 原始视频帧数据结构 (AoqVideoFrame) <span id="videofeat-s4s3h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>format</p></td><td><p>AoqVideoPixelFormat</p></td><td><p>像素格式</p></td></tr><tr><td><p>width</p></td><td><p>int</p></td><td><p>宽度（像素）</p></td></tr><tr><td><p>height</p></td><td><p>int</p></td><td><p>高度（像素）</p></td></tr><tr><td><p>data</p></td><td><p>byte\[] / ArrayBuffer</p></td><td><p>打包格式数据（NV12/NV21/BGRA/RGBA）</p></td></tr><tr><td><p>dataY / dataU / dataV</p></td><td><p>byte\[] / ArrayBuffer</p></td><td><p>I420 三平面数据</p></td></tr><tr><td><p>strideY / strideU / strideV</p></td><td><p>int</p></td><td><p>I420 三平面步长</p></td></tr><tr><td><p>textureId</p></td><td><p>int</p></td><td><p>纹理 ID（Android TextureOES/Texture2D 时有效）</p></td></tr><tr><td><p>transformMatrix</p></td><td><p>float\[16]</p></td><td><p>4x4 纹理变换矩阵（Android）</p></td></tr><tr><td><p>eglContext</p></td><td><p>EGLContext</p></td><td><p>共享 EGL 上下文（Android）</p></td></tr><tr><td><p>pixelBuffer</p></td><td><p>CVPixelBufferRef</p></td><td><p>Apple 零拷贝（iOS）</p></td></tr><tr><td><p>timeStamp</p></td><td><p>long</p></td><td><p>时间戳（ms），0 时 SDK 用本地时钟补</p></td></tr></tbody></table>

### 4.4 已编码视频帧数据结构 (AoqVideoEncodedFrame) <span id="videofeat-s4s4h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "25%" }} /><col style={{ width: "15%" }} /><col style={{ width: "40%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>codec</p></td><td><p>AoqVideoCodecType</p></td><td><p>JPEG(0)</p></td><td><p>编码格式</p></td></tr><tr><td><p>data</p></td><td><p>byte\[] / ArrayBuffer</p></td><td><p>-</p></td><td><p>编码后数据</p></td></tr><tr><td><p>width</p></td><td><p>int</p></td><td><p>-</p></td><td><p>宽度（像素）</p></td></tr><tr><td><p>height</p></td><td><p>int</p></td><td><p>-</p></td><td><p>高度（像素）</p></td></tr><tr><td><p>timeStamp</p></td><td><p>long</p></td><td><p>0</p></td><td><p>时间戳（ms）</p></td></tr></tbody></table>

### 4.5 API 对照 <span id="videofeat-s4s5h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>推送原始帧</p></td><td><p>pushExternalVideoCapturedFrame(trackType, frame)</p></td><td><p>pushExternalVideoCapturedFrame:frame:</p></td><td><p>pushExternalVideoCapturedFrame(trackType, frame)</p></td></tr><tr><td><p>推送已编码帧</p></td><td><p>pushExternalVideoEncodedFrame(trackType, frame)</p></td><td><p>pushExternalVideoEncodedFrame:frame:</p></td><td><p>pushExternalVideoEncodedFrame(trackType, frame)</p></td></tr></tbody></table>

## 5. 视频帧数据回调 <span id="videofeat-s5h2" />

### 5.1 功能说明 <span id="videofeat-s5s1h3" />

视频帧回调允许开发者在视频管线的不同位置获取原始帧数据，用于视频分析、自定义处理、录制等场景。支持只读和读写两种模式，读写模式下可修改帧数据并写回 SDK。

### 5.2 支持的数据源位置 <span id="videofeat-s5s2h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "65%" }} /></colgroup><thead><tr><th><p><strong>数据源</strong></p></th><th><p><strong>枚举值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Captured</p></td><td><p>0</p></td><td><p>采集后的视频数据（前处理前）</p></td></tr><tr><td><p>PreEncode</p></td><td><p>1</p></td><td><p>编码前的视频数据（前处理后）</p></td></tr><tr><td><p>Remote</p></td><td><p>2</p></td><td><p>远端解码后、渲染前的视频数据</p></td></tr></tbody></table>

### 5.3 回调配置参数 <span id="videofeat-s5s3h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "25%" }} /><col style={{ width: "15%" }} /><col style={{ width: "40%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>format</p></td><td><p>AoqVideoPixelFormat</p></td><td><p>I420(1)</p></td><td><p>期望回调的像素格式</p></td></tr><tr><td><p>alignment</p></td><td><p>AoqVideoObserverAlignment</p></td><td><p>Default(0)</p></td><td><p>宽度对齐策略</p></td></tr><tr><td><p>mode</p></td><td><p>AoqVideoObserverMode</p></td><td><p>ReadOnly(0)</p></td><td><p>只读(0)/读写(1) 模式</p></td></tr><tr><td><p>mirrorApplied</p></td><td><p>bool</p></td><td><p>false</p></td><td><p>是否对回调数据应用镜像</p></td></tr></tbody></table>

### 5.4 宽度对齐枚举 <span id="videofeat-s5s4h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "40%" }} /><col style={{ width: "15%" }} /><col style={{ width: "45%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqVideoObserverAlignmentDefault</p></td><td><p>0</p></td><td><p>默认对齐</p></td></tr><tr><td><p>AoqVideoObserverAlignmentEven</p></td><td><p>1</p></td><td><p>2 字节对齐</p></td></tr><tr><td><p>AoqVideoObserverAlignment4</p></td><td><p>2</p></td><td><p>4 字节对齐</p></td></tr><tr><td><p>AoqVideoObserverAlignment8</p></td><td><p>3</p></td><td><p>8 字节对齐</p></td></tr><tr><td><p>AoqVideoObserverAlignment16</p></td><td><p>4</p></td><td><p>16 字节对齐</p></td></tr></tbody></table>

### 5.5 使用步骤 <span id="videofeat-s5s5h3" />

1. <strong>注册观察者</strong>：调用 `setVideoFrameObserver` 设置视频帧回调监听器
2. <strong>启用数据源</strong>：调用 `enableVideoFrameObserver` 选择需要监听的数据源位置，开启回调
3. <strong>处理回调数据</strong>：在回调函数中获取帧数据（仅回调期间有效，异步使用需自行拷贝）

### 5.6 API 对照 <span id="videofeat-s5s6h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>注册观察者</p></td><td><p>setVideoFrameObserver(listener)</p></td><td><p>setVideoFrameObserver:</p></td><td><p>setVideoFrameObserver(observer)</p></td></tr><tr><td><p>启用回调</p></td><td><p>enableVideoFrameObserver(enabled, source, config)</p></td><td><p>enableVideoFrameObserver:videoSource:config:</p></td><td><p>enableVideoFrameObserver(enabled, source, config)</p></td></tr></tbody></table>

### 5.7 回调方法 <span id="videofeat-s5s7h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>回调</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>采集后数据</p></td><td><p>onCapturedVideoFrame(frame)</p></td><td><p>onCapturedVideoFrame:</p></td><td><p>onCapturedVideoFrame(frame)</p></td></tr><tr><td><p>编码前数据</p></td><td><p>onPreEncodeVideoFrame(trackType, frame)</p></td><td><p>onPreEncodeVideoFrame:frame:</p></td><td><p>onPreEncodeVideoFrame(trackType, frame)</p></td></tr><tr><td><p>远端数据</p></td><td><p>onRemoteVideoFrame(trackType, frame)</p></td><td><p>onRemoteVideoFrame:frame:</p></td><td><p>onRemoteVideoFrame(trackType, frame)</p></td></tr></tbody></table>

<Note>
  回调方法返回 `true`/`YES` 表示数据已修改、需写回 SDK（仅 ReadWrite 模式且 I420 格式时生效）。
</Note>

## 7. 媒体流发送控制 <span id="videofeat-s7h2" />

### 7.1 功能说明 <span id="videofeat-s7s1h3" />

控制本地媒体流的发送开关，通过 `trackType` 路由到不同轨道（Audio/Video/Screen）。停用发送后，采集和编码继续运行，但数据不会发送到远端。

### 7.2 API 对照 <span id="videofeat-s7s2h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>功能</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>控制流发送</p></td><td><p>enableSendMediaStream(trackType, enable)</p></td><td><p>enableSendMediaStream:enable:</p></td><td><p>enableSendMediaStream(trackType, enable)</p></td></tr></tbody></table>

### 7.3 轨道类型枚举 <span id="videofeat-s7s3h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>枚举值</strong></p></th><th><p><strong>数值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqTrackTypeAudio</p></td><td><p>0</p></td><td><p>音频轨道</p></td></tr><tr><td><p>AoqTrackTypeVideo</p></td><td><p>1</p></td><td><p>视频轨道</p></td></tr><tr><td><p>AoqTrackTypeData</p></td><td><p>2</p></td><td><p>数据轨道</p></td></tr></tbody></table>

## 8. 视频设备状态监控 <span id="videofeat-s8h2" />

### 8.1 功能说明 <span id="videofeat-s8s1h3" />

SDK 自动监测视频采集设备（摄像头）的状态变化，并通过 `onVideoDeviceStateChanged` 回调通知应用层。

### 8.2 设备状态码 <span id="videofeat-s8s2h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "35%" }} /><col style={{ width: "10%" }} /><col style={{ width: "55%" }} /></colgroup><thead><tr><th><p><strong>状态码</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqVideoDeviceNone</p></td><td><p>0</p></td><td><p>初始状态</p></td></tr><tr><td><p>AoqVideoDeviceCaptureStarting</p></td><td><p>1</p></td><td><p>采集启动中</p></td></tr><tr><td><p>AoqVideoDeviceCaptureStarted</p></td><td><p>2</p></td><td><p>采集已启动</p></td></tr><tr><td><p>AoqVideoDeviceCaptureStopping</p></td><td><p>3</p></td><td><p>采集停止中</p></td></tr><tr><td><p>AoqVideoDeviceCaptureStopped</p></td><td><p>4</p></td><td><p>采集已停止</p></td></tr><tr><td><p>AoqVideoDeviceCaptureFail</p></td><td><p>5</p></td><td><p>采集失败</p></td></tr></tbody></table>

### 8.3 回调对照 <span id="videofeat-s8s3h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /><col style={{ width: "28%" }} /></colgroup><thead><tr><th><p><strong>回调</strong></p></th><th><p><strong>Android</strong></p></th><th><p><strong>iOS</strong></p></th><th><p><strong>Ohos</strong></p></th></tr></thead><tbody><tr><td><p>设备状态变化</p></td><td><p>onVideoDeviceStateChanged(state)</p></td><td><p>onVideoDeviceStateChanged:</p></td><td><p>onVideoDeviceStateChanged(state)</p></td></tr></tbody></table>

## 9. 视频错误码与警告码 <span id="videofeat-s9h2" />

### 9.1 视频错误码 <span id="videofeat-s9s1h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "10%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th><p><strong>错误码</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqErrorCodeVideo</p></td><td><p>200</p></td><td><p>通用视频错误</p></td></tr><tr><td><p>VideoExternalBufferFull</p></td><td><p>210</p></td><td><p>视频外部缓冲区已满</p></td></tr><tr><td><p>VideoDevice</p></td><td><p>220</p></td><td><p>视频设备通用错误</p></td></tr><tr><td><p>CameraOpenFail</p></td><td><p>221</p></td><td><p>摄像头打开失败</p></td></tr><tr><td><p>CameraAuthFailed</p></td><td><p>222</p></td><td><p>摄像头权限被拒绝</p></td></tr><tr><td><p>CameraOccupied</p></td><td><p>223</p></td><td><p>摄像头被占用</p></td></tr><tr><td><p>CameraRunningError</p></td><td><p>224</p></td><td><p>摄像头运行错误</p></td></tr><tr><td><p>VideoCodec</p></td><td><p>230</p></td><td><p>视频编解码通用错误</p></td></tr><tr><td><p>EncoderInitFail</p></td><td><p>231</p></td><td><p>编码器初始化失败</p></td></tr><tr><td><p>VideoRender</p></td><td><p>240</p></td><td><p>视频渲染通用错误</p></td></tr><tr><td><p>RenderCreateFail</p></td><td><p>241</p></td><td><p>渲染器创建失败</p></td></tr><tr><td><p>RenderDrawError</p></td><td><p>242</p></td><td><p>渲染绘制错误</p></td></tr><tr><td><p>Screen</p></td><td><p>300</p></td><td><p>屏幕共享通用错误</p></td></tr></tbody></table>

<Note>
  Android 额外错误码：ScreenPermissionDenied(310) 屏幕共享权限被拒绝、ScreenForegroundServiceFailed(311) 前台服务启动失败。
</Note>

### 9.2 视频警告码 <span id="videofeat-s9s2h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "10%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th><p><strong>警告码</strong></p></th><th><p><strong>值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>AoqWCVideo</p></td><td><p>200</p></td><td><p>通用视频警告</p></td></tr><tr><td><p>CameraEnumerateError</p></td><td><p>201</p></td><td><p>摄像头枚举错误</p></td></tr><tr><td><p>EncoderSwitched</p></td><td><p>202</p></td><td><p>编码器切换警告</p></td></tr><tr><td><p>RenderDowngrade</p></td><td><p>203</p></td><td><p>渲染降级警告</p></td></tr></tbody></table>

## 附录：完整视频 API 方法列表 <span id="videofeat-sappendixh2" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15%" }} /><col style={{ width: "30%" }} /><col style={{ width: "55%" }} /></colgroup><thead><tr><th><p><strong>分类</strong></p></th><th><p><strong>方法名</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>采集控制</p></td><td><p>startVideoCapture</p></td><td><p>打开视频采集设备</p></td></tr><tr><td><p>采集控制</p></td><td><p>stopVideoCapture</p></td><td><p>关闭视频采集设备</p></td></tr><tr><td><p>采集控制</p></td><td><p>switchCamera</p></td><td><p>切换前后置摄像头</p></td></tr><tr><td><p>渲染控制</p></td><td><p>setLocalView</p></td><td><p>设置本地预览窗口</p></td></tr><tr><td><p>渲染控制</p></td><td><p>setRemoteView</p></td><td><p>设置远端渲染窗口</p></td></tr><tr><td><p>编解码</p></td><td><p>setVideoEncoderConfig</p></td><td><p>设置视频编码参数</p></td></tr><tr><td><p>外部输入</p></td><td><p>pushExternalVideoCapturedFrame</p></td><td><p>推送原始视频帧</p></td></tr><tr><td><p>外部输入</p></td><td><p>pushExternalVideoEncodedFrame</p></td><td><p>推送已编码视频帧</p></td></tr><tr><td><p>屏幕共享</p></td><td><p>startScreenCapture</p></td><td><p>启动屏幕采集</p></td></tr><tr><td><p>屏幕共享</p></td><td><p>stopScreenCapture</p></td><td><p>停止屏幕采集</p></td></tr><tr><td><p>流控制</p></td><td><p>enableSendMediaStream</p></td><td><p>控制媒体流发送</p></td></tr><tr><td><p>帧回调</p></td><td><p>setVideoFrameObserver</p></td><td><p>注册视频帧观察者</p></td></tr><tr><td><p>帧回调</p></td><td><p>enableVideoFrameObserver</p></td><td><p>启用/禁用视频帧回调</p></td></tr></tbody></table>
