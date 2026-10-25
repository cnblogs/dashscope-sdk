> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询 Agent 列表

> 分页查询当前租户下的 Agent 列表。

<meta property="og:title" content="查询 Agent 列表" />

## 前提 <span id="h-ragapi-list-prereq" />

已获取阿里云百炼 API Key 与业务空间 ID，鉴权方式见[API 认证](/zh/model-studio/rag-api-authentication)。本接口为读操作，仅需 API Key。

## 接口 <span id="h-ragapi-list-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/app/list`

分页查询指定场景下的 Agent 列表，支持按状态、名称、Agent ID、关联知识库 Pipeline ID 等条件过滤。

## 请求体 <span id="h-ragapi-list-req" />

| 字段             | 必填 | 类型      | 说明                                                 |
| -------------- | -- | ------- | -------------------------------------------------- |
| `agent_scene`  | 是  | string  | Agent 场景：`chat`（知识问答）或 `search`（知识检索）。             |
| `agent_status` | 否  | string  | 按状态过滤：`draft` / `deployed`（含 `edited`）/ `deleted`。 |
| `agent_name`   | 否  | string  | 按名称模糊搜索。                                           |
| `agent_id`     | 否  | string  | 精确匹配 Agent ID。                                     |
| `pipeline_id`  | 否  | string  | 按关联知识库 Pipeline ID 过滤。                             |
| `page_number`  | 否  | integer | 页码，默认 1。                                           |
| `page_size`    | 否  | integer | 每页条数，默认 10，上限 100。                                 |

## 请求示例 <span id="h-ragapi-list-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/app/list" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "agent_scene": "chat",
    "page_number": 1,
    "page_size": 20
}'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-list-resp" />

操作成功返回 200。

```json
{
  "request_id": "e5f6a7b8-c9d0-1234-efab-345678901234",
  "status_code": 200,
  "code": "Success",
  "message": "",
  "status": "SUCCESS",
  "success": true,
  "data": {
    "page_number": 1,
    "page_size": 20,
    "total_count": 2,
    "rows": [
      {
        "agent_id": "aid-8f3a1b2c4d5e6f70",
        "agent_name": "客服知识问答助手",
        "agent_scene": "chat",
        "agent_status": "deployed",
        "agent_version": "2",
        "create_time": "2026-07-10T14:30:00",
        "modify_time": "2026-07-15T09:15:00",
        "pipeline_list": [
          {
            "pipeline_id": "pipeline-xxxxxx",
            "pipeline_name": "产品文档知识库"
          }
        ]
      },
      {
        "agent_id": "aid-1a2b3c4d5e6f7a8b",
        "agent_name": "技术文档检索助手",
        "agent_scene": "chat",
        "agent_status": "draft",
        "agent_version": "beta",
        "create_time": "2026-07-12T10:00:00",
        "modify_time": "2026-07-12T10:00:00",
        "pipeline_list": [
          {
            "pipeline_id": "pipeline-yyyyyy",
            "pipeline_name": "技术文档知识库"
          },
          {
            "pipeline_id": "pipeline-zzzzzz",
            "pipeline_name": "FAQ 知识库"
          }
        ]
      }
    ]
  }
}
```

## 响应字段 <span id="h-ragapi-list-dto" />

| 字段                 | 类型             | 说明                         |
| ------------------ | -------------- | -------------------------- |
| `request_id`       | string         | 请求唯一标识，排查问题时请提供此 ID。       |
| `status_code`      | integer        | HTTP 状态码。                  |
| `code`             | string         | 响应码，成功时为 `Success`。        |
| `message`          | string         | 提示信息。                      |
| `status`           | string         | 请求状态：`SUCCESS` 或 `FAILED`。 |
| `success`          | boolean        | 请求是否成功。                    |
| `data.page_number` | integer        | 当前页码。                      |
| `data.page_size`   | integer        | 每页条数。                      |
| `data.total_count` | integer        | 总记录数。                      |
| `data.rows`        | array\<object> | Agent 列表。元素字段见下表。          |

<strong>data.rows\[] 元素</strong>

| 字段              | 类型             | 说明                                              |
| --------------- | -------------- | ----------------------------------------------- |
| `agent_id`      | string         | Agent ID。                                       |
| `agent_name`    | string         | Agent 名称。                                       |
| `agent_scene`   | string         | Agent 场景：`chat` 或 `search`。                     |
| `agent_status`  | string         | Agent 状态：`draft`、`deployed`、`edited`、`deleted`。 |
| `agent_version` | string         | Agent 当前版本号。                                    |
| `create_time`   | string         | 创建时间（ISO 8601 格式）。                              |
| `modify_time`   | string         | 修改时间（ISO 8601 格式）。                              |
| `pipeline_list` | array\<object> | 关联的知识库列表。元素字段见下表。                               |

<strong>data.rows\[].pipeline\_list\[] 元素</strong>

| 字段              | 类型     | 说明               |
| --------------- | ------ | ---------------- |
| `pipeline_id`   | string | 知识库 Pipeline ID。 |
| `pipeline_name` | string | 知识库名称。           |

## 错误码 <span id="h-ragapi-list-err" />

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
