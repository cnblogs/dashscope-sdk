> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 实时语音识别（Paraformer）客户端事件

> 本文介绍 Paraformer 实时语音识别服务中客户端通过 WebSocket 发送给服务端的客户端事件，包括 run-task（启动任务）和 finish-task（结束任务）两类指令的数据结构与字段含义。

<meta property="og:title" content="实时语音识别（Paraformer）客户端事件" />

<strong>用户指南：</strong>关于模型介绍和选型建议请参见[语音识别](/zh/model-studio/asr-model)。

<strong>事件交互流程</strong>：如需了解事件交互时序，请参见[WebSocket API](/zh/model-studio/websocket-for-paraformer-real-time-service)。

## run-task <span id="b618a7624b06f" />

<strong>说明</strong>：启动语音识别任务，设置模型、音频格式、采样率等参数。

<strong>发送时机</strong>：建立 WebSocket 连接后立即发送。

<strong>响应事件</strong>：服务端返回 `task-started` 事件后才能发送音频。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>header</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>action</strong>`string`<strong>（必选）</strong>

          指令类型，固定为 `run-task`。

          <strong>task\_id</strong>`string`<strong>（必选）</strong>

          客户端生成的任务 ID（UUID 格式），用于关联后续事件。

          <strong>streaming</strong>`string`<strong>（必选）</strong>

          固定为 `duplex`。
        </Accordion>

        <strong>payload</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>task\_group</strong>`string`<strong>（必选）</strong>

          任务组，固定为 `audio`。

          <strong>task</strong>`string`<strong>（必选）</strong>

          任务类型，固定为 `asr`。

          <strong>function</strong>`string`<strong>（必选）</strong>

          功能类型。固定为`recognition`。

          <strong>model</strong>`string`<strong>（必选）</strong>

          模型名称。

          <strong>input</strong>`object`<strong>（必选）</strong>

          固定为`{}`。

          <strong>parameters</strong>`object`<strong>（必选）</strong>

          语音识别参数。

          <Accordion title="属性" defaultOpen>
            <strong>format</strong>`string`<strong>（必选）</strong>

            音频格式。

            取值范围：

            - `pcm`
            - `wav`
            - `mp3`
            - `opus`
            - `speex`
            - `aac`
            - `amr`

            <Tip>
              Paraformer须遵循如下约束：

              - opus/speex：必须使用Ogg封装
              - wav：必须为PCM编码
              - amr：仅支持AMR-NB类型
            </Tip>

            <strong>sample\_rate</strong>`integer`<strong>（必选）</strong>

            采样率（Hz）。

            取值范围：

            - Paraformer（因模型而异）：

              - paraformer-realtime-v2：支持任意采样率

              - paraformer-realtime-8k-v2：仅支持 8000 Hz

            <strong>vocabulary\_id</strong>`string`（可选）

            热词列表 ID。

            <strong>disfluency\_removal\_enabled</strong>`boolean`（可选）

            <Tip>
              仅Paraformer支持该参数。
            </Tip>

            是否过滤语气词。

            默认值：false。

            <strong>language\_hints</strong>`array[string]`（可选）

            待识别音频语种。无默认值，不设置时模型自动识别。

            取值范围：

            - Paraformer：

              - zh: 中文
              - en: 英文
              - ja: 日语
              - yue: 粤语
              - ko: 韩语
              - de：德语
              - fr：法语
              - ru：俄语

            <strong>semantic\_punctuation\_enabled</strong>`boolean`（可选）

            <Tip>
              仅Paraformer（v2）支持该参数。
            </Tip>

            是否启用语义断句。

            默认值：false。

            - true：开启语义断句，关闭 VAD 断句。
            - false（默认）：开启 VAD 断句，关闭语义断句。

            语义断句准确性更高，适合会议转写场景；VAD（Voice Activity Detection，语音活动检测）断句延迟较低，适合交互场景。

            <strong>max\_sentence\_silence</strong>`integer`（可选）

            <Tip>
              - 仅Paraformer（v2）支持该参数。
              - 仅在`semantic_punctuation_enabled`参数为false时生效。
            </Tip>

            VAD 断句静音阈值（ms）。当一段语音后的静音时长超过该阈值时，系统会判定该句子已结束。

            默认值：1300。

            取值范围：\[200, 6000]。

            <strong>multi\_threshold\_mode\_enabled</strong>`boolean`（可选）

            <Tip>
              - 仅Paraformer（v2）支持该参数。
              - 仅在`semantic_punctuation_enabled`参数为false时生效。
            </Tip>

            是否启用多阈值模式。启用后可防止 VAD 断句切割过长。

            默认值：false。

            <strong>punctuation\_prediction\_enabled</strong>`boolean`（可选）

            <Tip>
              仅Paraformer（v2）支持该参数。
            </Tip>

            是否在识别结果中添加标点符号。

            默认值：true。

            <strong>heartbeat</strong>`boolean`（可选）

            <Tip>
              仅Paraformer（v2）支持该参数。
            </Tip>

            是否启用心跳包。

            默认值：false。

            - true：在持续发送静音音频的情况下，可保持与服务端的连接不中断。
            - false（默认）：即使持续发送静音音频，连接也将在一定时间后因超时而断开。

            <strong>inverse\_text\_normalization\_enabled</strong>`boolean`（可选）

            <Tip>
              仅Paraformer（v2）支持该参数。
            </Tip>

            是否启用逆文本正则化（ITN）。启用后，中文数字将转换为阿拉伯数字。

            默认值：true。
          </Accordion>
        </Accordion>
      </td>

      <td>
        ```json
        {
            "header": {
                "action": "run-task",
                "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
                "streaming": "duplex"
            },
            "payload": {
                "task_group": "audio",
                "task": "asr",
                "function": "recognition",
                "model": "paraformer-realtime-v2",
                "parameters": {
                    "format": "pcm",
                    "sample_rate": 16000,
                    "disfluency_removal_enabled": false,
                    "language_hints": [
                        "en"
                    ]
                },
                "input": {}
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## finish-task <span id="c28f5d7ddb6dc" />

<strong>说明</strong>：通知服务端音频发送完毕，请求结束任务。

<strong>发送时机</strong>：所有音频数据发送完毕后。

<strong>响应事件</strong>：服务端返回 `task-finished` 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>header</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>action</strong>`string`<strong>（必选）</strong>

          指令类型，固定为 `finish-task`。

          <strong>task\_id</strong>`string`<strong>（必选）</strong>

          客户端生成的任务 ID（UUID 格式），需与[run-task](/zh/model-studio/paraformer-client-events#b618a7624b06f)事件中的 task\_id 保持一致。

          <strong>streaming</strong>`string`<strong>（必选）</strong>

          固定为 `duplex`。
        </Accordion>

        <strong>payload</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>input</strong>`object`<strong>（必选）</strong>

          固定为`{}`。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "header": {
                "action": "finish-task",
                "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
                "streaming": "duplex"
            },
            "payload": {
                "input": {}
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
