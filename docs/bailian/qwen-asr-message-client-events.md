> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 实时语音识别（Qwen-Audio-ASR-Message）客户端事件

> 本文介绍 Qwen-Audio-ASR-Message 的客户端事件及参数。

<meta property="og:title" content="实时语音识别（Qwen-Audio-ASR-Message）客户端事件" />

## run-task <span id="9cae7e7b85ebm" />

启动语音识别任务，设置模型、音频格式、采样率等参数。

<strong>发送时机</strong>：连接建立成功后立即发送。

<strong>响应事件</strong>：服务端返回 `task-started` 事件后才能发送音频。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
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

          模型名，设置为 `qwen-audio-3.1-asr-flash-message`。模型信息请参见[Qwen-Audio-3.1-ASR-Flash-Message](/zh/model-studio/qwen-audio-3-1-asr-flash-message)。

          <strong>input</strong>`object`<strong>（必选）</strong>

          输入对象。不携带上下文时传入`{}`。

          <Accordion title="属性" defaultOpen>
            <strong>context</strong>`array(object)`（可选）

            对话上下文，用于辅助识别、提升专有词汇的识别准确率。使用方法详见[上下文增强](/zh/model-studio/improve-asr-accuracy#ctx_enhance_h2)。

            <Tip>
              上下文消息（`input_text` 和 `text` 类型）各最多 5 条，超出时保留最近的 5 条。每轮上下文文本总长度（`user` 和 `assistant` 的 `text` 字段长度之和）不超过 400 个字符（按字符数计算，每个字符计为 1），超出部分从末尾截断。
            </Tip>

            <Tip>
              携带上下文时，`context` 中的消息顺序有要求：上下文消息必须按对话轮次排列，每轮中 `user`（`input_text` 类型）必须在对应的 `assistant`（`text` 类型）之前。
            </Tip>

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`<strong>（必选）</strong>

              消息角色。取值范围：

              - `user`：前几轮用户语音的识别结果或领域相关的词表。
              - `assistant`：前几轮大语言模型的回复内容。

              <strong>content</strong>`array(object)`<strong>（必选）</strong>

              消息内容列表。

              <Accordion title="属性" defaultOpen>
                <strong>type</strong>`string`<strong>（必选）</strong>

                内容类型。取值范围：

                - `input_text`：前几轮用户语音的识别结果或领域相关的词表（role 为 user 时使用），需同时传入 `text` 字段。
                - `text`：前几轮大语言模型的回复内容（role 为 assistant 时使用），需同时传入 `text` 字段。

                <strong>text</strong>`string`<strong>（必选）</strong>

                文本内容。当 `type` 为 `input_text` 时，填入前几轮用户语音的识别结果或领域相关的词表；当 `type` 为 `text` 时，填入前几轮大语言模型的回复内容。
              </Accordion>
            </Accordion>
          </Accordion>

          <strong>parameters</strong>`object`<strong>（必选）</strong>

          语音识别参数。

          <Accordion title="属性" defaultOpen>
            <strong>disfluency\_removal\_enabled</strong> `boolean`（可选）

            是否过滤语气词并对输出结果进行润色，默认值为 `false`。设置为 `true` 时启用。

            <strong>intermediate\_result\_enabled</strong> `boolean`（可选）

            是否返回流式中间结果，默认值为 `false`。设置为 `true` 时返回流式中间结果。

            <strong>keep\_dialect</strong> `boolean`（可选）

            是否保留方言表达，默认值为 `false`。`false`：将方言转写为普通话文本；`true`：保留方言表达。

            仅在最终结果（`sentence_end=true`）生效，中间结果（`sentence_end=false`）不支持。

            <strong>vad\_model</strong> `string`（可选）

            VAD 模型，默认值为 `far_field_meeting_16k`。

            - `near_meeting_16k`：近场场景。
            - `far_field_meeting_16k`：远场场景。

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
              opus/speex：必须使用Ogg封装；

              wav：必须为PCM编码；

              amr：仅支持AMR-NB类型。
            </Tip>

            <strong>sample\_rate</strong>`integer`<strong>（必选）</strong>

            采样率（Hz）。

            仅支持 `16000` Hz。

            <strong>vocabulary\_id</strong>`string`（可选）

            预编译热词列表 ID。

            需预先调用创建热词列表接口生成，识别时传入该 ID 即可使用列表中的热词。

            适用于词汇已知且相对稳定、需要跨请求复用同一词表的场景。

            使用方法请参见[预编译热词](/zh/model-studio/improve-asr-accuracy#hw_precompiled_h3)。

            <strong>vocabulary</strong>`object`（可选）

            即时热词。

            以键值对形式传入，键为热词文本（`string`），值为热词权重（`integer`），无需预先创建热词列表。权重取值范围为 \[1, 5] 或 50：取 \[1, 5] 时值越大模型越倾向输出该词；取 50 时为超级热词，召回率大幅提升，但超级热词数量最多不超过 50 个。

            适用于临时性、会话级别的热词优化。

            与预编译热词同时配置时，系统会合并两类热词；合并后超过 2000 个时，随机选择 2000 个使用。使用方法请参见[即时热词](/zh/model-studio/improve-asr-accuracy#hw_instant_h3)。

            <strong>language\_hints</strong>`array[string]`（可选）

            待识别音频语种。无默认值，不设置时模型自动识别。

            最多支持设置 4 个值，超出时仅前 4 个生效。

            <Accordion title="点击查看支持的语言代码">
              - zh: 中文
              - en: 英文
              - ja: 日语
              - ko：韩语
              - vi：越南语
              - th：泰语
              - id：印尼语
              - ms：马来语
              - tl：菲律宾语
              - hi：印地语
              - ar：阿拉伯语
              - fr：法语
              - de：德语
              - es：西班牙语
              - pt：葡萄牙语
              - ru：俄语
              - it：意大利语
              - nl：荷兰语
              - sv：瑞典语
              - da：丹麦语
              - fi：芬兰语
              - no：挪威语
              - el：希腊语
              - pl：波兰语
              - cs：捷克语
              - hu：匈牙利语
              - ro：罗马尼亚语
              - bg：保加利亚语
              - hr：克罗地亚语
              - sk：斯洛伐克语
            </Accordion>

            仅在最终结果（`sentence_end=true`）生效，中间结果（`sentence_end=false`）不支持。

            <strong>max\_sentence\_silence</strong>`integer`（可选）

            VAD 断句静音阈值（ms）。当一段语音后的静音时长超过该阈值时，系统会判定该句子已结束。

            默认值：800。

            取值范围：\[200, 6000]。

            <strong>heartbeat</strong>`boolean`（可选）

            是否启用心跳包。

            默认值：false。

            - true：在持续发送静音音频的情况下，可保持与服务端的连接不中断。
            - false（默认）：即使持续发送静音音频，连接也将在一定时间后因超时而断开。

            静音音频指的是在音频文件或数据流中没有声音信号的内容。静音音频可以通过多种方法生成，例如使用音频编辑软件如Audacity或Adobe Audition，或者通过命令行工具如FFmpeg。

            <strong>speech\_noise\_threshold</strong>`float`（可选）

            语音与噪音的判定阈值，用于调整语音活动检测（VAD）的灵敏度。

            取值范围：\[-1.0, 1.0]。

            取值说明：

            - 取值越接近 -1：降低噪音判定阈值，噪音被识别为语音的概率增大，可能导致更多噪音被转写
            - 取值越接近 +1：提高噪音判定阈值，语音被误判为噪音的概率增大，可能导致部分语音被过滤

            此参数为高级配置参数，调整可能显著影响识别效果，建议：

            - 调整前充分测试验证效果
            - 根据实际音频环境小幅度调整（建议步长 0.1）
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```json 基本请求 expandable
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
                  "model": "qwen-audio-3.1-asr-flash-message",
                  "parameters": {
                      "format": "pcm",
                      "sample_rate": 16000
                  },
                  "input": {}
              }
          }
          ```

          ```json 携带上下文 expandable
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
                  "model": "qwen-audio-3.1-asr-flash-message",
                  "parameters": {
                      "format": "pcm",
                      "sample_rate": 16000
                  },
                  "input": {
                      "context": [
                          {
                              "role": "user",
                              "content": [
                                  {
                                      "type": "input_text",
                                      "text": "你好啊"
                                  }
                              ]
                          },
                          {
                              "role": "assistant",
                              "content": [
                                  {
                                      "type": "text",
                                      "text": "你好啊，我是通义千问，有什么可以帮助你的？"
                                  }
                              ]
                          }
                      ]
                  }
              }
          }
          ```

          ```json 即时热词 expandable
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
                  "model": "qwen-audio-3.1-asr-flash-message",
                  "parameters": {
                      "format": "pcm",
                      "sample_rate": 16000,
                      "vocabulary": {"张三": 5, "李四": 5}
                  },
                  "input": {}
              }
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## continue-task <span id="h-continue-task" />

在任务执行过程中更新对话上下文信息，用于辅助识别。

<strong>发送时机</strong>：任务运行中，需要更新对话上下文时发送。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>action</strong>`string`<strong>（必选）</strong>

          指令类型，固定为 `continue-task`。

          <strong>task\_id</strong>`string`<strong>（必选）</strong>

          客户端生成的任务 ID（UUID 格式），需与[run-task](/zh/model-studio/qwen-asr-message-client-events#9cae7e7b85ebm)事件中的 task\_id 保持一致。

          <strong>streaming</strong>`string`<strong>（必选）</strong>

          固定为 `duplex`。
        </Accordion>

        <strong>payload</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>input</strong>`object`<strong>（必选）</strong>

          输入对象。

          <Accordion title="属性" defaultOpen>
            <strong>context</strong>`array(object)`（可选）

            对话上下文，用于辅助识别、提升专有词汇的识别准确率。使用方法详见[上下文增强](/zh/model-studio/improve-asr-accuracy#ctx_enhance_h2)。

            <Tip>
              上下文消息（`input_text` 和 `text` 类型）各最多 5 条，超出时保留最近的 5 条。每轮上下文文本总长度（`user` 和 `assistant` 的 `text` 字段长度之和）不超过 400 个字符（按字符数计算，每个字符计为 1），超出部分从末尾截断。
            </Tip>

            <Tip>
              携带上下文时，`context` 中的消息顺序有要求：上下文消息必须按对话轮次排列，每轮中 `user`（`input_text` 类型）必须在对应的 `assistant`（`text` 类型）之前。
            </Tip>

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`<strong>（必选）</strong>

              消息角色。取值范围：

              - `user`：前几轮用户语音的识别结果或领域相关的词表。
              - `assistant`：前几轮大语言模型的回复内容。

              <strong>content</strong>`array(object)`<strong>（必选）</strong>

              消息内容列表。

              <Accordion title="属性" defaultOpen>
                <strong>type</strong>`string`<strong>（必选）</strong>

                内容类型。取值范围：

                - `input_text`：前几轮用户语音的识别结果或领域相关的词表（role 为 user 时使用），需同时传入 `text` 字段。
                - `text`：前几轮大语言模型的回复内容（role 为 assistant 时使用），需同时传入 `text` 字段。

                <strong>text</strong>`string`<strong>（必选）</strong>

                文本内容。当 `type` 为 `input_text` 时，填入前几轮用户语音的识别结果或领域相关的词表；当 `type` 为 `text` 时，填入前几轮大语言模型的回复内容。
              </Accordion>
            </Accordion>
          </Accordion>
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
                    "context": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "input_text",
                                    "text": "你好啊"
                                }
                            ]
                        },
                        {
                            "role": "assistant",
                            "content": [
                                {
                                    "type": "text",
                                    "text": "你好啊，我是通义千问，有什么可以帮助你的？"
                                }
                            ]
                        }
                    ]
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## finish-task <span id="85880fa44008n" />

通知服务端音频发送完毕，请求结束任务。

<strong>发送时机</strong>：所有音频数据发送完毕后。

<strong>响应事件</strong>：服务端返回 `task-finished` 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>header</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>action</strong>`string`<strong>（必选）</strong>

          指令类型，固定为 `finish-task`。

          <strong>task\_id</strong>`string`<strong>（必选）</strong>

          客户端生成的任务 ID（UUID 格式），需与[run-task](/zh/model-studio/qwen-asr-message-client-events#9cae7e7b85ebm)事件中的 task\_id 保持一致。

          <strong>streaming</strong>`string`<strong>（必选）</strong>

          固定为 `duplex`。
        </Accordion>

        <strong>payload</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>input</strong>`object`<strong>（必选）</strong>

          固定为`{}`。
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
      </td>
    </tr>
  </tbody>
</table>
