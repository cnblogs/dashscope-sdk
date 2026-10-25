> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Messages（下线中）

> 本文详细阐述了Assistant API中Message类的各项功能，涵盖了消息的创建、列举、检索、修改等操作。

<meta property="og:title" content="Messages（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

<strong>相关指南：</strong>关于Assistant API的快速使用方法，请参考[快速入门](/zh/model-studio/quick-start-of-assistant-api)。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30.077519%" }} /><col style={{ width: "69.922481%" }} /></colgroup><thead><tr><th><p><strong>函数名</strong></p></th><th><p><strong>类型</strong></p></th></tr></thead><tbody><tr><td><p>create</p></td><td><p>创建message 类</p></td></tr><tr><td><p>retrieve</p></td><td><p>检索message类</p></td></tr><tr><td><p>modify</p></td><td><p>修改message类</p></td></tr><tr><td><p>list</p></td><td><p>列出message类</p></td></tr></tbody></table>

## 创建消息 <span id="NTx71" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_e99a9fe7-0433-426f-98ad-a5139c36579c/messages' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "role": "user",
        "content": "你是谁",
        "metadata": {}
    }'
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread id</p></td><td><p>message 所传入的thread id</p></td><td><p>str</p></td><td><p>是</p></td></tr><tr><td><p>content</p></td><td><p>message的具体信息</p></td><td><p>str</p></td><td><p>是</p></td></tr><tr><td><p>role</p></td><td><p>传入信息的角色。目前只支持role = “user"</p></td><td><p>str</p></td><td><p>否</p></td></tr><tr><td><p>metadata</p></td><td><p>其他相关信息</p></td><td><p>str</p></td><td><p>否</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json expandable
    {
        "id": "message_f1933671-19e1-4162-ad25-7326165123e1",
        "object": "thread.message",
        "created_at": 1711508433283,
        "thread_id": "thread_e99a9fe7-0433-426f-98ad-a5139c36579c",
        "incomplete_details": {},
        "completed_at": null,
        "incomplete_at": null,
        "assistant_id": "",
        "run_id": "",
        "file_ids": [],
        "role": "user",
        "content": [
            {
                "type": "text",
                "text": {
                    "value": "你是谁",
                    "annotations": []
                }
            }
        ],
        "metadata": {},
        "name": "",
        "plugin_call": {},
        "tool_calls": [],
        "status": "",
        "request_id": "b3ad40b9-f052-9665-a064-dab11c34625f"
    }
    ```

    <strong>输出参数</strong>

    输出message类，并包含除了用户输入以外参数的其他额外字段：

    - id：message id
    - request\_id：请求id
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Messages
      import os

      msg = Messages.create(
          'the_thread_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          content='The message content.',
          role='user',
          metadata={'key': 'value'}
      )
      ```

      ```java
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.messages.Messages;
      import com.alibaba.dashscope.threads.messages.TextMessageParam;
      import com.alibaba.dashscope.threads.messages.ThreadMessage;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              // create a message to thread
              Messages messages = new Messages();
              TextMessageParam param = TextMessageParam.builder()
                      // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .role("user")
                      .content("如何做出美味的牛肉炖土豆？")
                      .build();
              ThreadMessage message = messages.create("threadId", param);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>Thread id</p></td></tr><tr><td><p>content</p></td><td><p>str</p></td><td><p>-</p></td><td><p>消息内容</p></td></tr><tr><td><p>role</p></td><td><p>str</p></td><td><p>'user'</p></td><td><p>Message的role，默认user</p></td></tr><tr><td><p>metadata</p></td><td><p>Dict</p></td><td><p>None</p></td><td><p>与该Message关联的key/value信息</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    结果为Message对象，json化内容为：

    ```json expandable
    {
        "id": "message_05494921-a646-484e-85fc-76329acba842",
        "object": "thread.message",
        "created_at": 1711345341301,
        "thread_id": "thread_f1e7737e-b045-479f-99d1-510db49d535b",
        "incomplete_details": {},
        "completed_at": null,
        "incomplete_at": null,
        "assistant_id": "",
        "run_id": "",
        "file_ids": [],
        "role": "user",
        "content": [
            {
                "type": "text",
                "text": {
                    "value": "sdhafjdasf",
                    "annotations": []
                }
            }
        ],
        "metadata": {
            "key": "value"
        },
        "name": "",
        "plugin_call": {},
        "tool_calls": [],
        "status": "",
        "status_code": 200,
        "request_id": "631de0b3-7e50-9c9e-8444-0924d1b7e7a5"
    }
    ```

    <strong>输出参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>字段类型</strong></p></th><th><p><strong>字段描述</strong></p></th></tr></thead><tbody><tr><td><p>status\_code</p></td><td><p>int</p></td><td><p>为调用http status code，200表示调用成功，其他表示调用出错</p></td></tr><tr><td><p>id</p></td><td><p>str</p></td><td><p>Message id，为uuid字符串</p></td></tr><tr><td><p>content</p></td><td><p>List\[dict]</p></td><td><p>消息内容</p></td></tr><tr><td><p>content.type</p></td><td><p>str</p></td><td><p>content内容类型，如text</p></td></tr><tr><td><p>content.text</p></td><td><p>dict</p></td><td><p>content内容</p></td></tr><tr><td><p>content.text.value</p></td><td><p>str</p></td><td><p>content text value</p></td></tr><tr><td><p>metadata</p></td><td><p>Dict</p></td><td><p>和这个Message关联的key/value信息</p></td></tr><tr><td><p>tool\_calls</p></td><td><p>Dict</p></td><td><p>调用tool信息</p></td></tr><tr><td><p>plugin\_call</p></td><td><p>Dict</p></td><td><p>调用plugin 信息</p></td></tr><tr><td><p>created\_at</p></td><td><p>timestamp</p></td><td><p>assistant创建时间</p></td></tr><tr><td><p>gmt\_created</p></td><td><p>datetime</p></td><td><p>2024-03-22 17:12:31</p></td></tr><tr><td><p>gmt\_modified</p></td><td><p>datetime</p></td><td><p>2024-03-22 17:12:31</p></td></tr><tr><td><p>code</p></td><td><p>str</p></td><td><p>表示请求失败，表示错误码，成功忽略。</p><p>python only</p></td></tr><tr><td><p>message</p></td><td><p>str</p></td><td><p>失败，表示失败详细信息，成功忽略。</p><p>python only</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 消息列表 <span id="Bj3EB" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_e99a9fe7-0433-426f-98ad-a5139c36579c/messages?limit=2&order=desc' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.976879%" }} /><col style={{ width: "27.976879%" }} /><col style={{ width: "24.508671%" }} /><col style={{ width: "19.537572%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread id</p></td><td><p>message 所传入的thread id</p></td><td><p>str</p></td><td><p>是</p></td></tr><tr><td><p>limit</p></td><td><p>创建message的数量</p></td><td><p>int</p></td><td><p>否</p></td></tr><tr><td><p>order</p></td><td><p>按照创建时间升序或降序</p></td><td><p>str</p></td><td><p>否（默认为”desc“）</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json expandable
    {
        "object": "list",
        "data": [
            {
                "id": "message_f1933671-19e1-4162-ad25-7326165123e1",
                "object": "thread.message",
                "created_at": 1711508433283,
                "thread_id": "thread_e99a9fe7-0433-426f-98ad-a5139c36579c",
                "assistant_id": "",
                "run_id": "",
                "role": "user",
                "content": [
                    {
                        "type": "text",
                        "text": {
                            "value": "你是谁",
                            "annotations": []
                        }
                    }
                ],
                "metadata": {},
                "name": "",
                "plugin_call": {},
                "tool_calls": [],
                "status": ""
            }
        ],
        "first_id": "message_f1933671-19e1-4162-ad25-7326165123e1",
        "last_id": "message_f1933671-19e1-4162-ad25-7326165123e1",
        "has_more": false,
        "request_id": "78f7d607-4a9a-90c6-8040-d3f81c84d60a"
    }
    ```

    <strong>输出参数</strong>

    输出List message类，并包含除了用户输入以外参数的其他额外字段：

    - 多个message 组成的列表
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Messages
      import os

      messages = Messages.list(
          'thread_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          limit=1,
          order='desc'
      )
      ```

      ```java
      import com.alibaba.dashscope.common.GeneralListParam;
      import com.alibaba.dashscope.common.ListResult;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.messages.Messages;
      import com.alibaba.dashscope.threads.messages.ThreadMessage;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Messages messages = new Messages();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              GeneralListParam listThreadMessages = GeneralListParam.builder()
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .build();
              ListResult<ThreadMessage> message = messages.list("threadId", listThreadMessages);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>指定要查询的thread id。</p></td></tr><tr><td><p>limit</p></td><td><p>int</p></td><td><p>None</p></td><td><p>要检索的消息数。</p></td></tr><tr><td><p>order</p></td><td><p>str</p></td><td><p>None</p></td><td><p>按 created\_at 排序的顺序。</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>字段类型</strong></p></th><th><p><strong>字段描述</strong></p></th></tr></thead><tbody><tr><td><p>has\_more</p></td><td><p>bool</p></td><td><p>是否还有更多消息可以检索。</p></td></tr><tr><td><p>last\_id</p></td><td><p>str</p></td><td><p>返回的消息列表中最后一个消息的 ID。</p></td></tr><tr><td><p>first\_id</p></td><td><p>str</p></td><td><p>返回的消息列表中第一个消息的 ID。</p></td></tr><tr><td><p>data</p></td><td><p>list\[Message]</p></td><td><p>Message对象列表。</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 检索消息 <span id="TobTm" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_e99a9fe7-0433-426f-98ad-a5139c36579c/messages/message_ea26d29d-4509-490e-98e9-9f6238bd821b' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread id</p></td><td><p>待检索的message 所传入的thread id</p></td><td><p>str</p></td><td><p>是</p></td></tr><tr><td><p>message\_id</p></td><td><p>待检索的message\_id</p></td><td><p>str</p></td><td><p>是</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json
    {
        "id": "message_ea26d29d-4509-490e-98e9-9f6238bd821b",
        "object": "thread.message",
        "created_at": 1711508622598,
        "thread_id": "thread_e99a9fe7-0433-426f-98ad-a5139c36579c",
        "assistant_id": "",
        "run_id": "",
        "role": "user",
        "content": [
            {
                "type": "text",
                "text": {
                    "value": "你好",
                    "annotations": []
                }
            }
        ],
        "metadata": {},
        "name": "",
        "plugin_call": {},
        "tool_calls": [],
        "status": "",
        "request_id": "4d5ce962-91c3-9edb-87f7-00bbf985135e"
    }
    ```

    <strong>输出参数</strong>

    输出检索到的message类，并包含除了用户输入以外参数的其他额外字段：

    - id：message\_id
    - request\_id：请求id
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Messages
      import os

      message = Messages.retrieve(
          'message_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          thread_id='thread_id'
      )
      ```

      ```java
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.messages.Messages;
      import com.alibaba.dashscope.threads.messages.ThreadMessage;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Messages messages = new Messages();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              ThreadMessage message = messages.retrieve("threadId", "messageId", apiKey);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>message\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>指定要查询的Message id</p></td></tr><tr><td><p>thread\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>指定要查询的Message所属Thread id</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    参考 create 结果
  </Tab>
</Tabs>

## 修改消息 <span id="a5jsi" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_e99a9fe7-0433-426f-98ad-a5139c36579c/messages/message_ea26d29d-4509-490e-98e9-9f6238bd821b' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "metadata": {
            "modified": "true",
            "user": "abc123"
        }
    }'
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>待修改的thread\_id</p></td><td><p>str</p></td><td><p>是</p></td></tr><tr><td><p>message\_id</p></td><td><p>待修改的message\_id</p></td><td><p>str</p></td><td><p>是</p></td></tr><tr><td><p><strong>metadata</strong></p></td><td><p>元数据</p></td><td><p>dict</p></td><td /></tr></tbody></table>

    <strong>返回结果</strong>

    ```json expandable
    {
        "id": "message_ea26d29d-4509-490e-98e9-9f6238bd821b",
        "object": "thread.message",
        "created_at": 1711508622598,
        "thread_id": "thread_e99a9fe7-0433-426f-98ad-a5139c36579c",
        "incomplete_details": {},
        "completed_at": null,
        "incomplete_at": null,
        "assistant_id": "",
        "run_id": "",
        "file_ids": [],
        "role": "user",
        "content": [
            {
                "type": "text",
                "text": {
                    "value": "你好",
                    "annotations": []
                }
            }
        ],
        "metadata": {
            "modified": "true",
            "user": "abc123"
        },
        "name": "",
        "plugin_call": {},
        "tool_calls": [],
        "status": "",
        "request_id": "7877b011-cb94-9df1-9add-dc42b7d611f6"
    }
    ```

    <strong>输出参数</strong>

    输出修改后的message类，并包含除了用户输入以外参数的其他额外字段：

    - id ：message\_id
    - request\_id ：请求id
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Messages

      import os

      thread = Messages.update(
          'message_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          thread_id='the_message_thread_id',
          metadata={'key': 'value'}
      )
      ```

      ```java
      import java.util.Collections;
      import com.alibaba.dashscope.common.UpdateMetadataParam;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.messages.Messages;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Messages messages = new Messages();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              UpdateMetadataParam updateMetadataParam = UpdateMetadataParam.builder()
                      .metadata(Collections.singletonMap("key", "value"))
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .build();
              messages.update("thread_id", "message_Id", updateMetadataParam);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>message\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>指定要更新的Message id</p></td></tr><tr><td><p>thread\_id</p></td><td><p><em>str</em></p></td><td><p>-</p></td><td><p>指定要更新的Message所属Thread id</p></td></tr><tr><td><p>metadata</p></td><td><p>Dict</p></td><td><p>None</p></td><td><p>Thread关联信息</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    参考 create 结果
  </Tab>
</Tabs>

## 错误码 <span id="aaed6bfcc7pbj" />

如果 Assistant API 调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
