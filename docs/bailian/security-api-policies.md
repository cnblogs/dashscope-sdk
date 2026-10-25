> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询安全策略

> 查询安全策略列表。共 11 条策略，固定返回全量，不分页。

<meta property="og:title" content="查询安全策略" />

## 前提 <span id="h-secapi-policies-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/security-api-overview)。

## 接口 <span id="h-secapi-policies-endpoint" />

<strong>GET</strong> `/policies`

查询安全策略列表。共 11 条策略，固定返回全量，不分页。

## 请求示例 <span id="h-secapi-policies-eg" />

```bash
curl -X GET "$BASE_URL/policies" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/security`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应体 <span id="h-secapi-policies-resp" />

| 字段            | 类型      | 说明                      |
| ------------- | ------- | ----------------------- |
| `policy_code` | string  | 策略标识，取值见下表              |
| `risk_domain` | string  | 策略分组，取值见下表              |
| `enabled`     | boolean | `true` 已启用，`false` 未启用  |
| `free`        | boolean | 是否为免费能力。免费策略未开通高级防护时仍生效 |

`policy_code` 取值：

| policy\_code          | risk\_domain         | 名称          |
| --------------------- | -------------------- | ----------- |
| content\_safety       | model\_interaction   | 内容安全        |
| prompt\_attack        | model\_interaction   | 提示词攻击       |
| sensitive\_data\_leak | model\_interaction   | 敏感数据外泄      |
| tool\_invoke          | runtime\_tool        | 工具调用安全      |
| skill\_scan           | runtime\_tool        | Skills 安全检测 |
| network\_protection   | runtime\_tool        | 网络防护        |
| rag\_poisoning        | knowledge\_memory    | RAG 数据投毒    |
| memory\_theft         | knowledge\_memory    | 记忆窃取        |
| identity\_credential  | identity\_credential | 身份与凭证安全     |
| baseline\_check       | config\_component    | 基线检查（免费）    |
| vulnerability\_scan   | config\_component    | 漏洞检测（免费）    |

`risk_domain` 取值：`model_interaction`（模型交互安全）、`runtime_tool`（运行环境与工具安全）、`knowledge_memory`（知识与记忆安全）、`identity_credential`（身份与凭证安全）、`config_component`（配置与组件安全）。

## 响应示例 <span id="h-secapi-policies-resp-eg" />

```json
{
  "success": true,
  "data": [
    {"policy_code": "content_safety",      "risk_domain": "model_interaction",   "enabled": false, "free": false},
    {"policy_code": "prompt_attack",       "risk_domain": "model_interaction",   "enabled": true,  "free": false},
    {"policy_code": "sensitive_data_leak", "risk_domain": "model_interaction",   "enabled": true,  "free": false},
    {"policy_code": "tool_invoke",         "risk_domain": "runtime_tool",        "enabled": true,  "free": false},
    {"policy_code": "skill_scan",          "risk_domain": "runtime_tool",        "enabled": true,  "free": false},
    {"policy_code": "network_protection",  "risk_domain": "runtime_tool",        "enabled": false, "free": false},
    {"policy_code": "rag_poisoning",       "risk_domain": "knowledge_memory",    "enabled": true,  "free": false},
    {"policy_code": "memory_theft",        "risk_domain": "knowledge_memory",    "enabled": true,  "free": false},
    {"policy_code": "identity_credential", "risk_domain": "identity_credential", "enabled": true,  "free": false},
    {"policy_code": "baseline_check",      "risk_domain": "config_component",    "enabled": true,  "free": true},
    {"policy_code": "vulnerability_scan",  "risk_domain": "config_component",    "enabled": true,  "free": true}
  ]
}
```
