> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取知识库监控数据

> 查询指定知识库在给定时间范围内的存储用量和 QPS 监控数据。

<meta property="og:title" content="获取知识库监控数据" />

## 前提 <span id="h-ragapi-get-index-monitor-prereq" />

已获取 API Key 和业务空间 ID，并完成鉴权配置，详见[API 总览](/zh/model-studio/rag-api-overview)与[鉴权说明](/zh/model-studio/rag-api-authentication)。

## 接口 <span id="h-ragapi-get-index-monitor-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/index/monitor`

查询指定知识库在给定时间范围内的存储用量和 QPS 监控数据。

<Warning>
  时间戳为**秒级** Unix 时间戳（不是毫秒）。建议以字符串格式传入（如 `"1780900000"`），也支持整数格式。
</Warning>

## 请求体 <span id="h-ragapi-get-index-monitor-req" />

| 字段               | 必填 | 类型               | 说明                                                                   |
| ---------------- | -- | ---------------- | -------------------------------------------------------------------- |
| `indexId`        | 是  | string           | 知识库 ID                                                               |
| `startTimestamp` | 是  | string 或 integer | 查询起始时间，秒级 Unix 时间戳（不是毫秒），支持字符串或整数格式（如 `"1780900000"` 或 `1780900000`） |
| `endTimestamp`   | 是  | string 或 integer | 查询结束时间，格式同 startTimestamp。结束时间最晚不超过起始时间 + 30 天                       |

## 请求示例 <span id="h-ragapi-get-index-monitor-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/index/monitor" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "indexId": "your-kb-id",
    "startTimestamp": "1780900000",
    "endTimestamp": "1781000000"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-get-index-monitor-resp" />

查询成功返回 200。

```json
{
  "code": "Success",
  "status_code": 200,
  "data": {
    "storageMonitorData": [],
    "qpsMonitorData": []
  },
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

### 响应字段 <span id="h-ragapi-get-index-monitor-dto" />

| 字段            | 类型      | 说明                        |
| ------------- | ------- | ------------------------- |
| `code`        | string  | 响应码，成功时为 `Success`        |
| `status_code` | integer | HTTP 状态码                  |
| `request_id`  | string  | 请求唯一标识，排查问题时请提供此 ID       |
| `success`     | boolean | 请求是否成功                    |
| `message`     | string  | 提示信息，成功时为 `success`       |
| `status`      | string  | 请求状态：`SUCCESS` 或 `FAILED` |
| `data`        | object  | 监控数据，子字段见下表               |

### data 字段 <span id="h-ragapi-get-index-monitor-data" />

| 字段                       | 类型     | 说明                                                                                                                                                                      |
| ------------------------ | ------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `pipelineCommercialType` | string | 知识库商业化类型，如 `standard`                                                                                                                                                   |
| `storageMonitorData`     | object | 存储用量监控数据。子字段 `indexStorageLimit`（number，存储上限，单位 GB）、`indexStorageUsage`（number，已用存储，单位 GB）                                                                              |
| `qpsMonitorData`         | object | QPS 监控数据。子字段 `peakQps`（integer，统计区间内的峰值 QPS）、`totalRequests`（integer，窗口内请求总数）、`avgQpsOfActiveSeconds`（number，窗口内活跃秒的平均 QPS）、`monitorData`（array，按时间窗口聚合的监控数据列表，元素字段见下表） |

### monitorData 元素字段 <span id="h-ragapi-get-index-monitor-dataitems" />

| 字段                      | 类型      | 说明                                                                                                                     |
| ----------------------- | ------- | ---------------------------------------------------------------------------------------------------------------------- |
| `windowRange`           | integer | 窗口开始时间，Unix 时间戳（秒）                                                                                                     |
| `windowRangeEnd`        | integer | 窗口结束时间，Unix 时间戳（秒）                                                                                                     |
| `peakQpsInWindowRange`  | integer | 窗口内峰值 QPS                                                                                                              |
| `totalRequests`         | integer | 窗口内请求总数                                                                                                                |
| `avgQpsOfActiveSeconds` | number  | 窗口内活跃秒的平均 QPS                                                                                                          |
| `successData`           | object  | 子字段 `peakQpsInWindowRange`（integer，窗口内峰值 QPS）、`totalRequests`（integer，请求总数）、`avgQpsOfActiveSeconds`（number，活跃秒的平均 QPS） |
| `limitData`             | object  | 子字段 `peakQpsInWindowRange`（integer，窗口内峰值 QPS）、`totalRequests`（integer，请求总数）、`avgQpsOfActiveSeconds`（number，活跃秒的平均 QPS） |
| `failData`              | object  | 子字段 `peakQpsInWindowRange`（integer，窗口内峰值 QPS）、`totalRequests`（integer，请求总数）、`avgQpsOfActiveSeconds`（number，活跃秒的平均 QPS） |
