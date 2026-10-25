> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Assistant API 调用示例（下线中）

> 本文档通过示例完整说明智能体的使用。

<meta property="og:title" content="Assistant API 调用示例（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

<Tip>
  要运行下面的示例，Python SDK 需要1.18.0 以及之后版本，您可以通过pip install -U dashscope更新。java SDK 需要2.14.2以及之后版本。
</Tip>

## Assistant 简单示例 <span id="15b257f40fe1l" />

<CodeGroup dropdown>
  ```python expandable
  import json
  import sys
  from http import HTTPStatus

  from dashscope import Assistants, Messages, Runs, Threads

  def create_assistant():
      # create assistant with information
      assistant = Assistants.create(
          model='qwen-max',  # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          name='smart helper',
          description='A tool helper.',
          instructions='You are a helpful assistant.',  # noqa E501
      )

      return assistant

  def verify_status_code(res):
      if res.status_code != HTTPStatus.OK:
          print('Failed: ')
          print(res)
          sys.exit(res.status_code)

  if __name__ == '__main__':
      # create assistant
      assistant = create_assistant()
      print(assistant)
      verify_status_code(assistant)

      # create thread.
      thread = Threads.create(
          messages=[{
              'role': 'user',
              'content': '如何做出美味的牛肉炖土豆？'
          }])
      print(thread)
      verify_status_code(thread)

      # create run
      run = Runs.create(thread.id, assistant_id=assistant.id)
      print(run)
      verify_status_code(run)
      # wait for run completed or requires_action
      run_status = Runs.wait(run.id, thread_id=thread.id)
      print(run_status)

      # get the thread messages.
      msgs = Messages.list(thread.id)
      print(msgs)
      print(json.dumps(msgs, ensure_ascii=False, default=lambda o: o.__dict__, sort_keys=True, indent=4))
  ```

  ```java expandable
  import com.alibaba.dashscope.assistants.Assistant;
  import com.alibaba.dashscope.assistants.AssistantParam;
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
  import com.alibaba.dashscope.threads.runs.Run;
  import com.alibaba.dashscope.threads.runs.RunParam;
  import com.alibaba.dashscope.threads.runs.Runs;

  public class Main {
      public static void assistantUsage() throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
          Assistants assistants = new Assistants();
          // build assistant parameters
          AssistantParam param = AssistantParam.builder()
                  // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  .model("qwen-max")
                  .name("intelligent guide")
                  .description("a smart guide")
                  .instructions("You are a helpful assistant.")
                  .build();
          Assistant assistant = assistants.create(param);

          // create a thread
          Threads threads = new Threads();
          AssistantThread assistantThread = threads.create(ThreadParam.builder().build());
          // create a message to thread
          Messages messages = new Messages();
          ThreadMessage message = messages.create(assistantThread.getId(), TextMessageParam.builder().role("user").content("如何做出美味的牛肉炖土豆？").build());
          System.out.println(message);

          // create run
          Runs runs = new Runs();
          RunParam runParam = RunParam.builder().assistantId(assistant.getId()).build();
          Run run = runs.create(assistantThread.getId(), runParam);
          System.out.println(run);

          // wait for run completed
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

          ListResult<ThreadMessage> threadMessages = messages.list(assistantThread.getId(), GeneralListParam.builder().build());
          System.out.println(threadMessages);
      }

      public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
          assistantUsage();
      }
  }
  ```
</CodeGroup>

## Assistant 简单示例（流式输出） <span id="779109311a3g1" />

<CodeGroup dropdown>
  ```python expandable
  import json
  import sys
  from http import HTTPStatus

  from dashscope import Assistants, Messages, Runs, Threads

  def create_assistant():
      # create assistant with information
      assistant = Assistants.create(
      # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          model='qwen-max',
          name='smart helper',
          description='A tool helper.',
          instructions='You are a helpful assistant.',  # noqa E501
      )

      return assistant

  def verify_status_code(res):
      if res.status_code != HTTPStatus.OK:
          print('Failed: ')
          print(res)
          sys.exit(res.status_code)

  if __name__ == '__main__':
      # create assistant
      assistant = create_assistant()
      print(assistant)
      verify_status_code(assistant)

      # create thread.
      thread = Threads.create(
          messages=[{
              'role': 'user',
              'content': '如何做出美味的牛肉炖土豆？'
          }])
      print(thread)
      verify_status_code(thread)

      # create run with stream.
      run_iterator = Runs.create(thread.id,
                        assistant_id=assistant.id,
                        stream=True)
      # iterator over the event and message.
      for event, msg in run_iterator:
          print(event)
          print(msg)
      # get the thread messages.
      msgs = Messages.list(thread.id)
      print(msgs)
      print(json.dumps(msgs, ensure_ascii=False, default=lambda o: o.__dict__, sort_keys=True, indent=4))
  ```

  ```java expandable
  import com.alibaba.dashscope.assistants.Assistant;
  import com.alibaba.dashscope.assistants.AssistantParam;
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
  import com.alibaba.dashscope.threads.runs.AssistantStreamMessage;
  import com.alibaba.dashscope.threads.runs.Run;
  import com.alibaba.dashscope.threads.runs.RunParam;
  import com.alibaba.dashscope.threads.runs.Runs;
  import io.reactivex.Flowable;

  public class Main {
      public static void assistantUsage() throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
          Assistants assistants = new Assistants();
          // build assistant parameters
          AssistantParam param = AssistantParam.builder()
                  // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  .model("qwen-max")
                  .name("intelligent guide")
                  .description("a smart guide")
                  .instructions("You are a helpful assistant.")
                  .build();
          Assistant assistant = assistants.create(param);

          // create a thread
          Threads threads = new Threads();
          AssistantThread assistantThread = threads.create(ThreadParam.builder().build());
          // create a message to thread
          Messages messages = new Messages();
          ThreadMessage message = messages.create(assistantThread.getId(), TextMessageParam.builder().role("user").content("如何做出美味的牛肉炖土豆？").build());
          System.out.println(message);

          // create run
          Runs runs = new Runs();
          RunParam runParam = RunParam.builder().assistantId(assistant.getId()).stream(true).build();
          Flowable<AssistantStreamMessage> runFlowable = runs.createStream(assistantThread.getId(), runParam);
          runFlowable.blockingForEach(assistantStreamMessage->{
              System.out.println("Event: " + assistantStreamMessage.getEvent());
              System.out.println("data: ");
              System.out.println(assistantStreamMessage.getData());
          });

          ListResult<ThreadMessage> threadMessages = messages.list(assistantThread.getId(), GeneralListParam.builder().build());
          System.out.println(threadMessages);
      }

      public static void main(String[] args) throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
          assistantUsage();
      }
  }
  ```
</CodeGroup>

## 通过Assistant进行函数调用 <span id="740dedf076e7o" />

<CodeGroup dropdown>
  ```python expandable
  import json
  import sys
  from http import HTTPStatus

  from dashscope import Assistants, Messages, Runs, Steps, Threads

  def create_assistant_call_function():
      # create assistant with information
      assistant = Assistants.create(
      # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
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
          print('Failed: ')
          print(res)
          sys.exit(res.status_code)

  if __name__ == '__main__':
      # create assistant
      assistant = create_assistant_call_function()
      print(assistant)
      verify_status_code(assistant)

      # create thread.
      thread = Threads.create()
      print(thread)
      verify_status_code(thread)

      # create a message.
      message = Messages.create(thread.id, content='Add 87787 to 788988737.')
      print(message)
      verify_status_code(message)

      # create a new run to run message

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

      run_steps = Steps.list(run.id,  thread_id=thread.id)
      print(run_steps)
      verify_status_code(run_steps)

      # get the thread messages.
      msgs = Messages.list(thread.id)
      print(msgs)
      print(json.dumps(msgs, ensure_ascii=False, default=lambda o: o.__dict__, sort_keys=True, indent=4))
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

  public class Main {
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
          // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
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
                      String functionArgument = ((ToolCallFunction) toolCall).getFunction().getArguments();
                      if (functionName.equals("add")) {
                        // Create the function object.
                        AddFunctionTool addFunction =
                            JsonUtils.fromJson(functionArgument, AddFunctionTool.class);
                        // call function.
                        int sum = addFunction.call();
                        System.out.println(sum);
                        SubmitToolOutputsParam submitToolOutputsParam = SubmitToolOutputsParam.builder()
                        .toolOutput(ToolOutput.builder().toolCallId("").output(String.valueOf(sum)).build())
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

## 通过Assistant进行函数调用（流式输出） <span id="fa110f6127z9u" />

本部分Java示例中有较为完整的json schema生成示例，包含字段描述（description）信息实现。

<CodeGroup dropdown>
  ```python expandable
  # yapf: disable
  import json
  import sys
  from http import HTTPStatus

  import dashscope
  from dashscope import Assistants, Messages, Runs, Steps, Threads

  def create_assistant_call_function():
      # create assistant with information
      assistant = Assistants.create(
      # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
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
          print('Failed: ')
          print(res)
          sys.exit(res.status_code)

  if __name__ == '__main__':
      # create assistant
      assistant = create_assistant_call_function()
      print(assistant)
      verify_status_code(assistant)

      # create run
      run_iterator = Runs.create_thread_and_run(thread={'messages': [{
              'role': 'user',
              'content': 'What is transformer? Explain it in simple terms.'
          }]}, assistant_id=assistant.id, stream=True)
      thread = None
      for event, run in run_iterator:
          print(event)
          print(run)
          if event == 'thread.created':
              thread = run
      verify_status_code(run)

      run_status = Runs.get(run.id, thread_id=thread.id)
      print(run_status)
      verify_status_code(run_status)

      # list run steps
      run_steps = Steps.list(run.id, thread_id=thread.id)
      print(run_steps)
      verify_status_code(run_steps)

      # create a message.
      message = Messages.create(thread.id, content='Add 87787 to 788988737.')
      print(message)
      verify_status_code(message)

      # create a new run to run message
      responses = Runs.create(thread.id, assistant_id=assistant.id, stream=True)
      for event, message_run in responses:
          print(event)
          print(message_run)
      verify_status_code(message_run)

      # get run statue
      run_status = Runs.get(message_run.id, thread_id=thread.id)
      print(run_status)
      verify_status_code(run_status)

      # if prompt input tool result, submit tool result.
      # should call big_add
      if run_status.required_action:
          tool_outputs = [{
              'output':
              '789076524'
          }]
          # submit output with stream.
          responses = Runs.submit_tool_outputs(message_run.id,
                                               thread_id=thread.id,
                                               tool_outputs=tool_outputs,
                                               stream=True)
          for event, run in responses:
              print(event)
              print(run)
          verify_status_code(run)

      run_status = Runs.get(run.id, thread_id=thread.id)
      print(run_status)
      verify_status_code(run_status)

      run_steps = Steps.list(run.id,  thread_id=thread.id)

      print(run_steps)
      verify_status_code(run_steps)

      # get the thread messages.
      msgs = Messages.list(thread.id)
      print(msgs)
      print(json.dumps(msgs, ensure_ascii=False, default=lambda o: o.__dict__, sort_keys=True, indent=4))
  ```

  ```java expandable
  import java.lang.annotation.ElementType;
  import java.lang.annotation.Retention;
  import java.lang.annotation.RetentionPolicy;
  import java.lang.annotation.Target;
  import java.util.ArrayList;
  import java.util.List;
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
  import com.alibaba.dashscope.threads.messages.Messages;
  import com.alibaba.dashscope.threads.messages.TextMessageParam;
  import com.alibaba.dashscope.threads.messages.ThreadMessage;
  import com.alibaba.dashscope.threads.runs.AssistantStreamMessage;
  import com.alibaba.dashscope.threads.runs.RequiredAction;
  import com.alibaba.dashscope.threads.runs.Run;
  import com.alibaba.dashscope.threads.runs.RunParam;
  import com.alibaba.dashscope.threads.runs.Runs;
  import com.alibaba.dashscope.threads.runs.SubmitToolOutputsParam;
  import com.alibaba.dashscope.threads.runs.ThreadAndRunParam;
  import com.alibaba.dashscope.threads.runs.ToolOutput;
  import com.alibaba.dashscope.tools.FunctionDefinition;
  import com.alibaba.dashscope.tools.ToolCallBase;
  import com.alibaba.dashscope.tools.wanx.ToolWanX;
  import com.alibaba.dashscope.utils.JsonUtils;
  import com.fasterxml.jackson.databind.node.ObjectNode;
  import com.github.victools.jsonschema.generator.Option;
  import com.github.victools.jsonschema.generator.OptionPreset;
  import com.github.victools.jsonschema.generator.SchemaGenerator;
  import com.github.victools.jsonschema.generator.SchemaGeneratorConfig;
  import com.github.victools.jsonschema.generator.SchemaGeneratorConfigBuilder;
  import com.github.victools.jsonschema.generator.SchemaVersion;
  import com.github.victools.jsonschema.generator.TypeScope;
  import com.github.victools.jsonschema.generator.MemberScope;
  import io.reactivex.Flowable;
  import com.alibaba.dashscope.tools.ToolCallFunction;

  public class AssistantFunctionCallStream {

      @Retention(RetentionPolicy.RUNTIME)
      @Target({ElementType.FIELD, ElementType.TYPE})
      public @interface JsonDescription {
          String value();
      }

      private static String resolveJsonDescription(MemberScope<?, ?> member) {
          JsonDescription jsonDescription = member.getAnnotationConsideringFieldAndGetterIfSupported(JsonDescription.class);
          if (jsonDescription != null) {
              return jsonDescription.value();
          }
          return null;
      }
      private static String resolveDescriptionForType(TypeScope scope) {
          Class<?> rawType = scope.getType().getErasedType();
          JsonDescription jsonDescription = rawType.getAnnotation(JsonDescription.class);
          if (jsonDescription != null) {
              return jsonDescription.value();
          }
          return null;
      }

      @JsonDescription("Add two numbers.")
      public class AddFunctionTool {
          @JsonDescription("The left operator")
          private Integer left;
          @JsonDescription("The right operator")
          private Integer right;

          public AddFunctionTool(int left, int right) {
              this.left = left;
              this.right = right;
          }

          public int call() {
              return left + right;
          }
      }

      static ToolFunction buildFunction() {
          SchemaGeneratorConfigBuilder configBuilder = new SchemaGeneratorConfigBuilder(
                  SchemaVersion.DRAFT_2020_12, OptionPreset.PLAIN_JSON);
          configBuilder.forFields().withDescriptionResolver(AssistantFunctionCallStream::resolveJsonDescription);
          configBuilder.forTypesInGeneral().withDescriptionResolver(AssistantFunctionCallStream::resolveDescriptionForType);
          SchemaGeneratorConfig config = configBuilder.with(Option.EXTRA_OPEN_API_FORMAT_VALUES)
                  .without(Option.FLATTENED_ENUMS_FROM_TOSTRING)
                  .build();
          SchemaGenerator generator = new SchemaGenerator(config);

          // generate jsonSchema of function.
          ObjectNode jsonSchema = generator.generateSchema(AddFunctionTool.class);

          // call with tools of function call, jsonSchema.toString() is jsonschema String.
          FunctionDefinition fd = FunctionDefinition.builder().name("add")
                  .description("add two number")
                  .parameters(JsonUtils.parseString(jsonSchema.toString()).getAsJsonObject()).build();
          return ToolFunction.builder().function(fd).build();
      }

      static public Assistant createAssistant() throws ApiException, NoApiKeyException {
          // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          AssistantParam assistantParam = AssistantParam.builder().model("qwen-max") // model must be
                                                                                     // set.
                  .description("a helper assistant").name("system") // name必须填写
                  .instructions(
                          "You are a helpful assistant. When asked a question, use tools wherever possible.")
                  .tool(buildFunction()).tool(ToolWanX.builder().build()).build();
          Assistants assistants = new Assistants();
          return assistants.create(assistantParam);
      }

      static public void streamRun(String assistantId)
              throws ApiException, NoApiKeyException, InvalidateParameter, InputRequiredException {
          Runs runs = new Runs();
          ThreadParam threadParam = ThreadParam.builder()
                  .message(TextMessageParam.builder().role("user")
                          .content("What is transformer? Explain it in simple terms.").build())
                  .build();
          ThreadAndRunParam threadAndRunParam =
                  ThreadAndRunParam.builder().thread(threadParam).stream(true) // set stream output
                          .assistantId(assistantId).build();

          Flowable<AssistantStreamMessage> streamResponse =
                  runs.createStreamThreadAndRun(threadAndRunParam);
          final List<AssistantStreamMessage> assistantStreamMessages = new ArrayList<>();
          streamResponse.blockingForEach(assistantStreamMessage -> {
              System.out.println("Event: " + assistantStreamMessage.getEvent());
              System.out.println("data: ");
              System.out.println(assistantStreamMessage.getData());
              assistantStreamMessages.add(assistantStreamMessage);
          });
          AssistantThread thread = (AssistantThread) assistantStreamMessages.get(0).getData();
          Run run = (Run) assistantStreamMessages.get(assistantStreamMessages.size() - 1).getData();
          // retrieve run
          run = runs.retrieve(thread.getId(), run.getId());
          // list steps
          runs.listSteps(thread.getId(), run.getId(), GeneralListParam.builder().build());

          // create a new message
          TextMessageParam textMessageParam =
                  TextMessageParam.builder().role("user").content("Add 87787 to 788988737.").build();
          Messages messages = new Messages();
          ThreadMessage threadMessage = messages.create(thread.getId(), textMessageParam);
          System.out.println(threadMessage);
          RunParam runParam = RunParam.builder().assistantId(assistantId).stream(true).build();
          streamResponse = runs.createStream(thread.getId(), runParam);
          assistantStreamMessages.clear();;
          streamResponse.blockingForEach(assistantStreamMessage -> {
              System.out.println("Event: " + assistantStreamMessage.getEvent());
              System.out.println("data: ");
              System.out.println(assistantStreamMessage.getData());
              assistantStreamMessages.add(assistantStreamMessage);
          });
          run = (Run) assistantStreamMessages.get(assistantStreamMessages.size() - 1).getData();
          if (run.getStatus().equals(Run.Status.REQUIRES_ACTION)) {
              // submit action output.
              RequiredAction requiredAction = run.getRequiredAction();
              if (requiredAction.getType().equals("submit_tool_outputs")) {
                  ToolCallBase toolCall = requiredAction.getSubmitToolOutputs().getToolCalls().get(0);
                  if (toolCall.getType().equals("function")) {
                      // get function call name and argument, both String.
                      String functionName = ((ToolCallFunction) toolCall).getFunction().getName();
                      String functionArgument =
                              ((ToolCallFunction) toolCall).getFunction().getArguments();
                      if (functionName.equals("add")) {
                          // Create the function object.
                          AddFunctionTool addFunction =
                                  JsonUtils.fromJson(functionArgument, AddFunctionTool.class);
                          // call function.
                          int sum = addFunction.call();
                          System.out.println(sum);
                          SubmitToolOutputsParam submitToolOutputsParam =
                                  SubmitToolOutputsParam.builder()
                                          .toolOutput(ToolOutput.builder().toolCallId("")
                                                  .output(String.valueOf(sum)).build())
                                          .stream(true).build();
                          streamResponse = runs.submitStreamToolOutputs(thread.getId(), run.getId(),
                                  submitToolOutputsParam);
                          assistantStreamMessages.clear();
                          streamResponse.blockingForEach(assistantStreamMessage -> {
                              System.out.println("Event: " + assistantStreamMessage.getEvent());
                              System.out.println("data: ");
                              System.out.println(assistantStreamMessage.getData());
                              assistantStreamMessages.add(assistantStreamMessage);
                          });
                      }
                  }
              }
          }
          GeneralListParam listParam = GeneralListParam.builder().limit(100l).build();
          ListResult<ThreadMessage> threadMessages = messages.list(thread.getId(), listParam);
          for (ThreadMessage threadMessage2 : threadMessages.getData()) {
              System.out.println(threadMessage2);
          }

      }

      public static void main(String[] args)
              throws ApiException, NoApiKeyException, InputRequiredException, InvalidateParameter {
          Assistant assistant = createAssistant();
          streamRun(assistant.getId());
      }
  }
  ```
</CodeGroup>

## 通过Assistant调用工具 <span id="dc2dd2ce779ue" />

<Tip>
  要使用工具，您可能需要申请响应工具的权限。
</Tip>

### 支持的tools列表 <span id="7cf940d7a675s" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50.070126%" }} /><col style={{ width: "49.929874%" }} /></colgroup><thead><tr><th><p><strong>工具</strong></p></th><th><p><strong>功能</strong></p></th></tr></thead><tbody><tr><td><p>quark\_search</p></td><td><p>调用夸克搜索，提供相关结果信息。</p></td></tr><tr><td><p>text\_to\_image</p></td><td><p>调用文生图工具，根据prompt生成图片</p></td></tr><tr><td><p>code\_interpreter</p></td><td><p>代码解释器插件</p></td></tr></tbody></table>

### 夸克搜索示例 <span id="d101e2f6163ag" />

<CodeGroup dropdown>
  ```python expandable
  import json
  import sys
  from http import HTTPStatus

  import dashscope

  def create_assistant():
      # create assistant with information
      assistant = dashscope.Assistants.create(
      # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          model='qwen-max',
          name='smart helper',
          description='A tool helper.',
          instructions='You are a helpful assistant. When asked a question, use tools wherever possible.',  # noqa E501
          tools=[{
              'type': 'quark_search'
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

      # create a thread.
      thread = dashscope.Threads.create()
      print(thread)
      verify_status_code(thread)

      # create a message.
      message = dashscope.Messages.create(thread.id, content='请帮忙查询今日北京天气？')
      print(message)
      verify_status_code(message)

      # create a new run to run message
      message_run = dashscope.Runs.create(thread.id, assistant_id=assistant.id)
      print(message_run)
      verify_status_code(message_run)

      # wait for run completed
      run = dashscope.Runs.wait(message_run.id, thread_id=thread.id)
      print(run)

      run = dashscope.Runs.get(run.id, thread_id=thread.id)
      print(run)
      verify_status_code(run)

      run_steps = dashscope.Steps.list(run.id, thread_id=thread.id)

      print(run_steps)
      verify_status_code(run_steps)

      # get the thread messages.
      msgs = dashscope.Messages.list(thread.id)
      print(msgs)
      print(json.dumps(msgs, default=lambda o: o.__dict__, sort_keys=True, indent=4, ensure_ascii=False))
  ```

  ```java expandable
  import com.alibaba.dashscope.assistants.Assistant;
  import com.alibaba.dashscope.assistants.AssistantParam;
  import com.alibaba.dashscope.tools.search.ToolQuarkSearch;
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
  import com.alibaba.dashscope.threads.runs.Run;
  import com.alibaba.dashscope.threads.runs.RunParam;
  import com.alibaba.dashscope.threads.runs.Runs;

  public class Main {
      static public Assistant createAssistant() throws ApiException, NoApiKeyException{
          AssistantParam assistantParam = AssistantParam.builder()
          // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          .model("qwen-max") // model must be set.
          .description("a helper assistant")
          .name("system")  // name必须填写
          .instructions("You are a helpful assistant. When asked a question, use tools wherever possible.")
          .tool(ToolQuarkSearch.builder().build())
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
          TextMessageParam textMessageParam = TextMessageParam.builder().role("user").content("请帮忙查询今日北京天气？").build();
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

### 通过Assistant调用夸克工具（流式输出） <span id="60053fdc81u82" />

<CodeGroup dropdown>
  ```python expandable
  import json
  import sys
  from http import HTTPStatus

  import dashscope

  def create_assistant():
      # create assistant with information
      assistant = dashscope.Assistants.create(
      # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          model='qwen-max',
          name='smart helper',
          description='A tool helper.',
          instructions='You are a helpful assistant. When asked a question, use tools wherever possible.',  # noqa E501
          tools=[{
              'type': 'quark_search'
          }],
      )

      return assistant

  def verify_status_code(res):
      if res.status_code != HTTPStatus.OK:
          print('Failed: ')
          print(res)
          sys.exit(res.status_code)

  if __name__ == '__main__':
      # create assistant
      assistant = create_assistant()
      print(assistant)
      verify_status_code(assistant)

      # create a thread.
      thread = dashscope.Threads.create()
      print(thread)
      verify_status_code(thread)

      # create a message.
      message = dashscope.Messages.create(thread.id,
                                          content='今天北京天气怎么样？')
      print(message)
      verify_status_code(message)

      # create a new run to run message
      stream_iterator = dashscope.Runs.create(thread.id,
                                          assistant_id=assistant.id,
                                          stream=True)
      for event, msg in stream_iterator:
          print(event)
          print(msg)
      verify_status_code(msg)

      # get run statue
      # run_9fa03862-aa36-4e1c-b2a7-9fdd91cb9a1d
      run = dashscope.Runs.get(msg.id, thread_id=thread.id)
      print(run)
      verify_status_code(run)
      # print run status, to verify run is completed.
      print(run.status)

      run_steps = dashscope.Steps.list(run.id, thread_id=thread.id)

      print(run_steps)
      verify_status_code(run_steps)

      # get the thread messages.
      msgs = dashscope.Messages.list(thread.id)
      print(msgs)
      print(json.dumps(msgs, default=lambda o: o.__dict__, sort_keys=True, indent=4, ensure_ascii=False))
  ```

  ```java expandable
  import com.alibaba.dashscope.assistants.Assistant;
  import com.alibaba.dashscope.assistants.AssistantParam;
  import com.alibaba.dashscope.tools.search.ToolQuarkSearch;
  import io.reactivex.Flowable;
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
  import com.alibaba.dashscope.threads.runs.AssistantStreamMessage;
  import com.alibaba.dashscope.threads.runs.RunParam;
  import com.alibaba.dashscope.threads.runs.Runs;

  public class Main {
      static public Assistant createAssistant() throws ApiException, NoApiKeyException{
          AssistantParam assistantParam = AssistantParam.builder()
          // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          .model("qwen-max") // model must be set.
          .description("a helper assistant")
          .name("system")  // name必须填写
          .instructions("You are a helpful assistant. When asked a question, use tools wherever possible.")
          .tool(ToolQuarkSearch.builder().build())
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
          TextMessageParam textMessageParam = TextMessageParam.builder().role("user").content("请帮忙查询今日北京天气？").build();
          Messages messages = new Messages();
          ThreadMessage threadMessage = messages.create(assistantThread.getId(), textMessageParam);
          System.out.println(threadMessage);
          // set stream to true
          RunParam runParam = RunParam.builder().assistantId(assistantId).stream(true).build();
          Flowable<AssistantStreamMessage> runFlowable = runs.createStream(assistantThread.getId(), runParam);
          runFlowable.blockingForEach(assistantStreamMessage->{
              System.out.println("Event: " + assistantStreamMessage.getEvent());
              System.out.println("data: ");
              System.out.println(assistantStreamMessage.getData());
          });

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

### 调用文生图示例 <span id="a8e8c50dbbgjx" />

<CodeGroup dropdown>
  ```python expandable
  import json
  import sys
  from http import HTTPStatus

  import dashscope

  def create_assistant():
      # create assistant with information
      assistant = dashscope.Assistants.create(
      # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          model='qwen-max',
          name='smart helper',
          description='A tool helper.',
          instructions='You are a helpful assistant. When asked a question, use tools wherever possible.',  # noqa E501
          tools=[{
              'type': 'text_to_image'
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

      # create a thread.
      thread = dashscope.Threads.create()
      print(thread)
      verify_status_code(thread)

      # create a message.
      message = dashscope.Messages.create(thread.id, content='Draw a picture of a cute kitten lying on the sofa.')
      print(message)
      verify_status_code(message)

      # create a new run to run message
      message_run = dashscope.Runs.create(thread.id, assistant_id=assistant.id)
      print(message_run)
      verify_status_code(message_run)

      # get run statue
      run = dashscope.Runs.get(message_run.id, thread_id=thread.id)
      print(run)
      verify_status_code(run)
      # print run status, to verify run is completed.
      print(run.status)

      # wait for run completed or requires_action
      run = dashscope.Runs.wait(run.id, thread_id=thread.id)
      print(run)

      run = dashscope.Runs.get(run.id, thread_id=thread.id)
      print(run)
      verify_status_code(run)

      run_steps = dashscope.Steps.list(run.id, thread_id=thread.id)

      print(run_steps)
      verify_status_code(run_steps)

      # get the thread messages.
      msgs = dashscope.Messages.list(thread.id)
      print(msgs)
      print(json.dumps(msgs, default=lambda o: o.__dict__, sort_keys=True, indent=4, ensure_ascii=False))
  ```

  ```java expandable
  import com.alibaba.dashscope.assistants.Assistant;
  import com.alibaba.dashscope.assistants.AssistantParam;
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
  import com.alibaba.dashscope.threads.runs.Run;
  import com.alibaba.dashscope.threads.runs.RunParam;
  import com.alibaba.dashscope.threads.runs.Runs;
  import com.alibaba.dashscope.tools.T2Image.Text2Image;

  public class AssistantCallWanx {
      static public Assistant createAssistant() throws ApiException, NoApiKeyException{
          AssistantParam assistantParam = AssistantParam.builder()
          // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          .model("qwen-plus") // model must be set.
          .description("a helper assistant")
          .name("system")  // name必须填写
          .instructions("You are a helpful assistant. When asked a question, use tools wherever possible.")
          .tool(Text2Image.builder().build())
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
          TextMessageParam textMessageParam = TextMessageParam.builder().role("user").content("生成一副九寨沟风景画").build();
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

### 调用代码解释器插件 <span id="d7528bcacd0h4" />

<CodeGroup dropdown>
  ```python expandable
  import json
  import sys
  from http import HTTPStatus

  import dashscope

  def create_assistant():
      # create assistant with information
      assistant = dashscope.Assistants.create(
      # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          model='qwen-max',
          name='smart helper',
          description='A tool helper.',
          instructions='You are a helpful assistant. When asked a question, use tools wherever possible.',  # noqa E501
          tools=[{
              'type': 'code_interpreter'
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

      # create a thread.
      thread = dashscope.Threads.create()
      print(thread)
      verify_status_code(thread)

      # create a message.
      message = dashscope.Messages.create(thread.id, content='有若干只鸡兔同在一个笼子里，从上面数，有100个头，从下面数，有334只脚。请用工具计算笼中各有多少只鸡和兔?, ')
      print(message)
      verify_status_code(message)

      # create a new run to run message
      message_run = dashscope.Runs.create(thread.id, assistant_id=assistant.id)
      print(message_run)
      verify_status_code(message_run)

      # get run statue
      run = dashscope.Runs.get(message_run.id, thread_id=thread.id)
      print(run)
      verify_status_code(run)
      # print run status, to verify run is completed.
      print(run.status)

      # wait for run completed or requires_action
      run = dashscope.Runs.wait(run.id, thread_id=thread.id)
      print(run)

      run = dashscope.Runs.get(run.id, thread_id=thread.id)
      print(run)
      verify_status_code(run)

      run_steps = dashscope.Steps.list(run.id, thread_id=thread.id)

      print(run_steps)
      verify_status_code(run_steps)

      # get the thread messages.
      msgs = dashscope.Messages.list(thread.id)
      print(msgs)
      print(json.dumps(msgs, default=lambda o: o.__dict__, sort_keys=True, indent=4, ensure_ascii=False))
  ```

  ```java expandable
  import com.alibaba.dashscope.assistants.Assistant;
  import com.alibaba.dashscope.assistants.AssistantParam;
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
  import com.alibaba.dashscope.threads.runs.Run;
  import com.alibaba.dashscope.threads.runs.RunParam;
  import com.alibaba.dashscope.threads.runs.Runs;
  import com.alibaba.dashscope.tools.codeinterpretertool.ToolCodeInterpreter;

  public class AssistantCallCode {
      static public Assistant createAssistant() throws ApiException, NoApiKeyException{
          AssistantParam assistantParam = AssistantParam.builder()
          // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          .model("qwen-max") // model must be set.
          .description("a helper assistant")
          .name("system")  // name必须填写
          .instructions("You are a helpful assistant. When asked a question, use tools wherever possible.")
          .tool(ToolCodeInterpreter.builder().build())
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
          TextMessageParam textMessageParam = TextMessageParam.builder().role("user").content("有若干只鸡兔同在一个笼子里，从上面数，有100个头，从下面数，有334只脚。请用工具计算笼中各有多少只鸡和兔?, ").build();
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

### 通过 Assistant 使用 RAG 功能 <span id="b3b1566809m56" />

<CodeGroup dropdown>
  ```python expandable
  from dashscope import Assistants, Messages, Runs, Threads

  assistant = Assistants.create(
  # 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
      model='qwen-max',
      name='smart helper',
      description='一个智能助手，可以通过用户诉求，调用已有的插件能力帮助用户。',
      instructions='你是一个智能助手，请记住以下信息。${document1}',  # from rag library
      tools=[
          {
              "type": "rag",
              "prompt_ra": {
                  "pipeline_id": ["fqcioazfej","fqcioazfej"],  # 知识库 id ["9c0simc8q8", "13loysdy91"]
                  "multiknowledge_rerank_top_n":10,  # 多个知识库总共召回的片段数
                  "rerank_top_n":5, # 单个知识库召回的片段数
                  "parameters": {
                      "type": "object",
                      "properties": {
                          "query_word": {
                              "type": "str",
                              "value": "${document1}"
                          }

                      }
                  }
              }

          },

      ]
  )

  def send_message(assistant, message='百炼是什么？'):
      print(f"Query: {message}")

      # create thread.
      # create a thread.
      thread = Threads.create()

      print(thread)

      # create a message.
      message = Messages.create(thread.id, content=message)
      # create run

      run = Runs.create(thread.id, assistant_id=assistant.id)
      print(run)

      # # get run statue
      # run_status = Runs.get(run.id, thread_id=thread.id)
      # print(run_status)

      # wait for run completed or requires_action
      run_status = Runs.wait(run.id, thread_id=thread.id)
      # print(run_status)

      # if prompt input tool result, submit tool result.

      run_status = Runs.get(run.id, thread_id=thread.id)
      print(run_status)
      # verify_status_code(run_status)

      # get the thread messages.
      msgs = Messages.list(thread.id)
      # print(msgs)
      # print(json.dumps(msgs, default=lambda o: o.__dict__, sort_keys=True, indent=4))

      print("运行结果:")
      for message in msgs['data'][::-1]:
          print("content: ", message['content'][0]['text']['value'])
      print("\n")

  if __name__ == "__main__":
      send_message(assistant, message='百炼是什么?')
  ```

  ```java expandable
  package com.modelstudio;
  import com.alibaba.dashscope.assistants.Assistant;
  import com.alibaba.dashscope.assistants.AssistantParam;
  import com.alibaba.dashscope.assistants.Assistants;
  import com.alibaba.dashscope.common.GeneralListParam;
  import com.alibaba.dashscope.common.ListResult;
  import com.alibaba.dashscope.exception.ApiException;
  import com.alibaba.dashscope.exception.InputRequiredException;
  import com.alibaba.dashscope.exception.InvalidateParameter;
  import com.alibaba.dashscope.exception.NoApiKeyException;
  import com.alibaba.dashscope.threads.AssistantThread;
  import com.alibaba.dashscope.threads.ContentText;
  import com.alibaba.dashscope.threads.ThreadParam;
  import com.alibaba.dashscope.threads.Threads;
  import com.alibaba.dashscope.threads.messages.Messages;
  import com.alibaba.dashscope.threads.messages.TextMessageParam;
  import com.alibaba.dashscope.threads.messages.ThreadMessage;
  import com.alibaba.dashscope.threads.runs.Run;
  import com.alibaba.dashscope.threads.runs.RunParam;
  import com.alibaba.dashscope.threads.runs.Runs;
  import com.alibaba.dashscope.tools.ToolBase;
  import com.google.gson.JsonObject;
  import com.google.gson.annotations.SerializedName;
  import lombok.Builder;
  import lombok.Data;
  import lombok.EqualsAndHashCode;
  import lombok.experimental.SuperBuilder;
  import java.lang.System;

  public class AssistantApiRag {
      public static Assistant createAssistant(String pipelineId) throws ApiException, NoApiKeyException {
          //注意instructions的${document1}占位符和buildPromptRa的${document1}占位符必须保持一致
          AssistantParam assistantParam = AssistantParam.builder()
                  .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                  // 此处以qwen-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  .model("qwen-max")
                  .name("smart helper")
                  .description("智能助手，支持知识库查询和插件调用。")
                  .instructions("你是一个智能助手，请记住以下信息。${document1}")
                  .tool(ToolRag.create(ToolRag.buildPromptRa("${document1}", pipelineId)))
                  .build();
          Assistants assistants = new Assistants();
          return assistants.create(assistantParam);
      }

      public static void sendMessage(Assistant assistant, String message) throws NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
          Threads threads = new Threads();
          AssistantThread assistantThread = threads.create(ThreadParam.builder().build());

          Runs runs = new Runs();
          // create a new message
          TextMessageParam textMessageParam = TextMessageParam.builder()
                  .role("user")
                  .content(message)
                  .build();
          Messages messages = new Messages();
          ThreadMessage threadMessage = messages.create(assistantThread.getId(), textMessageParam);
          System.out.println(threadMessage);

          RunParam runParam = RunParam.builder().assistantId(assistant.getId()).build();
          Run run = runs.create(assistantThread.getId(), runParam);
          while (true) {
              if (run.getStatus().equals(Run.Status.CANCELLED) ||
                      run.getStatus().equals(Run.Status.COMPLETED) ||
                      run.getStatus().equals(Run.Status.FAILED) ||
                      run.getStatus().equals(Run.Status.REQUIRES_ACTION) ||
                      run.getStatus().equals(Run.Status.EXPIRED)) {
                  break;
              } else {
                  Thread.sleep(1000);
              }
              run = runs.retrieve(assistantThread.getId(), run.getId());
          }

          System.out.println(run);

          GeneralListParam listParam = GeneralListParam.builder().limit(100L).build();
          ListResult<ThreadMessage> threadMessages = messages.list(assistantThread.getId(), listParam);
          for (ThreadMessage threadMessage2 : threadMessages.getData()) {
              System.out.printf("content: %s\n", ((ContentText) threadMessage2.getContent().get(0)).getText().getValue());
          }
      }

      public static void main(String[] args) throws NoApiKeyException, InputRequiredException, InvalidateParameter, InterruptedException {
          // 此处需填写知识库 ID
          String pipelineId = "YOUR_PIPELINE_ID";
          Assistant assistant = createAssistant(pipelineId);
          sendMessage(assistant, "你好");
      }
  }

  @Data
  @EqualsAndHashCode(callSuper = true)
  class ToolRag extends ToolBase {
      static {
          registerTool("rag", ToolRag.class);
      }

      private String type = "rag";
      @SerializedName("prompt_ra")
      private JsonObject promptRa;

      private ToolRag() {
          super(null);
      }

      public void setPromptRa(JsonObject promptRa) {
          this.promptRa = promptRa;
      }

      public static ToolRag create(JsonObject promptRa) {
          ToolRag tool = new ToolRag();
          tool.setPromptRa(promptRa);
          return tool;
      }

      @Override
      public String getType() {
          return type;
      }

      public static JsonObject buildPromptRa(String placeholder, String pipelineId) {
          JsonObject queryWord = new JsonObject();
          queryWord.addProperty("type", "str");
          queryWord.addProperty("value", placeholder);

          JsonObject properties = new JsonObject();
          properties.add("query_word", queryWord);

          JsonObject parameters = new JsonObject();
          parameters.addProperty("type", "object");
          parameters.add("properties", properties);

          JsonObject jsonObject = new JsonObject();
          jsonObject.addProperty("pipeline_id", pipelineId);
          jsonObject.add("parameters", parameters);

          return jsonObject;
      }
  }
  ```
</CodeGroup>
