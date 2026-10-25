> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询事件

> 查询异步添加记忆事件的执行状态与结果

<meta property="og:title" content="查询事件" />

查询[异步添加记忆](/zh/model-studio/memory/add-memory-async)生成的事件，获取执行状态和抽取结果。

## 请求方法与路径

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/events/{event_id}`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数         | 类型     | 必填 | 位置   | 说明    |
| ---------- | ------ | -- | ---- | ----- |
| `event_id` | string | 是  | Path | 事件 ID |

## 返回结果

| 字段                                 | 类型      | 说明                                                                              |
| ---------------------------------- | ------- | ------------------------------------------------------------------------------- |
| `request_id`                       | string  | 请求 ID                                                                           |
| `events`                           | array   | 事件列表                                                                            |
| `events[].created_at`              | integer | 创建时间                                                                            |
| `events[].event_id`                | string  | 事件 ID                                                                           |
| `events[].event_type`              | string  | 事件类型                                                                            |
| `events[].memory_library_id`       | string  | 记忆库 ID                                                                          |
| `events[].resource_id`             | string  | 资源 ID                                                                           |
| `events[].resource_type`           | string  | 资源类型：`observation`、`skill`、`user_profile`、`custom_observation` 或 `custom_skill` |
| `events[].status`                  | string  | 事件状态：`PENDING`、`RUNNING`、`SUCCEEDED`、`FAILED` 或 `UNRECORDED`                    |
| `events[].updated_at`              | integer | 更新时间                                                                            |
| `events[].user_id`                 | string  | 子用户 ID                                                                          |
| `events[].result`                  | array   | 执行结果。任务成功后返回抽取结果                                                                |
| `events[].result[].memory_type`    | string  | 记忆类型：`observation`、`skill` 或 `user_profile`                                     |
| `events[].result[].content`        | string  | `ADD` 或 `UPDATE` 时为新内容，`DELETE` 时为旧内容                                           |
| `events[].result[].name`           | string  | 用户画像属性名称，仅画像结果存在时返回                                                             |
| `events[].result[].event`          | string  | 操作类型：`ADD`、`UPDATE` 或 `DELETE`                                                  |
| `events[].result[].memory_node_id` | string  | observation 或 skill 的记忆节点 ID；user\_profile 结果不返回                                |
| `events[].result[].old_content`    | string  | 更新前的内容，仅 `event` 为 `UPDATE` 时返回                                                 |

## 请求示例

```bash cURL
curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/events/07c054c438584f279ff7db7c3243fa51' \
  --header "Authorization: Bearer $DASHSCOPE_API_KEY"
```

## 响应示例

```json
{
  "request_id": "bb499327-79c6-9a65-9f2d-c0c6abda6419",
  "events": [
    {
      "created_at": 1784602119,
      "event_id": "07c054c438584f279ff7db7c3243fa51",
      "event_type": "ADD_ASYNC",
      "memory_library_id": "memory_library_001",
      "resource_id": "project_001",
      "resource_type": "observation",
      "status": "SUCCEEDED",
      "updated_at": 1784602150,
      "user_id": "user_001",
      "result": [
        {
          "memory_type": "observation",
          "content": "用户想去非洲",
          "event": "DELETE",
          "memory_node_id": "3127d7445a57442d9f700d534b1b14e3"
        }
      ]
    }
  ]
}
```

<Tip>
  `status` 为 `PENDING` 或 `RUNNING` 时任务仍在后台执行，请稍后重新查询。
</Tip>
