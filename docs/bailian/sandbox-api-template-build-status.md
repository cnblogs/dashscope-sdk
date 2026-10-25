> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取构建状态

> 查询模版的镜像构建状态。构建状态变为 ready 后才能基于该模版创建实例。

<meta property="og:title" content="获取构建状态" />

## 前提 <span id="h-sbxapi-tpl-build-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-tpl-build-endpoint" />

<strong>GET</strong> `/templates/{templateCode}/builds/{buildID}/status`

查询模版的镜像构建状态。兼容路由 `GET /templates/{templateCode}/builds/{buildID}`。`buildID` 来自创建/更新模版接口的响应。构建状态变为 `ready` 后才能基于该模版创建实例。

## 路径参数 <span id="h-sbxapi-tpl-build-path" />

| 参数             | 必填 | 类型     | 说明                        |
| -------------- | -- | ------ | ------------------------- |
| `templateCode` | 是  | string | 模版唯一 code（即 `templateID`） |
| `buildID`      | 是  | string | 构建 ID，来自创建/更新模版接口的响应      |

## 请求示例 <span id="h-sbxapi-tpl-build-eg" />

```bash
curl -X GET "$BASE_URL/templates/your-template-id/builds/your-build-id/status" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-sbxapi-tpl-build-resp" />

构建失败时返回 `reason` 对象说明失败原因。

```json
{
  "templateID": "your-template-id",
  "buildID": "your-build-id",
  "status": "ready",
  "logs": ["build started", "build finished"],
  "logEntries": []
}
```

### 响应字段 <span id="h-sbxapi-tpl-build-dto" />

| 字段           | 类型             | 说明                                                                                  |
| ------------ | -------------- | ----------------------------------------------------------------------------------- |
| `templateID` | string         | 模版 ID                                                                               |
| `buildID`    | string         | 构建 ID                                                                               |
| `status`     | string         | 构建状态，取值为 `building`、`ready`、`error`。最终为 `ready`；失败时为 `error`                        |
| `logs`       | array\<string> | 构建日志                                                                                |
| `logEntries` | array\<object> | 结构化构建日志条目                                                                           |
| `reason`     | object         | 构建失败原因；成功时省略。子字段 `code`（string，失败原因码，例如 `InvalidArgument`）、`message`（string，失败原因说明） |
