> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 恢复实例

> 恢复已暂停的实例，从暂停时的文件系统与内存状态继续运行。兼容 E2B 恢复实例协议。

<meta property="og:title" content="恢复实例" />

## 前提 <span id="h-sbxapi-resume-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-resume-endpoint" />

<strong>POST</strong> `/sandboxes/{sandboxID}/resume`

恢复已暂停的实例，从暂停时的文件系统与内存状态继续运行。兼容 E2B 恢复实例协议。

## 路径参数 <span id="h-sbxapi-resume-path" />

| 参数          | 必填 | 类型     | 说明      |
| ----------- | -- | ------ | ------- |
| `sandboxID` | 是  | string | 实例唯一 ID |

## 请求体 <span id="h-sbxapi-resume-req" />

请求体可选。

| 字段        | 必填 | 类型      | 说明                                |
| --------- | -- | ------- | --------------------------------- |
| `timeout` | 否  | integer | 恢复后实例的超时时间，单位秒，取值范围 \[300,604800] |

## 请求示例 <span id="h-sbxapi-resume-eg" />

```bash
curl -X POST "$BASE_URL/sandboxes/sbx-xxx/resume" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{"timeout": 3600}'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-sbxapi-resume-resp" />

恢复成功返回 200 与完整连接信息。

```json
{
  "sandboxID": "sbx-xxx",
  "templateID": "your-template-id",
  "envdVersion": "your-envd-version",
  "domain": "your-sandbox-domain",
  "envdAccessToken": "your-envd-access-token",
  "trafficAccessToken": "your-traffic-access-token",
  "envdUrl": "your-envd-url",
  "state": "running"
}
```

### 响应字段 <span id="h-sbxapi-resume-dto" />

| 字段                   | 类型     | 说明                |
| -------------------- | ------ | ----------------- |
| `sandboxID`          | string | 实例唯一 ID           |
| `templateID`         | string | 模版 ID             |
| `envdVersion`        | string | envd 版本           |
| `domain`             | string | 数据面访问域名           |
| `envdAccessToken`    | string | envd 访问 token     |
| `trafficAccessToken` | string | 流量访问 token        |
| `envdUrl`            | string | envd 访问地址         |
| `state`              | string | 实例状态，例如 `running` |
