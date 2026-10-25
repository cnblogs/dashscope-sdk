> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Assistant API 代码解释器

> 大模型不擅长数学运算、数据可视化等精确计算任务，您可以使用 Assistant API 预置的 代码解释器 插件，使智能体编写和运行 Python 程序，从而逐步解决复杂的数据问题。如果智能体编写的代码未能成功运行，它还会不断调整代码，尝试不同的方法，直到代码能够顺利执行。

<meta property="og:title" content="Assistant API 代码解释器" />

## 快速入门 <span id="7f6fd1cec9o9n" />

### 工作原理 <span id="220ef05fb3b18" />

代码解释器的工作原理如下图所示，您可以直观地了解代码解释器如何作为 Assistant API 的工具，与其他组件协同工作，完成代码的生成、执行和结果处理。

如图所示，代码解释器将按照以下步骤完成任务：

1. 用户通过 Assistant API 提供输入（如代码或问题）。
2. 代码解释器插件接收输入，生成 Python 代码并执行。
3. 执行结果通过 Assistant API 返回给用户。

<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8136305371/p885035.png" alt="code_interpreter_workflow" width={700} height={437.5} />

### 快速配置 <span id="c0559f054bj8z" />

对于熟悉 Assistant API 的开发者，可以按照下列配置方法，快速添加代码解释器到智能体应用。

1. 在创建 Assistant 对象时，添加`code_interpreter`工具，这是启用代码解释器的关键配置。

```python
# 快速启用代码解释器
assistant = dashscope.Assistants.create(
    **kwargs, # **kwargs 用于接收其他可能的配置参数，例如 智能体名称、模型名称、系统指令等
    tools=[{
        'type': 'code_interpreter'  # 这是启用代码解释器的关键配置
    }]
)
```

2. 在获取大模型应用的回复时，您可以通过`dashscope.Steps.list`方法获取运行步骤，并提取代码执行的内容和结果。

```python
# 处理代码执行结果
steps = dashscope.Steps.list(thread_id=thread.id, run_id=run.id)

# 处理代码内容和执行结果
for step in steps:
    if step.step_details.type == 'tool_calls':
        for call in step.step_details.tool_calls:
            if call.type == 'code_interpreter':
                # 输出代码和执行结果
                print("代码:", call.code_interpreter.arguments)
                print("结果:", call.code_interpreter.output)
```

通过以上两步，您可以快速启用代码解释器插件，并处理代码执行的结果。如果需要更详细的功能说明，请参考后续章节。

## 应用场景 <span id="7d2367f8e4zrv" />

代码解释器作为 Assistant API 的预置插件，能通过执行大模型生成的 Python 代码，解决多种实际应用场景中的问题：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "49.456522%" }} /><col style={{ width: "50.543478%" }} /></colgroup><tbody><tr><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8263680371/p863150.png" alt="image" className="inline" noZoom width={20} height={18.414} /><strong>格式转换</strong></p><p>支持在常见数据格式间进行转换</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8263680371/p863149.png" alt="image" className="inline" noZoom width={20} height={20.584} /><strong>图像绘制</strong></p><p>借助 matplotlib 等库实现丰富的可视化效果</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>JSON 与 CSV 互转，方便数据导入导出</p><p>XML 解析与生成，适用于配置文件处理</p><p>Excel 数据提取与重组，简化表格处理流程</p></td><td style={{ verticalAlign: "top" }}><p>绘制折线图展示数据趋势和波动</p><p>通过散点图分析变量相关性</p><p>使用热力图呈现复杂的多维数据关系</p></td></tr><tr><td style={{ verticalAlign: "top" }}><div><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8263680371/p863129.png" alt="image" width={600} height={725.422} /></p></div></td><td style={{ verticalAlign: "top" }}><div><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8263680371/p863133.png" alt="image" width={600} height={1013.759} /></p></div></td></tr></tbody></table>

## 典型案例：代码教学助手 <span id="4d22648bd1o6y" />

在这个示例中，您将使用 Assistant API 和 Gradio 创建 Web 代码教学助手，帮助用户编辑并执行代码。

### 环境准备 <span id="7a4d7882802hr" />

- <strong>Python 环境</strong>：在开始之前，请确保您的开发环境满足以下条件：

  - 推荐使用 Python 3.10 或更高版本。您可以通过以下命令查看您的 Python 版本：

    `python3 --version`
  - Pip 版本是最新的。您可以通过以下命令升级 Pip 到最新版本：

    `pip install --upgrade pip`
  - 已安装 DashScope 和 Gradio 软件包。您可以通过以下命令安装软件包：

    `pip install dashscope==1.20.11 gradio==5.1.0`
- <strong>阿里云百炼 API-KEY</strong>：在 Assistant API 的开发场景中，您需要[获取](/zh/model-studio/get-api-key)并[配置](/zh/model-studio/configure-api-key-through-environment-variables)阿里云百炼 API-KEY。

接下来，您将了解开发示例场景的重要方法。建议先浏览并运行以下完整代码，有助于您了解整个示例的开发方法。代码运行后，您可以通过[http://127.0.0.1:7860/](http://127.0.0.1:7860/)访问 Web 代码教学助手。

<Accordion title="完整代码">
  ```python expandable
  """
  代码解释器教学示例

  这个示例展示了如何使用 dashscope 的 Assistant API 创建一个具有代码执行能力的AI智能体。
  整个过程分为五个主要步骤：
  1. 创建智能体 (Assistant) - 配置AI智能体的功能和行为
  2. 创建对话线程 (Thread) - 管理一次完整的对话会话
  3. 发送消息 (Message) - 在对话线程中添加用户输入
  4. 运行智能体 (Run) - 处理用户输入并生成响应
  5. 处理步骤 (Steps) - 跟踪并处理智能体的执行过程
  """

  import dashscope
  import gradio as gr
  import logging
  import asyncio

  # 配置日志以跟踪API调用和执行过程
  logging.basicConfig(level=logging.INFO)

  class CodeTeachingAssistant:
      """
      代码教学助手类

      工作流程：
      1. 初始化时创建助手和对话线程
      2. 接收用户消息后，将其添加到对话线程
      3. 创建运行实例处理用户输入
      4. 跟踪运行过程中的每个步骤
      5. 返回处理结果
      """
      def __init__(self):
          # 第一步：创建Assistant实例
          # - model: 选择支持代码解释器的模型
          # - tools: 启用代码解释器功能
          # - instructions: 定义Assistant的行为准则
          self.assistant = dashscope.Assistants.create(
              model='qwen-plus',  # 使用支持代码执行的千问模型
              name='编程教学助手',
              instructions='''
                  你是一个编程教师，负责:
                  1. 解释代码原理
                  2. 提供代码示例
                  3. 执行代码并分析结果
                  请用通俗易懂的方式教学。
              ''',
              tools=[{'type': 'code_interpreter'}]  # 启用代码解释器
          )

          # 第二步：创建对话线程
          # Thread用于维护一次完整的对话上下文
          self.thread = dashscope.Threads.create()

      async def chat(self, message: str):
          """
          处理用户消息并返回助手回复

          工作流程：
          1. 将用户消息添加到对话线程
          2. 创建运行实例处理消息
          3. 跟踪运行过程中的每个步骤
          4. 逐步返回处理结果
          """
          # 第三步：发送用户消息
          # 将用户输入添加到对话线程中
          dashscope.Messages.create(
              thread_id=self.thread.id,
              role="user",
              content=message
          )

          # 第四步：创建运行实例
          # Run 负责处理用户输入并生成响应
          run = dashscope.Runs.create(
              thread_id=self.thread.id,
              assistant_id=self.assistant.id
          )

          # 用于跟踪已处理的步骤，避免重复处理
          # processed_steps 是一个集合，用于存储已经处理过的步骤 ID。
          # 每个步骤在 Assistant 的执行过程中都会生成一个唯一的 ID。
          # 通过记录这些 ID，可以确保每个步骤只被处理一次，避免重复输出。
          processed_steps = set()

          # 第五步：处理运行步骤
          while True:
              # 获取运行状态
              run_status = dashscope.Runs.retrieve(
                  thread_id=self.thread.id,
                  run_id=run.id
              )

              # 获取执行步骤列表
              # Steps 包含了Assistant执行过程中的所有操作
              steps = dashscope.Steps.list(
                  thread_id=self.thread.id,
                  run_id=run.id
              )

              # 处理新的步骤
              if hasattr(steps, 'data'):
                  for step in steps.data:
                      # 跳过已处理的步骤
                      if step.id not in processed_steps:
                          processed_steps.add(step.id)

                          # 处理消息创建步骤
                          # 这类步骤包含Assistant的文本响应
                          if step.step_details.type == 'message_creation':
                              message_id = step.step_details.message_creation.message_id
                              message = dashscope.Messages.retrieve(
                                  thread_id=self.thread.id,
                                  message_id=message_id
                              )
                              if hasattr(message, 'content'):
                                  yield message.content[0].text.value + "\n"

                          # 处理代码执行步骤
                          # 这类步骤包含代码执行的过程和结果
                          elif step.step_details.type == 'tool_calls':
                              for tool_call in step.step_details.tool_calls:
                                  if tool_call.type == 'code_interpreter':
                                      # 显示执行的代码
                                      yield f"\n执行代码:\n{tool_call.code_interpreter.arguments}\n"
                                      # 显示执行结果
                                      yield f"\n执行结果:\n{tool_call.code_interpreter.output}\n"

              # 检查运行状态
              if run_status.status == 'completed':
                  break  # 处理完成
              elif run_status.status == 'failed':
                  yield "处理失败，请重试"
                  break  # 处理失败

              # 等待新的步骤
              await asyncio.sleep(1)

  def create_web_ui():
      """
      创建Web交互界面

      使用Gradio构建简单的聊天界面，包括：
      - 对话历史显示
      - 消息输入框
      - 示例问题
      """
      assistant = CodeTeachingAssistant()

      css = """
          .contain { display: flex; flex-direction: column; }
          .gradio-container { height: 100vh !important; }
          #component-0 { height: 100%; }
          #chatbot { flex-grow: 1; overflow: auto;}
      """

      with gr.Blocks(css=css) as demo:
          chatbot = gr.Chatbot(elem_id="chatbot", label="编程学习")
          msg = gr.Textbox(label="输入问题或代码")

          async def respond(message, history):
              history.append((message, ""))  # 立即添加用户消息
              yield "", history  # 清空输入框并显示用户消息

              # 创建一个新的响应条目
              response = ""
              async for chunk in assistant.chat(message):
                  response += chunk
                  # 实时更新最后一条消息
                  history[-1] = (message, response)
                  yield "", history  # 保持输入框为空，更新对话历史

          msg.submit(respond, [msg, chatbot], [msg, chatbot])

          # 添加示例问题
          gr.Examples([
              "请用Python写一个计算斐波那契数列的函数，并画出前20个数的趋势图",
              "帮我写一个冒泡排序算法，并用随机数据测试它的效果",
              "请用matplotlib画一个简单的正弦波图形"
          ], inputs=msg)

      return demo

  if __name__ == "__main__":
      demo = create_web_ui()
      demo.launch()
  ```
</Accordion>

### 创建智能体与线程 <span id="ea52132a87ntb" />

在 Assistant API 中，我们需要两个核心组件来启动对话系统：

- <strong>智能体（Assistant）</strong>

  智能体是对话系统的大脑，负责理解和处理用户输入。创建智能体时需要配置：

  - `model`：选择支持代码执行的模型（如 qwen-plus）
  - `name`：为智能体命名
  - `instructions`：定义智能体的行为准则
  - `tools`：启用代码解释器等工具
- <strong>线程（Thread）</strong>

  线程用于管理完整的对话上下文，确保对话的连贯性。每个用户会话对应一个独立的线程。

这两个组件在 `CodeTeachingAssistant` 类的初始化方法中配置：

```python expandable
class CodeTeachingAssistant:
    """
    代码教学助手接口
    主要职责：
    1. 初始化时创建助手和对话线程
    2. 接收用户消息后，将其添加到对话线程
    3. 创建运行实例处理用户输入
    4. 跟踪运行过程中的每个步骤
    5. 返回处理结果
    """
    def __init__(self):
        # 第一步：创建Assistant实例
        # - model: 选择支持代码解释器的模型
        # - tools: 启用代码解释器功能
        # - instructions: 定义Assistant的行为准则
        self.assistant = dashscope.Assistants.create(
            model='qwen-plus',  # 使用支持代码执行的千问模型
            name='编程教学助手',
            instructions='''
                你是一个编程教师，负责:
                1. 解释代码原理
                2. 提供代码示例
                3. 执行代码并分析结果
                请用通俗易懂的方式教学。
            ''',
            tools=[{'type': 'code_interpreter'}]  # 启用代码解释器
        )
        # 第二步：创建对话线程
        # Thread用于维护一次完整的对话上下文
        self.thread = dashscope.Threads.create()
```

创建这些组件后，它们可以在整个会话过程中重复使用，无需重新初始化。智能体将根据配置的指令和工具来处理用户的每一个请求，而线程则会保持对话的上下文连续性。

### 处理对话消息 <span id="f566adb791bbv" />

在 Assistant API 中，处理用户消息是一个异步过程，需要经过多个步骤来完成完整的对话交互。`chat`方法实现了这个处理流程：

1. <strong>发送用户消息</strong>

   首先，我们需要将用户的输入添加到对话线程中：

```python
class CodeTeachingAssistant:
    async def chat(self, message: str):
        """
        处理用户消息并返回助手回复

        工作流程：
        1. 将用户消息添加到对话线程
        2. 创建运行实例处理消息
        3. 跟踪运行过程中的每个步骤
        4. 逐步返回处理结果
        """
        # 第三步：发送用户消息
        # 将用户输入添加到对话线程中
        dashscope.Messages.create(
            thread_id=self.thread.id,
            role="user",
            content=message
        )
```

2. <strong>创建运行实例</strong>

   创建一个运行实例来处理这条消息：

```python
async def chat(self, message: str):
        """
        处理用户消息并返回助手回复

        工作流程：
        1. 将用户消息添加到对话线程
        2. 创建运行实例处理消息
        3. 跟踪运行过程中的每个步骤
        4. 逐步返回处理结果
        """
        # 第四步：创建运行实例
        # Run 负责处理用户输入并生成响应
        run = dashscope.Runs.create(
            thread_id=self.thread.id,
            assistant_id=self.assistant.id
        )
```

3. <strong>步骤追踪与处理</strong>

   系统会将智能体的运行过程分解为两种主要步骤：

   - <strong>消息创建步骤</strong>：当智能体生成文本响应时

```python
async def chat(self, message: str):
        # 这类步骤包含Assistant的文本响应
        if step.step_details.type == 'message_creation':
            message_id = step.step_details.message_creation.message_id
            message = dashscope.Messages.retrieve(
                thread_id=self.thread.id,
                message_id=message_id
            )
            if hasattr(message, 'content'):
                yield message.content[0].text.value + "\n"
```

- <strong>工具调用步骤</strong>：当智能体执行代码时

```python
async def chat(self, message: str):
        # 处理代码执行步骤
        # 这类步骤包含代码执行的过程和结果
        elif step.step_details.type == 'tool_calls':
            for tool_call in step.step_details.tool_calls:
                if tool_call.type == 'code_interpreter':
                    # 显示执行的代码
                    yield f"\n执行代码:\n{tool_call.code_interpreter.arguments}\n"
                    # 显示执行结果
                    yield f"\n执行结果:\n{tool_call.code_interpreter.output}\n"
```

4. <strong>状态监控</strong>

   系统会持续监控运行状态，直到处理完成或失败：

   - `completed`：处理成功完成
   - `failed`：处理失败，需要重试

```python
async def chat(self, message: str):
        # 检查运行状态
        if run_status.status == 'completed':
            break  # 处理完成
        elif run_status.status == 'failed':
            yield "处理失败，请重试"
            break  # 处理失败

        # 等待新的步骤
        await asyncio.sleep(1)
```

<Note>
  - 所有API调用都是异步的，需要适当处理等待时间
  - 步骤处理是顺序进行的，确保消息按正确顺序显示
  - 错误处理机制确保在处理失败时能够正常退出
</Note>

### 创建Web交互界面（可选） <span id="25a02e858cqoo" />

本示例使用 Gradio 框架构建 Web 界面。Gradio 是一个用于构建机器学习演示的 Python 库，它可以：

- 快速创建交互式 Web 界面
- 自动处理异步操作和实时更新
- 提供内置的聊天界面组件

下面是使用 Gradio 构建代码教学助手界面的示例：

```python
def create_web_ui():
    assistant = CodeTeachingAssistant()

    with gr.Blocks() as demo:
        chatbot = gr.Chatbot(label="编程学习")
        msg = gr.Textbox(label="输入问题或代码")

        # 添加示例问题
        gr.Examples([
            "请用Python写一个计算斐波那契数列的函数，并画出前20个数的趋势图",
            "帮我写一个冒泡排序算法，并用随机数据测试它的效果",
            "请用matplotlib画一个简单的正弦波图形"
        ], inputs=msg)

        return demo

if __name__ == "__main__":
    demo = create_web_ui()
    demo.launch()  # 启动后访问 http://127.0.0.1:7860/
```

该界面提供了基本的对话功能和一些示例问题，可以帮助您快速开始使用代码教学助手。

## 总结 <span id="dd24f5819ea73" />

通过这个示例，您已经了解了如何使用代码解释器来创建一个教学智能体。代码解释器可以帮助用户学习编程概念并执行代码。您可以根据自己的需求，进一步扩展和定制这个智能体，以满足不同的教学和学习场景。

关于 Assistant API，您还可以了解：

- [工具调用-知识检索增强](/zh/model-studio/retrieval-augmented-generation)：让智能体能够根据您的需求获取外部知识。
- [工具调用-函数调用](/zh/model-studio/function-calling)：让智能体能够根据您的需求自动调用外部函数。

## 常见问题 <span id="6dda1db5d4e8i" />

1. <strong>使用代码解释器会收取额外费用吗？</strong>

   目前，阿里云百炼预置的代码解释器限时免费。在本文的示例中，智能体运行时产生的所有“code\_interpreter”步骤暂不计费。
2. <strong>代码解释器支持哪些 Python 软件包？</strong>

   Python 软件包的支持情况可能发生变化，您可以尝试询问示例智能体获取最新的支持信息。
3. <strong>代码解释器存在哪些限制</strong>？

   - 只能执行 Python 代码
   - 无法读写外部文件
   - 无法安装外部软件包
   - 无法访问外部网络
   - 计算资源和时长受限
4. <strong>如何处理数据文件？</strong>

   代码解释器目前不支持直接处理数据文件。建议您将数据转换为文本格式，并通过输入框发送到智能体进行处理。
5. <strong>如何处理错误信息？</strong>

   当代码执行失败时，智能体会返回详细的错误信息。请根据错误提示进行相应调整。如果遇到网络不稳定导致的响应延迟或超时，请稍后重试。
