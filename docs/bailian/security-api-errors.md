> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 错误码

> Security API 的错误响应结构与处理建议。

<meta property="og:title" content="错误码" />

调用出错时，服务返回如下结构的错误响应，`request_id` 与响应头 `x-request-id` 一致：

```json
{
  "type": "error",
  "error": {
    "code": "405",
    "message": "请求方法不支持"
  },
  "request_id": "82a2ff07-d30f-9905-b9d3-fa2d1c122e01"
}
```

## 常见错误码 <span id="h-secapi-errors-codes" />

| code                       | HTTP 状态码 | 说明                                             |
| -------------------------- | -------- | ---------------------------------------------- |
| 405                        | 405      | 请求方法不支持                                        |
| 400                        | 400      | 参数校验失败，`message` 给出具体原因（如 `参数类型错误: page_size`） |
| InvalidApiKey              | 401      | 未携带或 API Key 无效（网关层）                           |
| Endpoint.AccessDenied      | 403      | 工作空间不存在或无权限（网关层）                               |
| BadRequest.IllegalEndpoint | 400      | 工作空间路由无效（网关层）                                  |

<Note>
  鉴权与工作空间路由类错误由网关直接返回，响应结构为扁平的 `{"request_id", "code", "message"}`；业务错误由服务返回，`error.code` 为业务错误码。
</Note>

## 服务级错误码 <span id="h-secapi-errors-service" />

| 错误码      | HTTP 状态码 | 说明      | 处理建议           |
| -------- | -------- | ------- | -------------- |
| 12000093 | 503      | 云安全服务异常 | 对应能力暂时不可用，稍后重试 |
| 12000094 | 503      | 告警查询失败  | 稍后重试           |

<Tip>
  排查问题时，可使用 `request_id` 定位具体请求；错误排除后，对照[概览](/zh/model-studio/security-api-overview)的接口说明继续调用。
</Tip>
