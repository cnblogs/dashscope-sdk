> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-Audio-3.x-ASR-Flash-Filetrans/Fun-ASR非实时语音识别HTTP API参考

> 本文介绍Qwen-Audio-3.x-ASR-Flash-Filetrans/Fun-ASR非实时语音识别HTTP API的参数和接口细节。

<meta property="og:title" content="Qwen-Audio-3.x-ASR-Flash-Filetrans/Fun-ASR非实时语音识别HTTP API参考" />

<strong>用户指南：</strong>[非实时语音识别](/zh/model-studio/non-realtime-speech-recognition-user-guide)。关于支持的音频格式、文件大小限制、时长限制等输入要求，请参见[音频规格](/zh/model-studio/asr-model#asr_audio_spec02)。

## 流程说明 <span id="4a1eca9360nd1" />

与DashScope同步调用（一次请求、立即返回结果）不同，异步调用专为处理长音频文件或耗时较长的任务设计，该模式采用“提交-轮询”的两步式流程，避免了因长时间等待而导致的请求超时：

1. 第一步：提交任务

   - 客户端发起一个异步处理请求。
   - 服务器验证请求后，不会立即执行任务，而是返回一个唯一的 `task_id`，表示任务已成功创建。
2. 第二步：获取结果

   - 客户端使用获取到的 `task_id`，通过轮询方式反复调用结果查询接口。
   - 当任务处理完成后，结果查询接口将返回最终的识别结果。

## 接口地址 <span id="h2-ep" />

<Tabs>
  <Tab title="华北2（北京）">
    提交任务接口：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/transcription`

    查询任务接口：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    提交任务接口：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/audio/asr/transcription`

    查询任务接口：`GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

<Tip>
  调用 `qwen-audio-3.1-asr-flash-filetrans` 或 `qwen-audio-3.0-asr-flash-filetrans` 时，必须传入 `parameters`；无需设置参数时传入空对象 `{}`。省略该字段会导致已提交的任务识别失败。

  使用新版域名（`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`）提交任务时，请求参数中必须包含`parameters`对象。即使无需设置任何参数，也必须传入空对象`{}`，否则任务可正常提交，但识别将失败。
</Tip>

## 请求头 <span id="h2-hd" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "24.97%" }} /><col style={{ width: "8.55%" }} /><col style={{ width: "10.35%" }} /><col style={{ width: "56.13%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>是否必选</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Authorization</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时，将"<code>\<your\_api\_key></code>"替换为实际的API Key。提交任务接口和查询任务接口均需要传入。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Content-Type</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>请求参数的媒体类型。仅提交任务接口需要传入，固定为<code>application/json</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>X-DashScope-Async</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>异步任务标识。仅提交任务接口需要传入，固定为<code>enable</code>，请勿遗漏，否则无法提交任务。</p></td></tr></tbody></table>

## 提交任务接口 <span id="418f2ac8ecxm4" />

提交语音识别任务。该接口异步返回，业务侧需结合[查询任务接口](/zh/model-studio/fun-asr-recorded-speech-recognition-http-api#480630e0582sb)轮询任务状态。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ### 请求参数 <span id="493b0f8f2ap0y" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        指定模型名。支持Qwen-Audio-3.x-ASR-Flash-Filetrans和Fun-ASR系列模型，详情请参见[支持的模型与地域](/zh/model-studio/non-realtime-speech-recognition-user-guide#4a43cc1bb7kxg)。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>file\_urls</strong> `array[string]`<strong>（必选）</strong>

          音视频文件转写的URL列表，支持HTTP / HTTPS协议，单次请求仅支持1个URL。关于支持的音频格式、文件大小限制、时长限制等输入要求，请参见[音频规格](/zh/model-studio/asr-model#asr_audio_spec02)。

          若录音文件存储在阿里云OSS，使用RESTful API方式支持使用以`oss://`为前缀的临时 URL，使用SDK方式不支持使用以 oss\://为前缀的临时 URL。

          <Tip>
            - 临时 URL 有效期48小时，过期后无法使用，<strong>请勿用于生产环境。</strong>
            - 文件上传凭证接口限流为 100 QPS 且不支持扩容，<strong>请勿用于生产环境、高并发及压测场景。</strong>
            - 生产环境建议使用[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss) 等稳定存储，确保文件长期可用并规避限流问题。
            - 录音文件URL设置成OSS临时公网访问不通该如何处理？请求头中将`X-DashScope-OssResourceResolve`设为`enable`（不推荐该方式）。

              SDK不支持对请求头进行配置。
          </Tip>

          <strong>context</strong>`array(object)`（可选）

          消息列表。包含可选的对话上下文（用于提升识别效果）。

          <Tip>
            SDK暂不支持该功能。
          </Tip>

          <Tip>
            上下文功能用于提升专有词汇的识别准确率，使用方法详见[上下文增强](/zh/model-studio/improve-asr-accuracy#ctx_enhance_h2)。

            <strong>约束</strong>：上下文消息（`input_text` 和 `text` 类型）各最多 5 条，超出时保留最近的 5 条。每轮上下文文本总长度（`user` 和 `assistant` 的 `text` 字段长度之和）不超过 400 个字符（按字符数计算，每个字符计为 1），超出部分从末尾截断。
          </Tip>

          <Tip>
            携带上下文时，`messages` 中的消息顺序有要求：上下文消息必须按对话轮次排列，每轮中 `user`（`input_text` 类型）必须在对应的 `assistant`（`text` 类型）之前；包含 `input_audio` 的 `user` 消息必须放在 `messages` 数组的最后。
          </Tip>

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string`<strong>（必选）</strong>

            消息角色。取值范围：

            - `user`：前几轮的识别结果或领域相关的词表。
            - `assistant`：前几轮大语言模型的回复内容。

            <strong>content</strong>`array(object)`<strong>（必选）</strong>

            消息内容列表。

            <Accordion title="属性" defaultOpen>
              <strong>type</strong>`string`<strong>（必选）</strong>

              内容类型。取值范围：

              - `input_text`（可选，上下文）：前几轮用户语音的识别结果或领域相关的词表（role为user），需同时传入`text`字段。
              - `text`（可选，上下文）：前几轮大语言模型的回复内容（role为assistant），需同时传入`text`字段。

              <strong>text</strong>`string`<strong>（条件必选）</strong>

              当`type`为`input_text`时，填入前几轮用户语音的识别结果或领域相关的词表；当`type`为`text`时，填入前几轮大语言模型的回复内容。文本按字符数计算，每个字符计为 1。每轮上下文中所有消息的 `text` 字段长度之和不超过 400 个字符，超出部分从末尾截断。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong>`object`（可选）

        请求参数对象。

        <Tip>
          使用新版域名（`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`）时，`parameters`为必填项。即使无需设置任何参数，也必须传入空对象`{}`。若省略该字段，任务可正常提交，但查询任务结果时将返回识别失败。
        </Tip>

        <Accordion title="属性" defaultOpen>
          <strong>keep\_dialect</strong> `boolean`（可选）

          仅 `qwen-audio-3.1-asr-flash-filetrans` 支持。是否保留方言表达，默认值为 `false`。`false`：将方言转写为普通话文本；`true`：保留方言表达。

          <strong>vocabulary\_id</strong> `string`（可选）

          预编译热词列表 ID。

          需预先调用创建热词列表接口生成，识别时传入该 ID 即可使用列表中的热词。

          适用于词汇已知且相对稳定、需要跨请求复用同一词表的场景。

          使用方法请参见[预编译热词](/zh/model-studio/improve-asr-accuracy#hw_precompiled_h3)。

          <strong>vocabulary</strong> `object`（可选）

          即时热词。

          以键值对形式传入，键为热词文本（`string`），值为热词权重（`integer`），无需预先创建热词列表。权重取值范围为 \[1, 5] 或 50：取 \[1, 5] 时值越大模型越倾向输出该词；取 50 时为超级热词，召回率大幅提升，但超级热词数量最多不超过 50 个。

          适用于临时性、会话级别的热词优化。

          与预编译热词同时配置时，系统会合并两类热词；合并后超过 2000 个时，随机选择 2000 个使用。使用方法请参见[即时热词](/zh/model-studio/improve-asr-accuracy#hw_instant_h3)。

          <Tip>
            仅`qwen-audio-3.1-asr-flash-filetrans`、`qwen-audio-3.0-asr-flash-filetrans`支持即时热词。
          </Tip>

          <strong>channel\_id</strong> `array[integer]`（可选）

          指定在多音轨音频文件中需要识别的音轨索引，索引从 0 开始。例如，\[0] 表示识别第一个音轨，\[0, 1] 表示同时识别第一和第二个音轨。如果省略此参数，则默认处理第一个音轨。

          <Tip>
            指定的每一个音轨都将独立计费。例如，为单个文件请求 \[0, 1] 会产生两笔独立的费用。
          </Tip>

          默认值：\[0]。

          <strong>special\_word\_filter</strong> `string`（可选）

          指定在语音识别过程中需要处理的敏感词，并支持对不同敏感词设置不同的处理方式。详情请参见[敏感词过滤](/zh/model-studio/non-realtime-speech-recognition-user-guide#nrt03_sensitive_h3)。

          <strong>diarization\_enabled</strong> `boolean`（可选）

          是否启用说话人分离，默认关闭。

          仅适用于单声道音频，多声道音频不支持说话人分离。

          启用该功能后，识别结果中将显示`speaker_id`字段，用于区分不同说话人。

          <Note>
            如果启用说话人分离功能，建议音频时长不超过2小时，否则可能导致识别失败或超时。
          </Note>

          有关`speaker_id`的示例，请参见[识别结果说明](/zh/model-studio/fun-asr-recorded-speech-recognition-http-api#5882b67243tcg)。

          默认值：false。

          <strong>speaker\_count</strong> `integer`（可选）

          <Tip>
            仅在开启说话人分离功能（`diarization_enabled`设置为`true`）时生效。
          </Tip>

          说话人数量参考值。取值范围为2至100的整数（包含2和100）。

          默认自动判断说话人数量，如果配置此项，只能辅助算法尽量输出指定人数，无法保证一定会输出此人数。

          无默认值。

          <strong>language\_hints</strong> `array[string]`（可选）

          设置待识别语言代码。如果无法提前确定语种，可不设置，模型会自动识别语种。

          对于 Qwen-Audio-3.x-ASR-Flash-Filetrans 系列模型，最多支持设置 4 个值，即便设置超出 4 个，也仅前 4 个生效；对于 Fun-ASR 系列模型，仅支持设置 1 个值，即便设置多个，也仅第一个生效。

          <Accordion title="点击查看支持的语言代码">
            - qwen-audio-3.1-asr-flash-filetrans、qwen-audio-3.0-asr-flash-filetrans、fun-asr：

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
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Tabs>
          <Tab title="普通调用">
            以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/transcription' \
                 --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                 --header "Content-Type: application/json" \
                 --header "X-DashScope-Async: enable" \
                 --data '{
                "model": "qwen-audio-3.1-asr-flash-filetrans",
                "input": {
                    "file_urls": [
                        "{YOUR_AUDIO_URL}"
                    ]
                },
                "parameters": {
                    "channel_id": [0]
                }
            }'
            ```
          </Tab>

          <Tab title="即时热词">
            使用即时热词时格式如下：

            ```json
            {
                "model": "qwen-audio-3.1-asr-flash-filetrans",
                "input": {
                    "file_urls": [
                        "{YOUR_AUDIO_URL}"
                    ]
                },
                "parameters": {
                    "vocabulary": {"张三": 5, "李四": 5}
                }
            }
            ```
          </Tab>

          <Tab title="上下文">
            使用上下文时格式如下：

            ```json expandable
            {
                "model": "qwen-audio-3.1-asr-flash-filetrans",
                "input": {
                    "file_urls": [
                        "{YOUR_AUDIO_URL}"
                    ],
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
                },
                "parameters": {
                    "vocabulary": {"张三": 5, "李四": 5}
                }
            }
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
        ### 响应参数 <span id="h3-st-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        提交任务返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          任务ID。该ID在[查询任务接口](/zh/model-studio/fun-asr-recorded-speech-recognition-http-api#480630e0582sb)中作为[string](/zh/model-studio/fun-asr-recorded-speech-recognition-http-api)传入。

          <strong>task\_status</strong>`string`

          任务状态。提交成功时返回`PENDING`。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {
          "output": {
            "task_status": "PENDING",
            "task_id": "c2e5d63b-96e1-4607-bb91-************"
          },
          "request_id": "77ae55ae-be17-97b8-9942--************"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 查询任务接口 <span id="480630e0582sb" />

查询语音识别任务的执行情况和结果。建议轮询调用直至任务终态。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ### 请求参数 <span id="h3-qt-rq" />

        <strong>task\_id</strong>`string`<strong>（必选）</strong>

        <Tip>
          该参数为URL路径参数，无请求参数。
        </Tip>

        查询任务需指定其ID，该ID为[提交任务接口](/zh/model-studio/fun-asr-recorded-speech-recognition-http-api#418f2ac8ecxm4)被调用后返回的`task_id`。
      </td>

      <td style={{ verticalAlign: "top" }}>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        ```bash
        curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}' \
             --header "Authorization: Bearer $DASHSCOPE_API_KEY"
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
        ### 响应参数 <span id="h3-qt-rs" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        查询任务返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          被查询任务的ID。

          <strong>task\_status</strong>`string`

          被查询任务的状态。

          <Note>
            当任务包含多个子任务时，只要存在任一子任务成功，整个任务状态将标记为`SUCCEEDED`，需通过`subtask_status`字段判断具体子任务结果。
          </Note>

          <strong>submit\_time</strong>`string`

          任务提交时间。

          <strong>scheduled\_time</strong>`string`

          任务被调度执行的时间。

          <strong>end\_time</strong>`string`

          任务结束时间。

          <strong>results</strong>`array[object]`

          每个待识别音频文件对应的子任务结果列表。

          <Accordion title="属性">
            <strong>subtask\_status</strong>`string`

            子任务状态。

            <strong>file\_url</strong>`string`

            文件转写任务中所处理的文件URL。

            <strong>transcription\_url</strong>`string`

            获取识别结果对应的链接。该链接有效期为24小时，超时后无法查询任务或通过先前查询结果中的URL下载结果。

            识别结果保存为JSON文件，您可以通过上述链接下载该文件或直接通过HTTP请求读取该文件中的内容。JSON数据中各字段含义请参见[识别结果说明](/zh/model-studio/fun-asr-recorded-speech-recognition-http-api#5882b67243tcg)。

            <strong>code</strong>`string`

            <Tip>
              仅当子任务失败时返回。
            </Tip>

            子任务失败的错误码。

            <strong>message</strong>`string`

            <Tip>
              仅当子任务失败时返回。
            </Tip>

            子任务失败的错误信息。
          </Accordion>

          <strong>task\_metrics</strong>`object`

          任务整体执行情况统计。

          <Accordion title="属性">
            <strong>TOTAL</strong>`integer`

            子任务总数。

            <strong>SUCCEEDED</strong>`integer`

            成功的子任务数。

            <strong>FAILED</strong>`integer`

            失败的子任务数。
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```json 正常示例
          {
            "request_id": "f9e1afad-94d3-997e-a83b-************",
            "output": {
              "task_id": "f86ec806-4d73-485f-a24f-************",
              "task_status": "SUCCEEDED",
              "submit_time": "2024-09-12 15:11:40.041",
              "scheduled_time": "2024-09-12 15:11:40.071",
              "end_time": "2024-09-12 15:11:40.903",
              "results": [
                {
                  "file_url": "{YOUR_AUDIO_URL}",
                  "transcription_url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/pre/filetrans-16k/20240912/15%3A11/409a4b92-445b-4dd8-8c1d-f110954d82d8-1.json?Expires=1726211500&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE",
                  "subtask_status": "SUCCEEDED"
                }
              ],
              "task_metrics": {
                "TOTAL": 1,
                "SUCCEEDED": 1,
                "FAILED": 0
              }
            },
            "usage": {
              "duration": 9
            }
          }
          ```

          ```json 异常示例
          {
              "task_id": "7bac899c-06ec-4a79-8875-xxxxxxxxxxxx",
              "task_status": "SUCCEEDED",
              "submit_time": "2024-12-16 16:30:59.170",
              "scheduled_time": "2024-12-16 16:30:59.204",
              "end_time": "2024-12-16 16:31:02.375",
              "results": [
                  {
                      "file_url": "{YOUR_AUDIO_URL}",
                      "code": "FILE_DOWNLOAD_FAILED",
                      "message": "FILE_DOWNLOAD_FAILED",
                      "subtask_status": "FAILED"
                  }
              ],
              "task_metrics": {
                  "TOTAL": 1,
                  "SUCCEEDED": 0,
                  "FAILED": 1
              }
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## 其他接口：批量查询任务状态/取消任务 <span id="012da710f6a6e" />

详情请参见[管理异步任务](/zh/model-studio/manage-asynchronous-tasks)：支持批量查询24小时内提交的非实时语音识别任务，同时支持取消`PENDING`（排队）状态的任务。

## 识别结果说明 <span id="5882b67243tcg" />

识别结果保存为JSON文件。

<Accordion title="点击查看识别结果示例">
  ```json expandable
  {
      "file_url":"{YOUR_AUDIO_URL}",
      "properties":{
          "audio_format":"pcm_s16le",
          "channels":[
              0
          ],
          "original_sampling_rate":16000,
          "original_duration_in_milliseconds":3834
      },
      "transcripts":[
          {
              "channel_id":0,
              "content_duration_in_milliseconds":3720,
              "text":"Hello world, 这里是阿里巴巴语音实验室。",
              "sentences":[
                  {
                      "begin_time":100,
                      "end_time":3820,
                      "text":"Hello world, 这里是阿里巴巴语音实验室。",
                      "sentence_id":1,
                      "speaker_id":0, //当开启自动说话人分离功能时才会显示该字段
                      "words":[
                          {
                              "begin_time":100,
                              "end_time":596,
                              "text":"Hello ",
                              "punctuation":""
                          },
                          {
                              "begin_time":596,
                              "end_time":844,
                              "text":"world",
                              "punctuation":", "
                          }
                          // 这里省略其它内容
                      ]
                  }
              ]
          }
      ]
  }
  ```
</Accordion>

需要关注的参数如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "12.81%" }} /><col style={{ width: "11.11%" }} /><col style={{ width: "76.08%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>audio\_format</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>源文件中音频的格式。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>channels</p></td><td style={{ verticalAlign: "top" }}><p>array\[integer]</p></td><td style={{ verticalAlign: "top" }}><p>源文件中音频的音轨索引信息，对单轨音频返回\[0]，对双轨音频返回\[0, 1]，以此类推。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>original\_sampling\_rate</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>源文件中音频的采样率（Hz）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>original\_duration\_in\_milliseconds</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>源文件中的原始音频时长（ms）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>channel\_id</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>转写结果的音轨索引，以0为起始。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>content\_duration</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>音轨中被判定为语音内容的时长（ms）。</p> <div><p>语音识别模型服务仅对音轨中被判定为语音内容的时长进行语音转写，并据此进行计量计费，非语音内容不计量、不计费。通常情况下语音内容时长会短于原始音频时长。由于对是否存在语音内容的判定是由AI模型给出的，可能与实际情况存在一定误差。</p></div></td></tr><tr><td style={{ verticalAlign: "top" }}><p>transcript</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>段落级别的语音转写结果。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>sentences</p></td><td style={{ verticalAlign: "top" }}><p>array</p></td><td style={{ verticalAlign: "top" }}><p>句子级别的语音转写结果。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>words</p></td><td style={{ verticalAlign: "top" }}><p>array</p></td><td style={{ verticalAlign: "top" }}><p>词级别的语音转写结果。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>begin\_time</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>开始时间戳（ms）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>end\_time</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>结束时间戳（ms）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>text</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>语音转写结果。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>speaker\_id</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>当前说话人的索引，以0为起始，用于区分不同的说话人。</p> <p>仅在启用说话人分离功能时，该字段才会显示于识别结果中。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>punctuation</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>预测出的词之后的标点符号（如有）。</p></td></tr></tbody></table>
