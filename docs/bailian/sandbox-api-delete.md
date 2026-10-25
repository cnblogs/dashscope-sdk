> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 释放实例

> 释放指定实例，回收计算与网络资源。释放后实例不可恢复。兼容 E2B 释放实例协议，对应 SDK 的 kill()。

<meta property="og:title" content="释放实例" />

## 前提 <span id="h-sbxapi-delete-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-delete-endpoint" />

<strong>DELETE</strong> `/sandboxes/{sandboxID}`

释放指定实例，回收计算与网络资源。释放后实例不可恢复。兼容 E2B 释放实例协议，对应 SDK 的 `kill()`。

## 路径参数 <span id="h-sbxapi-delete-path" />

| 参数          | 必填 | 类型     | 说明      |
| ----------- | -- | ------ | ------- |
| `sandboxID` | 是  | string | 实例唯一 ID |

## 请求示例 <span id="h-sbxapi-delete-eg" />

```bash
curl -X DELETE "$BASE_URL/sandboxes/sbx-xxx" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应 <span id="h-sbxapi-delete-resp" />

释放成功返回 204，无响应体。
