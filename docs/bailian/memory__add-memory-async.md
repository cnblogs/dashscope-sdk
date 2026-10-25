> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 异步添加记忆

> 异步将对话存储为记忆，通过事件 ID 查询执行状态与结果

<meta property="og:title" content="异步添加记忆" />

异步提交记忆抽取任务。接口立即返回事件 ID，实际结果通过 [GetEvent](/zh/model-studio/memory/get-event) 查询。该接口支持多项目并行抽取、技能抽取和用户画像抽取。

<Note>
  调用前请确保已获取 API Key，详见[鉴权](/zh/model-studio/memory/authentication)。
</Note>

## 请求方法与路径

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/add-async`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                                           | 类型             | 必填   | 说明                                                                                      |
| -------------------------------------------- | -------------- | ---- | --------------------------------------------------------------------------------------- |
| `messages`                                   | array          | 二选一  | 对话消息列表。`messages` 与 `custom_content` 至少传入一个                                             |
| `messages[].role`                            | string         | 否    | 消息角色，支持 `user`、`assistant` 和 `tool`                                                     |
| `messages[].content`                         | string / array | 否    | 消息内容。可传文本字符串，或传入包含文本和图片的多模态内容数组                                                         |
| `messages[].content[].type`                  | string         | 否    | 内容类型：`text` 或 `image_url`。仅支持多模态能力的项目会解析图片，其他项目不进行多模态解析                                 |
| `messages[].content[].text`                  | string         | 条件必填 | 文本内容。`type` 为 `text` 时传入                                                                |
| `messages[].content[].image_url`             | object         | 条件必填 | 图片信息。`type` 为 `image_url` 时传入                                                           |
| `messages[].content[].image_url.url`         | string         | 条件必填 | 图片 URL                                                                                  |
| `messages[].tool_calls`                      | array          | 否    | `assistant` 发起的工具调用，采用 OpenAI 格式。工具消息仅用于 skill 抽取，其他抽取类型会过滤工具消息                         |
| `messages[].tool_calls[].id`                 | string         | 否    | 工具调用 ID                                                                                 |
| `messages[].tool_calls[].type`               | string         | 否    | 工具调用类型，固定为 `function`                                                                   |
| `messages[].tool_calls[].function`           | object         | 条件必填 | 函数调用信息。存在 `tool_calls` 时必须传入                                                            |
| `messages[].tool_calls[].function.name`      | string         | 条件必填 | 函数名称。存在工具调用时必须传入                                                                        |
| `messages[].tool_calls[].function.arguments` | string         | 否    | 函数参数，必须是 JSON 字符串，而不是对象                                                                 |
| `messages[].tool_call_id`                    | string         | 条件必填 | `role` 为 `tool` 时，传入对应的工具调用 ID。工具消息仅用于 skill 抽取                                         |
| `custom_content`                             | string         | 二选一  | 自定义内容。传入后直接保存，不再基于 `messages` 抽取；不能与 `project_ids` 同时传入                                 |
| `user_id`                                    | string         | 是    | 子用户 ID，用于隔离记忆                                                                           |
| `memory_library_id`                          | string         | 否    | 记忆库 ID，不传则使用默认记忆库                                                                       |
| `project_id`                                 | string         | 否    | 自定义项目 ID，与 `project_ids` 互斥                                                             |
| `project_ids`                                | array\[string] | 否    | 多个自定义项目 ID，最多 5 个，与 `project_id` 互斥；不能与 `custom_content` 同时传入                           |
| `profile_schema`                             | string         | 否    | 用户画像模板 ID；需要提取用户画像时传入                                                                   |
| `extract_mode`                               | string         | 否    | 抽取模式。`profile_only` 表示仅抽取画像                                                             |
| `skill_name`                                 | string         | 条件必填 | 项目为 skill 且传入 `custom_content` 时，必须同时传入 `skill_name`、`skill_description` 和 `skill_tags` |
| `skill_description`                          | string         | 条件必填 | skill 描述。必填条件同 `skill_name`                                                             |
| `skill_tags`                                 | array\[string] | 条件必填 | skill 标签。必填条件同 `skill_name`                                                             |
| `meta_data`                                  | object         | 否    | 自定义元信息                                                                                  |

## 返回结果

| 字段                           | 类型      | 说明                                                                                |
| ---------------------------- | ------- | --------------------------------------------------------------------------------- |
| `request_id`                 | string  | 请求 ID                                                                             |
| `event_id`                   | string  | 执行任务事件 ID                                                                         |
| `events`                     | array   | 执行任务列表                                                                            |
| `events[].created_at`        | integer | 创建时间                                                                              |
| `events[].event_id`          | string  | 事件 ID                                                                             |
| `events[].event_type`        | string  | 事件类型，例如 `ADD_ASYNC`                                                               |
| `events[].memory_library_id` | string  | 任务对应的记忆库 ID                                                                       |
| `events[].resource_id`       | string  | 任务来源 ID，例如项目 ID 或画像模板 ID                                                          |
| `events[].resource_type`     | string  | 任务来源类型：`observation`、`skill`、`user_profile`、`custom_observation` 或 `custom_skill` |
| `events[].status`            | string  | 任务状态：`PENDING`、`RUNNING`、`SUCCEEDED`、`FAILED` 或 `UNRECORDED`                      |
| `events[].updated_at`        | integer | 状态最后更新时间                                                                          |
| `events[].user_id`           | string  | 任务对应的子用户 ID                                                                       |
| `events[].result`            | array   | 执行结果。首次响应为空                                                                       |

## 请求示例

```bash cURL
curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/add-async' \
  --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
  --header 'Content-Type: application/json' \
  --data '{
    "memory_library_id": "memory_library_001",
    "project_ids": ["project_001"],
    "user_id": "user_001",
    "messages": [
      {"role": "user", "content": "每天上午11点提醒我点外卖，明天提醒我穿衣服。"},
      {"role": "assistant", "content": "没问题。"}
    ],
    "meta_data": {"location_name": "北京"}
  }'
```

## 响应示例

```json
{
  "request_id": "048983ff-ed50-96e0-b0a6-482cce26cbe3",
  "event_id": "9d30e77a2f8e42378bf533406fdc09d9",
  "events": [
    {
      "created_at": 1784638015,
      "event_id": "9d30e77a2f8e42378bf533406fdc09d9",
      "event_type": "ADD_ASYNC",
      "memory_library_id": "memory_library_001",
      "resource_id": "project_001",
      "resource_type": "observation",
      "status": "PENDING",
      "updated_at": 1784638015,
      "user_id": "user_001",
      "result": []
    }
  ]
}
```

<Tip>
  `status` 为 `PENDING` 或 `RUNNING` 表示任务仍在后台执行，请使用 [GetEvent](/zh/model-studio/memory/get-event) 查询结果。
</Tip>
