> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 声音复刻HTTP API参考

> 本文介绍声音复刻的HTTP API接口详情，包括创建音色、查询音色列表、查询音色详情、更新音色和删除音色等操作。

<meta property="og:title" content="声音复刻HTTP API参考" />

## 接口地址 <span id="h2-url" />

### Qwen-Audio-TTS/Qwen-Audio-Realtime/CosyVoice <span id="h3-url-enrollment" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/audio/tts/customization`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`
</Tip>

### MiniMax <span id="h2-mm-endpoint" />

HTTP请求地址：`POST https://dashscope.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

SDK调用配置的base\_url：`https://dashscope.aliyuncs.com/api/v1`

<Note>
  第三方模型（MiniMax）目前仅支持通过`dashscope.aliyuncs.com`域名调用，暂不支持专属域名。
</Note>

## 请求头 <span id="h2-headers" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "24.99%" }} />

    <col style={{ width: "8.54%" }} />

    <col style={{ width: "10.32%" }} />

    <col style={{ width: "56.15%" }} />
  </colgroup>

  <thead>
    <tr>
      <th style={{ verticalAlign: "top" }}><strong>参数</strong></th>
      <th style={{ verticalAlign: "top" }}><strong>类型</strong></th>
      <th style={{ verticalAlign: "top" }}><strong>是否必选</strong></th>
      <th style={{ verticalAlign: "top" }}><strong>说明</strong></th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        Authorization
      </td>

      <td style={{ verticalAlign: "top" }}>
        string
      </td>

      <td style={{ verticalAlign: "top" }}>
        是
      </td>

      <td style={{ verticalAlign: "top" }}>
        鉴权令牌，格式为`Bearer <your_api_key>`，使用时，将"`<your_api_key>`"替换为实际的API Key。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        Content-Type
      </td>

      <td style={{ verticalAlign: "top" }}>
        string
      </td>

      <td style={{ verticalAlign: "top" }}>
        是
      </td>

      <td style={{ verticalAlign: "top" }}>
        请求体的媒体类型。按模型系列设置：

        - Qwen-Audio-TTS/Qwen-Audio-Realtime/CosyVoice：`application/json`。
        - MiniMax：`application/json; charset=utf-8`。
      </td>
    </tr>
  </tbody>
</table>

## 创建音色 <span id="h2-create" />

### Qwen-Audio-TTS/Qwen-Audio-Realtime/CosyVoice <span id="h3-create-enrollment" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求体 <span id="h3-cr-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        固定为 `voice-enrollment`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          固定为 `create_voice`。

          <strong>target\_model</strong> `string`<strong>（必选）</strong>

          驱动音色的目标模型。必须与后续使用该音色的模型一致，否则调用失败。

          <strong>audio</strong> `string`<strong>（必选）</strong>

          用于复刻音色的参考音频，支持公网可访问的 URL 或 Base64 Data URI，例如 `data:audio/wav;base64,...`。

          <Note>
            兼容旧参数 `url`；使用 `url` 时可不传 `audio`。`url` 将逐步废弃，建议改用 `audio`。
          </Note>

          <strong>prefix</strong> `string`<strong>（必选）</strong>

          音色名称前缀，仅允许数字和英文字母，不超过10个字符。生成的音色名格式：`{target_model}-{prefix}-{唯一标识}`。

          <strong>language\_hints</strong> `array[string]`（可选）

          辅助模型识别样本音频的语种，从而更准确地提取音色特征，提升复刻效果。若设置的语种与实际音频语种不符（例如为中文音频设置 `en`），系统将忽略该设置并自动检测语种。

          此参数为数组，长度必须为 1。

          <Note>
            适用于 qwen-audio-3.1-tts-plus、qwen-audio-3.1-tts-flash、qwen-audio-3.0-tts-plus、qwen-audio-3.0-tts-flash、qwen-audio-3.1-realtime-plus、qwen-audio-3.0-realtime-plus、qwen-audio-3.0-realtime-flash、cosyvoice-v3.5-plus、cosyvoice-v3.5-flash、cosyvoice-v3-plus、cosyvoice-v3-flash。
          </Note>

          <Accordion title="点击查看各模型支持的语言">
            - qwen-audio-3.1-tts-plus、qwen-audio-3.1-tts-flash、qwen-audio-3.0-tts-plus、qwen-audio-3.0-tts-flash、qwen-audio-3.1-realtime-plus、qwen-audio-3.0-realtime-plus、qwen-audio-3.0-realtime-flash：

              - zh：中文
              - en：英语
              - fr：法语
              - de：德语
              - ja：日语
              - ko：韩语
              - ru：俄语
              - pt：葡萄牙语
              - th：泰语
              - id：印尼语
              - vi：越南语
              - it：意大利语
              - es：西班牙语
              - ms：马来西亚语
              - fil：菲律宾语
              - ar：阿拉伯语
            - cosyvoice-v3-plus：

              - zh：中文
              - en：英文
              - fr：法语
              - de：德语
              - ja：日语
              - ko：韩语
              - ru：俄语
            - cosyvoice-v3.5-plus、cosyvoice-v3.5-flash、cosyvoice-v3-flash：

              - zh：中文
              - en：英文
              - fr：法语
              - de：德语
              - ja：日语
              - ko：韩语
              - ru：俄语
              - pt：葡萄牙语
              - th：泰语
              - id：印尼语
              - vi：越南语
          </Accordion>

          默认值：\["zh"]。

          <strong>max\_prompt\_audio\_length</strong> `float`（可选）

          音频预处理后用于声音复刻的参考音频最大时长（秒）。取值范围：\[3.0, 30.0]。

          默认值：10.0。

          <Note>
            适用于 qwen-audio-3.1-tts-plus、qwen-audio-3.1-tts-flash、qwen-audio-3.0-tts-plus、qwen-audio-3.0-tts-flash、qwen-audio-3.1-realtime-plus、qwen-audio-3.0-realtime-plus、qwen-audio-3.0-realtime-flash、cosyvoice-v3.5-plus、cosyvoice-v3.5-flash、cosyvoice-v3-flash。
          </Note>

          <strong>enable\_preprocess</strong> `boolean`（可选）

          是否开启音频预处理（降噪、音频增强、音量规整）。有背景噪音时建议开启；安静环境建议关闭以最大程度还原音色。

          默认值：false。

          <Note>
            适用于 qwen-audio-3.1-tts-plus、qwen-audio-3.1-tts-flash、qwen-audio-3.0-tts-plus、qwen-audio-3.0-tts-flash、qwen-audio-3.1-realtime-plus、qwen-audio-3.0-realtime-plus、qwen-audio-3.0-realtime-flash、cosyvoice-v3.5-plus、cosyvoice-v3.5-flash、cosyvoice-v3-flash。
          </Note>

          <strong>enable\_volume\_normalization</strong> `string`（可选）

          是否对用于声音复刻的样本音频进行音量归一化。取值：

          - `"true"`：开启音量归一化。
          - `"false"`：关闭音量归一化。

          开启后，使用所创建音色合成的音频，其音量可能与关闭该参数时创建的音色不同。

          默认值：`"false"`。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        <CodeGroup>
          ```bash 声音复刻
          curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -d '{
              "model": "voice-enrollment",
              "input": {
                  "action": "create_voice",
                  "target_model": "qwen-audio-3.0-tts-flash",
                  "prefix": "myvoice",
                  "audio": "https://your-audio-url.wav",
                  "language_hints": ["zh"],
                  "enable_volume_normalization": "false"
              }
          }'
          ```
        </CodeGroup>

        使用本地 WAV 音频时，可通过以下代码生成 Base64 Data URI，并将 `audio` 的值填入创建或更新请求的 `input.audio` 字段。将 `sample.wav` 替换为本地音频路径。

        ```python
        import base64
        from pathlib import Path

        audio = "data:audio/wav;base64," + base64.b64encode(
            Path("sample.wav").read_bytes()
        ).decode("ascii")
        ```
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
        #### 返回体 <span id="h3-cr-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>voice\_id</strong>`string`

          音色 ID，可用于语音合成接口的 `voice` 参数。
        </Accordion>

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          创建的音色数量，固定为1。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```json 声音复刻
          {
              "output": {
                  "voice_id": "qwen-audio-3.0-tts-flash-myvoice-xxxxxx"
              },
              "usage": {
                  "count": 1
              },
              "request_id": "xxxx-xxxx-xxxx"
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### MiniMax <span id="h2-mm-create" />

<Note>
  音色复刻请求会生成一段试听音频，试听音频按所选模型的同步语音合成单价额外计费。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求体 <span id="h3-mm-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        指定合成试听音频使用的语音模型。

        支持的模型：

        - MiniMax/speech-2.8-hd
        - MiniMax/speech-02-hd
        - MiniMax/speech-2.8-turbo
        - MiniMax/speech-02-turbo

        <strong>input</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>action</strong>`string`<strong>（必选）</strong>

          需要进行的操作，支持设置为：`voice_clone`（声音克隆）

          <strong>audio\_url</strong>`string`<strong>（必选）</strong>

          需要复刻的音频文件 URL，音频需符合以下规范：

          - 音频格式需为：mp3、m4a、wav 格式
          - 音频的时长最少应不低于 10 秒，最长应不超过 5 分钟
          - 音频文件大小需不超过 20 MB

          <strong>clone\_prompt</strong>`object`<strong>（可选）</strong>

          音色复刻示例音频，提供本参数将有助于增强语音合成的音色相似度和稳定性。若使用本参数，需准备一小段示例音频。

          <Accordion title="属性">
            <strong>prompt\_audio</strong>`string`<strong>（可选）</strong>

            示例音频文件 URL，音频需符合以下规范：

            - 音频格式需为：mp3、m4a、wav 格式
            - 音频时长小于 8 秒
            - 音频文件大小需不超过 20 MB

            <strong>prompt\_text</strong>`string`<strong>（可选）</strong>

            示例音频的对应文本，需确保和音频内容一致，句末需有标点符号做结尾。
          </Accordion>

          <strong>text</strong>`string`<strong>（必选）</strong>

          复刻声音期望试听的内容。限制 1000 字符以内。

          <Note>
            试听将根据字符数正常收取语音合成费用，收费标准参见[MiniMax模型价格](/zh/model-studio/model-pricing#8296764fe3l4x)。
          </Note>

          - 语气词标签：仅当模型选择 `speech-2.8-hd` 或 `speech-2.8-turbo` 时，支持在文本中插入语气词标签。支持的语气词：`(laughs)`（笑声）、`(chuckle)`（轻笑）、`(coughs)`（咳嗽）、`(clear-throat)`（清嗓子）、`(groans)`（呻吟）、`(breath)`（正常换气）、`(pant)`（喘气）、`(inhale)`（吸气）、`(exhale)`（呼气）、`(gasps)`（倒吸气）、`(sniffs)`（吸鼻子）、`(sighs)`（叹气）、`(snorts)`（喷鼻息）、`(burps)`（打嗝）、`(lip-smacking)`（咂嘴）、`(humming)`（哼唱）、`(hissing)`（嘶嘶声）、`(emm)`（嗯）、`(whistles)`（口哨）、`(sneezes)`（喷嚏）、`(crying)`（抽泣）、`(applause)`（鼓掌）

          <strong>voice\_id</strong> `string`<strong>（必选）</strong>

          克隆音色的 voice\_id，正确示例："MiniMax001"。用户进行自定义 voice\_id 时需注意：

          - 自定义的 voice\_id 长度范围\[8,256]
          - 首字符必须为英文字母
          - 允许数字、字母、-、\_
          - 末位字符不可为 -、\_
          - voice\_id 不可与已有 id 重复，否则会报错

          <Note>
            该参数全局唯一，建议使用时间戳等个性化信息命名。
          </Note>

          <strong>language\_boost</strong>`enum<string>`<strong>（可选）</strong>默认值：null

          是否增强对指定的小语种和方言的识别能力。可设置为 `auto` 让模型自主判断。

          <Accordion title="可用选项">
            `Chinese`, `Chinese,Yue`, `English`, `Arabic`, `Russian`, `Spanish`, `French`, `Portuguese`, `German`, `Turkish`, `Dutch`, `Ukrainian`, `Vietnamese`, `Indonesian`, `Japanese`, `Italian`, `Korean`, `Thai`, `Polish`, `Romanian`, `Greek`, `Czech`, `Finnish`, `Hindi`, `Bulgarian`, `Danish`, `Hebrew`, `Malay`, `Persian`, `Slovak`, `Swedish`, `Croatian`, `Filipino`, `Hungarian`, `Norwegian`, `Slovenian`, `Catalan`, `Nynorsk`, `Tamil`, `Afrikaans`,`auto`
          </Accordion>

          <strong>need\_noise\_reduction</strong>`boolean` <strong>（可选）</strong>默认值：false

          音频复刻参数，表示是否开启降噪，默认值为 false。

          <strong>need\_volume\_normalization</strong>`boolean` <strong>（可选）</strong>默认值：false

          是否开启音量归一化。

          <strong>aigc\_watermark</strong>`boolean` <strong>（可选）</strong>默认值：false

          是否在合成试听音频的末尾添加音频节奏标识。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Note>
          第三方模型（MiniMax）目前仅支持通过`dashscope.aliyuncs.com`域名调用，暂不支持专属域名
        </Note>

        ```bash
        curl -X POST 'https://dashscope.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H 'Content-Type: application/json; charset=utf-8' \
          -d '{
            "input": {
              "action": "voice_clone",
              "voice_id": "bailian-test-voice-22",
              "audio_url": "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/audio/cosyvoice/cosyvoice-zeroshot-sample.wav",
              "text": "今天天气怎么样？"
            },
            "model": "MiniMax/speech-2.8-turbo"
          }'
        ```
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
        #### 返回体 <span id="h3-mm-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        <Accordion title="属性" defaultOpen>
          <strong>input\_sensitive</strong>`boolean`

          输入音频是否命中风控。

          <strong>input\_sensitive\_type</strong>`integer`

          输入音频命中风控的类型，取值为以下其一：

          - 0：正常
          - 1：严重违规
          - 2：色情
          - 3：广告
          - 4：违禁
          - 5：谩骂
          - 6：暴恐
          - 7：其他

          <strong>demo\_audio</strong>`string`

          链接形式的试听音频。

          <strong>base\_resp</strong>`object`

          <Accordion title="属性">
            <strong>status\_code</strong>`integer`

            状态码

            - 0: 请求结果正常
            - 1000：未知错误
            - 1001：超时
            - 1002：触发限流
            - 1004：鉴权失败
            - 1013：服务内部错误
            - 2013：输入格式信息不正常
            - 2038：无复刻权限，请检查账号认证状态

            <strong>status\_msg</strong>`string`

            状态详情
          </Accordion>
        </Accordion>

        <strong>usage</strong>`object`

        本次请求的字符用量。

        <Accordion title="属性" defaultOpen>
          <strong>characters</strong> `integer`

          输入文本的字符数。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "output": {
                "base_resp": {
                    "status_code": 0,
                    "status_msg": "success"
                },
                "demo_audio": "https://minimax-algeng-chat-tts.oss-cn-wulanchabu.aliyuncs.com/audio%2Feffect%2F05fdf023562aea84632d2a8c01c2366f_1773059375796_1311.mp3?Expires=1773232175&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE",
                "input_sensitive": false,
                "input_sensitive_type": 0
            },
            "usage": {
                "characters": 18
            },
            "request_id": "b1160386-ebf1-913f-9275-ef176c5e1c91"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 查询音色列表 <span id="h2-list" />

适用于 Qwen-Audio-TTS、Qwen-Audio-Realtime 和 CosyVoice。

<Note>
  MiniMax 不支持通过本接口查询音色列表。MiniMax 的可用音色 ID（包括系统音色和复刻音色）请参见[声音管理](/zh/model-studio/sound-management#b447040127fbt)。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求体 <span id="h3-ls-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        固定为 `voice-enrollment`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          固定为 `list_voice`。

          <strong>prefix</strong> `string`（可选）

          按前缀筛选音色。

          <strong>page\_index</strong> `integer`（可选）

          页码索引。

          <strong>page\_size</strong> `integer`（可选）

          每页包含数据条数。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        <CodeGroup>
          ```bash 查询音色列表
          curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -d '{
              "model": "voice-enrollment",
              "input": {
                  "action": "list_voice",
                  "prefix": "myvoice",
                  "page_size": 10,
                  "page_index": 0
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
        #### 返回体 <span id="h3-ls-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>voice\_list</strong>`array[object]`

          查询到的音色列表。

          <Accordion title="属性" defaultOpen>
            <strong>voice\_id</strong>`string`

            音色 ID，可用于语音合成接口的 `voice` 参数。

            <strong>gmt\_create</strong>`string`

            创建时间。

            <strong>gmt\_modified</strong>`string`

            修改时间。

            <strong>status</strong>`string`

            音色状态，取值参见"音色状态说明"。
          </Accordion>
        </Accordion>

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          固定为 1。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```json 查询音色列表
          {
              "output": {
                  "voice_list": [
                      {
                          "voice_id": "qwen-audio-3.0-tts-flash-myvoice-xxxxxx",
                          "gmt_create": "2024-12-11 13:38:02",
                          "gmt_modified": "2024-12-11 13:38:02",
                          "status": "OK"
                      }
                  ]
              },
              "usage": {
                  "count": 1
              },
              "request_id": "xxxx-xxxx-xxxx"
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## 查询音色详情 <span id="h2-query" />

适用于 Qwen-Audio-TTS、Qwen-Audio-Realtime 和 CosyVoice。

<Note>
  MiniMax 不支持通过本接口查询音色详情。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求体 <span id="h3-qr-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        固定为 `voice-enrollment`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          固定为`query_voice`。

          <strong>voice\_id</strong> `string`<strong>（必选）</strong>

          要查询的音色ID。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        <Tabs>
          <Tab title="查询音色详情">
            ```bash
            curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization \
            -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
            -H "Content-Type: application/json" \
            -d '{
                "model": "voice-enrollment",
                "input": {
                    "action": "query_voice",
                    "voice_id": "yourVoiceId"
                }
            }'
            ```
          </Tab>
        </Tabs>
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
        #### 返回体 <span id="h3-qr-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>resource\_link</strong>`string`

          音频文件的URL地址。

          <strong>gmt\_create</strong>`string`

          创建时间。

          <strong>gmt\_modified</strong>`string`

          修改时间。

          <strong>status</strong>`string`

          音色状态，取值参见"音色状态说明"。

          <strong>target\_model</strong>`string`

          驱动音色的语音合成模型。
        </Accordion>

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          固定为1。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "output": {
                "gmt_create": "2024-12-11 13:38:02",
                "resource_link": "https://yourAudioFileUrl",
                "target_model": "qwen-audio-3.0-tts-flash",
                "gmt_modified": "2024-12-11 13:38:02",
                "status": "OK"
            },
            "usage": {
                "count": 1
            },
            "request_id": "xxxx-xxxx-xxxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 更新音色 <span id="h2-update" />

适用于 Qwen-Audio-TTS、Qwen-Audio-Realtime 和 CosyVoice。

<Note>
  MiniMax 不支持通过本接口更新音色。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求体 <span id="h3-up-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        固定为 `voice-enrollment`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          固定为`update_voice`。

          <strong>voice\_id</strong> `string`<strong>（必选）</strong>

          要更新的音色ID。

          <strong>audio</strong> `string`<strong>（必选）</strong>

          新的参考音频，支持公网可访问的 URL 或 Base64 Data URI，例如 `data:audio/wav;base64,...`。

          <Note>
            兼容旧参数 `url`；使用 `url` 时可不传 `audio`。`url` 将逐步废弃，建议改用 `audio`。
          </Note>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        ```bash
        curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization \
        -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
        -H "Content-Type: application/json" \
        -d '{
            "model": "voice-enrollment",
            "input": {
                "action": "update_voice",
                "voice_id": "yourVoiceId",
                "audio": "https://new-audio-url.wav"
            }
        }'
        ```
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
        #### 返回体 <span id="h3-up-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据，更新操作返回空对象。

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          固定为1。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
            "output": {},
            "usage": {
                "count": 1
            },
            "request_id": "xxxx-xxxx-xxxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 删除音色 <span id="h2-delete" />

适用于 Qwen-Audio-TTS、Qwen-Audio-Realtime 和 CosyVoice。

<Note>
  MiniMax 不支持通过本接口删除音色。MiniMax 音色的删除请参见[声音管理](/zh/model-studio/sound-management#62828c090dkbi)。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求体 <span id="h3-dl-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        固定为 `voice-enrollment`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          固定为 `delete_voice`。

          <strong>voice\_id</strong> `string`<strong>（必选）</strong>

          要删除的音色ID。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        <CodeGroup>
          ```bash 删除音色
          curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -d '{
              "model": "voice-enrollment",
              "input": {
                  "action": "delete_voice",
                  "voice_id": "yourVoiceId"
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
        #### 返回体 <span id="h3-dl-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据，为空对象。

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          固定为1。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```json 删除音色
          {
              "output": {},
              "usage": {
                  "count": 1
              },
              "request_id": "xxxx-xxxx-xxxx"
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## 音色状态说明 <span id="h2-status" />

音色创建后会经过审核流程。以下状态适用于 Qwen-Audio-TTS、Qwen-Audio-Realtime 和 CosyVoice。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "25%" }} />

    <col style={{ width: "75%" }} />
  </colgroup>

  <thead>
    <tr>
      <th style={{ verticalAlign: "top" }}><strong>状态</strong></th>
      <th style={{ verticalAlign: "top" }}><strong>说明</strong></th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        DEPLOYING
      </td>

      <td style={{ verticalAlign: "top" }}>
        审核中/处理中。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        OK
      </td>

      <td style={{ verticalAlign: "top" }}>
        审核通过，可正常使用。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        UNDEPLOYED
      </td>

      <td style={{ verticalAlign: "top" }}>
        审核未通过，不可使用。
      </td>
    </tr>
  </tbody>
</table>
