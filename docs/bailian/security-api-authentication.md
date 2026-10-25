> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 认证

> Security API 的 API Key 获取与使用方式。

<meta property="og:title" content="认证" />

Security API 采用 **API Key** 鉴权。全部请求通过 HTTP Header 携带阿里云百炼 API Key：

```text
Authorization: Bearer <your-api-key>
```

## 获取 API Key <span id="h-secapi-auth-get" />

1. 打开[百炼控制台](https://bailian.console.aliyun.com/?tab=model#/api-key)，登录阿里云账号并开通百炼。
2. 在 **API Key** 页面创建并复制 API Key。

<Warning>
  API Key 具有账号级权限，请妥善保管，不要在公开渠道分享或提交到代码仓库。
</Warning>

<Tip>
  配置完成后，从[查询 Agent 资产](/zh/model-studio/security-api-assets)开始调用。
</Tip>
