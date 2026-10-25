> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 列举实例

> 列举当前工作空间下的沙箱实例，支持按模版 ID、实例 ID 与状态过滤。兼容 E2B 列举实例协议。

<meta property="og:title" content="列举实例" />

## 前提 <span id="h-sbxapi-list-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-list-endpoint" />

<strong>GET</strong> `/v2/sandboxes`

列举当前工作空间下的沙箱实例。兼容 E2B 列举实例协议，兼容路由 `GET /sandboxes`。

## 查询参数 <span id="h-sbxapi-list-query" />

| 参数           | 必填 | 类型      | 说明                                   |
| ------------ | -- | ------- | ------------------------------------ |
| `templateID` | 否  | string  | 按模版 ID 过滤。兼容 `templateId`、`template` |
| `sandboxID`  | 否  | string  | 按实例 ID 精确过滤                          |
| `state`      | 否  | string  | 按状态过滤，例如 `running`、`paused`          |
| `limit`      | 否  | integer | 单页条数，默认 20，最大 50                     |

## 请求示例 <span id="h-sbxapi-list-eg" />

```bash
curl -X GET "$BASE_URL/v2/sandboxes?state=running&limit=20" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-sbxapi-list-resp" />

响应体直接返回实例数组，不封装为分页对象。

```json
[
  {
    "clientID": "your-client-id",
    "sandboxID": "sbx-xxx",
    "bailianSandboxId": "your-bailian-sandbox-id",
    "templateID": "your-template-id",
    "alias": "your-template-alias",
    "state": "running",
    "cpuCount": 1,
    "memoryMB": 2048,
    "diskSizeMB": 10240,
    "envdVersion": "your-envd-version",
    "startedAt": "2026-01-01T00:00:00Z",
    "endAt": "2026-01-01T01:00:00Z",
    "metadata": {"team": "data"}
  }
]
```

### 响应字段 <span id="h-sbxapi-list-dto" />

数组元素字段如下。

| 字段                 | 类型      | 说明                  |
| ------------------ | ------- | ------------------- |
| `clientID`         | string  | 客户端标识               |
| `sandboxID`        | string  | 实例唯一 ID             |
| `bailianSandboxId` | string  | 阿里云百炼扩展标识；开发者通常无需使用 |
| `templateID`       | string  | 模版 ID               |
| `alias`            | string  | 模版别名                |
| `state`            | string  | 实例状态，例如 `running`   |
| `cpuCount`         | integer | vCPU 核数             |
| `memoryMB`         | integer | 内存大小，单位 MB          |
| `diskSizeMB`       | integer | 磁盘大小，单位 MB          |
| `envdVersion`      | string  | envd 版本             |
| `startedAt`        | string  | 实例启动时间，date-time 格式 |
| `endAt`            | string  | 实例到期时间，date-time 格式 |
| `metadata`         | object  | 自定义元数据              |
