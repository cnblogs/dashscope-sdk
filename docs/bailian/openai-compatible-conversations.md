> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# OpenAI Conversations接口兼容

> 在跨设备或长时间中断的对话中，手动维护消息列表容易丢失上下文。阿里云百炼提供兼容 OpenAI 的 Conversations API。配合 Responses API，可自动注入历史上下文，无需手动同步消息，实现跨场景、跨设备的对话延续。

<meta property="og:title" content="OpenAI Conversations接口兼容" />

## Create conversation <span id="h2-create-conversation" />

创建一个新会话，可同时添加初始消息项。

<strong>华北2（北京）：POST</strong> `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations`

<strong>新加坡：POST</strong> `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/conversations`

<Tip>
  旧版URL路径 `/api/v2/apps/protocols/compatible-mode/v1/conversations` 即将停止维护，请尽快迁移至新版路径 `/compatible-mode/v1/conversations`。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>items</strong>`array`（可选）

        初始消息项列表，最多20条。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`<strong>(必选)</strong>

          消息类型，仅支持 `message`。

          <strong>role</strong>`string`<strong>(必选)</strong>

          消息的角色。`system` 与`developer` 角色的指令优先级高于 `user` 角色，`assistant` 角色表示模型在之前交互中生成的消息。取值：`user` 、`assistant` 、`system` 、`developer` 。

          <strong>content</strong>`string or array`<strong>(必选)</strong>

          消息内容。支持纯文本字符串或结构化内容列表（如 ResponseInputText 对象数组），列表格式可包含文本等多种内容类型。
        </Accordion>

        <strong>metadata</strong>`object`（可选）

        会话元数据，用于以结构化格式存储会话的附加信息。最多16对键值对，key最大长度64字符，value最大长度512字符。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          conversation = client.conversations.create(
              metadata={"topic": "demo"},
              items=[
                  {"type": "message", "role": "system", "content": "李红，一位温婉而坚韧的江南女子，出生在浙江省杭州市，她今年20岁，她的兴趣爱好是琴棋书画。"}
              ]
          )
          print(conversation)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const client = new OpenAI({
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          });

          const conversation = await client.conversations.create({
              metadata: { topic: "demo" },
              items: [
                  {
                      type: "message",
                      role: "system",
                      content: "李红，一位温婉而坚韧的江南女子，出生在浙江省杭州市，她今年20岁，她的兴趣爱好是琴棋书画。"
                  }
              ]
          });
          console.log(conversation);
          ```

          ```bash cURL
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations' \
          --header 'Content-Type: application/json' \
          --header 'Authorization: Bearer $DASHSCOPE_API_KEY' \
          --data '{
              "metadata": {
                  "topic": "demo"
              },
              "items": [
                  {
                      "type": "message",
                      "role": "system",
                      "content": "李红，一位温婉而坚韧的江南女子，出生在浙江省杭州市，她今年20岁，她的兴趣爱好是琴棋书画"
                  }
              ]
          }'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 响应参数 <span id="h3-create-response" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>created\_at</strong>`integer`

        会话创建的 Unix 时间戳（毫秒）。

        <strong>id</strong>`string`

        会话唯一标识符。

        <strong>metadata</strong>`object`

        会话元数据，以键值对形式存储的附加信息。最多16对，key最大长度64字符，value最大长度512字符。

        <strong>object</strong>`string`

        对象类型，固定为 `conversation`。
      </td>

      <td>
        ```json
        {
            "created_at": 1771316949128,
            "id": "conv_xxx",
            "metadata": {
                "topic": "demo"
            },
            "object": "conversation"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## Retrieve conversation <span id="h2-retrieve-conversation" />

获取指定会话的信息。

<strong>华北2（北京）：GET</strong> `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}`

<strong>新加坡：GET</strong> `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>conversation\_id</strong>`string`<strong>(必选, Path)</strong>

        会话ID。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          conversation = client.conversations.retrieve("conv_xxx")
          print(conversation)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const client = new OpenAI({
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          });

          const conversation = await client.conversations.retrieve(
              "conv_xxx"
          );
          console.log(conversation);
          ```

          ```bash cURL
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/conv_xxx' \
          --header 'Authorization: Bearer $DASHSCOPE_API_KEY'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 响应参数 <span id="h3-retrieve-response" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>created\_at</strong>`integer`

        会话创建的 Unix 时间戳（毫秒）。

        <strong>id</strong>`string`

        会话唯一标识符。

        <strong>metadata</strong>`object`

        会话元数据，以键值对形式存储的附加信息。最多16对，key最大长度64字符，value最大长度512字符。

        <strong>object</strong>`string`

        对象类型，固定为 `conversation`。
      </td>

      <td>
        ```json
        {
            "created_at": 1771316949128,
            "id": "conv_xxx",
            "metadata": {
                "topic": "demo"
            },
            "object": "conversation"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## Update conversation <span id="h2-update-conversation" />

更新会话的元数据信息。

<strong>华北2（北京）：POST</strong> `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}`

<strong>新加坡：POST</strong> `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>conversation\_id</strong>`string`<strong>(必选, Path)</strong>

        会话ID。

        <strong>metadata</strong>`object`<strong>(必选)</strong>

        会话元数据，会完全覆盖原有元数据。最多16对键值对，key最大长度64字符，value最大长度512字符。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          updated = client.conversations.update(
              "conv_xxx",
              metadata={"topic": "update"}
          )
          print(updated)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const client = new OpenAI({
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          });

          const updated = await client.conversations.update(
              "conv_xxx",
              { metadata: { topic: "update" } }
          );
          console.log(updated);
          ```

          ```bash cURL
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/conv_xxx' \
          --header 'Content-Type: application/json' \
          --header 'Authorization: Bearer $DASHSCOPE_API_KEY' \
          --data '{
              "metadata": {
                  "topic": "update"
              }
          }'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 响应参数 <span id="h3-update-response" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>created\_at</strong>`integer`

        会话创建的 Unix 时间戳（毫秒）。

        <strong>id</strong>`string`

        会话唯一标识符。

        <strong>metadata</strong>`object`

        会话元数据，以键值对形式存储的附加信息。最多16对，key最大长度64字符，value最大长度512字符。

        <strong>object</strong>`string`

        对象类型，固定为 `conversation`。
      </td>

      <td>
        ```json
        {
            "created_at": 1771318152759,
            "id": "conv_xxx",
            "metadata": {
                "topic": "update"
            },
            "object": "conversation"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## Delete conversation <span id="h2-delete-conversation" />

删除指定会话。会话中的消息项不会被删除。

<strong>华北2（北京）：DELETE</strong> `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}`

<strong>新加坡：DELETE</strong> `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>conversation\_id</strong>`string`<strong>(必选, Path)</strong>

        会话ID。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          result = client.conversations.delete("conv_xxx")
          print(result)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const client = new OpenAI({
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          });

          const result = await client.conversations.del(
              "conv_xxx"
          );
          console.log(result);
          ```

          ```bash cURL
          curl --location --request DELETE 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/conv_xxx' \
          --header 'Authorization: Bearer $DASHSCOPE_API_KEY'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 响应参数 <span id="h3-delete-response" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>deleted</strong>`boolean`

        是否删除成功。

        <strong>id</strong>`string`

        被删除的会话ID。

        <strong>object</strong>`string`

        对象类型，固定为 `conversation.deleted`。
      </td>

      <td>
        ```json
        {
            "deleted": true,
            "id": "conv_xxx",
            "object": "conversation.deleted"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## Create Items <span id="h2-create-items" />

向指定会话添加消息项。

<strong>华北2（北京）：POST</strong> `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}/items`

<strong>新加坡：POST</strong> `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}/items`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>conversation\_id</strong>`string`<strong>(必选, Path)</strong>

        会话ID。

        <strong>items</strong>`array`<strong>(必选)</strong>

        消息项列表，每次最多添加20条。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`<strong>(必选)</strong>

          消息类型，仅支持 `message`。

          <strong>role</strong>`string`<strong>(必选)</strong>

          消息的角色。`system` 、`developer`角色的指令优先级高于 `user` 角色，`assistant` 角色表示模型在之前交互中生成的消息。取值：`user`、`assistant`、`system`、`developer`。

          <strong>content</strong>`string or array`<strong>(必选)</strong>

          消息内容。支持纯文本字符串或结构化内容列表（如 ResponseInputText 对象数组），列表格式可包含文本等多种内容类型。
        </Accordion>
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          items = client.conversations.items.create(
              "conv_xxx",
              items=[
                  {
                      "type": "message",
                      "role": "user",
                      "content": [{"type": "input_text", "text": "李红的专业是师范教育"}],
                  }
              ],
          )
          print(items.data)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const client = new OpenAI({
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          });

          const items = await client.conversations.items.create(
              "conv_xxx",
              {
                  items: [
                      {
                          type: "message",
                          role: "user",
                          content: [{ type: "input_text", text: "李红的专业是师范教育" }]
                      }
                  ]
              }
          );
          console.log(items.data);
          ```

          ```bash cURL
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/conv_xxx/items' \
          --header 'Content-Type: application/json' \
          --header 'Authorization: Bearer $DASHSCOPE_API_KEY' \
          --data '{
              "items": [
                  {
                      "type": "message",
                      "role": "user",
                      "content": [{
                          "type": "input_text",
                          "text": "李红的专业是师范教育"
                      }]
                  }
              ]
          }'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 响应参数 <span id="h3-create-items-response" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>data</strong>`array[object]`

        创建的消息项列表。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          消息项唯一标识符。

          <strong>content</strong>`string or array`

          消息内容。纯文本字符串或结构化内容列表（如 ResponseInputText 对象数组）。

          <strong>role</strong>`string`

          消息的角色类型，取值：`user`、`assistant`、`system`、`developer`。

          <strong>status</strong>`string`

          消息的处理状态，取值：`in_progress`（处理中）、`completed`（已完成）、`incomplete`（未完成）。

          <strong>type</strong>`string`

          消息项的类型，固定为 `message`。
        </Accordion>

        <strong>first\_id</strong>`string`

        列表中第一条消息项的ID。

        <strong>has\_more</strong>`boolean`

        是否还有更多数据。

        <strong>last\_id</strong>`string`

        列表中最后一条消息项的ID。
      </td>

      <td>
        ```json
        {
            "data": [
                {
                    "content": [
                        {
                            "text": "李红的专业是师范教育",
                            "type": "input_text"
                        }
                    ],
                    "id": "msg_xxx",
                    "role": "user",
                    "status": "completed",
                    "type": "message"
                }
            ],
            "first_id": "msg_xxx",
            "has_more": false,
            "last_id": "msg_xxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## List Items <span id="h2-list-items" />

列出会话中的所有消息项。

<strong>华北2（北京）：GET</strong> `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}/items`

<strong>新加坡：GET</strong> `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}/items`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>conversation\_id</strong>`string`<strong>(必选, Path)</strong>

        会话ID。

        <strong>after</strong>`string`（可选）

        分页游标，返回指定消息ID之后的消息项。

        <strong>order</strong>`string`（可选）

        排序方式，`asc`（升序）或 `desc`（降序），默认 `desc`。

        <strong>limit</strong>`integer`（可选）

        返回数量，范围1-100，默认20。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          items = client.conversations.items.list("conv_xxx")
          print(items.data)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const client = new OpenAI({
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          });

          const items = await client.conversations.items.list(
              "conv_xxx"
          );
          console.log(items.data);
          ```

          ```bash cURL
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/conv_xxx/items?limit=10&order=asc' \
          --header 'Authorization: Bearer $DASHSCOPE_API_KEY'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 响应参数 <span id="h3-list-items-response" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>data</strong>`array[object]`

        消息项列表。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          消息项唯一标识符。

          <strong>content</strong>`string or array`

          消息内容。纯文本字符串或结构化内容列表（如 ResponseInputText 对象数组）。

          <strong>role</strong>`string`

          消息的角色类型，取值：`user`、`assistant`、`system`、`developer`。

          <strong>status</strong>`string`

          消息的处理状态，取值：`in_progress`（处理中）、`completed`（已完成）、`incomplete`（未完成）。

          <strong>type</strong>`string`

          消息项的类型，固定为 `message`。
        </Accordion>

        <strong>first\_id</strong>`string`

        列表中第一条消息项的ID。

        <strong>has\_more</strong>`boolean`

        是否还有更多数据。

        <strong>last\_id</strong>`string`

        列表中最后一条消息项的ID。

        <strong>object</strong>`string`

        对象类型，固定为 `list`。
      </td>

      <td>
        ```json expandable
        {
            "data": [
                {
                    "content": [
                        {
                            "text": "李红，一位温婉而坚韧的江南女子，出生在浙江省，今年20岁",
                            "type": "input_text"
                        }
                    ],
                    "id": "msg_7639f8f6-484b-454a-8125-96a3f40eb9e8",
                    "role": "user",
                    "status": "completed",
                    "type": "message"
                },
                {
                    "content": [
                        {
                            "text": "李红的闺蜜是小芳",
                            "type": "input_text"
                        }
                    ],
                    "id": "msg_288594f6-6ef1-4519-94d4-a545ca311828",
                    "role": "user",
                    "status": "completed",
                    "type": "message"
                }
            ],
            "first_id": "msg_7639f8f6-484b-454a-8125-96a3f40eb9e8",
            "has_more": false,
            "last_id": "msg_288594f6-6ef1-4519-94d4-a545ca311828",
            "object": "list"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## Retrieve Item <span id="h2-retrieve-item" />

获取指定消息项的详情。

<strong>华北2（北京）：GET</strong> `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}/items/{item_id}`

<strong>新加坡：GET</strong> `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}/items/{item_id}`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>conversation\_id</strong>`string`<strong>(必选, Path)</strong>

        会话ID。

        <strong>item\_id</strong>`string`<strong>(必选, Path)</strong>

        消息项ID。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          item = client.conversations.items.retrieve(
              "msg_xxx",
              conversation_id="conv_xxx"
          )
          print(item)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const client = new OpenAI({
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          });

          const item = await client.conversations.items.retrieve(
              "msg_xxx",
              { conversation_id: "conv_xxx" }
          );
          console.log(item);
          ```

          ```bash cURL
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/conv_xxx/items/msg_xxx' \
          --header 'Authorization: Bearer $DASHSCOPE_API_KEY'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 响应参数 <span id="h3-retrieve-item-response" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>content</strong>`array[object]`

        消息内容列表，包含一个或多个内容对象。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          内容类型，如 `input_text`（用户输入文本）或 `output_text`（模型输出文本）。

          <strong>text</strong>`string`

          文本内容。
        </Accordion>

        <strong>id</strong>`string`

        消息项唯一标识符。

        <strong>role</strong>`string`

        消息的角色类型，取值：`user`、`assistant`、`system`、`developer`。

        <strong>status</strong>`string`

        消息的处理状态，取值：`in_progress`（处理中）、`completed`（已完成）、`incomplete`（未完成）。

        <strong>type</strong>`string`

        消息项的类型，固定为 `message`。
      </td>

      <td>
        ```json
        {
            "content": [
                {
                    "text": "李红的专业是师范教育",
                    "type": "input_text"
                }
            ],
            "id": "msg_xxx",
            "role": "user",
            "status": "completed",
            "type": "message"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## Delete Item <span id="h2-delete-item" />

删除指定的消息项。

<strong>华北2（北京）：DELETE</strong> `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}/items/{item_id}`

<strong>新加坡：DELETE</strong> `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/conversations/{conversation_id}/items/{item_id}`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>conversation\_id</strong>`string`<strong>(必选, Path)</strong>

        会话ID。

        <strong>item\_id</strong>`string`<strong>(必选, Path)</strong>

        消息项ID。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          result = client.conversations.items.delete(
              "msg_xxx",
              conversation_id="conv_xxx"
          )
          print(result)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const client = new OpenAI({
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          });

          const result = await client.conversations.items.del(
              "msg_xxx",
              { conversation_id: "conv_xxx" }
          );
          console.log(result);
          ```

          ```bash cURL
          curl --location --request DELETE 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/conversations/conv_xxx/items/msg_xxx' \
          --header 'Authorization: Bearer $DASHSCOPE_API_KEY'
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 响应参数 <span id="h3-delete-item-response" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>deleted</strong>`boolean`

        是否删除成功。

        <strong>id</strong>`string`

        被删除的消息项ID。

        <strong>object</strong>`string`

        对象类型，固定为 `conversation.item.deleted`。
      </td>

      <td>
        ```json
        {
            "deleted": true,
            "id": "msg_xxx",
            "object": "conversation.item.deleted"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## Response API 使用 conversation 示例 <span id="8c6069bd19h5p" />

通过 Responses API 的 `conversation` 参数，可以实现多轮对话的上下文保持。

> 请勿同时传入`previous_response_id`和`conversation`，否则会报错：`[400] INVALID_REQUEST: Mutually exclusive parameters: Ensure you are only providing one of: previous_response_id or conversation.`

<CodeGroup dropdown>
  ```python Python expandable
  import os
  from openai import OpenAI

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )

  conversation = client.conversations.create(
      items=[
          {
              "type": "message",
              "role": "system",
              "content": "李红，一位温婉而坚韧的江南女子，出生在浙江省杭州市，她今年20岁，她的兴趣爱好是琴棋书画。",
          }
      ]
  )

  response1 = client.responses.create(
      conversation=conversation.id, model="qwen3.8-max", input="李红今年多大了"
  )
  print(f"第一轮响应: {response1.output_text}")

  response2 = client.responses.create(
      conversation=conversation.id, model="qwen3.8-max", input="她的兴趣爱好是什么？"
  )
  print(f"第二轮响应: {response2.output_text}")
  ```

  ```javascript Node.js expandable
  import OpenAI from "openai";

  const client = new OpenAI({
    apiKey: process.env.DASHSCOPE_API_KEY,
    baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  });

  const conversation = await client.conversations.create({
    items: [
      {
        type: "message",
        role: "system",
        content: "李红，一位温婉而坚韧的江南女子，出生在浙江省杭州市，她今年20岁，她的兴趣爱好是琴棋书画。"
      }
    ]
  });

  const response1 = await client.responses.create({
    conversation: conversation.id,
    model: "qwen3.8-max",
    input: "李红今年多大了"
  });
  console.log("第一轮响应:", response1.output_text);

  const response2 = await client.responses.create({
    conversation: conversation.id,
    model: "qwen3.8-max",
    input: "她的兴趣爱好是什么？"
  });
  console.log("第二轮响应:", response2.output_text);
  ```
</CodeGroup>

## 使用限制 <span id="h2-limits" />

- 创建会话或添加消息项时，`items` 最多包含20条。
- `metadata` 最多16对键值对，key最大长度64字符，value最大长度512字符。
- 会话信息保留最近7天内的最新100条，超出时间或数量限制的内容将自动清理。
