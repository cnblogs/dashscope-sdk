> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-OCR API参考

<meta property="og:title" content="Qwen-OCR API参考" />

本文介绍通过OpenAI兼容接口 或 DashScopeAPI 调用通义千问OCR 模型的输入与输出参数。

> 相关文档：[文字提取（Qwen-OCR）](/zh/model-studio/qwen-vl-ocr)

## OpenAI 兼容 <span id="d397bcc41eu3q" />

<Tabs>
  <Tab title="华北2（北京）地域">
    SDK 调用配置的`base_url`为：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 调用配置的`endpoint`：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="新加坡地域">
    SDK 调用配置的`base_url`为：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 调用配置的`endpoint`：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="美国（弗吉尼亚）地域">
    SDK 调用配置的`base_url`为：`https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 调用配置的`endpoint`：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
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

        模型名称。支持的模型可参见`选择模型`。

        <strong>messages</strong>`array`<strong>（必选）</strong>

        传递给大模型的上下文，按对话顺序排列。

        <Accordion title="消息类型">
          User Message`object`<strong>（必选）</strong>

          用户消息，用于向模型传递指令和待识别的图像。

          <Accordion title="属性">
            <strong>content</strong>`array`<strong>（必选）</strong>

            消息内容。

            <Accordion title="属性">
              <strong>type</strong>`string`<strong>（必选）</strong>

              可选值：

              - `text`

                输入文本时需设为`text`。
              - `image_url`

                输入图片时需设为`image_url`。

              <strong>text</strong>`string`<strong>（可选）</strong>

              输入的文本。

              默认值为：`Please output only the text content from the image without any additional descriptions or formatting.` ，即模型默认提取图像中的全部文本。

              <strong>image\_url</strong>`object`

              输入的图片信息。当`type`为`image_url`时是必选参数。

              <Accordion title="属性">
                <strong>url</strong> `string`<strong>（必选）</strong>

                图片的 URL或 Base64 Data URL。传入本地文件请参考[文字提取](/zh/model-studio/qwen-vl-ocr#23d2494606vg6)。
              </Accordion>

              <strong>min\_pixels</strong>`integer`（可选）

              用于设定输入图像的最小像素阈值，单位为像素。

              当输入图像像素小于`min_pixels`时，会将图像进行放大，直到总像素高于`min_pixels`。

              <Accordion title="图像Token与像素的转换关系">
                不同模型，每个图像 Token 对应的像素不同：

                - `qwen3.5-ocr`、`qwen-vl-ocr-2025-11-20`：每 Token 对应像素为`32*32`。
                - `qwen-vl-ocr`及之前更新的模型：每 Token 对应像素为`28*28`。
              </Accordion>

              <Accordion title="min_pixels 取值范围">
                - `qwen3.5-ocr`、`qwen-vl-ocr-2025-11-20`：默认值和最小值均为3072（即`3×32×32`）
                - `qwen-vl-ocr`及之前更新的模型：默认值和最小值均为 `3136` （即`4×28×28`）。
              </Accordion>

              示例值：`{"type": "image_url","image_url": {"url":"https://xxxx.jpg"},"min_pixels": 3072}`

              <strong>max\_pixels</strong>`integer`（可选）

              用于设定输入图像的最大像素阈值，单位为像素。

              当输入图像像素在`[min_pixels, max_pixels]`区间内时，模型会按原图进行识别。当输入图像像素大于`max_pixels`时，会将图像进行缩小，直到总像素低于`max_pixels`。

              <Accordion title="图像Token与像素的转换关系">
                不同模型，每个图像 Token 对应的像素不同：

                - `qwen3.5-ocr`、`qwen-vl-ocr-2025-11-20`：每 Token 对应像素为`32*32`。
                - `qwen-vl-ocr`及之前更新的模型：每 Token 对应像素为`28*28`。
              </Accordion>

              <Accordion title="max_pixels 取值范围">
                - `qwen3.5-ocr、qwen-vl-ocr-2025-11-20`

                  - 默认值：8388608 （即`8192x32x32`）
                  - 最大值：30720000（即`30000x32x32`）
                - `qwen-vl-ocr`及之前更新的模型

                  - 默认值：6422528（即`8192x28x28`）
                  - 最大值：23520000（即`30000x28x28`）
              </Accordion>

              示例值：`{"type": "image_url","image_url": {"url":"https://xxxx.jpg"},"max_pixels": 8388608}`
            </Accordion>

            <strong>role</strong>`string`<strong>（必选）</strong>

            用户消息的角色，固定为`user`。
          </Accordion>
        </Accordion>

        <strong>stream</strong>`boolean`（可选） 默认值为 `false`

        是否以流式方式输出回复。

        可选值：

        - `false`：等待模型生成完整回复后一次性返回。
        - `true`：模型边生成边返回数据块。客户端需逐块读取，以还原完整回复。

        <strong>stream\_options</strong>`object`（可选）

        流式输出的配置项，仅在 `stream` 为 `true` 时生效。

        <Accordion title="属性" defaultOpen>
          <strong>include\_usage</strong>`boolean`（可选）默认值为 `false`

          是否在<strong>最后一个数据块</strong>包含Token消耗信息。

          可选值：

          - `true`：包含；
          - `false`：不包含。
        </Accordion>

        <strong>max\_tokens</strong>`integer`（可选）

        用于限制模型输出的最大 Token 数。若生成内容超过此值，响应将被截断。

        - `qwen3.5-ocr`：默认值与最大值为32768。
        - `qwen-vl-ocr-2025-11-20`、`qwen-vl-ocr-2024-10-28`默认值与最大值均为模型的最大输出长度，请参见[模型选型](/zh/model-studio/qwen-vl-ocr#0573ce495fnnf)。
        - `qwen-vl-ocr`，默认值和最大值为4096。

          > 如需提高该参数值（4097\~8192范围），请联系商务经理进行申请，并提供以下信息：主账号ID、图像类型（如文档图、电商图、合同等）、模型名称、预计 QPS 和每日请求总数，以及模型输出长度超过4096的请求占比。

        <strong>logprobs</strong> `boolean` （可选）默认值为 `false`

        是否返回输出 Token 的对数概率，可选值：

        - `true`

          返回
        - `false`

          不返回

        <strong>top\_logprobs</strong> `integer` （可选）默认值为0

        指定在每一步生成时，返回模型最大概率的候选 Token 个数。

        取值范围：\[0,5]

        仅当 `logprobs` 为 `true` 时生效。

        <strong>temperature</strong>`float`（可选）默认值为0.01

        采样温度，控制模型生成文本的多样性。

        temperature越高，生成的文本更多样，反之，生成的文本更确定。

        取值范围： \[0, 2)

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。

        > 建议设置为默认值即可。

        <strong>top\_p</strong>`float`（可选）默认值为0.001

        核采样的概率阈值，控制模型生成文本的多样性。

        top\_p越高，生成的文本更多样。反之，生成的文本更确定。

        取值范围：（0,1.0]

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。

        > 建议设置为默认值即可。

        <strong>top\_k</strong>`integer`（可选）默认值为1

        生成过程中采样候选集的大小。例如，取值为50时，仅将单次生成中得分最高的50个Token组成随机采样的候选集。取值越大，生成的随机性越高；取值越小，生成的确定性越高。取值为None或当top\_k大于100时，表示不启用top\_k策略，此时仅有top\_p策略生效。

        取值需要大于或等于0。

        该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中，配置方式为：`extra_body={"top_k": xxx}`；通过 Node.js SDK 或 HTTP 方式调用时，请作为顶层参数传递。

        > 建议设置为默认值即可。

        <strong>repetition\_penalty</strong>`float`（可选）默认值为1.0

        模型生成时连续序列中的重复度。提高repetition\_penalty时可以降低模型生成的重复度，1.0表示不做惩罚。该参数对模型效果影响较大，建议保持默认值。

        > 建议设置为默认值即可。

        <strong>presence\_penalty</strong> `float`（可选）默认值为0.0

        控制模型生成文本时的内容重复度。

        取值范围：\[-2.0, 2.0]。正值降低重复度，负值增加重复度。

        在创意写作或头脑风暴等需要多样性、趣味性或创造力的场景中，建议调高该值；在技术文档或正式文本等强调一致性与术语准确性的场景中，建议调低该值。

        <Accordion title="原理介绍">
          如果参数值是正数，模型将对目前文本中已存在的Token施加一个惩罚值（惩罚值与文本出现的次数无关），减少这些Token重复出现的几率，从而减少内容重复度，增加用词多样性。
        </Accordion>

        > 建议设置为默认值即可。

        <strong>seed</strong>`integer`（可选）

        随机数种子。用于确保在相同输入和参数下生成结果可复现。若调用时传入相同的 `seed` 且其他参数不变，模型将尽可能返回相同结果。

        取值范围：`[0,2 31 −1]`。

        > 建议设置为默认值即可。

        <strong>stop</strong>`string 或 array`（可选）

        用于指定停止词。当模型生成的文本中出现`stop` 指定的字符串或`token_id`时，生成将立即终止。

        可传入敏感词以控制模型的输出。

        > stop为数组时，不可将`token_id`和字符串同时作为元素输入，比如不可以指定为`["你好",104307]`。
      </td>

      <td>
        <Tabs>
          <Tab title="非流式输出">
            <CodeGroup dropdown>
              ```python Python expandable
              from openai import OpenAI
              import os

              PROMPT_TICKET_EXTRACTION = """
              请提取车票图像中的发票号码、车次、起始站、终点站、发车日期和时间点、座位号、席别类型、票价、身份证号码、购票人姓名。
              要求准确无误的提取上述关键信息、不要遗漏和捏造虚假信息，模糊或者强光遮挡的单个文字可以用英文问号?代替。
              返回数据格式以json方式输出，格式为：{'发票号码': 'xxx', '起始站': 'xxx', '终点站': 'xxx', '发车日期和时间点':'xxx', '座位号': 'xxx','票价':'xxx', '身份证号码': 'xxx', '购票人姓名': 'xxx'},
              """

              try:
                  client = OpenAI(
                      # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                      # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                      api_key=os.getenv("DASHSCOPE_API_KEY"),
                      # 以下为北京地域的 base_url，若使用弗吉尼亚地域模型，需要将base_url换成https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1
                      # 若使用新加坡地域的模型，需将base_url替换为：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1
                      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
                  )
                  completion = client.chat.completions.create(
                      model="qwen3.5-ocr",
                      messages=[
                          {
                              "role": "user",
                              "content": [
                                  {
                                      "type": "image_url",
                                      "image_url": {"url":"https://img.alicdn.com/imgextra/i2/O1CN01ktT8451iQutqReELT_!!6000000004408-0-tps-689-487.jpg"},
                                      # 输入图像的最小像素阈值，小于该值图像会放大，直到总像素大于min_pixels
                                      "min_pixels": 32 * 32 * 3,
                                      # 输入图像的最大像素阈值，超过该值图像会缩小，直到总像素低于max_pixels
                                      "max_pixels": 32 * 32 * 8192
                                  },
                                  # 模型支持在以下text字段中传入Prompt，若未传入，则会使用默认的Prompt：Please output only the text content from the image without any additional descriptions or formatting.
                                  {"type": "text",
                                   "text": PROMPT_TICKET_EXTRACTION}
                              ]
                          }
                      ])
                  print(completion.choices[0].message.content)
              except Exception as e:
                  print(f"错误信息: {e}")
              ```

              ```javascript Node.js expandable
              import OpenAI from 'openai';

              // 定义提取车票信息的Prompt
              const PROMPT_TICKET_EXTRACTION = `
              请提取车票图像中的发票号码、车次、起始站、终点站、发车日期和时间点、座位号、席别类型、票价、身份证号码、购票人姓名。
              要求准确无误的提取上述关键信息、不要遗漏和捏造虚假信息，模糊或者强光遮挡的单个文字可以用英文问号?代替。
              返回数据格式以json方式输出，格式为：{'发票号码': 'xxx', '起始站': 'xxx', '终点站': 'xxx', '发车日期和时间点':'xxx', '座位号': 'xxx','票价':'xxx', '身份证号码': 'xxx', '购票人姓名': 'xxx'}
              `;

              // 初始化OpenAI客户端
              const client = new OpenAI({
                  // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",
                 // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  apiKey: process.env.DASHSCOPE_API_KEY,
                 // 以下为北京地域的 base_url，若使用弗吉尼亚地域模型，需要将base_url换成https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1
                 // 若使用新加坡地域的模型，需将base_url替换为：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              });

              async function main() {
                  try {
                      // 创建聊天完成请求
                      const completion = await client.chat.completions.create({
                          model: "qwen3.5-ocr",
                          messages: [
                              {
                                  role: "user",
                                  content: [
                                      // 模型支持在text字段中传入Prompt，若未传入，则会使用默认的Prompt：Please output only the text content from the image without any additional descriptions or formatting.
                                      {
                                          type: "image_url",
                                          image_url: {
                                              url: "https://img.alicdn.com/imgextra/i2/O1CN01ktT8451iQutqReELT_!!6000000004408-0-tps-689-487.jpg",
                                          },
                                          // 输入图像的最小像素阈值，小于该值图像会放大，直到总像素大于min_pixels
                                          min_pixels: 32 * 32 * 3,
                                          // 输入图像的最大像素阈值，超过该值图像会缩小，直到总像素低于max_pixels
                                          max_pixels: 32 * 32 * 8192
                                      },
                                      {type: "text",
                                       text: PROMPT_TICKET_EXTRACTION}
                                  ]
                              }
                          ]
                      });

                      // 输出结果
                      console.log(completion.choices[0].message.content);
                  } catch (error) {
                      console.log(`错误信息: ${error}`);
                  }
              }

              main();
              ```

              ```bash curl expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下是北京地域base_url，若使用弗吉尼亚地域模型，需要将base_url换成https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions
              # 如果使用新加坡地域的模型，需要将base_url替换为：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions
              # === 执行时请删除该注释 ===

              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                "model": "qwen3.5-ocr",
                "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "type": "image_url",
                                  "image_url": {"url":"https://img.alicdn.com/imgextra/i2/O1CN01ktT8451iQutqReELT_!!6000000004408-0-tps-689-487.jpg"},
                                  "min_pixels": 3072,
                                  "max_pixels": 8388608
                              },
                              {"type": "text", "text": "请提取车票图像中的发票号码、车次、起始站、终点站、发车日期和时间点、座位号、席别类型、票价、身份证号码、购票人姓名。要求准确无误的提取上述关键信息、不要遗漏和捏造虚假信息，模糊或者强光遮挡的单个文字可以用英文问号?代替。返回数据格式以json方式输出，格式为：{'发票号码': 'xxx', '起始站': 'xxx', '终点站': 'xxx', '发车日期和时间点':'xxx', '座位号': 'xxx','票价':'xxx', '身份证号码': 'xxx', '购票人姓名': 'xxx'"}
                          ]
                      }
                  ]
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="流式输出">
            <CodeGroup dropdown>
              ```python Python expandable
              import os
              from openai import OpenAI

              PROMPT_TICKET_EXTRACTION = """
              请提取车票图像中的发票号码、车次、起始站、终点站、发车日期和时间点、座位号、席别类型、票价、身份证号码、购票人姓名。
              要求准确无误的提取上述关键信息、不要遗漏和捏造虚假信息，模糊或者强光遮挡的单个文字可以用英文问号?代替。
              返回数据格式以json方式输出，格式为：{'发票号码': 'xxx', '起始站': 'xxx', '终点站': 'xxx', '发车日期和时间点':'xxx', '座位号': 'xxx','票价':'xxx', '身份证号码': 'xxx', '购票人姓名': 'xxx'},
              """

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  # 以下是北京地域base-url，如果使用新加坡地域的模型，需要将base_url替换为：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )
              completion = client.chat.completions.create(
                  model="qwen3.5-ocr",
                  messages=[
                      {
                          "role": "user",
                          "content": [
                              {
                                  "type": "image_url",
                                  "image_url": {"url":"https://img.alicdn.com/imgextra/i2/O1CN01ktT8451iQutqReELT_!!6000000004408-0-tps-689-487.jpg"},
                                  # 输入图像的最小像素阈值，小于该值图像会放大，直到总像素大于min_pixels
                                  "min_pixels": 32 * 32 * 3,
                                  # 输入图像的最大像素阈值，超过该值图像会缩小，直到总像素低于max_pixels
                                  "max_pixels": 32 * 32 * 8192
                              },
                                # 模型支持在text字段中传入Prompt，若未传入，则会使用默认的Prompt：Please output only the text content from the image without any additional descriptions or formatting.
                              {"type": "text","text": PROMPT_TICKET_EXTRACTION}

                          ]
                      }
                  ],
                  stream=True,
                  stream_options={"include_usage": True}
              )
              for chunk in completion:
                  print(chunk.model_dump_json())
              ```

              ```javascript Node.js expandable
              import OpenAI from 'openai';

              // 定义提取车票信息的Prompt
              const PROMPT_TICKET_EXTRACTION = `
              请提取车票图像中的发票号码、车次、起始站、终点站、发车日期和时间点、座位号、席别类型、票价、身份证号码、购票人姓名。
              要求准确无误的提取上述关键信息、不要遗漏和捏造虚假信息，模糊或者强光遮挡的单个文字可以用英文问号?代替。
              返回数据格式以json方式输出，格式为：{'发票号码': 'xxx','起始站': 'xxx', '终点站': 'xxx', '发车日期和时间点':'xxx', '座位号': 'xxx','票价':'xxx', '身份证号码': 'xxx', '购票人姓名': 'xxx'}
              `;

              const openai = new OpenAI({
                // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",
                // 以下是北京地域base-url，如果使用新加坡地域的模型，需要将base_url替换为：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1
                apiKey: process.env.DASHSCOPE_API_KEY,
                 // 以下是北京地域base-url，如果使用新加坡地域的模型，需要将base_url替换为：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1
                baseURL: 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1',
              });

              async function main() {
                const response = await openai.chat.completions.create({
                  model: 'qwen3.5-ocr',
                  messages: [
                    {
                      role: 'user',
                      content: [
                        // 模型支持在text字段中传入Prompt，若未传入，则会使用默认的Prompt：Please output only the text content from the image without any additional descriptions or formatting.
                        { type: 'text', text: PROMPT_TICKET_EXTRACTION},
                        {
                          type: 'image_url',
                          image_url: {
                            url: 'https://img.alicdn.com/imgextra/i2/O1CN01ktT8451iQutqReELT_!!6000000004408-0-tps-689-487.jpg',
                          },
                            //  输入图像的最小像素阈值，小于该值图像会放大，直到总像素大于min_pixels
                            "min_pixels": 32 * 32 * 3,
                            // 输入图像的最大像素阈值，超过该值图像会缩小，直到总像素低于max_pixels
                            "max_pixels": 32 * 32 * 8192
                        }
                      ]
                    }
                  ],
                  stream: true,
                  stream_options:{"include_usage": true}
                });
              let fullContent = ""
                console.log("流式输出内容为：")
                for await (const chunk of response) {
                  if (chunk.choices[0] && chunk.choices[0].delta.content != null) {
                    fullContent += chunk.choices[0].delta.content;
                    console.log(chunk.choices[0].delta.content);
                  }
              }
                console.log(`完整输出内容为：${fullContent}`)
              }

              main();
              ```

              ```bash curl expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下是北京地域base_url，若使用弗吉尼亚地域模型，需要将base_url换成https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions
              # 如果使用新加坡地域的模型，需要将base_url替换为：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions
              # === 执行时请删除该注释 ===

              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                "model": "qwen3.5-ocr",
                "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "type": "image_url",
                                  "image_url": {"url":"https://img.alicdn.com/imgextra/i2/O1CN01ktT8451iQutqReELT_!!6000000004408-0-tps-689-487.jpg"},
                                  "min_pixels": 3072,
                                  "max_pixels": 8388608
                              },
                              {"type": "text", "text": "请提取车票图像中的发票号码、车次、起始站、终点站、发车日期和时间点、座位号、席别类型、票价、身份证号码、购票人姓名。要求准确无误的提取上述关键信息、不要遗漏和捏造虚假信息，模糊或者强光遮挡的单个文字可以用英文问号?代替。返回数据格式以json方式输出，格式为：{'发票号码': 'xxx', '起始站': 'xxx', '终点站': 'xxx', '发车日期和时间点':'xxx', '座位号': 'xxx','票价':'xxx', '身份证号码': 'xxx', '购票人姓名': 'xxx'"}
                          ]
                      }
                  ],
                  "stream": true,
                  "stream_options": {"include_usage": true}
              }'
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
        ### chat响应对象（非流式输出） <span id="cd3063363egdc" />

        <strong>id</strong>`string`

        本次请求的唯一标识符。

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

            大模型的返回结果。

            <strong>processed\_text</strong> `string`

            对模型原始输出进行后处理的结果，自动删除重复片段等。当模型输出存在重复内容时，该字段提供清洗后的文本。

            > 仅通过 DashScope SDK 和 curl 调用时返回，OpenAI 兼容 SDK 不返回该字段。

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

        <strong>created</strong>`integer`

        本次请求被创建时的时间戳。

        <strong>model</strong>`string`

        本次请求使用的模型。

        <strong>object</strong> `string`

        始终为`chat.completion`。

        <strong>service\_tier</strong> `string`

        该参数当前固定为`null`。

        <strong>system\_fingerprint</strong>`string`

        该参数当前固定为`null`。

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

          模型输出Token的细粒度分类。

          <Accordion title="属性">
            <strong>accepted\_prediction\_tokens</strong>`integer`

            该参数当前固定为`null`。

            <strong>audio\_tokens</strong> `integer`

            该参数当前固定为`null`。

            <strong>reasoning\_tokens</strong> `integer`

            该参数当前固定为`null`。

            <strong>text\_tokens</strong> `integer`

            模型输出文本对应的 Token 数。

            <strong>rejected\_prediction\_tokens</strong>`integer`

            该参数当前固定为`null`。
          </Accordion>

          <strong>prompt\_tokens\_details</strong> `object`

          输入 Token 的细粒度分类。

          <Accordion title="属性">
            <strong>audio\_tokens</strong> `integer`

            该参数当前固定为`null`。

            <strong>cached\_tokens</strong> `integer`

            该参数当前固定为`null`。

            <strong>text\_tokens</strong> `integer`

            模型输入的文本对应的Token 数。

            <strong>image\_tokens</strong> `integer`

            模型输入的图像对应的 Token数。
          </Accordion>
        </Accordion>
      </td>

      <td>
        ````json expandable
        {
          "id": "chatcmpl-ba21fa91-dcd6-4dad-90cc-6d49c3c39094",
          "choices": [
            {
              "finish_reason": "stop",
              "index": 0,
              "logprobs": null,
              "message": {
                "content": "```json\n{\n    \"销售方名称\": \"null\",\n    \"购买方名称\": \"蔡应时\",\n    \"不含税价\": \"230769.23\",\n    \"组织机构代码\": \"null\",\n    \"发票代码\": \"142011726001\"\n}\n```",
                "refusal": null,
                "role": "assistant",
                "annotations": null,
                "audio": null,
                "function_call": null,
                "tool_calls": null
              }
            }
          ],
          "created": 1763283287,
          "model": "qwen3.5-ocr",
          "object": "chat.completion",
          "service_tier": null,
          "system_fingerprint": null,
          "usage": {
            "completion_tokens": 72,
            "prompt_tokens": 1185,
            "total_tokens": 1257,
            "completion_tokens_details": {
              "accepted_prediction_tokens": null,
              "audio_tokens": null,
              "reasoning_tokens": null,
              "rejected_prediction_tokens": null,
              "text_tokens": 72
            },
            "prompt_tokens_details": {
              "audio_tokens": null,
              "cached_tokens": null,
              "image_tokens": 1001,
              "text_tokens": 184
            }
          }
        }
        ````
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
        ### chat响应chunk对象（流式输出） <span id="e98e6b7aa5nb2" />

        <strong>id</strong>`string`

        本次调用的唯一标识符。每个chunk对象有相同的 id。

        <strong>choices</strong>`array`

        模型生成内容的数组。若设置`include_usage`参数为`true`，则在最后一个chunk中为空。

        <Accordion title="属性" defaultOpen>
          <strong>delta</strong> `object`

          流式返回的输出内容。

          <Accordion title="属性" defaultOpen>
            <strong>content</strong> `string`

            大模型的返回结果。

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

        <strong>created</strong>`integer`

        本次请求被创建时的时间戳。每个chunk有相同的时间戳。

        <strong>model</strong>`string`

        本次请求使用的模型。

        <strong>object</strong> `string`

        始终为`chat.completion.chunk`。

        <strong>service\_tier</strong> `string`

        该参数当前固定为`null`。

        <strong>system\_fingerprint</strong>`string`

        该参数当前固定为`null`。

        <strong>usage</strong> `object`

        本次请求消耗的Token。只在`include_usage`为`true`时，在最后一个chunk返回。

        <Accordion title="属性">
          <strong>completion\_tokens</strong> `integer`

          模型输出的 Token 数。

          <strong>prompt\_tokens</strong> `integer`

          输入的 Token 数。

          <strong>total\_tokens</strong> `integer`

          消耗的总 Token 数，为`prompt_tokens`与`completion_tokens`的总和。

          <strong>completion\_tokens\_details</strong> `object`

          模型输出Token的细粒度分类。

          <Accordion title="属性">
            <strong>accepted\_prediction\_tokens</strong>`integer`

            该参数当前固定为`null`。

            <strong>audio\_tokens</strong> `integer`

            该参数当前固定为`null`。

            <strong>reasoning\_tokens</strong> `integer`

            该参数当前固定为`null`。

            <strong>text\_tokens</strong> `integer`

            模型输出文本对应的 Token 数。

            <strong>rejected\_prediction\_tokens</strong>`integer`

            该参数当前固定为`null`。
          </Accordion>

          <strong>prompt\_tokens\_details</strong> `object`

          输入 Token 的细粒度分类。

          <Accordion title="属性">
            <strong>audio\_tokens</strong> `integer`

            该参数当前固定为`null`。

            <strong>cached\_tokens</strong> `integer`

            该参数当前固定为`null`。

            <strong>text\_tokens</strong> `integer`

            模型输入的文本对应的Token 数。

            <strong>image\_tokens</strong> `integer`

            模型输入的图像对应的 Token数。
          </Accordion>
        </Accordion>
      </td>

      <td>
        ````json
        {"id":"chatcmpl-f6fbdc0d-78d6-418f-856f-f099c2e4859b","choices":[{"delta":{"content":"","function_call":null,"refusal":null,"role":"assistant","tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1764139204,"model":"qwen3.5-ocr","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-f6fbdc0d-78d6-418f-856f-f099c2e4859b","choices":[{"delta":{"content":"```","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1764139204,"model":"qwen3.5-ocr","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-f6fbdc0d-78d6-418f-856f-f099c2e4859b","choices":[{"delta":{"content":"json","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1764139204,"model":"qwen3.5-ocr","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-f6fbdc0d-78d6-418f-856f-f099c2e4859b","choices":[{"delta":{"content":"\n","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1764139204,"model":"qwen3.5-ocr","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-f6fbdc0d-78d6-418f-856f-f099c2e4859b","choices":[{"delta":{"content":"{\n","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1764139204,"model":"qwen3.5-ocr","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-f6fbdc0d-78d6-418f-856f-f099c2e4859b","choices":[{"delta":{"content":"   ","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1764139204,"model":"qwen3.5-ocr","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        ......
        {"id":"chatcmpl-f6fbdc0d-78d6-418f-856f-f099c2e4859b","choices":[{"delta":{"content":"```","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1764139204,"model":"qwen3.5-ocr","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-f6fbdc0d-78d6-418f-856f-f099c2e4859b","choices":[{"delta":{"content":"","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":"stop","index":0,"logprobs":null}],"created":1764139204,"model":"qwen3.5-ocr","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-f6fbdc0d-78d6-418f-856f-f099c2e4859b","choices":[],"created":1764139204,"model":"qwen3.5-ocr","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":{"completion_tokens":141,"prompt_tokens":513,"total_tokens":654,"completion_tokens_details":{"accepted_prediction_tokens":null,"audio_tokens":null,"reasoning_tokens":null,"rejected_prediction_tokens":null,"text_tokens":141},"prompt_tokens_details":{"audio_tokens":null,"cached_tokens":null,"image_tokens":332,"text_tokens":181}}}
        ````
      </td>
    </tr>
  </tbody>
</table>

## DashScope <span id="69cac67a477k2" />

<Tabs>
  <Tab title="华北2（北京）地域">
    HTTP 调用配置的`endpoint`：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    SDK 调用无需配置 `base_url`。
  </Tab>

  <Tab title="新加坡地域">
    HTTP 调用配置的`endpoint`：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

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
        MultiModalConversation conv = new MultiModalConversation(Protocol.HTTP.getValue(), "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1");
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
    HTTP 调用配置的`endpoint`：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

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
        MultiModalConversation conv = new MultiModalConversation(Protocol.HTTP.getValue(), "https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1");
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

> 您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。若通过DashScope SDK进行调用，需要[安装DashScope SDK](/zh/model-studio/install-sdk)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="2a1c410015otp" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。支持的模型可参见`选择模型`。

        <strong>messages</strong>`array`<strong>（必选）</strong>

        传递给大模型的上下文，按对话顺序排列。

        > 通过HTTP调用时，请将<strong>messages</strong>放入 <strong>input</strong> 对象中。

        <Accordion title="消息类型">
          User Message`object`<strong>（必选）</strong>

          用户消息，用于向模型传递问题、指令或上下文等。

          <Accordion title="属性">
            <strong>content</strong>`string 或 array`<strong>（必选）</strong>

            消息内容。若输入只有文本，则为 string 类型；若输入包含图像数据，则为 array 类型。

            <Accordion title="属性">
              <strong>text</strong>`string`<strong>（可选）</strong>

              输入的文本。

              默认值为：`Please output only the text content from the image without any additional descriptions or formatting.` ，即模型默认提取图像中的全部文本。

              <strong>image</strong>`string`（可选）

              图片的URL、 Base64 Data URL、或本地路径。传入本地文件请参见[传入本地文件](/zh/model-studio/qwen-vl-ocr#23d2494606vg6)。

              示例值：`{"image":"https://xxxx.jpeg"}`

              <strong>enable\_rotate</strong>`boolean`（可选）默认值为`false`

              是否对倾斜的图像进行校正处理。

              可选值：

              - `true`：自动校正
              - `false`：不进行校正

              示例值：`{"image":"https://xxxx.jpeg","enable_rotate": True}`

              <strong>min\_pixels</strong>`integer`（可选）

              用于设定输入图像的最小像素阈值，单位为像素。

              当输入图像像素小于`min_pixels`时，会将图像进行放大，直到总像素高于`min_pixels`。

              <Accordion title="图像Token与像素的转换关系">
                不同模型，每个图像 Token 对应的像素不同：

                - `qwen3.5-ocr`、`qwen-vl-ocr-2025-11-20`：每 Token 对应像素为`32*32`。
                - `qwen-vl-ocr`及之前更新的模型：每 Token 对应像素为`28*28`。
              </Accordion>

              <Accordion title="min_pixels 取值范围">
                - `qwen3.5-ocr`、`qwen-vl-ocr-2025-11-20`：默认值和最小值均为3072（即`3×32×32`）
                - `qwen-vl-ocr`及之前更新的模型：默认值和最小值均为 `3136` （即`4×28×28`）。
              </Accordion>

              示例值：`{"image":"https://xxxx.jpeg","min_pixels": 3072}`

              <strong>max\_pixels</strong>`integer`（可选）

              用于设定输入图像的最大像素阈值，单位为像素。

              当输入图像像素在`[min_pixels, max_pixels]`区间内时，模型会按原图进行识别。当输入图像像素大于`max_pixels`时，会将图像进行缩小，直到总像素低于`max_pixels`。

              <Accordion title="图像Token与像素的转换关系">
                不同模型，每个图像 Token 对应的像素不同：

                - `qwen3.5-ocr`、`qwen-vl-ocr-2025-11-20`：每 Token 对应像素为`32*32`。
                - `qwen-vl-ocr`及之前更新的模型：每 Token 对应像素为`28*28`。
              </Accordion>

              <Accordion title="max_pixels 取值范围">
                - `qwen3.5-ocr、qwen-vl-ocr-2025-11-20`

                  - 默认值：8388608 （即`8192x32x32`）
                  - 最大值：30720000（即`30000x32x32`）
                - `qwen-vl-ocr`及之前更新的模型

                  - 默认值：6422528（即`8192x28x28`）
                  - 最大值：23520000（即`30000x28x28`）
              </Accordion>

              示例值：`{"image":"https://xxxx.jpeg","max_pixels": 8388608}`
            </Accordion>

            <strong>role</strong>`string`<strong>（必选）</strong>

            用户消息的角色，固定为`user`。
          </Accordion>
        </Accordion>

        <strong>max\_tokens</strong>`integer`（可选）

        用于限制模型输出的最大 Token 数。若生成内容超过此值，响应将被截断。

        - `qwen3.5-ocr`：默认值与最大值为32768。
        - `qwen-vl-ocr-2025-11-20`、`qwen-vl-ocr-2024-10-28`默认值与最大值均为模型的最大输出长度，请参见[模型选型](/zh/model-studio/qwen-vl-ocr#0573ce495fnnf)。
        - `qwen-vl-ocr`，默认值和最大值为4096。

          > 如需提高该参数值（4097\~8192范围），请联系商务经理进行申请，并提供以下信息：主账号ID、图像类型（如文档图、电商图、合同等）、模型名称、预计 QPS 和每日请求总数，以及模型输出长度超过4096的请求占比。

        > Java SDK中为<strong>maxTokens</strong>\*。\*通过HTTP调用时，请将 <strong>max\_tokens</strong>放入 <strong>parameters</strong> 对象中。

        <strong>ocr\_options</strong>*object*（可选）

        使用通义千问OCR模型[调用内置任务](/zh/model-studio/qwen-vl-ocr#3ea70c9a1a4lz)时需要配置的参数。调用内置任务时，无需传入`User Message`，模型内部会采用对应任务的`Prompt`。相关章节：[调用内置任务](/zh/model-studio/qwen-vl-ocr#3ea70c9a1a4lz)。

        <Accordion title="属性">
          <strong>task</strong> `string` （必选）

          内置任务的名称，可选值如下：

          - `text_recognition`：通用文字识别
          - `key_information_extraction`：信息抽取
          - `document_parsing`：文档解析
          - `table_parsing`：表格解析
          - `formula_recognition`：公式识别
          - `multi_lan`：多语言识别
          - `advanced_recognition`：高精识别

          <strong>task\_config</strong> `object` （可选）

          当`task`的取值为`key_information_extraction`（信息抽取）时，此参数用于指定需抽取的特定字段。如未指定 `task_config`，模型将默认提取图像中的所有字段。

          <Accordion title="属性">
            <strong>result\_schema</strong>`object` （可选）

            表示需要模型抽取的字段，应为JSON对象结构，最多可嵌套3层JSON 对象。

            在JSON对象的键（`key`）中指定待抽取字段的名称，对应的值（`value`）可为空，建议在值中提供字段描述或格式要求，可提高信息提取的准确率。

            示例值：

            ```json
            "result_schema": {
                 "发票号码": "发票的唯一识别编号，通常为数字和字母的组合。",
                 "开票日期": "发票开具的日期，请以YYYY-MM-DD格式提取，例如2023-10-26。",
                 "销售方名称": "发票上显示的销售方公司全称。",
                 "总金额": "发票中包含税费的总计金额，要求提取数值并保留两位小数，例如123.45。"
            }
            ```
          </Accordion>
        </Accordion>

        > Java SDK为<strong>OcrOptions</strong>，DashScope Python SDK 最低版本为1.22.2， Java SDK 最低版本为2.18.4。

        > 通过HTTP调用时，请将 <strong>ocr\_options</strong>放入 <strong>parameters</strong> 对象中。

        <strong>seed</strong>`integer`（可选）

        随机数种子。用于确保在相同输入和参数下生成结果可复现。若调用时传入相同的 `seed` 且其他参数不变，模型将尽可能返回相同结果。

        取值范围：`[0,2 31 −1]`。

        > 建议设置为默认值即可。

        > 通过HTTP调用时，请将 <strong>seed</strong>放入 <strong>parameters</strong> 对象中。

        <strong>temperature</strong>`float`（可选）默认值为0.01

        采样温度，控制模型生成文本的多样性。

        temperature越高，生成的文本更多样，反之，生成的文本更确定。

        取值范围： \[0, 2)

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。

        > 建议设置为默认值即可。

        > 通过HTTP调用时，请将 <strong>temperature</strong>放入 <strong>parameters</strong> 对象中。

        <strong>top\_p</strong>`float`（可选）默认值为0.001

        核采样的概率阈值，控制模型生成文本的多样性。

        top\_p越高，生成的文本更多样。反之，生成的文本更确定。

        取值范围：（0,1.0]

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。

        > 建议设置为默认值即可。

        > Java SDK中为<strong>topP</strong>\*。\*通过HTTP调用时，请将 <strong>top\_p</strong>放入 <strong>parameters</strong> 对象中。

        <strong>top\_k</strong>`integer`（可选）默认值为1

        生成过程中采样候选集的大小。例如，取值为50时，仅将单次生成中得分最高的50个Token组成随机采样的候选集。取值越大，生成的随机性越高；取值越小，生成的确定性越高。取值为None或当top\_k大于100时，表示不启用top\_k策略，此时仅有top\_p策略生效。

        取值需要大于或等于0。

        该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中，配置方式为：`extra_body={"top_k": xxx}`；通过 Node.js SDK 或 HTTP 方式调用时，请作为顶层参数传递。

        > 建议设置为默认值即可。

        <strong>repetition\_penalty</strong>`float`（可选）默认值为1.0

        模型生成时连续序列中的重复度。提高repetition\_penalty时可以降低模型生成的重复度，1.0表示不做惩罚。该参数对模型效果影响较大，建议保持默认值。

        > 建议设置为默认值即可。

        > Java SDK中为<strong>repetitionPenalty</strong>\*。\*通过HTTP调用时，请将 <strong>repetition\_penalty</strong>放入 <strong>parameters</strong> 对象中。

        <strong>presence\_penalty</strong> `float`（可选）默认值为0.0

        控制模型生成文本时的内容重复度。

        取值范围：\[-2.0, 2.0]。正值降低重复度，负值增加重复度。

        在创意写作或头脑风暴等需要多样性、趣味性或创造力的场景中，建议调高该值；在技术文档或正式文本等强调一致性与术语准确性的场景中，建议调低该值。

        <Accordion title="原理介绍">
          如果参数值是正数，模型将对目前文本中已存在的Token施加一个惩罚值（惩罚值与文本出现的次数无关），减少这些Token重复出现的几率，从而减少内容重复度，增加用词多样性。
        </Accordion>

        > 建议设置为默认值即可。

        <strong>stream</strong>`boolean`（可选）默认值为`false`

        是否流式输出回复。参数值：

        - false：模型生成完所有内容后一次性返回结果。
        - true：边生成边输出，即每生成一部分内容就立即输出一个片段（chunk）。

        > 该参数仅支持Python SDK。通过Java SDK实现流式输出请通过`streamCall`接口调用；通过HTTP实现流式输出请在Header中指定`X-DashScope-SSE`为`enable`。

        <strong>incremental\_output</strong>`boolean`（可选）默认为`false`

        在流式输出模式下是否开启增量输出。推荐您优先设置为`true`。

        参数值：

        - false：每次输出为当前已经生成的整个序列，最后一次输出为生成的完整结果。

        ```plaintext
        I
        I like
        I like apple
        I like apple.
        ```

        - true（推荐）：增量输出，即后续输出内容不包含已输出的内容。您需要实时地逐个读取这些片段以获得完整的结果。

        ```plaintext
        I
        like
        apple
        .
        ```

        > Java SDK中为<strong>incrementalOutput</strong>\*。\*通过HTTP调用时，请将 <strong>incremental\_output</strong>放入 <strong>parameters</strong> 对象中。

        <strong>stop</strong>`string 或 array`（可选）

        用于指定停止词。当模型生成的文本中出现`stop` 指定的字符串或`token_id`时，生成将立即终止。

        可传入敏感词以控制模型的输出。

        > stop为数组时，不可将`token_id`和字符串同时作为元素输入，比如不可以指定为`["你好",104307]`。

        <strong>logprobs</strong> `boolean` （可选）默认值为 `false`

        是否返回输出 Token 的对数概率，可选值：

        - `true`

          返回
        - `false`

          不返回

        支持的模型：qwen-vl-ocr-2025-11-20及之后更新的模型

        > 通过HTTP调用时，请将 <strong>logprobs</strong>放入 <strong>parameters</strong> 对象中。

        <strong>top\_logprobs</strong> `integer` （可选）默认值为0

        指定在每一步生成时，返回模型最大概率的候选 Token 个数。仅当 `logprobs` 为 `true` 时生效。

        取值范围：\[0,5]

        > Java SDK中为<strong>topLogprobs</strong>\*。\*通过HTTP调用时，请将 <strong>top\_logprobs</strong>放入 <strong>parameters</strong> 对象中。
      </td>

      <td>
        <Tabs>
          <Tab title="高精识别">
            > 以下为调用高精识别内置任务的代码示例，详情请参见[调用内置任务](/zh/model-studio/qwen-vl-ocr#3ea70c9a1a4lz)。

            <CodeGroup dropdown>
              ```python expandable
              import os
              import dashscope

              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              messages = [{
                          "role": "user",
                          "content": [{
                              "image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg",
                              # 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                              "min_pixels": 32 * 32 * 3,
                              # 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                              "max_pixels": 32 * 32 * 8192,
                              # 是否开启图像自动转正功能
                              "enable_rotate": False}]
                          }]

              response = dashscope.MultiModalConversation.call(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx",
                  # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  api_key=os.getenv('DASHSCOPE_API_KEY'),
                  model='qwen3.5-ocr',
                  messages=messages,
                  # 设置内置任务为高精识别
                  ocr_options={"task": "advanced_recognition"}
              )
              # 高精识别任务的文本与坐标结果从 ocr_result 字段中获取
              print(response["output"]["choices"][0]["message"].content[0]["ocr_result"])
              ```

              ```java expandable
              // dashscope SDK的版本 >= 2.21.8
              import java.util.Arrays;
              import java.util.Collections;
              import java.util.Map;
              import java.util.HashMap;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.aigc.multimodalconversation.OcrOptions;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {

                  // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
                  static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      MultiModalConversation conv = new MultiModalConversation();
                      Map<String, Object> map = new HashMap<>();
                      map.put("image", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg");
                      // 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                      map.put("max_pixels", 8388608);
                      // 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                      map.put("min_pixels", 3072);
                      // 是否开启图像自动转正功能
                      map.put("enable_rotate", false);
                      // 配置内置的OCR任务
                      OcrOptions ocrOptions = OcrOptions.builder()
                              .task(OcrOptions.Task.ADVANCED_RECOGNITION)
                              .build();
                      MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      map
                                      )).build();
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                               // 若没有配置环境变量，请用百炼API Key将下行替换为：.apiKey("sk-xxx")
                              // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3.5-ocr")
                              .message(userMessage)
                              .ocrOptions(ocrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent().get(0).get("ocr_result"));
                  }

                  public static void main(String[] args) {
                      try {
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```

              ```bash expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              # === 执行时请删除该注释 ===

              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '
              {
                "model": "qwen3.5-ocr",
                "input": {
                  "messages": [
                    {
                      "role": "user",
                      "content": [
                        {
                          "image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg",
                          "min_pixels": 3072,
                          "max_pixels": 8388608,
                          "enable_rotate": false
                        }
                      ]
                    }
                  ]
                },
                "parameters": {
                  "ocr_options": {
                    "task": "advanced_recognition"
                  }
                }
              }
              '
              ```
            </CodeGroup>
          </Tab>

          <Tab title="信息抽取">
            > 以下为调用信息抽取内置任务的代码示例，详情请参见[调用内置任务](/zh/model-studio/qwen-vl-ocr#3ea70c9a1a4lz)。

            <CodeGroup dropdown>
              ```python expandable
              # use [pip install -U dashscope] to update sdk

              import os
              import dashscope
              from dashscope import MultiModalConversation

              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              messages = [
                    {
                      "role":"user",
                      "content":[
                        {
                            "image":"http://duguang-labelling.oss-cn-shanghai.aliyuncs.com/demo_ocr/receipt_zh_demo.jpg",
                            "min_pixels": 32 * 32 * 3,
                            "max_pixels": 32 * 32 *8192,
                            "enable_rotate": False
                        }
                      ]
                    }
                  ]

              # 指定抽取字段
              params = {
                "ocr_options":{
                  "task": "key_information_extraction",
                  "task_config": {
                    "result_schema": {
                        "乘车日期": "对应图中乘车日期时间，格式为年-月-日，比如2025-03-05",
                        "发票代码": "提取图中的发票代码，通常为一组数字或字母组合",
                        "发票号码": "提取发票上的号码，通常由纯数字组成。"
                    }
                  }
                }
              }

              response = MultiModalConversation.call(
                  model='qwen3.5-ocr',
                  messages=messages,
                  **params,
                  # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  api_key=os.getenv('DASHSCOPE_API_KEY'))

              print(response.output.choices[0].message.content[0]["ocr_result"])
              ```

              ```java expandable
              import java.util.Arrays;
              import java.util.Collections;
              import java.util.Map;
              import java.util.HashMap;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.aigc.multimodalconversation.OcrOptions;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.google.gson.JsonObject;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {

                  // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
                  static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      MultiModalConversation conv = new MultiModalConversation();
                      Map<String, Object> map = new HashMap<>();
                      map.put("image", "http://duguang-labelling.oss-cn-shanghai.aliyuncs.com/demo_ocr/receipt_zh_demo.jpg");
                      // 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                      map.put("max_pixels", 8388608);
                      // 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                      map.put("min_pixels",3072);
                      // 是否开启图像自动转正功能
                      map.put("enable_rotate", false);

                      MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      map
                                      )).build();

                      // 创建主JSON对象
                      JsonObject resultSchema = new JsonObject();
                      resultSchema.addProperty("乘车日期", "对应图中乘车日期时间，格式为年-月-日，比如2025-03-05");
                      resultSchema.addProperty("发票代码", "提取图中的发票代码，通常为一组数字或字母组合");
                      resultSchema.addProperty("发票号码", "提取发票上的号码，通常由纯数字组成。");

                      // 配置内置的OCR任务
                      OcrOptions ocrOptions = OcrOptions.builder()
                              .task(OcrOptions.Task.KEY_INFORMATION_EXTRACTION)
                              .taskConfig(OcrOptions.TaskConfig.builder()
                                      .resultSchema(resultSchema)
                                      .build())
                              .build();

                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 若没有配置环境变量，请用百炼API Key将下行替换为：.apiKey("sk-xxx")
                             // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3.5-ocr")
                              .message(userMessage)
                              .ocrOptions(ocrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent().get(0).get("ocr_result"));
                  }

                  public static void main(String[] args) {
                      try {
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```

              ```bash expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              # === 执行时请删除该注释 ===

              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '
              {
                "model": "qwen3.5-ocr",
                "input": {
                  "messages": [
                    {
                      "role": "user",
                      "content": [
                        {
                          "image": "http://duguang-labelling.oss-cn-shanghai.aliyuncs.com/demo_ocr/receipt_zh_demo.jpg",
                          "min_pixels": 3072,
                          "max_pixels": 8388608,
                          "enable_rotate": false
                        }
                      ]
                    }
                  ]
                },
                "parameters": {
                  "ocr_options": {
                    "task": "key_information_extraction",
                    "task_config": {
                      "result_schema": {
                          "乘车日期": "对应图中乘车日期时间，格式为年-月-日，比如2025-03-05",
                          "发票代码": "提取图中的发票代码，通常为一组数字或字母组合",
                          "发票号码": "提取发票上的号码，通常由纯数字组成。"
                      }
                  }
                  }
                }
              }
              '
              ```
            </CodeGroup>
          </Tab>

          <Tab title="表格解析">
            > 以下为调用表格解析内置任务的代码示例，详情请参见[调用内置任务](/zh/model-studio/qwen-vl-ocr#3ea70c9a1a4lz)。

            <CodeGroup dropdown>
              ```python expandable
              import os
              import dashscope

              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              messages = [{
                          "role": "user",
                          "content": [{
                              "image": "http://duguang-llm.oss-cn-hangzhou.aliyuncs.com/llm_data_keeper/data/doc_parsing/tables/photo/eng/17.jpg",
                              # 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                              "min_pixels": 32 * 32 * 3,
                              # 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                              "max_pixels": 32 * 32 * 8192,
                              # 是否开启图像自动转正功能
                              "enable_rotate": False}]
                         }]
              response = dashscope.MultiModalConversation.call(
                   # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx",
                  api_key=os.getenv('DASHSCOPE_API_KEY'),
                  model='qwen3.5-ocr',
                  messages=messages,
                  # 设置内置任务为表格解析
                  ocr_options= {"task": "table_parsing"}
              )
              # 表格解析任务以HTML格式返回结果
              print(response["output"]["choices"][0]["message"].content[0]["text"])
              ```

              ```java expandable
              import java.util.Arrays;
              import java.util.Collections;
              import java.util.Map;
              import java.util.HashMap;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.aigc.multimodalconversation.OcrOptions;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {

                  // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
                  static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      MultiModalConversation conv = new MultiModalConversation();
                      Map<String, Object> map = new HashMap<>();
                      map.put("image", "https://duguang-llm.oss-cn-hangzhou.aliyuncs.com/llm_data_keeper/data/doc_parsing/tables/photo/eng/17.jpg");
                      // 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                      map.put("max_pixels", 8388608);
                      // 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                      map.put("min_pixels", 3072);
                      // 是否开启图像自动转正功能
                      map.put("enable_rotate", false);
                      // 配置内置的OCR任务
                      OcrOptions ocrOptions = OcrOptions.builder()
                              .task(OcrOptions.Task.TABLE_PARSING)
                              .build();
                      MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      map
                                      )).build();
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 若没有配置环境变量，请用百炼API Key将下行替换为：.apiKey("sk-xxx")
                              // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3.5-ocr")
                              .message(userMessage)
                              .ocrOptions(ocrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent().get(0).get("text"));
                  }

                  public static void main(String[] args) {
                      try {
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```

              ```bash expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              # === 执行时请删除该注释 ===

              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '
              {
                "model": "qwen3.5-ocr",
                "input": {
                  "messages": [
                    {
                      "role": "user",
                      "content": [
                        {
                          "image": "http://duguang-llm.oss-cn-hangzhou.aliyuncs.com/llm_data_keeper/data/doc_parsing/tables/photo/eng/17.jpg",
                          "min_pixels": 3072,
                          "max_pixels": 8388608,
                          "enable_rotate": false
                        }
                      ]
                    }
                  ]
                },
                "parameters": {
                  "ocr_options": {
                    "task": "table_parsing"
                  }
                }
              }
              '
              ```
            </CodeGroup>
          </Tab>

          <Tab title="文档解析">
            > 以下为调用文档解析内置任务的代码示例，详情请参见[调用内置任务](/zh/model-studio/qwen-vl-ocr#3ea70c9a1a4lz)。

            <CodeGroup dropdown>
              ```python expandable
              import os
              import dashscope

              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              messages = [{
                          "role": "user",
                          "content": [{
                              "image": "https://img.alicdn.com/imgextra/i1/O1CN01ukECva1cisjyK6ZDK_!!6000000003635-0-tps-1500-1734.jpg",
                              # 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                              "min_pixels": 32 * 32 * 3,
                              # 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                              "max_pixels": 32 * 32 * 8192,
                              # 是否开启图像自动转正功能
                              "enable_rotate": False}]
                          }]

              response = dashscope.MultiModalConversation.call(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx",
                  # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  api_key=os.getenv('DASHSCOPE_API_KEY'),
                  model='qwen3.5-ocr',
                  messages=messages,
                  # 设置内置任务为文档解析
                  ocr_options= {"task": "document_parsing"}
              )
              # 文档解析任务以LaTeX格式返回结果
              print(response["output"]["choices"][0]["message"].content[0]["text"])
              ```

              ```java expandable
              import java.util.Arrays;
              import java.util.Collections;
              import java.util.Map;
              import java.util.HashMap;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.aigc.multimodalconversation.OcrOptions;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {
                  // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
                  static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      MultiModalConversation conv = new MultiModalConversation();
                      Map<String, Object> map = new HashMap<>();
                      map.put("image", "https://img.alicdn.com/imgextra/i1/O1CN01ukECva1cisjyK6ZDK_!!6000000003635-0-tps-1500-1734.jpg");
                      // 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                      map.put("max_pixels", 8388608);
                      // 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                      map.put("min_pixels", 3072);
                      // 是否开启图像自动转正功能
                      map.put("enable_rotate", false);
                      // 配置内置的OCR任务
                      OcrOptions ocrOptions = OcrOptions.builder()
                              .task(OcrOptions.Task.DOCUMENT_PARSING)
                              .build();
                      MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      map
                              )).build();
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 若没有配置环境变量，请用百炼API Key将下行替换为：.apiKey("sk-xxx")
                              // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3.5-ocr")
                              .message(userMessage)
                              .ocrOptions(ocrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent().get(0).get("text"));
                  }

                  public static void main(String[] args) {
                      try {
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```

              ```bash expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              # === 执行时请删除该注释 ===

              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation'\
                --header "Authorization: Bearer $DASHSCOPE_API_KEY"\
                --header 'Content-Type: application/json'\
                --data '{
              "model": "qwen3.5-ocr",
              "input": {
                "messages": [
                  {
                    "role": "user",
                    "content": [{
                        "image": "https://img.alicdn.com/imgextra/i1/O1CN01ukECva1cisjyK6ZDK_!!6000000003635-0-tps-1500-1734.jpg",
                        "min_pixels": 3072,
                        "max_pixels": 8388608,
                        "enable_rotate": false
                      }
                    ]
                  }
                ]
              },
              "parameters": {
                "ocr_options": {
                  "task": "document_parsing"
                }
              }
              }
              '
              ```
            </CodeGroup>
          </Tab>

          <Tab title="公式识别">
            > 以下为调用公式识别内置任务的代码示例，详情请参见[调用内置任务](/zh/model-studio/qwen-vl-ocr#3ea70c9a1a4lz)。

            <CodeGroup dropdown>
              ```python expandable
              import os
              import dashscope

              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              messages = [{
                          "role": "user",
                          "content": [{
                              "image": "http://duguang-llm.oss-cn-hangzhou.aliyuncs.com/llm_data_keeper/data/formula_handwriting/test/inline_5_4.jpg",
                              # 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                              "min_pixels": 32 * 32 * 3,
                              # 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                              "max_pixels": 32 * 32 * 8192,
                              # 是否开启图像自动转正功能
                              "enable_rotate": False }]
                          }]

              response = dashscope.MultiModalConversation.call(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv('DASHSCOPE_API_KEY'),
                  model='qwen3.5-ocr',
                  messages=messages,
                  # 设置内置任务为公式识别
                  ocr_options= {"task": "formula_recognition"}
              )

              # 公式识别任务以LaTeX格式返回结果
              print(response["output"]["choices"][0]["message"].content[0]["text"])
              ```

              ```java expandable
              import java.util.Arrays;
              import java.util.Collections;
              import java.util.Map;
              import java.util.HashMap;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.aigc.multimodalconversation.OcrOptions;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {

                  // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
                  static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      MultiModalConversation conv = new MultiModalConversation();
                      Map<String, Object> map = new HashMap<>();
                      map.put("image", "http://duguang-llm.oss-cn-hangzhou.aliyuncs.com/llm_data_keeper/data/formula_handwriting/test/inline_5_4.jpg");
                      // 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                      map.put("max_pixels", 8388608);
                      // 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                      map.put("min_pixels", 3072);
                      // 是否开启图像自动转正功能
                      map.put("enable_rotate", false);
                      // 配置内置的OCR任务
                      OcrOptions ocrOptions = OcrOptions.builder()
                              .task(OcrOptions.Task.FORMULA_RECOGNITION)
                              .build();
                      MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      map
                                      )).build();
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 若没有配置环境变量，请用百炼API Key将下行替换为：.apiKey("sk-xxx")
                                              // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3.5-ocr")
                              .message(userMessage)
                              .ocrOptions(ocrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent().get(0).get("text"));
                  }

                  public static void main(String[] args) {
                      try {
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```

              ```bash expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              # === 执行时请删除该注释 ===

              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '
              {
                "model": "qwen3.5-ocr",
                "input": {
                  "messages": [
                    {
                      "role": "user",
                      "content": [
                        {
                          "image": "http://duguang-llm.oss-cn-hangzhou.aliyuncs.com/llm_data_keeper/data/formula_handwriting/test/inline_5_4.jpg",
                          "min_pixels": 3072,
                          "max_pixels": 8388608,
                          "enable_rotate": false
                        }
                      ]
                    }
                  ]
                },
                "parameters": {
                  "ocr_options": {
                    "task": "formula_recognition"
                  }
                }
              }
              '
              ```
            </CodeGroup>
          </Tab>

          <Tab title="通用文字识别">
            > 以下为调用通用文字识别内置任务的代码示例，详情请参见[调用内置任务](/zh/model-studio/qwen-vl-ocr#3ea70c9a1a4lz)。

            <CodeGroup dropdown>
              ```python expandable
              import os
              import dashscope

              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              messages = [{
                          "role": "user",
                          "content": [{
                              "image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg",
                              # 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                              "min_pixels": 32 * 32 * 3,
                              # 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                              "max_pixels": 32 * 32 * 8192,
                              # 是否开启图像自动转正功能
                              "enable_rotate": False}]
                      }]

              response = dashscope.MultiModalConversation.call(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx",
                  # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  api_key=os.getenv('DASHSCOPE_API_KEY'),
                  model='qwen3.5-ocr',
                  messages=messages,
                  # 设置内置任务为通用文字识别
                  ocr_options= {"task": "text_recognition"}
              )
              # 通用文字识别任务以纯文本格式返回结果
              print(response["output"]["choices"][0]["message"].content[0]["text"])
              ```

              ```java expandable
              import java.util.Arrays;
              import java.util.Collections;
              import java.util.Map;
              import java.util.HashMap;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.aigc.multimodalconversation.OcrOptions;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {

                  // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
                  static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      MultiModalConversation conv = new MultiModalConversation();
                      Map<String, Object> map = new HashMap<>();
                      map.put("image", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg");
                      // 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                      map.put("max_pixels", 8388608);
                      // 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                      map.put("min_pixels", 3072);
                      // 是否开启图像自动转正功能
                      map.put("enable_rotate", false);

                      // 配置内置任务
                      OcrOptions ocrOptions = OcrOptions.builder()
                              .task(OcrOptions.Task.TEXT_RECOGNITION)
                              .build();
                      MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      map
                                      )).build();
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 若没有配置环境变量，请用百炼API Key将下行替换为：.apiKey("sk-xxx")
                             // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3.5-ocr")
                              .message(userMessage)
                              .ocrOptions(ocrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent().get(0).get("text"));
                  }

                  public static void main(String[] args) {
                      try {
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```

              ```bash expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              # === 执行时请删除该注释 ===

              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation'\
                --header "Authorization: Bearer $DASHSCOPE_API_KEY"\
                --header 'Content-Type: application/json'\
                --data '{
              "model": "qwen3.5-ocr",
              "input": {
                "messages": [
                  {
                    "role": "user",
                    "content": [{
                        "image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg",
                        "min_pixels": 3072,
                        "max_pixels": 8388608,
                        "enable_rotate": false
                      }
                    ]
                  }
                ]
              },
              "parameters": {
                "ocr_options": {
                    "task": "text_recognition"
                  }
              }
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="多语言识别">
            > 以下为调用通用多语言识别内置任务的代码示例，详情请参见[调用内置任务](/zh/model-studio/qwen-vl-ocr#3ea70c9a1a4lz)。

            <CodeGroup dropdown>
              ```python expandable
              import os
              import dashscope

              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              messages = [{
                          "role": "user",
                          "content": [{
                              "image": "https://img.alicdn.com/imgextra/i2/O1CN01VvUMNP1yq8YvkSDFY_!!6000000006629-2-tps-6000-3000.png",
                              # 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                              "min_pixels": 32 * 32 * 3,
                              # 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                              "max_pixels": 32 * 32 * 8192,
                              # 是否开启图像自动转正功能
                              "enable_rotate": False }]
                          }]

              response = dashscope.MultiModalConversation.call(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx",
                  # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  api_key=os.getenv('DASHSCOPE_API_KEY'),
                  model='qwen3.5-ocr',
                  messages=messages,
                  # 设置内置任务为多语言识别
                  ocr_options={"task": "multi_lan"}
              )
              # 多语言识别任务以纯文本的形式返回结果
              print(response["output"]["choices"][0]["message"].content[0]["text"])
              ```

              ```java expandable
              import java.util.Arrays;
              import java.util.Collections;
              import java.util.Map;
              import java.util.HashMap;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.aigc.multimodalconversation.OcrOptions;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {

                  // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
                  static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      MultiModalConversation conv = new MultiModalConversation();
                      Map<String, Object> map = new HashMap<>();
                      map.put("image", "https://img.alicdn.com/imgextra/i2/O1CN01VvUMNP1yq8YvkSDFY_!!6000000006629-2-tps-6000-3000.png");
                      // 输入图像的最大像素阈值，超过该值图像会进行缩小，直到总像素低于max_pixels
                      map.put("max_pixels", 8388608);
                      // 输入图像的最小像素阈值，小于该值图像会进行放大，直到总像素大于min_pixels
                      map.put("min_pixels", 3072);
                      // 是否开启图像自动转正功能
                      map.put("enable_rotate", false);
                      // 配置内置的OCR任务
                      OcrOptions ocrOptions = OcrOptions.builder()
                              .task(OcrOptions.Task.MULTI_LAN)
                              .build();
                      MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      map
                                      )).build();
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 若没有配置环境变量，请用百炼API Key将下行替换为：.apiKey("sk-xxx")
                              // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3.5-ocr")
                              .message(userMessage)
                              .ocrOptions(ocrOptions)
                              .build();
                      MultiModalConversationResult result = conv.call(param);
                      System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent().get(0).get("text"));
                  }

                  public static void main(String[] args) {
                      try {
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```

              ```bash expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
              # === 执行时请删除该注释 ===

              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '
              {
                "model": "qwen3.5-ocr",
                "input": {
                  "messages": [
                    {
                      "role": "user",
                      "content": [
                        {
                          "image": "https://img.alicdn.com/imgextra/i2/O1CN01VvUMNP1yq8YvkSDFY_!!6000000006629-2-tps-6000-3000.png",
                          "min_pixels": 3072,
                          "max_pixels": 8388608,
                          "enable_rotate": false
                        }
                      ]
                    }
                  ]
                },
                "parameters": {
                  "ocr_options": {
                    "task": "multi_lan"
                  }
                }
              }
              '
              ```
            </CodeGroup>
          </Tab>

          <Tab title="流式输出">
            <CodeGroup dropdown>
              ```python Python expandable
              import os
              import dashscope

              # 以下为北京地域base_url，若使用弗吉尼亚地域模型，需要将base_url换成 https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1
              # 若使用新加坡地域的模型，需将base_url替换为：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              PROMPT_TICKET_EXTRACTION = """
              请提取车票图像中的发票号码、车次、起始站、终点站、发车日期和时间点、座位号、席别类型、票价、身份证号码、购票人姓名。
              要求准确无误的提取上述关键信息、不要遗漏和捏造虚假信息，模糊或者强光遮挡的单个文字可以用英文问号?代替。
              返回数据格式以json方式输出，格式为：{'发票号码': 'xxx', '起始站': 'xxx', '终点站': 'xxx', '发车日期和时间点':'xxx', '座位号': 'xxx','票价':'xxx', '身份证号码': 'xxx', '购票人姓名': 'xxx'},
              """

              messages = [
                  {
                      "role": "user",
                      "content": [
                          {
                              "image": "https://img.alicdn.com/imgextra/i2/O1CN01ktT8451iQutqReELT_!!6000000004408-0-tps-689-487.jpg",
                              # 输入图像的最小像素阈值，小于该值图像会放大，直到总像素大于min_pixels
                              "min_pixels": 32 * 32 * 3,
                              # 输入图像的最大像素阈值，超过该值图像会缩小，直到总像素低于max_pixels
                              "max_pixels": 32 * 32 * 8192,
                              # 开启图像自动转正功能
                              "enable_rotate": False,
                          },
                          # 未设置内置任务时，支持在text字段中传入Prompt，若未传入则使用默认的Prompt：Please output only the text content from the image without any additional descriptions or formatting.
                          {
                              "type": "text",
                              "text": PROMPT_TICKET_EXTRACTION,
                          },
                      ],
                  }
              ]
              response = dashscope.MultiModalConversation.call(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx",
                  # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  model="qwen3.5-ocr",
                  messages=messages,
                  stream=True,
                  incremental_output=True,
              )

              full_content = ""
              print("流式输出内容为：")
              for response in response:
                  try:
                      print(response["output"]["choices"][0]["message"].content[0]["text"])
                      full_content += response["output"]["choices"][0]["message"].content[0]["text"]
                  except:
                      pass
              print(f"完整内容为：{full_content}")
              ```

              ```java Java expandable
              import java.util.*;

              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
              import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
              import com.alibaba.dashscope.common.MultiModalMessage;
              import com.alibaba.dashscope.common.Role;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.exception.UploadFileException;
              import io.reactivex.Flowable;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {

                  // 以下为北京地域 base_url，若使用弗吉尼亚地域模型，需要将base_url换成 https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1
                  // 若使用新加坡地域的模型，需将base_url替换为：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1
                  static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

                  public static void simpleMultiModalConversationCall()
                          throws ApiException, NoApiKeyException, UploadFileException {
                      MultiModalConversation conv = new MultiModalConversation();
                      Map<String, Object> map = new HashMap<>();
                      map.put("image", "https://img.alicdn.com/imgextra/i2/O1CN01ktT8451iQutqReELT_!!6000000004408-0-tps-689-487.jpg");
                      // 输入图像的最大像素阈值，超过该值图像会缩小，直到总像素低于max_pixels
                      map.put("max_pixels", 8388608);
                      // 输入图像的最小像素阈值，小于该值图像会放大，直到总像素大于min_pixels
                      map.put("min_pixels", 3072);
                      // 开启图像自动转正功能
                      map.put("enable_rotate", false);
                      MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                              .content(Arrays.asList(
                                      map,
                                      // 模型未设置内置任务时，支持在text字段中传入Prompt，若未传入则使用默认的Prompt：Please output only the text content from the image without any additional descriptions or formatting.
                                      Collections.singletonMap("text", "请提取车票图像中的发票号码、车次、起始站、终点站、发车日期和时间点、座位号、席别类型、票价、身份证号码、购票人姓名。要求准确无误的提取上述关键信息、不要遗漏和捏造虚假信息，模糊或者强光遮挡的单个文字可以用英文问号?代替。返回数据格式以json方式输出，格式为：{'发票号码': 'xxx', '起始站': 'xxx', '终点站': 'xxx', '发车日期和时间点':'xxx', '座位号': 'xxx','票价':'xxx', '身份证号码': 'xxx', '购票人姓名': 'xxx'"))).build();
                      MultiModalConversationParam param = MultiModalConversationParam.builder()
                              // 若没有配置环境变量，请用百炼API Key将下行替换为：.apiKey("sk-xxx")
                             // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model("qwen3.5-ocr")
                              .message(userMessage)
                              .incrementalOutput(true)
                              .build();
                      Flowable<MultiModalConversationResult> result = conv.streamCall(param);
                      result.blockingForEach(item -> {
                          try {
                              List<Map<String, Object>> contentList = item.getOutput().getChoices().get(0).getMessage().getContent();
                              if (!contentList.isEmpty()){
                                  System.out.println(contentList.get(0).get("text"));
                              }//
                          } catch (Exception e){
                              System.exit(0);
                          }
                      });
                  }

                  public static void main(String[] args) {
                      try {
                          simpleMultiModalConversationCall();
                      } catch (ApiException | NoApiKeyException | UploadFileException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```

              ```bash curl expandable
              # ======= 重要提示 =======
              # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              # 以下为北京地域base_url，若使用弗吉尼亚地域模型，需要将base_url换成：https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation
              # 若使用新加坡地域的模型，需要将base_url换成：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation
              # === 执行时请删除该注释 ===

              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              -H 'X-DashScope-SSE: enable' \
              --data '{
                  "model": "qwen3.5-ocr",
                  "input":{
                      "messages":[
                        {
                          "role": "user",
                          "content": [
                              {
                                  "image": "https://img.alicdn.com/imgextra/i2/O1CN01ktT8451iQutqReELT_!!6000000004408-0-tps-689-487.jpg",
                                  "min_pixels": 3072,
                                  "max_pixels": 8388608
                              },
                              {"type": "text", "text": "请提取车票图像中的发票号码、车次、起始站、终点站、发车日期和时间点、座位号、席别类型、票价、身份证号码、购票人姓名。要求准确无误的提取上述关键信息、不要遗漏和捏造虚假信息，模糊或者强光遮挡的单个文字可以用英文问号?代替。返回数据格式以json方式输出，格式为：{'发票号码': 'xxx', '起始站': 'xxx', '终点站': 'xxx', '发车日期和时间点':'xxx', '座位号': 'xxx','票价':'xxx', '身份证号码': 'xxx', '购票人姓名': 'xxx'"}
                          ]
                        }
                      ]
                  },
                  "parameters": {
                      "incremental_output": true
                  }
              }'
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
        ### chat响应对象（流式与非流式输出格式一致） <span id="0e1c44902ea1e" />

        <strong>status\_code</strong>`string`

        本次请求的状态码。200 表示请求成功，否则表示请求失败。

        > Java SDK不会返回该参数。调用失败会抛出异常，异常信息为<strong>status\_code</strong>和<strong>message</strong>的内容。

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        > Java SDK返回参数为<strong>requestId。</strong>

        <strong>code</strong>`string`

        错误码，调用成功时为空值。

        > 只有Python SDK返回该参数。

        <strong>output</strong>`object`

        调用结果信息。

        <Accordion title="属性" defaultOpen>
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

              <strong>content</strong>`object`

              输出消息的内容。

              <Accordion title="属性">
                <strong>ocr\_result</strong>`object`

                当Qwen-OCR系列模型调用内置的信息抽取、高精识别任务时，输出的任务结果信息。

                <Accordion title="属性">
                  <strong>kv\_result</strong>`array`

                  信息抽取任务的输出结果。

                  <strong>words\_info</strong>`array`

                  高精识别任务的输出结果。

                  <Accordion title="属性">
                    <strong>rotate\_rect</strong> `array`

                    示例值：`[center_x, center_y, width, height, angle]`

                    文字框的旋转矩形表示：

                    - `center_x、center_y为文本框中心点坐标`
                    - `width`为文本框宽度，`height`为高度
                    - `angle`为文本框相对于水平方向的旋转角度，取值范围为`[-90, 90]`

                    <strong>location</strong> `array`

                    示例值：`[x1, y1, x2, y2, x3, y3, x4, y4]`

                    文字框四个顶点的坐标，坐标顺序为左上角开起，按左上角→右上角→右下角→左下角的顺时针顺序排列。

                    <strong>text</strong>`string`

                    文本行的内容
                  </Accordion>

                  <strong>text</strong>`string`

                  输出消息的内容。
                </Accordion>

                <strong>processed\_text</strong>`string`

                对模型原始输出进行后处理的结果，自动删除重复片段等。当模型输出存在重复内容时，该字段提供清洗后的文本。
              </Accordion>

              <strong>logprobs</strong>`object`

              当前 choices 对象的概率信息。

              <Accordion title="属性">
                <strong>content</strong> `array`

                带有对数概率信息的 Token 数组。

                <Accordion title="属性">
                  <strong>token</strong> `string`

                  当前 Token。

                  <strong>bytes</strong> `array`

                  当前 Token 的 UTF‑8 原始字节列表，用于精确还原输出内容，在处理表情符号、中文字符时有帮助。

                  <strong>logprob</strong> `float`

                  当前 Token 的对数概率。返回值为 null 表示概率值极低。

                  <strong>top\_logprobs</strong> `array`

                  当前 Token 位置最可能的若干个 Token 及其对数概率，元素个数与入参的`top_logprobs`保持一致。

                  <Accordion title="属性">
                    <strong>token</strong> `string`

                    当前 Token。

                    <strong>bytes</strong> `array`

                    当前 Token 的 UTF‑8 原始字节列表，用于精确还原输出内容，在处理表情符号、中文字符时有帮助。

                    <strong>logprob</strong> `float`

                    当前 Token 的对数概率。返回值为 null 表示概率值极低。
                  </Accordion>
                </Accordion>
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>usage</strong>`object`

        本次请求使用的Token信息。

        <Accordion title="属性" defaultOpen>
          <strong>input\_tokens</strong> `integer`

          输入 Token 数。

          <strong>output\_tokens</strong> `integer`

          输出 Token 数。

          <strong>characters</strong> `integer`

          该参数当前固定为0。

          <strong>input\_tokens\_details</strong>`object`

          输入 Token 的细粒度分类。

          <Accordion title="属性">
            <strong>image\_tokens</strong> `integer`

            模型输入的图像对应的 Token数。

            <strong>text\_tokens</strong> `integer`

            模型输入的文本对应的Token 数。
          </Accordion>

          <strong>output\_tokens\_details</strong>`object`

          输出 Token 的细粒度分类。

          <Accordion title="属性">
            <strong>text\_tokens</strong> `integer`

            模型输入的文本对应的Token 数。
          </Accordion>

          <strong>total\_tokens</strong> `integer`

          消耗的总 Token 数，为`input_tokens`与`output_tokens`的总和。

          <strong>image\_tokens</strong> `integer`

          输入内容包含`image`时返回该字段。为用户输入图片内容转换成Token后的长度。
        </Accordion>
      </td>

      <td>
        ````json expandable
        {"status_code": 200,
          "request_id": "8f8c0f6e-6805-4056-bb65-d26d66080a41",
          "code": "",
          "message": "",
          "output": {
            "text": null,
            "finish_reason": null,
            "choices": [
              {
                "finish_reason": "stop",
                "message": {
                  "role": "assistant",
                  "content": [
                    {
                      "ocr_result": {
                        "kv_result": {
                          "不含税价": "230769.23",
                          "发票代码": "142011726001",
                          "组织机构代码": "null",
                          "购买方名称": "蔡应时",
                          "销售方名称": "null"
                        }
                      },
                      "text": "```json\n{\n    \"不含税价\": \"230769.23\",\n    \"发票代码\": \"142011726001\",\n    \"组织机构代码\": \"null\",\n    \"购买方名称\": \"蔡应时\",\n    \"销售方名称\": \"null\"\n}\n```",
                      "processed_text": "```json\n{\n    \"不含税价\": \"230769.23\",\n    \"发票代码\": \"142011726001\",\n    \"组织机构代码\": \"null\",\n    \"购买方名称\": \"蔡应时\",\n    \"销售方名称\": \"null\"\n}\n```"
                    }
                  ]
                }
              }
            ],
            "audio": null
          },
          "usage": {
            "input_tokens": 926,
            "output_tokens": 72,
            "characters": 0,
            "image_tokens": 754,
            "input_tokens_details": {
              "image_tokens": 754,
              "text_tokens": 172
            },
            "output_tokens_details": {
              "text_tokens": 72
            },
            "total_tokens": 998
          }
        }
        ````
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="97e145aeabbwf" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
