> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 声音设计API参考

> 本文介绍声音设计的HTTP API接口详情，包括创建音色、查询音色列表、查询音色详情和删除音色四个操作。

<meta property="og:title" content="声音设计API参考" />

<strong>用户指南：</strong>[声音设计](/zh/model-studio/voice-design-user-guide)。

## 接口地址 <span id="vd-h2-url" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

## 请求头 <span id="vd-h2-headers" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.99%" }} /><col style={{ width: "8.54%" }} /><col style={{ width: "10.32%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Authorization</p></td><td><p>string</p></td><td><p>是</p></td><td><p>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时，将"<code>\<your\_api\_key></code>"替换为实际的API Key。</p></td></tr><tr><td><p>Content-Type</p></td><td><p>string</p></td><td><p>是</p></td><td><p>请求体的媒体类型。固定为<code>application/json</code>。</p></td></tr></tbody></table>

## 创建音色 <span id="vd-h2-create" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="vd-h3-cr-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        声音设计模型。取值：

        - `voice-enrollment`：Qwen-Audio-TTS/CosyVoice声音设计。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          操作类型。

          - Qwen-Audio-TTS/CosyVoice（`voice-enrollment`）：固定为`create_voice`。

          <strong>target\_model</strong> `string`<strong>（必选）</strong>

          驱动音色的语音合成模型。必须与后续调用语音合成接口时使用的模型一致，否则合成会失败。

          <strong>voice\_prompt</strong> `string`<strong>（必选）</strong>

          声音描述文本，仅支持中文和英文。

          - Qwen-Audio-TTS/CosyVoice（`voice-enrollment`）：最大长度500字符。

          <strong>preview\_text</strong> `string`<strong>（必选）</strong>

          预览音频对应的文本。

          - Qwen-Audio-TTS/CosyVoice（`voice-enrollment`）：最小长度15字符，最大长度200字符，支持中文和英文。

          <strong>prefix</strong> `string`<strong>（必选）</strong>

          <Tip>
            仅适用于Qwen-Audio-TTS/CosyVoice（model为`voice-enrollment`时）。
          </Tip>

          音色名称前缀，仅允许数字和英文字母，不超过10个字符。生成的音色名格式：`{target_model}-vd-{prefix}-{唯一标识}`

          <strong>language\_hints</strong> `array[string]`（可选）

          <Tip>
            仅适用于Qwen-Audio-TTS/CosyVoice（model为`voice-enrollment`时）。
          </Tip>

          指定生成音色的语言倾向，影响音色的语言特征和发音倾向，建议根据实际使用场景选择对应语言代码。若使用该参数，设置的语种须与 `preview_text` 的语种一致。

          此参数为数组，但当前版本仅处理第一个元素。

          取值范围：

          - zh：中文
          - en：英文

          默认值：\["zh"]。
        </Accordion>

        <strong>parameters</strong>`object`（可选）

        声音设计的参数配置。

        <Accordion title="属性" defaultOpen>
          <strong>sample\_rate</strong> `int`（可选）

          预览音频采样率（Hz）。

          - Qwen-Audio-TTS/CosyVoice支持：16000、24000、48000。

          默认值：24000。

          <strong>response\_format</strong> `string`（可选）

          预览音频格式。

          - Qwen-Audio-TTS/CosyVoice支持：pcm、wav、mp3。

          默认值：wav。
        </Accordion>
      </td>

      <td>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        <CodeGroup>
          ```bash Qwen-Audio-TTS/CosyVoice声音设计
          curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -d '{
              "model": "voice-enrollment",
              "input": {
                  "action": "create_voice",
                  "target_model": "cosyvoice-v3.5-plus",
                  "voice_prompt": "沉稳的中年男性，音色低沉浑厚",
                  "preview_text": "各位听众朋友们大家好，欢迎收听本期节目",
                  "prefix": "announcer",
                  "language_hints": ["zh"]
              },
              "parameters": {
                  "sample_rate": 24000,
                  "response_format": "wav"
              }
          }'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 返回体 <span id="vd-h3-cr-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>voice\_id</strong>`string`

          音色 ID，可用于语音合成接口的 `voice` 参数。

          <strong>preview\_audio</strong>`object`

          预览音频数据。

          <Accordion title="属性" defaultOpen>
            <strong>data</strong> `string`

            预览音频数据，Base64编码。

            <strong>sample\_rate</strong> `int`

            预览音频采样率（Hz）。

            <strong>response\_format</strong> `string`

            预览音频格式。
          </Accordion>

          <strong>target\_model</strong>`string`

          驱动音色的语音合成模型。
        </Accordion>

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          创建的音色数量，固定为1。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json Qwen-Audio-TTS/CosyVoice声音设计
          {
              "output": {
                  "preview_audio": {
                      "data": "{base64_encoded_audio}",
                      "sample_rate": 24000,
                      "response_format": "wav"
                  },
                  "target_model": "cosyvoice-v3.5-plus",
                  "voice_id": "cosyvoice-v3.5-plus-vd-announcer-xxxxxx"
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

## 查询音色列表 <span id="vd-h2-list" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="vd-h3-ls-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        声音设计模型。取值：

        - `voice-enrollment`：Qwen-Audio-TTS/CosyVoice声音设计。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          固定为 `list_voice`。

          <strong>prefix</strong> `string`（可选）

          <Tip>
            仅适用于Qwen-Audio-TTS/CosyVoice。
          </Tip>

          按前缀筛选音色。

          <strong>page\_index</strong> `integer`（可选）

          页码索引。

          <strong>page\_size</strong> `integer`（可选）

          每页包含数据条数。
        </Accordion>
      </td>

      <td>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        <CodeGroup>
          ```bash Qwen-Audio-TTS/CosyVoice
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

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 返回体 <span id="vd-h3-ls-rs" />

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

            <Tip>
              仅Qwen-Audio-TTS/CosyVoice返回。
            </Tip>

            音色状态，取值参见"音色状态说明"。

            <strong>voice\_prompt</strong>`string`

            声音描述文本。

            <strong>preview\_text</strong>`string`

            预览音频文本。
          </Accordion>
        </Accordion>

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          固定为 1。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json Qwen-Audio-TTS/CosyVoice
          {
              "output": {
                  "voice_list": [
                      {
                          "voice_id": "cosyvoice-v3.5-plus-vd-announcer-xxxxxx",
                          "gmt_create": "2025-12-10 14:54:09",
                          "gmt_modified": "2025-12-10 17:47:48",
                          "status": "OK",
                          "voice_prompt": "沉稳的中年男性播音员",
                          "preview_text": "各位听众朋友们，大家好"
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

## 查询音色详情 <span id="vd-h2-query" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="vd-h3-qr-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        声音设计模型。取值：

        - `voice-enrollment`：Qwen-Audio-TTS/CosyVoice声音设计。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          固定为 `query_voice`。

          <strong>voice\_id</strong> `string`<strong>（必选）</strong>

          <Tip>
            仅适用于Qwen-Audio-TTS/CosyVoice。
          </Tip>

          要查询的音色ID。
        </Accordion>
      </td>

      <td>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        <CodeGroup>
          ```bash Qwen-Audio-TTS/CosyVoice
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
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 返回体 <span id="vd-h3-qr-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>voice\_id</strong>`string`

          音色 ID，可用于语音合成接口的 `voice` 参数。

          <strong>gmt\_create</strong>`string`

          创建时间。

          <strong>gmt\_modified</strong>`string`

          修改时间。

          <strong>status</strong>`string`

          <Tip>
            仅Qwen-Audio-TTS/CosyVoice返回。
          </Tip>

          音色状态，取值参见"音色状态说明"。

          <strong>target\_model</strong>`string`

          驱动音色的语音合成模型。

          <strong>voice\_prompt</strong>`string`

          <Tip>
            仅Qwen-Audio-TTS/CosyVoice声音设计返回。
          </Tip>

          声音描述文本。

          <strong>preview\_text</strong>`string`

          <Tip>
            仅Qwen-Audio-TTS/CosyVoice声音设计返回。
          </Tip>

          预览音频文本。
        </Accordion>

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          固定为1。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json Qwen-Audio-TTS/CosyVoice声音设计
          {
              "output": {
                  "voice_id": "cosyvoice-v3.5-plus-vd-announcer-xxxxxx",
                  "gmt_create": "2025-12-10 14:54:09",
                  "gmt_modified": "2025-12-10 17:47:48",
                  "preview_text": "各位听众朋友们，大家好",
                  "target_model": "cosyvoice-v3.5-plus",
                  "status": "OK",
                  "voice_prompt": "沉稳的中年男性播音员，音色低沉浑厚"
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

## 删除音色 <span id="vd-h2-delete" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="vd-h3-dl-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        声音设计模型。取值：

        - `voice-enrollment`：Qwen-Audio-TTS/CosyVoice声音设计。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          固定为 `delete_voice`。

          <strong>voice\_id</strong> `string`<strong>（必选）</strong>

          <Tip>
            仅适用于Qwen-Audio-TTS/CosyVoice。
          </Tip>

          要删除的音色ID。
        </Accordion>
      </td>

      <td>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        <CodeGroup>
          ```bash Qwen-Audio-TTS/CosyVoice
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

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 返回体 <span id="vd-h3-dl-rs" />

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

      <td>
        <CodeGroup>
          ```json Qwen-Audio-TTS/CosyVoice
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

## 音色状态说明 <span id="vd-h2-status" />

音色创建后会经过审核流程。以下状态适用于 Qwen-Audio-TTS/CosyVoice（`model` 为 `voice-enrollment`）。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "75%" }} /></colgroup><thead><tr><th><p><strong>状态</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>DEPLOYING</p></td><td><p>审核中/处理中。</p></td></tr><tr><td><p>OK</p></td><td><p>审核通过，可正常使用。</p></td></tr><tr><td><p>UNDEPLOYED</p></td><td><p>审核未通过，不可使用。</p></td></tr></tbody></table>
