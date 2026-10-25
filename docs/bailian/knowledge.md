> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 知识检索与问答

> 知识检索与问答相关 API 概览，含知识检索与知识问答两个接口。

<meta property="og:title" content="知识检索与问答" />

知识检索与问答接口属于 <strong>DashScope 应用网关</strong>体系，与OpenAPI（如 `CreateIndex`、`ListIndices`、`Retrieve` 等 RPC 接口）不同：通过 HTTP REST 调用，使用 API Key Bearer 鉴权，Base URL 为 `https://{workspaceId}.cn-beijing.maas.aliyuncs.com`，其中 `{workspaceId}` 为业务空间 ID。

## 接口列表 <span id="abff22ac44gg4" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "20.02%" }} /><col style={{ width: "54.96%" }} /><col style={{ width: "25.02%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>接口</p></th><th style={{ verticalAlign: "top" }}><p>描述</p></th><th style={{ verticalAlign: "top" }}><p>路径</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p><a href="/zh/model-studio/rag-knowledge-retrieval">知识检索</a></p></td><td style={{ verticalAlign: "top" }}><p>跨多个知识库执行联合语义检索，返回按相关性排序的切片。</p></td><td style={{ verticalAlign: "top" }}><p>POST /api/v1/indices/knowledge/search</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><a href="/zh/model-studio/knowledgechat">知识问答</a></p></td><td style={{ verticalAlign: "top" }}><p>基于知识库的智能问答，通过 SSE 流式输出，依次返回规划、工具调用、生成三个阶段。</p></td><td style={{ verticalAlign: "top" }}><p>POST /api/v2/apps/knowledge/chat</p></td></tr></tbody></table>

<strong>鉴权</strong>：所有请求须在请求头携带 `Authorization: Bearer <API-Key>`与使用业务空间ID拼接的Base URL。API Key 在控制台 [API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)获取，业务空间在控制台[业务空间管理](https://bailian.console.aliyun.com/cn-beijing?tab=globalset#/efm/business_management)获取。

<strong>限流</strong>：默认用户维度 25 QPS。如遇限流，请稍后重试。
