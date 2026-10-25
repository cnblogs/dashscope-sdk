> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取响应

> 根据 Response ID 获取一个已完成的模型响应。

<meta property="og:title" content="获取响应" />

<Tabs>
  <Tab title="华北2（北京）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses/{response_id}`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ## 路径参数 <span id="retrieve-req-h2" />

        <strong>response\_id</strong>`string`（必选）

        要获取的 Response ID，格式为 `resp_xxx`。可从[创建响应](/zh/model-studio/qwen-api-via-openai-responses)接口的响应中获取。仅当原创建请求中 `store=true` 时返回的 Response ID 可被检索。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          response = client.responses.retrieve("resp_xxx")
          print(response)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const openai = new OpenAI({
              // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx"
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
          });

          async function main() {
              const response = await openai.responses.retrieve("resp_xxx");
              console.log(response);
          }

          main();
          ```

          ```bash curl
          curl https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses/resp_xxx \
            -H "Authorization: Bearer $DASHSCOPE_API_KEY"
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
        ## 返回结果 <span id="retrieve-resp-h2" />

        返回与[创建响应](/zh/model-studio/qwen-api-via-openai-responses#8b91df45f4k58)相同的 Response 对象。各字段含义如下：

        <strong>id</strong> `string`

        本次响应的唯一标识符，格式为 `resp_xxx`。

        <strong>object</strong> `string`

        对象类型，固定为 `response`。

        <strong>status</strong> `string`

        响应状态。枚举值:`completed`(完成)、`failed`(失败)、`in_progress`(生成中)、`cancelled`(已取消)、`queued`(排队中)、`incomplete`(不完整)。

        <strong>created\_at</strong> `integer`

        响应创建时的 Unix 时间戳（秒）。

        <strong>completed\_at</strong> `integer`

        响应生成完成时的 Unix 时间戳（秒）。响应未完成时为 `null`。

        <strong>error</strong> `object`

        当模型生成响应失败时返回的错误对象。成功时为 `null`。

        <strong>model</strong> `string`

        用于生成响应的模型 ID。

        <strong>output</strong> `array`

        模型生成的输出项数组。数组中的元素类型和顺序取决于模型的响应。

        <Accordion title="数组元素属性">
          <strong>type</strong> `string`

          输出项类型。枚举值：

          - `message`：消息类型，包含模型最终生成的回复内容。
          - `reasoning`：推理类型，设置 `reasoning.effort`（非 `none`）或开启思考模式时返回。推理 Token 会被计入 `output_tokens_details.reasoning_tokens` 中，按推理 Token 计费。
          - `function_call`：函数调用类型，使用自定义 `function` 工具时返回。需要处理函数调用并返回结果。
          - `web_search_call`：搜索调用类型，使用 `web_search` 工具时返回。
          - `code_interpreter_call`：代码执行类型，使用 `code_interpreter` 工具时返回。
          - `web_extractor_call`：网页抽取类型，使用 `web_extractor` 工具时返回。需要配合 `web_search` 工具一起使用。
          - `web_search_image_call`：文搜图调用类型，使用 `web_search_image` 工具时返回。包含搜索到的图片列表。
          - `image_search_call`：图搜图调用类型，使用 `image_search` 工具时返回。包含搜索到的相似图片列表。
          - `mcp_call`：MCP 调用类型，使用 `mcp` 工具时返回。包含 MCP 服务的调用结果。
          - `file_search_call`：知识库搜索调用类型，使用 `file_search` 工具时返回。包含知识库的检索查询和结果。

          <strong>id</strong> `string`

          输出项的唯一标识符。所有类型的输出项都包含此字段。

          <strong>role</strong> `string`

          消息角色，固定为 `assistant`。仅当 `type` 为 `message` 时存在。

          <strong>status</strong> `string`

          输出项状态。可选值：`completed`（完成）、`in_progress`（生成中）。当 `type` 不为`reasoning`时存在。

          <strong>name</strong> `string`

          工具或函数名称。当 `type` 为 `function_call`、`web_search_image_call`、`image_search_call`、`mcp_call` 时存在。

          对于 `web_search_image_call` 和 `image_search_call`，值分别固定为 `"web_search_image"` 和 `"image_search"`。

          对于 `mcp_call`，值为 MCP 服务中被调用的具体函数名（如 `amap-maps-maps_geo`）。

          <strong>arguments</strong> `string`

          工具调用的参数，JSON 字符串格式。当 `type` 为 `function_call`、`web_search_image_call`、`image_search_call`、`mcp_call` 时存在。使用前需要通过 `JSON.parse()` 解析。不同工具类型的 arguments 内容：

          - `web_search_image_call`：`{"queries": ["搜索关键词1", "搜索关键词2"]}`，其中 `queries` 为模型根据用户输入自动生成的搜索关键词列表。
          - `image_search_call`：`{"img_idx": 0, "bbox": [0, 0, 1000, 1000]}`，其中 `img_idx` 为输入图片的索引（从 0 开始），`bbox` 为搜索区域的边界框坐标 \[x1, y1, x2, y2]，坐标范围 0-1000。
          - `function_call`：按用户定义的函数参数 schema 生成的参数对象。
          - `mcp_call`：MCP 服务中被调用函数的参数对象。

          <strong>call\_id</strong> `string`

          函数调用的唯一标识符。仅当 `type` 为 `function_call` 时存在。在返回函数调用结果时，需要通过此 ID 关联请求与响应。

          <strong>content</strong> `array`

          消息内容数组。仅当 `type` 为 `message` 时存在。

          <Accordion title="数组元素属性">
            <strong>type</strong> `string`

            内容类型，固定为 `output_text`。

            <strong>text</strong> `string`

            模型生成的文本内容。

            <strong>annotations</strong> `array`

            文本注释数组。通常为空数组。
          </Accordion>

          <strong>summary</strong> `array`

          推理摘要数组。仅当 `type` 为 `reasoning` 时存在。每个元素包含 `type`（值为 `summary_text`）和 `text`（摘要文本）字段。

          <strong>action</strong> `object`

          搜索动作信息。仅当 `type` 为 `web_search_call` 时存在。

          <Accordion title="属性">
            <strong>query</strong> `string`

            搜索查询关键词。

            <strong>type</strong> `string`

            搜索类型，固定为 `search`。

            <strong>sources</strong> `array`

            搜索来源列表。每个元素包含 `type`和 `url`字段。
          </Accordion>

          <strong>code</strong> `string`

          模型生成并执行的代码。仅当 `type` 为 `code_interpreter_call` 时存在。

          <strong>outputs</strong> `array`

          代码执行输出数组。仅当 `type` 为 `code_interpreter_call` 时存在。每个元素包含 `type`（值为 `logs`）和 `logs`（代码执行日志）字段。

          <strong>container\_id</strong> `string`

          代码解释器容器标识符。仅当 `type` 为 `code_interpreter_call` 时存在。用于关联同一会话中的多次代码执行。

          <strong>goal</strong> `string`

          抽取目标描述，说明需要从网页中提取哪些信息。仅当 `type` 为 `web_extractor_call` 时存在。

          <strong>output</strong> `string`

          工具调用的输出结果，字符串格式。

          - 当 `type` 为 `web_extractor_call` 时为网页抽取的内容摘要
          - 当 `type` 为 `web_search_image_call` 或 `image_search_call` 时为 JSON 字符串，包含图片搜索结果数组，每个元素包含 `title`（图片标题）、`url`（图片 URL）和 `index`（序号）字段
          - 当 `type` 为 `mcp_call` 时为 MCP 服务返回的 JSON 字符串结果。

          <strong>urls</strong> `array`

          被抽取的网页 URL 列表。仅当 `type` 为 `web_extractor_call` 时存在。

          <strong>server\_label</strong> `string`

          MCP 服务标签。仅当 `type` 为 `mcp_call` 时存在。标识本次调用所使用的 MCP 服务。

          <strong>queries</strong> `array`

          知识库检索使用的查询列表。仅当 `type` 为 `file_search_call` 时存在。数组元素为字符串，表示模型生成的搜索查询词。

          <strong>results</strong> `array`

          知识库检索结果数组。仅当 `type` 为 `file_search_call` 时存在。

          <Accordion title="数组元素属性">
            <strong>file\_id</strong> `string`

            匹配文档的文件 ID。

            <strong>filename</strong> `string`

            匹配文档的文件名。

            <strong>score</strong> `float`

            匹配相关度评分，取值范围 0-1，值越大表示相关度越高。

            <strong>text</strong> `string`

            匹配到的文档内容片段。
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        本次请求的 Token 消耗信息。

        <Accordion title="属性">
          <strong>input\_tokens</strong> `integer`

          输入的 Token 数。

          <strong>output\_tokens</strong> `integer`

          模型输出的 Token 数。

          <strong>total\_tokens</strong> `integer`

          消耗的总 Token 数，为 input\_tokens 与 output\_tokens 的总和。

          <strong>input\_tokens\_details</strong> `object`

          输入 Token 的细粒度分类。

          <Accordion title="属性">
            <strong>cached\_tokens</strong> `integer`

            命中缓存的 Token 数。
          </Accordion>

          <strong>output\_tokens\_details</strong> `object`

          输出 Token 的细粒度分类。

          <Accordion title="属性">
            <strong>reasoning\_tokens</strong> `integer`

            思考过程 Token 数。
          </Accordion>

          <strong>x\_details</strong> `array`

          计费明细数组。

          <Accordion title="属性">
            <strong>input\_tokens</strong> `integer`

            本计费类型的输入 Token 数。

            <strong>output\_tokens</strong> `integer`

            本计费类型的输出 Token 数。

            <strong>total\_tokens</strong> `integer`

            本计费类型的总 Token 数。

            <strong>x\_billing\_type</strong> `string`

            固定为`response_api`。

            <strong>prompt\_tokens\_details</strong> `object`

            启用 Session 缓存后返回。包含 `cached_tokens`（命中缓存的 Token 数）字段。
          </Accordion>

          <strong>x\_tools</strong> `object`

          工具使用统计信息。当使用内置工具时，包含各工具的调用次数。示例：`{"web_search": {"count": 1}}`
        </Accordion>

        <strong>tools</strong> `array`

        回显原创建响应请求中 `tools` 参数的完整内容。结构与请求体中的 `tools` 参数相同。无工具时为空数组 `[]`。

        <strong>tool\_choice</strong> `string`

        回显原创建响应请求中 `tool_choice` 参数的值。枚举值：`auto`、`none`、`required`。

        <strong>parallel\_tool\_calls</strong> `boolean`

        回显原创建响应请求中 `parallel_tool_calls` 参数的值，表示是否允许模型并行调用多个工具。

        <strong>temperature</strong> `float`

        回显原创建响应请求中 `temperature` 参数的值。采样温度，控制模型生成文本的多样性，取值范围 \[0, 2)。未设置时返回模型默认值。

        <strong>top\_p</strong> `float`

        回显原创建响应请求中 `top_p` 参数的值。核采样的概率阈值，取值范围 (0, 1.0]。未设置时返回模型默认值。

        <strong>frequency\_penalty</strong> `float`

        回显原创建响应请求中 `frequency_penalty` 参数的值。频率惩罚系数，正值降低重复用词的概率。

        <strong>presence\_penalty</strong> `float`

        回显原创建响应请求中 `presence_penalty` 参数的值。存在惩罚系数，正值增加生成新主题的概率。

        <strong>top\_logprobs</strong> `integer`

        回显原创建响应请求中 `top_logprobs` 参数的值。每个位置返回的最可能 Token 数量。未启用时为 `0`。

        <strong>store</strong> `boolean`

        回显原创建响应请求中 `store` 参数的值。`true` 表示该响应已储存，可被 `previous_response_id` 引用；`false` 表示未储存。

        <strong>service\_tier</strong> `string`

        服务等级，固定为 `default`。

        <strong>background</strong> `boolean`

        是否为后台异步执行的响应。当前百炼仅支持同步调用，固定为 `false`。

        <strong>metadata</strong> `object`

        回显原创建响应请求中 `metadata` 参数的内容。可用于附加自定义键值对的对象，无设置时为空对象 `{}`。
      </td>

      <td>
        ```json expandable
        {
            "background": false,
            "completed_at": 1778676420,
            "created_at": 1778676418,
            "frequency_penalty": 0.0,
            "id": "resp_801bc2c4-93d9-910f-b35d-5274f5a737c1",
            "metadata": {},
            "model": "qwen-plus",
            "object": "response",
            "output": [
                {
                    "content": [
                        {
                            "annotations": [],
                            "text": "你好！很高兴见到你。有什么我可以帮助你的吗？",
                            "type": "output_text"
                        }
                    ],
                    "id": "msg_8c54756c-9b65-4a95-81d7-4276d91406db",
                    "role": "assistant",
                    "status": "completed",
                    "type": "message"
                }
            ],
            "parallel_tool_calls": true,
            "presence_penalty": 0.0,
            "service_tier": "default",
            "status": "completed",
            "store": true,
            "temperature": 1.0,
            "tool_choice": "auto",
            "tools": [],
            "top_logprobs": 0,
            "top_p": 1.0,
            "usage": {
                "input_tokens": 45,
                "input_tokens_details": {
                    "cached_tokens": 0
                },
                "output_tokens": 63,
                "output_tokens_details": {
                    "reasoning_tokens": 0
                },
                "total_tokens": 108,
                "x_details": [
                    {
                        "input_tokens": 45,
                        "output_tokens": 63,
                        "prompt_tokens_details": {
                            "cached_tokens": 0
                        },
                        "total_tokens": 108,
                        "x_billing_type": "response_api"
                    }
                ]
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 错误响应 <span id="retrieve-error-h2" />

当指定的 Response ID 不存在时，返回以下错误：

```json
{
    "error": {
        "message": "Response with id 'resp_xxx' not found.",
        "type": "InvalidParameter"
    }
}
```
