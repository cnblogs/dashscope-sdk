> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# DashScopeRerank

> DashScopeRerank提供阿里巴巴通义实验室开发的GTE-Rerank文本排序系列模型，开发者可以通过LlamaIndex框架进行集成高质量文本检索、排序。

<meta property="og:title" content="DashScopeRerank" />

## 开始 <span id="c5bad0170eh01" />

### 前提条件 <span id="6dbd9a309bzyv" />

- 已开通阿里云百炼服务：[产品开通](/zh/model-studio/activate-alibaba-cloud-model-studio)。
- 创建API\_KEY: [获取与配置 API Key](/zh/model-studio/get-api-key)。
- 安装DashScopeRerank的安装包 (python>=3.9,\<=3.12）

```python
pip install llama-index-core
pip install llama-index-postprocessor-dashscope-rerank
```

### 示例代码 <span id="ec23f0f77d2cx" />

设置API-KEY

```bash
export DASHSCOPE_API_KEY=YOUR_DASHSCOPE_API_KEY
```

```python
from llama_index.core.data_structs import Node
from llama_index.core.schema import NodeWithScore
from llama_index.postprocessor.dashscope_rerank import DashScopeRerank

nodes = [
    NodeWithScore(node=Node(text="text1"), score=0.7),
    NodeWithScore(node=Node(text="text2"), score=0.8),
]

dashscope_rerank = DashScopeRerank(top_n=5)
results = dashscope_rerank.postprocess_nodes(nodes, query_str="<user query>")
for res in results:
    print("Text: ", res.node.get_content(), "Score: ", res.score)
```

<strong>示例代码输出</strong>

```plaintext
Text:  text1 Score:  0.25589250620997755
Text:  text2 Score:  0.18071043165292258
```

## 参数说明 <span id="726f54c1469yc" />

### 输入参数 <span id="19986db12esqn" />

- DashScopeRerank参数说明如下

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.899761%" }} /><col style={{ width: "14.200477%" }} /><col style={{ width: "28.400955%" }} /><col style={{ width: "39.498807%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>默认值</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>model</p></td><td><p>string</p></td><td><p>gte-rerank</p></td><td><p>支持的模型列表：</p><ul><li><p>gte-rerank</p></li></ul></td></tr><tr><td><p>top\_n</p></td><td><p>int</p></td><td><p>3</p></td><td><p>排序返回的top文档数量, 如果没有指定则返回全部候选doc，如果指定的top\_n值大于输入的候选doc数量，返回全部doc</p></td></tr><tr><td><p>return\_documents</p></td><td><p>bool</p></td><td><p>False</p></td><td><p>返回的排序结果列表里面是否返回每一条document原文，默认值False</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>DashScope api key，可以通过环境变量等方法设置</p></td></tr></tbody></table>
