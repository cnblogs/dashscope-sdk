> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Run Steps（下线中）

> Run Steps 描述了智能体运行过程中所采取的步骤，包括模型和工具的调用。

<meta property="og:title" content="Run Steps（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

## 列出运行步骤 <span id="390a873ff7ndz" />

返回一个运行任务的所有步骤列表。

### 请求示例 <span id="446ee85f3bfx3" />

<Tabs>
  <Tab title="HTTP">
    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_cc2a3e9d-436b-482b-91c5-377e0f376506/runs/run_3de634fa-75d4-4370-adcf-92ba2a60c396/steps?limit=20&order=asc' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```
  </Tab>

  <Tab title="SDK">
    <CodeGroup dropdown>
      ```python
      from dashscope import Steps
      import os

      steps = Steps.list(
          'run_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          thread_id='thread_id',
          limit=20
      )
      ```

      ```java
      import com.alibaba.dashscope.common.GeneralListParam;
      import com.alibaba.dashscope.common.ListResult;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.runs.RunStep;
      import com.alibaba.dashscope.threads.runs.Runs;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Runs runs = new Runs();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              GeneralListParam listSteps = GeneralListParam.builder()
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .build();
              ListResult<RunStep> run = runs.listSteps("threadId", "runId", listSteps);
          }
      }
      ```
    </CodeGroup>
  </Tab>
</Tabs>

### 请求体 <span id="e5471f2b7ddrs" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.72%" }} /><col style={{ width: "11.71%" }} /><col style={{ width: "5.86%" }} /><col style={{ width: "11.71%" }} /><col style={{ width: "53%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>参数描述</strong></p></th></tr></thead><tbody><tr><td><p>run\_id</p></td><td><p>string</p></td><td><p>是</p></td><td /><td><p>需要列出步骤的运行 (Run) 的 ID。</p></td></tr><tr><td><p>thread\_id</p></td><td><p>string</p></td><td><p>是</p></td><td /><td><p>线程 (Thread) 的 ID。</p></td></tr><tr><td><p>limit</p></td><td><p>integer</p></td><td><p>否</p></td><td><p>None</p></td><td><p>要检索的运行步骤数量。默认为None，表示使用服务器端的默认值。</p></td></tr><tr><td><p>order</p></td><td><p>string</p></td><td><p>否</p></td><td><p>None</p></td><td><p>根据created\_at字段进行排序的顺序。可以设置为asc（升序）或desc（降序）。默认为None，表示使用服务器端的默认排序方式。</p></td></tr><tr><td><p>after</p></td><td><p>string</p></td><td><p>否</p></td><td><p>None</p></td><td><p>用于分页，指定一个运行步骤 ID，返回在此 ID 之后的运行步骤列表。与before参数互斥。默认为None。</p></td></tr><tr><td><p>before</p></td><td><p>string</p></td><td><p>否</p></td><td><p>None</p></td><td><p>用于分页，指定一个运行步骤 ID，返回在此 ID 之前的运行步骤列表。与after参数互斥。默认为None。</p></td></tr><tr><td><p>workspace</p></td><td><p><em>string</em></p></td><td><p>是</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p><em>string</em></p></td><td><p>是</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

### 响应示例 <span id="7e6b993517arj" />

```json expandable
{
    "object": "list",
    "data": [
        {
            "id": "[REDACTED]",
            "object": "thread.run.step",
            "created_at": 1711517599333,
            "assistant_id": "[REDACTED]",
            "thread_id": "[REDACTED]",
            "run_id": "[REDACTED]",
            "type": "message_creation",
            "status": "completed",
            "step_details": {
                "type": "message_creation",
                "message_creation": {
                    "message_id": "[REDACTED]"
                }
            },
            "last_error": {
                "code": "",
                "message": ""
            },
            "expired_at": 0,
            "cancelled_at": -1,
            "failed_at": -1,
            "completed_at": -1,
            "metadata": {},
            "usage": {}
        }
    ],
    "first_id": "[REDACTED]",
    "last_id": "[REDACTED]",
    "has_more": false,
    "request_id": "[REDACTED]"
}
```

### 响应体 <span id="fe89113d06b4f" />

一个[运行步骤对象](/zh/model-studio/run-steps#799b4925a4emb)列表

## 检索运行步骤 <span id="e505b3b14fv3n" />

### 请求示例 <span id="336860b0472y8" />

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_cc2a3e9d-436b-482b-91c5-377e0f376506/runs/run_3de634fa-75d4-4370-adcf-92ba2a60c396/steps/step_4db180b5-d44a-4b12-9390-4307c6cb87a5' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Steps
      import os

      step = Steps.retrieve(
          'step_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          thread_id='thread_id',
          run_id='run_id'
      )
      ```

      ```java
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.runs.RunStep;
      import com.alibaba.dashscope.threads.runs.Runs;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Runs runs = new Runs();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              RunStep run = runs.retrieveStep("threadId", "runId", "stepId", apiKey);
          }
      }
      ```
    </CodeGroup>
  </Tab>
</Tabs>

### 请求体 <span id="bf890d1018t9u" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.72%" }} /><col style={{ width: "11.71%" }} /><col style={{ width: "5.86%" }} /><col style={{ width: "11.71%" }} /><col style={{ width: "53%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>参数描述</strong></p></th></tr></thead><tbody><tr><td><p>step\_id</p></td><td><p>string</p></td><td><p>是</p></td><td /><td><p>要检索的运行步骤 (RunStep) 的 ID。</p></td></tr><tr><td><p>thread\_id</p></td><td><p>string</p></td><td><p>是</p></td><td /><td><p>线程 (Thread) 的 ID。</p></td></tr><tr><td><p>run\_id</p></td><td><p>string</p></td><td><p>是</p></td><td /><td><p>运行 (Run) 的 ID。</p></td></tr><tr><td><p>workspace</p></td><td><p><em>string</em></p></td><td><p>是</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p><em>string</em></p></td><td><p>是</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

### 响应示例 <span id="4b92bdef3fkah" />

```json
{
    "assistant_id": "[REDACTED]",
    "cancelled_at": null,
    "completed_at": 1735025124000,
    "created_at": 1735025123150,
    "expired_at": null,
    "failed_at": null,
    "id": "[REDACTED]",
    "last_error": null,
    "metadata": {},
    "object": "thread.run.step",
    "request_id": "[REDACTED]",
    "run_id": "[REDACTED]",
    "status": "completed",
    "step_details": {
        "message_creation": {
            "message_id": "[REDACTED]"
        },
        "type": "message_creation"
    },
    "thread_id": "[REDACTED]",
    "type": "message_creation",
    "usage": null
}
```

### 响应体 <span id="09820c6852r51" />

与指定 ID 匹配的[运行步骤对象](/zh/model-studio/run-steps#799b4925a4emb)。

## 运行步骤对象 <span id="799b4925a4emb" />

表示运行执行过程中的一个步骤。

### 对象示例 <span id="d21861f89ffcy" />

```json expandable
{
  "id": "[REDACTED]",
  "assistant_id": "[REDACTED]",
  "cancelled_at": null,
  "completed_at": 1678886400,
  "created_at": 1678886300,
  "expired_at": null,
  "failed_at": null,
  "last_error": null,
  "metadata": {
    "user_id": "[REDACTED]",
    "context": "order_processing"
  },
  "object": "thread.run.step",
  "run_id": "[REDACTED]",
  "status": "completed",
  "step_details": {
    "type": "message_creation",
    "message_creation": {
      "message_id": "[REDACTED]"
    }
  },
  "thread_id": "[REDACTED]",
  "type": "message_creation",
  "usage": {
    "prompt_tokens": 10,
    "completion_tokens": 20,
    "total_tokens": 30
  }
}
```

### 对象属性 <span id="c7931f6b9a0lz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "21.49%" }} /><col style={{ width: "14.21%" }} /><col style={{ width: "64.3%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>参数描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>string</p></td><td><p>运行步骤的唯一标识符，可在 API 端点中引用。</p></td></tr><tr><td><p>assistant\_id</p></td><td><p>string</p></td><td><p>与此运行步骤关联的助手的 ID。</p></td></tr><tr><td><p>cancelled\_at</p></td><td><p>integer</p></td><td><p>运行步骤被取消的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>completed\_at</p></td><td><p>integer</p></td><td><p>运行步骤完成的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>created\_at</p></td><td><p>integer</p></td><td><p>运行步骤创建的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>expired\_at</p></td><td><p>integer</p></td><td><p>运行步骤过期的时间戳（Unix 13位时间戳，单位：毫秒）。如果父运行已过期，则步骤也被视为已过期。</p></td></tr><tr><td><p>failed\_at</p></td><td><p>integer</p></td><td><p>运行步骤失败的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>last\_error</p></td><td><p>object</p></td><td><p>与此运行步骤相关的最后一个错误。如果没有错误，则为null。</p></td></tr><tr><td><p>metadata</p></td><td><p>map</p></td><td><p>一组最多包含 16 个键值对，可以附加到对象上。用于以结构化格式存储有关对象的附加信息。键的最大长度为 64 个字符，值的最大长度为 512 个字符。</p></td></tr><tr><td><p>object</p></td><td><p>string</p></td><td><p>对象类型，始终为thread.run.step。</p></td></tr><tr><td><p>run\_id</p></td><td><p>string</p></td><td><p>此运行步骤所属的运行的 ID。</p></td></tr><tr><td><p>status</p></td><td><p>string</p></td><td><p>运行步骤的状态，可以是in\_progress（进行中）、cancelled（已取消）、failed（已失败）、completed（已完成）或expired（已过期）。</p></td></tr><tr><td><p>step\_details</p></td><td><p>object</p></td><td><p>运行步骤的详细信息。</p></td></tr><tr><td><p>thread\_id</p></td><td><p>string</p></td><td><p>运行的线程的 ID。</p></td></tr><tr><td><p>type</p></td><td><p>string</p></td><td><p>运行步骤的类型，可以是message\_creation（消息创建）或tool\_calls（工具调用）。</p></td></tr><tr><td><p>usage</p></td><td><p>object</p></td><td><p>资源使用情况统计信息。</p></td></tr><tr><td><p>status\_code</p></td><td><p>integer</p></td><td><p>HTTP 状态码。<strong>注意：</strong>此参数主要用于内部处理，通常情况下，您无需关注此参数。</p></td></tr></tbody></table>

## 错误码 <span id="97e145aeabbwf" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
