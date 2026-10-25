> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 连接状态管理

> 本章节介绍 AOQ Client SDK 的连接状态机及其对应的 API 调用。

<meta property="og:title" content="连接状态管理" />

## 连接状态图 <span id="connmgr-h2-state-diagram" />

下图描述了 AOQ Client SDK 的连接状态迁移关系：

AOQ Client SDK 连接状态迁移图

<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3811364871/p1088081.svg" alt="111" width={415.422} />

## 状态说明 <span id="connmgr-h2-status-desc" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.33%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "33.34%" }} /></colgroup><thead><tr><th><p><strong>状态</strong></p></th><th><p><strong>枚举值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>链接中（Connecting）</p></td><td><p>1</p></td><td><p>调用 <code>connect</code> 后进入，正在与 AI Service 建立连接</p></td></tr><tr><td><p>已连接（Connected）</p></td><td><p>2</p></td><td><p>连接建立成功，可正常收发音视频和数据消息</p></td></tr><tr><td><p>失败（Failed）</p></td><td><p>3</p></td><td><p>连接异常（鉴权失败/超时/服务端拒绝等），SDK 内部会自动迁移到已断开</p></td></tr><tr><td><p>已断开（Disconnected）</p></td><td><p>0</p></td><td><p>初始状态/主动断开/异常断开后的终态</p></td></tr></tbody></table>

## 状态迁移规则 <span id="connmgr-h2-transition-rules" />

1. <strong>App 调用</strong> `connect` → 进入 `Connecting` 状态。
2. <strong>链接成功</strong> → 从 `Connecting` 迁移到 `Connected`。
3. <strong>链接异常</strong> → 从 `Connecting` 迁移到 `Failed`，随后 SDK 自动迁移到 `Disconnected`。
4. <strong>链接异常</strong> → 从 `Connected` 迁移到 `Failed`，随后 SDK 自动迁移到 `Disconnected`。
5. <strong>App 调用</strong> `disconnect` → 从 `Connected` 迁移到 `Disconnected`。

<Note>
  <strong>说明</strong>：`Failed` 是瞬态，SDK 触发 `onConnectionStatusChange(Failed)` 后会自动迁移到 `Disconnected`，业务层无需手动调用 disconnect。
</Note>

## connect API <span id="connmgr-h2-connect-api" />

调用 `connect` 发起与 AI Service 的连接，传入由 AppServer allocate 接口返回的鉴权凭证。

### 方法签名 <span id="connmgr-h2-connect-signature" />

<strong>Android：</strong>

```java
public abstract int connect(@NonNull AoqConnectConfig config);
```

<strong>iOS：</strong>

```objectivec
- (int)connect:(AoqConnectConfig * _Nonnull)config;
```

<strong>Ohos：</strong>

```javascript
connect(config: AoqConnectConfig): number;
```

<strong>返回值：</strong>`0` 表示调用成功（异步建连）；`< 0` 表示失败。

### 行为说明 <span id="connmgr-h2-connect-behavior" />

- 调用后触发 `onConnectionStatusChange(connecting)` 回调。
- 如果链接成功时触发 `onConnectionStatusChange(connected)` 回调。
- 如果链接失败时触发 `onConnectionStatusChange(failed)` 回调。

## disconnect API <span id="connmgr-h2-disconnect-api" />

调用 `disconnect` 主动断开与 AI Service 的连接。

### 方法签名 <span id="connmgr-h2-disconnect-signature" />

<strong>Android：</strong>

```java
public abstract int disconnect();
```

<strong>iOS：</strong>

```objectivec
- (int)disconnect;
```

<strong>Ohos：</strong>

```javascript
disconnect(): number;
```

<strong>返回值：</strong>`0` 表示成功；`< 0` 表示失败。

### 行为说明 <span id="connmgr-h2-disconnect-behavior" />

- 调用后触发 `onConnectionStatusChange(Disconnected)` 回调。
- 引擎不会自动释放，可重新调用 `connect` 进行重连。
- 未连接状态下调用 `disconnect` 是安全的，返回 0。

## onConnectionStatusChange 回调 <span id="connmgr-h2-callback" />

连接状态变化时，SDK 通过此回调通知业务层。

<strong>Android：</strong>

```java
public void onConnectionStatusChange(
    @NonNull AoqClientEngine.AoqConnectionStatus status) {}
```

<strong>iOS：</strong>

```objectivec
- (void)onConnectionStatusChange:(AoqConnectionStatus)status;
```

<strong>Ohos：</strong>

```javascript
onConnectionStatusChange?: (status: AoqConnectionStatus) => void;
```

### 示例代码 <span id="connmgr-h2-callback-example" />

```swift
func onConnectionStatusChange(_ status: AoqConnectionStatus) {
    switch status {
    case .connecting:
        print("正在连接...")
    case .connected:
        print("连接成功")
        // 连接成功后可发送 session.update
    case .failed:
        print("连接失败")
        // SDK 会自动迁移到 disconnected，无需手动 disconnect
    case .disconnected:
        print("已断开")
        // 可根据业务决定是否重连
    }
}
```
