> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# WebSocket 接入指南

> 本文介绍通过 WebSocket 连接访问 Paraformer 实时语音识别服务的接口地址、请求头和交互流程。

<meta property="og:title" content="WebSocket 接入指南" />

<Tip>
  阿里云百炼为华北2（北京）地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`。

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

<strong>用户指南：</strong>关于模型介绍和选型建议请参见[语音识别](/zh/model-studio/asr-model)，示例代码请参见[实时语音识别](/zh/model-studio/real-time-speech-recognition-user-guide)。

DashScope SDK 目前仅支持 Java 和 Python。使用其他编程语言时，可通过 WebSocket 连接与服务进行通信。

## 接口地址 <span id="f8ab6424cbcdk" />

Paraformer仅支持在华北2（北京）地域使用。WebSocket URL 固定如下：

`wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference`

调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tip>
  URL 必须使用 `wss://` 协议，且固定不变。Authorization 在请求头中设置（参见[请求头](/zh/model-studio/websocket-for-paraformer-real-time-service#f5b7af17168sz)）。
</Tip>

## 请求头 <span id="f5b7af17168sz" />

请求头中需添加如下信息：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "8.54%" }} /><col style={{ width: "10.31%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Authorization</p></td><td><p>string</p></td><td><p>是</p></td><td><p>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时，将“<code>\<your\_api\_key></code>”替换为实际的API Key。</p></td></tr><tr><td><p>user-agent</p></td><td><p>string</p></td><td><p>否</p></td><td><p>客户端标识，便于服务端追踪来源。</p></td></tr><tr><td><p>X-DashScope-WorkSpace</p></td><td><p>string</p></td><td><p>否</p></td><td><p>阿里云百炼<a href="/zh/model-studio/use-workspace">业务空间ID</a>。</p></td></tr><tr><td><p>X-DashScope-DataInspection</p></td><td><p>string</p></td><td><p>否</p></td><td><p>是否启用数据合规检测功能。默认不传或设为<code>enable</code>。如非必要，请勿启用该参数。</p></td></tr></tbody></table>

<Tip>
  Authorization 鉴权在 WebSocket 握手阶段验证。如果 API Key 无效或缺失，握手将失败并返回 HTTP 401/403 错误。
</Tip>

## 交互流程 <span id="62149ca39bx2i" />

客户端事件和服务端事件的详细说明，请参见[客户端事件](/zh/model-studio/paraformer-client-events)和[服务端事件](/zh/model-studio/paraformer-server-events)。

<img src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8352835871/CAEQURiBgMCczta5pxkiIGY0N2Q2YjIwZTM1MTQyNTY4ZmFkY2MwN2JmOTllODFl4709861_20241015153444.149.svg" alt="image" />

按时间顺序，客户端与服务端的交互流程如下：

1. 建立连接：客户端与服务端建立 WebSocket 连接。
2. 开启任务：客户端发送 run-task 指令以开启任务，并接收服务端返回的 task-started 事件，标志着任务已成功开启，可以进行后续步骤。
3. 发送音频流：客户端开始发送二进制音频（须为单声道音频），并同时接收服务端持续返回的 result-generated 事件，该事件包含语音识别结果。
4. 通知服务端结束任务：客户端发送 finish-task 指令通知服务端结束任务，并继续接收服务端返回的 result-generated 事件。
5. 任务结束：客户端收到服务端返回的 task-finished 事件，标志着任务结束。
6. 关闭连接：客户端关闭 WebSocket 连接。
