> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# DashScopeParse

> DashScopeParse是专门用于高效地解析和表示文件的解析器，以便通过LlamaIndex框架进行高效检索和上下文增强。目前使用的解析服务来自于阿里云 “文档智能（Document Mind）” 。

<meta property="og:title" content="DashScopeParse" />

## 开始 <span id="c5bad0170eh01" />

### 前提条件 <span id="6dbd9a309bzyv" />

首先，登录[https://bailian.console.aliyun.com/](https://bailian.console.aliyun.com/)，获取你的API-KEY，当需要指定业务空间时也要获取指定“业务空间id”。

- 已创建API-KEY：[获取与配置 API Key](/zh/model-studio/get-api-key)。
- 已创建业务空间：[业务空间管理](/zh/model-studio/use-workspace)。

然后，安装DashScopeParse的安装包（python>=3.9,\<=3.12）

```python
pip install llama-index-core
pip install llama-index-readers-dashscope
```

### 示例代码 <span id="ec23f0f77d2cx" />

现在你可以使用下面的代码获取PDF文件的解析结果了：

```python
import os

os.environ['DASHSCOPE_API_KEY'] = "<Your API Key>"
os.environ['DASHSCOPE_WORKSPACE_ID'] = "<Your Workspace id, Default workspace is empty.>"

from llama_index.readers.dashscope.base import DashScopeParse
from llama_index.readers.dashscope.utils import ResultType

file = ['aiayn.pdf', 'not_exist.pdf']
parse = DashScopeParse(result_type=ResultType.DASHSCOPE_DOCMIND, category_id="<category id>")
documents = parse.load_data(file_path=file)
```

<strong>Note：</strong>

- 目前只支持.doc、.docx、.pdf，大小在100M以内且文件页数在1000以内的文件上传、解析。

<strong>Note：</strong>

- DASHSCOPE\_API\_KEY：是必须指定的API-KEY。
- DASHSCOPE\_WORKSPACE\_ID：如果使用默认业务空间，可以不指定。如果需要指定业务空间，请指定“业务空间id”。

### 返回结果 <span id="8744c5c1b7d30" />

结果为Documents对象，json化内容为：（只示例重点字段，其中text字段由于长度过长，所以做了省略显示）

```json
[
    {
        "id_": "doc_ab3d344971084f288248d69ba9bada1310022601",
        "metadata": {
            "parse_fmt_type": "DASHSCOPE_DOCMIND"
        },
        "text": "\"{\\\"logics\\\":{\\\"docTree\\\":[{\\\"backlink\\\":{\\\"\u4e0a\u7ea7\\\":[\\\"ROOT\\\"]},\\\"level\\\":0,\\\"link\\\":{\\\"\u4e0b\u7ea7\\\":[],\\\"\u5305\u542b\\\":[]},\\\"uniqueId\\\":\\\"171c391e5e7c68a253eb0adf59fa476d\\\"},{\\\"backlink\\\":{\\\"\u4e0a\u7ea7\\\":[\\\"171c391e5e7c68a253eb0adf59fa476d\\\"]},\\\"level\\\":1,\\\"link\\\":{\\\"\u4e0b\u7ea7\\\":[],\\\"\u5305\u542b\\\":[]},\\\"uniqueId\\\":\\\"397257823ee2878fc4b4a5759fe9afe9\\\"},{\\\"backlink\\\":{\\\"\u4e0a\u7ea7\\\":[\\\"ROOT\\\"]},\\\"level\\\":0,\\\"link\\\":{\\\"\u4e0b\u7ea7\\\":[],\\\"\u5305\u542b\\\":[]},\\\"uniqueId\\\":\\\"08ab57d41ea73966c49191292e1abb5b\\\"},{\\\"backlink\\\":{\\\"\u4e0a\u7ea7\\\":[\\\"08ab57d41ea73966c49191292e1abb5b\\\"]},\\\"level\\\":1,\\\"link\\\":{\\\"\u4e0b\u7ea7\\\":[],\\\"\u5305\u542b\\\":[]},\\\"uniqueId\\\":\\\"2f5ac27b37e3cb65f14799d4811bb633\\\"},......",
    }
]
```

## 参数说明 <span id="19986db12esqn" />

### 输入参数 <span id="89034fe84evaj" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.83591%" }} /><col style={{ width: "11.058264%" }} /><col style={{ width: "31.747919%" }} /><col style={{ width: "39.357907%" }} /></colgroup><thead><tr><th><p>参数</p></th><th><p>类型</p></th><th><p>默认值</p></th><th><p>说明</p></th></tr></thead><tbody><tr><td><p>result\_type</p></td><td><p><em>Enum</em></p></td><td><p>ResultType.DASHSCOPE\_DOCMIND</p></td><td><p>Parser解析结果的类型</p></td></tr><tr><td><p>num\_workers</p></td><td><p>int</p></td><td><p>4</p></td><td><p>处理多个文件时发起API请求的worker数量</p></td></tr><tr><td><p>check\_interval</p></td><td><p>int</p></td><td><p>5</p></td><td><p>查询解析状态是的间隔，单位：秒</p></td></tr><tr><td><p>max\_timeout</p></td><td><p>int</p></td><td><p>3600</p></td><td><p>解析超时时间，单位：秒</p></td></tr><tr><td><p>workspace</p></td><td><p>str</p></td><td><p>None</p></td><td><p>DashScope workspace id</p></td></tr><tr><td><p>api\_key</p></td><td><p>str</p></td><td><p>None</p></td><td><p>DashScope api key，可以通过环境变量等方法设置。</p></td></tr><tr><td><p>category\_id</p></td><td><p>str</p></td><td><p>default</p></td><td><p>指定上传的类目ID</p></td></tr></tbody></table>

### 输出Document对象字段说明 <span id="86a27bc8feice" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /></colgroup><tbody><tr><td><p>字段名</p></td><td><p>字段类型</p></td><td><p>字段描述</p></td></tr><tr><td><p>id\_</p></td><td><p>str</p></td><td><p>系统生成的文档id</p></td></tr><tr><td><p>metadata.parse\_fmt\_type</p></td><td><p>str</p></td><td><p>解析结果格式化类型</p></td></tr><tr><td><p>text</p></td><td><p>str</p></td><td><p>解析结果字符串，为JSON转义的结果，如果是python语言可以使用json.loads(text)</p></td></tr></tbody></table>
