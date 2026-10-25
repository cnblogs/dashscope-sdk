> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 非实时语音识别（Qwen-ASR）API参考

> 本文介绍 Qwen-ASR 模型的输入与输出参数。可通过OpenAI 兼容或DashScope协议调用 API。

<meta property="og:title" content="非实时语音识别（Qwen-ASR）API参考" />

## 模型接入方式 <span id="6560a479ca3lm" />

不同[模型](/zh/model-studio/non-realtime-speech-recognition-user-guide)支持的接入方式不同，请根据下表选择正确的方式进行集成。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "21.85%" }} /><col style={{ width: "78.15%" }} /></colgroup><thead><tr><th><p><strong>模型</strong></p></th><th><p><strong>接入方式</strong></p></th></tr></thead><tbody><tr><td><p>千问3-ASR-Flash-Filetrans</p></td><td><p>仅支持<a href="/zh/model-studio/qwen-asr-api-reference#9937e8884002q">DashScope异步调用</a>方式</p></td></tr><tr><td><p>千问3-ASR-Flash</p></td><td><p><a href="/zh/model-studio/qwen-asr-api-reference#d397bcc41eu3q">OpenAI 兼容</a>和<a href="/zh/model-studio/qwen-asr-api-reference#1afc6b20a29ie">DashScope同步调用</a>两种方式</p></td></tr></tbody></table>

## OpenAI 兼容 <span id="d397bcc41eu3q" />

<Tip>
  美国地域不支持OpenAI兼容模式。
</Tip>

### URL <span id="57aa8161d39we" />

<Tabs>
  <Tab title="华北2（北京）">
    HTTP请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions`

    SDK调用配置的base\_url：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    HTTP请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`

    SDK调用配置的base\_url：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求参数 <span id="0eed3f701cd4j" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        [模型](/zh/model-studio/non-realtime-speech-recognition-user-guide)名称。仅适用于千问3-ASR-Flash模型。

        <strong>messages</strong>`array`<strong>（必选）</strong>

        消息列表。

        <Accordion title="消息类型" defaultOpen>
          System Message`object`（可选）

          用于为语音识别提供上下文（Context），如背景文本和实体词表等参考信息，不支持设置模型角色等传统系统提示词。如果设置系统消息，请放在messages列表的第一位。

          <Accordion title="属性">
            <strong>role</strong>`string`<strong>（必选）</strong>

            固定为`system`。
          </Accordion>

          User Message`object`<strong>（必选）</strong>

          用户发送给模型的消息。

          <Accordion title="属性">
            <strong>content</strong>`array`<strong>（必选）</strong>

            用户消息的内容。仅允许设置一组消息。

            <Accordion title="属性" defaultOpen>
              <strong>type</strong>`string`<strong>（必选）</strong>

              固定为`input_audio`，代表输入的是音频。

              <strong>input\_audio</strong>`string`<strong>（必选）</strong>

              待识别音频。具体用法请参见[调用示例](/zh/model-studio/qwen-asr-api-reference#qwen-asr-openai-examples)。

              千问3-ASR-Flash模型在OpenAI兼容模式下支持两种输入形式：Base64编码的文件和公网可访问的待识别文件URL。

              使用SDK时，若录音文件存储在[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/simple-upload#a632b50f190j8)，不支持使用以 `oss://`为前缀的临时 URL。

              使用RESTful API时，若录音文件存储在[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/simple-upload#a632b50f190j8)，支持使用以 `oss://`为前缀的临时 URL。但需注意：

              <Tip>
                - 临时 URL 有效期48小时，过期后无法使用，<strong>请勿用于生产环境。</strong>
                - 文件上传凭证接口限流为 100 QPS 且不支持扩容，<strong>请勿用于生产环境、高并发及压测场景。</strong>
                - 生产环境建议使用[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss) 等稳定存储，确保文件长期可用并规避限流问题。
              </Tip>
            </Accordion>

            <strong>role</strong>`string`<strong>（必选）</strong>

            用户消息的角色，固定为`user`。
          </Accordion>
        </Accordion>

        <strong>asr\_options</strong>`object`（可选）

        用来指定某些功能是否启用。

        > `asr_options`非OpenAI标准参数，若使用OpenAI SDK，请通过`extra_body`传入。

        <Accordion title="属性" defaultOpen>
          <strong>language</strong> *string*（可选）无默认值

          若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率。

          只能指定一个语种。

          若音频语种不确定，或包含多种语种（例如中英日韩混合），请勿指定该参数。

          <Accordion title="取值范围">
            - zh：中文（普通话、四川话、闽南语、吴语）
            - yue：粤语
            - en：英文
            - ja：日语
            - de：德语
            - ko：韩语
            - ru：俄语
            - fr：法语
            - pt：葡萄牙语
            - ar：阿拉伯语
            - it：意大利语
            - es：西班牙语
            - hi：印地语
            - id：印尼语
            - th：泰语
            - tr：土耳其语
            - uk：乌克兰语
            - vi：越南语
            - cs：捷克语
            - da：丹麦语
            - fil：菲律宾语
            - fi：芬兰语
            - is：冰岛语
            - ms：马来语
            - no：挪威语
            - pl：波兰语
            - sv：瑞典语
          </Accordion>

          <strong>enable\_itn</strong>`boolean`（可选）默认值为`false`

          是否启用ITN（Inverse Text Normalization，逆文本标准化）。该功能仅适用于中文和英文音频。

          开启后，语音识别结果中的中文数字（如"一百二十三"）或英文数字（如"one hundred"）将自动转换为阿拉伯数字（如"123"）。

          参数值：

          - true：开启；
          - false：关闭。
        </Accordion>

        <strong>stream</strong>`boolean`（可选）默认值为`false`

        是否以流式输出方式回复。相关文档：[流式输出](/zh/model-studio/stream)

        可选值：

        - `false`：模型生成全部内容后一次性返回；
        - `true`：边生成边输出，每生成一部分内容即返回一个数据块（chunk）。需实时逐个读取这些块以拼接完整回复。

        推荐设置为`true`，可提升阅读体验并降低超时风险。

        <strong>stream\_options</strong>`object`（可选）

        流式输出的配置项，仅在 `stream` 为 `true` 时生效。

        <Accordion title="属性" defaultOpen>
          <strong>include\_usage</strong>`boolean`（可选）默认值为`false`

          是否在响应的最后一个数据块包含Token消耗信息。

          可选值：

          - `true`：包含；
          - `false`：不包含。

          > 流式输出时，Token 消耗信息仅可出现在响应的最后一个数据块。
        </Accordion>
      </td>

      <td>
        ### 调用示例 <span id="qwen-asr-openai-examples" />

        <Tabs>
          <Tab title="输入内容：音频文件URL">
            <Tabs>
              <Tab title="Python SDK">
                ```python expandable
                from openai import OpenAI
                import os

                try:
                    client = OpenAI(
                        # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                        # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
                        api_key=os.getenv("DASHSCOPE_API_KEY"),
                        # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                        base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                    )

                    stream_enabled = False  # 是否开启流式输出
                    completion = client.chat.completions.create(
                        model="qwen3-asr-flash",
                        messages=[
                            {
                                "content": [
                                    {
                                        "type": "input_audio",
                                        "input_audio": {
                                            "data": "{YOUR_AUDIO_URL}"
                                        }
                                    }
                                ],
                                "role": "user"
                            }
                        ],
                        stream=stream_enabled,
                        # stream设为False时，不能设置stream_options参数
                        # stream_options={"include_usage": True},
                        extra_body={
                            "asr_options": {
                                # "language": "zh",
                                "enable_itn": False
                            }
                        }
                    )
                    if stream_enabled:
                        full_content = ""
                        print("流式输出内容为：")
                        for chunk in completion:
                            # 如果stream_options.include_usage为True，则最后一个chunk的choices字段为空列表，需要跳过（可以通过chunk.usage获取 Token 使用量）
                            print(chunk)
                            if chunk.choices and chunk.choices[0].delta.content:
                                full_content += chunk.choices[0].delta.content
                        print(f"完整内容为：{full_content}")
                    else:
                        print(f"非流式输出内容为：{completion.choices[0].message.content}")
                except Exception as e:
                    print(f"错误信息：{e}")
                ```
              </Tab>

              <Tab title="Node.js SDK">
                ```javascript expandable
                // 运行前的准备工作:
                // Windows/Mac/Linux 通用:
                // 1. 确保已安装 Node.js (建议版本 >= 14)
                // 2. 运行以下命令安装必要的依赖: npm install openai

                import OpenAI from "openai";

                const client = new OpenAI({
                  // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey: "sk-xxx",
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                });

                async function main() {
                  try {
                    const streamEnabled = false; // 是否开启流式输出
                    const completion = await client.chat.completions.create({
                      model: "qwen3-asr-flash",
                      messages: [
                        {
                          role: "user",
                          content: [
                            {
                              type: "input_audio",
                              input_audio: {
                                data: "{YOUR_AUDIO_URL}"
                              }
                            }
                          ]
                        }
                      ],
                      stream: streamEnabled,
                      // stream设为False时，不能设置stream_options参数
                      // stream_options: {
                      //   "include_usage": true
                      // },
                      asr_options: {
                        // language: "zh",
                        enable_itn: false
                      }
                    });

                    if (streamEnabled) {
                      let fullContent = "";
                      console.log("流式输出内容为：");
                      for await (const chunk of completion) {
                        console.log(JSON.stringify(chunk));
                        if (chunk.choices && chunk.choices.length > 0) {
                          const delta = chunk.choices[0].delta;
                          if (delta && delta.content) {
                            fullContent += delta.content;
                          }
                        }
                      }
                      console.log(`完整内容为：${fullContent}`);
                    } else {
                      console.log(`非流式输出内容为：${completion.choices[0].message.content}`);
                    }
                  } catch (err) {
                    console.error(`错误信息：${err}`);
                  }
                }

                main();
                ```
              </Tab>

              <Tab title="cURL">
                以下为华北2（北京）地域的配置，调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)，各地域的配置不同。

                ```bash
                curl -X POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H "Content-Type: application/json" \
                -d '{
                    "model": "qwen3-asr-flash",
                    "messages": [
                        {
                            "content": [
                                {
                                    "type": "input_audio",
                                    "input_audio": {
                                        "data": "{YOUR_AUDIO_URL}"
                                    }
                                }
                            ],
                            "role": "user"
                        }
                    ],
                    "stream":false,
                    "asr_options": {
                        "enable_itn": false
                    }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="输入内容：Base64编码的音频文件">
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

                  # input.mp3为用于声音复刻的本地音频文件，请替换为自己的音频文件路径，确保其符合音频要求
                  file_path = pathlib.Path("{YOUR_AUDIO_FILE}")
                  base64_str = base64.b64encode(file_path.read_bytes()).decode()
                  data_uri = f"data:audio/mpeg;base64,{base64_str}"
                  ```

                  ```java
                  import java.nio.file.*;
                  import java.util.Base64;

                  public class Main {
                      /**
                       * filePath为用于声音复刻的本地音频文件，请替换为自己的音频文件路径，确保其符合音频要求
                       */
                      public static String toDataUrl(String filePath) throws Exception {
                          byte[] bytes = Files.readAllBytes(Paths.get(filePath));
                          String encoded = Base64.getEncoder().encodeToString(bytes);
                          return "data:audio/mpeg;base64," + encoded;
                      }

                      // 使用示例
                      public static void main(String[] args) throws Exception {
                          System.out.println(toDataUrl("{YOUR_AUDIO_FILE}"));
                      }
                  }
                  ```
                </CodeGroup>
              </Accordion>

            <CodeGroup dropdown>
              ```python Python SDK expandable
              import base64
              from openai import OpenAI
              import os
              import pathlib

              try:
                  # 请替换为实际的音频文件路径
                  file_path = "{YOUR_AUDIO_FILE}"
                  # 请替换为实际的音频文件MIME类型
                  audio_mime_type = "audio/mpeg"

                  file_path_obj = pathlib.Path(file_path)
                  if not file_path_obj.exists():
                      raise FileNotFoundError(f"音频文件不存在: {file_path}")

                  base64_str = base64.b64encode(file_path_obj.read_bytes()).decode()
                  data_uri = f"data:{audio_mime_type};base64,{base64_str}"

                  client = OpenAI(
                      # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                      # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
                      api_key=os.getenv("DASHSCOPE_API_KEY"),
                      # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                  )

                  stream_enabled = False  # 是否开启流式输出
                  completion = client.chat.completions.create(
                      model="qwen3-asr-flash",
                      messages=[
                          {
                              "content": [
                                  {
                                      "type": "input_audio",
                                      "input_audio": {
                                          "data": data_uri
                                      }
                                  }
                              ],
                              "role": "user"
                          }
                      ],
                      stream=stream_enabled,
                      # stream设为False时，不能设置stream_options参数
                      # stream_options={"include_usage": True},
                      extra_body={
                          "asr_options": {
                              # "language": "zh",
                              "enable_itn": False
                          }
                      }
                  )
                  if stream_enabled:
                      full_content = ""
                      print("流式输出内容为：")
                      for chunk in completion:
                          # 如果stream_options.include_usage为True，则最后一个chunk的choices字段为空列表，需要跳过（可以通过chunk.usage获取 Token 使用量）
                          print(chunk)
                          if chunk.choices and chunk.choices[0].delta.content:
                              full_content += chunk.choices[0].delta.content
                      print(f"完整内容为：{full_content}")
                  else:
                      print(f"非流式输出内容为：{completion.choices[0].message.content}")
              except Exception as e:
                  print(f"错误信息：{e}")
              ```

              ```javascript Node.js SDK expandable
              // 运行前的准备工作:
              // Windows/Mac/Linux 通用:
              // 1. 确保已安装 Node.js (建议版本 >= 14)
              // 2. 运行以下命令安装必要的依赖: npm install openai

              import OpenAI from "openai";
              import { readFileSync } from 'fs';

              const client = new OpenAI({
                // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey: "sk-xxx",
                apiKey: process.env.DASHSCOPE_API_KEY,
                // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              });

              const encodeAudioFile = (audioFilePath) => {
                  const audioFile = readFileSync(audioFilePath);
                  return audioFile.toString('base64');
              };

              // 请替换为实际的音频文件路径
              const dataUri = `data:audio/mpeg;base64,${encodeAudioFile("{YOUR_AUDIO_FILE}")}`;

              async function main() {
                try {
                  const streamEnabled = false; // 是否开启流式输出
                  const completion = await client.chat.completions.create({
                    model: "qwen3-asr-flash",
                    messages: [
                      {
                        role: "user",
                        content: [
                          {
                            type: "input_audio",
                            input_audio: {
                              data: dataUri
                            }
                          }
                        ]
                      }
                    ],
                    stream: streamEnabled,
                    // stream设为False时，不能设置stream_options参数
                    // stream_options: {
                    //   "include_usage": true
                    // },
                    asr_options: {
                      // language: "zh",
                      enable_itn: false
                    }
                  });

                  if (streamEnabled) {
                    let fullContent = "";
                    console.log("流式输出内容为：");
                    for await (const chunk of completion) {
                      console.log(JSON.stringify(chunk));
                      if (chunk.choices && chunk.choices.length > 0) {
                        const delta = chunk.choices[0].delta;
                        if (delta && delta.content) {
                          fullContent += delta.content;
                        }
                      }
                    }
                    console.log(`完整内容为：${fullContent}`);
                  } else {
                    console.log(`非流式输出内容为：${completion.choices[0].message.content}`);
                  }
                } catch (err) {
                  console.error(`错误信息：${err}`);
                }
              }

              main();
              ```
            </CodeGroup>
          </Tab>
        </Tabs>
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
        ### 响应参数 <span id="8c9faf146399l" />

        <strong>id</strong>`string`

        本次调用的唯一标识符。

        <strong>choices</strong>`array`

        模型的输出信息。

        <Accordion title="属性">
          <strong>finish\_reason</strong>`string`

          有三种情况：

          - 正在生成时为null；
          - 因模型输出自然结束，或触发输入参数中的stop条件而结束时为stop；
          - 因生成长度过长而结束为length。

          <strong>index</strong>`integer`

          当前对象在`choices`数组中的索引。

          <strong>message</strong>`object`

          模型输出的消息对象。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string`

            输出消息的角色，固定为assistant。

            <strong>content</strong>`array`

            语音识别结果。

            <strong>annotations</strong>`array`

            输出标注信息（如语种）

            <Accordion title="属性" defaultOpen>
              <strong>language</strong>`string`

              被识别音频的语种。当请求参数`language`已指定语种时，该值与所指定的参数一致。

              <Accordion title="取值范围">
                - zh：中文（普通话、四川话、闽南语、吴语）
                - yue：粤语
                - en：英文
                - ja：日语
                - de：德语
                - ko：韩语
                - ru：俄语
                - fr：法语
                - pt：葡萄牙语
                - ar：阿拉伯语
                - it：意大利语
                - es：西班牙语
                - hi：印地语
                - id：印尼语
                - th：泰语
                - tr：土耳其语
                - uk：乌克兰语
                - vi：越南语
                - cs：捷克语
                - da：丹麦语
                - fil：菲律宾语
                - fi：芬兰语
                - is：冰岛语
                - ms：马来语
                - no：挪威语
                - pl：波兰语
                - sv：瑞典语
              </Accordion>

              <strong>type</strong>`string`

              固定为`audio_info`，表示音频信息。

              <strong>emotion</strong>`string`

              被识别音频的情感。支持的情感如下：

              - `surprised`：惊讶
              - `neutral`：平静
              - `happy`：愉快
              - `sad`：悲伤
              - `disgusted`：厌恶
              - `angry`：愤怒
              - `fearful`：恐惧
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>created</strong>`integer`

        请求创建时的 Unix 时间戳（秒）。

        <strong>model</strong>`string`

        本次请求使用的模型。

        <strong>object</strong>`string`

        始终为`chat.completion`。

        <strong>usage</strong>`object`

        本次请求的Token消耗信息。

        <Accordion title="属性" defaultOpen>
          <strong>completion\_tokens</strong> `integer`

          模型输出的 Token 数。

          <strong>completion\_tokens\_details</strong> `object`

          模型输出的 Token 细粒度详情。

          <Accordion title="属性">
            <strong>text\_tokens</strong> `integer`

            模型输出文本的Token数。
          </Accordion>

          <strong>prompt\_tokens</strong> `object`

          输入的Token数。

          <strong>prompt\_tokens\_details</strong> `object`

          输入的 Token 细粒度详情。

          <Accordion title="属性">
            <strong>audio\_tokens</strong> `integer`

            输入音频长度（Token）。音频转换Token规则：每秒音频转换为25个Token，不足1秒按1秒计算。

            <strong>text\_tokens</strong> `integer`

            无需关注该参数。
          </Accordion>

          <strong>seconds</strong> `integer`

          音频时长（秒）。

          <strong>total\_tokens</strong> `integer`

          输入和输出总Token数（`total_tokens = completion_tokens + prompt_tokens`）。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json 非流式输出 expandable
          {
              "choices": [
                  {
                      "finish_reason": "stop",
                      "index": 0,
                      "message": {
                          "annotations": [
                              {
                                  "emotion": "neutral",
                                  "language": "zh",
                                  "type": "audio_info"
                              }
                          ],
                          "content": "欢迎使用阿里云。",
                          "role": "assistant"
                      }
                  }
              ],
              "created": 1767683986,
              "id": "chatcmpl-487abe5f-d4f2-9363-a877-xxxxxxx",
              "model": "qwen3-asr-flash",
              "object": "chat.completion",
              "usage": {
                  "completion_tokens": 12,
                  "completion_tokens_details": {
                      "text_tokens": 12
                  },
                  "prompt_tokens": 42,
                  "prompt_tokens_details": {
                      "audio_tokens": 42,
                      "text_tokens": 0
                  },
                  "seconds": 1,
                  "total_tokens": 54
              }
          }
          ```

          ```json 流式输出
          data: {"model":"qwen3-asr-flash","id":"chatcmpl-3fb97803-d27f-9289-8889-xxxxx","created":1767685989,"object":"chat.completion.chunk","usage":null,"choices":[{"logprobs":null,"index":0,"delta":{"content":"","role":"assistant"}}]}

          data: {"model":"qwen3-asr-flash","id":"chatcmpl-3fb97803-d27f-9289-8889-xxxxx","choices":[{"delta":{"annotations":[{"type":"audio_info","language":"zh","emotion":"neutral"}],"content":"欢迎","role":null},"index":0}],"created":1767685989,"object":"chat.completion.chunk","usage":null}

          data: {"model":"qwen3-asr-flash","id":"chatcmpl-3fb97803-d27f-9289-8889-xxxxx","choices":[{"delta":{"annotations":[{"type":"audio_info","language":"zh","emotion":"neutral"}],"content":"使用","role":null},"index":0}],"created":1767685989,"object":"chat.completion.chunk","usage":null}

          data: {"model":"qwen3-asr-flash","id":"chatcmpl-3fb97803-d27f-9289-8889-xxxxx","choices":[{"delta":{"annotations":[{"type":"audio_info","language":"zh","emotion":"neutral"}],"content":"阿里","role":null},"index":0}],"created":1767685989,"object":"chat.completion.chunk","usage":null}

          data: {"model":"qwen3-asr-flash","id":"chatcmpl-3fb97803-d27f-9289-8889-xxxxx","choices":[{"delta":{"annotations":[{"type":"audio_info","language":"zh","emotion":"neutral"}],"content":"云","role":null},"index":0}],"created":1767685989,"object":"chat.completion.chunk","usage":null}

          data: {"model":"qwen3-asr-flash","id":"chatcmpl-3fb97803-d27f-9289-8889-xxxxx","choices":[{"delta":{"annotations":[{"type":"audio_info","language":"zh","emotion":"neutral"}],"content":"。","role":null},"index":0}],"created":1767685989,"object":"chat.completion.chunk","usage":null}

          data: {"model":"qwen3-asr-flash","id":"chatcmpl-3fb97803-d27f-9289-8889-xxxxx","choices":[{"delta":{"role":null},"index":0,"finish_reason":"stop"}],"created":1767685989,"object":"chat.completion.chunk","usage":null}

          data: [DONE]
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## DashScope同步调用 <span id="1afc6b20a29ie" />

### URL <span id="fe508c8badxjg" />

<Tabs>
  <Tab title="华北2（北京）">
    HTTP请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    SDK调用配置的base\_url：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    HTTP请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    SDK调用配置的base\_url：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    HTTP请求地址：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    SDK调用配置的base\_url：`https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1`
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求参数 <span id="2a1c410015otp" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        [模型](/zh/model-studio/non-realtime-speech-recognition-user-guide)名称。仅适用于千问3-ASR-Flash模型。

        <strong>messages</strong>`array`<strong>（必选）</strong>

        消息列表。

        > 通过HTTP调用时，请将<strong>messages</strong>放入 <strong>input</strong> 对象中。

        <Accordion title="消息类型" defaultOpen>
          System Message`object`（可选）

          用于为语音识别提供上下文（Context），如背景文本和实体词表等参考信息，不支持设置模型角色等传统系统提示词。如果设置系统消息，请放在messages列表的第一位。

          仅千问3-ASR-Flash支持该参数。

          <Accordion title="属性">
            <strong>role</strong>`string`<strong>（必选）</strong>

            固定为`system`。
          </Accordion>

          User Message`object`<strong>（必选）</strong>

          用户发送给模型的消息。

          <Accordion title="属性">
            <strong>content</strong>`array`<strong>（必选）</strong>

            用户消息的内容。仅允许设置一组消息。

            <Accordion title="属性" defaultOpen>
              <strong>audio</strong>`string`<strong>（必选）</strong>

              待识别音频。具体用法请参见[调用示例](/zh/model-studio/qwen-asr-api-reference#qwen-asr-sync-examples)。

              千问3-ASR-Flash模型在DashScope调用方式下支持三种输入形式：Base64编码的文件、本地文件绝对路径、公网可访问的待识别文件URL。

              使用SDK时，若录音文件存储在[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/simple-upload#a632b50f190j8)，不支持使用以 `oss://`为前缀的临时 URL。

              使用RESTful API时，若录音文件存储在[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/simple-upload#a632b50f190j8)，支持使用以 `oss://`为前缀的临时 URL。但需注意：

              <Tip>
                - 临时 URL 有效期48小时，过期后无法使用，<strong>请勿用于生产环境。</strong>
                - 文件上传凭证接口限流为 100 QPS 且不支持扩容，<strong>请勿用于生产环境、高并发及压测场景。</strong>
                - 生产环境建议使用[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss) 等稳定存储，确保文件长期可用并规避限流问题。
              </Tip>
            </Accordion>

            <strong>role</strong>`string`<strong>（必选）</strong>

            用户消息的角色，固定为`user`。
          </Accordion>
        </Accordion>

        <strong>asr\_options</strong>`object`（可选）

        用来指定某些功能是否启用。

        仅千问3-ASR-Flash支持该参数。

        <Accordion title="属性" defaultOpen>
          <strong>language</strong> *string*（可选）无默认值

          若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率。

          只能指定一个语种。

          若音频语种不确定，或包含多种语种（例如中英日韩混合），请勿指定该参数。

          <Accordion title="取值范围">
            - zh：中文（普通话、四川话、闽南语、吴语）
            - yue：粤语
            - en：英文
            - ja：日语
            - de：德语
            - ko：韩语
            - ru：俄语
            - fr：法语
            - pt：葡萄牙语
            - ar：阿拉伯语
            - it：意大利语
            - es：西班牙语
            - hi：印地语
            - id：印尼语
            - th：泰语
            - tr：土耳其语
            - uk：乌克兰语
            - vi：越南语
            - cs：捷克语
            - da：丹麦语
            - fil：菲律宾语
            - fi：芬兰语
            - is：冰岛语
            - ms：马来语
            - no：挪威语
            - pl：波兰语
            - sv：瑞典语
          </Accordion>

          <strong>enable\_itn</strong>`boolean`（可选）默认值为`false`

          是否启用ITN（Inverse Text Normalization，逆文本标准化）。该功能仅适用于中文和英文音频。

          开启后，语音识别结果中的中文数字（如"一百二十三"）或英文数字（如"one hundred"）将自动转换为阿拉伯数字（如"123"）。

          参数值：

          - true：开启；
          - false：关闭。
        </Accordion>
      </td>

      <td>
        ### 调用示例 <span id="qwen-asr-sync-examples" />

        Qwen3-ASR-Flash 支持最长 5 分钟录音，输入支持公网音频文件 URL 或本地文件上传，可流式返回识别结果。

        <Tabs>
          <Tab title="输入内容：音频文件URL">
            <CodeGroup>
              ```
              curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation" \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                  "model": "qwen3-asr-flash",
                  "input": {
                      "messages": [
                          {
                              "content": [
                                  {
                                      "audio": "{YOUR_AUDIO_URL}"
                                  }
                              ],
                              "role": "user"
                          }
                      ]
                  },
                  "parameters": {
                      "asr_options": {
                          "enable_itn": false
                      }
                  }
              }'
              ```

              ```
              import java.util.Arrays;
              import java.util.Collections;
              import java.util.HashMap;
              import java.util.Map;

              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.alibaba.dashscope.utils.Constants;
              import com.alibaba.dashscope.utils.JsonUtils;

              public class Main {
                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      MultiModalConversation conv = new MultiModalConversation();
                      MultiModalMessage userMessage = MultiModalMessage.builder()
                              .role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      Collections.singletonMap("audio", "{YOUR_AUDIO_URL}")))
                              .build();

                      Map<String, Object> asrOptions = new HashMap<>();
                      asrOptions.put("enable_itn", false);
                      // asrOptions.put("language", "zh"); // 可选，若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 新加坡/美国地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3-asr-flash")
                              .message(userMessage)
                              .parameter("asr_options", asrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(JsonUtils.toJson(result));
                  }
                  public static void main(String[] args) {
                      try {
                          // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                          Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```

              ```
              import os
              import dashscope

              # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
              dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

              messages = [
                  {"role": "user", "content": [{"audio": "{YOUR_AUDIO_URL}"}]}
              ]

              response = dashscope.MultiModalConversation.call(
                  # 新加坡/美国地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  model="qwen3-asr-flash",
                  messages=messages,
                  result_format="message",
                  asr_options={
                      # "language": "zh", # 可选，若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率
                      "enable_itn":False
                  }
              )
              print(response)
              ```
            </CodeGroup>
          </Tab>

          <Tab title="输入内容：Base64编码的音频文件">
            可输入Base64编码数据（[Data URL](https://www.rfc-editor.org/rfc/rfc2397)），格式为：`data:<mediatype>;base64,<data>`。

            - `<mediatype>`：MIME类型

              因音频格式而异，例如：

              - WAV：`audio/wav`
              - MP3：`audio/mpeg`
            - `<data>`：音频转成的Base64编码的字符串

              Base64编码会增大体积，请控制原文件大小，确保编码后仍符合输入音频大小限制（10MB）
            - 示例：`data:audio/mpeg;base64,SUQzBAAAAAAAI1RTU0UAAAAPAAADTGF2ZjU4LjI5LjEwMAAAAAAAAAAAAAAA//PAxABQ/BXRbMPe4IQAhl9`

              <Accordion title="点击查看示例代码">
                <CodeGroup dropdown>
                  ```python
                  import base64, pathlib

                  # input.mp3为待识别的本地音频文件，请替换为自己的音频文件路径，确保其符合音频要求
                  file_path = pathlib.Path("{YOUR_AUDIO_FILE}")
                  base64_str = base64.b64encode(file_path.read_bytes()).decode()
                  data_uri = f"data:audio/mpeg;base64,{base64_str}"
                  ```

                  ```java
                  import java.nio.file.*;
                  import java.util.Base64;

                  public class Main {
                      /**
                       * filePath为待识别的本地音频文件，请替换为自己的音频文件路径，确保其符合音频要求
                       */
                      public static String toDataUrl(String filePath) throws Exception {
                          byte[] bytes = Files.readAllBytes(Paths.get(filePath));
                          String encoded = Base64.getEncoder().encodeToString(bytes);
                          return "data:audio/mpeg;base64," + encoded;
                      }

                      // 使用示例
                      public static void main(String[] args) throws Exception {
                          System.out.println(toDataUrl("{YOUR_AUDIO_FILE}"));
                      }
                  }
                  ```
                </CodeGroup>
              </Accordion>

            <CodeGroup dropdown>
              ```python Python SDK expandable
              import base64
              import dashscope
              import os
              import pathlib

              # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
              dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

              # 请替换为实际的音频文件路径
              file_path = "{YOUR_AUDIO_FILE}"
              # 请替换为实际的音频文件MIME类型
              audio_mime_type = "audio/mpeg"

              file_path_obj = pathlib.Path(file_path)
              if not file_path_obj.exists():
                  raise FileNotFoundError(f"音频文件不存在: {file_path}")

              base64_str = base64.b64encode(file_path_obj.read_bytes()).decode()
              data_uri = f"data:{audio_mime_type};base64,{base64_str}"

              messages = [
                  {"role": "user", "content": [{"audio": data_uri}]}
              ]
              response = dashscope.MultiModalConversation.call(
                  # 新加坡/美国地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  model="qwen3-asr-flash",
                  messages=messages,
                  result_format="message",
                  asr_options={
                      # "language": "zh", # 可选，若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率
                      "enable_itn":False
                  }
              )
              print(response)
              ```

              ```java Java SDK expandable
              import java.io.IOException;
              import java.nio.file.Files;
              import java.nio.file.Paths;
              import java.util.*;

              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.alibaba.dashscope.utils.Constants;
              import com.alibaba.dashscope.utils.JsonUtils;

              public class Main {
                  // 请替换为实际的音频文件路径
                  private static final String AUDIO_FILE = "{YOUR_AUDIO_FILE}";
                  // 请替换为实际的音频文件MIME类型
                  private static final String AUDIO_MIME_TYPE = "audio/mpeg";

                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException, IOException {
                      MultiModalConversation conv = new MultiModalConversation();
                      MultiModalMessage userMessage = MultiModalMessage.builder()
                              .role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      Collections.singletonMap("audio", toDataUrl())))
                              .build();

                      Map<String, Object> asrOptions = new HashMap<>();
                      asrOptions.put("enable_itn", false);
                      // asrOptions.put("language", "zh"); // 可选，若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 新加坡/美国地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3-asr-flash")
                              .message(userMessage)
                              .parameter("asr_options", asrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(JsonUtils.toJson(result));
                  }

                  public static void main(String[] args) {
                      try {
                          // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                          Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException | IOException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }

                  // 生成 data URI
                  public static String toDataUrl() throws IOException {
                      byte[] bytes = Files.readAllBytes(Paths.get(AUDIO_FILE));
                      String encoded = Base64.getEncoder().encodeToString(bytes);
                      return "data:" + AUDIO_MIME_TYPE + ";base64," + encoded;
                  }
              }
              ```
            </CodeGroup>
          </Tab>

          <Tab title="输入内容：本地音频文件绝对路径">
            使用 DashScope SDK 处理本地音频文件时需传入文件路径。请参考下表，结合调用方式与操作系统创建对应路径。

            <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25.23%" }} /><col style={{ width: "20.6%" }} /><col style={{ width: "29.46%" }} /><col style={{ width: "24.71%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>系统</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>SDK</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>传入的文件路径</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>示例</strong></p></th></tr></thead><tbody><tr><td rowSpan={2} style={{ verticalAlign: "top" }}><p>Linux或macOS系统</p></td><td style={{ verticalAlign: "top" }}><p>Python SDK</p></td><td rowSpan={2} style={{ verticalAlign: "top" }}><p>file://\{文件的绝对路径}</p></td><td rowSpan={2} style={{ verticalAlign: "top" }}><p>file:///home/images/test.png</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Java SDK</p></td></tr><tr><td rowSpan={2} style={{ verticalAlign: "top" }}><p>Windows系统</p></td><td style={{ verticalAlign: "top" }}><p>Python SDK</p></td><td style={{ verticalAlign: "top" }}><p>file://\{文件的绝对路径}</p></td><td style={{ verticalAlign: "top" }}><p>file://D:/images/test.png</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Java SDK</p></td><td style={{ verticalAlign: "top" }}><p>file:///\{文件的绝对路径}</p></td><td style={{ verticalAlign: "top" }}><p>file:///D:/images/test.png</p></td></tr></tbody></table>

            <Tip>
              本地文件调用上限 100 QPS，不支持扩容，不适合生产环境、高并发或压测场景；如需更高并发，请将文件上传至 OSS 并通过 URL 方式调用。
            </Tip>

            <CodeGroup dropdown>
              ```python Python SDK expandable
              import os
              import dashscope

              # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
              dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

              # 请用您的本地音频的绝对路径替换 ABSOLUTE_PATH/{YOUR_AUDIO_FILE}
              audio_file_path = "file://ABSOLUTE_PATH/{YOUR_AUDIO_FILE}"

              messages = [
                  {"role": "user", "content": [{"audio": audio_file_path}]}
              ]
              response = dashscope.MultiModalConversation.call(
                  # 新加坡/美国地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  model="qwen3-asr-flash",
                  messages=messages,
                  result_format="message",
                  asr_options={
                      # "language": "zh", # 可选，若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率
                      "enable_itn":False
                  }
              )
              print(response)
              ```

              ```java Java SDK expandable
              import java.util.Arrays;
              import java.util.Collections;
              import java.util.HashMap;
              import java.util.Map;

              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.alibaba.dashscope.utils.Constants;
              import com.alibaba.dashscope.utils.JsonUtils;

              public class Main {
                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      // 请用您本地文件的绝对路径替换掉ABSOLUTE_PATH/{YOUR_AUDIO_FILE}
                      String localFilePath = "file://ABSOLUTE_PATH/{YOUR_AUDIO_FILE}";
                      MultiModalConversation conv = new MultiModalConversation();
                      MultiModalMessage userMessage = MultiModalMessage.builder()
                              .role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      Collections.singletonMap("audio", localFilePath)))
                              .build();

                      Map<String, Object> asrOptions = new HashMap<>();
                      asrOptions.put("enable_itn", false);
                      // asrOptions.put("language", "zh"); // 可选，若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 新加坡/美国地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3-asr-flash")
                              .message(userMessage)
                              .parameter("asr_options", asrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(JsonUtils.toJson(result));
                  }
                  public static void main(String[] args) {
                      try {
                          // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                          Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```
            </CodeGroup>
          </Tab>

          <Tab title="流式输出">
            <span id="qwen-asr-streaming-examples" />

            模型逐步生成中间结果，最终结果由其拼接而成。非流式调用需等待全部结果生成后一次性返回；流式调用边生成边返回，可显著降低首字延迟。根据调用方式选择对应的流式参数：

            - DashScope Python SDK方式：设置`stream`参数为true。
            - DashScope Java SDK方式：需要通过`streamCall`接口调用。
            - DashScope HTTP方式：需要在Header中指定`X-DashScope-SSE`为`enable`。

            <Tabs>
              <Tab title="Python SDK">
                ```python expandable
                import os
                import dashscope

                # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

                messages = [
                    {"role": "user", "content": [{"audio": "{YOUR_AUDIO_URL}"}]}
                ]
                response = dashscope.MultiModalConversation.call(
                    # 新加坡/美国地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx"
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    model="qwen3-asr-flash",
                    messages=messages,
                    result_format="message",
                    asr_options={
                        # "language": "zh", # 可选，若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率
                        "enable_itn":False
                    },
                    stream=True
                )

                for response in response:
                    try:
                        print(response["output"]["choices"][0]["message"].content[0]["text"])
                    except Exception as e:
                        print(f"解析响应失败：{e}")
                ```
              </Tab>

              <Tab title="Java SDK">
                ```java expandable
                import java.util.Arrays;
                import java.util.Collections;
                import java.util.HashMap;
                import java.util.Map;

                import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
                import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
                import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
                import com.alibaba.dashscope.common.MultiModalMessage;
                import com.alibaba.dashscope.common.Role;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.exception.UploadFileException;
                import com.alibaba.dashscope.utils.Constants;
                import io.reactivex.Flowable;

                public class Main {
                    public static void simpleMultiModalConversationCall()
                            throws ApiException, NoApiKeyException, UploadFileException {
                        MultiModalConversation conv = new MultiModalConversation();
                        MultiModalMessage userMessage = MultiModalMessage.builder()
                                .role(Role.USER.getValue())
                                .content(Arrays.asList(
                                        Collections.singletonMap("audio", "{YOUR_AUDIO_URL}")))
                                .build();

                        Map<String, Object> asrOptions = new HashMap<>();
                        asrOptions.put("enable_itn", false);
                        // asrOptions.put("language", "zh"); // 可选，若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率
                        MultiModalConversationParam param = MultiModalConversationParam.builder()
                                // 新加坡/美国地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .model("qwen3-asr-flash")
                                .message(userMessage)
                                .parameter("asr_options", asrOptions)
                                .build();
                        Flowable<MultiModalConversationResult> resultFlowable = conv.streamCall(param);
                        resultFlowable.blockingForEach(item -> {
                            try {
                                System.out.println(item.getOutput().getChoices().get(0).getMessage().getContent().get(0).get("text"));
                            } catch (Exception e){
                                System.out.println("解析响应失败：" + e.getMessage());
                                System.exit(1);
                            }
                        });
                    }

                    public static void main(String[] args) {
                        try {
                            // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                            simpleMultiModalConversationCall();
                        } catch (ApiException | NoApiKeyException | UploadFileException e) {
                            System.out.println(e.getMessage());
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="cURL">
                以下为华北2（北京）地域的配置，调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)，各地域的配置不同。

                ```bash
                curl -X POST "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation" \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H "Content-Type: application/json" \
                -H "X-DashScope-SSE: enable" \
                -d '{
                    "model": "qwen3-asr-flash",
                    "input": {
                        "messages": [
                            {
                                "content": [
                                    {
                                        "audio": "{YOUR_AUDIO_URL}"
                                    }
                                ],
                                "role": "user"
                            }
                        ]
                    },
                    "parameters": {
                        "incremental_output": true,
                        "asr_options": {
                            "enable_itn": false
                        }
                    }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>
        </Tabs>
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
        ### 响应参数 <span id="0e1c44902ea1e" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        > Java SDK返回参数为<strong>requestId。</strong>

        <strong>output</strong>`object`

        调用结果信息。

        <Accordion title="属性" defaultOpen>
          <strong>choices</strong>`array`

          模型的输出信息。当result\_format为message时返回choices参数。

          <Accordion title="属性">
            <strong>finish\_reason</strong>`string`

            有三种情况：

            - 正在生成时为null；
            - 因模型输出自然结束，或触发输入参数中的stop条件而结束时为stop；
            - 因生成长度过长而结束为length。

            <strong>message</strong>`object`

            模型输出的消息对象。

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`

              输出消息的角色，固定为assistant。

              <strong>content</strong>`array`

              输出消息的内容。

              <Accordion title="属性" defaultOpen>
                <strong>text</strong>`string`

                语音识别结果。
              </Accordion>

              <strong>annotations</strong>`array`

              输出标注信息（如语种）

              <Accordion title="属性" defaultOpen>
                <strong>language</strong>`string`

                被识别音频的语种。当请求参数`language`已指定语种时，该值与所指定的参数一致。

                <Accordion title="取值范围">
                  - zh：中文（普通话、四川话、闽南语、吴语）
                  - yue：粤语
                  - en：英文
                  - ja：日语
                  - de：德语
                  - ko：韩语
                  - ru：俄语
                  - fr：法语
                  - pt：葡萄牙语
                  - ar：阿拉伯语
                  - it：意大利语
                  - es：西班牙语
                  - hi：印地语
                  - id：印尼语
                  - th：泰语
                  - tr：土耳其语
                  - uk：乌克兰语
                  - vi：越南语
                  - cs：捷克语
                  - da：丹麦语
                  - fil：菲律宾语
                  - fi：芬兰语
                  - is：冰岛语
                  - ms：马来语
                  - no：挪威语
                  - pl：波兰语
                  - sv：瑞典语
                </Accordion>

                <strong>type</strong>`string`

                固定为`audio_info`，表示音频信息。

                <strong>emotion</strong>`string`

                被识别音频的情感。支持的情感如下：

                - `surprised`：惊讶
                - `neutral`：平静
                - `happy`：愉快
                - `sad`：悲伤
                - `disgusted`：厌恶
                - `angry`：愤怒
                - `fearful`：恐惧
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>usage</strong>`object`

        本次请求的Token消耗信息。

        <Accordion title="属性" defaultOpen>
          <strong>input\_tokens\_details</strong> `object`

          千问3-ASR-Flash输入内容长度（Token）。

          <Accordion title="属性">
            <strong>text\_tokens</strong> `integer`

            无需关注该参数。
          </Accordion>

          <strong>output\_tokens\_details</strong> `object`

          千问3-ASR-Flash输出内容长度（Token）。

          <Accordion title="属性">
            <strong>text\_tokens</strong> `integer`

            千问3-ASR-Flash输出的识别结果文本长度（Token）。
          </Accordion>

          <strong>seconds</strong> `integer`

          千问3-ASR-Flash音频时长（秒）。
        </Accordion>
      </td>

      <td>
        ```json expandable
        {
            "output": {
                "choices": [
                    {
                        "finish_reason": "stop",
                        "message": {
                            "annotations": [
                                {
                                    "language": "zh",
                                    "type": "audio_info",
                                    "emotion": "neutral"
                                }
                            ],
                            "content": [
                                {
                                    "text": "欢迎使用阿里云。"
                                }
                            ],
                            "role": "assistant"
                        }
                    }
                ]
            },
            "usage": {
                "input_tokens_details": {
                    "text_tokens": 0
                },
                "output_tokens_details": {
                    "text_tokens": 6
                },
                "seconds": 1
            },
            "request_id": "568e2bf0-d6f2-97f8-9f15-a57b11dc6977"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## DashScope异步调用 <span id="9937e8884002q" />

### 流程说明 <span id="4a1eca9360nd1" />

与OpenAI兼容模式或DashScope同步调用（均为一次请求、立即返回结果）不同，异步调用专为处理长音频文件或耗时较长的任务设计，该模式采用“提交-轮询”的两步式流程，避免了因长时间等待而导致的请求超时：

1. 第一步：提交任务

   - 客户端发起一个异步处理请求。
   - 服务器验证请求后，不会立即执行任务，而是返回一个唯一的 `task_id`，表示任务已成功创建。
2. 第二步：获取结果

   - 客户端使用获取到的 `task_id`，通过轮询方式反复调用结果查询接口。
   - 当任务处理完成后，结果查询接口将返回最终的识别结果。

您可以根据集成环境选择使用SDK或直接调用RESTful API。

- 使用 SDK（示例代码请参见[调用示例](/zh/model-studio/qwen-asr-api-reference#qwen-asr-async-examples)，请求参数请参见[提交任务](/zh/model-studio/qwen-asr-api-reference#88657039c4x0g)的请求参数[请求参数](/zh/model-studio/qwen-asr-api-reference#1a2369eebaueh)，返回结果请参见[异步调用识别结果说明](/zh/model-studio/qwen-asr-api-reference#2c27ad3e80p4y)）

  SDK封装了底层的API调用细节，提供了更便捷的编程体验。

  1. 提交任务：调用 `async_call()` (Python) 或 `asyncCall()` (Java) 方法提交任务。此方法将返回一个包含 `task_id` 的任务对象。
  2. 获取结果：使用上一步返回的任务对象或 `task_id`，调用 `fetch()` 方法获取结果。SDK内部会自动处理轮询逻辑，直到任务完成或超时。
- 2. 使用 RESTful API

  直接调用HTTP接口提供了最大的灵活性。

  1. [提交任务](/zh/model-studio/qwen-asr-api-reference#88657039c4x0g)，如果请求成功，响应参数[响应参数](/zh/model-studio/qwen-asr-api-reference#eca6c7d3f35hn)中将包含一个 `task_id`。
  2. 使用上一步获取的 `task_id`，[获取任务执行结果](/zh/model-studio/qwen-asr-api-reference#f9109f6ea3di2)。

### 完整示例 <span id="qwen-asr-async-examples" />

<Tabs>
  <Tab title="HTTP">
    <CodeGroup dropdown>
      ```java Java expandable
      import com.google.gson.Gson;
      import com.google.gson.annotations.SerializedName;
      import okhttp3.*;

      import java.io.IOException;
      import java.util.concurrent.TimeUnit;

      public class Main {
          // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
          private static final String API_URL_SUBMIT = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/transcription";
          // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
          private static final String API_URL_QUERY = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/";
          private static final Gson gson = new Gson();

          public static void main(String[] args) {
              // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：String apiKey = "sk-xxx"
              String apiKey = System.getenv("DASHSCOPE_API_KEY");

              OkHttpClient client = new OkHttpClient();

              // 1. 提交任务
              String payloadJson = """
                      {
                          "model": "qwen3-asr-flash-filetrans",
                          "input": {
                              "file_url": "{YOUR_AUDIO_URL}"
                          },
                          "parameters": {
                              "channel_id": [0],
                              "enable_itn": false,
                              "enable_words": true
                          }
                      }
                      """;

              RequestBody body = RequestBody.create(payloadJson, MediaType.get("application/json; charset=utf-8"));
              Request submitRequest = new Request.Builder()
                      .url(API_URL_SUBMIT)
                      .addHeader("Authorization", "Bearer " + apiKey)
                      .addHeader("Content-Type", "application/json")
                      .addHeader("X-DashScope-Async", "enable")
                      .post(body)
                      .build();

              String taskId = null;

              try (Response response = client.newCall(submitRequest).execute()) {
                  if (response.isSuccessful() && response.body() != null) {
                      String respBody = response.body().string();
                      ApiResponse apiResp = gson.fromJson(respBody, ApiResponse.class);
                      if (apiResp.output != null) {
                          taskId = apiResp.output.taskId;
                          System.out.println("任务已提交，task_id: " + taskId);
                      } else {
                          System.out.println("提交返回内容: " + respBody);
                          return;
                      }
                  } else {
                      System.out.println("任务提交失败! HTTP code: " + response.code());
                      if (response.body() != null) {
                          System.out.println(response.body().string());
                      }
                      return;
                  }
              } catch (IOException e) {
                  e.printStackTrace();
                  return;
              }

              // 2. 轮询任务状态
              boolean finished = false;
              while (!finished) {
                  try {
                      TimeUnit.SECONDS.sleep(2);  // 等待 2 秒再查询
                  } catch (InterruptedException e) {
                      Thread.currentThread().interrupt();
                      return;
                  }

                  String queryUrl = API_URL_QUERY + taskId;
                  Request queryRequest = new Request.Builder()
                          .url(queryUrl)
                          .addHeader("Authorization", "Bearer " + apiKey)
                          .addHeader("Content-Type", "application/json")
                          .get()
                          .build();

                  try (Response response = client.newCall(queryRequest).execute()) {
                      if (response.body() != null) {
                          String queryResponse = response.body().string();
                          ApiResponse apiResp = gson.fromJson(queryResponse, ApiResponse.class);

                          if (apiResp.output != null && apiResp.output.taskStatus != null) {
                              String status = apiResp.output.taskStatus;
                              System.out.println("当前任务状态: " + status);
                              if ("SUCCEEDED".equalsIgnoreCase(status)
                                      || "FAILED".equalsIgnoreCase(status)
                                      || "UNKNOWN".equalsIgnoreCase(status)) {
                                  finished = true;
                                  System.out.println("任务完成，最终结果: ");
                                  System.out.println(queryResponse);
                              }
                          } else {
                              System.out.println("查询返回内容: " + queryResponse);
                          }
                      }
                  } catch (IOException e) {
                      e.printStackTrace();
                      return;
                  }
              }
          }

          static class ApiResponse {
              @SerializedName("request_id")
              String requestId;
              Output output;
          }

          static class Output {
              @SerializedName("task_id")
              String taskId;
              @SerializedName("task_status")
              String taskStatus;
          }
      }
      ```

      ```python Python expandable
      import os
      import time
      import requests
      import json

      # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
      API_URL_SUBMIT = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/transcription"
      # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
      API_URL_QUERY_BASE = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/"

      def main():
          # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
          # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx"
          api_key = os.getenv("DASHSCOPE_API_KEY")

          headers = {
              "Authorization": f"Bearer {api_key}",
              "Content-Type": "application/json",
              "X-DashScope-Async": "enable"
          }

          # 1. 提交任务
          payload = {
              "model": "qwen3-asr-flash-filetrans",
              "input": {
                  "file_url": "{YOUR_AUDIO_URL}"
              },
              "parameters": {
                  "channel_id": [0],
                  # "language": "zh",
                  "enable_itn": False,
                  "enable_words": True
              }
          }

          print("提交 ASR 转写任务...")
          try:
              submit_resp = requests.post(API_URL_SUBMIT, headers=headers, data=json.dumps(payload))
          except requests.RequestException as e:
              print(f"请求提交任务失败: {e}")
              return

          if submit_resp.status_code != 200:
              print(f"任务提交失败! HTTP code: {submit_resp.status_code}")
              print(submit_resp.text)
              return

          resp_data = submit_resp.json()
          output = resp_data.get("output")
          if not output or "task_id" not in output:
              print("提交返回内容异常:", resp_data)
              return

          task_id = output["task_id"]
          print(f"任务已提交，task_id: {task_id}")

          # 2. 轮询任务状态
          finished = False
          while not finished:
              time.sleep(2)  # 等待 2 秒再查询

              query_url = API_URL_QUERY_BASE + task_id
              try:
                  query_resp = requests.get(query_url, headers={"Authorization": f"Bearer {api_key}"})
              except requests.RequestException as e:
                  print(f"请求查询任务失败: {e}")
                  return

              if query_resp.status_code != 200:
                  print(f"查询任务失败! HTTP code: {query_resp.status_code}")
                  print(query_resp.text)
                  return

              query_data = query_resp.json()
              output = query_data.get("output")
              if output and "task_status" in output:
                  status = output["task_status"]
                  print(f"当前任务状态: {status}")

                  if status.upper() in ("SUCCEEDED", "FAILED", "UNKNOWN"):
                      finished = True
                      print("任务完成，最终结果如下：")
                      print(json.dumps(query_data, indent=2, ensure_ascii=False))
              else:
                  print("查询返回内容:", query_data)

      if __name__ == "__main__":
          main()
      ```
    </CodeGroup>
  </Tab>

  <Tab title="Java SDK">
    ```java expandable
    import com.alibaba.dashscope.audio.qwen_asr.*;
    import com.alibaba.dashscope.utils.Constants;
    import com.google.gson.Gson;
    import com.google.gson.GsonBuilder;
    import com.google.gson.JsonObject;

    import java.io.BufferedReader;
    import java.io.InputStreamReader;
    import java.net.HttpURLConnection;
    import java.net.URL;
    import java.util.ArrayList;
    import java.util.HashMap;

    public class Main {
        public static void main(String[] args) {
            // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
            QwenTranscriptionParam param =
                    QwenTranscriptionParam.builder()
                            // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                            // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                            .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                            .model("qwen3-asr-flash-filetrans")
                            .fileUrl("{YOUR_AUDIO_URL}")
                            //.parameter("language", "zh")
                            //.parameter("channel_id", new ArrayList<String>(){{add("0");add("1");}})
                            .parameter("enable_itn", false)
                            .parameter("enable_words", true)
                            .build();
            try {
                QwenTranscription transcription = new QwenTranscription();
                // 提交任务
                QwenTranscriptionResult result = transcription.asyncCall(param);
                System.out.println("create task result: " + result);
                // 检查任务是否提交成功
                if (result.getTaskId() == null) {
                    System.out.println("Error: " + result.getOutput());
                    return;
                }
                // 查询任务状态
                result = transcription.fetch(QwenTranscriptionQueryParam.FromTranscriptionParam(param, result.getTaskId()));
                System.out.println("task status: " + result);
                // 等待任务完成
                result =
                        transcription.wait(
                                QwenTranscriptionQueryParam.FromTranscriptionParam(param, result.getTaskId()));
                System.out.println("task result: " + result);
                // 获取语音识别结果
                QwenTranscriptionTaskResult taskResult = result.getResult();
                if (taskResult != null) {
                    // 获取识别结果的url
                    String transcriptionUrl = taskResult.getTranscriptionUrl();
                    // 获取url内对应的结果
                    HttpURLConnection connection =
                            (HttpURLConnection) new URL(transcriptionUrl).openConnection();
                    connection.setRequestMethod("GET");
                    connection.connect();
                    BufferedReader reader =
                            new BufferedReader(new InputStreamReader(connection.getInputStream()));
                    // 格式化输出json结果
                    Gson gson = new GsonBuilder().setPrettyPrinting().create();
                    System.out.println(gson.toJson(gson.fromJson(reader, JsonObject.class)));
                }
            } catch (Exception e) {
                System.out.println("error: " + e);
            }
        }
    }
    ```
  </Tab>

  <Tab title="Python SDK">
    ```python expandable
    import json
    import os
    import sys
    from http import HTTPStatus

    import dashscope
    from dashscope.audio.qwen_asr import QwenTranscription
    from dashscope.api_entities.dashscope_response import TranscriptionResponse

    # run the transcription script
    if __name__ == '__main__':
        # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：dashscope.api_key = "sk-xxx"
        dashscope.api_key = os.getenv("DASHSCOPE_API_KEY")

        # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
        dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'
        task_response = QwenTranscription.async_call(
            model='qwen3-asr-flash-filetrans',
            file_url='{YOUR_AUDIO_URL}',
            #language="",
            enable_itn=False,
            enable_words=True
        )
        print(f'task_response: {task_response}')
        print(task_response.output.task_id)
        query_response = QwenTranscription.fetch(task=task_response.output.task_id)
        print(f'query_response: {query_response}')
        task_result = QwenTranscription.wait(task=task_response.output.task_id)
        print(f'task_result: {task_result}')
    ```
  </Tab>

  <Tab title="下载识别结果">
    任务成功后，查询接口返回的 `output.result.transcription_url` 指向公网可下载的 JSON 文件，包含完整识别结果。该 URL 默认在 <strong>24 小时</strong>内有效，请及时下载并落盘保存。

    ```bash
    # 将 {transcription_url} 替换为查询接口返回的 transcription_url 值
    curl -sS '{transcription_url}' -o transcription.json
    cat transcription.json | jq .
    ```
  </Tab>
</Tabs>

### 提交任务 <span id="88657039c4x0g" />

#### URL <span id="8ebdb3a43eq9j" />

<Tabs>
  <Tab title="华北2（北京）">
    HTTP请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/transcription`

    SDK调用配置的base\_url：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    HTTP请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/audio/asr/transcription`

    SDK调用配置的base\_url：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="1a2369eebaueh" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        [模型](/zh/model-studio/non-realtime-speech-recognition-user-guide)名称。仅适用于千问3-ASR-Flash-Filetrans模型。

        <strong>input</strong>`object`<strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>file\_url</strong> `string`<strong>（必选）</strong>

          待识别音频文件URL，URL必须公网可访问。

          使用SDK时，若录音文件存储在[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/simple-upload#a632b50f190j8)，不支持使用以 `oss://`为前缀的临时 URL。

          使用RESTful API时，若录音文件存储在[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/simple-upload#a632b50f190j8)，支持使用以 `oss://`为前缀的临时 URL。但需注意：

          <Tip>
            - 临时 URL 有效期48小时，过期后无法使用，<strong>请勿用于生产环境。</strong>
            - 文件上传凭证接口限流为 100 QPS 且不支持扩容，<strong>请勿用于生产环境、高并发及压测场景。</strong>
            - 生产环境建议使用[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss) 等稳定存储，确保文件长期可用并规避限流问题。
          </Tip>
        </Accordion>

        <strong>parameters</strong>`object`（可选）

        <Accordion title="属性" defaultOpen>
          <strong>language</strong> *string*（可选）无默认值

          若已知音频的语种，可通过该参数指定待识别语种，以提升识别准确率。

          只能指定一个语种。

          若音频语种不确定，或包含多种语种（例如中英日韩混合），请勿指定该参数。

          <Accordion title="取值范围">
            - zh：中文（普通话、四川话、闽南语、吴语）
            - yue：粤语
            - en：英文
            - ja：日语
            - de：德语
            - ko：韩语
            - ru：俄语
            - fr：法语
            - pt：葡萄牙语
            - ar：阿拉伯语
            - it：意大利语
            - es：西班牙语
            - hi：印地语
            - id：印尼语
            - th：泰语
            - tr：土耳其语
            - uk：乌克兰语
            - vi：越南语
            - cs：捷克语
            - da：丹麦语
            - fil：菲律宾语
            - fi：芬兰语
            - is：冰岛语
            - ms：马来语
            - no：挪威语
            - pl：波兰语
            - sv：瑞典语
          </Accordion>

          <strong>enable\_itn</strong>`boolean`（可选）默认值为`false`

          是否启用ITN（Inverse Text Normalization，逆文本标准化）。该功能仅适用于中文和英文音频。

          开启后，语音识别结果中的中文数字（如"一百二十三"）或英文数字（如"one hundred"）将自动转换为阿拉伯数字（如"123"）。

          参数值：

          - true：开启；
          - false：关闭。

          <strong>enable\_words</strong>`boolean`（可选）默认值为`false`

          控制是否返回字级别时间戳：

          - `false`：返回句级时间戳
          - `true`：返回字级时间戳

            字级别时间戳仅支持以下语种：中文、英语、日语、韩语、德语、法语、西班牙语、意大利语、葡萄牙语、俄语，其他语种可能无法保证准确性

          同时，该参数还影响断句规则：

          - `false`：基于 VAD（语音活动检测）断句
          - `true`：基于 VAD + 标点符号断句

          <strong>channel\_id</strong>`array`（可选）默认值为`[0]`

          指定在多音轨音频文件中需要识别的音轨索引，索引从 0 开始。例如，\[0] 表示识别第一个音轨，\[0, 1] 表示同时识别第一和第二个音轨。如果省略此参数，则默认处理第一个音轨。

          <Tip>
            指定的每一个音轨都将独立计费。例如，为单个文件请求 \[0, 1] 会产生两笔独立的费用。
          </Tip>
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="cURL">
            ```bash
            # ======= 重要提示 =======
            # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
            # 新加坡地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
            # === 执行时请删除该注释 ===

            curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/transcription' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header "Content-Type: application/json" \
            --header "X-DashScope-Async: enable" \
            --data '{
                "model": "qwen3-asr-flash-filetrans",
                "input": {
                    "file_url": "{YOUR_AUDIO_URL}"
                },
                "parameters": {
                    "channel_id":[
                        0
                    ],
                    "enable_itn": false
                }
            }'
            ```
          </Tab>

          <Tab title="Java">
            SDK示例请参见[调用示例](/zh/model-studio/qwen-asr-api-reference#qwen-asr-async-examples)。

            ```java expandable
            import com.google.gson.Gson;
            import com.google.gson.annotations.SerializedName;
            import okhttp3.*;

            import java.io.IOException;

            public class Main {
                // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                private static final String API_URL = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/transcription";

                public static void main(String[] args) {
                    // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                    // 若没有配置环境变量，请用百炼API Key将下行替换为：String apiKey = "sk-xxx"
                    String apiKey = System.getenv("DASHSCOPE_API_KEY");

                    OkHttpClient client = new OkHttpClient();
                    Gson gson = new Gson();

                    /*String payloadJson = """
                            {
                                "model": "qwen3-asr-flash-filetrans",
                                "input": {
                                    "file_url": "{YOUR_AUDIO_URL}"
                                },
                                "parameters": {
                                    "channel_id": [0],
                                    "enable_itn": false,
                                    "language": "zh",
                                    "corpus": {
                                        "text": ""
                                    }
                                }
                            }
                            """;*/
                    String payloadJson = """
                            {
                                "model": "qwen3-asr-flash-filetrans",
                                "input": {
                                    "file_url": "{YOUR_AUDIO_URL}"
                                },
                                "parameters": {
                                    "channel_id": [0],
                                    "enable_itn": false
                                }
                            }
                            """;

                    RequestBody body = RequestBody.create(payloadJson, MediaType.get("application/json; charset=utf-8"));
                    Request request = new Request.Builder()
                            .url(API_URL)
                            .addHeader("Authorization", "Bearer " + apiKey)
                            .addHeader("Content-Type", "application/json")
                            .addHeader("X-DashScope-Async", "enable")
                            .post(body)
                            .build();

                    try (Response response = client.newCall(request).execute()) {
                        if (response.isSuccessful() && response.body() != null) {
                            String respBody = response.body().string();
                            // 用 Gson 解析 JSON
                            ApiResponse apiResp = gson.fromJson(respBody, ApiResponse.class);
                            if (apiResp.output != null) {
                                System.out.println("task_id: " + apiResp.output.taskId);
                            } else {
                                System.out.println(respBody);
                            }
                        } else {
                            System.out.println("task failed! HTTP code: " + response.code());
                            if (response.body() != null) {
                                System.out.println(response.body().string());
                            }
                        }
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }

                static class ApiResponse {
                    @SerializedName("request_id")
                    String requestId;

                    Output output;
                }

                static class Output {
                    @SerializedName("task_id")
                    String taskId;

                    @SerializedName("task_status")
                    String taskStatus;
                }
            }
            ```
          </Tab>

          <Tab title="Python">
            SDK示例请参见[调用示例](/zh/model-studio/qwen-asr-api-reference#qwen-asr-async-examples)。

            ```python expandable
            import requests
            import json
            import os

            # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
            url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/transcription"

            # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
            # 若没有配置环境变量，请用百炼API Key将下行替换为：DASHSCOPE_API_KEY = "sk-xxx"
            DASHSCOPE_API_KEY = os.getenv("DASHSCOPE_API_KEY")

            headers = {
                "Authorization": f"Bearer {DASHSCOPE_API_KEY}",
                "Content-Type": "application/json",
                "X-DashScope-Async": "enable"
            }

            payload = {
                "model": "qwen3-asr-flash-filetrans",
                "input": {
                    "file_url": "{YOUR_AUDIO_URL}"
                },
                "parameters": {
                    "channel_id": [0],
                    # "language": "zh",
                    "enable_itn": False
                    # "corpus": {
                    #     "text": ""
                    # }
                }
            }

            response = requests.post(url, headers=headers, data=json.dumps(payload))
            if response.status_code == 200:
                print(f"task_id: {response.json()["output"]["task_id"]}")
            else:
                print("task failed!")
                print(response.json())
            ```
          </Tab>
        </Tabs>
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
        #### 响应参数 <span id="eca6c7d3f35hn" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        调用结果信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          任务ID。该ID在查询语音识别任务接口中作为请求参数传入。

          <strong>task\_status</strong>`string`

          任务状态：

          - PENDING：任务排队中
          - RUNNING：任务处理中
          - SUCCEEDED：任务执行成功
          - FAILED：任务执行失败
          - UNKNOWN：任务不存在或状态未知
        </Accordion>
      </td>

      <td>
        ```json
        {
            "request_id": "92e3decd-0c69-47a8-************",
            "output": {
                "task_id": "8fab76d0-0eed-4d20-************",
                "task_status": "PENDING"
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

### 获取任务执行结果 <span id="f9109f6ea3di2" />

#### URL <span id="03eb4a771btn0" />

<Tabs>
  <Tab title="华北2（北京）">
    HTTP请求地址：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    SDK调用配置的base\_url：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    HTTP请求地址：`GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    SDK调用配置的base\_url：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="6cd669c353c2i" />

        <strong>task\_id</strong>`string`<strong>（必选）</strong>

        任务ID。将[提交任务](/zh/model-studio/qwen-asr-api-reference#88657039c4x0g)返回结果中的task\_id作为参数传入，查询语音识别结果。
      </td>

      <td>
        <Tabs>
          <Tab title="cURL">
            ```bash
            # ======= 重要提示 =======
            # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
            # 新加坡地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
            # === 执行时请删除该注释 ===

            curl --location --request GET 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header "Content-Type: application/json"
            ```
          </Tab>

          <Tab title="Java">
            SDK示例请参见[调用示例](/zh/model-studio/qwen-asr-api-reference#qwen-asr-async-examples)。

            ```java expandable
            import okhttp3.*;

            import java.io.IOException;

            public class Main {
                public static void main(String[] args) {
                    // 替换为实际的task_id
                    String taskId = "xxx";
                    // 新加坡地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                    // 若没有配置环境变量，请用百炼API Key将下行替换为：String apiKey = "sk-xxx"
                    String apiKey = System.getenv("DASHSCOPE_API_KEY");

                    // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                    String apiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/" + taskId;

                    OkHttpClient client = new OkHttpClient();

                    Request request = new Request.Builder()
                            .url(apiUrl)
                            .addHeader("Authorization", "Bearer " + apiKey)
                            .addHeader("Content-Type", "application/json")
                            .get()
                            .build();

                    try (Response response = client.newCall(request).execute()) {
                        if (response.body() != null) {
                            System.out.println(response.body().string());
                        }
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
            ```
          </Tab>

          <Tab title="Python">
            SDK示例请参见[调用示例](/zh/model-studio/qwen-asr-api-reference#qwen-asr-async-examples)。

            ```python
            import os
            import requests

            # 新加坡地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
            # 若没有配置环境变量，请用百炼API Key将下行替换为：DASHSCOPE_API_KEY = "sk-xxx"
            DASHSCOPE_API_KEY = os.getenv("DASHSCOPE_API_KEY")

            # 替换为实际的task_id
            task_id = "xxx"
            # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
            url = f"https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}"

            headers = {
                "Authorization": f"Bearer {DASHSCOPE_API_KEY}",
                "Content-Type": "application/json"
            }

            response = requests.get(url, headers=headers)
            print(response.json())
            ```
          </Tab>
        </Tabs>
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
        #### 响应参数 <span id="b207932046i9z" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        调用结果信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          任务ID。该ID在查询语音识别任务接口中作为请求参数传入。

          <strong>task\_status</strong>`string`

          任务状态：

          - PENDING：任务排队中
          - RUNNING：任务处理中
          - SUCCEEDED：任务执行成功
          - FAILED：任务执行失败
          - UNKNOWN：任务不存在或状态未知

          <strong>result</strong>`object`

          语音识别结果。

          <Accordion title="属性">
            <strong>transcription\_url</strong>`string`

            识别结果文件的下载 URL，链接有效期为 24 小时。过期后无法查询任务，也无法通过先前的 URL 下载结果。<br />识别结果以 JSON 文件保存，可通过该链接下载文件，或直接使用 HTTP 请求读取文件内容。<br />

            详情参见[异步调用识别结果说明](/zh/model-studio/qwen-asr-api-reference#2c27ad3e80p4y)。
          </Accordion>

          <strong>submit\_time</strong>`string`

          任务提交时间。

          <strong>schedule\_time</strong>`string`

          任务调度时间，即开始执行时间。

          <strong>end\_time</strong>`string`

          任务结束时间。

          <strong>task\_metrics</strong>`object`

          任务指标，包含子任务状态的统计信息。

          <Accordion title="属性">
            <strong>TOTAL</strong>`integer`

            子任务总数。

            <strong>SUCCEEDED</strong>`integer`

            子任务成功数。

            <strong>FAILED</strong>`integer`

            子任务失败数。
          </Accordion>

          <strong>code</strong>`string`

          错误码，仅在任务失败时返回。

          <strong>message</strong>`string`

          错误信息，仅任务失败时返回。

          <strong>usage</strong>`object`

          本次请求的Token消耗信息。

          <Accordion title="属性">
            <strong>seconds</strong> `integer`

            千问3-ASR-Flash音频时长（秒）。
          </Accordion>
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json RUNNING
          {
              "request_id": "6769df07-2768-4fb0-ad59-************",
              "output": {
                  "task_id": "9be1700a-0f8e-4778-be74-************",
                  "task_status": "RUNNING",
                  "submit_time": "2025-10-27 14:19:31.150",
                  "scheduled_time": "2025-10-27 14:19:31.233",
                  "task_metrics": {
                      "TOTAL": 1,
                      "SUCCEEDED": 0,
                      "FAILED": 0
                  }
              }
          }
          ```

          ```json SUCCEEDED
          {
              "request_id": "1dca6c0a-0ed1-4662-aa39-************",
              "output": {
                  "task_id": "8fab76d0-0eed-4d20-929f-************",
                  "task_status": "SUCCEEDED",
                  "submit_time": "2025-10-27 13:57:45.948",
                  "scheduled_time": "2025-10-27 13:57:46.018",
                  "end_time": "2025-10-27 13:57:47.079",
                  "result": {
                      "transcription_url": "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/pre/pre-funasr-mlt-v1/20251027/13%3A57/7a3a8236-ffd1-4099-a280-0299686ac7da.json?Expires=1761631066&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE&response-content-disposition=attachment%3Bfilename%3D7a3a8236-ffd1-4099-a280-0299686ac7da.json"
                  }
              },
              "usage": {
                  "seconds": 3
              }
          }
          ```

          ```json FAILED
          {
              "request_id": "3d141841-858a-466a-9ff9-************",
              "output": {
                  "task_id": "c58c7951-7789-4557-9ea3-************",
                  "task_status": "FAILED",
                  "submit_time": "2025-10-27 15:06:06.915",
                  "scheduled_time": "2025-10-27 15:06:06.967",
                  "end_time": "2025-10-27 15:06:07.584",
                  "code": "FILE_403_FORBIDDEN",
                  "message": "FILE_403_FORBIDDEN"
              }
          }
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
        ### 异步调用识别结果说明 <span id="2c27ad3e80p4y" />

        <strong>file\_url</strong> `string`

        被识别的音频文件URL。

        <strong>audio\_info</strong>`object`

        被识别音频文件相关信息。

        <Accordion title="属性" defaultOpen>
          <strong>format</strong> `string`

          音频格式。

          <strong>sample\_rate</strong> `integer`

          音频采样率。
        </Accordion>

        <strong>transcripts</strong>`array`

        完整的识别结果列表，每个元素对应一条音轨的识别内容。

        <Accordion title="属性" defaultOpen>
          <strong>channel\_id</strong>`integer`

          音轨索引，以0为起始。

          <strong>text</strong>`string`

          识别结果文本。

          <strong>sentences</strong>`object`

          句子级别的识别结果列表。

          <Accordion title="属性" defaultOpen>
            <strong>begin\_time</strong>`integer`

            句子开始时间戳（毫秒）。

            <strong>end\_time</strong>`integer`

            句子结束时间戳（毫秒）。

            <strong>text</strong>`string`

            识别结果文本。

            <strong>sentence\_id</strong>`integer`

            句子索引，以0为起始。

            <strong>language</strong>`string`

            被识别音频的语种。当请求参数`language`已指定语种时，该值与所指定的参数一致。

            <Accordion title="取值范围">
              - zh：中文（普通话、四川话、闽南语、吴语）
              - yue：粤语
              - en：英文
              - ja：日语
              - de：德语
              - ko：韩语
              - ru：俄语
              - fr：法语
              - pt：葡萄牙语
              - ar：阿拉伯语
              - it：意大利语
              - es：西班牙语
              - hi：印地语
              - id：印尼语
              - th：泰语
              - tr：土耳其语
              - uk：乌克兰语
              - vi：越南语
              - cs：捷克语
              - da：丹麦语
              - fil：菲律宾语
              - fi：芬兰语
              - is：冰岛语
              - ms：马来语
              - no：挪威语
              - pl：波兰语
              - sv：瑞典语
            </Accordion>

            <strong>emotion</strong>`string`

            被识别音频的情感。支持的情感如下：

            - `surprised`：惊讶
            - `neutral`：平静
            - `happy`：愉快
            - `sad`：悲伤
            - `disgusted`：厌恶
            - `angry`：愤怒
            - `fearful`：恐惧

            <strong>words</strong>`object`

            词级别的识别结果列表。当请求参数`enable_words`设为`true`时展示该结果。

            <Accordion title="属性" defaultOpen>
              <strong>begin\_time</strong>`integer`

              开始时间戳（毫秒）。

              <strong>end\_time</strong>`integer`

              结束时间戳（毫秒）。

              <strong>text</strong>`string`

              识别结果文本。

              <strong>punctuation</strong>`string`

              标点符号。
            </Accordion>
          </Accordion>
        </Accordion>
      </td>

      <td>
        ```json expandable
        {
            "file_url": "https://***.mp3",
            "audio_info": {
                "format": "mp3",
                "sample_rate": 22050
            },
            "transcripts": [
                {
                    "channel_id": 0,
                    "text": "欢迎使用阿里云。",
                    "sentences": [
                        {
                            "sentence_id": 0,
                            "begin_time": 0,
                            "end_time": 1440,
                            "language": "zh",
                            "emotion": "neutral",
                            "text": "欢迎使用阿里云。",
                            "words": [
                                {
                                    "begin_time": 0,
                                    "end_time": 160,
                                    "text": "欢",
                                    "punctuation": ""
                                },
                                {
                                    "begin_time": 160,
                                    "end_time": 320,
                                    "text": "迎",
                                    "punctuation": ""
                                },
                                {
                                    "begin_time": 320,
                                    "end_time": 640,
                                    "text": "使",
                                    "punctuation": ""
                                },
                                {
                                    "begin_time": 640,
                                    "end_time": 720,
                                    "text": "用",
                                    "punctuation": ""
                                },
                                {
                                    "begin_time": 880,
                                    "end_time": 960,
                                    "text": "阿",
                                    "punctuation": ""
                                },
                                {
                                    "begin_time": 1040,
                                    "end_time": 1120,
                                    "text": "里",
                                    "punctuation": ""
                                },
                                {
                                    "begin_time": 1120,
                                    "end_time": 1440,
                                    "text": "云",
                                    "punctuation": "。"
                                }
                            ]
                        }
                    ]
                }
            ]
        }
        ```
      </td>
    </tr>
  </tbody>
</table>
