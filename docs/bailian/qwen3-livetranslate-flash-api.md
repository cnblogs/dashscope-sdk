> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 音视频翻译-通义千问 API 参考

> 本文介绍通过 OpenAI 兼容接口调用 qwen3-livetranslate-flash 模型的输入与输出参数。

<meta property="og:title" content="音视频翻译-通义千问 API 参考" />

> 相关文档：[音视频文件翻译-千问](/zh/model-studio/qwen3-livetranslate-flash)

> 不支持通过 DashScope 接口调用。

## OpenAI 兼容 <span id="d397bcc41eu3q" />

<Tabs>
  <Tab title="北京地域">
    SDK 调用配置的`base_url`为：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 调用配置的`endpoint`：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="新加坡地域">
    SDK 调用配置的`base_url`为：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 调用配置的`endpoint`：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

> 您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。若通过OpenAI SDK进行调用，需要[安装SDK](/zh/model-studio/install-sdk)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="afcd41d2b57zf" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。支持的模型：qwen3-livetranslate-flash、qwen3-livetranslate-flash-2025-12-01。

        <strong>messages</strong>`array`<strong>（必选）</strong>

        消息数组，用于向大模型传递上下文。仅支持传入一个 User Message。

        <Accordion title="消息类型" defaultOpen>
          User Message`object`<strong>（必选）</strong>

          用户消息。

          <Accordion title="属性">
            <strong>content</strong>`array`<strong>（必选）</strong>

            消息内容。

            <Accordion title="属性">
              <strong>type</strong>`string`<strong>（必选）</strong>

              可选值：

              - `input_audio`

                输入音频时需设为`input_audio`。
              - `video_url`

                输入视频文件时需设为`video_url`。

              <strong>input\_audio</strong>`object`

              输入的音频信息。当`type`为`input_audio`时是必选参数。

              <Accordion title="属性">
                <strong>data</strong> `string`<strong>（必选）</strong>

                音频的 URL 或Base64 Data URL。传入本地文件请参见：[输入 Base64 编码的本地文件](/zh/model-studio/qwen-omni#c516d1e824x03)。

                <strong>format</strong>`string`<strong>（必选）</strong>

                输入音频的格式，如`mp3`、`wav`等。
              </Accordion>

              <strong>video\_url</strong>`object`

              输入的视频文件信息。当`type`为`video_url`时是必选参数。

              <Accordion title="属性">
                <strong>url</strong> `string`<strong>（必选）</strong>

                视频文件的公网 URL 或 Base64 Data URL。输入本地视频文件请参见[输入 Base64 编码的本地文件](/zh/model-studio/qwen-omni#c516d1e824x03)。
              </Accordion>
            </Accordion>

            <strong>role</strong>`string`<strong>（必选）</strong>

            用户消息的角色，固定为`user`。
          </Accordion>
        </Accordion>

        <strong>stream</strong>`boolean`<strong>（必选）</strong> 默认值为 `false`

        是否以流式方式输出回复。模型仅支持流式输出方式调用，仅可设为`true`。

        <strong>stream\_options</strong>`object`（可选）

        流式输出的配置项，仅在 `stream` 为 `true` 时生效。

        <Accordion title="属性" defaultOpen>
          <strong>include\_usage</strong>`boolean`（可选）默认值为 `false`

          是否在<strong>最后一个数据块</strong>包含Token消耗信息。

          可选值：

          - `true`：包含；
          - `false`：不包含。
        </Accordion>

        <strong>modalities</strong>`array`（可选）默认值为`["text"]`

        输出数据的模态。可选值：

        - `["text","audio"]`：输出文本与音频；
        - `["text"]`：仅输出文本。

        <strong>audio</strong>`object`（可选）

        输出音频的音色与格式。`modalities`参数需为`["text","audio"]`。

        <Accordion title="属性">
          <strong>voice</strong>`string` <strong>（必选）</strong>

          输出音频的音色。请参见[支持的音色](/zh/model-studio/qwen3-livetranslate-flash#0a5bde7593gdk)。

          <strong>format</strong>`string` <strong>（必选）</strong>

          输出音频的格式，仅支持设定为`wav`。
        </Accordion>

        <strong>max\_tokens</strong>`integer`（可选）

        用于限制模型输出的最大 Token 数。若生成内容超过此值，响应将被截断。

        默认值与最大值均为模型的最大输出长度，请参见[模型选型](/zh/model-studio/machine-translation#154957e783dxk)。

        <strong>seed</strong>`integer`（可选）

        随机数种子。用于确保在相同输入和参数下生成结果可复现。若调用时传入相同的 `seed` 且其他参数不变，模型将尽可能返回相同结果。

        取值范围：`[0,2 31 −1]`。

        <strong>speech\_rate</strong>`float`（可选）默认值为1.0

        控制输出音频的语速。1.0为正常语速，小于1.0为慢速，大于1.0为快速。

        取值范围：\[0.5, 2.0]。

        <strong>temperature</strong>`float`（可选）默认值为0.000001

        采样温度，控制模型生成内容的多样性。temperature越高，生成的内容更多样，反之更确定。

        取值范围： \[0, 2)

        为了翻译的准确性，不建议修改该值。

        <strong>top\_p</strong>`float`（可选）默认值为0.8

        核采样的概率阈值，控制模型生成内容的多样性。

        top\_p越高，生成的内容更多样。反之更确定。

        取值范围：（0,1.0]

        为了翻译的准确性，不建议修改该值。

        <strong>presence\_penalty</strong> `float`（可选）默认值为0

        控制模型生成文本时的内容重复度。

        取值范围：\[-2.0, 2.0]。正值降低重复度，负值增加重复度。为了翻译的准确性，不建议修改该值。

        <strong>top\_k</strong>`integer`（可选）默认值为1

        生成过程中采样候选集的大小。例如，取值为50时，仅将单次生成中得分最高的50个Token组成随机采样的候选集。取值越大，生成的随机性越高；取值越小，生成的确定性越高。取值为None或当top\_k大于100时，表示不启用top\_k策略，此时仅有top\_p策略生效。

        取值需要大于或等于0。为了翻译的准确性，不建议修改该值。

        该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中，配置方式为：`extra_body={"top_k": xxx}`；通过 Node.js SDK 或 HTTP 方式调用时，请作为顶层参数传递。

        <strong>repetition\_penalty</strong>`float`（可选）默认值为1.05

        模型生成时连续序列中的重复度。提高repetition\_penalty时可以降低模型生成的重复度，1.0表示不做惩罚。取值大于0即可。为了翻译的准确性，不建议修改该值。

        该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中，配置方式为：`extra_body={"repetition_penalty": xxx}`；通过 Node.js SDK 或 HTTP 方式调用时，请作为顶层参数传递。

        <strong>translation\_options</strong>`object`<strong>（必选）</strong>

        需配置的翻译参数。

        <Accordion title="属性" defaultOpen>
          <strong>source\_lang</strong> `string` （可选）

          源语言的英文全称，请参见[支持的语种](/zh/model-studio/qwen3-livetranslate-flash#4ffd192226f0s)。若不设置，模型会自动识别输入的语种。

          <strong>target\_lang</strong> `string` <strong>（必选）</strong>

          目标语言的英文全称，请参见[支持的语种](/zh/model-studio/qwen3-livetranslate-flash#4ffd192226f0s)。
        </Accordion>

        该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中，配置方式为：`extra_body={"translation_options": xxx}`；通过 Node.js SDK 或 HTTP 方式调用时，请作为顶层参数传递。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python expandable
          import os
          from openai import OpenAI

          client = OpenAI(
              # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          # ----------------音频输入 ----------------
          messages = [
              {
                  "role": "user",
                  "content": [
                      {
                          "type": "input_audio",
                          "input_audio": {
                              "data": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250211/tixcef/cherry.wav",
                              "format": "wav",
                          },
                      }
                  ],
              }
          ]

          # ----------------视频输入(需取消注释)----------------
          # messages = [
          #     {
          #         "role": "user",
          #         "content": [
          #             {
          #                 "type": "video_url",
          #                 "video_url": {
          #                     "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241115/cqqkru/1.mp4"
          #                 },
          #             }
          #         ],
          #     },
          # ]

          completion = client.chat.completions.create(
              model="qwen3-livetranslate-flash",
              messages=messages,
              modalities=["text", "audio"],
              audio={"voice": "Cherry", "format": "wav"},
              stream=True,
              stream_options={"include_usage": True},
              extra_body={"translation_options": {"source_lang": "zh", "target_lang": "en"}},
          )

          for chunk in completion:
              print(chunk)
          ```

          ```javascript expandable
          import OpenAI from "openai";

          const client = new OpenAI({
              // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey: "sk-xxx",
              // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              apiKey: process.env.DASHSCOPE_API_KEY,
              // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          });

          // ---------------- 音频输入 ----------------
          const messages = [
              {
                  role: "user",
                  content: [
                      {
                          type: "input_audio",
                          input_audio: {
                              data: "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250211/tixcef/cherry.wav",
                              format: "wav",
                          },
                      },
                  ],
              },
          ];

          // ---------------- 视频输入(需取消注释) ----------------
          // const messages = [
          //     {
          //         role: "user",
          //         content: [
          //             {
          //                 type: "video_url",
          //                 video_url: {
          //                     url: "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241115/cqqkru/1.mp4",
          //                 },
          //             },
          //         ],
          //     },
          // ];

          async function main() {
              const completion = await client.chat.completions.create({
                  model: "qwen3-livetranslate-flash",
                  messages: messages,
                  modalities: ["text", "audio"],
                  audio: { voice: "Cherry", format: "wav" },
                  stream: true,
                  stream_options: { include_usage: true },
                  translation_options: { source_lang: "zh", target_lang: "en" },
              });

              for await (const chunk of completion) {
                  console.log(JSON.stringify(chunk));
              }
          }

          main();
          ```

          ```bash expandable
          # ======= 重要提示 =======
          # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
          # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          # === 执行时请删除该注释 ===
          curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -d '{
              "model": "qwen3-livetranslate-flash",
              "messages": [
                  {
                      "role": "user",
                      "content": [
                          {
                              "type": "input_audio",
                              "input_audio": {
                                  "data": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250211/tixcef/cherry.wav",
                                  "format": "wav"
                              }
                          }
                      ]
                  }
              ],
              "modalities": ["text", "audio"],
              "audio": {
                  "voice": "Cherry",
                  "format": "wav"
              },
              "stream": true,
              "stream_options": {
                  "include_usage": true
              },
              "translation_options": {
                  "source_lang": "zh",
                  "target_lang": "en"
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
        ### chat响应chunk对象（流式输出） <span id="d48fc149a0w4n" />

        <strong>id</strong>`string`

        本次调用的唯一标识符。每个chunk对象有相同的 id。

        <strong>choices</strong>`array`

        模型生成内容的数组。若设置`include_usage`参数为`true`，则`choices`在最后一个chunk中为空数组。

        <Accordion title="属性">
          <strong>delta</strong> `object`

          请求的增量对象。

          <Accordion title="属性">
            <strong>content</strong> `string`

            增量消息内容。

            <strong>reasoning\_content</strong> `string`

            该值固定为`null`。

            <strong>function\_call</strong> `object`

            该值固定为`null`。

            <strong>audio</strong>`object`

            输出的音频信息。

            <Accordion title="属性">
              <strong>data</strong> `string`

              增量的 Base64 音频编码数据。

              <strong>expires\_at</strong> `integer`

              创建请求时的时间戳。

              <strong>id</strong> `string`

              输出音频的唯一标识符。
            </Accordion>

            <strong>refusal</strong> `object`

            该参数当前固定为`null`。

            <strong>role</strong> `string`

            增量消息对象的角色，只在第一个chunk中有值。

            <strong>tool\_calls</strong> `array`

            该值固定为`null`。
          </Accordion>

          <strong>finish\_reason</strong> `string`

          模型停止生成的原因。有以下情况：

          - 自然停止输出时为`stop`；
          - 生成未结束时为`null`；
          - 生成长度过长而结束为`length`。

          <strong>index</strong> `integer`

          当前响应在`choices`数组中的索引，固定为0。

          <strong>logprobs</strong>`object`

          该值固定为`null`。
        </Accordion>

        <strong>created</strong>`integer`

        本次请求被创建时的时间戳。每个chunk有相同的时间戳。

        <strong>model</strong>`string`

        本次请求使用的模型。

        <strong>object</strong> `string`

        始终为`chat.completion.chunk`。

        <strong>service\_tier</strong> `string`

        该值固定为`null`。

        <strong>system\_fingerprint</strong>`string`

        该值固定为`null`。

        <strong>usage</strong> `object`

        本次请求消耗的Token。只在`include_usage`为`true`时，在最后一个chunk显示。

        <Accordion title="属性">
          <strong>completion\_tokens</strong> `integer`

          模型输出的 Token 数。

          <strong>prompt\_tokens</strong> `integer`

          输入 Token 数。

          <strong>total\_tokens</strong> `integer`

          总 Token 数，为`prompt_tokens`与`completion_tokens`的总和。

          <strong>completion\_tokens\_details</strong> `object`

          输出 Token 的详细信息。

          <Accordion title="属性">
            <strong>audio\_tokens</strong>`integer`

            输出的音频 Token 数。

            <strong>reasoning\_tokens</strong> `integer`

            该值固定为`null`。

            <strong>text\_tokens</strong>`integer`

            输出文本 Token 数。
          </Accordion>

          <strong>prompt\_tokens\_details</strong> `object`

          输入 Token的细粒度分类。

          <Accordion title="属性">
            <strong>audio\_tokens</strong> `integer`

            输入音频的 Token 数。

            > 视频文件中的音频 Token 数通过本参数返回。

            <strong>text\_tokens</strong> `integer`

            输入文本的 Token 数。该值固定为0。

            <strong>video\_tokens</strong> `integer`

            输入视频的 Token 数。
          </Accordion>
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json 文本输出chunk
          {
            "id": "chatcmpl-c22a54b8-40cc-4a1d-988b-f84cdf86868f",
            "choices": [
              {
                "delta": {
                  "content": " of",
                  "function_call": null,
                  "refusal": null,
                  "role": null,
                  "tool_calls": null
                },
                "finish_reason": null,
                "index": 0,
                "logprobs": null
              }
            ],
            "created": 1764755440,
            "model": "qwen3-livetranslate-flash",
            "object": "chat.completion.chunk",
            "service_tier": null,
            "system_fingerprint": null,
            "usage": null
          }
          ```

          ```json 音频输出chunk expandable
          {
            "id": "chatcmpl-c22a54b8-40cc-4a1d-988b-f84cdf86868f",
            "choices": [
              {
                "delta": {
                  "content": null,
                  "function_call": null,
                  "refusal": null,
                  "role": null,
                  "tool_calls": null,
                  "audio": {
                    "data": "///+//7////+////////////AAAAAAAAAAABA......",
                    "expires_at": 1764755440,
                    "id": "audio_c22a54b8-40cc-4a1d-988b-f84cdf86868f"
                  }
                },
                "finish_reason": null,
                "index": 0,
                "logprobs": null
              }
            ],
            "created": 1764755440,
            "model": "qwen3-livetranslate-flash",
            "object": "chat.completion.chunk",
            "service_tier": null,
            "system_fingerprint": null,
            "usage": null
          }
          ```

          ```json Token 消耗chunk expandable
          {
            "id": "chatcmpl-c22a54b8-40cc-4a1d-988b-f84cdf86868f",
            "choices": [],
            "created": 1764755440,
            "model": "qwen3-livetranslate-flash",
            "object": "chat.completion.chunk",
            "service_tier": null,
            "system_fingerprint": null,
            "usage": {
              "completion_tokens": 242,
              "prompt_tokens": 415,
              "total_tokens": 657,
              "completion_tokens_details": {
                "accepted_prediction_tokens": null,
                "audio_tokens": 191,
                "reasoning_tokens": null,
                "rejected_prediction_tokens": null,
                "text_tokens": 51
              },
              "prompt_tokens_details": {
                "audio_tokens": 415,
                "cached_tokens": null,
                "text_tokens": 0
              }
            }
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>
