> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 使用 Embedding 模型

> 本文档介绍使用DashScopeEmbedding服务来在LlamaIndex中构建向量索引服务的使用方法。

<meta property="og:title" content="使用 Embedding 模型" />

## 前提条件 <span id="c5bad0170eh01" />

- 您已开通百炼服务并获得API-KEY， 请参考[获取与配置 API Key](/zh/model-studio/get-api-key)。
- 已导入 API-KEY，请参考[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- 安装 LlamaIndex 核心组件、DashScopeEmbedding 以及相关依赖。

```bash
pip install llama-index-core
pip install llama-index-embeddings-dashscope
pip install llama-index-readers-file
pip install docx2txt
```

## 支持的模型 <span id="8dde9a2a902qv" />

> <strong>MTEB、CMTEB</strong>是 Embedding模型的通用评估指标，数值越大，模型效果越好。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p><strong>模型</strong></p></th><th><p><strong>MTEB</strong></p></th><th><p><strong>MTEB（Retrieval task）</strong></p></th><th><p><strong>CMTEB</strong></p></th><th><p><strong>CMTEB (Retrieval task)</strong></p></th></tr></thead><tbody><tr><td><p>text-embedding-v1</p></td><td><p>58.30</p></td><td><p>45.47</p></td><td><p>59.84</p></td><td><p>56.59</p></td></tr><tr><td><p>text-embedding-v2</p></td><td><p>60.13</p></td><td><p>49.49</p></td><td><p>62.17</p></td><td><p>62.78</p></td></tr><tr><td><p>text-embedding-v3</p></td><td><p>63.39</p></td><td><p>55.41</p></td><td><p>68.92</p></td><td><p>73.23</p></td></tr></tbody></table>

## 修改全局设置（可选） <span id="b5631e462csyy" />

```python
from llama_index.core import Settings
from llama_index.embeddings.dashscope import DashScopeEmbedding

# LlamaIndex默认使用的Embedding模型被替换为百炼的Embedding模型
Settings.embed_model = DashScopeEmbedding(
    model_name="text-embedding-v2"
)
```

## 模型调用示例 <span id="c39f371ef8h3f" />

```python
from llama_index.embeddings.dashscope import DashScopeEmbedding

# 初始化 Embedding 模型
embedder = DashScopeEmbedding(
    model_name="text-embedding-v2"
)
text_to_embedding = ["风急天高猿啸哀", "渚清沙白鸟飞回", "无边落木萧萧下", "不尽长江滚滚来"]
# 调用 Embedding 模型
result_embeddings = embedder.get_text_embedding_batch(text_to_embedding)
# 显示 Embedding 后结果
for index, embedding in enumerate(result_embeddings):
    print("Dimension of embeddings: %s" % len(embedding))
    print(
        "Input: %s, embedding is: %s"
        % (text_to_embedding[index], embedding[:5])
    )
```

<strong>示例代码输出</strong>

```plaintext
Dimension of embeddings: 1536
Input: 风急天高猿啸哀, embedding is: [-0.0016666285653348784, 0.008690492014557004, 0.02894828715284365, -0.01774133615134858, 0.03627544697161321]
Dimension of embeddings: 1536
Input: 渚清沙白鸟飞回, embedding is: [0.018255604113922633, 0.030631669725945727, 0.0031333343045102462, 0.014323813963475412, 0.009666154862176396]
Dimension of embeddings: 1536
Input: 无边落木萧萧下, embedding is: [-0.01270165436681136, 0.011355212676752505, -0.007090375205285297, 0.008317427977013809, 0.0341982923839579]
Dimension of embeddings: 1536
Input: 不尽长江滚滚来, embedding is: [0.003449439128962428, 0.02667092110022496, -0.0010223853088419568, -0.00971414215183749, 0.0035561228133633277]
```

## 构建文档的向量化索引 <span id="ecbb49f8f7wi6" />

示例中使用的被解析文件：[百炼系列产品介绍（虚构）.zip](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241129/idkzqh/%E7%99%BE%E7%82%BC%E7%B3%BB%E5%88%97%E4%BA%A7%E5%93%81%E4%BB%8B%E7%BB%8D%EF%BC%88%E8%99%9A%E6%9E%84%EF%BC%89.zip)

```python expandable
# 导入依赖
from llama_index.embeddings.dashscope import DashScopeEmbedding
from llama_index.core import SimpleDirectoryReader, VectorStoreIndex

# 读取被解析的文件目录下所有文件
documents = SimpleDirectoryReader("<请替换为需要解析的文件目录>").load_data()
print("已读取文件目录的文件")

# from_documents方法包含对文档进行切片与建立索引两个步骤
index = VectorStoreIndex.from_documents(
    documents,
    # 指定embedding 模型
    embed_model=DashScopeEmbedding(
        model_name="text-embedding-v2"
    ))
print("已使用DashScopeEmbedding模型构建了多个文档的向量化索引")

# 输出建立好的索引和压缩好的向量示例
print("输出向量化示例：")
for i, uuid in enumerate(index.vector_store.data.metadata_dict.keys()):
    print("文件名：", end='')
    print(index.vector_store.data.metadata_dict[uuid]['file_name'], end='')
    print("，文件大小：", index.vector_store.data.metadata_dict[uuid]['file_size'], end='')
    print("，文件类型：", index.vector_store.data.metadata_dict[uuid]['file_type'])
    print("压缩后向量：", end='')
    print(index.vector_store.data.embedding_dict[uuid][:3], '\n')
    if i > 3:
        break
```

<strong>示例代码输出</strong>

> 在构建向量化索引的过程中，原始文件会被拆分成多个部分，每个部分随后被转换成一个向量。

```plaintext
已读取文件目录的文件
已使用DashScopeEmbedding模型构建了多个文档的向量化索引
输出向量化示例：
文件名：百炼系列平板电脑产品介绍.pdf，文件大小： 144316，文件类型： application/pdf
压缩后向量：[0.040188307063141346, -0.00039877765589124394, -0.035738459756745854]

文件名：百炼系列平板电脑产品介绍.pdf，文件大小： 144316，文件类型： application/pdf
压缩后向量：[0.04814293357335188, 0.004163492388781393, -0.038165800733263575]

文件名：百炼系列手机产品介绍.docx，文件大小： 14265，文件类型： application/vnd.openxmlformats-officedocument.wordprocessingml.document
压缩后向量：[0.019914771026010504, 0.0009497773001332384, -0.040679692362629784]

文件名：百炼系列手机产品介绍.docx，文件大小： 14265，文件类型： application/vnd.openxmlformats-officedocument.wordprocessingml.document
压缩后向量：[0.02361539453526087, 0.00019768677449582677, -0.03274763169693275]

文件名：百炼系列智能音箱产品介绍.txt，文件大小： 2448，文件类型： text/plain
压缩后向量：[0.03064649761730314, -0.003089192569710745, -0.022280601331799776]
```

详细介绍与更多示例请前往 LlamaIndex 官方的[Embedding Examples](https://docs.llamaindex.ai/en/stable/examples/embeddings/dashscope_embeddings/) ，完整的 API Reference 请前往 LlamaIndex 官方的 [DashScopeEmbedding API Reference](https://docs.llamaindex.ai/en/stable/api_reference/embeddings/dashscope/)。
