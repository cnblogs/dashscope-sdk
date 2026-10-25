> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Assistant API 流式输出参数说明（下线中）

> 通过 Assistant API 流式输出，您可以实时获取 Assistant 的运行结果。这些结果称为 Assistant 事件流，包含了 Assistant 运行时产生的状态信息和对话消息。为了正确处理这些对话消息，您需要了解消息增量对象和运行步骤增量对象。

<meta property="og:title" content="Assistant API 流式输出参数说明（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

## 消息增量对象 <span id="d4b4158d2dffj" />

> 事件名：thread.message.delta

在流式运行中大模型生成的消息片段。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "40%" }} /><col style={{ width: "10%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>string</p></td><td><p>消息的标识符。</p></td></tr><tr><td><p>object</p></td><td><p>string</p></td><td><p>对象类型，与事件名保持一致。始终是 thread.message.delta。</p></td></tr><tr><td><p>delta</p></td><td><p>object</p></td><td><p>大模型生成的消息片段对象。</p></td></tr><tr><td><p>delta.role</p></td><td><p>string</p></td><td><p>生成消息的角色。可以是 user 或 assistant 中的一个。</p></td></tr><tr><td><p>delta.file\_ids</p></td><td><p>array</p></td><td><p>暂未使用的字段。</p></td></tr><tr><td><p>delta.content</p></td><td><p>object</p></td><td><p>消息内容对象。</p></td></tr><tr><td><p>delta.content.type</p></td><td><p>string</p></td><td><p>消息内容类型，始终是 text。</p></td></tr><tr><td><p>delta.content.text</p></td><td><p>object</p></td><td><p>消息中包含的文本内容对象。</p></td></tr><tr><td><p>delta.content.text.annotations</p></td><td><p>array</p></td><td><p>暂未使用的字段。</p></td></tr><tr><td><p>delta.content.text.value</p></td><td><p>string</p></td><td><p>消息中包含的文本内容。</p></td></tr></tbody></table>

<strong>消息增量对象示例</strong>

```python
{
    "delta": {
        "content": {
            "text": {
                "annotations": [],
                "value": "[REDACTED]"
            },
            "type": "text"
        },
        "role": "[REDACTED]",
        "file_ids": []
    },
    "id": "[REDACTED]",
    "object": "thread.message.delta",
    "request_id": "[REDACTED]",
    "status_code": 200
}
```

## 运行步骤增量对象 <span id="427f330f2c8yr" />

> 事件名：thread.run.step.delta

在流式运行中工具调用返回的消息片段。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "39.940828%" }} /><col style={{ width: "10.059172%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>string</p></td><td><p>运行步骤的标识符。</p></td></tr><tr><td><p>object</p></td><td><p>string</p></td><td><p>对象类型，与事件名保持一致。始终是 thread.run.step.delta。</p></td></tr><tr><td><p>delta</p></td><td><p>object</p></td><td><p>工具调用返回的消息片段对象。</p></td></tr><tr><td><p>delta.step\_details</p></td><td><p>object</p></td><td><p>片段的详细步骤信息对象。</p></td></tr><tr><td><p>delta.step\_details.type</p></td><td><p>string</p></td><td><p>步骤详情类型, 可能是 message\_creation 或 tool\_calls</p></td></tr><tr><td><p>delta.step\_details.message\_creation</p></td><td><p>object</p></td><td><p>步骤创建消息的详细信息。</p></td></tr><tr><td><p>delta.step\_details.message\_creation.type</p></td><td><p>string</p></td><td><p>始终为 message\_creation。</p></td></tr><tr><td><p>delta.step\_details.message\_creation.message\_id</p></td><td><p>string</p></td><td><p>此步骤创建的消息 ID。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls</p></td><td><p>array</p></td><td><p>工具调用的详细信息列表。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.type</p></td><td><p>string</p></td><td><p>工具调用的类型，可能为 code\_interpreter, quark\_search, text\_to\_image, calculator</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.code\_interpreter</p></td><td><p>object</p></td><td><p>代码解释器的消息对象。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.code\_interpreter.arguments</p></td><td><p>string</p></td><td><p>代码解释器的输入参数，包括代码类型，完整代码。由大模型生成。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.code\_interpreter.output</p></td><td><p>string</p></td><td><p>代码解释器的输出参数，包括执行结果。由代码解释器生成。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.quark\_search</p></td><td><p>object</p></td><td><p>夸克搜索的消息对象。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.quark\_search.arguments</p></td><td><p>string</p></td><td><p>夸克搜索的输入参数，包括改写后的查询语句。由大模型生成。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.quark\_search.output</p></td><td><p>string</p></td><td><p>夸克搜索的输出参数，包括成功标记，错误码，错误消息，搜索结果列表（标题、地址、描述、类型、消息源）。由夸克搜索生成。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.text\_to\_image</p></td><td><p>object</p></td><td><p>文生图的消息对象。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.text\_to\_image.arguments</p></td><td><p>string</p></td><td><p>文生图的输入参数，包括模型名，风格，图像大小，图像数量，正向提示词和负向提示词。由大模型生成。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.text\_to\_image.output</p></td><td><p>string</p></td><td><p>文生图的输出参数，包括请求id，任务id，任务状态，提交时间，预计完成时间，结束时间，生成结果，图像数量，成功标记和失败标记。由文生图生成。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.calculator</p></td><td><p>object</p></td><td><p>计算器的消息对象。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.calculator.arguments</p></td><td><p>string</p></td><td><p>计算器的输入参数，包括输入公式和请求头。由大模型生成。</p></td></tr><tr><td><p>delta.step\_details.tool\_calls.calculator.output</p></td><td><p>string</p></td><td><p>计算器的输出参数，包括公式和结果。由计算器生成。</p></td></tr></tbody></table>

<Note>
  代码解释器，夸克搜索，文生图和计算器支持流式输出。

  其他 Assistant API 工具不支持流式输出。如需在流式输出中获取这些工具调用的结果，请使用一般的工具调用处理方法。详情请参考[工具调用-概述](/zh/model-studio/tool-calling-overview)目录的对应章节。
</Note>

<strong>运行步骤增量对象示例</strong>

```python
{
    "delta": {
        "step_details": {
            "tool_calls": [
                {
                    "code_interpreter": {
                        "output": [],
                        "arguments": "[REDACTED]"
                    },
                    "type": "code_interpreter"
                }
            ],
            "type": "tool_calls"
        }
    },
    "id": "[REDACTED]",
    "object": "thread.run.step.delta",
    "request_id": "[REDACTED]",
    "status_code": 200
}
```

## Assistant 事件流 <span id="95cd5573f0k0g" />

在流式运行 Assistant 时发生的事件。

在 Assistant API 中，流式数据也称为“事件流”，由`event（事件）`和`data（事件数据）`组成：

- `event`：每当创建新对象、转换到新状态或流式传输时，服务器都会发出事件。

  例如，创建新的运行`thread.run.created` 、运行完成 `thread.run.completed` 等。
- `data`：包含了事件相关的详细数据。

  例如状态、文本或工具消息、错误等。

```python
event: thread.created
data: {"id": "thread_123", "object": "thread", ...}
```

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "36.363636%" }} /><col style={{ width: "18.181818%" }} /><col style={{ width: "45.454545%" }} /></colgroup><thead><tr><th><p><strong>事件名</strong></p></th><th><p><strong>数据类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>thread.created</p></td><td><p>thread</p></td><td><p>创建新线程时发生。</p></td></tr><tr><td><p>thread.run.created</p></td><td><p>run</p></td><td><p>创建新运行时发生。</p></td></tr><tr><td><p>thread.run.queued</p></td><td><p>run</p></td><td><p>当运行移动到 queued 状态时发生。</p></td></tr><tr><td><p>thread.run.in\_progress</p></td><td><p>run</p></td><td><p>当运行移动到 in\_progress 状态时发生。</p></td></tr><tr><td><p>thread.run.requires\_action</p></td><td><p>run</p></td><td><p>当运行移动到 requires\_action 状态时发生。</p></td></tr><tr><td><p>thread.run.completed</p></td><td><p>run</p></td><td><p>在运行完成时发生。</p></td></tr><tr><td><p>thread.run.failed</p></td><td><p>run</p></td><td><p>当运行失败时发生。</p></td></tr><tr><td><p>thread.run.cancelled</p></td><td><p>run</p></td><td><p>当运行被取消时发生。</p></td></tr><tr><td><p>thread.run.expired</p></td><td><p>run</p></td><td><p>发生在运行过期时。</p></td></tr><tr><td><p>thread.run.step.created</p></td><td><p>run step</p></td><td><p>在创建运行步骤时发生。</p></td></tr><tr><td><p>thread.run.step.in\_progress</p></td><td><p>run step</p></td><td><p>当运行步骤移动到 in\_progress 状态时发生。</p></td></tr><tr><td><p>thread.run.step.delta</p></td><td><p>run step delta</p></td><td><p>在运行步骤的部分内容被流式传输时发生。</p></td></tr><tr><td><p>thread.run.step.completed</p></td><td><p>run step</p></td><td><p>在运行步骤完成时发生。</p></td></tr><tr><td><p>thread.run.step.failed</p></td><td><p>run step</p></td><td><p>发生在运行步骤失败时。</p></td></tr><tr><td><p>thread.run.step.cancelled</p></td><td><p>run step</p></td><td><p>当运行步骤被取消时发生。</p></td></tr><tr><td><p>thread.run.step.expired</p></td><td><p>run step</p></td><td><p>发生在运行步骤过期时。</p></td></tr><tr><td><p>thread.message.created</p></td><td><p>message</p></td><td><p>在消息创建时发生。</p></td></tr><tr><td><p>thread.message.in\_progress</p></td><td><p>message</p></td><td><p>当消息变为 in\_progress 状态时发生。</p></td></tr><tr><td><p>thread.message.delta</p></td><td><p>message delta</p></td><td><p>当消息的部分内容被流式传输时发生。</p></td></tr><tr><td><p>thread.message.completed</p></td><td><p>message</p></td><td><p>当消息完成时发生。</p></td></tr><tr><td><p>thread.message.incomplete</p></td><td><p>message</p></td><td><p>当消息在完成之前结束时发生。</p></td></tr><tr><td><p>error</p></td><td><p>error</p></td><td><p>当发生错误时触发。这可能是由于内部服务器错误或超时引起的。</p></td></tr></tbody></table>

## 常见问题 <span id="40610b98820qn" />

- 在配置流式输出时，如果您遇到代码执行错误，请参阅[错误码](/zh/model-studio/error-code)排查错误类型。
- 如需了解 Assistant API 流式输出的配置方法，请参阅[Assistant API 流式输出快速入门](/zh/model-studio/streaming-output)以了解更多信息。
