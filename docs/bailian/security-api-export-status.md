> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询导出状态

> 查询导出任务状态与下载链接。仅 export_status 为 success 且 link 非空时可下载。

<meta property="og:title" content="查询导出状态" />

## 前提 <span id="h-secapi-exportstatus-prereq" />

已完成 Endpoint 与鉴权配置，详见[API 总览与认证](/zh/model-studio/security-api-overview)。

## 接口 <span id="h-secapi-exportstatus-endpoint" />

<strong>GET</strong> `/export_status`

查询导出任务状态与下载链接。仅 `export_status` 为 `success` 且 `link` 非空时可下载。

## 请求参数 <span id="h-secapi-exportstatus-params" />

| 参数          | 必填 | 类型      | 说明                  |
| ----------- | -- | ------- | ------------------- |
| `export_id` | 是  | integer | 导出任务 ID，取自导出告警接口返回值 |

## 请求示例 <span id="h-secapi-exportstatus-eg" />

```bash
curl -X GET "$BASE_URL/export_status?export_id=131231" \
  -H "Authorization: Bearer $BAILIAN_API_KEY"
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/agentstudio/security`，`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应体 <span id="h-secapi-exportstatus-resp" />

| 字段              | 类型      | 说明                                                           |
| --------------- | ------- | ------------------------------------------------------------ |
| `export_status` | string  | `init` / `exporting` / `success`。仅 `success` 且 `link` 非空时可下载 |
| `progress`      | integer | 导出进度百分比，0-100                                                |
| `current_count` | integer | 当前已导出条数                                                      |
| `total_count`   | integer | 导出任务记录总数                                                     |
| `link`          | string  | Excel 下载链接，任务未完成或失败时为 `null`                                 |
| `message`       | string  | 结果提示，成功时固定为 `success`                                        |

## 响应示例 <span id="h-secapi-exportstatus-resp-eg" />

```json
{
  "success": true,
  "data": {
    "export_status": "exporting",
    "progress": 20,
    "current_count": 20,
    "total_count": 100,
    "link": null,
    "message": null
  }
}
```
