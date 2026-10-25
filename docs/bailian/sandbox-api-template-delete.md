> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 删除模版

> 删除指定模版。若该模版仍存在运行中或暂停中的实例，接口返回 409 拒绝删除。

<meta property="og:title" content="删除模版" />

## 前提 <span id="h-sbxapi-tpl-delete-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/sandbox-api-overview)。

## 接口 <span id="h-sbxapi-tpl-delete-endpoint" />

<strong>DELETE</strong> `/templates/{templateCode}`

删除指定模版。存在活跃实例时禁止删除：若该模版仍存在运行中或暂停中的实例，接口返回 409 拒绝删除，需先释放这些实例后再删除模版。删除成功无响应体。

## 路径参数 <span id="h-sbxapi-tpl-delete-path" />

| 参数             | 必填 | 类型     | 说明        |
| -------------- | -- | ------ | --------- |
| `templateCode` | 是  | string | 模版唯一 code |

## 请求示例 <span id="h-sbxapi-tpl-delete-eg" />

```bash
curl -X DELETE "$BASE_URL/templates/your-template-id" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/sandbox`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应 <span id="h-sbxapi-tpl-delete-resp" />

删除成功返回 204，无响应体。

模版仍存在活跃实例时返回 409，需先释放关联实例。错误响应体字段如下：

| 字段          | 类型      | 说明                                                                                                                                       |
| ----------- | ------- | ---------------------------------------------------------------------------------------------------------------------------------------- |
| `code`      | integer | 错误码                                                                                                                                      |
| `message`   | string  | 错误信息，保留 `sandboxId` 与 `sandboxInstanceId`，例如 `template has running or paused sandbox instance, sandboxId=xxx, sandboxInstanceId=sbx-xxx` |
| `requestID` | string  | 请求 ID                                                                                                                                    |
