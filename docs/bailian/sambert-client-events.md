> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Sambert客户端事件

<meta property="og:title" content="Sambert客户端事件" />

<strong>用户指南：</strong>关于模型介绍和选型建议请参见[语音合成](/zh/model-studio/tts-model)。

<Tip>
  Sambert仅支持在北京地域使用。
</Tip>

## run-task <span id="sb_h2_runtask" />

<strong>说明</strong>：启动语音合成任务，设置模型、采样率等参数，并一次性发送待合成文本。

<strong>发送时机</strong>：建立 WebSocket 连接后立即发送。

<strong>响应事件</strong>：服务端返回 task-started 事件。

<Tip>
  Sambert 不支持流式输入，待合成文本需要在 run-task 的 `input.text` 中一次性发送。不支持 continue-task 和 finish-task 指令。
</Tip>

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

          固定为 `out`
        </Accordion>

        <strong>payload</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>task\_group</strong>`string`<strong>（必选）</strong>

          任务组，固定为 `audio`。

          <strong>task</strong>`string`<strong>（必选）</strong>

          任务类型，固定为 `tts`。

          <strong>function</strong>`string`<strong>（必选）</strong>

          功能类型，固定为 `SpeechSynthesizer`。

          <strong>model</strong>`string`<strong>（必选）</strong>

          模型名称，如 `sambert-zhichu-v1`。

          <strong>input</strong>`object`<strong>（必选）</strong>

          输入数据，包含 `text` 字段，直接传入待合成文本。

          <strong>text</strong>`string`<strong>（必选）</strong>

          待合成文本。

          <strong>parameters</strong>`object`<strong>（必选）</strong>

          语音合成参数。

          <Accordion title="属性" defaultOpen>
            <strong>text\_type</strong>`string`<strong>（必选）</strong>

            固定为 `PlainText`。

            <strong>format</strong>`string`（可选）

            音频编码格式。

            取值范围：

            - pcm
            - wav（默认）
            - mp3

            <strong>sample\_rate</strong>`integer`（可选）

            音频采样率（Hz）。

            取值范围：8000, 16000（默认）, 22050, 24000。

            <strong>volume</strong>`integer`（可选）

            音量。

            默认值：50。

            取值范围：\[0, 100]。

            <strong>rate</strong>`float`（可选）

            语速。

            默认值：1.0。

            取值范围：\[0.5, 2.0]。

            <strong>pitch</strong>`float`（可选）

            音调。

            默认值：1.0。

            取值范围：\[0.5, 2.0]。

            <strong>word\_timestamp\_enabled</strong>`boolean`（可选）

            是否开启字级别时间戳。

            默认值：false。

            适用范围：所有 Sambert 模型。

            <strong>phoneme\_timestamp\_enabled</strong>`boolean`（可选）

            是否开启音素级别时间戳。

            默认值：false。

            需要先开启 word\_timestamp\_enabled。
          </Accordion>
        </Accordion>
      </td>

      <td>
        ```json expandable
        {
            "header": {
                "action": "run-task",
                "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
                "streaming": "out"
            },
            "payload": {
                "task_group": "audio",
                "task": "tts",
                "function": "SpeechSynthesizer",
                "model": "sambert-zhichu-v1",
                "parameters": {
                    "text_type": "PlainText",
                    "format": "wav",
                    "sample_rate": 16000,
                    "volume": 50,
                    "rate": 1.0,
                    "pitch": 1.0,
                    "word_timestamp_enabled": true,
                    "phoneme_timestamp_enabled": true
                },
                "input": {
                    "text": "待合成文本"
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
