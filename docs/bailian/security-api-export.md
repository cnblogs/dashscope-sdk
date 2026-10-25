> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 导出告警

> 提交告警导出任务，导出当前筛选条件对应的全部记录。返回的 export_id 用于查询导出状态。

<meta property="og:title" content="导出告警" />

## 前提 <span id="h-secapi-export-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/security-api-overview)。

## 接口 <span id="h-secapi-export-endpoint" />

<strong>POST</strong> `/export_agent_logs`

提交告警导出任务，导出当前筛选条件对应的全部记录。返回的 `export_id` 用于查询导出状态。

## 请求体 <span id="h-secapi-export-req" />

| 字段       | 必填 | 类型     | 说明                          |
| -------- | -- | ------ | --------------------------- |
| `lang`   | 否  | string | 语言：`zh` / `en`，建议与列表查询一致    |
| `params` | 是  | string | 列表查询参数序列化成的 JSON 字符串（非嵌套对象） |

## 请求示例 <span id="h-secapi-export-eg" />

```bash
curl -X POST "$BASE_URL/export_agent_logs" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "lang": "zh",
    "params": "{\"CurrentPage\":1,\"PageSize\":20,\"RiskLevel\":\"high\",\"OrderBy\":\"CheckTime\",\"Order\":\"desc\"}"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/security`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应体 <span id="h-secapi-export-resp" />

| 字段          | 类型      | 说明             |
| ----------- | ------- | -------------- |
| `export_id` | integer | 导出任务 ID，用于查询进度 |

## 响应示例 <span id="h-secapi-export-resp-eg" />

```json
{
  "success": true,
  "data": {
    "export_id": 131231
  }
}
```
