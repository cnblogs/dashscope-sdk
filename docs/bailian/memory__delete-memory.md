> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 删除记忆

> 删除一条事实记忆，删除后不可恢复

<meta property="og:title" content="删除记忆" />

删除指定的记忆节点。

<Warning>
  删除操作不可逆，请谨慎操作。
</Warning>

## 请求方法与路径

`DELETE https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes/{memory_node_id}`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                  | 类型     | 必填 | 位置    | 说明                 |
| ------------------- | ------ | -- | ----- | ------------------ |
| `memory_node_id`    | string | 是  | Path  | 记忆节点 ID            |
| `memory_library_id` | string | 否  | Query | 记忆库 ID。不传时按默认记忆库解析 |

## 返回结果

| 字段           | 类型     | 说明    |
| ------------ | ------ | ----- |
| `request_id` | string | 请求 ID |

## 请求示例

<CodeGroup>
  ```bash cURL
  curl --location --request DELETE 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes/42dfc089dfa7409889966960a95c3b7e?memory_library_id=memory_library_001' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
  ```

  ```python Python
  from agentscope_runtime.tools.modelstudio_memory import (
      DeleteMemory, DeleteMemoryInput,
  )
  import asyncio

  async def main():
      delete_memory = DeleteMemory()
      try:
          await delete_memory.arun(DeleteMemoryInput(
              user_id="user_001",
              memory_node_id="MEMORY_NODE_ID"
          ))
          print("删除成功")
      finally:
          await delete_memory.close()

  asyncio.run(main())
  ```
</CodeGroup>

## 响应示例

```json
{
  "request_id": "a1b2c3d4-5678-90ab-cdef-1234567890ab"
}
```

<Tip>
  用户画像 API 参见 [CreateProfileSchema](/zh/model-studio/memory/create-schema)。
</Tip>
