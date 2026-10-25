> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-Audio-TTS服务端事件

<meta property="og:title" content="Qwen-Audio-TTS服务端事件" />

## task-started <span id="h3e7k9n3q5" />

当客户端发送 `run-task` 指令后，服务端返回 `task-started` 事件，标志任务已成功开启。只有在接收到该事件后，客户端才能继续发送后续指令。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header.task\_id</strong> `string`

        客户端生成的任务 ID。

        <strong>header.event</strong> `string`

        事件类型，固定为 `task-started`。

        <strong>payload</strong> `object`

        无内容，为空对象。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
          "header": {
            "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
            "event": "task-started",
            "attributes": {}
          },
          "payload": {}
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## result-generated <span id="h4r9t7v9x3" />

客户端发送文本后，服务端持续返回 `result-generated` 事件。该事件返回句子元信息。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header.task\_id</strong> `string`

        客户端生成的任务 ID。

        <strong>header.event</strong> `string`

        事件类型，固定为 `result-generated`。

        <strong>payload.output</strong> `object`

        输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong> `string`

          子事件类型，取值：

          - `sentence-begin`：句子开始，返回待合成的文本内容
          - `sentence-synthesis`：标识音频数据块，每个事件后立即通过 WebSocket binary 通道传输一个音频数据帧
          - `sentence-end`：句子结束，返回文本内容和用量信息（`usage`）

          <strong>sentence.index</strong> `integer`

          句子编号，从 0 开始。

          <strong>sentence.words</strong> `array`

          字级别时间戳信息数组。

          <Accordion title="words 元素属性" defaultOpen>
            <strong>text</strong> `string`

            字的文本内容。

            <strong>begin\_index</strong> `integer`

            字在句子中的开始位置索引，从 0 开始。

            <strong>end\_index</strong> `integer`

            字在句子中的结束位置索引，从 1 开始。

            <strong>begin\_time</strong> `integer`

            字对应音频的开始时间戳，单位：毫秒。

            <strong>end\_time</strong> `integer`

            字对应音频的结束时间戳，单位：毫秒。
          </Accordion>

          <strong>original\_text</strong> `string`

          分句后的句子文本内容。
        </Accordion>

        <strong>payload.usage</strong> `object`

        <span id="usage-fields" />

        计费用量，在 `sentence-end` 事件中返回。

        <Note>
          `qwen-audio-3.1-tts-flash` 按输入和输出 Token 计费；`qwen-audio-3.1-tts-plus`、`qwen-audio-3.0-tts-plus` 和 `qwen-audio-3.0-tts-flash` 按字符计费。
        </Note>

        <Accordion title="属性" defaultOpen>
          <strong>input\_tokens</strong> `integer`

          本次请求的输入 Token 数。

          <Note>
            适用于 `qwen-audio-3.1-tts-plus`、`qwen-audio-3.1-tts-flash`。
          </Note>

          <strong>output\_tokens</strong> `integer`

          本次请求的输出 Token 数。

          <Note>
            适用于 `qwen-audio-3.1-tts-plus`、`qwen-audio-3.1-tts-flash`。
          </Note>

          <strong>total\_tokens</strong> `integer`

          输入和输出 Token 数之和。

          <Note>
            适用于 `qwen-audio-3.1-tts-plus`、`qwen-audio-3.1-tts-flash`。
          </Note>

          <strong>characters</strong> `integer`

          累计计费字符数。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```json sentence-begin expandable
          {
            "header": {
              "task_id": "3f2d5c86-0550-45c0-801f-xxxxxxxxxx",
              "event": "result-generated",
              "attributes": {}
            },
            "payload": {
              "output": {
                "sentence": {
                  "index": 0,
                  "words": []
                },
                "type": "sentence-begin",
                "original_text": "床前明月光，"
              }
            }
          }
          ```

          ```json sentence-synthesis expandable
          {
              "header": {
                  "task_id": "3f2d5c86-0550-45c0-801f-xxxxxxxxxx",
                  "event": "result-generated",
                  "attributes": {}
              },
              "payload": {
                  "output": {
                      "sentence": {
                          "index": 0,
                          "words": []
                      },
                      "type": "sentence-synthesis"
                  }
              }
          }
          ```

          ```json sentence-end expandable
          {
            "header": {
              "task_id": "3f2d5c86-0550-45c0-801f-xxxxxxxxxx",
              "event": "result-generated",
              "attributes": {}
            },
            "payload": {
              "output": {
                "sentence": {
                  "index": 0,
                  "words": [
                    {
                      "text": "床",
                      "begin_index": 0,
                      "end_index": 1,
                      "begin_time": 0,
                      "end_time": 263
                    }
                  ]
                },
                "type": "sentence-end",
                "original_text": "床前明月光，"
              },
              "usage": {
                "characters": 6
              }
            }
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## task-finished <span id="h8d9f5h7j9" />

服务端返回 `task-finished` 事件，标志任务已结束。客户端可以关闭 WebSocket 连接或复用连接开启新任务。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header.task\_id</strong> `string`

        客户端生成的任务 ID。

        <strong>header.event</strong> `string`

        事件类型，固定为 `task-finished`。

        <strong>payload.usage</strong> `object`

        字段含义与 <Link href="#usage-fields">result-generated 的 usage</Link> 相同。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
          "header": {
            "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
            "event": "task-finished",
            "attributes": {
              "request_uuid": "0a9dba9e-d3a6-45a4-be6d-xxxxxxxxxxxx"
            }
          },
          "payload": {
            "usage": {
              "characters": 13
            }
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## task-failed <span id="h4h9j5m7p9" />

当任务失败时，服务端返回 `task-failed` 事件。客户端需要关闭 WebSocket 连接并处理错误。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header.task\_id</strong> `string`

        客户端生成的任务 ID。

        <strong>header.event</strong> `string`

        事件类型，固定为 `task-failed`。

        <strong>header.error\_code</strong> `string`

        错误码。

        <strong>header.error\_message</strong> `string`

        具体错误信息。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
          "header": {
            "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
            "event": "task-failed",
            "error_code": "InvalidParameter",
            "error_message": "[tts:]Engine return error code: 418",
            "attributes": {}
          },
          "payload": {}
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
