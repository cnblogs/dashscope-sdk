> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 工具调用概述（下线中）

<meta property="og:title" content="工具调用概述（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

Assistant API 目前支持调用以下工具：

- 官方预置插件：

  - [Python代码解释器](/zh/model-studio/code-interpreter)：编写或执行 Python 代码，可以处理批量的文件和数据。
  - 文生图工具：通过输入文字描述生成对应图像。
  - 夸克搜索：在互联网上获取最新信息。
  - 计算器：完成精确的计算任务。
- [知识检索增强](/zh/model-studio/retrieval-augmented-generation)（Retrieval Augmented Generation, RAG）：与阿里云百炼应用相似，智能体也支持知识检索增强，可获取外部知识。
- [函数调用](/zh/model-studio/function-calling)（Function Calling）：与阿里云百炼应用相似，智能体也支持函数调用。在 Assistant API 中，您可以在创建智能体时简要描述您的业务函数，它会智能地返回需要调用的函数及其参数。
