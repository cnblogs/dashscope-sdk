> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 列出记忆

> 分页查看用户的全部事实记忆

<meta property="og:title" content="列出记忆" />

分页列出指定子用户的记忆节点，可按记忆库和项目筛选。

## 请求方法与路径

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                  | 类型      | 必填 | 位置    | 说明             |
| ------------------- | ------- | -- | ----- | -------------- |
| `user_id`           | string  | 是  | Query | 子用户 ID，用于隔离记忆  |
| `memory_library_id` | string  | 否  | Query | 记忆库 ID         |
| `project_id`        | string  | 否  | Query | 项目 ID          |
| `page_num`          | integer | 否  | Query | 页码，默认值为 `1`    |
| `page_size`         | integer | 否  | Query | 每页数量，默认值为 `10` |

## 返回结果

| 字段                                 | 类型      | 说明                  |
| ---------------------------------- | ------- | ------------------- |
| `request_id`                       | string  | 请求 ID               |
| `memory_nodes`                     | array   | 记忆节点列表              |
| `memory_nodes[].memory_node_id`    | string  | 记忆节点 ID             |
| `memory_nodes[].content`           | string  | 记忆内容                |
| `memory_nodes[].timestamp`         | integer | 消息时间戳               |
| `memory_nodes[].created_at`        | integer | 创建时间                |
| `memory_nodes[].updated_at`        | integer | 更新时间                |
| `memory_nodes[].media_desc`        | string  | 多模态信息描述             |
| `memory_nodes[].meta_data`         | object  | 自定义元信息              |
| `memory_nodes[].project_id`        | string  | 添加或更新时指定的项目 ID，可能为空 |
| `memory_nodes[].memory_type`       | string  | 记忆节点类型              |
| `memory_nodes[].status`            | string  | 记忆节点状态              |
| `memory_nodes[].multimodal_medias` | array   | 多模态资源，存在时返回         |
| `page_num`                         | integer | 当前页码                |
| `page_size`                        | integer | 每页数量                |
| `total`                            | integer | 记忆节点总数              |

## 请求示例

<CodeGroup>
  ```bash cURL
  curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes?user_id=user_001&memory_library_id=memory_library_001&project_id=project_001&page_size=10&page_num=1' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
  ```

  ```python Python
  from agentscope_runtime.tools.modelstudio_memory import (
      ListMemory, ListMemoryInput,
  )
  import asyncio

  async def main():
      list_memory = ListMemory()
      try:
          result = await list_memory.arun(ListMemoryInput(
              user_id="user_001",
              page_size=10,
              page_num=1
          ))
          for node in result.memory_nodes:
              print(f"记忆: {node.content}")
      finally:
          await list_memory.close()

  asyncio.run(main())
  ```
</CodeGroup>

## 响应示例

```json
{
  "request_id": "c04d36e2-8fe8-9aad-b7ae-be40d5852c35",
  "memory_nodes": [
    {
      "memory_node_id": "4fe7ea3b791a4453b5e1d117da6b7483",
      "content": "用户后天要坐汽车去上海出差，记得带身份证",
      "timestamp": 1781765684,
      "created_at": 1781765715,
      "updated_at": 1781765715,
      "media_desc": "",
      "meta_data": {},
      "project_id": "project_001",
      "memory_type": "observation",
      "status": "valid",
      "multimodal_medias": []
    }
  ],
  "page_num": 1,
  "page_size": 10,
  "total": 1
}
```

<Tip>
  修改记忆使用 [UpdateMemory](/zh/model-studio/memory/update-memory)。
</Tip>
