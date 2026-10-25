> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Sambert服务端事件

<meta property="og:title" content="Sambert服务端事件" />

<strong>用户指南：</strong>关于模型介绍和选型建议请参见[语音合成](/zh/model-studio/tts-model)。

## task-started <span id="sb_srv_h3_taskstarted" />

当客户端发送 `run-task` 指令后，服务端返回 `task-started` 事件，标志任务已成功开启。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>header.task\_id</strong>`string`

        客户端生成的任务 ID。

        <strong>header.event</strong>`string`

        事件类型，固定为 `task-started`。

        <strong>payload</strong>`object`

        无内容，为空对象。
      </td>

      <td>
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

## result-generated <span id="sb_srv_h3_resultgen" />

客户端发送文本后，服务端持续返回 `result-generated` 事件。该事件在 `output.sentence` 中直接返回完整的时间戳信息，包括字级别和音素级别时间戳（需启用相应参数）。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>header.task\_id</strong>`string`

        客户端生成的任务 ID。

        <strong>header.event</strong>`string`

        事件类型，固定为 `result-generated`。

        <strong>payload.output</strong>`object`

        输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>sentence.begin\_time</strong>`integer`

          句子开始时间戳，单位：毫秒。

          <strong>sentence.end\_time</strong>`integer`

          句子结束时间戳，单位：毫秒。

          <strong>sentence.words</strong>`array`

          字级别时间戳信息数组。

          <Accordion title="words 元素属性" defaultOpen>
            <strong>text</strong>`string`

            字的文本内容。

            <strong>begin\_time</strong>`integer`

            字对应音频的开始时间戳，单位：毫秒。

            <strong>end\_time</strong>`integer`

            字对应音频的结束时间戳，单位：毫秒。

            <strong>phonemes</strong>`array`<strong>（需启用 phoneme\_timestamp\_enabled）</strong>

            音素级别时间戳信息数组。

            <Accordion title="phonemes 元素属性" defaultOpen>
              <strong>text</strong>`string`

              音素文本。

              <strong>tone</strong>`integer`

              音调。

              <strong>begin\_time</strong>`integer`

              音素开始时间戳，单位：毫秒。

              <strong>end\_time</strong>`integer`

              音素结束时间戳，单位：毫秒。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>payload.usage</strong>`object`

        计费信息。

        <Accordion title="属性" defaultOpen>
          <strong>characters</strong>*integer*

          截止当前累计的计费字符数。
        </Accordion>
      </td>

      <td>
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
                "end_time": 1162,
                "words": [
                  {
                    "text": "床",
                    "begin_time": 0,
                    "end_time": 263,
                    "phonemes": [
                      {
                        "begin_time": 0,
                        "end_time": 119,
                        "text": "ch_c",
                        "tone": 2
                      }
                    ]
                  }
                ]
              }
            },
            "usage": {
              "characters": 6
            }
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## task-finished <span id="sb_srv_h3_taskfinished" />

服务端返回 `task-finished` 事件，标志任务已结束。客户端可以关闭 WebSocket 连接或复用连接开启新任务。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>header.task\_id</strong>`string`

        客户端生成的任务 ID。

        <strong>header.event</strong>`string`

        事件类型，固定为 `task-finished`。

        <strong>payload.usage.characters</strong>`integer`

        截止当前累计的计费字符数。
      </td>

      <td>
        ```json
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

## task-failed <span id="sb_srv_h3_taskfailed" />

当任务失败时，服务端返回 `task-failed` 事件。客户端需要关闭 WebSocket 连接并处理错误。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>header.task\_id</strong>`string`

        客户端生成的任务 ID。

        <strong>header.event</strong>`string`

        事件类型，固定为 `task-failed`。

        <strong>header.error\_code</strong>`string`

        错误码。

        <strong>header.error\_message</strong>`string`

        具体错误信息。
      </td>

      <td>
        ```json
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
