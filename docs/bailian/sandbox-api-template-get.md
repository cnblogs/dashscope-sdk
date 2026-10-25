> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取模版

> 获取指定模版的详细配置，包含资源规格、实例数量、版本数量与当前版本构建信息。

<meta property="og:title" content="获取模版" />

## 前提 <span id="h-sbxapi-tpl-get-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-tpl-get-endpoint" />

<strong>GET</strong> `/templates/{templateCode}`

获取指定模版的详细配置。模版不存在时返回 404，错误文案为「沙箱模版不存在」。

## 路径参数 <span id="h-sbxapi-tpl-get-path" />

| 参数             | 必填 | 类型     | 说明        |
| -------------- | -- | ------ | --------- |
| `templateCode` | 是  | string | 模版唯一 code |

## 请求示例 <span id="h-sbxapi-tpl-get-eg" />

```bash
curl -X GET "$BASE_URL/templates/your-template-id" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-sbxapi-tpl-get-resp" />

```json
{
  "templateID": "your-template-id",
  "templateName": "my-template",
  "cpuCount": 1,
  "memoryMB": 2048,
  "public": false,
  "imageName": "your-image-name",
  "createdAt": "2026-01-01T00:00:00Z",
  "updatedAt": "2026-01-01T00:00:00Z",
  "spawnCount": 2,
  "buildCount": 1,
  "envdVersion": "your-envd-version",
  "builds": [
    {
      "buildID": "your-build-id",
      "status": "ready",
      "createdAt": "2026-01-01T00:00:00Z",
      "updatedAt": "2026-01-01T00:00:00Z",
      "finishedAt": "2026-01-01T00:00:00Z",
      "cpuCount": 1,
      "memoryMB": 2048,
      "diskSizeMB": 10240,
      "envdVersion": "your-envd-version"
    }
  ]
}
```

### 响应字段 <span id="h-sbxapi-tpl-get-dto" />

| 字段             | 类型             | 说明                                                                                                                                                                                                                                                                                                              |
| -------------- | -------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `templateID`   | string         | 模版 ID                                                                                                                                                                                                                                                                                                           |
| `templateName` | string         | 模版名称                                                                                                                                                                                                                                                                                                            |
| `cpuCount`     | integer        | vCPU 数量                                                                                                                                                                                                                                                                                                         |
| `memoryMB`     | integer        | 内存大小，单位 MB                                                                                                                                                                                                                                                                                                      |
| `public`       | boolean        | 是否公开                                                                                                                                                                                                                                                                                                            |
| `imageName`    | string         | 镜像展示名称                                                                                                                                                                                                                                                                                                          |
| `createdAt`    | string         | 创建时间，date-time 格式                                                                                                                                                                                                                                                                                               |
| `updatedAt`    | string         | 更新时间，date-time 格式                                                                                                                                                                                                                                                                                               |
| `spawnCount`   | integer        | 当前模版实例数量                                                                                                                                                                                                                                                                                                        |
| `buildCount`   | integer        | 模版版本数量                                                                                                                                                                                                                                                                                                          |
| `envdVersion`  | string         | envd 版本                                                                                                                                                                                                                                                                                                         |
| `builds`       | array\<object> | 当前版本构建信息。子字段 `buildID`（string，构建 ID）、`status`（string，构建状态，例如 `building`、`ready`、`error`）、`createdAt`（string，构建创建时间）、`updatedAt`（string，构建更新时间）、`finishedAt`（string，构建完成时间；无法提供准确值时省略）、`cpuCount`（integer，vCPU 数量）、`memoryMB`（integer，内存大小，单位 MB）、`diskSizeMB`（integer，磁盘大小，单位 MB）、`envdVersion`（string，envd 版本） |
