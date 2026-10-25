> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询记忆节点

> 查询单个记忆节点的详细信息

<meta property="og:title" content="查询记忆节点" />

查询指定记忆节点的详细信息。

## 请求方法与路径

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes/{memory_node_id}`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数               | 类型     | 必填 | 位置   | 说明      |
| ---------------- | ------ | -- | ---- | ------- |
| `memory_node_id` | string | 是  | Path | 记忆节点 ID |

## 返回结果

| 字段                              | 类型      | 说明           |
| ------------------------------- | ------- | ------------ |
| `request_id`                    | string  | 请求 ID        |
| `memory_node`                   | object  | 记忆节点详情       |
| `memory_node.memory_node_id`    | string  | 记忆节点 ID      |
| `memory_node.content`           | string  | 记忆内容         |
| `memory_node.timestamp`         | integer | 消息时间戳        |
| `memory_node.created_at`        | integer | 创建时间         |
| `memory_node.updated_at`        | integer | 更新时间         |
| `memory_node.media_desc`        | string  | 多模态信息描述      |
| `memory_node.meta_data`         | object  | 自定义元信息       |
| `memory_node.memory_type`       | string  | 记忆类型         |
| `memory_node.status`            | string  | 记忆状态         |
| `memory_node.project_id`        | string  | 项目 ID        |
| `memory_node.media_urls`        | array   | 多模态资源 URL 列表 |
| `memory_node.multimodal_medias` | array   | 多模态资源，存在时返回  |

## 请求示例

```bash cURL
curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/memory_nodes/42dfc089dfa7409889966960a95c3b7e' \
  --header "Authorization: Bearer $DASHSCOPE_API_KEY"
```

## 响应示例

```json
{
  "request_id": "c7d1c4d2-1234-5678-90ab-6f2b4381abcd",
  "memory_node": {
    "memory_node_id": "42dfc089dfa7409889966960a95c3b7e",
    "content": "用户需要被提醒明天穿衣服",
    "timestamp": 1781798400,
    "created_at": 1781765139,
    "updated_at": 1781765139,
    "media_desc": "",
    "meta_data": {},
    "memory_type": "observation",
    "status": "valid",
    "project_id": "project_001",
    "media_urls": [],
    "multimodal_medias": []
  }
}
```
