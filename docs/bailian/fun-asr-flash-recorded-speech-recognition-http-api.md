> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 非实时语音识别（Qwen-Audio-3.x-ASR-Flash/Fun-ASR-Flash） HTTP API

> 本文介绍Qwen-Audio-3.x-ASR-Flash/Fun-ASR-Flash非实时语音识别HTTP API的参数和接口细节。

<meta property="og:title" content="非实时语音识别（Qwen-Audio-3.x-ASR-Flash/Fun-ASR-Flash） HTTP API" />

<strong>用户指南：</strong>[非实时语音识别](/zh/model-studio/non-realtime-speech-recognition-user-guide)。关于支持的音频格式、文件大小限制、时长限制等输入要求，请参见[音频规格](/zh/model-studio/asr-model#asr_audio_spec02)。

## 接口地址 <span id="h-fl-url" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

## 请求头 <span id="h-fl-hd" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "24.97%" }} /><col style={{ width: "8.55%" }} /><col style={{ width: "10.35%" }} /><col style={{ width: "56.13%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>是否必选</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Authorization</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时将"<code>\<your\_api\_key></code>"替换为实际的API Key。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Content-Type</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>请求参数的媒体类型，固定为<code>application/json</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>X-DashScope-SSE</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>用于控制是否以SSE流式方式返回结果。设置为<code>enable</code>时开启SSE流式返回模式。仅当音频时长不少于1分钟时，服务端才会分多次返回中间识别结果和最终结果；设置为<code>disable</code>或不传该参数则仅返回最终结果。</p></td></tr></tbody></table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## 请求参数 <span id="h-fl-rq" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        指定模型名。支持Qwen-Audio-3.x-ASR-Flash和Fun-ASR-Flash系列模型，详情请参见[支持的模型与地域](/zh/model-studio/non-realtime-speech-recognition-user-guide#4a43cc1bb7kxg)。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入信息。

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong>`array(object)`<strong>（必选）</strong>

          消息列表。包含当前待识别的音频，以及可选的对话上下文（用于提升识别效果）。

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

            - `user`（必选）：用户消息。type为`input_audio`时表示当前待识别的音频；type为`input_text`时表示前几轮的识别结果或领域相关的词表（可选，上下文）。
            - `assistant`（可选，上下文）：前几轮大语言模型的回复内容。

            <strong>content</strong>`array(object)`<strong>（必选）</strong>

            消息内容列表。

            <Accordion title="属性" defaultOpen>
              <strong>type</strong>`string`<strong>（必选）</strong>

              内容类型。每个请求至少需要一条`input_audio`类型的消息。取值范围：

              - `input_audio`（必选）：当前待识别的音频输入（role为user），需同时传入`input_audio`对象。
              - `input_text`（可选，上下文）：前几轮用户语音的识别结果或领域相关的词表（role为user），需同时传入`text`字段。
              - `text`（可选，上下文）：前几轮大语言模型的回复内容（role为assistant），需同时传入`text`字段。

              <strong>input\_audio</strong>`object`<strong>（条件必选）</strong>

              当`type`为`input_audio`时必填。

              <Accordion title="属性" defaultOpen>
                <strong>data</strong>`string`<strong>（必选）</strong>

                待识别音频数据。关于支持的音频格式、文件大小限制、时长限制等输入要求，请参见[音频规格](/zh/model-studio/asr-model#asr_audio_spec02)。支持以下两种方式：

                - <strong>音频文件URL</strong>：直接传入可公开访问的音频文件地址。
                - <strong>Base64 Data URI</strong>：采用Data URI格式传入Base64编码的音频数据，值由`data:{MIME_TYPE};base64,`前缀与Base64编码的音频数据拼接而成。支持的MIME类型包括`audio/wav`、`audio/mp3`等。

                示例（URL方式）：`https://example.com/audio/sample.wav`

                示例（Base64方式）：`data:audio/wav;base64,{BASE64_ENCODED_DATA}`
              </Accordion>

              <strong>text</strong>`string`<strong>（条件必选）</strong>

              当`type`为`input_text`时，填入前几轮用户语音的识别结果或领域相关的词表；当`type`为`text`时，填入前几轮大语言模型的回复内容。文本按字符数计算，每个字符计为 1。每轮上下文中所有消息的 `text` 字段长度之和不超过 400 个字符，超出部分从末尾截断。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong>`object`<strong>（必选）</strong>

        模型参数。

        <Note>
          Qwen-Audio-3.0-ASR-Flash 和 Fun-ASR-Flash 的润色顺滑功能默认关闭，<strong>暂未开放。</strong>Qwen-Audio-3.1-ASR-Flash 支持原生文本润色。

          润色顺滑：模型在识别语音的同时，自动清理无意义语气词和口吃重复，处理说话过程中的自我纠正，理顺口语表达，并规范标点与文本格式。输出结果更加简洁、流畅、易读，同时尽可能保留用户的最终意图和关键信息。
        </Note>

        <Accordion title="属性" defaultOpen>
          <strong>speaker\_diarization\_enabled</strong> `boolean`（可选）

          仅 `qwen-audio-3.1-asr-flash` 支持。是否启用说话人分离，默认值为 `false`。设为 `true` 时，通过响应中的 `speaker_id` 区分说话人，并返回 `output.sentences`。开启后，`keep_dialect` 和 `language_hints` 不生效。

          <strong>keep\_dialect</strong> `boolean`（可选）

          仅 `qwen-audio-3.1-asr-flash` 支持。是否保留方言表达，默认值为 `false`。`false`：将方言转写为普通话文本；`true`：保留方言表达。

          <strong>format</strong>`string`<strong>（必选）</strong>

          音频格式。根据实际音频格式填写，支持`wav`、`mp3`、`opus`等。详情请参见[音频规格](/zh/model-studio/asr-model#asr_audio_spec02)。

          <strong>sample\_rate</strong>`string`（可选）

          音频采样率，单位Hz。例如`16000`表示16kHz采样率。详情请参见[音频规格](/zh/model-studio/asr-model#asr_audio_spec02)。

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

          <Tip>
            仅`qwen-audio-3.1-asr-flash`、`qwen-audio-3.0-asr-flash`支持即时热词。
          </Tip>

          <strong>language\_hints</strong> `array[string]`（可选）

          设置待识别语言代码。如果无法提前确定语种，可不设置，模型会自动识别语种。

          对于 Qwen-Audio-3.x-ASR-Flash 系列模型，最多支持设置 4 个值，即便设置超出 4 个，也仅前 4 个生效；对于 Fun-ASR-Flash 系列模型，仅支持设置 1 个值，即便设置多个，也仅第一个生效。

          <Accordion title="点击查看支持的语言代码">
            - qwen-audio-3.1-asr-flash、qwen-audio-3.0-asr-flash、fun-asr-flash-2026-06-15：

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
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        <Tabs>
          <Tab title="非流式">
            ```bash expandable
            curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
                 --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                 --header "Content-Type: application/json" \
                 --header "X-DashScope-SSE: disable" \
                 --data '{
                "model": "qwen-audio-3.1-asr-flash",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "input_audio",
                                    "input_audio": {
                                        "data": "{YOUR_AUDIO_URL}"
                                    }
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "format": "wav",
                    "sample_rate": "16000"
                }
            }'
            ```
          </Tab>

          <Tab title="流式">
            ```bash expandable
            curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
                 --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                 --header "Content-Type: application/json" \
                 --header "X-DashScope-SSE: enable" \
                 --data '{
                "model": "qwen-audio-3.1-asr-flash",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "input_audio",
                                    "input_audio": {
                                        "data": "{YOUR_AUDIO_URL}"
                                    }
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "format": "wav",
                    "sample_rate": "16000"
                }
            }'
            ```
          </Tab>

          <Tab title="携带上下文-非流式">
            ```bash expandable
            curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
                 --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                 --header "Content-Type: application/json" \
                 --header "X-DashScope-SSE: disable" \
                 --data '{
                "model": "qwen-audio-3.1-asr-flash",
                "input": {
                    "messages": [
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
                        },
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "input_audio",
                                    "input_audio": {
                                        "data": "{YOUR_AUDIO_URL}"
                                    }
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "format": "wav",
                    "sample_rate": "16000"
                }
            }'
            ```
          </Tab>

          <Tab title="携带上下文-流式">
            ```bash expandable
            curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
                 --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                 --header "Content-Type: application/json" \
                 --header "X-DashScope-SSE: enable" \
                 --data '{
                "model": "qwen-audio-3.1-asr-flash",
                "input": {
                    "messages": [
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
                        },
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "input_audio",
                                    "input_audio": {
                                        "data": "{YOUR_AUDIO_URL}"
                                    }
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "format": "wav",
                    "sample_rate": "16000"
                }
            }'
            ```
          </Tab>

          <Tab title="Base64">
            可输入Base64编码数据（[Data URL](https://www.rfc-editor.org/rfc/rfc2397)），格式为：`data:<mediatype>;base64,<data>`。

            - `<mediatype>`：MIME类型

              因音频格式而异，例如：

              - WAV：`audio/wav`
              - MP3：`audio/mpeg`
            - `<data>`：音频转成的Base64编码的字符串

              Base64编码会增大体积，请控制原文件大小，确保编码后仍符合输入音频大小限制（10MB）
            - 示例：`data:audio/wav;base64,SUQzBAAAAAAAI1RTU0UAAAAPAAADTGF2ZjU4LjI5LjEwMAAAAAAAAAAAAAAA//PAxABQ/BXRbMPe4IQAhl9`

              <Accordion title="点击查看示例代码">
                <CodeGroup dropdown>
                  ```python
                  import base64, pathlib

                  # 请替换为自己的音频文件路径，确保其符合音频要求
                  file_path = pathlib.Path("{YOUR_AUDIO_FILE}")
                  base64_str = base64.b64encode(file_path.read_bytes()).decode()
                  data_uri = f"data:audio/mpeg;base64,{base64_str}"
                  ```

                  ```java
                  import java.nio.file.*;
                  import java.util.Base64;

                  public class Main {
                      /**
                       * 请替换为自己的音频文件路径，确保其符合音频要求
                       */
                      public static String toDataUrl(String filePath) throws Exception {
                          byte[] bytes = Files.readAllBytes(Paths.get(filePath));
                          String encoded = Base64.getEncoder().encodeToString(bytes);
                          return "data:audio/mpeg;base64," + encoded;
                      }

                      public static void main(String[] args) throws Exception {
                          System.out.println(toDataUrl("{YOUR_AUDIO_FILE}"));
                      }
                  }
                  ```
                </CodeGroup>
              </Accordion>

            ```python expandable
            import base64, pathlib
            import os
            import requests

            # 请替换为自己的音频文件路径，确保其符合音频要求
            file_path = pathlib.Path("{YOUR_AUDIO_FILE}")
            base64_str = base64.b64encode(file_path.read_bytes()).decode()
            data_uri = f"data:audio/wav;base64,{base64_str}"

            url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation"

            headers = {
                "Authorization": f"Bearer {os.environ['DASHSCOPE_API_KEY']}",
                "Content-Type": "application/json",
                "X-DashScope-SSE": "disable",
            }

            payload = {
                "model": "qwen-audio-3.1-asr-flash",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "input_audio",
                                    "input_audio": {
                                        "data": data_uri,
                                    },
                                }
                            ],
                        }
                    ]
                },
                "parameters": {
                    "format": "wav",
                    "sample_rate": "16000",
                },
            }

            response = requests.post(url, headers=headers, json=payload)
            print(response.status_code)
            print(response.json())
            ```
          </Tab>

          <Tab title="即时热词">
            ```bash expandable
            curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
                 --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                 --header "Content-Type: application/json" \
                 --header "X-DashScope-SSE: disable" \
                 --data '{
                "model": "qwen-audio-3.1-asr-flash",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "type": "input_audio",
                                    "input_audio": {
                                        "data": "{YOUR_AUDIO_URL}"
                                    }
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "format": "wav",
                    "sample_rate": "16000",
                    "vocabulary": {"张三": 5, "李四": 5}
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
        ## 响应参数 <span id="h-fl-rs" />

        <strong>request\_id</strong>`string`

        本次请求的唯一标识。

        <strong>output</strong>`object`

        输出结果。

        <Accordion title="属性" defaultOpen>
          <strong>text</strong>`string`

          当前累积的完整识别文本。

          <strong>sentences</strong> `array[object]`

          仅 `qwen-audio-3.1-asr-flash` 开启说话人分离时返回。包含当前及历史句子的识别结果，每个元素的结构与 `sentence` 相同。

          <strong>sentence</strong>`object`

          当前句子的详细信息。

          <Accordion title="属性" defaultOpen>
            <strong>speaker\_id</strong> `integer | null`

            说话人编号，用于区分不同说话人。`qwen-audio-3.1-asr-flash` 开启说话人分离时返回编号，未开启时为 `null`。

            <strong>sentence\_id</strong>`integer`

            句子编号，从1开始。

            <strong>sentence\_end</strong>`boolean`

            是否为该句的最终结果。为`true`时表示该句识别完成。

            <strong>begin\_time</strong>`integer`

            句子开始时间，单位毫秒。

            <strong>end\_time</strong>`integer`

            句子结束时间，单位毫秒。仅在`sentence_end`为`true`时返回。

            <strong>text</strong>`string`

            当前句子的识别文本。

            <strong>channel\_id</strong>`integer`

            声道编号，从0开始。

            <strong>words</strong>`array`

            词级别时间戳列表。

            <Accordion title="属性">
              <strong>speaker\_id</strong> `integer | null`

              词对应的说话人编号。`qwen-audio-3.1-asr-flash` 开启说话人分离时返回编号，未开启时为 `null`。

              <strong>text</strong>`string`

              词文本。

              <strong>begin\_time</strong>`integer`

              词开始时间，单位毫秒。

              <strong>end\_time</strong>`integer`

              词结束时间，单位毫秒。

              <strong>punctuation</strong>`string`

              词后的标点符号。无标点时为空字符串。

              <strong>fixed</strong>`boolean`

              词是否已稳定。`false`表示后续事件中该词的时间戳可能调整。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>usage</strong>`object`

        用量信息。仅在`sentence_end`为`true`时返回。

        <Accordion title="属性">
          <strong>duration</strong>`integer`

          已处理的音频时长，单位秒。

          <strong>input\_tokens</strong>`integer`

          本次调用消耗的输入 Token 数。仅适用于 `qwen-audio-3.1-asr-flash`。

          <strong>output\_tokens</strong>`integer`

          本次调用消耗的输出 Token 数。仅适用于 `qwen-audio-3.1-asr-flash`。

          <strong>total\_tokens</strong>`integer`

          本次调用消耗的总 Token 数，等于 `input_tokens` 与 `output_tokens` 之和。仅适用于 `qwen-audio-3.1-asr-flash`。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Tabs>
          <Tab title="非流式">
            ```json
            {
                "output": {
                    "sentence": {
                        "begin_time": 760,
                        "channel_id": 0,
                        "end_time": 3800,
                        "sentence_end": true,
                        "sentence_id": 1,
                        "text": "Hello World，这里是阿里巴巴语音实验室。",
                        "words": [
                            {"begin_time": 760, "end_time": 1040, "fixed": true, "punctuation": "", "text": "Hello"},
                            {"begin_time": 1040, "end_time": 1240, "fixed": true, "punctuation": "，", "text": " World"},
                            {"begin_time": 1360, "end_time": 1880, "fixed": true, "punctuation": "", "text": "这里是"},
                            {"begin_time": 1880, "end_time": 2520, "fixed": true, "punctuation": "", "text": "阿里巴巴"},
                            {"begin_time": 2520, "end_time": 2840, "fixed": true, "punctuation": "", "text": "语音"},
                            {"begin_time": 2840, "end_time": 3800, "fixed": true, "punctuation": "。", "text": "实验室"}
                        ]
                    },
                    "text": "Hello World，这里是阿里巴巴语音实验室。"
                },
                "usage": {
                    "duration": 4
                },
                "request_id": "40e0734d-096f-9ae3-86c1-a8c013287561"
            }
            ```
          </Tab>

          <Tab title="流式">
            仅当音频时长不少于1分钟且设置`X-DashScope-SSE: enable`时，服务端才会以Server-Sent Events协议返回识别结果。SSE事件格式如下：

            ```plaintext
            id:{序列号}
            event:result
            :HTTP_STATUS/200
            data:{JSON数据}
            ```

            返回示例：

            ```plaintext
            id:1
            event:result
            :HTTP_STATUS/200
            data:{"output":{"sentence":{"sentence_id":1,"sentence_end":true,"end_time":3800,"words":[{"end_time":1040,"punctuation":"","begin_time":760,"fixed":true,"text":"Hello"},{"end_time":1240,"punctuation":"，","begin_time":1040,"fixed":true,"text":" World"},{"end_time":1880,"punctuation":"","begin_time":1360,"fixed":true,"text":"这里是"},{"end_time":2520,"punctuation":"","begin_time":1880,"fixed":true,"text":"阿里巴巴"},{"end_time":2840,"punctuation":"","begin_time":2520,"fixed":true,"text":"语音"},{"end_time":3800,"punctuation":"。","begin_time":2840,"fixed":true,"text":"实验室"}],"begin_time":760,"text":"Hello World，这里是阿里巴巴语音实验室。","channel_id":0},"text":"Hello World，这里是阿里巴巴语音实验室。"},"usage":{"duration":4},"request_id":"fc1582e4-935c-9fc2-a482-a98bf43daa69"}
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## SSE 流式结果处理逻辑 <span id="h-fl-stream-logic" />

在流式模式下，客户端需关注以下处理要点：

1. 每收到一个SSE事件，解析`data`字段中的JSON。
2. 对于 Qwen-Audio-3.0-ASR-Flash 和 Fun-ASR-Flash，通过`output.sentence.sentence_end`判断当前句子是否结束：当该值为`true`时，该句识别完成，词级时间戳已稳定，可作为最终结果使用；当该值为`false`时，识别仍在进行中，文本和时间戳可能在后续事件中更新。
3. `usage`信息仅在句子结束事件中返回，包含音频处理时长。对于 `qwen-audio-3.1-asr-flash`，还包含本次调用的输入、输出和总 Token 数，该模型按 Token 计费。

对于 `qwen-audio-3.1-asr-flash`，未开启说话人分离时，词级时间戳在 `output.sentence.words[i].fixed` 为 `true` 后才固定；在此之前，时间戳仍可能调整。开启说话人分离时，应读取 `output.sentences` 中当前及历史句子的结果。
