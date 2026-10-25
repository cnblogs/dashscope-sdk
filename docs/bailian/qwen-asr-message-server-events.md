> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 实时语音识别（Qwen-Audio-ASR-Message）服务端事件

> 本文介绍 Qwen-Audio-ASR-Message 的服务端事件及字段。

<meta property="og:title" content="实时语音识别（Qwen-Audio-ASR-Message）服务端事件" />

## task-started <span id="eac316aeacafj" />

任务启动成功，客户端可开始发送音频数据。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header</strong>`object`

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          客户端生成的任务 ID（UUID 格式）。

          <strong>event</strong>`string`

          事件类型，固定为 `task-started`。

          <strong>attributes</strong>`object`

          附加属性（通常为空）。
        </Accordion>

        <strong>payload</strong>`object`

        固定为`{}`。
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

## result-generated <span id="9cb9c5831c7cs" />

识别结果。默认返回句子最终识别文本（`sentence_end=true`）；设置 `intermediate_result_enabled=true` 后还返回中间文本（`sentence_end=false`）。中间文本可能修订，应按 `sentence_id` 更新展示，不要直接拼接。句子开始事件可包含 `sentence_begin=true`，此时文本可能为空。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header</strong>`object`

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          客户端生成的任务 ID（UUID 格式）。

          <strong>event</strong>`string`

          事件类型，固定为 `result-generated`。
        </Accordion>

        <strong>payload</strong>`object`

        <Accordion title="属性" defaultOpen>
          <strong>output</strong>`object`

          <Accordion title="属性" defaultOpen>
            <strong>sentence</strong>`object`

            <Accordion title="属性" defaultOpen>
              <strong>begin\_time</strong>`integer`

              句子开始时间（ms）。

              <strong>end\_time</strong>`integer`

              句子结束时间（ms）。

              <strong>text</strong>`string`

              识别文本。

              <strong>heartbeat</strong>`boolean`

              若为 true，可跳过该结果（心跳包）。

              <strong>sentence\_begin</strong>`boolean`

              用于标识句子开始。

              <strong>sentence\_end</strong>`boolean`

              是否句子结束（true=最终结果，false=中间结果）。

              <strong>sentence\_id</strong>`integer`

              句子的序号标识。正常识别结果中，sentence\_id 从 1 开始递增。当 heartbeat 为 true 时（即心跳包），sentence\_id 固定为 0。

              <strong>words</strong>`array[object]`

              字时间戳信息。

              <Accordion title="属性" defaultOpen>
                <strong>begin\_time</strong>`integer`

                字开始时间（ms）。

                <strong>end\_time</strong>`integer`

                字结束时间（ms）。

                <strong>text</strong>`string`

                识别文本。

                <strong>punctuation</strong>`string`

                标点符号。
              </Accordion>
            </Accordion>
          </Accordion>

          <strong>usage</strong>`object`

          当前任务截至本事件的累计用量。

          <Accordion title="属性" defaultOpen>
            <strong>duration</strong>`integer`

            累计音频时长（秒），为兼容保留。

            <strong>input\_tokens</strong>`integer`

            累计输入 Token 数。

            <strong>output\_tokens</strong>`integer`

            累计输出 Token 数。

            <strong>total\_tokens</strong>`integer`

            累计 Token 总数，为 `input_tokens` 与 `output_tokens` 之和。
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>句子开始结果：</strong>

        ```json expandable
        {
          "header": {
            "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
            "event": "result-generated",
            "attributes": {}
          },
          "payload": {
            "output": {
              "sentence": {
                "sentence_id": 1,
                "begin_time": 0,
                "end_time": null,
                "text": "",
                "channel_id": 0,
                "speaker_id": null,
                "sentence_end": false,
                "sentence_begin": true,
                "words": []
              }
            }
          }
        }
        ```

        <strong>最终结果：</strong>

        ```json expandable
        {
          "header": {
            "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
            "event": "result-generated",
            "attributes": {}
          },
          "payload": {
            "output": {
              "sentence": {
                "sentence_id": 1,
                "begin_time": 120,
                "end_time": 1700,
                "text": "欢迎使用阿里云。",
                "channel_id": 0,
                "speaker_id": null,
                "sentence_end": true,
                "words": [
                  {
                    "begin_time": 120,
                    "end_time": 520,
                    "text": "欢迎",
                    "punctuation": "",
                    "fixed": false,
                    "speaker_id": null
                  },
                  {
                    "begin_time": 520,
                    "end_time": 880,
                    "text": "使用",
                    "punctuation": "",
                    "fixed": false,
                    "speaker_id": null
                  },
                  {
                    "begin_time": 880,
                    "end_time": 1700,
                    "text": "阿里云",
                    "punctuation": "。",
                    "fixed": false,
                    "speaker_id": null
                  }
                ],
                "stash": {
                  "sentence_id": 2,
                  "text": "",
                  "begin_time": 1700,
                  "current_time": 1700,
                  "words": []
                }
              }
            },
            "usage": {
              "duration": 2,
              "input_tokens": 84,
              "output_tokens": 4,
              "total_tokens": 88
            }
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## task-finished <span id="b8aefe8e0c5fr" />

任务正常结束，可关闭连接或复用连接。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header</strong>`object`

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          客户端生成的任务 ID（UUID 格式）。

          <strong>event</strong>`string`

          事件类型，固定为 `task-finished`。

          <strong>attributes</strong>`object`

          附加属性（通常为空）。
        </Accordion>

        <strong>payload</strong>`object`

        包含 `output`（空对象）和 `usage`（本任务最终累计用量）。`usage` 字段定义与 [result-generated](#9cb9c5831c7cs) 一致。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
          "header": {
            "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
            "event": "task-finished",
            "attributes": {}
          },
          "payload": {
            "output": {},
            "usage": {
              "duration": 2,
              "input_tokens": 84,
              "output_tokens": 4,
              "total_tokens": 88
            }
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## task-failed <span id="3fb17b3b199fy" />

任务失败，连接会被关闭，无法复用。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header</strong>`object`

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          客户端生成的任务 ID（UUID 格式）。

          <strong>event</strong>`string`

          事件类型，固定为 `task-failed`。

          <strong>error\_code</strong>`string`

          错误类型描述。

          <strong>error\_message</strong>`string`

          具体错误原因。

          <strong>attributes</strong>`object`

          附加属性（通常为空）。
        </Accordion>

        <strong>payload</strong>`object`

        固定为`{}`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
            "header": {
                "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
                "event": "task-failed",
                "error_code": "CLIENT_ERROR",
                "error_message": "request timeout after 23 seconds.",
                "attributes": {}
            },
            "payload": {}
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
