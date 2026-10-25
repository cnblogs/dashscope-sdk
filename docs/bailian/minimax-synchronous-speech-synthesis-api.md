> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# MiniMax同步语音合成API参考

<meta property="og:title" content="MiniMax同步语音合成API参考" />

## 支持的模型 <span id="b00a633ba3xh5" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "24.98%" }} /><col style={{ width: "24.98%" }} /><col style={{ width: "24.98%" }} /><col style={{ width: "25.06%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>模型名称</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>语音合成单价（每万字符）</strong></p></th><th style={{ verticalAlign: "top" }}><p><a href="/zh/model-studio/mini-clone-api">复刻一个音色</a></p></th><th style={{ verticalAlign: "top" }}><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota#977b13081ab56">（注）</a></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>MiniMax/speech-2.8-hd</p></td><td style={{ verticalAlign: "top" }}><p>3.5元</p></td><td rowSpan={4} style={{ verticalAlign: "top" }}><p>9.9元</p> <p>（在首次使用复刻出来的音色进行语音合成的时候收取）</p></td><td rowSpan={4} style={{ verticalAlign: "top" }}><p>无</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MiniMax/speech-02-hd</p></td><td style={{ verticalAlign: "top" }}><p>3.5元</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MiniMax/speech-2.8-turbo</p></td><td style={{ verticalAlign: "top" }}><p>2元</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MiniMax/speech-02-turbo</p></td><td style={{ verticalAlign: "top" }}><p>2元</p></td></tr></tbody></table>

## URL <span id="fe508c8badxjg" />

<Tabs>
  <Tab title="华北2（北京）">
    HTTP请求地址：`POST https://dashscope.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    SDK调用配置的base\_url：`https://dashscope.aliyuncs.com/api/v1`
  </Tab>
</Tabs>

## Headers <span id="f5b7af17168sz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "24.99%" }} /><col style={{ width: "8.54%" }} /><col style={{ width: "10.32%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>是否必选</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Authorization</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时，将“<code>\<your\_api\_key></code>”替换为实际的API Key。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Content-Type</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>请求体的媒体类型，固定为<code>application/json</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>X-DashScope-SSE</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>是否启用 SSE（Server-Sent Events）流式输出。设为 <code>enable</code> 时，服务端将以流式事件逐步返回结果，适用于实时性要求较高的场景。不设置时，使用默认的同步响应模式。</p></td></tr></tbody></table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## 请求体 <span id="2a1c410015otp" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。支持：

        <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "100%" }} /></colgroup><tbody><tr><td style={{ verticalAlign: "top" }}><p>MiniMax/speech-2.8-hd</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MiniMax/speech-02-hd</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MiniMax/speech-2.8-turbo</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MiniMax/speech-02-turbo</p></td></tr></tbody></table>

        <strong>input</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>text</strong> `string`<strong>（必选）</strong>

          待合成语音的文本。

          长度限制小于 10000 字符，若文本长度大于 3000 字符，推荐使用流式输出。

          <strong>stream\_options</strong> `object`（可选）

          流式输出的配置项，仅在请求头 `X-DashScope-SSE: enable` 时生效。

          <Accordion title="属性">
            <strong>exclude\_aggregated\_audio</strong> `boolean`（可选） 默认值为`false`

            控制流式输出的合成结束帧中，`audio` 字段是否返回本次合成的完整音频（即此前所有合成中分块拼接后的整段 hex 数据）。

            取值范围：

            - `false`：合成结束帧的 `audio` 字段返回所有分块拼接后的完整音频，客户端可在流结束时直接取用整段音频，无需自行拼接前面的分块。
            - `true`：合成结束帧的 `audio` 字段为空字符串，整段音频需由客户端将此前所有合成中分块的 `audio` 按顺序自行拼接得到。可显著减少尾包体积与传输耗时，适用于客户端已经按 chunk 边接收边播放、或自行累积保存音频的场景。

            <Note>
              该参数仅在流式输出场景下生效；非流式输出场景下设置无效。
            </Note>
          </Accordion>

          <strong>voice\_setting</strong> `object`<strong>（必选）</strong>

          <Accordion title="属性">
            <strong>voice\_id</strong> `string`<strong>（必选）</strong>

            设置音色ID。

            若需要设置混合音色，请设置 `timbre_weights` 参数，本参数设置为空值。

            <strong>speed</strong> `float`（可选） 默认值为`1.0`

            设置语速。

            取值范围：`[0.5, 2.0]`。

            <strong>vol</strong> `float`（可选） 默认值为`1.0`

            设置音量。

            取值范围：`(0.0, 10.0]`。

            <strong>pitch</strong> `integer`（可选） 默认值为`0`

            设置音高。

            取值范围：`[-12, 12]`。

            <strong>emotion</strong> `string`（可选） 无默认值

            设置情感。模型会根据输入文本自动匹配合适的情感，一般无需手动指定。

            取值范围：

            - `happy`：高兴
            - `sad`：悲伤
            - `angry`：愤怒
            - `fearful`：害怕
            - `disgusted`：厌恶
            - `surprised`：惊讶
            - `calm`：中性
            - `whisper`：低语

            <Note>
              `speech-2.8-hd`, `speech-2.8-turbo` 模型不支持 `whisper`
            </Note>

            <strong>text\_normalization</strong> `boolean`（可选） 默认值为`false`

            是否启用中文、英语文本规范化，开启后可提升数字阅读场景的性能，但会略微增加延迟。

            取值范围：

            - `true`：开启
            - `false`：关闭

            <strong>latex\_read</strong> `boolean`（可选） 默认值为`false`

            是否启用朗读 LaTeX 公式的功能。

            取值范围：

            - `true`：开启
            - `false`：关闭

            示例：

            x = 2 a − b ± b 2 − 4 a c ​ ​

            如上公式应表示为：`$$x = \\frac{-b \\pm \\sqrt{b^2 - 4ac}}{2a}$$`。

            <Note>
              - 仅支持中文，开启该功能后，`language_boost`参数会被设置为`Chinese`。
              - 请求中的公式需要在公式的首尾加上 `$$`。
              - 请求中公式若有 `"\"`，需转义成 `"\\"`。
            </Note>
          </Accordion>

          <strong>audio\_setting</strong> `object`（可选）

          <Accordion title="属性">
            <strong>sample\_rate</strong> `integer`（可选） 默认值为`32000`

            设置生成的音频的采样率（单位为Hz）。

            取值范围：

            - `8000`
            - `16000`
            - `22050`
            - `24000`
            - `32000`
            - `44100`

            <strong>bitrate</strong> `integer`（可选） 默认值为`128000`

            设置生成的音频的码率（单位kbps）。

            取值范围：

            - `32000`
            - `64000`
            - `128000`
            - `256000`

            <Note>
              该参数仅在`format`参数为`mp3`时生效。对于其他格式，该参数将被忽略。
            </Note>

            <strong>format</strong> `string`（可选） 默认值为`mp3`

            设置生成的音频的格式。

            取值范围：

            - `mp3`
            - `pcm`
            - `flac`
            - `wav`

            <strong>channel</strong> `integer`（可选） 默认值为`1`

            设置生成的音频的声道数。

            取值范围：

            - `1`：单声道
            - `2`：双声道

            <strong>force\_cbr</strong> `boolean`（可选） 默认值为`false`

            是否以恒定码率进行音频编码。

            取值范围：

            - `true`：是
            - `false`：否

            <Note>
              仅流式输出且音频格式为`mp3`时该参数生效。
            </Note>
          </Accordion>

          <strong>pronunciation\_dict</strong> `object`（可选）

          <Accordion title="属性">
            <strong>tone</strong> `string[]`（可选）

            定义需要特殊标注的文字或符号对应的注音或发音替换规则。

            > 使用 / 作为分隔符。

            在中文文本中，声调用数字表示：一声为 1，二声为 2，三声为 3，四声为 4，轻声为 5。

            示例：

            `["燕少飞/(yan4)(shao3)(fei1)", "omg/oh my god"]`
          </Accordion>

          <strong>timbre\_weights</strong> `object[]`（可选）

          若需要设置混合音色，请对该参数进行设置。最多支持 4 种音色混合。

          <Accordion title="属性">
            <strong>voice\_id</strong> `string`<strong>（必选）</strong>

            设置音色ID。

            <strong>weight</strong> `integer`<strong>（必选）</strong>

            设置音色所占权重，必须与 `voice_id` 同步填写。单一音色取值占比越高，合成音色与该音色相似度越高。

            取值范围：`[1, 100]`。
          </Accordion>

          <strong>language\_boost</strong> `string`（可选） 默认值为`null`

          是否增强对指定的小语种和方言的识别能力。可设置为 `auto` 让模型自主判断。

          <Accordion title="取值范围（点击查看）：">
            - `Chinese`
            - `Chinese,Yue`
            - `English`
            - `Arabic`
            - `Russian`
            - `Spanish`
            - `French`
            - `Portuguese`
            - `German`
            - `Turkish`
            - `Dutch`
            - `Ukrainian`
            - `Vietnamese`
            - `Indonesian`
            - `Japanese`
            - `Italian`
            - `Korean`
            - `Thai`
            - `Polish`
            - `Romanian`
            - `Greek`
            - `Czech`
            - `Finnish`
            - `Hindi`
            - `Bulgarian`
            - `Danish`
            - `Hebrew`
            - `Malay`
            - `Persian`
            - `Slovak`
            - `Swedish`
            - `Croatian`
            - `Filipino`
            - `Hungarian`
            - `Norwegian`
            - `Slovenian`
            - `Catalan`
            - `Nynorsk`
            - `Tamil`
            - `Afrikaans`
            - `auto`
          </Accordion>

          <strong>voice\_modify</strong> `object`（可选）

          设置声音效果，该参数支持的音频格式：

          - 非流式：`mp3`、`wav`、`flac`
          - 流式：`mp3`

          <Accordion title="属性">
            <strong>pitch</strong> `integer`（可选） 无默认值

            设置音高（低沉/明亮）。

            数值越低声音越低沉，数值越高声音越明亮。

            取值范围：`[-100, 100]`。

            <strong>intensity</strong> `integer`（可选） 无默认值

            设置强度（力量感/柔和）。

            数值越低声音越刚劲，数值越高声音越轻柔。

            取值范围：`[-100, 100]`。

            <strong>timbre</strong> `integer`（可选） 无默认值

            设置音色明暗程度（磁性/清脆）。

            数值越低声音越浑厚，数值越高声音越清脆。

            取值范围：`[-100, 100]`。

            <strong>sound\_effects</strong> `string`（可选） 无默认值

            设置音效。

            取值范围：

            - `spacious_echo`：空旷回音
            - `auditorium_echo`：礼堂广播
            - `lofi_telephone`：电话失真
            - `robotic`：电音
          </Accordion>

          <strong>subtitle\_enable</strong> `boolean`（可选） 默认值为`false`

          是否开启字幕。

          取值范围：

          - `true`：是
          - `false`：否

          <Note>
            该参数仅在非流式输出场景下有效，且仅对 `speech-2.8-hd`, `speech-2.8-turbo`, `speech-2.6-hd`, `speech-2.6-turbo`, `speech-02-hd`, `speech-02-turbo`, `speech-01-hd`, `speech-01-turbo` 模型有效。
          </Note>

          <strong>output\_format</strong> `string`（可选） 默认值为`hex`

          设置输出结果形式。

          取值范围：

          - `url`：语音合成结果以URL形式返回，有效期为24小时
          - `hex`：语音合成结果以二进制形式返回

          <Note>
            该参数仅在非流式场景中生效（流式场景只返回`hex`形式）。
          </Note>

          <strong>aigc\_watermark</strong> `boolean`（可选） 默认值为`false`

          是否在合成音频末尾添加 AIGC 隐性标识。

          取值范围：

          - `true`：是
          - `false`：否

          <Note>
            该参数仅在非流式输出场景下有效。
          </Note>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```bash 非流式 expandable
          curl -X POST "https://dashscope.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation" \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -d '{
            "model": "MiniMax/speech-2.8-hd",
            "input": {
              "text": "今天是不是很开心呀(laughs)，当然了！",
              "voice_setting": {
                "voice_id": "male-qn-qingse",
                "speed": 1,
                "vol": 1,
                "pitch": 0,
                "emotion": "happy"
              },
              "audio_setting": {
                "sample_rate": 32000,
                "bitrate": 128000,
                "format": "mp3",
                "channel": 1
              },
              "pronunciation_dict": {
                "tone": [
                  "处理/(chu3)(li3)",
                  "危险/dangerous"
                ]
              },
              "subtitle_enable": false
            }
          }'
          ```

          ```bash 流式 expandable
          curl -X POST "https://dashscope.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation" \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -H "X-DashScope-SSE: enable" \
          -d '{
            "model": "MiniMax/speech-2.8-hd",
            "input": {
              "text": "今天是不是很开心呀(laughs)，当然了！",
              "voice_setting": {
                "voice_id": "male-qn-qingse",
                "speed": 1,
                "vol": 1,
                "pitch": 0,
                "emotion": "happy"
              },
              "audio_setting": {
                "sample_rate": 32000,
                "bitrate": 128000,
                "format": "mp3",
                "channel": 1
              },
              "pronunciation_dict": {
                "tone": [
                  "处理/(chu3)(li3)",
                  "危险/dangerous"
                ]
              },
              "subtitle_enable": false
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

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>base\_resp</strong>`object`

          本次请求的状态码和详情。

          <Accordion title="属性">
            <strong>status\_code</strong> `integer`

            状态码。

            您可在header中获取本次会话的trace\_id，用于在咨询/反馈时帮助定位问题

            - `0`: 请求结果正常
            - `1000`: 未知错误
            - `1001`: 超时
            - `1002`: 触发限流
            - `1004`: 鉴权失败
            - `1039`: 触发 TPM 限流
            - `1042`: 非法字符超过 10%
            - `2013`: 输入参数信息不正常

            更多内容可查看 [<strong>错误码查询列表</strong>](https://platform.minimaxi.com/docs/api-reference/errorcode) 了解详情

            <strong>status\_msg</strong> `string`

            状态详情。
          </Accordion>

          <strong>data</strong>`object`

          返回的合成数据对象，可能为 null，需进行非空判断。

          <Accordion title="属性">
            <strong>audio</strong>`string`

            合成后的音频数据，采用 hex 编码，格式与请求中（`output_format`参数）指定的输出格式一致。

            <strong>status</strong> `integer`

            当前音频流状态：1 表示合成中，2 表示合成结束
          </Accordion>

          <strong>extra\_info</strong>`object`

          <Accordion title="属性">
            <strong>audio\_length</strong>`integer`

            音频时长（毫秒）

            <strong>audio\_sample\_rate</strong>`integer`

            音频采样率

            <strong>audio\_size</strong>`integer`

            音频文件大小（字节）

            <strong>bitrate</strong>`integer`

            音频比特率

            <strong>audio\_format</strong>`string`

            生成音频文件的格式。取值范围 `[mp3, pcm, flac]`

            可用选项： `mp3`, `pcm`, `flac`。

            [<strong>​</strong>](https://platform.minimaxi.com/docs/api-reference/speech-t2a-http#response-extra-info-audio-channel)<strong>audio\_channel</strong> `integer`

            生成音频声道数,1：单声道，2：双声道

            [<strong>​</strong>](https://platform.minimaxi.com/docs/api-reference/speech-t2a-http#response-extra-info-invisible-character-ratio)<strong>invisible\_character\_ratio</strong> `float`

            非法字符占比.非法字符不超过 10%（包含 10%），音频会正常生成,并返回非法字符占比数据；如超过 10% 将进行报错

            [<strong>​</strong>](https://platform.minimaxi.com/docs/api-reference/speech-t2a-http#response-extra-info-usage-characters)<strong>usage\_characters</strong>`integer`

            计费字符数

            <strong>word\_count</strong>`integer`

            已发音的字数统计，包含汉字、数字、字母，不包含标点符号
          </Accordion>

          <strong>trace\_id</strong>`string`

          本次会话的 id，用于在咨询/反馈时帮助定位问题
        </Accordion>

        <strong>base\_resp</strong>`object`

        本次请求的状态码和详情。

        <Accordion title="属性">
          <strong>audio</strong>`string`

          合成后的音频数据，采用 hex 编码，格式与请求中（`output_format`参数）指定的输出格式一致。
        </Accordion>

        <strong>usage</strong>`object`

        本次请求的计费用量。

        <Accordion title="属性" defaultOpen>
          <strong>characters</strong> `integer`

          输入文本的字符数。

          <strong>count</strong> `integer`（可选）

          本次请求产生的复刻音色解锁计费次数。首次使用复刻音色合成语音会返回此字段，值为 `1`；后续使用同一音色时不返回此字段。

          音色解锁费仅收取一次。

          <CodeGroup>
            ```json 首次使用复刻音色
            {
                "characters": 17,
                "count": 1
            }
            ```

            ```json 再次使用同一音色
            {
                "characters": 17
            }
            ```
          </CodeGroup>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```json 非流式 expandable
          {
              "output": {
                  "base_resp": {
                      "status_code": 0,
                      "status_msg": "success"
                  },
                  "data": {
                      "audio": "<hex编码的audio>",
                      "ced": "",
                      "status": 2
                  },
                  "extra_info": {
                      "audio_channel": 1,
                      "audio_format": "mp3",
                      "audio_length": 4536,
                      "audio_sample_rate": 32000,
                      "audio_size": 74292,
                      "bitrate": 128000,
                      "invisible_character_ratio": 0,
                      "usage_characters": 34,
                      "word_count": 22
                  },
                  "trace_id": "07251d0708c723e3a8f8dd979928e429"
              },
              "usage": {
                  "characters": 34
              },
              "request_id": "35c7bb92-2898-9c4a-9be4-200bc7c5d61f"
          }
          ```

          ```json 流式 expandable
          {
              "output": {
                  "base_resp": {
                      "status_code": 0,
                      "status_msg": "success"
                  },
                  "data": {
                      "audio": "<hex编码的audio>",
                      "ced": "",
                      "status": 2
                  },
                  "extra_info": {
                      "audio_channel": 1,
                      "audio_format": "mp3",
                      "audio_length": 4003,
                      "audio_sample_rate": 32000,
                      "audio_size": 66804,
                      "bitrate": 128000,
                      "invisible_character_ratio": 0,
                      "usage_characters": 34,
                      "word_count": 22
                  },
                  "trace_id": "07251d0bd210b373b1c718f87b8452f4"
              },
              "usage": {
                  "characters": 34
              },
              "request_id": "eae023d1-5bbd-919f-8586-90e0ee99cd7e"
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>
