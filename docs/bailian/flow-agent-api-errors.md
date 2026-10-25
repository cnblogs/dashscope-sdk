> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 错误码

> Flow Agent API 两类接口的错误响应说明。

<meta property="og:title" content="错误码" />

Flow Agent API 两类接口的错误响应遵循各自网关约定：

- **应用调用接口**：错误响应包含错误码与错误信息，并在响应中提供 `request_id`，以各接口页面说明为准。
- **组件类接口**：遵循阿里云 OpenAPI 统一响应结构，出错时在 `Code` 与 `Message` 字段返回错误信息。

常见 HTTP 状态码含义如下：

| HTTP 状态码 | 说明        |
| -------- | --------- |
| 400      | 参数缺失、格式错误 |
| 401      | 凭证无效或签名错误 |
| 403      | 无操作权限     |
| 404      | 资源不存在     |
| 429      | 请求过于频繁    |
| 500      | 服务内部异常    |
