> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 复制 Agent

> 复制已有 Agent，生成新的草稿。

<meta property="og:title" content="复制 Agent" />

## 前提 <span id="h-ragapi-copy-prereq" />

已获取阿里云百炼 API Key 与业务空间 ID，鉴权方式见[API 认证](/zh/model-studio/rag-api-authentication)。复制 Agent 需要知识库-创建权限，由[业务空间成员管理](https://bailian.console.aliyun.com/?tab=globalset#/efm/business_management)中的角色决定，详见[权限要求](/zh/model-studio/rag-api-agent-overview)。

## 接口 <span id="h-ragapi-copy-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/app/copy`

复制指定 Agent，创建一个新的草稿 Agent。新 Agent 名称自动添加 `copy_` 前缀，版本号为 `beta`，状态为 `draft`，配置继承自源 Agent。

## 请求体 <span id="h-ragapi-copy-req" />

| 字段         | 必填 | 类型     | 说明          |
| ---------- | -- | ------ | ----------- |
| `agent_id` | 是  | string | 源 Agent ID。 |

## 请求示例 <span id="h-ragapi-copy-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/app/copy" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "agent_id": "aid-8f3a1b2c4d5e6f70"
}'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-copy-resp" />

操作成功返回 200。

```json
{
  "request_id": "a7b8c9d0-e1f2-3456-abcd-567890123456",
  "status_code": 200,
  "code": "Success",
  "message": "",
  "status": "SUCCESS",
  "success": true,
  "data": {
    "agent_id": "aid-9a0b1c2d3e4f5678",
    "agent_name": "copy_客服知识问答助手 v2",
    "agent_version": "beta",
    "agent_status": "draft"
  }
}
```

## 响应字段 <span id="h-ragapi-copy-dto" />

| 字段                   | 类型      | 说明                          |
| -------------------- | ------- | --------------------------- |
| `request_id`         | string  | 请求唯一标识，排查问题时请提供此 ID。        |
| `status_code`        | integer | HTTP 状态码。                   |
| `code`               | string  | 响应码，成功时为 `Success`。         |
| `message`            | string  | 提示信息。                       |
| `status`             | string  | 请求状态：`SUCCESS` 或 `FAILED`。  |
| `success`            | boolean | 请求是否成功。                     |
| `data.agent_id`      | string  | 新创建的 Agent ID。              |
| `data.agent_name`    | string  | 新 Agent 名称，自动添加 `copy_` 前缀。 |
| `data.agent_version` | string  | Agent 版本号，复制后为 `beta`。      |
| `data.agent_status`  | string  | Agent 状态，复制后为 `draft`。      |

## 错误码 <span id="h-ragapi-copy-err" />

| HTTP 状态码 | 错误码                      | 说明                      |
| -------- | ------------------------ | ----------------------- |
| 400      | `Index.InvalidParameter` | 请求参数不合法，请检查参数是否完整且类型正确。 |

失败响应示例：

```json
{
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status_code": 400,
  "code": "Index.InvalidParameter",
  "message": "请求参数不合法，请检查参数是否完整且类型正确。",
  "status": "FAILED",
  "success": false
}
```
