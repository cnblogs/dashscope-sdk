> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 创建模版

> 创建一个沙箱模版。创建成功后返回 templateID 与 buildID，需等待构建状态变为 ready 后再创建实例。

<meta property="og:title" content="创建模版" />

## 前提 <span id="h-sbxapi-tpl-create-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-tpl-create-endpoint" />

<strong>POST</strong> `/v3/templates`

创建一个沙箱模版。兼容路由 `POST /templates`。创建成功后返回 `templateID` 与 `buildID`，需等待构建状态变为 `ready` 后再创建实例。CPU 与内存必须同时传递，且必须匹配平台提供的资源规格组合。非法 JSON 返回明确的格式错误。

## 请求体 <span id="h-sbxapi-tpl-create-req" />

| 字段                  | 必填 | 类型             | 说明                                                                                                                                                                                                                                                                                                                           |
| ------------------- | -- | -------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `name`              | 是  | string         | 模版名称                                                                                                                                                                                                                                                                                                                         |
| `cpuCount`          | 是  | integer        | vCPU 数量，必须大于 0，且与内存匹配平台支持的规格组合                                                                                                                                                                                                                                                                                               |
| `memoryMB`          | 是  | integer        | 内存大小，单位 MB，必须大于 0，且与 CPU 匹配平台支持的规格组合                                                                                                                                                                                                                                                                                         |
| `fromImage`         | 否  | string         | 基础镜像，填写完整镜像地址。官方镜像位于 `fc-e2b-registry.cn-beijing.cr.aliyuncs.com/runtime/` 下，可选 `code-interpreter-v1`、`browser`、`all-in-one`。缺省时使用平台默认镜像（代码解释器镜像）                                                                                                                                                                            |
| `imageName`         | 否  | string         | 镜像展示名称                                                                                                                                                                                                                                                                                                                       |
| `tags`              | 否  | array\<string> | E2B 标签                                                                                                                                                                                                                                                                                                                       |
| `alias`             | 否  | string         | E2B 模版别名                                                                                                                                                                                                                                                                                                                     |
| `mntConfig`         | 否  | array\<object> | 文件挂载配置。子字段 `originFileName`（string，选填，原始文件名；`mountPath` 为目录时不生效）、`originFileId`（string，必填，需通过 `source=sandbox_template` 上传并处于 `available` 状态）、`mountPath`（string，必填，必须以 `/home/user/workspace/uploads/` 开头，推荐写完整文件路径；路径不在该目录下时接口接受配置但实例中不会出现文件。上传方式见[文件上传](/zh/model-studio/sandbox-api-overview#h-sandbox-api-file-upload)） |
| `networkConfig`     | 否  | object         | 网络规则。子字段 `allowOut`（array\<string>，出口白名单，支持 IPv4、IPv6、CIDR 和域名）、`denyOut`（array\<string>，出口黑名单，仅支持 IPv4、IPv6 和 CIDR，不支持域名）                                                                                                                                                                                                   |
| `envConfig`         | 否  | object         | 写入模版运行环境的环境变量键值对，值为字符串                                                                                                                                                                                                                                                                                                       |
| `autoPauseTime`     | 否  | integer        | 到期自动暂停时间，单位秒，取值范围 \[300,604800]                                                                                                                                                                                                                                                                                              |
| `maxRunningTimeout` | 否  | integer        | 最大运行时间，单位秒，取值范围 \[300,604800]。到期自动释放。仅传 `autoPauseTime` 时到期自动暂停；两者都传时 `maxRunningTimeout` 优先生效；两者都不传时默认最大运行时间为 604800 秒                                                                                                                                                                                                      |
| `description`       | 否  | string         | 模版描述                                                                                                                                                                                                                                                                                                                         |

## 请求示例 <span id="h-sbxapi-tpl-create-eg" />

```bash
curl -X POST "$BASE_URL/v3/templates" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "my-template",
    "cpuCount": 1,
    "memoryMB": 2048,
    "envConfig": {"STAGE": "dev"},
    "networkConfig": {
      "allowOut": ["example.com"],
      "denyOut": ["10.0.0.0/8"]
    },
    "autoPauseTime": 1800,
    "maxRunningTimeout": 3600,
    "description": "my first sandbox template"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-sbxapi-tpl-create-resp" />

创建成功返回 202，表示模版已提交构建。

```json
{
  "templateID": "your-template-id",
  "templateName": "my-template",
  "templateType": "CUSTOM",
  "cpuCount": 1,
  "memoryMB": 2048,
  "public": false,
  "regionId": "cn-beijing",
  "imageName": "your-image-name",
  "buildID": "your-build-id",
  "buildStatus": "building",
  "createdAt": "2026-01-01T00:00:00Z",
  "updatedAt": "2026-01-01T00:00:00Z",
  "version": 1,
  "tags": [],
  "aliases": []
}
```

### 响应字段 <span id="h-sbxapi-tpl-create-dto" />

| 字段             | 类型             | 说明                               |
| -------------- | -------------- | -------------------------------- |
| `templateID`   | string         | 模版 ID                            |
| `templateName` | string         | 模版名称                             |
| `templateType` | string         | 模版类型，例如 `CUSTOM`                 |
| `cpuCount`     | integer        | vCPU 数量                          |
| `memoryMB`     | integer        | 内存大小，单位 MB                       |
| `public`       | boolean        | 是否公开                             |
| `regionId`     | string         | 地域                               |
| `imageName`    | string         | 镜像展示名称                           |
| `buildID`      | string         | 构建 ID，用于查询构建状态                   |
| `buildStatus`  | string         | 构建状态，例如 `building`               |
| `createdAt`    | string         | 创建时间，date-time 格式                |
| `updatedAt`    | string         | 更新时间，date-time 格式                |
| `version`      | integer        | 模版版本号                            |
| `tags`         | array\<string> | 标签。在能够被后续列举/获取模版稳定返回前，不作为持久化字段   |
| `aliases`      | array\<string> | 别名列表。在能够被后续列举/获取模版稳定返回前，不作为持久化字段 |

<Note>
  构建状态变为 `ready` 后才能基于该模版创建实例，查询方式见[获取构建状态](/zh/model-studio/sandbox-api-template-build-status)。
</Note>
