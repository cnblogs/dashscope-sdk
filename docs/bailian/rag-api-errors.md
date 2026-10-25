> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 错误码

> RAG API 的错误响应结构、HTTP 状态码、业务错误码与重试策略。

<meta property="og:title" content="错误码" />

所有错误响应使用统一结构：

```json
{
  "error": {
    "code": "INVALID_PARAMETER",
    "message": "字段 knowledge_base_id 不能为空",
    "param": "knowledge_base_id"
  },
  "request_id": "req_xxx"
}
```

## HTTP 状态码 <span id="h-ragapi-errors-http" />

| 状态码 | 含义     | 处理建议        |
| --- | ------ | ----------- |
| 400 | 请求参数错误 | 校对参数        |
| 401 | 鉴权失败   | 检查 API Key  |
| 403 | 无权限    | 申请对应权限范围    |
| 404 | 资源不存在  | 校对 ID       |
| 409 | 资源冲突   | 通常为名称重复     |
| 413 | 请求体过大  | 拆分请求        |
| 429 | 限流     | 退避重试        |
| 500 | 服务内部错误 | 重试,持续失败联系支持 |
| 503 | 服务不可用  | 退避重试        |

## 业务错误码 <span id="h-ragapi-errors-codes" />

| code                      | HTTP | 说明                          |
| ------------------------- | ---- | --------------------------- |
| INVALID\_PARAMETER        | 400  | 参数缺失或格式错误                   |
| UNAUTHORIZED              | 401  | API Key 缺失或无效               |
| FORBIDDEN                 | 403  | 无操作权限                       |
| KB\_NOT\_FOUND            | 404  | 知识库不存在                      |
| DOCUMENT\_NOT\_FOUND      | 404  | 文档不存在                       |
| DATA\_SOURCE\_NOT\_FOUND  | 404  | 数据源不存在                      |
| KB\_NAME\_CONFLICT        | 409  | 知识库名称已存在                    |
| QUOTA\_EXCEEDED           | 429  | 超出租户配额                      |
| RATE\_LIMITED             | 429  | 接口限流                        |
| EMBEDDING\_FAILED         | 500  | 嵌入服务异常                      |
| RETRIEVAL\_TIMEOUT        | 504  | 检索超时,建议降低 top\_k 或启用更小的重排模型 |
| SYNC\_SOURCE\_UNREACHABLE | 502  | 数据源连接失败                     |

## 重试策略 <span id="h-ragapi-errors-retry" />

| 错误类型      | 是否重试 | 间隔                                |
| --------- | ---- | --------------------------------- |
| 4xx(限流除外) | 否    | —                                 |
| 429 限流    | 是    | `Retry-After` 头给出,或 1s/2s/4s 指数退避 |
| 5xx       | 是    | 1s/2s/4s,最多 3 次                   |
| 504 超时    | 是    | 直接重试,最多 2 次                       |

## 排错 <span id="h-ragapi-errors-troubleshoot" />

每个响应都带 `request_id`。联系支持时附上该 ID，可快速定位日志。

<Tip>
  看看具体接口，从[查询知识库列表](/zh/model-studio/rag-api-list-indices)开始。
</Tip>
