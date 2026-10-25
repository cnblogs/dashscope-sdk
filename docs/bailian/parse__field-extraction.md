> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 字段解析 API

> 字段解析 API：提交抽取任务并查询抽取结果。

<meta property="og:title" content="字段解析 API" />

字段解析 API 用于按 JSON Schema 从图文材料中抽取指定字段，返回结构化结果。调用为异步流程：先提交抽取任务获得 `biz_id`，再轮询查询抽取结果，直到任务成功或失败。

- [提交抽取任务](/zh/model-studio/parse/extract-submit)：按 JSON Schema 提交结构化信息抽取任务，返回 `biz_id` 用于异步查询。
- [查询抽取结果](/zh/model-studio/parse/extract-result)：按 `biz_id` 查询任务状态与抽取结果。
