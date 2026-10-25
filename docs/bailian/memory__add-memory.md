> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 添加记忆

> 将对话存储为事实记忆，自动提取关键信息

<meta property="og:title" content="添加记忆" />

将对话记录或自定义内容同步保存为记忆，并在响应中直接返回提取结果。当前主推[异步添加记忆](/zh/model-studio/memory/add-memory-async)；同步接口适合在 `efficient` 模式下对实时性要求较高的场景，在 `intelligent` 模式下可能超时。

<Note>
  调用前请确保已获取 API Key，详见[鉴权](/zh/model-studio/memory/authentication)。
</Note>

## 请求方法与路径

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/add`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                                           | 类型             | 必填   | 说明                                                                 |
| -------------------------------------------- | -------------- | ---- | ------------------------------------------------------------------ |
| `messages`                                   | array          | 二选一  | 对话消息列表。`messages` 与 `custom_content` 至少传入一个                        |
| `messages[].role`                            | string         | 否    | 消息角色，支持 `user`、`assistant` 和 `tool`                                |
| `messages[].content`                         | string / array | 否    | 消息内容。可传文本字符串，或传入包含文本和图片的多模态内容数组                                    |
| `messages[].content[].type`                  | string         | 否    | 内容类型：`text` 或 `image_url`。仅支持多模态能力的项目会解析图片，其他项目不进行多模态解析            |
| `messages[].content[].text`                  | string         | 条件必填 | 文本内容。`type` 为 `text` 时传入                                           |
| `messages[].content[].image_url`             | object         | 条件必填 | 图片信息。`type` 为 `image_url` 时传入                                      |
| `messages[].content[].image_url.url`         | string         | 条件必填 | 图片 URL                                                             |
| `messages[].tool_calls`                      | array          | 否    | `assistant` 发起的工具调用，采用 OpenAI 格式。工具消息仅用于 skill 抽取，其他抽取类型会过滤工具消息    |
| `messages[].tool_calls[].id`                 | string         | 否    | 工具调用 ID                                                            |
| `messages[].tool_calls[].type`               | string         | 否    | 工具调用类型，固定为 `function`                                              |
| `messages[].tool_calls[].function`           | object         | 条件必填 | 函数调用信息。存在 `tool_calls` 时必须传入                                       |
| `messages[].tool_calls[].function.name`      | string         | 条件必填 | 函数名称。存在工具调用时必须传入                                                   |
| `messages[].tool_calls[].function.arguments` | string         | 否    | 函数参数，必须是 JSON 字符串，而不是对象                                            |
| `messages[].tool_call_id`                    | string         | 条件必填 | `role` 为 `tool` 时，传入对应的工具调用 ID。工具消息仅用于 skill 抽取                    |
| `custom_content`                             | string         | 二选一  | 自定义内容。传入后直接保存，不再基于 `messages` 抽取                                   |
| `user_id`                                    | string         | 是    | 子用户 ID，用于隔离记忆                                                      |
| `memory_library_id`                          | string         | 否    | 记忆库 ID，不传则使用默认记忆库                                                  |
| `project_id`                                 | string         | 否    | 自定义项目 ID，用于记忆二级隔离                                                  |
| `profile_schema`                             | string         | 否    | 用户画像模板 ID；需要提取用户画像时传入                                              |
| `extract_mode`                               | string         | 否    | 抽取模式。`profile_only` 表示仅抽取画像，此时必须同时传入 `profile_schema` 和 `messages` |
| `meta_data`                                  | object         | 否    | 自定义元信息                                                             |

## 返回结果

| 字段                              | 类型     | 说明                                   |
| ------------------------------- | ------ | ------------------------------------ |
| `request_id`                    | string | 请求 ID                                |
| `memory_nodes`                  | array  | 发生变更的记忆列表                            |
| `memory_nodes[].memory_node_id` | string | 记忆节点 ID                              |
| `memory_nodes[].content`        | string | 记忆内容                                 |
| `memory_nodes[].event`          | string | 记忆事件：`ADD`、`UPDATE` 或 `DELETE`，存在时返回 |
| `memory_nodes[].old_content`    | string | 更新前的内容，仅 `event` 为 `UPDATE` 时返回      |

## 请求示例

<CodeGroup>
  ```bash cURL
  curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/add' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
      "user_id": "user_001",
      "messages": [
        {"role": "user", "content": "每天上午11点提醒我点外卖。"},
        {"role": "assistant", "content": "没问题。"}
      ],
      "meta_data": {"location_name": "北京"}
    }'
  ```

  ```python Python
  from agentscope_runtime.tools.modelstudio_memory import (
      AddMemory, AddMemoryInput, Message,
  )
  import asyncio

  async def main():
      add_memory = AddMemory()
      try:
          await add_memory.arun(AddMemoryInput(
              user_id="user_001",
              messages=[
                  Message(role="user", content="每天上午9点提醒我喝水"),
                  Message(role="assistant", content="好的，已记录"),
                  Message(role="user", content="明天10点提醒我整理会议纪要。"),
              ]
          ))
          print("记忆写入成功")
      finally:
          await add_memory.close()

  asyncio.run(main())
  ```
</CodeGroup>

## 响应示例

```json
{
  "request_id": "048983ff-ed50-96e0-b0a6-482cce26cbe3",
  "memory_nodes": [
    {
      "memory_node_id": "42dfc089dfa7409889966960a95c3b7e",
      "content": "用户需要每天上午11点被提醒点外卖",
      "event": "ADD"
    }
  ]
}
```

<Tip>
  写入后，使用 [SearchMemory](/zh/model-studio/memory/search-memory) 检索记忆。
</Tip>
