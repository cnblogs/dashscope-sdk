> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 了解 Assistant API 的流式输出功能（下线中）

> 流式输出功能可以让您实时获取 Assistant 的运行状态，以便向用户逐字展示大模型生成的内容。

<meta property="og:title" content="了解 Assistant API 的流式输出功能（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

流式输出功能具有以下特点：

- <strong>更自然的对话节奏</strong>：能够模拟人类交流的节奏，让交互过程更加流畅自然。
- <strong>更精细的控制机制</strong>：实时监测 Assistant 运行状态，及时展示生成进度，或在发现异常时及时终止。
- <strong>更可靠的连接保障</strong>：响应分块逐步返回，避免客户端等待时间过长。

## 快速开始 <span id="be4b9c55f5clc" />

如需启用流式输出，您只需使用`Run.create(stream=True)`运行 Assistant。

请确认您已[配置 API Key 到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)，并[安装 DashScope SDK](/zh/model-studio/install-sdk)。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>非流式输出</strong>

        ```python expandable
        # 非流式输出
        from dashscope import Assistants, Threads, Runs, Messages

        # 创建Assistant
        assistant = Assistants.create(
                model='qwen-plus',  # 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                name='示例Assistant',
                instructions='You are a helpful assistant'
            )

        # 创建对话线程
        thread = Threads.create(assistant_id=assistant.id,
                                messages=[{
                                    'role': 'user',
                                    'content': '你好！'
                                }])

        # 执行对话并等待响应
        run = Runs.create(thread.id,
                          assistant_id=assistant.id)
        Runs.wait(run.id,
                  thread_id=thread.id)

        # 获取并打印结果
        message = Messages.list(thread.id).data[0].content[0].text.value
        print(message)
        ```
      </td>

      <td>
        <strong>流式输出</strong>

        ```python expandable
        # 流式输出
        from dashscope import Assistants, Threads, Runs

        # 创建Assistant
        assistant = Assistants.create(
                model='qwen-plus',  # 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                name='示例Assistant',
                instructions='You are a helpful assistant'
            )

        # 创建对话线程
        thread = Threads.create(assistant_id=assistant.id,
                                messages=[{
                                    'role': 'user',
                                    'content': '你好！'
                                }])

        # 执行流式对话
        run = Runs.create(thread.id,
                          assistant_id=assistant.id,
                          stream=True)  # 启用流式输出的关键配置

        # 实时输出结果
        for event, data in run:
            if event == 'thread.message.delta':
                print(data.delta.content.text.value, end='', flush=True)
        ```
      </td>
    </tr>
  </tbody>
</table>

## 配置方法 <span id="db8b795f96t7g" />

在 Assistant API 中，为了呈现流式输出效果，需要完成两个步骤：

1. 流式运行 Assistant
2. 接收流式数据

如果将流式输出比作“水流”，那么运行 Assistant 就像是“打开水龙头”，而接收数据就像是“接收水流”。

### 流式运行 Assistant <span id="361869d996bv4" />

就像打开水龙头，您只需在创建运行时，将`stream`参数设置为`True`，即可流式运行 Assistant。

```python
run = Runs.create(thread_id=thread.id,
                  assistant_id=assistant.id,
                  stream=True)  # 只需配置stream=True，即可流式运行 Assistant
```

### 接收流式数据 <span id="a97c897a4a2ds" />

在流式运行中，Assistant 产生的数据流主要分为两类：

- <strong>状态信息流</strong>：用于追踪运行状态和进度
- <strong>对话消息流</strong>：包含实际的输出内容

就像接住源源不断的水流，您需要构建一个循环来处理数据，并设置一些规则来区分不同类型的数据。作为开发者，您通常只需关注对话消息流，除非需要实现更复杂的状态管理。详细的事件和事件数据列表，请参考[Assistant API 流式输出](/zh/model-studio/event-streaming)开发参考。

```python
for event, data in run_iterator:
    if event == 'thread.run.created':  # 【状态信息流】如果运行被创建，那么展示运行的当前状态
        print(data.status)
    if event == 'thread.message.delta':  # 【对话消息流】如果产生消息片段，那么立即展示这个片段
        print(data.delta.content.text.value, end='', flush=True)
```

#### 对话消息流 <span id="74311bffacdwj" />

在 Assistant API 中，Assistant 会产生两类对话消息流：

- <strong>文本消息流</strong>：在 Assistant 中，由大模型生成的文本消息流。

```python
for event, data in run_iterator:
    if event == 'thread.message.delta':  # 【对话消息流】消息增量对象，表示有新生成的文本消息片段
        print(data.delta.content.text.value, end='', flush=True)  # 输出这个片段
```

- <strong>工具消息流</strong>：在 Assistant 中，由工具调用返回的工具消息流。以代码解释器为例：

```python
for event, data in run_iterator:
    if event == 'thread.run.step.delta':  # 【对话消息流】运行步骤增量对象，表示有新的工具调用
        tool_call = data.delta.step_details.tool_calls[0]
        if getattr(tool_call, 'type', '') == 'code_interpreter':  # 以代码解释器为例
            print(getattr(tool_call.code_interpreter, 'arguments', ''), end='', flush=True)  # 输出将要执行的代码
            print(getattr(tool_call.code_interpreter, 'output', ''), end='', flush=True)  # 输出代码执行结果
```

代码解释器，夸克搜索，文生图和计算器支持流式输出。其他 Assistant API 工具不支持流式输出。详情请参阅[运行步骤增量对象](/zh/model-studio/event-streaming#427f330f2c8yr)。

至此，您已经了解了构建流式输出的基本方法。接下来您将通过一个案例，构建带有工具调用的流式输出。

## 示例：代码教学助手（流式输出） <span id="6993da8a9efbg" />

在这个简单的案例中，Assistant 配置了代码解释器工具，实现了文本消息、代码编写和代码执行的流式输出效果。如果您希望了解更多代码解释器的配置方法，请参考[代码解释器-功能特性](/zh/model-studio/code-interpreter)。

请确认您已[配置 API Key 到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)，并[安装 DashScope SDK](/zh/model-studio/install-sdk)。

```python expandable

from dashscope import Assistants, Threads, Runs
import time

class CodeTutorStream:
    """代码教学助手流式输出演示类"""

    def __init__(self):
        self.assistant = None
        self.thread = None

    def create_assistant(self):
        """创建配置代码解释器的教学助手"""
        self.assistant = Assistants.create(
            model='qwen-plus',
            name="Python教学助手",
            instructions="你是一个耐心的代码教学助手，请用流式输出逐步解释代码",
            tools=[{'type': 'code_interpreter'}]
        )
        print("教学助手初始化完成（已启用代码解释器）\n")

    def start_lesson(self, question):
        """创建教学线程并启动流式对话"""
        self.thread = Threads.create(messages=[{
            'role': 'user',
            'content': question
        }])

        print("学生提问:", question)
        print("\n助手思考中...\n")

        # 延迟1秒模拟处理时间
        time.sleep(1)

        # 启动流式运行
        run_stream = Runs.create(
            self.thread.id,
            assistant_id=self.assistant.id,
            stream=True
        )
        return run_stream

    def process_stream(self, stream):
        message_flag = False
        tool_call_flag = False
        """处理流式输出并模拟教学场景"""
        try:
            for event, data in stream:
                # 处理文本解释
                if event == 'thread.message.delta':
                    if not message_flag:
                        print(f"\n步骤讲解:")
                        message_flag = True
                    text_chunk = data.delta.content.text.value
                    self.simulate_typing(text_chunk)

                # 处理代码演示
                if event == 'thread.run.step.delta':
                    if not tool_call_flag:
                        print(f"\n代码演示:")
                        tool_call_flag = True
                    tool_call = data.delta.step_details.tool_calls[0]
                    if getattr(tool_call, 'type', '') == 'code_interpreter':
                        code = getattr(tool_call.code_interpreter, 'arguments', '')
                        output = getattr(tool_call.code_interpreter, 'output', '')

                        self.simulate_typing(code, speed=0.03)
                        self.simulate_typing(output)

        except KeyboardInterrupt:
            stream.close()
            print("\n教学中断")

    @staticmethod
    def simulate_typing(text, speed=0.03):
        """模拟打字机效果输出"""
        for char in text:
            print(char, end='', flush=True)
            time.sleep(speed)

if __name__ == "__main__":
    tutor = CodeTutorStream()

    # 创建教学助手
    tutor.create_assistant()

    # 设置教学问题
    question = """请详细讲解如何用Python绘制正弦函数图像：
1. 分步骤解释数学原理
2. 演示numpy和matplotlib的用法
3. 展示最终可视化效果"""

    # 启动流式教学
    stream = tutor.start_lesson(question)

    # 处理实时输出
    tutor.process_stream(stream)
```

## 常见问题 <span id="90258331eegex" />

在配置流式输出时，如果您遇到代码执行错误，可参阅[错误码](/zh/model-studio/error-code)排查错误类型。
