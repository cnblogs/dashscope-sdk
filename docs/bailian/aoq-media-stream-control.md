> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 媒体流发送管理

> enableSendMediaStream 用于控制客户端是否向 AI 服务发送音频/视频媒体流，在 AOQ 协议接入场景中精确控制发送时机。

<meta property="og:title" content="媒体流发送管理" />

## 概述 <span id="mstream-h2-overview" />

`enableSendMediaStream` 用于控制客户端是否向 AI 服务发送音频/视频媒体流。在 AOQ 协议接入场景中，部分模型要求在收到 `session.updated` 确认后才能接收媒体数据，因此需要通过此接口精确控制发送时机。

## API 定义 <span id="mstream-h2-api" />

<strong>iOS / Mac</strong>

```swift
// iOS / Mac
func enableSendMediaStream(_ trackType: AoqTrackType, enable: Bool)
```

<strong>Android</strong>

```java
// Android
void enableSendMediaStream(AoqTrackType trackType, boolean enable)
```

<strong>OHOS (ArkTS)</strong>

```javascript
// OHOS (ArkTS)
enableSendMediaStream(trackType: AoqTrackType, enable: boolean): void
```

<strong>参数说明：</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>trackType</p></td><td><p>AoqTrackType</p></td><td><p>媒体轨道类型：<code>.audio</code> 或 <code>.video</code></p></td></tr><tr><td><p>enable</p></td><td><p>Bool</p></td><td><p><code>true</code> = 开启发送，<code>false</code> = 暂停发送</p></td></tr></tbody></table>

## 控制流程 <span id="mstream-h2-flow" />

典型的媒体流控制流程如下：

<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0575914871/p1088082.svg" alt="111" width={461} />

## 使用示例（iOS Swift） <span id="mstream-h2-example" />

### connect 前禁用发送 <span id="mstream-h2-example-before" />

```swift
// 连接前关闭音视频发送，避免模型未就绪时收到数据
engine.enableSendMediaStream(.audio, enable: false)
engine.enableSendMediaStream(.video, enable: false)

// 发起连接
engine.connect(config)
```

### 收到 session.updated 后开启 <span id="mstream-h2-example-after" />

```swift
func onDataMsg(_ msg: AoqDataMsg) {
    guard let obj = try? JSONSerialization.jsonObject(with: msg.data) as? [String: Any],
          let type = obj["type"] as? String else { return }

    if type == "session.updated" {
        // AI 侧已确认会话配置，开启媒体发送
        engine.enableSendMediaStream(.audio, enable: true)
        engine.enableSendMediaStream(.video, enable: true)
    }
}
```

## 注意事项 <span id="mstream-h2-notes" />

- <strong>调用时机</strong>：必须在 `createEngine` 之后调用，引擎未创建时调用无效。
- <strong>默认行为</strong>：如果不调用此接口，connect 成功后 SDK 会立即开始发送媒体流。
- <strong>模型兼容性</strong>：部分模型要求先收到 `session.updated` 再接收媒体数据，建议统一采用"先禁用、后开启"模式。
- <strong>独立控制</strong>：音频和视频可独立控制，例如仅发送音频不发送视频。

## 常见场景 <span id="mstream-h2-scenarios" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>场景</strong></p></th><th><p><strong>操作</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>连接模型前</p></td><td><p><code>enable(.audio, false)</code></p></td><td><p>等待 session.updated 再发送</p></td></tr><tr><td><p>收到 session.updated</p></td><td><p><code>enable(.audio, true)</code></p></td><td><p>AI 已就绪，开始发送</p></td></tr></tbody></table>
