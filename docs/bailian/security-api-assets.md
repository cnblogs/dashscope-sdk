> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询 Agent 资产

> 按业务空间统计 Agent 及其挂载资源数量。以 Agent 为中心：各项为挂载到 Agent 上的资源数（去重）。

<meta property="og:title" content="查询 Agent 资产" />

## 前提 <span id="h-secapi-assets-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/security-api-overview)。

## 接口 <span id="h-secapi-assets-endpoint" />

<strong>GET</strong> `/asset_summary`

按业务空间统计 Agent 及其挂载资源数量，无入参。以 Agent 为中心：各项为挂载到 Agent 上的资源数（去重）。

## 请求示例 <span id="h-secapi-assets-eg" />

```bash
curl -X GET "$BASE_URL/asset_summary" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/security`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应体 <span id="h-secapi-assets-resp" />

| 字段               | 类型      | 说明                                           |
| ---------------- | ------- | -------------------------------------------- |
| `agent_count`    | integer | Agent 总数（Managed Agent 与 Agent 1.0/2.0 之和）   |
| `model`          | integer | 挂载的模型数（去重）                                   |
| `tool`           | integer | 挂载的 MCP 工具数（去重）                              |
| `skill`          | integer | 挂载的技能数（去重，仅 Managed Agent 支持）                |
| `knowledge_base` | integer | 挂载的知识库数（去重，仅 Agent 1.0/2.0）                  |
| `memory`         | integer | 挂载的记忆库数（去重，仅 Agent 1.0/2.0）                  |
| `channel`        | integer | 发布渠道数（排除已删除、已过期）                             |
| `network`        | integer | 联网搜索种类数（按模型去重，仅 `enable_web_search=true` 计入） |
| `identity`       | integer | 人设种类数（按名称去重）                                 |

<Note>
  取不到的字段返回 `null`，不返回 0。
</Note>

## 响应示例 <span id="h-secapi-assets-resp-eg" />

```json
{
  "success": true,
  "data": {
    "agent_count": 42,
    "model": 7,
    "tool": 13,
    "skill": 5,
    "knowledge_base": 3,
    "memory": 2,
    "channel": 8,
    "network": 2,
    "identity": 4
  }
}
```
