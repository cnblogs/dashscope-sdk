> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# DashScopeJsonNodeParser

> DashScopeJsonNodeParser提供阿里巴巴通义实验室开发的文本切分模型，开发者可以通过LlamaIndex框架对DashScopeParse的解析结果进行文档切分。

<meta property="og:title" content="DashScopeJsonNodeParser" />

## 开始 <span id="c5bad0170eh01" />

### 前提条件 <span id="6dbd9a309bzyv" />

首先，登录[https://bailian.console.aliyun.com/](https://bailian.console.aliyun.com/)，获取你的API-KEY，当需要指定业务空间时也要获取指定“业务空间id”。

- 已创建API-KEY: [获取与配置 API Key](/zh/model-studio/get-api-key)。
- 已创建业务空间：[业务空间管理](/zh/model-studio/use-workspace)。

然后，安装DashScopeJsonNodeParser的安装包（python>=3.9,\<=3.12）

```python
pip install llama-index-core
pip install llama-index-node-parser-dashscope
```

### 示例代码 <span id="ec23f0f77d2cx" />

```python expandable
import json
import os
from llama_index.node_parser.dashscope import (
    DashScopeJsonNodeParser,
)
from llama_index.core.ingestion import IngestionPipeline
from llama_index.core.schema import Document

# Set your Dashscope API key in the environment
os.environ["DASHSCOPE_API_KEY"] = "your_api_key_here"

documents = [
    # Prepare your documents obtained from the Dashscope reader
]

# Initialize the DashScope JsonNodeParser
node_parser = DashScopeJsonNodeParser(
    chunk_size=100, overlap_size=0, separator=" |,|，|。|？|！|\n|\?|\!"
)

# Set up the ingestion pipeline with the node parser
pipeline = IngestionPipeline(transformations=[node_parser])

# Process the documents and print the resulting nodes
nodes = pipeline.run(documents=documents, show_progress=True)
for node in nodes:
    print(node)
```

<strong>Note：</strong>

- DashScopeJsonNodeParser主要用于配合DashScopeParse使用

### 参数说明 <span id="51f7c1ca00zz0" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><tbody><tr><td><p>参数值</p></td><td><p>默认值</p></td><td><p>说明</p></td></tr><tr><td><p>try\_count\_limit</p></td><td><p>10</p></td><td><p>最大重试尝试次数</p></td></tr><tr><td><p>chunk\_size</p></td><td><p>500</p></td><td><p>每次处理的块大小</p></td></tr><tr><td><p>overlap\_size</p></td><td><p>100</p></td><td><p>连续块之间的重叠大小</p></td></tr><tr><td><p>separator</p></td><td><p>" |,|，|。|？|！|\n|\\?|\\!"</p></td><td><p>用于分割文本的分隔符字符</p></td></tr><tr><td><p>language</p></td><td><p>cn</p></td><td><p>词法分析器的语言，接受"cn"（中文），"en"（英文），"any"（任意）。注意，任意模式将会比较慢</p></td></tr></tbody></table>
