> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 使用百炼大模型

> 本文介绍如何基于LlamaIndex框架，调用百炼平台提供的大模型。

<meta property="og:title" content="使用百炼大模型" />

## 前提条件 <span id="a78a19d952plg" />

- 您已开通百炼服务并获得API-KEY， 请参考[获取与配置 API Key](/zh/model-studio/get-api-key)。
- 已导入 API-KEY，请参考[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## OPENAILIKE 方式调用 <span id="f7f8039703ke7" />

LlamIndex 的 OpenAI-like 封装可以用于调用百炼支持 [文本生成](/zh/model-studio/qwen-api-reference)的大模型。完整列表请参考：[OpenAI 兼容模式支持的模型列表](/zh/model-studio/compatibility-of-openai-with-dashscope#eadfc13038jd5)。调用费用、输入输出上限等请参考：[选择模型](/zh/model-studio/models)。

使用前需要安装 LlamaIndex 核心组件和 OpenAI Like：

```bash
pip install llama-index-core
pip install llama-index-llms-openai-like
```

### 修改全局设置（可选） <span id="b5631e462csyy" />

```python
import os
from llama_index.core import Settings
from llama_index.llms.openai_like import OpenAILike

# LlamaIndex默认使用的大模型被替换为百炼
Settings.llm = OpenAILike(
    model="qwen-plus",
    api_base="https://dashscope.aliyuncs.com/compatible-mode/v1",
    api_key=os.getenv("DASHSCOPE_API_KEY"),
    is_chat_model=True
)
```

### 大模型调用示例 <span id="323e80e83f4x3" />

```python
import os
from llama_index.llms.openai_like import OpenAILike

# 所调用的模型
llm = OpenAILike(
    model="qwen-plus",
    api_base="https://dashscope.aliyuncs.com/compatible-mode/v1",
    api_key=os.getenv("DASHSCOPE_API_KEY"),
    is_chat_model=True
)

response = llm.complete("帮我推荐一下江浙沪5天的旅游攻略。")
print(response)
```

详细介绍以及完整的 API Reference 请前往 LlamaIndex 官方的 [OpenAILike API Reference](https://docs.llamaindex.ai/en/stable/api_reference/llms/openai_like/)。

## DashScope 方式调用 <span id="1cb6f7fcb2ahm" />

支持百炼所有的文本生成模型，完整列表与调用费用请参考：[选择模型](/zh/model-studio/models)。（也支持[部署](/zh/model-studio/model-deployment-introduction)后的模型）

使用前需要安装 LlamaIndex 核心组件和 DashScopeLLM：

```bash
pip install llama-index-core
pip install llama-index-llms-dashscope
```

### 修改全局设置（可选） <span id="80f68552e3f46" />

```python
import os
from llama_index.core import Settings
from llama_index.llms.dashscope import DashScope

# LlamaIndex默认使用的大模型被替换为百炼
Settings.llm = DashScope(model_name="qwen-plus", api_key=os.getenv("DASHSCOPE_API_KEY"))
```

### 大模型调用示例 <span id="cbb52ff8f5iat" />

```python
import os
from llama_index.llms.dashscope import DashScope

llm = DashScope(model_name="qwen-plus", api_key=os.getenv("DASHSCOPE_API_KEY"))
response = llm.complete("帮我推荐一下江浙沪5天的旅游攻略。")
print(response)
```

详细介绍与更多示例请前往 LlamaIndex 官方的 [DashScope LLM Examples](https://docs.llamaindex.ai/en/stable/examples/llm/dashscope/)，完整的 API Reference 请前往 LlamaIndex 官方的 [DashScope API Reference](https://docs.llamaindex.ai/en/stable/api_reference/llms/dashscope/)。
