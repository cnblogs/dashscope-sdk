> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 搜索记忆

> 基于语义检索返回与查询最相关的历史记忆

<meta property="og:title" content="搜索记忆" />

根据对话消息进行相似度检索，返回与查询相关的记忆节点。

## 请求方法与路径

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes/search`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                                   | 类型             | 必填   | 说明                                                          |
| ------------------------------------ | -------------- | ---- | ----------------------------------------------------------- |
| `messages`                           | array          | 是    | 查询所使用的对话消息列表                                                |
| `messages[].role`                    | string         | 否    | 消息角色                                                        |
| `messages[].content`                 | string / array | 否    | 消息内容，支持文本字符串或多模态内容数组                                        |
| `messages[].content[].type`          | string         | 否    | 内容类型：`text` 或 `image_url`。仅支持多模态能力的项目会解析图片                  |
| `messages[].content[].text`          | string         | 条件必填 | 文本内容。`type` 为 `text` 时传入                                    |
| `messages[].content[].image_url`     | object         | 条件必填 | 图片信息。`type` 为 `image_url` 时传入                               |
| `messages[].content[].image_url.url` | string         | 条件必填 | 图片 URL                                                      |
| `user_id`                            | string         | 是    | 子用户 ID，用于隔离记忆                                               |
| `memory_library_id`                  | string         | 否    | 记忆库 ID                                                      |
| `project_id`                         | string         | 否    | 项目 ID，用于二级隔离，与 `project_ids` 互斥                             |
| `project_ids`                        | array\[string] | 否    | 多个项目 ID，与 `project_id` 互斥                                   |
| `top_k`                              | integer        | 否    | 最大召回数量，默认值为 `10`                                            |
| `min_score`                          | number         | 否    | 最小分数阈值，低于该分数的结果会被过滤。默认值为 `0.3`，仅 `plan_version` 为 `pro` 时生效 |
| `memory_types`                       | array\[string] | 否    | 记忆类型筛选。可选值为 `observation`、`skill`，默认值为 `["observation"]`    |
| `plan_version`                       | string         | 否    | 收费计划：`pro` 或 `lite`，默认值为 `pro`。不区分大小写，传入其他值会报错              |

<Note>
  `pro` 计划支持按 `min_score` 过滤结果；`lite` 计划不使用该参数。计费信息请参见[计费说明](/zh/model-studio/memory/billing)。
</Note>

## 返回结果

| 字段                                 | 类型      | 说明                         |
| ---------------------------------- | ------- | -------------------------- |
| `request_id`                       | string  | 请求 ID                      |
| `plan_version`                     | string  | 本次搜索生效的收费计划：`pro` 或 `lite` |
| `memory_nodes`                     | array   | 记忆节点列表                     |
| `memory_nodes[].memory_node_id`    | string  | 记忆节点 ID                    |
| `memory_nodes[].content`           | string  | 记忆内容                       |
| `memory_nodes[].timestamp`         | integer | 记忆相关时间                     |
| `memory_nodes[].created_at`        | integer | 创建时间                       |
| `memory_nodes[].updated_at`        | integer | 更新时间                       |
| `memory_nodes[].media_desc`        | string  | 多模态信息描述                    |
| `memory_nodes[].meta_data`         | object  | 自定义元信息                     |
| `memory_nodes[].memory_type`       | string  | 记忆类型                       |
| `memory_nodes[].status`            | string  | 记忆状态                       |
| `memory_nodes[].score`             | number  | 搜索分数                       |
| `memory_nodes[].project_id`        | string  | 项目 ID，存在时返回                |
| `memory_nodes[].multimodal_medias` | array   | 多模态资源，存在时返回                |

## 请求示例

<CodeGroup>
  ```bash cURL
  curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes/search' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
      "user_id": "user_001",
      "memory_library_id": "memory_library_001",
      "project_ids": ["project_001"],
      "messages": [
        {"role": "user", "content": "明天上午十一点我有什么日程安排？"}
      ],
      "top_k": 10,
      "min_score": 0.3,
      "memory_types": ["observation"],
      "plan_version": "pro"
    }'
  ```

  ```python Python
  from agentscope_runtime.tools.modelstudio_memory import (
      SearchMemory, SearchMemoryInput, Message,
  )
  import asyncio

  async def main():
      search_memory = SearchMemory()
      try:
          result = await search_memory.arun(SearchMemoryInput(
              user_id="user_001",
              messages=[Message(role="user", content="我需要做什么？")],
              top_k=10,
              min_score=0.6
          ))
          for node in result.memory_nodes:
              print(f"记忆: {node.content}")
      finally:
          await search_memory.close()

  asyncio.run(main())
  ```
</CodeGroup>

## 响应示例

```json
{
  "request_id": "416f3d78-2bba-9852-ac15-4b3bcee6c7e4",
  "plan_version": "pro",
  "memory_nodes": [
    {
      "memory_node_id": "cafff28fd3a44c04ac5a65d1ea0858b7",
      "content": "用户需要明天上午十一点被提醒点外卖",
      "timestamp": 1783267200,
      "created_at": 1783254720,
      "updated_at": 1783254720,
      "media_desc": "",
      "meta_data": {"location_name": "北京"},
      "memory_type": "observation",
      "status": "valid",
      "score": 0.651,
      "project_id": "project_001",
      "multimodal_medias": []
    }
  ]
}
```

<Tip>
  查看分页记忆列表使用 [ListMemory](/zh/model-studio/memory/list-memory)。
</Tip>
