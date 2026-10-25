> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# DashScopeCloudRetriever

> DashScopeCloudRetriever是百炼提供的检索增强服务管理SDK。使用该工具可以便捷地通过LlamaIndex框架初始化LlamaIndex检索器。

<meta property="og:title" content="DashScopeCloudRetriever" />

## 开始 <span id="c5bad0170eh01" />

### 前提条件 <span id="6dbd9a309bzyv" />

首先，登录[https://bailian.console.aliyun.com/](https://bailian.console.aliyun.com/)，获取你的API-KEY，当需要指定业务空间时也要获取指定“业务空间id”。

- 已创建API-KEY: [获取与配置 API Key](/zh/model-studio/get-api-key)。
- 已创建业务空间：[业务空间管理](/zh/model-studio/use-workspace)。

然后，安装DashScopeCloudIndex的安装包（python>=3.9,\<=3.12）

```python
pip install llama-index-core
pip install llama-index-indices-managed-dashscope
```

```python
import os
from llama_index.indices.managed.dashscope.retriever import DashScopeCloudRetriever

os.environ["DASHSCOPE_API_KEY"] = "your_api_key_here"
os.environ["DASHSCOPE_WORKSPACE_ID"] = "your_workspace_here"

retriever = DashScopeCloudRetriever("your index name")
nodes = retriever.retrieve("test query")
print(nodes)
```

## 参数说明 <span id="726f54c1469yc" />

### 输入参数 <span id="19986db12esqn" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "21.599045%" }} /><col style={{ width: "13.603819%" }} /><col style={{ width: "25.298329%" }} /><col style={{ width: "39.498807%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>默认值</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>index\_name</p></td><td><p>str</p></td><td><p>None</p></td><td><p>index\_name名</p></td></tr><tr><td><p>dense\_similarity\_top\_k</p></td><td><p>int</p></td><td><p>100</p></td><td><p>向量检索召回数量</p></td></tr><tr><td><p>sparse\_similarity\_top\_k</p></td><td><p>int</p></td><td><p>100</p></td><td><p>文本检索召回数量</p></td></tr><tr><td><p>enable\_reranking</p></td><td><p>bool</p></td><td><p>True</p></td><td><p>是否使用排序模型重排序</p></td></tr><tr><td><p>rerank\_model\_name</p></td><td><p>str</p></td><td><p>gte-rerank-hybrid</p></td><td><p>排序模型名称，支持的模型列表：</p><ul><li><p>gte-rerank-hybrid</p></li><li><p>gte-rerank</p></li></ul></td></tr><tr><td><p>rerank\_min\_score</p></td><td><p>float</p></td><td><p>0.0</p></td><td><p>retriever node分数过滤阈值。retriever仅返回高于rerank\_min\_score的node（该参数仅在enable\_reranking=True时生效）</p></td></tr><tr><td><p>rerank\_top\_n</p></td><td><p>int</p></td><td><p>5</p></td><td><p>retriever返回的节点数量,如果指定的top\_n值大于输入的候选节点数量，返回全部节点</p></td></tr><tr><td><p>workspace\_id</p></td><td><p>str</p></td><td><p>None</p></td><td><p>DashScope worksparce id，可以通过环境变量等方法设置</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>DashScope api key，可以通过环境变量等方法设置</p></td></tr></tbody></table>
