> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 非实时语音合成CosyVoice HTTP API参考

> 本文介绍非实时语音合成CosyVoice的HTTP调用方法，支持非流式和流式两种调用模式。

<meta property="og:title" content="非实时语音合成CosyVoice HTTP API参考" />

## 接口地址 <span id="fe508c8badxjg" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/SpeechSynthesizer`
  </Tab>
</Tabs>

调用时请将 `{WorkspaceId}` 替换为对应地域的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)，并使用同地域的 [API Key](/zh/model-studio/get-api-key)。

## 请求头 <span id="f5b7af17168sz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "22%" }} />

    <col style={{ width: "14%" }} />

    <col style={{ width: "10%" }} />

    <col style={{ width: "54%" }} />
  </colgroup>

  <thead>
    <tr>
      <th style={{ verticalAlign: "top" }}>
        <span><strong>参数</strong></span>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <span><strong>类型</strong></span>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <span><strong>是否必选</strong></span>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <span><strong>说明</strong></span>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>Authorization</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <code>string</code>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>是</span>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时，将“<code>\<your\_api\_key></code>”替换为实际的API Key。</span>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>Content-Type</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <code>string</code>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>是</span>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>请求体的媒体类型，固定为<code>application/json</code>。</span>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>X-DashScope-SSE</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <code>string</code>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>否</span>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>用于控制是否以流式方式返回输出结果。仅在流式合成时使用该参数，参数值固定为<code>enable</code>。</span>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## 请求体 <span id="2a1c410015otp" />

        <strong>model</strong> `string`<strong>（必选）</strong>

        语音合成模型。

        取值范围：

        - cosyvoice-v3.5-plus
        - cosyvoice-v3.5-flash
        - cosyvoice-v3-plus
        - cosyvoice-v3-flash
        - cosyvoice-v2

        <strong>input</strong> `object`<strong>（必选）</strong>

        输入参数对象<strong>。</strong>

        <Accordion title="属性" defaultOpen>
          <strong>text</strong> `string`<strong>（必选）</strong>

          待合成文本。

          支持 SSML 和 LaTeX 格式输入。将待合成文本替换为对应格式即可。

          - 使用 SSML 时，需同时将 `enable_ssml` 设置为 `true`。支持的 SSML 标签及用法，请参见[SSML 与 LaTeX](/zh/model-studio/ssml-latex-user-guide)。
          - 使用 LaTeX 时，将待合成文本替换为 LaTeX 格式即可，无需额外配置。支持的 LaTeX 语法及用法，请参见[LaTeX 公式转语音](/zh/model-studio/latex-capability-support-description)。

          <strong>voice</strong> `string`<strong>（必选）</strong>

          音色。

          取值范围：

          - 系统音色：参见[CosyVoice音色列表](/zh/model-studio/cosyvoice-voice-list)
          - 声音复刻音色：如何创建音色请参见<Link href="/zh/model-studio/voice-clone-design-http-api">声音复刻HTTP API参考</Link>
          - 声音设计音色：如何创建音色请参见<Link href="/zh/model-studio/voice-design-api-references">声音设计API参考</Link>

          <strong>format</strong> `string`（可选）

          音频编码格式。

          默认值：mp3。

          取值范围：

          - mp3
          - pcm
          - wav
          - opus

          <strong>sample\_rate</strong> `integer`（可选）

          音频采样率（Hz）。

          取值范围：8000、12000、16000、22050（默认）、24000、44100、48000。

          <Note>
            当 `format` 为 `opus` 时，`sample_rate` 仅支持 8000、12000、16000、24000、48000。
          </Note>

          <strong>volume</strong> `integer`（可选）

          音量。

          默认值：50。

          取值范围：\[0, 100]。

          <strong>rate</strong> `float`（可选）

          语速。

          默认值：1.0。

          取值范围：\[0.5, 2.0]。

          <strong>bit\_rate</strong> `integer`（可选）

          音频码率（单位：kbps）。

          默认值：32。

          取值范围：\[6, 510]。

          <Tip>
            仅在`format`为`opus`时支持使用该参数。
          </Tip>

          <strong>pitch</strong> `float`（可选）

          音调。

          默认值：1.0。

          取值范围：\[0.5, 2.0]。

          <strong>enable\_ssml</strong> `boolean`（可选）

          是否开启SSML功能。SSML 的使用限制（支持的模型、音色和接口），请参见[使用限制](/zh/model-studio/ssml-latex-user-guide#sl01_constraint_h3)。

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

          设置指令，用于控制方言、情感或角色等合成效果。

          具体用法请参见[非实时语音合成](/zh/model-studio/non-realtime-tts-user-guide)。

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
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```bash 非流式 expandable
          curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/SpeechSynthesizer \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -d '{
              "model": "cosyvoice-v3-plus",
              "input": {
                "text": "我家的后面有一个很大的花园。",
                "voice": "longanyang",
                "format": "wav",
                "sample_rate": 24000
              }
          }'
          ```

          ```bash 流式 expandable
          curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/SpeechSynthesizer \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -H "X-DashScope-SSE: enable" \
          -d '{
              "model": "cosyvoice-v3-plus",
              "input": {
                "text": "我家的后面有一个很大的花园。",
                "voice": "longanyang",
                "format": "wav",
                "sample_rate": 24000
              }
          }'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## 返回体 <span id="0e1c44902ea1e" />

        <strong>request\_id</strong> `string`

        本次调用的唯一标识符。

        <strong>output</strong> `object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>finish\_reason</strong> `string`

          任务停止原因，自然停止时为`stop`。

          取值范围：

          - null：语音合成中
          - stop：语音合成结束

          <strong>type</strong> `string`

          子事件类型。仅流式合成时返回该值。

          取值范围：

          - sentence-begin：标识句子开始，返回待合成的句子文本内容
          - sentence-synthesis：标识音频数据块

            - 一个句子的合成过程中会产生多个`sentence-synthesis`事件，每个对应一个音频数据块
            - 客户端需要按顺序接收这些音频数据块并以追加模式写入同一文件
            - `sentence-synthesis`事件与其后的音频数据帧是一一对应的关系，不会出现错位
          - sentence-end：标识句子结束，返回句子文本内容和累计的计费字符数

          <strong>original\_text</strong> `string`

          对用户输入文本进行分句后的句内容。最后一个句子可能没有此字段。

          <strong>sentence</strong> `object`

          句子信息。

          <Accordion title="属性" defaultOpen>
            <strong>index</strong> `integer`

            句子的编号，从0开始。

            <strong>words</strong> `array`

            每句话对应的字的信息。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong> `string`

              字。

              <strong>begin\_index</strong> `integer`

              字在句子中的开始位置索引，从 0 开始。

              <strong>end\_index</strong> `integer`

              字在句子中的结束位置索引，从 1 开始。

              <strong>begin\_time</strong> `integer`

              字对应音频的开始时间戳，单位为毫秒。

              <strong>end\_time</strong> `integer`

              字对应音频的结束时间戳，单位为毫秒。
            </Accordion>
          </Accordion>

          <strong>audio</strong> `object`

          合成的音频数据。

          <Accordion title="属性" defaultOpen>
            <strong>data</strong> `string`

            流式合成时输出Base64格式音频数据。非流式合成时为空。

            <strong>url</strong> `string`

            模型输出的完整音频文件的URL，有效期24小时。

            <strong>id</strong> `string`

            模型输出的音频信息对应的ID。

            <strong>expires\_at</strong> `integer`

            `url` 过期时间戳。
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        本次请求的字符用量。

        <Accordion title="属性" defaultOpen>
          <strong>characters</strong> `integer`

          本次请求中计费的有效字符数。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Tabs>
          <Tab title="非流式">
            ```json expandable
            {
                "request_id": "ee88b03d-0457-9286-8c67-xxxxxxxxxxxx",
                "output": {
                    "finish_reason": "stop",
                    "audio": {
                        "data": "",
                        "url": "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/pre/cosyvoice-v3-flash/20260304/xxxxxxxx/ee88b03d-0457-9286-8c67-xxxxxxxxxxxx.wav?xxxxxxx",
                        "id": "audio_ee88b03d-0457-9286-8c67-xxxxxxxxxxxx",
                        "expires_at": 1772697707
                    }
                },
                "usage": {
                    "characters": 15
                }
            }
            ```
          </Tab>

          <Tab title="流式">
            中间结果：

            ```json expandable
            {
                "request_id": "8ac1cd04-06af-9a63-b031-xxxxxxxxxxxx",
                "output": {
                    "finish_reason": "null",
                    "type": "sentence-begin",
                    "original_text": "我家的后面有一个很大的花园。",
                    "sentence": {
                        "index": 0,
                        "words": []
                    },
                    "audio": {
                        "data": "",
                        "id": "audio_ee88b03d-0457-9286-8c67-xxxxxxxxxxxx",
                        "expires_at": 1772697707
                    }
                },
                "usage": {
                    "characters": 15
                }
            }
            ```

            最终结果：

            ```json expandable
            {
                "request_id": "8ac1cd04-06af-9a63-b031-xxxxxxxxxxxx",
                "output": {
                    "finish_reason": "stop",
                    "audio": {
                        "data": "",
                        "url": "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/pre/cosyvoice-v3-flash/20260304/xxxxxxxx/8ac1cd04-06af-9a63-b031-xxxxxxxxxxxx.wav?xxxxxxx",
                        "id": "audio_8ac1cd04-06af-9a63-b031-xxxxxxxxxxxx",
                        "expires_at": 1772698611
                    }
                },
                "usage": {
                    "characters": 15
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>
