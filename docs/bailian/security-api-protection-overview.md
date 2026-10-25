> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询防护总览

> 查询防护总览数据，固定查询最近 24 小时。返回防护能力开关、各模块防护覆盖开关与拦截统计。

<meta property="og:title" content="查询防护总览" />

## 前提 <span id="h-secapi-protover-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/security-api-overview)。

## 接口 <span id="h-secapi-protover-endpoint" />

<strong>GET</strong> `/overview`

查询防护总览数据，固定查询最近 24 小时。

## 请求示例 <span id="h-secapi-protover-eg" />

```bash
curl -X GET "$BASE_URL/overview" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/security`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应体 <span id="h-secapi-protover-resp" />

| 字段               | 类型     | 说明                                          |
| ---------------- | ------ | ------------------------------------------- |
| `capabilities`   | array  | 防护能力开关列表，顺序固定。每项含 `key` 与 `enabled`         |
| `protection`     | array  | 各模块防护覆盖开关列表，结构同 `capabilities`              |
| `content_safety` | object | 内容安全统计卡片，含 `hit`（拦截或异常次数）与 `scanned`（检测总次数） |
| `file_scan`      | object | 文件扫描统计卡片，含 `hit` 与 `scanned`                |
| `skill_scan`     | object | 技能扫描统计卡片，含 `hit` 与 `scanned`                |

`capabilities` 的 `key` 取值：`agent_identity`（Agent 身份签发）、`content_safety`（内容安全）、`supply_chain_scan`（供应链静态扫描）、`credential_isolation`（凭证隔离）、`session_lifecycle`（Session 生命周期治理）。

`protection` 的 `key` 取值：`flow_agent`、`managed_agent`、`knowledge_base`、`memory`、`mcp`、`external_agent`。

顶部汇总的扫描总数为各项 `scanned` 之和，风险数为各项 `hit` 之和。

## 响应示例 <span id="h-secapi-protover-resp-eg" />

```json
{
  "success": true,
  "data": {
    "capabilities": [
      {"key": "agent_identity",       "enabled": true},
      {"key": "content_safety",        "enabled": false},
      {"key": "supply_chain_scan",     "enabled": true},
      {"key": "credential_isolation",  "enabled": true},
      {"key": "session_lifecycle",     "enabled": true}
    ],
    "protection": [
      {"key": "flow_agent",       "enabled": true},
      {"key": "managed_agent",    "enabled": true},
      {"key": "knowledge_base",   "enabled": true},
      {"key": "memory",           "enabled": false},
      {"key": "mcp",              "enabled": true},
      {"key": "external_agent",   "enabled": false}
    ],
    "content_safety": {"hit": 12, "scanned": 3860},
    "file_scan":      {"hit": 3,  "scanned": 96},
    "skill_scan":     {"hit": 1,  "scanned": 20}
  }
}
```
