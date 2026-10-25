> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 创建画像模板

> 创建用户画像模板，定义画像字段

<meta property="og:title" content="创建画像模板" />

创建用户画像模板，定义画像包含的字段及描述。

## 请求方法与路径

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                         | 类型     | 必填 | 位置   | 说明                                              |
| -------------------------- | ------ | -- | ---- | ----------------------------------------------- |
| `name`                     | string | 是  | Body | 画像模板名称，最多 32 个字符                                |
| `description`              | string | 否  | Body | 画像模板描述                                          |
| `plan_version`             | string | 否  | Body | 收费计划：`pro` 或 `lite`                             |
| `extract_scene`            | string | 否  | Body | 提取场景：`efficient` 或 `intelligent`，默认 `efficient` |
| `attributes`               | array  | 是  | Body | 画像属性定义列表，不能为空数组                                 |
| `attributes[].name`        | string | 是  | Body | 属性名称                                            |
| `attributes[].description` | string | 否  | Body | 属性描述                                            |
| `memory_library_id`        | string | 否  | Body | 记忆库 ID，不传则使用默认记忆库                               |

## 代码示例

<CodeGroup>
  ```bash cURL
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header "Content-Type: application/json" \
    --data '{
      "name": "用户基础画像",
      "description": "包含年龄和兴趣的用户信息",
      "extract_scene": "efficient",
      "attributes": [
        {"name": "年龄", "description": "用户年龄"},
        {"name": "爱好", "description": "用户的兴趣爱好"},
        {"name": "职业", "description": "用户职业"}
      ]
    }'
  ```

  ```python Python
  from agentscope_runtime.tools.modelstudio_memory import (
      CreateProfileSchema, CreateProfileSchemaInput, ProfileAttribute,
  )
  import asyncio

  async def main():
      create_schema = CreateProfileSchema()
      try:
          result = await create_schema.arun(CreateProfileSchemaInput(
              name="用户基础画像",
              description="包含年龄和兴趣的用户信息",
              attributes=[
                  ProfileAttribute(name="年龄", description="用户年龄"),
                  ProfileAttribute(name="爱好", description="用户的兴趣爱好"),
                  ProfileAttribute(name="职业", description="用户职业"),
              ]
          ))
          print(f"Schema ID: {result.profile_schema_id}")
      finally:
          await create_schema.close()

  asyncio.run(main())
  ```
</CodeGroup>

## 响应示例

```json
{
  "profile_schema_id": "50edf54eaa5842e891d959ae115205be",
  "request_id": "f190a540-814e-9c1c-873e-0661851c8fca"
}
```

<Tip>
  创建后，使用 [AddMemory](/zh/model-studio/memory/add-memory) 传入 `profile_schema` 提取画像，再用 [GetUserProfile](/zh/model-studio/memory/get-user-profile) 获取。
</Tip>
