> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询告警列表

> 查询安全告警列表，游标分页。支持按风险等级、处置状态、资产类型等条件筛选。

<meta property="og:title" content="查询告警列表" />

## 前提 <span id="h-secapi-alerts-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/security-api-overview)。

## 接口 <span id="h-secapi-alerts-endpoint" />

<strong>GET</strong> `/agent_logs`

查询安全告警列表，游标分页。支持按风险等级、处置状态、资产类型等条件筛选。

## 请求参数 <span id="h-secapi-alerts-params" />

| 参数             | 必填 | 类型      | 说明                                                                          |
| -------------- | -- | ------- | --------------------------------------------------------------------------- |
| `current_page` | 否  | integer | 当前页码，默认 1                                                                   |
| `page_size`    | 否  | integer | 每页条数，默认 20                                                                  |
| `risk_level`   | 否  | string  | 风险等级筛选：`high` / `medium` / `low`                                            |
| `status`       | 否  | string  | 处置状态筛选                                                                      |
| `status_list`  | 否  | array   | 处置状态批量筛选（多值）                                                                |
| `risk_name`    | 否  | string  | 风险名称筛选                                                                      |
| `app_name`     | 否  | string  | 应用名称筛选                                                                      |
| `asset_type`   | 否  | string  | 资产类型筛选：`agent` / `tool` / `skill` / `knowledge_base` / `memory` / `channel` |
| `vendor`       | 否  | string  | 厂商筛选                                                                        |
| `order_by`     | 否  | string  | 排序字段，默认 `check_time`                                                        |
| `order`        | 否  | string  | 排序方向：`asc` / `desc`，默认 `desc`                                               |
| `lang`         | 否  | string  | 语言：`zh` / `en`                                                              |

## 请求示例 <span id="h-secapi-alerts-eg" />

```bash
curl -X GET "$BASE_URL/agent_logs?current_page=1&page_size=20&risk_level=high&order_by=check_time&order=desc&lang=zh" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/security`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应体 <span id="h-secapi-alerts-resp" />

| 字段          | 类型      | 说明                                                |
| ----------- | ------- | ------------------------------------------------- |
| `stats`     | object  | 统计数据：`total` 总数，`high` / `medium` / `low` 按等级分组数量 |
| `data`      | array   | 告警列表，列表项字段见下表                                     |
| `next_page` | integer | 游标页码，末页返回 `null`                                  |

列表项字段：

| 字段            | 类型     | 说明                                              |
| ------------- | ------ | ----------------------------------------------- |
| `alert_id`    | string | 告警 ID，纯数字，用于查询详情                                |
| `risk_level`  | string | 风险等级：`high` / `medium` / `low`                  |
| `risk_name`   | string | 风险名称                                            |
| `risk_desc`   | string | 风险描述                                            |
| `asset_type`  | string | 资产类型                                            |
| `asset_name`  | string | 资产名称                                            |
| `app_id`      | string | 应用 ID                                           |
| `app_name`    | string | 应用名称                                            |
| `agent_name`  | string | Agent 名称                                        |
| `status`      | string | 处置状态：`unhandled` / `handling` / `intercepted` 等 |
| `source`      | string | 来源：`aiguard` / `Agent-Runtime-Guard` / `cspm` 等 |
| `check_time`  | string | 检查时间，毫秒时间戳字符串                                   |
| `handle_time` | string | 处置时间，毫秒时间戳字符串                                   |

## 响应示例 <span id="h-secapi-alerts-resp-eg" />

```json
{
  "success": true,
  "data": {
    "stats": {"total": 15, "high": 3, "medium": 7, "low": 5},
    "data": [
      {
        "alert_id": "4289016",
        "risk_level": "high",
        "risk_name": "提示词注入攻击",
        "risk_desc": "提示词泄漏，请求已被拦截",
        "asset_type": "agent",
        "asset_name": "demo_agent",
        "app_id": "app-1001",
        "app_name": "demo_application",
        "agent_name": "demo_agent",
        "status": "intercepted",
        "source": "Agent-Runtime-Guard",
        "check_time": "1788365324000",
        "handle_time": null
      }
    ],
    "next_page": null
  }
}
```
