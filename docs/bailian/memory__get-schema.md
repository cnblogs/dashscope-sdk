> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取画像模板

> 查看指定画像模板的字段定义与配置详情

<meta property="og:title" content="获取画像模板" />

获取指定画像模板的详情，包含字段定义和配置信息。

## 请求方法与路径

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas/{profile_schema_id}`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                  | 类型     | 必填 | 位置    | 说明      |
| ------------------- | ------ | -- | ----- | ------- |
| `profile_schema_id` | string | 是  | Path  | 画像模板 ID |
| `memory_library_id` | string | 否  | Query | 记忆库 ID  |

## 返回结果

| 字段                          | 类型     | 说明     |
| --------------------------- | ------ | ------ |
| `request_id`                | string | 请求 ID  |
| `name`                      | string | 画像模板名称 |
| `description`               | string | 画像模板描述 |
| `plan_version`              | string | 收费计划   |
| `extract_scene`             | string | 提取场景   |
| `attributes`                | array  | 画像属性列表 |
| `attributes[].attribute_id` | string | 属性 ID  |
| `attributes[].name`         | string | 属性名称   |
| `attributes[].description`  | string | 属性描述   |

## 代码示例

```bash cURL
curl -X GET "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas/{profile_schema_id}" \
  --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
  --header "Content-Type: application/json"
```

## 响应示例

```json
{
  "attributes": [
    {"attribute_id": "04eb06f5f04a437b82d07ceb088103f9", "description": "用户的兴趣爱好", "name": "爱好"},
    {"attribute_id": "2eff4652a6bc47b0803635033b1e08dd", "description": "用户职业", "name": "职业"},
    {"attribute_id": "bb28e6cd522443fc9bf7e75b1377bf9d", "description": "用户年龄", "name": "年龄"}
  ],
  "description": "包含年龄和兴趣的用户信息",
  "extract_scene": "efficient",
  "name": "用户基础画像",
  "plan_version": "pro",
  "request_id": "5eb49009-6782-95f4-85da-c5f20d288d9a"
}
```

<Tip>
  更新模板使用 [UpdateProfileSchema](/zh/model-studio/memory/update-schema)。
</Tip>
