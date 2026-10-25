> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 认证

> Flow Agent API 两类接口的鉴权方式。

<meta property="og:title" content="认证" />

Flow Agent API 包含两类接口，鉴权方式不同。

## 应用调用接口 <span id="h-flow-api-auth-invocation" />

通过 DashScope 应用网关调用，使用**阿里云百炼 API Key** 鉴权，API Key 在[控制台](https://bailian.console.aliyun.com/?tab=model#/api-key)创建：

```text
Authorization: Bearer <your-api-key>
```

## 组件类接口（Prompt 模板等） <span id="h-flow-api-auth-components" />

属于**阿里云 OpenAPI**，采用 AccessKey 签名（`ACS3-HMAC-SHA256`），不支持 `Authorization: Bearer` 方式。推荐使用阿里云 SDK 或 [OpenAPI Explorer](https://api.aliyun.com/) 发起调用，可免去自行计算签名。
