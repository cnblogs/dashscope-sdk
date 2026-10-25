> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 认证

> RAG API 的 API Key 获取、使用方式与安全建议。

<meta property="og:title" content="认证" />

RAG API 采用 **API Key** 鉴权。请求通过 `Authorization` Header 携带 API Key，业务空间由 Endpoint 中的 `{workspace_id}` 标识，格式为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/`。

## 获取 API Key <span id="h-ragapi-auth-get" />

1. 打开[百炼控制台](https://bailian.console.aliyun.com/)，使用阿里云账号登录，进入 [**API Key**](https://bailian.console.aliyun.com/?tab=model#/api-key) 页面。
2. 创建新的 API Key，或复制已有的 API Key。

<Warning>
  API Key 具有账号级权限，请妥善保管，不要在公开渠道分享或提交到代码仓库。
</Warning>

## 使用方式 <span id="h-ragapi-auth-use" />

每个请求携带 `Authorization` Header 即可：

```text
Authorization: Bearer <API-Key>
```

完整示例：

```bash
curl "$BASE_URL/api/v1/indices/rag/index/list?page_number=1&page_size=10" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `$BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`，`$BAILIAN_API_KEY` 为您的 API Key。

## 权限说明 <span id="h-ragapi-auth-permission" />

- **阿里云主账号**：可直接调用所有接口。
- **RAM 子账号**：需先获取 `AliyunBailianDataFullAccess` 权限并加入目标业务空间。
- API Key 的权限范围由控制台配置。

## 安全建议 <span id="h-ragapi-auth-security" />

- **不入代码仓库**：使用环境变量存储 API Key（如 `BAILIAN_API_KEY`）。
- **按应用拆分**：每个应用使用独立的 API Key，便于追踪和撤销。
- **定期轮转**：定期更换 API Key，降低泄露风险。

<Tip>
  准备好凭证后，试试调用[查询知识库列表](/zh/model-studio/rag-api-list-indices)接口。
</Tip>
