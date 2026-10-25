> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Runs（下线中）

> Runs 表示智能体在一个线程上执行的运行。

<meta property="og:title" content="Runs（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

## 创建运行任务 <span id="NTx71" />

创建一个运行。

<Tabs>
  <Tab title="HTTP">
    <strong>非流式</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_cc2a3e9d-436b-482b-91c5-377e0f376506/runs' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "assistant_id": "asst_46e95014-ccac-435d-b884-c97ac44a94d9",
        "metadata": {}
    }'
    ```

    <strong>流式</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_cc2a3e9d-436b-482b-91c5-377e0f376506/runs' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "assistant_id": "asst_46e95014-ccac-435d-b884-c97ac44a94d9",
        "metadata": {},
        "stream": true
    }'
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread id</p></td><td><p>所需要运行的thread id</p></td><td><p>string</p></td><td><p>是</p></td></tr><tr><td><p>assistant id</p></td><td><p>所需要运行的assistant id</p></td><td><p>string</p></td><td><p>是</p></td></tr><tr><td><p>model</p></td><td><p>所需要调用的model名字。如果在此传入，会替换掉assistant中定义的model</p></td><td><p>string</p></td><td><p>否</p></td></tr><tr><td><p>instructions</p></td><td><p>替换assistant中定义的instruction</p></td><td><p>string</p></td><td><p>否</p></td></tr><tr><td><p>tools</p></td><td><p>替换assistant中定义的tool 列表，</p><div><p>自定义tool鉴权信息传递</p><p>\{</p><p>"type": "plugin\_type", "auth": \{"type": "user\_http","user\_token": "bearer-token", }</p><p>}</p><p>assistant在请求插件时会将bearer放在\{"plugin\_type":\{"user\_token": "bearer-token"}}'添加到调用plugin的header里.</p></div></td><td><p>string</p></td><td><p>否</p></td></tr><tr><td><p>metadata</p></td><td><p>其他相关参数说明</p></td><td><p>string</p></td><td><p>否</p></td></tr><tr><td><p>stream</p></td><td><p>是否流式返回</p></td><td><p>boolean</p></td><td><p>否</p></td></tr><tr><td><p>temperature</p></td><td><p>用于控制随机性和多样性的程度。</p></td><td><p>float</p></td><td><p>否</p></td></tr><tr><td><p>top\_p</p></td><td><p>生成时，核采样方法的概率阈值。</p></td><td><p>float</p></td><td><p>否</p></td></tr><tr><td><p>top\_k</p></td><td><p>生成时，采样候选集的大小。</p></td><td><p>integer</p></td><td><p>否</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    非流式

    ```json
    {
        "id": "run_3de634fa-75d4-4370-adcf-92ba2a60c396",
        "object": "thread.run",
        "created_at": 1711517598169,
        "thread_id": "thread_cc2a3e9d-436b-482b-91c5-377e0f376506",
        "assistant_id": "asst_46e95014-ccac-435d-b884-c97ac44a94d9",
        "status": "in_progress",
        "model": "qwen-max",
        "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
        "tools": [
            {
                "type": "search"
            },
            {
                "type": "text_to_image"
            }
        ],
        "metadata": {},
        "request_id": "225292ff-9843-95d4-8591-b1377c33e2be"
    }
    ```

    流式

    ```bash expandable
    event:thread.created
    data:{"id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","object":"thread","created_at":1714448101977,"metadata":{},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.run.created
    data:{"id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","object":"thread.run","created_at":1714448101996,"thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","status":"queued","required_action":null,"last_error":null,"expires_at":null,"started_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"model":"qwen-max","instructions":"你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。","tools":[{"type":"text_to_image"},{"type":"function","function":{"name":"get_current_weather","description":"Get the current weather in a given location","parameters":{"type":"object","properties":{"location":{"type":"string","description":"The city and state, e.g. San Francisco, CA"},"unit":{"type":"string","enum":["celsius","fahrenheit"]}},"required":["location"]}}}],"file_ids":[],"metadata":{},"usage":null,"temperature":null,"top_p":null,"top_k":null,"max_tokens":null,"truncation_strategy":null,"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.run.queued
    data:{"id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","object":"thread.run","created_at":1714448101996,"thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","status":"queued","required_action":null,"last_error":null,"expires_at":null,"started_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"model":"qwen-max","instructions":"你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。","tools":[{"type":"text_to_image"},{"type":"function","function":{"name":"get_current_weather","description":"Get the current weather in a given location","parameters":{"type":"object","properties":{"location":{"type":"string","description":"The city and state, e.g. San Francisco, CA"},"unit":{"type":"string","enum":["celsius","fahrenheit"]}},"required":["location"]}}}],"file_ids":[],"metadata":{},"usage":null,"temperature":null,"top_p":null,"top_k":null,"max_tokens":null,"truncation_strategy":null,"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.run.in_progress
    data:{"id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","object":"thread.run","created_at":1714448101996,"thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","status":"in_progress","required_action":null,"last_error":null,"expires_at":null,"started_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"model":"qwen-max","instructions":"你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。","tools":[{"type":"text_to_image"},{"type":"function","function":{"name":"get_current_weather","description":"Get the current weather in a given location","parameters":{"type":"object","properties":{"location":{"type":"string","description":"The city and state, e.g. San Francisco, CA"},"unit":{"type":"string","enum":["celsius","fahrenheit"]}},"required":["location"]}}}],"file_ids":[],"metadata":{},"usage":null,"temperature":null,"top_p":null,"top_k":null,"max_tokens":null,"truncation_strategy":null,"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.run.step.created
    data:{"id":"step_52c42910-4993-496d-a7f3-ecd4cf007839","object":"thread.run.step","created_at":1714448102574,"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","run_id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","type":"message_creation","status":"in_progress","step_details":{"type":"message_creation","message_creation":{"message_id":"message_f4f68f6b-6003-4115-8f20-3ea590137502"}},"last_error":null,"expired_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"metadata":{},"usage":null,"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.run.step.in_progress
    data:{"id":"step_52c42910-4993-496d-a7f3-ecd4cf007839","object":"thread.run.step","created_at":1714448102574,"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","run_id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","type":"message_creation","status":"in_progress","step_details":{"type":"message_creation","message_creation":{"message_id":"message_f4f68f6b-6003-4115-8f20-3ea590137502"}},"last_error":null,"expired_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"metadata":{},"usage":null,"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.message.created
    data:{"id":"message_f4f68f6b-6003-4115-8f20-3ea590137502","object":"thread.message","created_at":1714448102574,"thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","incomplete_details":null,"completed_at":null,"incomplete_at":null,"role":"assistant","content":[],"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","run_id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","file_ids":[],"metadata":{},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.message.in_progress
    data:{"id":"message_f4f68f6b-6003-4115-8f20-3ea590137502","object":"thread.message","created_at":1714448102574,"thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","incomplete_details":null,"completed_at":null,"incomplete_at":null,"role":"assistant","content":[],"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","run_id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","file_ids":[],"metadata":{},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.message.delta
    data:{"id":"message_f4f68f6b-6003-4115-8f20-3ea590137502","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"9","annotations":[]}}],"file_ids":[]},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.message.delta
    data:{"id":"message_f4f68f6b-6003-4115-8f20-3ea590137502","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"9","annotations":[]}}],"file_ids":[]},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.message.delta
    data:{"id":"message_f4f68f6b-6003-4115-8f20-3ea590137502","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"9","annotations":[]}}],"file_ids":[]},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.message.delta
    data:{"id":"message_f4f68f6b-6003-4115-8f20-3ea590137502","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"乘以998","annotations":[]}}],"file_ids":[]},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.message.delta
    data:{"id":"message_f4f68f6b-6003-4115-8f20-3ea590137502","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"等于997002。","annotations":[]}}],"file_ids":[]},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.message.delta
    data:{"id":"message_f4f68f6b-6003-4115-8f20-3ea590137502","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"","annotations":[]}}],"file_ids":[]},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.message.completed
    data:{"id":"message_f4f68f6b-6003-4115-8f20-3ea590137502","object":"thread.message","created_at":1714448102574,"thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","incomplete_details":null,"completed_at":1714448104154,"incomplete_at":null,"role":"assistant","content":[{"type":"text","text":{"value":"999乘以998等于997002。","annotations":[]}}],"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","run_id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","file_ids":[],"metadata":{},"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.run.step.completed
    data:{"id":"step_52c42910-4993-496d-a7f3-ecd4cf007839","object":"thread.run.step","created_at":1714448102574,"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","run_id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","type":"message_creation","status":"completed","step_details":{"type":"message_creation","message_creation":{"message_id":"message_f4f68f6b-6003-4115-8f20-3ea590137502"}},"last_error":null,"expired_at":null,"cancelled_at":null,"failed_at":null,"completed_at":1714448104154,"metadata":{},"usage":null,"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:thread.run.completed
    data:{"id":"run_04fba6a3-3ac9-42e8-ba2b-9b5c889a8d58","object":"thread.run","created_at":1714448101996,"thread_id":"thread_5b7824dc-dbf9-4538-b214-624ee199caa4","assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","status":"completed","required_action":null,"last_error":null,"expires_at":null,"started_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"model":"qwen-max","instructions":"你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。","tools":[{"type":"text_to_image"},{"type":"function","function":{"name":"get_current_weather","description":"Get the current weather in a given location","parameters":{"type":"object","properties":{"location":{"type":"string","description":"The city and state, e.g. San Francisco, CA"},"unit":{"type":"string","enum":["celsius","fahrenheit"]}},"required":["location"]}}}],"file_ids":[],"metadata":{},"usage":{"input_tokens":419,"output_tokens":16,"total_tokens":435},"temperature":null,"top_p":null,"top_k":null,"max_tokens":null,"truncation_strategy":null,"request_id":"f38314af-21c4-945e-acdc-cb9765d1d48f"}

    event:done
    data:[DONE]
    ```

    <strong>输出参数</strong>

    一个[运行对象](/zh/model-studio/runs#dc2479a955ip5)。
  </Tab>

  <Tab title="SDK">
    <strong>流式</strong>

    <CodeGroup dropdown>
      ```python
      import dashscope
      import os
      stream_iterator = dashscope.Runs.create(
          'thread_id',  # 请替换为实际的 thread_id
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          assistant_id='assistant_id',  # 请替换为实际的 assistant_id
          stream=True
      )

      for event, msg in stream_iterator:
          print(event)
          print(msg)
      ```

      ```java
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.runs.AssistantStreamMessage;
      import com.alibaba.dashscope.threads.runs.RunParam;
      import com.alibaba.dashscope.threads.runs.Runs;
      import io.reactivex.Flowable;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Runs runs = new Runs();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              RunParam runParam = RunParam.builder()
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .assistantId("assistantId")
                      .stream(true)
                      .build();
              Flowable<AssistantStreamMessage> run = runs.createStream("threadId", runParam);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>string</p></td><td><p>-</p></td><td><p>期望在那个Thread run</p></td></tr><tr><td><p>assistant\_id</p></td><td><p>string</p></td><td><p>-</p></td><td><p>assistant id</p></td></tr><tr><td><p>model</p></td><td><p>string</p></td><td><p>-</p></td><td><p>指定使用的模型</p></td></tr><tr><td><p>instructions</p></td><td><p>string</p></td><td><p>None</p></td><td><p>指定本次run所用的指令</p></td></tr><tr><td><p>tools</p></td><td><p>List\[Dict]</p></td><td><p>\[]</p></td><td><p>assistant使用的tools</p><div><p>自定义tool鉴权信息传递</p><p>\{</p><p>"type": "plugin\_type", "auth": \{"type": "user\_http","user\_token": "bearer-token", }</p><p>}</p><p>assistant在请求插件时会将bearer放在\{"plugin\_type":\{"user\_token": "bearer-token"}}'添加到调用plugin的header里.</p></div></td></tr><tr><td><p>stream</p></td><td><p>boolean</p></td><td><p>False</p></td><td><p>用于控制是否是流式输出。</p></td></tr><tr><td><p>metadata</p></td><td><p>Dict</p></td><td><p>None</p></td><td><p>assistant关联信息</p></td></tr><tr><td><p>temperature</p></td><td><p>用于控制随机性和多样性的程度。</p></td><td><p>float</p></td><td><p>否</p></td></tr><tr><td><p>top\_p</p></td><td><p>生成时，核采样方法的概率阈值。</p></td><td><p>float</p></td><td><p>否</p></td></tr><tr><td><p>top\_k</p></td><td><p>生成时，采样候选集的大小。</p></td><td><p>integer</p></td><td><p>否</p></td></tr><tr><td><p>metadata</p></td><td><p>其他相关参数说明</p></td><td><p>string</p></td><td><p>否</p></td></tr><tr><td><p>truncation\_strategy</p></td><td><p>Dict</p></td><td><p>None</p></td><td><p>设置thread中消息截断策略, 目前只支持last\_messages,默认为：</p><p>"truncation\_strategy": \{</p><p>"type": "last\_messages",</p><p>"last\_messages": 10</p><p>}</p></td></tr><tr><td><p>workspace</p></td><td><p>string</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>string</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    流式输出将输出event和event 数据，如下

    ```json expandable
    thread.run.created
    {'tools': [{'type': 'quark_search'}], 'required_action': None, 'id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'object': 'thread.run', 'created_at': 1714460227904, 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'status': 'queued', 'last_error': None, 'expires_at': None, 'started_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': None, 'model': 'qwen-max', 'instructions': 'You are a helpful assistant. When asked a question, use tools wherever possible.', 'file_ids': [], 'metadata': {}, 'usage': None, 'temperature': None, 'top_p': None, 'top_k': None, 'max_tokens': None, 'truncation_strategy': None, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.queued
    {'tools': [{'type': 'quark_search'}], 'required_action': None, 'id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'object': 'thread.run', 'created_at': 1714460227904, 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'status': 'queued', 'last_error': None, 'expires_at': None, 'started_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': None, 'model': 'qwen-max', 'instructions': 'You are a helpful assistant. When asked a question, use tools wherever possible.', 'file_ids': [], 'metadata': {}, 'usage': None, 'temperature': None, 'top_p': None, 'top_k': None, 'max_tokens': None, 'truncation_strategy': None, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.in_progress
    {'tools': [{'type': 'quark_search'}], 'required_action': None, 'id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'object': 'thread.run', 'created_at': 1714460227904, 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'status': 'in_progress', 'last_error': None, 'expires_at': None, 'started_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': None, 'model': 'qwen-max', 'instructions': 'You are a helpful assistant. When asked a question, use tools wherever possible.', 'file_ids': [], 'metadata': {}, 'usage': None, 'temperature': None, 'top_p': None, 'top_k': None, 'max_tokens': None, 'truncation_strategy': None, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.created
    {'step_details': {'tool_calls': [], 'type': 'tool_calls'}, 'usage': None, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step', 'created_at': 1714460228960, 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'run_id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'type': 'tool_calls', 'status': 'in_progress', 'expired_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': None, 'metadata': {}, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200, 'last_error': None}
    thread.run.step.in_progress
    {'step_details': {'tool_calls': [], 'type': 'tool_calls'}, 'usage': None, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step', 'created_at': 1714460228960, 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'run_id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'type': 'tool_calls', 'status': 'in_progress', 'expired_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': None, 'metadata': {}, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200, 'last_error': None}
    thread.run.step.delta
    {'delta': {'step_details': {'tool_calls': [{'type': 'quark_search', 'quark_search': {'arguments': ''}}], 'type': 'tool_calls'}}, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.delta
    {'delta': {'step_details': {'tool_calls': [{'type': 'quark_search', 'quark_search': {'arguments': '{"query": "'}}], 'type': 'tool_calls'}}, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.delta
    {'delta': {'step_details': {'tool_calls': [{'type': 'quark_search', 'quark_search': {'arguments': '今天 北京 天气"}'}}], 'type': 'tool_calls'}}, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.delta
    {'delta': {'step_details': {'tool_calls': [{'type': 'quark_search', 'quark_search': {'arguments': ''}}], 'type': 'tool_calls'}}, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.delta
    {'delta': {'step_details': {'tool_calls': [{'type': 'quark_search', 'quark_search': {'arguments': ''}}], 'type': 'tool_calls'}}, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.delta
    {'delta': {'step_details': {'tool_calls': [{'type': 'quark_search', 'plugin_name': {'output': '{"success": true, "errorCode": null, "errorMsg": null, "data": [{"title": "中国北京市天气情况", "url": null, "desc": "实时天气情况，晴,气温24度，体感温度:23度，西风1级，冷热适宜，感觉很舒适。查看未来15天气预报，请点击这里", "type": "weather_moji", "original": null, "query": null, "source": null}, {"title": "北京市气象局", "url": "http://bj.cma.gov.cn/fzlm/index.html", "desc": "36小时天气预报（2024-01-25 05:24:58北京市气象台发布） 今天白天 最高气温5℃ 晴间多云 3级，阵风5级转2级 偏北风 今天夜间 最低气温-5℃ 晴 2级转3级 偏北风 明天白天 最高气温5℃ 晴 3级转1...", "type": "structure_web_info", "original": null, "query": null, "source": "北京市气象局"}, {"title": "北京", "url": "https://m.weather.com.cn/mweather/101010100.shtml?_t=1480291523052", "desc": "北京天气预报，及时准确发布中央气象台天气信息，便捷查询北京今日天气，北京周末天气，北京一周天气预报，北京蓝天预报，北京天气预报，北京40日天气预报，还提供北京的生活指数、健康指数、...", "type": "normal", "original": null, "query": null, "source": "中国天气"}, {"title": "墨迹天气中文官方网站", "url": "https://m.moji.com/", "desc": "墨迹天气预报app，全球约6.5亿人在使用的天气app，支持199个国家20多万个城市及地区的天气预报查询，下载墨迹天气预报app，实时监测阴晴雨雪，实时空气质量及空气质量等级预报。", "type": "structure_web_info", "original": null, "query": null, "source": "墨迹天气手机版"}, {"title": "北京", "url": "http://sq.weather.com.cn/mweather15d/101010100.shtml?from=singlemessage", "desc": "北京天气预报，及时准确发布中央气象台天气信息，便捷查询北京今日天气，北京周末天气，北京一周天气预报，北京蓝天预报，北京天气预报，北京40日天气预报，还提供北京的生活指数、健康指数、...", "type": "structure_web_info", "original": null, "query": null, "source": "中国天气网"}, {"title": "头条新闻", "url": null, "desc": "中国天气网于1天前报道：预报 预警 雷达 云图 天气地图 专业产品 资讯 视频 节气 我的天空 ...恭王府 北京奥林匹克公园 奥林匹克森林公园 北京石景山游乐园 金海湖 京东大溶洞旅游区 明十三陵 银山塔林风景区 中国航空博物馆 北京欢乐谷 北京中华...", "type": "news_uchq", "original": null, "query": null, "source": null}, {"title": "北京", "url": "http://www.weather.com.cn/weather1d/101010100.shtml", "desc": "北京天气预报，及时准确发布中央气象台天气信息，便捷查询北京今日天气，北京周末天气，北京一周天气预报，北京15日...", "type": "structure_web_info", "original": null, "query": null, "source": "中国天气网"}, {"title": "北京24小时天气查询_北京今日天气预报查询_2345天气王", "url": "http://waptianqi.2345.com/24hour-54511.htm", "desc": "2345天气预报为你及时提供最新的北京天气，今天24小时天气情况等信息，同时还提供当日北京气象指数等信息", "type": "structure_web_info", "original": null, "query": null, "source": "2345天气王"}, {"title": "北京15日天气详情", "url": "http://waptianqi.2345.com/beijing-54511.htm", "desc": "2345天气预报提供北京天气预报，未来北京15日天气，通过2345天气预报详细了解北京天气预报以及北京周边各地区未来15天、30日天气情况，温度，空气质量，降水，风力，气压，紫外线强度等！", "type": "structure_web_info", "original": null, "query": null, "source": "2345天气王"}, {"title": "北京天气预报15天", "url": "https://m.tianqi.com/beijing/15/", "desc": "北京天气预报15天详情 12/11 今天 小雪-2~2 ℃ 12/12 明天 多云转小雪-2~2 ℃ 12/13 后天 小雪-5~0 ℃ 12/14 周四 小雪-5~-1 ℃ 12/15 周五 阴转多云-10~-2 ℃ 12/16 周六 晴-10~-3 ℃ 12/17 ...", "type": "structure_web_info", "original": null, "query": null, "source": "天气网"}, {"title": "北京天气", "url": "https://t.8684.com/beijing_beijing", "desc": "北京天气预报一周15天,北京天气预报,北京天气预报一周,北京天气预报15天,北京本周天气预报,北京下周天气预报,北京天气预报15天查询,北京天气预报10天", "type": "structure_web_info", "original": null, "query": null, "source": "8684天气"}], "requestId": null, "failed": false}'}}], 'type': 'tool_calls'}}, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.delta
    {'delta': {'step_details': {'tool_calls': [{'quark_search': {'output': ''}}], 'type': 'tool_calls'}}, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.delta
    {'delta': {'step_details': {'tool_calls': [{'quark_search': {'output': ''}}], 'type': 'tool_calls'}}, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.completed
    {'step_details': {'tool_calls': [{'type': 'quark_search', 'plugin_name': {'arguments': '{"query": "今天 北京 天气"}', 'output': '{"success": true, "errorCode": null, "errorMsg": null, "data": [{"title": "中国北京市天气情况", "url": null, "desc": "实时天气情况，晴,气温24度，体感温度:23度，西风1级，冷热适宜，感觉很舒适。查看未来15日天气预报，请点击这里", "type": "weather_moji", "original": null, "query": null, "source": null}, {"title": "北京市气象局", "url": "http://bj.cma.gov.cn/fzlm/index.html", "desc": "36小时天气预报（2024-01-25 05:24:58北京市气象台发布） 今天白天 最高气温5℃ 晴间多云 3级，阵风5级转2级 偏北风 今天夜间 最低气温-5℃ 晴 2级转3级 偏北风 明天白天 最高气温5℃ 晴 3级转1...", "type": "structure_web_info", "original": null, "query": null, "source": "北京市气象局"}, {"title": "北京", "url": "https://m.weather.com.cn/mweather/101010100.shtml?_t=1480291523052", "desc": "北京天气预报，及时准确发布中央气象台天气信息，便捷查询北京今日天气，北京周末天气，北京一周天气预报，北京蓝天预报，北京天气预报，北京40日天气预报，还提供北京的生活指数、健康指数、...", "type": "normal", "original": null, "query": null, "source": "中国天气"}, {"title": "墨迹天气中文官方网站", "url": "https://m.moji.com/", "desc": "墨迹天气预报app，全球约6.5亿人在使用的天气app，支持199个国家20多万个城市及地区的天气预报查询，下载墨迹天气预报app，实时监测阴晴雨雪，实时空气质量及空气质量等级预报。", "type": "structure_web_info", "original": null, "query": null, "source": "墨迹天气手机版"}, {"title": "北京", "url": "http://sq.weather.com.cn/mweather15d/101010100.shtml?from=singlemessage", "desc": "北京天气预报，及时准确发布中央气象台天气信息，便捷查询北京今日天气，北京周末天气，北京一周天气预报，北京蓝天预报，北京天气预报，北京40日天气预报，还提供北京的生活指数、健康指数、...", "type": "structure_web_info", "original": null, "query": null, "source": "中国天气网"}, {"title": "头条新闻", "url": null, "desc": "中国天气网于1天前报道：预报 预警 雷达 云图 天气地图 专业产品 资讯 视频 节气 我的天空 ...恭王府 北京奥林匹克公园 奥林匹克森林公园 北京石景山游乐园 金海湖 京东大溶洞旅游区 明十三陵 银山塔林风景区 中国航空博物馆 北京欢乐谷 北京中华...", "type": "news_uchq", "original": null, "query": null, "source": null}, {"title": "北京", "url": "http://www.weather.com.cn/weather1d/101010100.shtml", "desc": "北京天气预报，及时准确发布中央气象台天气信息，便捷查询北京今日天气，北京周末天气，北京一周天气预报，北京15日...", "type": "structure_web_info", "original": null, "query": null, "source": "中国天气网"}, {"title": "北京24小时天气查询_北京今日天气预报查询_2345天气王", "url": "http://waptianqi.2345.com/24hour-54511.htm", "desc": "2345天气预报为你及时提供最新的北京今天天气，今天24小时天气情况等信息，同时还提供当日北京气象指数等信息", "type": "structure_web_info", "original": null, "query": null, "source": "2345天气王"}, {"title": "北京15日天气详情", "url": "http://waptianqi.2345.com/beijing-54511.htm", "desc": "2345天气预报提供北京天气预报，未来北京15日天气，通过2345天气预报详细了解北京天气预报以及北京周边各地区未来15天、30日天气情况，温度，空气质量，降水，风力，气压，紫外线强度等！", "type": "structure_web_info", "original": null, "query": null, "source": "2345天气王"}, {"title": "北京天气预报15天", "url": "https://m.tianqi.com/beijing/15/", "desc": "北京天气预报15天详情 12/11 今天 小雪-2~2 ℃ 12/12 明天 多云转小雪-2~2 ℃ 12/13 后天 小雪-5~0 ℃ 12/14 周四 小雪-5~-1 ℃ 12/15 周五 阴转多云-10~-2 ℃ 12/16 周六 晴-10~-3 ℃ 12/17 ...", "type": "structure_web_info", "original": null, "query": null, "source": "天气网"}, {"title": "北京天气", "url": "https://t.8684.com/beijing_beijing", "desc": "北京天气预报一周15天,北京天气预报,北京天气预报一周,北京天气预报15天,北京本周天气预报,北京下周天气预报,北京天气预报15天查询,北京天气预报10天", "type": "structure_web_info", "original": null, "query": null, "source": "8684天气"}], "requestId": null, "failed": false}'}}], 'type': 'tool_calls'}, 'usage': None, 'id': 'step_f923bd08-1e22-40d9-bf54-373b21fde378', 'object': 'thread.run.step', 'created_at': 1714460228960, 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'run_id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'type': 'tool_calls', 'status': 'in_progress', 'expired_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': None, 'metadata': {}, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200, 'last_error': None}
    thread.run.step.created
    {'step_details': {'message_creation': {'message_id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d'}, 'type': 'message_creation'}, 'usage': None, 'id': 'step_89e631c7-491a-4bff-b1f5-1298f88db531', 'object': 'thread.run.step', 'created_at': 1714460234236, 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'run_id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'type': 'message_creation', 'status': 'in_progress', 'expired_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': None, 'metadata': {}, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200, 'last_error': None}
    thread.run.step.in_progress
    {'step_details': {'message_creation': {'message_id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d'}, 'type': 'message_creation'}, 'usage': None, 'id': 'step_89e631c7-491a-4bff-b1f5-1298f88db531', 'object': 'thread.run.step', 'created_at': 1714460234236, 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'run_id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'type': 'message_creation', 'status': 'in_progress', 'expired_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': None, 'metadata': {}, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200, 'last_error': None}
    thread.message.created
    {'content': [], 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message', 'created_at': 1714460234235, 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'incomplete_details': None, 'completed_at': None, 'incomplete_at': None, 'role': 'assistant', 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'run_id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'file_ids': [], 'metadata': {}, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.in_progress
    {'content': [], 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message', 'created_at': 1714460234235, 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'incomplete_details': None, 'completed_at': None, 'incomplete_at': None, 'role': 'assistant', 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'run_id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'file_ids': [], 'metadata': {}, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '今天'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '北京的天气情况是'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '晴朗的，气温约为24'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '度，体感温度为23'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '度，西风1级，天气'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '较为舒适。不过，我也找到了北京市'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '气象局发布的更详细信息，提到'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '今天白天最高气温为5℃，'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '天气晴间多云，有3'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '级偏北风，阵风可达'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '5级，夜间最低气温降至-'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '5℃，依旧保持晴朗，'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '风向转为偏北风，'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '风力2级转3级。'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '请根据实时天气调整着装，'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': '注意保暖。'}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.delta
    {'delta': {'content': {'text': {'annotations': [], 'value': ''}, 'type': 'text'}, 'role': 'assistant'}, 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message.delta', 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.message.completed
    {'content': [{'text': {'annotations': [], 'value': '今天北京的天气情况是晴朗的，气温约为24度，体感温度为23度，西风1级，天气较为舒适。不过，我也找到了北京市气象局发布的更详细信息，提到今天白天最高气温为5℃，天气晴间多云，有3级偏北风，阵风可达5级，夜间最低气温降至-5℃，依旧保持晴朗，风向转为偏北风，风力2级转3级。请根据实时天气调整着装，注意保暖。'}, 'type': 'text'}], 'id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d', 'object': 'thread.message', 'created_at': 1714460234235, 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'incomplete_details': None, 'completed_at': 1714460245637, 'incomplete_at': None, 'role': 'assistant', 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'run_id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'file_ids': [], 'metadata': {}, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    thread.run.step.completed
    {'step_details': {'message_creation': {'message_id': 'message_93605cd7-fb74-468b-b6fb-4975cd253f8d'}, 'type': 'message_creation'}, 'usage': None, 'id': 'step_89e631c7-491a-4bff-b1f5-1298f88db531', 'object': 'thread.run.step', 'created_at': 1714460234236, 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'run_id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'type': 'message_creation', 'status': 'completed', 'expired_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': 1714460245637, 'metadata': {}, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200, 'last_error': None}
    thread.run.completed
    {'tools': [{'type': 'quark_search'}], 'required_action': None, 'id': 'run_0f314675-06a4-4379-929c-d29cd351a446', 'object': 'thread.run', 'created_at': 1714460227904, 'thread_id': 'thread_28f0b00b-d825-48f5-bb27-f3b171a64d33', 'assistant_id': 'asst_9453fe59-d6eb-498e-aeca-9abecd492e51', 'status': 'completed', 'last_error': None, 'expires_at': None, 'started_at': None, 'cancelled_at': None, 'failed_at': None, 'completed_at': None, 'model': 'qwen-max', 'instructions': 'You are a helpful assistant. When asked a question, use tools wherever possible.', 'file_ids': [], 'metadata': {}, 'usage': {'input_tokens': 1865, 'output_tokens': 139, 'total_tokens': 2004}, 'temperature': None, 'top_p': None, 'top_k': None, 'max_tokens': None, 'truncation_strategy': None, 'request_id': '38fb88b8-196a-9919-8815-f7ed9aa0891f', 'status_code': 200}
    ```

    <strong>输出参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>字段类型</strong></p></th><th><p><strong>字段描述</strong></p></th></tr></thead><tbody><tr><td><p>status\_code</p></td><td><p>integer</p></td><td><p>为调用http status code，200表示调用成功，其他表示调用出错</p></td></tr><tr><td><p>id</p></td><td><p>string</p></td><td><p>Run id，为uuid字符串</p></td></tr><tr><td><p>model</p></td><td><p>string</p></td><td><p>Run 使用的模型名称</p></td></tr><tr><td><p>instructions</p></td><td><p>string</p></td><td><p>assistant instruction信息，指定assisiant功能信息。</p></td></tr><tr><td><p>metadata</p></td><td><p>object</p></td><td><p>key，value对信息，描述assistant信息</p></td></tr><tr><td><p>status</p></td><td><p>string</p></td><td><p>Run的状态，包含'in\_progress, 等</p></td></tr><tr><td><p>thread\_id</p></td><td><p>string</p></td><td><p>Run所属Thread id</p></td></tr><tr><td><p>tools</p></td><td><p>List\[Tool]</p></td><td><p>Run使用的tool列表</p></td></tr><tr><td><p>created\_at</p></td><td><p>timestamp</p></td><td><p>assistant创建时间</p></td></tr><tr><td><p>gmt\_created</p></td><td><p>datetime</p></td><td><p>2024-03-22 17:12:31</p></td></tr><tr><td><p>gmt\_modified</p></td><td><p>datetime</p></td><td><p>2024-03-22 17:12:31</p></td></tr><tr><td><p>code</p></td><td><p>string</p></td><td><p>表示请求失败，表示错误码，成功忽略。</p><p>python only</p></td></tr><tr><td><p>message</p></td><td><p>string</p></td><td><p>失败，表示失败详细信息，成功忽略。</p><p>python only</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 列出运行任务 <span id="Bj3EB" />

返回属于线程的运行列表。

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_cc2a3e9d-436b-482b-91c5-377e0f376506/runs?limit=20&order=asc' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28.009259%" }} /><col style={{ width: "28.009259%" }} /><col style={{ width: "24.421296%" }} /><col style={{ width: "19.560185%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread id</p></td><td><p>run list 所隶属的thread id</p></td><td><p>string</p></td><td><p>是</p></td></tr><tr><td><p>limit</p></td><td><p>本次查询的任务数</p></td><td><p>integer</p></td><td><p>否</p></td></tr><tr><td><p>order</p></td><td><p>按照创建时间升序或降序排列</p></td><td><p>string</p></td><td><p>否（默认为“desc“）</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json expandable
    {
        "object": "list",
        "data": [
            {
                "id": "run_3de634fa-75d4-4370-adcf-92ba2a60c396",
                "object": "thread.run",
                "created_at": 1711517598169,
                "thread_id": "thread_cc2a3e9d-436b-482b-91c5-377e0f376506",
                "assistant_id": "asst_46e95014-ccac-435d-b884-c97ac44a94d9",
                "status": "completed",
                "required_action": {},
                "last_error": {},
                "expires_at": 0,
                "started_at": 0,
                "cancelled_at": 0,
                "failed_at": 0,
                "completed_at": 0,
                "model": "qwen-max",
                "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
                "tools": [
                    {
                        "type": "search"
                    },
                    {
                        "type": "text_to_image"
                    }
                ],
                "metadata": {},
                "usage": {
                    "input_tokens": 66,
                    "output_tokens": 9,
                    "total_tokens": 75
                }
            }
        ],
        "first_id": "run_3de634fa-75d4-4370-adcf-92ba2a60c396",
        "last_id": "run_3de634fa-75d4-4370-adcf-92ba2a60c396",
        "has_more": false,
        "request_id": "ff3d2aea-555c-9825-80ca-24029a636641"
    }
    ```

    <strong>输出参数</strong>

    一个[运行对象](/zh/model-studio/runs#dc2479a955ip5)列表。
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Runs
      import os
      runs = Runs.list(
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
      import com.alibaba.dashscope.threads.runs.Run;
      import com.alibaba.dashscope.threads.runs.Runs;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Runs runs = new Runs();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              GeneralListParam listRuns = GeneralListParam.builder()
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .build();
              ListResult<Run> run = runs.list("threadId", listRuns);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>指定要list的Thread id</p></td></tr><tr><td><p>limit</p></td><td><p>int</p></td><td><p>None</p></td><td><p>本次查询的任务数</p></td></tr><tr><td><p>order</p></td><td><p>str</p></td><td><p>None</p></td><td><p>按照创建时间升序或降序排列，默认值根据服务器配置确定</p><ul><li><p>desc：降序</p></li><li><p>asc：升序</p></li></ul></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>字段类型</strong></p></th><th><p><strong>字段描述</strong></p></th></tr></thead><tbody><tr><td><p>has\_more</p></td><td><p>bool</p></td><td><p>是否有更多的数据可供获取</p><ul><li><p>false：表示当前请求返回的数据已经是全部数据，没有更多数据可供查询</p></li><li><p>true：表示还有更多数据尚未返回，可通过进一步请求来获取</p></li></ul></td></tr><tr><td><p>last\_id</p></td><td><p>str</p></td><td><p>返回结果中的最后一个任务ID</p></td></tr><tr><td><p>first\_id</p></td><td><p>str</p></td><td><p>返回结果中的第一个任务ID</p></td></tr><tr><td><p>data</p></td><td><p>list\[Run]</p></td><td><p>Run对象列表</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 检索运行任务 <span id="TobTm" />

检索运行。

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_cc2a3e9d-436b-482b-91c5-377e0f376506/runs/run_3de634fa-75d4-4370-adcf-92ba2a60c396' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread id</p></td><td><p>待检索的run id 所传入的thread id</p></td><td><p>string</p></td><td><p>是</p></td></tr><tr><td><p>run\_id</p></td><td><p>待检索的run\_id</p></td><td><p>string</p></td><td><p>是</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json expandable
    {
        "id": "run_3de634fa-75d4-4370-adcf-92ba2a60c396",
        "object": "thread.run",
        "created_at": 1711517598169,
        "thread_id": "thread_cc2a3e9d-436b-482b-91c5-377e0f376506",
        "assistant_id": "asst_46e95014-ccac-435d-b884-c97ac44a94d9",
        "status": "completed",
        "required_action": {},
        "last_error": {},
        "expires_at": 0,
        "started_at": 0,
        "cancelled_at": 0,
        "failed_at": 0,
        "completed_at": 0,
        "model": "qwen-max",
        "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
        "tools": [
            {
                "type": "search"
            },
            {
                "type": "text_to_image"
            }
        ],
        "metadata": {},
        "usage": {
            "input_tokens": 66,
            "output_tokens": 9,
            "total_tokens": 75
        },
        "request_id": "05c5f4ea-5e6b-9901-9849-7a0943a0db43"
    }
    ```

    <strong>输出参数</strong>

    与指定 ID 匹配的修改后的[运行对象](/zh/model-studio/runs#dc2479a955ip5)。
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Runs
      import os
      run = Runs.retrieve(
          'run_id',
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
      import com.alibaba.dashscope.threads.runs.Run;
      import com.alibaba.dashscope.threads.runs.Runs;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Runs runs = new Runs();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              Run run = runs.retrieve("threadId", "runId", apiKey);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>run\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>指定要查询的Run id</p></td></tr><tr><td><p>thread\_id</p></td><td><p><em>str</em></p></td><td><p>-</p></td><td><p>指定Run所属Thread id</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    参考create结果
  </Tab>
</Tabs>

## 修改运行任务 <span id="a5jsi" />

修改运行。

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_cc2a3e9d-436b-482b-91c5-377e0f376506/runs/run_3de634fa-75d4-4370-adcf-92ba2a60c396' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "metadata": {
            "user": "abc123"
        }
    }'
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>待修改的thread\_id</p></td><td><p>string</p></td><td><p>是</p></td></tr><tr><td><p>run\_id</p></td><td><p>待修改的run\_id</p></td><td><p>string</p></td><td><p>是</p></td></tr><tr><td><p>\*</p></td><td><p>其他可选的输入参数</p></td><td><p>string</p></td><td><p>否</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    ```json expandable
    {
        "id": "run_3de634fa-75d4-4370-adcf-92ba2a60c396",
        "object": "thread.run",
        "created_at": 1711517598169,
        "thread_id": "thread_cc2a3e9d-436b-482b-91c5-377e0f376506",
        "assistant_id": "asst_46e95014-ccac-435d-b884-c97ac44a94d9",
        "status": "completed",
        "required_action": {},
        "last_error": {},
        "expires_at": 0,
        "started_at": 0,
        "cancelled_at": 0,
        "failed_at": 0,
        "completed_at": 0,
        "model": "qwen-max",
        "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
        "tools": [
            {
                "type": "search"
            },
            {
                "type": "text_to_image"
            }
        ],
        "metadata": {
            "user": "abc123"
        },
        "usage": {
            "input_tokens": 66,
            "output_tokens": 9,
            "total_tokens": 75
        },
        "request_id": "bb6a5c5c-146f-95b8-bafa-e6c467e4f2d3"
    }
    ```

    <strong>输出参数</strong>

    与指定 ID 匹配的修改后的[运行对象](/zh/model-studio/runs#dc2479a955ip5)。
  </Tab>

  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Runs
      import os

      run = Runs.update(
          'run_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          thread_id='thread_id',
          metadata={'newkey': 'newvalue'}
      )
      ```

      ```java
      import java.util.HashMap;
      import java.util.Map;
      import com.alibaba.dashscope.common.UpdateMetadataParam;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.runs.Run;
      import com.alibaba.dashscope.threads.runs.Runs;

      public class Main {
          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Runs runs = new Runs();
              Map<String, String> metadata = new HashMap<>();
              metadata.put("key", "value");
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              UpdateMetadataParam updateParam = UpdateMetadataParam.builder()
                      .metadata(metadata)
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .build();
              Run run = runs.update("threadId", "runId", updateParam);
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>run\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>要更新的Run id</p></td></tr><tr><td><p>thread\_id</p></td><td><p>str</p></td><td><p>-</p></td><td><p>Run所属的Thread id</p></td></tr><tr><td><p>metadata</p></td><td><p>Dict</p></td><td><p>None</p></td><td><p>assistant关联信息</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    同create，参考create
  </Tab>
</Tabs>

## 提交工具输出并继续执行任务 <span id="NegzX" />

当运行具有`status: "requires_action"`且`required_action.type`是`submit_tool_outputs`时，可以使用此端点在所有工具调用完成后提交工具输出。所有输出必须在单个请求中提交。

<Tabs>
  <Tab title="HTTP">
    <strong>代码示例</strong>

    在使用Function call过程中，需要把中间function运行结果返回给run，并将run的状态从“require\_action" 切换回正常状态。此时，“<strong>required\_action.type”</strong> 的值为“<strong>submit\_tool\_outputs</strong>”。

    非流式

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_9157dfad-71fd-4add-b7a9-1afa452d2d67/runs/run_99935dbd-e731-4bad-b450-60d9d6bf9a44/submit_tool_outputs' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "tool_outputs": [
          {
            "tool_call_id": "",
            "output": "rain"
          }
        ]
      }'
    ```

    流式

    ```bash
    curl --location 'https://dashscope.aliyuncs.com/api/v1/threads/thread_9157dfad-71fd-4add-b7a9-1afa452d2d67/runs/run_99935dbd-e731-4bad-b450-60d9d6bf9a44/submit_tool_outputs' \
    --header 'Content-Type: application/json' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --data '{
        "tool_outputs": [
          {
            "tool_call_id": "",
            "output": "rain"
          }
        ],
        "stream": true
      }'
    ```

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.956989%" }} /><col style={{ width: "27.956989%" }} /><col style={{ width: "24.492234%" }} /><col style={{ width: "19.593787%" }} /></colgroup><thead><tr><th><p><strong>输入参数名字</strong></p></th><th><p><strong>输入参数的描述</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>是否必须</strong></p></th></tr></thead><tbody><tr><td><p>thread\_id</p></td><td><p>待修改的thread\_id</p></td><td><p>string</p></td><td><p>是</p></td></tr><tr><td><p>run\_id</p></td><td><p>待修改的run\_id</p></td><td><p>string</p></td><td><p>是</p></td></tr><tr><td><p>tool\_outputs</p></td><td><p>待返回给 Assistant 的工具调用结果列表</p></td><td><p>array</p></td><td><p>是</p></td></tr><tr><td><p>tool\_outputs\[].tool\_call\_id</p></td><td><p>工具调用id</p></td><td><p>string</p></td><td><p>否</p></td></tr><tr><td><p>tool\_outputs\[].output</p></td><td><p>待返回的结果</p></td><td><p>string</p></td><td><p>是</p></td></tr><tr><td><p>stream</p></td><td><p>是否流式返回</p></td><td><p>boolean</p></td><td><p>否</p></td></tr></tbody></table>

    <strong>返回结果</strong>

    非流式

    ```json expandable
    {
        "id": "run_99935dbd-e731-4bad-b450-60d9d6bf9a44",
        "object": "thread.run",
        "created_at": 1711519089921,
        "thread_id": "thread_9157dfad-71fd-4add-b7a9-1afa452d2d67",
        "assistant_id": "asst_ce66cfa8-b2f8-4e4c-8e17-2c335145be42",
        "status": "in_progress",
        "required_action": {
            "type": "submit_tool_outputs",
            "submit_tool_outputs": {
                "tool_calls": [
                    {
                        "function": {
                            "name": "get_current_weather",
                            "arguments": "{\"location\": \"西安\"}"
                        },
                        "id": "",
                        "type": "function"
                    }
                ]
            }
        },
        "last_error": {},
        "expires_at": 0,
        "started_at": 0,
        "cancelled_at": 0,
        "failed_at": 0,
        "completed_at": 0,
        "model": "qwen-max",
        "instructions": "你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。",
        "tools": [
            {
                "type": "text_to_image"
            },
            {
                "type": "function",
                "function": {
                    "name": "get_current_weather",
                    "description": "Get the current weather in a given location",
                    "parameters": {
                        "type": "object",
                        "properties": {
                            "location": {
                                "type": "string",
                                "description": "The city and state, e.g. San Francisco, CA"
                            },
                            "unit": {
                                "type": "string",
                                "enum": [
                                    "celsius",
                                    "fahrenheit"
                                ]
                            }
                        },
                        "required": [
                            "location"
                        ]
                    }
                }
            }
        ],
        "metadata": {},
        "usage": null,
        "request_id": "792c29a2-e771-9840-9d99-df4f892badd0"
    }
    ```

    流式

    ```plaintext expandable
    event:thread.run.step.completed
    data:{"id":"step_74a8b5e2-3d23-41d9-b5c8-9bf3ba3839d0","object":"thread.run.step","created_at":1714448906129,"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","run_id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","type":"tool_calls","status":"completed","step_details":{"type":"tool_calls","tool_calls":[{"id":"","type":"function","function":{"name":"get_current_weather","arguments":"{\"location\": \"Xi'an\"}","output":"rain"}}]},"last_error":null,"expired_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"metadata":{},"usage":null,"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.run.queued
    data:{"id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","object":"thread.run","created_at":1714448903737,"thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","status":"queued","required_action":null,"last_error":null,"expires_at":null,"started_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"model":"qwen-max","instructions":"你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。","tools":[{"type":"text_to_image"},{"type":"function","function":{"name":"get_current_weather","description":"Get the current weather in a given location","parameters":{"type":"object","properties":{"location":{"type":"string","description":"The city and state, e.g. San Francisco, CA"},"unit":{"type":"string","enum":["celsius","fahrenheit"]}},"required":["location"]}}}],"file_ids":[],"metadata":{},"usage":null,"temperature":null,"top_p":null,"top_k":null,"max_tokens":null,"truncation_strategy":null,"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.run.in_progress
    data:{"id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","object":"thread.run","created_at":1714448903737,"thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","status":"in_progress","required_action":null,"last_error":null,"expires_at":null,"started_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"model":"qwen-max","instructions":"你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。","tools":[{"type":"text_to_image"},{"type":"function","function":{"name":"get_current_weather","description":"Get the current weather in a given location","parameters":{"type":"object","properties":{"location":{"type":"string","description":"The city and state, e.g. San Francisco, CA"},"unit":{"type":"string","enum":["celsius","fahrenheit"]}},"required":["location"]}}}],"file_ids":[],"metadata":{},"usage":null,"temperature":null,"top_p":null,"top_k":null,"max_tokens":null,"truncation_strategy":null,"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.run.step.created
    data:{"id":"step_d74df5eb-dcb8-4fc2-a943-d06ccda0e2e3","object":"thread.run.step","created_at":1714449008828,"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","run_id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","type":"message_creation","status":"in_progress","step_details":{"type":"message_creation","message_creation":{"message_id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360"}},"last_error":null,"expired_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"metadata":{},"usage":null,"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.run.step.in_progress
    data:{"id":"step_d74df5eb-dcb8-4fc2-a943-d06ccda0e2e3","object":"thread.run.step","created_at":1714449008828,"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","run_id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","type":"message_creation","status":"in_progress","step_details":{"type":"message_creation","message_creation":{"message_id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360"}},"last_error":null,"expired_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"metadata":{},"usage":null,"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.message.created
    data:{"id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360","object":"thread.message","created_at":1714449008828,"thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","incomplete_details":null,"completed_at":null,"incomplete_at":null,"role":"assistant","content":[],"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","run_id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","file_ids":[],"metadata":{},"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.message.in_progress
    data:{"id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360","object":"thread.message","created_at":1714449008828,"thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","incomplete_details":null,"completed_at":null,"incomplete_at":null,"role":"assistant","content":[],"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","run_id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","file_ids":[],"metadata":{},"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.message.delta
    data:{"id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"西","annotations":[]}}],"file_ids":[]},"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.message.delta
    data:{"id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"安目前的天气是","annotations":[]}}],"file_ids":[]},"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.message.delta
    data:{"id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"雨天。","annotations":[]}}],"file_ids":[]},"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.message.delta
    data:{"id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360","object":"thread.message.delta","delta":{"role":"assistant","content":[{"type":"text","text":{"value":"","annotations":[]}}],"file_ids":[]},"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.message.completed
    data:{"id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360","object":"thread.message","created_at":1714449008828,"thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","incomplete_details":null,"completed_at":1714449009919,"incomplete_at":null,"role":"assistant","content":[{"type":"text","text":{"value":"西安目前的天气是雨天。","annotations":[]}}],"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","run_id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","file_ids":[],"metadata":{},"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.run.step.completed
    data:{"id":"step_d74df5eb-dcb8-4fc2-a943-d06ccda0e2e3","object":"thread.run.step","created_at":1714449008828,"assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","run_id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","type":"message_creation","status":"completed","step_details":{"type":"message_creation","message_creation":{"message_id":"message_965b3bb5-dbe2-434a-a36e-f7cda3845360"}},"last_error":null,"expired_at":null,"cancelled_at":null,"failed_at":null,"completed_at":1714449009919,"metadata":{},"usage":null,"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:thread.run.completed
    data:{"id":"run_36d6fba9-0bf1-47f5-8e7b-f8c8140beda9","object":"thread.run","created_at":1714448903737,"thread_id":"thread_c6ac2eb0-ed95-4834-befb-ea66eba85b9f","assistant_id":"asst_5378d83a-f612-4fc1-9034-915351d67d98","status":"completed","required_action":null,"last_error":null,"expires_at":null,"started_at":null,"cancelled_at":null,"failed_at":null,"completed_at":null,"model":"qwen-max","instructions":"你是一个智能小助手，你可以根据用户的需求去调用不同的工具，进而给出回答。请酌情使用工具。","tools":[{"type":"text_to_image"},{"type":"function","function":{"name":"get_current_weather","description":"Get the current weather in a given location","parameters":{"type":"object","properties":{"location":{"type":"string","description":"The city and state, e.g. San Francisco, CA"},"unit":{"type":"string","enum":["celsius","fahrenheit"]}},"required":["location"]}}}],"file_ids":[],"metadata":{},"usage":{"input_tokens":439,"output_tokens":11,"total_tokens":450},"temperature":null,"top_p":null,"top_k":null,"max_tokens":null,"truncation_strategy":null,"request_id":"6d1573a4-3cbb-9066-99cc-7247ec17cb30"}

    event:done
    data:[DONE]
    ```

    <strong>输出参数</strong>

    与指定 ID 匹配的修改后的[运行对象](/zh/model-studio/runs#dc2479a955ip5)。
  </Tab>

  <Tab title="SDK">
    <CodeGroup dropdown>
      ```python expandable
      import json
      import sys
      from http import HTTPStatus
      from dashscope import Assistants, Messages, Runs, Steps, Threads

      def create_assistant():
          # create assistant with information
          assistant = Assistants.create(
              model='qwen-max',
              name='smart helper',
              description='A tool helper.',
              instructions='You are a helpful assistant. When asked a question, use tools wherever possible.',  # noqa E501
              tools=[{
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

          return assistant

      def verify_status_code(res):
          if res.status_code != HTTPStatus.OK:
              sys.exit(res.status_code)

      if __name__ == '__main__':
          # create assistant
          assistant = create_assistant()
          print(assistant)
          verify_status_code(assistant)

          # create thread.
          thread = Threads.create()
          print(thread)
          verify_status_code(thread)

          # create a message.
          message = Messages.create(thread.id, content='bigAdd 87787 to 788988737.')
          print(message)
          verify_status_code(message)

          # create run to run message

          message_run = Runs.create(thread.id, assistant_id=assistant.id)
          print(message_run)
          verify_status_code(message_run)

          # get run statue
          run_status = Runs.get(message_run.id, thread_id=thread.id)
          print(run_status)
          verify_status_code(run_status)

          # wait for run completed or requires_action
          run_status = Runs.wait(message_run.id, thread_id=thread.id)
          print(run_status)

          # if prompt input tool result, submit tool result.
          # should call big_add
          if run_status.required_action:
              tool_outputs = [{
                  'tool_call_id':  # optional
                  '',
                  'output':
                  '789076524'
              }]
              run = Runs.submit_tool_outputs(message_run.id,
                                             thread_id=thread.id,
                                             tool_outputs=tool_outputs)
              print(run)
              verify_status_code(run)

              # wait for run completed or requires_action
              run_status = Runs.wait(message_run.id, thread_id=thread.id)
              print(run_status)
              verify_status_code(run_status)

          run_status = Runs.get(run.id, thread_id=thread.id)
          print(run_status)
          verify_status_code(run_status)

          run_steps = Steps.list(run.id,  thread_id=thread.id)

          print(run_steps)
          verify_status_code(run_steps)

          # get the thread messages.
          msgs = Messages.list(thread.id)
          print(msgs)
          print(json.dumps(msgs, default=lambda o: o.__dict__, sort_keys=True, indent=4))
      ```

      ```java expandable
      import com.alibaba.dashscope.assistants.Assistant;
      import com.alibaba.dashscope.assistants.AssistantParam;
      import com.alibaba.dashscope.tools.ToolFunction;
      import com.alibaba.dashscope.assistants.Assistants;
      import com.alibaba.dashscope.common.GeneralListParam;
      import com.alibaba.dashscope.common.ListResult;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.AssistantThread;
      import com.alibaba.dashscope.threads.ThreadParam;
      import com.alibaba.dashscope.threads.Threads;
      import com.alibaba.dashscope.threads.messages.Messages;
      import com.alibaba.dashscope.threads.messages.TextMessageParam;
      import com.alibaba.dashscope.threads.messages.ThreadMessage;
      import com.alibaba.dashscope.threads.runs.RequiredAction;
      import com.alibaba.dashscope.threads.runs.Run;
      import com.alibaba.dashscope.threads.runs.RunParam;
      import com.alibaba.dashscope.threads.runs.Runs;
      import com.alibaba.dashscope.threads.runs.SubmitToolOutputsParam;
      import com.alibaba.dashscope.threads.runs.ToolOutput;
      import com.alibaba.dashscope.tools.FunctionDefinition;
      import com.alibaba.dashscope.tools.ToolCallBase;
      import com.alibaba.dashscope.utils.JsonUtils;
      import com.fasterxml.jackson.databind.node.ObjectNode;
      import com.github.victools.jsonschema.generator.Option;
      import com.github.victools.jsonschema.generator.OptionPreset;
      import com.github.victools.jsonschema.generator.SchemaGenerator;
      import com.github.victools.jsonschema.generator.SchemaGeneratorConfig;
      import com.github.victools.jsonschema.generator.SchemaGeneratorConfigBuilder;
      import com.github.victools.jsonschema.generator.SchemaVersion;
      import com.alibaba.dashscope.tools.ToolCallFunction;

      public class AssistantFunctionCall {
          public class AddFunctionTool {
              private int left;
              private int right;

              public AddFunctionTool(int left, int right) {
                  this.left = left;
                  this.right = right;
              }

              public int call() {
                  return left + right;
              }
          }

          static ToolFunction buildFunction() {
              SchemaGeneratorConfigBuilder configBuilder = new SchemaGeneratorConfigBuilder(SchemaVersion.DRAFT_2020_12,
                      OptionPreset.PLAIN_JSON);
              SchemaGeneratorConfig config = configBuilder.with(Option.EXTRA_OPEN_API_FORMAT_VALUES)
                      .without(Option.FLATTENED_ENUMS_FROM_TOSTRING).build();
              SchemaGenerator generator = new SchemaGenerator(config);

              // generate jsonSchema of function.
              ObjectNode jsonSchema = generator.generateSchema(AddFunctionTool.class);

              // call with tools of function call, jsonSchema.toString() is jsonschema String.
              FunctionDefinition fd = FunctionDefinition.builder().name("add").description("add two number")
                      .parameters(JsonUtils.parseString(jsonSchema.toString()).getAsJsonObject()).build();
              return ToolFunction.builder().function(fd).build();
          }
          static public Assistant createAssistant() throws ApiException, NoApiKeyException{
              AssistantParam assistantParam = AssistantParam.builder()
              .model("qwen-max") // model must be set.
              .description("a helper assistant")
              .name("system")  // name必须填写
              .instructions("You are a helpful assistant. When asked a question, use tools wherever possible.")
              .tool(buildFunction())
              .build();
              Assistants assistants = new Assistants();
              return assistants.create(assistantParam);
          }

          static public void run(String assistantId) throws ApiException, NoApiKeyException, InvalidateParameter, InputRequiredException, InterruptedException{
              // create a thread
              Threads threads = new Threads();
              AssistantThread assistantThread = threads.create(ThreadParam.builder().build());

              Runs runs = new Runs();
              // create a new message
              TextMessageParam textMessageParam = TextMessageParam.builder().role("user").content("Add 87787 to 788988737.").build();
              Messages messages = new Messages();
              ThreadMessage threadMessage = messages.create(assistantThread.getId(), textMessageParam);
              System.out.println(threadMessage);
              RunParam runParam = RunParam.builder().assistantId(assistantId).build();
              Run run = runs.create(assistantThread.getId(), runParam);
              while(true){
                  if(run.getStatus().equals(Run.Status.CANCELLED) ||
                  run.getStatus().equals(Run.Status.COMPLETED) ||
                  run.getStatus().equals(Run.Status.FAILED) ||
                  run.getStatus().equals(Run.Status.REQUIRES_ACTION)||
                  run.getStatus().equals(Run.Status.EXPIRED)){
                      break;
                  }else{
                      Thread.sleep(1000);
                  }
                  run = runs.retrieve(assistantThread.getId(), run.getId());
              }
              if(run.getStatus().equals(Run.Status.REQUIRES_ACTION)){
                  // submit action output.
                  RequiredAction requiredAction = run.getRequiredAction();
                  if(requiredAction.getType().equals("submit_tool_outputs")){
                      ToolCallBase toolCall = requiredAction.getSubmitToolOutputs().getToolCalls().get(0);
                      if (toolCall.getType().equals("function")) {
                          // get function call name and argument, both String.
                          String functionName = ((ToolCallFunction) toolCall).getFunction().getName();
                          String functionId = ((ToolCallFunction)toolCall).getId();
                          String functionArgument = ((ToolCallFunction) toolCall).getFunction().getArguments();
                          if (functionName.equals("add")) {
                            // Create the function object.
                            AddFunctionTool addFunction =
                                JsonUtils.fromJson(functionArgument, AddFunctionTool.class);
                            // call function.
                            int sum = addFunction.call();
                            System.out.println(sum);
                            SubmitToolOutputsParam submitToolOutputsParam = SubmitToolOutputsParam.builder()
                            .toolOutput(ToolOutput.builder().toolCallId(functionId).output(String.valueOf(sum)).build())
                            .build();
                            run = runs.submitToolOutputs(assistantThread.getId(), run.getId(), submitToolOutputsParam);
                          }
                        }
                  }
              }
              while(true){
                  if(run.getStatus().equals(Run.Status.CANCELLED) ||
                  run.getStatus().equals(Run.Status.COMPLETED) ||
                  run.getStatus().equals(Run.Status.FAILED) ||
                  run.getStatus().equals(Run.Status.REQUIRES_ACTION)||
                  run.getStatus().equals(Run.Status.EXPIRED)){
                      break;
                  }else{
                      Thread.sleep(1000);
                  }
                  run = runs.retrieve(assistantThread.getId(), run.getId());
              }

              GeneralListParam listParam = GeneralListParam.builder().limit(100l).build();
              ListResult<ThreadMessage> threadMessages = messages.list(assistantThread.getId(), listParam);
              for(ThreadMessage threadMessage2: threadMessages.getData()){
                  System.out.println(threadMessage2);
              }

          }

          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Assistant assistant = createAssistant();
              run(assistant.getId());
          }
      }
      ```
    </CodeGroup>
  </Tab>
</Tabs>

## 等待运行任务结束 <span id="7a111118723v7" />

等待run执行，run为终态返回，终止状态包含\['cancelled', 'failed', 'completed', 'expired', 'requires\_action']

<Tabs>
  <Tab title="SDK">
    <strong>代码示例</strong>

    <CodeGroup dropdown>
      ```python
      from dashscope import Runs
      import os

      run = Runs.wait(
          'run_id',
          # 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          thread_id='thread_id'
      )
      ```

      ```java expandable
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.runs.Run;
      import com.alibaba.dashscope.threads.runs.Runs;

      public class AssistantFunctionCall {

          public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
              Runs runs = new Runs();
              // 建议您优先配置环境变量。若没有配置环境变量，请用百炼API Key将下行替换为：apiKey("sk-xxx")
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              Run run = runs.retrieve("thread_id", "run_id", apiKey);
              while(true){
                  if(run.getStatus().equals(Run.Status.CANCELLED) ||
                  run.getStatus().equals(Run.Status.COMPLETED) ||
                  run.getStatus().equals(Run.Status.FAILED) ||
                  run.getStatus().equals(Run.Status.REQUIRES_ACTION)||
                  run.getStatus().equals(Run.Status.EXPIRED)){
                      break;
                  }else{
                      Thread.sleep(1000);
                  }
                  run = runs.retrieve("thread_id", run.getId());
              }
          }
      }
      ```
    </CodeGroup>

    <strong>输入参数</strong>

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "20.080321%" }} /><col style={{ width: "39.759036%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>run\_id</p></td><td><p>string</p></td><td><p>-</p></td><td><p>指定要取消的Run id</p></td></tr><tr><td><p>thread\_id</p></td><td><p>string</p></td><td><p>-</p></td><td><p>Run所属Thread id</p></td></tr><tr><td><p>workspace</p></td><td><p>string</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h">Workspace ID</a>，仅当 api\_key 为子业务空间 API Key 时才需要传入。</p></td></tr><tr><td><p>api\_key</p></td><td><p>string</p></td><td><p>None</p></td><td><p>阿里云百炼的<a href="/zh/model-studio/get-api-key">API Key</a>，建议您<a href="/zh/model-studio/configure-api-key-through-environment-variables">配置API Key到环境变量</a>。</p></td></tr></tbody></table>

    <strong>输出参数</strong>

    与指定 ID 匹配的修改后的[运行对象](/zh/model-studio/runs#dc2479a955ip5)。
  </Tab>
</Tabs>

## 运行对象 <span id="dc2479a955ip5" />

表示在一个线程上执行的运行。

### 对象示例 <span id="d1e1371bc0n01" />

```json expandable
{
    "assistant_id": "[REDACTED]",
    "cancelled_at": null,
    "completed_at": null,
    "created_at": 1735026246751,
    "enable_search": null,
    "expires_at": null,
    "failed_at": null,
    "file_ids": [],
    "id": "[REDACTED]",
    "instructions": "you are a helpful assistant",
    "last_error": null,
    "max_tokens": null,
    "metadata": {},
    "model": "qwen-plus",
    "object": "thread.run",
    "request_id": "[REDACTED]",
    "required_action": null,
    "started_at": null,
    "status": "in_progress",
    "status_code": 200,
    "stop": "",
    "temperature": null,
    "thread_id": "[REDACTED]",
    "tool_choice": "auto",
    "tools": [],
    "top_k": null,
    "top_p": null,
    "truncation_strategy": null,
    "usage": null
}
```

### 对象属性 <span id="07d7f73bcadq0" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "21.49%" }} /><col style={{ width: "14.21%" }} /><col style={{ width: "64.3%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>参数描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>string</p></td><td><p>运行 (Run) 的唯一标识符。</p></td></tr><tr><td><p>assistant\_id</p></td><td><p>string</p></td><td><p>与此运行关联的助手 (Assistant) 的 ID。</p></td></tr><tr><td><p>cancelled\_at</p></td><td><p>integer</p></td><td><p>运行被取消的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>completed\_at</p></td><td><p>integer</p></td><td><p>运行完成的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>created\_at</p></td><td><p>integer</p></td><td><p>运行创建的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>expires\_at</p></td><td><p>integer</p></td><td><p>运行过期的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>failed\_at</p></td><td><p>integer</p></td><td><p>运行失败的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>instructions</p></td><td><p>string</p></td><td><p>运行的指令。</p></td></tr><tr><td><p>metadata</p></td><td><p>map</p></td><td><p>一组最多包含 16 个键值对，可以附加到对象上。用于以结构化格式存储有关对象的附加信息。键的最大长度为 64 个字符，值的最大长度为 512 个字符。</p></td></tr><tr><td><p>last\_error</p></td><td><p>string</p></td><td><p>与此运行相关的最后一个错误。如果没有错误，则为null。</p></td></tr><tr><td><p>model</p></td><td><p>string</p></td><td><p>用于运行的模型名称。</p></td></tr><tr><td><p>object</p></td><td><p>string</p></td><td><p>对象类型，始终为thread.run。</p></td></tr><tr><td><p>required\_action</p></td><td><p>object</p></td><td><p>表示运行需要执行的操作，例如提交工具输出。</p></td></tr><tr><td><p>started\_at</p></td><td><p>integer</p></td><td><p>运行开始的时间戳（Unix 13位时间戳，单位：毫秒）。</p></td></tr><tr><td><p>status</p></td><td><p>string</p></td><td><p>运行的状态，可以是queued（已排队）、in\_progress（进行中）、requires\_action（需要操作）、cancelling（取消中）、cancelled（已取消）、failed（已失败）、completed（已完成）或expired（已过期）。</p></td></tr><tr><td><p>thread\_id</p></td><td><p>string</p></td><td><p>此运行所属的线程 (Thread) 的 ID。</p></td></tr><tr><td><p>tools</p></td><td><p>array</p></td><td><p>运行使用的工具列表。</p></td></tr><tr><td><p>usage</p></td><td><p>object</p></td><td><p>资源使用情况统计信息。</p></td></tr><tr><td><p>status\_code</p></td><td><p>integer</p></td><td><p>HTTP 状态码。<strong>注意：</strong>此参数主要用于内部处理，通常情况下，您无需关注此参数。</p></td></tr></tbody></table>

## 常见问题 <span id="6c48f01441nts" />

- Q：如何取消正在执行中的 Run 任务？

  A：Assistant API 尚未提供“取消 Run 任务”的接口，建议您使用其他代码逻辑作为替代。

## 错误码 <span id="97e145aeabbwf" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
