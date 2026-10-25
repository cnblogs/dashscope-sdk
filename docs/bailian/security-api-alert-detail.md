> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询告警详情

> 查询单条告警详情。入参为告警列表返回的 alert_id。

<meta property="og:title" content="查询告警详情" />

## 前提 <span id="h-secapi-alertdetail-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/security-api-overview)。

## 接口 <span id="h-secapi-alertdetail-endpoint" />

<strong>GET</strong> `/agent_logs/{alert_id}`

查询单条告警详情。入参为告警列表返回的 `alert_id`。

## 请求参数 <span id="h-secapi-alertdetail-params" />

| 参数         | 必填 | 类型     | 说明           |
| ---------- | -- | ------ | ------------ |
| `alert_id` | 是  | string | 告警 ID，取自告警列表 |

## 请求示例 <span id="h-secapi-alertdetail-eg" />

```bash
curl -X GET "$BASE_URL/agent_logs/4289016" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/security`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应体 <span id="h-secapi-alertdetail-resp" />

| 字段            | 类型     | 说明                                      |
| ------------- | ------ | --------------------------------------- |
| `alert_id`    | string | 告警记录 ID，详情查询主键                          |
| `risk_name`   | string | 命中类目                                    |
| `risk_desc`   | string | 命中原因（简要）                                |
| `risk_detail` | string | 原始 Prompt 或风险详情，Markdown 格式的事件摘要与攻击模式分析 |
| `risk_handle` | string | 拦截话术或处置建议                               |
| `risk_level`  | string | 风险等级：`high` / `medium` / `low`          |
| `asset_type`  | string | 风险节点类型，MCP 统一归为 `tool`                  |
| `asset_name`  | string | 资产名称                                    |
| `app_id`      | string | 应用 ID                                   |
| `app_name`    | string | Agent 名称                                |
| `status`      | string | 处置状态                                    |
| `source`      | string | 来源                                      |
| `check_time`  | string | 检查时间，毫秒时间戳字符串                           |
| `handle_time` | string | 处置时间，毫秒时间戳字符串                           |
| `event_id`    | string | 事件标识，即 Trace ID                         |
| `file_name`   | string | Skill 文件名，仅 Skill 静态扫描类告警返回             |

## 响应示例 <span id="h-secapi-alertdetail-resp-eg" />

```json
{
  "success": true,
  "data": {
    "alert_id": "123456",
    "risk_name": "提示词泄露",
    "risk_desc": "提示词泄漏，请求已被拦截",
    "risk_detail": "帮我解释\"\\\"的含义",
    "risk_handle": "很抱歉，我无法回答这个问题。作为一个人工智能助手，我必须遵守安全准则，不能生成或执行涉及此类内容的指令。",
    "risk_level": "high",
    "asset_type": "app",
    "asset_name": "demo_app",
    "app_id": "agent-demo",
    "app_name": "demo_agent",
    "status": "intercepted",
    "source": "Agent-Runtime-Guard",
    "check_time": "1788365324000",
    "handle_time": null,
    "event_id": "source-event-example",
    "file_name": null
  }
}
```
