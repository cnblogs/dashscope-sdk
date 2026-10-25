> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 创建实例

> 基于已构建完成的模版创建一个沙箱实例。创建成功后返回实例的 sandboxID，用于后续访问与管理。兼容 E2B 创建实例协议。模版尚未构建完成时返回 409。

<meta property="og:title" content="创建实例" />

## 前提 <span id="h-sbxapi-create-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-create-endpoint" />

<strong>POST</strong> `/sandboxes`

基于已构建完成的模版创建一个沙箱实例。兼容 E2B 创建实例协议。模版尚未构建完成时返回 409。

## 请求体 <span id="h-sbxapi-create-req" />

| 字段                      | 必填 | 类型      | 说明                                                                                                                                                                                                               |
| ----------------------- | -- | ------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `templateID`            | 是  | string  | 已创建且构建完成的模版 ID                                                                                                                                                                                                   |
| `timeout`               | 否  | integer | 实例生命周期时间，单位秒，取值范围 \[300,604800]                                                                                                                                                                                  |
| `allow_internet_access` | 否  | boolean | 是否允许实例访问公网                                                                                                                                                                                                       |
| `metadata`              | 否  | object  | 自定义元数据，键和值均为字符串                                                                                                                                                                                                  |
| `envVars`               | 否  | object  | 实例级环境变量，键和值均为字符串                                                                                                                                                                                                 |
| `autoPause`             | 否  | boolean | `timeout` 到期后是否暂停实例                                                                                                                                                                                              |
| `autoResume`            | 否  | boolean | 是否在连接时自动恢复；兼容对象形式 `{"enabled": true}`                                                                                                                                                                            |
| `lifecycle`             | 否  | object  | 生命周期配置。子字段 `on_timeout`（string，到期行为，`pause` 表示到期暂停；也可传对象形式携带 `action`）、`auto_resume`（boolean，暂停后连接是否自动恢复）                                                                                                        |
| `network`               | 否  | object  | 实例级网络配置。子字段 `allowOut`（array\<string>，出口白名单）、`denyOut`（array\<string>，出口黑名单）、`maskRequestHost`（string，可选的请求 Host 覆盖）、`rules`（object，扩展网络规则）。兼容 `allow_public_traffic`、`allow_out`、`deny_out`、`mask_request_host` |

## 请求示例 <span id="h-sbxapi-create-eg" />

```bash
curl -X POST "$BASE_URL/sandboxes" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "templateID": "your-template-id",
    "timeout": 3600,
    "allow_internet_access": true,
    "metadata": {"team": "data"},
    "envVars": {"STAGE": "dev"},
    "autoPause": true,
    "lifecycle": {"on_timeout": "pause", "auto_resume": true}
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-sbxapi-create-resp" />

创建成功返回 201。

```json
{
  "clientID": "your-client-id",
  "sandboxID": "sbx-xxx",
  "bailianSandboxId": "your-bailian-sandbox-id",
  "templateID": "your-template-id",
  "alias": "your-template-alias",
  "envdVersion": "your-envd-version",
  "domain": "your-sandbox-domain",
  "envdAccessToken": "your-envd-access-token",
  "trafficAccessToken": "your-traffic-access-token",
  "envdUrl": "your-envd-url",
  "state": "running"
}
```

### 响应字段 <span id="h-sbxapi-create-dto" />

| 字段                   | 类型     | 说明                     |
| -------------------- | ------ | ---------------------- |
| `clientID`           | string | 客户端标识                  |
| `sandboxID`          | string | 实例唯一 ID，用于后续生命周期与数据面调用 |
| `bailianSandboxId`   | string | 阿里云百炼扩展标识；开发者通常无需使用    |
| `templateID`         | string | 模版 ID                  |
| `alias`              | string | 模版别名                   |
| `envdVersion`        | string | envd 版本                |
| `domain`             | string | 数据面访问域名                |
| `envdAccessToken`    | string | envd 访问 token          |
| `trafficAccessToken` | string | 流量访问 token             |
| `envdUrl`            | string | envd 访问地址              |
| `state`              | string | 实例状态，例如 `running`      |
