> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# CosyVoice客户端事件

<meta property="og:title" content="CosyVoice客户端事件" />

## run-task <span id="h3runtask" />

启动语音合成任务，设置模型、音色、采样率等参数。

<strong>发送时机</strong>：建立 WebSocket 连接后立即发送。

<strong>响应事件</strong>：服务端返回 task-started 事件后才能发送后续指令。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header</strong> `object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          指令类型，固定为 `run-task`。

          <strong>task\_id</strong> `string`<strong>（必选）</strong>

          客户端生成的任务 ID（UUID 格式），用于关联后续事件。和后续 continue-task、finish-task 中的 task\_id 保持一致。

          <strong>streaming</strong> `string`<strong>（必选）</strong>

          固定为 `duplex`
        </Accordion>

        <strong>payload</strong> `object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>task\_group</strong> `string`<strong>（必选）</strong>

          任务组，固定为 `audio`。

          <strong>task</strong> `string`<strong>（必选）</strong>

          任务类型，固定为 `tts`。

          <strong>function</strong> `string`<strong>（必选）</strong>

          功能类型，固定为 `SpeechSynthesizer`。

          <strong>model</strong> `string`<strong>（必选）</strong>

          模型名称。

          <strong>input</strong> `object`<strong>（必选）</strong>

          输入数据：固定为空对象 `{}`，待合成文本通过 continue-task 指令发送。

          <strong>parameters</strong> `object`<strong>（必选）</strong>

          语音合成参数。

          <Accordion title="属性" defaultOpen>
            <strong>text\_type</strong> `string`<strong>（必选）</strong>

            固定为 `PlainText`。

            <strong>voice</strong> `string`<strong>（必选）</strong>

            语音合成所使用的音色。

            - <strong>系统音色</strong>：参见[CosyVoice音色列表](/zh/model-studio/cosyvoice-voice-list)
            - <strong>复刻音色</strong>：通过声音复刻功能定制
            - <strong>声音设计音色</strong>：通过声音设计功能定制

            <strong>format</strong> `string`（可选）

            音频编码格式。

            取值范围：

            - pcm
            - wav
            - mp3（默认）
            - opus

            <strong>sample\_rate</strong> `integer`（可选）

            音频采样率（Hz）。

            取值范围：8000, 16000, 22050（默认）, 24000, 44100, 48000。

            <strong>volume</strong> `integer`（可选）

            音量。

            默认值：50。

            取值范围：\[0, 100]。

            <strong>rate</strong> `float`（可选）

            语速。

            默认值：1.0。

            取值范围：\[0.5, 2.0]。

            <strong>pitch</strong> `float`（可选）

            音调。

            默认值：1.0。

            取值范围：\[0.5, 2.0]。

            <strong>bit\_rate</strong> `integer`（可选）

            音频码率（kbps）。音频格式为mp3或opus时，支持通过`bit_rate`参数调整码率。

            默认值：32。

            取值范围：\[6, 510]。

            <strong>enable\_ssml</strong> `boolean`（可选）

            是否开启 SSML 功能。

            默认值：false。

            设为 true 后，仅允许发送一次 continue-task 指令。

            SSML 的使用限制（支持的模型、音色和接口），请参见[使用限制](/zh/model-studio/ssml-latex-user-guide#sl01_constraint_h3)。

            <strong>word\_timestamp\_enabled</strong> `boolean`（可选）

            是否开启字级别时间戳。

            默认值：false。

            仅在流式输出模式下可用。支持的音色范围：cosyvoice-v3.5-plus、cosyvoice-v3.5-flash、cosyvoice-v3-flash、cosyvoice-v3-plus、cosyvoice-v2模型的复刻音色，以及[CosyVoice音色列表](/zh/model-studio/cosyvoice-voice-list)中标记为支持的系统音色。其他模型的复刻音色不支持此功能。

            <strong>seed</strong> `integer`（可选）

            生成时使用的随机数种子，使合成的效果产生变化。在模型版本、文本、音色及其他参数均相同的前提下，使用相同的seed可复现相同的合成结果。

            默认值0。

            取值范围：\[0, 65535]。

            <strong>language\_hints</strong> `array[string]`（可选）

            <Tip>
              - 此参数为数组，但当前版本仅处理第一个元素，因此建议只传入一个值。
              - 此参数用于指定语音合成的目标语言，该设置与声音复刻时的样本音频的语种无关。如需设置复刻任务的源语言，请参见<Link href="/zh/model-studio/voice-clone-design-http-api#h2-create">声音复刻API参考</Link>。
            </Tip>

            指定语音合成的目标语言，提升合成效果。

            当数字、缩写、符号等朗读方式或者小语种合成效果不符合预期时使用，例如：

            - 数字朗读方式不符合预期，“hello, this is 110”读成“hello, this is one one zero”而非“hello, this is 幺幺零”
            - 符号朗读不准确，“@”读成“艾特”而非“at”
            - 小语种合成效果差，合成不自然

            <Accordion title="取值范围">
              * zh：中文
              * en：英语
              * fr：法语
              * de：德语
              * ja：日语
              * ko：韩语
              * ru：俄语
              * pt：葡萄牙语
              * th：泰语
              * id：印尼语
              * vi：越南语
              * es：西班牙语
              * it：意大利语
              * ms：马来西亚语
              * fil：菲律宾语
              * ar：阿拉伯语
            </Accordion>

            <strong>instruction</strong> `string`（可选）

            设置指令，用于控制方言、情感或角色等合成效果。具体使用说明请参见[指令控制](/zh/model-studio/realtime-tts-user-guide#12884a10929p9)。

            <strong>enable\_aigc\_tag</strong> `boolean`（可选）

            是否在生成的音频中添加AIGC隐性标识。设置为true时，会将隐性标识嵌入到支持格式（wav/mp3/opus）的音频中。

            默认值：false。

            <Note>
              仅cosyvoice-v3-flash、cosyvoice-v3-plus、cosyvoice-v2支持该功能。
            </Note>

            <strong>aigc\_propagator</strong> `string`（可选）

            设置AIGC隐性标识中的 `ContentPropagator` 字段，用于标识内容的传播者。仅在 `enable_aigc_tag` 为 `true` 时生效。

            默认值：阿里云UID。

            <Note>
              仅cosyvoice-v3-flash、cosyvoice-v3-plus、cosyvoice-v2支持该功能。
            </Note>

            <strong>aigc\_propagate\_id</strong> `string`（可选）

            设置AIGC隐性标识中的 `PropagateID` 字段，用于唯一标识一次具体的传播行为。仅在 `enable_aigc_tag` 为 `true` 时生效。

            默认值：本次语音合成请求Request ID。

            <Note>
              仅cosyvoice-v3-flash、cosyvoice-v3-plus、cosyvoice-v2支持该功能。
            </Note>

            <strong>hot\_fix</strong> `object`（可选）

            文本热修复配置，用于自定义指定词语的发音或对待合成文本进行替换。

            <Note>
              cosyvoice-v2不支持该功能。
            </Note>

            参数介绍：

            - pronunciation：自定义发音。指定词语的拼音标注，用于纠正默认发音不准确的情况。
            - replace：文本替换。在语音合成前将指定词语替换为目标文本，替换后的文本将作为实际合成内容。

            示例：

            ```json
            "hot_fix": {
              "pronunciation": [
                {"天气": "tian1 qi4"}
              ],
              "replace": [
                {"今天": "金天"}
              ]
            }
            ```

            <strong>enable\_markdown\_filter</strong> `boolean`（可选）

            <Note>
              仅cosyvoice-v3-flash复刻音色支持该功能。
            </Note>

            是否启用 Markdown 过滤。启用该功能后，系统在合成语音前自动过滤输入文本中的 Markdown 标记符号，避免将其朗读为文字内容。

            默认值：false。

            取值范围：

            - true：启用Markdown过滤
            - false：禁用Markdown过滤
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
            "header": {
                "action": "run-task",
                "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
                "streaming": "duplex"
            },
            "payload": {
                "task_group": "audio",
                "task": "tts",
                "function": "SpeechSynthesizer",
                "model": "cosyvoice-v3-plus",
                "parameters": {
                    "text_type": "PlainText",
                    "voice": "longanyang",
                    "format": "mp3",
                    "sample_rate": 22050,
                    "volume": 50,
                    "rate": 1.0,
                    "pitch": 1.0,
                    "enable_ssml": false
                },
                "input": {}
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## continue-task <span id="h3continuetask" />

用于发送待合成文本。可一次性发送，也可分段按顺序发送。

<strong>发送时机</strong>：在接收到服务端返回的 task-started 事件后。

<strong>数量限制</strong>：

- 单次调用最多发送 20000 字符
- 累计最多发送 200000 字符
- 发送间隔不得超过 23 秒，否则连接超时

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header</strong> `object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          指令类型，固定为 `continue-task`。

          <strong>task\_id</strong> `string`<strong>（必选）</strong>

          任务 ID（UUID 格式），需要和 run-task 中的 task\_id 保持一致。

          <strong>streaming</strong> `string`<strong>（必选）</strong>

          固定为 `duplex`。
        </Accordion>

        <strong>payload</strong> `object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>input</strong> `object`<strong>（必选）</strong>

          包含待合成文本。

          <strong>text</strong> `string`<strong>（必选）</strong>

          待合成文本。单次最多 20000 字符，累计最多 200000 字符。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        {
            "header": {
                "action": "continue-task",
                "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
                "streaming": "duplex"
            },
            "payload": {
                "input": {
                    "text": "床前明月光，疑是地上霜"
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## finish-task <span id="h3finishtask" />

通知服务端文本发送完毕，请求结束任务。如需取消当前轮次的语音合成任务，可在 `input` 中设置 `directive` 为 `cancel`。

<strong>发送时机</strong>：所有文本发送完毕后立即发送。

<strong>响应事件</strong>：服务端返回 task-finished 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header</strong> `object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          指令类型，固定为 `finish-task`。

          <strong>task\_id</strong> `string`<strong>（必选）</strong>

          任务 ID（UUID 格式），需要和 run-task 中的 task\_id 保持一致。

          <strong>streaming</strong> `string`<strong>（必选）</strong>

          固定为 `duplex`
        </Accordion>

        <strong>payload</strong> `object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>input</strong> `object`<strong>（必选）</strong>

          任务输入。为空对象 `{}` 时表示正常结束任务；包含 `directive` 时可用于取消当前轮次的语音合成任务。

          <strong>directive</strong> `string`（可选）

          控制任务结束行为。当前仅支持取值为 `cancel`，表示取消当前轮次的语音合成任务，服务端会立即返回 `task-finished` 事件，且不会输出后续音频。

          取消后，可在当前 WebSocket 连接上重新发起语音合成任务（发送新的 `run-task` 事件），无需重新建立连接。

          <Tip>
            <strong>模型限制</strong>：

            - 华北2（北京）地域：CosyVoice 系列模型仅 v2 及以上版本支持该功能。
            - 新加坡地域：CosyVoice 系列模型不支持该功能。
          </Tip>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
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

        <strong>取消任务示例</strong>：

        ```json expandable
        {
            "header": {
                "action": "finish-task",
                "task_id": "2bf83b9a-baeb-4fda-8d9a-xxxxxxxxxxxx",
                "streaming": "duplex"
            },
            "payload": {
                "input": {
                    "directive": "cancel"
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
