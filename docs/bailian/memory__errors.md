> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 错误码

> API 错误响应与处理建议

<meta property="og:title" content="错误码" />

## HTTP 状态码

| 状态码 | 含义        | 处理建议                                    |
| --- | --------- | --------------------------------------- |
| 200 | 成功        | —                                       |
| 400 | 请求参数错误    | 校对参数                                    |
| 401 | 鉴权失败      | 检查 API Key                              |
| 403 | 无权限或服务未开通 | 检查 API Key 所属业务空间是否已开通记忆服务，以及账号是否具备调用权限 |
| 404 | 资源不存在     | 校对 ID                                   |
| 429 | 限流        | 退避重试                                    |
| 500 | 服务内部错误    | 重试，持续失败联系支持                             |

## 错误响应结构

```json
{
  "code": "InvalidParameter",
  "message": "user_id is required",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

## 重试策略

| 错误类型      | 是否重试 | 间隔              |
| --------- | ---- | --------------- |
| 4xx（限流除外） | 否    | —               |
| 429 限流    | 是    | 1s/2s/4s 指数退避   |
| 5xx       | 是    | 1s/2s/4s，最多 3 次 |

## 排错

每个响应都带 `request_id`。联系支持时附上该 ID，可快速定位日志。

<Tip>
  限流规则参见[限流说明](/zh/model-studio/memory/limits)。
</Tip>
