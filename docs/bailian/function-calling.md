> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 函数调用（下线中）

> Assistant API 支持函数调用（Function Calling），让智能体能够根据您的需求自动调用外部函数。例如，智能体可以调用函数进行文本翻译或处理其他任务。本文介绍了一个简单的"翻译助手"示例，帮助您快速上手函数调用的基本方法。

<meta property="og:title" content="函数调用（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

## 快速开始 <span id="423fa62284unl" />

在这个示例中，我们将创建一个翻译助手，并创建一个函数`translate_text`作为智能体可以调用的工具。在这个示例中，我们将询问智能体将"Hello world"翻译成中文。

### 在您开始前 <span id="ad282467a9ydq" />

首先，确保您已经安装了必要的依赖库，如 requests 和 dashscope。您可以通过以下命令安装它们：

```bash
pip install requests dashscope
```

### 第一步：创建"翻译文本"函数 <span id="bb176d27c5z1a" />

我们首先创建一个简单的翻译函数，用于演示目的，它使用预定义的翻译对照表。

```python expandable
def translate_text(text, target_language):
    """
    将文本翻译成指定的目标语言。
    这是一个使用预定义翻译的简单演示。

    参数：
        text (str): 需要翻译的文本
        target_language (str): 目标语言代码（例如：'zh'、'es'、'ja'）

    返回：
        str: 翻译后的文本或错误信息
    """
    # 用于演示的翻译字典
    mock_translations = {
        ('Hello world', 'zh'): '你好世界',
        ('Hello world', 'es'): '¡Hola Mundo!',
        ('Hello world', 'ja'): 'こんにちは世界',
        ('How are you?', 'zh'): '你好吗？',
        ('How are you?', 'es'): '¿Cómo estás?',
        ('How are you?', 'ja'): 'お元気ですか？'
    }

    try:
        return mock_translations.get((text, target_language),
            f"未找到翻译。在实际生产环境中，这里会调用翻译服务。")
    except Exception as e:
        return f"翻译失败：{str(e)}"
```

<strong>解释：</strong>

- <strong>翻译功能</strong>：通过预定义的翻译对照表来模拟翻译功能，支持多种语言之间的转换。
- <strong>错误处理</strong>：包含了基本的错误处理机制，确保函数在各种情况下都能返回合适的响应。

现在我们将通过 Assistant API 来创建一个智能体，该智能体将自动处理用户查询，并调用我们定义的 translate\_text 函数来提供翻译服务。

### 第二步：描述"翻译文本"函数 <span id="32693f5b0fh7i" />

您需要向智能体描述"翻译文本"函数。智能体会根据描述信息正确调用翻译函数。

```python
from dashscope import Assistants, Messages, Runs, Threads
import json

# 定义翻译工具
translation_tool = {
    "type": "function",
    "function": {
        "name": "translate_text",
        "description": "将文本翻译成指定的目标语言",
        "parameters": {
            "type": "object",
            "properties": {
                "text": {
                    "type": "string",
                    "description": "需要翻译的文本"
                },
                "target_language": {
                    "type": "string",
                    "description": "目标语言代码（例如：'zh'、'es'、'ja'）"
                }
            },
            "required": ["text", "target_language"]
        }
    }
}
```

<strong>解释：</strong>

- <strong>name</strong>: 函数的名称为 translate\_text，将在智能体中被调用。
- <strong>description</strong>: 提供了该工具的描述，帮助用户理解其用途。
- <strong>parameters</strong>: 定义了函数的参数，包括要翻译的文本和目标语言。

### 第三步：创建智能体 <span id="8ebc7c261en48" />

现在我们可以创建一个 Assistant 实例，它是一个智能体，并且将会使用我们定义的翻译工具。

```python
# 创建 Assistant
assistant = Assistants.create(
    model='qwen-plus',
    name='翻译助手',
    description='一个能够在不同语言之间进行文本翻译的助手',
    instructions='你是一个翻译助手。当用户请求翻译时，使用 translate_text 函数来帮助他们。',
    tools=[translation_tool]
)
```

<strong>解释：</strong>

- <strong>model</strong>: 使用的模型类型，这里是 qwen-plus，它支持语言理解和任务处理。
- <strong>name</strong>: 给智能体命名为"Translation Assistant"。
- <strong>description</strong>: 描述了智能体的功能，它能够帮助用户进行文本翻译。
- <strong>tools</strong>: 注册了我们之前定义的 translation\_tool，使得智能体可以调用该工具。

### 第四步：创建对话线程并与智能体互动 <span id="657f8e939abs9" />

创建一个新的对话线程，并向其中添加用户消息，然后运行智能体，处理用户的问题。

```python
# 创建一个新的线程
thread = Threads.create()

# 添加用户消息到线程
Messages.create(
    thread_id=thread.id,
    role="user",
    content="请将'Hello world'翻译成中文。"
)

# 运行 Assistant
run = Runs.create(thread_id=thread.id, assistant_id=assistant.id)

# 等待运行完成
run = Runs.wait(thread_id=thread.id, run_id=run.id)
```

<strong>解释：</strong>

- <strong>Threads.create()</strong>: 创建一个新的对话线程，后续的消息将在此线程中进行。
- <strong>Messages.create()</strong>: 向线程添加用户消息，这里用户请求将"Hello world"翻译成中文。
- <strong>Runs.create()</strong>: 触发智能体开始处理用户消息。
- <strong>Runs.wait()</strong>: 等待智能体处理完毕。

### 第五步：处理函数调用并返回结果 <span id="d398b8d7bdnqx" />

如果智能体在处理过程中需要调用工具，我们将调用 translate\_text 函数进行翻译，并将结果返回给用户。

```python
# 检查是否需要调用函数
if run.required_action:
    for tool_call in run.required_action.submit_tool_outputs.tool_calls:
        if tool_call.function.name == "translate_text":
            args = json.loads(tool_call.function.arguments)
            translation = translate_text(args["text"], args["target_language"])

            # 提交工具输出
            Runs.submit_tool_outputs(
                thread_id=thread.id,
                run_id=run.id,
                tool_outputs=[{"tool_call_id": tool_call.id, "output": translation}]
            )

            # 等待新的运行完成
            run = Runs.wait(thread_id=thread.id, run_id=run.id)
```

<strong>解释：</strong>

- <strong>检查函数调用</strong>：如果智能体需要调用某个函数，我们检查它调用的是否是 translate\_text，并通过之前定义的函数进行翻译。
- <strong>提交结果</strong>：将翻译结果通过 Runs.submit\_tool\_outputs 返回给智能体，并继续等待智能体的下一步回复。

### 第六步：获取智能体的回复 <span id="00bad43eeb17n" />

智能体处理完成后，我们可以从对话线程中获取智能体的回复并展示给用户。

```python
# 获取 Assistant 的回复
messages = Messages.list(thread_id=thread.id)
for message in messages.data:
    if message.role == "assistant":
        print(f"Assistant: {message.content[0].text.value}")
```

### 总结 <span id="38b868ca74nzm" />

通过以上步骤，您已经成功创建了一个智能体，它可以处理用户的翻译请求，并使用翻译函数来完成文本转换。Assistant API 使得构建复杂的任务驱动型智能体变得简单且高效。

您可以根据需求进一步扩展智能体的功能，例如增加更多工具或修改智能体的行为指令。

## 快速生成业务函数的描述信息 <span id="9f981d4fbangu" />

在快速入门案例中，您需要向智能体描述“翻译文本”函数，而这一过程比较繁琐。因此，我们提供了一个简单的转换函数，帮助您快速描述业务函数。

```python expandable
import inspect

def function_to_schema(func) -> dict:
    # 将 Python 类型映射为 JSON schema 类型
    type_map = {
        str: "string",
        int: "integer",
        float: "number",
        bool: "boolean",
        list: "array",
        dict: "object",
        type(None): "null",
    }

    # 尝试获取函数的签名
    try:
        signature = inspect.signature(func)
    except ValueError as e:
        # 如果签名获取失败，则抛出错误并附带错误信息
        raise ValueError(
            f"Failed to get signature for function {func.__name__}: {str(e)}"
        )

    # 初始化一个字典来存储参数类型
    parameters = {}
    # 遍历函数的参数，并映射它们的类型
    for param in signature.parameters.values():
        try:
            param_type = type_map.get(param.annotation, "string")
        except KeyError as e:
            # 如果参数的类型注解未知，则抛出错误
            raise KeyError(
                f"Unknown type annotation {param.annotation} for parameter {param.name}: {str(e)}"
            )
        parameters[param.name] = {"type": param_type}

    # 创建必需参数的列表（那些没有默认值的参数）
    required = [
        param.name
        for param in signature.parameters.values()
        if param.default == inspect._empty
    ]

    # 返回函数的 schema 作为字典
    return {
        "type": "function",
        "function": {
            "name": func.__name__,
            "description": (func.__doc__ or "").strip(),  # 获取函数描述（docstring）
            "parameters": {
                "type": "object",
                "properties": parameters,  # 参数类型
                "required": required,  # 必需参数列表
            },
        },
    }
```

以快速开始的翻译文本函数为例：

```python
translation_tool = function_to_schema(translate_text)
print(json.dumps(translation_tool, indent=4, ensure_ascii=False))
```

翻译文本函数将被自动转换为：

```json
{
    "type": "function",
    "function": {
        "name": "translate_text",
        "description": "将文本翻译成指定的目标语言。\n    这是一个使用预定义翻译的简单演示。\n\n    参数：\n        text (str): 需要翻译的文本\n        target_language (str): 目标语言代码（例如：'zh'、'es'、'ja'）\n\n    返回：\n        str: 翻译后的文本或错误信息",
        "parameters": {
            "type": "object",
            "properties": {
                "text": {
                    "type": "string"
                },
                "target_language": {
                    "type": "string"
                }
            },
            "required": [
                "text",
                "target_language"
            ]
        }
    }
}
```

现在，我们可以将函数的描述信息传递给模型。

```python
assistant = Assistants.create(
    model='qwen-plus',
    name='翻译助手',
    description='一个能够在不同语言之间进行文本翻译的助手',
    instructions='你是一个翻译助手。当用户请求翻译时，使用 translate_text 函数来帮助他们。',
    tools=[translation_tool]
)
```

## 使用流式输出 <span id="cc09f70409cgi" />

> 推荐您先了解[流式输出的基本用法](/zh/model-studio/streaming-output)，再阅读本章节内容。

在流式输出下，您需要修改[第五步：处理函数调用并返回结果](/zh/model-studio/function-calling#d398b8d7bdnqx)的代码逻辑，这是因为 Runs 现在返回的是 Assistant 事件流。

当 Assistant 决定调用函数时，Runs 会返回事件 `thread.run.requires_action`和大模型给出的入参信息 `data.required_action.submit_tool_outputs.tool_calls`，您需要在此时提交函数输出结果。

请注意，在提交函数输出`run = Runs.submit_tool_outputs`时，也需启用流式输出。

```python expandable
# 此代码仅供演示使用，请在充分理解逻辑后引入您的项目
# 假设已经创建了 assistant、thread 和 message 对象

# 定义工具函数映射
tools_map = {
    "translate_text": translate_text,  # 翻译函数
}

run = Runs.create(
        thread_id=thread.id,
        assistant_id=assistant.id,
        stream=True  # 开启流式输出
    )
while True:  # 添加外层循环
    for event, data in run:  # 事件流和事件数据的详细信息，请参阅 Assistant API 流式输出文档
        if event == 'thread.run.requires_action':   # Assistant 调用了工具，正在等待函数输出
            tool_outputs = []  # 提交输出的方法与第五步类似
            for tool in data.required_action.submit_tool_outputs.tool_calls:
                name = tool.function.name
                args = json.loads(tool.function.arguments)
                output = tools_map[name](**args)
                tool_outputs.append({
                    "tool_call_id": tool.id,
                    "output": output,
                })
            run = Runs.submit_tool_outputs(  # 提交函数输出
                thread_id=thread.id,
                run_id=data.id,
                tool_outputs=tool_outputs,
                stream=True  # 此处也需要开启流式输出
            )
            break  # 跳出当前的 for 循环，下一次循环将轮询新的 Runs 对象。
    else:
        break  # 如果首轮 for 循环正常结束（没有触发函数调用），就直接退出 while 循环
```

您可能会注意到，这里在处理事件流的 for 循环外，额外添加了一层 while 循环。这是因为您在提交函数输出时，系统会生成一个新的 Runs 对象。while 循环将帮助您自动跟踪最新的事件流，从而使 Assistant 在获取函数调用的结果后，继续生成相应的回复。
