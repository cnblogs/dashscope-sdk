> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 意图理解能力

> 千问的意图理解模型能够在百毫秒级时间内快速、准确地解析用户意图，并选择合适的工具来解决用户的问题。

<meta property="og:title" content="意图理解能力" />

<Tip>
  阿里云百炼为华北2（北京）、新加坡、中国香港地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## 支持的模型 <span id="ae5345ebfbj0z" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.928675%" }} /><col style={{ width: "11.499272%" }} /><col style={{ width: "11.353712%" }} /><col style={{ width: "10.77147%" }} /><col style={{ width: "11.499272%" }} /><col style={{ width: "11.499272%" }} /><col style={{ width: "16.448326%" }} /></colgroup><thead><tr><th rowSpan={2}><p><strong>模型名称</strong></p></th><th><p><strong>上下文长度</strong></p></th><th><p><strong>最大输入</strong></p></th><th><p><strong>最大输出</strong></p></th><th><p><strong>输入成本</strong></p></th><th><p><strong>输出成本</strong></p></th><th rowSpan={2}><p><strong>免费额度</strong></p> <p><a href="/zh/model-studio/new-free-quota#977b13081ab56">（注）</a></p></th></tr><tr><th colSpan={3}><p><strong>（Token数）</strong></p></th><th colSpan={2}><p><strong>（每百万Token）</strong></p></th></tr></thead><tbody><tr><td><p>tongyi-intent-detect-v3</p></td><td><p>8,192</p></td><td><p>8,192</p></td><td><p>1,024</p></td><td><p>0.4元</p></td><td><p>1元</p></td><td><p>100万Token</p> <p>有效期：百炼开通后90天内</p></td></tr></tbody></table>

## 使用方法 <span id="3bdf07a4fazr8" />

### 前提条件 <span id="78be117a7bjxg" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。如果通过OpenAI SDK或DashScope SDK进行调用，还需要[安装SDK](/zh/model-studio/install-sdk)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

### 同时输出意图与函数调用信息 <span id="b1da224bc8g6r" />

为了使意图理解模型可以同时输出意图与函数调用信息，您需要按照以下方式设置System Message：

```plaintext
You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:
{工具信息}
Response in INTENT_MODE.
```

您需要在System Message中说明`Response in INTENT_MODE.`并且放入可能使用到的工具信息。工具信息的格式为：

```json expandable
[{
    "name": "工具1的名称",
    "description": "工具1的描述",
    "parameters": {
        "type": "参数的类型，一般为object",
        "properties": {
            "parameter_1": {
                "description": "parameter_1的描述",
                "type": "parameter_1的类型",
                "default": "parameter_1的默认值"
            },
            ...
            "parameter_n": {
                "description": "parameter_n的描述",
                "type": "parameter_n的类型",
                "default": "parameter_n的默认值"
            }
        },
        "required": [
        "parameter_1",
        ...
        "parameter_n"
    ]
    },
},
...
{
    "name": "工具n的名称",
    "description": "工具n的描述",
    "parameters": {
        "type": "参数的类型，一般为object",
        "properties": {
            "parameter_1": {
                "description": "parameter_1的描述",
                "type": "parameter_1的类型",
                "default": "parameter_1的默认值"
            },
            ...
            "parameter_n": {
                "description": "parameter_n的描述",
                "type": "parameter_n的类型",
                "default": "parameter_n的默认值"
            }
        },
        "required": [
        "parameter_1",
        ...
        "parameter_n"
    ]
    },
}]
```

假设您的业务场景需要使用时间查询与天气查询两个工具，工具信息为：

```json
[
    {
        "name": "get_current_time",
        "description": "当你想知道现在的时间时非常有用。",
        "parameters": {}
    },
    {
        "name": "get_current_weather",
        "description": "当你想查询指定城市的天气时非常有用。",
        "parameters": {
            "type": "object",
            "properties": {
                "location": {
                    "type": "string",
                    "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                }
            },
            "required": ["location"]
        }
    }
]
```

#### 请求示例 <span id="bfc1fbb3fcnea" />

<CodeGroup>
  ```python OpenAI兼容 expandable
  import os
  import json
  from openai import OpenAI

  # 定义工具
  tools = [
      {
          "name": "get_current_time",
          "description": "当你想知道现在的时间时非常有用。",
          "parameters": {}
      },
      {
          "name": "get_current_weather",
          "description": "当你想查询指定城市的天气时非常有用。",
          "parameters": {
              "type": "object",
              "properties": {
                  "location": {
                      "type": "string",
                      "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                  }
              },
              "required": ["location"]
          }
      }
  ]

  tools_string = json.dumps(tools,ensure_ascii=False)

  system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:
  {tools_string}
  Response in INTENT_MODE."""
  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )
  messages = [
      {'role': 'system', 'content': system_prompt},
      {'role': 'user', 'content': "杭州天气"}
      ]
  response = client.chat.completions.create(
      model="tongyi-intent-detect-v3",
      messages=messages
  )

  print(response.choices[0].message.content)
  ```

  ```python DashScope expandable
  import os
  import json
  from dashscope import Generation
  # 以下为华北2（北京）地域的配置，调用时请将WorkspaceId替换为真实的业务空间ID，各地域的配置不同。
  dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

  # 定义工具
  tools = [
      {
          "name": "get_current_time",
          "description": "当你想知道现在的时间时非常有用。",
          "parameters": {}
      },
      {
          "name": "get_current_weather",
          "description": "当你想查询指定城市的天气时非常有用。",
          "parameters": {
              "type": "object",
              "properties": {
                  "location": {
                      "type": "string",
                      "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                  }
              },
              "required": ["location"]
          }
      }
  ]

  tools_string = json.dumps(tools,ensure_ascii=False)

  system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:
  {tools_string}
  Response in INTENT_MODE."""

  messages = [
      {'role': 'system', 'content': system_prompt},
      {'role': 'user', 'content': "杭州天气"}
      ]
  response = Generation.call(
      # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      model="tongyi-intent-detect-v3",
      messages=messages,
      result_format="message"
  )

  print(response.output.choices[0].message.content)
  ```
</CodeGroup>

<Note>
  dashscope CLI 暂不支持 understanding 子命令，请使用 Python SDK：

  ```python
  from dashscope import Understanding
  Understanding.call(model='opennlu-v1', sentence='今天天气真好', labels='天气，情感')
  ```
</Note>

<Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>

#### 响应示例 <span id="19dd4c3c9a0a1" />

```plaintext
<tags>
[function call, json response]
</tags><tool_call>
[{"name": "get_current_weather", "arguments": {"location": "杭州市"}}]
</tool_call><content>

</content>
```

在得到响应后，您需要使用`parse_text`函数解析出返回的工具与参数信息：

```python expandable
import re
import json

def parse_text(text):
    # 定义正则表达式模式来匹配 <tags>, <tool_call>, <content> 及其内容
    tags_pattern = r'<tags>(.*?)</tags>'
    tool_call_pattern = r'<tool_call>(.*?)</tool_call>'
    content_pattern = r'<content>(.*?)</content>'
    # 使用正则表达式查找匹配的内容
    tags_match = re.search(tags_pattern, text, re.DOTALL)
    tool_call_match = re.search(tool_call_pattern, text, re.DOTALL)
    content_match = re.search(content_pattern, text, re.DOTALL)
    # 提取匹配的内容，如果没有匹配到则返回空字符串
    tags = tags_match.group(1).strip() if tags_match else ""
    tool_call_str = tool_call_match.group(1).strip() if tool_call_match else ""
    tool_call = json.loads(tool_call_str) if tool_call_str else []
    content = content_match.group(1).strip() if content_match else ""
    # 将提取的内容存储在字典中
    result = {
      "tags": tags,
      "tool_call": tool_call,
      "content": content
    }
    return result

response = """<tags>
[function call, json response]
</tags><tool_call>
[{"name": "get_current_weather", "arguments": {"location": "杭州市"}}]
</tool_call><content>

</content>"""
print(parse_text(response))
```

得到输出为：

```json
{
    "tags": "[function call, json response]",
    "tool_call": [
        {
            "name": "get_current_weather",
            "arguments": {
                "location": "杭州市"
            }
        }
    ],
    "content": ""
}
```

### 只输出意图信息 <span id="c737e9d306g7p" />

为了使意图理解模型只输出意图信息，您需要按照以下方式设置System Message：

```plaintext
You are Qwen, created by Alibaba Cloud. You are a helpful assistant. \nYou should choose one tag from the tag list:\n{意图信息}\njust reply with the chosen tag.
```

意图信息的格式为：

```json
{
    "意图1": "意图1的描述",
    "意图2": "意图2的描述",
    "意图3": "意图3的描述",
    ...
}
```

#### 请求示例 <span id="151a183929htr" />

<CodeGroup>
  ```python OpenAI兼容 expandable
  import os
  import json
  from openai import OpenAI

  intent_dict = {
      "play_game": "玩游戏",
      "email_querycontact": "电子邮件查询联系人",
      "general_quirky": "quirky",
      "email_addcontact": "电子邮件添加联系人",
      "takeaway_query": "外卖查询",
      "recommendation_locations": "地点推荐",
      "transport_traffic": "交通运输",
      "iot_cleaning": "物联网-吸尘器, 清洁器",
      "general_joke": "笑话",
      "lists_query": "查询列表/清单",
      "calendar_remove": "日历删除事件",
      "transport_taxi": "打车, 出租车预约",
      "qa_factoid": "事实性问答",
      "transport_ticket": "交通票据",
      "play_radio": "播放广播",
      "alarm_set": "设置闹钟",
  }

  intent_string = json.dumps(intent_dict,ensure_ascii=False)

  system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant.
  You should choose one tag from the tag list:
  {intent_string}
  Just reply with the chosen tag."""

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )
  messages = [
      {'role': 'system', 'content': system_prompt},
      {'role': 'user', 'content': "星期五早上九点叫醒我"}
      ]
  response = client.chat.completions.create(
      model="tongyi-intent-detect-v3",
      messages=messages
  )

  print(response.choices[0].message.content)
  ```

  ```python DashScope expandable
  import os
  import json
  from dashscope import Generation
  # 以下为华北2（北京）地域的配置，调用时请将WorkspaceId替换为真实的业务空间ID，各地域的配置不同。
  dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

  intent_dict = {
      "play_game": "玩游戏",
      "email_querycontact": "电子邮件查询联系人",
      "general_quirky": "quirky",
      "email_addcontact": "电子邮件添加联系人",
      "takeaway_query": "外卖查询",
      "recommendation_locations": "地点推荐",
      "transport_traffic": "交通运输",
      "iot_cleaning": "物联网-吸尘器, 清洁器",
      "general_joke": "笑话",
      "lists_query": "查询列表/清单",
      "calendar_remove": "日历删除事件",
      "transport_taxi": "打车, 出租车预约",
      "qa_factoid": "事实性问答",
      "transport_ticket": "交通票据",
      "play_radio": "播放广播",
      "alarm_set": "设置闹钟",
  }

  intent_string = json.dumps(intent_dict,ensure_ascii=False)

  system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant.
  You should choose one tag from the tag list:
  {intent_string}
  Just reply with the chosen tag."""

  messages = [
      {'role': 'system', 'content': system_prompt},
      {'role': 'user', 'content': "周五早上九点叫醒我"}
      ]
  response = Generation.call(
      # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      model="tongyi-intent-detect-v3",
      messages=messages,
      result_format="message"
  )

  print(response.output.choices[0].message.content)
  ```
</CodeGroup>

#### 响应示例 <span id="91ae1892a9qis" />

```plaintext
alarm_set
```

#### 提升意图识别的响应速度 <span id="dc13b1d5d8wgk" />

为了提升意图识别的响应速度，您可以将意图的分类种类用一个简单的大写字母进行指代，意图识别响应结果将始终为一个 Token，这可以优化模型调用的响应时间。

<CodeGroup>
  ```python OpenAI兼容 expandable
  import os
  import json
  from openai import OpenAI

  intent_dict = {
      "A": "玩游戏",
      "B": "电子邮件查询联系人",
      "C": "quirky",
      "D": "电子邮件添加联系人",
      "E": "外卖查询",
      "F": "地点推荐",
      "G": "交通运输",
      "H": "物联网-吸尘器, 清洁器",
      "I": "笑话",
      "J": "查询列表/清单",
      "K": "日历删除事件",
      "L": "打车, 出租车预约",
      "M": "事实性问答",
      "N": "交通票据",
      "O": "播放广播",
      "P": "设置闹钟",
  }

  intent_string = json.dumps(intent_dict, ensure_ascii=False)

  system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant.
  You should choose one tag from the tag list:
  {intent_string}
  Just reply with the chosen tag."""

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )
  messages = [
      {"role": "system", "content": system_prompt},
      {"role": "user", "content": "从北京去杭州最早的飞机是？"},
  ]
  response = client.chat.completions.create(
      model="tongyi-intent-detect-v3", messages=messages
  )

  print(response.choices[0].message.content)
  ```

  ```python DashScope expandable
  import os
  import json
  from dashscope import Generation
  # 以下为华北2（北京）地域的配置，调用时请将WorkspaceId替换为真实的业务空间ID，各地域的配置不同。
  dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

  intent_dict = {
      "A": "玩游戏",
      "B": "电子邮件查询联系人",
      "C": "quirky",
      "D": "电子邮件添加联系人",
      "E": "外卖查询",
      "F": "地点推荐",
      "G": "交通运输",
      "H": "物联网-吸尘器, 清洁器",
      "I": "笑话",
      "J": "查询列表/清单",
      "K": "日历删除事件",
      "L": "打车, 出租车预约",
      "M": "事实性问答",
      "N": "交通票据",
      "O": "播放广播",
      "P": "设置闹钟",
  }

  intent_string = json.dumps(intent_dict, ensure_ascii=False)

  system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant.
  You should choose one tag from the tag list:
  {intent_string}
  Just reply with the chosen tag."""

  messages = [
      {"role": "system", "content": system_prompt},
      {"role": "user", "content": "从北京去杭州最早的飞机是？"},
  ]
  response = Generation.call(
      # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      model="tongyi-intent-detect-v3",
      messages=messages,
      result_format="message",
  )

  print(response.output.choices[0].message.content)
  ```
</CodeGroup>

运行代码后可以得到一个 Token 的意图分类结果。

```plaintext
M
```

### 只输出函数调用信息 <span id="9ad8433299t2i" />

为了使意图理解模型只输出函数调用信息，您需要按照以下方式设置System Message：

```plaintext
You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:\n{工具信息}\nResponse in NORMAL_MODE.
```

其中工具信息与[同时输出意图与函数调用信息](/zh/model-studio/intent-detect-capability#b1da224bc8g6r)中的工具信息格式相同。

#### 请求示例 <span id="386bb5a8420m7" />

<CodeGroup>
  ```python OpenAI兼容 expandable
  import os
  import json
  from openai import OpenAI

  # 定义工具
  tools = [
      {
          "name": "get_current_time",
          "description": "当你想知道现在的时间时非常有用。",
          "parameters": {}
      },
      {
          "name": "get_current_weather",
          "description": "当你想查询指定城市的天气时非常有用。",
          "parameters": {
              "type": "object",
              "properties": {
                  "location": {
                      "type": "string",
                      "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                  }
              },
              "required": ["location"]
          }
      }
  ]

  tools_string = json.dumps(tools,ensure_ascii=False)

  system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:
  {tools_string}
  Response in NORMAL_MODE."""
  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )
  messages = [
      {'role': 'system', 'content': system_prompt},
      {'role': 'user', 'content': "杭州天气"}
      ]
  response = client.chat.completions.create(
      model="tongyi-intent-detect-v3",
      messages=messages
  )

  print(response.choices[0].message.content)
  ```

  ```python DashScope expandable
  import os
  import json
  from dashscope import Generation
  # 以下为华北2（北京）地域的配置，调用时请将WorkspaceId替换为真实的业务空间ID，各地域的配置不同。
  dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

  # 定义工具
  tools = [
      {
          "name": "get_current_time",
          "description": "当你想知道现在的时间时非常有用。",
          "parameters": {}
      },
      {
          "name": "get_current_weather",
          "description": "当你想查询指定城市的天气时非常有用。",
          "parameters": {
              "type": "object",
              "properties": {
                  "location": {
                      "type": "string",
                      "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                  }
              },
              "required": ["location"]
          }
      }
  ]

  tools_string = json.dumps(tools,ensure_ascii=False)

  system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:
  {tools_string}
  Response in NORMAL_MODE."""

  messages = [
      {'role': 'system', 'content': system_prompt},
      {'role': 'user', 'content': "杭州天气"}
      ]
  response = Generation.call(
      # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      model="tongyi-intent-detect-v3",
      messages=messages,
      result_format="message"
  )

  print(response.output.choices[0].message.content)
  ```
</CodeGroup>

#### 响应示例 <span id="68fdd12a024ek" />

```plaintext
<tool_call>
{"name": "get_current_weather", "arguments": {"location": "杭州市"}}
</tool_call>
```

在得到响应后，您需要使用`parse_text`函数解析出返回的工具与参数信息：

```python
import re

def parse_text(text):
    tool_call_pattern = r'<tool_call>(.*?)</tool_call>'
    # 使用正则表达式查找匹配的内容
    tool_call_match = re.search(tool_call_pattern, text, re.DOTALL)
    # 提取匹配的内容，如果没有匹配到则返回空字符串
    tool_call = tool_call_match.group(1).strip() if tool_call_match else ""
    return tool_call

response = """<tool_call>
{"name": "get_current_weather", "arguments": {"location": "杭州市"}}
</tool_call>"""
print(parse_text(response))
```

得到输出为：

```json
{"name": "get_current_weather", "arguments": {"location": "杭州市"}}
```

### 多轮对话 <span id="060f9865fcsu7" />

如果用户在提问时未提供充足的信息，意图理解模型会进行反问，通过多轮对话采集到必要的参数后，再输出函数调用的信息。

<Tabs>
  <Tab title="同时输出意图与函数调用信息">
    ##### 请求示例 <span id="9c7eb415fb0h3" />

    <CodeGroup>
      ```python OpenAI 兼容 expandable
      import os
      import json
      from openai import OpenAI

      # 定义工具
      tools = [
          {
              "name": "get_current_time",
              "description": "当你想知道现在的时间时非常有用。",
              "parameters": {},
          },
          {
              "name": "get_current_weather",
              "description": "当你想查询指定城市的天气时非常有用。",
              "parameters": {
                  "type": "object",
                  "properties": {
                      "location": {
                          "type": "string",
                          "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                      }
                  },
                  "required": ["location"],
              },
          },
      ]

      tools_string = json.dumps(tools, ensure_ascii=False)

      system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:
      {tools_string}
      Response in INTENT_MODE."""
      client = OpenAI(
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
      )
      messages = [
          {"role": "system", "content": system_prompt},
          # 第一轮对话提出的问题
          {"role": "user", "content": "我想查天气"},
      ]
      response = client.chat.completions.create(
          model="tongyi-intent-detect-v3", messages=messages
      )

      print("查询问题：我想查天气")
      print("第一轮输出：\n")
      print(response.choices[0].message.content)
      messages.append(response.choices[0].message)
      # 第二轮对话提出的问题
      messages.append({"role": "user", "content": "杭州的"})
      response = client.chat.completions.create(
          model="tongyi-intent-detect-v3", messages=messages
      )
      print("\n查询问题：杭州的")
      print("第二轮输出：\n")
      print(response.choices[0].message.content)
      ```

      ```python DashScope expandable
      import os
      import json
      from dashscope import Generation
      # 以下为华北2（北京）地域的配置，调用时请将WorkspaceId替换为真实的业务空间ID，各地域的配置不同。
      dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

      # 定义工具
      tools = [
          {
              "name": "get_current_time",
              "description": "当你想知道现在的时间时非常有用。",
              "parameters": {},
          },
          {
              "name": "get_current_weather",
              "description": "当你想查询指定城市的天气时非常有用。",
              "parameters": {
                  "type": "object",
                  "properties": {
                      "location": {
                          "type": "string",
                          "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                      }
                  },
                  "required": ["location"],
              },
          },
      ]

      tools_string = json.dumps(tools, ensure_ascii=False)

      system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:
      {tools_string}
      Response in INTENT_MODE."""

      messages = [
          {"role": "system", "content": system_prompt},
          # 第一轮对话提出的问题
          {"role": "user", "content": "我想查天气"},
      ]
      response = Generation.call(
          # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          model="tongyi-intent-detect-v3",
          messages=messages,
          result_format="message",
      )
      print("查询问题：我想查天气")
      print("第一轮输出：\n")
      print(response.output.choices[0].message.content)

      messages.append(
          {"role": "assistant", "content": response.output.choices[0].message.content}
      )
      # 第二轮对话提出的问题
      messages.append({"role": "user", "content": "杭州"})

      response = Generation.call(
          # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          model="tongyi-intent-detect-v3",
          messages=messages,
          result_format="message",
      )
      print("\n查询问题：杭州")
      print("第二轮输出：\n")
      print(response.output.choices[0].message.content)
      ```
    </CodeGroup>

    ##### 响应示例 <span id="b7e4fab876lif" />

    ```plaintext
    查询问题：我想查天气
    第一轮输出：

    <tags>
    [weather inquiry]
    </tags><tool_call>
    []
    </tool_call><content>
    好的，请问您想查询哪个城市的天气呢？
    </content>

    查询问题：杭州
    第二轮输出：

    <tags>
    [function call, json response]
    </tags><tool_call>
    [{"name": "get_current_weather", "arguments": {"location": "杭州"}}]
    </tool_call><content>

    </content>
    ```
  </Tab>

  <Tab title="只输出函数调用信息">
    ##### 请求示例 <span id="925030c814ijt" />

    <CodeGroup>
      ```python OpenAI兼容 expandable
      import os
      import json
      from openai import OpenAI

      # 定义工具
      tools = [
          {
              "name": "get_current_time",
              "description": "当你想知道现在的时间时非常有用。",
              "parameters": {},
          },
          {
              "name": "get_current_weather",
              "description": "当你想查询指定城市的天气时非常有用。",
              "parameters": {
                  "type": "object",
                  "properties": {
                      "location": {
                          "type": "string",
                          "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                      }
                  },
                  "required": ["location"],
              },
          },
      ]

      tools_string = json.dumps(tools, ensure_ascii=False)

      system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:
      {tools_string}
      Response in NORMAL_MODE."""
      client = OpenAI(
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
      )
      messages = [
          {"role": "system", "content": system_prompt},
          {"role": "user", "content": "我想查天气"},
      ]
      response = client.chat.completions.create(
          model="tongyi-intent-detect-v3", messages=messages
      )

      messages.append(response.choices[0].message)
      print("查询问题：我想查天气")
      print("第一轮输出：\n")
      print(response.choices[0].message.content)
      messages.append({"role": "user", "content": "杭州"})
      response = client.chat.completions.create(
          model="tongyi-intent-detect-v3", messages=messages
      )
      print("\n查询问题：杭州")
      print("第二轮输出：\n")
      print(response.choices[0].message.content)
      ```

      ```python DashScope expandable
      import os
      import json
      from dashscope import Generation
      # 以下为华北2（北京）地域的配置，调用时请将WorkspaceId替换为真实的业务空间ID，各地域的配置不同。
      dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

      # 定义工具
      tools = [
          {
              "name": "get_current_time",
              "description": "当你想知道现在的时间时非常有用。",
              "parameters": {},
          },
          {
              "name": "get_current_weather",
              "description": "当你想查询指定城市的天气时非常有用。",
              "parameters": {
                  "type": "object",
                  "properties": {
                      "location": {
                          "type": "string",
                          "description": "城市或县区，比如北京市、杭州市、余杭区等。",
                      }
                  },
                  "required": ["location"],
              },
          },
      ]

      tools_string = json.dumps(tools, ensure_ascii=False)

      system_prompt = f"""You are Qwen, created by Alibaba Cloud. You are a helpful assistant. You may call one or more tools to assist with the user query. The tools you can use are as follows:
      {tools_string}
      Response in NORMAL_MODE."""

      messages = [
          {"role": "system", "content": system_prompt},
          {"role": "user", "content": "我想查天气"},
      ]
      response = Generation.call(
          # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          model="tongyi-intent-detect-v3",
          messages=messages,
          result_format="message",
      )
      print("查询问题：我想查天气")
      print("第一轮输出：\n")
      print(response.output.choices[0].message.content)
      messages.append(
          {"role": "assistant", "content": response.output.choices[0].message.content}
      )
      messages.append({"role": "user", "content": "杭州"})
      response = Generation.call(
          # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx",
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          model="tongyi-intent-detect-v3",
          messages=messages,
          result_format="message",
      )
      print("\n查询问题：杭州")
      print("第二轮输出：\n")
      print(response.output.choices[0].message.content)
      ```
    </CodeGroup>

    ##### 响应示例 <span id="86d54964b9oah" />

    ```plaintext
    查询问题：我想查天气
    第一轮输出：

    请问您想查询哪个城市的天气呢？

    查询问题：杭州
    第二轮输出：

    <tool_call>
    {"name": "get_current_weather", "arguments": {"location": "杭州"}}
    </tool_call>
    ```
  </Tab>
</Tabs>

## 常见问题 <span id="40b9b15ab75he" />

### Q：最多传入几个工具？ <span id="59775ce0e8b3h" />

A：我们建议您传入不超过10个的工具，否则模型调用工具的准确率可能会降低。
