> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 认证

> API Key 获取与使用方式

<meta property="og:title" content="认证" />

ParseX API 采用百炼 **API Key** 鉴权。请求通过 `Authorization` Header 携带 API Key。

## 获取凭证

1. **进入控制台**

   打开百炼控制台，使用阿里云账号登录。

2. **获取 API Key**

   在控制台获取 `DASHSCOPE_API_KEY`，以 `sk-` 开头。详见[获取 API Key](/zh/model-studio/get-api-key)。

   <Warning>
     API Key 具有账号级权限，请妥善保管，不要在公开渠道分享或提交到代码仓库。
   </Warning>

## 使用方式

将 API Key 配置到环境变量：

```bash
export DASHSCOPE_API_KEY="sk-xxxxxxxxxxxxxxxx"
```

每个请求携带 `Authorization` Header：

```bash
curl https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/parse/submit \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json"
```

## 权限说明

- **阿里云主账号**：可直接调用所有接口
- **RAM 子账号**：需获得相应权限授权后调用
- API Key 的权限范围由控制台配置

## 安全建议

- **不入代码仓库**：使用环境变量存储 API Key
- **按应用拆分**：每个应用使用独立的 API Key，便于追踪和撤销
- **定期轮转**：定期更换 API Key，降低泄露风险

<Tip>
  准备好凭证后，查看 [提交解析任务](/zh/model-studio/parse/parse-submit) 开始调用。
</Tip>
