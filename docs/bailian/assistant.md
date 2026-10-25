> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Assistants（下线中）

> Assistant API 旨在简化 Assistant（一种大模型应用）的构建流程。本文详细介绍了 Assistant API 提供的各项 Assistant 管理方法，包括 Assistant 的创建、列举、检索、更新和删除操作。

<meta property="og:title" content="Assistants（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

> <strong>功能介绍</strong>：如果您想了解 Assistant API 的功能和基本用法，请参考[Assistant API功能概览](/zh/model-studio/assistant-api)。

> <strong>有效期限</strong>：所有 Assistant 实例均保存在阿里云百炼服务器上，目前没有失效日期，您可通过 assistant.id 检索智能体。

<Note>
  [智能体应用（Agent 1.0）](/zh/model-studio/single-agent-application)和 Assistant 虽然均为大模型应用，但二者的功能相互独立，使用方法也不相同。

  - 智能体应用：仅可使用控制台进行创建、查看、更新和删除，以及通过[应用调用 API](/zh/model-studio/agent-and-workflow-application-api-reference)进行调用。
  - Assistant：仅可使用 Assistant API 创建、查看、更新、删除和调用 Assistant 。
</Note>

## 创建智能体 <span id="175dada553tmp" />

创建一个新的智能体。

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/assistants' \
    --header "Content-Type: application/json" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "model": "qwen-max",
        "name": "智能小助手",
        "description": "这是一个智能小助手",
        "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
        "tools": [
            {
                "type": "code_interpreter"
            }
        ],
        "metadata": {}
    }'
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
      <colgroup>
        <col style={{ width: "21.502209%" }} />

        <col style={{ width: "47.42268%" }} />

        <col style={{ width: "14.138439%" }} />

        <col style={{ width: "16.936672%" }} />
      </colgroup>

      <thead>
        <tr>
          <th>
            <strong>参数名</strong>
          </th>

          <th>
            <strong>描述</strong>
          </th>

          <th>
            <strong>参数类型</strong>
          </th>

          <th>
            <strong>是否必须</strong>
          </th>
        </tr>
      </thead>

      <tbody>
        <tr>
          <td>
            model
          </td>

          <td>
            智能体所使用的模型
          </td>

          <td>
            str
          </td>

          <td>
            是
          </td>
        </tr>

        <tr>
          <td>
            name
          </td>

          <td>
            智能体使用的名字
          </td>

          <td>
            str
          </td>

          <td>
            否
          </td>
        </tr>

        <tr>
          <td>
            description
          </td>

          <td>
            用于描述智能体的文字
          </td>

          <td>
            str
          </td>

          <td>
            否
          </td>
        </tr>

        <tr>
          <td>
            instructions
          </td>

          <td>
            System prompt 用于智能体里的大模型
          </td>

          <td>
            str
          </td>

          <td>
            否
          </td>
        </tr>

        <tr>
          <td>
            tools
          </td>

          <td>
            一个可以被智能体调用tools list.

            自定义插件的鉴权信息传递

            ```json
            {
             "type": "${插件id}",
                 "auth": {  # 当使用“用户级鉴权”时，才使用这一字段
                     "type": "user_http",
                     "user_token": "bearer-token",
                     }
             }
            ```
          </td>

          <td>
            Optional\[List\[Dict]]
          </td>

          <td>
            否（default=\[])
          </td>
        </tr>

        <tr>
          <td>
            metadata
          </td>

          <td>
            其他相关参数用于智能体，用于存储其他相关参数
          </td>

          <td>
            Dict
          </td>

          <td>
            否
          </td>
        </tr>

        <tr>
          <td>
            temperature
          </td>

          <td>
            用于控制随机性和多样性的程度。
          </td>

          <td>
            float
          </td>

          <td>
            否
          </td>
        </tr>

        <tr>
          <td>
            top\_p
          </td>

          <td>
            生成时，核采样方法的概率阈值。
          </td>

          <td>
            float
          </td>

          <td>
            否
          </td>
        </tr>

        <tr>
          <td>
            top\_k
          </td>

          <td>
            生成时，采样候选集的大小。
          </td>

          <td>
            integer
          </td>

          <td>
            否
          </td>
        </tr>
      </tbody>
    </table>

    <strong>返回结果</strong>

    ```json
    {
        "id": "asst_49079f4b-d1e8-4015-a12e-2dcdd1f18d84",
        "object": "assistant",
        "created_at": 1711713885724,
        "model": "qwen-max",
        "name": "智能小助手",
        "description": "这是一个智能小助手",
        "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
        "tools": [
            {
                "type": "code_interpreter"
            }
        ],
        "metadata": {},
        "temperature": null,
        "top_p": null,
        "top_k": null,
        "max_tokens": null,
        "request_id": "b1778226-3865-9006-9e95-56329a710322"
    }
    ```

    <strong>输出参数</strong>

    一个[智能体对象](/zh/model-studio/assistant#1718e4e39302j)。
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python expandable
      from dashscope import Assistants
      import os

      assistant = Assistants.create(
              # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
              model='qwen-max',
              name='smart helper',
              description='A tool helper.',
              instructions='You are a helpful assistant. When asked a question, use tools wherever possible.',
              tools=[{
                  'type': 'search'
              }, {
                  'type': 'function',
                  'function': {
                      'name': 'big_add',
                      'description': 'Add to number',
                      'parameters': {
                          'type': 'object',
                          'properties': {
                              'left': {
                                  'type': 'integer',
                                  'description': 'The left operator'
                              },
                              'right': {
                                  'type': 'integer',
                                  'description': 'The right operator.'
                              }
                          },
                          'required': ['left', 'right']
                      }
                  }
              }],
      )
      print(assistant)
      ```

      ```java expandable
      import com.alibaba.dashscope.assistants.Assistant;
      import com.alibaba.dashscope.assistants.AssistantParam;
      import com.alibaba.dashscope.assistants.Assistants;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import java.lang.System;
      import com.alibaba.dashscope.utils.JsonUtils;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Assistants assistants = new Assistants();
              // build assistant parameters
              AssistantParam param = AssistantParam.builder()
                      // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      .model("qwen-max")
                      .name("intelligent guide")
                      .description("a smart guide")
                      .instructions("You are a helpful assistant.")
                      // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .build();
              Assistant assistant = assistants.create(param);
              System.out.println(JsonUtils.toJson(assistant));
              // use assistant

          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>model</p></td><td><p><em>string</em></p></td><td><p>-</p></td><td><p>指定用于智能体所使用的模型</p></td></tr><tr><td><p>name</p></td><td><p><em>string</em></p></td><td><p>-</p></td><td><p>指定智能体名称</p></td></tr><tr><td><p>description</p></td><td><p><em>string</em></p></td><td><p>-</p></td><td><p>智能体描述</p></td></tr><tr><td><p>instructions</p></td><td><p><em>string</em></p></td><td><p>-</p></td><td><p>指定智能体功能信息</p></td></tr><tr><td><p>tools</p></td><td><p><em>array</em></p></td><td><p>\[]</p></td><td><p>智能体使用的tools</p><div><p>自定义tool鉴权信息传递</p><p><code>\{</code></p><p><code>"type": "plugin\_type", "auth": \{"type": "user\_http","user\_token": "bearer-token", }</code></p><p><code>}</code></p><p>assistant在请求插件时会将bearer放在<code>\{"plugin\_type":\{"user\_token": "bearer-token"}}</code>添加到调用plugin的header里.</p></div></td></tr><tr><td><p>metadata</p></td><td><p><em>object</em></p></td><td><p>None</p></td><td><p>智能体关联信息</p></td></tr><tr><td><p>workspace</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json expandable
    {
       "tools":[
          {
             "type":"search"
          },
          {
             "function":{
                "name":"big_add",
                "description":"Add to number",
                "parameters":{
                   "type":"object",
                   "properties":{
                      "left":{
                         "type":"integer",
                         "description":"The left operator"
                      },
                      "right":{
                         "type":"integer",
                         "description":"The right operator."
                      }
                   },
                   "required":[
                      "left",
                      "right"
                   ]
                }
             },
             "type":"function"
          }
       ],
       "id":"asst_714cac72-81b2-49bf-a75d-c575b90a9398",
       "object":"assistant",
       "created_at":1726033638848,
       "model":"qwen-max",
       "name":"smart helper",
       "description":"A tool helper.",
       "instructions":"You are a helpful assistant. When asked a question, use tools wherever possible.",
       "file_ids":[

       ],
       "metadata":{

       },
       "temperature":"None",
       "top_p":"None",
       "top_k":"None",
       "max_tokens":"None",
       "request_id":"00f5962e-8d9f-92fd-9320-3173fa1525d6",
       "status_code":200
    }
    ```

    <strong>输出参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.300578%" }} /><col style={{ width: "15.953757%" }} /><col style={{ width: "67.745665%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>字段类型</strong></p></th><th><p><strong>字段描述</strong></p></th></tr></thead><tbody><tr><td><p>status\_code</p></td><td><p><em>integer</em></p></td><td><p>为调用http status code，200表示调用成功，其他表示调用出错。</p></td></tr><tr><td><p>name</p></td><td><p><em>string</em></p></td><td><p>智能体名称。</p></td></tr><tr><td><p>id</p></td><td><p><em>string</em></p></td><td><p>智能体ID，为uuid字符串。</p></td></tr><tr><td><p>model</p></td><td><p><em>string</em></p></td><td><p>智能体使用的模型名称。</p></td></tr><tr><td><p>description</p></td><td><p><em>string</em></p></td><td><p>智能体描述信息。</p></td></tr><tr><td><p>instructions</p></td><td><p><em>string</em></p></td><td><p>指定智能体功能信息。</p></td></tr><tr><td><p>metadata</p></td><td><p><em>object</em></p></td><td><p>智能体的元数据。</p></td></tr><tr><td><p>tools</p></td><td><p><em>array</em></p></td><td><p>智能体可以用的tool列表。</p></td></tr><tr><td><p>created\_at</p></td><td><p><em>integer</em></p></td><td><p>智能体创建的时间戳表示。</p></td></tr><tr><td><p>code</p></td><td><p><em>string</em></p></td><td><p>表示请求失败，表示错误码，请求成功时忽略。只有在通过Python调用失败时会显示。</p></td></tr><tr><td><p>message</p></td><td><p><em>string</em></p></td><td><p>失败，表示失败详细信息，成功忽略。只有在通过Python调用失败时会显示。</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 列出智能体 <span id="Bj3EB" />

用于返回智能体列表。

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/assistants?limit=2&order=desc' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.96%" }} /><col style={{ width: "27.96%" }} /><col style={{ width: "24.49%" }} /><col style={{ width: "19.59%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>limit</p></td><td><p>创建智能体的数量</p></td><td><p>int</p></td><td><p>否</p></td></tr><tr><td><p>order</p></td><td><p>按照创建时间升序或降序</p></td><td><p>str</p></td><td><p>否（默认为<code>desc</code>）</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json expandable
    {
        "object": "list",
        "data": [
            {
                "id": "asst_0678aa33-43e2-4268-95e6-b0010f9f7937",
                "object": "assistant",
                "created_at": 1711435564909,
                "model": "qwen-max",
                "name": "智能小助手",
                "description": "这是一个智能小助手",
                "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
                "tools": [
                    {
                        "type": "search"
                    },
                    {
                        "type": "text_to_image"
                    },
                    {
                        "type": "code_interpreter"
                    }
                ],
                "metadata": {}
            },
            {
                "id": "asst_7af23142-52bc-4218-aa98-dfdb1128f19c",
                "object": "assistant",
                "created_at": 1711422620443,
                "model": "qwen-max",
                "name": "helpful assistant",
                "description": "",
                "instructions": "You are a helpful assistant.",
                "tools": [
                    {
                        "type": "text_to_image"
                    }
                ],
                "file_ids": [],
                "metadata": {}
            }
        ],
        "first_id": "asst_0678aa33-43e2-4268-95e6-b0010f9f7937",
        "last_id": "asst_7af23142-52bc-4218-aa98-dfdb1128f19c",
        "has_more": true,
        "request_id": "bc257359-ce86-9547-98be-d804effba8d1"
    }
    ```

    <strong>输出参数</strong>

    输出一个[智能体对象](/zh/model-studio/assistant#1718e4e39302j)列表。
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Assistants
      import os

      assistants = Assistants.list(
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          limit=1,
          order='desc'
      )
      ```

      ```java
      import com.alibaba.dashscope.assistants.Assistant;
      import com.alibaba.dashscope.assistants.Assistants;
      import com.alibaba.dashscope.common.GeneralListParam;
      import com.alibaba.dashscope.common.ListResult;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
            Assistants assistants = new Assistants();
            GeneralListParam listParam = GeneralListParam.builder()
                      // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .limit(10l)
                      .build();
            ListResult<Assistant> assistant = assistants.list(listParam);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>limit</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>指定列出assistant的数目，如果未指定则使用服务端默认值。</p></td></tr><tr><td><p>order</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>排序方式，可选值：desc(降序)、asc(升序)，如果未指定则使用服务端默认值。</p></td></tr><tr><td><p>workspace</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>字段类型</strong></p></th><th><p><strong>字段描述</strong></p></th></tr></thead><tbody><tr><td><p>has\_more</p></td><td><p><em>boolean</em></p></td><td><p>表示是否还有更多数据可以获取。</p></td></tr><tr><td><p>last\_id</p></td><td><p><em>string</em></p></td><td><p>data中最后一个assistant的ID。</p></td></tr><tr><td><p>first\_id</p></td><td><p><em>string</em></p></td><td><p>data中第一个assistant的ID。</p></td></tr><tr><td><p>data</p></td><td><p><em>array</em></p></td><td><p>智能体对象列表。</p></td></tr><tr><td><p>object</p></td><td><p><em>string</em></p></td><td><p>data的数据格式，如<code>"list"</code>。</p></td></tr><tr><td><p>request\_id</p></td><td><p><em>string</em></p></td><td><p>本次请求的ID。</p></td></tr><tr><td><p>status\_code</p></td><td><p><em>integer</em></p></td><td><p>请求状态码。</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 检索智能体 <span id="TobTm" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/assistants/asst_0678aa33-43e2-4268-95e6-b0010f9f7937' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>assistant\_id</p></td><td><p>待检索的智能体 ID</p></td><td><p>str</p></td><td><p>是</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json
    {
        "id": "asst_0678aa33-43e2-4268-95e6-b0010f9f7937",
        "object": "assistant",
        "created_at": 1711435564909,
        "model": "qwen-max",
        "name": "智能小助手",
        "description": "这是一个智能小助手",
        "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
        "tools": [
            {
                "type": "search"
            },
            {
                "type": "text_to_image"
            },
            {
                "type": "code_interpreter"
            }
        ],
        "metadata": {},
        "request_id": "f0ec05b0-8813-984c-81b5-1166ae3478d1"
    }
    ```

    <strong>输出参数</strong>

    输出检索到的[智能体对象](/zh/model-studio/assistant#1718e4e39302j)。
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Assistants
      import os

      assistant = Assistants.retrieve(
          assistant_id='your_assistant_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY")
      )

      print(assistant)
      ```

      ```java
      import com.alibaba.dashscope.assistants.Assistant;
      import com.alibaba.dashscope.assistants.Assistants;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;

      public class RetrieveAssistantExample {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Assistants assistants = new Assistants();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              Assistant assistant = assistants.retrieve("assistant_id", apiKey);
              System.out.println(assistant);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>assistant\_id</p></td><td><p><em>string</em></p></td><td><p>-</p></td><td><p>指定要查询的智能体 ID</p></td></tr><tr><td><p>workspace</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    请参考[智能体对象](/zh/model-studio/assistant#1718e4e39302j)。
  </Tab>
</Tabs>

## 更新智能体 <span id="jKGOA" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/assistants/asst_0678aa33-43e2-4268-95e6-b0010f9f7937' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "instructions": "你是一个搜索助手",
        "name": "新建应用-assistantAPI",
        "description": "",
        "model": "qwen-max"
    }'
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><tbody><tr><td><p><strong>输入参数名字</strong></p></td><td><p><strong>输入参数的描述</strong></p></td><td><p><strong>参数类型</strong></p></td><td><p><strong>是否必须</strong></p></td></tr><tr><td><p>assistant\_id</p></td><td><p>待检索的智能体 ID</p></td><td><p>str</p></td><td><p>是</p></td></tr><tr><td><p>\*</p></td><td><p>其他可选的输入参数</p></td><td><p>str</p></td><td><p>否</p></td></tr><tr><td><p>model</p></td><td><p>智能体所使用的大模型 ID</p></td><td><p>str</p></td><td><p>是</p></td></tr><tr><td><p>name</p></td><td><p>智能体使用的名字</p></td><td><p>str</p></td><td><p>否</p></td></tr><tr><td><p>description</p></td><td><p>用于描述智能体的文字</p></td><td><p>str</p></td><td><p>否</p></td></tr><tr><td><p>instructions</p></td><td><p>System prompt 用于智能体里的大模型</p></td><td><p>str</p></td><td><p>否</p></td></tr><tr><td><p>tools</p></td><td><p>一个可以被智能体调用tools list，用户需要在阿里云百炼中被注册</p></td><td><p>Optional\[List\[Dict]]</p></td><td><p>否（default=\[]）</p></td></tr><tr><td><p>metadata</p></td><td><p>其他相关参数用于智能体，用于存储其他相关参数</p></td><td><p>Dict</p></td><td><p>否</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json
    {
        "id": "asst_0678aa33-43e2-4268-95e6-b0010f9f7937",
        "object": "assistant",
        "created_at": 1711435564909,
        "model": "qwen-max",
        "name": "新建应用-assistantAPI",
        "description": "",
        "instructions": "你是一个搜索助手",
        "tools": [
            {
                "type": "search"
            },
            {
                "type": "text_to_image"
            },
            {
                "type": "code_interpreter"
            }
        ],
        "metadata": {},
        "request_id": "b0993831-a98b-9e71-b235-75174df9046e"
    }
    ```

    <strong>输出参数</strong>

    输出更新后的[智能体对象](/zh/model-studio/assistant#1718e4e39302j)。
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Assistants
      import os

      assistants = Assistants.update(
          'assistant_id',
          model='new_model_name',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY")
      )
      ```

      ```java
      import com.alibaba.dashscope.assistants.Assistant;
      import com.alibaba.dashscope.assistants.AssistantParam;
      import com.alibaba.dashscope.assistants.Assistants;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Assistants assistants = new Assistants();
              AssistantParam param = AssistantParam.builder()
                      .model("qwen-max")
                      .name("intelligent guide")
                      .description("a smart guide")
                      .instructions("You are a helpful assistant.  When asked a question, use tools wherever possible.")
                      // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .build();

              Assistant assistant = assistants.update("assistant_id", param);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>assistant\_id</p></td><td><p><em>string</em></p></td><td><p>-</p></td><td><p>指定需要更新的assistant ID</p></td></tr><tr><td><p>model</p></td><td><p><em>string</em></p></td><td><p>-</p></td><td><p>指定用于智能体所使用的模型</p></td></tr><tr><td><p>name</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>指定智能体名称</p></td></tr><tr><td><p>description</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>智能体描述</p></td></tr><tr><td><p>instructions</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>指定智能体功能信息。</p></td></tr><tr><td><p>tools</p></td><td><p><em>array</em></p></td><td><p>\[]</p></td><td><p>智能体使用的tools</p></td></tr><tr><td><p>metadata</p></td><td><p><em>object</em></p></td><td><p>None</p></td><td><p>智能体关联信息</p></td></tr><tr><td><p>workspace</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    请参考[智能体对象](/zh/model-studio/assistant#1718e4e39302j)。
  </Tab>
</Tabs>

## 删除智能体 <span id="mnZ7Z" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location --request DELETE 'https://dashscope.aliyuncs.com/api/v1/assistants/asst_0678aa33-43e2-4268-95e6-b0010f9f7937' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><tbody><tr><td><p><strong>输入参数名字</strong></p></td><td><p><strong>输入参数的描述</strong></p></td><td><p><strong>参数类型</strong></p></td><td><p><strong>是否必须</strong></p></td></tr><tr><td><p>assistant\_id</p></td><td><p>待检索的智能体 ID</p></td><td><p>str</p></td><td><p>是</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json
    {
        "id": "asst_0678aa33-43e2-4268-95e6-b0010f9f7937",
        "object": "assistant.deleted",
        "deleted": true,
        "request_id": "6af9320f-0430-9d01-b92f-d1beb6424dc5"
    }
    ```

    <strong>输出参数</strong>

    输出删除智能体后的状态。
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Assistants
      import os

      assistants = Assistants.delete(
          'assistant_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY")
      )
      ```

      ```java
      import com.alibaba.dashscope.assistants.Assistants;
      import com.alibaba.dashscope.common.DeletionStatus;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Assistants assistants = new Assistants();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              DeletionStatus assistant = assistants.delete("assistant_id", apiKey);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>assistant\_id</p></td><td><p><em>string</em></p></td><td><p>-</p></td><td><p>指定要删除的智能体 ID</p></td></tr><tr><td><p>workspace</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p><em>string</em></p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>字段类型</strong></p></th><th><p><strong>字段描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p><em>string</em></p></td><td><p>删除的对象的ID</p></td></tr><tr><td><p>object</p></td><td><p><em>string</em></p></td><td><p>本次请求目标，示例：<code>'assistant.deleted'</code></p></td></tr><tr><td><p>deleted</p></td><td><p><em>boolean</em></p></td><td><p>是否删除</p></td></tr><tr><td><p>request\_id</p></td><td><p><em>string</em></p></td><td><p>本次请求的ID。</p></td></tr><tr><td><p>status\_code</p></td><td><p><em>integer</em></p></td><td><p>请求状态码。</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 智能体对象 <span id="1718e4e39302j" />

一个能够调用模型并使用工具的`Assistant`对象。

<strong>智能体对象示例</strong>

```json
{
    "id": "asst_49079f4b-d1e8-4015-a12e-2dcdd1f18d84",
    "object": "assistant",
    "created_at": 1711713885724,
    "model": "qwen-max",
    "name": "智能小助手",
    "description": "这是一个智能小助手",
    "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
    "tools": [
        {
            "type": "code_interpreter"
        }
    ],
    "metadata": {},
    "temperature": null,
    "top_p": null,
    "top_k": null,
    "max_tokens": null,
    "request_id": "b1778226-3865-9006-9e95-56329a710322"
}
```

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.38%" }} /><col style={{ width: "11.98%" }} /><col style={{ width: "72.63%" }} /></colgroup><thead><tr><th><p><strong>参数名</strong></p></th><th><p><strong>数据类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>string</p></td><td><p>智能体唯一标识符，即 assistant ID。</p></td></tr><tr><td><p>object</p></td><td><p>string</p></td><td><p>对象类型，始终为<code>assistant</code>。</p></td></tr><tr><td><p>created\_at</p></td><td><p>integer</p></td><td><p>创建智能体时的 Unix 13位时间戳（以毫秒为单位）。</p></td></tr><tr><td><p>model</p></td><td><p>string</p></td><td><p>智能体使用的模型名。您可以在<a href="/zh/model-studio/assistant-api">Assistant API功能概览</a>查看所有可用模型，或者查看<a href="/zh/model-studio/models">选择模型</a>以了解它们的详情。</p></td></tr><tr><td><p>name</p></td><td><p>string</p></td><td><p>智能体的名称。</p></td></tr><tr><td><p>description</p></td><td><p>string</p></td><td><p>智能体的描述。</p></td></tr><tr><td><p>instructions</p></td><td><p>string</p></td><td><p>智能体使用的系统指令。</p></td></tr><tr><td><p>tools</p></td><td><p>array</p></td><td><p>智能体上启用的工具列表。工具的类型可以是官方插件（code\_interpreter 代码解释器、quark\_search 夸克搜索、text\_to\_image 文生图）、知识检索增强（RAG）或函数调用（Function Calling）。</p></td></tr><tr><td><p>metadata</p></td><td><p>dict</p></td><td><p>以结构化格式存储的智能体对象的附加信息。</p></td></tr><tr><td><p>temperature</p></td><td><p>float</p></td><td><p>采样温度，其取值在 0 到 2 之间。较高的值（如 1）会使输出更加随机；而较低的值（如 0.2）则会使输出更加集中和确定。</p></td></tr><tr><td><p>top\_p</p></td><td><p>float</p></td><td><p>一种替代温度采样的方法，称为核采样。在这种采样方法中，大模型选取具有前 top\_p 的累积概率质量的Token结果。其值为 0.1 表示仅考虑构成前 10% 概率质量的Token。</p><p>通常建议调整此参数或<code>temperature</code>，但不要同时调整两者。</p></td></tr><tr><td><p>top\_k</p></td><td><p>integer</p></td><td><p>与top\_p类似，但样本是从 k 个最高概率的Token中选取的，而不考虑它们的累积概率质量。</p><p>不要同时调整此参数和<code>temperature</code>或<code>top\_p</code>。</p></td></tr><tr><td><p>max\_tokens</p></td><td><p>integer</p></td><td><p>智能体能够一次性生成Token的最大数量。</p></td></tr><tr><td><p>request\_id</p></td><td><p>string</p></td><td><p>智能体关联的调用的唯一标识符。</p></td></tr></tbody></table>

## 错误码 <span id="97e145aeabbwf" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
