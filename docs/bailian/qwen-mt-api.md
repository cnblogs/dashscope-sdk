> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-MT API参考

> 本文介绍通过OpenAI兼容接口 或 DashScopeAPI 调用 Qwen-MT 模型的输入与输出参数。

<meta property="og:title" content="Qwen-MT API参考" />

> 相关文档： [翻译能力（Qwen-MT）](/zh/model-studio/machine-translation)

## OpenAI 兼容 <span id="d397bcc41eu3q" />

<Tabs>
  <Tab title="北京地域">
    SDK 调用配置的`base_url`为：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="新加坡地域">
    SDK 调用配置的`base_url`为：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="美国（弗吉尼亚）地域">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡、中国香港地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

> 您需要已 [获取与配置 API Key](/zh/model-studio/get-api-key) 并 [配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables) 。若通过OpenAI SDK进行调用，需要 [安装SDK](/zh/model-studio/install-sdk) 。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "57.57%" }} />

    <col style={{ width: "42.43%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="afcd41d2b57zf" />
      </td>

      <td rowSpan={12}>
        <Tabs>
          <Tab title="基础使用">
            <Tabs>
              <Tab title="Python">
                ```python expandable
                import os
                from openai import OpenAI

                client = OpenAI(
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    # 以下是北京地域的base_url
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                )
                messages = [
                    {
                        "role": "user",
                        "content": "我看到这个视频后没有笑"
                    }
                ]
                translation_options = {
                    "source_lang": "Chinese",
                    "target_lang": "English"
                }

                completion = client.chat.completions.create(
                    model="qwen-mt-plus",
                    messages=messages,
                    extra_body={
                        "translation_options": translation_options
                    }
                )
                print(completion.choices[0].message.content)
                ```
              </Tab>

              <Tab title="Node.js">
                ```javascript
                // 需要 Node.js v18+，需在 ES Module 环境下运行
                import OpenAI from "openai";

                const openai = new OpenAI(
                    {
                        // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey: "sk-xxx",
                        apiKey: process.env.DASHSCOPE_API_KEY,
                        // 以下是北京地域的base_url
                        baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                    }
                );
                const completion = await openai.chat.completions.create({
                    model: "qwen-mt-plus",
                    messages: [
                        { role: "user", content: "我看到这个视频后没有笑" }
                    ],
                    translation_options: {
                        source_lang: "Chinese",
                        target_lang: "English"
                    }
                });
                console.log(JSON.stringify(completion));
                ```
              </Tab>

              <Tab title="curl">
                各地域的[请求地址](/zh/model-studio/qwen-mt-api)和API Key不同，以下是北京地域的请求地址。

                ```bash
                curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H "Content-Type: application/json" \
                -d '{
                    "model": "qwen-mt-plus",
                    "messages": [{"role": "user", "content": "看完这个视频我没有笑"}],
                    "translation_options": {
                      "source_lang": "auto",
                      "target_lang": "English"
                      }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="术语干预">
            <Tabs>
              <Tab title="Python">
                ```python expandable
                import os
                from openai import OpenAI

                client = OpenAI(
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    # 以下是北京地域的base_url
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                )
                messages = [
                    {
                        "role": "user",
                        "content": "而这套生物传感器运用了石墨烯这种新型材料，它的目标物是化学元素，敏锐的“嗅觉”让它能更深度、准确地体现身体健康状况。"
                    }
                ]
                translation_options = {
                    "source_lang": "Chinese",
                    "target_lang": "English",
                    "terms": [
                        {
                            "source": "生物传感器",
                            "target": "biological sensor"
                        },
                        {
                            "source": "石墨烯",
                            "target": "graphene"
                        },
                        {
                            "source": "化学元素",
                            "target": "chemical elements"
                        },
                        {
                            "source": "身体健康状况",
                            "target": "health status of the body"
                        }
                    ]
                }

                completion = client.chat.completions.create(
                    model="qwen-mt-plus",
                    messages=messages,
                    extra_body={
                        "translation_options": translation_options
                    }
                )
                print(completion.choices[0].message.content)
                ```
              </Tab>

              <Tab title="Node.js">
                ```javascript expandable
                // 需要 Node.js v18+，需在 ES Module 环境下运行
                import OpenAI from "openai";

                const openai = new OpenAI(
                    {
                        // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey: "sk-xxx",
                        apiKey: process.env.DASHSCOPE_API_KEY,
                        // 以下是北京地域的base_url
                        baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                    }
                );
                const completion = await openai.chat.completions.create({
                    model: "qwen-mt-plus",
                    messages: [
                        { role: "user", content: "而这套生物传感器运用了石墨烯这种新型材料，它的目标物是化学元素，敏锐的“嗅觉”让它能更深度、准确地体现身体健康状况。" }
                    ],
                    translation_options: {
                        source_lang: "Chinese",
                        target_lang: "English",
                        terms: [
                            {
                                "source": "生物传感器",
                                "target": "biological sensor"
                            },
                            {
                                "source": "石墨烯",
                                "target": "graphene"
                            },
                            {
                                "source": "化学元素",
                                "target": "chemical elements"
                            },
                            {
                                "source": "身体健康状况",
                                "target": "health status of the body"
                            }
                        ]
                    }
                });
                console.log(JSON.stringify(completion));
                ```
              </Tab>

              <Tab title="curl">
                各地域的[请求地址](/zh/model-studio/qwen-mt-api)和API Key不同，以下是北京地域的请求地址。

                ```bash expandable
                curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H "Content-Type: application/json" \
                -d '{
                  "model": "qwen-mt-plus",
                  "messages": [
                    {
                      "role": "user",
                      "content": "而这套生物传感器运用了石墨烯这种新型材料，它的目标物是化学元素，敏锐的“嗅觉”让它能更深度、准确地体现身体健康状况。"
                    }
                  ],
                  "translation_options": {
                    "source_lang": "Chinese",
                    "target_lang": "English",
                    "terms": [
                      {
                        "source": "生物传感器",
                        "target": "biological sensor"
                      },
                      {
                        "source": "石墨烯",
                        "target": "graphene"
                      },
                      {
                        "source": "化学元素",
                        "target": "chemical elements"
                      },
                      {
                        "source": "身体健康状况",
                        "target": "health status of the body"
                      }
                    ]
                  }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="翻译记忆">
            <Tabs>
              <Tab title="Python">
                ```python expandable
                import os
                from openai import OpenAI

                client = OpenAI(
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    # 以下是北京地域的base_url
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                )
                messages = [
                    {
                        "role": "user",
                        "content": "通过如下命令可以看出安装thrift的版本信息；"
                    }
                ]
                translation_options = {
                    "source_lang": "Chinese",
                    "target_lang": "English",
                    "tm_list": [
                        {
                            "source": "您可以通过如下方式查看集群的内核版本信息:",
                            "target": "You can use one of the following methods to query the engine version of a cluster:"
                        },
                        {
                            "source": "我们云HBase的thrift环境是0.9.0,所以建议客户端的版本也为 0.9.0,可以从这里下载thrift的0.9.0 版本,下载的源码包我们后面会用到,这里需要先安装thrift编译环境,对于源码安装可以参考thrift官网;",
                            "target": "The version of Thrift used by ApsaraDB for HBase is 0.9.0. Therefore, we recommend that you use Thrift 0.9.0 to create a client. Click here to download Thrift 0.9.0. The downloaded source code package will be used later. You must install the Thrift compiling environment first. For more information, see Thrift official website."
                        },
                        {
                            "source": "您可以通过PyPI来安装SDK,安装命令如下:",
                            "target": "You can run the following command in Python Package Index (PyPI) to install Elastic Container Instance SDK for Python:"
                        }
                    ]
                }

                completion = client.chat.completions.create(
                    model="qwen-mt-plus",
                    messages=messages,
                    extra_body={
                        "translation_options": translation_options
                    }
                )
                print(completion.choices[0].message.content)
                ```
              </Tab>

              <Tab title="Node.js">
                ```javascript expandable
                // 需要 Node.js v18+，需在 ES Module 环境下运行
                import OpenAI from "openai";

                const openai = new OpenAI(
                    {
                        // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey: "sk-xxx",
                        apiKey: process.env.DASHSCOPE_API_KEY,
                        // 以下是北京地域的base_url
                        baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                    }
                );
                const completion = await openai.chat.completions.create({
                    model: "qwen-mt-plus",
                    messages: [
                        { role: "user", content: "通过如下命令可以看出安装thrift的版本信息；" }
                    ],
                    translation_options: {
                        source_lang: "Chinese",
                        target_lang: "English",
                        tm_list: [
                            {
                                "source": "您可以通过如下方式查看集群的内核版本信息:",
                                "target": "You can use one of the following methods to query the engine version of a cluster:"
                            },
                            {
                                "source": "我们云HBase的thrift环境是0.9.0,所以建议客户端的版本也为 0.9.0,可以从这里下载thrift的0.9.0 版本,下载的源码包我们后面会用到,这里需要先安装thrift编译环境,对于源码安装可以参考thrift官网;",
                                "target": "The version of Thrift used by ApsaraDB for HBase is 0.9.0. Therefore, we recommend that you use Thrift 0.9.0 to create a client. Click here to download Thrift 0.9.0. The downloaded source code package will be used later. You must install the Thrift compiling environment first. For more information, see Thrift official website."
                            },
                            {
                                "source": "您可以通过PyPI来安装SDK,安装命令如下:",
                                "target": "You can run the following command in Python Package Index (PyPI) to install Elastic Container Instance SDK for Python:"
                            }
                        ]
                    }
                });
                console.log(JSON.stringify(completion));
                ```
              </Tab>

              <Tab title="curl">
                各地域的[请求地址](/zh/model-studio/qwen-mt-api)和API Key不同，以下是北京地域的请求地址。

                ```bash
                curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H "Content-Type: application/json" \
                -d '{
                  "model": "qwen-mt-plus",
                  "messages": [
                    {
                      "role": "user",
                      "content": "通过如下命令可以看出安装thrift的版本信息；"
                    }
                  ],
                  "translation_options": {
                    "source_lang": "Chinese",
                    "target_lang": "English",
                    "tm_list":[
                          {"source": "您可以通过如下方式查看集群的内核版本信息:", "target": "You can use one of the following methods to query the engine version of a cluster:"},
                          {"source": "我们云HBase的thrift环境是0.9.0,所以建议客户端的版本也为 0.9.0,可以从这里下载thrift的0.9.0 版本,下载的源码包我们后面会用到,这里需要先安装thrift编译环境,对于源码安装可以参考thrift官网;", "target": "The version of Thrift used by ApsaraDB for HBase is 0.9.0. Therefore, we recommend that you use Thrift 0.9.0 to create a client. Click here to download Thrift 0.9.0. The downloaded source code package will be used later. You must install the Thrift compiling environment first. For more information, see Thrift official website."},
                          {"source": "您可以通过PyPI来安装SDK,安装命令如下:", "target": "You can run the following command in Python Package Index (PyPI) to install Elastic Container Instance SDK for Python:"}
                    ]
                  }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="领域提示">
            <Tabs>
              <Tab title="Python">
                ```python expandable
                import os
                from openai import OpenAI

                client = OpenAI(
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    # 以下是北京地域的base_url
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                )
                messages = [
                    {
                        "role": "user",
                        "content": "第二个SELECT语句返回一个数字，表示在没有LIMIT子句的情况下，第一个SELECT语句返回了多少行。"
                    }
                ]
                translation_options = {
                    "source_lang": "Chinese",
                    "target_lang": "English",
                    "domains": "The sentence is from Ali Cloud IT domain. It mainly involves computer-related software development and usage methods, including many terms related to computer software and hardware. Pay attention to professional troubleshooting terminologies and sentence patterns when translating. Translate into this IT domain style."
                }

                completion = client.chat.completions.create(
                    model="qwen-mt-plus",
                    messages=messages,
                    extra_body={
                        "translation_options": translation_options
                    }
                )
                print(completion.choices[0].message.content)
                ```
              </Tab>

              <Tab title="Node.js">
                ```javascript
                // 需要 Node.js v18+，需在 ES Module 环境下运行
                import OpenAI from "openai";

                const openai = new OpenAI(
                    {
                        // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey: "sk-xxx",
                        apiKey: process.env.DASHSCOPE_API_KEY,
                        // 以下是北京地域的base_url
                        baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                    }
                );
                const completion = await openai.chat.completions.create({
                    model: "qwen-mt-plus",
                    messages: [
                        { role: "user", content: "第二个SELECT语句返回一个数字，表示在没有LIMIT子句的情况下，第一个SELECT语句返回了多少行。" }
                    ],
                    translation_options: {
                        source_lang: "Chinese",
                        target_lang: "English",
                        domains: "The sentence is from Ali Cloud IT domain. It mainly involves computer-related software development and usage methods, including many terms related to computer software and hardware. Pay attention to professional troubleshooting terminologies and sentence patterns when translating. Translate into this IT domain style."
                    }
                });
                console.log(JSON.stringify(completion));
                ```
              </Tab>

              <Tab title="curl">
                各地域的[请求地址](/zh/model-studio/qwen-mt-api)和API Key不同，以下是北京地域的请求地址。

                ```bash
                curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H "Content-Type: application/json" \
                -d '{
                  "model": "qwen-mt-plus",
                  "messages": [
                    {
                      "role": "user",
                      "content": "第二个SELECT语句返回一个数字，表示在没有LIMIT子句的情况下，第一个SELECT语句返回了多少行。"
                    }
                  ],
                  "translation_options": {
                    "source_lang": "Chinese",
                    "target_lang": "English",
                    "domains": "The sentence is from Ali Cloud IT domain. It mainly involves computer-related software development and usage methods, including many terms related to computer software and hardware. Pay attention to professional troubleshooting terminologies and sentence patterns when translating. Translate into this IT domain style."
                  }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。支持的模型：qwen-mt-plus、qwen-mt-flash、qwen-mt-lite。
      </td>
    </tr>

    <tr>
      <td>
        <strong>messages</strong>`array`<strong>（必选）</strong>

        消息数组，用于向大模型传递上下文。仅支持传入 User Message。

        <Accordion title="消息类型" defaultOpen>
          User Message`object`<strong>（必选）</strong>

          用户消息，用于传递待翻译的句子。

          <Accordion title="属性" defaultOpen>
            <strong>content</strong>`string`<strong>（必选）</strong>

            待翻译的句子。

            <strong>role</strong>`string`<strong>（必选）</strong>

            用户消息的角色，必须设为`user`。
          </Accordion>
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>stream</strong>`boolean`（可选） 默认值为 `false`

        是否以流式方式输出回复。

        可选值：

        - `false`：等待模型生成完整回复后一次性返回。
        - `true`：模型边生成边返回数据块。客户端需逐块读取，以还原完整回复。

        <Note>
          当前仅qwen-mt-flash、qwen-mt-lite模型支持以增量形式返回数据，每次返回仅包含新生成的内容。qwen-mt-plus模型以非增量形式返回数据，每次返回当前已经生成的整个序列，暂时无法修改。如：

          I

          I didn

          I didn't

          I didn't laugh

          I didn't laugh after

          ...
        </Note>
      </td>
    </tr>

    <tr>
      <td>
        <strong>stream\_options</strong>`object`（可选）

        流式输出的配置项，仅在 `stream` 为 `true` 时生效。

        <Accordion title="属性" defaultOpen>
          <strong>include\_usage</strong>`boolean`（可选）默认值为 `false`

          是否在<strong>最后一个数据块</strong>包含Token消耗信息。

          可选值：

          - `true`：包含；
          - `false`：不包含。
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>max\_tokens</strong>`integer`（可选）

        用于限制模型输出的最大 Token 数。若生成内容超过此值，响应将被截断。

        默认值与最大值均为模型的最大输出长度，请参见[模型选型](/zh/model-studio/machine-translation#154957e783dxk)。
      </td>
    </tr>

    <tr>
      <td>
        <strong>seed</strong>`integer`（可选）

        随机数种子。用于确保在相同输入和参数下生成结果可复现。若调用时传入相同的 `seed` 且其他参数不变，模型将尽可能返回相同结果。

        取值范围：`[0,2 31 −1]`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>temperature</strong>`float`（可选）默认值为0.65

        采样温度，控制模型生成文本的多样性。

        temperature越高，生成的文本更多样，反之，生成的文本更确定。

        取值范围： \[0, 2)

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。
      </td>
    </tr>

    <tr>
      <td>
        <strong>top\_p</strong>`float`（可选）默认值为0.8

        核采样的概率阈值，控制模型生成文本的多样性。

        top\_p越高，生成的文本更多样。反之，生成的文本更确定。

        取值范围：（0,1.0]

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。
      </td>
    </tr>

    <tr>
      <td>
        <strong>top\_k</strong>`integer`（可选）默认值为1

        生成过程中采样候选集的大小。例如，取值为50时，仅将单次生成中得分最高的50个Token组成随机采样的候选集。取值越大，生成的随机性越高；取值越小，生成的确定性越高。取值为None或当top\_k大于100时，表示不启用top\_k策略，此时仅有top\_p策略生效。

        取值需要大于或等于0。

        该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中，配置方式为：`extra_body={"top_k": xxx}`；通过 Node.js SDK或HTTP方式调用时，请作为顶层参数传递。
      </td>
    </tr>

    <tr>
      <td>
        <strong>repetition\_penalty</strong>`float`（可选）默认值为1.0

        模型生成时连续序列中的重复度。提高repetition\_penalty时可以降低模型生成的重复度，1.0表示不做惩罚。没有严格的取值范围，只要大于0即可。

        该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中，配置方式为：`extra_body={"repetition_penalty": xxx}`；通过 Node.js SDK或HTTP方式调用时，请作为顶层参数传递。
      </td>
    </tr>

    <tr>
      <td>
        <strong>translation\_options</strong>`object`<strong>（必选）</strong>

        需配置的翻译参数。

        <Accordion title="属性" defaultOpen>
          <strong>source\_lang</strong> `string` （必选）

          源语言的英文全称，详情请参见[支持的语言](/zh/model-studio/machine-translation#038d2865bbydc)。若设为`auto`，模型会自动识别输入的语种。

          <strong>target\_lang</strong> `string` （必选）

          目标语言的英文全称，详情请参见[支持的语言](/zh/model-studio/machine-translation#038d2865bbydc)。

          <strong>terms</strong> `arrays` （可选）

          使用[术语干预](/zh/model-studio/machine-translation#2bf54a5ab5voe)功能时需设置的术语数组。

          <Accordion title="属性">
            <strong>source</strong> `string` （必选）

            源语言的术语。

            <strong>target</strong> `string` （必选）

            目标语言的术语。
          </Accordion>

          <strong>tm\_list</strong> `arrays` （可选）

          使用[翻译记忆](/zh/model-studio/machine-translation#17e15234e7gfp)功能时需设置的翻译记忆数组。

          <Accordion title="属性">
            <strong>source</strong> `string` （必选）

            源语言的语句。

            <strong>target</strong> `string` （必选）

            目标语言的语句。
          </Accordion>

          <strong>domains</strong> `string` （可选）

          使用[领域提示](/zh/model-studio/machine-translation#4af23a31db7lf)功能时需设置的领域提示语句。

          > 领域提示语句暂时只支持英文。
        </Accordion>

        该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中，配置方式为：`extra_body={"translation_options": xxx}`；通过 Node.js SDK或HTTP方式调用时，请作为顶层参数传递。
      </td>
    </tr>
  </tbody>
</table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "57.57%" }} />

    <col style={{ width: "42.43%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### chat响应对象（非流式输出） <span id="cd3063363egdc" />
      </td>

      <td rowSpan={9}>
        ```json expandable
        {
          "id": "chatcmpl-999a5d8a-f646-4039-968a-167743ae0f22",
          "choices": [
            {
              "finish_reason": "stop",
              "index": 0,
              "logprobs": null,
              "message": {
                "content": "I didn't laugh after watching this video.",
                "refusal": null,
                "role": "assistant",
                "annotations": null,
                "audio": null,
                "function_call": null,
                "tool_calls": null
              }
            }
          ],
          "created": 1762346157,
          "model": "qwen-mt-plus",
          "object": "chat.completion",
          "service_tier": null,
          "system_fingerprint": null,
          "usage": {
            "completion_tokens": 9,
            "prompt_tokens": 53,
            "total_tokens": 62,
            "completion_tokens_details": null,
            "prompt_tokens_details": null
          }
        }
        ```
      </td>
    </tr>

    <tr>
      <td>
        <strong>id</strong>`string`

        本次请求的唯一标识符。
      </td>
    </tr>

    <tr>
      <td>
        <strong>choices</strong>`array`

        模型生成内容的数组。

        <Accordion title="属性" defaultOpen>
          <strong>finish\_reason</strong>`string`

          模型停止生成的原因。

          有两种情况：

          - 自然停止输出时为`stop`；
          - 生成长度过长而结束为`length`。

          <strong>index</strong>`integer`

          当前对象在`choices`数组中的索引。

          <strong>message</strong>`object`

          模型输出的消息。

          <Accordion title="属性" defaultOpen>
            <strong>content</strong> `string`

            模型翻译结果。

            <strong>refusal</strong> `string`

            该参数当前固定为`null`。

            <strong>role</strong> `string`

            消息的角色，固定为`assistant`。

            <strong>audio</strong> `object`

            该参数当前固定为`null`。

            <strong>function\_call</strong> `object`

            该参数当前固定为`null`。

            <strong>tool\_calls</strong> `array`

            该参数当前固定为`null`。
          </Accordion>
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>created</strong>`integer`

        本次请求被创建时的时间戳。
      </td>
    </tr>

    <tr>
      <td>
        <strong>model</strong>`string`

        本次请求使用的模型。
      </td>
    </tr>

    <tr>
      <td>
        <strong>object</strong> `string`

        始终为`chat.completion`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>service\_tier</strong> `string`

        该参数当前固定为`null`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>system\_fingerprint</strong>`string`

        该参数当前固定为`null`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>usage</strong> `object`

        本次请求的 Token 消耗信息。

        <Accordion title="属性">
          <strong>completion\_tokens</strong> `integer`

          模型输出的 Token 数。

          <strong>prompt\_tokens</strong> `integer`

          输入的 Token 数。

          <strong>total\_tokens</strong> `integer`

          消耗的总 Token 数，为`prompt_tokens`与`completion_tokens`的总和。

          <strong>completion\_tokens\_details</strong> `object`

          该参数当前固定为`null`。

          <strong>prompt\_tokens\_details</strong> `object`

          该参数当前固定为`null`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "57.13%" }} />

    <col style={{ width: "42.87%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### chat响应chunk对象（流式输出） <span id="e98e6b7aa5nb2" />
      </td>

      <td rowSpan={9}>
        <CodeGroup>
          ```json 增量输出
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": "", "function_call": null, "refusal": null, "role": "assistant", "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": "I", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": " didn", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": "'t", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": " laugh", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": " after", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": " watching", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": " this", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": " video", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": ".", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": null, "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": "", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": "stop", "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [{"delta": {"content": "", "function_call": null, "refusal": null, "role": null, "tool_calls": null}, "finish_reason": "stop", "index": 0, "logprobs": null}], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": null}
          {"id": "chatcmpl-d8aa6596-b366-4ed0-9f6d-2e89247f554e", "choices": [], "created": 1762504029, "model": "qwen-mt-flash", "object": "chat.completion.chunk", "service_tier": null, "system_fingerprint": null, "usage": {"completion_tokens": 9, "prompt_tokens": 56, "total_tokens": 65, "completion_tokens_details": null, "prompt_tokens_details": null}}
          ```

          ```json 非增量输出
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"","function_call":null,"refusal":null,"role":"assistant","tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn’t","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn’t laugh","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn’t laugh after","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn’t laugh after watching","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn’t laugh after watching this","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn’t laugh after watching this video","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn’t laugh after watching this video.","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn’t laugh after watching this video.","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":"stop","index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[{"delta":{"content":"I didn’t laugh after watching this video.","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":"stop","index":0,"logprobs":null}],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
          {"id":"chatcmpl-478e183e-cbdc-4ea0-aeae-4c2ba1d03e4d","choices":[],"created":1762346453,"model":"qwen-mt-plus","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":{"completion_tokens":9,"prompt_tokens":56,"total_tokens":65,"completion_tokens_details":null,"prompt_tokens_details":null}}
          ```
        </CodeGroup>
      </td>
    </tr>

    <tr>
      <td>
        <strong>id</strong>`string`

        本次调用的唯一标识符。每个chunk对象有相同的 id。
      </td>
    </tr>

    <tr>
      <td>
        <strong>choices</strong>`array`

        模型生成内容的数组。若设置`include_usage`参数为`true`，则在最后一个chunk中为空。

        <Accordion title="属性" defaultOpen>
          <strong>delta</strong> `object`

          流式返回的输出内容。

          <Accordion title="属性" defaultOpen>
            <strong>content</strong> `string`

            翻译结果，qwen-mt-flash和qwen-mt-lite为增量式更新，qwen-mt-plus为非增量式更新。

            <strong>function\_call</strong> `object`

            该参数当前固定为`null`。

            <strong>refusal</strong> `object`

            该参数当前固定为`null`。

            <strong>role</strong> `string`

            消息对象的角色，只在第一个chunk中有值。
          </Accordion>

          <strong>finish\_reason</strong> `string`

          模型停止生成的原因。有三种情况：

          - 自然停止输出时为`stop`；
          - 生成未结束时为`null`；
          - 生成长度过长而结束为`length`。

          <strong>index</strong> `integer`

          当前响应在`choices`数组中的索引。
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>created</strong>`integer`

        本次请求被创建时的时间戳。每个chunk有相同的时间戳。
      </td>
    </tr>

    <tr>
      <td>
        <strong>model</strong>`string`

        本次请求使用的模型。
      </td>
    </tr>

    <tr>
      <td>
        <strong>object</strong> `string`

        始终为`chat.completion.chunk`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>service\_tier</strong> `string`

        该参数当前固定为`null`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>system\_fingerprint</strong>`string`

        该参数当前固定为`null`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>usage</strong> `object`

        本次请求消耗的Token。只在`include_usage`为`true`时，在最后一个chunk返回。

        <Accordion title="属性">
          <strong>completion\_tokens</strong> `integer`

          模型输出的 Token 数。

          <strong>prompt\_tokens</strong> `integer`

          输入 Token 数。

          <strong>total\_tokens</strong> `integer`

          总 Token 数，为`prompt_tokens`与`completion_tokens`的总和。

          <strong>completion\_tokens\_details</strong> `object`

          该参数当前固定为`null`。

          <strong>prompt\_tokens\_details</strong> `object`

          该参数当前固定为`null`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## DashScope <span id="69cac67a477k2" />

<Tabs>
  <Tab title="北京地域">
    HTTP 请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text-generation/generation`

    SDK 调用无需配置 `base_url`，其默认值为`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1`。
  </Tab>

  <Tab title="新加坡地域">
    HTTP 请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/text-generation/generation`

    SDK调用配置的`base_url`：

    <Tabs>
      <Tab title="Python代码">
        ```python
        dashscope.base_http_api_url = 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1'
        ```
      </Tab>

      <Tab title="Java代码">
        - <strong>方式一：</strong>

        ```java
        import com.alibaba.dashscope.protocol.Protocol;
        Generation gen = new Generation(Protocol.HTTP.getValue(), "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1");
        ```

        - <strong>方式二：</strong>

        ```java
        import com.alibaba.dashscope.utils.Constants;
        Constants.baseHttpApiUrl="https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1";
        ```
      </Tab>
    </Tabs>
  </Tab>

  <Tab title="美国（弗吉尼亚）地域">
    HTTP 请求地址：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/text-generation/generation`

    SDK调用配置的`base_url`：

    <Tabs>
      <Tab title="Python代码">
        ```python
        dashscope.base_http_api_url = 'https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1'
        ```
      </Tab>

      <Tab title="Java代码">
        - <strong>方式一：</strong>

        ```java
        import com.alibaba.dashscope.protocol.Protocol;
        Generation gen = new Generation(Protocol.HTTP.getValue(), "https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1");
        ```

        - <strong>方式二：</strong>

        ```java
        import com.alibaba.dashscope.utils.Constants;
        Constants.baseHttpApiUrl="https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1";
        ```
      </Tab>
    </Tabs>
  </Tab>
</Tabs>

> 您需要已 [获取与配置 API Key](/zh/model-studio/get-api-key) 并 [配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables) 。若通过DashScope SDK进行调用，需要 [安装DashScope SDK](/zh/model-studio/install-sdk) 。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "56.98%" }} />

    <col style={{ width: "43.02%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="2a1c410015otp" />
      </td>

      <td rowSpan={11}>
        <Tabs>
          <Tab title="基础使用">
            <Tabs>
              <Tab title="Python">
                ```python
                import os
                import dashscope

                # 若使用新加坡地域的模型，请将{WorkspaceId}替换为真实的业务空间ID，并释放下列注释
                # dashscope.base_http_api_url = "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1"

                messages = [
                    {
                        "role": "user",
                        "content": "我看到这个视频后没有笑"
                    }
                ]
                translation_options = {
                    "source_lang": "auto",
                    "target_lang": "English",
                }
                response = dashscope.Generation.call(
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                    api_key=os.getenv('DASHSCOPE_API_KEY'),
                    model="qwen-mt-plus",
                    messages=messages,
                    result_format='message',
                    translation_options=translation_options
                )
                print(response.output.choices[0].message.content)
                ```
              </Tab>

              <Tab title="Java">
                ```java expandable
                // DashScope SDK 版本需要不低于 2.20.6
                import java.lang.System;
                import java.util.Collections;
                import com.alibaba.dashscope.aigc.generation.Generation;
                import com.alibaba.dashscope.aigc.generation.GenerationParam;
                import com.alibaba.dashscope.aigc.generation.GenerationResult;
                import com.alibaba.dashscope.aigc.generation.TranslationOptions;
                import com.alibaba.dashscope.common.Message;
                import com.alibaba.dashscope.common.Role;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.utils.Constants;

                public class Main {
                    // 若使用新加坡地域的模型，请将{WorkspaceId}替换为真实的业务空间ID，并释放下列注释
                    // static {Constants.baseHttpApiUrl="https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1";}
                    public static GenerationResult callWithMessage() throws ApiException, NoApiKeyException, InputRequiredException {
                        Generation gen = new Generation();
                        Message userMsg = Message.builder()
                                .role(Role.USER.getValue())
                                .content("我看到这个视频后没有笑")
                                .build();
                        TranslationOptions options = TranslationOptions.builder()
                                .sourceLang("auto")
                                .targetLang("English")
                                .build();
                        GenerationParam param = GenerationParam.builder()
                                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .model("qwen-mt-plus")
                                .messages(Collections.singletonList(userMsg))
                                .resultFormat(GenerationParam.ResultFormat.MESSAGE)
                                .translationOptions(options)
                                .build();
                        return gen.call(param);
                    }
                    public static void main(String[] args) {
                        // 以下为华北2（北京）地域的配置，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的配置不同。
                        Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                        try {
                            GenerationResult result = callWithMessage();
                            System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent());
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.err.println("错误信息："+e.getMessage());
                            e.printStackTrace();
                        } finally {
                            System.exit(0);
                        }
                    }
                }
                ```
              </Tab>

              <Tab title="curl">
                各地域的[请求地址](/zh/model-studio/qwen-mt-api)和API Key不同，以下是北京地域的请求地址。

                ```bash
                curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text-generation/generation \
                -H "Authorization: $DASHSCOPE_API_KEY" \
                -H "Content-Type: application/json" \
                -d '{
                  "model": "qwen-mt-plus",
                  "input": {
                    "messages": [
                      {
                        "content": "我看到这个视频后没有笑",
                        "role": "user"
                      }
                    ]
                  },
                  "parameters": {
                    "translation_options": {
                      "source_lang": "auto",
                      "target_lang": "English"
                    }
                  }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="术语干预">
            <Tabs>
              <Tab title="Python">
                ```python expandable
                import os
                import dashscope

                # 若使用新加坡地域的模型，请将{WorkspaceId}替换为真实的业务空间ID，并释放下列注释
                # dashscope.base_http_api_url = "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1"
                messages = [
                    {
                        "role": "user",
                        "content": "而这套生物传感器运用了石墨烯这种新型材料，它的目标物是化学元素，敏锐的“嗅觉”让它能更深度、准确地体现身体健康状况。"
                    }
                ]
                translation_options = {
                    "source_lang": "Chinese",
                    "target_lang": "English",
                    "terms": [
                        {
                            "source": "生物传感器",
                            "target": "biological sensor"
                        },
                        {
                            "source": "石墨烯",
                            "target": "graphene"
                        },
                        {
                            "source": "化学元素",
                            "target": "chemical elements"
                        },
                        {
                            "source": "身体健康状况",
                            "target": "health status of the body"
                        }
                    ]
                }
                response = dashscope.Generation.call(
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                    api_key=os.getenv('DASHSCOPE_API_KEY'),
                    model="qwen-mt-plus",
                    messages=messages,
                    result_format='message',
                    translation_options=translation_options
                )
                print(response.output.choices[0].message.content)
                ```
              </Tab>

              <Tab title="Java">
                ```java expandable
                // DashScope SDK 版本需要不低于 2.20.6
                import java.lang.System;
                import java.util.Collections;
                import java.util.Arrays;
                import com.alibaba.dashscope.aigc.generation.Generation;
                import com.alibaba.dashscope.aigc.generation.GenerationParam;
                import com.alibaba.dashscope.aigc.generation.GenerationResult;
                import com.alibaba.dashscope.aigc.generation.TranslationOptions;
                import com.alibaba.dashscope.aigc.generation.TranslationOptions.Term;
                import com.alibaba.dashscope.common.Message;
                import com.alibaba.dashscope.common.Role;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.utils.Constants;

                public class Main {
                    // 若使用新加坡地域的模型，请将{WorkspaceId}替换为真实的业务空间ID，并释放下列注释
                    // static {Constants.baseHttpApiUrl="https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1";}
                    public static GenerationResult callWithMessage() throws ApiException, NoApiKeyException, InputRequiredException {
                        Generation gen = new Generation();
                        Message userMsg = Message.builder()
                                .role(Role.USER.getValue())
                                .content("而这套生物传感器运用了石墨烯这种新型材料，它的目标物是化学元素，敏锐的“嗅觉”让它能更深度、准确地体现身体健康状况。")
                                .build();
                        Term term1 = Term.builder()
                                .source("生物传感器")
                                .target("biological sensor")
                                .build();
                        Term term2 = Term.builder()
                                .source("身体健康状况")
                                .target("health status of the body")
                                .build();
                        TranslationOptions options = TranslationOptions.builder()
                                .sourceLang("auto")
                                .targetLang("English")
                                .terms(Arrays.asList(term1, term2))
                                .build();
                        GenerationParam param = GenerationParam.builder()
                                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .model("qwen-mt-plus")
                                .messages(Collections.singletonList(userMsg))
                                .resultFormat(GenerationParam.ResultFormat.MESSAGE)
                                .translationOptions(options)
                                .build();
                        return gen.call(param);
                    }
                    public static void main() {
                        try {
                            GenerationResult result = callWithMessage();
                            System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent());
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.err.println("错误信息："+e.getMessage());
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="curl">
                各地域的[请求地址](/zh/model-studio/qwen-mt-api)和API Key不同，以下是北京地域的请求地址。

                ```bash expandable
                curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text-generation/generation \
                -H "Authorization: $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                  "model": "qwen-mt-plus",
                  "input": {
                    "messages": [
                      {
                        "content": "而这套生物传感器运用了石墨烯这种新型材料，它的目标物是化学元素，敏锐的“嗅觉”让它能更深度、准确地体现身体健康状况。",
                        "role": "user"
                      }
                    ]
                  },
                  "parameters": {
                    "translation_options": {
                      "source_lang": "Chinese",
                      "target_lang": "English",
                      "terms": [
                        {
                          "source": "生物传感器",
                          "target": "biological sensor"
                        },
                        {
                          "source": "身体健康状况",
                          "target": "health status of the body"
                        }
                      ]
                  }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="翻译记忆">
            <Tabs>
              <Tab title="Python">
                ```python expandable
                import os
                import dashscope

                # 若使用新加坡地域的模型，请将{WorkspaceId}替换为真实的业务空间ID，并释放下列注释
                # dashscope.base_http_api_url = "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1"
                messages = [
                    {
                        "role": "user",
                        "content": "通过如下命令可以看出安装thrift的版本信息；"
                    }
                ]
                translation_options = {
                    "source_lang": "Chinese",
                    "target_lang": "English",
                    "tm_list": [
                        {
                            "source": "您可以通过如下方式查看集群的内核版本信息:",
                            "target": "You can use one of the following methods to query the engine version of a cluster:"
                        },
                        {
                            "source": "我们云HBase的thrift环境是0.9.0,所以建议客户端的版本也为 0.9.0,可以从这里下载thrift的0.9.0 版本,下载的源码包我们后面会用到,这里需要先安装thrift编译环境,对于源码安装可以参考thrift官网;",
                            "target": "The version of Thrift used by ApsaraDB for HBase is 0.9.0. Therefore, we recommend that you use Thrift 0.9.0 to create a client. Click here to download Thrift 0.9.0. The downloaded source code package will be used later. You must install the Thrift compiling environment first. For more information, see Thrift official website."
                        },
                        {
                            "source": "您可以通过PyPI来安装SDK,安装命令如下:",
                            "target": "You can run the following command in Python Package Index (PyPI) to install Elastic Container Instance SDK for Python:"
                        }
                    ]}
                response = dashscope.Generation.call(
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                    api_key=os.getenv('DASHSCOPE_API_KEY'),
                    model="qwen-mt-plus",
                    messages=messages,
                    result_format='message',
                    translation_options=translation_options
                )
                print(response.output.choices[0].message.content)
                ```
              </Tab>

              <Tab title="Java">
                ```java expandable
                // DashScope SDK 版本需要不低于 2.20.6
                import java.lang.System;
                import java.util.Collections;
                import java.util.Arrays;
                import com.alibaba.dashscope.aigc.generation.Generation;
                import com.alibaba.dashscope.aigc.generation.GenerationParam;
                import com.alibaba.dashscope.aigc.generation.GenerationResult;
                import com.alibaba.dashscope.aigc.generation.TranslationOptions;
                import com.alibaba.dashscope.aigc.generation.TranslationOptions.Tm;
                import com.alibaba.dashscope.common.Message;
                import com.alibaba.dashscope.common.Role;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.utils.Constants;

                public class Main {
                    // 若使用新加坡地域的模型，请将{WorkspaceId}替换为真实的业务空间ID，并释放下列注释
                    // static {Constants.baseHttpApiUrl="https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1";}
                    public static GenerationResult callWithMessage() throws ApiException, NoApiKeyException, InputRequiredException {
                        Generation gen = new Generation();
                        Message userMsg = Message.builder()
                                .role(Role.USER.getValue())
                                .content("通过如下命令可以看出安装thrift的版本信息；")
                                .build();
                        Tm tm1 = Tm.builder()
                                .source("您可以通过如下方式查看集群的内核版本信息:")
                                .target("You can use one of the following methods to query the engine version of a cluster:")
                                .build();
                        Tm tm2 = Tm.builder()
                                .source("我们云HBase的thrift环境是0.9.0,所以建议客户端的版本也为 0.9.0,可以从这里下载thrift的0.9.0 版本,下载的源码包我们后面会用到,这里需要先安装thrift编译环境,对于源码安装可以参考thrift官网;")
                                .target("The version of Thrift used by ApsaraDB for HBase is 0.9.0. Therefore, we recommend that you use Thrift 0.9.0 to create a client. Click here to download Thrift 0.9.0. The downloaded source code package will be used later. You must install the Thrift compiling environment first. For more information, see Thrift official website.")
                                .build();
                        Tm tm3 = Tm.builder()
                                .source("您可以通过PyPI来安装SDK,安装命令如下:")
                                .target("You can run the following command in Python Package Index (PyPI) to install Elastic Container Instance SDK for Python:")
                                .build();
                        TranslationOptions options = TranslationOptions.builder()
                                .sourceLang("auto")
                                .targetLang("English")
                                .tmList(Arrays.asList(tm1, tm2, tm3))
                                .build();
                        GenerationParam param = GenerationParam.builder()
                                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .model("qwen-mt-plus")
                                .messages(Collections.singletonList(userMsg))
                                .resultFormat(GenerationParam.ResultFormat.MESSAGE)
                                .translationOptions(options)
                                .build();
                        return gen.call(param);
                    }
                    public static void main() {
                        try {
                            GenerationResult result = callWithMessage();
                            System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent());
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.err.println("错误信息："+e.getMessage());
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="curl">
                各地域的[请求地址](/zh/model-studio/qwen-mt-api)和API Key不同，以下是北京地域的请求地址。

                ```bash
                curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text-generation/generation \
                -H "Authorization: $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                  "model": "qwen-mt-plus",
                  "input": {
                    "messages": [
                      {
                        "content": "通过如下命令可以看出安装thrift的版本信息；",
                        "role": "user"
                      }
                    ]
                  },
                  "parameters": {
                    "translation_options": {
                      "source_lang": "Chinese",
                      "target_lang": "English",
                      "tm_list":[
                          {"source": "您可以通过如下方式查看集群的内核版本信息:", "target": "You can use one of the following methods to query the engine version of a cluster:"},
                          {"source": "我们云HBase的thrift环境是0.9.0,所以建议客户端的版本也为 0.9.0,可以从这里下载thrift的0.9.0 版本,下载的源码包我们后面会用到,这里需要先安装thrift编译环境,对于源码安装可以参考thrift官网;", "target": "The version of Thrift used by ApsaraDB for HBase is 0.9.0. Therefore, we recommend that you use Thrift 0.9.0 to create a client. Click here to download Thrift 0.9.0. The downloaded source code package will be used later. You must install the Thrift compiling environment first. For more information, see Thrift official website."},
                          {"source": "您可以通过PyPI来安装SDK,安装命令如下:", "target": "You can run the following command in Python Package Index (PyPI) to install Elastic Container Instance SDK for Python:"}
                      ]
                  }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="领域提示">
            <Tabs>
              <Tab title="Python">
                ```python expandable
                import os
                import dashscope

                # 若使用新加坡地域的模型，请将{WorkspaceId}替换为真实的业务空间ID，并释放下列注释
                # dashscope.base_http_api_url = "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1"

                messages = [
                    {
                        "role": "user",
                        "content": "第二个SELECT语句返回一个数字，表示在没有LIMIT子句的情况下，第一个SELECT语句返回了多少行。"
                    }
                ]
                translation_options = {
                    "source_lang": "Chinese",
                    "target_lang": "English",
                    "domains": "The sentence is from Ali Cloud IT domain. It mainly involves computer-related software development and usage methods, including many terms related to computer software and hardware. Pay attention to professional troubleshooting terminologies and sentence patterns when translating. Translate into this IT domain style."
                }
                response = dashscope.Generation.call(
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
                    api_key=os.getenv('DASHSCOPE_API_KEY'),
                    model="qwen-mt-plus",
                    messages=messages,
                    result_format='message',
                    translation_options=translation_options
                )
                print(response.output.choices[0].message.content)
                ```
              </Tab>

              <Tab title="Java">
                ```java expandable
                // DashScope SDK 版本需要不低于 2.20.6
                import java.lang.System;
                import java.util.Collections;
                import com.alibaba.dashscope.aigc.generation.Generation;
                import com.alibaba.dashscope.aigc.generation.GenerationParam;
                import com.alibaba.dashscope.aigc.generation.GenerationResult;
                import com.alibaba.dashscope.aigc.generation.TranslationOptions;
                import com.alibaba.dashscope.common.Message;
                import com.alibaba.dashscope.common.Role;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.utils.Constants;

                public class Main {
                    // 若使用新加坡地域的模型，请将{WorkspaceId}替换为真实的业务空间ID，并释放下列注释
                    // static {Constants.baseHttpApiUrl="https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1";}
                    public static GenerationResult callWithMessage() throws ApiException, NoApiKeyException, InputRequiredException {
                        Generation gen = new Generation();
                        Message userMsg = Message.builder()
                                .role(Role.USER.getValue())
                                .content("第二个SELECT语句返回一个数字，表示在没有LIMIT子句的情况下，第一个SELECT语句返回了多少行。")
                                .build();
                        TranslationOptions options = TranslationOptions.builder()
                                .sourceLang("auto")
                                .targetLang("English")
                                .domains("The sentence is from Ali Cloud IT domain. It mainly involves computer-related software development and usage methods, including many terms related to computer software and hardware. Pay attention to professional troubleshooting terminologies and sentence patterns when translating. Translate into this IT domain style.")
                                .build();
                        GenerationParam param = GenerationParam.builder()
                                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .model("qwen-mt-plus")
                                .messages(Collections.singletonList(userMsg))
                                .resultFormat(GenerationParam.ResultFormat.MESSAGE)
                                .translationOptions(options)
                                .build();
                        return gen.call(param);
                    }
                    public static void main() {
                        try {
                            GenerationResult result = callWithMessage();
                            System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent());
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.err.println("错误信息："+e.getMessage());
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="curl">
                各地域的[请求地址](/zh/model-studio/qwen-mt-api)和API Key不同，以下是北京地域的请求地址。

                ```bash
                curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text-generation/generation \
                -H "Authorization: $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                  "model": "qwen-mt-plus",
                  "input": {
                    "messages": [
                      {
                        "content": "第二个SELECT语句返回一个数字，表示在没有LIMIT子句的情况下，第一个SELECT语句返回了多少行。",
                        "role": "user"
                      }
                    ]
                  },
                  "parameters": {
                    "translation_options": {
                      "source_lang": "Chinese",
                      "target_lang": "English",
                      "domains": "The sentence is from Ali Cloud IT domain. It mainly involves computer-related software development and usage methods, including many terms related to computer software and hardware. Pay attention to professional troubleshooting terminologies and sentence patterns when translating. Translate into this IT domain style."}
                  }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。支持的模型：qwen-mt-plus、qwen-mt-flash、qwen-mt-lite。
      </td>
    </tr>

    <tr>
      <td>
        <strong>messages</strong>`array`<strong>（必选）</strong>

        消息数组，用于向大模型传递上下文。仅支持传入 User Message。

        <Accordion title="消息类型" defaultOpen>
          User Message`object`<strong>（必选）</strong>

          用户消息，用于传递待翻译的句子。

          <Accordion title="属性" defaultOpen>
            <strong>content</strong>`string`<strong>（必选）</strong>

            待翻译的句子。

            <strong>role</strong>`string`<strong>（必选）</strong>

            用户消息的角色，必须设为`user`。
          </Accordion>
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>max\_tokens</strong>`integer`（可选）

        用于限制模型输出的最大 Token 数。若生成内容超过此值，响应将被截断。

        默认值与最大值均为模型的最大输出长度，请参见[模型选型](/zh/model-studio/machine-translation#154957e783dxk)。

        > Java SDK中为<strong>maxTokens</strong>\*。\*通过HTTP调用时，请将 <strong>max\_tokens</strong>放入 <strong>parameters</strong> 对象中。
      </td>
    </tr>

    <tr>
      <td>
        <strong>seed</strong>`integer`（可选）

        随机数种子。用于确保在相同输入和参数下生成结果可复现。若调用时传入相同的 `seed` 且其他参数不变，模型将尽可能返回相同结果。

        取值范围：`[0,2 31 −1]`。

        > 通过HTTP调用时，请将 <strong>seed</strong>放入 <strong>parameters</strong> 对象中。
      </td>
    </tr>

    <tr>
      <td>
        <strong>temperature</strong>`float`（可选）默认值为0.65

        采样温度，控制模型生成文本的多样性。

        temperature越高，生成的文本更多样，反之，生成的文本更确定。

        取值范围： \[0, 2)

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。

        > 通过HTTP调用时，请将 <strong>temperature</strong>放入 <strong>parameters</strong> 对象中。
      </td>
    </tr>

    <tr>
      <td>
        <strong>top\_p</strong>`float`（可选）默认值为0.8

        核采样的概率阈值，控制模型生成文本的多样性。

        top\_p越高，生成的文本更多样。反之，生成的文本更确定。

        取值范围：（0,1.0]

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。

        > Java SDK中为<strong>topP</strong>\*。\*通过HTTP调用时，请将 <strong>top\_p</strong>放入 <strong>parameters</strong> 对象中。
      </td>
    </tr>

    <tr>
      <td>
        <strong>repetition\_penalty</strong>`float`（可选）默认值为1.0

        模型生成时连续序列中的重复度。提高repetition\_penalty时可以降低模型生成的重复度，1.0表示不做惩罚。没有严格的取值范围，只要大于0即可。

        > Java SDK中为<strong>repetitionPenalty</strong>\*。\*通过HTTP调用时，请将 <strong>repetition\_penalty</strong>放入 <strong>parameters</strong> 对象中。
      </td>
    </tr>

    <tr>
      <td>
        <strong>top\_k</strong>`integer`（可选）默认值为1

        生成过程中采样候选集的大小。例如，取值为50时，仅将单次生成中得分最高的50个Token组成随机采样的候选集。取值越大，生成的随机性越高；取值越小，生成的确定性越高。取值为None或当top\_k大于100时，表示不启用top\_k策略，此时仅有top\_p策略生效。

        取值需要大于或等于0。

        > Java SDK中为<strong>topK</strong>\*。\*通过HTTP调用时，请将 <strong>top\_k</strong>放入 <strong>parameters</strong> 对象中。
      </td>
    </tr>

    <tr>
      <td>
        <strong>stream</strong>`boolean`（可选）

        是否以流式方式输出回复。

        可选值：

        - `false`：等待模型生成完整回复后一次性返回。
        - `true`：模型边生成边返回数据块。客户端需逐块读取，以还原完整回复。

        <Note>
          当前仅qwen-mt-flash、qwen-mt-lite模型支持以增量形式返回数据，每次返回仅包含新生成的内容。qwen-mt-plus模型以非增量形式返回数据，每次返回当前已经生成的整个序列，暂时无法修改。如：

          I

          I didn

          I didn't

          I didn't laugh

          I didn't laugh after

          ...
        </Note>

        > 该参数仅支持Python SDK。通过Java SDK实现流式输出请通过`streamCall`接口调用；通过HTTP实现流式输出请在Header中指定`X-DashScope-SSE`为`enable`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>translation\_options</strong>`object`<strong>（必选）</strong>

        需配置的翻译参数。

        <Accordion title="属性" defaultOpen>
          <strong>source\_lang</strong> `string` （必选）

          源语言的英文全称，详情请参见[支持的语言](/zh/model-studio/machine-translation#038d2865bbydc)。若设为`auto`，模型会自动识别输入的语种。

          <strong>target\_lang</strong> `string` （必选）

          目标语言的英文全称，详情请参见[支持的语言](/zh/model-studio/machine-translation#038d2865bbydc)。

          <strong>terms</strong> `arrays` （可选）

          使用[术语干预](/zh/model-studio/machine-translation#2bf54a5ab5voe)功能时需设置的术语数组。

          <Accordion title="属性">
            <strong>source</strong> `string` （必选）

            源语言的术语。

            <strong>target</strong> `string` （必选）

            目标语言的术语。
          </Accordion>

          <strong>tm\_list</strong> `arrays` （可选）

          使用[翻译记忆](/zh/model-studio/machine-translation#17e15234e7gfp)功能时需设置的翻译记忆数组。

          <Accordion title="属性">
            <strong>source</strong> `string` （必选）

            源语言的语句。

            <strong>target</strong> `string` （必选）

            目标语言的语句。
          </Accordion>

          <strong>domains</strong> `string` （可选）

          使用[领域提示](/zh/model-studio/machine-translation#4af23a31db7lf)功能时需设置的领域提示语句。

          > 领域提示语句暂时只支持英文。
        </Accordion>

        > Java SDK中为`translationOptions`。通过HTTP调用时，请将 <strong>translation\_options</strong>放入 <strong>parameters</strong> 对象中。
      </td>
    </tr>
  </tbody>
</table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "56.83%" }} />

    <col style={{ width: "43.17%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### chat响应对象（流式与非流式输出格式一致） <span id="0e1c44902ea1e" />
      </td>

      <td rowSpan={6}>
        ```json
        {
          "status_code": 200,
          "request_id": "9b4ec3b2-6d29-40a6-a08b-7e3c9a51c289",
          "code": "",
          "message": "",
          "output": {
            "text": null,
            "finish_reason": "stop",
            "choices": [
              {
                "finish_reason": "stop",
                "message": {
                  "role": "assistant",
                  "content": "I didn't laugh after watching this video."
                }
              }
            ],
            "model_name": "qwen-mt-plus"
          },
          "usage": {
            "input_tokens": 53,
            "output_tokens": 9,
            "total_tokens": 62
          }
        }
        ```
      </td>
    </tr>

    <tr>
      <td>
        <strong>status\_code</strong>`string`

        本次请求的状态码。200 表示请求成功，否则表示请求失败。

        > Java SDK 通过`GenerationResult.getStatusCode()`返回该参数（成功时为 200）。调用失败会抛出异常，可通过`ApiException.getStatus().getStatusCode()`获取错误状态码。
      </td>
    </tr>

    <tr>
      <td>
        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        > Java SDK返回参数为<strong>requestId。</strong>
      </td>
    </tr>

    <tr>
      <td>
        <strong>code</strong>`string`

        错误码，调用成功时为空值。

        > 只有Python SDK返回该参数。
      </td>
    </tr>

    <tr>
      <td>
        <strong>output</strong>`object`

        调用结果信息。

        <Accordion title="属性">
          <strong>text</strong>`string`

          该参数当前固定为`null`。

          <strong>finish\_reason</strong>`string`

          模型结束生成的原因。有以下情况：

          - 正在生成时为`null`；
          - 模型输出自然结束为`stop`；
          - 因生成长度过长而结束为`length`；

          <strong>choices</strong>`array`

          模型的输出信息。

          <Accordion title="属性">
            <strong>finish\_reason</strong>`string`

            有以下情况：

            - 正在生成时为`null`；
            - 因模型输出自然结束为`stop`；
            - 因生成长度过长而结束为`length`；

            <strong>message</strong>`object`

            模型输出的消息对象。

            <Accordion title="属性">
              <strong>role</strong>`string`

              输出消息的角色，固定为`assistant`。

              <strong>content</strong>`string`

              翻译的结果。
            </Accordion>
          </Accordion>

          <strong>model\_name</strong>`string`

          本次请求使用的模型名称。
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>usage</strong>`object`

        本次请求使用的Token信息。

        <Accordion title="属性">
          <strong>input\_tokens</strong> `integer`

          输入 Token 数。

          <strong>output\_tokens</strong> `integer`

          输出 Token 数。

          <strong>total\_tokens</strong> `integer`

          总 Token 数，为<strong>input\_tokens</strong>与<strong>output\_tokens</strong>之和<strong>。</strong>
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="97e145aeabbwf" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

.aliyun-docs-content .one-codeblocks pre \{

max-height: calc(80vh - 136px) !important;

height: auto;

}

.tab-item \{

font-size: 12px !important; /*你可以根据需要调整字体大小*/

padding: 0px 5px !important;

}

.expandable-content \{

border-left: none !important;

border-right: none !important;

border-bottom: none !important;

}
