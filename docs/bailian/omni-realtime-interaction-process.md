> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# WebSocket 接入指南

> 介绍 Qwen-Omni-Realtime 系列模型的 WebSocket 接入地址、交互流程及 SDK。

<meta property="og:title" content="WebSocket 接入指南" />

## 接入地址

适用模型：`qwen3.8-omni-flash-realtime`、`qwen3.5-omni-plus-realtime`、`qwen3.5-omni-flash-realtime`。

<Tabs>
  <Tab title="华北2（北京）">
    `wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/realtime?model=qwen3.5-omni-plus-realtime`
  </Tab>

  <Tab title="新加坡">
    `wss://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api-ws/v1/realtime?model=qwen3.5-omni-plus-realtime`
  </Tab>
</Tabs>

将 `{WorkspaceId}` 替换为对应地域的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)，将 `model` 替换为要调用的模型名称。鉴权和连接前的准备工作请参见[接入概览](/zh/model-studio/realtime-websocket-overview)。

## 接入流程

### VAD 模式 <span id="9b49887720jcw" />

将[客户端事件](/zh/model-studio/client-events)事件的`session.turn_detection.type` 设为`"server_vad"`以启用 VAD 模式。在 VAD 模式下，服务端对传入的音频进行语音活动检测，并在检测到作出响应。此模式适用于客户端到服务器始终发送音频的情况，也是当前的默认模式。

![server\_vad](https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0520773571/p991064.svg)

- 服务端在检测到语音开始时发送`input_audio_buffer.speech_started` 事件。
- 客户端随时可以选择通过发送 `input_audio_buffer.append` 事件将音频追加到缓冲区。
- 服务端在检测到语音结束时发送`input_audio_buffer.speech_stopped`事件。
- 服务端通过发送 `input_audio_buffer.committed` 事件来提交输入音频缓冲区。
- 服务端发送 `conversation.item.created` 事件，其中包含从音频缓冲区创建的用户消息项。

#### Function Calling 流程 <span id="613b113654ayb" />

在 VAD 模式下，当服务端生成的响应需要客户端执行 Function Calling 工具时，遵循以下交互流程：

![image.svg](https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3014236771/p1067613.svg)

- 服务端在检测到语音结束并生成响应时，识别到需要调用工具。
- 服务端发送 `response.function_call_arguments.delta` 事件，包含工具调用参数的增量数据。
- 服务端发送 `response.function_call_arguments.done` 事件，表示工具调用参数传递完成。
- 客户端执行工具调用并获取结果。
- 客户端通过 `conversation.item.create` 事件发送工具调用结果。
- 客户端发送 `response.create` 事件，触发服务端基于工具调用结果生成响应。

### Manual 模式 <span id="ee09a3493fsuc" />

将[客户端事件](/zh/model-studio/client-events)事件的`session.turn_detection` 设为 null 以启用 Manual 模式。此模式下，客户端通过显式发送`input_audio_buffer.commit` 和`response.create`事件请求服务器响应。适用于按下即说场景，如聊天软件中的发送语音。

![manual](https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0520773571/p991066.svg)

- 客户端可以通过发送 `input_audio_buffer.append` 事件将音频追加到缓冲区。
- 客户端通过发送 `input_audio_buffer.commit`事件来提交输入音频缓冲区。 该提交会在对话中创建一个新的用户消息项。
- 服务器通过发送 `input_audio_buffer.committed`事件进行响应。
- 客户端发送 `response.create` 事件，触发模型生成最终响应。
- 服务器通过发送 `conversation.item.created`事件进行响应。

#### Function Calling 流程 <span id="9c958b42d8xkl" />

![image.svg](https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3014236771/p1067740.svg)

在 Manual 模式下，当服务端生成的响应需要客户端执行 Function Calling 工具时，遵循以下交互流程：

- 客户端发送 `response.create` 事件后，服务端生成响应并识别到需要调用工具。
- 服务端发送 `response.function_call_arguments.delta` 事件，包含工具调用参数的增量数据。
- 服务端发送 `response.function_call_arguments.done` 事件，表示工具调用参数传递完成。
- 客户端执行工具调用并获取结果。
- 客户端通过 `conversation.item.create` 事件发送工具调用结果。
- 客户端发送 `response.create` 事件，触发模型生成最终响应。
- 服务端基于工具调用结果生成响应，并通过 `response.audio.delta` 或 `response.text.delta` 事件返回给客户端。

## MCP 工具调用 <span id="qwen38-mcp-flow" />

以下流程适用于 Qwen3.8-Omni-Flash-Realtime，可与上述 VAD 或 Manual 模式配合使用。

Function Calling 与 MCP 可以在同一会话的 `session.tools` 中配置，不能同时启用联网搜索（`enable_search`）。百炼不额外收取 MCP 工具调用费，模型推理仍按模型价格计费。

MCP 由服务端执行工具，前述 Function Calling 流程则由客户端执行工具，两者的结果回传方式不同。

### 工具发现

客户端通过 `session.update` 配置 MCP 服务后，由 Realtime 服务异步发现工具，交互流程如下：

<Image src="https://g-adoc.alcasset.com/media/maas_docs/sfm-cn/zh/images/6a4b3c2d1e0f92df.png" alt="MCP 工具发现时序图" width={558} />

本节时序图中，`opt` 表示满足条件时执行，`alt` 表示按条件选择不同分支，`par` 表示并行处理。

- `session.updated` 仅表示 MCP 配置已接受，不表示工具已经可用。
- `mcp_list_tools.in_progress` 可能早于 `session.updated` 到达。
- 客户端可以在工具发现完成前继续发送音频或 `response.create`；但只有发现完成后的 Response 才能使用对应 MCP 工具。
- 工具发现成功时，服务端先发送包含最终工具列表的 `conversation.item.created`，再发送 `mcp_list_tools.completed`。
- 工具发现失败时，服务端先发送包含 error 的 `conversation.item.created`，再发送 `mcp_list_tools.failed`。

### MCP 调用与续答

模型选择 MCP 工具后，由 Realtime 服务执行工具调用；需要审批时，客户端先回复审批结果。交互流程如下：

<Image src="https://g-adoc.alcasset.com/media/maas_docs/sfm-cn/zh/images/6a4b3c2d1e0f92de.png" alt="MCP 工具调用、审批与续答时序图" width={558} />

审批拒绝或超时会直接触发 `response.mcp_call.failed`，不执行工具，也不会经过 `response.mcp_call.in_progress`。无需审批或审批通过时，典型事件顺序如下：

```text
response.output_item.added（mcp_call）
→ response.mcp_call_arguments.delta
→ response.mcp_call_arguments.done
→ 可选：conversation.item.created（mcp_approval_request）
→ 可选：conversation.item.create（mcp_approval_response）
→ response.mcp_call.in_progress
→ response.mcp_call.completed 或 response.mcp_call.failed
→ response.output_item.done（最终 mcp_call）
→ response.done（父 Response）
```

服务端会在同一父 Response 的全部 MCP item 进入终态后发送 `response.done`。其中 `response.output` 已包含最终 mcp\_call 快照：成功项为 completed 并包含 output；失败项为 failed 并包含 error。

如果需要模型基于 MCP 结果继续生成回答，客户端应在收到父 `response.done` 后发送一次不附带 MCP 结果的 `response.create`。

参见[客户端 MCP 工具配置](/zh/model-studio/client-events#session-tools-mcp)、[审批回复](/zh/model-studio/client-events#qwen38-mcp-approval-response)、[MCP 服务端事件](/zh/model-studio/server-events#qwen38-server)和[MCP 对话项](/zh/model-studio/server-events#mcp-items)。

### 使用限制 <span id="qwen38-mcp-limits" />

以下为每个会话的 MCP 默认约束：

| 项目       | 默认值  |
| -------- | ---- |
| MCP 服务数  | 8    |
| MCP 工具数  | 128  |
| 工具发现超时   | 30 秒 |
| 审批超时     | 60 秒 |
| 工具执行超时   | 30 秒 |
| 单次上游响应大小 | 2 MB |
| 累计工具结果大小 | 8 MB |
| 累计工具调用数  | 256  |

## SDK

- [Python SDK](/zh/model-studio/omni-realtime-python-sdk)
- [Java SDK](/zh/model-studio/omni-realtime-java-sdk)
