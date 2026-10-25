> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取实例

> 获取指定实例的详细信息，包含资源规格、数据面访问信息、网络与生命周期配置。兼容 E2B 获取实例协议，对应 SDK 的 get_info()。

<meta property="og:title" content="获取实例" />

## 前提 <span id="h-sbxapi-get-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-get-endpoint" />

<strong>GET</strong> `/sandboxes/{sandboxID}`

获取指定实例的详细信息。兼容 E2B 获取实例协议，对应 SDK 的 `get_info()`。实例不存在时返回 404，错误文案为「沙箱实例不存在」。无网络或生命周期配置时，对应字段省略或返回空对象。

## 路径参数 <span id="h-sbxapi-get-path" />

| 参数          | 必填 | 类型     | 说明      |
| ----------- | -- | ------ | ------- |
| `sandboxID` | 是  | string | 实例唯一 ID |

## 请求示例 <span id="h-sbxapi-get-eg" />

```bash
curl -X GET "$BASE_URL/sandboxes/sbx-xxx" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-sbxapi-get-resp" />

```json
{
  "sandboxID": "sbx-xxx",
  "bailianSandboxId": "your-bailian-sandbox-id",
  "templateID": "your-template-id",
  "state": "running",
  "cpuCount": 1,
  "memoryMB": 2048,
  "envdVersion": "your-envd-version",
  "domain": "your-sandbox-domain",
  "envdAccessToken": "your-envd-access-token",
  "trafficAccessToken": "your-traffic-access-token",
  "envdUrl": "your-envd-url",
  "allowInternetAccess": true,
  "network": {
    "allowPublicTraffic": true,
    "allowOut": ["example.com"],
    "denyOut": ["10.0.0.0/8"]
  },
  "lifecycle": {
    "onTimeout": "pause",
    "autoResume": true
  }
}
```

### 响应字段 <span id="h-sbxapi-get-dto" />

| 字段                    | 类型      | 说明                                                                                                                                 |
| --------------------- | ------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `sandboxID`           | string  | 实例唯一 ID                                                                                                                            |
| `bailianSandboxId`    | string  | 阿里云百炼扩展标识；开发者通常无需使用                                                                                                                |
| `templateID`          | string  | 模版 ID                                                                                                                              |
| `state`               | string  | 实例状态，例如 `running`                                                                                                                  |
| `cpuCount`            | integer | vCPU 核数                                                                                                                            |
| `memoryMB`            | integer | 内存大小，单位 MB                                                                                                                         |
| `envdVersion`         | string  | envd 版本                                                                                                                            |
| `domain`              | string  | 数据面访问域名                                                                                                                            |
| `envdAccessToken`     | string  | envd 访问 token                                                                                                                      |
| `trafficAccessToken`  | string  | 流量访问 token                                                                                                                         |
| `envdUrl`             | string  | envd 访问地址                                                                                                                          |
| `allowInternetAccess` | boolean | 是否允许实例访问公网                                                                                                                         |
| `network`             | object  | 实例级网络配置。子字段 `allowPublicTraffic`（boolean，是否允许公网流量）、`allowOut`（array\<string>，出口白名单）、`denyOut`（array\<string>，出口黑名单）。无网络配置时省略或返回空对象 |
| `lifecycle`           | object  | 生命周期配置。子字段 `onTimeout`（string，到期行为，例如 `pause`）、`autoResume`（boolean，暂停后连接是否自动恢复）。无生命周期配置时省略或返回空对象                                  |
