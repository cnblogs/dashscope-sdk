> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 暂停实例

> 暂停指定实例，保留文件系统与内存状态。再次连接实例时自动恢复。兼容 E2B 暂停实例协议，对应 SDK 的 pause()。

<meta property="og:title" content="暂停实例" />

## 前提 <span id="h-sbxapi-pause-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-pause-endpoint" />

<strong>POST</strong> `/sandboxes/{sandboxID}/pause`

暂停指定实例，保留文件系统与内存状态。再次连接实例时自动恢复。兼容 E2B 暂停实例协议，对应 SDK 的 `pause()`。

## 路径参数 <span id="h-sbxapi-pause-path" />

| 参数          | 必填 | 类型     | 说明      |
| ----------- | -- | ------ | ------- |
| `sandboxID` | 是  | string | 实例唯一 ID |

## 请求示例 <span id="h-sbxapi-pause-eg" />

```bash
curl -X POST "$BASE_URL/sandboxes/sbx-xxx/pause" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应 <span id="h-sbxapi-pause-resp" />

暂停成功返回 204，无响应体。
