> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-Deep-Research API 参考

> 本文介绍通过 DashScope API 调用 Qwen-Deep-Research 模型的输入与输出参数。

<meta property="og:title" content="Qwen-Deep-Research API 参考" />

> 相关文档：[深入研究（Qwen-Deep-Research）](/zh/model-studio/qwen-deep-research)

<Tip>
  Qwen-Deep-Research模型仅支持华北2（北京）地域，如需使用模型，请使用华北2（北京）地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

<Note>
  模型当前仅支持通过 Python DashScope SDK 调用，暂不支持 Java SDK 与 OpenAI 兼容接口。
</Note>

## DashScope <span id="c906e0db10vf1" />

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

        模型名称。支持的模型：qwen-deep-research。

        <strong>messages</strong>`array`<strong>（必选）</strong>

        传递给大模型的上下文，按对话顺序排列。

        <Accordion title="消息类型" defaultOpen>
          User Message`object`<strong>（必选）</strong>

          用户消息，用于向模型传递问题、指令或上下文。在 Qwen-Deep-Research 的两阶段调用流程中，用户消息起到不同作用：

          - <strong>第一步（模型反问确认）</strong>：用户消息用于发起初始的研究请求，提出一个较为宽泛的研究主题。
          - <strong>第二步（深入研究）</strong>：用户消息用于回答模型提出的澄清式问题，帮助模型聚焦研究方向，进行更具针对性的深入分析。

          <Accordion title="属性">
            <strong>content</strong>`string`<strong>（必选）</strong>

            消息内容。

            <strong>role</strong>`string`<strong>（必选）</strong>

            系统消息的角色，固定为user。
          </Accordion>

          Assistant Message `object`（可选）

          模型对用户消息的回复。在第二步（深入研究）的API调用中，此参数用以传入模型在第一步（反问确认）中返回的澄清式问题，作为对话历史的一部分，从而引导模型进行更具针对性的分析。

          <strong>content</strong>`string`（可选）

          消息内容。

          <strong>role</strong>`string`<strong>（必选）</strong>

          固定为`assistant`。
        </Accordion>

        <strong>output\_format</strong>`string`（可选）

        指定输出研究报告的格式和详细程度。支持以下取值：

        - `model_detailed_report`（默认） 生成一份结构完整、内容详尽的深度研究报告，篇幅约6000 Token，适合需要全面深入分析的场景。
        - `model_summary_report` 生成一份核心观点突出、内容精炼的摘要式研究报告，篇幅约1500-2000 Token，适合快速了解关键信息和结论的场景。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python expandable
          import os
          import dashscope
          # 以下为华北2（北京）地域的配置，调用时请将WorkspaceId替换为真实的业务空间ID，各地域的配置不同。
          dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

          # 第一步：模型反问确认
          messages = [{'role': 'user', 'content': '研究一下人工智能在教育中的应用'}]

          responses = dashscope.Generation.call(
              api_key=os.getenv('DASHSCOPE_API_KEY'),
              model="qwen-deep-research",
              messages=messages,
              stream=True
          )

          # 获取模型反问内容
          step1_content = ""
          for response in responses:
              if hasattr(response, 'output') and response.output:
                  message = response.output.get('message', {})
                  content = message.get('content', '')
                  if content:
                      step1_content += content
                      print(content, end='', flush=True)

          # 第二步：深入研究
          messages = [
              {'role': 'user', 'content': '研究一下人工智能在教育中的应用'},
              {'role': 'assistant', 'content': step1_content},
              {'role': 'user', 'content': '我主要关注个性化学习和智能评估这两个方面'}
          ]

          responses = dashscope.Generation.call(
              api_key=os.getenv('DASHSCOPE_API_KEY'),
              model="qwen-deep-research",
              messages=messages
          )

          # 流式输出研究结果
          for response in responses:
              if hasattr(response, 'output') and response.output:
                  message = response.output.get('message', {})
                  content = message.get('content', '')
                  if content:
                      print(content, end='', flush=True)
          ```

          ```bash curl expandable
          echo "第一步：模型反问确认"
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text-generation/generation' \
          --header 'X-DashScope-SSE: enable' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "input": {
                  "messages": [
                      {
                          "content": "研究一下人工智能在教育中的应用",
                          "role": "user"
                      }
                  ]
              },
              "model": "qwen-deep-research"
          }'

          echo -e "\n\n"
          echo "第二步：深入研究"
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text-generation/generation' \
          --header 'X-DashScope-SSE: enable' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "input": {
                  "messages": [
                      {
                          "content": "研究一下人工智能在教育中的应用",
                          "role": "user"
                      },
                      {
                          "content": "请告诉我您希望重点研究人工智能在教育中的哪些具体应用场景？",
                          "role": "assistant"
                      },
                      {
                          "content": "我主要关注个性化学习方面",
                          "role": "user"
                      }
                  ]
              },
              "model": "qwen-deep-research"
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
        ### 响应对象 <span id="0e1c44902ea1e" />

        <strong>status\_code</strong>`string`

        本次请求的状态码。200 表示请求成功，否则表示请求失败。

        > 调用失败会抛出异常，异常信息为<strong>status\_code</strong>和<strong>message</strong>的内容。

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>code</strong>`string`

        错误码，调用成功时为空值。

        > 只有Python SDK返回该参数。

        <strong>message</strong> `string`

        错误提示信息，调用成功时为空值。

        <strong>output</strong>`object`

        调用结果信息。

        <Accordion title="属性">
          <strong>text</strong>`string`

          该参数当前固定为`null`。

          <strong>finish\_reason</strong>`string`

          模型结束生成的原因。有以下情况：

          - 正在生成时为`null`；
          - 模型输出自然结束为`stop`；
          - 因生成长度过长而结束为`length`

          <strong>choices</strong>`array`

          模型的输出信息。

          <Accordion title="属性">
            <strong>finish\_reason</strong>`string`

            有以下情况：

            - 正在生成时为`null`；
            - 因模型输出自然结束为`stop`；
            - 因生成长度过长而结束为`length`
          </Accordion>

          <strong>message</strong>`object`

          模型输出的消息对象。

          <Accordion title="属性">
            <strong>属性</strong>

            <strong>phase</strong>`string`

            当前所处阶段，其中包含：

            - answer：反问确认与回答阶段;
            - ResearchPlanning：研究规划阶段
            - WebResearch：网络搜索阶段
            - KeepAlive：连接保持阶段

            <strong>role</strong>`string`

            输出消息的角色，固定为`assistant`。

            <strong>content</strong>`string`

            模型的输出内容。

            <strong>extra</strong> `array`

            模型获取的网络搜索与参考信息。

            <strong>deep\_research</strong>`object`

            仅在`answer`与`WebResearch`阶段包含获取的网络搜索与参考信息，其余阶段均为null。

            <strong>research</strong>`object`

            模型的研究过程与内容信息。

            <Accordion title="属性">
              <strong>researchGoal</strong>`string`

              研究目标。

              <strong>query</strong>`string`

              研究过程中的搜索内容。

              <strong>id</strong>`integer`

              搜索的轮数\*，\*取值范围 \[1-15]。

              <strong>learningMap</strong>`object`

              从调用工具总结获取到的内容，和调用工具相关联。

              <strong>references</strong>`object`

              模型生成答案所引用的内容，仅回答阶段包含此参数。

              <Accordion title="属性">
                <strong>icon</strong> `string`

                参考内容URL的网页图标链接。

                <strong>index\_number</strong> `integer`

                参考内容的索引。

                <strong>description</strong>`string`

                参考内容的简介。

                <strong>title</strong>`string`

                参考内容的网页标题。

                <strong>url</strong> `string`

                参考内容的网页URL。
              </Accordion>

              <strong>webSites</strong>`object`

              模型研究过程中所参考的内容，仅网络搜索阶段包含此参数。

              <Accordion title="属性">
                <strong>icon</strong> `string`

                参考内容URL的网页图标链接。

                <strong>index\_number</strong> `integer`

                参考内容的索引。

                <strong>description</strong>`string`

                参考内容的简介。

                <strong>title</strong>`string`

                参考内容的网页标题。

                <strong>url</strong> `string`

                参考内容的网页URL。
              </Accordion>
            </Accordion>

            <strong>status</strong>`string`

            模型输出过程中不同阶段的状态：

            - typing：正在生成该阶段内容。
            - finished：阶段已完成。
            - streamingQueries：正在生成研究目标和搜索查询
            - streamingWebResult：正在执行搜索、网页阅读和代码执行
            - WebResultFinished：网络搜索阶段完成
          </Accordion>

          <strong>finished</strong>`boolean`

          标识模型的内容流式输出是否已全部完成。有以下情况：

          - 内容仍在持续输出中为`false`；
          - 内容已全部输出完毕，当前为最后一个响应为`true`

          <strong>finished\_reason</strong> `string`

          标识模型的内容流式输出结束的原因。有以下情况：

          - 正在生成时为`null`；
          - 模型内容流式输出自然结束为`stop`
        </Accordion>

        <strong>usage</strong>`object`

        本次请求使用的Token信息。

        <Accordion title="属性">
          <strong>input\_tokens</strong> `integer`

          输入 Token 数。

          <strong>output\_tokens</strong> `integer`

          输出 Token 数。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json 研究规划阶段 expandable
          {
            "status_code": 200,
            "request_id": "2a6187f0-7e7b-40bb-a87e-xxx",
            "code": "",
            "message": "",
            "output": {
                  "text": null,
                  "finish_reason": null,
                  "choices": null,
                  "message": {
                      "phase": "ResearchPlanning",
                      "role": "assistant",
                      "content": "",
                      "extra": {
                          "deep_research": {}
                      },
                      "status": "typing"
                  },
                  "fininshed": false,
                  "fininshed_reason": "null"
              },
              "usage": {
                  "input_tokens": 694,
                  "output_tokens": 0
              },
              "request_id": "2a6187f0-7e7b-40bb-xxx"
          }
          ```

          ```json 网络搜索阶段 expandable
          {
            "status_code": 200,
            "request_id": "2a6187f0-7e7b-40bb-a87e-xxx",
            "code": "",
            "message": "",
            "output": {
              "message": {
                "phase": "WebResearch",
                "role": "assistant",
                "content": "",
                "extra": {
                  "deep_research": {
                    "query": {
                      "researchGoal": "通过查找",
                      "query": "",
                      "id": 1
                    }
                  }
                },
                "status": "streamingQueries"
              },
              "fininshed": false,
              "fininshed_reason": "null"
            },
            "usage": {
              "input_tokens": 694,
              "output_tokens": 0
            }
          }
          ```

          ```json 连接保持阶段
          {
            "status_code": 200,
            "request_id": "2a6187f0-7e7b-40bb-a87e-xxx",
            "code": "",
            "message": "",
            "output": {
              "message": {
                "phase": "KeepAlive",
                "role": "assistant",
                "content": "",
                "extra": {
                  "deep_research": {}
                },
                "status": "typing"
              },
              "fininshed": false,
              "fininshed_reason": "null"
            },
            "usage": {
              "input_tokens": 694,
              "output_tokens": 0
            }
          }
          ```

          ```json 反问确认与回答阶段 expandable
          {
            "status_code": 200,
            "request_id": "2a6187f0-7e7b-40bb-a87e-xxx",
            "code": "",
            "message": "",
            "output": {
              "message": {
                "phase": "answer",
                "role": "assistant",
                "content": "，这些承诺相互",
                "extra": {
                  "deep_research": {
                    "references": [
                      {
                        "icon": "",
                        "index_number": 1,
                        "description": "计划中设想的两个xxx从未在 ",
                        "title": "历史和背景| 联合国 - the United Nations",
                        "url": "https://www.un.org/xxx"
                      }
                    ]
                  }
                },
                "status": "typing"
              },
              "fininshed": false,
              "fininshed_reason": "null"
            },
            "usage": {
              "input_tokens": 694,
              "output_tokens": 0
            }
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>
