> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Threads（下线中）

> 本文详细阐述了Assistant API中Thread类的各项功能，涵盖了线程的创建、检索、修改以及删除操作。

<meta property="og:title" content="Threads（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

> <strong>功能介绍</strong>：如果您想了解 Assistant API 的功能和基本用法，请参考[Assistant API功能概览](/zh/model-studio/assistant-api)。

> <strong>有效期限</strong>：所有 Thread 实例均保存在阿里云百炼服务器上，目前没有失效日期，您可通过 thread.id 检索上下文信息。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30.077519%" }} /><col style={{ width: "69.922481%" }} /></colgroup><thead><tr><th><p><strong>函数名</strong></p></th><th><p><strong>类型</strong></p></th></tr></thead><tbody><tr><td><p>create</p></td><td><p>创建 Thread 类</p></td></tr><tr><td><p>retrieve</p></td><td><p>检索 Thread 类</p></td></tr><tr><td><p>update</p></td><td><p>修改 Thread 类</p></td></tr><tr><td><p>delete</p></td><td><p>删除 Thread 类</p></td></tr></tbody></table>

## 创建线程 <span id="NTx71" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "messages": [
            {
                "role": "user",
                "content": "你好"
            }
        ]
    }'
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>messages</p></td><td><p>thread 所传入的message</p></td><td><p>Message 类</p></td><td><p>否</p></td></tr><tr><td><p>metadata</p></td><td><p>thread使用的名字</p></td><td><p>str</p></td><td><p>否</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json
    {
        "id": "thread_e99a9fe7-0433-426f-98ad-a5139c36579c",
        "object": "thread",
        "created_at": 1711448377850,
        "metadata": {},
        "request_id": "dd9489ec-dbdb-95d4-9ff8-cfe29b61db27"
    }
    ```

    <strong>输出参数</strong>

    输出thread类，并包含除了用户输入以外参数的其他额外字段：

    - id ：thread id
    - request\_id ：请求id
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      import json
      import os
      from dashscope import Threads

      thread = Threads.create(
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          messages=[{"role": "user", "content": "How does AI work? Explain it in simple terms."}]
      )
      print(json.dumps(thread, default=lambda o: o.__dict__, sort_keys=True, indent=4))
      ```

      ```java
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.AssistantThread;
      import com.alibaba.dashscope.threads.ThreadParam;
      import com.alibaba.dashscope.threads.Threads;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Threads threads = new Threads();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              AssistantThread assistantThread = threads.create(ThreadParam.builder()
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .build());
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>messages</p></td><td><p>List\[Dict]</p></td><td><p>None</p></td><td><p>Thread初始messages</p></td></tr><tr><td><p>metadata</p></td><td><p>Dict</p></td><td><p>None</p></td><td><p>与该Thread关联的key/value信息</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    结果为Thread对象，json化内容为：

    ```json
    {
        "created_at": 1711338305031,
        "id": "thread_97934051-2c15-44bf-97de-310039d873f9",
        "metadata": {},
        "object": "thread",
        "request_id": "982d4b9a-b982-9d53-9c79-a75b32f7168a",
        "status_code": 200
    }
    ```

    <strong>输出参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>字段类型</strong></p></th><th><p><strong>字段描述</strong></p></th></tr></thead><tbody><tr><td><p>status\_code</p></td><td><p>int</p></td><td><p>为调用http status code，200表示调用成功，其他表示调用出错</p></td></tr><tr><td><p>id</p></td><td><p>str</p></td><td><p>thread id，为uuid字符串</p></td></tr><tr><td><p>metadata</p></td><td><p>Dict</p></td><td><p>和这个Thread关联的key/value信息</p></td></tr><tr><td><p>created\_at</p></td><td><p>timestamp</p></td><td><p>thread 创建时间</p></td></tr><tr><td><p>code</p></td><td><p>str</p></td><td><p>表示请求失败，显示错误码，成功忽略。</p><p>python only</p></td></tr><tr><td><p>message</p></td><td><p>str</p></td><td><p>失败，表示失败的详细信息，成功则忽略。</p><p>python only</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 检索线程 <span id="TobTm" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_c7ebb0ca-2e4f-43e5-b223-6e1f8c6fccc7' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>待检索的thread\_id</p></td><td><p>str</p></td><td><p>是</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json
    {
        "id": "thread_c7ebb0ca-2e4f-43e5-b223-6e1f8c6fccc7",
        "object": "thread",
        "created_at": 1711507920700,
        "metadata": {},
        "request_id": "4d4e73ad-15fb-96ac-9262-0643a0fdb5ca"
    }
    ```

    <strong>输出参数</strong>

    输出检索到的thread类，并包含除了用户输入以外参数的其他额外字段：

    - id ：thread\_id
    - request\_id ：请求id
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Threads
      import os

      thread = Threads.retrieve(
          'thread_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY")
      )
      ```

      ```java
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.AssistantThread;
      import com.alibaba.dashscope.threads.Threads;

      public class Main {
          public static void main(String[] args) {
              Threads threads = new Threads();
              // 直接传入 thread_id 和 apiKey
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为："sk-xxx"
              AssistantThread assistantThread = threads.retrieve(
                  "thread_id",
                  System.getenv("DASHSCOPE_API_KEY")
              );
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p><em>str</em></p></td><td><p>-</p></td><td><p>指定要查询的thread id</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    参考create结果
  </Tab>
</Tabs>

## 修改线程 <span id="a5jsi" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_c7ebb0ca-2e4f-43e5-b223-6e1f8c6fccc7' \
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

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>待修改的thread\_id</p></td><td><p>str</p></td><td><p>是</p></td></tr><tr><td><p>metadata</p></td><td><p>thread使用的名字</p></td><td><p>dict</p></td><td><p>否</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json
    {
        "id": "thread_c7ebb0ca-2e4f-43e5-b223-6e1f8c6fccc7",
        "object": "thread",
        "created_at": 1711507920700,
        "metadata": {
            "modified": "true",
            "user": "abc123"
        },
        "request_id": "a9ad63fa-b884-94be-9ec6-5000882de3c4"
    }
    ```

    <strong>输出参数</strong>

    输出检索到的thread类，并包含除了用户输入以外参数的其他额外字段：

    - id ：thread\_id
    - request\_id ：请求id
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Threads
      import os

      thread = Threads.update(
          'thread_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
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
      import com.alibaba.dashscope.threads.Threads;

      public class AssistantGeneral {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Threads threads = new Threads();
              UpdateMetadataParam updateMetadataParam = UpdateMetadataParam.builder()
                      .metadata(Collections.singletonMap("key", "value"))
                      // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .build();
              threads.update("thread_id", updateMetadataParam);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>指定需要update的thread id</p></td></tr><tr><td><p>metadata</p></td><td><p>Dict</p></td><td><p>None</p></td><td><p>Thread关联信息</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    同create，参考create
  </Tab>
</Tabs>

## 删除线程 <span id="mnZ7Z" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location --request DELETE 'https://dashscope.aliyuncs.com/api/v1/threads/thread_c7ebb0ca-2e4f-43e5-b223-6e1f8c6fccc7' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>待检索的thread id</p></td><td><p>str</p></td><td><p>是</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json
    {
        "id": "thread_c7ebb0ca-2e4f-43e5-b223-6e1f8c6fccc7",
        "object": "thread.deleted",
        "deleted": true,
        "request_id": "b4edb7b8-5855-9787-b5c3-0374ee2b3b2c"
    }
    ```

    <strong>输出参数</strong>

    输出删除thread后的状态
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Threads
      import os

      thread = Threads.delete(
          'thread_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY")
      )
      ```

      ```java
      import com.alibaba.dashscope.common.DeletionStatus;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.Threads;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Threads threads = new Threads();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              DeletionStatus assistantThread = threads.delete("thread_id", apiKey);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p><em>str</em></p></td><td><p>-</p></td><td><p>指定要删除的thread id</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>字段类型</strong></p></th><th><p><strong>字段描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>str</p></td><td><p>删除的对象的id</p></td></tr><tr><td><p>deleted</p></td><td><p>bool</p></td><td><p>是否删除</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 错误码 <span id="97e145aeabbwf" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
