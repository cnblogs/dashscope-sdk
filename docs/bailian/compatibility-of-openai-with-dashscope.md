> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# OpenAI Chat接口兼容

<meta property="og:title" content="OpenAI Chat接口兼容" />

阿里云百炼的千问模型支持 OpenAI 兼容接口，您只需调整 API Key、BASE\_URL 和模型名称，即可将原有 OpenAI 代码迁移至阿里云百炼服务使用。

## 兼容OpenAI需要信息 <span id="54e1cc7a9bq65" /> <span id="56e9761e923mg" />

### BASE\_URL <span id="21d628d5f82z7" /> <span id="ed282cb8338lp" />

BASE\_URL表示模型服务的网络访问点或地址。通过该地址，您可以访问服务提供的功能或数据。在Web服务或API的使用中，BASE\_URL通常对应于服务的具体操作或资源的URL。当您使用OpenAI兼容接口来使用阿里云百炼模型服务时，需要配置BASE\_URL。

- 当您通过OpenAI SDK或其他OpenAI兼容的SDK调用时，需要配置的BASE\_URL如下：

```http
北京：https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1
弗吉尼亚：https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1
新加坡：https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1
日本（东京）：https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1
```

- 当您通过HTTP请求调用时，需要配置的完整访问endpoint如下：

```http
北京：POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions
弗吉尼亚：POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions
新加坡：POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions
日本（东京）：POST https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions
```

<Tip>
  阿里云百炼为华北2（北京）、新加坡、中国香港地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

> <strong>调用失败排查</strong>：通过 OpenAI 兼容接口调用时，如遇到 404、401、403 或连接失败等错误，请依次检查以下配置：

#### 跨地域调用 <span id="h4-cross-region-calling" /> <span id="cross-region-calling" />

百炼 API Key 按地域绑定。调用某个地域的 `base_url` 时，必须使用在同一地域创建的 API Key；使用其他地域的 API Key 调用该地域 endpoint 会被鉴权拒绝。

该规则适用于所有提供 endpoint 的地域，包括华北2（北京）、美国（弗吉尼亚）、新加坡和日本（东京）。请在所调用 endpoint 对应地域的控制台中创建 API Key。

使用北京地域 API Key 调用弗吉尼亚地域 endpoint 时，返回 HTTP 401，错误信息为 `Incorrect API key provided`，错误码为 `invalid_api_key`。该错误表示 API Key 与 endpoint 所属地域不匹配，而非 API Key 失效或权限不足。

### 支持的模型列表 <span id="eadfc13038jd5" /> <span id="7f9c78ae99pwz" />

支持的模型：Qwen 大语言模型（商业版、开源版）、Qwen-VL、Qwen-Coder、Qwen-Omni、Qwen-Math、DeepSeek（阿里云直供、硅基流动直供、快手万擎直供）、Kimi（阿里云直供、月之暗面直供）、GLM（阿里云直供）、MiniMax（阿里云直供、稀宇科技直供）。

> 三方直供模型仅在中国站的华北2（北京）地域可用，调用前需先在百炼控制台开通对应服务（以 SiliconFlow DeepSeek 为例：搜索 deepseek → 找到 SiliconFlow DeepSeek 模型卡片 → 单击立即开通 → 确认授权）。

> Qwen-Audio不支持OpenAI兼容协议，仅支持DashScope协议。

## 通过OpenAI SDK调用 <span id="aad9ffd00arml" /> <span id="43f870c0cfvsy" />

### 前提条件 <span id="o6ecl" />

- 请确保您的计算机上安装了Python环境。

- 请安装最新版OpenAI SDK。

```bash
# 如果下述命令报错，请将pip替换为pip3
pip install -U openai
```

- 您需要开通阿里云百炼模型服务并获得API-KEY，详情请参考：获取与配置 API Key。

- 我们推荐您将API-KEY配置到环境变量中以降低API-KEY的泄露风险，配置方法可参考配置API Key到环境变量。您也可以在代码中配置API-KEY，<strong>但是泄露风险会提高</strong>。

- 请选择您需要使用的模型：[支持的模型列表](/zh/model-studio/compatibility-of-openai-with-dashscope#eadfc13038jd5)。

### 使用方式 <span id="t4GMg" />

您可以参考以下示例来使用OpenAI SDK访问百炼服务上的千问模型。

#### 非流式调用示例 <span id="d80da2d585741" />

```python
from openai import OpenAI
import os

def get_response():
    client = OpenAI(
        # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
        # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        # 以下是北京地域base_url
        base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
    )
    completion = client.chat.completions.create(
        model="qwen3.8-max",  # 此处以qwen-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
        messages=[{'role': 'system', 'content': 'You are a helpful assistant.'},
                  {'role': 'user', 'content': '你是谁？'}]
        )
    print(completion.model_dump_json())

if __name__ == '__main__':
    get_response()
```

运行代码可以获得以下结果：

```json
{
    "id": "chatcmpl-xxx",
    "choices": [
        {
            "finish_reason": "stop",
            "index": 0,
            "logprobs": null,
            "message": {
                "content": "我是来自阿里云的超大规模预训练模型，我叫千问。",
                "role": "assistant",
                "function_call": null,
                "tool_calls": null
            }
        }
    ],
    "created": 1716430652,
    "model": "qwen3.8-max",
    "object": "chat.completion",
    "system_fingerprint": null,
    "usage": {
        "completion_tokens": 18,
        "prompt_tokens": 22,
        "total_tokens": 40
    }
}
```

#### 流式调用示例 <span id="fece922d5boc1" />

```python expandable
from openai import OpenAI
import os

def get_response():
    client = OpenAI(
        # 如果您没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx"
        # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        # 以下是北京地域base_url
        base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
    )
    completion = client.chat.completions.create(
        model="qwen3.8-max",  # 此处以qwen-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
        messages=[{'role': 'system', 'content': 'You are a helpful assistant.'},
                  {'role': 'user', 'content': '你是谁？'}],
        stream=True,
        # 通过以下设置，在流式输出的最后一行展示token使用信息
        stream_options={"include_usage": True}
        )
    for chunk in completion:
        print(chunk.model_dump_json())

if __name__ == '__main__':
    get_response()
```

运行代码可以获得以下结果：

```json
{"id":"chatcmpl-xxx","choices":[{"delta":{"content":"","function_call":null,"role":"assistant","tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1719286190,"model":"qwen3.8-max","object":"chat.completion.chunk","system_fingerprint":null,"usage":null}
{"id":"chatcmpl-xxx","choices":[{"delta":{"content":"我是","function_call":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1719286190,"model":"qwen3.8-max","object":"chat.completion.chunk","system_fingerprint":null,"usage":null}
{"id":"chatcmpl-xxx","choices":[{"delta":{"content":"来自","function_call":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1719286190,"model":"qwen3.8-max","object":"chat.completion.chunk","system_fingerprint":null,"usage":null}
{"id":"chatcmpl-xxx","choices":[{"delta":{"content":"阿里","function_call":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1719286190,"model":"qwen3.8-max","object":"chat.completion.chunk","system_fingerprint":null,"usage":null}
{"id":"chatcmpl-xxx","choices":[{"delta":{"content":"云的大规模语言模型","function_call":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1719286190,"model":"qwen3.8-max","object":"chat.completion.chunk","system_fingerprint":null,"usage":null}
{"id":"chatcmpl-xxx","choices":[{"delta":{"content":"，我叫千问。","function_call":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1719286190,"model":"qwen3.8-max","object":"chat.completion.chunk","system_fingerprint":null,"usage":null}
{"id":"chatcmpl-xxx","choices":[{"delta":{"content":"","function_call":null,"role":null,"tool_calls":null},"finish_reason":"stop","index":0,"logprobs":null}],"created":1719286190,"model":"qwen3.8-max","object":"chat.completion.chunk","system_fingerprint":null,"usage":null}
{"id":"chatcmpl-xxx","choices":[],"created":1719286190,"model":"qwen3.8-max","object":"chat.completion.chunk","system_fingerprint":null,"usage":{"completion_tokens":16,"prompt_tokens":22,"total_tokens":38}}
```

#### function call示例 <span id="97e2b45391x08" />

此处以天气查询工具与时间查询工具为例，向您展示通过OpenAI接口兼容实现function call的功能。示例代码可以实现多轮工具调用。

```python expandable
from openai import OpenAI
from datetime import datetime
import json
import os

client = OpenAI(
    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key=os.getenv("DASHSCOPE_API_KEY"),
    # 以下是北京地域base_url
    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",  # 填写DashScope SDK的base_url
)

# 定义工具列表，模型在选择使用哪个工具时会参考工具的name和description
tools = [
    # 工具1 获取当前时刻的时间
    {
        "type": "function",
        "function": {
            "name": "get_current_time",
            "description": "当你想知道现在的时间时非常有用。",
            # 因为获取当前时间无需输入参数，因此parameters为空字典
            "parameters": {}
        }
    },
    # 工具2 获取指定城市的天气
    {
        "type": "function",
        "function": {
            "name": "get_current_weather",
            "description": "当你想查询指定城市的天气时非常有用。",
            "parameters": {
                "type": "object",
                "properties": {
                    # 查询天气时需要提供位置，因此参数设置为location
                    "location": {
                        "type": "string",
                        "description": "城市或县区，比如北京市、杭州市、余杭区等。"
                    }
                }
            },
            "required": [
                "location"
            ]
        }
    }
]

# 模拟天气查询工具。返回结果示例：“北京今天是雨天。”
def get_current_weather(location):
    return f"{location}今天是雨天。 "

# 查询当前时间的工具。返回结果示例：“当前时间：2024-04-15 17:15:18。“
def get_current_time():
    # 获取当前日期和时间
    current_datetime = datetime.now()
    # 格式化当前日期和时间
    formatted_time = current_datetime.strftime('%Y-%m-%d %H:%M:%S')
    # 返回格式化后的当前时间
    return f"当前时间：{formatted_time}。"

# 封装模型响应函数
def get_response(messages):
    completion = client.chat.completions.create(
        model="qwen3.8-max",  # 此处以qwen-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
        messages=messages,
        tools=tools
        )
    return completion.model_dump()

def call_with_messages():
    print('\n')
    messages = [
            {
                "content": input('请输入：'),  # 提问示例："现在几点了？" "一个小时后几点" "北京天气如何？"
                "role": "user"
            }
    ]
    print("-"*60)
    # 模型的第一轮调用
    i = 1
    first_response = get_response(messages)
    assistant_output = first_response['choices'][0]['message']
    print(f"\n第{i}轮大模型输出信息：{first_response}\n")
    if  assistant_output['content'] is None:
        assistant_output['content'] = ""
    messages.append(assistant_output)
    # 如果不需要调用工具，则直接返回最终答案
    if assistant_output['tool_calls'] == None:  # 如果模型判断无需调用工具，则将assistant的回复直接打印出来，无需进行模型的第二轮调用
        print(f"无需调用工具，我可以直接回复：{assistant_output['content']}")
        return
    # 如果需要调用工具，则进行模型的多轮调用，直到模型判断无需调用工具
    while assistant_output['tool_calls'] != None:
        # 如果判断需要调用查询天气工具，则运行查询天气工具
        if assistant_output['tool_calls'][0]['function']['name'] == 'get_current_weather':
            tool_info = {"name": "get_current_weather", "role":"tool"}
            # 提取位置参数信息
            location = json.loads(assistant_output['tool_calls'][0]['function']['arguments'])['location']
            tool_info['content'] = get_current_weather(location)
        # 如果判断需要调用查询时间工具，则运行查询时间工具
        elif assistant_output['tool_calls'][0]['function']['name'] == 'get_current_time':
            tool_info = {"name": "get_current_time", "role":"tool"}
            tool_info['content'] = get_current_time()
        print(f"工具输出信息：{tool_info['content']}\n")
        print("-"*60)
        messages.append(tool_info)
        assistant_output = get_response(messages)['choices'][0]['message']
        if  assistant_output['content'] is None:
            assistant_output['content'] = ""
        messages.append(assistant_output)
        i += 1
        print(f"第{i}轮大模型输出信息：{assistant_output}\n")
    print(f"最终答案：{assistant_output['content']}")

if __name__ == '__main__':
    call_with_messages()
```

### 输入参数配置 <span id="d553cbbee6mxk" />

输入参数与OpenAI的接口参数对齐，当前已支持的参数如下：

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "16.424419%" }} />

    <col style={{ width: "16.860465%" }} />

    <col style={{ width: "10.901163%" }} />

    <col style={{ width: "55.813953%" }} />
  </colgroup>

  <thead>
    <tr>
      <th style={{ verticalAlign: "top" }}>
        <strong>参数</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>类型</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>默认值</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>说明</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        model
      </td>

      <td style={{ verticalAlign: "top" }}>
        *string*
      </td>

      <td style={{ verticalAlign: "top" }}>
        -
      </td>

      <td style={{ verticalAlign: "top" }}>
        用户使用model参数指明对应的模型，可选的模型请见[支持的模型列表](/zh/model-studio/compatibility-of-openai-with-dashscope#eadfc13038jd5)。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        messages
      </td>

      <td style={{ verticalAlign: "top" }}>
        *array*
      </td>

      <td style={{ verticalAlign: "top" }}>
        -
      </td>

      <td style={{ verticalAlign: "top" }}>
        用户与模型的对话历史。array中的每个元素形式为`{"role":角色, "content": 内容}`。角色当前可选值：system、user、assistant，其中，仅`messages[0]`中支持role为system，一般情况下，user和assistant需要交替出现，且messages中最后一个元素的role必须为user。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        top\_p（可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *float*
      </td>

      <td style={{ verticalAlign: "top" }}>
        -
      </td>

      <td style={{ verticalAlign: "top" }}>
        生成过程中的核采样方法概率阈值，例如，取值为0.8时，仅保留概率加起来大于等于0.8的最可能token的最小集合作为候选集。取值范围为（0,1.0)，取值越大，生成的随机性越高；取值越小，生成的确定性越高。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        temperature（可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *float*
      </td>

      <td style={{ verticalAlign: "top" }}>
        -
      </td>

      <td style={{ verticalAlign: "top" }}>
        用于控制模型回复的随机性和多样性。具体来说，temperature值控制了生成文本时对每个候选词的概率分布进行平滑的程度。较高的temperature值会降低概率分布的峰值，使得更多的低概率词被选择，生成结果更加多样化；而较低的temperature值则会增强概率分布的峰值，使得高概率词更容易被选择，生成结果更加确定。

        取值范围： \[0, 2)，不建议取值为0，无意义。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        presence\_penalty

        （可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *float*
      </td>

      <td style={{ verticalAlign: "top" }}>
        -
      </td>

      <td style={{ verticalAlign: "top" }}>
        用户控制模型生成时整个序列中的重复度。提高presence\_penalty时可以降低模型生成的重复度，取值范围\[-2.0, 2.0]。

        <Note>
          目前仅在千问商业模型和qwen1.5及以后的开源模型上支持该参数。
        </Note>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        n（可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *integer*
      </td>

      <td style={{ verticalAlign: "top" }}>
        1
      </td>

      <td style={{ verticalAlign: "top" }}>
        生成响应的个数，取值范围是`1-4`。对于需要生成多个响应的场景（如创意写作、广告文案等），可以设置较大的 n 值。

        > 设置较大的 n 值不会增加输入 Token 消耗，会增加输出 Token 的消耗。

        > 当前仅支持 qwen-plus 模型，且在传入 tools 参数时固定为1。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        max\_tokens（可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *integer*
      </td>

      <td style={{ verticalAlign: "top" }}>
        -
      </td>

      <td style={{ verticalAlign: "top" }}>
        指定模型可生成的最大token个数。例如模型最大输出长度为2k，您可以设置为1k，防止模型输出过长的内容。

        不同的模型有不同的输出上限，具体请参见模型列表。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        seed（可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *integer*
      </td>

      <td style={{ verticalAlign: "top" }}>
        -
      </td>

      <td style={{ verticalAlign: "top" }}>
        生成时使用的随机数种子，用于控制模型生成内容的随机性。seed支持无符号64位整数。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        stream（可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *boolean*
      </td>

      <td style={{ verticalAlign: "top" }}>
        False
      </td>

      <td style={{ verticalAlign: "top" }}>
        用于控制是否使用流式输出。当以stream模式输出结果时，接口返回结果为generator，需要通过迭代获取结果，每次输出为当前生成的增量序列。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        stop（可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *string or array*
      </td>

      <td style={{ verticalAlign: "top" }}>
        None
      </td>

      <td style={{ verticalAlign: "top" }}>
        stop参数用于实现内容生成过程的精确控制，在模型生成的内容即将包含指定的字符串或token\_id时自动停止。stop可以为string类型或array类型。

        - string类型

          当模型将要生成指定的stop词语时停止。

          例如将stop指定为"你好"，则模型将要生成“你好”时停止。
        - array类型

          array中的元素可以为token\_id或者字符串，或者元素为token\_id的array。当模型将要生成的token或其对应的token\_id在stop中时，模型生成将会停止。以下为stop为array时的示例（tokenizer对应模型为qwen3.5-flash）：

          1.元素为token\_id：

          token\_id为108386和104307分别对应token为“你好”和“天气”，设定stop为`[108386,104307]`，则模型将要生成“你好”或者“天气”时停止。

          2.元素为字符串：

          设定stop为`["你好","天气"]`，则模型将要生成“你好”或者“天气”时停止。

          3.元素为array：

          token\_id为108386和103924分别对应token为“你好”和“啊”，token\_id为35946和101243分别对应token为“我”和“很好”。设定stop为`[[108386, 103924],[35946, 101243]]`，则模型将要生成“你好啊”或者“我很好”时停止。

          <Note>
            stop为array类型时，不可以将token\_id和字符串同时作为元素输入，比如不可以指定stop为`["你好",104307]`。
          </Note>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        tools（可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *array*
      </td>

      <td style={{ verticalAlign: "top" }}>
        None
      </td>

      <td style={{ verticalAlign: "top" }}>
        用于指定可供模型调用的工具库，一次function call流程模型会从中选择其中一个工具。tools中每一个tool的结构如下：

        - type，类型为string，表示tools的类型，当前仅支持function。
        - function，类型为object，键值包括name，description和parameters：

          - name：类型为string，表示工具函数的名称，必须是字母、数字，可以包含下划线和短划线，最大长度为64。
          - description：类型为string，表示工具函数的描述，供模型选择何时以及如何调用工具函数。
          - parameters：类型为object，表示工具的参数描述，需要是一个合法的JSON Schema。JSON Schema的描述可以见[链接](https://json-schema.org/understanding-json-schema)。如果parameters参数为空，表示function没有入参。

            parameters中各属性的`type`支持JSON Schema定义的常见类型，包括`string`、`number`、`integer`、`boolean`、`array`和`object`等。当属性类型为`array`时，需要通过`items`字段指定数组元素的类型。示例如下：

        ```json
        {
            "type": "object",
            "properties": {
                "command": {
                    "type": "array",
                    "items": {"type": "string"},
                    "description": "要执行的命令，例如['ls', '-l']"
                }
            },
            "required": ["command"]
        }
        ```

        在function call流程中，无论是发起function call的轮次，还是向模型提交工具函数的执行结果，均需设置tools参数。当前支持的模型包括qwen-plus和qwen-max。

        <Note>
          tools暂时无法与stream=True同时使用。
        </Note>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        stream\_options（可选）
      </td>

      <td style={{ verticalAlign: "top" }}>
        *object*
      </td>

      <td style={{ verticalAlign: "top" }}>
        None
      </td>

      <td style={{ verticalAlign: "top" }}>
        该参数用于配置在流式输出时是否展示使用的token数目。只有当stream为True的时候该参数才会激活生效。若您需要统计流式输出模式下的token数目，可将该参数配置为`stream_options={"include_usage":True}`。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        enable\_search
      </td>

      <td style={{ verticalAlign: "top" }}>
        *boolean*
      </td>

      <td style={{ verticalAlign: "top" }}>
        False
      </td>

      <td style={{ verticalAlign: "top" }}>
        用于控制模型在生成文本时是否使用互联网搜索结果进行参考。取值如下：

        - True：启用互联网搜索，模型会将搜索结果作为文本生成过程中的参考信息，但模型会基于其内部逻辑判断是否使用互联网搜索结果。

          > 若开启后未联网搜索，可优化提示词，或设置`search_options`中的`forced_search`参数开启强制搜索。
        - False（默认）：关闭互联网搜索。

        > qwen-long暂不支持此参数。

        配置方式为：`extra_body={"enable_search": True}`。
      </td>
    </tr>
  </tbody>
</table>

### 返回参数说明 <span id="f667afe46e3c1" /> <span id="dcea7e40cf8ve" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "17.042254%" }} /><col style={{ width: "13.239437%" }} /><col style={{ width: "44.507042%" }} /><col style={{ width: "25.211268%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>返回参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>数据类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>备注</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>id</p></td><td style={{ verticalAlign: "top" }}><p><em>string</em></p></td><td style={{ verticalAlign: "top" }}><p>系统生成的标识本次调用的id。</p></td><td style={{ verticalAlign: "top" }}><p>无</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>model</p></td><td style={{ verticalAlign: "top" }}><p><em>string</em></p></td><td style={{ verticalAlign: "top" }}><p>本次调用的模型名。</p></td><td style={{ verticalAlign: "top" }}><p>无</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>system\_fingerprint</p></td><td style={{ verticalAlign: "top" }}><p><em>string</em></p></td><td style={{ verticalAlign: "top" }}><p>模型运行时使用的配置版本，当前暂时不支持，返回为空字符串“”。</p></td><td style={{ verticalAlign: "top" }}><p>无</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>choices</p></td><td style={{ verticalAlign: "top" }}><p><em>array</em></p></td><td style={{ verticalAlign: "top" }}><p>模型生成内容的详情。</p></td><td rowSpan={6} style={{ verticalAlign: "top" }}><p>无</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>choices\[i].finish\_reason</p></td><td style={{ verticalAlign: "top" }}><p><em>string</em></p></td><td style={{ verticalAlign: "top" }}><p>有三种情况：</p><ul><li><p>正在生成时为null；</p></li><li><p>因触发输入参数中的stop条件而结束为stop；</p></li><li><p>因生成长度过长而结束为length。</p></li></ul></td></tr><tr><td style={{ verticalAlign: "top" }}><p>choices\[i].message</p></td><td style={{ verticalAlign: "top" }}><p><em>object</em></p></td><td style={{ verticalAlign: "top" }}><p>模型输出的消息。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>choices\[i].message.role</p></td><td style={{ verticalAlign: "top" }}><p><em>string</em></p></td><td style={{ verticalAlign: "top" }}><p>模型的角色，固定为assistant。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>choices\[i].message.content</p></td><td style={{ verticalAlign: "top" }}><p><em>string</em></p></td><td style={{ verticalAlign: "top" }}><p>模型生成的文本。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>choices\[i].index</p></td><td style={{ verticalAlign: "top" }}><p><em>integer</em></p></td><td style={{ verticalAlign: "top" }}><p>生成的结果序列编号，默认为0。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>created</p></td><td style={{ verticalAlign: "top" }}><p><em>integer</em></p></td><td style={{ verticalAlign: "top" }}><p>当前生成结果的时间戳（s）。</p></td><td style={{ verticalAlign: "top" }}><p>无</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>usage</p></td><td style={{ verticalAlign: "top" }}><p><em>object</em></p></td><td style={{ verticalAlign: "top" }}><p>计量信息，表示本次请求所消耗的token数据。</p></td><td style={{ verticalAlign: "top" }}><p>无</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>usage.prompt\_tokens</p></td><td style={{ verticalAlign: "top" }}><p><em>integer</em></p></td><td style={{ verticalAlign: "top" }}><p>用户输入文本转换成token后的长度。</p></td><td style={{ verticalAlign: "top" }}><p>无</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>usage.completion\_tokens</p></td><td style={{ verticalAlign: "top" }}><p><em>integer</em></p></td><td style={{ verticalAlign: "top" }}><p>模型生成回复转换为token后的长度。</p></td><td style={{ verticalAlign: "top" }}><p>无</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>usage.total\_tokens</p></td><td style={{ verticalAlign: "top" }}><p><em>integer</em></p></td><td style={{ verticalAlign: "top" }}><p>usage.prompt\_tokens与usage.completion\_tokens的总和。</p></td><td style={{ verticalAlign: "top" }}><p>无</p></td></tr></tbody></table>

## 通过langchain\_openai SDK调用 <span id="2197f744f0d2j" /> <span id="414defd168byq" />

### 前提条件 <span id="39560b807bss9" />

- 请确保您的计算机上安装了Python环境。

- 通过运行以下命令安装langchain\_openai SDK。

```bash
# 如果下述命令报错，请将pip替换为pip3
pip install -U langchain_openai
```

- 您需要开通阿里云百炼模型服务并获得API-KEY，详情请参考：获取与配置 API Key。
- 我们推荐您将API-KEY配置到环境变量中以降低API-KEY的泄露风险，详情可参考配置API Key到环境变量。您也可以在代码中配置API-KEY，<strong>但是泄露风险会提高</strong>。
- 请选择您需要使用的模型：[支持的模型列表](/zh/model-studio/compatibility-of-openai-with-dashscope#eadfc13038jd5)。

### 使用方式 <span id="c946143631li6" />

您可以参考以下示例来通过langchain\_openai SDK使用阿里云百炼的千问模型。

#### 非流式输出 <span id="337e4fa5a6b89" />

非流式输出使用invoke方法实现，请参考以下示例代码：

```python
from langchain_openai import ChatOpenAI
import os

def get_response():
    llm = ChatOpenAI(
        # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
        # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        # 以下是北京地域base_url
        base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
        model="qwen3.8-max"    # 此处以qwen-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
        )
    messages = [
        {"role":"system","content":"You are a helpful assistant."},
        {"role":"user","content":"你是谁？"}
    ]
    response = llm.invoke(messages)
    print(response.json())

if __name__ == "__main__":
    get_response()
```

运行代码，可以得到以下结果：

```json
{
    "content": "我是来自阿里云的大规模语言模型，我叫千问。",
    "additional_kwargs": {},
    "response_metadata": {
        "token_usage": {
            "completion_tokens": 16,
            "prompt_tokens": 22,
            "total_tokens": 38
        },
        "model_name": "qwen-plus",
        "system_fingerprint": "",
        "finish_reason": "stop",
        "logprobs": null
    },
    "type": "ai",
    "name": null,
    "id": "run-xxx",
    "example": false,
    "tool_calls": [],
    "invalid_tool_calls": []
}
```

#### 流式输出 <span id="b1fd57317ba60" />

流式输出使用stream方法实现，无需在参数中配置stream参数。

```python
from langchain_openai import ChatOpenAI
import os

def get_response():
    llm = ChatOpenAI(
        # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
        # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        # 以下是北京地域base_url
        base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
        model="qwen3.8-max",  # 此处以qwen-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
        stream_usage=True
        )
    messages = [
        {"role":"system","content":"You are a helpful assistant."},
        {"role":"user","content":"你是谁？"},
    ]
    response = llm.stream(messages)
    for chunk in response:
        print(chunk.model_dump_json())

if __name__ == "__main__":
    get_response()
```

运行代码，可以得到以下结果：

```json
{"content": "", "additional_kwargs": {}, "response_metadata": {}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": null, "tool_call_chunks": []}
{"content": "我是", "additional_kwargs": {}, "response_metadata": {}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": null, "tool_call_chunks": []}
{"content": "来自", "additional_kwargs": {}, "response_metadata": {}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": null, "tool_call_chunks": []}
{"content": "阿里", "additional_kwargs": {}, "response_metadata": {}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": null, "tool_call_chunks": []}
{"content": "云", "additional_kwargs": {}, "response_metadata": {}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": null, "tool_call_chunks": []}
{"content": "的大规模语言模型", "additional_kwargs": {}, "response_metadata": {}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": null, "tool_call_chunks": []}
{"content": "，我叫通", "additional_kwargs": {}, "response_metadata": {}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": null, "tool_call_chunks": []}
{"content": "义千问。", "additional_kwargs": {}, "response_metadata": {}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": null, "tool_call_chunks": []}
{"content": "", "additional_kwargs": {}, "response_metadata": {"finish_reason": "stop"}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": null, "tool_call_chunks": []}
{"content": "", "additional_kwargs": {}, "response_metadata": {}, "type": "AIMessageChunk", "name": null, "id": "run-xxx", "example": false, "tool_calls": [], "invalid_tool_calls": [], "usage_metadata": {"input_tokens": 22, "output_tokens": 16, "total_tokens": 38}, "tool_call_chunks": []}
```

关于输入参数的配置，可以参考[输入参数配置](/zh/model-studio/compatibility-of-openai-with-dashscope#d553cbbee6mxk)，相关参数在ChatOpenAI对象中定义。

## 通过HTTP接口调用 <span id="341800c1f8h93" /> <span id="19d25ff026ru9" />

您可以通过HTTP接口来调用阿里云百炼服务，获得与通过HTTP接口调用OpenAI服务相同结构的返回结果。

### 前提条件 <span id="a762f3cf04wue" />

- 您需要开通阿里云百炼模型服务并获得API-KEY，详情请参考：获取与配置 API Key。
- 我们推荐您将API-KEY配置到环境变量中以降低API-KEY的泄露风险，配置方法可参考配置API Key到环境变量。您也可以在代码中配置API-KEY，<strong>但是泄露风险会提高</strong>。

### 提交接口调用 <span id="cd5c7c20fb74b" />

```http
北京：POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions
美国（弗吉尼亚）：POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions
新加坡：POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions
```

### 请求示例 <span id="3cf917d126qar" />

以下示例展示通过`cURL`命令来调用API的脚本。

<Note>
  如果您没有配置API-KEY为环境变量，需将\$DASHSCOPE\_API\_KEY更改为您的API-KEY\*。\*
</Note>

#### 非流式输出 <span id="4ce0604d0104l" />

```bash
# ======= 重要提示 =======
# 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
# 以下是北京地域base_url
# === 执行时请删除该注释 ===
curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY" \
--header 'Content-Type: application/json' \
--data '{
    "model": "qwen3.8-max",
    "messages": [
        {
            "role": "system",
            "content": "You are a helpful assistant."
        },
        {
            "role": "user",
            "content": "你是谁？"
        }
    ]
}'
```

运行命令可得到以下结果：

```json
{
    "choices": [
        {
            "message": {
                "role": "assistant",
                "content": "我是来自阿里云的大规模语言模型，我叫千问。"
            },
            "finish_reason": "stop",
            "index": 0,
            "logprobs": null
        }
    ],
    "object": "chat.completion",
    "usage": {
        "prompt_tokens": 11,
        "completion_tokens": 16,
        "total_tokens": 27
    },
    "created": 1715252778,
    "system_fingerprint": "",
    "model": "qwen3.8-max",
    "id": "chatcmpl-xxx"
}
```

#### 流式输出 <span id="c304107315suc" />

如果您需要使用流式输出，请在请求体中指定stream参数为true。

```bash
# ======= 重要提示 =======
# 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
# 以下是北京地域base_url
# === 执行时请删除该注释 ===
curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY" \
--header 'Content-Type: application/json' \
--data '{
    "model": "qwen3.8-max",
    "messages": [
        {
            "role": "system",
            "content": "You are a helpful assistant."
        },
        {
            "role": "user",
            "content": "你是谁？"
        }
    ],
    "stream":true
}'
```

运行命令可得到以下结果：

```json
data: {"choices":[{"delta":{"content":"","role":"assistant"},"index":0,"logprobs":null,"finish_reason":null}],"object":"chat.completion.chunk","usage":null,"created":1715931028,"system_fingerprint":null,"model":"qwen3.8-max","id":"chatcmpl-3bb05cf5cd819fbca5f0b8d67a025022"}

data: {"choices":[{"finish_reason":null,"delta":{"content":"我是"},"index":0,"logprobs":null}],"object":"chat.completion.chunk","usage":null,"created":1715931028,"system_fingerprint":null,"model":"qwen3.8-max","id":"chatcmpl-3bb05cf5cd819fbca5f0b8d67a025022"}

data: {"choices":[{"delta":{"content":"来自"},"finish_reason":null,"index":0,"logprobs":null}],"object":"chat.completion.chunk","usage":null,"created":1715931028,"system_fingerprint":null,"model":"qwen3.8-max","id":"chatcmpl-3bb05cf5cd819fbca5f0b8d67a025022"}

data: {"choices":[{"delta":{"content":"阿里"},"finish_reason":null,"index":0,"logprobs":null}],"object":"chat.completion.chunk","usage":null,"created":1715931028,"system_fingerprint":null,"model":"qwen3.8-max","id":"chatcmpl-3bb05cf5cd819fbca5f0b8d67a025022"}

data: {"choices":[{"delta":{"content":"云的大规模语言模型"},"finish_reason":null,"index":0,"logprobs":null}],"object":"chat.completion.chunk","usage":null,"created":1715931028,"system_fingerprint":null,"model":"qwen3.8-max","id":"chatcmpl-3bb05cf5cd819fbca5f0b8d67a025022"}

data: {"choices":[{"delta":{"content":"，我叫千问。"},"finish_reason":null,"index":0,"logprobs":null}],"object":"chat.completion.chunk","usage":null,"created":1715931028,"system_fingerprint":null,"model":"qwen3.8-max","id":"chatcmpl-3bb05cf5cd819fbca5f0b8d67a025022"}

data: {"choices":[{"delta":{"content":""},"finish_reason":"stop","index":0,"logprobs":null}],"object":"chat.completion.chunk","usage":null,"created":1715931028,"system_fingerprint":null,"model":"qwen3.8-max","id":"chatcmpl-3bb05cf5cd819fbca5f0b8d67a025022"}

data: [DONE]
```

输入参数的详情请参考[输入参数配置](/zh/model-studio/compatibility-of-openai-with-dashscope#d553cbbee6mxk)。

### 异常响应示例 <span id="0ea417000ckjs" />

在访问请求出错的情况下，输出的结果中会通过 code 和 message 指明出错原因。

```json
{
    "error": {
        "message": "Invalid API-key provided.",
        "type": "invalid_request_error",
        "param": null,
        "code": "invalid_api_key"
    }
}
```

## 第三方客户端配置 <span id="h2-third-party-client-config" /> <span id="third-party-client-config" />

除 SDK 与 HTTP 直接调用外，您还可以在支持 OpenAI 兼容协议的第三方大模型客户端中接入阿里云百炼。以智谱客户端为例，按以下步骤填写配置后即可发起模型调用：

1. 在客户端的服务商设置中选择<strong>自定义服务商</strong>。
2. <strong>Base URL</strong>：填写本文“兼容OpenAI需要信息”中 [BASE\_URL](/zh/model-studio/compatibility-of-openai-with-dashscope#21d628d5f82z7) 部分所在地域对应的 OpenAI SDK 形式地址，即以 `/compatible-mode/v1` 结尾、不含 `/chat/completions` 的地址。各地域的 Base URL 不同，需与 API Key 所属地域保持一致。

   例如，华北2（北京）地域填写 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`。其中，`{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的业务空间详情页面查看。原 `https://dashscope.aliyuncs.com` 域名仍可正常使用，但建议优先使用业务空间专属域名。
3. <strong>API Key</strong>：填写对应地域的百炼 API Key。您可以在百炼控制台[API Key管理页面](https://bailian.console.aliyun.com/model/settings/api-key)创建并获取 API Key。
4. <strong>模型名称</strong>：填写支持 OpenAI 兼容协议的大语言模型名称。可选模型以本文“兼容OpenAI需要信息”中[支持的模型列表](/zh/model-studio/compatibility-of-openai-with-dashscope#eadfc13038jd5)部分为准；例如 `qwen3.7-plus` 仅作为模型名称示例，不表示免费额度承诺。
5. 保存配置后发起一次对话，验证第三方客户端是否可以正常调用模型。

如果调用返回 HTTP 400，且 `error.message` 为 `current user api does not support http call`、`error.type` 为 `invalid_request_error`，说明当前填写的模型不支持通过 OpenAI 兼容接口进行 HTTP 调用。请更换为[支持的模型列表](/zh/model-studio/compatibility-of-openai-with-dashscope#eadfc13038jd5)中的模型后重试。

## 状态码说明 <span id="8dd39ae94bygm" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>错误码</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>400 - Invalid Request Error</p></td><td style={{ verticalAlign: "top" }}><p>输入请求错误，细节请参见具体报错信息。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>401 - Invalid API-key provided</p></td><td style={{ verticalAlign: "top" }}><p>API key不正确。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>429 - Rate limit reached for requests</p></td><td style={{ verticalAlign: "top" }}><p>QPS、QPM等超限。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>429 - You exceeded your current quota, please check your plan and billing details</p></td><td style={{ verticalAlign: "top" }}><p>额度超限或者欠费。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>500 - The server had an error while processing your request</p></td><td style={{ verticalAlign: "top" }}><p>服务端错误。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>503 - The engine is currently overloaded, please try again later</p></td><td style={{ verticalAlign: "top" }}><p>服务端负载过高，可重试。</p></td></tr></tbody></table>
