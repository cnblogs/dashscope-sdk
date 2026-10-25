> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取输入项列表

> 获取生成指定 Response 时所使用的输入项列表。在多轮对话（使用 previous_response_id 串联）中，会一并返回历史轮次的用户输入和模型回复。仅当原创建请求中 store=true 时，返回的 Response ID 才支持查询。

<meta property="og:title" content="获取输入项列表" />

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tabs>
  <Tab title="华北2（北京）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses/{response_id}/input_items`
  </Tab>
</Tabs>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ## 路径参数 <span id="list-req-path-h2" />

        <strong>response\_id</strong>`string`（必选）

        要查询输入项的 Response ID，格式为 `resp_xxx`。仅当原创建请求中 `store=true` 时，返回的 Response ID 才支持查询。

        ## 查询参数 <span id="list-query-h2" />

        <strong>after</strong>`string`（可选）

        以指定的 item ID（格式为 `msg_xxx`）作为起点，返回排在该 ID 之后的数据，顺序由 `order` 决定。<strong>item ID 来源</strong>：[创建响应](/zh/model-studio/qwen-api-via-openai-responses)返回的 `output[].id`；或本接口返回的 `data[].id`、`first_id`、`last_id`。常用于翻页：把上一次返回的 `last_id` 作为本次的 `after` 传入，即可继续获取后续数据。

        <strong>limit</strong>`integer`（可选）

        返回的最大条数，取值范围 \[1, 100]，默认值为 20。

        <strong>order</strong>`string`（可选）

        排序方式，支持 `asc`（升序）和 `desc`（降序），默认值为 `desc`。
      </td>

      <td>
        <Tabs>
          <Tab title="默认查询">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )

              response = client.responses.input_items.list("resp_xxx")
              print(response.data)
              ```

              ```javascript Node.js
              import OpenAI from "openai";

              const openai = new OpenAI({
                  // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx"
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
              });

              async function main() {
                  const response = await openai.responses.inputItems.list("resp_xxx");
                  console.log(response.data);
              }

              main();
              ```

              ```bash curl
              curl https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses/resp_xxx/input_items \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY"
              ```
            </CodeGroup>
          </Tab>

          <Tab title="按条件查询">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )

              response = client.responses.input_items.list(
                  "resp_xxx",
                  # after 取 item ID（msg_xxx）：可来自创建响应返回的 output[].id，或本接口返回的 data[].id / first_id / last_id
                  after="msg_xxx",
                  limit=10,
                  order="asc",
              )
              print(response.data)
              ```

              ```javascript Node.js
              import OpenAI from "openai";

              const openai = new OpenAI({
                  // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx"
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
              });

              async function main() {
                  const response = await openai.responses.inputItems.list("resp_xxx", {
                      // after 取 item ID（msg_xxx）：可来自创建响应返回的 output[].id，或本接口返回的 data[].id / first_id / last_id
                      after: "msg_xxx",
                      limit: 10,
                      order: "asc",
                  });
                  console.log(response.data);
              }

              main();
              ```

              ```bash curl
              # after 取 item ID（msg_xxx）：可来自创建响应返回的 output[].id，或本接口返回的 data[].id / first_id / last_id
              curl "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses/resp_xxx/input_items?after=msg_xxx&limit=10&order=asc" \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY"
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
        ## 返回结果 <span id="list-resp-h2" />

        <strong>data</strong> `array`

        输入项列表。每个元素是一个消息对象，包含 `id`、`role`、`content`、`type`、`status` 字段。`role` 取值为 `user` 或 `assistant`；`content` 中元素的 `type` 取值为 `input_text`（用户输入）或 `output_text`（模型回复）。多轮对话（使用 `previous_response_id` 串联）中，会按发生顺序包含历史用户消息与历史模型回复。

        <strong>first\_id</strong> `string`

        列表中第一个元素的 ID。

        <strong>last\_id</strong> `string`

        列表中最后一个元素的 ID。

        <strong>has\_more</strong> `boolean`

        是否还有未返回的数据。当为 `true` 时，把本次返回的 `last_id` 作为下一次请求的 `after` 参数，可继续获取后续数据。

        <strong>id</strong> `string`

        对应的 Response ID。

        <strong>model</strong> `string`

        生成该 Response 时使用的模型名称。

        <strong>created\_at</strong> `integer`

        Response 创建时间的 Unix 时间戳（<strong>毫秒</strong>）。注意：与"创建响应/检索响应"接口返回的 `created_at`（秒）单位不同。

        <strong>previous\_response\_id</strong> `string`

        多轮对话（使用 `previous_response_id` 串联）时返回，值为上一轮的 Response ID。
      </td>

      <td>
        ```json
        {
            "created_at": 1778674066000,
            "data": [
                {
                    "content": [
                        {
                            "text": "你好",
                            "type": "input_text"
                        }
                    ],
                    "id": "msg_8dac4972-fdba-42b5-bed9-280441ced8ea",
                    "role": "user",
                    "status": "completed",
                    "type": "message"
                }
            ],
            "first_id": "msg_8dac4972-fdba-42b5-bed9-280441ced8ea",
            "has_more": false,
            "id": "resp_4ca7fa5e-6ff5-9787-bc18-af6ca5eff36c",
            "last_id": "msg_8dac4972-fdba-42b5-bed9-280441ced8ea",
            "model": "qwen3.8-max"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 错误响应 <span id="list-error-h2" />

当指定的 Response ID 不存在时，返回以下错误：

```json
{
    "request_id": "339b771548c7fbb15717316b49627681",
    "code": "InvalidParameter",
    "message": "Response with id 'resp_xxx' not found."
}
```
