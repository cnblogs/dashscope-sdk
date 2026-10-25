> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 通过 DashScopeCloudIndex（DashScopeCloudRetriever）构建阿里云百炼云端知识库并使用云端知识索引服务

> 使用 DashScopeCloudIndex（DashScopeCloudRetriever）可以便捷地通过 LlamaIndex 框架进行阿里云百炼云端文档索引管理以及检索器的构建。

<meta property="og:title" content="通过 DashScopeCloudIndex（DashScopeCloudRetriever）构建阿里云百炼云端知识库并使用云端知识索引服务" />

## 前提条件 <span id="6d5f2ea3ee5vj" />

- [获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到系统环境变量中](/zh/model-studio/configure-api-key-through-environment-variables)。
- [获取业务空间 ID](/zh/model-studio/use-workspace)并以相同方式配置到系统环境变量中，变量名请使用`DASHSCOPE_WORKSPACE_ID`。

  <Tip>
    如果您使用了 IDE 或其他辅助开发插件，需自行将DASHSCOPE\_API\_KEY和DASHSCOPE\_WORKSPACE\_ID变量配置到相应的开发环境中。
  </Tip>

## 基于本地文件构建云端知识库 <span id="32038c1fd2ddw" />

<img src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6067484671/CAEQURiBgIC226zynxkiIGUyYzVjOTg2ZGUwMTQxYzY4MTliN2EwZDhiYmMyZjQ04831159_20241219114514.229.svg" alt="image" />

运行本示例前需要注意的：

1. 安装以下依赖

```bash
pip install llama-index-readers-dashscope
pip install llama-index-indices-managed-dashscope
```

2. 准备被解析文件，示例中使用的被解析文件：[阿里云百炼系列产品介绍（虚构）.zip](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241129/idkzqh/%E7%99%BE%E7%82%BC%E7%B3%BB%E5%88%97%E4%BA%A7%E5%93%81%E4%BB%8B%E7%BB%8D%EF%BC%88%E8%99%9A%E6%9E%84%EF%BC%89.zip)。本地文件的上传和解析使用 [DashScopeParse 模块](/zh/model-studio/dashscopeparse)。

   <Note>
     DashScopeParse 智能文档解析支持 PDF、DOC、DOCX、TXT、MD、PPT、PPTX、XLS、XLSX 格式。单文档最大限制100MB或1000页，一次上传最多支持200个文件。

     <strong>只有 PDF、DOC、DOCX 文件会进行智能化解析，其他类型的文件只原样上传到云端，不进行解析。</strong>
   </Note>
3.

<Accordion title="获取需要上传文档的 类目 ID （category_id）。">
  前往<strong>应用数据文件</strong>获取类目 ID（category\_id）。

  <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/1503624671/p890502.png" alt="image" width={900} />
</Accordion>

<Tip>
  在使用 DashScopeParse 时，不设置类目 ID （category\_id）将使用<strong>应用数据</strong>-<strong>文件</strong>中的默认类目。
</Tip>

<strong>示例代码：</strong>

```python expandable
import os
from llama_index.readers.dashscope import DashScopeParse
from llama_index.indices.managed.dashscope import DashScopeCloudIndex

# 您可以通过 使用以下命令设置DASHSCOPE_WORKSPACE_ID（业务空间ID）的临时环境变量。
# os.environ["DASHSCOPE_WORKSPACE_ID"] = "llm-xxx<请替换为您的workspace_id>"
if "DASHSCOPE_WORKSPACE_ID" not in os.environ or os.environ["DASHSCOPE_WORKSPACE_ID"] is None:
    raise ValueError("DASHSCOPE_WORKSPACE_ID 未设置，无法操作 DashScope Cloud Index。")

# 设置需要上传的文件列表
file_list = [
    "./docs/阿里云百炼系列平板电脑产品介绍.pdf",
    "./docs/阿里云百炼系列手机产品介绍.docx",
    "./docs/阿里云百炼系列智能音箱产品介绍.txt"
]
# 上传文件并在云端进行智能化解析
documents = DashScopeParse(
    category="cate_xxx<请替换为类目 ID，不设置将使用默认类目>",
    # 支持更多参数设置
).load_data(file_path=file_list)
print("完成文档上传和智能化解析")

# 在云端进行智能切分并进行智能索引
index = DashScopeCloudIndex.from_documents(
    documents,
    name="my_first_index",
    # 支持更多参数设置
)
# 云端支持库支持以下操作
# 增加更多文档
# index._insert(documents)
# 删除部分文档
# index.delete_ref_doc([documents.doc_id])
print("完成云端知识库构建")

# 初始化检索引擎，retriever为DashScopeCloudRetriever类
retriever = index.as_retriever(
    # 支持更多参数设置
    # dense_similarity_top_k = 100,
    # sparse_similarity_top_k = 100,
    # enable_reranking = True,
    # ...
)
print("===========================================================================================================")
# 执行向量数据库检索
nodes = retriever.retrieve("你知道的阿里云百炼手机是什么吗？")
# 展示检索到的第一个TextNode向量
print(nodes[0])
```

<strong>示例代码输出：</strong>

```plaintext
完成文档上传和智能化解析
Index my_first_index created successfully!
完成云端知识库构建
===========================================================================================================
命中测试：你知道的阿里云百炼手机是什么吗？
命中测试获取的第一个结果为：
Node ID: llm-xxx_xx_file_xx
Text: 欢迎来到未来科技的前沿，探索我们精心打造的智能手机系列，每一款都是为了满足您对科技生活的无限遐想而生。阿里云百炼 X1
——畅享极致视界：搭载 6.7英寸 1440 x 3200像素超清屏幕，搭配
120Hz刷新率，流畅视觉体验跃然眼前。256GB海量存储空间与 12GB RAM强强联合，无论是大型游戏还是多任务处理，都能轻松应对。50
00mAh电池长续航，加上超感光四摄系统，记录生活每一刻精彩。参考售价：4599- 4999通义 Vivid 7 ——智能摄影新体验：拥有
6.5英寸 1080 x 2400像素全面屏，AI智能摄影功能让每一张照片都能展现专业级色彩与细节。8GB RAM与
128GB存储空间确保流畅操作，4500mAh电池满足日常所需。侧面指纹解锁，便捷又安全。参考售价：29...
Score:  0.567
```

## 直接使用阿里云百炼云端知识库 <span id="b285cc1da9ky2" />

<img src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6067484671/CAEQURiBgMD4wa3ynxkiIDkwMDI4ZTZkNzMzNzQ4ZjhiZmFjMDRlNjZjNDllMjc14831159_20241219114514.229.svg" alt="image" />

<strong>示例代码：</strong>

```python
import os
from llama_index.indices.managed.dashscope import DashScopeCloudIndex

# 您可以通过 使用以下命令设置DASHSCOPE_WORKSPACE_ID（业务空间ID）的临时环境变量。
# os.environ["DASHSCOPE_WORKSPACE_ID"] = "llm-xxx<请替换为您的workspace_id>"
if "DASHSCOPE_WORKSPACE_ID" not in os.environ or os.environ["DASHSCOPE_WORKSPACE_ID"] is None:
    raise ValueError("DASHSCOPE_WORKSPACE_ID 未设置，无法操作 DashScope Cloud Index。")

# 需要工作空间下存在名称为”my_first_index“的知识库
index = DashScopeCloudIndex("my_first_index")
print("完成云端知识库构建")

# 初始化检索引擎，retriever为DashScopeCloudRetriever类
retriever = index.as_retriever(
    # 支持更多参数设置
    # dense_similarity_top_k = 100,
    # sparse_similarity_top_k = 100,
    # enable_reranking = True,
    # ...
)
print("===========================================================================================================")
# 执行向量数据库检索
nodes = retriever.retrieve("你知道的阿里云百炼手机是什么吗？")
# 展示检索到的第一个TextNode向量
print(nodes[0])
```

<strong>示例代码输出：</strong>

```plaintext
完成云端知识库构建
===========================================================================================================
命中测试：你知道的阿里云百炼手机是什么吗？
命中测试获取的第一个结果为：
Node ID: llm-xxx_xx_file_xx
Text: 欢迎来到未来科技的前沿，探索我们精心打造的智能手机系列，每一款都是为了满足您对科技生活的无限遐想而生。阿里云百炼 X1
——畅享极致视界：搭载 6.7英寸 1440 x 3200像素超清屏幕，搭配
120Hz刷新率，流畅视觉体验跃然眼前。256GB海量存储空间与 12GB RAM强强联合，无论是大型游戏还是多任务处理，都能轻松应对。50
00mAh电池长续航，加上超感光四摄系统，记录生活每一刻精彩。参考售价：4599- 4999通义 Vivid 7 ——智能摄影新体验：拥有
6.5英寸 1080 x 2400像素全面屏，AI智能摄影功能让每一张照片都能展现专业级色彩与细节。8GB RAM与
128GB存储空间确保流畅操作，4500mAh电池满足日常所需。侧面指纹解锁，便捷又安全。参考售价：29...
Score:  0.567
```

## API 参考 <span id="67b7574c0funl" />

[DashScopeParse API 参考](https://docs.llamaindex.ai/en/stable/api_reference/readers/dashscope/)

[DashScopeCloudIndex API 参考](https://docs.llamaindex.ai/en/stable/api_reference/indices/dashscope/)
