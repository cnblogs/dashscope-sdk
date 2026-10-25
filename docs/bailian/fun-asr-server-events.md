> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 实时语音识别（Fun-ASR-Realtime）服务端事件

> 本文介绍 Fun-ASR-Realtime 实时语音识别服务通过 WebSocket 推送给客户端的服务端事件，包括 task-started、result-generated、task-finished、task-failed 四类事件的数据结构与字段含义。

<meta property="og:title" content="实时语音识别（Fun-ASR-Realtime）服务端事件" />

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

识别结果，包含中间结果（sentence\_end=false）和最终结果（sentence\_end=true）。其中，新句子的首个中间结果包含 sentence\_begin=true。

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

          当`payload.output.sentence.sentence_end`为`false`（当前句子未结束）时，`usage`为`null`。

          当`payload.output.sentence.sentence_end`为`true`（当前句子已结束）时，`usage.duration`为当前任务计费时长。

          <Accordion title="属性" defaultOpen>
            <strong>duration</strong>`integer`

            任务计费时长（s）。
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
                "begin_time": 0,
                "end_time": null,
                "text": "",
                "sentence_begin": true,
                "sentence_end": false,
                "sentence_id": 1,
                "words": []
              }
            },
            "usage": null
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
                "begin_time": 170,
                "end_time": 920,
                "text": "好，我知道了",
                "heartbeat": false,
                "sentence_end": true,
                "sentence_id": 1,
                "words": [
                  {
                    "begin_time": 170,
                    "end_time": 295,
                    "text": "好",
                    "punctuation": "，"
                  },
                  {
                    "begin_time": 295,
                    "end_time": 503,
                    "text": "我",
                    "punctuation": ""
                  },
                  {
                    "begin_time": 503,
                    "end_time": 711,
                    "text": "知道",
                    "punctuation": ""
                  },
                  {
                    "begin_time": 711,
                    "end_time": 920,
                    "text": "了",
                    "punctuation": ""
                  }
                ]
              }
            },
            "usage": {
              "duration": 3
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

        无需关注其中内容，通常为`{}`。
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
                "usage": null
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
