> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 更新模版

> 更新指定模版的配置。templateID 在更新前后保持不变，每次成功更新后 version 递增，并提交新一轮构建。

<meta property="og:title" content="更新模版" />

## 前提 <span id="h-sbxapi-tpl-update-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-tpl-update-endpoint" />

<strong>PUT</strong> `/templates/{templateCode}`

更新指定模版的配置。该接口为阿里云百炼扩展协议，不声明完全兼容 E2B Update Template v2。`templateID` 在更新前后保持不变；每次成功更新后 `version` 递增。`createdAt` 表示模版首次创建时间，`updatedAt` 表示本次更新时间。更新成功表示新一轮构建已提交；是否可创建实例以构建状态为准。

## 路径参数 <span id="h-sbxapi-tpl-update-path" />

| 参数             | 必填 | 类型     | 说明                               |
| -------------- | -- | ------ | -------------------------------- |
| `templateCode` | 是  | string | 模版唯一 code（即创建接口返回的 `templateID`） |

## 请求体 <span id="h-sbxapi-tpl-update-req" />

| 字段                  | 必填 | 类型             | 说明                                                                                                                                                                                                                                                                                                                                        |
| ------------------- | -- | -------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `name`              | 否  | string         | 模版名称，未传时保留原名称                                                                                                                                                                                                                                                                                                                             |
| `cpuCount`          | 否  | integer        | vCPU 数量，必须与 `memoryMB` 同时传；未传时保留原规格                                                                                                                                                                                                                                                                                                       |
| `memoryMB`          | 否  | integer        | 内存大小，单位 MB，必须与 `cpuCount` 同时传；未传时保留原规格                                                                                                                                                                                                                                                                                                    |
| `fromImage`         | 否  | string         | 基础镜像，未传时保留原镜像                                                                                                                                                                                                                                                                                                                             |
| `imageName`         | 否  | string         | 镜像展示名称，未传时保留原展示名称                                                                                                                                                                                                                                                                                                                         |
| `mntConfig`         | 否  | array\<object> | 文件挂载配置，传入时重新校验文件 ID。子字段 `originFileName`（string，选填，原始文件名；`mountPath` 为目录时不生效）、`originFileId`（string，必填，需通过 `source=sandbox_template` 上传并处于 `available` 状态）、`mountPath`（string，必填，必须以 `/home/user/workspace/uploads/` 开头，推荐写完整文件路径；路径不在该目录下时接口接受配置但实例中不会出现文件。上传方式见[文件上传](/zh/model-studio/sandbox-api-overview#h-sandbox-api-file-upload)） |
| `networkConfig`     | 否  | object         | 网络规则，与创建模版一致。子字段 `allowOut`（array\<string>，出口白名单，支持 IPv4、IPv6、CIDR 和域名）、`denyOut`（array\<string>，出口黑名单，仅支持 IPv4、IPv6 和 CIDR，不支持域名）                                                                                                                                                                                                        |
| `envConfig`         | 否  | object         | 替换为本次传入的环境变量键值对，值为字符串                                                                                                                                                                                                                                                                                                                     |
| `autoPauseTime`     | 否  | integer        | 到期自动暂停时间，单位秒，取值范围 \[300,604800]                                                                                                                                                                                                                                                                                                           |
| `maxRunningTimeout` | 否  | integer        | 最大运行时间，单位秒，取值范围 \[300,604800]                                                                                                                                                                                                                                                                                                             |
| `description`       | 否  | string         | 模版描述                                                                                                                                                                                                                                                                                                                                      |

## 请求示例 <span id="h-sbxapi-tpl-update-eg" />

```bash
curl -X PUT "$BASE_URL/templates/your-template-id" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "my-template-v2",
    "cpuCount": 4,
    "memoryMB": 8192,
    "envConfig": {"STAGE": "prod"},
    "description": "updated template"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-sbxapi-tpl-update-resp" />

更新成功返回 200 与更新后的模版对象。`buildStatus` 可能为 `building`，表示新一轮构建已提交。

```json
{
  "templateID": "your-template-id",
  "templateName": "my-template-v2",
  "cpuCount": 4,
  "memoryMB": 8192,
  "buildID": "your-new-build-id",
  "buildStatus": "building",
  "version": 2,
  "createdAt": "2026-01-01T00:00:00Z",
  "updatedAt": "2026-01-02T00:00:00Z"
}
```

### 响应字段 <span id="h-sbxapi-tpl-update-dto" />

| 字段             | 类型      | 说明                    |
| -------------- | ------- | --------------------- |
| `templateID`   | string  | 模版 ID，更新前后保持不变        |
| `templateName` | string  | 模版名称                  |
| `cpuCount`     | integer | vCPU 数量               |
| `memoryMB`     | integer | 内存大小，单位 MB            |
| `buildID`      | string  | 新版本构建 ID，用于查询构建状态     |
| `buildStatus`  | string  | 构建状态，可能为 `building`   |
| `version`      | integer | 模版版本号，每次成功更新后递增       |
| `createdAt`    | string  | 模版首次创建时间，date-time 格式 |
| `updatedAt`    | string  | 本次更新时间，date-time 格式   |

<Note>
  `mntConfig` 中的文件需先通过文件上传接口上传，并带 `source=sandbox_template`。上传方式见[文件上传](/zh/model-studio/sandbox-api-overview#h-sandbox-api-file-upload)。
</Note>
