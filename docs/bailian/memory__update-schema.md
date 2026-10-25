> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 更新画像模板

> 更新画像模板的名称、描述或策略版本

<meta property="og:title" content="更新画像模板" />

更新画像模板。修改 `plan_version` 后，新写入的记忆遵循新策略版本规则，已写入的记忆不受影响。

请求体必须至少包含 `name`、`description`、`plan_version`、`extract_scene` 或 `attributes_operations` 中的一项。

## 请求方法与路径

`PATCH https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas/{profile_schema_id}`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                                      | 类型     | 必填   | 位置   | 说明                                  |
| --------------------------------------- | ------ | ---- | ---- | ----------------------------------- |
| `profile_schema_id`                     | string | 是    | Path | 画像模板 ID                             |
| `name`                                  | string | 否    | Body | 更新后的模板名称，最多 32 个字符                  |
| `description`                           | string | 否    | Body | 更新后的模板描述                            |
| `plan_version`                          | string | 否    | Body | 收费计划：`pro` 或 `lite`                 |
| `extract_scene`                         | string | 否    | Body | 提取场景：`efficient` 或 `intelligent`    |
| `attributes_operations`                 | array  | 否    | Body | 属性变更操作列表                            |
| `attributes_operations[].op`            | string | 是    | Body | 操作类型：`add`、`update` 或 `delete`      |
| `attributes_operations[].name`          | string | 条件必填 | Body | 属性名称；`op=add` 时必填，`op=update` 时可选   |
| `attributes_operations[].description`   | string | 否    | Body | 属性描述；适用于 `add` 和 `update`           |
| `attributes_operations[].default_value` | string | 否    | Body | 属性默认值；`op=update` 时可更新              |
| `attributes_operations[].attribute_id`  | string | 条件必填 | Body | 属性 ID；`op=update` 或 `op=delete` 时必填 |
| `memory_library_id`                     | string | 否    | Body | 记忆库 ID                              |

## 代码示例

```bash cURL
curl -X PATCH "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas/{profile_schema_id}" \
  --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
  --header "Content-Type: application/json" \
  --data '{
    "name": "更新后的用户基础画像",
    "description": "包含用户的稳定属性和偏好信息",
    "plan_version": "lite",
    "extract_scene": "intelligent",
    "attributes_operations": [
      {"op": "add", "name": "喜欢的音乐", "description": "用户喜欢的音乐类型"},
      {"op": "update", "attribute_id": "attr_002", "name": "常用运动", "default_value": "跑步"},
      {"op": "delete", "attribute_id": "attr_003"}
    ]
  }'
```

## 响应示例

```json
{
  "request_id": "813e8d62-062c-97d5-877b-e10f356ca7d4"
}
```
