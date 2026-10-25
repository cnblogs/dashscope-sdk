> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 服务端事件

> 本文介绍千问实时语音/音视频翻译 API 的服务端事件，包括事件类型、参数和示例。

<meta property="og:title" content="服务端事件" />

> 相关文档：[实时语音/音视频翻译-千问](/zh/model-studio/qwen3-5-livetranslate-flash-realtime)。

## error <span id="9e1059c1d3tcx" />

服务端返回的错误信息。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`error`。

        <strong>error</strong>`object`

        错误的详细信息。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          错误类型。

          <strong>code</strong>`string`

          错误码。

          <strong>message</strong>`string`

          错误信息。

          <strong>param</strong>`string`

          与错误相关的参数，如`session.modalities`。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "event_id": "event_RoUu4T8yExPMI37GKwaOC",
          "type": "error",
          "error": {
            "type": "invalid_request_error",
            "code": "invalid_value",
            "message": "Invalid modalities: ['audio']. Supported combinations are: ['text'] and ['audio', 'text'].",
            "param": "session.modalities"
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.created <span id="39689ed6e90ag" />

客户端连接后，服务端返回的第一个事件，包含本次连接的默认配置信息。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`session.created`。

        <strong>session</strong>`object`

        会话的配置。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          会话的唯一标识符。

          <strong>object</strong>`string`

          固定为`realtime.session`。

          <strong>model</strong>`string`

          使用的模型。

          <strong>input\_modalities</strong>`array`

          适用于 `qwen3.8-livetranslate-flash-realtime`。输入模态，例如 `["audio"]`。

          <strong>output\_modalities</strong>`array`

          适用于 `qwen3.8-livetranslate-flash-realtime`。输出模态：`["text"]` 或 `["text", "audio"]`。

          <strong>audio</strong>`object`

          适用于 `qwen3.8-livetranslate-flash-realtime`。音频输入与输出配置。

          <Accordion title="属性" defaultOpen>
            <strong>input.format</strong>`object`

            输入格式。`type` 默认值为 `pcm`，`sample_rate` 默认值为 `16000`（Hz）。

            <strong>input.turn\_detection</strong>`object`

            断句配置。默认 `type` 为 `speaker_detection`，`threshold` 为 `0.5`，`silence_duration_ms` 为 `1000`。

            <strong>output.format</strong>`object`

            输出格式。`type` 默认值为 `pcm`，`sample_rate` 默认值为 `24000`（Hz）。

            <strong>output.voice</strong>`string`

            输出音色，默认值为 `Tina`。
          </Accordion>

          <strong>modalities</strong>`array`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          模型输出模态设置。

          <strong>voice</strong>`string`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          模型生成音频的音色。

          <strong>input\_audio\_format</strong>`string`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          输入音频的格式，默认为`pcm`。

          <strong>sample\_rate</strong>`integer`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          输入音频的采样率，单位为Hz。

          <strong>output\_audio\_format</strong>`string`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          输出音频的格式，默认为`pcm`。

          <strong>turn\_detection</strong>`object`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          VAD（语音活动检测）配置。

          <strong>translation</strong>`object`（可选）

          翻译配置。

          <Accordion title="属性" defaultOpen>
            <strong>language</strong>`string`（可选）

            设置的翻译目标语种。

            <strong>corpus</strong>`object` （可选）

            热词配置，用于提升特定词汇的翻译准确性。

            <Accordion title="属性" defaultOpen>
              <strong>corpus.phrases</strong>`object` （可选）

              热词映射表。key 为源语言词汇，value 为目标语言对应翻译，参见[支持的语种](/zh/model-studio/qwen3-5-livetranslate-flash-realtime#4ffd192226f0s) 。
            </Accordion>
          </Accordion>
        </Accordion>
      </td>

      <td>
        适用于 `qwen3.8-livetranslate-flash-realtime`。

        ```json
        {
          "event_id": "event_example",
          "type": "session.created",
          "session": {
            "id": "sess_example",
            "object": "realtime.session",
            "model": "qwen3.8-livetranslate-flash-realtime",
            "input_modalities": [
              "audio"
            ],
            "output_modalities": [
              "text",
              "audio"
            ],
            "audio": {
              "input": {
                "format": {
                  "type": "pcm",
                  "sample_rate": 16000
                },
                "turn_detection": {
                  "type": "speaker_detection",
                  "threshold": 0.5,
                  "silence_duration_ms": 1000
                }
              },
              "output": {
                "format": {
                  "type": "pcm",
                  "sample_rate": 24000
                },
                "voice": "Tina"
              }
            },
            "translation": {
              "language": "en"
            }
          }
        }
        ```

        `qwen3.5-livetranslate-flash-realtime` 示例：

        ```json expandable
        {
            "event_id": "event_QxBGpjBDmDDQQWDtrqBKB",
            "type": "session.created",
            "session": {
                "id": "sess_OozZ1vtbPt2muDflHODIH",
                "object": "realtime.session",
                "model": "qwen3.5-livetranslate-flash-realtime",
                "modalities": [
                    "text",
                    "audio"
                ],
                "voice": "Cherry",
                "input_audio_format": "pcm",
                "sample_rate": 16000,
                "output_audio_format": "pcm",
                "turn_detection": {
                    "type": "server_vad",
                    "threshold": 0.2,
                    "silence_duration_ms": 1000
                },
                "translation": {
                   "language": "en",
                   "corpus": {
                       "phrases": {
                           "人工智能": "Artificial Intelligence",
                           "机器学习": "Machine Learning"
                       }
                   }
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.updated <span id="424ef2e774q9p" />

收到用户的 `session.update` 请求后，若处理成功，则返回此事件；若出错，则返回 `error` 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`session.updated`。

        <strong>session</strong>`object`

        会话的配置。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          会话的唯一标识符。

          <strong>object</strong>`string`

          固定为`realtime.session`。

          <strong>model</strong>`string`

          使用的模型。

          <strong>input\_modalities</strong>`array`

          适用于 `qwen3.8-livetranslate-flash-realtime`。输入模态，例如 `["audio"]`。

          <strong>output\_modalities</strong>`array`

          适用于 `qwen3.8-livetranslate-flash-realtime`。输出模态：`["text"]` 或 `["text", "audio"]`。

          <strong>audio</strong>`object`

          适用于 `qwen3.8-livetranslate-flash-realtime`。音频输入与输出配置。

          <Accordion title="属性" defaultOpen>
            <strong>input.format</strong>`object`

            输入格式。`type` 默认值为 `pcm`，`sample_rate` 默认值为 `16000`（Hz）。

            <strong>input.turn\_detection</strong>`object`

            断句配置。默认 `type` 为 `speaker_detection`，`threshold` 为 `0.5`，`silence_duration_ms` 为 `1000`。

            <strong>output.format</strong>`object`

            输出格式。`type` 默认值为 `pcm`，`sample_rate` 默认值为 `24000`（Hz）。

            <strong>output.voice</strong>`string`

            输出音色，默认值为 `Tina`。
          </Accordion>

          <strong>modalities</strong>`array`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          模型输出模态设置。

          <strong>voice</strong>`string`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          模型生成音频的音色。

          <strong>sample\_rate</strong>`integer`（可选）

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          输入音频的采样率。

          <strong>input\_audio\_format</strong>`string`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          输入音频的格式，固定为`pcm`。

          <strong>output\_audio\_format</strong>`string`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          输出音频的格式，固定为`pcm`。

          <strong>input\_audio\_transcription</strong>`object`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          输入音频转录配置。仅在会话配置了`input_audio_transcription.model`参数时返回。

          <Accordion title="属性" defaultOpen>
            <strong>model</strong>`string`

            语音识别模型。

            <strong>language</strong>`string`

            设置的语音识别语种。
          </Accordion>

          <strong>turn\_detection</strong>`object`

          适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。

          VAD（语音活动检测）配置。Manual 模式下（客户端在`session.update`中将该参数设为`null`）不返回此字段。

          <Accordion title="属性" defaultOpen>
            <strong>type</strong>`string`

            VAD 类型，固定为`server_vad`。

            <strong>threshold</strong>`float`

            VAD 检测灵敏度。

            <strong>prefix\_padding\_ms</strong>`integer`

            语音开始前保留的音频时长（毫秒），避免丢失语音起始部分。

            <strong>silence\_duration\_ms</strong>`integer`

            语音结束后需保持静音的最短时长（毫秒），超过该时长即判定语音结束。

            <strong>create\_response</strong>`boolean`

            VAD 检测到语音结束后，是否自动触发翻译响应。

            <strong>interrupt\_response</strong>`boolean`

            VAD 检测到新一轮语音开始时，是否打断当前正在生成的翻译响应。
          </Accordion>

          <strong>translation</strong>`object`（可选）

          翻译配置。

          <Accordion title="属性" defaultOpen>
            <strong>language</strong>`string`（可选）

            设置的翻译目标语种。

            <strong>corpus</strong>`object`（可选）

            热词配置，用于提升特定词汇的翻译准确性。

            <Accordion title="属性" defaultOpen>
              <strong>corpus.phrases</strong>`object`（可选）

              热词映射表。key 为源语言词汇，value 为目标语言对应翻译。
            </Accordion>
          </Accordion>

          <strong>enable\_voice\_clone</strong>`boolean`

          是否启用声音复刻。

          <strong>voice\_clone\_options</strong>`object`

          声音复刻控制参数，仅在`enable_voice_clone`为`true`时返回。

          <Accordion title="属性" defaultOpen>
            <strong>frequency</strong>`string`

            音色复刻频率。
          </Accordion>
        </Accordion>
      </td>

      <td>
        适用于 `qwen3.8-livetranslate-flash-realtime`。

        ```json
        {
          "event_id": "event_example",
          "type": "session.updated",
          "session": {
            "id": "sess_example",
            "object": "realtime.session",
            "model": "qwen3.8-livetranslate-flash-realtime",
            "input_modalities": [
              "audio"
            ],
            "output_modalities": [
              "text",
              "audio"
            ],
            "audio": {
              "input": {
                "format": {
                  "type": "pcm",
                  "sample_rate": 16000
                },
                "turn_detection": {
                  "type": "speaker_detection",
                  "threshold": 0.5,
                  "silence_duration_ms": 1000
                }
              },
              "output": {
                "format": {
                  "type": "pcm",
                  "sample_rate": 24000
                },
                "voice": "Tina"
              }
            },
            "translation": {
              "language": "en"
            }
          }
        }
        ```

        `qwen3.5-livetranslate-flash-realtime` 示例：

        ```json expandable
        {
            "event_id": "event_QxBGpjBDmDDQQWDtrqBKB",
            "type": "session.updated",
            "session": {
                "id": "sess_OozZ1vtbPt2muDflHODIH",
                "object": "realtime.session",
                "model": "qwen3.5-livetranslate-flash-realtime",
                "modalities": [
                    "text",
                    "audio"
                ],
                "voice": "Ethan",
                "sample_rate": 16000,
                "input_audio_format": "pcm",
                "output_audio_format": "pcm",
                "input_audio_transcription": {
                    "model": "qwen3-asr-flash-realtime",
                    "language": "zh"
                },
                "turn_detection": {
                    "type": "server_vad",
                    "threshold": 0.2,
                    "prefix_padding_ms": 300,
                    "silence_duration_ms": 1000,
                    "create_response": true,
                    "interrupt_response": true
                },
                "translation": {
                   "language": "en",
                   "corpus": {
                       "phrases": {
                           "人工智能": "Artificial Intelligence",
                           "机器学习": "Machine Learning"
                       }
                   }
                },
                "enable_voice_clone": true,
                "voice_clone_options": {
                    "frequency": "once"
                }
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.finished <span id="5369266402pgb" />

会话结束事件，表示当前会话中，所有音频翻译已完成。

该事件在客户端发送[session.finish](/zh/model-studio/live-translator-client-events#f8075550b26jf)后才会发送，客户端接收到该事件后可主动断开连接。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`session.finished`。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "session.finished"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.created <span id="38033afc582r1" />

当服务端生成新的模型响应时，会返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.created`。

        <strong>response</strong>`object`

        响应对象。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong> `string`

          响应的唯一标识符。

          <strong>conversation\_id</strong> `string`

          当前会话的唯一标识符。

          <strong>object</strong> `string`

          对象类型，此事件下固定为`realtime.response`。

          <strong>status</strong> `string`

          响应状态，取值范围：

          - `completed`（已完成）
          - `failed`（失败）
          - `in_progress`（进行中）
          - `incomplete`（不完整）

          <strong>modalities</strong> `array`

          响应的模态。

          <strong>voice</strong> `string`

          模型生成音频的音色。

          <strong>output\_audio\_format</strong>`string`

          输出音频的格式。

          <strong>output</strong> `string`

          此事件下目前为空。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_L8hHVI5jYis6BzAjnPWJh",
            "type": "response.created",
            "response": {
                "id": "resp_P79OOMs8LnrXVpiIHUCKR",
                "object": "realtime.response",
                "conversation_id": "conv_UFClXtYkRkFXrs48y8pmK",
                "status": "in_progress",
                "modalities": [
                    "text",
                    "audio"
                ],
                "voice": "Cherry",
                "output_audio_format": "pcm16",
                "output": []
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.done <span id="f2333c777d9s4" />

响应生成完成后，服务端会返回此事件。事件中的 `response` 对象包含除原始音频数据外的全部输出项。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.done`。

        <strong>response</strong>`object`

        响应对象。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong> `string`

          响应的唯一标识符。

          <strong>conversation\_id</strong> `string`

          当前会话的唯一标识符。

          <strong>object</strong> `string`

          对象类型，此事件下固定为`realtime.response`。

          <strong>status</strong> `string`

          响应的状态。

          <strong>modalities</strong> `array`

          响应的模态。

          <strong>voice</strong> `string`

          模型生成音频的音色。

          <strong>output\_audio\_format</strong>`string`

          输出音频的格式。

          <strong>output</strong> `object`

          响应的输出。

          <Accordion title="属性" defaultOpen>
            <strong>id</strong> `string`

            响应输出的唯一标识符。

            <strong>type</strong> `string`

            输出项的类型，当前固定为`message`。

            <strong>object</strong> `string`

            输出项的对象类型，当前固定为`realtime.item`。

            <strong>status</strong> `string`

            输出项的状态。

            <strong>role</strong> `string`

            输出项的角色。

            <strong>content</strong> `array`

            输出项的内容。

            <Accordion title="属性" defaultOpen>
              <strong>type</strong> `string`

              输出内容的类型。输出为纯文本时，为`text`；输出包含音频时，为`audio`。

              <strong>text</strong> `string`

              输出的文本内容。

              <strong>transcript</strong> `string`

              音频转录为文字后的内容。
            </Accordion>
          </Accordion>

          <strong>usage</strong> `object`

          本次响应的 Token 消耗信息。
        </Accordion>
      </td>

      <td>
        ```json expandable
        {
          "event_id": "event_CNea8oXNipVanSg2VIzkO",
          "type": "response.done",
          "response": {
            "id": "resp_TfhYTqej692vsGA2jNEtH",
            "object": "realtime.response",
            "conversation_id": "conv_ZtyLfKVm8XqLwYRlsuDih",
            "status": "completed",
            "modalities": [
              "text",
              "audio"
            ],
            "voice": "Cherry",
            "output_audio_format": "pcm16",
            "output": [
              {
                "id": "item_MKtkMwN9RtcyE9eJShyWy",
                "object": "realtime.item",
                "type": "message",
                "status": "completed",
                "role": "assistant",
                "content": [
                  {
                    "type": "audio",
                    "transcript": "Hello? "
                  }
                ]
              }
            ],
            "usage": {
              "total_tokens": 56,
              "input_tokens": 47,
              "output_tokens": 9,
              "input_tokens_details": {
                "text_tokens": 20,
                "audio_tokens": 27
              },
              "output_tokens_details": {
                "text_tokens": 2,
                "audio_tokens": 7
              }
            }
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.text.delta <span id="text-delta" />

适用于 `qwen3.8-livetranslate-flash-realtime`。 仅输出文本时，返回译文的新增文本片段。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        事件唯一标识符。

        <strong>type</strong>`string`

        `response.text.delta`。

        <strong>response\_id</strong>`string`

        响应 ID。

        <strong>item\_id</strong>`string`

        消息 ID。

        <strong>output\_index</strong>`integer`

        输出项索引。

        <strong>content\_index</strong>`integer`

        内容片段索引。

        <strong>delta</strong>`string`

        本次新增的文本片段。按接收顺序累加，不覆盖已收到的文本。
      </td>

      <td>
        ```json
        {
          "event_id": "event_example",
          "type": "response.text.delta",
          "response_id": "resp_example",
          "item_id": "item_example",
          "output_index": 0,
          "content_index": 0,
          "delta": "Hello"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.text.text <span id="0c54be63e0c3w" />

适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。 使用 `qwen3.8-livetranslate-flash-realtime` 时，请处理 [response.text.delta](#text-delta) 事件。

当输出模态仅包含文本，且模型增量生成新的文本时，服务端将返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.text.text`。

        <strong>text</strong>`string`

        返回的增量文本。

        <strong>response\_id</strong>`string`

        回复的ID。

        <strong>item\_id</strong>`string`

        消息项ID，可以关联同一个消息项。

        <strong>output\_index</strong>`integer`

        目前固定为 0。

        <strong>content\_index</strong>`integer`

        目前固定为 0。

        <strong>stash</strong>`string`

        初步生成的临时文本，与当前 `text` 拼接后构成临时生成结果；系统会通过 `response.text.text` 事件持续更新 `text` 和 `stash`，直至收到[response.text.done](/zh/model-studio/live-translator-server-events#d675635a94jfb)事件，此时可通过 `text` 字段获取完整的最终文本。
      </td>

      <td>
        ```json
        {
            "event_id": "event_B1lIeyOXR7qJMEExbqtTG",
            "type": "response.text.text",
            "response_id": "resp_B1lIdtjF4Noqpn5NOjznj",
            "item_id": "item_B1lIdJsAJlJiFs8ztWpJt",
            "output_index": 0,
            "content_index": 0,
            "text": "How are",
            "stash": " you today?"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.text.done <span id="d675635a94jfb" />

当输出模态仅包含文本，且模型生成的文本结束时，服务端返回此事件。

> 当响应中断、不完整或取消时，服务端也会返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.text.done`。

        <strong>response\_id</strong>`string`

        响应的唯一标识符。

        <strong>item\_id</strong>`string`

        消息项的唯一标识符。

        <strong>output\_index</strong>`integer`

        目前固定为 0。

        <strong>content\_index</strong>`integer`

        目前固定为 0。

        <strong>text</strong> `string`

        模型输出的完整文本。
      </td>

      <td>
        ```json
        {
            "event_id": "event_B1lIeE2Nac33zn5V7h2mm",
            "type": "response.text.done",
            "response_id": "resp_B1lIdtjF4Noqpn5NOjznj",
            "item_id": "item_B1lIdJsAJlJiFs8ztWpJt",
            "output_index": 0,
            "content_index": 0,
            "text": "How can I assist you today?"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio.delta <span id="a25cc50a15car" />

当输出模态包含音频，且模型增量生成新的音频数据时，服务端将返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio.delta`。

        <strong>response\_id</strong>`string`

        响应的唯一标识符。

        <strong>item\_id</strong>`string`

        消息项唯一标识符。

        <strong>output\_index</strong>*integer*

        目前固定为 0。

        <strong>content\_index</strong>`integer`

        目前固定为 0。

        <strong>delta</strong> `string`

        模型增量输出的音频数据，使用Base64编码。
      </td>

      <td>
        ```json
        {
            "event_id": "event_B1osWMZBtrEQbiIwW0qHQ",
            "type": "response.audio.delta",
            "response_id": "resp_P79OOMs8LnrXVpiIHUCKR",
            "item_id": "item_OFaPGtzfWCPyGzxnuEX9i",
            "output_index": 0,
            "content_index": 0,
            "delta": "UklGRnoGAABXQVZFZm10IBAAAAAB..."
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio.done <span id="9e8eb59c67qnt" />

当输出模态包含音频，且模型生成音频结束时，服务端返回此事件。

> 当响应中断、不完整或取消时，服务端也会返回此事件。

> 该事件不返回完整音频数据。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio.done`。

        <strong>response\_id</strong>`string`

        响应的唯一标识符。

        <strong>item\_id</strong>`string`

        消息项唯一标识符。

        <strong>output\_index</strong>`integer`

        目前固定为 0。

        <strong>content\_index</strong>`integer`

        目前固定为 0。
      </td>

      <td>
        ```json
        {
            "event_id": "event_B1osWMWoDRYyITDyNYcBu",
            "type": "response.audio.done",
            "response_id": "resp_P79OOMs8LnrXVpiIHUCKR",
            "item_id": "item_OFaPGtzfWCPyGzxnuEX9i",
            "output_index": 0,
            "content_index": 0
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.speech\_started <span id="vad001speechstartedh2" />

当服务端 VAD（语音活动检测）检测到用户开始说话时，返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`input_audio_buffer.speech_started`。

        <strong>audio\_start\_ms</strong>`integer`

        检测到语音开始的时间点（毫秒），相对于音频流开始的偏移量。

        <strong>item\_id</strong>`string`

        关联的消息项唯一标识符。

        <strong>speaker\_id</strong>`integer`

        适用于 `qwen3.8-livetranslate-flash-realtime`。启用 `speaker_detection` 时返回的说话人标识。可通过本事件的 `item_id` 关联语音识别结果，并通过翻译消息项的 `previous_item_id` 关联译文。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_audio_buffer.speech_started",
            "audio_start_ms": 568,
            "item_id": "item_xxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.speech\_stopped <span id="vad002speechstoppedh2" />

当服务端 VAD 检测到用户停止说话时，返回此事件，标志本轮语音输入结束。由于翻译响应基于流式语音同步生成，实际的翻译响应可能已经在语音输入过程中开始，无需等待此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`input_audio_buffer.speech_stopped`。

        <strong>audio\_end\_ms</strong>`integer`

        检测到语音结束的时间点（毫秒），相对于音频流开始的偏移量。

        <strong>item\_id</strong>`string`

        关联的消息项唯一标识符。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_audio_buffer.speech_stopped",
            "audio_end_ms": 3900,
            "item_id": "item_xxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.committed <span id="ltcommitted001h2" />

Manual 模式（`turn_detection`为`null`）下，客户端发送`input_audio_buffer.commit`事件后，服务端返回此事件进行确认，并自动开始生成翻译响应。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`input_audio_buffer.committed`。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_audio_buffer.committed"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_audio\_buffer.cleared <span id="ltcleared001h2" />

客户端发送`input_audio_buffer.clear`事件后，服务端返回此事件进行确认，表示已清空缓冲区中尚未提交的音频数据。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`input_audio_buffer.cleared`。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "input_audio_buffer.cleared"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.created <span id="ltitemcreated001h2" />

当对话中创建新的消息项时，服务端返回此事件。以下场景会触发此事件：

- 服务端开始生成翻译响应时，创建对应的 assistant 消息项（此时`content`为空数组，内容随流式响应逐步填充）。
- Manual 模式下，客户端发送`input_audio_buffer.commit`事件后，服务端会额外创建一个对应用户输入音频的消息项（`content`中包含`{"type": "input_audio"}`）。

对于同一个 VAD 片段，服务端会分别创建语音识别结果和翻译结果消息项。语音识别结果消息项的 `item.id` 与翻译结果事件的 `previous_item_id` 相同。客户端可据此关联原文和译文，并同时展示。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`conversation.item.created`。

        <strong>previous\_item\_id</strong>`string`

        前一消息项的唯一标识符。对于翻译结果事件，该值与同一 VAD 片段的语音识别结果消息项 `item.id` 相同。

        <strong>item</strong>`object`

        消息项信息。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          消息项的唯一标识符。

          <strong>type</strong>`string`

          固定为`message`。

          <strong>object</strong>`string`

          固定为`realtime.item`。

          <strong>status</strong>`string`

          消息项的状态。

          <strong>role</strong>`string`

          消息的角色，取值为`assistant`或`user`。

          <strong>content</strong>`array`

          消息的内容。响应刚创建时为空数组，随流式响应逐步填充；Manual 模式下 commit 产生的用户消息项中包含`{"type": "input_audio"}`。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.created",
            "previous_item_id": "item_asr_xxx",
            "item": {
                "id": "item_translation_xxx",
                "object": "realtime.item",
                "type": "message",
                "status": "in_progress",
                "role": "assistant",
                "content": []
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.delta <span id="transcription-delta" />

适用于 `qwen3.8-livetranslate-flash-realtime`。 返回输入语音的原文识别增量。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        事件唯一标识符。

        <strong>type</strong>`string`

        `conversation.item.input_audio_transcription.delta`。

        <strong>item\_id</strong>`string`

        消息 ID。

        <strong>content\_index</strong>`integer`

        内容片段索引。

        <strong>delta</strong>`string`

        本次新增的文本片段。按接收顺序累加，不覆盖已收到的文本。
      </td>

      <td>
        ```json
        {
          "event_id": "event_example",
          "type": "conversation.item.input_audio_transcription.delta",
          "item_id": "item_example",
          "content_index": 0,
          "delta": "Hello"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.text <span id="asr002texth2" />

适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。 使用 `qwen3.8-livetranslate-flash-realtime` 时，请处理 [conversation.item.input\_audio\_transcription.delta](#transcription-delta) 事件。

当配置了`input_audio_transcription.model`参数时，服务端会流式返回输入音频的语音识别结果（源语言原文）。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`conversation.item.input_audio_transcription.text`。

        <strong>item\_id</strong>`string`

        消息项唯一标识符。

        <strong>content\_index</strong>`integer`

        目前固定为 0。

        <strong>text</strong>`string`

        已确认的识别文本。

        <strong>stash</strong>`string`

        待确认的识别文本（可能会被后续事件修正）。

        <strong>language</strong>`string`

        检测到的源语种。

        <strong>emotion</strong>`string`

        被识别音频的情感。支持的情感如下：

        - `surprised`：惊讶
        - `neutral`：平静
        - `happy`：愉快
        - `sad`：悲伤
        - `disgusted`：厌恶
        - `angry`：愤怒
        - `fearful`：恐惧
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.input_audio_transcription.text",
            "item_id": "item_xxx",
            "content_index": 0,
            "text": "",
            "stash": "今天天气真好",
            "language": "zh",
            "emotion": "neutral"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.completed <span id="asr003comph2" />

当配置了`input_audio_transcription.model`参数时，语音识别完成后服务端会返回此事件，包含最终的完整识别结果。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`conversation.item.input_audio_transcription.completed`。

        <strong>item\_id</strong>`string`

        消息项唯一标识符。

        <strong>content\_index</strong>`integer`

        目前固定为 0。

        <strong>transcript</strong>`string`

        完整的语音识别结果（源语言原文）。

        <strong>language</strong>`string`

        检测到的源语种。

        <strong>emotion</strong>`string`

        被识别音频的情感。支持的情感如下：

        - `surprised`：惊讶
        - `neutral`：平静
        - `happy`：愉快
        - `sad`：悲伤
        - `disgusted`：厌恶
        - `angry`：愤怒
        - `fearful`：恐惧
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.input_audio_transcription.completed",
            "item_id": "item_xxx",
            "content_index": 0,
            "transcript": "今天天气真好，我们一起去公园散步吧。",
            "language": "zh",
            "emotion": ""
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## conversation.item.input\_audio\_transcription.failed <span id="ltasrfailed001h2" />

当输入了音频但识别失败时，服务端发送该事件。与其他`error`事件分开处理，便于客户端识别相关的具体项目。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>type</strong>`string`

        事件类型，固定为`conversation.item.input_audio_transcription.failed`。

        <strong>item\_id</strong>`string`

        关联的对话项 ID。

        <strong>content\_index</strong>`integer`

        包含音频的内容部分的索引。

        <strong>error.code</strong>`string`

        错误代码。

        <strong>error.message</strong>`string`

        错误消息。
      </td>

      <td>
        ```json
        {
            "event_id": "event_xxx",
            "type": "conversation.item.input_audio_transcription.failed",
            "item_id": "item_xxx",
            "content_index": 0,
            "error": {
                "code": "xxx",
                "message": "xxx",
                "param": "xxx"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio\_transcript.delta <span id="audio-transcript-delta" />

适用于 `qwen3.8-livetranslate-flash-realtime`。输出文本和音频时，返回译文的新增文本片段。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        事件唯一标识符。

        <strong>type</strong>`string`

        `response.audio_transcript.delta`。

        <strong>response\_id</strong>`string`

        响应 ID。

        <strong>item\_id</strong>`string`

        消息 ID。

        <strong>output\_index</strong>`integer`

        输出项索引。

        <strong>content\_index</strong>`integer`

        内容片段索引。

        <strong>delta</strong>`string`

        本次新增的文本片段。按接收顺序累加，不覆盖已收到的文本。
      </td>

      <td>
        ```json
        {
          "event_id": "event_example",
          "type": "response.audio_transcript.delta",
          "response_id": "resp_example",
          "item_id": "item_example",
          "output_index": 0,
          "content_index": 0,
          "delta": "Hello"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio\_transcript.text <span id="35396453cfood" />

适用于 `qwen3.5-livetranslate-flash-realtime` 等模型。 使用 `qwen3.8-livetranslate-flash-realtime` 时，请处理 [response.audio\_transcript.delta](#audio-transcript-delta) 事件。

当输出模态包含音频时，服务端可能返回此事件，用于展示实时翻译内容。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio_transcript.text`。

        <strong>response\_id</strong>`string`

        响应的唯一标识符。

        <strong>item\_id</strong>`string`

        消息项唯一标识符。

        <strong>output\_index</strong>`integer`

        目前固定为 0。

        <strong>content\_index</strong>`integer`

        目前固定为 0。

        <strong>text</strong>`string`

        已确认无误的翻译文本片段。

        <strong>stash</strong>`string`

        初步翻译的临时文本，与当前 `text` 拼接后构成临时翻译结果；系统会通过 `response.audio_transcript.text` 事件持续更新 `text` 和 `stash`，直至收到[response.audio\_transcript.done](/zh/model-studio/live-translator-server-events#f4d1698567bsm)事件，此时可通过 `transcript` 字段获取完整的最终翻译文本。
      </td>

      <td>
        ```json
        {
          "event_id": "event_xxx",
          "type": "response.audio_transcript.text",
          "response_id": "resp_xxx",
          "item_id": "item_xxx",
          "output_index": 0,
          "content_index": 0,
          "text": "Hello,",
          "stash": " who are you?"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio\_transcript.done <span id="f4d1698567bsm" />

当输出模态包含音频，且模型生成文本结束时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio_transcript.done`。

        <strong>response\_id</strong>`string`

        响应的唯一标识符。

        <strong>item\_id</strong>`string`

        消息项唯一标识符。

        <strong>output\_index</strong>`integer`

        目前固定为 0。

        <strong>content\_index</strong>`integer`

        目前固定为 0。

        <strong>transcript</strong>`string`

        完整文本。
      </td>

      <td>
        ```json
        {
            "event_id": "event_VN4Q4GJugLcc1S23viW8E",
            "type": "response.audio_transcript.done",
            "response_id": "resp_P79OOMs8LnrXVpiIHUCKR",
            "item_id": "item_JvJauNH2CTXb1D9WV6pD4",
            "output_index": 0,
            "content_index": 0,
            "transcript": "How can I assist you today?"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.output\_item.added <span id="dae2260d40qtu" />

在响应生成过程中创建新输出项时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.output_item.added`。

        <strong>response\_id</strong>`string`

        响应的唯一标识符。

        <strong>output\_index</strong>`integer`

        目前固定为 0。

        <strong>item</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          输出项的唯一标识符。

          <strong>type</strong>`string`

          固定为 `message`。

          <strong>object</strong>`string`

          始终为 `realtime.item` 。

          <strong>status</strong>`string`

          输出项的状态。

          <strong>role</strong>`string`

          消息的角色。

          <strong>content</strong>`string`

          消息的内容。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_B4O5yPt3Gjnjy5eYH3plG",
            "type": "response.output_item.added",
            "response_id": "resp_P79OOMs8LnrXVpiIHUCKR",
            "output_index": 0,
            "item": {
                "id": "item_OFaPGtzfWCPyGzxnuEX9i",
                "object": "realtime.item",
                "type": "message",
                "status": "in_progress",
                "role": "assistant",
                "content": []
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.output\_item.done <span id="f580421f45w3h" />

当新的项输出完成时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.output_item.done`。

        <strong>response\_id</strong>`string`

        响应的唯一标识符。

        <strong>output\_index</strong>`integer`

        目前固定为 0。

        <strong>item</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          输出项的唯一标识符。

          <strong>object</strong>`string`

          始终为 `realtime.item` 。

          <strong>type</strong>`string`

          固定为 `message`。

          <strong>status</strong>`string`

          输出项的状态。

          <strong>role</strong>`string`

          发送消息的角色。

          <strong>content</strong>`string`

          消息的内容。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_XkiwbYTBC9Wcdwy6uYJ2G",
            "type": "response.output_item.done",
            "response_id": "resp_P79OOMs8LnrXVpiIHUCKR",
            "output_index": 0,
            "item": {
                "id": "item_JvJauNH2CTXb1D9WV6pD4",
                "object": "realtime.item",
                "type": "message",
                "status": "completed",
                "role": "assistant",
                "content": [
                    {
                        "type": "audio",
                        "text": "你好，我是阿里云研发的大规模语言模型，我叫千问。有什么我可以帮助你的吗？"
                    }
                ]
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.content\_part.added <span id="de7fa0b877j25" />

当新的内容部分输出时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.content_part.added`。

        <strong>response\_id</strong>`string`

        响应的唯一标识符。

        <strong>item\_id</strong>`string`

        消息项唯一标识符。

        <strong>output\_index</strong>`integer`

        目前固定为 0。

        <strong>content\_index</strong>`integer`

        目前固定为 0。

        <strong>part</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          内容部分的类型。

          <strong>text</strong>`string`

          内容部分的文本。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_J2UixwYKZsXg7c9YXZetL",
            "type": "response.content_part.added",
            "response_id": "resp_P79OOMs8LnrXVpiIHUCKR",
            "item_id": "item_OFaPGtzfWCPyGzxnuEX9i",
            "output_index": 0,
            "content_index": 0,
            "part": {
                "type": "audio",
                "text": ""
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.content\_part.done <span id="011ad54242wft" />

当新的内容部分输出完成时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        本次事件唯一标识符。

        <strong>type</strong>`string`

        事件类型，固定为`response.content_part.done`。

        <strong>response\_id</strong>`string`

        响应的唯一标识符。

        <strong>item\_id</strong>`string`

        消息项唯一标识符。

        <strong>output\_index</strong>`integer`

        目前固定为 0。

        <strong>content\_index</strong>`integer`

        目前固定为 0。

        <strong>part</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          内容部分的类型。

          <strong>text</strong>`string`

          内容部分的文本。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_VN4Q4GJugLcc1S23viW8E",
            "type": "response.content_part.done",
            "response_id": "resp_P79OOMs8LnrXVpiIHUCKR",
            "item_id": "item_JvJauNH2CTXb1D9WV6pD4",
            "output_index": 0,
            "content_index": 0,
            "part": {
                "type": "audio",
                "text": "你好，我是阿里云研发的大规模语言模型，我叫千问。有什么我可以帮助你的吗？"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
