> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 错误码

> Sandbox API 的错误响应与状态码说明。

<meta property="og:title" content="错误码" />

调用出错时，接口返回的错误响应如下：

```json
{
  "code": 100004,
  "message": "参数缺失",
  "requestID": "request-id"
}
```

常见 HTTP 状态码含义如下：

| HTTP 状态码 | 说明                                 |
| -------- | ---------------------------------- |
| 400      | 参数缺失、格式错误、超出取值范围                   |
| 401      | API Key 无效                         |
| 404      | 模版或实例不存在                           |
| 409      | 当前资源状态不允许执行该操作                     |
| 415      | 文件类型不支持，或请求体不是 multipart/form-data |
| 500      | 服务内部异常                             |
| 501      | 当前版本暂不支持                           |

文件相关错误：

| code                    | HTTP 状态码 | 说明                              |
| ----------------------- | -------- | ------------------------------- |
| `100005`                | 400      | `source` 只支持 `sandbox_template` |
| `11900001`              | 500      | 文件上传失败                          |
| `11900006`              | 404      | 文件不存在                           |
| `11900007`              | 403      | 文件不可下载                          |
| `11900008`              | 413      | 文件超过大小上限                        |
| `11900014`              | 415      | 文件类型不被允许                        |
| `11900019`              | 415      | 视频格式不支持                         |
| `11900023`              | 404      | 文件不存在或正在内容校验，暂不可用               |
| `11900024`              | 400      | 文件不是沙箱模版来源                      |
| `RequestEntityTooLarge` | 413      | multipart 请求体超过 50 MB           |

<Tip>
  兼容 E2B 协议的接口错误结构贴近 E2B 原生定义，可对照 [E2B 官方文档](https://docs.e2b.dev/api-reference)理解。
</Tip>
