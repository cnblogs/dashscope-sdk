> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 更新记忆

> 更新一条事实记忆的内容

<meta property="og:title" content="更新记忆" />

更新指定记忆节点的内容。接口会自动识别节点类型并格式化内容。

## 请求方法与路径

`PATCH https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes/{memory_node_id}`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                  | 类型             | 必填   | 位置   | 说明                                                                   |
| ------------------- | -------------- | ---- | ---- | -------------------------------------------------------------------- |
| `memory_node_id`    | string         | 是    | Path | 记忆节点 ID                                                              |
| `custom_content`    | string         | 是    | Body | 更新后的记忆内容                                                             |
| `memory_library_id` | string         | 否    | Body | 记忆库 ID                                                               |
| `meta_data`         | object         | 否    | Body | 元信息。增量更新，未指定的键保持不变                                                   |
| `skill_name`        | string         | 条件必填 | Body | 节点为 skill 类型时，必须同时传入 `skill_name`、`skill_description` 和 `skill_tags` |
| `skill_description` | string         | 条件必填 | Body | skill 描述。必填条件同 `skill_name`                                          |
| `skill_tags`        | array\[string] | 条件必填 | Body | skill 标签。必填条件同 `skill_name`                                          |

## 返回结果

| 字段           | 类型     | 说明    |
| ------------ | ------ | ----- |
| `request_id` | string | 请求 ID |

## 请求示例

<CodeGroup>
  ```bash cURL
  curl --location --request PATCH 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes/42dfc089dfa7409889966960a95c3b7e' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
      "custom_content": "更新后的记忆内容",
      "memory_library_id": "memory_library_001",
      "meta_data": {"location_name": "杭州"}
    }'
  ```

  ```python Python
  from agentscope_runtime.tools.modelstudio_memory import UpdateMemory
  import asyncio

  async def main():
      update_memory = UpdateMemory()
      try:
          await update_memory.arun(
              memory_node_id="MEMORY_NODE_ID",
              user_id="user_001",
              custom_content="还要提醒我上午10点吃药。"
          )
          print("更新成功")
      finally:
          await update_memory.close()

  asyncio.run(main())
  ```
</CodeGroup>

## 响应示例

```json
{
  "request_id": "cc2690f9f2b3485a93013e5705c91241"
}
```

<Tip>
  删除记忆使用 [DeleteMemory](/zh/model-studio/memory/delete-memory)。
</Tip>
